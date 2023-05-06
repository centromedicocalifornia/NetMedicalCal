using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Drawing.Imaging;
using Microsoft.Office.Interop.Excel;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Common;

namespace Sigesoft.Node.WinClient.UI.UserControls
{
    public partial class ucExFacial : UserControl
    {
        private string path;
        public string PersonId { get; set; }
        public string ServiceComponentId { get; set; }
        private byte[] b_PersonImage;
        public ucExFacial()
        {
            InitializeComponent();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog Guardar = new SaveFileDialog();
                Guardar.Filter = "JPEG(*.JPG)|*.JPG|BMP(*.BMP)|*.BMP";
                Image facial = pictureBox1.Image;
                Guardar.FileName = "FOTOTIPO_" + ServiceComponentId;
                Guardar.ShowDialog();
                facial.Save(Guardar.FileName);
                path = Guardar.FileName;
                if (checkDirecto.Checked == true)
                {
                    string _ejecutar = path;
                    Process proceso = new Process();
                    proceso.StartInfo.FileName = _ejecutar;
                    //proceso.Start();

                    //proceso.StartInfo.FileName = "mspaint.exe";
                    //proceso.StartInfo.Arguments = path;
                    proceso.Start();
                }
                else
                {
                    Process.Start("C:\\Windows\\System32\\mspaint.exe", Guardar.FileName);

                }

                //btnEditar.Enabled = false;

               
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                OperationResult pobjOperationResult = new OperationResult();
                FileInfoDto oFile = new FileInfoDto();
                pictureBox1.Image = new Bitmap(path);
                btnActualizar.Enabled = false;
                MemoryStream ms = new MemoryStream();
                Bitmap bm = new Bitmap(pictureBox1.Image);
                bm.Save(ms, ImageFormat.Jpeg);
                b_PersonImage = Common.Utils.ResizeUploadedImage(ms);
                MultimediaFileBL multimedia = new MultimediaFileBL();
                oFile.PersonId = PersonId;
                oFile.FileName = "FOTOTIPO - " + ServiceComponentId;
                oFile.ByteArrayFile = b_PersonImage;
                oFile.ServiceComponentId = ServiceComponentId;
                byte[] find = BuscarImagen(PersonId, ServiceComponentId);
                if (find.Length > 0) //Actualizar
                {
                    multimedia.UpdateMultimediaFileComponent(ref pobjOperationResult, oFile, Globals.ClientSession.GetAsList());
                }
                else //Insertar
                {
                    multimedia.AddMultimediaFileComponent(ref pobjOperationResult, oFile, Globals.ClientSession.GetAsList());
                }
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ucExFacial_Load(object sender, EventArgs e)
        {
            b_PersonImage = BuscarImagen(PersonId, ServiceComponentId);
            if (b_PersonImage.Length > 0)
            {
                pictureBox1.Image = Common.Utils.BytesArrayToImage(b_PersonImage, pictureBox1);
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            }
            else
            {
                pictureBox1.ImageLocation = "C:\\Program Files (x86)\\NetMedical\\Archivos\\Facial.jpg";
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        private byte[] BuscarImagen(string PersonId, string ServiceComponentId)
        {
            byte[] ImagenFacial = new byte[] { };
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            string cadena = "select MF.b_File from multimediafile MF " +
                            "inner join servicecomponentmultimedia SC on MF.v_MultimediaFileId=SC.v_MultimediaFileId " +
                            "where MF.v_PersonId='"+PersonId+"' and SC.v_ServiceComponentId='"+ServiceComponentId+"'";
            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                ImagenFacial = lector.GetValue(0) as byte[];
            }

            return ImagenFacial;
        }
    }
}
