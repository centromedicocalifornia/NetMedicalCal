using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms.PropertyGridInternal;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Sigesoft.Node.WinClient.BE;
using Font = iTextSharp.text.Font;
using Sigesoft.Node.WinClient.BE.Custom;
using Sigesoft.Common;
using System.Data.SqlClient;

namespace NetPdf
{
    public class LiquidacionParticularesHospDetalle
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateLiquidacionParticulares(List<ServiciosDetalle> servicios, personDto dataPacient, organizationDto dataOrganization, organizationDto dataAseguradora, List<HospitalizacionCustom> dataHospitalizacion, DatosSeguuro dataSeguro, List<MeidicinasTicketsLista> tickets, List<RecetasDetalle> receta, string pathFile, List<HospitalizacionHabitacionList> camas, hospitalizacionhabitacionDto hospitHabit, hospitalizacionDto DataHosp, int validador)
        {

            //iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 30f, 30f, 15f, 41f);
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 30f, 30f, 90f, 65f);

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
            //document.Add(new Paragraph("\r\n"));
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
            string paciente = "- - -", titular = "- - -", DocNumber = "- - -", telefono = "- - -", direccion = "- - -", ocupacion = "- - -";
            if (dataPacient != null)
            {
                paciente = ": " + dataPacient.v_FirstLastName + " " + dataPacient.v_SecondLastName + ", " +
                           dataPacient.v_FirstName;

                titular = string.IsNullOrEmpty(dataPacient.v_OwnerName) ? ": - - -" : ": " + dataPacient.v_OwnerName.Split('|')[0];
                DocNumber = string.IsNullOrEmpty(dataPacient.v_DocNumber) ? ": - - -" : ": CSL-" + dataPacient.v_DocNumber;
                telefono = string.IsNullOrEmpty(dataPacient.v_TelephoneNumber) ? ": - - -" : ": " + dataPacient.v_TelephoneNumber;
                direccion = string.IsNullOrEmpty(dataPacient.v_AdressLocation) ? ": - - -" : ": " + dataPacient.v_AdressLocation;
                ocupacion = string.IsNullOrEmpty(dataPacient.v_CurrentOccupation) ? ": - - -" : ": " + dataPacient.v_CurrentOccupation;
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
            if (DataHosp != null)
            {
                texto_Alta = "FECHA DE ALTA";
                FechAlta = DataHosp.d_FechaAlta.ToString().Split(' ')[0];
            }
            double constoUnitaro = 0.00;
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
            
            string n_habitac = "";
            if (hospitHabit.i_HabitacionId == 1) n_habitac = "201";
            else if (hospitHabit.i_HabitacionId == 2) n_habitac = "202";
            else if (hospitHabit.i_HabitacionId == 3) n_habitac = "203";
            else if (hospitHabit.i_HabitacionId == 4) n_habitac = "204";
            else if (hospitHabit.i_HabitacionId == 5) n_habitac = "205";
            else if (hospitHabit.i_HabitacionId == 6) n_habitac = "206";
            else if (hospitHabit.i_HabitacionId == 7) n_habitac = "207";
            else if (hospitHabit.i_HabitacionId == 8) n_habitac = "208";
            else if (hospitHabit.i_HabitacionId == 9) n_habitac = "301";
            else if (hospitHabit.i_HabitacionId == 10) n_habitac = "302";
            else if (hospitHabit.i_HabitacionId == 11) n_habitac = "303";
            else if (hospitHabit.i_HabitacionId == 12) n_habitac = "304";
            else if (hospitHabit.i_HabitacionId == 13) n_habitac = "305";
            else if (hospitHabit.i_HabitacionId == 14) n_habitac = "306";
            else if (hospitHabit.i_HabitacionId == 15) n_habitac = "307";
            else if (hospitHabit.i_HabitacionId == 16) n_habitac = "308";

            string cadena_Cabecera = "";
            if (validador == 2)
            {
                cadena_Cabecera = " HOSPITALARIA - PACIENTE";
            }
            else if (validador == 1)
            {
                cadena_Cabecera = " HOSPITALARIA - MEDICO";
            }
            else
            {
                cadena_Cabecera = " HOSPITALARIA GENERAL";
            }
            var cellsTit = new List<PdfPCell>()
            { 
                
                new PdfPCell(new Phrase("LIQUIDACIÓN " + cadena_Cabecera, fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("Cajamarca, " + DateTime.Now.ToShortDateString() + " \n" + DateTime.Now.ToShortTimeString(), fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, BorderColor = BaseColor.WHITE},
            };
            columnWidths = new float[] { 50f, 50f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);

            #region CABECERA
            cells = new List<PdfPCell>()
            {           
                new PdfPCell(new Phrase("PACIENTE", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(paciente, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(textoDiasHab, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(Dias.ToString(), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("Cama ", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": " + n_habitac, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("H.C", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(DocNumber, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("TITULAR", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(titular, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("SOLC. / CART. GAR.", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": " + Solicitud, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase(Plan == "- - -"? "OCUPACION: ":"CENTRO DE TRABAJO: ", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(Plan == "- - -"? ocupacion : orgName, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("TELEFONO", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(Plan == "- - -"? telefono : orgPhone, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase("DIRECCIÓN", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(Plan == "- - -"? direccion : orgAdress, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("PLAN", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": " + Plan, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase("FECHA DE INGRESO", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(FechIngreso, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("FACTOR: ", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(Plan == "- - -" ? ": - - -": ": " + Factor.ToString() , fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("DESCUENTO PPS: ", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(Plan == "- - -" ? ": - - -": ": " +  Descuento_PPS + "%", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                
                new PdfPCell(new Phrase(texto_Alta, fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": "+ FechAlta, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("DEDUCIBLE: ", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(Plan == "- - -" ? ": - - -": ": S/. " + Deducible, fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase("COASEGURO", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(Plan == "- - -" ? ": - - -": ": " + Coaseguro.ToString() + "%", fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},

                new PdfPCell(new Phrase("MEDICO A CARGO", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
                new PdfPCell(new Phrase(": " + Medico, fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f, BorderColor = BaseColor.WHITE},
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
            decimal TotalServicios = 0;
            decimal precioConsulta = 0;
            decimal totalTickets = 0;
            int serviciosC1 = 0;
            int serviciosC2 = 0;
            
            if (validador == 1)
            {
                #region SERVICIOS DINÁMICO
                foreach (var item in servicios)
                {
                    var ListaServicios_1 = item.Lista1.FindAll(p => p.i_conCargoA == 1);
                    if (ListaServicios_1 != null)
                    {
                        serviciosC1 = ListaServicios_1.Count();
                    }
                    if (item.Lista2 != null)
                    {
                        foreach (var item1 in item.Lista2)
                        {
                            var ListaServicios_2 = item1.Lista2.FindAll(p => p.i_conCargoA == 1);
                            if (ListaServicios_2 != null)
                            {
                                serviciosC2 = ListaServicios_2.Count();
                            }
                        }
                    }
                } 
                cells = new List<PdfPCell>();

                if (servicios != null)
                {
                    double precio = 0.00;
                    //var ListaServicios = servicios.FindAll(p => p.i_conCargoA == 1);
                    foreach (var item in servicios)
                    {
                        var ListaServicios = item.Lista1.FindAll(p => p.i_conCargoA == 1);
                        decimal servicioTotal = 0;
                        if (ListaServicios != null && ListaServicios.Count() != 0)
                        {
                            
                            int contador_ = 1;
                            cell = new PdfPCell(new Phrase(contKindOfService.ToString() + ") " + item.Tipo, fontColumnValueBold2)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                            cells.Add(cell);

                            if (item.Lista1.Count >= 1 && item.Lista2 == null)
                            {
                                int conteo = 1;
                                foreach (var item1 in ListaServicios)
                                {
                                    precio = Math.Round(double.Parse(item1.Precio.ToString()) / 1.18, 2);
                                    double pUni = precio / (item1.Cantidad == null ? 1.00 : double.Parse(item1.Cantidad.ToString()));
                                    if (contador_ == 1)
                                    {
                                        precioConsulta = decimal.Parse(precio.ToString());
                                    }
                                    cell = new PdfPCell(new Phrase("     " + conteo.ToString() + ". [" + item1.Segus + "] - " + item1.NombreComponente, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(item1.Cantidad == null ? "1" : item1.Cantidad.ToString("N0"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(pUni.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(precio.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    conteo++;
                                    totalImporte += decimal.Parse(precio.ToString());
                                    servicioTotal += decimal.Parse(precio.ToString());
                                    contador_++;
                                }
                            }
                        }
                        else if (item.Lista2 != null && item.Lista2.Count >= 1 && item.Lista1.Count == 0)
                        {
                            int conteo = 1;
                            foreach (var item1 in item.Lista2)
                            {
                                if (serviciosC2 >= 1)
                                {
                                    cell = new PdfPCell(new Phrase(contKindOfService.ToString() + ") " + item.Tipo, fontColumnValueBold2)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                }
                               
                                var ListaServicios2 = item1.Lista2.FindAll(p => p.i_conCargoA == 1);
                                if (ListaServicios2 != null && ListaServicios2.Count() != 0)
                                {
                                    cell = new PdfPCell(new Phrase("     " + item1.Grupo, fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);

                                    foreach (var item2 in ListaServicios2)
                                    {
                                        precio = Math.Round(double.Parse(item2.Precio.ToString()) / 1.18, 2);
                                        double pUni = precio / (item1.Cantidad == null ? 1.00 : double.Parse(item1.Cantidad.ToString()));

                                        cell = new PdfPCell(new Phrase("          " + conteo.ToString() + ". [" + item2.Segus + "] - " + item2.NombreComponente, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                        cells.Add(cell);
                                        cell = new PdfPCell(new Phrase(item2.Cantidad == null ? "1" : item2.Cantidad.ToString("N0"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                        cells.Add(cell);
                                        cell = new PdfPCell(new Phrase(pUni.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                        cells.Add(cell);
                                        cell = new PdfPCell(new Phrase(precio.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                        cells.Add(cell);
                                        conteo++;
                                        totalImporte += decimal.Parse(precio.ToString());
                                        servicioTotal += decimal.Parse(precio.ToString());
                                    }
                                }
                            }
                        }
                        //if (serviciosC1>=1)
                        if (servicioTotal != 0)
                        {
                            cell = new PdfPCell(new Phrase("TOTAL : " + item.Tipo, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                            cells.Add(cell);
                            cell = new PdfPCell(new Phrase(servicioTotal.ToString("N2"), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK };
                            cells.Add(cell);
                            TotalServicios += servicioTotal;
                            
                        }
                        //if (serviciosC2>=1)
                        //{
                        //     cell = new PdfPCell(new Phrase("TOTAL : " + item.Tipo, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                        //    cells.Add(cell);
                        //    cell = new PdfPCell(new Phrase(servicioTotal.ToString("N2"), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK };
                        //    cells.Add(cell);
                        //    TotalServicios += servicioTotal;
                        //}
                        
                        contKindOfService++;

                    }
                    columnWidths = new float[] { 55f, 15f, 15f, 15f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                    document.Add(filiationWorker);
                }
                #endregion

                #region Tickets
                cells = new List<PdfPCell>();
                int cuentatickets = 0;
                int CuentaCantidad = 0;
                foreach (var item in tickets)
                {
                    var listaTickets = item.Lista_1.FindAll(p => p.i_conCargoA == 1);
                    if (listaTickets != null)
                    {
                        cuentatickets = listaTickets.Count();
                    }
                    CuentaCantidad += cuentatickets;
                }
                if (CuentaCantidad >= 1)
                {
                    int validatickets = 0;
                    cell = new PdfPCell(new Phrase(contKindOfService.ToString() + ")  FARMACIA HOSPITALARIA", fontColumnValueBold2)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                    cells.Add(cell);
                    double precioUnitario = 0.00;
                    double precioVenta = 0.00;
                    
                    foreach (var item in tickets)
                    {
                        decimal TickrtTotal = 0;

                        var listaTickets = item.Lista_1.FindAll(p => p.i_conCargoA == 1);
                        if (listaTickets != null && listaTickets.Count() != 0)
                        {
                            validatickets = 1;
                            cell = new PdfPCell(new Phrase(item.Tipo, fontColumnValueBold2)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                            cells.Add(cell);
                            int conteo = 1;
                            decimal sumatickets = 0;
                            foreach (var item1 in listaTickets)
                            {
                                cell = new PdfPCell(new Phrase("     " + item1.Ticket + " " + item1.Fecha.ToString().Split(' ')[0], fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);
                                decimal tick = 0;
                                foreach (var item2 in item1.Lista_2)
                                {
                                    precioVenta = double.Parse(item2.P_Venta.ToString()) / 1.18;
                                    //precioUnitario = precioVenta / item2.Cantidad;
                                    precioUnitario = (double.Parse(item2.P_Unitario.ToString()) / 1.18);
                                    cell = new PdfPCell(new Phrase("          " + conteo.ToString() + ". " + item2.Medicina, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(item2.Cantidad.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(precioUnitario.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(precioVenta.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    conteo++;
                                    totalImporte += decimal.Parse(precioVenta.ToString());
                                    TickrtTotal += decimal.Parse(precioVenta.ToString());
                                    tick += decimal.Parse(precioVenta.ToString());
                                }
                                cell = new PdfPCell(new Phrase("TOTAL : " + item1.Ticket, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                                cells.Add(cell);
                                cell = new PdfPCell(new Phrase(tick.ToString("N2"), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK };
                                cells.Add(cell);
                            }
                            //if (validatickets == 1)
                            //{
                                cell = new PdfPCell(new Phrase("TOTAL : " + item.Tipo, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                                cells.Add(cell);
                                cell = new PdfPCell(new Phrase(TickrtTotal.ToString("N2"), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK };
                                cells.Add(cell);

                            //}
                        }

                        
                        
                        //contKindOfService++;
                        totalTickets += TickrtTotal;

                    }
                    if (validatickets == 1)
                    {
                        cell = new PdfPCell(new Phrase("TOTAL :  FARMACIA HOSPITALARIA ", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase(totalTickets.ToString("N2"), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK };
                        cells.Add(cell);


                        columnWidths = new float[] { 55f, 15f, 15f, 15f };

                        filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                        document.Add(filiationWorker);
                    }
                    contKindOfService++;
                }
                #endregion
            }
            if (validador == 2)
            {
                #region SERVICIOS DINÁMICO
                foreach (var item in servicios)
                {
                    var ListaServicios_1 = item.Lista1.FindAll(p => p.i_conCargoA == 2);
                    if (ListaServicios_1 != null)
                    {
                        serviciosC1 = ListaServicios_1.Count();
                    }
                    if (item.Lista2 != null)
                    {
                        foreach (var item1 in item.Lista2)
                        {
                            var ListaServicios_2 = item1.Lista2.FindAll(p => p.i_conCargoA == 2);
                            if (ListaServicios_2 != null)
                            {
                                serviciosC2 = ListaServicios_2.Count();
                            }
                        }
                    }
                }
                cells = new List<PdfPCell>();

                if (servicios != null)
                {
                    double precio = 0.00;
                    //var ListaServicios = servicios.FindAll(p => p.i_conCargoA == 1);
                    foreach (var item in servicios)
                    {
                        var ListaServicios = item.Lista1.FindAll(p => p.i_conCargoA == 2);
                        decimal servicioTotal = 0;
                        if (ListaServicios != null && ListaServicios.Count() != 0)
                        {

                            int contador_ = 1;
                            cell = new PdfPCell(new Phrase(contKindOfService.ToString() + ") " + item.Tipo, fontColumnValueBold2)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                            cells.Add(cell);

                            if (item.Lista1.Count >= 1 && item.Lista2 == null)
                            {
                                int conteo = 1;
                                foreach (var item1 in ListaServicios)
                                {
                                    precio = Math.Round(double.Parse(item1.Precio.ToString()) / 1.18, 2);
                                    double pUni = precio / (item1.Cantidad == null ? 1.00 : double.Parse(item1.Cantidad.ToString()));
                                    if (contador_ == 1)
                                    {
                                        precioConsulta = decimal.Parse(precio.ToString());
                                    }
                                    cell = new PdfPCell(new Phrase("     " + conteo.ToString() + ". [" + item1.Segus + "] - " + item1.NombreComponente, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(item1.Cantidad == null ? "1" : item1.Cantidad.ToString("N0"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(pUni.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(precio.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    conteo++;
                                    totalImporte += decimal.Parse(precio.ToString());
                                    servicioTotal += decimal.Parse(precio.ToString());
                                    contador_++;
                                }
                            }
                        }
                        else if (item.Lista2 != null && item.Lista1.Count == 0)
                        {
                            int conteo = 1;
                            foreach (var item1 in item.Lista2)
                            {
                                if (serviciosC2 >= 1)
                                {
                                    cell = new PdfPCell(new Phrase(contKindOfService.ToString() + ") " + item.Tipo, fontColumnValueBold2)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                }

                                var ListaServicios2 = item1.Lista2.FindAll(p => p.i_conCargoA == 2);
                                if (ListaServicios2 != null && ListaServicios2.Count() != 0)
                                {
                                    cell = new PdfPCell(new Phrase("     " + item1.Grupo, fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);

                                    foreach (var item2 in ListaServicios2)
                                    {
                                        precio = Math.Round(double.Parse(item2.Precio.ToString()) / 1.18, 2);
                                        double pUni = precio / (item2.Cantidad == null ? 1.00 : double.Parse(item2.Cantidad.ToString()));

                                        cell = new PdfPCell(new Phrase("          " + conteo.ToString() + ". [" + item2.Segus + "] - " + item2.NombreComponente, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                        cells.Add(cell);
                                        cell = new PdfPCell(new Phrase(item2.Cantidad == null ? "1" : item2.Cantidad.ToString("N0"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                        cells.Add(cell);
                                        cell = new PdfPCell(new Phrase(pUni.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                        cells.Add(cell);
                                        cell = new PdfPCell(new Phrase(precio.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                        cells.Add(cell);
                                        conteo++;
                                        totalImporte += decimal.Parse(precio.ToString());
                                        servicioTotal += decimal.Parse(precio.ToString());
                                    }
                                }
                            }
                        }
                        if (serviciosC1 >= 1)
                        {
                            cell = new PdfPCell(new Phrase("TOTAL : " + item.Tipo, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                            cells.Add(cell);
                            cell = new PdfPCell(new Phrase(servicioTotal.ToString("N2"), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK };
                            cells.Add(cell);
                            TotalServicios += servicioTotal;

                        }
                        if (serviciosC2 >= 1)
                        {
                            cell = new PdfPCell(new Phrase("TOTAL : " + item.Tipo, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                            cells.Add(cell);
                            cell = new PdfPCell(new Phrase(servicioTotal.ToString("N2"), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK };
                            cells.Add(cell);
                            TotalServicios += servicioTotal;
                        }

                        contKindOfService++;

                    }
                    columnWidths = new float[] { 55f, 15f, 15f, 15f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                    document.Add(filiationWorker);
                }
                #endregion

                #region Tickets
                cells = new List<PdfPCell>();
                int cuentatickets = 0;
                int cuentaCantidad = 0;
                foreach (var item in tickets)
                {
                    var listaTickets = item.Lista_1.FindAll(p => p.i_conCargoA == 2);
                    if (listaTickets != null)
                    {
                        cuentatickets = listaTickets.Count();
                    }
                    cuentaCantidad += cuentatickets;
                }
                if (cuentaCantidad >= 1)
                {
                    int validatickets = 0;
                    cell = new PdfPCell(new Phrase(contKindOfService.ToString() + ")  FARMACIA HOSPITALARIA", fontColumnValueBold2)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                    cells.Add(cell);
                    double precioUnitario = 0.00;
                    double precioVenta = 0.00;

                    foreach (var item in tickets)
                    {
                        decimal TickrtTotal = 0;

                        var listaTickets = item.Lista_1.FindAll(p => p.i_conCargoA == 2);
                        if (listaTickets != null && listaTickets.Count() != 0)
                        {
                            validatickets = 1;
                            cell = new PdfPCell(new Phrase(item.Tipo, fontColumnValueBold2)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                            cells.Add(cell);
                            int conteo = 1;
                            decimal sumatickets = 0;
                            foreach (var item1 in listaTickets)
                            {
                                cell = new PdfPCell(new Phrase("     " + item1.Ticket + " " + item1.Fecha.ToString().Split(' ')[0], fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);
                                decimal tick = 0;
                                foreach (var item2 in item1.Lista_2)
                                {
                                    precioVenta = double.Parse(item2.P_Venta.ToString()) / 1.18;
                                    //precioUnitario = precioVenta / item2.Cantidad;
                                    precioUnitario = (double.Parse(item2.P_Unitario.ToString()) / 1.18);
                                    cell = new PdfPCell(new Phrase("          " + conteo.ToString() + ". " + item2.Medicina, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(item2.Cantidad.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(precioUnitario.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(precioVenta.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    conteo++;
                                    totalImporte += decimal.Parse(precioVenta.ToString());
                                    TickrtTotal += decimal.Parse(precioVenta.ToString());
                                    tick += decimal.Parse(precioVenta.ToString());
                                }
                                cell = new PdfPCell(new Phrase("TOTAL : " + item1.Ticket, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                                cells.Add(cell);
                                cell = new PdfPCell(new Phrase(tick.ToString("N2"), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK };
                                cells.Add(cell);
                            }

                            //if (validatickets == 1)
                            //{
                                cell = new PdfPCell(new Phrase("TOTAL : " + item.Tipo, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                                cells.Add(cell);
                                cell = new PdfPCell(new Phrase(TickrtTotal.ToString("N2"), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK };
                                cells.Add(cell);

                            //}

                        }


                        //contKindOfService++;
                        totalTickets += TickrtTotal;

                    }
                    if (validatickets == 1)
                    {
                        cell = new PdfPCell(new Phrase("TOTAL :  FARMACIA HOSPITALARIA ", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase(totalTickets.ToString("N2"), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK };
                        cells.Add(cell);


                        columnWidths = new float[] { 55f, 15f, 15f, 15f };

                        filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                        document.Add(filiationWorker);
                    }
                    contKindOfService++;
                }
                #endregion
            }
            else if (validador == 3)
            {
                #region SERVICIOS DINÁMICO
                cells = new List<PdfPCell>();

                if (servicios != null)
                {
                    double precio = 0.00;
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
                                precio = Math.Round(double.Parse(item1.Precio.ToString()) / 1.18, 2);
                                if (contador_ == 1)
                                {
                                    precioConsulta = decimal.Parse(precio.ToString());
                                }
                                cell = new PdfPCell(new Phrase("     " + conteo.ToString() + ". [" + item1.Segus + "] - " + item1.NombreComponente, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);
                                cell = new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);
                                cell = new PdfPCell(new Phrase(precio.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);
                                cell = new PdfPCell(new Phrase(precio.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);
                                conteo++;
                                totalImporte += decimal.Parse(precio.ToString());
                                servicioTotal += decimal.Parse(precio.ToString());
                                contador_++;
                            }
                        }
                        else if (item.Lista2.Count >= 1 && item.Lista1.Count == 0)
                        {
                            int conteo = 1;
                            foreach (var item1 in item.Lista2)
                            {
                                cell = new PdfPCell(new Phrase("     " + item1.Grupo, fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);

                                foreach (var item2 in item1.Lista2)
                                {
                                    precio = Math.Round(double.Parse(item2.Precio.ToString()) / 1.18, 2);
                                    cell = new PdfPCell(new Phrase("          " + conteo.ToString() + ". [" + item2.Segus + "] - " + item2.NombreComponente, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(precio.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    cell = new PdfPCell(new Phrase(precio.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                    cells.Add(cell);
                                    conteo++;
                                    totalImporte += decimal.Parse(precio.ToString());
                                    servicioTotal += decimal.Parse(precio.ToString());
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
                if (tickets != null && tickets.Count >= 1)
                {
                    cell = new PdfPCell(new Phrase(contKindOfService.ToString() + ")  FARMACIA HOSPITALARIA", fontColumnValueBold2)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                    cells.Add(cell);
                    double precioUnitario = 0.00;
                    double precioVenta = 0.00;

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
                                precioVenta = double.Parse(item2.P_Venta.ToString()) / 1.18;
                                //precioUnitario = precioVenta / item2.Cantidad;
                                precioUnitario = (double.Parse(item2.P_Unitario.ToString()) / 1.18);
                                cell = new PdfPCell(new Phrase("          " + conteo.ToString() + ". " + item2.Medicina, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);
                                cell = new PdfPCell(new Phrase(item2.Cantidad.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);
                                cell = new PdfPCell(new Phrase(precioUnitario.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);
                                cell = new PdfPCell(new Phrase(precioVenta.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                                cells.Add(cell);
                                conteo++;
                                totalImporte += decimal.Parse(precioVenta.ToString());
                                TickrtTotal += decimal.Parse(precioVenta.ToString());
                                tick += decimal.Parse(precioVenta.ToString());
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

                        contKindOfService++;
                        totalTickets += TickrtTotal;

                    }

                    cell = new PdfPCell(new Phrase("TOTAL :  FARMACIA HOSPITALARIA ", fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(totalTickets.ToString("N2"), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK };
                    cells.Add(cell);


                    columnWidths = new float[] { 55f, 15f, 15f, 15f };

                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                    document.Add(filiationWorker);
                }
                #endregion
            }
            

            #region Receta
            cells = new List<PdfPCell>();
            decimal totalReceta = 0;
            if (receta != null && receta.Count >= 1)
            {
                cell = new PdfPCell(new Phrase(contKindOfService.ToString() + ")  FARMACIA x TTO", fontColumnValueBold2)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                cells.Add(cell);
                double precioUnitario = 0.00;
                double precioVenta = 0.00;

                foreach (var item in receta)
                {
                    decimal servicioTotal = 0;

                    int conteo = 1;
                    foreach (var item1 in item.Lista)
                    {
                        precioVenta = double.Parse(item1.Total.ToString()) / 1.18;
                        precioUnitario = (double.Parse(item1.P_Unitario.ToString()) / 1.18);
                        cell = new PdfPCell(new Phrase("   " + conteo.ToString() + ". " + item1.Medicina, fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase(item1.Cantidad.ToString(), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase(precioUnitario.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase(precioVenta.ToString("N2"), fontColumnValue)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE };
                        cells.Add(cell);
                        conteo++;
                        totalImporte += decimal.Parse(precioVenta.ToString());
                        servicioTotal += decimal.Parse(precioVenta.ToString());
                    }
                    contKindOfService++;
                    totalReceta = servicioTotal;
                }

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
            if (validador == 1)
            {
                #region 
                if (camas.Count > 0)
                {
                    var listahabitaciones = camas.FindAll(p => p.i_conCargoA == 1);
                    if (listahabitaciones != null && listahabitaciones.Count() >= 1)
                    {
                        cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase(contKindOfService.ToString() + ") HABITACIONES" , fontColumnValueBold2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                        };

                        columnWidths = new float[] { 100f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                        contKindOfService++;

                        foreach (var item in listahabitaciones)
                        {


                            DateTime inicio = item.d_StartDate.Value.Date;
                            DateTime fin;

                            if (item.d_EndDate != null || item.d_EndDate.ToString() == "00/00/0000 0:0:0")
                            {
                                fin = item.d_EndDate.Value.Date;
                            }
                            else
                            {
                                fin = DateTime.Now.Date;

                            }

                            TimeSpan nDias = fin - inicio;

                            int tSpan = nDias.Days;

                            //+ 1
                            int dias = 0;
                            if (tSpan == 0)
                            {
                                dias = tSpan + 1;
                            }
                            else
                            {
                                dias = tSpan;
                            }

                            decimal importe = 0;
                            importe = decimal.Parse(item.d_Precio.ToString());
                            totalImporte += importe;
                            double costoUnitsinIgv = (double)item.d_Precio / 1.18;

                            cells = new List<PdfPCell>()
                            {
                                new PdfPCell(new Phrase("Habitación " + item.NroHabitacion + " Del " + item.d_StartDate.ToString().Split(' ')[0] + " AL " + item.d_EndDate.ToString().Split(' ')[0], fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(dias.ToString(), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(costoUnitsinIgv.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase((costoUnitsinIgv*dias).ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            };

                            columnWidths = new float[] { 55f, 15f, 15f, 15f };
                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                            document.Add(table);

                            InsertDays = true;
                            habitacion = decimal.Parse(costoUnitsinIgv.ToString()) * decimal.Parse(dias.ToString());
                        }
                    }
                }
                #endregion
            }
            else if (validador == 2)
            {
                #region
                if (camas.Count > 0)
                {
                    var listahabitaciones = camas.FindAll(p => p.i_conCargoA == 2);
                    if (listahabitaciones != null && listahabitaciones.Count() >= 1)
                    {
                        cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase(contKindOfService.ToString() + ") HABITACIONES" , fontColumnValueBold2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                        };

                        columnWidths = new float[] { 100f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                        contKindOfService++;

                        foreach (var item in listahabitaciones)
                        {


                            DateTime inicio = item.d_StartDate.Value.Date;
                            DateTime fin;

                            if (item.d_EndDate != null || item.d_EndDate.ToString() == "00/00/0000 0:0:0")
                            {
                                fin = item.d_EndDate.Value.Date;
                            }
                            else
                            {
                                fin = DateTime.Now.Date;

                            }

                            TimeSpan nDias = fin - inicio;

                            int tSpan = nDias.Days;

                            //+ 1
                            int dias = 0;
                            if (tSpan == 0)
                            {
                                dias = tSpan + 1;
                            }
                            else
                            {
                                dias = tSpan;
                            }

                            decimal importe = 0;
                            importe = decimal.Parse(item.d_Precio.ToString());
                            totalImporte += importe;
                            double costoUnitsinIgv = (double)item.d_Precio / 1.18;

                            cells = new List<PdfPCell>()
                            {
                                new PdfPCell(new Phrase("Habitación " + item.NroHabitacion + " Del " + item.d_StartDate.ToString().Split(' ')[0] + " AL " + item.d_EndDate.ToString().Split(' ')[0], fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(dias.ToString(), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(costoUnitsinIgv.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase((costoUnitsinIgv*dias).ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            };

                            columnWidths = new float[] { 55f, 15f, 15f, 15f };
                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                            document.Add(table);

                            InsertDays = true;
                            habitacion = decimal.Parse(costoUnitsinIgv.ToString()) * decimal.Parse(dias.ToString());
                        }
                    }
                }
                #endregion
            }
            if (validador == 3)
            {
                if (camas.Count > 0)
                {
                    var listahabitaciones = camas.FindAll(p => p.i_isdelete == 0);
                    if (listahabitaciones != null && listahabitaciones.Count() >= 1)
                    {
                        cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase(contKindOfService.ToString() + ") HABITACIONES" , fontColumnValueBold2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                        };

                        columnWidths = new float[] { 100f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                        contKindOfService++;

                        foreach (var item in listahabitaciones)
                        {


                            DateTime inicio = item.d_StartDate.Value.Date;
                            DateTime fin;

                            if (item.d_EndDate != null || item.d_EndDate.ToString() == "00/00/0000 0:0:0")
                            {
                                fin = item.d_EndDate.Value.Date;
                            }
                            else
                            {
                                fin = DateTime.Now.Date;

                            }

                            TimeSpan nDias = fin - inicio;

                            int tSpan = nDias.Days;

                            //+ 1
                            int dias = 0;
                            if (tSpan == 0)
                            {
                                dias = tSpan + 1;
                            }
                            else
                            {
                                dias = tSpan;
                            }

                            decimal importe = 0;
                            importe = decimal.Parse(item.d_Precio.ToString());
                            totalImporte += importe;
                            double costoUnitsinIgv = (double)item.d_Precio / 1.18;

                            cells = new List<PdfPCell>()
                            {
                                new PdfPCell(new Phrase("Habitación " + item.NroHabitacion + " Del " + item.d_StartDate.ToString().Split(' ')[0] + " AL " + item.d_EndDate.ToString().Split(' ')[0], fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(dias.ToString(), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase(costoUnitsinIgv.ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                                new PdfPCell(new Phrase((costoUnitsinIgv*dias).ToString("N2"), fontColumnValue)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                            };

                            columnWidths = new float[] { 55f, 15f, 15f, 15f };
                            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                            document.Add(table);

                            InsertDays = true;
                            habitacion = decimal.Parse(costoUnitsinIgv.ToString()) * decimal.Parse(dias.ToString());
                        }
                    }
                }
            }



            //
            //Alfinal de todo el foreach
            string igv = "1.18";

            decimal total = (habitacion + totalReceta + totalTickets + TotalServicios) * decimal.Parse(igv);
            
            if (total == 0)
            {
                total = totalImporte * decimal.Parse(igv);
            }

            decimal totalSinConsulta = precioConsulta;

            decimal subTotalGeneral = total / decimal.Parse(igv);
            decimal IgvGeneral = total - subTotalGeneral;
            decimal totalGeneral = subTotalGeneral + IgvGeneral;
            subTotalGeneral = Math.Round(subTotalGeneral, 2);
            IgvGeneral = Math.Round(IgvGeneral, 2);
            totalGeneral = Math.Round(totalGeneral, 2);
            //string totalGeneralPalabra = Utils.enletras(totalGeneral.ToString());
            string totalGeneralPalabra = Utils.enletras(totalGeneral.ToString());

            //NUEVOS CALCULOS
            decimal deducible_ = Deducible;
            decimal coaseguro_ = Coaseguro;
            decimal TotalParcial = 0;

            decimal _coaseguroGr = 0;
            if (Deducible != 0)
            {
                TotalParcial = decimal.Parse(total.ToString()) - (precioConsulta * decimal.Parse("1.18"));
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
                _coaseguroGr = decimal.Parse(total.ToString()) * (coaseguro_ / 100);
            }
            else
            {
                _coaseguroGr = decimal.Parse(TotalParcial.ToString()) * (coaseguro_ / 100);
            }
            decimal TotalFinal = decimal.Parse(total.ToString()) - (_coaseguroGr + deducible_);
            if (Plan == "- - -")
            {
                cells = new List<PdfPCell>()
                    {
                    new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 7f, UseVariableBorders = true, BorderColorTop = BaseColor.BLACK,  BorderColorBottom = BaseColor.WHITE, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE},

                    new PdfPCell(new Phrase(totalGeneralPalabra, fontColumnValue2)) {Colspan = 1, Rowspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase("SUB TOTAL:", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(subTotalGeneral.ToString("N2"), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    new PdfPCell(new Phrase("I.G.V. 18% :", fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(IgvGeneral.ToString("N2"), fontColumnValue2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},

                    new PdfPCell(new Phrase("TOTAL GENERAL :", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                    new PdfPCell(new Phrase(total.ToString("N2"), fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, BorderColor = BaseColor.WHITE},
                };
                columnWidths = new float[] { 40f, 45f, 15f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);
            }
            else
            {
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

            }

            #endregion

            document.Close();
            writer.Close();
            writer.Dispose();
            RunFile(pathFile);

        }

    }
}
