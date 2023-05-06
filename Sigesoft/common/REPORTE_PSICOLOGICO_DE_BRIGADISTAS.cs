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
namespace NetPdf
{
    public class REPORTE_PSICOLOGICO_DE_BRIGADISTAS
    {
        private static void RunFile(string filePdf)
        {
            Process proceso = Process.Start(filePdf);
            proceso.WaitForExit();
            proceso.Close();
        }

        #region Reporte informe
        public static void CreateReportePsicologicoBrigadistas(PacientList filiationData, List<ServiceComponentList> serviceComponent, organizationDto infoEmpresa,
            PacientList datosPac,
            string filePDF, UsuarioGrabo DatosGrabo)
        {
            //Document document = new Document(PageSize.A4, 30f, 30f, 42f, 41f);
            Document document = new Document(PageSize.A4, 30f, 30f, 75f, 65f);

            document.SetPageSize(iTextSharp.text.PageSize.A4);

            //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
            //pdfPage page = new pdfPage();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
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
            Font fontTitle1 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue_1 = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            #endregion

            var tamaño_celda = 10f;
            #region TÍTULO

            cells = new List<PdfPCell>();

            //if (infoEmpresa.b_Image != null)
            //{
            //    iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresa.b_Image));
            //    imagenEmpresa.ScalePercent(25);
            //    imagenEmpresa.SetAbsolutePosition(40, 790);
            //    document.Add(imagenEmpresa);
            //}
            //iTextSharp.text.Image imagenMinsa = iTextSharp.text.Image.GetInstance("C:/Banner/Minsa.png");
            //imagenMinsa.ScalePercent(10);
            //imagenMinsa.SetAbsolutePosition(400, 785);
            //document.Add(imagenMinsa);
            string[] fechaServicio = datosPac.FechaServicio.ToString().Split(' ');

            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("Fecha: " + fechaServicio[0], fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                    new PdfPCell(new Phrase("Unidad: LA ZANJA", fontColumnValue)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

                    new PdfPCell(new Phrase("REPORTE PSICOLÓGICO DE BRIGADITAS", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            ServiceComponentList reporte_psicologico_brigadistas = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_ID);

            #region DATOS GENERALES
            
            string empresageneral = filiationData.empresa_;
            string empresacontrata = filiationData.contrata;
            string empresasubcontrata = filiationData.subcontrata;

            string empr_Conct = "";
            if (empresageneral != empresasubcontrata) empr_Conct = empresacontrata + " / " + empresasubcontrata;
            else empr_Conct = empresacontrata;


            cells = new List<PdfPCell>()
            {         
               
                new PdfPCell(new Phrase("NOMBRE Y APELLIDOS", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK },    
                new PdfPCell(new Phrase(":  " + datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan = 15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK },    
                
                new PdfPCell(new Phrase("OCUPACIÓN:", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(":  " + filiationData.v_CurrentOccupation, fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("EDAD", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(":  " + datosPac.Edad.ToString() + "AÑOS", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                
                new PdfPCell(new Phrase("GRADO DE INSTRUCCIÓN", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(":  " +datosPac.GradoInstruccion, fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("DNI", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(":  " + datosPac.v_DocNumber, fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                
                new PdfPCell(new Phrase("EMPRESA", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(":  " + empresageneral, fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase("TIPO DE EXAMEN: ", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(": ESPECIAL", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },    
                
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 10f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion


            #region  FACTORES
            var inteligencia_general = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_INTELIGENCIA_GENERAL) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_INTELIGENCIA_GENERAL).v_Value1;

            var orientacion_Espacial = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_ORIENTACION_ESPACIAL) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_ORIENTACION_ESPACIAL).v_Value1;
            var percepcion_atencion = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PERCEPCION_Y_ATENCION) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PERCEPCION_Y_ATENCION).v_Value1;
            var coordinacion_visomanual = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_COORDINACION_VISOMANUAL) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_COORDINACION_VISOMANUAL).v_Value1;
            var resistencia_monotomia = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_RESISTENCIA_A_LA_MONOTONIA) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_RESISTENCIA_A_LA_MONOTONIA).v_Value1;
            var precision = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PRECISION) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PRECISION).v_Value1;

            var fobia = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_FOBIA) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_FOBIA).v_Value1;
            var ansiedad = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_ANSIEDAD) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_ANSIEDAD).v_Value1;
            var estres = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_ESTRES) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_ESTRES).v_Value1;

            var paranoica = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PERSONALIDAD_PARANOICA) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PERSONALIDAD_PARANOICA).v_Value1;
            var esquizoide = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PERSONALIDAD_ESQUIZOIDE) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PERSONALIDAD_ESQUIZOIDE).v_Value1;
            var esquizolopica = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PERSONALIDAD_ESQUIZOLOPICA) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PERSONALIDAD_ESQUIZOLOPICA).v_Value1;
            var limitrofe = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PERSONALIDAD_LIMITROFE) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PERSONALIDAD_LIMITROFE).v_Value1;
            var antisocial = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PERSONALIDAD_ANTISOCIAL) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PERSONALIDAD_ANTISOCIAL).v_Value1;
            var narcisista = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PERSONALIDAD_NARCISISTA) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PERSONALIDAD_NARCISISTA).v_Value1;
            var histrionica = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PERSONALIDAD_HISTRIONICA) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PERSONALIDAD_HISTRIONICA).v_Value1;
            var dependiente = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PERSONALIDAD_DEPENDIENTE) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_PERSONALIDAD_DEPENDIENTE).v_Value1;



            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase(" - ", fontColumnValueBold)) {Rowspan=2, Colspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase(" FACTORES ", fontColumnValueBold)) {Rowspan=2, Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase(" NIVELES ", fontColumnValueBold)) {Colspan = 12,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f, BackgroundColor = BaseColor.GRAY },       

                new PdfPCell(new Phrase("BAJO", fontColumnValueBold)) {Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase("MEDIO", fontColumnValueBold)) {Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase("ALTO", fontColumnValueBold)) {Colspan = 4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f, BackgroundColor = BaseColor.GRAY },       

                new PdfPCell(new Phrase("COGNITIVO", fontColumnValueBold)) {Colspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase("Inteligencia General", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(inteligencia_general == "-1" ?"":inteligencia_general == "1" ?"X":inteligencia_general=="2"?"":inteligencia_general=="3"?"":inteligencia_general, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(inteligencia_general == "-1" ?"":inteligencia_general == "1" ?"":inteligencia_general=="2"?"X":inteligencia_general=="3"?"":inteligencia_general, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(inteligencia_general == "-1" ?"":inteligencia_general == "1" ?"":inteligencia_general=="2"?"":inteligencia_general=="3"?"X":inteligencia_general, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                
                new PdfPCell(new Phrase("HABILIDADES MOTRICES", fontColumnValueBold)) {Rowspan=5, Colspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase("Orientación Espacial", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(orientacion_Espacial == "-1" ?"":orientacion_Espacial == "1" ?"X":orientacion_Espacial=="2"?"":orientacion_Espacial=="3"?"":orientacion_Espacial, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(orientacion_Espacial == "-1" ?"":orientacion_Espacial == "1" ?"":orientacion_Espacial=="2"?"X":orientacion_Espacial=="3"?"":orientacion_Espacial, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(orientacion_Espacial == "-1" ?"":orientacion_Espacial == "1" ?"":orientacion_Espacial=="2"?"":orientacion_Espacial=="3"?"X":orientacion_Espacial, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                
                new PdfPCell(new Phrase("Percepción y Atención", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(percepcion_atencion == "-1" ?"":percepcion_atencion == "1" ?"X":percepcion_atencion=="2"?"":percepcion_atencion=="3"?"":percepcion_atencion, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(percepcion_atencion == "-1" ?"":percepcion_atencion == "1" ?"":percepcion_atencion=="2"?"X":percepcion_atencion=="3"?"":percepcion_atencion, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(percepcion_atencion == "-1" ?"":percepcion_atencion == "1" ?"":percepcion_atencion=="2"?"":percepcion_atencion=="3"?"X":percepcion_atencion, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                
                new PdfPCell(new Phrase("Coordinación Visomanual", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(coordinacion_visomanual == "-1" ?"":coordinacion_visomanual == "1" ?"X":coordinacion_visomanual=="2"?"":coordinacion_visomanual=="3"?"":coordinacion_visomanual, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(coordinacion_visomanual == "-1" ?"":coordinacion_visomanual == "1" ?"":coordinacion_visomanual=="2"?"X":coordinacion_visomanual=="3"?"":coordinacion_visomanual, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(coordinacion_visomanual == "-1" ?"":coordinacion_visomanual == "1" ?"":coordinacion_visomanual=="2"?"":coordinacion_visomanual=="3"?"X":coordinacion_visomanual, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                
                new PdfPCell(new Phrase("Resistencia a la Monotonia", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(resistencia_monotomia == "-1" ?"":resistencia_monotomia == "1" ?"X":resistencia_monotomia=="2"?"":resistencia_monotomia=="3"?"":resistencia_monotomia, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(resistencia_monotomia == "-1" ?"":resistencia_monotomia == "1" ?"":resistencia_monotomia=="2"?"X":resistencia_monotomia=="3"?"":resistencia_monotomia, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(resistencia_monotomia == "-1" ?"":resistencia_monotomia == "1" ?"":resistencia_monotomia=="2"?"":resistencia_monotomia=="3"?"X":resistencia_monotomia, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                
                new PdfPCell(new Phrase("Precisión", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(precision == "-1" ?"":precision == "1" ?"X":precision=="2"?"":precision=="3"?"":precision, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(precision == "-1" ?"":precision == "1" ?"":precision=="2"?"X":precision=="3"?"":precision, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(precision == "-1" ?"":precision == "1" ?"":precision=="2"?"":precision=="3"?"X":precision, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                
                new PdfPCell(new Phrase("ASPECTO AFECTIVO EMOCIONAL", fontColumnValueBold)) {Rowspan=3, Colspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase("Fobia", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(fobia == "-1" ?"":fobia == "1" ?"X":fobia=="2"?"":fobia=="3"?"":fobia, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(fobia == "-1" ?"":fobia == "1" ?"":fobia=="2"?"X":fobia=="3"?"":fobia, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(fobia == "-1" ?"":fobia == "1" ?"":fobia=="2"?"":fobia=="3"?"X":fobia, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                
                new PdfPCell(new Phrase("Ansiedad", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(ansiedad == "-1" ?"":ansiedad == "1" ?"X":ansiedad=="2"?"":ansiedad=="3"?"":ansiedad, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(ansiedad == "-1" ?"":ansiedad == "1" ?"":ansiedad=="2"?"X":ansiedad=="3"?"":ansiedad, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(ansiedad == "-1" ?"":ansiedad == "1" ?"":ansiedad=="2"?"":ansiedad=="3"?"X":ansiedad, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                
                new PdfPCell(new Phrase("Estrés", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(estres == "-1" ?"":estres == "1" ?"X":estres=="2"?"":estres=="3"?"":estres, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(estres == "-1" ?"":estres == "1" ?"":estres=="2"?"X":estres=="3"?"":estres, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(estres == "-1" ?"":estres == "1" ?"":estres=="2"?"":estres=="3"?"X":estres, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                
                new PdfPCell(new Phrase("PERSONALIDAD", fontColumnValueBold)) {Rowspan=8, Colspan = 3,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f, BackgroundColor = BaseColor.GRAY },       
                new PdfPCell(new Phrase("Personalidad Paranoica", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(paranoica == "-1" ?"":paranoica == "1" ?"X":paranoica=="2"?"":paranoica=="3"?"":paranoica, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(paranoica == "-1" ?"":paranoica == "1" ?"":paranoica=="2"?"X":paranoica=="3"?"":paranoica, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(paranoica == "-1" ?"":paranoica == "1" ?"":paranoica=="2"?"":paranoica=="3"?"X":paranoica, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                
                new PdfPCell(new Phrase("Personalidad Esquizoide", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(esquizoide == "-1" ?"":esquizoide == "1" ?"X":esquizoide=="2"?"":esquizoide=="3"?"":esquizoide, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(esquizoide == "-1" ?"":esquizoide == "1" ?"":esquizoide=="2"?"X":esquizoide=="3"?"":esquizoide, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(esquizoide == "-1" ?"":esquizoide == "1" ?"":esquizoide=="2"?"":esquizoide=="3"?"X":esquizoide, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                
                new PdfPCell(new Phrase("Personalidad Esquizolopica", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(esquizolopica == "-1" ?"":esquizolopica == "1" ?"X":esquizolopica=="2"?"":esquizolopica=="3"?"":esquizolopica, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(esquizolopica == "-1" ?"":esquizolopica == "1" ?"":esquizolopica=="2"?"X":esquizolopica=="3"?"":esquizolopica, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(esquizolopica == "-1" ?"":esquizolopica == "1" ?"":esquizolopica=="2"?"":esquizolopica=="3"?"X":esquizolopica, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                
                new PdfPCell(new Phrase("Personalidad Limítrofe", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(limitrofe == "-1" ?"":limitrofe == "1" ?"X":limitrofe=="2"?"":limitrofe=="3"?"":limitrofe, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(limitrofe == "-1" ?"":limitrofe == "1" ?"":limitrofe=="2"?"X":limitrofe=="3"?"":limitrofe, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(limitrofe == "-1" ?"":limitrofe == "1" ?"":limitrofe=="2"?"":limitrofe=="3"?"X":limitrofe, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                
                new PdfPCell(new Phrase("Personalidad Antisocial", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(antisocial == "-1" ?"":antisocial == "1" ?"X":antisocial=="2"?"":antisocial=="3"?"":antisocial, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(antisocial == "-1" ?"":antisocial == "1" ?"":antisocial=="2"?"X":antisocial=="3"?"":antisocial, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(antisocial == "-1" ?"":antisocial == "1" ?"":antisocial=="2"?"":antisocial=="3"?"X":antisocial, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                
                new PdfPCell(new Phrase("Personalidad Narcisista", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(narcisista == "-1" ?"":narcisista == "1" ?"X":narcisista=="2"?"":narcisista=="3"?"":narcisista, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(narcisista == "-1" ?"":narcisista == "1" ?"":narcisista=="2"?"X":narcisista=="3"?"":narcisista, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(narcisista == "-1" ?"":narcisista == "1" ?"":narcisista=="2"?"":narcisista=="3"?"X":narcisista, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                
                new PdfPCell(new Phrase("Personalidad Histrionica", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(histrionica == "-1" ?"":histrionica == "1" ?"X":histrionica=="2"?"":histrionica=="3"?"":histrionica, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(histrionica == "-1" ?"":histrionica == "1" ?"":histrionica=="2"?"X":histrionica=="3"?"":histrionica, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(histrionica == "-1" ?"":histrionica == "1" ?"":histrionica=="2"?"":histrionica=="3"?"X":histrionica, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                
                new PdfPCell(new Phrase("Personalidad Dependiente", fontColumnValueBold)) { Colspan =5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(dependiente == "-1" ?"":dependiente == "1" ?"X":dependiente=="2"?"":dependiente=="3"?"":dependiente, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(dependiente == "-1" ?"":dependiente == "1" ?"":dependiente=="2"?"X":dependiente=="3"?"":dependiente, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                new PdfPCell(new Phrase(dependiente == "-1" ?"":dependiente == "1" ?"":dependiente=="2"?"":dependiente=="3"?"X":dependiente, fontColumnValue)) { Colspan =4,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f},    
                
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 10f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region ENTREVISTA
            var entrevista = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_ENTREVISTA) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_ENTREVISTA).v_Value1;
            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("ENTREVISTA: ", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 18f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK },       
                new PdfPCell(new Phrase(entrevista, fontColumnValue_1)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP, MinimumHeight = 80f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region RECOMENDACIONES
            var recomendaciones = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_PSICOSENSOMETRICO_DESC) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_GOLDFIELDS_PSICOSENSOMETRICO_DESC).v_Value1;
            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("RECOMENDACIONES", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 18f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK },       
                new PdfPCell(new Phrase(recomendaciones, fontColumnValue)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP, MinimumHeight = 20f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region  CONCLUSIONES
            var aptitud = reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_APTITUD) == null ? "" : reporte_psicologico_brigadistas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_APTITUD).v_Value1;
            string aptitudF = "";
            if (aptitud == "-1")
            {
                aptitudF = "- - -";
            }
            else if (aptitud == "1")
            {
                aptitudF = "APTO";
            }
            else if (aptitud == "2")
            {
                aptitudF = "OBSERVADO";
            }
            else if (aptitud == "3")
            {
                aptitudF = "NO APTO";
            }

            cells = new List<PdfPCell>()
            {         
                new PdfPCell(new Phrase("CONCLUSIÓN", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK },       
                new PdfPCell(new Phrase("El Sr.(a) " + datosPac.v_FirstName + " " + datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " se encuentra con la categoría de "+ aptitudF +", para el puesto que solicita.", fontColumnValue)) { Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 25f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
       
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            //var psico = serviceComponent.Find(p => p.i_CategoryId == (int)Sigesoft.Common.Consultorio.Psicología);

            #region  EVALUADOR

            cells = new List<PdfPCell>()
            {     
                new PdfPCell(new Phrase("EVALUADOR: " + DatosGrabo.Nombre, fontColumnValue)){ Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight= tamaño_celda},
                new PdfPCell(new Phrase("COLEGIATURA: " + DatosGrabo.CMP , fontColumnValue)){ Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight= tamaño_celda},
    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Firma

            #region Creando celdas de tipo Imagen y validando nulls
            PdfPCell cellFirmaTrabajador = null;
            PdfPCell cellHuellaTrabajador = null;
            PdfPCell cellFirma = null;

            // Firma del trabajador ***************************************************


            if (filiationData.FirmaTrabajador != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaTrabajador, null, null, 80, 40));
            else
                cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirmaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
            cellFirmaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellFirmaTrabajador.FixedHeight = 50F;
            // Huella del trabajador **************************************************

            if (filiationData.HuellaTrabajador != null)
                cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.HuellaTrabajador, null, null, 30, 45));
            else
                cellHuellaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellHuellaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHuellaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellHuellaTrabajador.FixedHeight = 50F;
            // Firma del doctor Auditor **************************************************

            if (DatosGrabo.Firma != null)
                cellFirma = new PdfPCell(HandlingItextSharp.GetImage(DatosGrabo.Firma, null, null, 120, 50)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER };
            else
                cellFirma = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirma.HorizontalAlignment = Element.ALIGN_CENTER;
            cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellFirma.FixedHeight = 50F;
            #endregion

            cells = new List<PdfPCell>()
            {
                new PdfPCell(cellFirmaTrabajador){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(cellHuellaTrabajador){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                new PdfPCell(new Phrase("FIRMA Y SELLO DEL PSICÓLOGO", fontColumnValueBold)) {Rowspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda},    
                new PdfPCell(cellFirma){Rowspan=3, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
 
                new PdfPCell(new Phrase("FIRMA DEL EXAMINADO", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 12f},    
                new PdfPCell(new Phrase("Huella DEL EXAMINADO", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 12f},    

                new PdfPCell(new Phrase("CON LA CUAL DECLARA QUE LA INFORMACIÓN DECLARADA ES VERAZ", fontColumnValueBold)) {Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    

            };
            columnWidths = new float[] { 25f, 25f, 25f, 25f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            document.Close();
            writer.Close();
            writer.Dispose();
        }
        #endregion
    }
}
