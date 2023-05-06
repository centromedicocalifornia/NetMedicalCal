using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.MKT_DIGITAL
{
    public partial class frmMoivoEliminacionDigital : Form
    {
        public string motivo;
        public frmMoivoEliminacionDigital()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            motivo = txtDescripcion.Text;
            this.Close();
        }
    }
}
