using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI.UserControls
{
    public partial class ucHistoriaHolo : UserControl
    {
        bool _isChangueValueControl = false;
        List<ServiceComponentFieldValuesList> _listOfAtencionAdulto1 = new List<ServiceComponentFieldValuesList>();
        ServiceComponentFieldValuesList _UserControlValores = null;

        #region "------------- Public Events -------------"
        /// <summary>
        /// Se desencadena cada vez que se cambia un valor del examen de Audiometria.
        /// </summary>
        public event EventHandler<AudiometriaAfterValueChangeEventArgs> AfterValueChange;
        protected void OnAfterValueChange(AudiometriaAfterValueChangeEventArgs e)
        {
            if (AfterValueChange != null)
                AfterValueChange(this, e);
        }
        #endregion

        #region "--------------- Properties --------------------"
        public string PersonId { get; set; }
        public string ServiceId { get; set; }

        public List<ServiceComponentFieldValuesList> DataSource
        {
            get
            {
                SaveValueControlForInterfacingESO(Constants.MOTIVO_CONSULTA, txtMotivoConsulta.Text);
                SaveValueControlForInterfacingESO(Constants.TIEMPO_ENFERMEDAD, txtT_Enf.Text);
                SaveValueControlForInterfacingESO(Constants.SIGNOS_SINTOMAS, txtSignos.Text);
                SaveValueControlForInterfacingESO(Constants.GLASGOW, txtGlasgow.Text);
                SaveValueControlForInterfacingESO(Constants.OCULAR, txtOcular.Text);
                SaveValueControlForInterfacingESO(Constants.VERBAL, txtVerbal.Text);
                SaveValueControlForInterfacingESO(Constants.MOTOR, txtMotor.Text);
                SaveValueControlForInterfacingESO(Constants.EX_FISICO, txtExFisico.Text);
                SaveValueControlForInterfacingESO(Constants.PLAN_TRABAJO, txtPlan.Text);
                SaveValueControlForInterfacingESO(Constants.REEVALUACION, txtReeva.Text);
                return _listOfAtencionAdulto1;
            }
            set
            {
                if (value != _listOfAtencionAdulto1)
                {
                    ClearValueControl();
                    _listOfAtencionAdulto1 = value;
                    SearchControlAndFill(value);
                    
                }
            }
        }

        private void SearchControlAndFill(List<ServiceComponentFieldValuesList> DataSource)
        {
            if (DataSource == null || DataSource.Count == 0) return;
            // Ordenar Lista Datasource
            var DataSourceOrdenado = DataSource.OrderBy(p => p.v_ComponentFieldId).ToList();

            // recorrer la lista que viene de la BD
            foreach (var item in DataSourceOrdenado)
            {
                var matchedFields = this.Controls.Find(item.v_ComponentFieldId, true);

                if (matchedFields.Length > 0)
                {
                    var field = matchedFields[0];

                    if (field is TextBox)
                    {
                        if (field.Name == item.v_ComponentFieldId)
                        {
                            ((TextBox)field).Text = item.v_Value1;
                        }
                    }
                }
            }
        }

        private void SaveValueControlForInterfacingESO(string name, string value)
        {
            #region Capturar Valor del campo

            _listOfAtencionAdulto1.RemoveAll(p => p.v_ComponentFieldId == name);

            _UserControlValores = new ServiceComponentFieldValuesList();

            _UserControlValores.v_ComponentFieldId = name;
            _UserControlValores.v_Value1 = value;
            _UserControlValores.v_ComponentId = Constants.OJO_SECO_ID;

            _listOfAtencionAdulto1.Add(_UserControlValores);

            DataSource = _listOfAtencionAdulto1;

            #endregion
        }

        public void ClearValueControl()
        {
            _isChangueValueControl = false;
        }

        public bool IsChangeValueControl { get { return _isChangueValueControl; } }
        #endregion


        public ucHistoriaHolo()
        {
            InitializeComponent();
        }

        private void ucHistoriaHolo_Load(object sender, EventArgs e)
        {
            organizationDto objOrganizationDto = new organizationDto();
            OrganizationBL _objBL = new OrganizationBL();
            OperationResult objOperationResult = new OperationResult();
            //var dxpresun = ObtenerDxPre(ServiceId);

            objOrganizationDto = _objBL.GetOrganization(ref objOperationResult, "N009-OO000000052");
            pbLogo.Image = Common.Utils.BytesArrayToImage(objOrganizationDto.b_Image, pbLogo);

            txtMotivoConsulta.Name = "N009-HCL00000001";
            txtT_Enf.Name = "N009-HCL00000002";
            txtSignos.Name = "N009-HCL00000003";
            txtGlasgow.Name = "N009-HCL00000004";
            txtOcular.Name = "N009-HCL00000005";
            txtVerbal.Name = "N009-HCL00000006";
            txtMotor.Name = "N009-HCL00000007";
            txtExFisico.Name = "N009-HCL00000008";
            txtPlan.Name = "N009-HCL00000009";
            txtReeva.Name = "N009-HCL00000010";
        }

        private object ObtenerDxPre(string ServiceId)
        {
            throw new NotImplementedException();
        }

        private void SearchControlAndSetEvents(Control ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is TextBox)
                {
                    if (ctrl.Name.Contains("N009-HCL"))
                    {
                        ctrl.Leave += new EventHandler(lbl_Leave);
                    }
                }

            }
        }

        private void lbl_Leave(object sender, System.EventArgs e)
        {
            TextBox senderCtrl = (TextBox)sender;

            SaveValueControlForInterfacingESO(senderCtrl.Name, senderCtrl.Text.ToString());

            _isChangueValueControl = true;
        }
        
    }
}
