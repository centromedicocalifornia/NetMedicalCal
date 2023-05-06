using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmPayPorcentaje : Form
    {
        public decimal _newMonto;
        private decimal _monto;
        public frmPayPorcentaje(decimal monto)
        {
            _monto = monto;
            InitializeComponent();
        }

        private void frmPayPorcentaje_Load(object sender, EventArgs e)
        {

        }

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;

            if (char.IsNumber(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator) e.Handled = false;
            else e.Handled = true;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            if (decimal.Parse(txtMonto.Text)>0)
            {
                _newMonto = (_monto/(decimal) 1.18) * decimal.Parse(txtMonto.Text) / 100;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ingrese un monto correcto", "¡¡¡ERROR!!!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}
