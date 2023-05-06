using Infragistics.Win.UltraWinGrid;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE.Custom;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Server.WebClientAdmin.BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmCovid15 : Form
    {
        List<ServiceCovid19> ServiceCovid19_ = new List<ServiceCovid19>();
        int TipoBusqueda = 0;
        int nodeId;
        public frmCovid15()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "PRUEBAS COVID-19 DESDE " + dtInicio.Text + " HASTA " + dtFin.Text;
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grCovid, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            BindingGrid();

            if (ddlTipoDePrueba.SelectedValue.ToString() == "2")
            {
                if (ServiceCovid19_.Count >= 1)
                {
                    btnExportarExcelMolecular.Enabled = true;
                }
                else
                {
                    btnExportarExcelMolecular.Enabled = false;
                }
            }
            else
            {
                btnExportarExcelMolecular.Enabled = false;
            }  
        }

        private void BindingGrid()
        {

            var objData = GetData();

            ServiceCovid19_ = objData;
            grCovid.DataSource = objData;
            grCovid.DataBind();

            if (objData == null) return;
            if (objData.Count >= 0)
            {
                btnPerson.Enabled = true;
                lblContadorFilas.Text = string.Format("Se encontraron {0} registros.", objData.Count);
            }
            else
            {
                btnPerson.Enabled = false;
                lblContadorFilas.Text = string.Format("Se encontraron {0} registros.", objData.Count);
            }
        }

        private List<ServiceCovid19> GetData()
        {
            List<ServiceCovid19> DataSource = new List<ServiceCovid19>();

            DateTime? pdatBeginDate = dtInicio.Value.Date;
            DateTime? pdatEndDate = dtFin.Value.Date.AddDays(1);

            string nombre = null;
            string servicio = null;
            string empresa = null;
            string contrata = null;
            if (!String.IsNullOrEmpty(txtValue.Text))
            {
                nombre = txtValue.Text.ToUpper();
            }

            if (ddlServiceTypeId.SelectedValue.ToString() == "1")//EMPRESARIAL
            {
                servicio = "1";
            }
            else if (ddlServiceTypeId.SelectedValue.ToString() == "9")//PARTICULAR
            {
                servicio = "9";
            }
            else if (ddlServiceTypeId.SelectedValue.ToString() == "11")//seguros
            {
                servicio = "11";
            }


            if (ddlCustomerOrganization.SelectedIndex != 0)
            {
                string[] Empresa = ddlCustomerOrganization.Text.Split('/');
                empresa = Empresa[0];
            }

            if (ddlEmployerOrganization.SelectedIndex != 0)
            {
                string[] Empresa = ddlEmployerOrganization.Text.Split('/');
                contrata = Empresa[0];
                           }
            if (ddlTipoDePrueba.SelectedValue.ToString() == "1")
            {
                DataSource = new ServiceBL().GetServicesCovid19_Filters(pdatBeginDate.Value.Date, pdatEndDate.Value, nombre, servicio, empresa, contrata);
            }
            else if (ddlTipoDePrueba.SelectedValue.ToString() == "2")
            {
                DataSource = new ServiceBL().GetServicesCovid19_Filters_MOLECULAR(pdatBeginDate.Value.Date, pdatEndDate.Value, nombre, servicio, empresa, contrata);
            }
            else if (ddlTipoDePrueba.SelectedValue.ToString() == "3")
            {
                System.Threading.Tasks.Task.Factory.StartNew(() => DataSource = new ServiceBL().GetServicesCovid19_Filters_ANTIGENO(pdatBeginDate.Value.Date, pdatEndDate.Value, nombre, servicio, empresa, contrata)).Wait(); 

                //DataSource = new ServiceBL().GetServicesCovid19_Filters_ANTIGENO(pdatBeginDate.Value.Date, pdatEndDate.Value, nombre, servicio, empresa, contrata);
                //DataSource = System.Threading.Tasks.Task.Factory.StartNew(() => new ServiceBL().GetServicesCovid19_Filters_ANTIGENO(pdatBeginDate.Value.Date, pdatEndDate.Value, nombre, servicio, empresa, contrata)).Wait();

            }
            else
            {
                DataSource = new ServiceBL().GetServicesCovid19_Filters_TODOS(pdatBeginDate.Value.Date, pdatEndDate.Value, nombre, servicio, empresa, contrata);
            }
            

            return DataSource;
            
        }

        private void grCovid_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
            var banda = e.Row.Band.Index.ToString();
            var row = e.Row;
            if (banda == "0")
            {
                if (row.Band.Index.ToString() == "0")
                {

                    if (e.Row.Cells["RESULTADO"].Value.ToString() == "IgM Positivo" || 
                        e.Row.Cells["RESULTADO"].Value.ToString() == "IgG Positivo" ||
                        e.Row.Cells["RESULTADO"].Value.ToString() == "IgM e IgG Positivo" || 
                        e.Row.Cells["RESULTADO"].Value.ToString() == "POSITIVO")
                    {
                        e.Row.Appearance.BackColor = Color.Orange;
                        e.Row.Appearance.BackColor2 = Color.White;
                        e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
                    }
                    else if (e.Row.Cells["RESULTADO"].Value.ToString() == "NO VÁLIDO")
                    {
                        e.Row.Appearance.BackColor = Color.Red;
                        e.Row.Appearance.BackColor2 = Color.White;
                        e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
                    }
                    else
                    {
                        e.Row.Appearance.BackColor = Color.GreenYellow;
                        e.Row.Appearance.BackColor2 = Color.White;
                        //Y doy el efecto degradado vertical
                        e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
                    }
                }
            }

        }

        private void btnPerson_Click(object sender, EventArgs e)
        {
            string personId = grCovid.Selected.Rows[0].Cells["ID_PACIENTE"].Value.ToString();
            if (personId != null)
            {
                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    var frm = new frmPacient(personId);
                    frm.ShowDialog();
                }
            }
            else
            {
                btnPerson.Enabled = false;
            }
            
        }

        private void grCovid_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grCovid.Selected.Rows.Count == 0) return;
            if (grCovid.Selected.Rows[0].Cells != null)
            {
                OperationResult objOperationResult = new OperationResult();
                List<string> Filters = new List<string>();

                foreach (UltraGridRow rowSelected in this.grCovid.Selected.Rows)
                {

                    if (rowSelected.Band.Index.ToString() == "0")
                    {
                        var ServiceId = grCovid.Selected.Rows[0].Cells["ID_PACIENTE"].Value.ToString();
                        if (ServiceId != null)
                        {
                            btnPerson.Enabled = true;
                        }
                        else
                        {
                            btnPerson.Enabled = true;
                        }

                    }
                   
                }
                if (ServiceCovid19_ != null)
                {
                    btnPerson.Enabled = true;
                }
                else
                {
                    btnPerson.Enabled = false;
                }


                if (grCovid.Selected.Rows.Count == 0)
                    return;


            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new frmService();
            frm.ShowDialog();
        }

        private void frmCovid15_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            nodeId = int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"));

            Utils.LoadDropDownList(ddlServiceTypeId, "Value1", "Id", BLL.Utils.GetServiceType(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.All);
            ddlServiceTypeId.SelectedValue = "-1";

            Utils.LoadDropDownList(ddlTipoDePrueba, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 397, null), DropDownListAction.All);
            ddlTipoDePrueba.SelectedValue = "1";

            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, nodeId), DropDownListAction.All);
            Utils.LoadDropDownList(ddlEmployerOrganization, "Value1", "Id", BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, nodeId), DropDownListAction.All);
        }

        private void btnNeGATIVO_Click(object sender, EventArgs e)
        {
            TipoBusqueda = 1;

            List<ServiceCovid19> Data = new List<ServiceCovid19>(ServiceCovid19_.Where(p => p.RESULTADO.Contains("NEGATIVO")));

            grCovid.DataSource = Data;
            grCovid.DataBind();

            if (Data == null) return;
            if (Data.Count >= 0)
            {
                btnPerson.Enabled = true;
                lblContadorFilas.Text = string.Format("Se encontraron {0} registros.", Data.Count);
            }
            else
            {
                btnPerson.Enabled = false;
                lblContadorFilas.Text = string.Format("Se encontraron {0} registros.", Data.Count);
            }

            TipoBusqueda = 0;
           

            //else if (TipoBusqueda == 3)
            //{
            //    List<ServiceCovid19> Data = new List<ServiceCovid19>(DataSource.Where(p => p.RESULTADO.Contains("NO VÁLIDO")));
            //    DataSource = new List<ServiceCovid19>(Data);
            //    TipoBusqueda = 0;
            //}
            //else
            //{

            //}
        }

        private void btnPositivo_Click(object sender, EventArgs e)
        {
            TipoBusqueda = 2;
            List<ServiceCovid19> Data = new List<ServiceCovid19>();

            if (rbTodos.Checked == true)
            {
                Data = new List<ServiceCovid19>(ServiceCovid19_.Where(p => p.RESULTADO.Contains("IgM Positivo") || p.RESULTADO.Contains("IgG Positivo") || p.RESULTADO.Contains("IgM e IgG Positivo") || p.RESULTADO.Contains("POSITIVO")));
                TipoBusqueda = 0;
            }
            else if (rbIgMPositivo.Checked == true)
            {
                Data = new List<ServiceCovid19>(ServiceCovid19_.Where(p => p.RESULTADO == "IgM Positivo"));
                TipoBusqueda = 0;
            }
            else if (rbIgGPositivo.Checked == true)
            {
                Data = new List<ServiceCovid19>(ServiceCovid19_.Where(p => p.RESULTADO == "IgG Positivo"));
                TipoBusqueda = 0;
            }
            else if (rbIgMeIgGPositivo.Checked == true)
            {
                Data = new List<ServiceCovid19>(ServiceCovid19_.Where(p => p.RESULTADO == "IgM e IgG Positivo"));
                TipoBusqueda = 0;
            }

            grCovid.DataSource = Data;
            grCovid.DataBind();

            if (Data == null) return;
            if (Data.Count >= 0)
            {
                btnPerson.Enabled = true;
                lblContadorFilas.Text = string.Format("Se encontraron {0} registros.", Data.Count);
            }
            else
            {
                btnPerson.Enabled = false;
                lblContadorFilas.Text = string.Format("Se encontraron {0} registros.", Data.Count);
            }
            TipoBusqueda = 0;
            //BindingGrid();
        }

        private void btnNoValido_Click(object sender, EventArgs e)
        {
            TipoBusqueda = 3;

            List<ServiceCovid19> Data = new List<ServiceCovid19>(ServiceCovid19_.Where(p => p.RESULTADO.Contains("NO VÁLIDO")));

            grCovid.DataSource = Data;
            grCovid.DataBind();

            if (Data == null) return;
            if (Data.Count >= 0)
            {
                btnPerson.Enabled = true;
                lblContadorFilas.Text = string.Format("Se encontraron {0} registros.", Data.Count);
            }
            else
            {
                btnPerson.Enabled = false;
                lblContadorFilas.Text = string.Format("Se encontraron {0} registros.", Data.Count);
            }

            TipoBusqueda = 0;
        }

        private void btnGraficos_Click(object sender, EventArgs e)
        {
            DateTime pdatBeginDate = dtInicio.Value.Date;
            DateTime pdatEndDate = dtFin.Value.Date.AddDays(1);
            var frm = new frmGraficosCovid19(pdatBeginDate, pdatEndDate, ServiceCovid19_);
            frm.ShowDialog();
        }

        private void btnGraficos2_Click(object sender, EventArgs e)
        {
            DateTime pdatBeginDate = dtInicio.Value.Date;
            DateTime pdatEndDate = dtFin.Value.Date.AddDays(1);
            var frm = new frmGraficosCov19_2(pdatBeginDate, pdatEndDate, ServiceCovid19_);
            frm.ShowDialog();

            //frmGraficosCov19_PCR_ANT
        }

        private void ddlServiceTypeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            
            if (ddlServiceTypeId.SelectedValue.ToString() == "1")
            {
                ddlCustomerOrganization.SelectedValue = "-1";
                ddlCustomerOrganization.Enabled = true;
                ddlEmployerOrganization.SelectedValue = "-1";
                ddlEmployerOrganization.Enabled = true;
            }
            else //else (ddlServiceTypeId.SelectedValue.ToString() == "-1") //todos
            {
                ddlCustomerOrganization.SelectedValue = "-1";
                ddlCustomerOrganization.Enabled = false;
                ddlEmployerOrganization.SelectedValue = "-1";
                ddlEmployerOrganization.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            grCovid.DataSource = ServiceCovid19_;
            grCovid.DataBind();

            if (ServiceCovid19_ == null) return;
            if (ServiceCovid19_.Count >= 0)
            {
                btnPerson.Enabled = true;
                lblContadorFilas.Text = string.Format("Se encontraron {0} registros.", ServiceCovid19_.Count);
            }
            else
            {
                btnPerson.Enabled = false;
                lblContadorFilas.Text = string.Format("Se encontraron {0} registros.", ServiceCovid19_.Count);
            }

            TipoBusqueda = 0;
        }

        private void btnReportAsync_Click(object sender, EventArgs e)
        {
            try
            {
                var StatusLiquidation = grCovid.Selected.Rows[0].Cells["i_StatusLiquidation"].Value == null
                    ? 1
                    : int.Parse(grCovid.Selected.Rows[0].Cells["i_StatusLiquidation"].Value.ToString());

                if (StatusLiquidation == 2)
                {
                    var DialogResult =
                        MessageBox.Show("Este servicio ya tiene, reportes generados, ¿Desea volver a generar?",
                            "INFORMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (DialogResult == DialogResult.No)
                    {
                        string ruta = Common.Utils.GetApplicationConfigValue("rutaConsolidado").ToString();
                        System.Diagnostics.Process.Start(ruta);
                        var pacientName = grCovid.Selected.Rows[0].Cells["PACIENTE"].Value.ToString();
                        string paciente = pacientName.Split(',')[0] + pacientName.Split(',')[1];
                        Clipboard.SetText(paciente);
                        return;
                    }
                }

                int flagPantalla =
                    int.Parse(grCovid.Selected.Rows[0].Cells["i_MasterServiceId"].Value
                        .ToString()); 

                if (flagPantalla == 2)
                {
                    var dni = grCovid.Selected.Rows[0].Cells["DNI"].Value.ToString();
                    var pacientName = grCovid.Selected.Rows[0].Cells["PACIENTE"].Value.ToString();
                    string paciente = pacientName.Split(',')[0] + pacientName.Split(',')[1];
                    var _serviceId = grCovid.Selected.Rows[0].Cells["SERVICIO"].Value.ToString();
                    var _pacientId = grCovid.Selected.Rows[0].Cells["ID_PACIENTE"].Value.ToString();
                    var _EmpresaClienteId = grCovid.Selected.Rows[0].Cells["MINAID"].Value.ToString();
                    var _customerOrganizationName = grCovid.Selected.Rows[0].Cells["MINANAME"].Value.ToString();
                    var frm = new Reports.frmManagementReports_Async(_serviceId, _EmpresaClienteId, _pacientId, _customerOrganizationName, dni, paciente, "");
                    frm.ShowDialog();
                }
                else
                {
                    var edad = int.Parse(grCovid.Selected.Rows[0].Cells["EDAD"].Value.ToString());
                    var _serviceId = grCovid.Selected.Rows[0].Cells["SERVICIO"].Value.ToString();
                    var _pacientId = grCovid.Selected.Rows[0].Cells["ID_PACIENTE"].Value.ToString();
                    var _EmpresaClienteId = grCovid.Selected.Rows[0].Cells["MINAID"].Value.ToString();
                    var _customerOrganizationName = grCovid.Selected.Rows[0].Cells["MINANAME"].Value.ToString();
                    var pacientName = grCovid.Selected.Rows[0].Cells["PACIENTE"].Value.ToString();
                    string paciente = pacientName.Split(',')[0] + pacientName.Split(',')[1];
                    var frm = new Reports.frmManagementReportsMedical(_serviceId, _pacientId, _customerOrganizationName, paciente, _EmpresaClienteId, edad);
                    frm.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnFiltrar_Click(sender, e);
            }
        }

        private void btnExportarExcelMolecular_Click(object sender, EventArgs e)
        {
            try
            {
                //liquidacionID = grdData.Selected.Rows[0].Cells["v_NroLiquidacion"].Value.ToString();
                creaExcel(sender, e, "");
               
            }
            catch
            {

            }
        }

        public void creaExcel(object sender, EventArgs e, string liquidacion)
        {
            OperationResult objOperationResult = new OperationResult();

            //string liquidacionID = null;
            //string serviceID;
            //string protocolId;
            //if (tabControl1.SelectedTab.Name == "tpESO")
            //{
            //               
            //}
            //else if (tabControl1.SelectedTab.Name == "tpEmpresa")
            //{
            //    
            //}
            string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

            //var lista = _serviceBL.GetListaLiquidacion(ref _objOperationResult, liquidacion);

            BackgroundWorker bw = sender as BackgroundWorker;

            Excel.Application excel = new Excel.Application();
            Excel._Workbook libro = null;
            Excel._Worksheet hoja = null;
            Excel.Range rango = null;

            try
            {
                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    //creamos un libro nuevo y la hoja con la que vamos a trabajar
                    libro = (Excel._Workbook)excel.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);

                    hoja = (Excel._Worksheet)libro.Worksheets.Add();
                    hoja.Application.ActiveWindow.DisplayGridlines = false;
                    hoja.Name = "PCR DEL " + dtInicio.Value.ToShortDateString().Replace('/', '-');
                    ((Excel.Worksheet)excel.ActiveWorkbook.Sheets["Hoja1"]).Delete();   //Borro hoja que crea en el libro por defecto

                    
                    //LOGO CSL
                    rango = (Microsoft.Office.Interop.Excel.Range)hoja.get_Range("B2", "D5");
                    rango.Select();
                    rango.RowHeight = 25;
                    hoja.get_Range("B2", "D5").Merge(true);

                    hoja.get_Range("B2", "D5").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    hoja.get_Range("B2", "D5").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    Microsoft.Office.Interop.Excel.Pictures oPictures = (Microsoft.Office.Interop.Excel.Pictures)hoja.Pictures(System.Reflection.Missing.Value);

                    hoja.Shapes.AddPicture(@"C:\Program Files (x86)\NetMedical\Banner\banner.jpg",
                        Microsoft.Office.Core.MsoTriState.msoFalse,
                        Microsoft.Office.Core.MsoTriState.msoCTrue,
                        float.Parse(rango.Left.ToString()),
                        float.Parse(rango.Top.ToString()),
                        190,
                        82);

                    //LOGO CSL
                    rango = (Microsoft.Office.Interop.Excel.Range)hoja.get_Range("B6", "c6");
                    rango.Select();
                    rango.RowHeight = 25;
                    hoja.get_Range("B6", "C6").Merge(true);

                    hoja.get_Range("B6", "C6").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    hoja.get_Range("B6", "C6").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    Microsoft.Office.Interop.Excel.Pictures oPictures2 = (Microsoft.Office.Interop.Excel.Pictures)hoja.Pictures(System.Reflection.Missing.Value);

                    hoja.Shapes.AddPicture(@"C:\Program Files (x86)\NetMedical\Banner\unilabs.png",
                        Microsoft.Office.Core.MsoTriState.msoFalse,
                        Microsoft.Office.Core.MsoTriState.msoCTrue,
                        float.Parse(rango.Left.ToString()),
                        float.Parse(rango.Top.ToString()),
                        160,
                        55);

                    montaCabeceras(3, ref hoja, liquidacion);
                    
                    //DatosDinamicos
                    int fila = 16;
                    int count = 1;
                    int i = 0;
                    int fila_final = 0;
                    foreach (var item in ServiceCovid19_)
                    {
                        hoja.Cells[fila + i, 2] = count + ".";
                        hoja.Cells[fila + i, 3] = item.AP_PATERNO.Trim();
                        hoja.Cells[fila + i, 4] = item.AP_MATERNO.Trim();
                        hoja.Cells[fila + i, 5] = item.NOMBRES.Trim();
                        hoja.Cells[fila + i, 6] = item.DNI;
                        hoja.Cells[fila + i, 7] = item.SEXO;
                        hoja.Cells[fila + i, 8] = item.NACIMIENTO;
                        hoja.Cells[fila + i, 9] = item.Direccion_Toma;
                        hoja.Cells[fila + i, 10] = item.Lugar;

                        hoja.get_Range("B" + (fila + i)).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        hoja.get_Range("C" + (fila + i)).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        hoja.get_Range("D" + (fila + i)).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        hoja.get_Range("E" + (fila + i)).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        hoja.get_Range("F" + (fila + i)).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        hoja.get_Range("B" + (fila + i)).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        hoja.get_Range("G" + (fila + i)).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        hoja.get_Range("H" + (fila + i)).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        hoja.get_Range("I" + (fila + i)).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                        hoja.get_Range("J" + (fila + i)).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);


                        hoja.get_Range("B" + +(fila + i)).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        hoja.get_Range("C" + +(fila + i)).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        hoja.get_Range("D" + +(fila + i)).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        hoja.get_Range("E" + +(fila + i)).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        hoja.get_Range("F" + +(fila + i)).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        hoja.get_Range("G" + +(fila + i)).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        hoja.get_Range("H" + +(fila + i)).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        hoja.get_Range("I" + +(fila + i)).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        hoja.get_Range("J" + +(fila + i)).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

                        hoja.get_Range("B" + +(fila + i)).VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        hoja.get_Range("C" + +(fila + i)).VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        hoja.get_Range("D" + +(fila + i)).VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        hoja.get_Range("E" + +(fila + i)).VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        hoja.get_Range("F" + +(fila + i)).VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        hoja.get_Range("G" + +(fila + i)).VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        hoja.get_Range("H" + +(fila + i)).VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        hoja.get_Range("I" + +(fila + i)).VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        hoja.get_Range("J" + +(fila + i)).VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        

                        i++;
                        count++;

                    }


                    //LOGO CSL
                    string x7 = "B" + (fila + i + 4).ToString();
                    string y7 = "J" + (fila + i + 4).ToString();
                    rango = (Microsoft.Office.Interop.Excel.Range)hoja.get_Range(x7, y7);
                    rango.Select();
                    hoja.get_Range(x7, y7).Merge(true);

                    Microsoft.Office.Interop.Excel.Pictures oPictures3 = (Microsoft.Office.Interop.Excel.Pictures)hoja.Pictures(System.Reflection.Missing.Value);

                    hoja.Shapes.AddPicture(@"C:\Program Files (x86)\NetMedical\Banner\banner2.jpg",
                       Microsoft.Office.Core.MsoTriState.msoFalse,
                       Microsoft.Office.Core.MsoTriState.msoCTrue,
                       float.Parse(rango.Left.ToString()),
                       float.Parse(rango.Top.ToString()),
                       float.Parse(rango.Width.ToString()),
                       80);

                    
                    libro.Saved = true;

                    libro.SaveAs(ruta + @"\" + "PRUEBAS MOLECULARES DEL " + dtInicio.Value.ToShortDateString().Replace('/', '-') + ".xlsx");

                    //bw.WorkerReportsProgress = true;
                    //bw.ReportProgress(100, ti);

                    libro.Close();
                    releaseObject(libro);

                    excel.UserControl = false;
                    excel.Quit();
                    releaseObject(excel);
                }
                Process.Start(ruta + @"\" + "PRUEBAS MOLECULARES DEL " + dtInicio.Value.ToShortDateString().Replace('/', '-') + ".xlsx");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en creación/actualización de LISTA DE PRUEBA MOLECULAR DEL " + dtInicio.Value.ToShortDateString().Replace('/', '-'), MessageBoxButtons.OK, MessageBoxIcon.Error);
                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    libro.Saved = true;
                    libro.SaveAs(ruta + @"\" + "PRUEBAS MOLECULARES DEL " + dtInicio.Value.ToShortDateString().Replace('/','-') + "_fail.xlsx");

                    libro.Close();
                    releaseObject(libro);

                    excel.UserControl = false;
                    excel.Quit();
                    releaseObject(excel);
                }
                Process.Start(ruta + @"\" + "PRUEBAS MOLECULARES DEL " + dtInicio.Value.ToShortDateString().Replace('/', '-') + "_fail.xlsx");
            }

        }

        private void montaCabeceras(int fila, ref Excel._Worksheet hoja, string _liquidacion)
        {
            var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
          
            try
            {
                Excel.Range rango;


                //** TITULO DEL LIBRO **
                ////hoja.Cells[1, 2] = MedicalCenter.b_Image;
                //hoja.get_Range("B1", "C1");

                hoja.Cells[6, 4] = "LISTADO DE PACIENTES \nCHEQUEOS";
                hoja.get_Range("B6", "J6").Merge(true);
                hoja.get_Range("B6", "J6").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("B6", "J6").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("B6", "J6").Font.Bold = true;
                hoja.get_Range("B6", "J6").Font.Size = 16;
                hoja.get_Range("B6", "J6").RowHeight = 60;
                hoja.get_Range("B6", "J6").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja.Cells[8, 7] = "Fecha de envío de muestra :";
                hoja.Cells[8, 9] = dtInicio.Value.ToShortDateString();
                hoja.get_Range("G8", "H8").Merge(true);
                hoja.get_Range("I8", "I8").Merge(true);
                hoja.get_Range("G8", "H8").Font.Bold = true;

                hoja.get_Range("I8", "I8").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja.get_Range("G8", "H8").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("I8", "I8").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                hoja.get_Range("G8", "H8").RowHeight = 18;
                hoja.get_Range("I9", "I8").RowHeight = 18;
                hoja.get_Range("G8", "H8").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("I8", "I8").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;


                hoja.Cells[9, 7]= "Tipo Prueba";
                hoja.Cells[9, 8] = "Rapida";
                hoja.Cells[9, 9] = "Molecular";
                hoja.Range["G9", "G10"].Merge();
                //hoja.get_Range("G10", "G11").Merge(true);
                hoja.get_Range("H9", "H9").Merge(true);
                hoja.get_Range("I9", "I9").Merge(true);

                hoja.get_Range("G9", "G10").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("H9", "H9").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("I9", "I9").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                hoja.get_Range("G9", "G10").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("H9", "H9").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("I9", "I9").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja.get_Range("G9", "G10").Font.Bold = true;
                hoja.get_Range("H9", "H9").Font.Bold = true;
                hoja.get_Range("I9", "I9").Font.Bold = true;

                hoja.get_Range("G9", "G10").RowHeight = 36;
                hoja.get_Range("H9", "H9").RowHeight = 18;
                hoja.get_Range("I9", "I9").RowHeight = 18;

                hoja.get_Range("G9", "G10").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("H9", "H9").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("I9", "I9").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;


                hoja.Cells[10, 2] = "Razón Social Cliente:";
                hoja.Cells[10, 4] = MedicalCenter.v_Name;

                hoja.Cells[10, 8] = "";
                hoja.Cells[10, 9] = "X";

                hoja.get_Range("B10", "C10").Merge(true);
                hoja.get_Range("D10", "E10").Merge(true);
                hoja.get_Range("H10", "H10").Merge(true);
                hoja.get_Range("I10", "I10").Merge(true);

                hoja.get_Range("B10", "C10").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("D10", "E10").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("H10", "H10").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("I10", "I10").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                hoja.get_Range("B10", "C10").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                hoja.get_Range("D10", "E10").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("H10", "H10").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("I10", "I10").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja.get_Range("B10", "C10").Font.Bold = true;
                hoja.get_Range("D10", "D10").Font.Bold = true;
                hoja.get_Range("H10", "H10").Font.Bold = true;
                hoja.get_Range("I10", "I10").Font.Bold = true;

                hoja.get_Range("B10", "C10").RowHeight = 18;
                hoja.get_Range("D10", "E10").RowHeight = 18;
                hoja.get_Range("H10", "H10").RowHeight = 18;
                hoja.get_Range("I10", "I10").RowHeight = 18;

                hoja.get_Range("B10", "C10").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("D10", "E10").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("H10", "H10").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("I10", "I10").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja.Cells[11, 2] = "RUC del Cliente:";
                hoja.Cells[11, 4] = MedicalCenter.v_IdentificationNumber;

                hoja.get_Range("B11", "C11").Merge(true);
                hoja.get_Range("D11", "E11").Merge(true);

                hoja.get_Range("B11", "C11").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("D11", "E11").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                hoja.get_Range("B11", "C11").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                hoja.get_Range("D11", "E11").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja.get_Range("B11", "C11").Font.Bold = true;
                hoja.get_Range("D11", "D11").Font.Bold = true;

                hoja.get_Range("B11", "C11").RowHeight = 18;
                hoja.get_Range("D11", "E11").RowHeight = 18;

                hoja.get_Range("B11", "C11").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("D11", "E11").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;


                hoja.Cells[11, 7] = "REGISTRO DE TEMPERATURA DE ENVÍO DE MUESTRAS SARS COV-2";

                hoja.get_Range("G11", "I11").Merge(true);

                hoja.get_Range("G11", "I11").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                hoja.get_Range("G11", "I11").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja.get_Range("G11", "I11").Font.Bold = true;

                hoja.get_Range("G11", "I11").RowHeight = 18;

                hoja.get_Range("G11", "I11").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;


                hoja.Cells[12, 7] = "T° de salida de laboratorio CSL:";
                hoja.Cells[12, 9] = "T° de llegada a laboratorio UNILABS:";

                hoja.get_Range("G12", "H12").Merge(true);
                hoja.get_Range("I12", "I12").Merge(true);

                hoja.get_Range("G12", "H12").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("I12", "I12").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                hoja.get_Range("G12", "H12").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                hoja.get_Range("I12", "I12").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

                hoja.get_Range("G12", "H12").Font.Bold = true;
                hoja.get_Range("I12", "I12").Font.Bold = true;

                hoja.get_Range("G12", "H12").RowHeight = 18;
                hoja.get_Range("I12", "I12").RowHeight = 18;

                //hoja.get_Range("G12", "H12").VerticalAlignment = Excel.XlHAlign.xlHAlignLeft;
                //hoja.get_Range("I12", "I12").VerticalAlignment = Excel.XlHAlign.xlHAlignLeft;



                hoja.Cells[14, 2] = "Datos Personales";

                hoja.get_Range("B14", "J14").Merge(true);
                hoja.get_Range("B14", "J14").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("B14", "J14").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("B14", "J14").Font.Bold = true;
                hoja.get_Range("B14", "J14").RowHeight = 25;
                hoja.get_Range("B14", "J14").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja.Cells[15, 2] = "N°";
                hoja.Cells[15, 3] = "Apellido Paterno*";
                hoja.Cells[15, 4] = "Apellido Materno*";
                hoja.Cells[15, 5] = "Nombres*";
                hoja.Cells[15, 6] = "DNI*";
                hoja.Cells[15, 7] = "Sexo*";
                hoja.Cells[15, 8] = "Fecha de nacimiento \n(dd/mm/aaaa)*";
                hoja.Cells[15, 9] = "Dirección de Toma de Muestra";
                hoja.Cells[15, 10] = "Lugar TM";

                hoja.get_Range("B15").Merge(true);
                hoja.get_Range("C15").Merge(true);
                hoja.get_Range("D15").Merge(true);
                hoja.get_Range("E15").Merge(true);
                hoja.get_Range("F15").Merge(true);
                hoja.get_Range("G15").Merge(true);
                hoja.get_Range("H15").Merge(true);
                hoja.get_Range("I15").Merge(true);
                hoja.get_Range("J15").Merge(true);

                hoja.get_Range("B15").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("C15").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("D15").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("E15").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("F15").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("B15").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("G15").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("H15").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("I15").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("J15").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                hoja.get_Range("B15", "J15").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("B15", "J15").Font.Bold = true;
                hoja.get_Range("B15", "J15").RowHeight = 50;
                hoja.get_Range("B15", "J15").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;


                rango = hoja.Range["B14", "J15"];
                rango.Interior.Color = Color.DarkGray;
                ////Asigna borde
                //rango = hoja.Range["A1", "Z1000"];
                //rango.Interior.Color = Color.Aqua;

                //Modificamos los anchos de las columnas
                rango = hoja.Columns[1];
                rango.ColumnWidth = 1;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[2];
                rango.ColumnWidth = 4;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[3];
                rango.ColumnWidth = 25;
                rango.Cells.WrapText = true;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[4];
                rango.ColumnWidth = 25;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[5];
                rango.ColumnWidth = 29;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[6];
                rango.ColumnWidth = 8;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[7];
                rango.ColumnWidth = 15;
                rango.Cells.WrapText = true;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[8];
                rango.ColumnWidth = 20;
                rango.Cells.WrapText = true;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[9];
                rango.ColumnWidth = 38;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.Cells.WrapText = true;
                //rango.NumberFormat = "#0.00";

                rango = hoja.Columns[10];
                rango.ColumnWidth = 26;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.Cells.WrapText = true;
                //rango.NumberFormat = "#0.00";

                //rango = hoja.Columns[11];
                //rango.ColumnWidth = 8;
                //rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                //rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //rango.NumberFormat = "#0.00";

                //rango = hoja.Columns[12];
                //rango.ColumnWidth = 20;
                //rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de redondeo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Error mientras liberaba objecto " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void ddlTipoDePrueba_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoDePrueba.SelectedValue.ToString() == "2")
            {
                rbIgMPositivo.Enabled = false;
                rbIgGPositivo.Enabled = false;
                rbIgMeIgGPositivo.Enabled = false;

                if (ServiceCovid19_.Count >= 1)
                {
                    btnExportarExcelMolecular.Enabled = true;
                }
                else
                {
                    btnExportarExcelMolecular.Enabled = false;
                }

            }
            else if (ddlTipoDePrueba.SelectedValue.ToString() == "3")
            {
                btnExportarExcelMolecular.Enabled = false;
                rbIgMPositivo.Enabled = false;
                rbIgGPositivo.Enabled = false;
                rbIgMeIgGPositivo.Enabled = false;
            }
            else
            {
                btnExportarExcelMolecular.Enabled = false;

                rbIgMPositivo.Enabled = true;
                rbIgGPositivo.Enabled = true;
                rbIgMeIgGPositivo.Enabled = true;
            }  
        }

        private void ddlTipoDePrueba_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DateTime pdatBeginDate = dtInicio.Value.Date;
            DateTime pdatEndDate = dtFin.Value.Date.AddDays(1);
            var frm = new frmGraficosCov19_PCR_ANT(pdatBeginDate, pdatEndDate, ServiceCovid19_);
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime pdatBeginDate = dtInicio.Value.Date;
            DateTime pdatEndDate = dtFin.Value.Date.AddDays(1);
            var frm = new frmGraficosCov19_Todos(pdatBeginDate, pdatEndDate, ServiceCovid19_);
            frm.ShowDialog();
        }

        
    }
}
