using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.DAL;
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
    public partial class frmDescargarAdjuntosConsultorios : Form
    {
        string consultorio_;
        string servicio_;
        string persona_;
        string nombrePersona_;
        string dni_;
        string fecha;

        public frmDescargarAdjuntosConsultorios(string consultorio, string servicio, string persona, string nombrePersona, string dni)
        {
            consultorio_ = consultorio;
            servicio_ = servicio;
            persona_ = persona;
            nombrePersona_ = nombrePersona;
            dni_ = dni;
            InitializeComponent();
        }

        private void frmDescargarAdjuntosConsultorios_Load(object sender, EventArgs e)
        {
            //using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            //{
                this.BindGrid();
            //}
            
        }
        private void BindGrid()
        {
            try
            {
                OperationResult operationResult = new OperationResult();
                lblNombreservicio.Text = nombrePersona_ + " - " + servicio_;
                List<servicecomponent> serviceComponent = new ServiceBL().GetServiceComponentList(servicio_);

                if (consultorio_ == "RX")
                {
                    servicecomponent rxComponent = serviceComponent.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.OIT_ID).FirstOrDefault();
                    if (rxComponent != null)
                    {
                        var _multimediaFiles1 = new MultimediaFileBL().GetMultimediaFiles(ref operationResult, rxComponent.v_ServiceComponentId);
                        grdDataService.DataSource = _multimediaFiles1;
                        fecha = rxComponent.d_InsertDate.Value.ToShortDateString();
                    }

                }
            }
            catch (Exception ex)
            {
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void GetDataFromDataGridView()
        {
            if (grdDataService.Selected.Rows.Count == 0)
                return;

            var _byteArrayFile = (byte[])grdDataService.Selected.Rows[0].Cells["ThumbnailFile"].Value;

            if (_byteArrayFile != null)
                pbProductImage.Image = Sigesoft.Common.Utils.byteArrayToImage(_byteArrayFile);
   
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo rutaOrigen = null;
                string rutaDestino = null;
                DirectoryInfo ruta = null;
                fecha = DateTime.Now.ToShortDateString();
                fecha = fecha.Replace("/", "-");

                if (consultorio_ == "RX")
                {
                    rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgRxOrigen").ToString());
                    ruta = new DirectoryInfo(("C:\\Users\\" + Environment.UserName.ToString() + "\\Downloads\\").ToString());

                    rutaDestino = ("C:\\Users\\" + Environment.UserName.ToString() + "\\Downloads\\").ToString() + nombrePersona_ + "-" + dni_ + "-" + fecha + "-" + servicio_;

                    var FileName = grdDataService.Selected.Rows[0].Cells["FileName"].Value.ToString();

                    FileInfo[] files = rutaOrigen.GetFiles();

                    foreach (FileInfo file in files)
                    {
                        if (file.Name == FileName)
                        {
                            string ext = Path.GetExtension(file.ToString());
                            file.CopyTo(Path.Combine(rutaDestino + ext), true);
                        }
                    }

                    MessageBox.Show("Los archivos se copiaron correctamente en la siguiente ruta: " + ruta);
                    System.Diagnostics.Process.Start(ruta.ToString());
                    Clipboard.SetText(nombrePersona_ + "-" + dni_ + "-" + fecha + "-" + servicio_);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("A ocurrido un error: ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            }
        }

        private void ultraDataSource1_CellDataRequested(object sender, Infragistics.Win.UltraWinDataSource.CellDataRequestedEventArgs e)
        {

        }


        private void gbImage_Enter(object sender, EventArgs e)
        {

        }

        private void grdDataService_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            GetDataFromDataGridView();
        }

    }
}
