using Infragistics.Win.UltraWinGrid;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sigesoft.Node.WinClient.UI.Gerencia
{
    public partial class frmEstadisticas : Form
    {
        ServiceBL _serviceBL = new ServiceBL();
        private BindingList<ServiceGridJerarquizadaList> ListaGrilla = new BindingList<ServiceGridJerarquizadaList>();
        private BindingList<ServiceGridJerarquizadaList> ListaGrillaTemp = new BindingList<ServiceGridJerarquizadaList>();

        private BindingList<ServiceGridJerarquizadaListNew> ListaGrillaAsistencial = new BindingList<ServiceGridJerarquizadaListNew>();
        private BindingList<ServiceGridJerarquizadaList> ListaGrillaTempAsistencial = new BindingList<ServiceGridJerarquizadaList>();


        private List<HospitalizacionList> ListaGrillaSopHosp = new List<HospitalizacionList>();
        private List<HospitalizacionList> ListaGrillaSopHospTemp = new List<HospitalizacionList>();

        HospitalizacionBL _objHospBL = new HospitalizacionBL();

        ArrayList PorESO_Tipe = new ArrayList();
        ArrayList PorESO_Cant = new ArrayList();
        ArrayList PorESO_Cant2 = new ArrayList();

        ArrayList TipoGeneroHospSop = new ArrayList();
        ArrayList CantidadGeneroHospSop = new ArrayList();

        string nombreparaGraficoHospSop = "";
        string nombreparaGraficoGlobal = "";


        List<ListaEstadisticos> ListaEstadisticostemp = new List<ListaEstadisticos>();

        List<ListaEstadisticosAsist> ListaEstadisticosConsultorioMdico = new List<ListaEstadisticosAsist>();

        public frmEstadisticas()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            var usuarioActual = Globals.ClientSession.i_SystemUserId;
            var usuario_data = new ServiceBL().GetSystemUserId(usuarioActual);
            var usuario_professional = new ServiceBL().GetProfessional(usuario_data.v_PersonId);
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.BindGrid();
            };
        }
        private void BindGrid()
        {
            var objData = GetData(0, null, "", "");
            ListaGrilla = objData;
            grdDataServiceOcupacional.DataSource = objData;
            #region Ocultar columnas
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_PersonId"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["b_FechaEntrega"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_ServiceStatusName"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["i_ServiceStatusId"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_LocationName"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_ProtocolId"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_ComponentId"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["i_LineStatusId"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_DiagnosticRepositoryId"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_DiseasesName"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["d_ExpirationDateDiagnostic"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["d_ServiceDate"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["TipoServicioMaster"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["TipoServicioESO"].Hidden = true;

            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_Recommendation"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["i_ServiceId"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["i_ServiceTypeId"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_CustomerOrganizationId"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_CustomerLocationId"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["i_MasterServiceId"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["i_AptitudeStatusId"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["i_EsoTypeId"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["i_IsDeleted"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_CreationUser"].Hidden = true;

            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_UpdateUser"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["d_CreationDate"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["d_UpdateDate"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["i_StatusLiquidation"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_MasterServiceName"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_EsoTypeName"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["CIE10"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["d_FechaNacimiento"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["NroPoliza"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["d_FechaEntrega"].Hidden = true;

            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["Moneda"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["NroFactura"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["Valor"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["i_FinalQualificationId"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_Restriccion"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["d_Deducible"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["i_IsDeletedDx"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["LogoEmpresaPropietaria"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["i_IsDeletedRecomendaciones"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["i_IsDeletedRestricciones"].Hidden = true;

            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["i_age"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["UsuarioMedicina"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["item"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["i_ApprovedUpdateUserId"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_Consultorio"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_ConsultorioId"].Hidden = true;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["i_ServiceComponentStatusId"].Hidden = true;

            #endregion

            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["Liq"].Header.VisiblePosition = 0;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_DocNumber"].Header.VisiblePosition = 1;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["Fecha"].Header.VisiblePosition = 2;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_Pacient"].Header.VisiblePosition = 3;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["Edad"].Header.VisiblePosition = 4;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_ServiceStatusName"].Header.VisiblePosition = 5;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_AptitudeStatusName"].Header.VisiblePosition = 6;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["CompMinera"].Header.VisiblePosition = 7;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_OrganizationName"].Header.VisiblePosition = 8;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["Tercero"].Header.VisiblePosition = 9;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_ProtocolName"].Header.VisiblePosition = 10;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_ServiceId"].Header.VisiblePosition = 11;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["TipoServicio"].Header.VisiblePosition = 12;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["UsuarioCrea"].Header.VisiblePosition = 13;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_MedicoTratante"].Header.VisiblePosition = 14;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["d_BirthDate"].Header.VisiblePosition = 15;
            grdDataServiceOcupacional.DisplayLayout.Bands[0].Columns["v_TelephoneNumber"].Header.VisiblePosition = 21;

            lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", objData.Count());

        }

        private BindingList<ServiceGridJerarquizadaList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            DateTime? FCI = dtpDateTimeStar.Value.Date;
            DateTime? FCF = dptDateTimeEnd.Value.Date.AddDays(1);

           BindingList<ServiceGridJerarquizadaList> _objData = new BindingList<ServiceGridJerarquizadaList>();

            _objData = _serviceBL.GetServicesPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate, null, FCI, FCF, "");

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
            if (ddlEsoType.SelectedIndex != 0)
            {
                List<ServiceGridJerarquizadaList> Data = _objData.Where(p => p.i_EsoTypeId == Convert.ToInt32(ddlEsoType.SelectedValue)).ToList();
                _objData = new BindingList<ServiceGridJerarquizadaList>(Data);
            }

            if (ddlConsultorio.SelectedIndex != 0)
            {
                List<ServiceGridJerarquizadaList> Data = new List<ServiceGridJerarquizadaList>();

              
                    foreach (var item in _objData)
                    {
                        bool result = ObtenerConsultorio(item.v_ServiceId, ddlConsultorio.Text);
                        if (result)
                        {
                            Data.Add(item);
                        }
                    }


                _objData = new BindingList<ServiceGridJerarquizadaList>(Data);
            }

            return _objData;

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

        private void frmEstadisticas_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            Utils.LoadDropDownList(ddlServiceTypeId, "Value1", "Id", BLL.Utils.GetServiceType(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.All);
            ddlServiceTypeId.SelectedValue = "1";

            Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetMasterService(ref objOperationResult, 1, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.All);
            ddlMasterServiceId.SelectedValue = "2";



            Utils.LoadDropDownList(ddlServiceTypeIdAsistencial, "Value1", "Id", BLL.Utils.GetServiceType(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.All);
            ddlServiceTypeIdAsistencial.SelectedValue = "9";

           

            Utils.LoadDropDownList(ddlEsoType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 118, null), DropDownListAction.All);
            Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 116, null, 40), DropDownListAction.All);

            //Utils.LoadDropDownList(ddlMasterServiceIdAsistencial, "Value1", "Id", BLL.Utils.GetMasterService(ref objOperationResult, -1, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.All);
            //ddlMasterServiceIdAsistencial.SelectedValue = "-1";

            Utils.LoadDropDownList(ddlEsoTypeAsistencial, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 118, null), DropDownListAction.All);
            Utils.LoadDropDownList(ddlConsultorioAsistencial, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 116, null, 40), DropDownListAction.All);

            Utils.LoadDropDownList(cboCantidadMostrar, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 411, null), DropDownListAction.All);
            Utils.LoadDropDownList(cboCantidadMostrarHospSop, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 411, null), DropDownListAction.All);
            Utils.LoadDropDownList(cboCantidadMostrarAsistencial, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 411, null), DropDownListAction.All);

        }

        private void ddlServiceTypeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //OperationResult objOperationResult = new OperationResult();
            //if (ddlServiceTypeId.SelectedValue == "-1")
            //{
            //    ddlMasterServiceId.SelectedValue = "-1";
            //    ddlMasterServiceId.Enabled = false;
            //    ddlConsultorio.Enabled = false;
            //    Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 116, null, 0), DropDownListAction.All);

            //    return;
            //}
            //else if (ddlServiceTypeId.SelectedValue.ToString() == "1" )
            //{
            //    ddlMasterServiceId.Enabled = true;
            //    ddlConsultorio.Enabled = true;
            //    Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 116, null, 40), DropDownListAction.All);
            //}
            //else if (ddlServiceTypeId.SelectedValue.ToString() == "9")
            //{
            //    ddlMasterServiceId.Enabled = true;
            //    ddlConsultorio.Enabled = true;
            //    Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 116, null, 41), DropDownListAction.All);
            //}
            //else if (ddlServiceTypeId.SelectedValue.ToString() == "11")
            //{
            //    ddlMasterServiceId.Enabled = true;
            //    ddlConsultorio.Enabled = true;
            //    Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 116, null, 41), DropDownListAction.All);
            //}
            //else
            //{
            //    ddlMasterServiceId.Enabled = true;
            //    ddlConsultorio.Enabled = true;

            //    ddlMasterServiceId.Enabled = true;
            //    ddlConsultorio.Enabled = true;
            //    Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 116, null, 0), DropDownListAction.All);

            //}
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
                ddlEsoType.SelectedValue = "-1";
                ddlEsoType.Enabled = false;
                return;
            }

            OperationResult objOperationResult = new OperationResult();


            if (ddlMasterServiceId.SelectedValue.ToString() == ((int)Common.MasterService.Eso).ToString() ||
                ddlMasterServiceId.SelectedValue.ToString() == "12")
            {
                ddlEsoType.Enabled = true;
            }
            else
            {
                ddlEsoType.SelectedValue = "-1";
                ddlEsoType.Enabled = false;

            }
        }

        private void btnGraficar_Click(object sender, EventArgs e)
        {
            List<ServiceGridJerarquizadaList> list = ListaGrilla.ToList();
            if (checkEso.Checked == true)
            {
                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.TipoServicio).Select(s => s.First()).ToList();
                if (cboCantidadMostrar.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.TipoServicio == item.TipoServicio).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        //objListaEstadisticostemp.Id = contador.ToString();
                        objListaEstadisticostemp.Grupo = item.TipoServicio;
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDesc.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }

                    int contador = 1;
                

                foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);
                        contador++;
                    }

                    grdRESUMEN.DataSource = ListaEstadisticostemp;
                    chartGraficoGlobal.Titles[0].Text = nombreparaGraficoGlobal = "AGRUPADO POR ESO DEL " + dtpDateTimeStar.Value.ToShortDateString() + " AL " + dptDateTimeEnd.Value.ToShortDateString();

                    chartGraficoGlobal.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);

                    button6.Enabled = true;
                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.TipoServicio == item.TipoServicio).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        //objListaEstadisticostemp.Id = contador.ToString();
                        objListaEstadisticostemp.Grupo = item.TipoServicio;
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);
                    }
                    if (checkAscDesc.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();
                    }
                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrar.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }

                    foreach (var item in ListaEstadisticostemp_)
                    {
                       ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }
                            contador++;
                    }

                    grdRESUMEN.DataSource = ListaEstadisticostemp;
                    chartGraficoGlobal.Titles[0].Text = nombreparaGraficoGlobal = "AGRUPADO POR ESO DEL " + dtpDateTimeStar.Value.ToShortDateString() + " AL " + dptDateTimeEnd.Value.ToShortDateString();

                    chartGraficoGlobal.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    button6.Enabled = true;
                }
                
            }
            else if (checkProtocolo.Checked == true)
            {
                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.v_ProtocolName).Select(s => s.First()).ToList();
                if (cboCantidadMostrar.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.v_ProtocolName == item.v_ProtocolName).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        //objListaEstadisticostemp.Id = contador.ToString();
                        objListaEstadisticostemp.Grupo = item.v_ProtocolName;
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDesc.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }

                    int contador = 1;


                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);
                        contador++;
                    }

                    grdRESUMEN.DataSource = ListaEstadisticostemp;
                    chartGraficoGlobal.Titles[0].Text = nombreparaGraficoGlobal = "AGRUPADO POR PROTOCOLO DEL " + dtpDateTimeStar.Value.ToShortDateString() + " AL " + dptDateTimeEnd.Value.ToShortDateString();

                    chartGraficoGlobal.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    button6.Enabled = true;
                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.v_ProtocolName == item.v_ProtocolName).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        //objListaEstadisticostemp.Id = contador.ToString();
                        objListaEstadisticostemp.Grupo = item.v_ProtocolName;
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);
                    }
                    if (checkAscDesc.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();
                    }
                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrar.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }

                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }
                        contador++;
                    }

                    grdRESUMEN.DataSource = ListaEstadisticostemp;
                    chartGraficoGlobal.Titles[0].Text = nombreparaGraficoGlobal = "AGRUPADO POR PROTOCOLO DEL " + dtpDateTimeStar.Value.ToShortDateString() + " AL " + dptDateTimeEnd.Value.ToShortDateString();

                    chartGraficoGlobal.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    button6.Enabled = true;
                }
            }
            else if (checkMedico.Checked == true)
            {
                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.v_MedicoTratante).Select(s => s.First()).ToList();
                if (cboCantidadMostrar.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.v_MedicoTratante == item.v_MedicoTratante).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        //objListaEstadisticostemp.Id = contador.ToString();
                        objListaEstadisticostemp.Grupo = item.v_MedicoTratante;
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDesc.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }

                    int contador = 1;


                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);
                        contador++;
                    }

                    grdRESUMEN.DataSource = ListaEstadisticostemp;
                    chartGraficoGlobal.Titles[0].Text = nombreparaGraficoGlobal = "AGRUPADO POR MÉDICO DEL " + dtpDateTimeStar.Value.ToShortDateString() + " AL " + dptDateTimeEnd.Value.ToShortDateString();

                    chartGraficoGlobal.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    button6.Enabled = true;
                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.v_MedicoTratante == item.v_MedicoTratante).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        //objListaEstadisticostemp.Id = contador.ToString();
                        objListaEstadisticostemp.Grupo = item.v_MedicoTratante;
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);
                    }
                    if (checkAscDesc.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();
                    }
                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrar.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }

                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }
                        contador++;
                    }

                    grdRESUMEN.DataSource = ListaEstadisticostemp;
                    chartGraficoGlobal.Titles[0].Text = nombreparaGraficoGlobal = "AGRUPADO POR MÉDICO DEL " + dtpDateTimeStar.Value.ToShortDateString() + " AL " + dptDateTimeEnd.Value.ToShortDateString();

                    chartGraficoGlobal.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    button6.Enabled = true;
                }
            }
            else if (checkEmpresa.Checked == true)
            {
                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.CompMinera).Select(s => s.First()).ToList();
                if (cboCantidadMostrar.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.CompMinera == item.CompMinera).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        //objListaEstadisticostemp.Id = contador.ToString();
                        objListaEstadisticostemp.Grupo = item.CompMinera;
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDesc.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }

                    int contador = 1;


                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);
                        contador++;
                    }

                    grdRESUMEN.DataSource = ListaEstadisticostemp;
                    chartGraficoGlobal.Titles[0].Text = nombreparaGraficoGlobal = "AGRUPADO POR COMPAÑIA MINERA DEL " + dtpDateTimeStar.Value.ToShortDateString() + " AL " + dptDateTimeEnd.Value.ToShortDateString();

                    chartGraficoGlobal.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    button6.Enabled = true;
                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.CompMinera == item.CompMinera).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        //objListaEstadisticostemp.Id = contador.ToString();
                        objListaEstadisticostemp.Grupo = item.CompMinera;
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);
                    }
                    if (checkAscDesc.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();
                    }
                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrar.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }

                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }
                        contador++;
                    }

                    grdRESUMEN.DataSource = ListaEstadisticostemp;
                    chartGraficoGlobal.Titles[0].Text = nombreparaGraficoGlobal = "AGRUPADO POR COMPAÑIA MINERA DEL " + dtpDateTimeStar.Value.ToShortDateString() + " AL " + dptDateTimeEnd.Value.ToShortDateString();

                    chartGraficoGlobal.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    button6.Enabled = true;
                }
            }
            else if (checkContrata.Checked == true)
            {
                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.v_OrganizationName).Select(s => s.First()).ToList();
                if (cboCantidadMostrar.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.v_OrganizationName == item.v_OrganizationName).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        //objListaEstadisticostemp.Id = contador.ToString();
                        objListaEstadisticostemp.Grupo = item.v_OrganizationName;
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDesc.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }

                    int contador = 1;


                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);
                        contador++;
                    }

                    grdRESUMEN.DataSource = ListaEstadisticostemp;
                    chartGraficoGlobal.Titles[0].Text = nombreparaGraficoGlobal = "AGRUPADO POR CONTRATA DEL " + dtpDateTimeStar.Value.ToShortDateString() + " AL " + dptDateTimeEnd.Value.ToShortDateString();

                    chartGraficoGlobal.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    button6.Enabled = true;
                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.v_OrganizationName == item.v_OrganizationName).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        //objListaEstadisticostemp.Id = contador.ToString();
                        objListaEstadisticostemp.Grupo = item.v_OrganizationName;
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);
                    }
                    if (checkAscDesc.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();
                    }
                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrar.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }

                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }
                        contador++;
                    }

                    grdRESUMEN.DataSource = ListaEstadisticostemp;
                    chartGraficoGlobal.Titles[0].Text = nombreparaGraficoGlobal = "AGRUPADO POR CONTRATA DEL " + dtpDateTimeStar.Value.ToShortDateString() + " AL " + dptDateTimeEnd.Value.ToShortDateString();

                    chartGraficoGlobal.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    button6.Enabled = true;
                }
            }
            else if (checkMinayContrata.Checked == true)
            {
                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.CompMinera).Select(s => s.First()).ToList();
                // v_OrganizationName => contrata 
                if (cboCantidadMostrar.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.CompMinera == item.CompMinera).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        //objListaEstadisticostemp.Id = contador.ToString();
                        objListaEstadisticostemp.Grupo = item.CompMinera;
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        var esotempgroup2 = esotempgroup_.GroupBy(p => p.v_OrganizationName).Select(s => s.First()).ToList();
                        objListaEstadisticostemp.Cantidad2 = esotempgroup2.Count();

                        List<ListaEstadisticos2> ListaEstadisticostempHios_ = new List<ListaEstadisticos2>();

                        foreach (var item2 in esotempgroup2)
                        {
                            var contar = esotempgroup_.FindAll(p => p.v_OrganizationName == item2.v_OrganizationName).ToList();

                            ListaEstadisticos2 objListaEstadisticostemp_ = new ListaEstadisticos2();
                            objListaEstadisticostemp_.GrupoC = item2.v_OrganizationName;
                            objListaEstadisticostemp_.Cantidad = contar.Count();
                            List<ListaEstadisticosDetalle> lista = new List<ListaEstadisticosDetalle>();
                            foreach (var item3 in contar)
                            {
                                ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                                ListaEstadisticosDetalleObj.v_Servicio = item3.v_ServiceId;
                                ListaEstadisticosDetalleObj.v_Paciente = item3.v_Pacient;
                                ListaEstadisticosDetalleObj.v_DocNumber = item3.v_DocNumber;
                                ListaEstadisticosDetalleObj.d_ServiceDate = item3.Fecha;
                                ListaEstadisticosDetalleObj.v_AptitudeStatusName = item3.v_AptitudeStatusName;
                                ListaEstadisticosDetalleObj.v_ProtocolName = item3.v_ProtocolName;
                                ListaEstadisticosDetalleObj.v_ServiceStatusName = item3.TipoServicioESO;

                                lista.Add(ListaEstadisticosDetalleObj);
                            }
                            objListaEstadisticostemp_.Detalle = lista;
                            ListaEstadisticostempHios_.Add(objListaEstadisticostemp_);
                        }

                        objListaEstadisticostemp.DetalleGrupos = ListaEstadisticostempHios_;

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDesc.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }

                    int contador = 1;



                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;

                        item.DetalleGrupos = item.DetalleGrupos.OrderByDescending(p => p.Cantidad).ToList();
                        List<ListaEstadisticos2> ListaEstadisticostemp2 = new List<ListaEstadisticos2>();
                        int contador2 = 1;
                        foreach (var item2 in item.DetalleGrupos)
                        {
                            ListaEstadisticos2 objListaEstadisticostemp2 = new ListaEstadisticos2();

                            objListaEstadisticostemp2.Id = contador2;
                            objListaEstadisticostemp2.GrupoC = item2.GrupoC;
                            objListaEstadisticostemp2.Cantidad = item2.Cantidad;
                            objListaEstadisticostemp2.Detalle = item2.Detalle;

                            ListaEstadisticostemp2.Add(objListaEstadisticostemp2);
                            contador2++;
                        }

                        objListaEstadisticostemp.DetalleGrupos = ListaEstadisticostemp2;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);
                        contador++;
                    }

                    grdRESUMEN.DataSource = ListaEstadisticostemp;
                    chartGraficoGlobal.Titles[0].Text = nombreparaGraficoGlobal = "AGRUPADO POR CONTRATA DEL " + dtpDateTimeStar.Value.ToShortDateString() + " AL " + dptDateTimeEnd.Value.ToShortDateString();

                    chartGraficoGlobal.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    button6.Enabled = true;
                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.CompMinera == item.CompMinera).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        //objListaEstadisticostemp.Id = contador.ToString();
                        objListaEstadisticostemp.Grupo = item.CompMinera;
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        var esotempgroup2 = esotempgroup_.GroupBy(p => p.v_OrganizationName).Select(s => s.First()).ToList();
                        objListaEstadisticostemp.Cantidad2 = esotempgroup2.Count();

                        List<ListaEstadisticos2> ListaEstadisticostempHios_ = new List<ListaEstadisticos2>();

                        foreach (var item2 in esotempgroup2)
                        {
                            var contar = esotempgroup_.FindAll(p => p.v_OrganizationName == item2.v_OrganizationName).ToList();

                            ListaEstadisticos2 objListaEstadisticostemp_ = new ListaEstadisticos2();
                            objListaEstadisticostemp_.GrupoC = item2.v_OrganizationName;
                            objListaEstadisticostemp_.Cantidad = contar.Count();
                            List<ListaEstadisticosDetalle> lista = new List<ListaEstadisticosDetalle>();
                            foreach (var item3 in contar)
                            {
                                ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                                ListaEstadisticosDetalleObj.v_Servicio = item3.v_ServiceId;
                                ListaEstadisticosDetalleObj.v_Paciente = item3.v_Pacient;
                                ListaEstadisticosDetalleObj.v_DocNumber = item3.v_DocNumber;
                                ListaEstadisticosDetalleObj.d_ServiceDate = item3.Fecha;
                                ListaEstadisticosDetalleObj.v_AptitudeStatusName = item3.v_AptitudeStatusName;
                                ListaEstadisticosDetalleObj.v_ProtocolName = item3.v_ProtocolName;
                                ListaEstadisticosDetalleObj.v_ServiceStatusName = item3.TipoServicioESO;


                                lista.Add(ListaEstadisticosDetalleObj);
                            }
                            objListaEstadisticostemp_.Detalle = lista;
                            ListaEstadisticostempHios_.Add(objListaEstadisticostemp_);
                        }

                        objListaEstadisticostemp.DetalleGrupos = ListaEstadisticostempHios_;

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDesc.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();
                    }
                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrar.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }

                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Cantidad2 = item.Cantidad2;

                        item.DetalleGrupos = item.DetalleGrupos.OrderByDescending(p => p.Cantidad).ToList();
                        List<ListaEstadisticos2> ListaEstadisticostemp2 = new List<ListaEstadisticos2>();
                        int contador2 = 1;
                        foreach (var item2 in item.DetalleGrupos)
                        {
                            ListaEstadisticos2 objListaEstadisticostemp2 = new ListaEstadisticos2();

                            objListaEstadisticostemp2.Id = contador2;
                            objListaEstadisticostemp2.GrupoC = item2.GrupoC;
                            objListaEstadisticostemp2.Cantidad = item2.Cantidad;
                            objListaEstadisticostemp2.Detalle = item2.Detalle;

                            ListaEstadisticostemp2.Add(objListaEstadisticostemp2);
                            contador2++;
                        }

                        objListaEstadisticostemp.DetalleGrupos = ListaEstadisticostemp2;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }
                        contador++;
                    }

                    grdRESUMEN.DataSource = ListaEstadisticostemp;
                    chartGraficoGlobal.Titles[0].Text = nombreparaGraficoGlobal = "AGRUPADO POR CONTRATA DEL " + dtpDateTimeStar.Value.ToShortDateString() + " AL " + dptDateTimeEnd.Value.ToShortDateString();

                    chartGraficoGlobal.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    button6.Enabled = true;
                }
            }
            else if (checkMinayContrataESO.Checked == true)
            {
                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.CompMinera).Select(s => s.First()).ToList();
                // v_OrganizationName => contrata 
                if (cboCantidadMostrar.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.CompMinera == item.CompMinera).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        //objListaEstadisticostemp.Id = contador.ToString();
                        objListaEstadisticostemp.Grupo = item.CompMinera;
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        var esotempgroup2 = esotempgroup_.GroupBy(p => p.v_OrganizationName).Select(s => s.First()).ToList();
                        objListaEstadisticostemp.Cantidad2 = esotempgroup2.Count();

                        List<ListaEstadisticos2> ListaEstadisticostempHios_ = new List<ListaEstadisticos2>();

                        foreach (var item2 in esotempgroup2)
                        {
                            var contar = esotempgroup_.FindAll(p => p.v_OrganizationName == item2.v_OrganizationName).ToList();

                            ListaEstadisticos2 objListaEstadisticostemp_ = new ListaEstadisticos2();
                            objListaEstadisticostemp_.GrupoC = item2.v_OrganizationName;
                            objListaEstadisticostemp_.Cantidad = contar.Count();
                            objListaEstadisticostemp_.CompMinera = item2.CompMinera;


                            var esotempgroupESO = contar.GroupBy(p => p.TipoServicioESO).Select(s => s.First()).ToList();


                            List<ListaEstadisticos3> ListaEstadisticostempHijosESO_ = new List<ListaEstadisticos3>();

                            foreach (var item3 in esotempgroupESO)
                            {
                                var contarESO = contar.FindAll(p => p.TipoServicioESO == item3.TipoServicioESO).ToList();

                                ListaEstadisticos3 objListaEstadisticostempESO_ = new ListaEstadisticos3();
                                objListaEstadisticostempESO_.Grupo = item3.TipoServicioESO;
                                objListaEstadisticostempESO_.Cantidad = contarESO.Count();

                                List<ListaEstadisticosDetalle> lista = new List<ListaEstadisticosDetalle>();
                                foreach (var item4 in contarESO)
                                {
                                    ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                                    ListaEstadisticosDetalleObj.v_Servicio = item4.v_ServiceId;
                                    ListaEstadisticosDetalleObj.v_Paciente = item4.v_Pacient;
                                    ListaEstadisticosDetalleObj.v_DocNumber = item4.v_DocNumber;
                                    ListaEstadisticosDetalleObj.d_ServiceDate = item4.Fecha;
                                    ListaEstadisticosDetalleObj.v_AptitudeStatusName = item4.v_AptitudeStatusName;
                                    ListaEstadisticosDetalleObj.v_ProtocolName = item4.v_ProtocolName;
                                    ListaEstadisticosDetalleObj.v_ServiceStatusName = item4.TipoServicioESO;

                                    lista.Add(ListaEstadisticosDetalleObj);
                                }
                                objListaEstadisticostempESO_.Detalles = lista;

                                ListaEstadisticostempHijosESO_.Add(objListaEstadisticostempESO_);
                            }

                            objListaEstadisticostemp_.DetalleESO = ListaEstadisticostempHijosESO_;
                            
                            ListaEstadisticostempHios_.Add(objListaEstadisticostemp_);
                        }

                        objListaEstadisticostemp.DetalleGrupos = ListaEstadisticostempHios_;

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDesc.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }

                    int contador = 1;



                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;

                        item.DetalleGrupos = item.DetalleGrupos.OrderByDescending(p => p.Cantidad).ToList();
                        List<ListaEstadisticos2> ListaEstadisticostemp2 = new List<ListaEstadisticos2>();
                        int contador2 = 1;
                        foreach (var item2 in item.DetalleGrupos)
                        {
                            ListaEstadisticos2 objListaEstadisticostemp2 = new ListaEstadisticos2();

                            objListaEstadisticostemp2.Id = contador2;
                            objListaEstadisticostemp2.GrupoC = item2.GrupoC;
                            objListaEstadisticostemp2.Cantidad = item2.Cantidad;
                            objListaEstadisticostemp2.CompMinera = item2.CompMinera;

                            List<ListaEstadisticos3> ListaEstadisticostemp3 = new List<ListaEstadisticos3>();

                            int contador3 = 1;
                            foreach (var item3 in item2.DetalleESO)
                            {
                                ListaEstadisticos3 objListaEstadisticostemp3 = new ListaEstadisticos3();

                                objListaEstadisticostemp3.Id = contador3;
                                objListaEstadisticostemp3.Grupo = item3.Grupo;
                                objListaEstadisticostemp3.Cantidad = item3.Cantidad;
                                objListaEstadisticostemp3.Detalles = item3.Detalles;
                                ListaEstadisticostemp3.Add(objListaEstadisticostemp3);
                            }

                            objListaEstadisticostemp2.DetalleESO = ListaEstadisticostemp3;

                            ListaEstadisticostemp2.Add(objListaEstadisticostemp2);
                            contador2++;
                        }

                        objListaEstadisticostemp.DetalleGrupos = ListaEstadisticostemp2;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);
                        contador++;
                    }

                    grdRESUMEN.DataSource = ListaEstadisticostemp;
                    chartGraficoGlobal.Titles[0].Text = nombreparaGraficoGlobal = "AGRUPADO POR CONTRATA DEL " + dtpDateTimeStar.Value.ToShortDateString() + " AL " + dptDateTimeEnd.Value.ToShortDateString();

                    chartGraficoGlobal.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    button6.Enabled = true;
                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.CompMinera == item.CompMinera).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        //objListaEstadisticostemp.Id = contador.ToString();
                        objListaEstadisticostemp.Grupo = item.CompMinera;
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        var esotempgroup2 = esotempgroup_.GroupBy(p => p.v_OrganizationName).Select(s => s.First()).ToList();
                        objListaEstadisticostemp.Cantidad2 = esotempgroup2.Count();

                        List<ListaEstadisticos2> ListaEstadisticostempHios_ = new List<ListaEstadisticos2>();

                        foreach (var item2 in esotempgroup2)
                        {
                            var contar = esotempgroup_.FindAll(p => p.v_OrganizationName == item2.v_OrganizationName).ToList();

                            ListaEstadisticos2 objListaEstadisticostemp_ = new ListaEstadisticos2();
                            objListaEstadisticostemp_.GrupoC = item2.v_OrganizationName;
                            objListaEstadisticostemp_.Cantidad = contar.Count();
                            objListaEstadisticostemp_.CompMinera = item2.CompMinera;

                            var esotempgroupESO = contar.GroupBy(p => p.TipoServicioESO).Select(s => s.First()).ToList();


                            List<ListaEstadisticos3> ListaEstadisticostempHijosESO_ = new List<ListaEstadisticos3>();

                            foreach (var item3 in esotempgroupESO)
                            {
                                var contarESO = contar.FindAll(p => p.TipoServicioESO == item3.TipoServicioESO).ToList();

                                ListaEstadisticos3 objListaEstadisticostempESO_ = new ListaEstadisticos3();
                                objListaEstadisticostempESO_.Grupo = item3.TipoServicioESO;
                                objListaEstadisticostempESO_.Cantidad = contarESO.Count();

                                List<ListaEstadisticosDetalle> lista = new List<ListaEstadisticosDetalle>();
                                foreach (var item4 in contarESO)
                                {
                                    ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                                    ListaEstadisticosDetalleObj.v_Servicio = item4.v_ServiceId;
                                    ListaEstadisticosDetalleObj.v_Paciente = item4.v_Pacient;
                                    ListaEstadisticosDetalleObj.v_DocNumber = item4.v_DocNumber;
                                    ListaEstadisticosDetalleObj.d_ServiceDate = item4.Fecha;
                                    ListaEstadisticosDetalleObj.v_AptitudeStatusName = item4.v_AptitudeStatusName;
                                    ListaEstadisticosDetalleObj.v_ProtocolName = item4.v_ProtocolName;
                                    ListaEstadisticosDetalleObj.v_ServiceStatusName = item4.TipoServicioESO;

                                    lista.Add(ListaEstadisticosDetalleObj);
                                }
                                objListaEstadisticostempESO_.Detalles = lista;

                                ListaEstadisticostempHijosESO_.Add(objListaEstadisticostempESO_);
                            }

                            objListaEstadisticostemp_.DetalleESO = ListaEstadisticostempHijosESO_;

                            ListaEstadisticostempHios_.Add(objListaEstadisticostemp_);
                        }

                        objListaEstadisticostemp.DetalleGrupos = ListaEstadisticostempHios_;

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDesc.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();
                    }
                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrar.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrar.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }
                    
                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;

                        item.DetalleGrupos = item.DetalleGrupos.OrderByDescending(p => p.Cantidad).ToList();
                        List<ListaEstadisticos2> ListaEstadisticostemp2 = new List<ListaEstadisticos2>();
                        int contador2 = 1;
                        foreach (var item2 in item.DetalleGrupos)
                        {
                            ListaEstadisticos2 objListaEstadisticostemp2 = new ListaEstadisticos2();

                            objListaEstadisticostemp2.Id = contador2;
                            objListaEstadisticostemp2.GrupoC = item2.GrupoC;
                            objListaEstadisticostemp2.Cantidad = item2.Cantidad;
                            objListaEstadisticostemp2.CompMinera = item2.CompMinera;

                            List<ListaEstadisticos3> ListaEstadisticostemp3 = new List<ListaEstadisticos3>();

                            int contador3 = 1;
                            foreach (var item3 in item2.DetalleESO)
                            {
                                ListaEstadisticos3 objListaEstadisticostemp3 = new ListaEstadisticos3();

                                objListaEstadisticostemp3.Id = contador3;
                                objListaEstadisticostemp3.Grupo = item3.Grupo;
                                objListaEstadisticostemp3.Cantidad = item3.Cantidad;
                                objListaEstadisticostemp3.Detalles = item3.Detalles;
                                ListaEstadisticostemp3.Add(objListaEstadisticostemp3);
                            }

                            objListaEstadisticostemp2.DetalleESO = ListaEstadisticostemp3;

                            ListaEstadisticostemp2.Add(objListaEstadisticostemp2);
                            contador2++;
                        }

                        objListaEstadisticostemp.DetalleGrupos = ListaEstadisticostemp2;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }
                        
                        contador++;
                    }

                    grdRESUMEN.DataSource = ListaEstadisticostemp;
                    chartGraficoGlobal.Titles[0].Text = nombreparaGraficoGlobal = "AGRUPADO POR CONTRATA DEL " + dtpDateTimeStar.Value.ToShortDateString() + " AL " + dptDateTimeEnd.Value.ToShortDateString();

                    chartGraficoGlobal.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    button6.Enabled = true;
                }
            }
            else
            {
                ListaEstadisticostemp = new List<ListaEstadisticos>();

                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();
            }

        }

        private void checkEso_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEso.Checked == true)
            {
                checkProtocolo.Checked = false;
                checkMedico.Checked = false;
            }
            else
            {
                checkEso.Checked = false;
                checkProtocolo.Checked = false;
                checkMedico.Checked = false;
            }
        }

        private void checkProtocolo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkProtocolo.Checked == true)
            {
                checkEso.Checked = false;
                checkMedico.Checked = false;
            }
            else
            {
                checkProtocolo.Checked = false;
                checkEso.Checked = false;
                checkMedico.Checked = false;
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Atenciones del " + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text;
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdDataServiceOcupacional, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            string nombre = "";
            if (checkEso.Checked == true)
            {
                nombre = "ESO";
            }
            else if (checkProtocolo.Checked == true)
            {
                nombre = "PROTOCOLO";
            }
            else if (checkProtocolo.Checked == true)
            {
                nombre = "MÉDICO";
            }

            NombreArchivo = "RESUMEN DE ATENCIONES POR "+ nombre +" CONSOLIDADO DEL" + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text;
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdRESUMEN, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void checkMedico_CheckedChanged(object sender, EventArgs e)
        {
            if (checkMedico.Checked == true)
            {
                checkEso.Checked = false;
                checkProtocolo.Checked = false;
            }
            else
            {
                checkProtocolo.Checked = false;
                checkEso.Checked = false;
                checkMedico.Checked = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.BindGridSopHosp();
            };
        }

        private void BindGridSopHosp()
        {
            var objData = GetDataSopHosp(0, null, "", "");

            ListaGrillaSopHosp = objData;

            grdDataSopHosp.DataSource = objData;

            grdDataSopHosp.DisplayLayout.Bands[0].Columns["d_FechaAlta"].Hidden = true;
            grdDataSopHosp.DisplayLayout.Bands[0].Columns["d_FechaIngreso"].Hidden = true;
            grdDataSopHosp.DisplayLayout.Bands[0].Columns["v_PersonId"].Hidden = true;
            grdDataSopHosp.DisplayLayout.Bands[0].Columns["d_Birthdate"].Hidden = true;
            grdDataSopHosp.DisplayLayout.Bands[0].Columns["i_IsDeleted"].Hidden = true;
            grdDataSopHosp.DisplayLayout.Bands[0].Columns["v_Comentario"].Hidden = true;
            grdDataSopHosp.DisplayLayout.Bands[0].Columns["v_NroLiquidacion"].Hidden = true;
            grdDataSopHosp.DisplayLayout.Bands[0].Columns["d_PagoMedico"].Hidden = true;
            grdDataSopHosp.DisplayLayout.Bands[0].Columns["MedicoPago"].Hidden = true;
            grdDataSopHosp.DisplayLayout.Bands[0].Columns["d_PagoPaciente"].Hidden = true;
            grdDataSopHosp.DisplayLayout.Bands[0].Columns["PacientePago"].Hidden = true;
            grdDataSopHosp.DisplayLayout.Bands[0].Columns["v_Comprobantes"].Hidden = true;
            grdDataSopHosp.DisplayLayout.Bands[0].Columns["d_MontoPagado"].Hidden = true;
            grdDataSopHosp.DisplayLayout.Bands[0].Columns["i_PacientePago"].Hidden = true;
            grdDataSopHosp.DisplayLayout.Bands[0].Columns["i_MedicoPago"].Hidden = true;
            grdDataSopHosp.DisplayLayout.Bands[0].Columns["v_NroHospitalizacion"].Hidden = true;


            label7.Text = string.Format("Se encontraron {0} registros.", objData.Count());

        }

        private List<HospitalizacionList> GetDataSopHosp(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpInHospSop.Value.Date;
            DateTime? pdatEndDate = dtpEndHospSop.Value.Date.AddDays(1);

            DateTime? FCI = dtpInHospSop.Value.Date;
            DateTime? FCF = dtpEndHospSop.Value.Date.AddDays(1);

            List<HospitalizacionList> _objData = new List<HospitalizacionList>();

            _objData = _objHospBL.GetHospitalizacionPagedAndFilteredEstadisticas(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);

            if (rbHospitalizados.Checked == true)
            {
                List<HospitalizacionList> Data = _objData.Where(p => p.Hosp == "SI").ToList();
                _objData = new List<HospitalizacionList>(Data);
            }

            if (rbSOP.Checked == true)
            {
                List<HospitalizacionList> Data = _objData.Where(p => p.Sop == "SI").ToList();
                _objData = new List<HospitalizacionList>(Data);
            }

            return _objData;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<HospitalizacionList> list = ListaGrillaSopHosp.ToList();
            List<ListaEstadisticos> ListaEstadisticostemp = new List<ListaEstadisticos>();
            if (checkMedicoTto.Checked == true)
            {
                grdDataSopHosp.DisplayLayout.Bands[0].Columns["TipoHosp"].Hidden = true;

                chartGraficoGlobalHospSop.Visible = true;
                chartPastelGeneroHospSop.Visible = false;
                chartServicioMedico.Visible = false;

                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.v_MedicoTratante).Select(s => s.First()).ToList();
                if (cboCantidadMostrarHospSop.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.v_MedicoTratante == item.v_MedicoTratante).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Grupo = item.v_MedicoTratante;
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ =  new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Paciente;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_Servicio;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.d_FechaIngreso.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.v_Cie10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.v_Diagnostico;
                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }
                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescHospSop.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;
                   
                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;
                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        contador++;
                    }

                    grdRESUMENHospSop.DataSource = ListaEstadisticostemp;
                    chartGraficoGlobalHospSop.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR MÉDICO DEL " + dtpInHospSop.Value.ToShortDateString() + " AL " + dtpEndHospSop.Value.ToShortDateString();

                    chartGraficoGlobalHospSop.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvas.Enabled = true;
                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.v_MedicoTratante == item.v_MedicoTratante).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Grupo = item.v_MedicoTratante;
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Paciente;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_Servicio;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.d_FechaIngreso.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.v_Cie10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.v_Diagnostico;
                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }

                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;


                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescHospSop.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrarHospSop.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrarHospSop.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrarHospSop.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrarHospSop.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrarHospSop.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }


                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }
                        contador++;
                    }

                    grdRESUMENHospSop.DataSource = ListaEstadisticostemp;
                    chartGraficoGlobalHospSop.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR MÉDICO DEL " + dtpInHospSop.Value.ToShortDateString() + " AL " + dtpEndHospSop.Value.ToShortDateString();

                    chartGraficoGlobalHospSop.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvas.Enabled = true;
                }
            }
            else if (checkGenero.Checked == true)
            {
                grdDataSopHosp.DisplayLayout.Bands[0].Columns["TipoHosp"].Hidden = true;

                chartGraficoGlobalHospSop.Visible = false;
                chartPastelGeneroHospSop.Visible = true;
                chartServicioMedico.Visible = false;

                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.MedicoPago).Select(s => s.First()).ToList();
                if (cboCantidadMostrarHospSop.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.MedicoPago == item.MedicoPago).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Grupo = item.MedicoPago ;
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();
                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Paciente;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_Servicio;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.d_FechaIngreso.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.v_Cie10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.v_Diagnostico;
                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }
                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;
                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescHospSop.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;

                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        TipoGeneroHospSop.Add(item.Grupo);
                        CantidadGeneroHospSop.Add(item.Cantidad);

                        contador++;
                    }

                    grdRESUMENHospSop.DataSource = ListaEstadisticostemp;
                    chartPastelGeneroHospSop.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR GÉNERO DEL " + dtpInHospSop.Value.ToShortDateString() + " AL " + dtpEndHospSop.Value.ToShortDateString();

                    chartPastelGeneroHospSop.Series[0].Points.DataBindXY(TipoGeneroHospSop, CantidadGeneroHospSop);
                    btnDescargarCurvas.Enabled = true;

                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var generotempgroup_ = list.FindAll(p => p.MedicoPago == item.MedicoPago).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Grupo = item.MedicoPago;
                        objListaEstadisticostemp.Cantidad = generotempgroup_.Count();
                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in generotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Paciente;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_Servicio;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.d_FechaIngreso.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.v_Cie10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.v_Diagnostico;
                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }
                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;
                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescHospSop.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrarHospSop.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrarHospSop.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrarHospSop.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrarHospSop.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrarHospSop.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }


                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        TipoGeneroHospSop.Add(item.Grupo);
                        CantidadGeneroHospSop.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }
                        contador++;
                    }


                    grdRESUMENHospSop.DataSource = ListaEstadisticostemp;
                    chartPastelGeneroHospSop.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR GÉNERO DEL " + dtpInHospSop.Value.ToShortDateString() + " AL " + dtpEndHospSop.Value.ToShortDateString();

                    chartPastelGeneroHospSop.Series[0].Points.DataBindXY(TipoGeneroHospSop, CantidadGeneroHospSop);

                    btnDescargarCurvas.Enabled = true;
                }
            }
            else if (checkEspecialidad.Checked == true)
            {
                grdDataSopHosp.DisplayLayout.Bands[0].Columns["TipoHosp"].Hidden = true;

                chartGraficoGlobalHospSop.Visible = true;
                chartPastelGeneroHospSop.Visible = false;
                chartServicioMedico.Visible = false;

                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.Especialidad1).Select(s => s.First()).ToList();
                if (cboCantidadMostrarHospSop.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.Especialidad1 == item.Especialidad1).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Grupo = item.Especialidad1;
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();
                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Paciente;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_Servicio;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.d_FechaIngreso.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.v_Cie10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.v_Diagnostico;
                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }
                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;
                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescHospSop.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;

                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;
                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        contador++;
                    }

                    grdRESUMENHospSop.DataSource = ListaEstadisticostemp;
                    chartGraficoGlobalHospSop.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR ESPECIALIDAD DEL " + dtpInHospSop.Value.ToShortDateString() + " AL " + dtpEndHospSop.Value.ToShortDateString();

                    chartGraficoGlobalHospSop.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvas.Enabled = true;

                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var generotempgroup_ = list.FindAll(p => p.Especialidad1 == item.Especialidad1).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Grupo = item.Especialidad1;
                        objListaEstadisticostemp.Cantidad = generotempgroup_.Count();
                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in generotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Paciente;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_Servicio;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.d_FechaIngreso.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.v_Cie10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.v_Diagnostico;
                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }
                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescHospSop.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrarHospSop.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrarHospSop.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrarHospSop.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrarHospSop.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrarHospSop.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }


                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;
                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }
                        contador++;
                    }


                    grdRESUMENHospSop.DataSource = ListaEstadisticostemp;
                    chartGraficoGlobalHospSop.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR ESPECIALIDAD DEL " + dtpInHospSop.Value.ToShortDateString() + " AL " + dtpEndHospSop.Value.ToShortDateString();

                    chartGraficoGlobalHospSop.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);

                    btnDescargarCurvas.Enabled = true;
                }
            }
            else if (checkServicio.Checked == true)
            {
                grdDataSopHosp.DisplayLayout.Bands[0].Columns["TipoHosp"].Hidden = true;

                chartGraficoGlobalHospSop.Visible = true;
                chartPastelGeneroHospSop.Visible = false;
                chartServicioMedico.Visible = false;

                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                if (rbHospitalizados.Checked == true)
                {
                    var esotempgroup = list.GroupBy(p => p.TipoHosp).Select(s => s.First()).ToList();
                    if (cboCantidadMostrarHospSop.SelectedValue == "-1")
                    {
                        ListaEstadisticostemp = new List<ListaEstadisticos>();

                        List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                        foreach (var item in esotempgroup)
                        {
                            var esotempgroup_ = list.FindAll(p => p.TipoHosp == item.TipoHosp).ToList();

                            ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                            objListaEstadisticostemp.Grupo = item.TipoHosp;
                            objListaEstadisticostemp.Cantidad = esotempgroup_.Count();
                            List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                            foreach (var item2 in esotempgroup_)
                            {
                                ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                                ListaEstadisticosDetalleObj.v_Paciente = item2.v_Paciente;
                                ListaEstadisticosDetalleObj.v_Servicio = item2.v_Servicio;
                                ListaEstadisticosDetalleObj.d_FechaIngreso = item2.d_FechaIngreso.ToString();
                                ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                                ListaEstadisticosDetalleObj.v_Cie10 = item2.v_Cie10;
                                ListaEstadisticosDetalleObj.v_Diagnostico = item2.v_Diagnostico;
                                ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                            }
                            objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;
                            ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                        }

                        if (checkAscDescHospSop.Checked == true)
                        {
                            ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                        }
                        else
                        {
                            ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                        }
                        int contador = 1;

                        foreach (var item in ListaEstadisticostemp_)
                        {
                            ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                            objListaEstadisticostemp.Id = contador;
                            objListaEstadisticostemp.Grupo = item.Grupo;
                            objListaEstadisticostemp.Cantidad = item.Cantidad;
                            objListaEstadisticostemp.Detalle = item.Detalle;
                            ListaEstadisticostemp.Add(objListaEstadisticostemp);

                            PorESO_Tipe.Add(contador.ToString());
                            PorESO_Cant.Add(item.Cantidad);

                            contador++;
                        }

                        grdRESUMENHospSop.DataSource = ListaEstadisticostemp;
                        chartGraficoGlobalHospSop.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR SERVICIO DEL " + dtpInHospSop.Value.ToShortDateString() + " AL " + dtpEndHospSop.Value.ToShortDateString();

                        chartGraficoGlobalHospSop.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                        btnDescargarCurvas.Enabled = true;

                    }
                    else
                    {
                        ListaEstadisticostemp = new List<ListaEstadisticos>();

                        List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                        foreach (var item in esotempgroup)
                        {
                            var generotempgroup_ = list.FindAll(p => p.TipoHosp == item.TipoHosp).ToList();

                            ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                            objListaEstadisticostemp.Grupo = item.TipoHosp;
                            objListaEstadisticostemp.Cantidad = generotempgroup_.Count();
                            List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                            foreach (var item2 in generotempgroup_)
                            {
                                ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                                ListaEstadisticosDetalleObj.v_Paciente = item2.v_Paciente;
                                ListaEstadisticosDetalleObj.v_Servicio = item2.v_Servicio;
                                ListaEstadisticosDetalleObj.d_FechaIngreso = item2.d_FechaIngreso.ToString();
                                ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                                ListaEstadisticosDetalleObj.v_Cie10 = item2.v_Cie10;
                                ListaEstadisticosDetalleObj.v_Diagnostico = item2.v_Diagnostico;
                                ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                            }
                            objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;
                            ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                        }

                        if (checkAscDescHospSop.Checked == true)
                        {
                            ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                        }
                        else
                        {
                            ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                        }
                        int contador = 1;
                        int cantidadMostrar = 0;
                        if (cboCantidadMostrarHospSop.SelectedIndex == 1)
                        {
                            cantidadMostrar = 5;
                        }
                        else if (cboCantidadMostrarHospSop.SelectedIndex == 2)
                        {
                            cantidadMostrar = 10;
                        }
                        else if (cboCantidadMostrarHospSop.SelectedIndex == 3)
                        {
                            cantidadMostrar = 15;
                        }
                        else if (cboCantidadMostrarHospSop.SelectedIndex == 4)
                        {
                            cantidadMostrar = 20;
                        }
                        else if (cboCantidadMostrarHospSop.SelectedIndex == 5)
                        {
                            cantidadMostrar = 25;
                        }


                        foreach (var item in ListaEstadisticostemp_)
                        {
                            ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                            objListaEstadisticostemp.Id = contador;
                            objListaEstadisticostemp.Grupo = item.Grupo;
                            objListaEstadisticostemp.Cantidad = item.Cantidad;
                            objListaEstadisticostemp.Detalle = item.Detalle;

                            ListaEstadisticostemp.Add(objListaEstadisticostemp);

                            PorESO_Tipe.Add(contador.ToString());
                            PorESO_Cant.Add(item.Cantidad);

                            if (cantidadMostrar == contador)
                            {
                                break;
                            }
                            contador++;
                        }


                        grdRESUMENHospSop.DataSource = ListaEstadisticostemp;
                        chartGraficoGlobalHospSop.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR SERVICIO DEL " + dtpInHospSop.Value.ToShortDateString() + " AL " + dtpEndHospSop.Value.ToShortDateString();

                        chartGraficoGlobalHospSop.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);

                        btnDescargarCurvas.Enabled = true;
                    }
                }
                else if (rbSOP.Checked == true)
                {
                    var esotempgroup = list.GroupBy(p => p.v_Servicio).Select(s => s.First()).ToList();
                    if (cboCantidadMostrarHospSop.SelectedValue == "-1")
                    {
                        ListaEstadisticostemp = new List<ListaEstadisticos>();

                        List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                        foreach (var item in esotempgroup)
                        {
                            var esotempgroup_ = list.FindAll(p => p.v_Servicio == item.v_Servicio).ToList();

                            ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                            objListaEstadisticostemp.Grupo = item.v_Servicio == "HOSPITALIZACIÓN" ? "HOSPITALARIO / SALA DE OPERACIONES" : item.v_Servicio == "SALA DE OPERACIONES" ? "AMBULATORIO" : string.Empty;
                            objListaEstadisticostemp.Cantidad = esotempgroup_.Count();
                            List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                            foreach (var item2 in esotempgroup_)
                            {
                                ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                                ListaEstadisticosDetalleObj.v_Paciente = item2.v_Paciente;
                                ListaEstadisticosDetalleObj.v_Servicio = item2.v_Servicio;
                                ListaEstadisticosDetalleObj.d_FechaIngreso = item2.d_FechaIngreso.ToString();
                                ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                                ListaEstadisticosDetalleObj.v_Cie10 = item2.v_Cie10;
                                ListaEstadisticosDetalleObj.v_Diagnostico = item2.v_Diagnostico;
                                ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                            }
                            objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;
                            ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                        }

                        if (checkAscDescHospSop.Checked == true)
                        {
                            ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                        }
                        else
                        {
                            ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                        }
                        int contador = 1;

                        foreach (var item in ListaEstadisticostemp_)
                        {
                            ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                            objListaEstadisticostemp.Id = contador;
                            objListaEstadisticostemp.Grupo = item.Grupo;
                            objListaEstadisticostemp.Cantidad = item.Cantidad;
                             objListaEstadisticostemp.Detalle = item.Detalle;

                            ListaEstadisticostemp.Add(objListaEstadisticostemp);

                            PorESO_Tipe.Add(contador.ToString());
                            PorESO_Cant.Add(item.Cantidad);

                            contador++;
                        }

                        grdRESUMENHospSop.DataSource = ListaEstadisticostemp;
                        chartGraficoGlobalHospSop.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR SERVICIO DEL " + dtpInHospSop.Value.ToShortDateString() + " AL " + dtpEndHospSop.Value.ToShortDateString();

                        chartGraficoGlobalHospSop.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                        btnDescargarCurvas.Enabled = true;

                    }
                    else
                    {
                        ListaEstadisticostemp = new List<ListaEstadisticos>();

                        List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                        foreach (var item in esotempgroup)
                        {
                            var generotempgroup_ = list.FindAll(p => p.v_Servicio == item.v_Servicio).ToList();

                            ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                            objListaEstadisticostemp.Grupo = item.v_Servicio == "HOSPITALIZACIÓN" ? "HOSPITALARIO / SALA DE OPERACIONES" : item.v_Servicio == "SALA DE OPERACIONES" ? "AMBULATORIO" : string.Empty;
                            objListaEstadisticostemp.Cantidad = generotempgroup_.Count();
                            List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                            foreach (var item2 in generotempgroup_)
                            {
                                ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                                ListaEstadisticosDetalleObj.v_Paciente = item2.v_Paciente;
                                ListaEstadisticosDetalleObj.v_Servicio = item2.v_Servicio;
                                ListaEstadisticosDetalleObj.d_FechaIngreso = item2.d_FechaIngreso.ToString();
                                ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                                ListaEstadisticosDetalleObj.v_Cie10 = item2.v_Cie10;
                                ListaEstadisticosDetalleObj.v_Diagnostico = item2.v_Diagnostico;
                                ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                            }
                            objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;

                            ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                        }

                        if (checkAscDescHospSop.Checked == true)
                        {
                            ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                        }
                        else
                        {
                            ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                        }
                        int contador = 1;
                        int cantidadMostrar = 0;
                        if (cboCantidadMostrarHospSop.SelectedIndex == 1)
                        {
                            cantidadMostrar = 5;
                        }
                        else if (cboCantidadMostrarHospSop.SelectedIndex == 2)
                        {
                            cantidadMostrar = 10;
                        }
                        else if (cboCantidadMostrarHospSop.SelectedIndex == 3)
                        {
                            cantidadMostrar = 15;
                        }
                        else if (cboCantidadMostrarHospSop.SelectedIndex == 4)
                        {
                            cantidadMostrar = 20;
                        }
                        else if (cboCantidadMostrarHospSop.SelectedIndex == 5)
                        {
                            cantidadMostrar = 25;
                        }


                        foreach (var item in ListaEstadisticostemp_)
                        {
                            ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                            objListaEstadisticostemp.Id = contador;
                            objListaEstadisticostemp.Grupo = item.Grupo;
                            objListaEstadisticostemp.Cantidad = item.Cantidad;
                            objListaEstadisticostemp.Detalle = item.Detalle;

                            ListaEstadisticostemp.Add(objListaEstadisticostemp);

                            PorESO_Tipe.Add(contador.ToString());
                            PorESO_Cant.Add(item.Cantidad);

                            if (cantidadMostrar == contador)
                            {
                                break;
                            }
                            contador++;
                        }


                        grdRESUMENHospSop.DataSource = ListaEstadisticostemp;
                        chartGraficoGlobalHospSop.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR SERVICIO DEL " + dtpInHospSop.Value.ToShortDateString() + " AL " + dtpEndHospSop.Value.ToShortDateString();

                        chartGraficoGlobalHospSop.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);

                        btnDescargarCurvas.Enabled = true;
                    }
                }
                
            }
            else if (checkMedicoServicio.Checked == true)
            {
                grdDataSopHosp.DisplayLayout.Bands[0].Columns["TipoHosp"].Hidden = false;

                chartGraficoGlobalHospSop.Visible = false;
                chartPastelGeneroHospSop.Visible = false;
                chartServicioMedico.Visible = true;

                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();
                PorESO_Cant2 = new ArrayList(); 

                var esotempgroup = list.GroupBy(p => p.v_MedicoTratante).Select(s => s.First()).ToList();
                if (cboCantidadMostrarHospSop.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var gruposervicio_ = list.FindAll(p => p.v_MedicoTratante == item.v_MedicoTratante).ToList();

                        var gruposervicionew_ = gruposervicio_.GroupBy(p => p.v_Servicio).Select(s => s.First()).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Grupo = item.v_MedicoTratante;
                        int servicioHospitalización = 0;
                        int servicioSop = 0;
                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();

                        foreach (var item2 in gruposervicionew_)
                        {
                            var gruposervicioend_ = gruposervicio_.FindAll(p => p.v_Servicio == item2.v_Servicio).ToList();

                            if (item2.v_Servicio == "SALA DE OPERACIONES")
                            {
                                servicioHospitalización = gruposervicioend_.Count();
                                foreach (var item3 in gruposervicioend_)
                                {
                                    ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                                    ListaEstadisticosDetalleObj.v_Paciente = item3.v_Paciente;
                                    ListaEstadisticosDetalleObj.v_Servicio = item3.v_Servicio;
                                    ListaEstadisticosDetalleObj.d_FechaIngreso = item3.d_FechaIngreso.ToString();
                                    ListaEstadisticosDetalleObj.v_MedicoTratante = item3.v_MedicoTratante;
                                    ListaEstadisticosDetalleObj.v_Cie10 = item3.v_Cie10;
                                    ListaEstadisticosDetalleObj.v_Diagnostico = item3.v_Diagnostico;
                                    ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                                }
                            }
                            else if (item2.v_Servicio == "HOSPITALIZACIÓN")
                            {
                                servicioSop = gruposervicioend_.Count();
                                foreach (var item3 in gruposervicioend_)
                                {
                                    ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                                    ListaEstadisticosDetalleObj.v_Paciente = item3.v_Paciente;
                                    ListaEstadisticosDetalleObj.v_Servicio = item3.v_Servicio;
                                    ListaEstadisticosDetalleObj.d_FechaIngreso = item3.d_FechaIngreso.ToString();
                                    ListaEstadisticosDetalleObj.v_MedicoTratante = item3.v_MedicoTratante;
                                    ListaEstadisticosDetalleObj.v_Cie10 = item3.v_Cie10;
                                    ListaEstadisticosDetalleObj.v_Diagnostico = item3.v_Diagnostico;
                                    ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                                }
                            }

                           

                        }
                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;

                        objListaEstadisticostemp.Cantidad = servicioHospitalización;
                        objListaEstadisticostemp.Cantidad2 = servicioSop;
                        

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);


                    }

                    if (checkAscDescHospSop.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;

                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Cantidad2 = item.Cantidad2;
                        objListaEstadisticostemp.Detalle = item.Detalle;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);
                        PorESO_Cant2.Add(item.Cantidad2);

                        contador++;
                    }

                    grdRESUMENHospSop.DataSource = ListaEstadisticostemp;
                    chartServicioMedico.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR MEDICO Y SERVICIO DEL " + dtpInHospSop.Value.ToShortDateString() + " AL " + dtpEndHospSop.Value.ToShortDateString();

                    chartServicioMedico.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    chartServicioMedico.Series[1].Points.DataBindXY(PorESO_Tipe, PorESO_Cant2);

                    btnDescargarCurvas.Enabled = true;

                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticos>();

                    List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();
                    foreach (var item in esotempgroup)
                    {
                        var gruposervicio_ = list.FindAll(p => p.v_MedicoTratante == item.v_MedicoTratante).ToList();

                        var gruposervicionew_ = gruposervicio_.GroupBy(p => p.v_Servicio).Select(s => s.First()).ToList();

                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Grupo = item.v_MedicoTratante;
                        int servicioHospitalización = 0;
                        int servicioSop = 0;
                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();

                        foreach (var item2 in gruposervicionew_)
                        {
                            var gruposervicioend_ = gruposervicio_.FindAll(p => p.v_Servicio == item2.v_Servicio).ToList();

                            if (item2.v_Servicio == "SALA DE OPERACIONES")
                            {
                                servicioHospitalización = gruposervicioend_.Count();
                                foreach (var item3 in gruposervicioend_)
                                {
                                    ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                                    ListaEstadisticosDetalleObj.v_Paciente = item3.v_Paciente;
                                    ListaEstadisticosDetalleObj.v_Servicio = item3.v_Servicio;
                                    ListaEstadisticosDetalleObj.d_FechaIngreso = item3.d_FechaIngreso.ToString();
                                    ListaEstadisticosDetalleObj.v_MedicoTratante = item3.v_MedicoTratante;
                                    ListaEstadisticosDetalleObj.v_Cie10 = item3.v_Cie10;
                                    ListaEstadisticosDetalleObj.v_Diagnostico = item3.v_Diagnostico;
                                    ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                                }
                            }
                            else if (item2.v_Servicio == "HOSPITALIZACIÓN")
                            {
                                servicioSop = gruposervicioend_.Count();
                                foreach (var item3 in gruposervicioend_)
                                {
                                    ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                                    ListaEstadisticosDetalleObj.v_Paciente = item3.v_Paciente;
                                    ListaEstadisticosDetalleObj.v_Servicio = item3.v_Servicio;
                                    ListaEstadisticosDetalleObj.d_FechaIngreso = item3.d_FechaIngreso.ToString();
                                    ListaEstadisticosDetalleObj.v_MedicoTratante = item3.v_MedicoTratante;
                                    ListaEstadisticosDetalleObj.v_Cie10 = item3.v_Cie10;
                                    ListaEstadisticosDetalleObj.v_Diagnostico = item3.v_Diagnostico;
                                    ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                                }
                            }
                           
                        }
                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;

                        objListaEstadisticostemp.Cantidad = servicioHospitalización;
                        objListaEstadisticostemp.Cantidad2 = servicioSop;
                       
                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);


                    }

                    if (checkAscDescHospSop.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;

                    int cantidadMostrar = 0;
                    if (cboCantidadMostrarHospSop.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrarHospSop.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrarHospSop.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrarHospSop.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrarHospSop.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }


                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Cantidad2 = item.Cantidad2;
                        objListaEstadisticostemp.Detalle = item.Detalle;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);
                        PorESO_Cant2.Add(item.Cantidad2);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }
                        contador++;
                    }

                    
                    grdRESUMENHospSop.DataSource = ListaEstadisticostemp;
                    chartServicioMedico.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR MEDICO Y SERVICIO DEL " + dtpInHospSop.Value.ToShortDateString() + " AL " + dtpEndHospSop.Value.ToShortDateString();

                    chartServicioMedico.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    chartServicioMedico.Series[1].Points.DataBindXY(PorESO_Tipe, PorESO_Cant2);

                    btnDescargarCurvas.Enabled = true;
                }
            }
            else
            {
                grdDataSopHosp.DisplayLayout.Bands[0].Columns["TipoHosp"].Hidden = true;

                chartGraficoGlobalHospSop.Visible = false;
                chartPastelGeneroHospSop.Visible = false;
                btnDescargarCurvas.Enabled = false;
                chartServicioMedico.Visible = false;

                ListaEstadisticostemp = new List<ListaEstadisticos>();

                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                TipoGeneroHospSop = new ArrayList();
                CantidadGeneroHospSop = new ArrayList();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            string servicio = "";
            if (rbHospitalizados.Checked == true)
            {
                servicio = "HOSPITALIZADOS";
            }
            else if (rbSOP.Checked == true)
            {
                servicio = "SOP";
            }

            NombreArchivo = "ATENCIONES "+servicio+" DEL " + dtpInHospSop.Text + " al " + dtpEndHospSop.Text;
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdDataSopHosp, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            string nombre = "";
            string servicio = "";
            if (rbHospitalizados.Checked == true)
            {
                servicio = "HOSPITALIZADOS";
            }
            else if (rbSOP.Checked == true)
            {
                servicio = "SOP";
            }

            if (checkMedicoTto.Checked == true)
            {
                nombre = "MEDICO";
            }
            else if (checkGenero.Checked == true)
            {
                nombre = "GENERO";
            }
            else if (checkEspecialidad.Checked == true)
            {
                nombre = "ESPECIALIDAD";
            }

            NombreArchivo = "RESUMEN DE ATENCIONES " + servicio + " POR " + nombre + " CONSOLIDADO DEL" + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text;
            
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdRESUMENHospSop, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GuardarGrafico(Chart graficoGuardar, string nombreGuardara)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Imagen|*.jpg|Bitmap Imagen|*.bmp|PNG Imagen|*.png";
            saveFileDialog1.Title = "Guardar Grafico en Imagen";
            saveFileDialog1.FileName = nombreGuardara.Replace("/", "-");
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        graficoGuardar.SaveImage(fs, ChartImageFormat.Jpeg);
                        MessageBox.Show("Guardada correctamente.");
                        break;
                    case 2:
                        graficoGuardar.SaveImage(fs, ChartImageFormat.Bmp);
                        MessageBox.Show("Guardada correctamente.");
                        break;
                    case 3:
                        graficoGuardar.SaveImage(fs, ChartImageFormat.Png);
                        MessageBox.Show("Guardada correctamente.");
                        break;
                }
                fs.Close();
            }
        }


        private void btnDescargarCurvas_Click(object sender, EventArgs e)
        {
            if (checkMedicoTto.Checked == true)
            {
                GuardarGrafico(chartGraficoGlobalHospSop, nombreparaGraficoHospSop);
            }
            else if (checkGenero.Checked == true)
            {
                GuardarGrafico(chartPastelGeneroHospSop, nombreparaGraficoHospSop);
            }
            else if (checkEspecialidad.Checked == true)
            {
                GuardarGrafico(chartGraficoGlobalHospSop, nombreparaGraficoHospSop);
            }
            else if (checkServicio.Checked == true)
            {
                GuardarGrafico(chartGraficoGlobalHospSop, nombreparaGraficoHospSop);
            }
            else if (checkMedicoServicio.Checked == true)
            {
                GuardarGrafico(chartServicioMedico, nombreparaGraficoHospSop);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            GuardarGrafico(chartGraficoGlobal, nombreparaGraficoGlobal);
        }
        // asistencial
        private void button8_Click(object sender, EventArgs e)
        {
            var usuarioActual = Globals.ClientSession.i_SystemUserId;
            var usuario_data = new ServiceBL().GetSystemUserId(usuarioActual);
            var usuario_professional = new ServiceBL().GetProfessional(usuario_data.v_PersonId);

            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.BindGridAsistencial();
            };
        }

        private void BindGridAsistencial()
        {
            var objData = GetDataAsistencial(0, null, "", "");
            ListaGrillaAsistencial = objData;
            grdDataServiceAsistencial.DataSource = objData;
            #region Ocultar columnas
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_PersonId"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["b_FechaEntrega"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_ServiceStatusName"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["i_ServiceStatusId"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_LocationName"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_ProtocolId"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_ComponentId"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["i_LineStatusId"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_DiagnosticRepositoryId"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_DiseasesName"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["d_ExpirationDateDiagnostic"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["d_ServiceDate"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["TipoServicioMaster"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["TipoServicioESO"].Hidden = true;

            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_Recommendation"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["i_ServiceId"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["i_ServiceTypeId"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_CustomerOrganizationId"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_CustomerLocationId"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["i_MasterServiceId"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["i_AptitudeStatusId"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["i_EsoTypeId"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["i_IsDeleted"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_CreationUser"].Hidden = true;

            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_UpdateUser"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["d_CreationDate"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["d_UpdateDate"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["i_StatusLiquidation"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_MasterServiceName"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_EsoTypeName"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["CIE10"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["d_FechaNacimiento"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["NroPoliza"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["d_FechaEntrega"].Hidden = true;

            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["Moneda"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["NroFactura"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["Valor"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["i_FinalQualificationId"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_Restriccion"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["d_Deducible"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["i_IsDeletedDx"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["LogoEmpresaPropietaria"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["i_IsDeletedRecomendaciones"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["i_IsDeletedRestricciones"].Hidden = true;

            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["i_age"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["UsuarioMedicina"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["item"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["i_ApprovedUpdateUserId"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_Consultorio"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_ConsultorioId"].Hidden = true;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["i_ServiceComponentStatusId"].Hidden = true;

            #endregion

            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["Liq"].Header.VisiblePosition = 0;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_DocNumber"].Header.VisiblePosition = 1;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["Fecha"].Header.VisiblePosition = 2;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_Pacient"].Header.VisiblePosition = 3;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["Edad"].Header.VisiblePosition = 4;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_ServiceStatusName"].Header.VisiblePosition = 5;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_AptitudeStatusName"].Header.VisiblePosition = 6;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["CompMinera"].Header.VisiblePosition = 7;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_OrganizationName"].Header.VisiblePosition = 8;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["Tercero"].Header.VisiblePosition = 9;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_ProtocolName"].Header.VisiblePosition = 10;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_ServiceId"].Header.VisiblePosition = 11;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["TipoServicio"].Header.VisiblePosition = 12;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["UsuarioCrea"].Header.VisiblePosition = 13;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_MedicoTratante"].Header.VisiblePosition = 14;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["d_BirthDate"].Header.VisiblePosition = 15;
            //grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["v_TelephoneNumber"].Header.VisiblePosition = 21;

            lablAsistencial.Text = string.Format("Se encontraron {0} registros.", objData.Count());

        }

        private BindingList<ServiceGridJerarquizadaListNew> GetDataAsistencial(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dateTimePicker2.Value.Date;
            DateTime? pdatEndDate = dateTimePicker1.Value.Date.AddDays(1);

            DateTime? FCI = dateTimePicker2.Value.Date;
            DateTime? FCF = dateTimePicker1.Value.Date.AddDays(1);

            BindingList<ServiceGridJerarquizadaListNew> _objData = new BindingList<ServiceGridJerarquizadaListNew>();

            _objData = _serviceBL.GetServicesPagedAndFilteredEstadisticasNoOcp(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate, null, FCI, FCF, "");

            if (ddlServiceTypeIdAsistencial.SelectedIndex != 0)
            {
                List<ServiceGridJerarquizadaListNew> Data = _objData.Where(p => p.i_ServiceTypeId == Convert.ToInt32(ddlServiceTypeIdAsistencial.SelectedValue)).ToList();
                _objData = new BindingList<ServiceGridJerarquizadaListNew>(Data);
            }
            if (ddlMasterServiceIdAsistencial.SelectedIndex != 0)
            {
                List<ServiceGridJerarquizadaListNew> Data = _objData.Where(p => p.i_MasterServiceId == Convert.ToInt32(ddlMasterServiceIdAsistencial.SelectedValue)).ToList();
                _objData = new BindingList<ServiceGridJerarquizadaListNew>(Data);
            }
            if (ddlEsoTypeAsistencial.SelectedIndex != 0)
            {
                List<ServiceGridJerarquizadaListNew> Data = _objData.Where(p => p.i_EsoTypeId == Convert.ToInt32(ddlEsoTypeAsistencial.SelectedValue)).ToList();
                _objData = new BindingList<ServiceGridJerarquizadaListNew>(Data);
            }

            if (ddlConsultorioAsistencial.SelectedIndex != 0)
            {
                List<ServiceGridJerarquizadaListNew> Data = new List<ServiceGridJerarquizadaListNew>();


                foreach (var item in _objData)
                {
                    bool result = ObtenerConsultorio(item.v_ServiceId, ddlConsultorioAsistencial.Text);
                    if (result)
                    {
                        Data.Add(item);
                    }
                }


                _objData = new BindingList<ServiceGridJerarquizadaListNew>(Data);
            }

            return _objData;

        }

        private void ddlServiceTypeIdAsistencial_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (ddlServiceTypeIdAsistencial.SelectedValue.ToString() == "-1")
            {
                ddlMasterServiceIdAsistencial.SelectedValue = "-1";
                ddlMasterServiceIdAsistencial.Enabled = false;
                ddlConsultorioAsistencial.Enabled = false;
                Utils.LoadDropDownList(ddlConsultorioAsistencial, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 119, null, 0), DropDownListAction.All);

                return;
            }
            else if (ddlServiceTypeIdAsistencial.SelectedValue.ToString() == "1")
            {
                ddlMasterServiceIdAsistencial.Enabled = true;
                ddlConsultorioAsistencial.Enabled = true;
                Utils.LoadDropDownList(ddlConsultorioAsistencial, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 119, null, 40), DropDownListAction.All);
            }
            else if (ddlServiceTypeIdAsistencial.SelectedValue.ToString() == "9")
            {
                ddlMasterServiceIdAsistencial.Enabled = true;
                ddlConsultorioAsistencial.Enabled = true;
                Utils.LoadDropDownList(ddlConsultorioAsistencial, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 119, null, 41), DropDownListAction.All);
                Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 116, null, 41), DropDownListAction.All);
            }
            else if (ddlServiceTypeIdAsistencial.SelectedValue.ToString() == "11")
            {
                ddlMasterServiceIdAsistencial.Enabled = true;
                ddlConsultorioAsistencial.Enabled = true;
                Utils.LoadDropDownList(ddlConsultorioAsistencial, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 119, null, 41), DropDownListAction.All);
            }
            else
            {
                ddlMasterServiceIdAsistencial.Enabled = true;
                ddlConsultorioAsistencial.Enabled = true;

                ddlMasterServiceIdAsistencial.Enabled = true;
                ddlConsultorioAsistencial.Enabled = true;
                Utils.LoadDropDownList(ddlConsultorioAsistencial, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 119, null, 0), DropDownListAction.All);

            }
        }

        private void ddlServiceTypeId_SelectedValueChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (ddlServiceTypeId.SelectedValue == "-1")
            {
                ddlMasterServiceId.SelectedValue = "-1";
                ddlMasterServiceId.Enabled = false;
                ddlConsultorio.Enabled = false;
                Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 116, null, 0), DropDownListAction.All);

                return;
            }
            else if ( ddlServiceTypeId.SelectedValue != null &&   int.Parse(ddlServiceTypeId.SelectedValue.ToString()) == 1)
            {
                if (ddlServiceTypeId.SelectedIndex == 0 || ddlServiceTypeId.SelectedIndex == -1)
                    return;

                var id = int.Parse(ddlServiceTypeId.SelectedValue.ToString());
                Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, id, null), DropDownListAction.Select);


                ddlMasterServiceId.Enabled = true;
                ddlConsultorio.Enabled = true;
                Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 116, null, 40), DropDownListAction.All);


            }
            else if (ddlServiceTypeId.SelectedValue == "9")
            {
                ddlMasterServiceId.Enabled = true;
                ddlConsultorio.Enabled = true;
                Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo_Consultorio(ref objOperationResult, 116, null, 41), DropDownListAction.All);

                ddlMasterServiceId.SelectedValue = "9";

            }
            else if (ddlServiceTypeId.SelectedValue == "11")
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

        //private void grdRESUMEN_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        //{
        //    List<ServiceGridJerarquizadaList> list = ListaGrilla.ToList();
        //    List<ListaEstadisticos> ListaEstadisticostemp = new List<ListaEstadisticos>();

        //    if (checkMinayContrataESO.Checked == true)
        //    {
        //        PorESO_Tipe = new ArrayList();
        //        PorESO_Cant = new ArrayList();

        //        bool activador = false;

        //        string grupoMina = grdRESUMEN.Selected.Rows[0].Cells["Grupo"].Value.ToString();

        //        ListaEstadisticostemp = new List<ListaEstadisticos>();

        //        List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();

        //        var esotempgroup_ = list.FindAll(p => p.CompMinera == grupoMina).ToList();

        //        var esotempgroup2 = esotempgroup_.GroupBy(p => p.v_OrganizationName).Select(s => s.First()).ToList();

        //        List<ListaEstadisticos> ListaEstadisticostempHios_ = new List<ListaEstadisticos>();

        //        foreach (var item2 in esotempgroup2)
        //        {
        //            var contar = esotempgroup_.FindAll(p => p.v_OrganizationName == item2.v_OrganizationName).ToList();

        //            ListaEstadisticos objListaEstadisticostemp_ = new ListaEstadisticos();
        //            objListaEstadisticostemp_.Grupo = item2.v_OrganizationName;
        //            objListaEstadisticostemp_.Cantidad = contar.Count();

        //            ListaEstadisticostempHios_.Add(objListaEstadisticostemp_);
        //        }


        //        ListaEstadisticostempHios_ = ListaEstadisticostempHios_.OrderByDescending(p => p.Cantidad).ToList();

        //        int contador = 1;


        //        foreach (var item in ListaEstadisticostempHios_)
        //        {
        //            ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

        //            objListaEstadisticostemp.Id = contador;
        //            objListaEstadisticostemp.Grupo = item.Grupo;
        //            objListaEstadisticostemp.Cantidad = item.Cantidad;

        //            ListaEstadisticostemp.Add(objListaEstadisticostemp);

        //            PorESO_Tipe.Add(contador.ToString());
        //            PorESO_Cant.Add(item.Cantidad);
        //            contador++;
        //        }

        //        chartGraficoGlobal.Titles[0].Text = nombreparaGraficoGlobal = "AGRUPADO POR " + grupoMina + " Y CONTRATAS DEL " + dtpDateTimeStar.Value.ToShortDateString() + " AL " + dptDateTimeEnd.Value.ToShortDateString();

        //        chartGraficoGlobal.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
        //        button6.Enabled = true;

        //    }
        //}

        private void grdRESUMEN_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (checkMinayContrataESO.Checked == true)
            {
                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();
                
                bool activador = false;
                foreach (UltraGridRow rowSelected in this.grdRESUMEN.Selected.Rows)
                {
                    if (rowSelected.Band.Index.ToString() == "0")
                    {
                        if (grdRESUMEN.Selected.Rows[0].Cells["Grupo"].Value != null)
                        {
                            List<ListaEstadisticos> ListaEstadisticosTemporal = new List<ListaEstadisticos>();

                            ListaEstadisticosTemporal = ListaEstadisticostemp;

                            string grupoMina = grdRESUMEN.Selected.Rows[0].Cells["Grupo"].Value.ToString();

                            var esotempgroup_ = ListaEstadisticosTemporal.FindAll(p => p.Grupo == grupoMina);
                            List<ListaEstadisticos> listaGrafico = new List<ListaEstadisticos>();
                            foreach (var item in esotempgroup_)
                            {
                                foreach (var item2 in item.DetalleGrupos)
                                {
                                    ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                                    objListaEstadisticostemp.Id = item2.Id;
                                    objListaEstadisticostemp.Grupo = item2.GrupoC;
                                    objListaEstadisticostemp.Cantidad = item2.Cantidad;

                                    listaGrafico.Add(objListaEstadisticostemp);

                                    PorESO_Tipe.Add(item2.Id.ToString());
                                    PorESO_Cant.Add(item2.Cantidad);
                                }
                            }

                            chartGraficoGlobal.Titles[0].Text = nombreparaGraficoGlobal = "AGRUPADO POR " + grupoMina + " Y CONTRATAS DEL " + dtpDateTimeStar.Value.ToShortDateString() + " AL " + dptDateTimeEnd.Value.ToShortDateString();

                            chartGraficoGlobal.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                            button6.Enabled = true;

                            activador = true;
                            #region MyRegion
                            //////////////////
                            //string grupoMina = grdRESUMEN.Selected.Rows[0].Cells["Grupo"].Value.ToString();

                            //ListaEstadisticostemp = new List<ListaEstadisticos>();

                            //List<ListaEstadisticos> ListaEstadisticostemp_ = new List<ListaEstadisticos>();

                            //var esotempgroup_ = list.FindAll(p => p.CompMinera == grupoMina).ToList();

                            //var esotempgroup2 = esotempgroup_.GroupBy(p => p.v_OrganizationName).Select(s => s.First()).ToList();

                            //List<ListaEstadisticos> ListaEstadisticostempHios_ = new List<ListaEstadisticos>();

                            //foreach (var item2 in esotempgroup2)
                            //{
                            //    var contar = esotempgroup_.FindAll(p => p.v_OrganizationName == item2.v_OrganizationName).ToList();

                            //    ListaEstadisticos objListaEstadisticostemp_ = new ListaEstadisticos();
                            //    objListaEstadisticostemp_.Grupo = item2.v_OrganizationName;
                            //    objListaEstadisticostemp_.Cantidad = contar.Count();

                            //    ListaEstadisticostempHios_.Add(objListaEstadisticostemp_);
                            //}


                            //ListaEstadisticostempHios_ = ListaEstadisticostempHios_.OrderByDescending(p => p.Cantidad).ToList();

                            //int contador = 1;


                            //foreach (var item in ListaEstadisticostempHios_)
                            //{
                            //    ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                            //    objListaEstadisticostemp.Id = contador;
                            //    objListaEstadisticostemp.Grupo = item.Grupo;
                            //    objListaEstadisticostemp.Cantidad = item.Cantidad;

                            //    ListaEstadisticostemp.Add(objListaEstadisticostemp);

                            //    PorESO_Tipe.Add(contador.ToString());
                            //    PorESO_Cant.Add(item.Cantidad);
                            //    contador++;
                            //}

                            //chartGraficoGlobal.Titles[0].Text = nombreparaGraficoGlobal = "AGRUPADO POR " + grupoMina + " Y CONTRATAS DEL " + dtpDateTimeStar.Value.ToShortDateString() + " AL " + dptDateTimeEnd.Value.ToShortDateString();

                            //chartGraficoGlobal.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                            //button6.Enabled = true;
                            #endregion
                            
                        }
                    }
                    else if (rowSelected.Band.Index.ToString() == "2")
                    {
                        if (grdRESUMEN.Selected.Rows[0].Cells["CompMinera"].Value != null)
                        {
                            List<ListaEstadisticos> ListaEstadisticosTemporal = new List<ListaEstadisticos>();

                            ListaEstadisticosTemporal = ListaEstadisticostemp;

                            string grupoMina = grdRESUMEN.Selected.Rows[0].Cells["CompMinera"].Value.ToString();
                            string grupoContrata = grdRESUMEN.Selected.Rows[0].Cells["GrupoC"].Value.ToString();


                            var esotempgroup_ = ListaEstadisticosTemporal.FindAll(p => p.Grupo == grupoMina);
                            List<ListaEstadisticos> listaGrafico = new List<ListaEstadisticos>();
                            foreach (var item in esotempgroup_)
                            {
                                var esotempgroupcontrata_ = item.DetalleGrupos.FindAll(p => p.GrupoC == grupoContrata);

                                foreach (var item2 in esotempgroupcontrata_)
                                {
                                    foreach (var item3 in item2.DetalleESO)
                                    {
                                        ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                                        objListaEstadisticostemp.Id = item3.Id;
                                        objListaEstadisticostemp.Grupo = item3.Grupo;
                                        objListaEstadisticostemp.Cantidad = item3.Cantidad;

                                        listaGrafico.Add(objListaEstadisticostemp);

                                        PorESO_Tipe.Add(item3.Grupo);
                                        PorESO_Cant.Add(item3.Cantidad);
                                    } 
                                }
                            }

                            chartGraficoGlobal.Titles[0].Text = nombreparaGraficoGlobal =
                                "AGRUPADO POR " + grupoMina + " - CONTRATA " + grupoContrata + " - ESO DEL " +
                                dtpDateTimeStar.Value.ToShortDateString() + " AL " +
                                dptDateTimeEnd.Value.ToShortDateString();

                            chartGraficoGlobal.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                            button6.Enabled = true;

                            activador = true;
                        }
                    }
                }
            }
            else if (checkMinayContrata.Checked == true)
            {
                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                bool activador = false;
                foreach (UltraGridRow rowSelected in this.grdRESUMEN.Selected.Rows)
                {
                    if (rowSelected.Band.Index.ToString() == "0")
                    {
                        if (grdRESUMEN.Selected.Rows[0].Cells["Grupo"].Value != null)
                        {
                            List<ListaEstadisticos> ListaEstadisticosTemporal = new List<ListaEstadisticos>();

                            ListaEstadisticosTemporal = ListaEstadisticostemp;

                            string grupoMina = grdRESUMEN.Selected.Rows[0].Cells["Grupo"].Value.ToString();

                            var esotempgroup_ = ListaEstadisticosTemporal.FindAll(p => p.Grupo == grupoMina);
                            List<ListaEstadisticos> listaGrafico = new List<ListaEstadisticos>();
                            foreach (var item in esotempgroup_)
                            {
                                foreach (var item2 in item.DetalleGrupos)
                                {
                                    ListaEstadisticos objListaEstadisticostemp = new ListaEstadisticos();

                                    objListaEstadisticostemp.Id = item2.Id;
                                    objListaEstadisticostemp.Grupo = item2.GrupoC;
                                    objListaEstadisticostemp.Cantidad = item2.Cantidad;

                                    listaGrafico.Add(objListaEstadisticostemp);

                                    PorESO_Tipe.Add(item2.Id.ToString());
                                    PorESO_Cant.Add(item2.Cantidad);
                                }
                            }

                            chartGraficoGlobal.Titles[0].Text = nombreparaGraficoGlobal = "AGRUPADO POR " + grupoMina + " Y CONTRATAS DEL " + dtpDateTimeStar.Value.ToShortDateString() + " AL " + dptDateTimeEnd.Value.ToShortDateString();

                            chartGraficoGlobal.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                            button6.Enabled = true;

                            activador = true;
                        }
                    }
                }
            }
        }

        private void ultraExpandableGroupBox1_ExpandedStateChanging(object sender, CancelEventArgs e)
        {

        }

        private void ddlServiceTypeIdAsistencial_TextChanged(object sender, EventArgs e)
        {
            if (ddlServiceTypeIdAsistencial.SelectedIndex == 0 || ddlServiceTypeIdAsistencial.SelectedIndex == -1)
                return;

            OperationResult objOperationResult = new OperationResult();
            var id = int.Parse(ddlServiceTypeIdAsistencial.SelectedValue.ToString());
            Utils.LoadDropDownList(ddlMasterServiceIdAsistencial, "Value1", "Id", BLL.Utils.GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, id, null), DropDownListAction.Select);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            List<ServiceGridJerarquizadaListNew> list = ListaGrillaAsistencial.ToList();
            List<ListaEstadisticosAsist> ListaEstadisticostemp = new List<ListaEstadisticosAsist>();
            if (checkEdad.Checked == true)
            {
                grdRESUMENAsist.DisplayLayout.Bands[0].Columns["Cantidad2"].Hidden = true;

                chartAsistencial.Visible = true;
                chartComparativoASIST.Visible = false;

                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.Edad).Select(s => s.First()).ToList();
                if (cboCantidadMostrarAsistencial.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.Edad == item.Edad).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.Edad.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            
                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }

                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }

                    int contador = 1;

                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;
                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        contador++;
                    }

                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop =
                        "AGRUPADO POR EDAD DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " +
                        dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.Edad == item.Edad).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.Edad.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }

                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;


                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }

                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrarAsistencial.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }


                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }

                        contador++;
                    }

                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop =
                        "AGRUPADO POR EDAD DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " +
                        dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
            }
            else if (checkProtocoloAsist.Checked == true)
            {
                grdRESUMENAsist.DisplayLayout.Bands[0].Columns["Cantidad2"].Hidden = true;

                chartAsistencial.Visible = true;
                chartComparativoASIST.Visible = false;

                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.v_ProtocolName).Select(s => s.First()).ToList();
                if (cboCantidadMostrarAsistencial.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.v_ProtocolName == item.v_ProtocolName).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.v_ProtocolName.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }
                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;

                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;
                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        contador++;
                    }

                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR PROTOCOLO DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " + dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.v_ProtocolName == item.v_ProtocolName).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.v_ProtocolName.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }

                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;


                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrarAsistencial.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }


                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }
                        contador++;
                    }

                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR PROTOCOLO DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " + dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
            }
            else if (chkConsultorio.Checked == true)
            {
                grdRESUMENAsist.DisplayLayout.Bands[0].Columns["Cantidad2"].Hidden = true;

                chartAsistencial.Visible = true;
                chartComparativoASIST.Visible = false;

                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.v_Consultorio).Select(s => s.First()).ToList();
                if (cboCantidadMostrarAsistencial.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.v_Consultorio == item.v_Consultorio).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.v_Consultorio.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }
                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;

                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;
                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        contador++;
                    }

                    ListaEstadisticosConsultorioMdico = ListaEstadisticostemp;

                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR CONSULTORIO DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " + dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.v_Consultorio == item.v_Consultorio).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.v_Consultorio.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }

                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;


                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrarAsistencial.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }


                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }
                        contador++;
                    }

                    ListaEstadisticosConsultorioMdico = ListaEstadisticostemp;
                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR CONSULTORIO DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " + dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
            }
            else if (checkMedicoAsist.Checked == true)
            {
                grdRESUMENAsist.DisplayLayout.Bands[0].Columns["Cantidad2"].Hidden = true;

                chartAsistencial.Visible = true;
                chartComparativoASIST.Visible = false;

                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.v_MedicoTratante).Select(s => s.First()).ToList();
                if (cboCantidadMostrarAsistencial.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.v_MedicoTratante == item.v_MedicoTratante).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.v_MedicoTratante.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }
                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;

                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;
                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        contador++;
                    }

                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR MEDICO TRATANTE DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " + dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.v_MedicoTratante == item.v_MedicoTratante).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.v_MedicoTratante.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }

                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;


                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrarAsistencial.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }


                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }
                        contador++;
                    }

                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR MEDICO TRATANTE DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " + dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
            }
            else if (checkGrupoEtareoAsist.Checked == true)
            {
                ArrayList Etareo_Tipe = new ArrayList();
                ArrayList Etareo_Cant = new ArrayList();

                grdDataServiceAsistencial.DisplayLayout.Bands[0].Columns["Edad"].Hidden = true;

                chartAsistencial.Visible = true;
                chartComparativoASIST.Visible = false;

                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                int niño_c = 0;
                int adolescente_c = 0;
                int joven_c = 0;
                int adulto_c = 0;
                int adulto_mayor_c = 0;
                ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                foreach (var item in ListaGrillaAsistencial)
                {
                    if (item.Edad >= 0 && item.Edad <= 11) //niño
                    {
                        niño_c++;
                    }
                    else if (item.Edad >= 12 && item.Edad <= 17) //adolescente
                    {
                        adolescente_c++;
                    }
                    else if (item.Edad >= 18 && item.Edad <= 29) //joven
                    {
                        joven_c++;
                    }
                    else if (item.Edad >= 30 && item.Edad <= 59) //adulto
                    {
                        adulto_c++;
                    }
                    else if (item.Edad >= 60) //adulto mayor
                    {
                        adulto_mayor_c++;
                    }
                }

                ListaEstadisticosAsist objListaEstadisticosTMP1 = new ListaEstadisticosAsist();
                objListaEstadisticosTMP1.Grupo = "0-11";
                objListaEstadisticosTMP1.Cantidad = niño_c;
                ListaEstadisticostemp_.Add(objListaEstadisticosTMP1);

                ListaEstadisticosAsist objListaEstadisticosTMP2 = new ListaEstadisticosAsist();
                objListaEstadisticosTMP2.Grupo = "12-17";
                objListaEstadisticosTMP2.Cantidad = adolescente_c;
                ListaEstadisticostemp_.Add(objListaEstadisticosTMP2);

                ListaEstadisticosAsist objListaEstadisticosTMP3 = new ListaEstadisticosAsist();
                objListaEstadisticosTMP3.Grupo = "18-29";
                objListaEstadisticosTMP3.Cantidad = joven_c;
                ListaEstadisticostemp_.Add(objListaEstadisticosTMP3);

                ListaEstadisticosAsist objListaEstadisticosTMP4 = new ListaEstadisticosAsist();
                objListaEstadisticosTMP4.Grupo = "30-59";
                objListaEstadisticosTMP4.Cantidad = adulto_c;
                ListaEstadisticostemp_.Add(objListaEstadisticosTMP4);

                ListaEstadisticosAsist objListaEstadisticosTMP5 = new ListaEstadisticosAsist();
                objListaEstadisticosTMP5.Grupo = "Mayores 60";
                objListaEstadisticosTMP5.Cantidad = adulto_mayor_c;
                ListaEstadisticostemp_.Add(objListaEstadisticosTMP5);

                int contador = 1;

                foreach (var item in ListaEstadisticostemp_)
                {
                    ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                    objListaEstadisticostemp.Id = contador;
                    objListaEstadisticostemp.Grupo = item.Grupo;
                    objListaEstadisticostemp.Cantidad = item.Cantidad;
                    objListaEstadisticostemp.Detalle = item.Detalle;
                    ListaEstadisticostemp.Add(objListaEstadisticostemp);

                    PorESO_Tipe.Add(contador.ToString());
                    PorESO_Cant.Add(item.Cantidad);

                    contador++;
                }

                grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop =
                    "AGRUPADO POR GRUPO ETAREO DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " +
                    dateTimePicker1.Value.ToShortDateString();

                chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                btnDescargarCurvasAsistencial.Enabled = true;
            }
            else if (checkGeneroAsist.Checked == true)
            {
                grdRESUMENAsist.DisplayLayout.Bands[0].Columns["Cantidad2"].Hidden = true;

                chartAsistencial.Visible = true;
                chartComparativoASIST.Visible = false;

                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.Value1).Select(s => s.First()).ToList();

                ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                foreach (var item in esotempgroup)
                {
                    var esotempgroup_ = list.FindAll(p => p.Value1 == item.Value1).ToList();

                    ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                    objListaEstadisticostemp.Grupo = item.Value1.ToString();
                    objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                    List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                    foreach (var item2 in esotempgroup_)
                    {
                        ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                        ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                        ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                        ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                        ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                        ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                        ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                        ListaEstadisticosDetalleObj.Mont = item2.Value2;
                        ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                        ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                        ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                    }
                    objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;

                    ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                }


                int contador = 1;

                foreach (var item in ListaEstadisticostemp_)
                {
                    ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                    objListaEstadisticostemp.Id = contador;
                    objListaEstadisticostemp.Grupo = item.Grupo;
                    objListaEstadisticostemp.Cantidad = item.Cantidad;
                    objListaEstadisticostemp.Detalle = item.Detalle;
                    ListaEstadisticostemp.Add(objListaEstadisticostemp);

                    PorESO_Tipe.Add(contador.ToString());
                    PorESO_Cant.Add(item.Cantidad);

                    contador++;
                }

                grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR GENERO DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " + dateTimePicker1.Value.ToShortDateString();

                chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                btnDescargarCurvasAsistencial.Enabled = true;
            }
            else if (checkServicioAsist.Checked == true)
            {
                grdRESUMENAsist.DisplayLayout.Bands[0].Columns["Cantidad2"].Hidden = true;

                chartAsistencial.Visible = true;
                chartComparativoASIST.Visible = false;

                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.TipoServicioMaster).Select(s => s.First()).ToList();
                if (cboCantidadMostrarAsistencial.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.TipoServicioMaster == item.TipoServicioMaster).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.TipoServicioMaster.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }

                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }

                    int contador = 1;

                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;
                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        contador++;
                    }

                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop =
                        "AGRUPADO POR SERVICIO DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " +
                        dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.TipoServicioMaster == item.TipoServicioMaster).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.TipoServicioMaster.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }

                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;


                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }

                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrarAsistencial.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }


                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }

                        contador++;
                    }

                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop =
                        "AGRUPADO POR SERVICIO DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " +
                        dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
            }

            else if (checkDx.Checked == true)
            {
                grdRESUMENAsist.DisplayLayout.Bands[0].Columns["Cantidad2"].Hidden = true;

                chartAsistencial.Visible = true;
                chartComparativoASIST.Visible = false;

                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.Diagnostico).Select(s => s.First()).ToList();
                if (cboCantidadMostrarAsistencial.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.Diagnostico == item.Diagnostico).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.Diagnostico.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }

                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }

                    int contador = 1;

                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;
                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        contador++;
                    }

                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop =
                        "AGRUPADO POR DIAGNOSTICO DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " +
                        dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.Diagnostico == item.Diagnostico).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.Diagnostico.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }

                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;


                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }

                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrarAsistencial.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }


                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }

                        contador++;
                    }

                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop =
                        "AGRUPADO POR DIAGNOSTICO DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " +
                        dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
            }
            else if (chkMkt.Checked == true)
            {
                grdRESUMENAsist.DisplayLayout.Bands[0].Columns["Cantidad2"].Hidden = true;

                chartAsistencial.Visible = true;
                chartComparativoASIST.Visible = false;

                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.Val_Mkt).Select(s => s.First()).ToList();
                if (cboCantidadMostrarAsistencial.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.Val_Mkt == item.Val_Mkt).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.Val_Mkt.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }
                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;

                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;
                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        contador++;
                    }

                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR MKT DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " + dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.Val_Mkt == item.Val_Mkt).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.Val_Mkt.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }

                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;


                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrarAsistencial.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }


                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }
                        contador++;
                    }

                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR MKT DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " + dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
            }
            else if (chkMedicoRef.Checked == true)
            {
                grdRESUMENAsist.DisplayLayout.Bands[0].Columns["Cantidad2"].Hidden = true;

                chartAsistencial.Visible = true;
                chartComparativoASIST.Visible = false;

                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.Val_Med_Sol_Ext).Select(s => s.First()).ToList();
                if (cboCantidadMostrarAsistencial.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.Val_Med_Sol_Ext == item.Val_Med_Sol_Ext).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.Val_Med_Sol_Ext.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }
                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;

                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;
                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        contador++;
                    }

                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR MEDICO REFIERE DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " + dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.Val_Med_Sol_Ext == item.Val_Med_Sol_Ext).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.Val_Med_Sol_Ext.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }

                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;


                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrarAsistencial.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }


                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }
                        contador++;
                    }

                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR MEDICO REFIERE DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " + dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
            }
            else if (chkEstablecimiento.Checked == true)
            {
                grdRESUMENAsist.DisplayLayout.Bands[0].Columns["Cantidad2"].Hidden = true;

                chartAsistencial.Visible = true;
                chartComparativoASIST.Visible = false;

                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.Val_Establecimiento).Select(s => s.First()).ToList();
                if (cboCantidadMostrarAsistencial.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.Val_Establecimiento == item.Val_Establecimiento).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.Val_Establecimiento.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }
                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;

                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;
                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        contador++;
                    }

                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR MEDICO REFIERE DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " + dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.Val_Establecimiento == item.Val_Establecimiento).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.Val_Establecimiento.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }

                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;


                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrarAsistencial.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }


                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }
                        contador++;
                    }

                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR MEDICO REFIERE DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " + dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
            }
            else if (chkEspMedica.Checked == true)
            {
                grdRESUMENAsist.DisplayLayout.Bands[0].Columns["Cantidad2"].Hidden = true;

                chartAsistencial.Visible = true;
                chartComparativoASIST.Visible = false;

                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var esotempgroup = list.GroupBy(p => p.ESPECIALIDAD_MEDICA).Select(s => s.First()).ToList();
                if (cboCantidadMostrarAsistencial.SelectedValue == "-1")
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.ESPECIALIDAD_MEDICA == item.ESPECIALIDAD_MEDICA).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.ESPECIALIDAD_MEDICA.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }
                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;

                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;

                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;
                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        contador++;
                    }

                    ListaEstadisticosConsultorioMdico = ListaEstadisticostemp;

                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR ESPECIALIDAD MÉDICA DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " + dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
                else
                {
                    ListaEstadisticostemp = new List<ListaEstadisticosAsist>();

                    List<ListaEstadisticosAsist> ListaEstadisticostemp_ = new List<ListaEstadisticosAsist>();
                    foreach (var item in esotempgroup)
                    {
                        var esotempgroup_ = list.FindAll(p => p.ESPECIALIDAD_MEDICA == item.ESPECIALIDAD_MEDICA).ToList();

                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Grupo = item.ESPECIALIDAD_MEDICA.ToString();
                        objListaEstadisticostemp.Cantidad = esotempgroup_.Count();

                        List<ListaEstadisticosDetalle> ListaEstadisticosDetalle_ = new List<ListaEstadisticosDetalle>();
                        foreach (var item2 in esotempgroup_)
                        {
                            ListaEstadisticosDetalle ListaEstadisticosDetalleObj = new ListaEstadisticosDetalle();
                            ListaEstadisticosDetalleObj.v_Paciente = item2.v_Pacient;
                            ListaEstadisticosDetalleObj.v_Servicio = item2.v_ServiceId;
                            ListaEstadisticosDetalleObj.d_FechaIngreso = item2.Fecha.ToString();
                            ListaEstadisticosDetalleObj.v_MedicoTratante = item2.v_MedicoTratante;
                            ListaEstadisticosDetalleObj.v_Cie10 = item2.CIE_10;
                            ListaEstadisticosDetalleObj.v_Diagnostico = item2.Diagnostico;
                            ListaEstadisticosDetalleObj.Mont = item2.Value2;
                            ListaEstadisticosDetalleObj.Compr = item2.ComprobanteServicio;
                            ListaEstadisticosDetalleObj.v_ProtocolName = item2.v_ProtocolName;

                            ListaEstadisticosDetalle_.Add(ListaEstadisticosDetalleObj);
                        }

                        objListaEstadisticostemp.Detalle = ListaEstadisticosDetalle_;


                        ListaEstadisticostemp_.Add(objListaEstadisticostemp);

                    }

                    if (checkAscDescAsistencial.Checked == true)
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderBy(p => p.Cantidad).ToList();
                    }
                    else
                    {
                        ListaEstadisticostemp_ = ListaEstadisticostemp_.OrderByDescending(p => p.Cantidad).ToList();

                    }
                    int contador = 1;
                    int cantidadMostrar = 0;
                    if (cboCantidadMostrarAsistencial.SelectedIndex == 1)
                    {
                        cantidadMostrar = 5;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 2)
                    {
                        cantidadMostrar = 10;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 3)
                    {
                        cantidadMostrar = 15;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 4)
                    {
                        cantidadMostrar = 20;
                    }
                    else if (cboCantidadMostrarAsistencial.SelectedIndex == 5)
                    {
                        cantidadMostrar = 25;
                    }


                    foreach (var item in ListaEstadisticostemp_)
                    {
                        ListaEstadisticosAsist objListaEstadisticostemp = new ListaEstadisticosAsist();

                        objListaEstadisticostemp.Id = contador;
                        objListaEstadisticostemp.Grupo = item.Grupo;
                        objListaEstadisticostemp.Cantidad = item.Cantidad;
                        objListaEstadisticostemp.Detalle = item.Detalle;

                        ListaEstadisticostemp.Add(objListaEstadisticostemp);

                        PorESO_Tipe.Add(contador.ToString());
                        PorESO_Cant.Add(item.Cantidad);

                        if (cantidadMostrar == contador)
                        {
                            break;
                        }
                        contador++;
                    }

                    ListaEstadisticosConsultorioMdico = ListaEstadisticostemp;
                    grdRESUMENAsist.DataSource = ListaEstadisticostemp;
                    chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop = "AGRUPADO POR ESPECIALIDA MÉDICA DEL " + dateTimePicker2.Value.ToShortDateString() + " AL " + dateTimePicker1.Value.ToShortDateString();

                    chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);
                    btnDescargarCurvasAsistencial.Enabled = true;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            string servicio = "ASISTENCIAL";


            NombreArchivo = "ATENCIONES " + servicio + " DEL " + dateTimePicker2.Text + " al " + dateTimePicker1.Text;
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdDataServiceAsistencial, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            string nombre = "";
            string servicio = "";
            //if (rbHospitalizados.Checked == true)
            //{
            //    servicio = "HOSPITALIZADOS";
            //}


            NombreArchivo = "RESUMEN DE ATENCIONES " + servicio + " POR " + nombre + " CONSOLIDADO DEL" + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text;

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdRESUMENAsist, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDescargarCurvasAsistencial_Click(object sender, EventArgs e)
        {
            if (checkEdad.Checked == true)
            {
                GuardarGrafico(chartAsistencial, nombreparaGraficoHospSop);
            }
            else if (checkProtocoloAsist.Checked == true)
            {
                GuardarGrafico(chartAsistencial, nombreparaGraficoHospSop);
            }
            else if (checkMedicoAsist.Checked == true)
            {
                GuardarGrafico(chartAsistencial, nombreparaGraficoHospSop);
            }
            else if (checkGrupoEtareoAsist.Checked == true)
            {
                GuardarGrafico(chartAsistencial, nombreparaGraficoHospSop);
            }
            else if (checkGeneroAsist.Checked == true)
            {
                GuardarGrafico(chartAsistencial, nombreparaGraficoHospSop);
            }
            else if (checkServicioAsist.Checked == true)
            {
                GuardarGrafico(chartAsistencial, nombreparaGraficoHospSop);
            }
        }

        private void checkProtocoloAsist_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkConsultorio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConsultorio.Checked == true)
            {
                btnGraficarConsultorioMedico.Visible = true;
            }
            else
            {
                btnGraficarConsultorioMedico.Visible = false;
            }
        }

        private void btnGraficarConsultorioMedico_Click(object sender, EventArgs e)
        {
            //var a = ListaEstadisticosConsultorioMdico;
            if (ListaEstadisticosConsultorioMdico.Count() != 0)
            {
                PorESO_Tipe = new ArrayList();
                PorESO_Cant = new ArrayList();

                var grupo = grdRESUMENAsist.Selected.Rows[0].Cells["Grupo"].Value.ToString();

                foreach (var a in ListaEstadisticosConsultorioMdico)
                {
                    if (a.Grupo == grupo)
                    {
                        var esotempgroup = a.Detalle.GroupBy(p => p.v_MedicoTratante).Select(s => s.First()).ToList();
                        foreach (var b in esotempgroup)
                        {
                            var esotempgroup_ = a.Detalle.FindAll(p => p.v_MedicoTratante == b.v_MedicoTratante).ToList();

                            PorESO_Tipe.Add(b.v_MedicoTratante);
                            PorESO_Cant.Add(esotempgroup_.Count());
                        }
                    }
                }

                chartAsistencial.Titles[0].Text = nombreparaGraficoHospSop = grupo + " - MÉDICOS" ;

                chartAsistencial.Series[0].Points.DataBindXY(PorESO_Tipe, PorESO_Cant);

                //btnDescargarCurvasAsistencial.Enabled = true;
            }
        }

    }
}
