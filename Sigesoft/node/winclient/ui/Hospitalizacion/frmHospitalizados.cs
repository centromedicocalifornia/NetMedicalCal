using System;
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
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid.DocumentExport;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Sigesoft.Node.Contasol.Integration;
using NetPdf;
using System.Data.SqlClient;
using Sigesoft.Node.WinClient.BE.Custom;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Configuration;
using System.Diagnostics;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmHospitalizados : Form
    {
        string strFilterExpression;
        List<HospitalizacionListNew> _objData = new List<HospitalizacionListNew>();
        HospitalizacionBL _objHospBL = new HospitalizacionBL();
        List<string> ListaComponentes = new List<string>();
        private string _ticketId;
        private List<TicketList> _tempTicket = null;
        private TicketBL _ticketlBL = new TicketBL();
        private HospitalizacionBL _hospitBL = new HospitalizacionBL();

        private ServiceBL _serviceBL = new ServiceBL();
        private PacientBL _pacientBL = new PacientBL();
        private OperationResult _objOperationResult = new OperationResult();
        private List<PersonList> personalList;
        private List<HospitalizacionListNew> hospitalizacionlList;
        private List<HospitalizacionServiceList> hospitalizacionServicelList;
        private List<TicketList> ticketlList;
        private List<TicketDetalleList> ticketdetallelList;
        private List<TicketDetalleList> _tempticketdetallelList = null;
        private List<HospitalizacionListNew> objData = new List<HospitalizacionListNew>();
        private List<HospitalizacionListNew> objDataTemp = new List<HospitalizacionListNew>();

        List<TicketDetalleList> ListaTickets = new List<TicketDetalleList>();
        string _serviceId;
        string _EmpresaClienteId;
        string _pacientId;
        string _customerOrganizationName;
        string _personFullName;
        string ruta;
        int _edad;
        private string v_ProtocoloId;

        private hospitalizacionserviceDto hospser = new hospitalizacionserviceDto();
        private ticketDto Ticket = new ticketDto();
        private protocolDto prot = new protocolDto();
        private serviceDto serv = new serviceDto();
        public frmHospitalizados()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            //List<string> Filters = new List<string>();
            //if (!string.IsNullOrEmpty(txtPacient.Text)) Filters.Add("v_Paciente.Contains(\"" + txtPacient.Text.Trim() + "\")");

            //Filters.Add("i_IsDeleted==0");
            //strFilterExpression = null;
            //if (Filters.Count > 0)
            //{
            //    foreach (string item in Filters)
            //    {
            //        strFilterExpression = strFilterExpression + item + " && ";
            //    }
            //    strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
            //}

           this.BindGrid();
           btnTicket.Enabled = false;
           btnAgregarExamenes.Enabled = false;
           btnEditExamen.Enabled = false;
           bntEliminarExamen.Enabled = false;
           btnAsignarHabitacion.Enabled = false;
           btnReportePDF.Enabled = false;
           btnReportePDF.Enabled = false;
           btnDarAlta.Enabled = false;
           btnGenerarLiq.Enabled = false;
           btnLiberar.Enabled = false;
        }
        //
        private void BindGrid()
        {
            objData = GetData(0, null, "", strFilterExpression);

            grdData.DataSource = objData;

            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
            if (objData.Count() >= 1)
            {
                btnExport.Enabled = true;
            }
            else
            {
                btnExport.Enabled = false;
            }

            //this.grdData.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;

        }

        private void grdData_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
          
        }

        private List<HospitalizacionListNew> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            _objData = _objHospBL.GetHospitalizacionPagedAndFilteredNew(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);

            if (!String.IsNullOrEmpty(txtPacient.Text))
            {
                List<HospitalizacionListNew> Data = new List<HospitalizacionListNew>(_objData.Where(p => p.v_Paciente.Contains(txtPacient.Text.ToUpper())));
                _objData = new List<HospitalizacionListNew>(Data);

            }
            //foreach (var datos in _objData)
            //{
            //    if (datos.d_FechaAlta.Value.ToString() != "" || datos.d_FechaAlta.Value.ToString() != null)
            //    {
            //        grdData.Row[0].Band.AutoPreviewEnabled = false;
            //    }
            //}
            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void btnTicket_Click(object sender, EventArgs e)
        {
            // obtener serviceId y protocolId
            var ServiceId = grdData.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
            var protocolId = grdData.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();

            #region Conexion SAM Obtener Plan
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena1 = "select PL.i_PlanId from [dbo].[plan] PL where PL.v_ProtocoloId ='" + protocolId + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            string plan = "";
            while (lector.Read()){plan = lector.GetValue(0).ToString();}
            lector.Close();
            conectasam.closesigesoft();
            string modoMasterService;
            if (plan != ""){modoMasterService = "ASEGU";}
            else{modoMasterService = "HOSPI";}
            #endregion
            // construir formulario ticket
            frmTicket ticket = new frmTicket(_tempTicket, ServiceId, string.Empty, "New", protocolId, modoMasterService);
            ticket.ShowDialog();
            btnFilter_Click(sender, e);
            btnTicket.Enabled = false;
        }

        private void txtHospitalizados_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void calendar1Hospitalizados_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void calendar2Hospitalizados_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void btnEditarTicket_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            var ServiceId = grdData.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
            var ticketId = grdData.Selected.Rows[0].Cells["v_TicketId"].Value.ToString();
            #region Conexion SAM
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            #endregion
            var cadena1 = "select PL.i_PlanId from service SR inner join protocol PR on SR.v_ProtocolId = PR.v_ProtocolId inner join [dbo].[plan] PL on PR.v_ProtocolId = PL.v_ProtocoloId where SR.v_ServiceId ='" + ServiceId + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            string plan = "";
            string protocolId = "";
            while (lector.Read())
            {
                plan = lector.GetValue(0).ToString();
            }
            lector.Close();
            conectasam.closesigesoft();
            ServiceList personData = _serviceBL.GetServicePersonData(ref objOperationResult, ServiceId);
            string modoMasterService;
            if (plan != "")
            {
                modoMasterService = "ASEGU";
            }
            else
            {
                modoMasterService = "HOSPI";
            }
            _ticketId = ticketId;
            frmTicket ticket = new frmTicket(_tempTicket, ServiceId, _ticketId, "Edit", personData.v_ProtocolId, modoMasterService);
            ticket.ShowDialog();

            btnFilter_Click(sender, e);
        }

        private void grd_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            bool activador = false;
            foreach (UltraGridRow rowSelected in this.grdData.Selected.Rows)
            {
                if (rowSelected.Band.Index.ToString() == "0")
                {
                    contextMenuStrip2.Items["itemCerrarHabitacion"].Enabled = false;    
                    if (grdData.Selected.Rows[0].Cells["d_FechaAlta"].Value != null)
                    {
                        btnTicket.Enabled = false;
                        btnEditarTicket.Enabled = false;
                        btnEliminarTicket.Enabled = false;
                        btnImprimirTicket.Enabled = false;
                        btnAgregarExamenes.Enabled = false;
                        btnAsignarHabitacion.Enabled = false;
                        btnEditarHabitacion.Enabled = false;
                        btnEliminarHabitacion.Enabled = false;
                        btnDarAlta.Enabled = false;
                        activador = true;
                        contextMenuStrip2.Items["itemLimpieza"].Enabled = false;
                        btnReportePDF.Enabled = true;
                        btnLiberar.Enabled = true;
                    }
                    else
                    {
                        btnAsignarHabitacion.Enabled = true;
                        btnImprimirTicket.Enabled = false;
                        btnReportePDF.Enabled = true;
                        btnDarAlta.Enabled = true;
                        btnReportePDF.Enabled = true;
                        contextMenuStrip2.Items["itemLimpieza"].Enabled = false;
                        btnLiberar.Enabled = false;
                    }
                    
                }

                if (rowSelected.Band.Index.ToString() == "1")
                {
                    contextMenuStrip2.Items["itemCerrarHabitacion"].Enabled = false;    
                    contextMenuStrip2.Items["itemLimpieza"].Enabled = false;
                    if (grdData.Selected.Rows[0].Cells["d_FechaAlta"].Value != null)
                    {
                        btnTicket.Enabled = false;
                        btnEditarTicket.Enabled = false;
                        btnEliminarTicket.Enabled = false;
                        btnImprimirTicket.Enabled = false;
                        btnAgregarExamenes.Enabled = false;
                        btnEditExamen.Enabled = false;
                        bntEliminarExamen.Enabled = false;
                        btnAsignarHabitacion.Enabled = false;
                        btnEditarHabitacion.Enabled = false;
                        btnEliminarHabitacion.Enabled = false;
                        btnDarAlta.Enabled = false;
                        activador = true;
                        btnGenerarLiq.Enabled = false;
                        btnReportePDF.Enabled = false;
                        btnLiberar.Enabled = false;
                    }
                    else
                    {
                        btnTicket.Enabled = true;
                        btnAgregarExamenes.Enabled = true;
                        btnEditExamen.Enabled = false;
                        bntEliminarExamen.Enabled = false;
                        btnEditarTicket.Enabled = false;
                        btnEliminarTicket.Enabled = false;
                        btnImprimirTicket.Enabled = false;
                        btnAsignarHabitacion.Enabled = false;
                        btnEditarHabitacion.Enabled = false;
                        btnEliminarHabitacion.Enabled = false;
                        btnDarAlta.Enabled = false;
                        btnReportePDF.Enabled = false;
                        btnLiberar.Enabled = false;
                    }
                }

                if (rowSelected.Band.Index.ToString() == "2")
                {
                    contextMenuStrip2.Items["itemCerrarHabitacion"].Enabled = false;    
                    contextMenuStrip2.Items["itemLimpieza"].Enabled = false;
                    if (grdData.Selected.Rows[0].Cells["d_FechaAlta"].Value != null)
                    {
                        btnTicket.Enabled = false;
                        btnEditarTicket.Enabled = false;
                        btnEliminarTicket.Enabled = false;
                        btnImprimirTicket.Enabled = true;
                        btnAgregarExamenes.Enabled = false;
                        btnEditExamen.Enabled = false;
                        bntEliminarExamen.Enabled = false;
                        btnAsignarHabitacion.Enabled = false;
                        btnEditarHabitacion.Enabled = false;
                        btnEliminarHabitacion.Enabled = false;
                        btnDarAlta.Enabled = false;
                        btnGenerarLiq.Enabled = false;
                        activador = true;
                        btnReportePDF.Enabled = false;
                        btnLiberar.Enabled = false;
                    }
                    else
                    {
                        btnEditarTicket.Enabled = true;
                        btnEliminarTicket.Enabled = true;
                        btnImprimirTicket.Enabled = true;
                        btnTicket.Enabled = false;
                        btnAgregarExamenes.Enabled = false;
                        btnEditExamen.Enabled = false;
                        bntEliminarExamen.Enabled = false;
                        btnAsignarHabitacion.Enabled = false;
                        btnEditarHabitacion.Enabled = false;
                        btnEliminarHabitacion.Enabled = false;
                        btnDarAlta.Enabled = false;
                        btnReportePDF.Enabled = false;
                        btnLiberar.Enabled = false;
                    }
                }

                if (rowSelected.Band.Index.ToString() == "3")
                {
                    contextMenuStrip2.Items["itemCerrarHabitacion"].Enabled = false;    
                    contextMenuStrip2.Items["itemLimpieza"].Enabled = false;
                    btnTicket.Enabled = false;
                    btnEditarTicket.Enabled = false;
                    btnEliminarTicket.Enabled = false;
                    btnImprimirTicket.Enabled = false;
                    btnAgregarExamenes.Enabled = false;
                    btnEditExamen.Enabled = false;
                    bntEliminarExamen.Enabled = false;
                    btnAsignarHabitacion.Enabled = false;
                    btnEditarHabitacion.Enabled = false;
                    btnEliminarHabitacion.Enabled = false;
                    btnDarAlta.Enabled = false;
                    activador = true;
                    btnReportePDF.Enabled = false;
                    btnLiberar.Enabled = false;
                }
                if (rowSelected.Band.Index.ToString() == "4")
                {
                    contextMenuStrip2.Items["itemCerrarHabitacion"].Enabled = false;                    
                    contextMenuStrip2.Items["itemLimpieza"].Enabled = false;
                    contextMenuStrip2.Items["itemEditarExamen"].Enabled = true;
                    btnTicket.Enabled = false;
                    btnEditarTicket.Enabled = false;
                    btnEliminarTicket.Enabled = false;
                    btnImprimirTicket.Enabled = false;
                    btnAgregarExamenes.Enabled = false;
                    if (grdData.Selected.Rows[0].Cells["d_FechaAlta"].Value != null)
                    {
                        btnEditExamen.Enabled = false;
                        bntEliminarExamen.Enabled = false;
                    }
                    else
                    {
                        btnEditExamen.Enabled = true;
                        bntEliminarExamen.Enabled = true;
                    }
                    btnAsignarHabitacion.Enabled = false;
                    btnEditarHabitacion.Enabled = false;
                    btnEliminarHabitacion.Enabled = false;
                    btnDarAlta.Enabled = false;
                    activador = true;
                    btnReportePDF.Enabled = false;
                    btnLiberar.Enabled = false;
                }
                else
                {
                    contextMenuStrip2.Items["itemEditarExamen"].Enabled = false;
                }

                if (rowSelected.Band.Index.ToString() == "5")
                {

                    contextMenuStrip2.Items["itemCerrarHabitacion"].Enabled = true;
                    contextMenuStrip2.Items["itemLimpieza"].Enabled = true;
                    if (grdData.Selected.Rows[0].Cells["d_FechaAlta"].Value != null)
                    {
                        btnTicket.Enabled = false;
                        btnEditarTicket.Enabled = false;
                        btnEliminarTicket.Enabled = false;
                        btnImprimirTicket.Enabled = false;
                        btnAgregarExamenes.Enabled = false;
                        btnEditExamen.Enabled = false;
                        bntEliminarExamen.Enabled = false;
                        btnAsignarHabitacion.Enabled = false;
                        btnEditarHabitacion.Enabled = false;
                        btnEliminarHabitacion.Enabled = false;
                        btnDarAlta.Enabled = false;
                        activador = true;
                        btnReportePDF.Enabled = false;
                        btnLiberar.Enabled = false;
                    }
                    else
                    {
                        btnEditarHabitacion.Enabled = true;
                        btnEliminarHabitacion.Enabled = true;
                        btnTicket.Enabled = false;
                        btnEditarTicket.Enabled = false;
                        btnEliminarTicket.Enabled = false;
                        btnImprimirTicket.Enabled = false;
                        btnAgregarExamenes.Enabled = false;
                        btnEditExamen.Enabled = false;
                        bntEliminarExamen.Enabled = false;
                        btnDarAlta.Enabled = false;
                        btnReportePDF.Enabled = false;
                        btnLiberar.Enabled = false;
                    }
                }

                


            }

            if (grdData.Selected.Rows.Count == 0)
                 return;

            btnExport.Enabled = grdData.Rows.Count > 0;
        }

        private void btnAsignarHabitacion_Click(object sender, EventArgs e)
        {
            try
            {
                var v_HopitalizacionId = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();
                
                #region Conexion SIGESOFT Obtener Protocolo
                ConexionSigesoft conectasam = new ConexionSigesoft();
                conectasam.opensigesoft();
                var cadena1 = "select PR.v_ProtocolId " +
                              "from hospitalizacionservice HS " +
                              "inner join service SR on SR.v_ServiceId= HS.v_ServiceId " +
                              "inner join protocol PR on SR.v_ProtocolId=PR.v_ProtocolId " +
                              "where v_HopitalizacionId='"+v_HopitalizacionId+"'";
                SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                SqlDataReader lector = comando.ExecuteReader();
                v_ProtocoloId = "";
                while (lector.Read()) { v_ProtocoloId = lector.GetValue(0).ToString(); }
                lector.Close();
                conectasam.closesigesoft();
                #endregion

                #region Conexion SIGESOFT Obtener Plan
                conectasam = new ConexionSigesoft();
                conectasam.opensigesoft();
                cadena1 = "select PL.i_PlanId from [dbo].[plan] PL where PL.v_ProtocoloId ='" + v_ProtocoloId + "'";
                comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                lector = comando.ExecuteReader();
                string plan = "";
                while (lector.Read()) { plan = lector.GetValue(0).ToString(); }
                lector.Close();
                conectasam.closesigesoft();
                #endregion
                string modoMasterService;
                if (plan != "") { modoMasterService = "ASEGU"; }
                else { modoMasterService = "HOSPI"; }
                //var hospitalizacionId = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();
                //frmHabitacion frm = new frmHabitacion(hospitalizacionId, "New" + modoMasterService, "", v_ProtocoloId);
                var frm = new frmHabitaciones(v_HopitalizacionId, "New" + modoMasterService, "", v_ProtocoloId);
                frm.ShowDialog();
                btnFilter_Click(sender, e);
                btnAsignarHabitacion.Enabled = false;

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                MessageBox.Show("NO SE PUEDE DAR DE ALTA - YA ASIGNADO", "ERROR", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                //throw;
            }

        }

        private void btnEditarHabitacion_Click(object sender, EventArgs e)
        {
            try
            {
                var v_HopitalizacionId = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();

                #region Conexion SIGESOFT Obtener Protocolo
                ConexionSigesoft conectasam = new ConexionSigesoft();
                conectasam.opensigesoft();
                var cadena1 = "select PR.v_ProtocolId " +
                              "from hospitalizacionservice HS " +
                              "inner join service SR on SR.v_ServiceId= HS.v_ServiceId " +
                              "inner join protocol PR on SR.v_ProtocolId=PR.v_ProtocolId " +
                              "where v_HopitalizacionId='" + v_HopitalizacionId + "'";
                SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                SqlDataReader lector = comando.ExecuteReader();
                v_ProtocoloId = "";
                while (lector.Read()) { v_ProtocoloId = lector.GetValue(0).ToString(); }
                lector.Close();
                conectasam.closesigesoft();
                #endregion

                #region Conexion SIGESOFT Obtener Plan
                conectasam = new ConexionSigesoft();
                conectasam.opensigesoft();
                cadena1 = "select PL.i_PlanId from [dbo].[plan] PL where PL.v_ProtocoloId ='" + v_ProtocoloId + "'";
                comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                lector = comando.ExecuteReader();
                string plan = "";
                while (lector.Read()) { plan = lector.GetValue(0).ToString(); }
                lector.Close();
                conectasam.closesigesoft();
                #endregion
                string modoMasterService;
                if (plan != "") { modoMasterService = "ASEGU"; }
                else { modoMasterService = "HOSPI"; }
                var hospitalizacionId = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();
                var hospitalizacionHabitacionId = grdData.Selected.Rows[0].Cells["v_HospitalizacionHabitacionId"].Value.ToString();

                frmHabitaciones frm = new frmHabitaciones(hospitalizacionId, "Edit" + modoMasterService, hospitalizacionHabitacionId, v_ProtocoloId);


                //frmHabitacion frm = new frmHabitacion(hospitalizacionId, "Edit" + modoMasterService, hospitalizacionHabitacionId, v_ProtocoloId);
                //frmHabitacion frm = new frmHabitacion(hospitalizacionId, "Edit", hospitalizacionHabitacionId, v_ProtocoloId);

                frm.ShowDialog();
                btnFilter_Click(sender, e);
                btnEditarHabitacion.Enabled = false;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                MessageBox.Show("NO SE PUEDE EDITAR HABITACIÓN - YA ASIGNADO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //throw;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Reporte Hospitalización del " + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text;
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdData, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            //saveFileDialog1.FileName = string.Empty;
            //saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    this.ultraGridExcelExporter1.Export(this.grdData, saveFileDialog1.FileName);
            //    MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}      
        }

        private void btnAgregarExamenes_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            var serviceId = grdData.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
            var protocolId = grdData.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();
            var NroHospitalizacion = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();
            var dni = grdData.Selected.Rows[0].Cells["v_DocNumber"].Value.ToString();
            #region Conexion SAM
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            #endregion
            var cadena1 = "select PL.i_PlanId from [dbo].[plan] PL where PL.v_ProtocoloId ='" + protocolId + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            string plan = "";
            while (lector.Read())
            {
                plan = lector.GetValue(0).ToString();
            }
            lector.Close();
            conectasam.closesigesoft();

            var ListServiceComponent = new ServiceBL().GetServiceComponents_(ref objOperationResult, serviceId);
            ListaComponentes = new List<string>();
            foreach (var item in ListServiceComponent)
            {
                ListaComponentes.Add(item.v_ComponentId);
            }

            if (plan != "")
            {
                var frm = new frmAddExam(ListaComponentes, "ASEGU", protocolId, "Asegu", NroHospitalizacion, dni, serviceId, null) { _serviceId = serviceId };
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    btnFilter_Click(sender, e);
            }
            else
            {
                var frm = new frmAddExam(ListaComponentes, "HOSPI", protocolId, "Hospi", NroHospitalizacion, dni, serviceId, null) { _serviceId = serviceId };
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    btnFilter_Click(sender, e);
            }
            
                
        }

        private void btnReportePDF_Click(object sender, EventArgs e)
        {
            var hospitId = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();

            frmLiquidacionReport liquidacionReport = new frmLiquidacionReport(hospitId);
            liquidacionReport.ShowDialog();

               
        }

        private void frmHospitalizados_Load(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
        }

        private void btnDarAlta_Click(object sender, EventArgs e)
        {
            try
            {
                var hospitalizacionId = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();
                DateTime? fechaAlta = (DateTime?) (grdData.Selected.Rows[0].Cells["d_FechaAlta"].Value == null
                    ? (ValueType) null
                    : DateTime.Parse(grdData.Selected.Rows[0].Cells["d_FechaAlta"].Value.ToString()));
                var comentario = grdData.Selected.Rows[0].Cells["v_Comentario"].Value == null
                    ? ""
                    : grdData.Selected.Rows[0].Cells["v_Comentario"].Value.ToString();
                var frm = new frmDarAlta(hospitalizacionId, "Edit", fechaAlta, comentario);
                frm.ShowDialog();
                btnFilter_Click(sender, e);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                MessageBox.Show("NO SE PUEDE DAR DE ALTA - YA ASIGNADO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //throw;
            }
        }

        private void grdData_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            foreach (UltraGridRow rowSelected in this.grdData.Rows)
            {
                var banda = e.Row.Band.Index.ToString();

                if (banda == "0")
                {
                    if (rowSelected.Band.Index.ToString() == "0")
                    {
                        if (e.Row.Cells["d_FechaAlta"].Value!=null)
                        {
                            
                            e.Row.Appearance.BackColor = Color.Yellow;

                            e.Row.Appearance.BackColor2 = Color.White;
                            btnTicket.Enabled = false;
                            btnEditarTicket.Enabled = false;
                            btnEliminarTicket.Enabled = false;
                            btnImprimirTicket.Enabled = false;
                            btnAgregarExamenes.Enabled = false;
                            btnEditExamen.Enabled = false;
                            bntEliminarExamen.Enabled = false;
                            btnAsignarHabitacion.Enabled = false;
                            btnEditarHabitacion.Enabled = false;
                            btnEliminarHabitacion.Enabled = false;
                            btnDarAlta.Enabled = false;
                            btnLiberar.Enabled = false;
                            //Y doy el efecto degradado vertical
                            e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
                            if (e.Row.Cells["MedicoPago"].Value.ToString() == "S-L" && e.Row.Cells["PacientePago"].Value.ToString() == "S-L" )//MedicoPago PacientePago
                            {
                                e.Row.Appearance.BackColor = Color.Red;

                                e.Row.Appearance.BackColor2 = Color.White;
                                btnTicket.Enabled = false;
                                btnEditarTicket.Enabled = false;
                                btnEliminarTicket.Enabled = false;
                                btnImprimirTicket.Enabled = false;
                                btnAgregarExamenes.Enabled = false;
                                btnEditExamen.Enabled = false;
                                bntEliminarExamen.Enabled = false;
                                btnAsignarHabitacion.Enabled = false;
                                btnEditarHabitacion.Enabled = false;
                                btnEliminarHabitacion.Enabled = false;
                                btnDarAlta.Enabled = false;
                                btnLiberar.Enabled = false;
                                //Y doy el efecto degradado vertical
                                e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
                                
                            }
                            //if (e.Row.Cells["d_MontoPagado"].Value.ToString() != "0" && e.Row.Cells["d_MontoPagado"].Value.ToString() != "0.00")
                            //{

                            if (decimal.Parse(e.Row.Cells["d_MontoPagado"].Value.ToString()) != 0)
                            {
                                e.Row.Appearance.BackColor = Color.GreenYellow;

                                e.Row.Appearance.BackColor2 = Color.White;
                                btnTicket.Enabled = false;
                                btnEditarTicket.Enabled = false;
                                btnEliminarTicket.Enabled = false;
                                btnImprimirTicket.Enabled = false;
                                btnAgregarExamenes.Enabled = false;
                                btnEditExamen.Enabled = false;
                                bntEliminarExamen.Enabled = false;
                                btnAsignarHabitacion.Enabled = false;
                                btnEditarHabitacion.Enabled = false;
                                btnEliminarHabitacion.Enabled = false;
                                btnDarAlta.Enabled = false;
                                btnLiberar.Enabled = false;
                                //Y doy el efecto degradado vertical
                                e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
                            }
                        }


                    }
                }
                if (banda == "2")
                {
                    if (rowSelected.Band.Index.ToString() == "0")
                    {
                        if (e.Row.Cells["TicketInterno"].Value == "SI")
                        {
                            e.Row.Appearance.BackColor = Color.LightGreen;
                            e.Row.Appearance.BackColor2 = Color.White;
                            //Y doy el efecto degradado vertical
                            e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
                        }
                    }
                }

            }
        }

        private void btnEliminarTicket_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar el ticket?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                OperationResult objOperationResult = new OperationResult();
                TicketBL oTicketBL = new TicketBL();

                var ServiceId = grdData.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
                var ticketId = grdData.Selected.Rows[0].Cells["v_TicketId"].Value.ToString();
                oTicketBL.DeleteTicket(ticketId, Globals.ClientSession.GetAsList());

            }
        }

        private void btnGenerarLiq_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            var HopitalizacionId = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();
            
            _serviceBL.GenerarLiquidacionHospitalizacion(ref objOperationResult, HopitalizacionId, Globals.ClientSession.GetAsList());

            btnFilter_Click(sender, e);

        }

        private void grdData_MouseDown(object sender, MouseEventArgs e)
        {
            System.Drawing.Point point = new System.Drawing.Point(e.X, e.Y);
            Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

            if (uiElement == null || uiElement.Parent == null)
                return;

            Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

            if (row != null)
            {
                contextMenuStrip2.Items["btnRemoverEsamen"].Enabled = true;
            }
            else
            {
                contextMenuStrip2.Items["btnRemoverEsamen"].Enabled = false;
                contextMenuStrip2.Items["itemLimpieza"].Enabled = false;
            }

        }

        private void btnRemoverEsamen_Click(object sender, EventArgs e)
        {
            CalendarBL _objCalendarBL = new CalendarBL();
             if (grdData.Selected.Rows.Count == 0)
                return;

            ServiceBL oServiceBL = new ServiceBL();
            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?", "ADVERTENCIA!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                var _auxiliaryExams = new List<ServiceComponentList>();
                OperationResult objOperationResult = new OperationResult();

                string v_ServiceComponentId = grdData.Selected.Rows[0].Cells["ServiceComponentId"].Value.ToString();
                string v_ServiceId = grdData.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();


                ServiceComponentList auxiliaryExam = new ServiceComponentList();
                auxiliaryExam.v_ServiceComponentId = v_ServiceComponentId;
                _auxiliaryExams.Add(auxiliaryExam);

                _objCalendarBL.UpdateAdditionalExam(_auxiliaryExams, v_ServiceId, (int?)SiNo.NO, Globals.ClientSession.GetAsList());
                btnFilter_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            var hospitalizacionId = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();
            var hospitalizacionHabitacionId = grdData.Selected.Rows[0].Cells["v_HospitalizacionHabitacionId"].Value.ToString();

            var habtacion = new HospitalizacionHabitacionBL().GetHabitacion(ref objOperationResult, hospitalizacionHabitacionId);

            habtacion.i_IsDeleted = 1;
            habtacion.i_EstateRoom = 3;

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar habitación?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                new HospitalizacionHabitacionBL().UpdateHospitalizacionHabitacion(ref objOperationResult, habtacion, Globals.ClientSession.GetAsList());

                //this.Close();
               
               btnFilter_Click(sender, e);
            }
            //else
            //{
            //    this.Close();

            //}

            //btnFilter_Click(sender, e);
            //btnEliminarHabitacion.Enabled = false;
        }

        private void grdData_AfterRowUpdate(object sender, RowEventArgs e)
        {

        }

        private void btnLiberar_Click(object sender, EventArgs e)
        {
            Validar validacion = new Validar();
            validacion.ShowDialog();
            if (validacion.Respuesta == "ACEPTADO")
            {
                //MessageBox.Show("PASE CORRECTO", "ACEPTADO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;

                var v_HopitalizacionId = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();

                bool HabitacionEnUso = new HabitacionBL().GetHabitacionByHabitacionId(v_HopitalizacionId);



                #region Conexion SAM
                ConexionSigesoft conectasam = new ConexionSigesoft();
                conectasam.opensigesoft();
                #endregion

                var cadena1 = "update hospitalizacion set " +
                              "d_FechaAlta = NULL , v_ComentaryUpdate = CONCAT('" + validacion.Usuario + " - " + DateTime.Now + " | ', v_ComentaryUpdate)" +
                              "where v_HopitalizacionId='" + v_HopitalizacionId + "' ";
                SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                SqlDataReader lector = comando.ExecuteReader();
                lector.Close();
                btnFilter_Click(sender, e);
                MessageBox.Show("Se liberó el registro exitosamente.", "Información", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            
        }


        private void btnVerHabitaciones_Click(object sender, EventArgs e)
        {
            //var v_HopitalizacionId = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();
            var frm = new frmHabitaciones("", "View", "", "");
            frm.ShowDialog();
        }

        private void itemLimpieza_Click(object sender, EventArgs e)
        {
            if (grdData.Selected.Rows.Count == 0)
                return;
            var fechaFin = grdData.Selected.Rows[0].Cells["d_EndDate"].Value;
            if (fechaFin == null)
            {
                MessageBox.Show("La habitación está ocupada y no puede pasar a limpieza directamente.",
                    "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var v_HopitalizacionId = grdData.Selected.Rows[0].ParentCollection[0].Cells["v_HopitalizacionId"].Value.ToString();
            var nroHabitacion = grdData.Selected.Rows[0].Cells["NroHabitacion"].Value.ToString();
            
            var DialogResult = MessageBox.Show("Se pondrá en limpieza la habitación, ¿desea continuar?",
                "CONFIRMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.Yes)
            {
                new HabitacionBL().UpdateEstateHabitacionLimpieza(v_HopitalizacionId, nroHabitacion);
            }
        }

        private void btnImprimirTicket_Click(object sender, EventArgs e)
        {
            OperationResult _objOperationResult = new OperationResult();
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;

                var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

                var ticketId = grdData.Selected.Rows[0].Cells["v_TicketId"].Value.ToString();

                var lista = _hospitBL.BuscarTicketsDetalle(ticketId);

                //var serviceId = lista.SelectMany(p => p.Servicios.Select(q=>q.v_ServiceId));
                //int doctor = 1;
                Ticket = _hospitBL.GetHospitServTicket(ticketId);
                hospser = _hospitBL.GetHospitServwithTicekt(Ticket.v_ServiceId);

                serv = _hospitBL.GetService(Ticket.v_ServiceId);
                prot = _hospitBL.GetProtocol(serv.v_ProtocolId);

                var datosP = _pacientBL.DevolverDatosPaciente(Ticket.v_ServiceId);

                string ruta = Common.Utils.GetApplicationConfigValue("rutaTicketsH").ToString();
                ServiceList personData = _serviceBL.GetServicePersonData(ref _objOperationResult, hospser.v_ServiceId);

                var hospitalizacion = _hospitBL.GetHospitalizacion(ref _objOperationResult, hospser.v_HopitalizacionId);
                var hospitalizacionhabitacion = _hospitBL.GetHospitalizacionHabitacion(ref _objOperationResult, hospser.v_HopitalizacionId);
                var medicoTratante = new ServiceBL().GetMedicoTratante(Ticket.v_ServiceId);
                
                string nombre = "Ticket N° " + ticketId + "_" + personData.v_DocNumber;

                TicketHosp.CreateTicket(ruta + nombre + ".pdf", MedicalCenter, lista, datosP, hospitalizacion, hospitalizacionhabitacion, medicoTratante, Ticket, prot);

                this.Enabled = true;
            }
            //this.Close();

        }

        private void itemCerrarHabitacion_Click(object sender, EventArgs e)
        {
            var v_HopitalizacionId = grdData.Selected.Rows[0].ParentCollection[0].Cells["v_HopitalizacionId"].Value.ToString();
            bool IsUpdateHabitacion = new HabitacionBL().UpdateEstateHabitacionByHospId(v_HopitalizacionId);
            if (IsUpdateHabitacion)
            {
                MessageBox.Show(
                    "El estado de la habitación será de 'En Limpieza', por favor dar aviso al personal correspondiente",
                    "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGrid();
            }
            else
            {
                MessageBox.Show(
                    "Sucedió un error, por favor vuelva a intentar.",
                    "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void editarExamenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string serviceComponentId = grdData.Selected.Rows[0].Cells["ServiceComponentId"].Value.ToString();
            string serviceComponentName = grdData.Selected.Rows[0].Cells["Componente"].Value.ToString();
            frmEditServiceComponent from = new frmEditServiceComponent(serviceComponentId, serviceComponentName);
            from.ShowDialog();
            btnFilter_Click(sender, e);
        }

        private void btnEditExamen_Click(object sender, EventArgs e)
        {
            var ServiceComponentId = grdData.Selected.Rows[0].Cells["ServiceComponentId"].Value.ToString();
            Hospitalizacion.CargoExamen from = new Hospitalizacion.CargoExamen(ServiceComponentId);
            from.ShowDialog();
        }

        private void bntEliminarExamen_Click(object sender, EventArgs e)
        {
            var ServiceComponentId = grdData.Selected.Rows[0].Cells["ServiceComponentId"].Value.ToString();
            #region CONSULTA
            ConexionSigesoft conectasam_1 = new ConexionSigesoft();
            conectasam_1.opensigesoft();
            var cadena1_1 = "select c.v_Name from [dbo].[servicecomponent] sc join [dbo].[component] c on sc.v_ComponentId = c.v_ComponentId  where sc.v_ServiceComponentId = '" + ServiceComponentId + "'";
            SqlCommand comando_1 = new SqlCommand(cadena1_1, connection: conectasam_1.conectarsigesoft);
            SqlDataReader lector_1 = comando_1.ExecuteReader();
            string Componente = "";
            while (lector_1.Read())
            {
                Componente = lector_1.GetValue(0).ToString();
            }
            lector_1.Close();
            conectasam_1.closesigesoft();

            #endregion
            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar el servicio " + Componente + " ? ", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                ConexionSigesoft conectasam = new ConexionSigesoft();
                conectasam.opensigesoft();
                var cadena1 = "update [dbo].[servicecomponent] set i_IsRequiredId = 0 , i_IsDeleted  = 1 where v_ServiceComponentId = '" + ServiceComponentId + "'";
                SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                SqlDataReader lector = comando.ExecuteReader();
                lector.Close();
                conectasam.closesigesoft();
            }
            btnFilter_Click(sender, e);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                OperationResult _objOperationResult = new OperationResult();

                var hospitId = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();

                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    string ruta = Common.Utils.GetApplicationConfigValue("LiquidacionHospitalizacion").ToString();
                    string rutaBasura = Common.Utils.GetApplicationConfigValue("rutaReportesBasura").ToString();
                    ///
                    //var objHospitalizacionId = new PacientBL().HospitService(hospitId);
                    List<HopsitService> objHospitalizacionServices = new PacientBL().HospitServiceList(hospitId);

                    //objetos
                    var objPacient = new PacientBL().GetDataPacientByServiceId(objHospitalizacionServices.Select(p=>p.v_ServiceId).FirstOrDefault()); 
                    var objSeguro = new PacientBL().Particulares_Information(objHospitalizacionServices.Select(p=>p.v_ServiceId).FirstOrDefault());
                    var objAseguradora = new OrganizationBL().GetDataAseguradoraByServiceiId(objHospitalizacionServices.Select(p => p.v_ServiceId).FirstOrDefault());
                    var objOrganization = new OrganizationBL().GetDataOrganizationByServiceiId(objHospitalizacionServices.Select(p => p.v_ServiceId).FirstOrDefault());

                    //listas
                    List<ServiciosDetalle> servicios = new ServiciosDetalleBL().ServiciosServicioDetalle(objHospitalizacionServices.Select(p => p.v_ServiceId).FirstOrDefault()); 
                    //List<HospitalizacionCustom> objHospitalizacion = new List<HospitalizacionCustom>();
                    List<MeidicinasTicketsLista> tickets = new ServiciosDetalleBL().Tickets_Detalle(objHospitalizacionServices.Select(p => p.v_ServiceId).FirstOrDefault());
                    List<RecetasDetalle> recetas = new ServiciosDetalleBL().Receta_Detalle(objHospitalizacionServices.Select(p => p.v_ServiceId).FirstOrDefault());
                    var objHospitalizacion = new PacientBL().GetDataHospitalizacionByServiceId(objHospitalizacionServices.Select(p => p.v_ServiceId).FirstOrDefault());

                    //foreach (var item in objHospitalizacionServices)
                    //{
                        ////
                        //var servicios_ = new ServiciosDetalleBL().ServiciosServicioDetalle(item.v_ServiceId);
                        
                        //foreach (var item2 in servicios_)
                        //{
                        //    servicios.Add(item2);
                        //}
                        
                        //var objHospitalizacion_ = new PacientBL().GetDataHospitalizacionByServiceId(item.v_ServiceId);
                        //foreach (var item2 in objHospitalizacion_)
                        //{
                        //    objHospitalizacion.Add(item2);
                        //}
                        //
                        //var tickets_ = new ServiciosDetalleBL().Tickets_Detalle(item.v_ServiceId);

                        //foreach (var item2 in tickets_)
                        //{
                        //    tickets.Add(item2);
                        //}
                        //
                        //var recetas_ = new ServiciosDetalleBL().Receta_Detalle(item.v_ServiceId);
                        //foreach (var item2 in recetas_)
                        //{
                        //    recetas.Add(item2);
                        //}
                    //}

                    

                    var habitaciones = new HospitalizacionBL().BuscarHospitalizacionHabitaciones(hospitId);

                    var hospitalizacion = new HospitalizacionBL().GetHosp(hospitId);

                    var hospitalizacionhabitacion = _hospitBL.GetHospitalizacionHabitacion(ref _objOperationResult, hospitId);
                    

                    string nroliq = "";
                    string pathFile = ruta + "LIQUIDACIÓN [DETALLADA]-" + hospitId + ".pdf";

                    int validador = 3;

                    LiquidacionParticularesHospDetalle.CreateLiquidacionParticulares(servicios, objPacient, objOrganization, objAseguradora, objHospitalizacion, objSeguro, tickets, recetas, pathFile, habitaciones, hospitalizacionhabitacion, hospitalizacion, validador);

                    this.Enabled = true;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Error Inesperado: " + err.ToString(),"AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OperationResult _objOperationResult = new OperationResult();

                var hospitId = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();

                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    string ruta = Common.Utils.GetApplicationConfigValue("LiquidacionHospitalizacion").ToString();
                    string rutaBasura = Common.Utils.GetApplicationConfigValue("rutaReportesBasura").ToString();
                    ///
                    var objHospitalizacionId = new PacientBL().HospitService(hospitId);

                    var objPacient = new PacientBL().GetDataPacientByServiceId(objHospitalizacionId.v_ServiceId);
                    var objSeguro = new PacientBL().Particulares_Information(objHospitalizacionId.v_ServiceId);
                    var objAseguradora = new OrganizationBL().GetDataAseguradoraByServiceiId(objHospitalizacionId.v_ServiceId);

                    //var servicios_ = new ServiciosDetalleBL().ServiciosServicioDetalle(objHospitalizacionId.v_ServiceId);
                    var servicios_ = new ServiciosDetalleBL().ServiciosServicioDetalleMedicoYPaciente(objHospitalizacionId.v_ServiceId);

                    var objHospitalizacion = new PacientBL().GetDataHospitalizacionByServiceId(objHospitalizacionId.v_ServiceId);

                    var tickets_ = new ServiciosDetalleBL().Tickets_Detalle(objHospitalizacionId.v_ServiceId);
                    var recetas_ = new ServiciosDetalleBL().Receta_Detalle(objHospitalizacionId.v_ServiceId);

                    var habitaciones = new HospitalizacionBL().BuscarHospitalizacionHabitaciones(hospitId);
                    var hospitalizacion = new HospitalizacionBL().GetHosp(hospitId);

                    var hospitalizacionhabitacion = _hospitBL.GetHospitalizacionHabitacion(ref _objOperationResult, hospitId);
                    var objOrganization = new OrganizationBL().GetDataOrganizationByServiceiId(objHospitalizacionId.v_ServiceId);

                    var lista = _hospitBL.GetHospitalizcion(ref _objOperationResult, hospitId);

                    string nroliq = "";
                    string pathFile = ruta + "LIQUIDACIÓN [DETALLADA]-" + hospitId + ".pdf";

                    int validador = 1;
                    #region MANDAR PRECIO A BASE
                    decimal totalFinal = 0;
                    foreach (var hospitalizacion_precios in lista)
                    {
                        var ListaServicios = hospitalizacion_precios.Servicios.FindAll(p => p.v_ServiceId != null);
                        decimal totalParcialMedicina = 0;
                        decimal sumaMedicina = 0;
                        decimal sumaServicio = 0;
                        foreach (var servicios in ListaServicios)
                        {
                            if (servicios.Tickets != null)
                            {
                                var ListaTickets = servicios.Tickets.FindAll(p => p.i_conCargoA == 1);
                                if (ListaTickets.Count() >= 1)
                                {
                                    foreach (var tickets in ListaTickets)
                                    {
                                        if (tickets.Productos != null)
                                        {
                                            var detalletickets = tickets.Productos.FindAll(p => p.d_Cantidad != 0);
                                            foreach (var Detalle in detalletickets)
                                            {
                                                int cantidad = (int)Detalle.d_Cantidad;
                                                totalParcialMedicina = (decimal)(Detalle.d_PrecioVenta * cantidad);
                                                sumaMedicina += (decimal)totalParcialMedicina;
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    totalParcialMedicina = decimal.Round(totalParcialMedicina, 2);
                                }
                            }

                            var ListaComponentes = servicios.Componentes.FindAll(p => p.Precio != 0 && p.i_conCargoA == 1);
                            foreach (var compo in ListaComponentes)
                            {
                                decimal compoPrecio = (decimal)compo.Precio;
                                sumaServicio += compoPrecio;
                            }
                        }

                        decimal totalParcialHabitacion = 0;
                        decimal sumaHabitacion = 0;
                        var ListaHabitaciones = hospitalizacion_precios.Habitaciones.FindAll(p => p.i_conCargoA == 1);

                        foreach (var habitacion in ListaHabitaciones)
                        {
                            DateTime inicio = habitacion.d_StartDate.Value.Date;
                            DateTime fin;

                            if (habitacion.d_EndDate != null || habitacion.d_EndDate.ToString() == "00/00/0000 0:0:0")
                            {
                                fin = habitacion.d_EndDate.Value.Date;
                            }
                            else
                            {
                                fin = DateTime.Now.Date;

                            }

                            TimeSpan nDias = fin - inicio;

                            int tSpan = nDias.Days;

                            //+ 1
                            int dias = 0;
                            if (tSpan == 0)
                            {
                                dias = tSpan + 1;
                            }
                            else
                            {
                                dias = tSpan;
                            }

                            decimal _habitacionPrecio = (decimal)habitacion.d_Precio;
                            _habitacionPrecio = decimal.Round(_habitacionPrecio, 2);

                            totalParcialHabitacion = (decimal)(habitacion.d_Precio * dias);
                            totalParcialHabitacion = decimal.Round(totalParcialHabitacion, 2);

                            sumaHabitacion += (decimal)totalParcialHabitacion;
                        }

                        totalFinal = sumaMedicina + sumaServicio + sumaHabitacion;

                        totalFinal = decimal.Round(totalFinal, 2);
                    }


                    var _Hospitalizacion = new HospitalizacionBL().GetHospitalizacion(ref _objOperationResult, hospitId);

                    _Hospitalizacion.d_PagoMedico = totalFinal;

                    _hospitBL.UpdateHospitalizacion(ref _objOperationResult, _Hospitalizacion, Globals.ClientSession.GetAsList());
                    #endregion

                    LiquidacionParticularesHospDetalle.CreateLiquidacionParticulares(servicios_, objPacient, objOrganization, objAseguradora, objHospitalizacion, objSeguro, tickets_, recetas_, pathFile, habitaciones, hospitalizacionhabitacion, hospitalizacion, validador);

                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Error Inesperado: " + err.ToString(), "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                OperationResult _objOperationResult = new OperationResult();

                var hospitId = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();

                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    string ruta = Common.Utils.GetApplicationConfigValue("LiquidacionHospitalizacion").ToString();
                    string rutaBasura = Common.Utils.GetApplicationConfigValue("rutaReportesBasura").ToString();
                    ///
                    var objHospitalizacionId = new PacientBL().HospitService(hospitId);

                    var objPacient = new PacientBL().GetDataPacientByServiceId(objHospitalizacionId.v_ServiceId);
                    var objSeguro = new PacientBL().Particulares_Information(objHospitalizacionId.v_ServiceId);
                    var objAseguradora = new OrganizationBL().GetDataAseguradoraByServiceiId(objHospitalizacionId.v_ServiceId);

                    //var servicios_ = new ServiciosDetalleBL().ServiciosServicioDetalle(objHospitalizacionId.v_ServiceId);
                    var servicios_ = new ServiciosDetalleBL().ServiciosServicioDetalleMedicoYPaciente(objHospitalizacionId.v_ServiceId);

                    var objHospitalizacion = new PacientBL().GetDataHospitalizacionByServiceId(objHospitalizacionId.v_ServiceId);

                    var tickets_ = new ServiciosDetalleBL().Tickets_Detalle(objHospitalizacionId.v_ServiceId);
                    var recetas_ = new ServiciosDetalleBL().Receta_Detalle(objHospitalizacionId.v_ServiceId);

                    var habitaciones = new HospitalizacionBL().BuscarHospitalizacionHabitaciones(hospitId);
                    var hospitalizacion = new HospitalizacionBL().GetHosp(hospitId);

                    var hospitalizacionhabitacion = _hospitBL.GetHospitalizacionHabitacion(ref _objOperationResult, hospitId);
                    var objOrganization = new OrganizationBL().GetDataOrganizationByServiceiId(objHospitalizacionId.v_ServiceId);

                    var lista = _hospitBL.GetHospitalizcion(ref _objOperationResult, hospitId);

                    string nroliq = "";
                    string pathFile = ruta + "LIQUIDACIÓN [DETALLADA]-" + hospitId + ".pdf";

                    int validador = 2;

                    #region MANDAR PRECIO A BASE
                    decimal totalFinal = 0;
                    foreach (var hospitalizacion_precios in lista)
                    {
                        var ListaServicios = hospitalizacion_precios.Servicios.FindAll(p => p.v_ServiceId != null);
                        decimal totalParcialMedicina = 0;
                        decimal sumaMedicina = 0;
                        decimal sumaServicio = 0;
                        foreach (var servicios in ListaServicios)
                        {
                            if (servicios.Tickets != null)
                            {
                                var ListaTickets = servicios.Tickets.FindAll(p => p.i_conCargoA == 2);
                                if (ListaTickets.Count() >= 1)
                                {
                                    foreach (var tickets in ListaTickets)
                                    {
                                        var detalletickets = tickets.Productos.FindAll(p => p.d_Cantidad != 0);
                                        foreach (var Detalle in detalletickets)
                                        {
                                            int cantidad = (int)Detalle.d_Cantidad;
                                            totalParcialMedicina = (decimal)(Detalle.d_PrecioVenta * cantidad);
                                            sumaMedicina += (decimal)totalParcialMedicina;
                                        }
                                    }
                                }
                                else
                                {
                                    totalParcialMedicina = decimal.Round(totalParcialMedicina, 2);
                                }
                            }

                            var ListaComponentes = servicios.Componentes.FindAll(p => p.Precio != 0 && p.i_conCargoA == 2);
                            foreach (var compo in ListaComponentes)
                            {
                                decimal compoPrecio = (decimal)compo.Precio;
                                sumaServicio += compoPrecio;
                            }
                        }

                        decimal totalParcialHabitacion = 0;
                        decimal sumaHabitacion = 0;
                        var ListaHabitaciones = hospitalizacion_precios.Habitaciones.FindAll(p => p.i_conCargoA == 2);

                        foreach (var habitacion in ListaHabitaciones)
                        {
                            DateTime inicio = habitacion.d_StartDate.Value.Date;
                            DateTime fin;

                            if (habitacion.d_EndDate != null || habitacion.d_EndDate.ToString() == "00/00/0000 0:0:0")
                            {
                                fin = habitacion.d_EndDate.Value.Date;
                            }
                            else
                            {
                                fin = DateTime.Now.Date;

                            }
                            TimeSpan nDias = fin - inicio;

                            int tSpan = nDias.Days;

                            //+ 1
                            int dias = 0;
                            if (tSpan == 0)
                            {
                                dias = tSpan + 1;
                            }
                            else
                            {
                                dias = tSpan;
                            }

                            decimal _habitacionPrecio = (decimal)habitacion.d_Precio;
                            _habitacionPrecio = decimal.Round(_habitacionPrecio, 2);

                            totalParcialHabitacion = (decimal)(habitacion.d_Precio * dias);
                            totalParcialHabitacion = decimal.Round(totalParcialHabitacion, 2);

                            sumaHabitacion += (decimal)totalParcialHabitacion;
                        }

                        totalFinal = sumaMedicina + sumaServicio + sumaHabitacion;

                        totalFinal = decimal.Round(totalFinal, 2);
                    }


                    var _Hospitalizacion = new HospitalizacionBL().GetHospitalizacion(ref _objOperationResult, hospitId);

                    _Hospitalizacion.d_PagoPaciente = totalFinal;

                    _hospitBL.UpdateHospitalizacion(ref _objOperationResult, _Hospitalizacion, Globals.ClientSession.GetAsList());
                    #endregion

                    LiquidacionParticularesHospDetalle.CreateLiquidacionParticulares(servicios_, objPacient, objOrganization, objAseguradora, objHospitalizacion, objSeguro, tickets_, recetas_, pathFile, habitaciones, hospitalizacionhabitacion, hospitalizacion, validador);

                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Error Inesperado: " + err.ToString(), "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void editarDiagnósticoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var hospitalizacionId = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();

            var frm = new frmEditDx(hospitalizacionId, "DxE");
            frm.ShowDialog();
            btnFilter_Click(sender, e);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var hospitalizacionId = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();
            frmTipoPacHospSala frm = new Hospitalizacion.frmTipoPacHospSala(hospitalizacionId);
            frm.ShowDialog();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            //var ListaFinal = _AgendaDtoList.ToList().OrderBy(g => g.CONSULTORIO).ToList();

            string ruta = Common.Utils.GetApplicationConfigValue("LiquidacionHospitalizacion").ToString();

            BackgroundWorker bw = sender as BackgroundWorker;

            Excel.Application excel = new Excel.Application();
            Excel._Workbook libro = null;
            Excel._Worksheet hoja = null;
            Excel.Range rango = null;
            string finic = dtpDateTimeStar.Text;
            finic = finic.Replace('/', '-');
            string fend = dptDateTimeEnd.Text;
            fend = fend.Replace('/', '-');
            try
            {
                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    //creamos un libro nuevo y la hoja con la que vamos a trabajar
                    libro = (Excel._Workbook)excel.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);

                    hoja = (Excel._Worksheet)libro.Worksheets.Add();
                    hoja.Application.ActiveWindow.DisplayGridlines = false;
                    hoja.Name = "RESUMEN DE HOSPITALIZACIONES";
                    ((Excel.Worksheet)excel.ActiveWorkbook.Sheets["Hoja1"]).Delete();   //Borro hoja que crea en el libro por defecto

                    //DatosEmpresa
                    rango = (Microsoft.Office.Interop.Excel.Range)hoja.get_Range("B2", "D5");
                    rango.Select();
                    rango.RowHeight = 25;
                    hoja.get_Range("B2", "D5").Merge(true);

                    Microsoft.Office.Interop.Excel.Pictures oPictures = (Microsoft.Office.Interop.Excel.Pictures)hoja.Pictures(System.Reflection.Missing.Value);

                    hoja.Shapes.AddPicture(@"C:\Program Files (x86)\NetMedical\Banner\banner.jpg",
                        Microsoft.Office.Core.MsoTriState.msoFalse,
                        Microsoft.Office.Core.MsoTriState.msoCTrue,
                        float.Parse(rango.Left.ToString()),
                        float.Parse(rango.Top.ToString()),
                        200,
                        90);

                    montaCabeceras2(3, ref hoja, "");

                    //DatosDinamicos
                    int fila = 13;
                    int i = 0;
                    decimal sumatipoExm = 0;
                    decimal sumatipoExm_1 = 0;
                    decimal igvPerson = 0;
                    decimal _igvPerson = 0;
                    decimal subTotalPerson = 0;
                    decimal _subTotalPerson = 0;
                    decimal totalFinal = 0;
                    decimal totalFinal_1 = 0;

                    hoja.Cells[12, 2] = "HOSPITALIZACION ID";
                    hoja.Cells[12, 3] = "SERVICIO";
                    hoja.Cells[12, 4] = "PACIENTE";
                    hoja.Cells[12, 5] = "DNI";
                    hoja.Cells[12, 6] = "EDAD";
                    hoja.Cells[12, 7] = "FECHA DE INGRESO";
                    hoja.Cells[12, 8] = "FECHA DE ALTA";
                    hoja.Cells[12, 9] = "MEDICO TRATANTE";
                    hoja.Cells[12, 10] = "COMENTARIO";
                    hoja.Cells[12, 11] = "MONTO P. MEDICO";
                    hoja.Cells[12, 12] = "ESTADO P. MEDICO";
                    hoja.Cells[12, 13] = "MONTO P. PACIENTE";
                    hoja.Cells[12, 14] = "ESTADO P. PACIENTE";
                    hoja.Cells[12, 15] = "CIE 10";
                    hoja.Cells[12, 16] = "DIAGNOSTICO";
                    hoja.Cells[12, 17] = "PROCEDENCIA";
                    hoja.Cells[12, 18] = "COMPROBANTE";
                    hoja.Cells[12, 19] = "MONTO PAGADO";

                    string x3 = "B" + (12 + i).ToString();
                    string y3 = "S" + (12 + i).ToString();
                    rango = hoja.Range[x3, y3];
                    rango.Borders.LineStyle = Excel.XlLineStyle.xlDash;
                    rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    rango.RowHeight = 25;
                    rango.Font.Bold = true;

                    decimal total = 0;
                    if (objData != null)
                    {
                        foreach (var lista1 in objData)
                        {

                            hoja.Cells[fila + i, 2] = lista1.v_HopitalizacionId;
                            hoja.Cells[fila + i, 3] = lista1.v_Servicio;
                            hoja.Cells[fila + i, 4] = lista1.v_Paciente.Trim();
                            hoja.Cells[fila + i, 5] = lista1.v_DocNumber.Trim();
                            hoja.Cells[fila + i, 6] = lista1.i_Years + " Años" ;
                            hoja.Cells[fila + i, 7] = lista1.d_FechaIngreso == null ? "-" : lista1.d_FechaIngreso.Value.ToString("MM/dd/yyyy");
                            hoja.Cells[fila + i, 8] = lista1.d_FechaAlta == null ? "-" : lista1.d_FechaAlta.Value.ToString("MM/dd/yyyy");
                            hoja.Cells[fila + i, 9] = lista1.v_MedicoTratante;
                            hoja.Cells[fila + i, 10] = lista1.v_Comentario == null ? "-" : lista1.v_Comentario;
                            hoja.Cells[fila + i, 11] = lista1.d_PagoMedico == null ? 0 : lista1.d_PagoMedico.Value;
                            hoja.Cells[fila + i, 12] = lista1.MedicoPago;
                            hoja.Cells[fila + i, 13] = lista1.d_PagoPaciente == null ? 0 : lista1.d_PagoPaciente.Value;
                            hoja.Cells[fila + i, 14] = lista1.PacientePago;
                            hoja.Cells[fila + i, 15] = lista1.v_Cie10;
                            hoja.Cells[fila + i, 16] = lista1.v_Diagnostico;
                            hoja.Cells[fila + i, 17] = lista1.v_ProcedenciaPac;
                            hoja.Cells[fila + i, 18] = lista1.v_Comprobantes;
                            hoja.Cells[fila + i, 19] = lista1.d_MontoPagado;
                            i++;
                        }
                    }

                    //LOGO CSL
                    string x7 = "B" + (fila + i + 4).ToString();
                    string y7 = "J" + (fila + i + 4).ToString();
                    rango = (Microsoft.Office.Interop.Excel.Range)hoja.get_Range(x7, y7);
                    rango.Select();
                    hoja.get_Range(x7, y7).Merge(true);

                    Microsoft.Office.Interop.Excel.Pictures oPictures2 = (Microsoft.Office.Interop.Excel.Pictures)hoja.Pictures(System.Reflection.Missing.Value);

                    hoja.Shapes.AddPicture(@"C:\Program Files (x86)\NetMedical\Banner\banner2.jpg",
                        Microsoft.Office.Core.MsoTriState.msoFalse,
                        Microsoft.Office.Core.MsoTriState.msoCTrue,
                        float.Parse(rango.Left.ToString()),
                        float.Parse(rango.Top.ToString()),
                        float.Parse(rango.Width.ToString()),
                        80);

                    libro.Saved = true;

                    libro.SaveAs(ruta + @"\" + "Resumen de Hospitalizaciones del " + finic + " al " + fend + ".xlsx");

                    libro.Close();
                    releaseObject_(libro);

                    excel.UserControl = false;
                    excel.Quit();
                    releaseObject_(excel);
                }

                Process.Start(ruta + @"\" + "Resumen de Hospitalizaciones del " + finic + " al " + fend + ".xlsx");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en creación/actualización de Reporte Hospitalario ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    libro.Saved = true;
                    libro.SaveAs(ruta + @"\" + "Resumen Hospitalizaciones fail.xlsx");

                    libro.Close();
                    releaseObject_(libro);

                    excel.UserControl = false;
                    excel.Quit();
                    releaseObject_(excel);
                }
                Process.Start(ruta + @"\" + "Resumen Hospitalizaciones fail.xlsx");
            }
        }

        private void montaCabeceras2(int fila, ref Excel._Worksheet hoja, string _liquidacion)
        {


            var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();

            var obtenerInformacionEmpresas = new ServiceBL().GetInfoMedicalCenter();
            try
            {
                Excel.Range rango;

                //** TITULO DEL LIBRO **
                ////hoja.Cells[1, 2] = MedicalCenter.b_Image;
                //hoja.get_Range("B1", "C1");

                hoja.Cells[6, 4] = "RESUMEN DE HOSPITALIZACIONES DEL " + dtpDateTimeStar.Text + " AL " + dptDateTimeEnd.Text;
                hoja.get_Range("B6", "S6").Merge(true);
                hoja.get_Range("B6", "S6").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("B6", "S6").Font.Bold = true;
                hoja.get_Range("B6", "S6").Font.Size = 18;
                hoja.get_Range("B6", "S6").RowHeight = 35;
                hoja.get_Range("B6", "S6").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja.Cells[8, 2] = "EMPRESA:";
                hoja.Cells[8, 4] = obtenerInformacionEmpresas.v_Name;
                hoja.get_Range("B8", "B8").Merge(true);
                hoja.get_Range("C8", "S8").Merge(true);
                hoja.get_Range("B8", "B8").Font.Bold = true;
                hoja.get_Range("B8", "C8").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("B8", "C8").RowHeight = 30;
                hoja.get_Range("C8", "S8").RowHeight = 30;
                hoja.get_Range("B8", "B8").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("C8", "S8").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja.Cells[9, 2] = "RUC: ";
                hoja.Cells[9, 4] = obtenerInformacionEmpresas.v_IdentificationNumber;
                hoja.get_Range("B9", "B9").Merge(true);
                hoja.get_Range("C9", "S9").Merge(true);
                hoja.get_Range("C9", "S9").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                hoja.get_Range("B9", "B9").Font.Bold = true;
                hoja.get_Range("B9", "B9").RowHeight = 30;
                hoja.get_Range("C9", "S9").RowHeight = 30;
                hoja.get_Range("B9", "B9").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("C9", "S9").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja.Cells[10, 2] = "DIRECCION: ";
                hoja.Cells[10, 4] = obtenerInformacionEmpresas.v_Address;
                hoja.get_Range("B10", "B10").Merge(true);
                hoja.get_Range("C10", "S10").Merge(true);
                hoja.get_Range("B10", "B10").Font.Bold = true;
                hoja.get_Range("B10", "B10").RowHeight = 30;
                hoja.get_Range("C10", "S10").RowHeight = 30;
                hoja.get_Range("B10", "B10").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("C10", "S10").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                //Asigna borde
                rango = hoja.Range["B8", "S10"];
                rango.Borders.LineStyle = Excel.XlLineStyle.xlDot;

                //Modificamos los anchos de las columnas
                rango = hoja.Columns[1];
                rango.ColumnWidth = 3;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[2];
                rango.ColumnWidth = 25;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[3];
                rango.ColumnWidth = 25;
                rango.Cells.WrapText = true;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[4];
                rango.ColumnWidth = 40;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[5];
                rango.ColumnWidth = 15;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[6];
                rango.ColumnWidth = 12;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[7];
                rango.ColumnWidth = 20;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[8];
                rango.ColumnWidth = 20;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[9];
                rango.ColumnWidth = 30;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[10];
                rango.ColumnWidth = 50;
                rango.Cells.WrapText = true;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                
                rango = hoja.Columns[11];
                rango.ColumnWidth = 20;
                rango.Cells.WrapText = true;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.NumberFormat = "#00";

                rango = hoja.Columns[12];
                rango.ColumnWidth = 20;
                rango.Cells.WrapText = true;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[13];
                rango.ColumnWidth = 20;
                rango.Cells.WrapText = true;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.NumberFormat = "#00";

                rango = hoja.Columns[14];
                rango.ColumnWidth = 20;
                rango.Cells.WrapText = true;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //rango.NumberFormat = "#0.00";

                rango = hoja.Columns[15];
                rango.ColumnWidth = 15;
                rango.Cells.WrapText = true;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[16];
                rango.ColumnWidth = 50;
                rango.Cells.WrapText = true;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[17];
                rango.ColumnWidth = 20;
                rango.Cells.WrapText = true;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[18];
                rango.ColumnWidth = 30;
                rango.Cells.WrapText = true;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[19];
                rango.ColumnWidth = 20;
                rango.Cells.WrapText = true;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de redondeo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void releaseObject_(object obj)
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

        private void label15_Click(object sender, EventArgs e)
        {
            objDataTemp = new List<HospitalizacionListNew>(objData.Where(p => p.d_FechaAlta == null && p.v_Paciente.Contains(txtPacient.Text.ToUpper())));
            grdData.DataSource = objDataTemp;
            if (objDataTemp != null)
            {
                lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataTemp.Count());
            }
        }

        private void txtPacient_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPacient.Text != string.Empty)
            {
                objDataTemp = new List<HospitalizacionListNew>(objData.Where(p => p.v_Paciente.Contains(txtPacient.Text.ToUpper())));

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

        private void label4_Click(object sender, EventArgs e)
        {
            grdData.DataSource = objData;
            if (objData != null)
            {
                lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            objDataTemp = new List<HospitalizacionListNew>(objData.Where(p => p.d_FechaAlta != null && p.d_MontoPagado == 0 && (p.d_PagoMedico != null || p.d_PagoPaciente != null) && p.v_Paciente.Contains(txtPacient.Text.ToUpper())));
            grdData.DataSource = objDataTemp;
            if (objDataTemp != null)
            {
                lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataTemp.Count());
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            objDataTemp = new List<HospitalizacionListNew>(objData.Where(p => p.d_FechaAlta != null && p.d_MontoPagado != 0 && p.v_Paciente.Contains(txtPacient.Text.ToUpper())));
            grdData.DataSource = objDataTemp;
            if (objDataTemp != null)
            {
                lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataTemp.Count());
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {
            objDataTemp = new List<HospitalizacionListNew>(objData.Where(p => p.d_FechaAlta != null && p.MedicoPago == "S-L" && p.PacientePago == "S-L" && p.v_Paciente.Contains(txtPacient.Text.ToUpper())));
            grdData.DataSource = objDataTemp;
            if (objDataTemp != null)
            {
                lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataTemp.Count());
            }
        }

        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTodos.Checked == true)
            {
                grdData.DataSource = objData;

                if (objData != null)
                {
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                }
            }
            
        }

        private void rbHospitalizados_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHospitalizados.Checked == true)
            {
                objDataTemp = new List<HospitalizacionListNew>(objData.Where(p => p.Hosp == "SI"));
                grdData.DataSource = objDataTemp;
                if (objDataTemp != null)
                {
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataTemp.Count());
                }
            }
        }

        private void rbSOP_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSOP.Checked == true)
            {
                objDataTemp = new List<HospitalizacionListNew>(objData.Where(p => p.Sop == "SI"));
                grdData.DataSource = objDataTemp;
                if (objDataTemp != null)
                {
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataTemp.Count());
                }
            }
        }

        private void grdData_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            var HospId = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();
            var Pac = grdData.Selected.Rows[0].Cells["v_Paciente"].Value.ToString();
            var Dni = grdData.Selected.Rows[0].Cells["v_DocNumber"].Value.ToString();
            var Edad = grdData.Selected.Rows[0].Cells["i_Years"].Value.ToString();
            var Medico = grdData.Selected.Rows[0].Cells["v_MedicoTratante"].Value.ToString();
            var Cie10 = grdData.Selected.Rows[0].Cells["v_Cie10"].Value.ToString();
            var Dx = grdData.Selected.Rows[0].Cells["v_Diagnostico"].Value.ToString();
            var Procedencia = grdData.Selected.Rows[0].Cells["v_ProcedenciaPac"].Value.ToString();
            var Hosp = grdData.Selected.Rows[0].Cells["Hosp"].Value.ToString();
            var Sop = grdData.Selected.Rows[0].Cells["Sop"].Value.ToString();
            var FechaAlta = grdData.Selected.Rows[0].Cells["d_FechaAlta"].Value == null ? "" : grdData.Selected.Rows[0].Cells["d_FechaAlta"].Value.ToString();

            var Comentarios = grdData.Selected.Rows[0].Cells["v_Comentario"].Value == null ? "" : grdData.Selected.Rows[0].Cells["v_Comentario"].Value.ToString();
            var PersonId = grdData.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();

            frmDetalleHospitalizacion ticket = new frmDetalleHospitalizacion(HospId, Pac, Dni, Edad, Medico, Cie10, Dx, Procedencia, Hosp, Sop, FechaAlta, Comentarios, PersonId);
            ticket.ShowDialog();
            //btnFilter_Click(sender, e);
            //btnTicket.Enabled = false;
        }
    }
}
