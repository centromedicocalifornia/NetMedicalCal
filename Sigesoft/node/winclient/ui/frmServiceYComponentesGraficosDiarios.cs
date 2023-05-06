using Sigesoft.Node.WinClient.BE;
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
    public partial class frmServiceYComponentesGraficosDiarios : Form
    {
        private List<ServiceGridJerarquizadaList> Lista = new List<ServiceGridJerarquizadaList>();

        private List<ServiceGridJerarquizadaList> Data_G = new List<ServiceGridJerarquizadaList>();
        private List<ServiceGridJerarquizadaList> Data_G2 = new List<ServiceGridJerarquizadaList>();

        private List<ServiceGridJerarquizadaList> Data_Diaria = new List<ServiceGridJerarquizadaList>();
        private List<ServiceGridJerarquizadaList> Data_Diaria_Exam = new List<ServiceGridJerarquizadaList>();
        private List<ServiceGridJerarquizadaList> Data_Diaria_Exam_Agrup = new List<ServiceGridJerarquizadaList>();



        ArrayList PorDia1_Tipe = new ArrayList();
        ArrayList PorDia1_Cant = new ArrayList();

        ArrayList PorDia2_Tipe = new ArrayList();
        ArrayList PorDia2_Cant = new ArrayList();

        ArrayList PorDia3_Tipe = new ArrayList();
        ArrayList PorDia3_Cant = new ArrayList();

        ArrayList PorDia4_Tipe = new ArrayList();
        ArrayList PorDia4_Cant = new ArrayList();

        ArrayList PorDia5_Tipe = new ArrayList();
        ArrayList PorDia5_Cant = new ArrayList();

        ArrayList PorDia6_Tipe = new ArrayList();
        ArrayList PorDia6_Cant = new ArrayList();

        ArrayList PorDia7_Tipe = new ArrayList();
        ArrayList PorDia7_Cant = new ArrayList();

        ArrayList PorDia8_Tipe = new ArrayList();
        ArrayList PorDia8_Cant = new ArrayList();

        ArrayList PorDia9_Tipe = new ArrayList();
        ArrayList PorDia9_Cant = new ArrayList();

        ArrayList PorDia10_Tipe = new ArrayList();
        ArrayList PorDia10_Cant = new ArrayList();

        ArrayList PorDia11_Tipe = new ArrayList();
        ArrayList PorDia11_Cant = new ArrayList();

        ArrayList PorDia12_Tipe = new ArrayList();
        ArrayList PorDia12_Cant = new ArrayList();

        ArrayList PorDia13_Tipe = new ArrayList();
        ArrayList PorDia13_Cant = new ArrayList();

        ArrayList PorDia14_Tipe = new ArrayList();
        ArrayList PorDia14_Cant = new ArrayList();

        ArrayList PorDia15_Tipe = new ArrayList();
        ArrayList PorDia15_Cant = new ArrayList();

        ArrayList PorDia16_Tipe = new ArrayList();
        ArrayList PorDia16_Cant = new ArrayList();

        ArrayList PorDia17_Tipe = new ArrayList();
        ArrayList PorDia17_Cant = new ArrayList();

        ArrayList PorDia18_Tipe = new ArrayList();
        ArrayList PorDia18_Cant = new ArrayList();

        ArrayList PorDia19_Tipe = new ArrayList();
        ArrayList PorDia19_Cant = new ArrayList();

        ArrayList PorDia20_Tipe = new ArrayList();
        ArrayList PorDia20_Cant = new ArrayList();

        ArrayList PorDia21_Tipe = new ArrayList();
        ArrayList PorDia21_Cant = new ArrayList();

        ArrayList PorDia22_Tipe = new ArrayList();
        ArrayList PorDia22_Cant = new ArrayList();

        ArrayList PorDia23_Tipe = new ArrayList();
        ArrayList PorDia23_Cant = new ArrayList();

        ArrayList PorDia24_Tipe = new ArrayList();
        ArrayList PorDia24_Cant = new ArrayList();

        ArrayList PorDia25_Tipe = new ArrayList();
        ArrayList PorDia25_Cant = new ArrayList();

        ArrayList PorDia26_Tipe = new ArrayList();
        ArrayList PorDia26_Cant = new ArrayList();

        ArrayList PorDia27_Tipe = new ArrayList();
        ArrayList PorDia27_Cant = new ArrayList();

        ArrayList PorDia28_Tipe = new ArrayList();
        ArrayList PorDia28_Cant = new ArrayList();

        ArrayList PorDia29_Tipe = new ArrayList();
        ArrayList PorDia29_Cant = new ArrayList();

        ArrayList PorDia30_Tipe = new ArrayList();
        ArrayList PorDia30_Cant = new ArrayList();

        ArrayList PorDia31_Tipe = new ArrayList();
        ArrayList PorDia31_Cant = new ArrayList();

        ArrayList PorGlobal_Tipe = new ArrayList();
        ArrayList PorGlobal_Cant = new ArrayList();

        private string Fi;
        private string Ff;
        public frmServiceYComponentesGraficosDiarios(List<ServiceGridJerarquizadaList> _Lista, string _Fi, string _Ff)
        {
            Fi = _Fi;
            Ff = _Ff;
            Lista = _Lista;
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmServiceYComponentesGraficosDiarios_Load(object sender, EventArgs e)
        {
            Data_G = Lista;

            Data_G = Data_G.GroupBy(p => p.Fecha.Split(' ')[0]).Select(s => s.First()).ToList();

            foreach (var item in Data_G)
            {
                Data_Diaria = Lista.FindAll(p => p.Fecha.Split(' ')[0] == item.Fecha.Split(' ')[0]).ToList();

                Data_Diaria_Exam = Data_Diaria.GroupBy(p => p.Value2).Select(s => s.First()).ToList();

                chartGraf1.Titles[0].Text = "Examenes del 01";
                chartGraf2.Titles[0].Text = "Examenes del 02";
                chartGraf3.Titles[0].Text = "Examenes del 03";
                chartGraf4.Titles[0].Text = "Examenes del 04";
                chartGraf5.Titles[0].Text = "Examenes del 05";
                chartGraf6.Titles[0].Text = "Examenes del 06";
                chartGraf7.Titles[0].Text = "Examenes del 07";
                chartGraf8.Titles[0].Text = "Examenes del 08";
                chartGraf9.Titles[0].Text = "Examenes del 09";
                chartGraf10.Titles[0].Text = "Examenes del 10";
                chartGraf11.Titles[0].Text = "Examenes del 11";
                chartGraf12.Titles[0].Text = "Examenes del 12";
                chartGraf13.Titles[0].Text = "Examenes del 13";
                chartGraf14.Titles[0].Text = "Examenes del 14";
                chartGraf15.Titles[0].Text = "Examenes del 15";
                chartGraf16.Titles[0].Text = "Examenes del 16";
                chartGraf17.Titles[0].Text = "Examenes del 17";
                chartGraf18.Titles[0].Text = "Examenes del 18";
                chartGraf19.Titles[0].Text = "Examenes del 19";
                chartGraf20.Titles[0].Text = "Examenes del 20";
                chartGraf21.Titles[0].Text = "Examenes del 21";
                chartGraf22.Titles[0].Text = "Examenes del 22";
                chartGraf23.Titles[0].Text = "Examenes del 23";
                chartGraf24.Titles[0].Text = "Examenes del 24";
                chartGraf25.Titles[0].Text = "Examenes del 25";
                chartGraf26.Titles[0].Text = "Examenes del 26";
                chartGraf27.Titles[0].Text = "Examenes del 27";
                chartGraf28.Titles[0].Text = "Examenes del 28";
                chartGraf29.Titles[0].Text = "Examenes del 29";
                chartGraf30.Titles[0].Text = "Examenes del 30";
                chartGraf31.Titles[0].Text = "Examenes del 31";


                chartGraf1.Titles[0].Text = "Examenes del 01";
                if (item.Fecha.Split('/', ' ')[0] == "1")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia1_Tipe.Add(item3.Tipo);
                        PorDia1_Cant.Add(item3.Cantidad);
                    }

                    chartGraf1.Series[0].Points.DataBindXY(PorDia1_Tipe, PorDia1_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "2")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia2_Tipe.Add(item3.Tipo);
                        PorDia2_Cant.Add(item3.Cantidad);
                    }

                    chartGraf2.Series[0].Points.DataBindXY(PorDia2_Tipe, PorDia2_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "3")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia3_Tipe.Add(item3.Tipo);
                        PorDia3_Cant.Add(item3.Cantidad);
                    }

                    chartGraf3.Series[0].Points.DataBindXY(PorDia3_Tipe, PorDia3_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "4")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia4_Tipe.Add(item3.Tipo);
                        PorDia4_Cant.Add(item3.Cantidad);
                    }

                    chartGraf4.Series[0].Points.DataBindXY(PorDia4_Tipe, PorDia4_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "5")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia5_Tipe.Add(item3.Tipo);
                        PorDia5_Cant.Add(item3.Cantidad);
                    }

                    chartGraf5.Series[0].Points.DataBindXY(PorDia5_Tipe, PorDia5_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "6")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia6_Tipe.Add(item3.Tipo);
                        PorDia6_Cant.Add(item3.Cantidad);
                    }

                    chartGraf6.Series[0].Points.DataBindXY(PorDia6_Tipe, PorDia6_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "7")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia7_Tipe.Add(item3.Tipo);
                        PorDia7_Cant.Add(item3.Cantidad);
                    }

                    chartGraf7.Series[0].Points.DataBindXY(PorDia7_Tipe, PorDia7_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "8")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia8_Tipe.Add(item3.Tipo);
                        PorDia8_Cant.Add(item3.Cantidad);
                    }

                    chartGraf8.Series[0].Points.DataBindXY(PorDia8_Tipe, PorDia8_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "9")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia9_Tipe.Add(item3.Tipo);
                        PorDia9_Cant.Add(item3.Cantidad);
                    }

                    chartGraf9.Series[0].Points.DataBindXY(PorDia9_Tipe, PorDia9_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "10")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia10_Tipe.Add(item3.Tipo);
                        PorDia10_Cant.Add(item3.Cantidad);
                    }

                    chartGraf10.Series[0].Points.DataBindXY(PorDia10_Tipe, PorDia10_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "11")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia11_Tipe.Add(item3.Tipo);
                        PorDia11_Cant.Add(item3.Cantidad);
                    }

                    chartGraf11.Series[0].Points.DataBindXY(PorDia11_Tipe, PorDia11_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "12")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia12_Tipe.Add(item3.Tipo);
                        PorDia12_Cant.Add(item3.Cantidad);
                    }

                    chartGraf12.Series[0].Points.DataBindXY(PorDia12_Tipe, PorDia12_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "13")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia13_Tipe.Add(item3.Tipo);
                        PorDia13_Cant.Add(item3.Cantidad);
                    }

                    chartGraf13.Series[0].Points.DataBindXY(PorDia13_Tipe, PorDia13_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "14")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia14_Tipe.Add(item3.Tipo);
                        PorDia14_Cant.Add(item3.Cantidad);
                    }

                    chartGraf14.Series[0].Points.DataBindXY(PorDia14_Tipe, PorDia14_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "15")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia15_Tipe.Add(item3.Tipo);
                        PorDia15_Cant.Add(item3.Cantidad);
                    }

                    chartGraf15.Series[0].Points.DataBindXY(PorDia15_Tipe, PorDia15_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "16")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia16_Tipe.Add(item3.Tipo);
                        PorDia16_Cant.Add(item3.Cantidad);
                    }

                    chartGraf16.Series[0].Points.DataBindXY(PorDia16_Tipe, PorDia16_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "17")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia17_Tipe.Add(item3.Tipo);
                        PorDia17_Cant.Add(item3.Cantidad);
                    }

                    chartGraf17.Series[0].Points.DataBindXY(PorDia17_Tipe, PorDia17_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "18")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia18_Tipe.Add(item3.Tipo);
                        PorDia18_Cant.Add(item3.Cantidad);
                    }

                    chartGraf18.Series[0].Points.DataBindXY(PorDia18_Tipe, PorDia18_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "19")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia19_Tipe.Add(item3.Tipo);
                        PorDia19_Cant.Add(item3.Cantidad);
                    }

                    chartGraf19.Series[0].Points.DataBindXY(PorDia19_Tipe, PorDia19_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "20")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia20_Tipe.Add(item3.Tipo);
                        PorDia20_Cant.Add(item3.Cantidad);
                    }

                    chartGraf20.Series[0].Points.DataBindXY(PorDia20_Tipe, PorDia20_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "21")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia21_Tipe.Add(item3.Tipo);
                        PorDia21_Cant.Add(item3.Cantidad);
                    }

                    chartGraf21.Series[0].Points.DataBindXY(PorDia21_Tipe, PorDia21_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "22")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia22_Tipe.Add(item3.Tipo);
                        PorDia22_Cant.Add(item3.Cantidad);
                    }

                    chartGraf22.Series[0].Points.DataBindXY(PorDia22_Tipe, PorDia22_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "23")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia23_Tipe.Add(item3.Tipo);
                        PorDia23_Cant.Add(item3.Cantidad);
                    }

                    chartGraf23.Series[0].Points.DataBindXY(PorDia23_Tipe, PorDia23_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "24")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia24_Tipe.Add(item3.Tipo);
                        PorDia24_Cant.Add(item3.Cantidad);
                    }

                    chartGraf24.Series[0].Points.DataBindXY(PorDia24_Tipe, PorDia24_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "25")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia25_Tipe.Add(item3.Tipo);
                        PorDia25_Cant.Add(item3.Cantidad);
                    }

                    chartGraf25.Series[0].Points.DataBindXY(PorDia25_Tipe, PorDia25_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "26")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia26_Tipe.Add(item3.Tipo);
                        PorDia26_Cant.Add(item3.Cantidad);
                    }

                    chartGraf26.Series[0].Points.DataBindXY(PorDia26_Tipe, PorDia26_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "27")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia27_Tipe.Add(item3.Tipo);
                        PorDia27_Cant.Add(item3.Cantidad);
                    }

                    chartGraf27.Series[0].Points.DataBindXY(PorDia27_Tipe, PorDia27_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "28")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia28_Tipe.Add(item3.Tipo);
                        PorDia28_Cant.Add(item3.Cantidad);
                    }

                    chartGraf28.Series[0].Points.DataBindXY(PorDia28_Tipe, PorDia28_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "29")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia29_Tipe.Add(item3.Tipo);
                        PorDia29_Cant.Add(item3.Cantidad);
                    }

                    chartGraf29.Series[0].Points.DataBindXY(PorDia29_Tipe, PorDia29_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "30")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia30_Tipe.Add(item3.Tipo);
                        PorDia30_Cant.Add(item3.Cantidad);
                    }

                    chartGraf30.Series[0].Points.DataBindXY(PorDia30_Tipe, PorDia30_Cant);
                }
                else if (item.Fecha.Split('/', ' ')[0] == "31")
                {
                    List<ListTemp> list = new List<ListTemp>();
                    foreach (var item2 in Data_Diaria_Exam)
                    {
                        ListTemp listObj = new ListTemp();

                        Data_Diaria_Exam_Agrup = Data_Diaria.FindAll(p => p.Value2 == item2.Value2).ToList();
                        listObj.Tipo = item2.Value2;
                        listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                        list.Add(listObj);
                    }

                    list = list.OrderBy(p => p.Cantidad).ToList();

                    foreach (var item3 in list)
                    {
                        PorDia31_Tipe.Add(item3.Tipo);
                        PorDia31_Cant.Add(item3.Cantidad);
                    }

                    chartGraf31.Series[0].Points.DataBindXY(PorDia31_Tipe, PorDia31_Cant);
                }

            }


            Data_G2 = Lista;

            Data_G2 = Data_G2.GroupBy(p => p.Value2).Select(s => s.First()).ToList();

            List<ListTemp> listG = new List<ListTemp>();

            foreach (var item in Data_G2)
            {

                chartGrafGlobal.Titles[0].Text = "Examenes del " + Fi + " al " + Ff;

                ListTemp listObj = new ListTemp();

                Data_Diaria_Exam_Agrup = Lista.FindAll(p => p.Value2 == item.Value2).ToList();

                listObj.Tipo = item.Value2;
                listObj.Cantidad = Data_Diaria_Exam_Agrup.Count();
                listG.Add(listObj);
            }

            listG = listG.OrderBy(p => p.Cantidad).ToList();

            foreach (var item3 in listG)
            {
                PorGlobal_Tipe.Add(item3.Tipo);
                PorGlobal_Cant.Add(item3.Cantidad);
            }

            chartGrafGlobal.Series[0].Points.DataBindXY(PorGlobal_Tipe, PorGlobal_Cant);
        }

        private void chartGraf1_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia1_Tipe, PorDia1_Cant, chartGraf1.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf2_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia2_Tipe, PorDia2_Cant, chartGraf2.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf3_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia3_Tipe, PorDia3_Cant, chartGraf3.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf4_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia4_Tipe, PorDia4_Cant, chartGraf4.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf5_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia5_Tipe, PorDia5_Cant, chartGraf5.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf6_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia6_Tipe, PorDia6_Cant, chartGraf6.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf7_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia7_Tipe, PorDia7_Cant, chartGraf7.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf8_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia8_Tipe, PorDia8_Cant, chartGraf8.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf9_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia9_Tipe, PorDia9_Cant, chartGraf9.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf10_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia10_Tipe, PorDia10_Cant, chartGraf10.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf11_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia11_Tipe, PorDia11_Cant, chartGraf11.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf12_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia12_Tipe, PorDia12_Cant, chartGraf12.Titles[0].Text);
            ticket.ShowDialog();
        }
        
        private void chartGraf13_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia13_Tipe, PorDia13_Cant, chartGraf13.Titles[0].Text);
            ticket.ShowDialog();
        } 
        
         private void chartGraf14_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia14_Tipe, PorDia14_Cant, chartGraf14.Titles[0].Text);
            ticket.ShowDialog();
        }
        
        private void chartGraf15_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia15_Tipe, PorDia15_Cant, chartGraf15.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf16_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia16_Tipe, PorDia16_Cant, chartGraf16.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf17_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia17_Tipe, PorDia17_Cant, chartGraf17.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf18_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia18_Tipe, PorDia18_Cant, chartGraf18.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf19_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia19_Tipe, PorDia19_Cant, chartGraf19.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf20_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia20_Tipe, PorDia20_Cant, chartGraf20.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf21_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia21_Tipe, PorDia21_Cant, chartGraf21.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf22_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia22_Tipe, PorDia22_Cant, chartGraf22.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf23_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia23_Tipe, PorDia23_Cant, chartGraf23.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf24_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia24_Tipe, PorDia24_Cant, chartGraf24.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf25_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia25_Tipe, PorDia25_Cant, chartGraf25.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf26_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia26_Tipe, PorDia26_Cant, chartGraf26.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf27_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia27_Tipe, PorDia27_Cant, chartGraf27.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf28_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia28_Tipe, PorDia28_Cant, chartGraf28.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf29_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia29_Tipe, PorDia29_Cant, chartGraf29.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf30_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia30_Tipe, PorDia30_Cant, chartGraf30.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGraf31_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorDia31_Tipe, PorDia31_Cant, chartGraf31.Titles[0].Text);
            ticket.ShowDialog();
        }

        private void chartGrafGlobal_Click(object sender, EventArgs e)
        {
            frmServiceYComponentesGraficosDiariosInd ticket = new frmServiceYComponentesGraficosDiariosInd(PorGlobal_Tipe, PorGlobal_Cant, chartGrafGlobal.Titles[0].Text);
            ticket.ShowDialog();
        }


        

       
    }
}
