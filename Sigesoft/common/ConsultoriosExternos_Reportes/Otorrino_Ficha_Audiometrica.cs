using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BE.Custom;

namespace NetPdf
{
    public class Otorrino_Ficha_Audiometrica
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateOtorrino_Ficha_Audiometrica(string filePDF,
            DatosDoctorMedicina medico,
            PacientList datosPac,
            organizationDto infoEmpresaPropietaria,
            List<ServiceComponentList> exams,
            List<DiagnosticRepositoryList> Diagnosticos,
            MedicoTratanteAtenciones medicoo,
            UsuarioGrabo DatosGrabo, List<Categoria> DataSource, string edadActual,
            List<AudiometriaUserControlList> AudiometriaValores)
        {
            //Document document = new Document(PageSize.A4, 30f, 30f, 45f, 41f);
            Document document = new Document(PageSize.A4, 30f, 30f, 70f, 58f);


            document.SetPageSize(iTextSharp.text.PageSize.A4);

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
            pdfPage_NEW page = new pdfPage_NEW();
            writer.PageEvent = page;
            page.Title = string.Empty;
            document.Open();


            #region Declaration Tables
            var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.Gray);
            string include = string.Empty;
            List<PdfPCell> cells = null;
            List<PdfPCell> cells2 = null;
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
            PdfPCell cell2 = null;
            document.Add(new Paragraph("\r\n"));
            #endregion

