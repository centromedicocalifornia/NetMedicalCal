using Sigesoft.Node.WinClient.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.UI.Reports;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using System.Data;
using System.Threading.Tasks;
using Utils = Sigesoft.Node.WinClient.BLL.Utils;

//using SAMBHS.Common.Resource;


namespace Sigesoft.Node.Contasol.Integration
{
    public partial class frmRecetaMedica : Form
    {
        private string _serviceId;
        private OperationResult _pobjOperationResult;
        private readonly RecetaBl _objRecetaBl;
        private readonly List<DiagnosticRepositoryList> _listDiagnosticRepositoryLists;
        private string _protocolId;
        private int _usuariocrea;
        private string _userId;
        private string _username;
        private OperationResult _objOperationResult;
        private List<recetadespachoDto> _dataReporte;


        public frmRecetaMedica(List<DiagnosticRepositoryList> ListaDX, string serviceId, string protocolId, int usuario, string userId, string username)
        {
            _serviceId = serviceId;
            InitializeComponent();
            _objRecetaBl = new RecetaBl();
            _pobjOperationResult = new OperationResult();
            _listDiagnosticRepositoryLists = ListaDX;
            _usuariocrea = usuario;
            _userId = userId;
            _username = username;
            _protocolId = protocolId;
            
        }

