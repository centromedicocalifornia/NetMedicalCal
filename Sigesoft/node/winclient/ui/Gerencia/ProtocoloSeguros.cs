using Infragistics.Win.UltraWinGrid;
using Sigesoft.Node.WinClient.BE.Custom;
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
    public partial class ProtocoloSeguros : Form
    {
        private GerenciaProtocoloSegurosBL oGerenciaProtocoloSegurosBl = new GerenciaProtocoloSegurosBL();
        private List<GerenciaProtocoloSeguros> _listProtocoloSeguros = new List<GerenciaProtocoloSeguros>();

        public ProtocoloSeguros()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            using (new LoadingClass.PleaseWait(Location, "Generando..."))
            {
                _listProtocoloSeguros = oGerenciaProtocoloSegurosBl.Filter(pdatBeginDate.Value, pdatEndDate.Value);
                grdTree.DataSource = oGerenciaProtocoloSegurosBl.ProcessDataTreeView(_listProtocoloSeguros);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Liquidación x Protocolo Seguros  " + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text;
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

        private void grdTree_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grdTree.Selected.Rows.Count == 0) return;
            if (grdTree.Selected.Rows[0].Cells == null) return;

            foreach (UltraGridRow rowSelected in grdTree.Selected.Rows)
            {
                if (rowSelected.Band.Index.ToString() == "0")
                {
                    grdData.DataSource = _listProtocoloSeguros;
                }
                else if (rowSelected.Band.Index.ToString() == "1")
                {
                    var protocolo = grdTree.Selected.Rows[0].Cells["TipoEso"].Value.ToString();
                    grdData.DataSource = _listProtocoloSeguros.FindAll(p => p.Protocolo == protocolo).ToList();
                }
                else if (rowSelected.Band.Index.ToString() == "2")
                {
                    var tipoEso = grdTree.Selected.Rows[0].Cells["TipoEso"].Value.ToString();
                    var empresa = grdTree.Selected.Rows[0].Cells["Nombre"].Value.ToString();

                    grdData.DataSource = _listProtocoloSeguros.FindAll(p => p.Protocolo == tipoEso && p.Tipo == empresa).ToList();
                }
            }
        }
    }
}
