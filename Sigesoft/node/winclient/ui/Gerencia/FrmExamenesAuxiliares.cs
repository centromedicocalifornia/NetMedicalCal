using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Gerencia
{
    public partial class FrmExamenesAuxiliares : Form
    {
        public FrmExamenesAuxiliares()
        {
            InitializeComponent();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            string tipo = "";
            if (rbLaboratorio.Checked == true)
            {
                tipo = "Laboratorio";
            }
            else if (rbEcografia.Checked == true)
            {
                tipo = "Ecografias";

            }
            else if (rbRx.Checked == true)
            {
                tipo = "RayosX";
            }
            NombreArchivo = "Lista de Producción 5 % de  " + tipo + " " + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text;
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdData, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        

            {
                this.BindGrid();
            }
        }
        private void BindGrid()
        {
            GerenciaResonanciasBL o = new GerenciaResonanciasBL();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);
            int tipo = 0;
            if (rbLaboratorio.Checked == true)
            {
                tipo = 19;
            }
            else if (rbEcografia.Checked == true)
            {
                tipo = 173;
                
            }
            else if (rbRx.Checked == true)
            {
                tipo = 242;
            }
            var lista = o.ExamenesAuxiliares(pdatBeginDate.Value, pdatEndDate.Value, tipo);
            decimal TotalComisiones = 0;
            foreach (var item in lista)
            {
                TotalComisiones += decimal.Parse(item.Total_Reonancias.ToString());
            }
            lblPrecio.Text = TotalComisiones.ToString();
            grdData.DataSource = lista;

        }

        private void FrmExamenesAuxiliares_Load(object sender, EventArgs e)
        {
            rbLaboratorio.Checked = true;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(Location, "Generando..."))
            {
                this.BindGrid();
            }
        }

        private void grdData_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }
    }
}