            #region Fonts
            Font fontTitle1 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE, new BaseColor(System.Drawing.Color.Black));

            Font fontTitle1_1 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle1_2 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontTitle2 = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.White));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL | iTextSharp.text.Font.UNDERLINE, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValueBold_SubTitulo = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValueBold1 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));

            Font fontColumnValueBold_Dx = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE, new BaseColor(System.Drawing.Color.Blue));

            Font fontColumnValueBold_2 = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendiceRed = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Red));
            Font fontColumnValueApendiceBlue = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Blue));

            
            #endregion

            #region TÍTULO

            cells = new List<PdfPCell>();


            string[] servicio = datosPac.FechaServicio.ToString().Split(' ');
            string med = "";
            if (DatosGrabo != null)
            {
                med = DatosGrabo.Nombre;
            }
            else
            {
                med = "CLINICA SAN LORENZO";
            }


            var cellsTit = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("FICHA DE EVALUACIÓN AUDIOMÉTRICA", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, Border = PdfPCell.NO_BORDER},
               
            };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Valores
            var tamaño_celda = 13f;
            #endregion

            #region Datos del Servicio

            string fechaInforme = DateTime.Now.ToString().Split(' ')[0];
            string[] fechaNac = datosPac.d_Birthdate.ToString().Split(' ');
            ServiceComponentList evaluacionAudit = exams.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_AUDIOMETRICA_ID);

            var OTORRINO_EVALUACION_COMPAÑIA_MINERA = evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_COMPAÑIA_MINERA) == null ? "" : evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_COMPAÑIA_MINERA).v_Value1;
            var OTORRINO_EVALUACION_EMPRESA = evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_EMPRESA) == null ? "" : evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_EMPRESA).v_Value1;

            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=2f, Border = PdfPCell.NO_BORDER},
                    
                    new PdfPCell(new Phrase("FECHA: ", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                    new PdfPCell(new Phrase(": " +datosPac.FechaServicio.ToString(), fontColumnValue)) { Colspan = 6 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},

                    new PdfPCell(new Phrase("COMPAÑÍA MINERA", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                    new PdfPCell(new Phrase(": " + OTORRINO_EVALUACION_COMPAÑIA_MINERA, fontColumnValue)) { Colspan = 9 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                    new PdfPCell(new Phrase("EDAD: ", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                    new PdfPCell(new Phrase(edadActual.Split(' ')[0] + " Años", fontColumnValue)) { Colspan = 5 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                    
                    new PdfPCell(new Phrase("EMPRESA", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                    new PdfPCell(new Phrase(": " + OTORRINO_EVALUACION_EMPRESA, fontColumnValue)) { Colspan = 9 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                    new PdfPCell(new Phrase("SEXO: ", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                    new PdfPCell(new Phrase(datosPac.Genero, fontColumnValue)) { Colspan = 5 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    

                    new PdfPCell(new Phrase("APELLIDOS Y NOMBRES", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                    new PdfPCell(new Phrase(": " + datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan = 9 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                    new PdfPCell(new Phrase("CARGO: ", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                    new PdfPCell(new Phrase(datosPac.v_CurrentOccupation, fontColumnValueApendice)) { Colspan = 5 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region EVALUACION

          
            var OTORRINO_EVALUACION_RUIDO_14H = evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_RUIDO_14H) == null ? "" : evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_RUIDO_14H).v_Value1;
            var OTORRINO_EVALUACION_VIAJE_ALTURA = evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_VIAJE_ALTURA) == null ? "" : evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_VIAJE_ALTURA).v_Value1;
            var OTORRINO_EVALUACION_HORAS_DESCANSO_SINO = evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_HORAS_DESCANSO_SINO) == null ? "" : evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_HORAS_DESCANSO_SINO).v_Value1;
            var OTORRINO_EVALUACION_HORAS_DESCANSO_RES = evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_HORAS_DESCANSO_RES) == null ? "" : evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_HORAS_DESCANSO_RES).v_Value1;

            var OTORRINO_EVALUACION_INFECCIONES_AUDITIVAS = evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_INFECCIONES_AUDITIVAS) == null ? "" : evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_INFECCIONES_AUDITIVAS).v_Value1;
            var OTORRINO_EVALUACION_INFECCIONES_OROFARINGEAS = evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_INFECCIONES_OROFARINGEAS) == null ? "" : evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_INFECCIONES_OROFARINGEAS).v_Value1;
            var OTORRINO_EVALUACION_RESFRIOS = evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_RESFRIOS) == null ? "" : evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_RESFRIOS).v_Value1;
            var OTORRINO_EVALUACION_ACCIDENTE_TRAUMAT = evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_ACCIDENTE_TRAUMAT) == null ? "" : evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_ACCIDENTE_TRAUMAT).v_Value1;
            var OTORRINO_EVALUACION_MEDICAMENTOS_OTOTOXICOS = evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_MEDICAMENTOS_OTOTOXICOS) == null ? "" : evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_MEDICAMENTOS_OTOTOXICOS).v_Value1;

            var OTORRINO_EVALUACION_SORDERA = evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_SORDERA) == null ? "" : evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_SORDERA).v_Value1;
            var OTORRINO_EVALUACION_ZUMBIDO = evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_ZUMBIDO) == null ? "" : evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_ZUMBIDO).v_Value1;
            var OTORRINO_EVALUACION_VERTIGO = evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_VERTIGO) == null ? "" : evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_VERTIGO).v_Value1;
            var OTORRINO_EVALUACION_OTALGIA = evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_OTALGIA) == null ? "" : evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_OTALGIA).v_Value1;
            var OTORRINO_EVALUACION_SECRECION_OTICA = evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_SECRECION_OTICA) == null ? "" : evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_SECRECION_OTICA).v_Value1;

            var OTORRINO_EVALUACION_OTOSC_OI = evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_OTOSC_OI) == null ? "" : evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_OTOSC_OI).v_Value1;
            var OTORRINO_EVALUACION_OTOSC_OD = evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_OTOSC_OD) == null ? "" : evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_OTOSC_OD).v_Value1;

            var OTORRINO_EVALUACION_CONTROL = evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_CONTROL) == null ? "" : evaluacionAudit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_CONTROL).v_Value1;


            cellsTit = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=2f, Border = PdfPCell.NO_BORDER},

                new PdfPCell(new Phrase("EXPOSICIÓN AL RUIDO", fontColumnValueBold)) {Colspan = 3,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("SI", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("ANTECEDENTES MÉDICOS", fontColumnValueBold)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("SI", fontColumnValueBold)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("Ha estado en ambientes de ruido de las últimas 14 horas", fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_RUIDO_14H =="1"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_RUIDO_14H =="0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("  Infecciones auditivas", fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_INFECCIONES_AUDITIVAS=="1"?"X":String.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_INFECCIONES_AUDITIVAS=="0"?"X":String.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    

                new PdfPCell(new Phrase("Viajes recientes a altura", fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_VIAJE_ALTURA =="1"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_VIAJE_ALTURA =="0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("  Infecciones orofaríngeas", fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_INFECCIONES_OROFARINGEAS =="1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_INFECCIONES_OROFARINGEAS =="0"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    

                new PdfPCell(new Phrase("¿Cuántas horas ha descansado antes del examen?", fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_HORAS_DESCANSO_SINO =="1"?OTORRINO_EVALUACION_HORAS_DESCANSO_RES:string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_HORAS_DESCANSO_SINO =="1"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_HORAS_DESCANSO_SINO =="0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("  Resfríos", fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_RESFRIOS=="1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_RESFRIOS=="0"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    

                new PdfPCell(new Phrase("SÍNTOMAS ACTUALES", fontColumnValueBold)) {Colspan = 3,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("  Accidente Traumático", fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_ACCIDENTE_TRAUMAT=="1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_ACCIDENTE_TRAUMAT=="0"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    

                new PdfPCell(new Phrase("Sordera", fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_SORDERA=="1"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_SORDERA=="0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("  Uso de medicamentos ototóxicos", fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_MEDICAMENTOS_OTOTOXICOS=="1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_MEDICAMENTOS_OTOTOXICOS=="0"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    

                new PdfPCell(new Phrase("Zumbido", fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_ZUMBIDO =="1"?"X":String.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_ZUMBIDO =="0"?"X":String.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan  = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                
                new PdfPCell(new Phrase("Vértigo", fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_VERTIGO=="1"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_VERTIGO=="0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan  = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                
                new PdfPCell(new Phrase("Otalgia", fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_OTALGIA=="1"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_OTALGIA=="0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan  = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("Secreción Ótica", fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_SECRECION_OTICA=="1"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_SECRECION_OTICA=="0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan  = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=5f, Border = PdfPCell.NO_BORDER},
            };

            columnWidths = new float[] { 36f, 10f, 2f, 4f, 2f, 4f, 32f, 4f, 2f, 4f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);

            document.Add(table);


            cellsTit = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("OTOSCOPIA", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("OÍDO IZQUIERDO", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_OTOSC_OI, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                
                new PdfPCell(new Phrase("", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("OÍDO DERECHO", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_OTOSC_OD, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=5f, Border = PdfPCell.NO_BORDER},
            };

            columnWidths = new float[] { 15f,  20f, 70f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);

            document.Add(table);
            PdfPCell cellOD_1 = null;
            //PdfPCell cellOI_1 = null;
            byte[] GraficoDerecho_1 = null;
            foreach (var item in AudiometriaValores)
            {
                GraficoDerecho_1 = item.b_AudiogramaOD;
            }
            if (GraficoDerecho_1 != null)
                cellOD_1 = new PdfPCell(HandlingItextSharp.GetImage(GraficoDerecho_1, null, null, 340, 210));
            else
                cellOD_1 = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellOD_1.HorizontalAlignment = Element.ALIGN_CENTER;
            cellOD_1.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellOD_1.MinimumHeight = 190f;
            var tamaño_celda_entre_linea = 8f;
            var tamaño_celda_1 = 13f;

            cellsTit = new List<PdfPCell>()
            {          
           
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 190f , Border = PdfPCell.NO_BORDER },
                new PdfPCell(new Phrase("Decibeles (dB)", fontColumnValueApendice)){ Rotation=90, Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 190f, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(cellOD_1){ Colspan = 15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 190f, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 190f , Border = PdfPCell.NO_BORDER },

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Frecuencia (Hz)", fontColumnValueApendice)){ Colspan = 15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("AEREA OD ROJO", fontColumnValueApendice)){ Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("O", fontColumnValueApendiceRed)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("OI AZUL", fontColumnValueApendice)){ Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("X", fontColumnValueApendiceBlue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("OSEA OD ROJO", fontColumnValueApendice)){ Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("<", fontColumnValueApendiceRed)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("OI AZUL", fontColumnValueApendice)){ Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(">", fontColumnValueApendiceBlue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    


                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 2f , Border = PdfPCell.NO_BORDER  },    

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);

            document.Add(table);


            #region DIAGNOSTICOS


            var filterDiagnosticRepository = Diagnosticos.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_AUDIOMETRICA_ID);
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("", fontColumnValueBold_SubTitulo)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
                new PdfPCell(new Phrase("DIAGNÓSTICO:", fontColumnValueBold_SubTitulo)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
                new PdfPCell(new Phrase("", fontColumnValueBold_SubTitulo)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
            };
            columnWidths = new float[] { 2f, 96f, 2f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);
            cells = new List<PdfPCell>();
            if (filterDiagnosticRepository != null && filterDiagnosticRepository.Count > 0)
            {
                int Contador_Dx = 1;
                foreach (var item in filterDiagnosticRepository)
                {
                    if (item.v_DiseasesId == "N009-DD000000029")
                    {
                        cell = new PdfPCell(new Phrase("")) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Border = PdfPCell.BOTTOM_BORDER };
                        cells.Add(cell);
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase("", fontColumnValueBold_Dx)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Border = PdfPCell.NO_BORDER };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase(Contador_Dx + ". " + item.v_DiseasesName + " (" + item.v_Dx_CIE10 + ")", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, Border = PdfPCell.BOTTOM_BORDER, MinimumHeight = tamaño_celda };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase("", fontColumnValueBold_Dx)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Border = PdfPCell.NO_BORDER };
                        cells.Add(cell);
                        Contador_Dx++;

                    }
                }
                columnWidths = new float[] { 2f, 96f, 2f };
            }
            else
            {
                cell = new PdfPCell(new Phrase("", fontColumnValueBold_Dx)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Border = PdfPCell.NO_BORDER, MinimumHeight = 40f };
                cells.Add(cell);
                columnWidths = new float[] { 100f };
            }
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);




            List<ListaComun> Listacomun = new List<ListaComun>();
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("", fontColumnValueBold_SubTitulo)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
                new PdfPCell(new Phrase("RECOMENDACIONES:", fontColumnValueBold_SubTitulo)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
                new PdfPCell(new Phrase("", fontColumnValueBold_SubTitulo)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
 
            };
            columnWidths = new float[] { 2f, 96f, 2f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);
            cells = new List<PdfPCell>();
            if (filterDiagnosticRepository != null && filterDiagnosticRepository.Count > 0)
            {
                int Contador = 1;
                foreach (var item in filterDiagnosticRepository)
                {

                    ListaComun oListaComun = null;

                    foreach (var Reco in item.Recomendations)
                    {
                        oListaComun = new ListaComun();

                        oListaComun.Valor1 = Reco.v_RecommendationName;
                        oListaComun.i_Item = Contador.ToString();
                        Listacomun.Add(oListaComun);
                        Contador++;
                    }
                }
            }
            else
            {
                cell = new PdfPCell(new Phrase("", fontColumnValueBold_Dx)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Border = PdfPCell.BOTTOM_BORDER, MinimumHeight = 40f };
                cells.Add(cell);
                columnWidths = new float[] { 100f };
            }

            if (filterDiagnosticRepository != null && filterDiagnosticRepository.Count > 0)
            {
                foreach (var item in Listacomun)
                {
                    cell = new PdfPCell(new Phrase("", fontColumnValueBold_Dx)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Border = PdfPCell.NO_BORDER, MinimumHeight = tamaño_celda };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.i_Item + ". " + item.Valor1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, Border = PdfPCell.BOTTOM_BORDER, MinimumHeight = tamaño_celda };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase("", fontColumnValueBold_Dx)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Border = PdfPCell.NO_BORDER, MinimumHeight = tamaño_celda };
                    cells.Add(cell);
                }
                columnWidths = new float[] { 2f, 96f, 2f };
            }
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion

            #region FIRMA

            cellsTit = new List<PdfPCell>()
            {
                    
                new PdfPCell(new Phrase("", fontColumnValueBold))
                {
                    Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,
                    VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER
                },
                new PdfPCell(new Phrase(string.Empty, fontColumnValueBold_Dx))
                {
                    Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,
                    VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER
                },
                new PdfPCell(new Phrase("", fontColumnValueBold))
                {
                    Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,
                    VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER
                },
            };

            columnWidths = new float[] { 3f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 3f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);

            document.Add(table);


            #region Firma

            #region Creando celdas de tipo Imagen y validando nulls
            PdfPCell cellFirmaTrabajador = null;
            PdfPCell cellHuellaTrabajador = null;
            PdfPCell cellFirma = null;

            // Firma del trabajador ***************************************************

            if (datosPac.FirmaTrabajador != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(datosPac.FirmaTrabajador, null, null, 120, 50));
            else
                cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirmaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
            cellFirmaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellFirmaTrabajador.MinimumHeight = 70f;
            // Huella del trabajador **************************************************

            if (datosPac.HuellaTrabajador != null)
                cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(datosPac.HuellaTrabajador, null, null, 30, 50));
            else
                cellHuellaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellHuellaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHuellaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellHuellaTrabajador.MinimumHeight = 70f;
            // Firma del doctor Auditor **************************************************
            if (DatosGrabo != null)
            {
                if (DatosGrabo.Firma != null)
                    cellFirma = new PdfPCell(HandlingItextSharp.GetImage(DatosGrabo.Firma, null, null, 120, 50)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER };
                else
                    cellFirma = new PdfPCell(new Phrase(" ", fontColumnValue));
            }
            else
                cellFirma = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirma.HorizontalAlignment = Element.ALIGN_CENTER;
            cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellFirma.MinimumHeight = 70f;
            #endregion

            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("CONTROL: ", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(OTORRINO_EVALUACION_CONTROL, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f, Border = PdfPCell.BOTTOM_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f, Border = PdfPCell.NO_BORDER },    
            };
            columnWidths = new float[] { 10f, 20f, 70f};

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);

            document.Add(filiationWorker);



            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER},
                new PdfPCell(cellFirma){ HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.BOTTOM_BORDER},
                new PdfPCell(new Phrase("" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 30f},
                new PdfPCell(cellFirmaTrabajador){ HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.BOTTOM_BORDER},
                new PdfPCell(new Phrase("" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 30f},
                new PdfPCell(cellHuellaTrabajador){ HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                new PdfPCell(new Phrase("" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 30f},
                new PdfPCell(new Phrase("FIRMA DEL MEDICO CERTIFICADO" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_CENTER,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 30f},
                new PdfPCell(new Phrase("" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 30f},
                new PdfPCell(new Phrase("FIRMA DEL PACIENTE" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_CENTER,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 30f},
                new PdfPCell(new Phrase("" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 30f},
                new PdfPCell(new Phrase("HUELLA DIGITAL \nÍndice derecho\nDNI: " + datosPac.v_DocNumber , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_CENTER,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 30f},



            };
            columnWidths = new float[] { 18f, 30f, 2f, 30f, 2f, 18f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #endregion
            #endregion
            document.Close();
            writer.Close();
            writer.Dispose();
        }
    }
}
