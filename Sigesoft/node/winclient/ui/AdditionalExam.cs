using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BE.Custom;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.UI.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Constants = Sigesoft.Common.Constants;
using System.Transactions;
using System.IO;
using NetPdf;
//using iTextSharp.text.pdf;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class AdditionalExam : Form
    {
        ServiceBL objServiceBL = new ServiceBL();
        private string _protocolId;
        private string _serviceId;
        private string _personId;
        AdditionalExamBL _additionalExamBl = new AdditionalExamBL();
        List<string> _ListaComponentes = null;
        public List<ServiceComponentList> _auxiliaryExams = null;
        private MergeExPDF _mergeExPDF = new MergeExPDF();
        #region Properties

        private string MedicalExamId { get; set; }
        private string MedicalExamName { get; set; }
        private string CategoryName { get; set; }
        List<Categoria> ListServiceComponent = new List<Categoria>();
        List<Categoria> ListServiceComponentTemp = new List<Categoria>();

        #endregion

        private string N_Orden;

        public AdditionalExam(List<string> ListaComponentes, string protocolId, string serviceId, string personId, string N_Orden_)
        {
            _ListaComponentes = ListaComponentes;
            _serviceId = serviceId;
            _protocolId = protocolId;
            _personId = personId;
            N_Orden = N_Orden_;
            InitializeComponent();
        }

        private void AdditionalExam_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            ListServiceComponent = objServiceBL.GetAllComponentsNew(ref objOperationResult, null, "");
            //grdDataServiceComponent.DataSource = ListServiceComponent;
            gdDataExamsNew.DataSource = ListServiceComponent;
            lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", ListServiceComponent.Count());
            #region Conexion SIGESOFT Obtener nombre del protocolo

            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena1 = "select PR.v_Name, OO.v_Name " +
                          "from protocol PR " +
                          "inner join [dbo].[plan] PL on PR.v_ProtocolId=PL.v_ProtocoloId " +
                          "inner join organization OO on PL.v_OrganizationSeguroId=OO.v_OrganizationId " +
                          "where v_ProtocolId='" + _protocolId + "' " +
                          "group by PR.v_Name, OO.v_Name ";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();

            string protocolName = "";
            string aseguradoraName = "";

            while (lector.Read())
            {
                protocolName = lector.GetValue(0).ToString();
                aseguradoraName = lector.GetValue(1).ToString();
            }

            lector.Close();
            conectasam.closesigesoft();

            #endregion
        }

        private void btnAgregarExamenAuxiliar_Click(object sender, EventArgs e)
        {
            AddAuxiliaryExam();   
        }


        private void AddAuxiliaryExam()
        {
            if (MedicalExamId != null)
            {
                var findResult = lvExamenesSeleccionados.FindItemWithText(MedicalExamId);
                string IsProcessed = "0";
                string IsNewService = "0";
                // El examen ya esta agregado
                if (findResult != null)
                {
                    MessageBox.Show("Por favor seleccione otro examen.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var res = _ListaComponentes.Find(p => p == MedicalExamId);
                if (res != null)
                {
                    var DialogResult = MessageBox.Show("Este examen ya se encuentra agregado, ¿Desea crear nuevo servicio?", "Error de validación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (DialogResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        IsNewService = "1";
                    }
                    else
                    {
                        return;
                    }

                }


                var row = new ListViewItem(new[] { MedicalExamName, MedicalExamId, IsProcessed, IsNewService });

                lvExamenesSeleccionados.Items.Add(row);

                gbExamenesSeleccionados.Text = string.Format("Examenes Seleccionados {0}", lvExamenesSeleccionados.Items.Count);
                
                MedicalExamId = null;
            }
            else return;
        }

        private void gdDataExams_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            bool row = gdDataExams.Selected.Rows.Count > 0;
            if (!row)
            {
                return;
            }
            if (gdDataExams.Selected.Rows[0].Cells["v_ComponentId"].Value == null)
            {
                btnAgregarExamenAuxiliar.Enabled = false;

                return;
            }
            else
            {
                btnAgregarExamenAuxiliar.Enabled = true;
            }

            lvExamenesSeleccionados.SelectedItems.Clear();

            if (gdDataExams.Selected.Rows.Count == 0)
                return;

            MedicalExamId = gdDataExams.Selected.Rows[0].Cells["v_ComponentId"].Value.ToString();

            MedicalExamName = gdDataExams.Selected.Rows[0].Cells["v_ComponentName"].Value.ToString();
            //ServiceComponentConcatId = gdDataExams.Selected.Rows[0].Cells["v_ServiceComponentId"].Value.ToString();

            if (gdDataExams.Selected.Rows[0].Cells["v_ComponentId"].Value != null)
            {
                MedicalExamName = gdDataExams.Selected.Rows[0].Cells["v_ComponentName"].Value.ToString();
            }
            else
            {
                MedicalExamName = string.Empty;
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            FilterComponents();
        }

        private void FilterComponents()
        {
            OperationResult objOperationResult = new OperationResult();
            int? busqueda = null;
            if (rbNombreCategoria.Checked)
            {
                busqueda = (int)TipoBusqueda.NombreCategoria;
            }
            else if (rbNombreSubCategoria.Checked)
            {
                busqueda = (int)TipoBusqueda.NombreSubCategoria;
            }
            else if (rbNombreComponente.Checked)
            {
                busqueda = (int)TipoBusqueda.NombreComponent;
            }
            else if (rbPorCodigoSegus.Checked)
            {
                busqueda = (int)TipoBusqueda.CodigoSegus;
            }

            var ListServiceComponent = objServiceBL.GetAllComponentsNew(ref objOperationResult, busqueda, txtFiltro.Text);

            gdDataExamsNew.DataSource = ListServiceComponent;

            lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", ListServiceComponent.Count());
        }

        private void txtFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                FilterComponents();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //if (_auxiliaryExams == null)
            //    _auxiliaryExams = new List<ServiceComponentList>();
            try
            {
                var ruta = Common.Utils.GetApplicationConfigValue("rutaExamenesAdicionales").ToString();
                var rutaBasura = Common.Utils.GetApplicationConfigValue("rutaReportesBasura").ToString();
                string pathFile = "";
                string CMP = "";
                var openFile = false;
                using (new LoadingClass.PleaseWait(this.Location, "Cargando..."))
                {
                    OperationResult objOperationResult = new OperationResult();
                    List<AdditionalExamCustom> ListAdditionalExam = new List<AdditionalExamCustom>();
                    
                    foreach (ListViewItem item in lvExamenesSeleccionados.Items)
                    {

                        AdditionalExamCustom _additionalExam = new AdditionalExamCustom();
                        var fields = item.SubItems;
                        if (fields[1].Text != "")
                        {
                            _additionalExam.ComponentId = fields[1].Text;
                            _additionalExam.IsProcessed = int.Parse(fields[2].Text);
                            _additionalExam.IsNewService = int.Parse(fields[3].Text);
                            _additionalExam.Commentary = txtCommentary.Text;
                            _additionalExam.ServiceId = _serviceId;
                            _additionalExam.PersonId = _personId;
                            _additionalExam.ProtocolId = "";
                            _additionalExam.v_IdGrupoAdicional = N_Orden;
                        }
                        else
                            return;
                       

                        if (_additionalExam.IsNewService == 1)
                        {
                            _additionalExam.ProtocolId = Constants.Prot_Hospi_Adic;
                        }
                        ListAdditionalExam.Add(_additionalExam);

                    }


                    var datosGrabo = objServiceBL.DevolverDatosUsuarioFirma(Globals.ClientSession.i_SystemUserId);
                    

                    if (datosGrabo != null)
                    {
                        if (datosGrabo.CMP != null)
                        {
                            CMP = datosGrabo.CMP;
                            pathFile = string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + "ORDEN-EX-MED-ADICI-" + datosGrabo.CMP));
                        }

                    }

                    string idAdicional = "";
                    using (var ts = new TransactionScope())
                    {
                        idAdicional = _additionalExamBl.AddAdditionalExam(ListAdditionalExam, Globals.ClientSession.GetAsList());
                        if (idAdicional == string.Empty)
                        {
                            throw new Exception("Sucedió un error, por favor vuelva a intentarlo");
                            return;
                        }

                        ts.Complete();
                    }
                    List<Categoria> AdditionalExam = new List<Categoria>();
                    List<Categoria> DataSource = new List<Categoria>();
                    List<string> ComponentList = new List<string>();

                    //var ListadditExam = new AdditionalExamBL().GetAdditionalExamByServiceId_all(_serviceId, Globals.ClientSession.i_SystemUserId);
                    var ListadditExam = new AdditionalExamBL().GetAdditionalExamByServiceId_Por_Orden(_serviceId, Globals.ClientSession.i_SystemUserId, idAdicional);

                    foreach (var componenyId in ListadditExam)
                    {
                        ComponentList.Add(componenyId.ComponentId);
                    }

                    foreach (var componentId in ComponentList)
                    {
                        var ListServiceComponent = new ServiceBL().GetAllComponents(ref objOperationResult, (int)TipoBusqueda.ComponentId, componentId);

                        Categoria categoria = DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId);
                        if (categoria != null)
                        {
                            List<ComponentDetailList> componentDetail = new List<ComponentDetailList>();
                            componentDetail = ListServiceComponent[0].Componentes;
                            DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId).Componentes.AddRange(componentDetail);
                        }
                        else
                        {
                            DataSource.AddRange(ListServiceComponent);
                        }
                    }

                    
                    var MedicalCenter = objServiceBL.GetInfoMedicalCenter();
                    var DatosPaciente = new PacientBL().DevolverDatosPaciente(_serviceId);

                    var Edad = DiferenciaFechas(DatosPaciente.FechaServicio.Value, DatosPaciente.d_Birthdate.Value);


                    new PrintAdditionalExam().GenerateAdditionalexam(pathFile, MedicalCenter, DatosPaciente, datosGrabo, txtCommentary.Text, DataSource, ListadditExam, Edad);
                }

                List<string> pdfList = new List<string>();
                pdfList.Add(pathFile);
                _mergeExPDF.FilesName = pdfList;
                _mergeExPDF.DestinationFile = string.Format("{0}.pdf", Path.Combine(rutaBasura, _serviceId + "-" + "ORDEN-EX-MED-ADICI-" + CMP));
                _mergeExPDF.Execute();
                _mergeExPDF.RunFile();

                this.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            

        }
        private string DiferenciaFechas(DateTime newdt, DateTime olddt)
        {
            int anios;
            int meses;
            int dias;
            string str = "";

            anios = (newdt.Year - olddt.Year);
            meses = (newdt.Month - olddt.Month);
            dias = (newdt.Day - olddt.Day);

            if (meses < 0)
            {
                anios -= 1;
                meses += 12;
            }
            if (dias < 0)
            {
                meses -= 1;
                dias += DateTime.DaysInMonth(newdt.Year, newdt.Month);
            }

            if (anios < 0)
            {
                return "La fecha inicial es mayor a la fecha final";
            }
            if (anios > 0)
            {
                if (anios == 1)
                    str = str + anios.ToString() + " año ";
                else
                    str = str + anios.ToString() + " años ";
            }
            if (meses > 0)
            {
                if (meses == 1)
                    str = str + meses.ToString() + " mes y ";
                else
                    str = str + meses.ToString() + " meses y ";
            }
            if (dias > 0)
            {
                if (dias == 1)
                    str = str + dias.ToString() + " día ";
                else
                    str = str + dias.ToString() + " días ";
            }
            return str;
        }

        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvExamenesSeleccionados_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            btnRemoverExamenAuxiliar.Enabled = (lvExamenesSeleccionados.SelectedItems.Count > 0);
        }

        private void btnRemoverExamenAuxiliar_Click(object sender, EventArgs e)
        {
            var selectedItem = lvExamenesSeleccionados.SelectedItems[0];
            var medicalExamId = selectedItem.SubItems[1].Text;

            // Eliminacion fisica
            lvExamenesSeleccionados.Items.Remove(selectedItem);
            gbExamenesSeleccionados.Text = string.Format("Examenes Seleccionados {0}", lvExamenesSeleccionados.Items.Count);


        }

        private void gdDataExams_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private void gdDataExamsNew_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            bool row = gdDataExamsNew.Selected.Rows.Count > 0;
            if (!row)
            {
                return;
            }
            if (gdDataExamsNew.Selected.Rows[0].Cells["v_ComponentId"].Value == null)
            {
                btnAgregarExamenAuxiliar.Enabled = false;

                return;
            }
            else
            {
                btnAgregarExamenAuxiliar.Enabled = true;
            }

            lvExamenesSeleccionados.SelectedItems.Clear();

            if (gdDataExamsNew.Selected.Rows.Count == 0)
                return;

            MedicalExamId = gdDataExamsNew.Selected.Rows[0].Cells["v_ComponentId"].Value.ToString();
            MedicalExamName = gdDataExamsNew.Selected.Rows[0].Cells["v_ComponentName"].Value.ToString();
            //ServiceComponentConcatId = gdDataExams.Selected.Rows[0].Cells["v_ServiceComponentId"].Value.ToString();

            if (gdDataExamsNew.Selected.Rows[0].Cells["v_ComponentId"].Value != null)
            {
                MedicalExamName = gdDataExamsNew.Selected.Rows[0].Cells["v_ComponentName"].Value.ToString();
            }
            else
            {
                MedicalExamName = string.Empty;
            }
        }

        private void gdDataExamsNew_DoubleClickCell(object sender, Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs e)
        {
            //AddAuxiliaryExam();   
        }

        private void gdDataExamsNew_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            AddAuxiliaryExam();   
        }

        private void lvExamenesSeleccionados_DoubleClick(object sender, EventArgs e)
        {
            var selectedItem = lvExamenesSeleccionados.SelectedItems[0];
            var medicalExamId = selectedItem.SubItems[1].Text;

            // Eliminacion fisica
            lvExamenesSeleccionados.Items.Remove(selectedItem);
            gbExamenesSeleccionados.Text = string.Format("Examenes Seleccionados {0}", lvExamenesSeleccionados.Items.Count);
        }

        private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFiltro.Text != string.Empty)
            {
                ListServiceComponentTemp = new List<Categoria>(ListServiceComponent.Where(p => p.v_ComponentName.Contains(txtFiltro.Text.ToUpper())));

                gdDataExamsNew.DataSource = ListServiceComponentTemp;

                if (ListServiceComponentTemp != null)
                {
                    lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", ListServiceComponentTemp.Count());

                    //grdData.Rows[0].Selected = true;
                }

            }
            else
            {
                gdDataExamsNew.DataSource = ListServiceComponent;

                if (ListServiceComponent != null)
                {
                    lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", ListServiceComponent.Count());

                    //grdData.Rows[0].Selected = true;
                }
            }
        }
    }
}
