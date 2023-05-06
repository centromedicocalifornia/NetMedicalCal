using Sigesoft.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmPaymentNew : Form
    {
        paymentmedicDAL paymentmedicDal = new paymentmedicDAL();
        OperationResult objOperationResult = new OperationResult();
        List<CategoryComp> category = new List<CategoryComp>();
        private bool select = false;
        private string _modo;
        private PaymentMedic _pay;
        public frmPaymentNew(string modo, PaymentMedic pay)
        {
            _modo = modo;
            _pay = pay;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtPorcentaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            DigitaOnlyDecimal(e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            valida validator = FindValidator();
            if (validator.state)
            {
                try
                {
                    _pay = new PaymentMedic();
                    _pay.i_CategoryId = Convert.ToInt32(grdCategory.Selected.Rows[0].Cells["i_CategoryId"].Value.ToString());
                    _pay.i_UserId = paymentmedicDal.getUserId(cboUserMed.Text);
                    _pay.i_TypePay = cboTipoAtx.Text == "POR INDICACIÓN" ? 1 : 2;
                    _pay.r_PayPercentage = float.Parse(txtPorcentaje.Text);
                    if (cboTipoAtx.Text == "FARMACIA")
                    {
                        _pay.i_TypePay = 3;
                        _pay.r_QuotaMonth = float.Parse(txtQuotaMonth.Text);
                        _pay.i_CategoryId = 0;
                    }
                    else { _pay.r_QuotaMonth = 0; }
                    if (_modo == "new")
                    {
                        paymentmedicDal.InsertPay(_pay);
                        MessageBox.Show("Registrado correctamente", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (_modo == "edit")
                    {
                        paymentmedicDal.UpdatePay(_pay);
                        MessageBox.Show("Actualizado correctamente", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "¡¡ERROR!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            else
            {
                MessageBox.Show(validator.message, "¡¡ERROR!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Close();
        }

        
        private valida FindValidator()
        {
            valida val = new valida();
            if (cboUserMed.Text == "--Seleccionar--")
            {
                val.state = false;
                val.message = "Seleccione un usuario válido";
            }
            else if (cboTipoAtx.Text == "")
            {
                val.state = false;
                val.message = "Seleccione un tipo de atención";
            }
            else if (txtPorcentaje.Text == "" || Convert.ToInt32(txtPorcentaje.Text) <= 0)
            {
                val.state = false;
                val.message = "Ingrese un porcentaje correcto";
            }
            else if (!select && _modo=="new")
            {
                val.state = false;
                val.message = "Seleccione una categoría de exámenes";
            }
            else if (select)
            {
                if (paymentmedicDal.RegisterExist(Convert.ToInt32(grdCategory.Selected.Rows[0].Cells["i_CategoryId"].Value.ToString()), cboTipoAtx.Text, cboUserMed.Text))
                {
                    val.state = false;
                    val.message = "Ya existe un registro para esta categoría";
                }
                else
                {
                    val.state = true;
                    val.message = "";
                }
            }
            else
            {
                val.state = true;
                val.message = "";
            }
            return val;
        }

        private void frmPaymentNew_Load(object sender, EventArgs e)
        {
            Utils.LoadDropDownList(cboUserMed, "Value1", "Id", BLL.Utils.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
            cboUserMed.SelectedValue = "-1";

            category = paymentmedicDal.getCategory();
            grdCategory.DataSource = category;
            if (_modo == "edit")
            {
                cboUserMed.Text = _pay.v_UserName;
                cboUserMed.Enabled = false;
                cboTipoAtx.Text = _pay.TypePayName;
                txtPorcentaje.Text = _pay.r_PayPercentage.ToString();
                txtQuotaMonth.Text = _pay.r_QuotaMonth.ToString();
            }
        }

       private void grdCategory_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            @select = true;
        }

       private void cboTipoAtx_SelectedIndexChanged(object sender, EventArgs e)
       {
           if (cboTipoAtx.Text == "FARMACIA")
           {
               grdCategory.Enabled = false;
               lblQuota.Visible = true;
               txtQuotaMonth.Visible = true;
           }
           else
           {
               grdCategory.Enabled = true;
               lblQuota.Visible = false;
               txtQuotaMonth.Visible = false;
           }
       }

       private void txtQuotaMonth_KeyPress(object sender, KeyPressEventArgs e)
       {
           DigitaOnlyDecimal(e);
       }

       private void DigitaOnlyDecimal(KeyPressEventArgs e)
       {
           CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;

           if (char.IsNumber(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator) e.Handled = false;
           else e.Handled = true;
       }
    }
}
