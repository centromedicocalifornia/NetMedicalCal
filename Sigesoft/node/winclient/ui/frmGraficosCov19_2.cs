using Sigesoft.Node.WinClient.BE.Custom;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Imaging;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmGraficosCov19_2 : Form
    {
        DateTime inicio_;
        DateTime fin_;

        ArrayList Tipo_1 = new ArrayList();
        ArrayList Cantidad_1 = new ArrayList();

        ArrayList TipoIgM = new ArrayList();
        ArrayList CantidadIgM = new ArrayList();

        ArrayList TipoIgMIgG = new ArrayList();
        ArrayList CantidadIgMIgG = new ArrayList();

        ArrayList TipoIgG = new ArrayList();
        ArrayList CantidadIgG = new ArrayList();

        ArrayList TipoPOSITIVO = new ArrayList();
        ArrayList CantidadPOSITIVO = new ArrayList();

        ArrayList TipoPositivos = new ArrayList();
        ArrayList CantidadPositivos = new ArrayList();

        ArrayList PorcentajesPositivos = new ArrayList();
        ArrayList PorcentajesPositivosCantidad = new ArrayList();
        List<ServiceCovid19> DataSource_1 = new List<ServiceCovid19>();


        ArrayList Tipo = new ArrayList();
        ArrayList Cantidad = new ArrayList();

        ArrayList TipoSERVICIO = new ArrayList();
        ArrayList CantidadSERVICIO = new ArrayList();

        ArrayList Sexo_Tipe = new ArrayList();
        ArrayList Sexo_Cant = new ArrayList();

        ArrayList Etareo_Tipe = new ArrayList();
        ArrayList Etareo_Cant = new ArrayList();

        ArrayList Etareo_Tipe_Rest = new ArrayList();
        ArrayList Etareo_Cant_Rest = new ArrayList();
        string nombrePositivos = "";
        string _chartPositivosPorc = "";
        public frmGraficosCov19_2(DateTime inicio, DateTime fin, List<ServiceCovid19> _DataSource_1)
        {
            inicio_ = inicio;
            fin_ = fin;
            DataSource_1 = _DataSource_1;
            InitializeComponent();
        }

        private void frmGraficosCov19_2_Load(object sender, EventArgs e)
        {
            BindingGrid();
        }

        private void BindingGrid()
        {
            
            List<ServiceCovid19> DataSource_2 = new List<ServiceCovid19>();
            List<ServiceCovid19> DataSource_3 = new List<ServiceCovid19>();
            List<ServiceCovid19> DataSource_4 = new List<ServiceCovid19>();

            //DataSource_1 = new ServiceBL().GetServicesCovid19(inicio_, fin_);
            //var result = query.GroupBy(g => g.v_ServiceId).Select(s => s.First()).ToList();
            DataSource_1 = DataSource_1.OrderBy(p => p.FECHA.Value).ToList();

            DataSource_2 = DataSource_1.GroupBy(p => p.FECHA.Value.ToShortDateString()).Select(s => s.First()).ToList();

            int countigm = 0;
            int countigmigg = 0;
            int countigg = 0;
            int countpositivo = 0;
            decimal porcentaje = 0;
            decimal cantidad = 0;
            
            int cantidadPositivos = 0;
            foreach (var item in DataSource_2)
            {
                //Tipo.Add(item.FECHA.ToString().Split('/')[0]);
                //int cantidad = 0;
                //cantidad = DataSource_1.Count(c => c.FECHA.Value.ToShortDateString() == item.FECHA.Value.ToShortDateString());
                //Cantidad.Add(cantidad);

                DataSource_3 = DataSource_1.FindAll(p => p.FECHA.Value.ToShortDateString() == item.FECHA.Value.ToShortDateString()).ToList();

                DataSource_4= DataSource_3.GroupBy(p => p.RESULTADO).Select(s => s.First()).ToList(); 

                
                foreach (var item2 in DataSource_4)
                {
                    if (item2.RESULTADO == "IgM Positivo")
                    {
                        List<ServiceCovid19> DataSource_Cont = new List<ServiceCovid19>();
                        DataSource_Cont = DataSource_3.FindAll(p => p.RESULTADO == "IgM Positivo").ToList();
                        countigm = DataSource_Cont.Count();
                    }
                    else if (item2.RESULTADO == "IgM e IgG Positivo")
                    {
                        List<ServiceCovid19> DataSource_Cont = new List<ServiceCovid19>();
                        DataSource_Cont = DataSource_3.FindAll(p => p.RESULTADO == "IgM e IgG Positivo").ToList();
                        countigmigg = DataSource_Cont.Count();
                    }
                    else if (item2.RESULTADO == "IgG Positivo")
                    {
                        List<ServiceCovid19> DataSource_Cont = new List<ServiceCovid19>();
                        DataSource_Cont = DataSource_3.FindAll(p => p.RESULTADO == "IgG Positivo").ToList();
                        countigg = DataSource_Cont.Count();
                    }
                    //else if (item2.RESULTADO == "POSITIVO")
                    //{
                    //    List<ServiceCovid19> DataSource_Cont = new List<ServiceCovid19>();
                    //    DataSource_Cont = DataSource_3.FindAll(p => p.RESULTADO == "POSITIVO").ToList();
                    //    countpositivo = DataSource_Cont.Count();
                    //}
                }
                TipoIgM.Add(item.FECHA.ToString().Split('/')[0] + "/" + item.FECHA.ToString().Split('/')[1]);
                CantidadIgM.Add(countigm);

                TipoIgMIgG.Add(item.FECHA.ToString().Split('/')[0] + "/" + item.FECHA.ToString().Split('/')[1]);
                CantidadIgMIgG.Add(countigmigg);

                TipoIgG.Add(item.FECHA.ToString().Split('/')[0] + "/" + item.FECHA.ToString().Split('/')[1]);
                CantidadIgG.Add(countigg);

                //TipoPOSITIVO.Add(item.FECHA.ToString().Split('/')[0] + "/" + item.FECHA.ToString().Split('/')[1]);
                //CantidadPOSITIVO.Add(countpositivo);

                porcentaje = Decimal.Round((Convert.ToDecimal((countigm + countigmigg + countigg) * 100) / DataSource_3.Count()), 2);

                Tipo_1.Add(item.FECHA.ToString().Split('/')[0] + "/" + item.FECHA.ToString().Split('/')[1]);
                
                cantidad = Decimal.Round((100 - porcentaje),2);
                Cantidad_1.Add(cantidad);

                //positivos diarios
                TipoPositivos.Add(item.FECHA.ToString().Split('/')[0] + "/" + item.FECHA.ToString().Split('/')[1]);
                cantidadPositivos = countigm + countigmigg + countigg;
                CantidadPositivos.Add(cantidadPositivos);

                PorcentajesPositivos.Add(item.FECHA.ToString().Split('/')[0] + "/" + item.FECHA.ToString().Split('/')[1]);
                PorcentajesPositivosCantidad.Add(porcentaje);

                countigm = countigmigg = countigg = 0;
                cantidad = cantidadPositivos = 0;
            }


            DateTime _finOriginal = fin_.Date.AddDays(-1);
            chartPositivosPorc.Titles[0].Text = _chartPositivosPorc =  "PACIENTES ATENDIDOS DEL " + inicio_.ToShortDateString() + " AL " + _finOriginal.ToShortDateString() + " COVID-19 ";

            chartPositivosPorc.Series[0].Points.DataBindXY(Tipo_1, Cantidad_1);
            chartPositivosPorc.Series[1].Points.DataBindXY(PorcentajesPositivos, PorcentajesPositivosCantidad);

            //

            

            chartPositivos.Series[0].Points.DataBindXY(TipoIgM, CantidadIgM);
            chartPositivos.Series[1].Points.DataBindXY(TipoIgMIgG, CantidadIgMIgG);
            chartPositivos.Series[2].Points.DataBindXY(TipoIgG, CantidadIgG);

            chartPositivos.Titles[0].Text = nombrePositivos = "CURVA DE RESULTADOS POSITIVOS COVID-19 DEL" + inicio_.ToShortDateString() + " AL " + _finOriginal.ToShortDateString();
            
            //chartPastel.Series[0].Points.DataBindXY(Tipo, Cantidad);

            chartPositivosDiarios.Series[0].Points.DataBindXY(TipoPositivos, CantidadPositivos);
            chartPositivosDiarios.Titles[0].Text = "GRÁFICO LINEAL DE RESULTADOS POSITIVOS COVID-19 DEL" + inicio_.ToShortDateString() + " AL " + _finOriginal.ToShortDateString();

            #region GRUPO ETAREO

            int niño_c = 0;
            int niño_val = 0;

            int niño_IgG = 0;
            int niño_IgM = 0;
            int niño_IgG_IgM = 0;
            int niño_negativo = 0;

            int adolescente_c = 0;
            int adolescente_val = 0;

            int adolescente_IgG = 0;
            int adolescente_IgM = 0;
            int adolescente_IgG_IgM = 0;
            int adolescente_negativo = 0;

            int joven_c = 0;
            int joven_val = 0;

            int joven_IgG = 0;
            int joven_IgM = 0;
            int joven_IgG_IgM = 0;
            int joven_negativo = 0;

            int adulto_c = 0;
            int adult_val = 0;

            int adulto_IgG = 0;
            int adulto_IgM = 0;
            int adulto_IgG_IgM = 0;
            int adulto_negativo = 0;

            int adulto_mayor_c = 0;
            int adulto_mayor_val = 0;

            int adulto_mayor_IgG = 0;
            int adulto_mayor_IgM = 0;
            int adulto_mayor_IgG_IgM = 0;
            int adulto_mayor_negativo = 0;

            foreach (var item in DataSource_1)
            {
                if (item.EDAD >= 0 && item.EDAD <= 11)//niño
                {
                    if (item.RESULTADO == "NEGATIVO" || item.RESULTADO == "NO VÁLIDO")
                    {
                        niño_negativo++;
                    }
                    else if (item.RESULTADO == "IgM Positivo")
                    {
                        niño_IgM++;
                    }
                    else if (item.RESULTADO == "IgG Positivo")
                    {
                        niño_IgG++;
                    }
                    else if (item.RESULTADO == "IgM e IgG Positivo")
                    {
                        niño_IgG_IgM++;
                    }
                    niño_c++;
                }
                else if (item.EDAD >= 12 && item.EDAD <= 17)//adolescente
                {
                    if (item.RESULTADO == "NEGATIVO" || item.RESULTADO == "NO VÁLIDO")
                    {
                        adolescente_negativo++;
                    }
                    else if (item.RESULTADO == "IgM Positivo")
                    {
                        adolescente_IgM++;
                    }
                    else if (item.RESULTADO == "IgG Positivo")
                    {
                        adolescente_IgG++;
                    }
                    else if (item.RESULTADO == "IgM e IgG Positivo")
                    {
                        adolescente_IgG_IgM++;
                    }
                    adolescente_c++;
                }
                else if (item.EDAD >= 18 && item.EDAD <= 29)//joven
                {
                    if (item.RESULTADO == "NEGATIVO" || item.RESULTADO == "NO VÁLIDO")
                    {
                        joven_negativo++;
                    }
                    else if (item.RESULTADO == "IgM Positivo")
                    {
                        joven_IgM++;
                    }
                    else if (item.RESULTADO == "IgG Positivo")
                    {
                        joven_IgG++;
                    }
                    else if (item.RESULTADO == "IgM e IgG Positivo")
                    {
                        joven_IgG_IgM++;
                    }
                    joven_c++;
                }
                else if (item.EDAD >= 30 && item.EDAD <= 59)//adulto
                {
                    if (item.RESULTADO == "NEGATIVO" || item.RESULTADO == "NO VÁLIDO")
                    {
                        adulto_negativo++;
                    }
                    else if (item.RESULTADO == "IgM Positivo")
                    {
                        adulto_IgM++;
                    }
                    else if (item.RESULTADO == "IgG Positivo")
                    {
                        adulto_IgG++;
                    }
                    else if (item.RESULTADO == "IgM e IgG Positivo")
                    {
                        adulto_IgG_IgM++;
                    }
                    adulto_c++;
                }
                else if (item.EDAD >= 60)//adulto mayor
                {
                    if (item.RESULTADO == "NEGATIVO" || item.RESULTADO == "NO VÁLIDO")
                    {
                        adulto_mayor_negativo++;
                    }
                    else if (item.RESULTADO == "IgM Positivo")
                    {
                        adulto_mayor_IgM++;
                    }
                    else if (item.RESULTADO == "IgG Positivo")
                    {
                        adulto_mayor_IgG++;
                    }
                    else if (item.RESULTADO == "IgM e IgG Positivo")
                    {
                        adulto_mayor_IgG_IgM++;
                    }
                    adulto_mayor_c++;
                }

            }
            Etareo_Tipe.Add("0-11");
            Etareo_Tipe.Add("12-17");
            Etareo_Tipe.Add("18-29");
            Etareo_Tipe.Add("30-59");
            Etareo_Tipe.Add("Mayores 60");

            Etareo_Cant.Add(niño_c);
            Etareo_Cant.Add(adolescente_c);
            Etareo_Cant.Add(joven_c);
            Etareo_Cant.Add(adulto_c);
            Etareo_Cant.Add(adulto_mayor_c);

            chartEtareo.Series[0].Points.DataBindXY(Etareo_Tipe, Etareo_Cant);

            chartEtarioResult.Series[0].Points.AddXY("0-11", niño_negativo);
            chartEtarioResult.Series[1].Points.AddXY("0-11", niño_IgM);
            chartEtarioResult.Series[2].Points.AddXY("0-11", niño_IgG_IgM);
            chartEtarioResult.Series[3].Points.AddXY("0-11", niño_IgG);

            chartEtarioResult.Series[0].Points.AddXY("12-17", adolescente_negativo);
            chartEtarioResult.Series[1].Points.AddXY("12-17", adolescente_IgM);
            chartEtarioResult.Series[2].Points.AddXY("12-17", adolescente_IgG_IgM);
            chartEtarioResult.Series[3].Points.AddXY("12-17", adolescente_IgG);

            chartEtarioResult.Series[0].Points.AddXY("18-29", joven_negativo);
            chartEtarioResult.Series[1].Points.AddXY("18-29", joven_IgM);
            chartEtarioResult.Series[2].Points.AddXY("18-29", joven_IgG_IgM);
            chartEtarioResult.Series[3].Points.AddXY("18-29", joven_IgG);

            chartEtarioResult.Series[0].Points.AddXY("30-59", adulto_negativo);
            chartEtarioResult.Series[1].Points.AddXY("30-59", adulto_IgM);
            chartEtarioResult.Series[2].Points.AddXY("30-59", adulto_IgG_IgM);
            chartEtarioResult.Series[3].Points.AddXY("30-59", adulto_IgG);

            chartEtarioResult.Series[0].Points.AddXY("Mayores 60", adulto_mayor_negativo);
            chartEtarioResult.Series[1].Points.AddXY("Mayores 60", adulto_mayor_IgM);
            chartEtarioResult.Series[2].Points.AddXY("Mayores 60", adulto_mayor_IgG_IgM);
            chartEtarioResult.Series[3].Points.AddXY("Mayores 60", adulto_mayor_IgG);

            #endregion

            #region RECUENTO DE RESULTADOS COVID-19
            List<ServiceCovid19> DataSource_2_ = new List<ServiceCovid19>();

            DataSource_2_ = DataSource_1.GroupBy(p => p.RESULTADO).Select(s => s.First()).ToList();


            foreach (var item in DataSource_2_)
            {
                //int cont = 0;
                if (item.RESULTADO == "NEGATIVO")
                {
                    Tipo.Add(item.RESULTADO);
                    int cont = DataSource_1.Count(p => p.RESULTADO == "NEGATIVO");
                    Cantidad.Add(cont);
                }
                else if (item.RESULTADO == "NO VÁLIDO")
                {
                    Tipo.Add(item.RESULTADO);
                    int cont = DataSource_1.Count(p => p.RESULTADO == "NO VÁLIDO");
                    Cantidad.Add(cont);
                }
                else if (item.RESULTADO == "IgM Positivo")
                {
                    Tipo.Add(item.RESULTADO);
                    int cont = DataSource_1.Count(p => p.RESULTADO == "IgM Positivo");
                    Cantidad.Add(cont);
                }
                else if (item.RESULTADO == "IgG Positivo")
                {
                    Tipo.Add(item.RESULTADO);
                    int cont = DataSource_1.Count(p => p.RESULTADO == "IgG Positivo");
                    Cantidad.Add(cont);
                }
                else if (item.RESULTADO == "IgM e IgG Positivo")
                {
                    Tipo.Add(item.RESULTADO);
                    int cont = DataSource_1.Count(p => p.RESULTADO == "IgM e IgG Positivo");
                    Cantidad.Add(cont);
                }
                
            }

            chartBarras.Titles[0].Text = "RESULTADOS DE PRUEBA RÁPIDA COVID-19 DEL " + inicio_.ToShortDateString() + " AL " + _finOriginal.ToShortDateString();
            chartBarras.Series[0].Points.DataBindXY(Tipo, Cantidad);

            #endregion

            #region SEXO
            List<ServiceCovid19> DataSource_5 = new List<ServiceCovid19>();
            DataSource_5 = DataSource_1.GroupBy(p => p.SEXO).Select(s => s.First()).ToList();
            foreach (var item in DataSource_5)
            {
                if (item.SEXO == "MASCULINO")
                {
                    Sexo_Tipe.Add(item.SEXO);
                    int cont = DataSource_1.Count(p => p.SEXO == "MASCULINO");
                    Sexo_Cant.Add(cont);
                }
                else if (item.SEXO == "FEMENINO")
                {
                    Sexo_Tipe.Add(item.SEXO);
                    int cont = DataSource_1.Count(p => p.SEXO == "FEMENINO");
                    Sexo_Cant.Add(cont);
                }
            }

            chartSexos.Series[0].Points.DataBindXY(Sexo_Tipe, Sexo_Cant);
            #endregion

            #region SERVICIO
            List<ServiceCovid19> DataSource_6 = new List<ServiceCovid19>();
            DataSource_6 = DataSource_1.GroupBy(p => p.ATENCION).Select(s => s.First()).ToList();
            foreach (var item in DataSource_6)
            {
                if (item.ATENCION == "PARTICULAR")
                {
                    TipoSERVICIO.Add(item.ATENCION);
                    int cont = DataSource_1.Count(p => p.ATENCION == "PARTICULAR");
                    CantidadSERVICIO.Add(cont);
                }
                else if (item.ATENCION == "OCUPACIONAL")
                {
                    TipoSERVICIO.Add(item.ATENCION);
                    int cont = DataSource_1.Count(p => p.ATENCION == "OCUPACIONAL");
                    CantidadSERVICIO.Add(cont);
                }
                else if (item.ATENCION == "SEGUROS")
                {
                    TipoSERVICIO.Add(item.ATENCION);
                    int cont = DataSource_1.Count(p => p.ATENCION == "SEGUROS");
                    CantidadSERVICIO.Add(cont);
                }
            }
            chartPastel.Titles[0].Text = "SERVICIOS COVID-19 DEL " + inicio_.ToShortDateString() + " AL " + _finOriginal.ToShortDateString();
            chartPastel.Series[0].Points.DataBindXY(TipoSERVICIO, CantidadSERVICIO);
            #endregion



            
            ///


        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnDescargarCurvas_Click(object sender, EventArgs e)
        {
            GuardarGrafico(chartPositivos, nombrePositivos);
            //GUARDAR IMAGEN
            //SaveFileDialog sf = new SaveFileDialog();
            //sf.Filter = "JPG(*.JPG)|*.jpg";
            //if (sf.ShowDialog () == DialogResult.OK)
            //{
            //Imagen a guardar
            //    Image img; 
            //    img.save(sf.Filename);
            //}

            //GUARDAR CHART DIRECTO
            //string ruta = Common.Utils.GetApplicationConfigValue("ImgRxDestino").ToString();
            //string pathImage = ruta + "chartPositivos.png";
            //chartPositivos.SaveImage(pathImage, ChartImageFormat.Png);
            //MessageBox.Show("Imagen " + chartPositivos + ".png guardada correctamente.");
        }


        private void btnRESULTADOSDEPOSITIVOSPORCENTAJE_Click(object sender, EventArgs e)
        {
            GuardarGrafico(chartPositivosPorc, _chartPositivosPorc);
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

        private void chartEtareo_Click(object sender, EventArgs e)
        {
            //GuardarGrafico(chartEtareo, "PACIENTES ATENDIDOS POR GRUPO ETAREO DEL " + inicio_.ToShortDateString() + " AL " + fin_.ToShortDateString());
        }

        private void btnchartEtareo_Click(object sender, EventArgs e)
        {
            GuardarGrafico(chartEtareo, "PACIENTES ATENDIDOS POR GRUPO ETAREO DEL " + inicio_.ToShortDateString() + " AL " + fin_.ToShortDateString());
        }

        private void btnchartEtarioResult_Click(object sender, EventArgs e)
        {
            GuardarGrafico(chartEtarioResult, "RESULTADOS PACIENTES ATENDIDOS POR GRUPO ETAREO DEL " + inicio_.ToShortDateString() + " AL " + fin_.ToShortDateString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GuardarGrafico(chartBarras, "RESULTADOS DE PRUEBA RÁPIDA DE COVID-19 DEL " + inicio_.ToShortDateString() + " AL " + fin_.ToShortDateString());
        }

        private void btnchartSexos_Click(object sender, EventArgs e)
        {
            GuardarGrafico(chartSexos, "PACIENTES ATENDIDOS POR SEXO DEL " + inicio_.ToShortDateString() + " AL " + fin_.ToShortDateString());
        }

        private void btnchartPastel_Click(object sender, EventArgs e)
        {
            GuardarGrafico(chartPastel, "SERVICIOS COVID-19 DEL " + inicio_.ToShortDateString() + " AL " + fin_.ToShortDateString());
        }

        private void btnchartPositivosDiarios_Click(object sender, EventArgs e)
        {
            GuardarGrafico(chartPositivosDiarios, "GRÁFICO LINEAL DE RESULTADOS POSITIVOS COVID-19 DEL " + inicio_.ToShortDateString() + " AL " + fin_.ToShortDateString());
        }
      
    }
}
