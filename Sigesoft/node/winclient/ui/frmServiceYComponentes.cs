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
using System.IO;
using NetPdf;
using Infragistics.Win.UltraWinGrid;
using System.Diagnostics;
using System.Linq.Dynamic;
using System.Threading;
using System.Windows.Shell;
using Infragistics.Win.UltraWinDataSource;
using Sigesoft.Node.WinClient.UI.Reports;
using System.Data.SqlClient;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Node.WinClient.UI.Operations.Popups;
using Sigesoft.Node.WinClient.UI.Visor;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmServiceYComponentes : Form
    {
        OperationResult _objOperationResult = new OperationResult();
        List<ServiceComponentList> ConsolidadoReportes = new List<ServiceComponentList>();
        List<string> _componentIds;
        PacientBL _pacientBL = new PacientBL();
        HistoryBL _historyBL = new HistoryBL();
        List<KeyValueDTO> _componentListTemp = new List<KeyValueDTO>();
        string strFilterExpression;
        ServiceBL _serviceBL = new ServiceBL();
        private string _serviceId;
        private string _EmpresaClienteId;
        private string _pacientId;
        private string _protocolId;
        private string _customerOrganizationName;
        private string _personFullName;
        List<string> _ListaServicios;
        List<string> _ListaServiciosHistoria;
        List<string> _ListaServiciosAdjuntar;
        private OrganizationBL _organizationBL = new OrganizationBL();
        private SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        private Sigesoft.Node.WinClient.UI.Utils.CustomizedToolTip _customizedToolTip = null;
        private SaveFileDialog saveFileDialog2 = new SaveFileDialog();
        private BindingList<ServiceGridJerarquizadaList> ListaGrilla = new BindingList<ServiceGridJerarquizadaList>();
        private BindingList<ServiceGridJerarquizadaList> ListaGrillaTemp = new BindingList<ServiceGridJerarquizadaList>();

        private MergeExPDF _mergeExPDF = new MergeExPDF();
        private bool mergeRow = false;
        private List<string> _ComponentsIdsOrdenados = new List<string>();
        LogicReports _LogicReports = new LogicReports();
        private string _modo;

        public frmServiceYComponentes()
        {
            InitializeComponent();
        }

        private void frmServiceYComponentes_Load(object sender, EventArgs e)
        {
            this.Show();
            var usuarioActual = Globals.ClientSession.i_SystemUserId;

            var usuario_data = new ServiceBL().GetSystemUserId(usuarioActual);
            var usuario_professional = new ServiceBL().GetProfessional(usuario_data.v_PersonId);

                _customizedToolTip = new Sigesoft.Node.WinClient.UI.Utils.CustomizedToolTip(grdDataService);

                ddlConsultorio.SelectedValueChanged -= ddlConsultorio_SelectedValueChanged;
            
                dtpDateTimeStar.Value = dtpDateTimeStar.Value.AddDays(0);

                OperationResult objOperationResult = new OperationResult();

                Utils.LoadDropDownList(ddlServiceTypeId, "Value1", "Id", BLL.Utils.GetServiceType(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.All);
                Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetMasterService(ref objOperationResult, -1, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.All);

                Utils.LoadDropDownList(cboGrupoExamen, "Value1", "Id", BLL.Utils.GetGrupoExamen(ref objOperationResult, -1), DropDownListAction.All);


                //Utils.LoadDropDownList(ddlEsoType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 118, null), DropDownListAction.All);

                var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);
                Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);
                Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, "-1", "-1", null), DropDownListAction.All);

                //Utils.LoadDropDownList(ddServiceStatusId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 125, null), DropDownListAction.All);


                // Obtener permisos de cada examen de un rol especifico
                var componentProfile = _serviceBL.GetRoleNodeComponentProfileByRoleNodeId(Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_RoleId.Value);

                _componentListTemp = BLL.Utils.GetAllComponents(ref objOperationResult);

                var xxx = _componentListTemp.FindAll(p => p.Value4 != -1);

                List<KeyValueDTO> groupComponentList = xxx.GroupBy(x => x.Value4).Select(group => group.First()).ToList();

                groupComponentList.AddRange(_componentListTemp.ToList().FindAll(p => p.Value4 == -1));
                var results = groupComponentList.FindAll(f => componentProfile.Any(t => t.v_ComponentId == f.Value2));

                Utils.LoadDropDownList(cboUserMed, "Value1", "Id", BLL.Utils.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);


                Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 116, null, 40), DropDownListAction.All);

                dtpDateTimeStar.CustomFormat = "dd/MM/yyyy";
                dptDateTimeEnd.CustomFormat = "dd/MM/yyyy";
                strFilterExpression = null;

                ddlConsultorio.SelectedValueChanged += ddlConsultorio_SelectedValueChanged;


        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            var usuarioActual = Globals.ClientSession.i_SystemUserId;
            var usuario_data = new ServiceBL().GetSystemUserId(usuarioActual);
            var usuario_professional = new ServiceBL().GetProfessional(usuario_data.v_PersonId);
            strFilterExpression = null;
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.BindGrid();
            };
        }

        private void BindGrid()
        {
            var objData = GetData(0, null, "", strFilterExpression);
            ListaGrilla = objData;
            grdDataService.DataSource = objData;
            #region Ocultar columnas
            grdDataService.DisplayLayout.Bands[0].Columns["v_PersonId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["b_FechaEntrega"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_ServiceStatusName"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_ServiceStatusId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_LocationName"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_ProtocolId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_ComponentId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_LineStatusId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_DiagnosticRepositoryId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_DiseasesName"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["d_ExpirationDateDiagnostic"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["d_ServiceDate"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["TipoServicioMaster"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["TipoServicioESO"].Hidden = true;

            grdDataService.DisplayLayout.Bands[0].Columns["v_Recommendation"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_ServiceId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_ServiceTypeId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_CustomerOrganizationId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_CustomerLocationId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_MasterServiceId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_AptitudeStatusId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_EsoTypeId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_IsDeleted"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_CreationUser"].Hidden = true;

            grdDataService.DisplayLayout.Bands[0].Columns["v_UpdateUser"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["d_CreationDate"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["d_UpdateDate"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_StatusLiquidation"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_MasterServiceName"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_EsoTypeName"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["CIE10"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["d_FechaNacimiento"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["NroPoliza"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["d_FechaEntrega"].Hidden = true;

            grdDataService.DisplayLayout.Bands[0].Columns["Moneda"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["NroFactura"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["Valor"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_FinalQualificationId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_Restriccion"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["d_Deducible"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_IsDeletedDx"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["LogoEmpresaPropietaria"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_IsDeletedRecomendaciones"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_IsDeletedRestricciones"].Hidden = true;

            grdDataService.DisplayLayout.Bands[0].Columns["i_age"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["UsuarioMedicina"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["item"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_ApprovedUpdateUserId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_Consultorio"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_ConsultorioId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_ServiceComponentStatusId"].Hidden = true;
            ///////

            grdDataService.DisplayLayout.Bands[0].Columns["d_BirthDate"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["Facturacion"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_TelephoneNumber"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["Rx_DCM"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["Rx_JPG"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["Rx_PDF"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["Esp"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["Ekg"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_ComprobantePago"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["Liq"].Hidden = true;

            //grdDataService.DisplayLayout.Bands[0].Columns["Liq"].Header.VisiblePosition = 0;

            grdDataService.DisplayLayout.Bands[0].Columns["d_BirthDate"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["Facturacion"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_TelephoneNumber"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["Rx_DCM"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["Rx_JPG"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["Rx_PDF"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["Esp"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["Ekg"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_ComprobantePago"].Hidden = true;
            #endregion

            grdDataService.DisplayLayout.Bands[0].Columns["Value4"].Header.VisiblePosition = 0;
            grdDataService.DisplayLayout.Bands[0].Columns["Value2"].Header.VisiblePosition = 1;
            grdDataService.DisplayLayout.Bands[0].Columns["Fecha_"].Header.VisiblePosition = 2;
            grdDataService.DisplayLayout.Bands[0].Columns["v_DocNumber"].Header.VisiblePosition = 3;
            grdDataService.DisplayLayout.Bands[0].Columns["v_Pacient"].Header.VisiblePosition = 4;
            grdDataService.DisplayLayout.Bands[0].Columns["Edad"].Header.VisiblePosition = 5;
            grdDataService.DisplayLayout.Bands[0].Columns["v_ServiceId"].Header.VisiblePosition = 6;
            grdDataService.DisplayLayout.Bands[0].Columns["CompMinera"].Header.VisiblePosition = 7;
            grdDataService.DisplayLayout.Bands[0].Columns["v_OrganizationName"].Header.VisiblePosition = 8;
            grdDataService.DisplayLayout.Bands[0].Columns["Tercero"].Header.VisiblePosition = 9;
            grdDataService.DisplayLayout.Bands[0].Columns["TipoServicio"].Header.VisiblePosition = 10;

            grdDataService.DisplayLayout.Bands[0].Columns["v_ProtocolName"].Header.VisiblePosition = 11;


            grdDataService.DisplayLayout.Bands[0].Columns["v_ServiceStatusName"].Header.VisiblePosition = 12;
            grdDataService.DisplayLayout.Bands[0].Columns["v_AptitudeStatusName"].Header.VisiblePosition = 13;
            
            grdDataService.DisplayLayout.Bands[0].Columns["UsuarioCrea"].Header.VisiblePosition = 14;
            grdDataService.DisplayLayout.Bands[0].Columns["v_MedicoTratante"].Header.VisiblePosition = 15;
            grdDataService.DisplayLayout.Bands[0].Columns["Value1"].Header.VisiblePosition = 16;
            grdDataService.DisplayLayout.Bands[0].Columns["Value3"].Header.VisiblePosition = 17;
            grdDataService.DisplayLayout.Bands[0].Columns["Value5"].Header.VisiblePosition = 18;



            lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            //if (ddlServiceTypeId.SelectedValue == "-1")
            //{
            //    grdDataService.DisplayLayout.Bands[0].Columns["d_BirthDate"].Hidden = true;
            //    grdDataService.DisplayLayout.Bands[0].Columns["Facturacion"].Hidden = true;
            //    grdDataService.DisplayLayout.Bands[0].Columns["v_TelephoneNumber"].Hidden = true;
            //    grdDataService.DisplayLayout.Bands[0].Columns["Rx_DCM"].Hidden = true;
            //    grdDataService.DisplayLayout.Bands[0].Columns["Rx_JPG"].Hidden = true;
            //    grdDataService.DisplayLayout.Bands[0].Columns["Rx_PDF"].Hidden = true;
            //    grdDataService.DisplayLayout.Bands[0].Columns["Esp"].Hidden = true;
            //    grdDataService.DisplayLayout.Bands[0].Columns["Ekg"].Hidden = true;
            //    grdDataService.DisplayLayout.Bands[0].Columns["v_ComprobantePago"].Hidden = true;

            //    grdDataService.DisplayLayout.Bands[0].Columns["Liq"].Header.VisiblePosition = 0;
            //    grdDataService.DisplayLayout.Bands[0].Columns["v_DocNumber"].Header.VisiblePosition = 1;
            //    grdDataService.DisplayLayout.Bands[0].Columns["Fecha"].Header.VisiblePosition = 2;
            //    grdDataService.DisplayLayout.Bands[0].Columns["v_Pacient"].Header.VisiblePosition = 3;
            //    grdDataService.DisplayLayout.Bands[0].Columns["Edad"].Header.VisiblePosition = 4;
            //    grdDataService.DisplayLayout.Bands[0].Columns["v_ServiceStatusName"].Header.VisiblePosition = 5;
            //    grdDataService.DisplayLayout.Bands[0].Columns["v_AptitudeStatusName"].Header.VisiblePosition = 6;
            //    grdDataService.DisplayLayout.Bands[0].Columns["CompMinera"].Header.VisiblePosition = 7;
            //    grdDataService.DisplayLayout.Bands[0].Columns["v_OrganizationName"].Header.VisiblePosition = 8;
            //    grdDataService.DisplayLayout.Bands[0].Columns["Tercero"].Header.VisiblePosition = 9;
            //    grdDataService.DisplayLayout.Bands[0].Columns["v_ProtocolName"].Header.VisiblePosition = 10;
            //    grdDataService.DisplayLayout.Bands[0].Columns["v_ServiceId"].Header.VisiblePosition = 11;
            //    grdDataService.DisplayLayout.Bands[0].Columns["TipoServicio"].Header.VisiblePosition = 12;
            //    grdDataService.DisplayLayout.Bands[0].Columns["UsuarioCrea"].Header.VisiblePosition = 13;
            //    grdDataService.DisplayLayout.Bands[0].Columns["v_MedicoTratante"].Header.VisiblePosition = 14;
            //}
            if (int.Parse(ddlServiceTypeId.SelectedValue.ToString()) == 1)
            {
                grdDataService.DisplayLayout.Bands[0].Columns["d_BirthDate"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["Facturacion"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["v_TelephoneNumber"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["Rx_DCM"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["Rx_JPG"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["Rx_PDF"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["Esp"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["Ekg"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["v_ComprobantePago"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["v_ProtocolName"].Hidden = false;
                grdDataService.DisplayLayout.Bands[0].Columns["v_ServiceId"].Hidden = true;

                //grdDataService.DisplayLayout.Bands[0].Columns["Liq"].Header.VisiblePosition = 0;
                //grdDataService.DisplayLayout.Bands[0].Columns["v_DocNumber"].Header.VisiblePosition = 1;
                //grdDataService.DisplayLayout.Bands[0].Columns["Fecha"].Header.VisiblePosition = 2;
                //grdDataService.DisplayLayout.Bands[0].Columns["v_Pacient"].Header.VisiblePosition = 3;
                //grdDataService.DisplayLayout.Bands[0].Columns["Edad"].Header.VisiblePosition = 4;
                //grdDataService.DisplayLayout.Bands[0].Columns["v_ServiceStatusName"].Header.VisiblePosition = 5;
                //grdDataService.DisplayLayout.Bands[0].Columns["v_AptitudeStatusName"].Header.VisiblePosition = 6;
                //grdDataService.DisplayLayout.Bands[0].Columns["CompMinera"].Header.VisiblePosition = 7;
                //grdDataService.DisplayLayout.Bands[0].Columns["v_OrganizationName"].Header.VisiblePosition = 8;
                //grdDataService.DisplayLayout.Bands[0].Columns["Tercero"].Header.VisiblePosition = 9;
                //grdDataService.DisplayLayout.Bands[0].Columns["TipoServicio"].Header.VisiblePosition = 10;
                //grdDataService.DisplayLayout.Bands[0].Columns["v_MedicoTratante"].Header.VisiblePosition = 11;
                //grdDataService.DisplayLayout.Bands[0].Columns["UsuarioCrea"].Header.VisiblePosition = 12;
                //grdDataService.DisplayLayout.Bands[0].Columns["v_ProtocolName"].Header.VisiblePosition = 13;


                //grdDataService.DisplayLayout.Bands[0].Columns["CompMinera"].Header.Caption = "Minera/Emp General";
                //grdDataService.DisplayLayout.Bands[0].Columns["v_OrganizationName"].Header.Caption = "Contrata";
                //grdDataService.DisplayLayout.Bands[0].Columns["Tercero"].Header.Caption = "Sub Contrata";
            }
            else if (int.Parse(ddlServiceTypeId.SelectedValue.ToString()) == 9 || int.Parse(ddlServiceTypeId.SelectedValue.ToString()) == 11 || int.Parse(ddlServiceTypeId.SelectedValue.ToString()) == 34)
            {
                grdDataService.DisplayLayout.Bands[0].Columns["d_BirthDate"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["Facturacion"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["v_TelephoneNumber"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["Rx_DCM"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["Rx_JPG"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["Rx_PDF"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["Esp"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["Ekg"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["v_ComprobantePago"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["v_ServiceId"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["CompMinera"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["Tercero"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["TipoServicio"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["v_ServiceStatusName"].Hidden = true;
                grdDataService.DisplayLayout.Bands[0].Columns["v_ProtocolName"].Hidden = false;


                //grdDataService.DisplayLayout.Bands[0].Columns["Liq"].Header.VisiblePosition = 0;
                //grdDataService.DisplayLayout.Bands[0].Columns["v_DocNumber"].Header.VisiblePosition = 1;
                //grdDataService.DisplayLayout.Bands[0].Columns["Fecha"].Header.VisiblePosition = 2;
                //grdDataService.DisplayLayout.Bands[0].Columns["v_Pacient"].Header.VisiblePosition = 3;
                //grdDataService.DisplayLayout.Bands[0].Columns["Edad"].Header.VisiblePosition = 4;
                //grdDataService.DisplayLayout.Bands[0].Columns["v_AptitudeStatusName"].Header.VisiblePosition = 5;
                //grdDataService.DisplayLayout.Bands[0].Columns["v_ProtocolName"].Header.VisiblePosition = 6;
                //grdDataService.DisplayLayout.Bands[0].Columns["v_OrganizationName"].Header.VisiblePosition = 7;
                //grdDataService.DisplayLayout.Bands[0].Columns["UsuarioCrea"].Header.VisiblePosition = 8;
                //grdDataService.DisplayLayout.Bands[0].Columns["v_MedicoTratante"].Header.VisiblePosition = 9;

                //grdDataService.DisplayLayout.Bands[0].Columns["v_OrganizationName"].Header.Caption = "Empresa";
            }

        }

        private BindingList<ServiceGridJerarquizadaList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            DateTime? FCI = FechaControlIni.Value.Date;
            DateTime? FCF = FechaControlFin.Value.Date.AddDays(1);

            var usuarioActual = Globals.ClientSession.i_SystemUserId;

            var usuario_data = new ServiceBL().GetSystemUserId(usuarioActual);
            var usuario_professional = new ServiceBL().GetProfessional(usuario_data.v_PersonId);

            BindingList<ServiceGridJerarquizadaList> _objData = new BindingList<ServiceGridJerarquizadaList>();

            _objData = _serviceBL.GetServicesPagedAndFilteredComponentes(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate, _componentIds, FCI, FCF, txtDiagnostico.Text);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #region Filters
            
            if (ddlServiceTypeId.SelectedIndex != 0)
            {
                List<ServiceGridJerarquizadaList> Data = _objData.Where(p => p.i_ServiceTypeId == Convert.ToInt32(ddlServiceTypeId.SelectedValue)).ToList();
                _objData = new BindingList<ServiceGridJerarquizadaList>(Data);
            }
            if (ddlMasterServiceId.SelectedIndex != 0)
            {
                List<ServiceGridJerarquizadaList> Data = _objData.Where(p => p.i_MasterServiceId == Convert.ToInt32(ddlMasterServiceId.SelectedValue)).ToList();
                _objData = new BindingList<ServiceGridJerarquizadaList>(Data);
            }
            int cadenaDNI = 0;
            if (!String.IsNullOrEmpty(txtPacient.Text))
            {

                if (txtPacient.Text.Any(x => !char.IsNumber(x)))
                {
                    List<ServiceGridJerarquizadaList> Data = new List<ServiceGridJerarquizadaList>(_objData.Where(p => p.v_Pacient.Contains(txtPacient.Text.ToUpper())));
                    _objData = new BindingList<ServiceGridJerarquizadaList>(Data);
                }
                else
                {
                    List<ServiceGridJerarquizadaList> Data = _objData.Where(p => p.v_DocNumber == (txtPacient.Text)).ToList();
                    _objData = new BindingList<ServiceGridJerarquizadaList>(Data);
                }
            }

            //if (!String.IsNullOrEmpty(txtServicioId.Text))
            //{
            //    List<ServiceGridJerarquizadaList> Data = new List<ServiceGridJerarquizadaList>(_objData.Where(p => p.v_ServiceId.Contains(txtServicioId.Text.ToUpper())));
            //    _objData = new BindingList<ServiceGridJerarquizadaList>(Data);
            //}

            //if (ddServiceStatusId.SelectedIndex != 0)
            //{
            //    List<ServiceGridJerarquizadaList> Data = _objData.Where(p => p.i_ServiceStatusId == Convert.ToInt32(ddServiceStatusId.SelectedValue)).ToList();
            //    _objData = new BindingList<ServiceGridJerarquizadaList>(Data);
            //}

            //if (ddlEsoType.SelectedIndex != 0)
            //{
            //    List<ServiceGridJerarquizadaList> Data = _objData.Where(p => p.i_EsoTypeId == Convert.ToInt32(ddlEsoType.SelectedValue)).ToList();
            //    _objData = new BindingList<ServiceGridJerarquizadaList>(Data);
            //}
            if (!String.IsNullOrEmpty(txtDiagnostico.Text))
            {
                List<ServiceGridJerarquizadaList> Data = new List<ServiceGridJerarquizadaList>(_objData.Where(p => p.v_DiseasesName.Contains(txtDiagnostico.Text.ToUpper())));
                _objData = new BindingList<ServiceGridJerarquizadaList>(Data);
            }


            if (cboGrupoExamen.SelectedIndex != 0)
            {
                List<ServiceGridJerarquizadaList> Data = _objData.Where(p => p.Value4 == cboGrupoExamen.Text).ToList();
                _objData = new BindingList<ServiceGridJerarquizadaList>(Data);
            }

            if (checkFacturacion.Checked == true)
            {
                if (ddlCustomerOrganization.SelectedIndex != 0)
                {
                    string[] Empresa = ddlCustomerOrganization.Text.Split('/');
                    List<ServiceGridJerarquizadaList> Data = new List<ServiceGridJerarquizadaList>(_objData.Where(p => p.Facturacion.Contains(Empresa[0])));
                    _objData = new BindingList<ServiceGridJerarquizadaList>(Data);
                }
            }
            else
            {
                if (ddlCustomerOrganization.SelectedIndex != 0)
                {
                    string[] Empresa = ddlCustomerOrganization.Text.Split('/');
                    List<ServiceGridJerarquizadaList> Data = new List<ServiceGridJerarquizadaList>(_objData.Where(p => p.CompMinera.Contains(Empresa[0]) || p.Tercero.Contains(Empresa[0]) || p.v_OrganizationName.Contains(Empresa[0])));
                    _objData = new BindingList<ServiceGridJerarquizadaList>(Data);
                }
            }


          

            if (cboUserMed.SelectedIndex != 0)
            {
                bool result = false;
                List<string> componentPermiso = ObtenerPermisosDelRol(Globals.ClientSession.i_SystemUserId);
                List<ServiceGridJerarquizadaList> Data = new List<ServiceGridJerarquizadaList>();
                foreach (var item in _objData)
                {
                    if (usuario_professional.v_ProfessionalInformation == "BLOQUEADO" && usuario_professional.i_ProfessionId == (int)TipoProfesional.Especialista)
                    {
                        List<compServ> componentService = ObtenerServComp(item.v_ServiceId, componentPermiso, Globals.ClientSession.i_SystemUserId);
                        if (componentService.Count > 0) { result = true; }
                    }
                    else if (usuario_professional.v_ProfessionalInformation == "BLOQUEADO" && usuario_professional.i_ProfessionId == (int)TipoProfesional.Auditor_Evaluador)
                    { result = ObtenerUserBlockAuditor(item.v_ServiceId, Globals.ClientSession.i_SystemUserId); }
                    else { result = ObtenerUser(item.v_ServiceId, cboUserMed.Text); }

                    if (result)
                    {
                        Data.Add(item);
                    }
                }
                _objData = new BindingList<ServiceGridJerarquizadaList>(Data);
            }

            #endregion

            return _objData;

        }

        private void ddlConsultorio_SelectedValueChanged(object sender, EventArgs e)
        {

        }
        private bool ObtenerConsultorio(string service, string category)
        {
            bool result = false;
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena1 = @"select SP.v_Value1 from servicecomponent SC  " +
                          "inner join component CP on SC.v_ComponentId=CP.v_ComponentId " +
                          "inner join systemparameter SP on CP.i_CategoryId=SP.i_ParameterId and SP.i_GroupId=116 " +
                          "where SC.v_ServiceId='" + service + "' and SC.i_IsRequiredId=1 group by SP.v_Value1";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector1 = comando.ExecuteReader();
            while (lector1.Read())
            {
                string cat = lector1.GetValue(0).ToString();
                if (cat == category)
                {
                    result = true;
                    break;
                }
            }
            lector1.Close();
            conectasam.closesigesoft();
            return result;
        }
        private bool ObtenerEstadoConsultorio(string service, string category, string estadocategory)
        {
            bool result = false;
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena1 = @"select DISTINCT SP.v_Value1 , SP1.v_Value1 from servicecomponent SC   " +
                          " inner join component CP on SC.v_ComponentId=CP.v_ComponentId " +
                          " inner join systemparameter SP on CP.i_CategoryId=SP.i_ParameterId and SP.i_GroupId=116 " +
                          " inner join systemparameter SP1 on SC.i_ServiceComponentStatusId = SP1.i_ParameterId and SP1.i_GroupId=127 " +
                          "where SC.v_ServiceId='" + service + "' and SC.i_IsRequiredId=1 group by SP.v_Value1, SP1.v_Value1 ORDER BY SP1.v_Value1 DESC";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector1 = comando.ExecuteReader();
            while (lector1.Read())
            {
                string cat = lector1.GetValue(0).ToString();
                string estcat = lector1.GetValue(1).ToString();
                if (cat == category)
                {
                    if (estcat == estadocategory)
                    {
                        result = true;
                        break;
                    }
                }
            }
            lector1.Close();
            conectasam.closesigesoft();
            return result;
        }
        private List<string> ObtenerPermisosDelRol(int i_SystemUserId)
        {
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena1 = @"select CP.v_ComponentId from systemuser SU " +
                          "inner join systemuserrolenode SUR on SU.i_SystemUserId=SUR.i_SystemUserId " +
                          "inner join rolenodecomponentprofile RNC on SUR.i_RoleId=RNC.i_RoleId " +
                          "inner join component CP on RNC.v_ComponentId=CP.v_ComponentId " +
                          "where SU.i_SystemUserId=" + i_SystemUserId + " and RNC.i_IsDeleted=0 and SUR.i_IsDeleted=0 and CP.v_ComponentId<> 'N002-ME000000001' and CP.v_ComponentId<> 'N002-ME000000002'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector1 = comando.ExecuteReader();
            List<string> comp = new List<string>();
            while (lector1.Read())
            {
                string cp = lector1.GetValue(0).ToString();
                comp.Add(cp);
            }
            lector1.Close();
            conectasam.closesigesoft();
            return comp;
        }

        private List<compServ> ObtenerServComp(string v_ServiceId, List<string> compList, int userUpdate)
        {
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            string where = "where v_ServiceId='" + v_ServiceId + "' and (";
            for (int i = 0; i < compList.Count; i++)
            {
                if (i == compList.Count - 1) { where = where + "  v_ComponentId = '" + compList[i] + "' )  and i_IsRequiredId=1"; }
                else { where = where + "  v_ComponentId = '" + compList[i] + "' or"; }
            }

            var cadena1 = @"select v_ComponentId, v_ServiceComponentId, i_ApprovedUpdateUserId from servicecomponent " + where;
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector1 = comando.ExecuteReader();
            List<compServ> comp = new List<compServ>();
            while (lector1.Read())
            {
                compServ cp = new compServ();
                object x = lector1.GetValue(2);
                cp.v_ComponentId = lector1.GetValue(0).ToString();
                cp.v_ServiceComponentId = lector1.GetValue(1).ToString();
                cp.i_ApprovedUpdateUserId = x.ToString() == "" ? 0 : Convert.ToInt32(x); // lector1.GetValue(2).ToString() == "" ? 0 : Convert.ToInt32(lector1.GetValue(1).ToString());
                if (cp.i_ApprovedUpdateUserId == userUpdate || cp.i_ApprovedUpdateUserId == 0)
                {
                    comp.Add(cp);
                }
            }
            lector1.Close();
            conectasam.closesigesoft();
            return comp;
        }

        private bool ObtenerUser(string service, string userName)
        {
            bool result = false;
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena1 = @"select SU.v_UserName from servicecomponent SC  " +
                          "inner join systemuser SU on SC.i_ApprovedUpdateUserId=SU.i_SystemUserId " +
                          "where SC.v_ServiceId='" + service + "' and SC.i_IsRequiredId=1 group by SU.v_UserName";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector1 = comando.ExecuteReader();
            while (lector1.Read())
            {
                string user = lector1.GetValue(0).ToString();
                if (user == userName)
                {
                    result = true;
                    break;
                }
            }
            lector1.Close();
            conectasam.closesigesoft();
            return result;
        }

        private bool ObtenerUserBlockAuditor(string v_service, int userId)
        {
            bool result = false;
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena1 = @"select i_UpdateUserMedicalAnalystId from service where v_ServiceId='" + v_service + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector1 = comando.ExecuteReader();
            while (lector1.Read())
            {
                object user = lector1.GetValue(0).ToString();
                if (userId.ToString() == user.ToString() || user.ToString() == "")
                {
                    result = true;
                    break;
                }
            }
            lector1.Close();
            conectasam.closesigesoft();
            return result;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //"Matriz de datos <Empresa Cliente> de <fecha inicio> a <fecha fin>"
            string NombreArchivo = "";

         
            NombreArchivo = "Reporte Servicios y Componentes de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
         

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog3.FileName = NombreArchivo;
            saveFileDialog3.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog3.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdDataService, saveFileDialog3.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ddlServiceTypeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (ddlServiceTypeId.SelectedValue.ToString() == "-1")
            {
                ddlMasterServiceId.SelectedValue = "-1";
                ddlMasterServiceId.Enabled = false;
                ddlConsultorio.Enabled = false;
                Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 116, null, 0), DropDownListAction.All);
                return;
            }
            else if (ddlServiceTypeId.SelectedValue.ToString() == "1")
            {
                //OCUPACIONAL
                ddlMasterServiceId.Enabled = true;
                ddlConsultorio.Enabled = true;
                Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 116, null, 40), DropDownListAction.All);

                Utils.LoadDropDownList(cboGrupoExamen, "Value1", "Id", BLL.Utils.GetGrupoExamen(ref objOperationResult, 40), DropDownListAction.All);

            }
            else if (ddlServiceTypeId.SelectedValue.ToString() == "9")
            {
                //ASISTENCIAL

                ddlMasterServiceId.Enabled = true;
                ddlConsultorio.Enabled = true;
                Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 116, null, 41), DropDownListAction.All);

                Utils.LoadDropDownList(cboGrupoExamen, "Value1", "Id", BLL.Utils.GetGrupoExamen(ref objOperationResult, 41), DropDownListAction.All);

            }
            else if (ddlServiceTypeId.SelectedValue.ToString() == "11")
            {
                ddlMasterServiceId.Enabled = true;
                ddlConsultorio.Enabled = true;
                Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 116, null, 41), DropDownListAction.All);
            }
            else
            {
                ddlMasterServiceId.Enabled = true;
                ddlConsultorio.Enabled = true;

                ddlMasterServiceId.Enabled = true;
                ddlConsultorio.Enabled = true;
               
                Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 116, null, 0), DropDownListAction.All);

            }
        }

        private void ddlServiceTypeId_TextChanged(object sender, EventArgs e)
        {
            if (ddlServiceTypeId.SelectedIndex == 0 || ddlServiceTypeId.SelectedIndex == -1)
                return;

            OperationResult objOperationResult = new OperationResult();
            var id = int.Parse(ddlServiceTypeId.SelectedValue.ToString());
            Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, id, null), DropDownListAction.Select);

        }

        private void ddlMasterServiceId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMasterServiceId.SelectedValue == null)
                return;

            if (ddlMasterServiceId.SelectedValue.ToString() == "-1")
            {
                //ddlEsoType.SelectedValue = "-1";
                //ddlEsoType.Enabled = false;
                return;
            }

            OperationResult objOperationResult = new OperationResult();


            if (ddlMasterServiceId.SelectedValue.ToString() == ((int)Common.MasterService.Eso).ToString() ||
                ddlMasterServiceId.SelectedValue.ToString() == "12")
            {

                //ddlEsoType.Enabled = true;

               
            }
            else
            {

                //ddlEsoType.SelectedValue = "-1";
                //ddlEsoType.Enabled = false;

            }
        }

        private void btnGraficar_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiarios ticket = new frmServiceYComponentesGraficosDiarios(ListaGrilla.ToList(), dtpDateTimeStar.Value.ToShortDateString(), dptDateTimeEnd.Value.ToShortDateString());
            ticket.ShowDialog();
        }
    }
}
