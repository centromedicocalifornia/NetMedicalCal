using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmServiceYComponentesGraficosDiariosInd : Form
    {
        ArrayList PorDia_Tipe = new ArrayList();
        ArrayList PorDia_Cant = new ArrayList();
        private string titulo;

        public frmServiceYComponentesGraficosDiariosInd(ArrayList _PorDia_Tipe, ArrayList _PorDia_Cant, string _titulo)
        {
            PorDia_Tipe = _PorDia_Tipe;
            PorDia_Cant = _PorDia_Cant;
            titulo = _titulo;

            InitializeComponent();
        }

        private void frmServiceYComponentesGraficosDiariosInd_Load(object sender, EventArgs e)
        {
            chartGrafxDia.Titles[0].Text = titulo;
            chartGrafxDia.Series[0].Points.DataBindXY(PorDia_Tipe, PorDia_Cant);
        }

        private void chartGrafxDia_Click(object sender, EventArgs e)
        {
            //frmServiceYComponentesGraficosDiarios ticket = new frmServiceYComponentesGraficosDiarios(ListaGrilla.ToList());
            //ticket.ShowDialog();
        }

        private void btnDescargarCurvas_Click(object sender, EventArgs e)
        {
            GuardarGrafico(chartGrafxDia, titulo);
        }

        private void GuardarGrafico(Chart graficoGuardar, string nombreGuardara)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Imagen|*.jpg|Bitmap Imagen|*.bmp|PNG Imagen|*.png";
            saveFileDialog1.Title = "Guardar Grafico en Imagen";
            saveFileDialog1.FileName = nombreGuardara.Replace("/", "-");
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        graficoGuardar.SaveImage(fs, ChartImageFormat.Jpeg);
                        MessageBox.Show("Guardada correctamente.");
                        break;
                    case 2:
                        graficoGuardar.SaveImage(fs, ChartImageFormat.Bmp);
                        MessageBox.Show("Guardada correctamente.");
                        break;
                    case 3:
                        graficoGuardar.SaveImage(fs, ChartImageFormat.Png);
                        MessageBox.Show("Guardada correctamente.");
                        break;
                }
                fs.Close();
            }
        }
    }
}
