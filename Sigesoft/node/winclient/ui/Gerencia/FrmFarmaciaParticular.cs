using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;

namespace Sigesoft.Node.WinClient.UI.Gerencia
{
    public partial class FrmFarmaciaParticular : Form
    {
        public FrmFarmaciaParticular()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(Location, "Generando..."))
            {
                //grdData.DataSource = o.Filter(pdatBeginDate.Value, pdatEndDate.Value);
                this.BindGrid();
            }
        }
        private void BindGrid()
        {
            GerenciaFarmaciaAsistencialBL o = new GerenciaFarmaciaAsistencialBL();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

           var lista = o.Filter(pdatBeginDate.Value, pdatEndDate.Value);
           decimal TotalComisiones = 0;
           foreach (var item in lista)
           {
               TotalComisiones += decimal.Parse(item.TotalComision.ToString());
           }
           lblPrecio.Text = TotalComisiones.ToString();
           grdData.DataSource = lista;

        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Liquidación % de  Medicinas para " + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text;
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdData, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
