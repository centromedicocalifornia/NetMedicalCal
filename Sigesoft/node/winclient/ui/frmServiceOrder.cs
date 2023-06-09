﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.UI.Cotizaciones;
using Sigesoft.Node.WinClient.BE.Custom;
using NetPdf;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmServiceOrder : Form
    {
        string strFilterExpression;
        ServiceOrderBL _objServicOrderBL = new ServiceOrderBL();
        string _ServiceOrderId;
        string _ProtocolId;
        private List<KeyValueDTO> _formActions = null;
        private bool _isEditEnabled;
        private bool _isDeleteEnabled;
        private bool _isPrintEnabled;
        List<ServiceOrderList> objData = new List<ServiceOrderList>();
        List<ServiceOrderList> objDataTemp = new List<ServiceOrderList>();

        public frmServiceOrder()
        {
            InitializeComponent();
        }
      
        private void frmServiceOrder_Load(object sender, EventArgs e)
        {
            if (rbListado.Checked == true)
            {
                grdData.DisplayLayout.Bands[0].Columns["v_Comentary"].Header.Caption = "Empresa";

                label4.Enabled = false;
                ddlEmployerOrganization.Enabled = false;
                label5.Enabled = false;
                ddlWorkerOrganization.Enabled = false;
                label3.Enabled = false;
                ddlServiceOrderStatusId.Enabled = false;
                btnReport.Enabled = false;
                btnSendEmail.Enabled = false;
            }


            OperationResult objOperationResult = new OperationResult();

            Utils.LoadDropDownList(ddlServiceOrderStatusId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 194, null), DropDownListAction.All);

            int nodeId = int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"));

            var customerOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, nodeId);

            Utils.LoadDropDownList(ddlCustomerOrganization,
             "Value1",
             "Id",
             customerOrganization,
             DropDownListAction.Select);

            var employerOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, nodeId);
            Utils.LoadDropDownList(ddlEmployerOrganization,
             "Value1",
             "Id",
             employerOrganization,
             DropDownListAction.Select);

            var workingOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, nodeId);
            Utils.LoadDropDownList(ddlWorkerOrganization,
             "Value1",
             "Id",
             workingOrganization,
             DropDownListAction.Select); 
            
            dtpDateTimeStar.CustomFormat = "dd/MM/yyyy";
            dptDateTimeEnd.CustomFormat = "dd/MM/yyyy";
            strFilterExpression = null;   

            #region Get / Set Form Actions

            _formActions = Sigesoft.Node.WinClient.BLL.Utils.SetFormActionsInSession("frmServiceOrder",
                                                                                Globals.ClientSession.i_CurrentExecutionNodeId,
                                                                                Globals.ClientSession.i_RoleId.Value,
                                                                                Globals.ClientSession.i_SystemUserId);
            //// Setear privilegios / permisos      

            btnNew.Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmServiceOrder_ADD", _formActions);
            _isEditEnabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmServiceOrder_EDIT", _formActions);
            _isDeleteEnabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmServiceOrder_DELETE", _formActions);
            _isPrintEnabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmServiceOrder_PRINT", _formActions);
          
            #endregion

            btnFilter_Click(sender, e);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            
                // Get the filters from the UI
                List<string> Filters = new List<string>();

                if (ddlServiceOrderStatusId.SelectedValue.ToString() != "-1")
                    Filters.Add("i_ServiceOrderStatusId==" + ddlServiceOrderStatusId.SelectedValue);

                if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
                {
                    var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
                    Filters.Add("v_CustomerOrganizationId==" + "\"" + id3[0] + "\"&&v_CustomerLocationId==" + "\"" +
                                id3[1] + "\"");
                }

                if (ddlEmployerOrganization.SelectedValue.ToString() != "-1")
                {
                    var id3 = ddlEmployerOrganization.SelectedValue.ToString().Split('|');
                    Filters.Add("v_EmployerOrganizationId==" + "\"" + id3[0] + "\"&&v_EmployerLocationId==" + "\"" +
                                id3[1] + "\"");
                }

                if (ddlWorkerOrganization.SelectedValue.ToString() != "-1")
                {
                    var id3 = ddlWorkerOrganization.SelectedValue.ToString().Split('|');
                    Filters.Add("v_WorkingOrganizationId==" + "\"" + id3[0] + "\"&&v_WorkingLocationId==" + "\"" +
                                id3[1] + "\"");
                }

                // Create the Filter Expression
                strFilterExpression = null;
                if (Filters.Count > 0)
                {
                    foreach (string item in Filters)
                    {
                        strFilterExpression = strFilterExpression + item + " && ";
                    }

                    strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
                }

                this.BindGrid();
            
        }

        private void BindGrid()
        {
            if (rbListado.Checked == true)
            {
                objData = GetDataCotizaciones(0, null, "", strFilterExpression);

                grdData.DataSource = objData;
                lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());

                if (grdData.Rows.Count > 0)
                    grdData.Rows[0].Selected = true;
            }
            else if (rbProtocolo.Checked == true)
            {
                objData = GetData(0, null, "", strFilterExpression);

                grdData.DataSource = objData;
                lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());

                if (grdData.Rows.Count > 0)
                    grdData.Rows[0].Selected = true;
            }

            //grdDataService.DataSource = new List<ServiceComponentList>();
        }

        private List<ServiceOrderList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            var _objData = _objServicOrderBL.GetServiceOrderPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }
        private List<ServiceOrderList> GetDataCotizaciones(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            var _objData = _objServicOrderBL.GetServiceOrderPagedAndFilteredCotizaciones(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (rbListado.Checked == true)
            {
                //string CotizacionId = grdData.Selected.Rows[0].Cells["v_ServiceOrderId"].Value.ToString();


                frmCotizacionOc frm = new frmCotizacionOc("", "New");
                frm.ShowDialog();
            }
            else if (rbProtocolo.Checked == true)
            {

                frmServiceOrderEdit frm = new frmServiceOrderEdit("0", "", "New");
                frm.ShowDialog();

                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    // Refrescar grilla
                    btnFilter_Click(sender, e);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (rbListado.Checked == true)
            {
                string CotizacionId = grdData.Selected.Rows[0].Cells["v_ServiceOrderId"].Value.ToString();


                frmCotizacionOc frm = new frmCotizacionOc(CotizacionId, "Edit");
                frm.ShowDialog();
            }
            else if (rbProtocolo.Checked == true)
            {

                frmServiceOrderEdit frm = new frmServiceOrderEdit(_ServiceOrderId, _ProtocolId, "Edit");
                frm.ShowDialog();

                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    // Refrescar grilla
                    btnFilter_Click(sender, e);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada

            if (rbListado.Checked == true)
            {
                DialogResult Result =
                    MessageBox.Show(
                        "¿Está seguro de eliminar este registro?:" + System.Environment.NewLine +
                        objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                if (Result == System.Windows.Forms.DialogResult.Yes)
                {
                    // Delete the item              
                    string pstrServiceOrderId = grdData.Selected.Rows[0].Cells[0].Value.ToString();

                    _objServicOrderBL.DeleteCotizacion(ref objOperationResult, pstrServiceOrderId,
                        Globals.ClientSession.GetAsList());

                    btnFilter_Click(sender, e);
                }
            }
            else if (rbProtocolo.Checked == true)
            {
                DialogResult Result =
                    MessageBox.Show(
                        "¿Está seguro de eliminar este registro?:" + System.Environment.NewLine +
                        objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                if (Result == System.Windows.Forms.DialogResult.Yes)
                {
                    // Delete the item              
                    string pstrServiceOrderId = grdData.Selected.Rows[0].Cells[0].Value.ToString();

                    _objServicOrderBL.DeleteServiceOrder(ref objOperationResult, pstrServiceOrderId,
                        Globals.ClientSession.GetAsList());

                    btnFilter_Click(sender, e);
                }
            }
        }

        private void grdData_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
            if (e.Row.Cells["d_DeliveryDate"].Value != null)
            {
                string DeliveryDate = (((DateTime)e.Row.Cells["d_DeliveryDate"].Value).Year + ((DateTime)e.Row.Cells["d_DeliveryDate"].Value).Month + ((DateTime)e.Row.Cells["d_DeliveryDate"].Value).Day).ToString();
                string FechaActual = (DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day).ToString();

                //Si el contenido de la columna Vip es igual a SI
                if (DeliveryDate == FechaActual)
                {
                    //Escojo 2 colores
                    e.Row.Appearance.BackColor = Color.Pink;
                    e.Row.Appearance.BackColor2 = Color.Pink;
                    //Y doy el efecto degradado vertical
                    //e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
                }
            }
           
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (rbListado.Checked == true)
            {

            }
            else if (rbProtocolo.Checked == true)
            {
                ServiceOrderBL oServiceOrderBL = new ServiceOrderBL();
                var Lista = oServiceOrderBL.ObtenerProtocolos(_ServiceOrderId);
                List<string> ListaProtocolos = new List<string>();
                foreach (var item in Lista)
                {
                    ListaProtocolos.Add(item.Value1);
                }

                var frm = new Reports.frmConsolidateServiceOrder(_ServiceOrderId, ListaProtocolos);
                frm.ShowDialog();

            }

            //foreach (var item in Lista)
            //{
            //    Reports.frmReportOrderService frm = new Reports.frmReportOrderService(_ServiceOrderId,item.Value1);
            //    frm.ShowDialog();
            //}
          
        }

        private void grdData_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            // Setear privilegios / permisos             

            btnReport.Enabled = (grdData.Selected.Rows.Count > 0 && _isPrintEnabled);
            btnCotizacion.Enabled = (grdData.Selected.Rows.Count > 0 && _isPrintEnabled);
            btnDelete.Enabled = (grdData.Selected.Rows.Count > 0 && _isDeleteEnabled);
            btnEdit.Enabled   = (grdData.Selected.Rows.Count > 0 && _isEditEnabled);
            btnSendEmail.Enabled = (grdData.Selected.Rows.Count > 0 && _isEditEnabled);

            if (grdData.Selected.Rows.Count == 0)
                return;

            _ServiceOrderId = grdData.Selected.Rows[0].Cells["v_ServiceOrderId"].Value.ToString();
            //_ProtocolId = grdData.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();
        }

        private void dtpDateTimeStar_Validating(object sender, CancelEventArgs e)
        {
            if (dtpDateTimeStar.Value.Date > dptDateTimeEnd.Value.Date)
            {
                e.Cancel = true;
                MessageBox.Show("El campo fecha Inicial no puede ser Mayor a la fecha final.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void dptDateTimeEnd_Validating(object sender, CancelEventArgs e)
        {
            if (dptDateTimeEnd.Value.Date < dtpDateTimeStar.Value.Date)
            {
                e.Cancel = true;
                MessageBox.Show("El campo fecha final no puede ser Menor a la fecha Inicial.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnCotizacion_Click(object sender, EventArgs e)
        {
            if (rbListado.Checked == true)
            {

            }
            else if (rbProtocolo.Checked == true)
            {
                ServiceOrderBL oServiceOrderBL = new ServiceOrderBL();
                var Lista = oServiceOrderBL.ObtenerProtocolos(_ServiceOrderId);
                List<string> ListaProtocolos = new List<string>();
                foreach (var item in Lista)
                {
                    ListaProtocolos.Add(item.Value1);
                }

                var frm = new Reports.frmConsolidateCotizacion(_ServiceOrderId, ListaProtocolos);
                frm.ShowDialog();
            }

            //Reports.frmReporteCotizacion frm = new Reports.frmReporteCotizacion(_ServiceOrderId);
            //frm.ShowDialog();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            if (rbListado.Checked == true)
            {

            }
            else if (rbProtocolo.Checked == true)
            {
                ServiceOrderBL oServiceOrderBL = new ServiceOrderBL();
                List<string> ListaProtocolos = new List<string>();
                var Lista = oServiceOrderBL.ObtenerProtocolos(_ServiceOrderId);
                foreach (var item in Lista)
                {
                    ListaProtocolos.Add(item.Value1);
                }

                frmEmailOrdenServicioCotizacion frm =
                    new frmEmailOrdenServicioCotizacion(_ServiceOrderId, ListaProtocolos);
                frm.ShowDialog();
            }
        }

        private void rbListado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbListado.Checked == true)
            {
                grdData.DisplayLayout.Bands[0].Columns["v_Comentary"].Header.Caption = "Empresa";

                label4.Enabled = false;
                ddlEmployerOrganization.Enabled = false;
                label5.Enabled = false;
                ddlWorkerOrganization.Enabled = false;
                label3.Enabled = false;
                ddlServiceOrderStatusId.Enabled = false;
                btnReport.Enabled = false;
                btnSendEmail.Enabled = false;

                label6.Visible = false;
                ddlCustomerOrganization.Visible = false;
                label4.Visible = false;
                ddlEmployerOrganization.Visible = false;
                label5.Visible = false;
                ddlWorkerOrganization.Visible = false;

                label7.Visible = true;
                txtPacient.Visible = true;
            }
        }

        private void rbProtocolo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbProtocolo.Checked == true)
            {
                grdData.DisplayLayout.Bands[0].Columns["v_Comentary"].Header.Caption = "Comentario";

                label4.Enabled = true;
                ddlEmployerOrganization.Enabled = true;
                label5.Enabled = true;
                ddlWorkerOrganization.Enabled = true;
                label3.Enabled = true;
                ddlServiceOrderStatusId.Enabled = true;
                btnReport.Enabled = true;
                btnSendEmail.Enabled = true;

                label6.Visible = true;
                ddlCustomerOrganization.Visible = true;
                label4.Visible = true;
                ddlEmployerOrganization.Visible = true;
                label5.Visible = true;
                ddlWorkerOrganization.Visible = true;

                label7.Visible = false;
                txtPacient.Visible = false;
            }

        }

        private void btnReportePDF_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            CotizacionocDtoList CotizacionDto = new CotizacionocDtoList();
            List<CotizacionocdetaillList> CotizacionList = new List<CotizacionocdetaillList>();

            string CotizacionId = grdData.Selected.Rows[0].Cells["v_ServiceOrderId"].Value.ToString();

            int tipoReport = 1;
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;
                var MedicalCenter = new ServiceBL().GetInfoMedicalCenterSede();
                CotizacionDto = _objServicOrderBL.GetCotizacionesbj(ref objOperationResult, CotizacionId);
                CotizacionList = _objServicOrderBL.GetCotizacionesListabj(ref objOperationResult, CotizacionId);

                string ruta = Common.Utils.GetApplicationConfigValue("rutaCotizacion").ToString();

                CotizacionesOc.CrearCotizacionesOc(tipoReport, CotizacionList, CotizacionDto, MedicalCenter, ruta + CotizacionId + "_" + tipoReport + ".pdf");
                this.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            CotizacionocDtoList CotizacionDto = new CotizacionocDtoList();
            List<CotizacionocdetaillList> CotizacionList = new List<CotizacionocdetaillList>();

            string CotizacionId = grdData.Selected.Rows[0].Cells["v_ServiceOrderId"].Value.ToString();

            int tipoReport = 2;
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;
                var MedicalCenter = new ServiceBL().GetInfoMedicalCenterSede();
                CotizacionDto = _objServicOrderBL.GetCotizacionesbj(ref objOperationResult, CotizacionId);
                CotizacionList = _objServicOrderBL.GetCotizacionesListabj(ref objOperationResult, CotizacionId);

                string ruta = Common.Utils.GetApplicationConfigValue("rutaCotizacion").ToString();

                CotizacionesOc.CrearCotizacionesOc(tipoReport, CotizacionList, CotizacionDto, MedicalCenter, ruta + CotizacionId + "_" + tipoReport + ".pdf");
                this.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            CotizacionocDtoList CotizacionDto = new CotizacionocDtoList();
            List<CotizacionocdetaillList> CotizacionList = new List<CotizacionocdetaillList>();

            string CotizacionId = grdData.Selected.Rows[0].Cells["v_ServiceOrderId"].Value.ToString();

            int tipoReport = 3;
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;
                var MedicalCenter = new ServiceBL().GetInfoMedicalCenterSede();
                CotizacionDto = _objServicOrderBL.GetCotizacionesbj(ref objOperationResult, CotizacionId);
                CotizacionList = _objServicOrderBL.GetCotizacionesListabj(ref objOperationResult, CotizacionId);

                string ruta = Common.Utils.GetApplicationConfigValue("rutaCotizacion").ToString();

                CotizacionesOc.CrearCotizacionesOc(tipoReport, CotizacionList, CotizacionDto, MedicalCenter, ruta + CotizacionId + "_" + tipoReport + ".pdf");
                this.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (rbListado.Checked == true)
            {
                string CotizacionId = grdData.Selected.Rows[0].Cells["v_ServiceOrderId"].Value.ToString();


                frmCotizacionOc frm = new frmCotizacionOc(CotizacionId, "Clone");
                frm.ShowDialog();
            }
            //else if (rbProtocolo.Checked == true)
            //{

            //    frmServiceOrderEdit frm = new frmServiceOrderEdit(_ServiceOrderId, _ProtocolId, "Edit");
            //    frm.ShowDialog();

            //    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            //    {
            //        // Refrescar grilla
            //        btnFilter_Click(sender, e);
            //    }
            //}
        }

        private void txtPacient_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPacient.Text != string.Empty)
            {
                objDataTemp = new List<ServiceOrderList>(objData.Where(p => p.v_Comentary.Contains(txtPacient.Text.ToUpper())));

                grdData.DataSource = objDataTemp;

                if (objDataTemp != null)
                {
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataTemp.Count());
                }

            }
            else
            {
                grdData.DataSource = objData;

                if (objData != null)
                {
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Reporte Cotizaciones del " + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text;
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdData, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
