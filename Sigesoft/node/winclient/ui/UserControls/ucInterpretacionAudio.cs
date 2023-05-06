using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Node.WinClient.UI.Operations.Popups;
using Sigesoft.Node.WinClient.BE;

namespace Sigesoft.Node.WinClient.UI.UserControls
{
    public partial class ucInterpretacionAudio : UserControl
    {
        private List<DiagnosticRepositoryList> _tmpExamDiagnosticComponentList;
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

        public string PersonId { get; set; }
        public string ServiceId { get; set; }

        public List<ServiceComponentFieldValuesList> DataSource
        {
            get
            {
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

        private void ClearValueControl()
        {
            _isChangueValueControl = false;
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

       

        public ucInterpretacionAudio()
        {
            InitializeComponent();
        }

        private void ucInterpretacionAudio_Load(object sender, EventArgs e)
        {
            txtClinica.Text = "";
            txtKlockhoff.Text = "";
            txtClinica.Name = "N009-ITA00000001";
            txtKlockhoff.Name = "N009-ITA00000002";
        }

        private void btnAddClinica_Click(object sender, EventArgs e)
        {
            GrabarDxTxt(_tmpExamDiagnosticComponentList, txtClinica);
        }
       

        private void btnKlockhoff_Click(object sender, EventArgs e)
        {
            GrabarDxTxt(_tmpExamDiagnosticComponentList, txtKlockhoff);
        }

        private void GrabarDxTxt(List<DiagnosticRepositoryList> _tmpExamDiagnosticComponentList, TextBox txt)
        {
            _tmpExamDiagnosticComponentList = new List<DiagnosticRepositoryList>();
            var frm = new frmAddExamDiagnosticComponent("New");

            if (_tmpExamDiagnosticComponentList != null)
                frm._tmpExamDiagnosticComponentList = _tmpExamDiagnosticComponentList;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.Cancel)
                return;
            foreach (var dd in _tmpExamDiagnosticComponentList)
            {
                txt.Text = txt.Text + " - " + dd.v_DiseasesName;
            }
        }
    }
}
