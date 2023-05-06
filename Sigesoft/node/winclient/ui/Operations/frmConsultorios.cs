using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.UI.UserControls;
using Infragistics.Win.UltraWinMaskedEdit;
using System.Data.SqlClient;
//using Infragistics.Win.UltraWinSchedule;
using System.Threading.Tasks;

namespace Sigesoft.Node.WinClient.UI.Operations
{
    public partial class frmConsultorios : Form
    {
        private readonly string _serviceId;
        private static List<GroupParameter> _cboEso;
        private readonly int _nodeId;
        OperationResult objOperationResult = new OperationResult();
        TypeESO _esoTypeId;
        private int _age;
        private readonly int _roleId;
        private readonly string _componentIdDefault;
        private List<ComponentList> _tmpServiceComponentsForBuildMenuList = null;
        private Dictionary<string, UltraValidator> _dicUltraValidators;
        private bool flagValueChange = false;
        private List<ServiceComponentFieldValuesList> _tmpListValuesOdontograma = null;
        public bool _isChangeValue = false;
        private bool _cancelEventSelectedIndexChange;
        List<Sigesoft.Node.WinClient.UI.Operations.FrmEsoV2.ValidacionAMC> _oListValidacionAMC = new List<Sigesoft.Node.WinClient.UI.Operations.FrmEsoV2.ValidacionAMC>();
        private readonly string _action;
        private string _personId;
        private string _Dni;
        private DateTime? _FechaServico;
        private Gender _sexType;
        private List<DiagnosticRepositoryList> _tmpExamDiagnosticComponentList;
        private string _componentId;
        private string _oldValue;
        private string _serviceComponentId;
        private ServiceComponentListNew2 _serviceComponentsInfo = null;
        private string aptitud;
        private rolenodecomponentprofileDto componentProfile;
        ServiceList personData = new ServiceList();

        private ServiceList _datosPersona;
        private int _masterServiceId;

        private string _examName;
        public int _profesionId;
        string _personName;
        private string _ProtocolId;
        private ServiceBL _serviceBL = new ServiceBL();
        private int? _sexTypeId;
        private string _v_CustomerOrganizationId;
        serviceDto idPerson = new serviceDto();
        private List<KeyValueDTO> _formActions;


        private AtencionesIntegralesBL _objAtencionesIntegralesBl = new AtencionesIntegralesBL();


        public frmConsultorios(string serviceId, string componentIdDefault, string action, int roleId, int nodeId, int userId, int tipo)
        {
            _serviceId = serviceId;
            _componentIdDefault = componentIdDefault;
            _action = action;
            _roleId = roleId;
            _nodeId = nodeId;
            aptitud = VerificarAptitud(_serviceId);
            InitializeComponent();
        }

        private string VerificarAptitud(string _serviceId)
        {
            //ARNODL STORE
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena1 = @"select SP.v_Value1 from service SR " +
                          "inner join systemparameter SP on SR.i_AptitudeStatusId=SP.i_ParameterId and SP.i_GroupId=124 " +
                          "where v_ServiceId='" + _serviceId + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector1 = comando.ExecuteReader();
            string aptitud = "";
            while (lector1.Read())
            {
                aptitud = lector1.GetValue(0).ToString();
            }
            lector1.Close();
            conectasam.closesigesoft();
            return aptitud;
        }
        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitGeneral_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmConsultorios_Load(object sender, EventArgs e)
        {

            InitializeForm();


            OperationResult objOperationResult = new OperationResult();
            personData = new ServiceBL().GetServicePersonData(ref objOperationResult, _serviceId);
           _personId = personData.v_PersonId;
            _Dni = personData.v_DocNumber;
            _FechaServico = personData.d_ServiceDate;
            var dataService = GetServiceData(_serviceId);
            _cancelEventSelectedIndexChange = true;
            LoadTabExamenes(dataService);
        }

        private void InitializeForm()
        {
             InitializeData();
            InitializeStaticControls();
            LoadData();
        }
        private void InitializeStaticControls()
        {
            LoadComboBox();
            FormActions();
        }

        private void LoadData()
        {
            //revisar
            OperationResult objOperationResult = new OperationResult();
            ServiceList personData = new ServiceBL().GetServicePersonData(ref objOperationResult, _serviceId);

            var dataService = GetServiceData(_serviceId);
            _cancelEventSelectedIndexChange = true;
            //LoadTabAnamnesis(dataService);
            _cancelEventSelectedIndexChange = false;
            LoadTabExamenes(dataService);
            //LoadTabControlCalidad();
            //LoadTabAptitud();

            _ProtocolId = personData.v_ProtocolId;
            _personName = string.Format("{0} {1} {2}", personData.v_FirstLastName, personData.v_SecondLastName, personData.v_FirstName);
            _personId = personData.v_PersonId;
            _Dni = personData.v_DocNumber;
            _masterServiceId = personData.i_MasterServiceId.Value;
            if (_masterServiceId == 2) btnReceta.Enabled = false;
            else btnReceta.Enabled = true;

            //cbAptitudEso.SelectedValue = personData.i_AptitudeStatusId.ToString();
            //txtComentarioAptitud.Text = personData.v_ObsStatusService;

            DateTime? FechaActual = DateTime.Now;
            //dptDateGlobalExp.Value = personData.d_GlobalExpirationDate == null ? FechaActual.Value.Date.AddYears(1) : personData.d_GlobalExpirationDate.Value;
            //cbNuevoControl.SelectedValue = personData.i_IsNewControl;

            _FechaServico = personData.d_ServiceDate;
            //LoadTabGenericDataandAntecedent(personData);

        }
        private void FormActions()
        {
            //1319
            //_formActions = BLL.Utils.SetFormActionsInSession("frmConsultorios", _nodeId, _roleId, _userId);
            btnGuardarExamen.Enabled = BLL.Utils.IsActionEnabled("frmEso_EXAMENES_SAVE", _formActions);
            //btnAgregarTotalDiagnostico.Enabled = BLL.Utils.IsActionEnabled("frmEso_ANADX_ADDDX", _formActions);
            //_removerTotalDiagnostico = BLL.Utils.IsActionEnabled("frmEso_ANADX_REMOVEDX", _formActions);
            //btnAgregarRecomendaciones_AnalisisDx.Enabled = BLL.Utils.IsActionEnabled("frmEso_ANADX_ADDRECOME", _formActions);
            //_removerRecomendacionAnalisisDx = BLL.Utils.IsActionEnabled("frmEso_ANADX_REMOVERECOME", _formActions);
            //btnAgregarRestriccion_Analisis.Enabled = BLL.Utils.IsActionEnabled("frmEso_ANADX_ADDRESTRIC", _formActions);
            //_removerRestriccionAnalisis = BLL.Utils.IsActionEnabled("frmEso_ANADX_REMOVERESTRIC", _formActions);
            //btnAceptarDX.Enabled = BLL.Utils.IsActionEnabled("frmEso_ANADX_SAVE", _formActions);

            //btnAgregarRecomendaciones_Conclusiones.Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEso_CONCLUSIONES_ADDRECOME", _formActions);
            //btnAgregarRestriccion_ConclusionesTratamiento.Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEso_CONCLUSIONES_ADDRESTRIC", _formActions);

            //_removerRecomendaciones_Conclusiones = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEso_CONCLUSIONES_REMOVERECOME", _formActions);
            //_removerRestricciones_ConclusionesTratamiento = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEso_CONCLUSIONES_REMOVERESTRIC", _formActions);
            //if (btnAceptarDX.Enabled) return;
            //cbCalificacionFinal.Enabled = false;
            //cbTipoDx.Enabled = false;
            //cbEnviarAntecedentes.Enabled = false;
        }

