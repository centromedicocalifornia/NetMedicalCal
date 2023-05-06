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
    public partial class FrmVentasAsistencial : Form
    {
        public FrmVentasAsistencial()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(Location, "Generando..."))
            {
                this.BindGrid();
            }
        }
        private void BindGrid()
        {
            GerenciaVentaBL o = new GerenciaVentaBL();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            var lista = o.Busqueda(pdatBeginDate.Value, pdatEndDate.Value);

            decimal TotalComisiones = 0;
            decimal soles = 0;
            decimal egresos = 0;
            decimal otros = 0;
            foreach (var item in lista)
            {
                if (item.Tipo == "EFECTIVO SOLES")
                {
                    soles = item.TotalGrupo;
                }
                if (item.Tipo == "EGRESO")
                {
                    egresos = item.TotalGrupo * -1;
                }
            }
            TotalComisiones = soles + egresos;
            lblPrecio.Text = TotalComisiones.ToString();
            grdData.DataSource = lista;

        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Ventas Asistencial " + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text;
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
