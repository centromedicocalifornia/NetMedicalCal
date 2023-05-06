using NetPdf;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE.Custom;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Visor
{
    public partial class frmVisorPdf : Form
    {
        string PersonId = "";
        string Paciente = "";
        ServiceBL _servicios = new ServiceBL();
        List<string> listfiles;

        private MergeExPDF _mergeExPDF = new MergeExPDF();
        public List<string> _filesNameToMerge = new List<string>();
        List<HistorialAtencionesPac> _HistorialAtencionesPac = new List<HistorialAtencionesPac>();
        private int busqueda = 0;
        public frmVisorPdf(string _PersonId, string _Paciente)
        {
            PersonId = _PersonId;
            Paciente = _Paciente;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();

            if (fd.ShowDialog() == DialogResult.OK)
            {
                pdfRecetas.src = fd.FileName;
            }
            else
            {
                MessageBox.Show("¡Selecciona un documento!");
            }
        }

        private void frmVisorPdf_Load(object sender, EventArgs e)
        {
            string rutaDirectorio = string.Empty;
            OperationResult objOperationResult = new OperationResult();

            lblPaciente.Text = Paciente;

            Task.Factory.StartNew(() =>
            {
                _HistorialAtencionesPac = _servicios.getlistaatencionesparahistoria(PersonId);     
            }).ContinueWith(t =>
            {
                grData.DataSource = _HistorialAtencionesPac;

                lblRecordCount.Text = string.Format("Se encontraron {0} registros.", _HistorialAtencionesPac.Count());
                if (_HistorialAtencionesPac.Count() >= 1)
                {
                    btnExport.Enabled = true;
                }
                else
                {
                    btnExport.Enabled = false;
                }

                HistoriasMedicasAdjuntas(PersonId);

            }, TaskScheduler.FromCurrentSynchronizationContext());


            #region BUSCAR Y ABRIR ARCHIVO EN RUTA
            //FolderBrowserDialog fbd = new FolderBrowserDialog();

            //if (fbd.ShowDialog() == DialogResult.OK)
            //{
            //    rutaDirectorio = fbd.SelectedPath;
            //}

            //var ruta = Common.Utils.GetApplicationConfigValue("rutaExamenesAdicionales").ToString();
            //txtRuta.Text = ruta + "\\";

            //DirectoryInfo di = new DirectoryInfo(ruta);


            //foreach (var item in di.GetFiles())
            //{
            //    lbArchivos.Items.Add(item.Name);
            //}
            #endregion
        }

        //private void BindGrid()
        //{
        //    OperationResult objOperationResult = new OperationResult();
        //    lblPaciente.Text = Paciente;
        //    var objData = _servicios.getlistaatencionesparahistoria(PersonId);
        //    grData.DataSource = objData;

        //    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        //    if (objData.Count() >= 1)
        //    {
        //        btnExport.Enabled = true;
        //    }
        //    else
        //    {
        //        btnExport.Enabled = false;
        //    }


        //}

        private void lbArchivos_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lbArchivos.SelectedItem.ToString().Split('.')[1] == "pdf")
            {
                pdfRecetas.src = txtRuta.Text + lbArchivos.SelectedItem.ToString();
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grData_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            try
            {
                string servicio = grData.Selected.Rows[0].Cells["Servicio"].Value.ToString();
                if (servicio != null)
                {
                    #region ANTERIOR METODO
                    //var rutaHistoria = Common.Utils.GetApplicationConfigValue("rutaConsolidado").ToString();
                    //string _rutaHistoria = rutaHistoria + "\\";

                    //DirectoryInfo diHistoria = new DirectoryInfo(_rutaHistoria);
                    //List<ListaArchivosPac> ListaHistorias = new List<ListaArchivosPac>();
                    //foreach (var item in diHistoria.GetFiles())
                    //{
                    //    ListaArchivosPac objListaHistorias = new ListaArchivosPac();
                    //    objListaHistorias.Ruta = _rutaHistoria;
                    //    objListaHistorias.Archivo = item.Name;
                    //    ListaHistorias.Add(objListaHistorias);
                    //}



                    //foreach (var item in diHistoria.GetDirectories())
                    //{
                    //    DirectoryInfo rutaTemp = new DirectoryInfo(item.FullName + "\\\\");
                    //    foreach (var item2 in rutaTemp.GetFiles())
                    //    {
                    //        ListaArchivosPac objListaHistorias = new ListaArchivosPac();
                    //        objListaHistorias.Ruta = item.FullName + "\\\\";
                    //        objListaHistorias.Archivo = item2.Name;
                    //        ListaHistorias.Add(objListaHistorias);
                    //    }
                    //}
                    #endregion

                    //Task.Factory.StartNew(() =>
                    //{
                        HistoriasMedicas(servicio);

                        InterconsultasMedicas(servicio);

                        RecetasMedicas(servicio);


                        ExamenesAdicionales(servicio);

                        ArchivosAdjuntos();

                        //tmpTotalDiagnosticByServiceIdList = new ServiceBL().GetServiceComponentDisgnosticsByServiceId(ref _objOperationResult, _serviceId);
                    //}).ContinueWith(t =>
                    //{
                        //grdTotalDiagnosticos.DataSource = tmpTotalDiagnosticByServiceIdList;
                        //lblRecordCountTotalDiagnosticos.Text = string.Format("Se encontraron {0} registros.", tmpTotalDiagnosticByServiceIdList.Count());
                        //SetCurrentRow();

                    //}, TaskScheduler.FromCurrentSynchronizationContext());

                    
                }
                _filesNameToMerge = new List<string>();
            }
            catch (Exception)
            {
                MessageBox.Show("Error en operación:", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        }

        private void ArchivosAdjuntos()
        {
            string atencion = grData.Selected.Rows[0].Cells["Atencion"].Value.ToString(); //.Split(' ')[0].Trim();

            DateTime a = DateTime.Parse(atencion);

            string ass = String.Format("{0:dd/MM/yyyy}", a);

            atencion = ass.Replace("/", "");

            string personId = grData.Selected.Rows[0].Cells["Historia"].Value.ToString();

            var rutaAdjuntosRx = Common.Utils.GetApplicationConfigValue("ImgRxOrigen").ToString();
            string _rutaAdjuntosRx = rutaAdjuntosRx + "\\";

            DirectoryInfo diAdjuntosRx = new DirectoryInfo(_rutaAdjuntosRx);
            List<ListaArchivosPac> _ListaArchivos = new List<ListaArchivosPac>();

            var listaBGlobal = diAdjuntosRx.GetFiles().ToList();
            var ListaFiltrada = listaBGlobal.FindAll(p => p.Name.Contains(atencion) && p.Name.Contains(personId));

            foreach (var item in ListaFiltrada)
            {
               
                    ListaArchivosPac objListaArchivosPac = new ListaArchivosPac();
                    objListaArchivosPac.Archivo = item.Name;
                    objListaArchivosPac.Ruta = rutaAdjuntosRx;
                    objListaArchivosPac.Origen = "Rayos X";
                    _ListaArchivos.Add(objListaArchivosPac);
              
            }

            ultraGrid1.DataSource = _ListaArchivos;
        }

        private void ExamenesAdicionales(string servicio)
        {
            var rutaOrdenesMedicas = Common.Utils.GetApplicationConfigValue("rutaExamenesAdicionales").ToString();
            string _rutaOrdenesMedicas = rutaOrdenesMedicas + "\\";

            DirectoryInfo diOrdenesMedicas = new DirectoryInfo(_rutaOrdenesMedicas);

            var listaBGlobal = diOrdenesMedicas.GetFiles().ToList();
            var ListaFiltrada = listaBGlobal.FindAll(p => p.Name.Contains(servicio));
            if (ListaFiltrada.Count == 0)
            {
                panelOrdenes.Visible = true;
                panelOrdenes.Dock = DockStyle.Fill;
                pdfOrdenesMedicas.src = null;
            }
            else
            {
                foreach (var item in diOrdenesMedicas.GetFiles())
                {
                    panelOrdenes.Visible = false;
                    pdfOrdenesMedicas.src = _rutaOrdenesMedicas + item.Name;
                    break;
                }    
            } 
        }

        private void RecetasMedicas(string servicio)
        {
            var rutaRecetas = Common.Utils.GetApplicationConfigValue("Receta").ToString();
            string _rutaRecetas = rutaRecetas + "\\";
            string rutaBasura = Common.Utils.GetApplicationConfigValue("rutaReportesBasura").ToString();

            DirectoryInfo diRecetas = new DirectoryInfo(_rutaRecetas);

            var listaBGlobal = diRecetas.GetFiles().ToList();
            var ListaFiltrada = listaBGlobal.FindAll(p => p.Name.Contains(servicio));

            if (ListaFiltrada.Count == 0)
            {
                pdfRecetas.src = null;
            }
            else
            {
                foreach (var item in ListaFiltrada)
                {
                    _filesNameToMerge.Add(_rutaRecetas + item.Name);                
                }
            }
            

            if (_filesNameToMerge.Count != 0)
            {
                panelRecetas.Visible = false;
                var x = _filesNameToMerge.ToList();
                _mergeExPDF.FilesName = x;
                _mergeExPDF.DestinationFile = Application.StartupPath + @"\TempMerge\" + servicio + ".pdf";
                _mergeExPDF.DestinationFile = rutaBasura + servicio + ".pdf";
                _mergeExPDF.Execute();
                pdfRecetas.src = _mergeExPDF.DestinationFile;
                //_mergeExPDF.RunFile();
            }
            else
            {
                panelRecetas.Visible = true;
                panelRecetas.Dock = DockStyle.Fill;
            }
        }

        private void InterconsultasMedicas(string servicio)
        {
            var rutaInterconsultas = Common.Utils.GetApplicationConfigValue("Interconsulta").ToString();
            string _rutaInterconsultas = rutaInterconsultas + "\\";

            DirectoryInfo diInterconsultas = new DirectoryInfo(_rutaInterconsultas);

            var listaBGlobal = diInterconsultas.GetFiles().ToList();
            var ListaFiltrada = listaBGlobal.FindAll(p => p.Name.Contains(servicio));

            if (ListaFiltrada.Count == 0)
            {
                panelInterconsultas.Visible = true;
                panelInterconsultas.Dock = DockStyle.Fill;
                pdfInterconsultas.src = null;
            }
            else
            {
                foreach (var item in ListaFiltrada)
                {
                    panelInterconsultas.Visible = false;
                    pdfInterconsultas.src = _rutaInterconsultas + item.Name;
                    break;
                }
            }
            
        }

        private void HistoriasMedicas_OLD(string servicio)
        {
            var ListaGlobal = listfiles.FindAll(p => p.Contains(servicio));

            if (ListaGlobal.Count == 0)
            {
                panelMensaje.Visible = true;
                panelMensaje.Dock = DockStyle.Fill;
            }
            else
            {
                foreach (var item in ListaGlobal)
                {
                    string atencion_ = grData.Selected.Rows[0].Cells["Atencion"].Value.ToString(); //.Split(' ')[0].Trim();

                    //DateTime b = DateTime.Parse(atencion_);
                    //string fechaConvertida = String.Format("{0:dd MMMM,  yyyy}", b);
                    
                    panelMensaje.Visible = false;
                    pdfAtencion.src = item;
                    break;
                }
            } 
        }


        private void HistoriasMedicas(string servicio)
        {
            try
            {
                var consolidado = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();
                string _rutaconsolidado = consolidado + "\\";


                panelMensaje.Visible = false;
                pdfAtencion.src = _rutaconsolidado + servicio + ".pdf";

            }
            catch (Exception)
            {
                panelMensaje.Visible = true;
                panelMensaje.Dock = DockStyle.Fill;
            }
        }



        private void btnExport_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Historial de Atenciones Paciente: " + lblPaciente.Text;
           
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

        private void button2_Click(object sender, EventArgs e)
        {
            string archivo = ultraGrid1.Selected.Rows[0].Cells["Archivo"].Value.ToString();
            if (archivo != null)
            {
                //if (archivo.Contains(".dcm"))
                //{
                    string ruta = ultraGrid1.Selected.Rows[0].Cells["Ruta"].Value.ToString();
                    string _ejecutar = ruta + archivo;
                    Process proceso = new Process();
                    proceso.StartInfo.FileName = _ejecutar;
                    proceso.Start();
                    //Process.Start("C:\\Program Files\\MicroDicom\\mDicom.exe", _ejecutar);
                    //path = Guardar.FileName;
                //}
            }

            
            
        }

        private void btnReportAsync_Click(object sender, EventArgs e)
        {
            try
            {
                string servicio = grData.Selected.Rows[0].Cells["Servicio"].Value.ToString();

                string aptitud = VerificarAptitud(servicio);

                string _EmpresaClienteId = grData.Selected.Rows[0].Cells["Anexos"].Value.ToString();

                var frm = new Reports.frmManagementReports_Async(servicio, _EmpresaClienteId, "",
                    "", "", "", aptitud);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //btnFilter_Click(sender, e);
            }
        }

        private string VerificarAptitud(string _serviceId)
        {
            //ARNODL STORE
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena1 = @"select SP.v_Value1 from service SR " +
                          "inner join systemparameter SP on SR.i_AptitudeStatusId=SP.i_ParameterId and SP.i_GroupId=124 " +
                          "where v_ServiceId='" + _serviceId + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector1 = comando.ExecuteReader();
            string aptitud = "";
            while (lector1.Read())
            {
                aptitud = lector1.GetValue(0).ToString();
            }
            lector1.Close();
            conectasam.closesigesoft();
            return aptitud;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta = Common.Utils.GetApplicationConfigValue("rutaHistMedAdjunta").ToString();
                string destino = ruta + PersonId + "-" + Paciente + ".pdf";
                if (File.Exists(destino))
                {
                    System.IO.File.Delete(destino);
                    File.Copy(openFileDialog1.FileName, destino);
                }
                else { File.Copy(openFileDialog1.FileName, destino); }
                MessageBox.Show("El archivo se anexó correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.Close();
                HistoriasMedicasAdjuntas(PersonId);
                btnAceptar.Enabled = false;
            }
            catch (Exception)
            {
            }
            

        }

        private void HistoriasMedicasAdjuntas(string person)
        {
            try
            {
                var consolidado = Common.Utils.GetApplicationConfigValue("rutaHistMedAdjunta").ToString();
                string _rutaconsolidado = consolidado + "\\";


                //panelMensaje.Visible = false;
                pdfHistoriaAdjunta.src = _rutaconsolidado + person + "-" + Paciente + ".pdf";

            }
            catch (Exception)
            {
                //panelMensaje.Visible = true;
                //panelMensaje.Dock = DockStyle.Fill;
                //throw;
            }


        }


        private void btnBrowser_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var fileSize = Convert.ToInt32(Convert.ToSingle(Common.Utils.GetFileSizeInMegabytes(openFileDialog1.FileName)));

                if (fileSize > 7)
                {
                    MessageBox.Show("La imagen que está tratando de subir es damasiado grande.\nEl tamaño maximo es de 7 MB.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                txtFileName.Text = Path.GetFileName(openFileDialog1.FileName);
                btnAceptar.Enabled = true;
            }
            else
            {
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Validar validacion = new Validar();
            validacion.ShowDialog();
            if (validacion.Respuesta == "ACEPTADO")
            {
                var consolidado = Common.Utils.GetApplicationConfigValue("rutaHistMedAdjunta").ToString();
                string _rutaconsolidado = consolidado + "\\";

                string fileName = _rutaconsolidado + PersonId + "-" + Paciente + ".pdf";

                if (File.Exists(fileName))
                {
                    try
                    {
                        File.Delete(fileName);
                        MessageBox.Show("Archivo eliminado correctamente.", "INFORMACIÓN", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        HistoriasMedicasAdjuntas(PersonId);


                    }
                    catch (Exception f)
                    {
                        MessageBox.Show("No se puede eliminar archivo", "Advertencia", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        HistoriasMedicasAdjuntas(PersonId);

                    }
                }
                else
                {
                    MessageBox.Show("No se puede eliminar archivo", "Advertencia", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
        }
    }
}
