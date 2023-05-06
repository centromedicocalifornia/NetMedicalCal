using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.Contasol.Integration;
using Sigesoft.Node.Contasol.Integration.Contasol;
using Sigesoft.Node.Contasol.Integration.Contasol.Models;

namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    public partial class frmSearchMedicamento : Form
    {
        private OperationResult _opbjOperationResult;
        private MedicamentoBl _objMedicamentoBl;

        List<MedicamentoDto> MedicamentoDtoList = new List<MedicamentoDto>();
        List<MedicamentoDto> MedicamentoDtoList_Temp = new List<MedicamentoDto>();

        public MedicamentoDto MedicamentoSeleccionado
        {
            get
            {
                var activeRow = ultraGrid1.ActiveRow;
                if (activeRow == null || activeRow.IsFilterRow) return null;
                return (MedicamentoDto)activeRow.ListObject;
            }
        }

        public frmSearchMedicamento()
        {
            InitializeComponent();
            _objMedicamentoBl = new MedicamentoBl();
            _opbjOperationResult = new OperationResult();
            Width = 940;
            Height = 626;
        }

        private void frmSearchMedicamento_Load(object sender, EventArgs e)
        {
            _opbjOperationResult = new OperationResult();
            gbBuscarMedicamento.Visible = true;
            gbRegistrarMedicamento.Visible = false;
            gbBuscarMedicamento.Dock = DockStyle.Fill;
            MedicamentoDao.ObtenerLineasParaCombo(cboLinea);
            MedicamentoDao.ObtenerUmParaCombo(cboUM);
            MedicamentoDao.ObtenerMarca(cboMarca);
            txtCodigo.Text = MedicamentoDao.CodigoNuevo();
            CargarData();

            //Utils.LoadDropDownList(cboUM, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref _opbjOperationResult, 105, null));
            //txtLaboratorio.DataSource = _objMedicamentoBl.GetFarmaciasForCombo(ref _opbjOperationResult);
            //cboUM.SelectedIndex = 0;
        }

        private void btnRegistrarMedicamento_Click(object sender, EventArgs e)
        {
            gbBuscarMedicamento.Visible = false;
            gbRegistrarMedicamento.Visible = true;
            gbRegistrarMedicamento.Dock = DockStyle.Fill;
            txtNombreMedicamento.Focus();
            txtNombreMedicamento.Text = txtBuscarNombre.Text;
            chekMedicinaNoExistente.Checked = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            gbBuscarMedicamento.Visible = true;
            gbRegistrarMedicamento.Visible = false;
            gbBuscarMedicamento.Dock = DockStyle.Fill;
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            CargarData();
        }

        private void CargarData()
        {
            try
            {
                _opbjOperationResult = new OperationResult();
                var data = _objMedicamentoBl.GetListMedicamentos(ref _opbjOperationResult, txtBuscarNombre.Text,
                    txtBuscarAccionF.Text, chekMedicinaNoExistente.Checked);

                MedicamentoDtoList = data;

                if (_opbjOperationResult.Success == 0)
                {
                    MessageBox.Show(_opbjOperationResult.ErrorMessage, @"Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                ultraGrid1.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"CargarData()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnGuardarMedicamento_Click(object sender, EventArgs e)
        {
            try
            {
                if (!uvRegistro.Validate(true, false).IsValid) return;
                
                _opbjOperationResult = new OperationResult();
                decimal d;
                int i;
                var entidad = new MedicamentoDto();
                entidad.Nombre = "(*)"+txtNombreMedicamento.Text.Trim();
                entidad.CodInterno = txtCodigo.Text.Trim();
                entidad.PrecioVenta = decimal.Round((decimal.TryParse(txtPrecioVenta.Text.Trim(), out d) ? d : 0),2);
                entidad.AccionFarmaco = txtAccionFarmaco.Text.Trim();
                entidad.Concentracion = txtConcentracion.Text.Trim();
                entidad.PrincipioActivo = txtPrincipioActivo.Text.Trim();
                entidad.Presentacion = txtPresentacion.Text.Trim();
                entidad.Laboratorio = cboMarca.Value.ToString();
                entidad.Ubicacion = txtUbicacion.Text.Trim();
                entidad.IdUnidadMedida = int.Parse(cboUM.Value.ToString());
                entidad.IdLinea = cboLinea.Value.ToString();
                entidad.v_Descripcion2 = txtDescripcionAlterna.Text.Trim();

                _objMedicamentoBl.AddUpdateMedicamento(ref _opbjOperationResult, entidad);
                if (_opbjOperationResult.Success == 0)
                {
                    MessageBox.Show(_opbjOperationResult.ErrorMessage, @"Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                gbBuscarMedicamento.Visible = true;
                gbRegistrarMedicamento.Visible = false;
                gbBuscarMedicamento.Dock = DockStyle.Fill;
                txtBuscarNombre.Text = txtNombreMedicamento.Text;
                txtBuscarAccionF.Clear();
                ultraButton1_Click(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"btnGuardarMedicamento_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void txtPrecioVenta_Validating(object sender, CancelEventArgs e)
        {
            decimal d;
            if (!string.IsNullOrWhiteSpace(txtPrecioVenta.Text))
                txtPrecioVenta.Text = decimal.TryParse(txtPrecioVenta.Text.Trim(), out d) ? d.ToString(CultureInfo.InvariantCulture) : "0";
        }

        private void ultraGrid1_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            btnAceptar_Click(this, EventArgs.Empty);
        }

        private void ultraGrid1_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
            var banda = e.Row.Band.Index.ToString();
            var row = e.Row;
            if (banda == "0")
            {
                if (row.Band.Index.ToString() == "0")
                {
                    if (e.Row.Cells["CodInterno"].Value.ToString().Split('-')[0] == "FNC")
                    {
                        e.Row.Appearance.BackColor = Color.Orange;
                        e.Row.Appearance.BackColor2 = Color.White;
                        //Y doy el efecto degradado vertical
                        e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
                    }
                }
            }
        }

        private void txtBuscarNombre_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtBuscarNombre.Text != string.Empty)
            {
                MedicamentoDtoList_Temp = new List<MedicamentoDto>(MedicamentoDtoList.Where(p => p.Nombre.Contains(txtBuscarNombre.Text.ToUpper())));

                ultraGrid1.DataSource = MedicamentoDtoList_Temp;
            }
            else
            {
                ultraGrid1.DataSource = MedicamentoDtoList;
            }
        }

        private void txtBuscarAccionF_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtBuscarAccionF.Text != string.Empty)
            {
                MedicamentoDtoList_Temp = new List<MedicamentoDto>(MedicamentoDtoList.Where(p => p.v_Descripcion2.Contains(txtBuscarAccionF.Text.ToUpper()) || p.AccionFarmaco.Contains(txtBuscarAccionF.Text.ToUpper()) || p.PrincipioActivo.Contains(txtBuscarAccionF.Text.ToUpper())));

                ultraGrid1.DataSource = MedicamentoDtoList_Temp;

                if (MedicamentoDtoList_Temp != null)
                {
                    //lblRecordCount.Text = string.Format("Se encontraron {0} registros.", MedicamentoDtoList_Temp.Count());

                    ultraGrid1.Rows[0].Selected = true;
                }

            }
            else
            {
                ultraGrid1.DataSource = MedicamentoDtoList;

                if (MedicamentoDtoList != null)
                {
                    //lblRecordCount.Text = string.Format("Se encontraron {0} registros.", MedicamentoDtoList.Count());

                    ultraGrid1.Rows[0].Selected = true;
                }
            }
        }
    }
}
