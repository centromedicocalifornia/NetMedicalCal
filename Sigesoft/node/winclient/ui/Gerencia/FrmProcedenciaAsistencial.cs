using Infragistics.Win.UltraWinGrid;
using Sigesoft.Node.WinClient.BE;
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
    public partial class FrmProcedenciaAsistencial : Form
    {
        private ProcedenciaAsistencialBL oGerenciaProtocoloBl = new ProcedenciaAsistencialBL();
        private List<GerenciaProcedenciaAsistencial> _listGerenciaCredito = new List<GerenciaProcedenciaAsistencial>();

        public FrmProcedenciaAsistencial()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            using (new LoadingClass.PleaseWait(Location, "Generando..."))
            {
                _listGerenciaCredito = oGerenciaProtocoloBl.Filter(pdatBeginDate.Value, pdatEndDate.Value);
                grdTree.DataSource = oGerenciaProtocoloBl.ProcessDataTreeView(_listGerenciaCredito);
            }
        }

        private void FrmProcedenciaAsistencial_Load(object sender, EventArgs e)
        {
            dtpDateTimeStar.CustomFormat = @"dd/MM/yyyy";
            dptDateTimeEnd.CustomFormat = @"dd/MM/yyyy";
        }

        private void grdTree_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (grdTree.Selected.Rows.Count == 0) return;
            if (grdTree.Selected.Rows[0].Cells == null) return;

            foreach (UltraGridRow rowSelected in grdTree.Selected.Rows)
            {
                if (rowSelected.Band.Index.ToString() == "0")
                {
                    grdData.DataSource = _listGerenciaCredito;
                }
                else if (rowSelected.Band.Index.ToString() == "1")
                {
                    var procedencia = grdTree.Selected.Rows[0].Cells["TipoEso"].Value.ToString();
                    grdData.DataSource = _listGerenciaCredito.FindAll(p => p.Procedencia == procedencia).ToList();
                }
                else if (rowSelected.Band.Index.ToString() == "2")
                {
                    var tipoEso = grdTree.Selected.Rows[0].Cells["TipoEso"].Value.ToString();
                    var empresa = grdTree.Selected.Rows[0].Cells["Nombre"].Value.ToString();

                    grdData.DataSource = _listGerenciaCredito.FindAll(p => p.Procedencia == tipoEso && p.Tipo == empresa).ToList();
                }
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Procedencia de Servicios " + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text;
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
