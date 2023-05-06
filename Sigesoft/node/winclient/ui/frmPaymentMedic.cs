using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using iTextSharp.awt.geom;
using Sigesoft.Common;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmPaymentMedic : Form
    {
        paymentmedicDAL paymentmedicDal = new paymentmedicDAL();
        List<PaymentMedic> payment = new List<PaymentMedic>();
        List<PaymentMedic> payment_temp = new List<PaymentMedic>();

        private bool select = false;
        public frmPaymentMedic()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmPaymentNew frm = new frmPaymentNew("new", null);
            frm.ShowDialog();
            btnBuscar_Click(sender, e);
            blockButtons(btnEdit, btnEliminar, false);

            txtUserName_KeyUp(sender, new KeyEventArgs(Keys.A));


        }

        private void blockButtons(Button btnEdit, Button btnEliminar, bool state)
        {
            btnEdit.Enabled = state;
            btnEliminar.Enabled = state;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int PaymetId = Convert.ToInt32(grdPayment.Selected.Rows[0].Cells["i_PaymetId"].Value.ToString());
            PaymentMedic paym = paymentmedicDal.GetPaynetById(PaymetId);
            frmPaymentNew frm = new frmPaymentNew("edit", paym);
            frm.ShowDialog();
            btnBuscar_Click(sender, e);
            blockButtons(btnEdit, btnEliminar, false);
            @select = false;
            txtUserName_KeyUp(sender, new KeyEventArgs(Keys.A));

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int PaymetId = Convert.ToInt32(grdPayment.Selected.Rows[0].Cells["i_PaymetId"].Value.ToString());
            paymentmedicDal.DeletePayment(PaymetId);
            MessageBox.Show("Eliminado correctamente", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnBuscar_Click(sender, e);
            blockButtons(btnEdit, btnEliminar, false);
            @select = false;
            txtUserName_KeyUp(sender, new KeyEventArgs(Keys.A));

        }

        private void grdPayment_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {

        }

        private void grdPayment_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            @select = true;
            btnEdit.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            payment = paymentmedicDal.GetPayment();
            grdPayment.DataSource = payment;
        }

       

        private void frmPaymentMedic_Load(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e);
        }

        private void txtUserName_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtUserName.Text != string.Empty)
            {
                payment_temp = new List<PaymentMedic>(payment.Where(p => p.v_UserName.Contains(txtUserName.Text)));

                grdPayment.DataSource = payment_temp;

            

            }
            else
            {
                grdPayment.DataSource = payment;

            }
        }
    }
}