        private void InitializeData()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(InitializeData));
            }
            else
            {

                // Cargar datos generales del paciente
                OperationResult objOperationResult = new OperationResult();

                ServiceList personData = _serviceBL.GetServicePersonData(ref objOperationResult, _serviceId);
                _datosPersona = personData;
                _v_CustomerOrganizationId = personData.v_CustomerOrganizationId;
                _Dni = personData.v_DocNumber;

                _FechaServico = personData.d_ServiceDate;
                _personName = string.Format("{0} {1} {2}", personData.v_FirstLastName, personData.v_SecondLastName, personData.v_FirstName);
                _personId = personData.v_PersonId;

                lblTipoEso.Text = personData.v_EsoTypeName;
                lblServicio.Text = personData.v_ServiceId;
                lblProtocolName.Text = personData.v_ProtocolName;
                _ProtocolId = personData.v_ProtocolId;
             _masterServiceId = personData.i_MasterServiceId.Value;

                if (_masterServiceId == 2) btnReceta.Enabled = false;
                else btnReceta.Enabled = true;

                // calcular edad
                _age = DateTime.Today.AddTicks(-personData.d_BirthDate.Value.Ticks).Year - 1;

                _cancelEventSelectedIndexChange = true;
                //_cancelEventSelectedIndexChange = false;

             
                idPerson = _objAtencionesIntegralesBl.GetService(_serviceId);

                // Analizar el resultado de la operación
                if (objOperationResult.Success != 1)
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void LoadTabExamenes(ServiceData dataService)
        {
            _cboEso = GetListCboEso();
            SumaryWorker(dataService);
            CreateMissingExamens();
        }
        private ServiceData GetServiceData(string serviceId)
        {
            return new ServiceBL().GetServiceData(ref objOperationResult, serviceId);
        }
        private static List<GroupParameter> GetListCboEso()
        {
            return BLL.Utils.GetSystemParameterForComboFormEso();
        }
        private void SumaryWorker(ServiceData dataService)
        {
            lblTrabajador.Text = dataService.Trabajador;
            lblProtocolName.Text = dataService.ProtocolName;
            _esoTypeId = (TypeESO)dataService.EsoTypeId;
            _age = Common.Utils.GetAge(dataService.FechaNacimiento.Value);
        }

        private void CreateMissingExamens()
        {
            try
            {
                using (new LoadingClass.PleaseWait(this.Location, "Cargando..."))
                {
                    var listExamenes = new ServiceBL().ListMissingExamenesNames(ref objOperationResult, _serviceId, _nodeId, _roleId).ToList();
                    foreach (var examen in listExamenes)
                    {
                        var componentsId = new ServiceBL().ConcatenateComponents(_serviceId, examen.v_ComponentId);
                        AsyncCreateNextExamen(componentsId, examen.v_CategoryName);
                    }
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void AsyncCreateNextExamen(string componentId, string threadName)
        {
            //ThreadPool.QueueUserWorkItem(CallBack,componentId);
            var t = new Thread(() => { DoWorkGetExamen(componentId); }) { Name = threadName };
            t.Priority = t.Name == _componentIdDefault ? ThreadPriority.Highest : ThreadPriority.Lowest;
            t.Start();
        }
        public void DoWorkGetExamen_OLD(string componentId)
        {
            if (_tmpServiceComponentsForBuildMenuList == null)
            {
                _tmpServiceComponentsForBuildMenuList = new List<ComponentList>();
            }
            var nextExamen = new ServiceBL().ExamenByDefaultOrAssigned(ref objOperationResult, _serviceId, componentId)[0];
            _tmpServiceComponentsForBuildMenuList.Add(nextExamen);
            var serviceComponentsInfo = new ServiceBL().GetServiceComponentsInfo(ref objOperationResult, nextExamen.v_ServiceComponentId, _serviceId);
            CreateExamen(nextExamen, serviceComponentsInfo);
        }

        public void DoWorkGetExamen(string componentId)
        {
            if (_tmpServiceComponentsForBuildMenuList == null)
            {
                _tmpServiceComponentsForBuildMenuList = new List<ComponentList>();
            }
            //ARNOLD 13
            var nextExamen = new ServiceBL().ExamenByDefaultOrAssigned(ref objOperationResult, _serviceId, componentId)[0];
            _tmpServiceComponentsForBuildMenuList.Add(nextExamen);
            var serviceComponentsInfo = new ServiceBL().GetServiceComponentsInfo(ref objOperationResult, nextExamen.v_ServiceComponentId, _serviceId);

            CreateExamen(nextExamen, serviceComponentsInfo);

        }

        private void CreateExamen(ComponentList component, ServiceComponentListNew2 serviceComponentsInfo)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ComponentList, ServiceComponentListNew2>(CreateExamen), component, serviceComponentsInfo);
            }
            else
            {
                var row = 1;
                const int column = 1;
                var fieldsByGroupBoxCount = 1;

                var tab = CrearTab(component);
                var tlpParent = CreateTableLayoutPanelParent(tab);

                var uv = CreateValidators(component);
                foreach (var groupComponentName in component.GroupedComponentsName)
                {
                    var gbComponent = CreateGroupBoxComponent(groupComponentName);
                    row++;
                    var tlpChildren = CreateTableLayoutPanelChildren(groupComponentName);
                    var groupBoxes = GetGroupBoxes(component, groupComponentName.v_ComponentId);
                    foreach (var groupbox in groupBoxes)
                    {
                        var groupBox = CreateGroupBoxForGroup(groupbox, serviceComponentsInfo);
                        fieldsByGroupBoxCount++;
                        var tableLayoutPanel = CreateTableLayoutForControls(groupbox, component);
                        var fieldsByGroupBox = GetFieldsEachGroups(component, groupbox, groupComponentName);
                        fieldsByGroupBox.Aggregate(1, (current, field) => CreateCampoComponente(component, field, groupbox.i_Column, current, tableLayoutPanel, uv));
                        groupBox.Controls.Add(tableLayoutPanel);
                        tlpChildren.Controls.Add(groupBox, column, fieldsByGroupBoxCount);
                    }
                    gbComponent.Controls.Add(tlpChildren);
                    tlpParent.Controls.Add(gbComponent, column, row);
                }
                tlpParent.AutoScroll = true;
                tlpParent.Dock = DockStyle.Fill;
                tlpParent.BackColor = Color.Gray;
                _cancelEventSelectedIndexChange = true;
                if (component.i_ServiceComponentStatusId == (int)ServiceComponentStatus.Iniciado ||
                    component.i_ServiceComponentStatusId == (int)ServiceComponentStatus.PorIniciar)
                    SetDefaultValueAfterBuildMenu(component);
                else
                {
                    SetDefaultValueAfterBuildMenu(component);
                    SearchControlAndSetValue(tab.TabPage, serviceComponentsInfo);
                }
                _cancelEventSelectedIndexChange = false;

            }
        }

        private void SearchControlAndSetValue(Control tlpParent, ServiceComponentListNew2 serviceComponentsInfo)
        {

            KeyTagControl keyTagControl = null;
            var breakHazChildrenUc = false;
            Sigesoft.Node.WinClient.UI.Operations.FrmEsoV2.ValidacionAMC oValidacionAMC = null;
            var x = serviceComponentsInfo.ServiceComponentFields;
            var y = x.Select(p => p.ServiceComponentFieldValues).ToList();

            //var value = y.Find(p => p[0].v_ComponentFieldId = "idcomponent");

            _oListValidacionAMC = new List<Sigesoft.Node.WinClient.UI.Operations.FrmEsoV2.ValidacionAMC>();
            foreach (var item in y)
            {
                oValidacionAMC = new Sigesoft.Node.WinClient.UI.Operations.FrmEsoV2.ValidacionAMC();
                oValidacionAMC.v_ServiceComponentFieldValuesId = item[0].v_ServiceComponentFieldValuesId;
                oValidacionAMC.v_Value1 = item[0].v_Value1;
                oValidacionAMC.v_ComponentFieldsId = item[0].v_ComponentFieldId;
                _oListValidacionAMC.Add(oValidacionAMC);
            }
            foreach (Control ctrl in tlpParent.Controls)
            {
                if (ctrl.Tag != null)
                {
                    var t = ctrl.Tag.GetType();
                    if (t == typeof(KeyTagControl))
                    {
                        // Capturar objeto tag
                        keyTagControl = (KeyTagControl)ctrl.Tag;

                        List<ServiceComponentFieldValuesList> dataSourceUserControls;
                        switch (keyTagControl.i_ControlId)
                        {
                            case (int)ControlType.UcOdontograma:

                                #region Setear valores en Odontograma
                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("ODO"));
                                ((UserControls.ucOdontograma)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucOdontograma)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                break;
                            case (int)ControlType.UcAudiometria:
                                #region Setear valores en udiometria

                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("AUD"));
                                ((UserControls.ucAudiometria)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucAudiometria)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                break;
                            case (int)ControlType.ucFacial:

                                break;
                            case (int)ControlType.UcSomnolencia:
                                #region Setear valores en udiometria

                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("SOM"));
                                ((UserControls.ucSomnolencia)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucSomnolencia)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                break;
                            case (int)ControlType.UcAcumetria:
                                #region Setear valores en udiometria

                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("ACU"));
                                ((UserControls.ucAcumetria)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucAcumetria)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                break;
                            case (int)ControlType.UcFototipo:
                                #region Setear valores en FotoTipo

                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("FOT"));
                                var multimediaFileId = dataSourceUserControls.Find(xp => xp.v_ComponentFieldId == Constants.txt_MULTIMEDIA_FILE_FOTO_TIPO).v_Value1;
                                ServiceBL oServiceBl = new ServiceBL();
                                var a = oServiceBl.ObtenerImageMultimedia(multimediaFileId);
                                ((UserControls.ucFotoTipo)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                dataSourceUserControls.Add(new ServiceComponentFieldValuesList() { fotoTipo = a });
                                ((UserControls.ucFotoTipo)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                break;
                            case (int)ControlType.UcSintomaticoRespi:
                                #region Setear valores en udiometria

                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("RES"));
                                ((UserControls.ucSintomaticoResp)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucSintomaticoResp)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                break;
                            case (int)ControlType.UcRxLumboSacra:
                                #region Setear valores en udiometria

                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("RXL"));
                                ((UserControls.ucRXLumboSacra)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucRXLumboSacra)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                break;
                            case (int)ControlType.UcOjoSeco:
                                #region Setear valores en udiometria

                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("OJS"));
                                ((UserControls.ucOjoSeco)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucOjoSeco)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                break;
                            case (int)ControlType.UcOtoscopia:
                                #region Setear valores en udiometria

                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("OTO"));
                                ((UserControls.ucOtoscopia)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucOtoscopia)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                break;
                            case (int)ControlType.UcEvaluacionErgonomica:
                                #region Setear valores en udiometria

                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("EVA"));
                                ((UserControls.ucEvaluacionErgonomica)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucEvaluacionErgonomica)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                break;
                            case (int)ControlType.UcOsteoMuscular:
                                #region Setear valores en udiometria

                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("OTS"));
                                ((UserControls.ucOsteoMuscular)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucOsteoMuscular)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                break;

                            default:
                                foreach (var item in serviceComponentsInfo.ServiceComponentFields)
                                {
                                    var componentFieldsId = item.v_ComponentFieldsId;

                                    foreach (var fv in item.ServiceComponentFieldValues)
                                    {
                                        #region Setear valores en el caso de controles dinamicos

                                        SetValueControl(keyTagControl.i_ControlId,
                                            ctrl,
                                            componentFieldsId,
                                            keyTagControl.v_ComponentFieldsId,
                                            fv.v_Value1,
                                            item.i_HasAutomaticDxId == null ? (int)SiNo.NO : (SiNo)item.i_HasAutomaticDxId);

                                        #endregion
                                    }
                                }
                                break;
                        }
                    }
                }

                if (!ctrl.HasChildren) continue;
                if (!breakHazChildrenUc && keyTagControl == null)
                {
                    SearchControlAndSetValue(ctrl, serviceComponentsInfo);
                }
            }
        }

        private static void SetValueControl(int controlId, Control ctrl, string componentFieldsId, string tagComponentFieldsId, string value1, SiNo hasAutomaticDx)
        {
            switch ((ControlType)controlId)
            {
                case ControlType.Fecha:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        if (value1 == null)
                        {
                            value1 = DateTime.Now.ToString();
                        }
                        ((DateTimePicker)ctrl).Value = Convert.ToDateTime(value1);
                        if (hasAutomaticDx == SiNo.SI)
                            ctrl.BackColor = Color.Pink;
                        else
                            ctrl.BackColor = Color.White;
                    }
                    break;
                case ControlType.CadenaTextual:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        ((TextBox)ctrl).Text = value1;
                        if (hasAutomaticDx == SiNo.SI)
                            ctrl.BackColor = Color.Pink;
                        else
                            ctrl.BackColor = Color.White;

                    }
                    break;
                case ControlType.CadenaMultilinea:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        ((TextBox)ctrl).Text = value1;
                        if (hasAutomaticDx == SiNo.SI)
                            ctrl.BackColor = Color.Pink;
                        else
                            ctrl.BackColor = Color.White;

                    }
                    break;
                case ControlType.NumeroEntero:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        if (string.IsNullOrEmpty(value1))
                            value1 = "0";

                        ((UltraNumericEditor)ctrl).Value = value1;
                        if (hasAutomaticDx == SiNo.SI)
                            ctrl.BackColor = Color.Pink;
                        else
                            ctrl.BackColor = Color.White;
                    }
                    break;
                case ControlType.NumeroDecimal:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        if (string.IsNullOrEmpty(value1))
                            value1 = "0";

                        ((UltraNumericEditor)ctrl).Value = value1;
                        if (hasAutomaticDx == SiNo.SI)
                            ctrl.BackColor = Color.Pink;
                        else
                            ctrl.BackColor = Color.White;
                    }
                    break;
                case ControlType.SiNoCheck:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        ((CheckBox)ctrl).Checked = Convert.ToBoolean(int.Parse(value1));
                    }
                    break;
                case ControlType.SiNoRadioButton:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        ((RadioButton)ctrl).Checked = Convert.ToBoolean(int.Parse(value1));
                    }
                    break;

                case ControlType.Radiobutton:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        ((RadioButton)ctrl).Checked = Convert.ToBoolean(int.Parse(value1));
                    }
                    break;
                case ControlType.SiNoCombo:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        ((ComboBox)ctrl).SelectedValue = value1;
                    }
                    break;
                case ControlType.Lista:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        var cb = (ComboBox)ctrl;
                        cb.SelectedValue = value1;
                    }
                    break;
            }
        }

        private void SetDefaultValueAfterBuildMenu(ComponentList examen)
        {
            try
            {
                var findTab = tcExamList.Tabs[examen.v_ComponentId];

                foreach (ComponentFieldsList cf in examen.Fields)
                {
                    var ctrl = findTab.TabPage.Controls.Find(cf.v_ComponentFieldId, true);

                    if (ctrl.Length != 0)
                    {
                        #region Setear valor x defecto del control

                        switch ((ControlType)cf.i_ControlId)
                        {
                            case ControlType.Fecha:
                                DateTimePicker dtp = (DateTimePicker)ctrl[0];
                                dtp.CreateControl();
                                dtp.Value = string.IsNullOrEmpty(cf.v_DefaultText) ? DateTime.Now : DateTime.Parse(cf.v_DefaultText);
                                break;
                            case ControlType.CadenaTextual:
                                TextBox txtt = (TextBox)ctrl[0];
                                txtt.CreateControl();
                                txtt.Text = cf.v_DefaultText;
                                txtt.BackColor = Color.White;
                                break;
                            case ControlType.CadenaMultilinea:
                                TextBox txtm = (TextBox)ctrl[0];
                                txtm.CreateControl();
                                txtm.Text = cf.v_DefaultText;
                                txtm.BackColor = Color.White;
                                break;
                            case ControlType.NumeroEntero:
                                UltraNumericEditor uni = (UltraNumericEditor)ctrl[0];
                                uni.CreateControl();
                                uni.Value = string.IsNullOrEmpty(cf.v_DefaultText) ? 0 : int.Parse(cf.v_DefaultText);
                                uni.BackColor = Color.White;
                                break;
                            case ControlType.NumeroDecimal:
                                UltraNumericEditor und = (UltraNumericEditor)ctrl[0];
                                und.CreateControl();
                                und.Value = string.IsNullOrEmpty(cf.v_DefaultText) ? 0 : double.Parse(cf.v_DefaultText);
                                und.BackColor = Color.White;
                                break;
                            case ControlType.SiNoCheck:
                                CheckBox chkSiNo = (CheckBox)ctrl[0];
                                chkSiNo.CreateControl();
                                chkSiNo.Checked = !string.IsNullOrEmpty(cf.v_DefaultText) && Convert.ToBoolean(int.Parse(cf.v_DefaultText));
                                break;
                            case ControlType.SiNoRadioButton:
                                RadioButton rbSiNo = (RadioButton)ctrl[0];
                                rbSiNo.CreateControl();
                                rbSiNo.Checked = !string.IsNullOrEmpty(cf.v_DefaultText) && Convert.ToBoolean(int.Parse(cf.v_DefaultText));
                                break;
                            case ControlType.Radiobutton:
                                RadioButton rb = (RadioButton)ctrl[0];
                                rb.CreateControl();
                                rb.Checked = !string.IsNullOrEmpty(cf.v_DefaultText) && Convert.ToBoolean(int.Parse(cf.v_DefaultText));
                                break;
                            case ControlType.SiNoCombo:
                                ComboBox cbSiNo = (ComboBox)ctrl[0];
                                cbSiNo.CreateControl();
                                cbSiNo.SelectedValue = string.IsNullOrEmpty(cf.v_DefaultText) ? "-1" : cf.v_DefaultText;
                                cbSiNo.MouseWheel += new MouseEventHandler(comboBox1_MouseWheel);
                                break;
                            case ControlType.UcFileUpload:
                                break;
                            case ControlType.Lista:
                                ComboBox cbList = (ComboBox)ctrl[0];
                                cbList.CreateControl();
                                cbList.SelectedValue = string.IsNullOrEmpty(cf.v_DefaultText) ? "-1" : cf.v_DefaultText;
                                cbList.MouseWheel += new MouseEventHandler(comboBox1_MouseWheel);
                                break;
                            case ControlType.UcOdontograma:
                                //(UserControls.ucOdontograma).ClearValueControl();;
                                break;
                            case ControlType.UcAudiometria:
                                ((ucAudiometria)ctrl[0]).ClearValueControl();
                                break;
                            case ControlType.ucFacial:
                                //((ucExFacial)ctrl[0]).ClearValueControl();
                                break;
                            case ControlType.UcSomnolencia:
                                ((ucSomnolencia)ctrl[0]).ClearValueControl();
                                break;
                            case ControlType.UcAcumetria:
                                ((ucAcumetria)ctrl[0]).ClearValueControl();
                                break;
                            case ControlType.UcSintomaticoRespi:
                                ((ucSintomaticoResp)ctrl[0]).ClearValueControl();
                                break;
                            case ControlType.UcRxLumboSacra:
                                ((ucRXLumboSacra)ctrl[0]).ClearValueControl();
                                break;
                            case ControlType.UcOtoscopia:
                                ((ucOtoscopia)ctrl[0]).ClearValueControl();
                                break;
                            case ControlType.UcEvaluacionErgonomica:
                                ((ucEvaluacionErgonomica)ctrl[0]).ClearValueControl();
                                break;
                            case ControlType.UcOsteoMuscular:
                                ((ucOsteoMuscular)ctrl[0]).ClearValueControl();
                                break;

                            case ControlType.UcOjoSeco:
                                ((ucOjoSeco)ctrl[0]).ClearValueControl();
                                break;
                        }

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void comboBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        private UltraTab CrearTab(ComponentList component)
        {
            var ultraTab = new UltraTab
            {
                Text = component.v_Name,
                Key = component.v_ComponentId,
                Tag = component.v_ServiceComponentId,
                ToolTipText = component.v_Name + @" / " + component.v_ServiceComponentId + @" / " + component.v_ComponentId
            };
            if (component.i_ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado) ultraTab.Appearance.BackColor = Color.Pink;
            tcExamList.Tabs.Add(ultraTab);
            return ultraTab;
        }
        private static TableLayoutPanel CreateTableLayoutPanelParent(UltraTab tab)
        {
            var tblpParent = new TableLayoutPanel
            {
                Name = "tblpParent",
                ColumnCount = 1,
                RowCount = new List<ComponentFieldsList>().Count
            };
            tab.TabPage.Controls.Add(tblpParent);
            return tblpParent;
        }
        private UltraValidator CreateValidators(ComponentList component)
        {
            var uv = CreateUltraValidatorByComponentId(component.v_ComponentId);
            return uv;
        }
        private UltraValidator CreateUltraValidatorByComponentId(string componentId)
        {
            var uv = new UltraValidator(components);

            if (_dicUltraValidators == null)
                _dicUltraValidators = new Dictionary<string, UltraValidator>();

            _dicUltraValidators.Add(componentId, uv);

            return uv;
        }
        private static GroupBox CreateGroupBoxComponent(ComponentList groupBox)
        {
            var subcategory = ObtenerCategory(groupBox.v_ComponentId);
            var gbGroupedComponent = new GroupBox
            {
                Text = groupBox.v_GroupedComponentName + " - " + subcategory,
                Name = "gb_" + groupBox.v_GroupedComponentName,
                BackColor = Color.LightCyan,
                AutoSize = true,
                Dock = DockStyle.Top,

            };

            return gbGroupedComponent;
        }
        private static object ObtenerCategory(string componentId)
        {
            var name = new ServiceBL().GetSubCategoryName(componentId);

            if (name == null)
            {
                return "";
            }
            return name;
        }
        private static TableLayoutPanel CreateTableLayoutPanelChildren(ComponentList groupComponentName)
        {
            TableLayoutPanel tblpGroupedComponent = new TableLayoutPanel
            {
                Name = "tblpGroup_" + groupComponentName.v_GroupedComponentName,
                ColumnCount = 1,
                RowCount = 1,
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            return tblpGroupedComponent;
        }
        private static IEnumerable<ComponentFieldsList> GetGroupBoxes(ComponentList component, string componentId)
        {
            try
            {
                var fieldsByComponent = component.Fields
                    .ToList()
                    .FindAll(p => p.v_ComponentId == componentId);

                var groupBoxes = fieldsByComponent.GroupBy(e => new { e.v_Group }).Select(g => g.First()).OrderBy(o => o.v_Group).ToList();

                return groupBoxes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        private static GroupBox CreateGroupBoxForGroup(ComponentFieldsList groupbox, ServiceComponentListNew2 serviceComponentsInfo)
        {
            var groupBox = new GroupBox
            {
                Text = groupbox.v_Group,
                Name = "gb_" + groupbox.v_Group,
                BackColor = Color.Azure,
                AutoSize = true,
                Dock = DockStyle.Top
            };
            var x = serviceComponentsInfo.ServiceComponentFields;
            if (serviceComponentsInfo.v_ComponentId == "N009-ME000000062")
            {
                if (serviceComponentsInfo.v_ServiceComponentStatusName != "POR INICIAR")
                {
                    var y = x.Select(p => p.ServiceComponentFieldValues).ToList();
                    if (y.Count > 0)
                    {
                        if (y.Find(p => p[0].v_ComponentFieldId == "N002-MF000000222")[0].v_Value1 == "1" && groupbox.v_Group == "C 3. Forma y Tamaño: (Consulte las radiografías estandar, se requieres dos símbolos; marque un primario y un secundario)")
                        {
                            groupBox.Enabled = false;
                        }
                        if (y.Find(p => p[0].v_ComponentFieldId == "N009-MF000000761")[0].v_Value1 == "1" && (groupbox.v_Group == "C 4.1 Placa Pleurales" || groupbox.v_Group == "C 4.2 Engrosamiento Difuso de la Pleura"))
                        {
                            groupBox.Enabled = false;
                        }
                        if (y.Find(p => p[0].v_ComponentFieldId == "N009-MF000000760")[0].v_Value1 == "1" && groupbox.v_Group == "C 6. MARQUE  la respuesta adecuada; si marca \"od\", escriba a continuación un COMENTARIO")
                        {
                            groupBox.Enabled = false;
                        }
                    }

                }
                else
                {
                    if (groupbox.v_Group == "C 3. Forma y Tamaño: (Consulte las radiografías estandar, se requieres dos símbolos; marque un primario y un secundario)" || groupbox.v_Group == "C 4.1 Placa Pleurales" || groupbox.v_Group == "C 4.2 Engrosamiento Difuso de la Pleura" || groupbox.v_Group == "C 6. MARQUE  la respuesta adecuada; si marca \"od\", escriba a continuación un COMENTARIO")
                    {
                        groupBox.Enabled = false;
                    }
                }

            }
            return groupBox;
        }
        private static TableLayoutPanel CreateTableLayoutForControls(ComponentFieldsList groupbox, ComponentList component)
        {
            var tblpGroup = new TableLayoutPanel
            {
                Name = "tblpGroup_" + groupbox.v_Group,
                ColumnCount = groupbox.i_Column * Constants.COLUMNAS_POR_CONTROL,
                RowCount = RedondeoMayor(component.Fields.Count, groupbox.i_Column),
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            return tblpGroup;
        }
        private static int RedondeoMayor(int a, int b)
        {
            return (int)Math.Ceiling(a / (double)b);
        }
        private static IEnumerable<ComponentFieldsList> GetFieldsEachGroups(ComponentList component, ComponentFieldsList groupbox, ComponentList groupComponentName)
        {
            return component.Fields.FindAll(p => p.v_Group == groupbox.v_Group && p.v_ComponentId == groupComponentName.v_ComponentId);

        }
        private int CreateCampoComponente(ComponentList component, ComponentFieldsList field, int groupboxIColumn, int nroControlNet, TableLayoutPanel tableLayoutPanel, UltraValidator ultraValidator)
        {
            CreateLabelField(field, groupboxIColumn, nroControlNet, tableLayoutPanel);
            nroControlNet++;

            CreateControl(component, field, groupboxIColumn, nroControlNet, tableLayoutPanel, ultraValidator);
            nroControlNet++;

            CreateUnidadMedida(field, groupboxIColumn, nroControlNet, tableLayoutPanel);
            nroControlNet++;

            return nroControlNet;
        }

        private static void CreateUnidadMedida(ComponentFieldsList field, int groupboxIColumn, int nroControlNet, TableLayoutPanel tableLayoutPanel)
        {
            var labelUnidadMedida = LabelUnidadMedida(field);
            var fila = RedondeoMayor(nroControlNet, groupboxIColumn * Constants.COLUMNAS_POR_CONTROL);
            var columna = nroControlNet - (fila - 1) * (groupboxIColumn * Constants.COLUMNAS_POR_CONTROL);
            tableLayoutPanel.Controls.Add(labelUnidadMedida, columna - 1, fila - 1);
        }

        private static Label LabelUnidadMedida(ComponentFieldsList field)
        {
            var lbl1 = new Label
            {
                AutoSize = false,
                Width = 50,
                Text = field.v_MeasurementUnitName
            };
            lbl1.Font = new Font(lbl1.Font, FontStyle.Bold | FontStyle.Italic);
            lbl1.TextAlign = ContentAlignment.BottomLeft;

            return lbl1;
        }

        private static void CreateLabelField(ComponentFieldsList fields, int column, int nroControlNet, TableLayoutPanel tableLayoutPanel)
        {
            var lbl = CreateLabel(fields);
            var fila = RedondeoMayor(nroControlNet, column * Constants.COLUMNAS_POR_CONTROL);
            var columna = nroControlNet - (fila - 1) * (column * Constants.COLUMNAS_POR_CONTROL);
            tableLayoutPanel.Controls.Add(lbl, columna - 1, fila - 1);
        }

        private static Label CreateLabel(ComponentFieldsList fields)
        {
            var lbl = new Label
            {
                Text = fields.v_TextLabel,
                Width = fields.i_LabelWidth,
                TextAlign = ContentAlignment.BottomRight,
                AutoSize = false
            };
            lbl.Font = new Font(lbl.Font.FontFamily.Name, 7.25F);

            return lbl;
        }
        private void CreateControl(ComponentList component, ComponentFieldsList field, int column, int nroControlNet, TableLayoutPanel tableLayoutPanel, UltraValidator ultraValidator)
        {
            try
            {
                var control = CreateControlInEso(field, component, ultraValidator);
                var fila = RedondeoMayor(nroControlNet, column * Constants.COLUMNAS_POR_CONTROL);
                var columna = nroControlNet - (fila - 1) * (column * Constants.COLUMNAS_POR_CONTROL);
                tableLayoutPanel.Controls.Add(control, columna - 1, fila - 1);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        private Control CreateControlInEso(ComponentFieldsList field, ComponentList component, UltraValidator validator)
        {
            var esMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
            UltraNumericEditor une;
            TextBox txt;
            Control ctl = null;

            switch ((ControlType)field.i_ControlId)
            {
                #region Creacion del control
                case ControlType.Fecha:
                    var dtp = new DateTimePicker();
                    dtp.Width = field.i_ControlWidth;
                    dtp.Height = field.i_HeightControl;
                    dtp.Name = field.v_ComponentFieldId;
                    dtp.Format = DateTimePickerFormat.Custom;
                    dtp.CustomFormat = @"dd/MM/yyyy";
                    ctl = dtp;
                    break;

                case ControlType.CadenaTextual:
                    txt = new TextBox();
                    txt.Width = field.i_ControlWidth;
                    txt.Height = field.i_HeightControl;
                    txt.MaxLength = field.i_MaxLenght;
                    txt.Name = field.v_ComponentFieldId;
                    txt.CharacterCasing = esMayuscula == 1 ? CharacterCasing.Upper : CharacterCasing.Normal;

                    if (field.i_IsCalculate == (int)SiNo.SI)
                    {
                        txt.Enabled = false;
                    }
                    else
                    {
                        txt.Leave += txt_Leave;
                    }

                    if (field.i_IsRequired == (int)SiNo.SI)
                        SetControlValidate(field.i_ControlId, txt, null, null, validator);

                    txt.Enter += Capture_Value;

                    //AMC
                    if (field.i_Enabled == (int)SiNo.NO)
                    {
                        txt.Enabled = true;
                    }
                    else
                    {
                        txt.Enabled = false;
                    }


                    if (field.i_ReadOnly == (int)SiNo.SI)
                    {
                        txt.ReadOnly = true;
                    }
                    else
                    {
                        txt.ReadOnly = false;
                    }

                    if (_action == "View")
                    {
                        txt.ReadOnly = true;
                    }

                    ctl = txt;
                    break;
                case ControlType.CadenaMultilinea:
                    txt = new TextBox()
                    {
                        Width = field.i_ControlWidth,
                        Height = field.i_HeightControl,
                        Multiline = true,
                        MaxLength = field.i_MaxLenght,
                        ScrollBars = ScrollBars.Vertical,
                        Name = field.v_ComponentFieldId
                    };

                    //AMC
                    if (field.i_Enabled == (int)SiNo.NO)
                    {
                        txt.Enabled = true;
                    }
                    else
                    {
                        txt.Enabled = false;
                    }


                    if (field.i_ReadOnly == (int)SiNo.SI)
                    {
                        txt.ReadOnly = true;
                    }
                    else
                    {
                        txt.ReadOnly = false;
                    }

                    txt.CharacterCasing = esMayuscula == 1 ? CharacterCasing.Upper : CharacterCasing.Normal;

                    txt.Enter += Capture_Value;
                    txt.Leave += txt_Leave;

                    if (field.i_IsRequired == (int)SiNo.SI)
                        SetControlValidate(field.i_ControlId, txt, null, null, validator);

                    if (_action == "View")
                    {
                        txt.ReadOnly = true;
                    }

                    ctl = txt;
                    break;
                case ControlType.NumeroEntero:
                    une = new UltraNumericEditor()
                    {
                        Width = field.i_ControlWidth,
                        Height = field.i_HeightControl,
                        NumericType = NumericType.Integer,
                        PromptChar = ' ',
                        Name = field.v_ComponentFieldId,
                        MaskDisplayMode = MaskMode.Raw

                    };

                    // Asociar el control a un evento
                    une.Enter += Capture_Value;

                    if (field.i_IsCalculate == (int)SiNo.SI)
                    {
                        une.ReadOnly = true;
                        une.ValueChanged += txt_ValueChanged;
                    }
                    else
                    {
                        une.Leave += txt_Leave;
                    }

                    if (field.i_IsRequired == (int)SiNo.SI)
                    {
                        // Establecer condición por rangos
                        SetControlValidate(field.i_ControlId, une, field.r_ValidateValue1, field.r_ValidateValue2, validator);
                    }

                    if (_action == "View")
                    {
                        une.ReadOnly = true;
                    }

                    ctl = une;
                    break;
                case ControlType.NumeroDecimal:
                    une = new UltraNumericEditor()
                    {
                        Width = field.i_ControlWidth,
                        Height = field.i_HeightControl,
                        PromptChar = ' ',
                        Name = field.v_ComponentFieldId,
                        NumericType = NumericType.Double,
                        MaskDisplayMode = MaskMode.Raw

                    };

                    if (field.i_NroDecimales == 1)
                    {
                        une.MaskInput = "nnnnn.n";
                    }
                    else if (field.i_NroDecimales == 2)
                    {
                        une.MaskInput = "nnnnn.nn";
                    }
                    else if (field.i_NroDecimales == 3)
                    {
                        une.MaskInput = "nnnnn.nnn";
                    }
                    else if (field.i_NroDecimales == 4)
                    {
                        une.MaskInput = "nnnnn.nnnn";
                    }




                    // Asociar el control a un evento
                    une.Enter += Capture_Value;

                    if (field.i_IsCalculate == (int)SiNo.SI)
                    {
                        une.ValueChanged += txt_ValueChanged;
                        une.ReadOnly = true;
                    }
                    else
                    {
                        une.Leave += txt_Leave;
                    }

                    if (field.i_IsRequired == (int)SiNo.SI)
                    {
                        // Establecer condición por rangos                                                              
                        SetControlValidate(field.i_ControlId, une, field.r_ValidateValue1, field.r_ValidateValue2, validator);
                    }

                    if (_action == "View")
                    {
                        une.ReadOnly = true;
                    }

                    ctl = une;
                    break;
                case ControlType.SiNoCheck:
                    ctl = new CheckBox()
                    {
                        Width = field.i_ControlWidth,
                        Height = field.i_HeightControl,
                        Text = @"Si/No",
                        Name = field.v_ComponentFieldId,
                    };

                    ctl.Enter += Capture_Value;
                    ctl.Leave += txt_Leave;

                    if (_action == "View")
                    {
                        ctl.Enabled = false;
                    }

                    break;
                case ControlType.SiNoRadioButton:
                    ctl = new RadioButton()
                    {
                        Width = field.i_ControlWidth,
                        Height = field.i_HeightControl,
                        Text = @"Si/No",
                        Name = field.v_ComponentFieldId
                    };

                    ctl.Enter += Capture_Value;
                    ctl.Leave += txt_Leave;

                    if (_action == "View")
                    {
                        ctl.Enabled = false;
                    }

                    break;
                case ControlType.Radiobutton:
                    ctl = new RadioButton()
                    {
                        Width = field.i_ControlWidth,
                        Height = field.i_HeightControl,
                        Name = field.v_ComponentFieldId
                    };
                    ctl.Enter += new EventHandler(Capture_Value);
                    ctl.Leave += new EventHandler(txt_Leave);
                    if (_action == "View")
                    {
                        ctl.Enabled = false;
                    }
                    break;
                case ControlType.SiNoCombo:
                    ctl = new ComboBox()
                    {
                        Width = field.i_ControlWidth,
                        Height = field.i_HeightControl,
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Name = field.v_ComponentFieldId
                    };
                    var data = _cboEso.Find(p => p.Id == field.i_GroupId.ToString()).Items;
                    var list = ConvertClassToStruct(data);
                    Utils.LoadDropDownList((ComboBox)ctl, "Value1", "Id", list, DropDownListAction.Select);

                    if (field.i_IsRequired == (int)SiNo.SI)
                    {
                        SetControlValidate(field.i_ControlId, ctl, null, null, validator);
                    }

                    ctl.Enter += Capture_Value;
                    ctl.Leave += txt_Leave;

                    if (_action == "View")
                    {
                        ctl.Enabled = false;
                    }

                    break;
                #region ...
                case ControlType.UcFileUpload:
                    var ucFileUpload = new ucFileUpload
                    {
                        PersonId = _personId,
                        Name = field.v_ComponentFieldId,
                        Dni = _Dni,
                        Fecha = _FechaServico.Value.ToString("ddMMyyyy"),
                        Consultorio = component.v_CategoryName,
                        ServiceComponentId = component.v_ServiceComponentId
                    };
                    //ucFileUpload.Dni = _Dni;
                    //ucFileUpload.Fecha = lblFecInicio.Text;
                    //ucFileUpload.Consultorio = com.v_CategoryName;
                    //ucFileUpload.ServiceComponentId = com.v_ServiceComponentId;

                    //ctl = new Sigesoft.Node.WinClient.UI.UserControls.ucFileUpload();
                    ctl = ucFileUpload;
                    break;
                case ControlType.UcOdontograma:
                    var ucOdontograma = new ucOdontograma { Name = field.v_ComponentFieldId };
                    ctl = ucOdontograma;
                    break;
                case ControlType.UcFototipo:
                    var ucFotoTipo = new ucFotoTipo { Name = field.v_ComponentFieldId };
                    ctl = ucFotoTipo;
                    break;
                case ControlType.UcAudiometria:
                    var ucAudiometria = new ucAudiometria
                    {
                        Name = field.v_ComponentFieldId,
                        PersonId = _personId,
                        ServiceComponentId = component.v_ServiceComponentId
                    };
                    ctl = ucAudiometria;
                    break;
                case ControlType.ucFacial:
                    var ucFacial = new ucExFacial
                    {
                        Name = field.v_ComponentFieldId,
                        PersonId = _personId,
                        ServiceComponentId = component.v_ServiceComponentId
                    };
                    ctl = ucFacial;
                    break;
                case ControlType.UcSomnolencia:
                    var ucSomnolencia = new ucSomnolencia { Name = field.v_ComponentFieldId };
                    ctl = ucSomnolencia;
                    break;

                case ControlType.UcBoton:
                    var ucBoton = new ucBoton
                    {
                        Name = field.v_ComponentFieldId,
                        Examen = component.v_Name
                    };
                    //ucBoton.Dni = _Dni;
                    //ucBoton.FechaServicio = _FechaServico.Value;
                    ctl = ucBoton;
                    break;

                case ControlType.UcAcumetria:
                    var ucAcumetria = new ucAcumetria { Name = field.v_ComponentFieldId };
                    ctl = ucAcumetria;
                    break;
                case ControlType.UcSintomaticoRespi:
                    var ucSintomaticoRespi = new ucSintomaticoResp { Name = field.v_ComponentFieldId };
                    ctl = ucSintomaticoRespi;
                    break;
                case ControlType.UcRxLumboSacra:
                    var ucRxLumboSacra = new ucRXLumboSacra { Name = field.v_ComponentFieldId };
                    ctl = ucRxLumboSacra;
                    break;
                case ControlType.UcOjoSeco:
                    var ucOjoSeco = new ucOjoSeco { Name = field.v_ComponentFieldId };
                    ctl = ucOjoSeco;
                    break;

                case ControlType.UcOtoscopia:
                    var ucOtoscopia = new ucOtoscopia { Name = field.v_ComponentFieldId };
                    ctl = ucOtoscopia;
                    break;

                case ControlType.UcEvaluacionErgonomica:
                    var ucEvaluacionErgonomica = new ucEvaluacionErgonomica { Name = field.v_ComponentFieldId };
                    ctl = ucEvaluacionErgonomica;
                    break;

                case ControlType.UcOsteoMuscular:
                    var ucOsteo = new ucOsteoMuscular { Name = field.v_ComponentFieldId };
                    ctl = ucOsteo;
                    break;


                #endregion

                case ControlType.Lista:
                    var cb = new ComboBox()
                    {
                        Width = field.i_ControlWidth,
                        Height = field.i_HeightControl,
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Name = field.v_ComponentFieldId
                    };
                    var dataLista = _cboEso.Find(p => p.Id == field.i_GroupId.ToString()).Items;
                    var Listas = ConvertClassToStruct(dataLista);
                    Utils.LoadDropDownList(cb, "Value1", "Id", Listas, DropDownListAction.Select);

                    if (field.i_IsRequired == (int)SiNo.SI)
                    {
                        SetControlValidate(field.i_ControlId, cb, null, null, validator);
                    }

                    // Setear levantamiento de popup para el ingreso de los hallazgos solo cuando 
                    // se seleccione un valor alterado

                    if ((field.v_ComponentId == Constants.EXAMEN_FISICO_ID
                                                  || field.v_ComponentId == Constants.RX_TORAX_ID
                                                  || field.v_ComponentId == Constants.OFTALMOLOGIA_ID
                                                  || field.v_ComponentId == Constants.ALTURA_ESTRUCTURAL_ID
                                                  || field.v_ComponentId == Constants.TACTO_RECTAL_ID
                                                  || field.v_ComponentId == Constants.EVAL_NEUROLOGICA_ID
                                                  || field.v_ComponentId == Constants.TEST_ROMBERG_ID
                                                  || field.v_ComponentId == Constants.TAMIZAJE_DERMATOLOGIO_ID
                                                  || field.v_ComponentId == Constants.GINECOLOGIA_ID
                                                  || field.v_ComponentId == Constants.EXAMEN_MAMA_ID
                                                  || field.v_ComponentId == Constants.AUDIOMETRIA_ID
                                                  || field.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID
                                                  || field.v_ComponentId == Constants.ESPIROMETRIA_ID
                                                  || field.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1
                                                  || field.v_ComponentId == Constants.PRUEBA_ESFUERZO_ID
                                                  || field.v_ComponentId == Constants.TAMIZAJE_DERMATOLOGIO_ID
                                                  || field.v_ComponentId == Constants.ODONTOGRAMA_ID
                                                  || field.v_ComponentId == Constants.EXAMEN_FISICO_7C_ID)
                                                  && (field.i_GroupId == (int)SystemParameterGroups.ConHallazgoSinHallazgosNoSeRealizo))
                    {
                        cb.SelectedIndexChanged += new EventHandler(cb_SelectedIndexChanged);
                    }

                    cb.Enter += Capture_Value;
                    cb.Leave += txt_Leave;

                    if (_action == "View")
                    {
                        cb.Enabled = false;
                    }

                    ctl = cb;
                    break;

                default:
                    MessageBox.Show("ALERT", field.i_ControlId.ToString());
                    break;

                #endregion
            }

            if (ctl != null)
                ctl.Tag = new KeyTagControl
                {
                    i_ControlId = field.i_ControlId,
                    v_ComponentId = field.v_ComponentId,
                    v_ComponentFieldsId = field.v_ComponentFieldId,
                    i_IsSourceFieldToCalculate = field.i_IsSourceFieldToCalculate,
                    v_Formula = field.v_Formula,
                    v_TargetFieldOfCalculateId = field.v_TargetFieldOfCalculateId,
                    v_SourceFieldToCalculateJoin = field.v_SourceFieldToCalculateJoin,
                    v_FormulaChild = field.v_FormulaChild,
                    Formula = field.Formula,
                    TargetFieldOfCalculateId = field.TargetFieldOfCalculateId,
                    v_TextLabel = field.v_TextLabel,
                    v_ComponentName = component.v_Name
                };

            return ctl;
        }

        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (_cancelEventSelectedIndexChange)
                return;

            var tagCtrl = (KeyTagControl)((ComboBox)sender).Tag;
            var componentId = tagCtrl.v_ComponentId;
            var value1 = int.Parse(((ComboBox)sender).SelectedValue.ToString());


            if (value1 == (int)NormalAlterado.Alterado)
            {
                Operations.Popups.frmRegisterFinding frm = null;


                if (componentId != Constants.EXAMEN_FISICO_7C_ID)
                {
                    frm = new Operations.Popups.frmRegisterFinding(tagCtrl.v_ComponentName, "", tagCtrl.v_TextLabel);
                    frm.ShowDialog();

                    if (frm.DialogResult == DialogResult.Cancel)
                        return;
                }

                TextBox field = null;
                TextBox txtDes = null;

                #region Obtener campo Hallazgo

                if (componentId == Constants.EXAMEN_FISICO_ID)
                {
                    field = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_HALLAZGOS_ID)[0];

                    #region Hallazgos

                    if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_PIEL_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_PIEL_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_CABELLO_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_CABELLO_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_OJOSANEXOS_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_OJOSANEXOS_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_OIDOS_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_OIDOS_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_NARIZ_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_NARIZ_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_BOCA_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_BOCA_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_FARINGE_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_FARINGE_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_CUELLO_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_CUELLO_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_APARATORESPIRATORIO_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_APARATO_RESPIRATORIO_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_CARDIO_VASCULAR_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_CARDIO_VASCULAR_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_GENITOURINARIO_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_MARCHA_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_MARCHA_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_COLMNA_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_COLUMNA_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_EXTREMIDADE_SUPERIORES_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_EXTREMIDADES_SUPERIORES_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_LINFATICOS_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_LINFATICOS_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_ECTOSCOPIA_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_ECTOSCOPIA_GENERAL_DESCRIPCION_ID)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_ESTADO_METAL_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_ESTADO_METAL_DESCRIPCION_ID)[0];
                    }

                    #endregion

                    if (txtDes != null)
                        txtDes.Text = frm.FindingText.Substring(frm.FindingText.IndexOf(':') + 2);

                }
                else if (componentId == Constants.RX_TORAX_ID)
                {


                    field = (TextBox)FindControlInCurrentTab(Constants.RX_HALLAZGOS)[0];

                    if (tagCtrl.v_ComponentFieldsId == Constants.RX_VERTICES_COMBO_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.RX_VERTICES_ID)[0];
                    }

                    if (tagCtrl.v_ComponentFieldsId == Constants.RX_CAMPOS_PULMONARES_COMBO_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.RX_CAMPOS_PULMONARES_ID)[0];
                    }

                    if (tagCtrl.v_ComponentFieldsId == Constants.RX_HILOS_COMBO_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.RX_HILOS_ID)[0];
                    }

                    if (tagCtrl.v_ComponentFieldsId == Constants.RX_COSTO_ODIAFRAGMATICO_COMBO_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.RX_COSTO_ODIAFRAGMATICO_ID)[0];
                    }

                    if (tagCtrl.v_ComponentFieldsId == Constants.RX_SENOS_CARDIOFRENICOS_COMBO_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.RX_SENOS_CARDIOFRENICOS_DESCRIPCION_ID)[0];
                    }

                    if (tagCtrl.v_ComponentFieldsId == Constants.RX_MEDIASTINOS_COMBO_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.RX_MEDIASTINOS_DESCRIPCION_ID)[0];
                    }

                    if (tagCtrl.v_ComponentFieldsId == Constants.RX_SILUETA_CARDIACA_COMBO_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.RX_SILUETA_CARDIACA_DESCRIPCION_ID)[0];
                    }

                    if (tagCtrl.v_ComponentFieldsId == Constants.RX_INDICE_CARDIACO_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.RX_INDICE_CARDIACO_DESCRIPCION_ID)[0];
                    }

                    if (tagCtrl.v_ComponentFieldsId == Constants.RX_PARTES_BLANDAS_OSEAS_COMBO_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.RX_PARTES_BLANDAS_OSEAS_ID)[0];
                    }
                    if (txtDes != null)
                        txtDes.Text = frm.FindingText.Substring(frm.FindingText.IndexOf(':') + 2);

                }
                else if (componentId == Constants.OFTALMOLOGIA_ID)
                {
                    field = (TextBox)FindControlInCurrentTab(Constants.OFTALMOLOGIA_HALLAZGOS_ID)[0];
                }
                //else if (componentId == Constants.ALTURA_ESTRUCTURAL_ID)
                //{
                //    field = (TextBox)FindControlInCurrentTab(Constants.ALTURA_ESTRUCTURAL_HALLAZGOS)[0];
                //}
                else if (componentId == Constants.TACTO_RECTAL_ID)
                {
                    field = (TextBox)FindControlInCurrentTab(Constants.TACTO_RECTAL_HALLAZGOS)[0];
                }
                else if (componentId == Constants.EVAL_NEUROLOGICA_ID)
                {
                    field = (TextBox)FindControlInCurrentTab(Constants.EVAL_NEUROLOGICA_HALLAZGOS)[0];
                }
                else if (componentId == Constants.TEST_ROMBERG_ID)
                {
                    field = (TextBox)FindControlInCurrentTab(Constants.TEST_ROMBERG_HALLAZGOS_ID)[0];
                }
                else if (componentId == Constants.TAMIZAJE_DERMATOLOGIO_ID)
                {
                    field = (TextBox)FindControlInCurrentTab(Constants.TAMIZAJE_DERMATOLOGIO_DESCRIPCION1_ID)[0];
                }
                else if (componentId == Constants.GINECOLOGIA_ID)
                {
                    field = (TextBox)FindControlInCurrentTab(Constants.GINECOLOGIA_HALLAZGOS_ID)[0];
                }
                else if (componentId == Constants.EXAMEN_MAMA_ID)
                {
                    field = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_MAMA_HALLAZGOS_ID)[0];
                }
                else if (componentId == Constants.AUDIOMETRIA_ID)
                {
                    field = (TextBox)FindControlInCurrentTab(Constants.AUDIOMETRIA_CONCLUSIONES_ID)[0];
                }
                else if (componentId == Constants.ELECTROCARDIOGRAMA_ID)
                {
                    field = (TextBox)FindControlInCurrentTab(Constants.ELECTROCARDIOGRAMA_DESCRIPCION_ID)[0];
                }
                else if (componentId == Constants.ESPIROMETRIA_ID)
                {
                    field = (TextBox)FindControlInCurrentTab(Constants.ESPIROMETRIA_FUNCIÓN_RESPIRATORIA_ABS_OBSERVACION)[0];
                }
                else if (componentId == Constants.OSTEO_MUSCULAR_ID_1)
                {
                    field = (TextBox)FindControlInCurrentTab(Constants.DESCRIPCION)[0];
                }
                else if (componentId == Constants.PRUEBA_ESFUERZO_ID)
                {
                    field = (TextBox)FindControlInCurrentTab(Constants.PRUEBA_ESFUERZO_DESCRIPCION_ID)[0];
                }
                else if (componentId == Constants.ODONTOGRAMA_ID)
                {
                    field = (TextBox)FindControlInCurrentTab(Constants.ODONTOGRAMA_CONCLUSIONES_DESCRIPCION_ID)[0];
                }
                else if (componentId == Constants.EXAMEN_FISICO_7C_ID)
                {
                    field = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_7C_HALLAZGOS_ID)[0];

                    #region Hallazgos

                    if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_7C_CABEZA_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_CABEZA_DESCRIPCION)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_7C_CUELLO_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_CUELLO_DESCRIPCION)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_7C_NARIZ_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_NARIZ_DESCRIPCION)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_7C_BOCA_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_BOCA_ADMIGDALA_FARINGE_LARINGE_DESCRIPCION)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_7C_REFLEJOS_PUPILARES_ID)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_REFLEJOS_PUPILARES_DESCRIPCION)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_7C_MIEMBROS_SUPERIORES)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MIEMBROS_SUPERIORES_DESCRIPCION)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_7C_MIEMBROS_INFERIORES)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MIEMBROS_INFERIORES_DESCRIPCION)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_7C_REFLEJOS_OSTEO)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_REFLEJOS_OSTEO_TENDINOSOS_DESCRIPCION)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_7C_MARCHA)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MARCHA_DESCRIPCION)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_7C_COLUMNA)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_COLUMNA_DESCRIPCION)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_7C_ABDOMEN)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_7C_EXAMENFISICO_ABDOMEN_DESCRIPCION)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_7C_ANILLOS_IMGUINALES)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_7C_EXAMENFISICO_ANILLOS_INGUINALES_DESCRIPCION)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_7C_HERNIAS)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_HERNIAS_DESCRIPCION)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_7C_VARICES)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_VARICES_DESCRIPCION)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_7C_ORGANOS_GENITALES)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_ORGANOS_GENITALES_DESCRIPCION)[0];
                    }
                    else if (tagCtrl.v_ComponentFieldsId == Constants.EXAMEN_FISICO_7C_GANGLIOS)
                    {
                        txtDes = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_GANGLIOS_DESCRIPCION)[0];
                    }

                    #endregion

                    frm = new Operations.Popups.frmRegisterFinding(tagCtrl.v_ComponentName, txtDes.Text, tagCtrl.v_TextLabel, Constants.EXAMEN_FISICO_7C_ID);

                    frm.ShowDialog();

                    if (frm.DialogResult == DialogResult.Cancel)
                        return;

                    txtDes.Text = frm.FindingText.Substring(frm.FindingText.IndexOf(':') + 2);

                }


                #endregion

                if (field != null)
                {
                    #region Escribir en el campo hallazgo

                    StringBuilder sb = new StringBuilder();

                    if (field.Text == string.Empty)
                    {
                        sb.Append(frm.FindingText);
                    }
                    else
                    {
                        sb.Append(field.Text);
                        sb.Append("\r\n");
                        sb.Append(frm.FindingText);
                    }

                    field.Text = sb.ToString();

                    #endregion

                }
            }
        }

        private Control[] FindControlInCurrentTab(string key)
        {
            // Obtener TabPage actual
            var currentTabPage = tcExamList.SelectedTab.TabPage;
            var findControl = currentTabPage.Controls.Find(key, true);
            return findControl;
        }

        private static List<Sigesoft.Node.WinClient.UI.Operations.FrmEsoV2.StructKeyDto> ConvertClassToStruct(IEnumerable<KeyValueDTO> data)
        {
            return data.Select(item => new Sigesoft.Node.WinClient.UI.Operations.FrmEsoV2.StructKeyDto
            {
                Value1 = item.Value1,
                Id = item.Id
            }).ToList();
        }

        private void txt_ValueChanged(object sender, EventArgs e)
        {
            if (flagValueChange)
            {
                // Capturar el control invocador
                Control senderCtrl = (Control)sender;
                // Obtener información contenida en la propiedad Tag del control invocante
                var tagCtrl = (KeyTagControl)senderCtrl.Tag;
                string valueToAnalyze = GetValueControl(tagCtrl.i_ControlId, senderCtrl);
                int isSourceField = tagCtrl.i_IsSourceFieldToCalculate;
                Dictionary<string, object> Params = null;
                List<double> evalExpResultList = new List<double>();

                ////MessageBox.Show(senderCtrl.Text);
                if (isSourceField == (int)SiNo.SI)
                {
                    #region Nueva logica de calculo de formula soporta n parametros

                    // Recorrer las formulas en las cuales el campo esta referenciado
                    foreach (var formu in tagCtrl.Formula)
                    {
                        // Obtener Campos fuente participantes en el calculo
                        var sourceFields = Common.Utils.GetTextFromExpressionInCorchete(formu.v_Formula);
                        Params = new Dictionary<string, object>();

                        foreach (string sf in sourceFields)
                        {
                            // Buscar controles fuentes
                            var findCtrlResult = FindDynamicControl(sf);
                            var length = findCtrlResult.Length;
                            // La busqueda si tuvo exito
                            if (length != 0)
                            {
                                // Obtener información del control encontrado 
                                var tagSourceField = (KeyTagControl)findCtrlResult[0].Tag;
                                // Obtener el tipo de dato al cual se va castear un control encontrado
                                string dtc = GetDataTypeControl(tagSourceField.i_ControlId);
                                // Obtener value del control encontrado
                                var value = GetValueControl(tagSourceField.i_ControlId, findCtrlResult[0]);

                                if (dtc == "int")
                                {
                                    //var ival = int.Parse(value);
                                    Params[sf] = int.Parse(value);
                                }
                                else if (dtc == "double")
                                {
                                    Params[sf] = double.Parse(value);
                                }
                            }
                            else
                            {
                                if (sf.ToUpper() == "EDAD")
                                {
                                    Params[sf] = _age;
                                }
                                else if (sf.ToUpper() == "GENERO_2")
                                {
                                    Params[sf] = _sexType == Gender.FEMENINO ? 0 : 1;
                                }
                                else if (sf.ToUpper() == "GENERO_1")
                                {
                                    Params[sf] = _sexType == Gender.MASCULINO ? 0 : 1;
                                }
                            }

                        } // fin foreach sourceFields

                        bool isFound = false;

                        // Buscar algun cero
                        foreach (var item in Params)
                        {
                            if (item.Value.ToString() == "0" &&
                                item.Key != "EDAD" &&
                                item.Key != "GENERO_1" &&
                                item.Key != "GENERO_2")
                            {
                                isFound = true;
                                break;
                            }
                        }

                        if (!isFound)
                        {
                            var evalExpResult = Common.Utils.EvaluateExpression(formu.v_Formula, Params);
                            evalExpResultList.Add(evalExpResult);
                        }

                    } // fin foreach Formula

                    // Mostrar el resultado en el control indicado
                    if (evalExpResultList.Count != 0)
                    {
                        for (int i = 0; i < tagCtrl.TargetFieldOfCalculateId.Count; i++)
                        {
                            var targetFieldOfCalculate1 = FindDynamicControl(tagCtrl.TargetFieldOfCalculateId[i].v_TargetFieldOfCalculateId);

                            for (int j = 0; j < evalExpResultList.Count; j++)
                            {
                                if (i == j)
                                {
                                    targetFieldOfCalculate1[0].Text = evalExpResultList[j].ToString();
                                }
                            }
                        }
                    }

                    #endregion

                }

                GeneratedAutoDX(valueToAnalyze, senderCtrl, tagCtrl);

            }

        }

        private void GeneratedAutoDX(string valueToAnalyze, Control senderCtrl, KeyTagControl tagCtrl)
        {
            string componentFieldsId = tagCtrl.v_ComponentFieldsId;

            // Retorna el DX (automático) generado, luego de una serie de evaluaciones.
            var diagnosticRepository = SearchDxSugeridoOfSystem(valueToAnalyze, componentFieldsId);

            DiagnosticRepositoryList findControlResult = null;

            if (_tmpExamDiagnosticComponentList != null)
            {
                // Buscar control que haya generado algun DX automático
                findControlResult = _tmpExamDiagnosticComponentList.Find(p => p.v_ComponentFieldsId == componentFieldsId && p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
            }

            // Remover DX (automático) encontrado.
            if (findControlResult != null)
            {
                if (findControlResult.i_RecordType == (int)RecordType.Temporal)
                    _tmpExamDiagnosticComponentList.Remove(findControlResult);
                else
                    findControlResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
            }

            // Si se generó un DX (automático).
            if (diagnosticRepository != null)
            {
                // Setear v_ComponentFieldValuesId en mi variable de información TAG
                tagCtrl.v_ComponentFieldValuesId = diagnosticRepository.v_ComponentFieldValuesId;

                // Pintar de rojo el fondo del control que generó el DX (automático) 
                // en caso hubiera una alteracion si es normal NO se pinta.               
                senderCtrl.BackColor = Color.Pink;   // DX Alterado              

                if (_tmpExamDiagnosticComponentList != null)
                {
                    // Se agrega el DX obtenido a la lista de DX general.
                    _tmpExamDiagnosticComponentList.Add(diagnosticRepository);
                }
                else
                {
                    _tmpExamDiagnosticComponentList = new List<DiagnosticRepositoryList>();
                    _tmpExamDiagnosticComponentList.Add(diagnosticRepository);
                }
            }
            else        // No
            {
                senderCtrl.BackColor = Color.White;
            }

            if (_tmpExamDiagnosticComponentList != null)
            {
                // Filtar para Mostrar en la grilla solo registros que no están eliminados
                var dataList = _tmpExamDiagnosticComponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                // Refrescar grilla                        
                grdDiagnosticoPorExamenComponente.DataSource = dataList;
                lblRecordCountDiagnosticoPorExamenCom.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }
        }

        private DiagnosticRepositoryList SearchDxSugeridoOfSystem_OLD(string valueToAnalyze, string pComponentFieldsId)
        {
            DiagnosticRepositoryList diagnosticRepository = null;
            string matchValId = null;
            bool exitLoop = false;
            var componentField = _tmpServiceComponentsForBuildMenuList
                                .Find(p => p.v_ComponentId == _componentId)
                                .Fields.Find(p => p.v_ComponentFieldId == pComponentFieldsId);

            if (componentField != null)
            {
                // Obtener el tipo de dato al cual se va castear un control especifico
                string dataTypeControlToParse = GetDataTypeControl(componentField.i_ControlId);

                if (componentField != null)
                {
                    foreach (ComponentFieldValues val in componentField.Values)
                    {
                        switch ((Operator2Values)val.i_OperatorId)
                        {
                            #region Analizar valor ingresado x el medico contra una serie de valores k se obtinen desde la BD

                            case Operator2Values.X_esIgualque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) == int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) == double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_noesIgualque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) != int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) != double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMenorque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) < int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    // X < 18.5 (bajo peso)
                                    if (double.Parse(valueToAnalyze) < double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMenorIgualque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) <= int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    // X < 18.5 (bajo peso)
                                    if (double.Parse(valueToAnalyze) <= double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) > int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) > double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorIgualque_A:
                                // X >= 40.0 (Obesidad clase III)
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) >= int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    // X < 18.5 (bajo peso)
                                    if (double.Parse(valueToAnalyze) >= double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorque_A_yMenorque_B:

                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) > int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyze) < int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) > double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyze) < double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorque_A_yMenorIgualque_B:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) > int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyze) <= int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    // X < A && X <= B 
                                    if (double.Parse(valueToAnalyze) > double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyze) <= double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorIgualque_A_yMenorque_B:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) >= int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyze) < int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) >= double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyze) < double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorIgualque_A_yMenorIgualque_B:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) >= int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyze) <= int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    var parse = double.Parse(valueToAnalyze);
                                    if (double.Parse(valueToAnalyze) >= double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyze) <= double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            default:
                                MessageBox.Show("valor no encontrado " + valueToAnalyze);
                                break;

                            #endregion
                        }

                        if (exitLoop)
                        {
                            #region CREAR / AGREGAR DX (automático)

                            matchValId = val.v_ComponentFieldValuesId;

                            // Si el valor analizado se encuentra en el rango de valores NORMALES, 
                            // entonces NO se genera un DX (automático).
                            if (val.v_DiseasesId == null)
                                break;

                            val.Recomendations.ForEach(item => { item.v_RecommendationId = Guid.NewGuid().ToString(); });
                            val.Restrictions.ForEach(item => { item.v_RestrictionByDiagnosticId = Guid.NewGuid().ToString(); });
                            // Insertar DX sugerido (automático) a la bolsa de DX 
                            diagnosticRepository = new DiagnosticRepositoryList();
                            diagnosticRepository.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                            diagnosticRepository.v_DiseasesId = val.v_DiseasesId;
                            diagnosticRepository.i_AutoManualId = (int)AutoManual.Automático;

                            diagnosticRepository.i_PreQualificationId = (int)PreQualification.SinPreCalificar;


                            //if (val.v_DiseasesId == "N009-DD000000789" || val.v_DiseasesId == "N009-DD000000788" || val.v_DiseasesId == "N009-DD000000787" || val.v_DiseasesId == "N009-DD000000786" || val.v_DiseasesId == "N009-DD000000785"
                            //    || val.v_DiseasesId == "N009-DD000000784" || val.v_DiseasesId == "N009-DD000000783" || val.v_DiseasesId == "N009-DD000000782" || val.v_DiseasesId == "N009-DD000000780" || val.v_DiseasesId == "N009-DD000000779"
                            //    || val.v_DiseasesId == "N009-DD000000778" || val.v_DiseasesId == "N009-DD000000777" || val.v_DiseasesId == "N009-DD000000776" || val.v_DiseasesId == "N009-DD000000775" || val.v_DiseasesId == "N009-DD000000774"
                            //    || val.v_DiseasesId == "N009-DD000000661" || val.v_DiseasesId == "N009-DD000000505" || val.v_DiseasesId == "N009-DD000000484" || val.v_DiseasesId == "N009-DD000000483" || val.v_DiseasesId == "N009-DD000000771"
                            //    || val.v_DiseasesId == "N009-DD000000662" || val.v_DiseasesId == "N009-DD000000795" || val.v_DiseasesId == "N009-DD000000797" || val.v_DiseasesId == "N009-DD000000799")
                            //{
                            //if (val.v_CIE10 == "Z000" || val.v_CIE10 == "Z001")
                            //{
                            //    diagnosticRepository.i_DiagnosticTypeId = (int)TipoDx.Normal;
                            //    diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.Descartado;
                            //}
                            //else
                            //{
                            //    diagnosticRepository.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;
                            //    diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                            //}


                            diagnosticRepository.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;
                            diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.Descartado;

                            diagnosticRepository.v_ServiceId = _serviceId;
                            diagnosticRepository.v_ComponentId = val.v_ComponentId;
                            diagnosticRepository.v_DiseasesName = val.v_DiseasesName;
                            diagnosticRepository.v_AutoManualName = "AUTOMÁTICO";
                            diagnosticRepository.v_RestrictionsName = ConcatenateRestrictions(val.Restrictions);
                            diagnosticRepository.v_RecomendationsName = ConcatenateRecommendations(val.Recomendations);
                            diagnosticRepository.v_PreQualificationName = "SIN PRE-CALIFICAR";
                            // ID enlace DX automatico para grabar valores dinamicos
                            diagnosticRepository.v_ComponentFieldValuesId = val.v_ComponentFieldValuesId;
                            diagnosticRepository.v_ComponentFieldsId = pComponentFieldsId;
                            diagnosticRepository.Recomendations = RefreshRecomendationList(val.Recomendations);
                            diagnosticRepository.Restrictions = RefreshRestrictionList(val.Restrictions);
                            diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
                            diagnosticRepository.i_RecordType = (int)RecordType.Temporal;

                            int vm = val.i_ValidationMonths == null ? 0 : val.i_ValidationMonths.Value;
                            diagnosticRepository.d_ExpirationDateDiagnostic = DateTime.Now.AddMonths(vm);

                            #endregion
                            //#region CREAR / AGREGAR DX (automático)

                            //matchValId = val.v_ComponentFieldValuesId;

                            //// Si el valor analizado se encuentra en el rango de valores NORMALES, 
                            //// entonces NO se genera un DX (automático).
                            //if (val.v_DiseasesId == null)
                            //    break;

                            //val.Recomendations.ForEach(item => { item.v_RecommendationId = Guid.NewGuid().ToString(); });
                            //val.Restrictions.ForEach(item => { item.v_RestrictionByDiagnosticId = Guid.NewGuid().ToString(); });
                            //// Insertar DX sugerido (automático) a la bolsa de DX 
                            //diagnosticRepository = new DiagnosticRepositoryList();
                            //diagnosticRepository.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                            //diagnosticRepository.v_DiseasesId = val.v_DiseasesId;
                            //diagnosticRepository.i_AutoManualId = (int)AutoManual.Automático;
                            //diagnosticRepository.i_PreQualificationId = (int)PreQualification.SinPreCalificar;
                            //diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.SinCalificar;
                            //diagnosticRepository.v_ServiceId = _serviceId;
                            //diagnosticRepository.v_ComponentId = val.v_ComponentId;
                            //diagnosticRepository.v_DiseasesName = val.v_DiseasesName;
                            //diagnosticRepository.v_AutoManualName = "AUTOMÁTICO";
                            //diagnosticRepository.v_RestrictionsName = ConcatenateRestrictions(val.Restrictions);
                            //diagnosticRepository.v_RecomendationsName = ConcatenateRecommendations(val.Recomendations);
                            //diagnosticRepository.v_PreQualificationName = "SIN PRE-CALIFICAR";
                            //// ID enlace DX automatico para grabar valores dinamicos
                            //diagnosticRepository.v_ComponentFieldValuesId = val.v_ComponentFieldValuesId;
                            //diagnosticRepository.v_ComponentFieldsId = pComponentFieldsId;
                            //diagnosticRepository.Recomendations = RefreshRecomendationList(val.Recomendations);
                            //diagnosticRepository.Restrictions = RefreshRestrictionList(val.Restrictions);
                            //diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
                            //diagnosticRepository.i_RecordType = (int)RecordType.Temporal;

                            //int vm = val.i_ValidationMonths == null ? 0 : val.i_ValidationMonths.Value;
                            //diagnosticRepository.d_ExpirationDateDiagnostic = DateTime.Now.AddMonths(vm);

                            //#endregion
                            break;
                        }

                    }
                }
            }

            return diagnosticRepository;

        }

        private DiagnosticRepositoryList SearchDxSugeridoOfSystem(string valueToAnalyze, string pComponentFieldsId)
        {
            DiagnosticRepositoryList diagnosticRepository = null;
            string matchValId = null;
            bool exitLoop = false;
            var componentField = _tmpServiceComponentsForBuildMenuList
                                .Find(p => p.v_ComponentId == _componentId)
                                .Fields.Find(p => p.v_ComponentFieldId == pComponentFieldsId);

            if (componentField != null)
            {
                // Obtener el tipo de dato al cual se va castear un control especifico
                string dataTypeControlToParse = GetDataTypeControl(componentField.i_ControlId);

                if (componentField != null)
                {
                    foreach (ComponentFieldValues val in componentField.Values)
                    {
                        switch ((Operator2Values)val.i_OperatorId)
                        {
                            #region Analizar valor ingresado x el medico contra una serie de valores k se obtinen desde la BD

                            case Operator2Values.X_esIgualque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) == int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) == double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_noesIgualque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) != int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) != double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMenorque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) < int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    // X < 18.5 (bajo peso)
                                    if (double.Parse(valueToAnalyze) < double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMenorIgualque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) <= int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    // X < 18.5 (bajo peso)
                                    if (double.Parse(valueToAnalyze) <= double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) > int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) > double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorIgualque_A:
                                // X >= 40.0 (Obesidad clase III)
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) >= int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    // X < 18.5 (bajo peso)
                                    if (double.Parse(valueToAnalyze) >= double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorque_A_yMenorque_B:

                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) > int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyze) < int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) > double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyze) < double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorque_A_yMenorIgualque_B:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) > int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyze) <= int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    // X < A && X <= B 
                                    if (double.Parse(valueToAnalyze) > double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyze) <= double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorIgualque_A_yMenorque_B:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) >= int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyze) < int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) >= double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyze) < double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorIgualque_A_yMenorIgualque_B:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) >= int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyze) <= int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    var parse = double.Parse(valueToAnalyze);
                                    if (double.Parse(valueToAnalyze) >= double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyze) <= double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            default:
                                MessageBox.Show("valor no encontrado " + valueToAnalyze);
                                break;

                            #endregion
                        }

                        if (exitLoop)
                        {
                            #region CREAR / AGREGAR DX (automático)

                            matchValId = val.v_ComponentFieldValuesId;

                            // Si el valor analizado se encuentra en el rango de valores NORMALES, 
                            // entonces NO se genera un DX (automático).
                            if (val.v_DiseasesId == null)
                                break;

                            val.Recomendations.ForEach(item => { item.v_RecommendationId = Guid.NewGuid().ToString(); });
                            val.Restrictions.ForEach(item => { item.v_RestrictionByDiagnosticId = Guid.NewGuid().ToString(); });
                            // Insertar DX sugerido (automático) a la bolsa de DX 
                            diagnosticRepository = new DiagnosticRepositoryList();
                            diagnosticRepository.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                            diagnosticRepository.v_DiseasesId = val.v_DiseasesId;
                            diagnosticRepository.i_AutoManualId = (int)AutoManual.Automático;

                            diagnosticRepository.i_PreQualificationId = (int)PreQualification.SinPreCalificar;


                            diagnosticRepository.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;
                            diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.Descartado;

                            diagnosticRepository.v_ServiceId = _serviceId;
                            diagnosticRepository.v_ComponentId = val.v_ComponentId;
                            diagnosticRepository.v_DiseasesName = val.v_DiseasesName;
                            diagnosticRepository.v_AutoManualName = "AUTOMÁTICO";
                            diagnosticRepository.v_RestrictionsName = ConcatenateRestrictions(val.Restrictions);
                            diagnosticRepository.v_RecomendationsName = ConcatenateRecommendations(val.Recomendations);
                            diagnosticRepository.v_PreQualificationName = "SIN PRE-CALIFICAR";
                            // ID enlace DX automatico para grabar valores dinamicos
                            diagnosticRepository.v_ComponentFieldValuesId = val.v_ComponentFieldValuesId;
                            diagnosticRepository.v_ComponentFieldsId = pComponentFieldsId;
                            diagnosticRepository.Recomendations = RefreshRecomendationList(val.Recomendations);
                            diagnosticRepository.Restrictions = RefreshRestrictionList(val.Restrictions);
                            diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
                            diagnosticRepository.i_RecordType = (int)RecordType.Temporal;

                            int vm = val.i_ValidationMonths == null ? 0 : val.i_ValidationMonths.Value;
                            diagnosticRepository.d_ExpirationDateDiagnostic = DateTime.Now.AddMonths(vm);

                            #endregion

                            break;
                        }

                    }
                }
            }

            return diagnosticRepository;

        }


        private List<RestrictionList> RefreshRestrictionList(List<RestrictionList> prestrictions)
        {
            var restrictionsList = new List<RestrictionList>();

            foreach (var item in prestrictions)
            {
                // Agregar restricciones (Automáticas) a la Lista mas lo que ya tiene
                RestrictionList restriction = new RestrictionList();

                restriction.v_RestrictionByDiagnosticId = item.v_RestrictionByDiagnosticId;
                restriction.v_ServiceId = _serviceId;
                restriction.v_DiagnosticRepositoryId = item.v_DiagnosticRepositoryId;
                restriction.v_MasterRestrictionId = item.v_MasterRestrictionId;
                restriction.v_RestrictionName = item.v_RestrictionName;
                restriction.i_RecordStatus = (int)RecordStatus.Agregado;
                restriction.i_RecordType = (int)RecordType.Temporal;
                restriction.v_ComponentId = item.v_ComponentId;

                restrictionsList.Add(restriction);
            }

            return restrictionsList;
        }

        private List<RecomendationList> RefreshRecomendationList(List<RecomendationList> precomendations)
        {
            var recomendationsList = new List<RecomendationList>();

            foreach (var item in precomendations)
            {
                // Agregar restricciones a la Lista mas lo que ya tiene
                RecomendationList recomendation = new RecomendationList();

                recomendation.v_RecommendationId = item.v_RecommendationId;
                recomendation.v_ServiceId = _serviceId;
                recomendation.v_DiagnosticRepositoryId = item.v_DiagnosticRepositoryId;
                recomendation.v_RecommendationId = item.v_RecommendationId;
                recomendation.v_MasterRecommendationId = item.v_MasterRecommendationId;  // ID -> RECOME / RESTRIC (BOLSA CONFIG POR M. MENDEZ)
                recomendation.v_RecommendationName = item.v_RecommendationName;
                recomendation.i_RecordStatus = (int)RecordStatus.Agregado;
                recomendation.i_RecordType = (int)RecordType.Temporal;
                recomendation.v_ComponentId = item.v_ComponentId;

                recomendationsList.Add(recomendation);
            }

            return recomendationsList;
        }

        private string ConcatenateRecommendations(List<RecomendationList> precomendations)
        {
            if (precomendations == null)
                return string.Empty;

            var qry = (from a in precomendations  // RESTRICCIONES POR Diagnosticos                                           
                where a.i_RecordStatus != (int)RecordStatus.EliminadoLogico
                select new
                {
                    v_RecommendationName = a.v_RecommendationName
                }).ToList();

            return string.Join(", ", qry.Select(p => p.v_RecommendationName));
        }

        private string ConcatenateRestrictions(List<RestrictionList> prestrictions)
        {
            if (prestrictions == null)
                return string.Empty;

            var qry = (from a in prestrictions  // RESTRICCIONES POR Diagnosticos                                           
                where a.i_RecordStatus != (int)RecordStatus.EliminadoLogico
                select new
                {
                    v_RestrictionsName = a.v_RestrictionName
                }).ToList();

            return string.Join(", ", qry.Select(p => p.v_RestrictionsName));
        }

        private string GetDataTypeControl(int ControlId)
        {
            string dataType = null;

            switch ((ControlType)ControlId)
            {
                case ControlType.NumeroEntero:
                    dataType = "int";
                    break;
                case ControlType.NumeroDecimal:
                    dataType = "double";
                    break;
                case ControlType.SiNoCheck:
                    dataType = "int";
                    break;
                case ControlType.SiNoRadioButton:
                    break;
                case ControlType.Radiobutton:
                    break;
                case ControlType.SiNoCombo:
                    break;
                case ControlType.Lista:
                    dataType = "int";
                    break;
                default:
                    break;
            }

            return dataType;
        }

        private Control[] FindDynamicControl(string key)
        {
            // Obtener TabPage actual
            var currentTabPage = tcExamList.SelectedTab.TabPage;
            //var findControl = currentTabPage.Controls.Find(key, true);

            var findControl = tcExamList.Tabs.TabControl.Controls.Find(key, true);

            return findControl;
        }

        private void Capture_Value(object sender, EventArgs e)
        {
            Control senderCtrl = (Control)sender;
            // Obtener información contenida en la propiedad Tag del control invocante
            var tagCtrl = (KeyTagControl)senderCtrl.Tag;
            // Capturar valor inicial
            //_oldValue = GetValueControl(tagCtrl.i_ControlId, senderCtrl);
            if (tagCtrl.v_ComponentId == "N009-ME000000062")
            {
                GroupBox gb = null;
                gb = (GroupBox)FindControlInCurrentTab("gb_C 3. Forma y Tamaño: (Consulte las radiografías estandar, se requieres dos símbolos; marque un primario y un secundario)")[0];

                if (tagCtrl.v_ComponentFieldsId == "N002-MF000000222")
                {
                    gb.Enabled = false;
                }
                else if (tagCtrl.v_ComponentFieldsId == "N002-MF000000220" || tagCtrl.v_ComponentFieldsId == "N002-MF000000221" || tagCtrl.v_ComponentFieldsId == "N002-MF000000223" || tagCtrl.v_ComponentFieldsId == "N009-MF000000720"
                    || tagCtrl.v_ComponentFieldsId == "N009-MF000000721" || tagCtrl.v_ComponentFieldsId == "N009-MF000000722" || tagCtrl.v_ComponentFieldsId == "N009-MF000000723" || tagCtrl.v_ComponentFieldsId == "N009-MF000000724"
                    || tagCtrl.v_ComponentFieldsId == "N009-MF000000725" || tagCtrl.v_ComponentFieldsId == "N009-MF000000726"
                    || tagCtrl.v_ComponentFieldsId == "N009-MF000000727")
                {
                    gb.Enabled = true;
                }

                GroupBox gb41 = null;
                GroupBox gb42 = null;
                gb41 = (GroupBox)FindControlInCurrentTab("gb_C 4.1 Placa Pleurales")[0];
                gb42 = (GroupBox)FindControlInCurrentTab("gb_C 4.2 Engrosamiento Difuso de la Pleura")[0];
                if (tagCtrl.v_ComponentFieldsId == "N009-MF000003194")
                {
                    gb41.Enabled = true;
                    gb42.Enabled = true;

                }
                else if (tagCtrl.v_ComponentFieldsId == "N009-MF000000761")
                {
                    gb41.Enabled = false;
                    gb42.Enabled = false;
                }

                GroupBox gb6 = null;
                gb6 = (GroupBox)FindControlInCurrentTab("gb_C 6. MARQUE  la respuesta adecuada; si marca \"od\", escriba a continuación un COMENTARIO")[0];

                if (tagCtrl.v_ComponentFieldsId == "N009-MF000003195")
                {
                    gb6.Enabled = true;
                    gb6.Enabled = true;

                }
                else if (tagCtrl.v_ComponentFieldsId == "N009-MF000000760")
                {
                    gb6.Enabled = false;
                    gb6.Enabled = false;
                }
            }
            else if (tagCtrl.v_ComponentId == "N009-ME000000444")
            {
                GroupBox gbE = null;
                gbE = (GroupBox)FindControlInCurrentTab("gb_E. FUNCIONES COGNITIVAS")[0];
                GroupBox gbF = null;
                gbF = (GroupBox)FindControlInCurrentTab("gb_F. ASPECTOS INTRA E INTERPERSONALES")[0];
                GroupBox gbG = null;
                gbG = (GroupBox)FindControlInCurrentTab("gb_G. PERFIL DE PERSONALIDAD - TEST DE COLORES")[0];
                GroupBox gbH = null;
                gbH = (GroupBox)FindControlInCurrentTab("gb_H. ADAPTABILIDAD, CAPACIDAD DE AFRONTE - HOMBRE BAJO LA LLUVIA")[0];

                GroupBox gbI = null;
                gbI = (GroupBox)FindControlInCurrentTab("gb_I. FUNCIONES COGNITIVAS.")[0];
                GroupBox gbJ = null;
                gbJ = (GroupBox)FindControlInCurrentTab("gb_J. ASPECTOS INTRA E INTERPERSONALES")[0];
                GroupBox gbK = null;
                gbK = (GroupBox)FindControlInCurrentTab("gb_K. PERFIL DE PERSONALIDAD")[0];
                GroupBox gbL = null;
                gbL = (GroupBox)FindControlInCurrentTab("gb_L.  ADAPTABILIDAD - CLIMA SOCIAL, LABORAL")[0];

                var value = GetValueControl(tagCtrl.i_ControlId, senderCtrl);


                RadioButton valSC = (RadioButton)FindControlInCurrentTab("N009-MF000003531")[0];
                RadioButton valSI = (RadioButton)FindControlInCurrentTab("N009-MF000003530")[0];
                RadioButton valGA = (RadioButton)FindControlInCurrentTab("N009-MF000003532")[0];

                if (tagCtrl.v_ComponentFieldsId == "N009-MF000003531")
                {

                    gbE.Enabled = true;
                    gbF.Enabled = true;
                    gbG.Enabled = true;
                    gbH.Enabled = true;

                    gbI.Enabled = true;
                    gbJ.Enabled = true;
                    gbK.Enabled = true;
                    gbL.Enabled = true;

                    if (valSC.Checked != true)
                    {
                        gbE.Enabled = false;
                        gbF.Enabled = false;
                        gbG.Enabled = false;
                        gbH.Enabled = false;

                        gbI.Enabled = true;
                        gbJ.Enabled = true;
                        gbK.Enabled = true;
                        gbL.Enabled = true;
                    }
                    //else
                    //{
                    //    gbE.Enabled = true;
                    //    gbF.Enabled = true;
                    //    gbG.Enabled = true;
                    //    gbH.Enabled = true;
                    //}

                }
                else if (tagCtrl.v_ComponentFieldsId == "N009-MF000003530")
                {
                    gbE.Enabled = true;
                    gbF.Enabled = true;
                    gbG.Enabled = true;
                    gbH.Enabled = true;

                    gbI.Enabled = true;
                    gbJ.Enabled = true;
                    gbK.Enabled = true;
                    gbL.Enabled = true;

                    if (valSI.Checked != true)
                    {
                        gbI.Enabled = false;
                        gbJ.Enabled = false;
                        gbK.Enabled = false;
                        gbL.Enabled = false;

                        gbE.Enabled = true;
                        gbF.Enabled = true;
                        gbG.Enabled = true;
                        gbH.Enabled = true;
                    }
                    //else
                    //{
                    //    gbI.Enabled = true;
                    //    gbJ.Enabled = true;
                    //    gbK.Enabled = true;
                    //    gbL.Enabled = true;
                    //}
                }


            }

        }

        private static void SetControlValidate(int controlId, Control ctrl, float? validateValue1, float? validateValue2, UltraValidator uv)
        {
            // Objetos para validar
            RangeCondition rc;
            ValidationSettings vs;

            uv.ErrorAppearance.BackColor = Color.FromArgb(255, 255, 192);
            uv.ErrorAppearance.BackGradientStyle = GradientStyle.Vertical;
            uv.ErrorAppearance.BorderColor = Color.Pink;
            uv.NotificationSettings.Action = NotificationAction.MessageBox;

            switch ((ControlType)controlId)
            {
                case ControlType.CadenaTextual:
                    uv.GetValidationSettings((TextBox)ctrl).IsRequired = true;
                    break;
                case ControlType.CadenaMultilinea:
                    uv.GetValidationSettings((TextBox)ctrl).IsRequired = true;
                    break;
                case ControlType.NumeroEntero:
                    // Establecer condición por rangos
                    rc = new RangeCondition(validateValue1,
                                                             validateValue2,
                                                             typeof(int));
                    vs = uv.GetValidationSettings((UltraNumericEditor)ctrl);
                    vs.Condition = rc;
                    break;
                case ControlType.NumeroDecimal:
                    // Establecer condición por rangos
                    rc = new RangeCondition(validateValue1,
                                                             validateValue2,
                                                             typeof(double));
                    vs = uv.GetValidationSettings((UltraNumericEditor)ctrl);
                    vs.Condition = rc;
                    break;
                case ControlType.SiNoCheck:
                    break;
                case ControlType.SiNoRadioButton:
                    break;
                case ControlType.Radiobutton:
                    break;
                case ControlType.SiNoCombo:
                    uv.GetValidationSettings(ctrl).Condition = new OperatorCondition(ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
                    uv.GetValidationSettings(ctrl).EmptyValueCriteria = EmptyValueCriteria.NullOrEmptyString;
                    uv.GetValidationSettings(ctrl).IsRequired = true;
                    break;
                case ControlType.UcFileUpload:
                    break;
                case ControlType.Lista:
                    uv.GetValidationSettings(ctrl).Condition = new OperatorCondition(ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
                    uv.GetValidationSettings(ctrl).EmptyValueCriteria = EmptyValueCriteria.NullOrEmptyString;
                    uv.GetValidationSettings(ctrl).IsRequired = true;
                    break;
            }
        }

        private void txt_Leave(object sender, System.EventArgs e)
        {
            flagValueChange = true;

            // Capturar el control invocador
            Control senderCtrl = (Control)sender;
            // Obtener información contenida en la propiedad Tag del control invocante
            var tagCtrl = (KeyTagControl)senderCtrl.Tag;
            string valueToAnalyze = GetValueControl(tagCtrl.i_ControlId, senderCtrl);
            int isSourceField = tagCtrl.i_IsSourceFieldToCalculate;

            Dictionary<string, object> Params = null;
            List<double> evalExpResultList = new List<double>();

            #region logica de modificacion de flag [_isChangeValue]

            if (!_isChangeValue)
            {
                if (_oldValue != valueToAnalyze)
                {
                    _isChangeValue = true;
                }
            }

            #endregion

            if (isSourceField == (int)SiNo.SI)
            {

                #region Nueva logica de calculo de formula soporta n parametros

                // Recorrer las formulas en las cuales el campo esta referenciado
                foreach (var formu in tagCtrl.Formula)
                {
                    // Obtener Campos fuente participantes en el calculo
                    var sourceFields = Common.Utils.GetTextFromExpressionInCorchete(formu.v_Formula);
                    Params = new Dictionary<string, object>();

                    foreach (string sf in sourceFields)
                    {
                        // Buscar controles fuentes
                        var findCtrlResult = FindDynamicControl(sf);
                        var length = findCtrlResult.Length;
                        // La busqueda si tuvo exito
                        if (length != 0)
                        {
                            // Obtener información del control encontrado 
                            var tagSourceField = (KeyTagControl)findCtrlResult[0].Tag;
                            // Obtener el tipo de dato al cual se va castear un control encontrado
                            string dtc = GetDataTypeControl(tagSourceField.i_ControlId);
                            // Obtener value del control encontrado
                            var value = GetValueControl(tagSourceField.i_ControlId, findCtrlResult[0]);

                            if (dtc == "int")
                            {
                                //var ival = int.Parse(value);
                                Params[sf] = int.Parse(value);
                            }
                            else if (dtc == "double")
                            {
                                Params[sf] = double.Parse(value);
                            }
                        }
                        else
                        {
                            if (sf.ToUpper() == "EDAD")
                            {
                                Params[sf] = _age;
                            }
                            else if (sf.ToUpper() == "GENERO_2")
                            {
                                Params[sf] = _sexType == Gender.FEMENINO ? 0 : 1;
                            }
                            else if (sf.ToUpper() == "GENERO_1")
                            {
                                Params[sf] = _sexType == Gender.MASCULINO ? 0 : 1;
                            }
                        }

                    }

                    bool isFound = false;

                    var isContain = formu.v_Formula.Contains("/");
                    if (isContain)
                    {
                        foreach (var item in Params)
                        {
                            if (item.Value.ToString() == "0")
                            {
                                isFound = true;
                                break;
                            }
                        }
                    }

                    if (!isFound)
                    {
                        var evalExpResult = Common.Utils.EvaluateExpression(formu.v_Formula, Params);
                        evalExpResultList.Add(evalExpResult);
                        var targetFieldOfCalculate1 = FindDynamicControl(formu.v_TargetFieldOfCalculateId);
                        targetFieldOfCalculate1[0].Text = evalExpResult.ToString();
                    }

                } // fin foreach Formula

                #endregion

                GeneratedAutoDX(valueToAnalyze, senderCtrl, tagCtrl);
            }
            else
            {
                GeneratedAutoDX(valueToAnalyze, senderCtrl, tagCtrl);
            }

        }

        private string GetValueControl(int ControlId, Control ctrl)
        {
            string value1 = null;

            switch ((ControlType)ControlId)
            {
                case ControlType.CadenaTextual:
                    value1 = ((TextBox)ctrl).Text;
                    break;
                case ControlType.CadenaMultilinea:
                    value1 = ((TextBox)ctrl).Text;
                    break;
                case ControlType.NumeroEntero:
                    value1 = ((UltraNumericEditor)ctrl).Value.ToString();
                    break;
                case ControlType.NumeroDecimal:
                    //value1 = ((UltraNumericEditor)ctrl).Value.ToString();
                    value1 = ctrl.Text.Trim();
                    break;
                case ControlType.SiNoCheck:
                    value1 = Convert.ToInt32(((CheckBox)ctrl).Checked).ToString();
                    break;
                case ControlType.SiNoRadioButton:
                    value1 = Convert.ToInt32(((RadioButton)ctrl).Checked).ToString();
                    break;
                case ControlType.Radiobutton:
                    value1 = Convert.ToInt32(((RadioButton)ctrl).Checked).ToString();
                    break;
                case ControlType.SiNoCombo:
                    value1 = ((ComboBox)ctrl).SelectedValue.ToString();
                    break;
                case ControlType.Lista:
                    value1 = ((ComboBox)ctrl).SelectedValue.ToString();
                    break;
                case ControlType.UcOdontograma:
                    _tmpListValuesOdontograma = ((UserControls.ucOdontograma)ctrl).DataSource;
                    break;
                case ControlType.UcAudiometria:
                    _tmpListValuesOdontograma = ((UserControls.ucAudiometria)ctrl).DataSource;
                    break;
                case ControlType.ucFacial:
                    //_tmpListValuesOdontograma = ((UserControls.ucExFacial)ctrl).DataSource;
                    break;
                case ControlType.UcSomnolencia:
                    _tmpListValuesOdontograma = ((UserControls.ucSomnolencia)ctrl).DataSource;
                    break;
                case ControlType.UcAcumetria:
                    _tmpListValuesOdontograma = ((UserControls.ucAcumetria)ctrl).DataSource;
                    break;
                case ControlType.UcSintomaticoRespi:
                    _tmpListValuesOdontograma = ((UserControls.ucSintomaticoResp)ctrl).DataSource;
                    break;
                case ControlType.UcRxLumboSacra:
                    _tmpListValuesOdontograma = ((UserControls.ucRXLumboSacra)ctrl).DataSource;
                    break;
                case ControlType.UcOtoscopia:
                    _tmpListValuesOdontograma = ((UserControls.ucOtoscopia)ctrl).DataSource;
                    break;
                case ControlType.UcEvaluacionErgonomica:
                    _tmpListValuesOdontograma = ((UserControls.ucEvaluacionErgonomica)ctrl).DataSource;
                    break;
                case ControlType.UcOjoSeco:
                    _tmpListValuesOdontograma = ((UserControls.ucOjoSeco)ctrl).DataSource;
                    break;
                case ControlType.UcOsteoMuscular:
                    _tmpListValuesOdontograma = ((UserControls.ucOsteoMuscular)ctrl).DataSource;
                    break;
                case ControlType.UcFototipo:
                    _tmpListValuesOdontograma = ((UserControls.ucFotoTipo)ctrl).DataSource;
                    break;
                case ControlType.Fecha:
                    value1 = ((DateTimePicker)ctrl).Text;
                    break;

                //case ControlType.ucPsicologia:
                //    if (_esoTypeId == TypeESO.PreOcupacional)
                //    {
                //        _tmpListValuesOdontograma = ((UserControls.ucPsychologicalExam)ctrl).DataSource;
                //    }
                //    else if (_esoTypeId == TypeESO.PeriodicoAnual)
                //    {
                //        _tmpListValuesOdontograma = ((UserControls.ucPsychologicalExamAnual)ctrl).DataSource;
                //    }
                //    break;
                //case ControlType.ucEspirometria:
                //    _tmpListValuesOdontograma = ((UserControls.ucEspirometria)ctrl).DataSource;
                //    break;
                //case ControlType.ucOftalmologia:
                //    _tmpListValuesOdontograma = ((UserControls.ucOftalmologia)ctrl).DataSource;
                //    break;
                //case ControlType.ucRadiografiaOIT:
                //    _tmpListValuesOdontograma = ((UserControls.ucRadiografiaOIT)ctrl).DataSource;
                //    break;
                default:
                    break;
            }

            return value1;
        }

        private void tcExamList_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
        {
            if (personData.v_CustomerOrganizationId == "N009-OO000000587" || personData.v_EmployerOrganizationId == "N009-OO000000587" || personData.v_WorkingOrganizationId == "N009-OO000000587" || personData.v_CustomerOrganizationId == "N002-OO000000717" || personData.v_EmployerOrganizationId == "N002-OO000000717" || personData.v_WorkingOrganizationId == "N002-OO000000717")
            {
                if (e.Tab.Text != "MEDICINA"
                    && e.Tab.Text != "PSICOLOGÍA"
                    && e.Tab.Text != "TRIAJE")
                {
                    chkUtilizarFirma.Checked = true;
                }
                else
                {
                    chkUtilizarFirma.Checked = false;
                }
            }
            else
            {
                chkUtilizarFirma.Checked = false;

            }

            _examName = e.Tab.Text;

            EXAMENES_lblComentarios.Text = string.Format("Comentarios de {0}", _examName);
            EXAMENES_lblEstadoComponente.Text = string.Format("Estado del exámen ({0})", _examName);
            btnGuardarExamen.Text = string.Format("&Guardar ({0})", _examName);
            _componentId = e.Tab.Key;
            _serviceComponentId = e.Tab.Tag.ToString();
            LoadDataBySelectedComponent(_componentId, _serviceComponentId);
        }
        private void LoadDataBySelectedComponent(string componentId, string serviceComponentId)
        {
            SetSecurityByComponent();
            PopulateGridDiagnostic(componentId);
            PaintGrdDiagnosticoPorExamenComponente();
            PopulateDataExam(serviceComponentId);
        }

        private void SetSecurityByComponent()
        {
            OperationResult objOperationResult = new OperationResult();

            var nodeId = Globals.ClientSession.i_CurrentExecutionNodeId;
            var roleId = Globals.ClientSession.i_RoleId.Value;

            bool isReadOnly = false;
            bool isWriteOnly = false;

            // Obtener campos de un componente especifico
            var componentFields = _tmpServiceComponentsForBuildMenuList.Find(p => p.v_ComponentId == _componentId).Fields;
            // Obtener permisos de cada examen de un rol especifico

            componentProfile = new ServiceBL().GetRoleNodeComponentProfile(ref objOperationResult, nodeId, roleId, _componentId);


            if (componentProfile != null)
            {


                if (_action == "View")
                {
                    isReadOnly = true;
                    btnGuardarExamen.Enabled = false;
                }
                else
                {
                    if (componentProfile.i_Read == (int)SiNo.SI && componentProfile.i_Write == (int)SiNo.SI)
                    {
                        isReadOnly = false;
                        btnGuardarExamen.Enabled = true;
                    }
                    else
                    {
                        isReadOnly = true;
                        btnGuardarExamen.Enabled = false;
                    }

                    if (componentProfile.i_Write == (int)SiNo.SI)
                    {
                        isWriteOnly = true;
                        btnGuardarExamen.Enabled = true;
                    }
                    else
                    {
                        isWriteOnly = false;
                        btnGuardarExamen.Enabled = false;
                    }
                    if (cbEstadoComponente.SelectedValue.ToString() == ((int)ServiceComponentStatus.Iniciado).ToString())
                    {
                        btnGuardarExamen.Enabled = false;
                        btnVisorReporteExamen.Enabled = false;
                    }
                    else
                    {
                        btnGuardarExamen.Enabled = true;
                        btnVisorReporteExamen.Enabled = true;
                    }

                    if (_serviceComponentsInfo != null)
                        _serviceComponentsInfo = null;

                    _serviceComponentsInfo = new ServiceBL().GetServiceComponentsInfo(ref objOperationResult, _serviceComponentId, _serviceId);

                    if (_serviceComponentsInfo != null)
                    {
                        txtComentario.Text = _serviceComponentsInfo.v_Comment;
                        cbEstadoComponente.SelectedValue = _serviceComponentsInfo.i_ServiceComponentStatusId == (int)ServiceComponentStatus.PorIniciar ? ((int)ServiceComponentStatus.Iniciado).ToString() : _serviceComponentsInfo.i_ServiceComponentStatusId.ToString();
                        cbTipoProcedenciaExamen.SelectedValue = _serviceComponentsInfo.i_ExternalInternalId == null ? "1" : _serviceComponentsInfo.i_ExternalInternalId.ToString();
                        chkApproved.Checked = Convert.ToBoolean(_serviceComponentsInfo.i_IsApprovedId);

                        //old
                        //if (_serviceComponentsInfo.i_ServiceComponentStatusId != 1)
                        //{
                        //    _cancelEventSelectedIndexChange = true;
                        //    SearchControlAndSetValue(tcExamList.SelectedTab.TabPage, _serviceComponentsInfo);

                        //    _cancelEventSelectedIndexChange = false;
                        //}
                        //else
                        //{
                        //    SetDefaultValueBySelectedTab();
                        //}

                        if (_serviceComponentsInfo.i_ServiceComponentStatusId == 1)
                        {
                            SetDefaultValueBySelectedTab();
                        }

                        lblUsuGraba.Text = string.Format("Usuario Crea : {0}", _serviceComponentsInfo.v_CreationUser);
                        lblFechaGraba.Text = string.Format("Fecha Crea : {0} {1}", _serviceComponentsInfo.d_CreationDate.Value.ToShortDateString(), _serviceComponentsInfo.d_CreationDate.Value.ToShortTimeString());
                    }

                }

                #region Establecer permisos Lectura / escritura a cada campo de un examen componente
                bool EnableYesNo = false;
                //if (_action == "View"){EnableYesNo = false;}
                foreach (ComponentFieldsList cf in componentFields)
                {
                    if (_action == "View")
                    {
                        EnableYesNo = false;
                    }
                    else if (cf.i_Enabled == 1)
                    {
                        EnableYesNo = false;
                    }
                    else
                    {
                        int escritura = VerificarEscritura(cf.v_ComponentId, Globals.ClientSession.i_SystemUserId);
                        if (escritura == 1) { EnableYesNo = true; }
                        else { EnableYesNo = false; }
                    }
                    var ctrl__ = tcExamList.SelectedTab.TabPage.Controls.Find(cf.v_ComponentFieldId, true);
                    if (ctrl__.Length != 0)
                    {
                        #region Setear valor

                        switch ((ControlType)cf.i_ControlId)
                        {
                            case ControlType.CadenaTextual:
                                TextBox txtt = (TextBox)ctrl__[0];
                                txtt.CreateControl();
                                txtt.Enabled = EnableYesNo;
                                break;
                            case ControlType.CadenaMultilinea:
                                TextBox txtm = (TextBox)ctrl__[0];
                                txtm.CreateControl();
                                txtm.Enabled = EnableYesNo;
                                break;
                            case ControlType.NumeroEntero:
                                UltraNumericEditor uni = (UltraNumericEditor)ctrl__[0];
                                uni.CreateControl();
                                uni.Enabled = EnableYesNo;
                                break;
                            case ControlType.NumeroDecimal:
                                UltraNumericEditor und = (UltraNumericEditor)ctrl__[0];
                                und.CreateControl();
                                und.Enabled = EnableYesNo;
                                break;
                            case ControlType.SiNoCheck:
                                CheckBox chkSiNo = (CheckBox)ctrl__[0];
                                chkSiNo.CreateControl();
                                chkSiNo.Enabled = EnableYesNo;
                                break;
                            case ControlType.SiNoRadioButton:
                                RadioButton rbSiNo = (RadioButton)ctrl__[0];
                                rbSiNo.CreateControl();
                                rbSiNo.Enabled = EnableYesNo;
                                break;
                            case ControlType.Radiobutton:
                                RadioButton rb = (RadioButton)ctrl__[0];
                                rb.CreateControl();
                                rb.Enabled = EnableYesNo;
                                break;
                            case ControlType.SiNoCombo:
                                ComboBox cbSiNo = (ComboBox)ctrl__[0];
                                cbSiNo.CreateControl();
                                cbSiNo.Enabled = EnableYesNo;
                                break;
                            case ControlType.UcFileUpload:
                                UserControl uc = (UserControl)ctrl__[0];
                                uc.CreateControl();
                                uc.Enabled = EnableYesNo;
                                break;
                            case ControlType.UcAudiometria:
                                UserControl ucAudio = (UserControl)ctrl__[0];
                                ucAudio.CreateControl();
                                ucAudio.Enabled = EnableYesNo;
                                break;
                            case ControlType.ucFacial:
                                UserControl ucFacial = (UserControl)ctrl__[0];
                                ucFacial.CreateControl();
                                ucFacial.Enabled = EnableYesNo;
                                break;
                            case ControlType.UcOdontograma:
                                UserControl ucOdonto = (UserControl)ctrl__[0];
                                ucOdonto.CreateControl();
                                ucOdonto.Enabled = EnableYesNo;
                                break;
                            case ControlType.UcOsteoMuscular:
                                UserControl ucOsteo = (UserControl)ctrl__[0];
                                ucOsteo.CreateControl();
                                ucOsteo.Enabled = EnableYesNo;
                                break;
                            case ControlType.UcSomnolencia:
                                UserControl ucSom = (UserControl)ctrl__[0];
                                ucSom.CreateControl();
                                ucSom.Enabled = EnableYesNo;
                                break;
                            case ControlType.UcSintomaticoRespi:
                                UserControl ucSint = (UserControl)ctrl__[0];
                                ucSint.CreateControl();
                                ucSint.Enabled = EnableYesNo;
                                break;
                            case ControlType.Lista:
                                ComboBox cbList = (ComboBox)ctrl__[0];
                                cbList.CreateControl();
                                cbList.Enabled = EnableYesNo;
                                break;
                            case ControlType.ucAudiometria1GR:
                                UserControl ucAudio1GR = (UserControl)ctrl__[0];
                                ucAudio1GR.CreateControl();
                                ucAudio1GR.Enabled = EnableYesNo;
                                break;
                            case ControlType.ucCalculoSTS:
                                UserControl ucCalculoSTS = (UserControl)ctrl__[0];
                                ucCalculoSTS.CreateControl();
                                ucCalculoSTS.Enabled = EnableYesNo;
                                break;
                            default:
                                break;
                        }

                        #endregion
                    }
                }

                #endregion

                #region Es Diagnosticable

                if (_action == "View")
                {
                    if (aptitud != "OBSERVADO")
                    {
                        btnAgregarDxExamen.Enabled = false;
                        btnEditarDxExamen.Enabled = false;
                        btnRemoverDxExamen.Enabled = false;
                    }
                }
                else
                {
                    if (componentProfile.i_Dx == (int)SiNo.SI)
                    {
                        //btnAgregarDxExamen.Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEso_EXAMENES_ADDDX", _formActions);
                        //btnEditarDxExamen.Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEso_EXAMENES_EDITDX", _formActions);
                        //btnRemoverDxExamen.Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEso_EXAMENES_REMOVEDX", _formActions);

                    }

                    if (componentProfile.i_Dx == (int)SiNo.NO)
                    {
                        btnAgregarDxExamen.Enabled = false;
                        btnEditarDxExamen.Enabled = false;
                        btnRemoverDxExamen.Enabled = false;
                    }
                }


                #endregion

                #region Es Aprobable?

                if (componentProfile.i_Approved == (int)SiNo.NO)
                {
                    chkApproved.Enabled = false;
                }
                else if (componentProfile.i_Approved == (int)SiNo.SI)
                {
                    //chkApproved.Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEso_EXAMENES_APPROVED", _formActions);
                }

                #endregion

            }
            else
            {
                #region Establecer permisos Lectura / escritura a cada campo de un examen componente

                foreach (ComponentFieldsList cf in componentFields)
                {
                    var ctrl__ = tcExamList.SelectedTab.TabPage.Controls.Find(cf.v_ComponentFieldId, true);

                    if (ctrl__.Length != 0)
                    {
                        #region Setear valor

                        switch ((ControlType)cf.i_ControlId)
                        {
                            case ControlType.CadenaTextual:
                                TextBox txtt = (TextBox)ctrl__[0];
                                txtt.CreateControl();
                                txtt.ReadOnly = true;
                                if (_action == "View")
                                {
                                    txtt.ReadOnly = true;
                                }
                                break;
                            case ControlType.CadenaMultilinea:
                                TextBox txtm = (TextBox)ctrl__[0];
                                txtm.CreateControl();
                                txtm.ReadOnly = true;
                                if (_action == "View")
                                {
                                    txtm.ReadOnly = true;
                                }
                                break;
                            case ControlType.NumeroEntero:
                                UltraNumericEditor uni = (UltraNumericEditor)ctrl__[0];
                                uni.CreateControl();
                                uni.ReadOnly = true;
                                if (_action == "View")
                                {
                                    uni.ReadOnly = true;
                                }
                                break;
                            case ControlType.NumeroDecimal:
                                UltraNumericEditor und = (UltraNumericEditor)ctrl__[0];
                                und.CreateControl();
                                und.ReadOnly = true;
                                if (_action == "View")
                                {
                                    und.ReadOnly = true;
                                }
                                break;
                            case ControlType.SiNoCheck:
                                CheckBox chkSiNo = (CheckBox)ctrl__[0];
                                chkSiNo.CreateControl();
                                chkSiNo.Enabled = false;
                                if (_action == "View")
                                {
                                    chkSiNo.Enabled = false;
                                }
                                break;
                            case ControlType.SiNoRadioButton:
                                RadioButton rbSiNo = (RadioButton)ctrl__[0];
                                rbSiNo.CreateControl();
                                rbSiNo.Enabled = false;
                                if (_action == "View")
                                {
                                    rbSiNo.Enabled = false;
                                }
                                break;
                            case ControlType.Radiobutton:
                                RadioButton rb = (RadioButton)ctrl__[0];
                                rb.CreateControl();
                                rb.Enabled = isWriteOnly;
                                if (_action == "View")
                                {
                                    rb.Enabled = false;
                                }
                                break;
                            case ControlType.SiNoCombo:
                                ComboBox cbSiNo = (ComboBox)ctrl__[0];
                                cbSiNo.CreateControl();
                                cbSiNo.Enabled = false;
                                if (_action == "View")
                                {
                                    cbSiNo.Enabled = false;
                                }
                                break;
                            case ControlType.UcFileUpload:
                                break;
                            case ControlType.Lista:
                                ComboBox cbList = (ComboBox)ctrl__[0];
                                cbList.CreateControl();
                                cbList.Enabled = false;
                                if (_action == "View")
                                {
                                    cbList.Enabled = false;
                                }
                                break;
                            default:
                                break;
                        }

                        #endregion
                    }
                }

                #endregion

                btnGuardarExamen.Enabled = false;
                btnAgregarDxExamen.Enabled = false;
                btnEditarDxExamen.Enabled = false;
                btnRemoverDxExamen.Enabled = false;

                // el check se activa o desactiva dependiendo del rol
                chkApproved.Enabled = false;
            }
        }
        private void LoadComboBox()
        {
            OperationResult objOperationResult = new OperationResult();
            var listDataForCombo = BLL.Utils.GetSystemParameterForComboForm(ref objOperationResult, "frmEso");
            
            var serviceComponent = listDataForCombo.Find(p => p.Id == "127").Items;
            Utils.LoadDropDownList(cbEstadoComponente, "Value1", "Id", serviceComponent.FindAll(p => p.Id != ((int)ServiceComponentStatus.PorIniciar).ToString()));
            Utils.LoadDropDownList(cbTipoProcedenciaExamen, "Value1", "Id", listDataForCombo.Find(p => p.Id == "132").Items);
            cbTipoProcedenciaExamen.SelectedValue = Convert.ToInt32(ComponenteProcedencia.Interno).ToString();
            var data138 = listDataForCombo.Find(p => p.Id == "138").Items;
            var data139 = listDataForCombo.Find(p => p.Id == "139").Items;
            var data111 = listDataForCombo.Find(p => p.Id == "111").Items;
        }

        private int VerificarEscritura(string p1, int p2)
        {
            int userId = new ServiceBL().VerificarEscrituraDB(p1, p2);
            return userId;
        }

        private void SetDefaultValueBySelectedTab()
        {
            try
            {
                var component = _tmpServiceComponentsForBuildMenuList.Find(p => p.v_ComponentId == _componentId);
                foreach (ComponentFieldsList cf in component.Fields)
                {
                    var field = tcExamList.SelectedTab.TabPage.Controls.Find(cf.v_ComponentFieldId, true);
                    if (field.Length != 0)
                    {
                        #region Setear valor x defecto del control

                        switch ((ControlType)cf.i_ControlId)
                        {
                            case ControlType.CadenaTextual:

                                TextBox txtt = (TextBox)field[0];
                                txtt.CreateControl();
                                //AMC15
                                if (cf.v_ComponentFieldId == "N009-MF000001788" || cf.v_ComponentFieldId == "N002-MF000000211")
                                {
                                    txtt.Text = _Dni;
                                }
                                else if (cf.v_ComponentFieldId == "N009-MF000000588" || cf.v_ComponentFieldId == "N009-MF000000587")
                                {
                                    txtt.Text = DateTime.Now.ToShortDateString();
                                }
                                else
                                {
                                    txtt.Text = cf.v_DefaultText;
                                }


                                txtt.BackColor = Color.White;
                                break;

                            case ControlType.CadenaMultilinea:
                                TextBox txtm = (TextBox)field[0];
                                txtm.CreateControl();
                                txtm.Text = cf.v_DefaultText;
                                txtm.BackColor = Color.White;
                                break;
                            case ControlType.NumeroEntero:
                                UltraNumericEditor uni = (UltraNumericEditor)field[0];
                                uni.CreateControl();
                                uni.Value = string.IsNullOrEmpty(cf.v_DefaultText) ? 0 : int.Parse(cf.v_DefaultText);
                                uni.BackColor = Color.White;
                                break;
                            case ControlType.NumeroDecimal:
                                UltraNumericEditor und = (UltraNumericEditor)field[0];
                                und.CreateControl();
                                und.Value = string.IsNullOrEmpty(cf.v_DefaultText) ? 0 : double.Parse(cf.v_DefaultText);
                                und.BackColor = Color.White;
                                break;
                            case ControlType.SiNoCheck:
                                CheckBox chkSiNo = (CheckBox)field[0];
                                chkSiNo.CreateControl();
                                chkSiNo.Checked = string.IsNullOrEmpty(cf.v_DefaultText) ? false : Convert.ToBoolean(int.Parse(cf.v_DefaultText));
                                break;
                            case ControlType.SiNoRadioButton:
                                RadioButton rbSiNo = (RadioButton)field[0];
                                rbSiNo.CreateControl();
                                rbSiNo.Checked = string.IsNullOrEmpty(cf.v_DefaultText) ? false : Convert.ToBoolean(int.Parse(cf.v_DefaultText));
                                break;
                            case ControlType.Radiobutton:
                                RadioButton rb = (RadioButton)field[0];
                                rb.CreateControl();
                                rb.Checked = string.IsNullOrEmpty(cf.v_DefaultText) ? false : Convert.ToBoolean(int.Parse(cf.v_DefaultText));
                                break;
                            case ControlType.SiNoCombo:
                                ComboBox cbSiNo = (ComboBox)field[0];
                                cbSiNo.CreateControl();
                                cbSiNo.SelectedValue = string.IsNullOrEmpty(cf.v_DefaultText) ? "-1" : cf.v_DefaultText;
                                break;
                            case ControlType.UcFileUpload:
                                break;
                            case ControlType.Lista:
                                ComboBox cbList = (ComboBox)field[0];
                                cbList.CreateControl();
                                cbList.SelectedValue = string.IsNullOrEmpty(cf.v_DefaultText) ? "-1" : cf.v_DefaultText;
                                break;
                            case ControlType.UcOdontograma:
                                ((UserControls.ucOdontograma)field[0]).ClearValueControl();
                                break;
                            case ControlType.UcAudiometria:
                                ((UserControls.ucAudiometria)field[0]).ClearValueControl();
                                break;
                            case ControlType.ucAudiometria1GR:
                                ((UserControls.ucAudiometria1GR)field[0]).ClearValueControl();
                                break;
                            case ControlType.ucCalculoSTS:
                                ((UserControls.ucCalculoSTS)field[0]).ClearValueControl();
                                break;
                            case ControlType.ucFacial:
                                //((UserControls.ucExFacial)field[0]).ClearValueControl();
                                break;
                            case ControlType.UcSomnolencia:
                                ((UserControls.ucSomnolencia)field[0]).ClearValueControl();
                                break;
                            case ControlType.UcAcumetria:
                                ((UserControls.ucAcumetria)field[0]).ClearValueControl();
                                break;
                            case ControlType.UcFototipo:
                                ((UserControls.ucFotoTipo)field[0]).ClearValueControl();
                                break;
                            case ControlType.UcSintomaticoRespi:
                                ((UserControls.ucSintomaticoResp)field[0]).ClearValueControl();
                                break;
                            case ControlType.UcRxLumboSacra:
                                ((UserControls.ucRXLumboSacra)field[0]).ClearValueControl();
                                break;
                            case ControlType.UcOtoscopia:
                                ((UserControls.ucOtoscopia)field[0]).ClearValueControl();
                                break;
                            case ControlType.UcEvaluacionErgonomica:
                                ((UserControls.ucEvaluacionErgonomica)field[0]).ClearValueControl();
                                break;
                            case ControlType.UcOsteoMuscular:
                                ((UserControls.ucOsteoMuscular)field[0]).ClearValueControl();
                                break;
                            case ControlType.UcOjoSeco:
                                ((UserControls.ucOjoSeco)field[0]).ClearValueControl();
                                break;
                            default:
                                break;
                        }

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void PopulateGridDiagnostic(string componentId)
        {
            ClearGridDiagnostic();
            List<DiagnosticRepositoryList> diagnosticList = null;
            Task.Factory.StartNew(() =>
            {
                diagnosticList = new ServiceBL().GetServiceComponentDisgnosticsForGridView(ref objOperationResult, _serviceId, componentId);
            }).ContinueWith(t =>
            {
                if (!diagnosticList.Any()) return;
                _tmpExamDiagnosticComponentList = diagnosticList;
                grdDiagnosticoPorExamenComponente.DataSource = diagnosticList;
                lblRecordCountDiagnosticoPorExamenCom.Text = string.Format("Se encontraron {0} registros.", diagnosticList.Count);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void ClearGridDiagnostic()
        {
            _tmpExamDiagnosticComponentList = null;
            grdDiagnosticoPorExamenComponente.DataSource = new List<DiagnosticRepositoryList>();
            lblRecordCountDiagnosticoPorExamenCom.Text = @"Se encontraron 0 registros.";
        }

        private void PaintGrdDiagnosticoPorExamenComponente()
        {
            foreach (var t in grdDiagnosticoPorExamenComponente.Rows)
            {
                var caliFinal = (PreQualification)t.Cells["i_PreQualificationId"].Value;
                switch (caliFinal)
                {
                    case PreQualification.SinPreCalificar:
                        t.Appearance.BackColor = Color.Pink;
                        t.Appearance.BackColor2 = Color.Pink;
                        break;
                    case PreQualification.Aceptado:
                        t.Appearance.BackColor = Color.LawnGreen;
                        t.Appearance.BackColor2 = Color.LawnGreen;
                        break;
                    case PreQualification.Rechazado:
                        t.Appearance.BackColor = Color.DarkGray;
                        t.Appearance.BackColor2 = Color.DarkGray;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                t.Appearance.BackGradientStyle = GradientStyle.VerticalBump;
            }
        }

        private void PopulateDataExam(string serviceComponentId)
        {
            ServiceComponentListNew2 serviceComponentsInfo = null;
            Task.Factory.StartNew(() =>
            {
                serviceComponentsInfo = new ServiceBL().GetServiceComponentsInfo(ref objOperationResult, serviceComponentId, _serviceId);

            }).ContinueWith(t =>
            {

                if (serviceComponentsInfo == null) return;
                txtComentario.Text = serviceComponentsInfo.v_Comment;
                cbEstadoComponente.SelectedValue = serviceComponentsInfo.i_ServiceComponentStatusId == (int)ServiceComponentStatus.PorIniciar ? ((int)ServiceComponentStatus.Iniciado).ToString() : serviceComponentsInfo.i_ServiceComponentStatusId.ToString();
                cbTipoProcedenciaExamen.SelectedValue = serviceComponentsInfo.i_ExternalInternalId == null ? "1" : serviceComponentsInfo.i_ExternalInternalId.ToString();
                chkApproved.Checked = Convert.ToBoolean(serviceComponentsInfo.i_IsApprovedId);
                if (serviceComponentsInfo.v_UpdateUser != null)
                    lblUsuGraba.Text = serviceComponentsInfo.v_UpdateUser == null ? "" : serviceComponentsInfo.v_UpdateUser.ToUpper();
                if (serviceComponentsInfo.d_UpdateDate != null)
                    lblFechaGraba.Text = serviceComponentsInfo.d_UpdateDate == null ? "" : serviceComponentsInfo.d_UpdateDate.Value.ToString("dd-MMMM-yyyy (hh:mm) ");
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }


    }

}
