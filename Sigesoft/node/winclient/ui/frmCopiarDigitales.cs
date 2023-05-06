using Sigesoft.Node.WinClient.BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmCopiarDigitales : Form
    {
        private BindingList<ServiceGridJerarquizadaList> _ListaGrilla = new BindingList<ServiceGridJerarquizadaList>();
        List<RutaRx> ListaRx = new List<RutaRx>();
        public frmCopiarDigitales(BindingList<ServiceGridJerarquizadaList> ListaGrilla)
        {
            _ListaGrilla = ListaGrilla;
            InitializeComponent();
        }

        private void frmCopiarDigitales_Load(object sender, EventArgs e)
        {
            if (rbDigitales.Checked = true)
            {
                rbDigitales_CheckedChanged(sender, e);
            }
            else
            {
                rbExpedientes_CheckedChanged(sender, e);
            }

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            var consolidado = Common.Utils.GetApplicationConfigValue("DesacargaAdjuntosMasivos").ToString();

            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                foreach (var item in ListaRx)
                {
                    //File.Copy(item.Ruta, consolidado + item.InformacionArchivo + item.Extension );

                    FileInfo fi = new FileInfo(item.Ruta);
                    fi.CopyTo(consolidado + item.InformacionArchivo + item.Extension, true);
                }

                MessageBox.Show("Guardado Correctamente, Vea la dirección: " + consolidado, "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rbDigitales_CheckedChanged(object sender, EventArgs e)
        {
            ListaRx = new List<RutaRx>();

            var consolidado = Common.Utils.GetApplicationConfigValue("ImgRxOrigen").ToString();

            foreach (var item in _ListaGrilla)
            {
                if (item.Rx_DCM == "SI" || item.Rx_JPG == "SI")
                {
                    DateTime a = DateTime.Parse(item.Fecha.ToString());

                    string ass = String.Format("{0:dd/MM/yyyy}", a);

                    var atencion = ass.Replace("/", "");

                    var listfiles = Directory.GetFiles(consolidado, "*" + item.v_DocNumber + "-" + atencion + "*", SearchOption.AllDirectories);

                    foreach (var item2 in listfiles)
                    {
                        RutaRx objRutaRx = new RutaRx();
                        objRutaRx.Ruta = item2;
                        objRutaRx.Extension = item2.Substring((item2.Length - 4), 4);
                        objRutaRx.InformacionArchivo = item.v_Pacient + "-" + item.v_DocNumber + "-" + atencion + "-" +
                                                       item.v_ServiceId;
                        ListaRx.Add(objRutaRx);
                    }
                }
            }

            grData.DataSource = ListaRx;

            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", ListaRx.Count());
        }

        private void btnExportExamenes_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Reporte Digitales CSL";
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grData, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rbExpedientes_CheckedChanged(object sender, EventArgs e)
        {
            ListaRx = new List<RutaRx>();

            var consolidado = Common.Utils.GetApplicationConfigValue("rutaConsolidado").ToString();
            var listfiles = Directory.GetFiles(consolidado, "*.*", SearchOption.AllDirectories);
            foreach (var item in _ListaGrilla)
            {
               
                if (item.i_StatusLiquidation.ToString() == "2")
                {

                
                    DateTime a = DateTime.Parse(item.Fecha.ToString());

                    string ass = String.Format("{0:dd/MM/yyyy}", a);

                    var atencion = ass.Replace("/", "");

                    //var listfiles = Directory.GetFiles(consolidado, "*" + item.v_ServiceId + "*", SearchOption.AllDirectories);
                    foreach (var item2 in listfiles)
                    {
                       if (item2.Contains(item.v_ServiceId) == true)
	                    {
                            RutaRx objRutaRx = new RutaRx();
                            objRutaRx.Ruta = item2;
                            objRutaRx.Extension = item2.Substring((item2.Length - 4), 4);
                            objRutaRx.InformacionArchivo = item.CompMinera + " - " +  item.v_OrganizationName + " - " + item.v_Pacient + "-" + item.v_DocNumber + "-" + atencion + "-" +
                                                           item.v_ServiceId;
                            objRutaRx.Fecha = item.Fecha.ToString();
                            ListaRx.Add(objRutaRx);
	                    }
                    }
                }
            }

            grData.DataSource = ListaRx;

            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", ListaRx.Count());
        }
    }
}
