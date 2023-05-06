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
    public class ConsentimientoInformadoExamenPRUEBAANTIGENOCovid19_LC
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }
        public static void CreateConsentimientoInformadoExamenPRUEBAANTIGENOCovid19_LC(ServiceList DataService, string filePDF,
         PacientList datosPac,
         organizationDto infoEmpresaPropietaria, PacientList filiationData,
         List<ServiceComponentList> serviceComponent)
        {
            Document document = new Document(PageSize.A4, 30f, 30f, 50f, 0f);


            document.SetPageSize(iTextSharp.text.PageSize.A4);

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
            pdfPage page = new pdfPage();
            writer.PageEvent = page;
            document.Open();

            #region Declaration Tables
            var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.Gray);
            string include = string.Empty;
            List<PdfPCell> cells = null;
            float[] columnWidths = null;
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
            document.Add(new Paragraph("\r\n"));
            #endregion

            #region Fonts
            Font fontTitle1 = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue1 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold1 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue2 = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));

            #endregion

            #region TÍTULO
            var tamaño_celda = 18f;
            cells = new List<PdfPCell>();

            //if (infoEmpresa.b_Image != null)
            //{
            //    iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresa.b_Image));
            //    imagenEmpresa.ScalePercent(25);
            //    imagenEmpresa.SetAbsolutePosition(40, 790);
            //    document.Add(imagenEmpresa);
            //}

            iTextSharp.text.Image imagenMinsa = iTextSharp.text.Image.GetInstance("C:/Banner/PLANTILLA - VERTICAL.png");
            imagenMinsa.ScalePercent(40);
            imagenMinsa.SetAbsolutePosition(0, 0);
            imagenMinsa.Alignment = iTextSharp.text.Image.UNDERLYING;
            document.Add(imagenMinsa);




            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("\n", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 40f, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase("\nConsentimiento Informado para examen de ANTIGENO COVID-19", fontTitle1)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 40f, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase("\n", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 40f, Border = PdfPCell.NO_BORDER},

                    new PdfPCell(new Phrase("", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 8f, Border = PdfPCell.NO_BORDER},

                };
            columnWidths = new float[] { 5f, 83f, 17f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            ServiceComponentList covid = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CONSENTIMIENTO_INFORMADO_PARA_EXAMENES_DE_PRUEBA_ANTIGENO_COVID_19_ID_LC);

            #region INFORMACIÓN GENERAL
            string empresa = "";
            if (covid != null)
            {
                empresa = covid.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CONSENTIMIENTO_INFORMADO_PARA_EXAMENES_DE_PRUEBA_ANTIGENO_COVID_19_LC_EMPRESA) == null ? "- - -" : covid.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CONSENTIMIENTO_INFORMADO_PARA_EXAMENES_DE_PRUEBA_ANTIGENO_COVID_19_LC_EMPRESA).v_Value1;
            }

            string empresageneral = filiationData.empresa_;
            string empresacontrata = filiationData.contrata;
            string empresasubcontrata = filiationData.subcontrata;

            string contrata = "";
            if (empresageneral != empresasubcontrata) contrata = empresacontrata + " / " + empresasubcontrata;
            else contrata = empresacontrata;





            cells = new List<PdfPCell>()
            {    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("Señor (a):", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase(datosPac.v_FirstName + " " + datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName, fontColumnValue)){ Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("Fecha:", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase(datosPac.FechaServicio.ToString().Split(' ')[0], fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("DNI:", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase(datosPac.v_DocNumber, fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("Edad:", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase(datosPac.Edad.ToString(), fontColumnValue)){ Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("Empresa:", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase(empresa == "- - -" || empresa == "" ?contrata:empresa, fontColumnValue)){ Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("En pleno de mis facultades, libre y voluntariamente manifiesto que he sido debidamente informado", fontColumnValue)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("y en consecuencia autorizo a que se me realice el procedimiento médico para la identificación de", fontColumnValue)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("ANTIGENO COVID-19, con la toma de muestra de HISOPADO NASOFARINGEO, teniendo encuenta que:", fontColumnValue)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("1. He comprendido la naturaleza y propósito del procedimiento.", fontColumnValue)){ Colspan = 17 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("2. He tenido la oportunidad de aclarar mis dudas.", fontColumnValue)){ Colspan = 17 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("3. Estoy satisfecho(a) con la información proporcionada.", fontColumnValue)){ Colspan = 17 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("4. Entiendo que mi consentimiento puede ser revocado en cualquier momento antes de la realización del", fontColumnValue)){ Colspan = 17 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("procedimiento.", fontColumnValue)){ Colspan = 17 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("5. Reconozco que todos los datos proporcionados son ciertos y que no he omitido ninguno que pueda influir", fontColumnValue)){ Colspan = 17 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("en el diagnóstico.", fontColumnValue)){ Colspan = 17 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("Por tanto declaro estar debiddamente informado y doy mi expreso consentimmiento a la realización del examen", fontColumnValue)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 4f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("propuesto.", fontColumnValue)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         

            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region CONTINUIDAD DE LA ATENCIÓN

            PdfPCell cellHuellaTrabajador = null;

            PdfPCell cellFirmaTrabajador = null;

            if (filiationData.FirmaTrabajador != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaTrabajador, null, null, 110, 45));
            else
                cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirmaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellFirmaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

            if (filiationData.HuellaTrabajador != null)
                cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.HuellaTrabajador, null, null, 40, 65));
            else
                cellHuellaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellHuellaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellHuellaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

            #region Fecha / Firmma
            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, Border = PdfPCell.NO_BORDER},    


                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 80f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(cellFirmaTrabajador ) {Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM, FixedHeight=80f, BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 80f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(cellHuellaTrabajador ) {Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER, FixedHeight=80f, BorderColor=BaseColor.BLACK},
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 80f, Border = PdfPCell.NO_BORDER},    
                
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("____________________________________", fontColumnValue)){ Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = 13f , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("FIRMA DEL TRABAJADOR", fontColumnValue)){ Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_celda , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            #endregion

            document.Close();
            writer.Close();
            writer.Dispose();
        }
    }
}
