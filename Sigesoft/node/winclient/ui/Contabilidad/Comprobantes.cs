using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.BE;
using System.IO;
using NetPdf;
using Infragistics.Win.UltraWinGrid;
using System.Diagnostics;
using System.Linq.Dynamic;
using System.Threading;
using System.Windows.Shell;
using Infragistics.Win.UltraWinDataSource;
using Sigesoft.Node.WinClient.UI.Reports;
using System.Data.SqlClient;
using Sigesoft.Node.WinClient.BE.Custom;

namespace Sigesoft.Node.WinClient.UI.Contabilidad
{
    public partial class Comprobantes : Form
    {
        string strFilterExpression;
        private OperationResult _objOperationResult = new OperationResult();

        public Comprobantes()
        {
            InitializeComponent();
            dtpDateTimeStar.Focus();
            dtpDateTimeStar.Select();
        }

        private void Comprobantes_Load(object sender, EventArgs e)
        {
        }

        private void BindGrid()
        {
            var objData = GetData(0, null, "", strFilterExpression);
            grdData.DataSource = objData;

            lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }

        private List<ServiceListComprobante> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            var usuarioActual = Globals.ClientSession.i_SystemUserId;

            var usuario_data = new ServiceBL().GetSystemUserId(usuarioActual);
            var usuario_professional = new ServiceBL().GetProfessional(usuario_data.v_PersonId);

            List<ServiceListComprobante> _objData = new List<ServiceListComprobante>();

            _objData = new ServiceBL().ListadeComprobantes(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate, usuario_professional.v_ComponentId, usuarioActual);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtPacient.Text)) Filters.Add("v_PacientDocument.Contains(\"" + txtPacient.Text.Trim() + "\")");
            if (!string.IsNullOrEmpty(txtServicioId.Text)) Filters.Add("v_ServiceId.Contains(\"" + txtServicioId.Text.Trim() + "\")");
            if (!string.IsNullOrEmpty(txtComprobante.Text)) Filters.Add("v_ComprobantePago.Contains(\"" + txtComprobante.Text.Trim() + "\")");

            // Create the Filter Expression
            strFilterExpression = null;
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    strFilterExpression = strFilterExpression + item + " && ";
                }
                strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
            }

            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.BindGrid();
            };

            
        }

        private void dtpDateTimeStar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
            //else if (e.KeyChar == (char)Keys.Escape)
            //{
            //    this.Close();
            //}
        }

        private void dptDateTimeEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void txtPacient_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void txtServicioId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void txtComprobante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Reporte Hospitalización del " + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text;
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

        private void btnLiqd1_Click(object sender, EventArgs e)
        {
            var coprobanteId = grdData.Selected.Rows[0].Cells["v_ComprobantePago"].Value.ToString();
            //var serviceID = grdData.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
            //var protocolId = grdData.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();

            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;

                var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();

                var lista = new ServiceBL().ListadeComprobantes2(ref _objOperationResult, coprobanteId);

                string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                string nombre = "Listado en comprobante ° " + coprobanteId + " - CSL";

                var traerEmpresa = new ServiceBL().ServiceIdbyComprobante(ref _objOperationResult, coprobanteId);
                string idEmpresa = traerEmpresa.v_OrganizationId;
                var obtenerInformacionEmpresas = new ServiceBL().GetOrganizationId(ref _objOperationResult, idEmpresa);
                ListaComprobante.CreateListaLiquidacion(ruta + nombre + ".pdf", MedicalCenter, lista, obtenerInformacionEmpresas);
                this.Enabled = true;
            }
        }
    }
}
