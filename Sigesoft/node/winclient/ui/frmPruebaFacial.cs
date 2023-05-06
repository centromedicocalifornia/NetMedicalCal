using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmPruebaFacial : Form
    {
        private string path;
        public frmPruebaFacial()
        {
            InitializeComponent();
        }

        private void frmPruebaFacial_Load(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            SaveFileDialog Guardar = new SaveFileDialog();
            Guardar.Filter = "JPEG(*.JPG)|*.JPG|BMP(*.BMP)|*.BMP";
            Image facial = pictureBox1.BackgroundImage;
            Guardar.FileName = "Facial";
            Guardar.ShowDialog();
            facial.Save(Guardar.FileName);
            Process.Start("C:\\Windows\\System32\\mspaint.exe",Guardar.FileName);
            path = Guardar.FileName;
            facial.Dispose();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = new Bitmap(path);
        }
    }
}
