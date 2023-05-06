using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmEditDx : Form
    {
        private string HospiId;
        object lista;
        private hospitalizacionDto _HospiDto = null;
        private hospitalizacionDto _hospitalizacionDto = null;
        private string DxT;
        public frmEditDx(string _HospiId, string _DxT)
        {
            HospiId = _HospiId;
            DxT = _DxT;
            InitializeComponent();
        }

        private void frmEditDx_Load(object sender, EventArgs e)
        {

            OperationResult objOperationResult = new OperationResult();
            lista = new PacientBL().LlenarDxsTramas(ref objOperationResult);


            #region Lista de Diagnosticos
            cbDx.DataSource = lista;
            cbDx.DisplayMember = "v_Name";
            cbDx.ValueMember = "v_CIE10Id";
            cbDx.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            cbDx.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.cbDx.DropDownWidth = 550;
            cbDx.DisplayLayout.Bands[0].Columns[0].Width = 500;
            cbDx.DisplayLayout.Bands[0].Columns[1].Width = 50;
            #endregion
            _HospiDto = new HospitalizacionBL().GetHospitalizacion(ref objOperationResult, HospiId);

            if (DxT == "DxE")
            {
                if (!string.IsNullOrEmpty(_HospiDto.v_DiseasesName))
                {
                    cbDx.Text = _HospiDto.v_DiseasesName;
                    txtCie10.Text = _HospiDto.v_CIE10Id;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(_HospiDto.v_DiseasesName))
                {
                    cbDx.Text = _HospiDto.v_DiseasesNameSalida;
                    txtCie10.Text = _HospiDto.v_CIE10IdSalida;
                }
            }
            
            

        }

        private void btnGuardarTicket_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            if (_hospitalizacionDto == null)
            {
                _hospitalizacionDto = new hospitalizacionDto();
            }

            if (cbDx.Text == "")
            {
                MessageBox.Show("Seleccione un Dx para poder dar Alta Médica", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _hospitalizacionDto = new HospitalizacionBL().GetHospitalizacion(ref objOperationResult, HospiId);

            if (DxT == "DxE")
            {
                _hospitalizacionDto.v_DiseasesName = cbDx.Text;
                _hospitalizacionDto.v_CIE10Id = txtCie10.Text;
            }
            else
            {
                _hospitalizacionDto.v_DiseasesNameSalida = cbDx.Text;
                _hospitalizacionDto.v_CIE10IdSalida = txtCie10.Text;                 
            }

            new HospitalizacionBL().UpdateHospitalizacion(ref objOperationResult, _hospitalizacionDto, Globals.ClientSession.GetAsList());
                
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
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

        private void cbDx_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbDx.Text != "")
            {
                cbDx.SelectionStart = 0;
                cbDx.SelectionLength = cbDx.Text.Length;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
