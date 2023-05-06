using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System.Data.SqlClient;

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmDarAlta : Form
    {
        private string _hospitalizacionId;
        private string _mode;
        private DateTime? _fechaAlta;
        private string _comentario;
        private hospitalizacionDto _hospitalizacionDto = null;
        private HospitalizacionBL _hospitalizacionBL = new HospitalizacionBL();

        private hospitalizacionDto _HospiDto = null;
        object lista;

        public frmDarAlta(string HopitalizacionId, string mode, DateTime? fechaAlta, string comentario )
        {
            _hospitalizacionId = HopitalizacionId;
            _mode = mode;
            _fechaAlta = fechaAlta;
            _comentario = comentario;
            InitializeComponent();
        }

        private void frmDarAlta_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            lista = new PacientBL().LlenarDxsTramas(ref objOperationResult);

            if (_mode == "New")
            {
                dtpFechaAlta.Checked = false;
                cbDx.Select();

                cbDx.DataSource = lista;
                cbDx.DisplayMember = "v_Name";
                cbDx.ValueMember = "v_CIE10Id";
                cbDx.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                cbDx.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                this.cbDx.DropDownWidth = 550;
                cbDx.DisplayLayout.Bands[0].Columns[0].Width = 500;
                cbDx.DisplayLayout.Bands[0].Columns[1].Width = 50;
            }
            else if (_mode == "Edit")
            {
                _HospiDto = _hospitalizacionBL.GetHospitalizacion(ref objOperationResult, _hospitalizacionId);

                if (_fechaAlta != null) dtpFechaAlta.Value = _fechaAlta.Value;
                txtComentario.Text = _comentario;

                #region Lista de Diagnosticos
                cbDx.DataSource = lista;
                cbDx.DisplayMember = "v_Name";
                cbDx.ValueMember = "v_CIE10Id";
                cbDx.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                cbDx.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                this.cbDx.DropDownWidth = 550;
                cbDx.DisplayLayout.Bands[0].Columns[0].Width = 500;
                cbDx.DisplayLayout.Bands[0].Columns[1].Width = 50;
                if (!string.IsNullOrEmpty(_HospiDto.v_DiseasesName))
                {
                    cbDx.Text = _HospiDto.v_DiseasesName;
                    txtCie10.Text = _HospiDto.v_CIE10Id;
                }
                #endregion

            }
        }

        private void btnGuardarTicket_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            if (_hospitalizacionDto == null)
            {
                _hospitalizacionDto = new hospitalizacionDto();
            }

            if (_mode == "New")
            {

            }
             else if (_mode == "Edit")
            {
                if (cbDx.Text == "")
                {
                    MessageBox.Show("Seleccione un Dx para poder dar Alta Médica", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _hospitalizacionDto = _hospitalizacionBL.GetHospitalizacion(ref objOperationResult, _hospitalizacionId);
                _hospitalizacionDto.d_FechaAlta = (DateTime?)(dtpFechaAlta.Checked == false ? (ValueType)null : dtpFechaAlta.Value);
                _hospitalizacionDto.v_Comentario = txtComentario.Text;
                _hospitalizacionDto.v_DiseasesName = cbDx.Text;
                _hospitalizacionDto.v_CIE10Id = txtCie10.Text;

                _hospitalizacionBL.UpdateHospitalizacion(ref objOperationResult, _hospitalizacionDto, Globals.ClientSession.GetAsList());


                

                
            }
            // hacer update al service culminado
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void cbDx_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbDx.Text != "")
            {
                cbDx.SelectionStart = 0;
                cbDx.SelectionLength = cbDx.Text.Length;
            }
        }

        private void cbDx_RowSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e)
        {
            //MessageBox.Show("hola MUNDO", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            #region Conexion SAM
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            #endregion

            string dx = cbDx.Text;
            var cadena1 = "select v_CIE10Description2 from cie10 where v_CIE10Description1='" + dx + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                txtCie10.Text = lector.GetValue(0).ToString();
            }
            lector.Close();
            conectasam.closesigesoft();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
