using iTextSharp.text;
using iTextSharp.text.pdf;
using NetPdf;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BE.Custom;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Sigesoft.Common
{
    public class LiquidacionSegurosDetalle
    {
        public static void CreateLiquidacionSeguros(List<ServiciosDetalle> servicios, personDto dataPacient, organizationDto dataOrganization, organizationDto dataAseguradora, List<HospitalizacionCustom> dataHospitalizacion, DatosSeguuro dataSeguro, List<MeidicinasTicketsLista> tickets, List<RecetasDetalle> receta, string pathFile, decimal CostoConsultaDeducible)
        {

            //iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 30f, 30f, 15f, 41f);
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 30f, 30f, 75f, 65f);

            document.SetPageSize(iTextSharp.text.PageSize.A4);

            //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pathFile, FileMode.Create));
            //pdfPage page = new pdfPage();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pathFile, FileMode.Create));
            pdfPage_NEW page = new pdfPage_NEW();
            page.Title = string.Empty;
            writer.PageEvent = page;
            document.Open();

            #region Declaration Tables
            var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.Gray);
            string include = string.Empty;
            List<PdfPCell> cells = null;
            float[] columnWidths = null;
            string[] columnValues = null;
            string[] columnHeaders = null;
            PdfPTable header2 = new PdfPTable(6);
            header2.HorizontalAlignment = Element.ALIGN_CENTER;
            header2.WidthPercentage = 100;
            float[] widths1 = new float[] { 16.6f, 18.6f, 16.6f, 16.6f, 16.6f, 16.6f };
            header2.SetWidths(widths1);
            PdfPTable companyData = new PdfPTable(6);
            companyData.HorizontalAlignment = Element.ALIGN_CENTER;
            companyData.WidthPercentage = 100;
            float[] widthscolumnsCompanyData = new float[] { 16.6f, 16.6f, 16.6f, 16.6f, 16.6f, 16.6f };
            companyData.SetWidths(widthscolumnsCompanyData);
            PdfPTable filiationWorker = new PdfPTable(4);
            PdfPTable table = null;
            PdfPCell cell = null;
            document.Add(new Paragraph("\r\n"));
            #endregion

            #region Fonts
            Font fontTitle1 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Blue));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontTitleTableAntecedentesOcupacionales = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueAntecedentesOcupacionales = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            #endregion






            #region Cabezera
            #region variables
            //paciente
            string paciente = "- - -", titular = "- - -", DocNumber = "- - -";
            if (dataPacient != null)
            {
                paciente = ": " + dataPacient.v_FirstLastName + " " + dataPacient.v_SecondLastName + ", " +
                           dataPacient.v_FirstName;

                titular = string.IsNullOrEmpty(dataPacient.v_OwnerName) ? ": - - -" : ": " + dataPacient.v_OwnerName.Split('|')[0];
                DocNumber = string.IsNullOrEmpty(dataPacient.v_DocNumber) ? ": - - -" : ": CSL-" + dataPacient.v_DocNumber;
            }

            //empresa
            string orgName = "- - -", orgPhone = "- - -", orgAdress = "- - -";
            if (dataOrganization != null)
            {
                orgName = string.IsNullOrEmpty(dataOrganization.v_Name) ? ": - - -" : ": " + dataOrganization.v_Name;
                orgAdress = string.IsNullOrEmpty(dataOrganization.v_Address) ? ": - - -" : ": " + dataOrganization.v_Address;
                orgPhone = string.IsNullOrEmpty(dataOrganization.v_PhoneNumber) ? ": - - -" : ": " + dataOrganization.v_PhoneNumber;
            }

            //aseguradora
            string asgName = "- - -", asgRuc = "- - -";
            if (dataAseguradora != null)
            {
                asgRuc = string.IsNullOrEmpty(dataAseguradora.v_IdentificationNumber) ? ": - - -" : ": " + dataAseguradora.v_IdentificationNumber;
                asgName = string.IsNullOrEmpty(dataAseguradora.v_Name) ? ": - - -" : ": " + dataAseguradora.v_Name;
            }

            #region seguro

            string Servicio = "- - -", F_Servicio = "- - -", Plan = "- - -", Protocolo = "- - -", Solicitud = "- - -", Medico = "- - -";
            decimal Factor = 0, Descuento_PPS = 0, Deducible = 0, Coaseguro = 0, Habitacion = 0;
            if (dataSeguro != null)
            {
                Servicio = dataSeguro.Servicio;
                Solicitud = dataSeguro.Solicitud;
                F_Servicio = dataSeguro.FechaServicio.ToString().Split(' ')[0];
                Plan = dataSeguro.Plan;
                Factor = dataSeguro.Factor;
                Descuento_PPS = dataSeguro.DescuentoPPS;
                Deducible = dataSeguro.Deducible;
                Coaseguro = dataSeguro.Coaseguro;
                Habitacion = dataSeguro.Habitacion;
                Protocolo = dataSeguro.Protocolo;
                Medico = dataSeguro.Medico;
            }
            #endregion
            //hospitalizacion
            int i_dias = 1;
            int nroHabit = 1;
            string FechIngreso = ": " + F_Servicio, FechAlta = "", Habit = "", Dias = "", Habitaciones = ": 1", costoHabit = "---", texto_Cuarto = "", textoDiasHab = "", texto_Alta = "";
            decimal constoUnitaro = 0;
            //int dias = 0;
            #endregion
            #region Conexion SAM
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            #endregion
            var cadena1 = "select SR.v_NroLiquidacion, LQ.v_LiquidacionId from service SR inner join liquidacion LQ on SR.v_NroLiquidacion = LQ.v_NroLiquidacion where SR.v_ServiceId='" + Servicio + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            string nroliq = "";
            string codigointerno = "";
            while (lector.Read())
            {
                nroliq = lector.GetValue(0).ToString();
                codigointerno = lector.GetValue(1).ToString();
            }
            lector.Close();
            if (dataHospitalizacion != null)
            {
                #region


                #endregion
                if (dataHospitalizacion.Count > 0)
                {
                    List<DateTime> DIngreso = new List<DateTime>();
                    List<DateTime> DAlta = new List<DateTime>();
                    foreach (var data in dataHospitalizacion)
                    {
                        DIngreso.Add(data.d_FechaIngreso);
                        DAlta.Add(data.d_FechaAlta);
                    }

                    List<DateTime> ListDIngreso = DIngreso.OrderBy(x => x).ToList();
                    List<DateTime> ListDAlta = DAlta.OrderByDescending(x => x).ToList();
                    texto_Cuarto = "CUARTO";
                    Habit = dataHospitalizacion[0].v_Habitacion == null ? ": ---" : ": " + dataHospitalizacion[0].v_Habitacion;
                    List<string> habitaciones = new List<string>();
                    double costo = 0.00;
                    double costoUnit = 0.00;
                    if (ListDIngreso.Count() > 0 && ListDAlta.Count() > 0)
                    {
                        FechIngreso = ListDIngreso.Count() == 0 ? ": ---" : ": " + ListDIngreso[0].ToShortDateString();
                        FechAlta = ListDAlta.Count() == 0 ? " " : ": " + ListDAlta[0].ToShortDateString();
                        texto_Alta = "FECHA DE ALTA";
                        TimeSpan ts = ListDAlta[0] - ListDIngreso[0];
                        // Difference in days.
                        int differenceInDays = ts.Days;
                        if (differenceInDays <= 1)
                        {
                            differenceInDays = 1;
                            i_dias = 1;
                        }

                        i_dias = differenceInDays;
                        textoDiasHab = "DIAS HAB.";
                        Dias = ": " + differenceInDays.ToString();
                    }

                    foreach (var dat in dataHospitalizacion)
                    {
                        costo += double.Parse(dat.d_Precio.ToString()) * i_dias;
                        constoUnitaro = decimal.Parse(dat.d_Precio.ToString());
                        var find = habitaciones.Find(x => x == dat.v_Habitacion);
                        if (find == null)
                        {
                            habitaciones.Add(dat.v_Habitacion);
                        }
                    }
                    costo = Math.Round(costo, 2);
                    costoHabit = (costo).ToString();
                    Habitaciones = habitaciones.Count.ToString();

                    // Difference in days, hours, and minutes.

                }

            }

            var cellsTit = new List<PdfPCell>()
            { 
                
                new PdfPCell(new Phrase("LIQUIDACIÓN " + nroliq + " [DETALLADA] ", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("Cajamarca, " + DateTime.Now.ToShortDateString() + " \n" + DateTime.Now.ToShortTimeString(), fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, BorderColor = BaseColor.WHITE},
            };
            columnWidths = new float[] { 60f, 40f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);

            #region CABECERA
            cells = new List<PdfPCell>()
            {                
                new PdfPCell(new Phrase("SEÑOR(ES)", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(asgName, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("H.C", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(DocNumber, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("RUC", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(asgRuc, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(texto_Cuarto, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(Habit, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("PACIENTE", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(paciente, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(textoDiasHab, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(Dias.ToString(), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("TITULAR", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(titular, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("SOLC. / CART. GAR.", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": " + Solicitud, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase("CENTRO DE TRABAJO", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(orgName, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("TELEFONO", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(orgPhone, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase("DIRECCIÓN", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(orgAdress, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("PLAN", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": " + Plan, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase("FECHA DE INGRESO", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(FechIngreso, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("FACTOR: ", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": " + Factor.ToString() , fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("DESCUENTO PPS: ", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": " + Descuento_PPS + "%", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase(texto_Alta, fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(FechAlta, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("DEDUCIBLE: ", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": S/. " + Deducible, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("COASEGURO", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": " + Coaseguro.ToString() + "%", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("MEDICO A CARGO", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": " + Medico, fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("CÓDIGO INTERNO", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": " + Servicio, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("\n", fontColumnValue)) {Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE, BorderWidthBottom = 0},


            };
            columnWidths = new float[] { 12f, 8f, 16f, 18f, 12f, 17f, 17f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #endregion

            #region Cuerpo

            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("NOMBRE DEL SERVICIO", fontTitle2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, BorderColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("CANTIDAD", fontTitle2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, BorderColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("P. UNITARIO", fontTitle2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, BorderColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("TOTAL", fontTitle2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, BorderColor = BaseColor.BLACK},

            };

            columnWidths = new float[] { 55f, 15f, 15f, 15f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            int contador = 0;



            decimal totalConsumoGeneral = 0;
            decimal TotalDeducibleGlob = 0;
            decimal totalDesctGeneral = 0;
                    bool InsertFarmOperaciones = false;
                    bool InsertFarmReceta = false;
                    bool InsertDays = false;
                    bool EsHospitalizacion = false;
                    List<string> ComponentesAgregados_ = new List<string>();
                    List<string> ComponentesAgregados = new List<string>();
                    int contKindOfService = 1;
                    decimal totalImporte = 0;
                    decimal totalDescuento = 0;
                    decimal totalConDescuento = 0;
                    decimal totalDeducible = 0;
                    decimal totalConsumos = 0;

                    cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f, BorderColor = BaseColor.WHITE, BorderWidthTop = 0},
                            new PdfPCell(new Phrase("-----------------------------------------------------------------------------------------------------------------------", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, BorderColor = BaseColor.WHITE, BorderWidthTop = 0},
                        };

                    columnWidths = new float[] { 40f, 60f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);
                    #region SERVICIOS DINÁMICO
                    decimal TotalServicios = 0;
                    decimal precioConsulta = 0;
                    cells = new List<PdfPCell>();
                    int contadorMedicina = 0;
                    if (servicios != null)
                    {
                        decimal precio = 0;
                        foreach (var item in servicios)
                        {
                            decimal servicioTotal = 0;
                            int contador_ = 1;
                            cell = new PdfPCell(new Phrase(contKindOfService.ToString() + ") " + item.Tipo, fontColumnValueBold2)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                            cells.Add(cell);
                            
                            if (item.Lista1.Count >= 1 && item.Lista2 == null)
                            {
                                int conteo = 1;
                                foreach (var item1 in item.Lista1)
                                {
                                    precio = decimal.Round(decimal.Parse(item1.Precio.ToString()) / decimal.Parse("1.18"), 2);
                                    if (contador_ == 1)
                                    {
                                        precioConsulta = decimal.Parse(precio.ToString());
                                    }
                                    decimal cant = decimal.Round(item1.Cantidad);
                                    decimal pU = decimal.Round((precio / cant), 2);

                                    cell = new PdfPCell(new Phrase("     " + conteo.ToString() + ". [" + item1.Segus + "] - " + item1.NombreComponente, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(cant.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(pU.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(precio.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    conteo++;
                                    totalImporte += decimal.Round(decimal.Parse(precio.ToString()),2);
                                    servicioTotal += decimal.Round(decimal.Parse(precio.ToString()),2);
                                    contador_++;
                                    if (item1.Grupo == "MEDICINA C")
                                    {
                                        contadorMedicina++;
                                    }
                                }
                            }
                            else if ((item.Lista2 != null && item.Lista2.Count >= 1) && item.Lista1.Count == 0)
                            {
                                foreach (var item1 in item.Lista2)
                                {
                                    int conteo = 1;

                                    cell = new PdfPCell(new Phrase("     " + item1.Grupo, fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);

                                    foreach (var item2 in item1.Lista2)
                                    {
                                        precio = decimal.Round(decimal.Parse(item2.Precio.ToString()) / decimal.Parse("1.18"), 2);
                                       
                                        decimal cant = decimal.Round(item2.Cantidad);
                                        decimal pU = decimal.Round((precio / item2.Cantidad), 2);

                                        cell = new PdfPCell(new Phrase("          " + conteo.ToString() + ". [" + item2.Segus + "] - " + item2.NombreComponente, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                        cells.Add(cell);
                                        cell = new PdfPCell(new Phrase(cant.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                        cells.Add(cell);
                                        cell = new PdfPCell(new Phrase(pU.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                        cells.Add(cell);
                                        cell = new PdfPCell(new Phrase(precio.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                        cells.Add(cell);
                                        conteo++;
                                        totalImporte += decimal.Round(decimal.Parse(precio.ToString()), 2);
                                        servicioTotal += decimal.Round(decimal.Parse(precio.ToString()), 2);
                                    }
                                    
                                }
                            }
                            cell = new PdfPCell(new Phrase("TOTAL : " + item.Tipo, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                            cells.Add(cell);
                            cell = new PdfPCell(new Phrase(servicioTotal.ToString("N2"), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK };
                            cells.Add(cell);
                            TotalServicios += servicioTotal;
                            contKindOfService++;

                        }
                        columnWidths = new float[] { 55f, 15f, 15f, 15f };

                        filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                        document.Add(filiationWorker);
                    }
                    #endregion

                    #region Tickets
                    cells = new List<PdfPCell>();
                    decimal totalTickets = 0;
                    if (tickets != null && tickets.Count >= 1)
                    {
                        cell = new PdfPCell(new Phrase(contKindOfService.ToString() + ")  FARMACIA HOSPITALARIA", fontColumnValueBold2)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                        cells.Add(cell);
                        decimal precioUnitario = 0;
                        decimal precioVenta = 0;
                        
                        foreach (var item in tickets)
                        {
                            decimal TickrtTotal = 0;

                            cell = new PdfPCell(new Phrase(item.Tipo, fontColumnValueBold2)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                            cells.Add(cell);
                            int conteo = 1;
                            decimal sumatickets = 0;
                            foreach (var item1 in item.Lista_1)
                            {                                
                                cell = new PdfPCell(new Phrase("     " + item1.Ticket + " " + item1.Fecha.ToString().Split(' ')[0], fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);
                                decimal tick = 0;
                                foreach (var item2 in item1.Lista_2)
                                {
                                    //precioVenta = decimal.Round(decimal.Parse(item2.P_Venta.ToString()) / decimal.Parse("1.18"), 2);
                                    //precioUnitario = precioVenta / item2.Cantidad;
                                    precioUnitario = decimal.Round(decimal.Parse(item2.P_Unitario.ToString()) / decimal.Parse("1.18"), 2);

                                    precioVenta = decimal.Round(precioUnitario * item2.Cantidad, 2);

                                    cell = new PdfPCell(new Phrase("          " + conteo.ToString() + ". " + item2.Medicina, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(item2.Cantidad.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(precioUnitario.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(precioVenta.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    conteo++;
                                    totalImporte += decimal.Round(decimal.Parse(precioVenta.ToString()),2);
                                    TickrtTotal += decimal.Round(decimal.Parse(precioVenta.ToString()),2);
                                    tick += decimal.Round(decimal.Parse(precioVenta.ToString()),2);
                                }
                                cell = new PdfPCell(new Phrase("TOTAL : " + item1.Ticket, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                                cells.Add(cell);
                                cell = new PdfPCell(new Phrase(tick.ToString("N2"), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK };
                                cells.Add(cell);
                            }
                            cell = new PdfPCell(new Phrase("TOTAL : " + item.Tipo, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                            cells.Add(cell);
                            cell = new PdfPCell(new Phrase(TickrtTotal.ToString("N2"), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK };
                            cells.Add(cell);

                            totalTickets += TickrtTotal;

                        }

                        contKindOfService++;

                        cell = new PdfPCell(new Phrase("TOTAL :  FARMACIA HOSPITALARIA ", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase(totalTickets.ToString("N2"), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK };
                        cells.Add(cell);
                        

                        columnWidths = new float[] { 55f, 15f, 15f, 15f };

                        filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                        document.Add(filiationWorker);
                    }
                    #endregion

                    #region Receta
                    cells = new List<PdfPCell>();
                    decimal totalReceta = 0;
                    if (receta != null && receta.Count >= 1)
                    {
                        cell = new PdfPCell(new Phrase(contKindOfService.ToString() + ")  FARMACIA x TTO", fontColumnValueBold2)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                        cells.Add(cell);
                        decimal precioUnitario = 0;
                        decimal precioVenta = 0;

                        foreach (var item in receta)
                        {
                            decimal servicioTotal = 0;

                            int conteo = 1;
                            foreach (var item1 in item.Lista)
                            {
                                //precioUnitario = Decimal.Round(decimal.Parse(item1.P_Unitario.ToString()) / decimal.Parse("1.18"), 2);
                                //precioVenta = decimal.Round(precioUnitario * item1.Cantidad, 2);

                                precioVenta = decimal.Round(decimal.Parse(item1.Total.ToString()) / decimal.Parse("1.18"), 2);
                                precioUnitario = decimal.Round(precioVenta / item1.Cantidad, 2);
                                //precioUnitario = decimal.Round(decimal.Parse(item1.P_Unitario.ToString()) / decimal.Parse("1.18"), 2);

                                cell = new PdfPCell(new Phrase("   " + conteo.ToString() + ". " + item1.Medicina, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);
                                cell = new PdfPCell(new Phrase(item1.Cantidad.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);
                                cell = new PdfPCell(new Phrase(precioUnitario.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);
                                cell = new PdfPCell(new Phrase(precioVenta.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);
                                conteo++;
                                totalImporte += decimal.Round(decimal.Parse(precioVenta.ToString()), 2);
                                servicioTotal += decimal.Round(decimal.Parse(precioVenta.ToString()), 2);       
                            }
                            totalReceta = servicioTotal;
                        }
                        contKindOfService++;

                        cell = new PdfPCell(new Phrase("TOTAL :  FARMACIA x TTO ", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase(totalReceta.ToString("N2"), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK };
                        cells.Add(cell);


                        columnWidths = new float[] { 55f, 15f, 15f, 15f };

                        filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                        document.Add(filiationWorker);
                    }
                    #endregion
                    ///////
                    decimal habitacion = 0;
                    if (!InsertDays && dataHospitalizacion != null)
                    {
                        if (dataHospitalizacion.Count > 0)
                        {

                            cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase(contKindOfService.ToString() + ") HABITACIONES" , fontColumnValueBold2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                        };

                            columnWidths = new float[] { 100f };
                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                            document.Add(table);
                            contKindOfService++;

                            decimal importe = 0;
                            importe = decimal.Parse(costoHabit);
                            totalImporte += importe;
                            decimal costoUnitsinIgv = Decimal.Round(constoUnitaro / decimal.Parse("1.18"), 2);
                            
                            cells = new List<PdfPCell>()
                            {
                                new PdfPCell(new Phrase("("+ Habitaciones +")" + " Habitación " + Habit + " Del " + FechIngreso + " AL " + FechAlta, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(Dias.ToString(), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(costoUnitsinIgv.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase((costoUnitsinIgv*i_dias).ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            };

                            columnWidths = new float[] { 55f, 15f, 15f, 15f };
                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                            document.Add(table);

                            InsertDays = true;
                            habitacion = decimal.Parse(costoUnitsinIgv.ToString()) * decimal.Parse(i_dias.ToString());
                        }

                    }


                    //Alfinal de todo el foreach
                    string igv = "1.18";

                    decimal total = decimal.Round((habitacion + totalReceta + totalTickets + TotalServicios) * decimal.Parse(igv), 2);

                    decimal totalSinConsulta = decimal.Round(precioConsulta, 2);

                    decimal subTotalGeneral = decimal.Round(total / decimal.Parse(igv), 2);
                    decimal IgvGeneral = decimal.Round(total - subTotalGeneral, 2);
                    decimal totalGeneral = decimal.Round(subTotalGeneral + IgvGeneral, 2);

                    //string totalGeneralPalabra = Utils.enletras(totalGeneral.ToString());
                    string totalGeneralPalabra = Utils.enletras(totalGeneral.ToString());

                    //NUEVOS CALCULOS
                    decimal deducible_ = Deducible;
                    decimal coaseguro_ = Coaseguro;
                    decimal TotalParcial = 0;
                    
                    decimal _coaseguroGr = 0;
                    if (Deducible != 0)
                    {
                            TotalParcial = decimal.Round(decimal.Parse(total.ToString()) - ((CostoConsultaDeducible - deducible_) + decimal.Parse(deducible_.ToString())), 2);
                     
                    }
                    else 
                    {
                        TotalParcial = 0;
                    }
                    if (contador == 1 && (receta == null || receta.Count == 0) && (tickets == null || tickets.Count == 0))
                    {
                        _coaseguroGr = 0;
                    }
                    else if (TotalParcial == 0)
                    {
                        if (deducible_ == 0)
                        {
                            if (contadorMedicina == 1)
                            {
                                _coaseguroGr = 0;
                            }
                            else
                            {
                                _coaseguroGr = decimal.Parse(total.ToString()) * (coaseguro_ / 100);
                            }
                        }                        
                    }
                    else
                    {
                        _coaseguroGr = decimal.Round(decimal.Parse(TotalParcial.ToString()) * (coaseguro_ / 100), 2);                      
                    }
                    decimal TotalFinal = decimal.Round(decimal.Parse(total.ToString()) - (_coaseguroGr + deducible_), 2);

                    cells = new List<PdfPCell>()
                    {
                    new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 7f, UseVariableBorders = true, BorderColorTop = BaseColor.BLACK,  BorderColorBottom = BaseColor.WHITE, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE},

                    new PdfPCell(new Phrase(totalGeneralPalabra, fontColumnValue2)) {Colspan = 1, Rowspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase("SUB TOTAL:", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(subTotalGeneral.ToString("N2"), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    new PdfPCell(new Phrase("I.G.V. 18% :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(IgvGeneral.ToString("N2"), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    new PdfPCell(new Phrase("TOTAL GENERAL :", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(total.ToString("N2"), fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    new PdfPCell(new Phrase("MONTO GENERAL COASEGURO :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase("- " + _coaseguroGr.ToString("N2"), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    
                    new PdfPCell(new Phrase("MONTO GENERAL DEDUCIBLE :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase("- " + deducible_.ToString("N2"), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    new PdfPCell(new Phrase("PAGO DEL SEGURO :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(TotalFinal.ToString("N2"), fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    
                };
                    columnWidths = new float[] { 40f, 45f, 15f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);

                

            #endregion

                document.Close();
                writer.Close();
                writer.Dispose();
            
        }

        public static void CreateLiquidacionSegurosGlobal(List<ServiciosDetalle> servicios, personDto dataPacient, organizationDto dataOrganization, organizationDto dataAseguradora, List<HospitalizacionCustom> dataHospitalizacion, DatosSeguuro dataSeguro, List<MeidicinasTicketsLista> tickets, List<RecetasDetalle> receta, string pathFile, decimal CostoConsultaDeducible)
        {

            //iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 30f, 30f, 15f, 41f);
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 30f, 30f, 75f, 65f);

            document.SetPageSize(iTextSharp.text.PageSize.A4);

            //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pathFile, FileMode.Create));
            //pdfPage page = new pdfPage();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pathFile, FileMode.Create));
            pdfPage_NEW page = new pdfPage_NEW();
            page.Title = string.Empty;
            writer.PageEvent = page;
            document.Open();

            #region Declaration Tables
            var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.Gray);
            string include = string.Empty;
            List<PdfPCell> cells = null;
            float[] columnWidths = null;
            string[] columnValues = null;
            string[] columnHeaders = null;
            PdfPTable header2 = new PdfPTable(6);
            header2.HorizontalAlignment = Element.ALIGN_CENTER;
            header2.WidthPercentage = 100;
            float[] widths1 = new float[] { 16.6f, 18.6f, 16.6f, 16.6f, 16.6f, 16.6f };
            header2.SetWidths(widths1);
            PdfPTable companyData = new PdfPTable(6);
            companyData.HorizontalAlignment = Element.ALIGN_CENTER;
            companyData.WidthPercentage = 100;
            float[] widthscolumnsCompanyData = new float[] { 16.6f, 16.6f, 16.6f, 16.6f, 16.6f, 16.6f };
            companyData.SetWidths(widthscolumnsCompanyData);
            PdfPTable filiationWorker = new PdfPTable(4);
            PdfPTable table = null;
            PdfPCell cell = null;
            document.Add(new Paragraph("\r\n"));
            #endregion

            #region Fonts
            Font fontTitle1 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Blue));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));


            Font fontTitleTableAntecedentesOcupacionales = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueAntecedentesOcupacionales = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            #endregion






            #region Cabezera
            #region variables
            //paciente
            string paciente = "- - -", titular = "- - -", DocNumber = "- - -";
            if (dataPacient != null)
            {
                paciente = ": " + dataPacient.v_FirstLastName + " " + dataPacient.v_SecondLastName + ", " +
                           dataPacient.v_FirstName;

                titular = string.IsNullOrEmpty(dataPacient.v_OwnerName) ? ": - - -" : ": " + dataPacient.v_OwnerName.Split('|')[0];
                DocNumber = string.IsNullOrEmpty(dataPacient.v_DocNumber) ? ": - - -" : ": CSL-" + dataPacient.v_DocNumber;
            }

            //empresa
            string orgName = "- - -", orgPhone = "- - -", orgAdress = "- - -";
            if (dataOrganization != null)
            {
                orgName = string.IsNullOrEmpty(dataOrganization.v_Name) ? ": - - -" : ": " + dataOrganization.v_Name;
                orgAdress = string.IsNullOrEmpty(dataOrganization.v_Address) ? ": - - -" : ": " + dataOrganization.v_Address;
                orgPhone = string.IsNullOrEmpty(dataOrganization.v_PhoneNumber) ? ": - - -" : ": " + dataOrganization.v_PhoneNumber;
            }

            //aseguradora
            string asgName = "- - -", asgRuc = "- - -";
            if (dataAseguradora != null)
            {
                asgRuc = string.IsNullOrEmpty(dataAseguradora.v_IdentificationNumber) ? ": - - -" : ": " + dataAseguradora.v_IdentificationNumber;
                asgName = string.IsNullOrEmpty(dataAseguradora.v_Name) ? ": - - -" : ": " + dataAseguradora.v_Name;
            }

            #region seguro

            string Servicio = "- - -", F_Servicio = "- - -", Plan = "- - -", Protocolo = "- - -", Solicitud = "- - -", Medico = "- - -";
            decimal Factor = 0, Descuento_PPS = 0, Deducible = 0, Coaseguro = 0, Habitacion = 0;
            if (dataSeguro != null)
            {
                Servicio = dataSeguro.Servicio;
                Solicitud = dataSeguro.Solicitud;
                F_Servicio = dataSeguro.FechaServicio.ToString().Split(' ')[0];
                Plan = dataSeguro.Plan;
                Factor = dataSeguro.Factor;
                Descuento_PPS = dataSeguro.DescuentoPPS;
                Deducible = dataSeguro.Deducible;
                Coaseguro = dataSeguro.Coaseguro;
                Habitacion = dataSeguro.Habitacion;
                Protocolo = dataSeguro.Protocolo;
                Medico = dataSeguro.Medico;
            }
            #endregion
            //hospitalizacion
            int i_dias = 1;
            int nroHabit = 1;
            string FechIngreso = ": " + F_Servicio, FechAlta = "", Habit = "", Dias = "", Habitaciones = ": 1", costoHabit = "---", texto_Cuarto = "", textoDiasHab = "", texto_Alta = "";
            decimal constoUnitaro = 0;
            //int dias = 0;
            #endregion
            #region Conexion SAM
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            #endregion
            var cadena1 = "select SR.v_NroLiquidacion, LQ.v_LiquidacionId from service SR inner join liquidacion LQ on SR.v_NroLiquidacion = LQ.v_NroLiquidacion where SR.v_ServiceId='" + Servicio + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            string nroliq = "";
            string codigointerno = "";
            while (lector.Read())
            {
                nroliq = lector.GetValue(0).ToString();
                codigointerno = lector.GetValue(1).ToString();
            }
            lector.Close();
            if (dataHospitalizacion != null)
            {
                #region


                #endregion
                if (dataHospitalizacion.Count > 0)
                {
                    List<DateTime> DIngreso = new List<DateTime>();
                    List<DateTime> DAlta = new List<DateTime>();
                    foreach (var data in dataHospitalizacion)
                    {
                        DIngreso.Add(data.d_FechaIngreso);
                        DAlta.Add(data.d_FechaAlta);
                    }

                    List<DateTime> ListDIngreso = DIngreso.OrderBy(x => x).ToList();
                    List<DateTime> ListDAlta = DAlta.OrderByDescending(x => x).ToList();
                    texto_Cuarto = "CUARTO";
                    Habit = dataHospitalizacion[0].v_Habitacion == null ? ": ---" : ": " + dataHospitalizacion[0].v_Habitacion;
                    List<string> habitaciones = new List<string>();
                    double costo = 0.00;
                    double costoUnit = 0.00;
                    if (ListDIngreso.Count() > 0 && ListDAlta.Count() > 0)
                    {
                        FechIngreso = ListDIngreso.Count() == 0 ? ": ---" : ": " + ListDIngreso[0].ToShortDateString();
                        FechAlta = ListDAlta.Count() == 0 ? " " : ": " + ListDAlta[0].ToShortDateString();
                        texto_Alta = "FECHA DE ALTA";
                        TimeSpan ts = ListDAlta[0] - ListDIngreso[0];
                        // Difference in days.
                        int differenceInDays = ts.Days;
                        if (differenceInDays <= 1)
                        {
                            differenceInDays = 1;
                            i_dias = 1;
                        }

                        i_dias = differenceInDays;
                        textoDiasHab = "DIAS HAB.";
                        Dias = ": " + differenceInDays.ToString();
                    }

                    foreach (var dat in dataHospitalizacion)
                    {
                        costo += double.Parse(dat.d_Precio.ToString()) * i_dias;
                        constoUnitaro = decimal.Parse(dat.d_Precio.ToString());
                        var find = habitaciones.Find(x => x == dat.v_Habitacion);
                        if (find == null)
                        {
                            habitaciones.Add(dat.v_Habitacion);
                        }
                    }
                    costo = Math.Round(costo, 2);
                    costoHabit = (costo).ToString();
                    Habitaciones = habitaciones.Count.ToString();

                    // Difference in days, hours, and minutes.

                }

            }

            var cellsTit = new List<PdfPCell>()
            { 
                
                new PdfPCell(new Phrase("LIQUIDACIÓN " + nroliq + " [RESUMEN] ", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("Cajamarca, " + DateTime.Now.ToShortDateString() + " \n" + DateTime.Now.ToShortTimeString(), fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, BorderColor = BaseColor.WHITE},
            };
            columnWidths = new float[] { 60f, 40f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);

            #region CABECERA
            cells = new List<PdfPCell>()
            {                
                new PdfPCell(new Phrase("SEÑOR(ES)", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(asgName, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("H.C", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(DocNumber, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("RUC", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(asgRuc, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(texto_Cuarto, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(Habit, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("PACIENTE", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(paciente, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(textoDiasHab, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(Dias.ToString(), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("TITULAR", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(titular, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("SOLC. / CART. GAR.", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": " + Solicitud, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase("CENTRO DE TRABAJO", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(orgName, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("TELEFONO", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(orgPhone, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase("DIRECCIÓN", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(orgAdress, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("PLAN", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": " + Plan, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase("FECHA DE INGRESO", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(FechIngreso, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("FACTOR: ", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": " + Factor.ToString() , fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("DESCUENTO PPS: ", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": " + Descuento_PPS + "%", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase(texto_Alta, fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(FechAlta, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("DEDUCIBLE: ", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": S/. " + Deducible, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("COASEGURO", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": " + Coaseguro.ToString() + "%", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("MEDICO A CARGO", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": " + Medico, fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("CÓDIGO INTERNO", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": " + Servicio, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("\n", fontColumnValue)) {Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE, BorderWidthBottom = 0},


            };
            columnWidths = new float[] { 12f, 8f, 16f, 18f, 12f, 17f, 17f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #endregion

            #region Cuerpo

            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("NOMBRE DEL SERVICIO", fontTitle2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, BorderColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("IMPORTE", fontTitle2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, BorderColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("- - - - - -", fontTitle2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, BorderColor = BaseColor.BLACK},
                new PdfPCell(new Phrase("TOTAL", fontTitle2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, BorderColor = BaseColor.BLACK},

            };

            columnWidths = new float[] { 55f, 15f, 15f, 15f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            int contador = 0;



            decimal totalConsumoGeneral = 0;
            decimal TotalDeducibleGlob = 0;
            decimal totalDesctGeneral = 0;
            bool InsertFarmOperaciones = false;
            bool InsertFarmReceta = false;
            bool InsertDays = false;
            bool EsHospitalizacion = false;
            List<string> ComponentesAgregados_ = new List<string>();
            List<string> ComponentesAgregados = new List<string>();
            int contKindOfService = 1;
            decimal totalImporte = 0;
            decimal totalDescuento = 0;
            decimal totalConDescuento = 0;
            decimal totalDeducible = 0;
            decimal totalConsumos = 0;

            cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f, BorderColor = BaseColor.WHITE, BorderWidthTop = 0},
                            new PdfPCell(new Phrase("-----------------------------------------------------------------------------------------------------------------------", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, BorderColor = BaseColor.WHITE, BorderWidthTop = 0},
                        };

            columnWidths = new float[] { 40f, 60f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #region SERVICIOS DINÁMICO
            decimal TotalServicios = 0;
            decimal precioConsulta = 0;
            cells = new List<PdfPCell>();
            int contadorMedicina = 0;
            if (servicios != null)
            {
                decimal precio = 0;
                foreach (var item in servicios)
                {
                    decimal servicioTotal = 0;
                    int contador_ = 1;
                    cell = new PdfPCell(new Phrase(contKindOfService.ToString() + ") " + item.Tipo, fontColumnValueBold2)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                    cells.Add(cell);

                    if (item.Lista1.Count >= 1 && item.Lista2 == null)
                    {
                        int conteo = 1;
                        foreach (var item1 in item.Lista1)
                        {
                            precio = decimal.Round(decimal.Parse(item1.Precio.ToString()) / decimal.Parse("1.18"), 2);

                            decimal cant = decimal.Round(item1.Cantidad);
                            decimal pU = decimal.Round((precio / item1.Cantidad), 2);

                            if (contador_ == 1)
                            {
                                precioConsulta = decimal.Parse(precio.ToString());
                            }

                            cell = new PdfPCell(new Phrase("     " + conteo.ToString() + ". [" + item1.Segus + "] - " + item1.NombreComponente + "( " + cant + " )", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                            cells.Add(cell);
                            cell = new PdfPCell(new Phrase(pU.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                            cells.Add(cell);
                            cell = new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                            cells.Add(cell);
                            cell = new PdfPCell(new Phrase(precio.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                            cells.Add(cell);
                            conteo++;
                            totalImporte += decimal.Round(decimal.Parse(precio.ToString()), 2);
                            servicioTotal += decimal.Round(decimal.Parse(precio.ToString()), 2);
                            contador_++;
                            if (item1.Grupo == "MEDICINA C")
                            {
                                contadorMedicina++;
                            }
                        }
                    }
                    else if ((item.Lista2 != null && item.Lista2.Count >= 1) && item.Lista1.Count == 0)
                    {
                        
                        foreach (var item1 in item.Lista2)
                        {
                            int conteo = 1;
                            cell = new PdfPCell(new Phrase("     " + item1.Grupo, fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                            cells.Add(cell);

                            //var lista = item1.Lista2

                            foreach (var item2 in item1.Lista2)
                            {
                                precio = decimal.Round(decimal.Parse(item2.Precio.ToString()) / decimal.Parse("1.18"), 2);

                                decimal cant = decimal.Round(item2.Cantidad);
                                decimal pU = decimal.Round((precio / item2.Cantidad), 2);

                                cell = new PdfPCell(new Phrase("          " + conteo.ToString() + ". [" + item2.Segus + "] - " + item2.NombreComponente + "( "+cant+" )", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);
                                cell = new PdfPCell(new Phrase(pU.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);
                                cell = new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);
                                cell = new PdfPCell(new Phrase(precio.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);
                                conteo++;
                                totalImporte += decimal.Round(decimal.Parse(precio.ToString()), 2);
                                servicioTotal += decimal.Round(decimal.Parse(precio.ToString()), 2);
                            }

                        }
                    }
                    cell = new PdfPCell(new Phrase("TOTAL : " + item.Tipo, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(servicioTotal.ToString("N2"), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK };
                    cells.Add(cell);
                    TotalServicios += servicioTotal;
                    contKindOfService++;

                }
                columnWidths = new float[] { 55f, 15f, 15f, 15f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                document.Add(filiationWorker);
            }
            #endregion

            #region Tickets
            cells = new List<PdfPCell>();
            decimal totalTickets = 0;
            if (tickets != null && tickets.Count >= 1)
            {
                

                cell = new PdfPCell(new Phrase(contKindOfService.ToString() + ")  FARMACIA HOSPITALARIA", fontColumnValueBold2)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                cells.Add(cell);
                decimal precioUnitario = 0;
                decimal precioVenta = 0;

                int conteo = 1;
                foreach (var item in tickets)
                {
                    decimal TickrtTotal = 0;

                    int contarProductos = 0;
                   
                    decimal sumatickets = 0;
                    foreach (var item1 in item.Lista_1)
                    {
                       
                        decimal tick = 0;
                        foreach (var item2 in item1.Lista_2)
                        {
                            //precioVenta = decimal.Round(decimal.Parse(item2.P_Venta.ToString()) / decimal.Parse("1.18"), 2);
                            //precioUnitario = precioVenta / item2.Cantidad;
                            precioUnitario = decimal.Round(decimal.Parse(item2.P_Unitario.ToString()) / decimal.Parse("1.18"), 2);

                            precioVenta = decimal.Round(precioUnitario * item2.Cantidad, 2);

                            contarProductos += item2.Cantidad;
                            
                            totalImporte += decimal.Round(decimal.Parse(precioVenta.ToString()), 2);
                            TickrtTotal += decimal.Round(decimal.Parse(precioVenta.ToString()), 2);
                            tick += decimal.Round(decimal.Parse(precioVenta.ToString()), 2);
                        }
                       
                    }


                    cell = new PdfPCell(new Phrase("   " + conteo + ".- " + item.Tipo + " ( " + contarProductos + " )", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(TickrtTotal.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(TickrtTotal.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                    cells.Add(cell);

                    
                    totalTickets += TickrtTotal;
                    conteo++;

                }

                cell = new PdfPCell(new Phrase("TOTAL :  FARMACIA HOSPITALARIA ", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(totalTickets.ToString("N2"), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK };
                cells.Add(cell);


                columnWidths = new float[] { 55f, 15f, 15f, 15f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                document.Add(filiationWorker);

                contKindOfService++;
            }
            #endregion

            #region Receta
            cells = new List<PdfPCell>();
            decimal totalReceta = 0;
            if (receta != null && receta.Count >= 1)
            {
                cell = new PdfPCell(new Phrase(contKindOfService.ToString() + ")  FARMACIA x TTO", fontColumnValueBold2)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                cells.Add(cell);
                decimal precioUnitario = 0;
                decimal precioVenta = 0;

                foreach (var item in receta)
                {
                    decimal servicioTotal = 0;

                    int conteo = 1;
                    foreach (var item1 in item.Lista)
                    {
                        //precioUnitario = Decimal.Round(decimal.Parse(item1.P_Unitario.ToString()) / decimal.Parse("1.18"), 2);
                        //precioVenta = decimal.Round(precioUnitario * item1.Cantidad, 2);

                        precioVenta = decimal.Round(decimal.Parse(item1.Total.ToString()) / decimal.Parse("1.18"), 2);
                        precioUnitario = decimal.Round(precioVenta / item1.Cantidad, 2);
                        //precioUnitario = decimal.Round(decimal.Parse(item1.P_Unitario.ToString()) / decimal.Parse("1.18"), 2);

                        cell = new PdfPCell(new Phrase("   " + conteo.ToString() + ". " + item1.Medicina, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase(item1.Cantidad.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase(precioUnitario.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase(precioVenta.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                        cells.Add(cell);
                        conteo++;
                        totalImporte += decimal.Round(decimal.Parse(precioVenta.ToString()), 2);
                        servicioTotal += decimal.Round(decimal.Parse(precioVenta.ToString()), 2);
                    }
                    totalReceta = servicioTotal;
                }
                contKindOfService++;

                cell = new PdfPCell(new Phrase("TOTAL :  FARMACIA x TTO ", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(totalReceta.ToString("N2"), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK };
                cells.Add(cell);


                columnWidths = new float[] { 55f, 15f, 15f, 15f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                document.Add(filiationWorker);
            }
            #endregion
            ///////
            decimal habitacion = 0;
            if (!InsertDays && dataHospitalizacion != null)
            {
                if (dataHospitalizacion.Count > 0)
                {

                    cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase(contKindOfService.ToString() + ") HABITACIONES" , fontColumnValueBold2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                        };

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);
                    contKindOfService++;

                    decimal importe = 0;
                    importe = decimal.Parse(costoHabit);
                    totalImporte += importe;
                    decimal costoUnitsinIgv = Decimal.Round(constoUnitaro / decimal.Parse("1.18"), 2);

                    cells = new List<PdfPCell>()
                            {
                                new PdfPCell(new Phrase("("+ Habitaciones +")" + " Habitación " + Habit + " Del " + FechIngreso + " AL " + FechAlta, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(Dias.ToString(), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(costoUnitsinIgv.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase((costoUnitsinIgv*i_dias).ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            };

                    columnWidths = new float[] { 55f, 15f, 15f, 15f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);

                    InsertDays = true;
                    habitacion = decimal.Parse(costoUnitsinIgv.ToString()) * decimal.Parse(i_dias.ToString());
                }

            }


            //Alfinal de todo el foreach
            string igv = "1.18";

            decimal total = decimal.Round((habitacion + totalReceta + totalTickets + TotalServicios) * decimal.Parse(igv), 2);

            decimal totalSinConsulta = decimal.Round(precioConsulta, 2);

            decimal subTotalGeneral = decimal.Round(total / decimal.Parse(igv), 2);
            decimal IgvGeneral = decimal.Round(total - subTotalGeneral, 2);
            decimal totalGeneral = decimal.Round(subTotalGeneral + IgvGeneral, 2);

            //string totalGeneralPalabra = Utils.enletras(totalGeneral.ToString());
            string totalGeneralPalabra = Utils.enletras(totalGeneral.ToString());

            //NUEVOS CALCULOS
            decimal deducible_ = Deducible;
            decimal coaseguro_ = Coaseguro;
            decimal TotalParcial = 0;

            decimal _coaseguroGr = 0;
            if (Deducible != 0)
            {
                TotalParcial = decimal.Round(decimal.Parse(total.ToString()) - ((CostoConsultaDeducible - deducible_) + decimal.Parse(deducible_.ToString())), 2);

            }
            else
            {
                TotalParcial = 0;
            }
            if (contador == 1 && (receta == null || receta.Count == 0) && (tickets == null || tickets.Count == 0))
            {
                _coaseguroGr = 0;
            }
            else if (TotalParcial == 0)
            {
                if (deducible_ == 0)
                {
                    if (contadorMedicina == 1)
                    {
                        _coaseguroGr = 0;
                    }
                    else
                    {
                        _coaseguroGr = decimal.Parse(total.ToString()) * (coaseguro_ / 100);
                    }
                }
            }
            else
            {
                _coaseguroGr = decimal.Round(decimal.Parse(TotalParcial.ToString()) * (coaseguro_ / 100), 2);
            }
            decimal TotalFinal = decimal.Round(decimal.Parse(total.ToString()) - (_coaseguroGr + deducible_), 2);

            cells = new List<PdfPCell>()
                    {
                    new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 7f, UseVariableBorders = true, BorderColorTop = BaseColor.BLACK,  BorderColorBottom = BaseColor.WHITE, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE},

                    new PdfPCell(new Phrase(totalGeneralPalabra, fontColumnValue2)) {Colspan = 1, Rowspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase("SUB TOTAL:", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(subTotalGeneral.ToString("N2"), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    new PdfPCell(new Phrase("I.G.V. 18% :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(IgvGeneral.ToString("N2"), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    new PdfPCell(new Phrase("TOTAL GENERAL :", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(total.ToString("N2"), fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    new PdfPCell(new Phrase("MONTO GENERAL COASEGURO :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase("- " + _coaseguroGr.ToString("N2"), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    
                    new PdfPCell(new Phrase("MONTO GENERAL DEDUCIBLE :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase("- " + deducible_.ToString("N2"), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    new PdfPCell(new Phrase("PAGO DEL SEGURO :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(TotalFinal.ToString("N2"), fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    
                };
            columnWidths = new float[] { 40f, 45f, 15f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);



            #endregion

            document.Close();
            writer.Close();
            writer.Dispose();

        }

    }
}
