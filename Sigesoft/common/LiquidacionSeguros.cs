using iTextSharp.text;
using iTextSharp.text.pdf;
using NetPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BE.Custom;
using Sigesoft.Common;
using System.Data.SqlClient;

namespace Sigesoft
{
    public class LiquidacionSeguros
    {
        public static void CreateLiquidacionSeguros(JoinTicketDetails dataRecetaDetail, List<TiposCuenta> dataFarmaHops, List<ComponentForLiquiCustom> ListServicesAndCost, personDto dataPacient, organizationDto dataOrganization, organizationDto dataAseguradora, List<HospitalizacionCustom> dataHospitalizacion, DatosSeguuro dataSeguro, List<MeidicinasTickets> listaTipoMedicina, List<RecetasSeguros> listaReceta, string pathFile, decimal CostoConsultaDeducible )
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
            string FechIngreso = ": "+ F_Servicio, FechAlta = "", Habit = "", Dias = "", Habitaciones = ": 1", costoHabit = "---", texto_Cuarto = "", textoDiasHab = "", texto_Alta = "";
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
                    decimal costo = 0;
                    decimal costoUnit = 0;
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
                        costo += decimal.Parse(dat.d_Precio.ToString()) * i_dias;
                        constoUnitaro = decimal.Parse(dat.d_Precio.ToString());
                        var find = habitaciones.Find(x => x == dat.v_Habitacion);
                        if (find == null)
                        {
                            habitaciones.Add(dat.v_Habitacion);
                        }
                    }
                    costo = Math.Round(costo, 4);
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
                new PdfPCell(new Phrase(": " + Medico, fontColumnValue)) {Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
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
            decimal serviciosTotal = 0;
            decimal camasTotal = 0;
            decimal medicinasTotal = 0;


            if (ListServicesAndCost != null)
            {
                if (ListServicesAndCost.Count > 0)
                {
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
                    decimal precioConsulta = 0;
                    foreach (var service in ListServicesAndCost)
                    {
                        cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f, BorderColor = BaseColor.WHITE, BorderWidthTop = 0},
                            new PdfPCell(new Phrase("-----------------------------------------------------------------------------------------------------------------------", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, BorderColor = BaseColor.WHITE, BorderWidthTop = 0},
                        };

                        columnWidths = new float[] { 40f, 60f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);

                        string KindName = "";
                        
                        foreach (var serComponent in service.ListKindOfServices)
                        {

                            cells = new List<PdfPCell>()
                            {
                                new PdfPCell(new Phrase(contKindOfService.ToString() + ") " + serComponent.KindOfServiceName, fontColumnValueBold2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            };

                            columnWidths = new float[] { 100f };
                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                            document.Add(table);
                            KindName = serComponent.KindOfServiceName;
                            contKindOfService++;
                            int ServiciosAuxiliares = 2;
                            foreach (var categorias in serComponent.ListCategoryForKOS)
                            {
                                if (serComponent.KindOfServiceId == ServiciosAuxiliares)
                                {
                                    cells = new List<PdfPCell>()
                                    {
                                        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase(categorias.CategoryName, fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                        
                                    };

                                    columnWidths = new float[] { 5f, 95f };
                                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                                    document.Add(table);

                                    foreach (var comp in categorias.ListServicesComponentForKOS)
                                    {
                                        decimal importe = 0;
                                        decimal discount = 0;
                                        decimal total = 0;
                                        var findComp = ComponentesAgregados_.Find(x => x == comp.ComponentName);
                                        if (findComp == null)
                                        {
                                            int totalEncontrados = 0;
                                            foreach (var comFind in categorias.ListServicesComponentForKOS)
                                            {
                                                if (comp.ComponentName == comFind.ComponentName)
                                                {
                                                    if (comFind.SaldoPaciente != null)
                                                    {
                                                        totalDeducible += decimal.Parse(comFind.SaldoPaciente.Value.ToString("N4"));
                                                    }
                                                    if (service.Discount.Value > 0)
                                                    {
                                                        decimal importeActual = decimal.Parse(comFind.Price.ToString()) / (1 - (decimal.Parse(service.Discount.Value.ToString()) / 100));
                                                        importe = importeActual;
                                                        discount = importeActual * decimal.Parse(service.Discount.Value.ToString());

                                                    }
                                                    else
                                                    {
                                                        importe = decimal.Parse(comFind.Price.ToString());
                                                    }
                                                    total += decimal.Parse(comFind.Price.ToString());
                                                    totalEncontrados++;

                                                }
                                            }

                                            total = decimal.Round(total, 4);
                                            var prinImporte = (importe / decimal.Parse("1.18")) * totalEncontrados;

                                            var prinImporteSIG = (importe / decimal.Parse("1.18"));

                                            importe = decimal.Round(importe, 4) * totalEncontrados;

                                            cells = new List<PdfPCell>()
                                            {
                                                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                                new PdfPCell(new Phrase("("+ totalEncontrados.ToString() +")" + " [" + comp.CodigoSegus + "] " + comp.ComponentName, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                                new PdfPCell(new Phrase(prinImporteSIG.ToString("N4"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                                new PdfPCell(new Phrase(prinImporte.ToString("N4"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                            };

                                            columnWidths = new float[] { 5f, 50f, 15f, 15f, 15f };
                                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                                            document.Add(table);
                                        }
                                        ComponentesAgregados_.Add(comp.ComponentName);
                                        totalImporte += decimal.Round(importe, 4);
                                        totalConDescuento += decimal.Round(total, 4);
                                        totalDescuento += decimal.Round(discount, 4);

                                        contador++;
                                    }

                                }
                                else
                                {
                                    foreach (var componentes in categorias.ListServicesComponentForKOS)
                                    {
                                        decimal importe = 0;
                                        decimal discount = 0;
                                        decimal total = 0;
                                        var findComp = ComponentesAgregados_.Find(x => x == componentes.ComponentName);
                                        if (findComp == null)
                                        {
                                            int totalEncontrados = 0;
                                            int contador_ = 1;
                                            foreach (var comFind in categorias.ListServicesComponentForKOS)
                                            {
                                                if (componentes.ComponentName == comFind.ComponentName)
                                                {
                                                    if (comFind.SaldoPaciente != null)
                                                    {
                                                        totalDeducible += decimal.Parse(comFind.SaldoPaciente.Value.ToString("N4"));
                                                    }
                                                    if (service.Discount.Value > 0)
                                                    {
                                                        decimal importeActual = decimal.Parse(comFind.Price.Value.ToString()) / (1 - (decimal.Parse(service.Discount.Value.ToString()) / 100));
                                                        importe = importeActual;
                                                        discount = importeActual * decimal.Parse(service.Discount.Value.ToString());
                                                    }
                                                    else
                                                    {
                                                        importe = decimal.Parse(comFind.Price.Value.ToString());
                                                    }
                                                    total += decimal.Parse(comFind.Price.ToString());
                                                    totalEncontrados++;
                                                    contador++;
                                                }
                                            }



                                            total = Math.Round(total, 4);

                                            var prinImporte = (importe / decimal.Parse("1.18")) * totalEncontrados;
                                            var prinImporteSIG = (importe / decimal.Parse("1.18"));

                                            importe = Math.Round(importe, 4) * totalEncontrados;

                                            if (contador_ == 1)
                                            {
                                                precioConsulta = decimal.Parse(prinImporte.ToString());
                                            }
                                            cells = new List<PdfPCell>()
                                            {
                                                new PdfPCell(new Phrase("("+ totalEncontrados.ToString() +")" + " [" + componentes.CodigoSegus + "] " + componentes.ComponentName, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                                new PdfPCell(new Phrase(prinImporteSIG.ToString("N4"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                                new PdfPCell(new Phrase(discount.ToString("N4"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                                new PdfPCell(new Phrase(prinImporte.ToString("N4"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                            };

                                            columnWidths = new float[] { 55f, 15f, 15f, 15f };
                                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                                            document.Add(table);
                                            totalImporte += decimal.Round(importe, 4);
                                            totalConDescuento += decimal.Round(total, 4);
                                            totalDescuento += decimal.Round(discount, 4);
                                        }
                                        ComponentesAgregados_.Add(componentes.ComponentName);
                                    }
                                }


                            }

                            //Esto se ejecuta cuando llega al último de la lista
                            var printImporteTotal = totalImporte / decimal.Parse("1.18");
                            cells = new List<PdfPCell>()
                                    {
                                        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, UseVariableBorders = true, BorderColorTop = BaseColor.BLACK,  BorderColorBottom = BaseColor.WHITE, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE},
                                        new PdfPCell(new Phrase("Total : "+ KindName, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase(printImporteTotal.ToString("N4"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                        new PdfPCell(new Phrase(printImporteTotal.ToString("N4"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                                    };
                            columnWidths = new float[] { 55f, 15f, 15f, 15f };
                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                            document.Add(table);
                            totalConsumos += Math.Round(totalConDescuento, 4);

                            serviciosTotal += printImporteTotal;

                            totalImporte = 0;
                            totalDescuento = 0;
                            totalConDescuento = 0;
                            string deducible = service.ImporteDeducible == null
                                ? "0"
                                : service.ImporteDeducible.Value.ToString();

                            TotalDeducibleGlob += Math.Round(totalDeducible, 4);
                            totalDeducible = 0;
                        }

                    }
                    ///////
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
                            totalImporte += decimal.Round(importe, 4);
                            decimal costoUnitsinIgv = decimal.Round(constoUnitaro / decimal.Parse("1.18"), 4);
                            camasTotal = costoUnitsinIgv * i_dias;
                            cells = new List<PdfPCell>()
                            {
                                new PdfPCell(new Phrase("("+ Habitaciones +")" + " Habitación " + Habit + " Del " + FechIngreso + " AL " + FechAlta, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(costoUnitsinIgv.ToString("N4"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase((camasTotal).ToString("N4"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            };

                            columnWidths = new float[] { 55f, 15f, 15f, 15f };
                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                            document.Add(table);

                            InsertDays = true;
                        }

                    }
                    if (InsertDays)
                    {
                        decimal costoUnitsinIgv = decimal.Round(constoUnitaro / decimal.Parse("1.18"), 4);

                        cells = new List<PdfPCell>()
                            {
                                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, UseVariableBorders = true, BorderColorTop = BaseColor.BLACK,  BorderColorBottom = BaseColor.WHITE, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE},
                                new PdfPCell(new Phrase("Total : HABITACIÓN", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(costoUnitsinIgv.ToString("N4"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase((costoUnitsinIgv*i_dias).ToString("N4"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                            };
                        columnWidths = new float[] { 55f, 15f, 15f, 15f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                        totalConsumos += Math.Round(totalImporte, 4);
                        totalImporte = 0;

                    }

                    //tickets sala
                    if (listaTipoMedicina != null)
                    {
                        decimal totalCoaseguro = 0;
                        if (listaTipoMedicina.Count > 0)
                        {//para el total de productos usados en las operaciones
                            cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase(contKindOfService.ToString() + ") FARMACIA HOSPITALARIA" , fontColumnValueBold2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                        };

                            columnWidths = new float[] { 100f };
                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                            document.Add(table);
                            contKindOfService++;

                            decimal importeSinIgv = 0;
                            int totalProd = 1;
                            decimal totalFarmacia = 0;
                            foreach (var data in listaTipoMedicina)
                            {

                                decimal importe = 0;
                                importe = decimal.Parse(data.Total.ToString());
                                importeSinIgv = importe / decimal.Parse("1.18");
                                totalImporte += decimal.Round(importe, 4);
                                cells = new List<PdfPCell>()
                            {
                                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase("("+ data.Cantidad.ToString() +")" + " [FARMACIA]" + data.Tipo, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(importeSinIgv.ToString("N4"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(importeSinIgv.ToString("N4"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            };

                                columnWidths = new float[] { 5f, 50f, 15f, 15f, 15f };
                                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                                document.Add(table);
                                totalCoaseguro += decimal.Parse(data.Total.ToString("N4"));
                                totalFarmacia += decimal.Round(decimal.Parse(importeSinIgv.ToString()), 4);
                            }

                            cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, BorderColor = BaseColor.WHITE},
                            new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, UseVariableBorders = true, BorderColorTop = BaseColor.BLACK,  BorderColorBottom = BaseColor.WHITE, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE},
                            new PdfPCell(new Phrase("Total : FARMACIA HOSPITALARIA", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            new PdfPCell(new Phrase(totalFarmacia.ToString("N4"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            new PdfPCell(new Phrase(totalFarmacia.ToString("N4"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                        };
                            columnWidths = new float[] { 55f, 15f, 15f, 15f };
                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                            document.Add(table);
                            totalConsumos += Math.Round(totalImporte, 4);
                            totalImporte = 0;

                            medicinasTotal += totalFarmacia;

                        }


                        totalDesctGeneral += decimal.Parse(totalCoaseguro.ToString());
                    }
                    ////para los medicamentos que se dieron con receta
                    if (listaReceta != null)
                    {
                        double totalCoaseguro = 0.00;
                        if (listaReceta.Count > 0)
                        {
                            cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase(contKindOfService.ToString() + ") FARMACIA x TTO " , fontColumnValueBold2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                        };

                            columnWidths = new float[] { 100f };
                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                            document.Add(table);
                            contKindOfService++;

                            decimal importeSinIgv =0;
                            int totalProd = 1;
                            decimal totalFarmacia = 0;
                            foreach (var data in listaReceta)
                            {

                                decimal importe = 0;
                                importe = decimal.Round(decimal.Parse(data.Total.ToString()), 4);
                                importeSinIgv = decimal.Round(importe / decimal.Parse("1.18"), 4);
                                totalImporte += decimal.Round(importe, 4);
                                cells = new List<PdfPCell>()
                            {
                                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase("("+ data.Cantidad.ToString() +")" + " [FARMACIA]" + data.Receta, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(importeSinIgv.ToString("N4"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(importeSinIgv.ToString("N4"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            };

                                columnWidths = new float[] { 5f, 50f, 15f, 15f, 15f };
                                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                                document.Add(table);
                                totalCoaseguro += double.Parse(data.Total.ToString("N4"));
                                totalFarmacia += decimal.Round(decimal.Parse(importeSinIgv.ToString()), 4);
                            }

                            cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, BorderColor = BaseColor.WHITE},
                            new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, UseVariableBorders = true, BorderColorTop = BaseColor.BLACK,  BorderColorBottom = BaseColor.WHITE, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE},
                            new PdfPCell(new Phrase("Total : FARMACIA x TTO", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            new PdfPCell(new Phrase(totalFarmacia.ToString("N4"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            new PdfPCell(new Phrase(totalFarmacia.ToString("N4"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                        };
                            columnWidths = new float[] { 55f, 15f, 15f, 15f };
                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                            document.Add(table);
                            totalConsumos += Math.Round(totalImporte, 4);
                            totalImporte = 0;
                            medicinasTotal += totalFarmacia;
                        }
                    }

                    totalConsumoGeneral += decimal.Round(totalConsumos, 4);
                    //Alfinal de todo el foreach
                    string igv = "1.18";
                    
                    decimal subTotalGeneral = decimal.Round(serviciosTotal + camasTotal + medicinasTotal, 4);
                    decimal totalGeneral = decimal.Round(subTotalGeneral * decimal.Parse(igv), 4);
                    decimal IgvGeneral = decimal.Round(totalGeneral - subTotalGeneral, 4);

                    string totalGeneralPalabra = Utils.enletras(totalGeneral.ToString());

                    //NUEVOS CALCULOS
                    decimal deducible_ = Deducible;
                    decimal coaseguro_ = Coaseguro;
                    decimal TotalParcial = 0;

                    //if (Deducible != 0)
                    //{
                    //    TotalParcial = decimal.Round(decimal.Parse(totalConsumoGeneral.ToString()) - (precioConsulta * decimal.Parse("1.18")), 4);
                    //}
                    //else
                    //{
                    //    TotalParcial = 0;
                    //}

                    if (Deducible != 0)
                    {
                        TotalParcial = decimal.Round(decimal.Parse(totalGeneral.ToString()) - ((CostoConsultaDeducible - deducible_) + decimal.Parse(deducible_.ToString())), 4);

                    }
                    else
                    {
                        TotalParcial = 0;
                    }

                    decimal _coaseguroGr = 0;
                    if (contador == 1 && (listaReceta == null || listaReceta.Count == 0) && (listaTipoMedicina == null || listaTipoMedicina.Count == 0))
                    {
                        _coaseguroGr = 0;
                    }
                    else if (TotalParcial == 0)
                    {
                        _coaseguroGr = decimal.Round(decimal.Parse(totalConsumoGeneral.ToString()) * (coaseguro_ / 100), 4);
                    }
                    else
                    {
                        _coaseguroGr = decimal.Round(decimal.Parse(TotalParcial.ToString()) * (coaseguro_ / 100), 4);
                    }
                    //decimal TotalFinal = decimal.Round(decimal.Parse(totalConsumoGeneral.ToString()) - (_coaseguroGr + deducible_), 4);
                    decimal TotalFinal = decimal.Round(decimal.Parse(totalGeneral.ToString()) - (_coaseguroGr + deducible_), 4);
 
                    cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 7f, UseVariableBorders = true, BorderColorTop = BaseColor.BLACK,  BorderColorBottom = BaseColor.WHITE, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE},

                    new PdfPCell(new Phrase(totalGeneralPalabra, fontColumnValue2)) {Colspan = 1, Rowspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase("SUB TOTAL:", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(subTotalGeneral.ToString("N4"), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    new PdfPCell(new Phrase("I.G.V. 18% :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(IgvGeneral.ToString("N4"), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    new PdfPCell(new Phrase("TOTAL GENERAL :", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(totalGeneral.ToString("N4"), fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    new PdfPCell(new Phrase("MONTO GENERAL COASEGURO :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase("- " + _coaseguroGr.ToString("N4"), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    
                    new PdfPCell(new Phrase("MONTO GENERAL DEDUCIBLE :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase("- " + deducible_.ToString("N4"), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    new PdfPCell(new Phrase("PAGO DEL SEGURO :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(TotalFinal.ToString("N4"), fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    
                };
                    columnWidths = new float[] { 40f, 45f, 15f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);

                }


            #endregion

                document.Close();
                writer.Close();
                writer.Dispose();
            }
        }

    }
}
