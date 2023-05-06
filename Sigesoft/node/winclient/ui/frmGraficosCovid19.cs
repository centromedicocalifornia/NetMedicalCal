using Sigesoft.Node.WinClient.BE.Custom;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmGraficosCovid19 : Form
    {
        DateTime inicio_;
        DateTime fin_;

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
        List<ServiceCovid19> DataSource_1 = new List<ServiceCovid19>();

        public frmGraficosCovid19(DateTime inicio, DateTime fin, List<ServiceCovid19> _DataSource_1)
        {
            inicio_ = inicio;
            fin_ = fin;
            DataSource_1 = _DataSource_1;
            InitializeComponent();
        }

        private void frmGraficosCovid19_Load(object sender, EventArgs e)
        {
            BindingGrid();
        }


        private void BindingGrid()
        {
            
            List<ServiceCovid19> DataSource_2_ = new List<ServiceCovid19>();
            

            //DataSource_1 = new ServiceBL().GetServicesCovid19(inicio_, fin_);
            //var result = query.GroupBy(g => g.v_ServiceId).Select(s => s.First()).ToList();
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

            DateTime _finOriginal = fin_.Date.AddDays(-1);
            chartBarras.Titles[0].Text = "RESULTADOS DE PRUEBA RÁPIDA COVID-19 DEL " + inicio_.ToShortDateString() + " AL " + _finOriginal.ToShortDateString();
            

            chartBarras.Series[0].Points.DataBindXY(Tipo, Cantidad);
            //chartPastel.Series[0].Points.DataBindXY(Tipo, Cantidad);
            ///TIPO SERVICIO
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
            ///



            ///SEXO
            List<ServiceCovid19> DataSource_3 = new List<ServiceCovid19>();
            DataSource_3 = DataSource_1.GroupBy(p => p.SEXO).Select(s => s.First()).ToList();
            foreach (var item in DataSource_3)
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
            ///

            ///GRUPO ETAREO
           
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
            Etareo_Tipe.Add("0 - 11");
            Etareo_Tipe.Add("12 - 17");
            Etareo_Tipe.Add("18 - 29");
            Etareo_Tipe.Add("30 - 59");
            Etareo_Tipe.Add("> 60");
           
            Etareo_Cant.Add(niño_c);
            Etareo_Cant.Add(adolescente_c);
            Etareo_Cant.Add(joven_c);
            Etareo_Cant.Add(adulto_c);
            Etareo_Cant.Add(adulto_mayor_c);

            chartEtareo.Series[0].Points.DataBindXY(Etareo_Tipe, Etareo_Cant);

            chartEtarioResult.Series[0].Points.AddXY("0 - 11", niño_negativo);
            chartEtarioResult.Series[1].Points.AddXY("0 - 11", niño_IgM);
            chartEtarioResult.Series[2].Points.AddXY("0 - 11", niño_IgG_IgM);
            chartEtarioResult.Series[3].Points.AddXY("0 - 11", niño_IgG);

            chartEtarioResult.Series[0].Points.AddXY("12 - 17", adolescente_negativo);
            chartEtarioResult.Series[1].Points.AddXY("12 - 17", adolescente_IgM);
            chartEtarioResult.Series[2].Points.AddXY("12 - 17", adolescente_IgG_IgM);
            chartEtarioResult.Series[3].Points.AddXY("12 - 17", adolescente_IgG);

            chartEtarioResult.Series[0].Points.AddXY("18 - 29", joven_negativo);
            chartEtarioResult.Series[1].Points.AddXY("18 - 29", joven_IgM);
            chartEtarioResult.Series[2].Points.AddXY("18 - 29", joven_IgG_IgM);
            chartEtarioResult.Series[3].Points.AddXY("18 - 29", joven_IgG);

            chartEtarioResult.Series[0].Points.AddXY("30 - 59", adulto_negativo);
            chartEtarioResult.Series[1].Points.AddXY("30 - 59", adulto_IgM);
            chartEtarioResult.Series[2].Points.AddXY("30 - 59", adulto_IgG_IgM);
            chartEtarioResult.Series[3].Points.AddXY("30 - 59", adulto_IgG);

            chartEtarioResult.Series[0].Points.AddXY("> 60", adulto_mayor_negativo);
            chartEtarioResult.Series[1].Points.AddXY("> 60", adulto_mayor_IgM);
            chartEtarioResult.Series[2].Points.AddXY("> 60", adulto_mayor_IgG_IgM);
            chartEtarioResult.Series[3].Points.AddXY("> 60", adulto_mayor_IgG);
            //int niño_IgG = 0;
            //int niño_IgM = 0;
            //int niño_IgG_IgM = 0;
            //int niño_negativo = 0;
            ///d
        
        }

        
        private void GetResultado()
        { 
        
        }
    }
}
