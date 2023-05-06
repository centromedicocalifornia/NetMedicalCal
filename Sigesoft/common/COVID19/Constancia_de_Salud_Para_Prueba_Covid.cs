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
    public class Constancia_de_Salud_Para_Prueba_Covid
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }
        public static void CreateConstancia_de_Salud_Para_Prueba_Covid(ServiceList DataService, string filePDF,
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
                    new PdfPCell(new Phrase("\nCONSTANCIA DE SALUD PARA PRUEBA COVID", fontTitle1)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 40f, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase("\n", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 40f, Border = PdfPCell.NO_BORDER},

                    new PdfPCell(new Phrase("", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 8f, Border = PdfPCell.NO_BORDER},

                };
            columnWidths = new float[] { 5f, 83f, 17f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            ServiceComponentList covid = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CONSENTIMIENTO_INFORMADO_PARA_EXAMENES_DE_PRUEBA_ANTIGENO_COVID_19_ID_OC);

            #region INFORMACIÓN GENERAL
            string empresa = "";
            if (covid != null)
            {
                empresa = covid.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CONSENTIMIENTO_INFORMADO_PARA_EXAMENES_DE_PRUEBA_ANTIGENO_COVID_19_EMPRESA) == null ? "- - -" : covid.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CONSENTIMIENTO_INFORMADO_PARA_EXAMENES_DE_PRUEBA_ANTIGENO_COVID_19_EMPRESA).v_Value1;
            }

            string empresageneral = filiationData.empresa_;
            string empresacontrata = filiationData.contrata;
            string empresasubcontrata = filiationData.subcontrata;

            string contrata = "";
            if (empresageneral != empresasubcontrata) contrata = empresacontrata + " / " + empresasubcontrata;
            else contrata = empresacontrata;


            ServiceComponentList constanciaSaLUdCovid = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CONSTANCIA_DE_SALUD_PARA_PRUEBA_COVID_ID);
            string resultado1 = "", resultado2 = "", resultado3 = "";
            if (constanciaSaLUdCovid != null)
            {
                resultado1 = constanciaSaLUdCovid.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CONSTANCIA_DE_SALUD_PARA_PRUEBA_COVID_1) == null ? "- - -" : constanciaSaLUdCovid.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CONSTANCIA_DE_SALUD_PARA_PRUEBA_COVID_1).v_Value1Name;
                resultado2 = constanciaSaLUdCovid.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CONSTANCIA_DE_SALUD_PARA_PRUEBA_COVID_2) == null ? "- - -" : constanciaSaLUdCovid.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CONSTANCIA_DE_SALUD_PARA_PRUEBA_COVID_2).v_Value1Name;
                resultado3 = constanciaSaLUdCovid.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CONSTANCIA_DE_SALUD_PARA_PRUEBA_COVID_3) == null ? "- - -" : constanciaSaLUdCovid.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CONSTANCIA_DE_SALUD_PARA_PRUEBA_COVID_3).v_Value1Name;

            }
            ServiceComponentList pruebaAntigeno = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.RESULTADO_PRUEBA_DE_ANTIGENO_COVID_19_ID);
            string pruebaAntigenoresultado = "";
            if (pruebaAntigeno != null)
            {
                pruebaAntigenoresultado = pruebaAntigeno.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RESULTADO_PRUEBA_DE_ANTIGENO_COVID_19_RESULTADO) == null ? "- - -" : pruebaAntigeno.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RESULTADO_PRUEBA_DE_ANTIGENO_COVID_19_RESULTADO).v_Value1Name;
            }

            cells = new List<PdfPCell>()
            {    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, Border = PdfPCell.NO_BORDER},    

                
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("Por la presente, se deja en constancia que el colaborador:", fontColumnValue)){ Colspan = 9 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase(datosPac.v_FirstName + " " + datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName, fontColumnValueBold)){ Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("Identificado con DNI: ", fontColumnValue)){ Colspan = 4 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase(datosPac.v_DocNumber, fontColumnValueBold)){ Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("perteneciente a la empresa: ", fontColumnValue)){ Colspan = 6 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 5 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
 
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase(empresa == "- - -" || empresa == "" ?contrata:empresa, fontColumnValueBold)){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("El paciente fue evaluado el día: " + datosPac.FechaServicio.ToString().Split(' ')[0] + " de acuerdo al protocolo para detección del la COVID-19,", fontColumnValue)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("por la empresa de salud CLINICA SAN LORENZO S.R.L.", fontColumnValue)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("HALLAZGOS:", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("* Presenta síntomas (Dolor de garganta, tos, fiebre, orofaringe congestiva, cefalea, etc.", fontColumnValue)){ Colspan = 17 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase(resultado1 == "SI"?"SI (X)":"SI ( )", fontColumnValue)){ Colspan = 4 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase(resultado1 == "NO"?"NO (X)":"NO ( )", fontColumnValue)){ Colspan = 4 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 10 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("* ¿Es Vulnerable? (Hipertenso, Diabético, Obeso con IMC de 35 a más, etc.)", fontColumnValue)){ Colspan = 17 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase(resultado2 == "SI"?"SI (X)":"SI ( )", fontColumnValue)){ Colspan = 4 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase(resultado2 == "NO"?"NO (X)":"NO ( )", fontColumnValue)){ Colspan = 4 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 10 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("* Tiene antecedentes de contacto con caso sospechoso o confirmado de COVID-19", fontColumnValue)){ Colspan = 17 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
         
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase(resultado3 == "SI"?"SI (X)":"SI ( )", fontColumnValue)){ Colspan = 4 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase(resultado3 == "NO"?"NO (X)":"NO ( )", fontColumnValue)){ Colspan = 4 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 10 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("Resultados de Prueba Realizada:", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 18 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("* (_) Prueba Molecular", fontColumnValueBold)){ Colspan = 6 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("(_) Positivo ", fontColumnValue)){ Colspan = 3 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("/", fontColumnValue)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("(_) Negativo", fontColumnValue)){ Colspan = 4 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 4 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("* (_) Prueba Rápida Molecular", fontColumnValueBold)){ Colspan = 6 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("(_) Positivo ", fontColumnValue)){ Colspan = 3 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("/", fontColumnValue)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("(_) Negativo", fontColumnValue)){ Colspan = 4 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 4 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 2 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase(pruebaAntigenoresultado == "POSITIVO"|| pruebaAntigenoresultado == "NEGATIVO"  ?"* (X) Prueba Rápida Antígenos":"* (_) Prueba Rápida Antígenos ", fontColumnValueBold)){ Colspan = 6 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase(pruebaAntigenoresultado == "POSITIVO" ?"(X) Reactivo":"(_) Reactivo ", fontColumnValue)){ Colspan = 3 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("/", fontColumnValue)){ Colspan = 1 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase(pruebaAntigenoresultado == "NEGATIVO" ?"(X) No Reactivo":"(_) No Reactivo ", fontColumnValue)){ Colspan = 4 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 4 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
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


                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 80f, Border = PdfPCell.NO_BORDER},    
                //new PdfPCell(new Phrase("", fontColumnValue)){Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM, FixedHeight=80f, BorderColor=BaseColor.WHITE},
                //new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 80f, Border = PdfPCell.NO_BORDER},    
                //new PdfPCell(new Phrase("", fontColumnValue)) {Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER, FixedHeight=80f, BorderColor=BaseColor.BLACK},
                //new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 80f, Border = PdfPCell.NO_BORDER},    
                
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("____________________________________", fontColumnValue)){ Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = 13f , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("FIRMA Y SELLO DEL MÉDICO EVALUADOR", fontColumnValue)){ Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = tamaño_celda , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
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
