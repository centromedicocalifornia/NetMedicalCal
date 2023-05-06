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
    public class FichadeEvaluacionParaConductoresHudbay
    {
        private static void RunFile(string filePdf)
        {
            Process proceso = Process.Start(filePdf);
            proceso.WaitForExit();
            proceso.Close();
        }
        public static void CreateFichadeEvaluacionParaConductoresHudbay(ServiceList DataService, string filePDF,
          PacientList datosPac,
          organizationDto infoEmpresa, PacientList filiationData,
          List<ServiceComponentList> serviceComponent,
         UsuarioGrabo usuariograba, byte[] LogoEmpresa)
        {
            //Document document = new Document(PageSize.A4, 30f, 30f, 45f, 41f);
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
            Font fontTitle1 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue1 = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue2 = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold_1 = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold_2 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontTitleTableAntecedentesOcupacionales = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueAntecedentesOcupacionales = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            #endregion

            var tamaño_celda = 14f;
            var tamaño_celda1 = 45f;

            var tamaño_celda_1 = 13f;
            var tamaño_celda_2 = 10f;
            #region TÍTULO

            cells = new List<PdfPCell>();

            //if (infoEmpresa.b_Image != null)
            //{
            //    iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresa.b_Image));
            //    imagenEmpresa.ScalePercent(25);
            //    imagenEmpresa.SetAbsolutePosition(40, 790);
            //    document.Add(imagenEmpresa);
            //}
            if (LogoEmpresa != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(LogoEmpresa));
                imagenEmpresa.ScalePercent(25);
                imagenEmpresa.SetAbsolutePosition(38, 710);
                document.Add(imagenEmpresa);
            }
            Font fontColumnValue_1 = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));

            var cellsTit = new List<PdfPCell>()
                { 
                  new PdfPCell(new Phrase("", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 35f },
                new PdfPCell(new Phrase("FORMATOS PARA LA VALORACIÓN DE LA \nAPTITUD MÉDICA OCUPACIONAL", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 35f},
                new PdfPCell(new Phrase("Formato \n\nDocumento ID   : FOR-SSO-293\nVersión              :  01\nFecha              : 29/11/2022", fontColumnValue_1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 35f },
                    
                new PdfPCell(new Phrase("FICHA DE EVALUACION PARA CONDUCTORES", fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f , UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

                };
            columnWidths = new float[] { 20f, 50f, 30f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            ServiceComponentList conductores = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_ID);
            
            #region filiacion

            string[] fechaServicio = datosPac.FechaServicio.ToString().Split(' ');
            var conductores_1_1 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_1_1) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_1_1).v_Value1;
            var conductores_1_2 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_1_2) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_1_2).v_Value1;
            var conductores_1_3 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_1_3) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_1_3).v_Value1;
            var conductores_1_4 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_1_4) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_1_4).v_Value1;
            var conductores_1_5 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_1_5) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_1_5).v_Value1;

            cells = new List<PdfPCell>()
             {     

                new PdfPCell(new Phrase("FICHA DE DETECCION DE SINDROME DE APNEA OBSTRUCTIVA DEL SUEÑO  (SAOS) \n(Conductores de equipo liviano, pesado, transporte personal, transporte de sustancias peligrosas)", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f,  UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    

                new PdfPCell(new Phrase("1. FILIACIÓN", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BackgroundColor = BaseColor.LIGHT_GRAY },       

                new PdfPCell(new Phrase("Apellidos Y Nombres", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Fecha:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(fechaServicio[0], fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
              
                new PdfPCell(new Phrase("Trabaja de Noche", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(conductores_1_1 == "1"?"Si ( X )":"Si ( )", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(conductores_1_1 == "0"?"NO ( X )":"NO ( )", fontColumnValue)) { Colspan = 2,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("# días de trabajo: " + conductores_1_2, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase("# días de descanso: " + conductores_1_3, fontColumnValue)) { Colspan = 7,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
            
                new PdfPCell(new Phrase("Años que trabaja en dicho horario de trabajo "+ conductores_1_4 + " años", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(conductores_1_5 == "0"?"Ope. Eq. Pesado( X )":"Ope. Eq. Pesado(  )", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
                new PdfPCell(new Phrase(conductores_1_5 == "1"?"Ope. Eq. Liviano( X )":"Ope. Eq. Liviano(  )", fontColumnValue)) { Colspan = 5,HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda }, 
              

            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region antecedentes personales

            var conductores_2_1 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_2_1) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_2_1).v_Value1;
            var conductores_2_2 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_2_2) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_2_2).v_Value1;
            var conductores_2_3 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_2_3) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_2_3).v_Value1;
            var conductores_2_4 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_2_4) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_2_4).v_Value1;
            var conductores_2_5 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_2_5) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_2_5).v_Value1;
            var conductores_2_6 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_2_6) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_2_6).v_Value1;

            cells = new List<PdfPCell>()
             {     
                new PdfPCell(new Phrase("2. ANTECEDENTES PERSONALES", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BackgroundColor = BaseColor.LIGHT_GRAY },       

                new PdfPCell(new Phrase(conductores_2_1 == "1"?"HTA:     SI ( X )   NO (   )":conductores_2_1 == "0"?"HTA:     SI (   )   NO ( X )":"HTA:     SI (   )   NO (   )", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Medicación  (riesgo >2) : " + conductores_2_2, fontColumnValue)) { Colspan = 15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase("Polisomnografía realizada alguna vez", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Antecedente de choque en Mina", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_2_3 == "1"?"SI( X )":"SI(   )", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_2_3 == "0"?"NO( X )":"NO(   )", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase(conductores_2_5 == "1"?"Si ( X )     Fecha("+conductores_2_6+")":"Si (   )     Fecha(  /  /   )", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_2_5 == "0"?"NO ( X )":"NO (   )", fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Antecedente de choque en fuera de Mina", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_2_4 == "1"?"SI( X )":"SI(   )", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_2_4 == "0"?"NO( X )":"NO(   )", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion


            #region 3 PREGUNTAS

            var conductores_3_1 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_3_1) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_3_1).v_Value1;
            var conductores_3_2 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_3_2) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_3_2).v_Value1;
            var conductores_3_3 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_3_3) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_3_3).v_Value1;
            var conductores_3_4 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_3_4) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_3_4).v_Value1;
            var conductores_3_5 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_3_5) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_3_5).v_Value1;
            var conductores_3_6 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_3_6) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_3_6).v_Value1;
            var conductores_3_7 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_3_7) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_3_7).v_Value1;
            var conductores_3_8 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_3_8) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_3_8).v_Value1;
            var conductores_3_9 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_3_9) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_3_9).v_Value1;

            int valor_nunca = 0;
            int valor_poco = 0;
            int valor_moderada = 0;
            int valor_alta = 0;

            if (conductores_3_1 == "0"){ valor_nunca += int.Parse(conductores_3_1.ToString()); }
            else if (conductores_3_2 == "0") { valor_nunca += int.Parse(conductores_3_2.ToString()); }
            else if (conductores_3_3 == "0") { valor_nunca += int.Parse(conductores_3_3.ToString()); }
            else if (conductores_3_4 == "0") { valor_nunca += int.Parse(conductores_3_4.ToString()); }
            else if (conductores_3_5 == "0") { valor_nunca += int.Parse(conductores_3_5.ToString()); }
            else if (conductores_3_6 == "0") { valor_nunca += int.Parse(conductores_3_6.ToString()); }
            else if (conductores_3_7 == "0") { valor_nunca += int.Parse(conductores_3_7.ToString()); }
            else if (conductores_3_8 == "0") { valor_nunca += int.Parse(conductores_3_8.ToString()); }

            if (conductores_3_1 == "1") { valor_poco += int.Parse(conductores_3_1.ToString()); }
            else if (conductores_3_2 == "1") { valor_poco += int.Parse(conductores_3_2.ToString()); }
            else if (conductores_3_3 == "1") { valor_poco += int.Parse(conductores_3_3.ToString()); }
            else if (conductores_3_4 == "1") { valor_poco += int.Parse(conductores_3_4.ToString()); }
            else if (conductores_3_5 == "1") { valor_poco += int.Parse(conductores_3_5.ToString()); }
            else if (conductores_3_6 == "1") { valor_poco += int.Parse(conductores_3_6.ToString()); }
            else if (conductores_3_7 == "1") { valor_poco += int.Parse(conductores_3_7.ToString()); }
            else if (conductores_3_8 == "1") { valor_poco += int.Parse(conductores_3_8.ToString()); }

            if (conductores_3_1 == "2") { valor_moderada += int.Parse(conductores_3_1.ToString()); }
            else if (conductores_3_2 == "2") { valor_moderada += int.Parse(conductores_3_2.ToString()); }
            else if (conductores_3_3 == "2") { valor_moderada += int.Parse(conductores_3_3.ToString()); }
            else if (conductores_3_4 == "2") { valor_moderada += int.Parse(conductores_3_4.ToString()); }
            else if (conductores_3_5 == "2") { valor_moderada += int.Parse(conductores_3_5.ToString()); }
            else if (conductores_3_6 == "2") { valor_moderada += int.Parse(conductores_3_6.ToString()); }
            else if (conductores_3_7 == "2") { valor_moderada += int.Parse(conductores_3_7.ToString()); }
            else if (conductores_3_8 == "2") { valor_moderada += int.Parse(conductores_3_8.ToString()); }

            if (conductores_3_1 == "3") { valor_alta += int.Parse(conductores_3_1.ToString()); }
            else if (conductores_3_2 == "3") { valor_alta += int.Parse(conductores_3_2.ToString()); }
            else if (conductores_3_3 == "3") { valor_alta += int.Parse(conductores_3_3.ToString()); }
            else if (conductores_3_4 == "3") { valor_alta += int.Parse(conductores_3_4.ToString()); }
            else if (conductores_3_5 == "3") { valor_alta += int.Parse(conductores_3_5.ToString()); }
            else if (conductores_3_6 == "3") { valor_alta += int.Parse(conductores_3_6.ToString()); }
            else if (conductores_3_7 == "3") { valor_alta += int.Parse(conductores_3_7.ToString()); }
            else if (conductores_3_8 == "3") { valor_alta += int.Parse(conductores_3_8.ToString()); }

            cells = new List<PdfPCell>()
             {     
                new PdfPCell(new Phrase("3. ¿Con qué frecuencia se queda Ud. dormido en las siguientes situaciones? Incluso si no ha realizado recientemente alguna de las actividades mencionadas a continuación, trate de imaginar en qué medida le afectarían. Utilice la siguiente escala y elija la cifra adecuada para cada situación.", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f, BackgroundColor = BaseColor.LIGHT_GRAY },       

                new PdfPCell(new Phrase("Escala de Epworth \n(Escala modificada peruana)", fontColumnValueBold)) { Colspan = 12, Rowspan= 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Puntaje", fontColumnValueBold)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase("NUNCA \n(0)", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("POCO \n(1)", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("MODERADA \n(2)", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("ALTA \n(3)", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase("Sentado leyendo.", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_1 == "0"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_1 == "1"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_1 == "2"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_1 == "3"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase("Mirando la televisión.", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_2 == "0"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_2 == "1"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_2 == "2"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_2 == "3"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase("Sentado en un lugar público (en una conferencia, teatro, cine, reunión social o escuchando misa).", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_3 == "0"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_3 == "1"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_3 == "2"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_3 == "3"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase("Como pasajero de auto, micro, combi u ómnibus.", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_4 == "0"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_4 == "1"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_4 == "2"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_4 == "3"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase("Recostado en la tarde.", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_5 == "0"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_5 == "1"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_5 == "2"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_5 == "3"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase("Sentado y hablando con otra persona.", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_6 == "0"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_6 == "1"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_6 == "2"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_6 == "3"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase("Sentado tranquilamente después de almorzar sin haber ingerido alcohol.", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_7 == "0"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_7 == "1"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_7 == "2"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_7 == "3"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase("Manejando el auto, cuando se detiene por razones de tráfico.", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_8 == "0"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_8 == "1"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_8 == "2"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_8 == "3"?"X":string.Empty, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase("Sub total", fontColumnValueBold)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(valor_nunca.ToString(), fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(valor_poco.ToString(), fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(valor_moderada.ToString(), fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(valor_alta.ToString(), fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase("PUNTAJE TOTAL", fontColumnValueBold)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_3_9, fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
            
                //new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    

            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region 4 PREGUNTAS
            var conductores_4_1 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_4_1) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_4_1).v_Value1;
            var conductores_4_2 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_4_2) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_4_2).v_Value1;
            var conductores_4_3 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_4_3) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_4_3).v_Value1;
            var conductores_4_4 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_4_4) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_4_4).v_Value1;
            var conductores_4_5 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_4_5) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_4_5).v_Value1;
            var conductores_4_6 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_4_6) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_4_6).v_Value1;
            var conductores_4_7 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_4_7) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_4_7).v_Value1;
            var conductores_4_8 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_4_8) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_4_8).v_Value1;


            cells = new List<PdfPCell>()
             {     
                new PdfPCell(new Phrase("4. ENTREVISTA AL PACIENTE: ", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BackgroundColor = BaseColor.LIGHT_GRAY },       


                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("SI", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
     
                new PdfPCell(new Phrase("En los últimos 5 años, su pareja o esposa le ha comentado que ronca al dormir.", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_4_1 == "1"?"X":string.Empty, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_4_1 == "0"?"X":string.Empty, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase("En los últimos 5 años, su pareja o esposa le ha comentado que hacer ruidos al respirar mientras duerme.", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_4_2 == "1"?"X":string.Empty, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_4_2 == "0"?"X":string.Empty, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase("En los últimos 5 años, su pareja o esposa le ha comentado que deja de respirar cuando duerme (pausa respiratoria).", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_4_3 == "1"?"X":string.Empty, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_4_3 == "0"?"X":string.Empty, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase("Comparado con sus compañeros, usted siente que tiene más sueño o cansancio que ellos mientras trabaja.", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_4_4 == "1"?"X":string.Empty, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_4_4 == "0"?"X":string.Empty, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase("Ha tenido algún accidente o incidente vehicular porque cabeceo.", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_4_5 == "1"?"X":string.Empty, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_4_5 == "0"?"X":string.Empty, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase("Ha tenido algún accidente o incidente vehicular considerado “por falla humana”.", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_4_6 == "1"?"X":string.Empty, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_4_6 == "0"?"X":string.Empty, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase("Está recibiendo tratamiento para Apnea del sueño con CPAP", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_4_7 == "1"?"X":string.Empty, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_4_7 == "0"?"X":string.Empty, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase("Se le ha realizado una PSG durante el sueño para descartarle un trastorno del sueño.", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_4_8 == "1"?"X":string.Empty, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_4_8 == "0"?"X":string.Empty, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               


            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            PdfPCell cellFirmaTrabajador = null;
            PdfPCell cellHuellaTrabajador = null;
            PdfPCell cellfirmaMedico = null;
            if (filiationData.FirmaTrabajador != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaTrabajador, null, null, 130, 50));
            else
                cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirmaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellFirmaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

            if (filiationData.HuellaTrabajador != null)
                cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.HuellaTrabajador, null, null, 35, 50));
            else
                cellHuellaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellHuellaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellHuellaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

            if (usuariograba.Firma != null)
                cellfirmaMedico = new PdfPCell(HandlingItextSharp.GetImage(usuariograba.Firma, null, null, 130, 50));
            else
                cellfirmaMedico = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellfirmaMedico.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellfirmaMedico.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            
            cells = new List<PdfPCell>()
            {        
                new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(cellFirmaTrabajador){ Colspan =7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 51f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(cellHuellaTrabajador){ Colspan =6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 51f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    

                new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("FIRMA TRABAJADOR", fontColumnValueBold_1)){ Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("Huella", fontColumnValueBold_1)){ Colspan =6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               
            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);


            #region nueva hoja

            document.NewPage();

            //if (infoEmpresa.b_Image != null)
            //{
            //    iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresa.b_Image));
            //    imagenEmpresa.ScalePercent(25);
            //    imagenEmpresa.SetAbsolutePosition(40, 790);
            //    document.Add(imagenEmpresa);
            //}

            if (LogoEmpresa != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(LogoEmpresa));
                imagenEmpresa.ScalePercent(25);
                imagenEmpresa.SetAbsolutePosition(40, 700);
                document.Add(imagenEmpresa);
            }
            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("", fontColumnValue)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 22f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                    
                };
            columnWidths = new float[] { 20f, 50f, 30f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 35f },
                    new PdfPCell(new Phrase("FORMATOS PARA LA VALORACIÓN DE LA \nAPTITUD MÉDICA OCUPACIONAL", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 35f},
                    new PdfPCell(new Phrase("Formato \n\nDocumento ID   : FOR-SSO-293\nVersión              :  01\nFecha              : 29/11/2022", fontColumnValue_1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 35f },
                    
                    new PdfPCell(new Phrase("FICHA DE EVALUACION PARA CONDUCTORES", fontColumnValue)) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 16f , UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

                };
            columnWidths = new float[] { 20f, 50f, 30f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion


            #region 5 PREGUNTAS
            ServiceComponentList antro = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ANTROPOMETRIA_ID);
            ServiceComponentList funcVit = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FUNCIONES_VITALES_ID);
            string peso = "", talla = "", imc = "", ps = "", pd = "", cuello = "";
            string hombresi = "", hombreno = "";
            string mujersi = "", mujerno = "";
            if (antro!= null)
            {
                peso = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID).v_Value1;
                talla = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID).v_Value1;
                imc = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID).v_Value1;
                cuello = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PORCENTAJE_CUELLO) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PORCENTAJE_CUELLO).v_Value1;
                if (filiationData.i_SexTypeId == 1)
                {
                    if (decimal.Parse(cuello.ToString()) < (decimal)43.2)
                    {
                        hombresi = "X";
                    }
                    else
                    {
                        hombreno = "X";
                    }
                }
                else if (filiationData.i_SexTypeId == 2)
                {
                    if (decimal.Parse(cuello.ToString()) < (decimal)40.06)
                    {
                        mujersi = "X";
                    }
                    else
                    {
                        mujerno = "X";
                    }
                }
                
            }

            if (funcVit != null)
            {
                ps = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID).v_Value1;
                pd = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID).v_Value1;
            }
            var conductores_5_1 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_5_1) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_5_1).v_Value1;

            cells = new List<PdfPCell>()
             {     
                new PdfPCell(new Phrase("5. EXAMEN FÍSICO", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BackgroundColor = BaseColor.LIGHT_GRAY },       


                new PdfPCell(new Phrase("PESO (Kg): " + peso, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("TALLA (mts) : " + talla, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("IMC (kg/m2):   " + imc+ "    (>35 es de alto riesgo)", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
     
                new PdfPCell(new Phrase("Circunferencia de Cuello: " + cuello, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Varón (menor de 43,2 cm, es normal) \nNORMAL:  SI( "+hombresi+" )    NO( "+hombreno+" )", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Mujer (menor de 40,6 cm, es normal) \nNORMAL:  SI( "+mujersi+" )    NO( "+mujerno+" )", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
     
                new PdfPCell(new Phrase("P. Sistólica:    "+ps+"   mmHg", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("P. Diastólica:    "+pd+"   mmHg", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_5_1 == "1"?"HTA nueva    SI( X )    NO(   )":conductores_5_1 == "0"?"HTA nueva    SI(  )    NO( X )":"HTA nueva    SI(   )    NO(   )", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
     
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 1f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    

            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region CLASIFICACIÓN DE MALLAMPATI
            //VALORES FILIACION

            PdfPCell Imagen1 = null;
            PdfPCell Imagen2 = null;
            PdfPCell Imagen3 = null;
            PdfPCell Imagen4 = null;
            PdfPCell Imagen5 = null;

            Imagen1 = new PdfPCell(HandlingItextSharp.GetImage("C:/Banner/BOCA_1.jpg", 36, PdfPCell.ALIGN_CENTER, 32, 75)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE };
            Imagen2 = new PdfPCell(HandlingItextSharp.GetImage("C:/Banner/BOCA_2.jpg", 36, PdfPCell.ALIGN_CENTER, 32, 75)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE };
            Imagen3 = new PdfPCell(HandlingItextSharp.GetImage("C:/Banner/BOCA_3.jpg", 36, PdfPCell.ALIGN_CENTER, 32, 75)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE };
            Imagen4 = new PdfPCell(HandlingItextSharp.GetImage("C:/Banner/BOCA_4.jpg", 36, PdfPCell.ALIGN_CENTER, 32, 75)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE };
            Imagen5 = new PdfPCell(HandlingItextSharp.GetImage("C:/Banner/BOCA_6.png", 35, PdfPCell.ALIGN_CENTER, 120, 75)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE };
            
            var conductores_5_2 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_5_2) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_5_2).v_Value1;
            var conductores_5_3 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_5_3) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_5_3).v_Value1;
            var conductores_5_4 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_5_4) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_5_4).v_Value1;
            var conductores_5_5 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_5_5) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_5_5).v_Value1;

            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("Clasificación de Mallampati", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BackgroundColor = BaseColor.LIGHT_GRAY },       
                
                new PdfPCell(Imagen1) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 80f },    
                new PdfPCell(Imagen2) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 80f },    
                new PdfPCell(Imagen3) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 80f },    
                new PdfPCell(Imagen4) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 80f },    
                new PdfPCell(Imagen5) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 80f },    


                new PdfPCell(new Phrase(conductores_5_2=="1"?"I (X)":"I (  )", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase(conductores_5_3=="1"?"II (X)":"II (  )", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase(conductores_5_4=="1"?"III (X)":"III (  )", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase(conductores_5_5=="1"?"IV (X)":"IV (  )", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase("-", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion


            #region CONCLUSION DE LA EVALUACION
            //VALORES FILIACION

            var conductores_6_1 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_6_1) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_6_1).v_Value1;
            var conductores_6_2 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_6_2) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_6_2).v_Value1;
            var conductores_6_3 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_6_3) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_6_3).v_Value1;

            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("6. CONCLUSIÓN DE LA EVALUACIÓN", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BackgroundColor = BaseColor.LIGHT_GRAY },       
                
                new PdfPCell(new Phrase("", fontColumnValueBold_2)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f },    
                new PdfPCell(new Phrase("SI", fontColumnValueBold_2)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f },    
                new PdfPCell(new Phrase("NO", fontColumnValueBold_2)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 12f },    
     
                new PdfPCell(new Phrase("Riesgo alto de  SAHS. (un criterio positivo)  \nRequiere polisomnografia antes de dar aptitud para conducir:", fontColumnValueBold_2)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_6_1 == "1"?"X":string.Empty, fontColumnValue)) { Colspan = 4, Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_6_1 == "0"?"X":string.Empty, fontColumnValue)) { Colspan = 4, Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase("Criterio A: Excesiva somnolencia determinada por ESS mayor de 15 o cabeceo presenciado durante la evaluación (espera), antecedente de accidente por somnolencia o con alta sospecha por somnolencia  \nCRITERIO B: : Antecedentes de SAHS sin control reciente o sin cumplimiento de tratamiento (con CPAP o cirugía)", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase("Riesgo medio de SAHS. (un criterio positivo) \nRequiere polisomnografia antes de dar aptitud para conducir", fontColumnValueBold_2)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_6_2 == "1"?"X":string.Empty, fontColumnValue)) { Colspan = 4, Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_6_2 == "0"?"X":string.Empty, fontColumnValue)) { Colspan = 4, Rowspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               
                new PdfPCell(new Phrase("Criterio C: Historia de higiene de sueño sugiere SAS (presencia de ronquidos, somnolencia excesiva durante la actividad, pausas respiratorias) \nCRITERIO D: Cumple con 2 o más de los siguientes: \nIMC mayor o igual a 35 \nHipertensión Arterial (nueva, no controlada con una sola medicación) \nCircunferencia del cuello anormal \nPuntuación de Epworth mayor de 10 y menor de 16 \nAntecedente de trastorno del sueño (diagnosticado) sin seguimiento \nÍndice de ápnea-hipopnea (AHI) mayor de 5 y menor de 30  \n\nCRITERIO E: : Evaluación de vía aérea superior patológico*", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    

                new PdfPCell(new Phrase("Riesgo bajo de SAHS", fontColumnValueBold_2)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_6_3 == "1"?"X":string.Empty, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_6_3 == "0"?"X":string.Empty, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
               
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region CONCLUSION DE LA EVALUACION
            //VALORES FILIACION

            var conductores_7_1 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_7_1) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_7_1).v_Value1;
            var conductores_7_2 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_7_2) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_7_2).v_Value1;
            var conductores_7_1_V = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_7_1) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_7_1).v_Value1Name;
            var conductores_7_2_V = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_7_2) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_7_2).v_Value1Name;
            string sc = "-", cc= "-";
            if (conductores_7_1 != "11" && conductores_7_1 != "-1"  && conductores_7_1 != null)
            {
                sc = conductores_7_1_V;
            }
            if (conductores_7_2 != "11" && conductores_7_2 != "-1" && conductores_7_2 != null)
            {
                cc = conductores_7_2_V;
            }
            var conductores_7_3 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_7_3) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_7_3).v_Value1;
            var conductores_7_4 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_7_4) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_7_4).v_Value1;
            var conductores_7_5 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_7_5) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_7_5).v_Value1;
            var conductores_7_6 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_7_6) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_7_6).v_Value1;
            var conductores_7_7 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_7_7) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_7_7).v_Value1;

            var conductores_8_1 = conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_8_1) == null ? "" : conductores.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_8_1).v_Value1;

            string rojosi = "", rojono = "", verdesi = "", verdeno = "", amarillosi = "", amarillono = "";
            if (conductores_7_5 == "1") { rojosi = "X"; }
            else if (conductores_7_5 == "0") { rojono = "X"; }

            if (conductores_7_6 == "1") { verdesi = "X"; }
            else if (conductores_7_6 == "0") { verdeno = "X"; }

            if (conductores_7_7 == "1") { amarillosi = "X"; }
            else if (conductores_7_7 == "0") { amarillono = "X"; }

            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase("6. CONCLUSIÓN DE LA EVALUACIÓN", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, BackgroundColor = BaseColor.LIGHT_GRAY },       
                
                new PdfPCell(new Phrase("AGUDEZA VISUAL AO: SC( "+sc+" )   CC( "+cc+" )", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase(conductores_7_3 == "1"?"Psicosensometrico APTO ( X )     NO APTO (   )":conductores_7_3 == "2"?"Psicosensometrico APTO (   )     NO APTO ( X )":"Psicosensometrico APTO (   )     NO APTO (   )", fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
     
                new PdfPCell(new Phrase("Estereopsis (%)  : " + conductores_7_4, fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Vision de colores: Rojo  SI ( "+rojosi+" ) NO ( "+rojono+" )  - Verde SI ( "+verdesi+" ) NO ( "+verdeno+" ) - Amarillo SI ( "+amarillosi+" ) NO ( "+amarillono+" )", fontColumnValue)) { Colspan = 13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },    
     
                new PdfPCell(new Phrase("CONCLUSION FINAL DE LA EVALUACION(Criterios I al V)", fontColumnValueBold_2)) { Colspan = 12,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, BackgroundColor = BaseColor.LIGHT_GRAY },       
                new PdfPCell(new Phrase(conductores_8_1 =="1"?"APTO(  X  )    NO APTO(     )":conductores_8_1 =="2"?"APTO(     )    NO APTO(  X  )":"APTO(     )    NO APTO(     )", fontColumnValueBold_2)) { Colspan = 8,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 25f, BackgroundColor = BaseColor.LIGHT_GRAY },       

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region PIE PAGINA
           
            
            cells = new List<PdfPCell>()
            {            
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 5f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    

                new PdfPCell(cellfirmaMedico){ Colspan =8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 54f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(cellFirmaTrabajador){ Colspan =7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 54f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(cellHuellaTrabajador){ Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 54f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    

                new PdfPCell(new Phrase("Nombre y Apellidos del Médico \nNº de Colegiatura: " + usuariograba.CMP, fontColumnValueBold_1)){ Colspan =8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("Firma del trabajador", fontColumnValueBold_1)){ Colspan =7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("Huella(Indice Der)", fontColumnValueBold_1)){ Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    

            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion


            document.Close();
            writer.Close();
            writer.Dispose();
        }
    }
}