        private void GetData(List<DiagnosticRepositoryList> ListaDX)
        {
           
            try
            {
                ListaDX.ForEach(l => l.RecipeDetail = new List<recetaDto>());
                var data = _objRecetaBl.GetHierarchycalData(ref _pobjOperationResult, ListaDX);
                
                if (data.Any())
                {
                    var previousIndex = grdTotalDiagnosticos.ActiveRow != null ? grdTotalDiagnosticos.ActiveRow.Index : 0;
                    grdTotalDiagnosticos.DataSource = data;
                    grdTotalDiagnosticos.Rows.Refresh(RefreshRow.ReloadData);
                    grdTotalDiagnosticos.Rows[previousIndex].Activate();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, @"GetData()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MedicinaReceta(string serviceId) {

            var data = _objRecetaBl.GetHierarchycalData(ref _pobjOperationResult, _listDiagnosticRepositoryLists);

            if (data.Any())
            {
                var previousIndex = grdTotalDiagnosticos.ActiveRow != null ? grdTotalDiagnosticos.ActiveRow.Index : 0;
                grdTotalDiagnosticos.DataSource = data;
                grdTotalDiagnosticos.Rows.Refresh(RefreshRow.ReloadData);
                grdTotalDiagnosticos.Rows[previousIndex].Activate();
            }
        }

        private void frmRecetaMedica_Load(object sender, EventArgs e)
        {
            GetData(_listDiagnosticRepositoryLists);
        }

        private void grdTotalDiagnosticos_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private void grdTotalDiagnosticos_ClickCellButton(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            try
            {
                if (e.Cell == null || e.Cell.Row.Cells["v_DiagnosticRepositoryId"].Value == null) return;
                var diagnosticRepositoryId = e.Cell.Row.Cells["v_DiagnosticRepositoryId"].Value.ToString();
                
                #region Conexion SIGESOFT verificar la unidad productiva del componente
                ConexionSigesoft conectasam = new ConexionSigesoft();
                conectasam.opensigesoft();
                var cadena1 = "select CP.v_IdUnidadProductiva " +
                              "from diagnosticrepository DR " +
                              "inner join component CP on DR.v_ComponentId=CP.v_ComponentId "+
                              "where DR.v_DiagnosticRepositoryId='"+diagnosticRepositoryId+"' ";
                SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                SqlDataReader lector = comando.ExecuteReader();
                string LineId = "";
                while (lector.Read())
                {
                    LineId = lector.GetValue(0).ToString();
                }
                lector.Close();
                conectasam.closesigesoft();
                #endregion
                int contador = 0;
                #region Verificar si existe receta por Dx
                ConexionSigesoft conectasam2 = new ConexionSigesoft();
                conectasam2.opensigesoft();
                var cadena2 = "select COUNT(v_ReceipId) " +
                              "from receipHeader " +
                              "where v_diaxrepositoryId='" + diagnosticRepositoryId + "' ";
                SqlCommand comando2 = new SqlCommand(cadena2, connection: conectasam2.conectarsigesoft);
                SqlDataReader lector2 = comando2.ExecuteReader();

                while (lector2.Read())
                {
                    contador = int.Parse(lector2.GetValue(0).ToString());
                }
                lector.Close();
                conectasam.closesigesoft();
                #endregion

                #region Verificar si existe receta por Dx
                ConexionSigesoft conectasam3 = new ConexionSigesoft();
                conectasam3.opensigesoft();
                var cadena3 = "select TOP 1 v_ReceipId " +
                              "from receipHeader " +
                              "where v_diaxrepositoryId='" + diagnosticRepositoryId + "' ";
                SqlCommand comando3 = new SqlCommand(cadena3, connection: conectasam3.conectarsigesoft);
                SqlDataReader lector3 = comando3.ExecuteReader();
                string id_receta = "";
                while (lector3.Read())
                {
                    id_receta = lector3.GetValue(0).ToString();
                }
                lector.Close();
                conectasam.closesigesoft();
                #endregion


                if (contador == 0)
                {
                   id_receta =  new PacientBL().AddRecipeNEwsByServiceId(_serviceId, 0, 0,_usuariocrea, diagnosticRepositoryId, _userId, _username);

                }
                switch (e.Cell.Column.Key)
                {
                    case "_AddRecipe":
                    {
                        var categoryName = e.Cell.Row.Cells["v_ComponentName"].Value.ToString();

                        var f = new frmAddRecipe(ActionForm.Add, diagnosticRepositoryId, 0, _protocolId, _serviceId, categoryName, LineId, id_receta, _userId, _usuariocrea) { StartPosition = FormStartPosition.CenterScreen };
                        f.ShowDialog();
                        GetData(_listDiagnosticRepositoryLists);
                    }
                        break;

                    case "_EditRecipe":
                    {
                        var recipeId = int.Parse(e.Cell.Row.Cells["i_IdReceta"].Value.ToString());
                        var f = new frmAddRecipe(ActionForm.Edit, diagnosticRepositoryId, recipeId, _protocolId, _serviceId, "", LineId, id_receta, _userId, _usuariocrea) { StartPosition = FormStartPosition.CenterScreen };
                        f.ShowDialog();
                        GetData(_listDiagnosticRepositoryLists);
                    }
                        break;

                    case "_DeleteRecipe":
                    {
                        _pobjOperationResult = new OperationResult();
                        var recipeId = int.Parse(e.Cell.Row.Cells["i_IdReceta"].Value.ToString());
                        var msg = MessageBox.Show(@"¿Seguro de eliminar esta receta?", @"Confirmación",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (msg == DialogResult.No) return;

                        #region Verificar si existe receta por Dx
                        ConexionSigesoft conectasam4 = new ConexionSigesoft();
                        conectasam4.opensigesoft();
                        var cadena4 = "UPDATE receta set i_IsDeleted = 1 where i_IdReceta ='" + recipeId + "' ";
                        SqlCommand comando4 = new SqlCommand(cadena4, connection: conectasam4.conectarsigesoft);
                        SqlDataReader lector4 = comando4.ExecuteReader();
                        lector.Close();
                        conectasam.closesigesoft();
                        #endregion

                        //_objRecetaBl.DeleteRecipe(ref _pobjOperationResult, recipeId);
                        //if (_pobjOperationResult.Success == 0)
                        //{
                        //    MessageBox.Show(_pobjOperationResult.ErrorMessage, @"Error", MessageBoxButtons.OK,
                        //        MessageBoxIcon.Error);
                        //    return;
                        //}

                        GetData(_listDiagnosticRepositoryLists);
                    }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"grdTotalDiagnosticos_ClickCellButton()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdTotalDiagnosticos.Selected.Rows.Count == 0)
                {
                    if (chkConsolidar.Checked == true)
                    {
                        CargarConsolidado();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    //var f = new frmReporteReceta(_serviceId, recomendaciones, restricciones, diagnosticRepositoryId);
                    //f.ShowDialog();
                    if (chkConsolidar.Checked == true)
                    {
                        CargarConsolidado();
                    }
                    else
                    {
                        Cargar();
                    }
                }
            }
            catch (Exception exception)
            {
                if (grdTotalDiagnosticos.Selected.Rows.Count == 0)
                {
                    if (chkConsolidar.Checked == true)
                    {
                        CargarConsolidado();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    //var f = new frmReporteReceta(_serviceId, recomendaciones, restricciones, diagnosticRepositoryId);
                    //f.ShowDialog();
                    if (chkConsolidar.Checked == true)
                    {
                        CargarConsolidado();
                    }
                    else
                    {
                        Cargar();
                    }
                }
            }
            
        }

        private void Cargar()
        {
            try
            {
                var _v_DiagnosticRepositoryId = grdTotalDiagnosticos.Selected.Rows[0].Cells["v_DiagnosticRepositoryId"].Value.ToString();

                var _recomendaciones = string.Join("\n-", _listDiagnosticRepositoryLists.Where(o => !string.IsNullOrWhiteSpace(o.v_RecomendationsName) && o.v_DiagnosticRepositoryId == _v_DiagnosticRepositoryId).Select(p => p.v_RecomendationsName).Distinct()).Trim();
                var _restricciones = string.Join("\n-", _listDiagnosticRepositoryLists.Where(o => !string.IsNullOrWhiteSpace(o.v_RestrictionsName) && o.v_DiagnosticRepositoryId == _v_DiagnosticRepositoryId).Select(p => p.v_RestrictionsName).Distinct()).Trim();

                var objRecetaBl = new RecetaBl();
                var _ruta = Common.Utils.GetApplicationConfigValue("Receta").ToString();
                DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
                _objOperationResult = new OperationResult();

                //Task.Factory.StartNew(() =>
                //{
                    _dataReporte = objRecetaBl.GetRecetaToReport(ref _objOperationResult, _serviceId, _v_DiagnosticRepositoryId);

                //}, TaskCreationOptions.LongRunning).ContinueWith(t =>
                //{
                    if (_objOperationResult.Success == 0)
                    {
                        MessageBox.Show(_objOperationResult.ErrorMessage, @"Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                    var rp = new crRecetaPresentacion();
                    var ds = new DataSet();
                    var dsReport = Utils.ConvertToDatatable(_dataReporte);
                    ds.Tables.Add(dsReport);
                    ds.Tables[0].TableName = "dsReporteReceta";
                    rp.SetDataSource(dsReport);
                    rp.SetParameterValue("_Recomendaciones", _recomendaciones);
                    rp.SetParameterValue("_Restricciones", _restricciones);
                    crystalReportViewer1.ReportSource = rp;

                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = _ruta + _serviceId + "-" + _v_DiagnosticRepositoryId + ".pdf";
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    rp.Dispose();
                //},
                //TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Cargar();
            }
        }
        private void CargarConsolidado()
        {
            try
            {
                var _recomendaciones = string.Join("\n-", _listDiagnosticRepositoryLists.Where(o => !string.IsNullOrWhiteSpace(o.v_RecomendationsName)  ).Select(p => p.v_RecomendationsName).Distinct()).Trim();
                var _restricciones = string.Join("\n-", _listDiagnosticRepositoryLists.Where(o => !string.IsNullOrWhiteSpace(o.v_RestrictionsName)).Select(p => p.v_RestrictionsName).Distinct()).Trim();

                var objRecetaBl = new RecetaBl();
                var _ruta = Common.Utils.GetApplicationConfigValue("Receta").ToString();
                DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
                _objOperationResult = new OperationResult();

                Task.Factory.StartNew(() =>
                {
                    _dataReporte = objRecetaBl.GetRecetaToReportConsolidado(ref _objOperationResult, _serviceId);

                }, TaskCreationOptions.LongRunning).ContinueWith(t =>
                {
                    List<string> Dx = new List<string>();

                    foreach (var item in _dataReporte)
                    {
                        Dx.Add(item.Cie10);
                    }

                    List<string> DxNew = Dx.Distinct().ToList();

                    foreach (var item in DxNew)
                    {
                        _recomendaciones = string.Join("\n-", _listDiagnosticRepositoryLists.Where(o => !string.IsNullOrWhiteSpace(o.v_RecomendationsName) && o.v_DiagnosticRepositoryId == item).Select(p => p.v_RecomendationsName).Distinct()).Trim();
                        _restricciones = string.Join("\n-", _listDiagnosticRepositoryLists.Where(o => !string.IsNullOrWhiteSpace(o.v_RestrictionsName) && o.v_DiagnosticRepositoryId == item).Select(p => p.v_RestrictionsName).Distinct()).Trim();
                    }

                    if (_objOperationResult.Success == 0)
                    {
                        MessageBox.Show(_objOperationResult.ErrorMessage, @"Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                    var rp = new crRecetaPresentacion();
                    var ds = new DataSet();
                    var dsReport = Utils.ConvertToDatatable(_dataReporte);
                    ds.Tables.Add(dsReport);
                    ds.Tables[0].TableName = "dsReporteReceta";
                    rp.SetDataSource(dsReport);
                    rp.SetParameterValue("_Recomendaciones", _recomendaciones);
                    rp.SetParameterValue("_Restricciones", _restricciones);
                    crystalReportViewer1.ReportSource = rp;

                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = _ruta + _serviceId + " - Consolidado"   + ".pdf";
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    rp.Dispose();
                },
                TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                CargarConsolidado();
            }
        }


        private void ultraButton2_Click(object sender, EventArgs e)
        {
            if (grdTotalDiagnosticos.Selected.Rows.Count == 0)
            {

                return;
            }
            else
            {
                var diagnosticRepositoryId = grdTotalDiagnosticos.Selected.Rows[0].Cells["v_DiagnosticRepositoryId"].Value.ToString();

                var f = new frmConfirmarDespacho(_serviceId, diagnosticRepositoryId);
                f.ShowDialog();
            }

        }

        private void grdTotalDiagnosticos_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            try
            {
                if (e.Cell == null || e.Cell.Row.Cells["v_DiagnosticRepositoryId"].Value == null) return;
                var diagnosticRepositoryId = e.Cell.Row.Cells["v_DiagnosticRepositoryId"].Value.ToString();

                #region Conexion SIGESOFT verificar la unidad productiva del componente
                ConexionSigesoft conectasam = new ConexionSigesoft();
                conectasam.opensigesoft();
                var cadena1 = "select CP.v_IdUnidadProductiva " +
                              "from diagnosticrepository DR " +
                              "inner join component CP on DR.v_ComponentId=CP.v_ComponentId " +
                              "where DR.v_DiagnosticRepositoryId='" + diagnosticRepositoryId + "' ";
                SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                SqlDataReader lector = comando.ExecuteReader();
                string LineId = "";
                while (lector.Read())
                {
                    LineId = lector.GetValue(0).ToString();
                }
                lector.Close();
                conectasam.closesigesoft();
                #endregion
                //int contador = 0;
                //#region Verificar si existe receta por Dx
                //ConexionSigesoft conectasam2 = new ConexionSigesoft();
                //conectasam2.opensigesoft();
                //var cadena2 = "select COUNT(v_ReceipId) " +
                //              "from receipHeader " +
                //              "where v_diaxrepositoryId='" + diagnosticRepositoryId + "' ";
                //SqlCommand comando2 = new SqlCommand(cadena2, connection: conectasam2.conectarsigesoft);
                //SqlDataReader lector2 = comando2.ExecuteReader();

                //while (lector2.Read())
                //{
                //    contador = int.Parse(lector2.GetValue(0).ToString());
                //}
                //lector.Close();
                //conectasam.closesigesoft();
                //#endregion

                //#region Verificar si existe receta por Dx
                //ConexionSigesoft conectasam3 = new ConexionSigesoft();
                //conectasam3.opensigesoft();
                //var cadena3 = "select TOP 1 v_ReceipId " +
                //              "from receipHeader " +
                //              "where v_diaxrepositoryId='" + diagnosticRepositoryId + "' ";
                //SqlCommand comando3 = new SqlCommand(cadena3, connection: conectasam3.conectarsigesoft);
                //SqlDataReader lector3 = comando3.ExecuteReader();
                string id_receta = "";
                //while (lector3.Read())
                //{
                //    id_receta = lector3.GetValue(0).ToString();
                //}
                //lector.Close();
                //conectasam.closesigesoft();
                //#endregion


                //if (contador == 0)
                //{
                id_receta = new PacientBL().AddRecipeNEwsByServiceId(_serviceId, 0, 0, _usuariocrea, diagnosticRepositoryId, _userId, _username);

                //}
                //switch (e.Cell.Column.Key)
                //{
                //    case "_AddRecipe":
                //        {
                            var categoryName = e.Cell.Row.Cells["v_ComponentName"].Value.ToString();

                            var f = new frmAddRecipe(ActionForm.Add, diagnosticRepositoryId, 0, _protocolId, _serviceId, categoryName, LineId, id_receta, _userId, _usuariocrea) { StartPosition = FormStartPosition.CenterScreen };
                            f.ShowDialog();
                            GetData(_listDiagnosticRepositoryLists);
                //        }
                //        break;

                //    case "_EditRecipe":
                //        {
                //            var recipeId = int.Parse(e.Cell.Row.Cells["i_IdReceta"].Value.ToString());
                //            var f = new frmAddRecipe(ActionForm.Edit, diagnosticRepositoryId, recipeId, _protocolId, _serviceId, "", LineId, id_receta, _userId, _usuariocrea) { StartPosition = FormStartPosition.CenterScreen };
                //            f.ShowDialog();
                //            GetData(_listDiagnosticRepositoryLists);
                //        }
                //        break;

                //    case "_DeleteRecipe":
                //        {
                //            _pobjOperationResult = new OperationResult();
                //            var recipeId = int.Parse(e.Cell.Row.Cells["i_IdReceta"].Value.ToString());
                //            var msg = MessageBox.Show(@"¿Seguro de eliminar esta receta?", @"Confirmación",
                //                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //            if (msg == DialogResult.No) return;

                //            #region Verificar si existe receta por Dx
                //            ConexionSigesoft conectasam4 = new ConexionSigesoft();
                //            conectasam4.opensigesoft();
                //            var cadena4 = "UPDATE receta set i_IsDeleted = 1 where i_IdReceta ='" + recipeId + "' ";
                //            SqlCommand comando4 = new SqlCommand(cadena4, connection: conectasam4.conectarsigesoft);
                //            SqlDataReader lector4 = comando4.ExecuteReader();
                //            lector.Close();
                //            conectasam.closesigesoft();
                //            #endregion

                //            GetData(_listDiagnosticRepositoryLists);
                //        }
                //        break;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"grdTotalDiagnosticos_ClickCellButton()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
