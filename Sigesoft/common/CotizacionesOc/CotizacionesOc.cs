using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BE.Custom;
using Font = iTextSharp.text.Font;

namespace NetPdf
{
    public class CotizacionesOc
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();

        }
        public static void CrearCotizacionesOc(int TipoReport,
            List<CotizacionocdetaillList> CotizacionList,
            CotizacionocDtoList CotizacionDto,
            OrganizationList infoEmpresaPropietaria,
            string filePDF)
        {

            Document document = new Document(PageSize.A4, 30f, 30f, 75f, 58f);
            document.SetPageSize(iTextSharp.text.PageSize.A4);


            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
            pdfPage_NEW page = new pdfPage_NEW();
            writer.PageEvent = page;
            page.Title = string.Empty;
            document.Open();

            #region Declaration Tables

            var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.White);
            string include = string.Empty;
            List<PdfPCell> cells = null;
            float[] columnWidths = null;
            //string[] columnValues = null;
            string[] columnHeaders = null;


            PdfPTable filiationWorker = new PdfPTable(8);

            PdfPTable table = null;

            PdfPCell cell = null;

            #endregion

            #region Fonts

            Font fontTitle1 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue2 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Blue));

            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValueBold_LINE = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue1 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold1 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));

            Font fontColumnValueNegrita = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueNormal = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueNegrita1 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));

            Font fontAptitud = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            #endregion
            var tamaño_celda = 17f;
            var tamaño_celda2 = 13f;

            #region PRIMERA PÁGINA
            #region TÍTULO

            cells = new List<PdfPCell>();

            //if (infoEmpresaPropietaria.b_Image != null)
            //{
            //    iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image));
            //    imagenEmpresa.ScalePercent(25);
            //    imagenEmpresa.SetAbsolutePosition(40, 790);
            //    document.Add(imagenEmpresa);
            //}
            //iTextSharp.text.Image imagenfirma = iTextSharp.text.Image.GetInstance("C:/Banner/GetImageText.jpg");
            //imagenfirma.ScalePercent(100);
            //imagenfirma.SetAbsolutePosition(200, 200);
            //document.Add(imagenfirma);
            #endregion
            #region Contenido
            cells = new List<PdfPCell>()
            {          
                
                new PdfPCell(new Phrase("", fontColumnValue))
                { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
               
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            string[] fechaServicio = CotizacionDto.d_DeliveryDate.ToString().Split('/', ' ');
            string mes = "";
            if (fechaServicio[1] == "01") mes = "Enero";
            else if (fechaServicio[1] == "02") mes = "Febrero";
            else if (fechaServicio[1] == "03") mes = "Marzo";
            else if (fechaServicio[1] == "04") mes = "Abril";
            else if (fechaServicio[1] == "05") mes = "Mayo";
            else if (fechaServicio[1] == "06") mes = "Junio";
            else if (fechaServicio[1] == "07") mes = "Julio";
            else if (fechaServicio[1] == "08") mes = "Agosto";
            else if (fechaServicio[1] == "09") mes = "Setiembre";
            else if (fechaServicio[1] == "10") mes = "Octubre";
            else if (fechaServicio[1] == "11") mes = "Noviembre";
            else if (fechaServicio[1] == "12") mes = "Diciembre";

            string Doc = CotizacionDto.v_Sumilla.Replace("(XXX)", CotizacionDto.i_CotizacionIdOc);
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("\n"+infoEmpresaPropietaria.v_Sede + ", " + fechaServicio[0] + " de " + mes + " del " + fechaServicio[2], fontColumnValue)) { Colspan = 17, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, BorderColor = BaseColor.WHITE, MinimumHeight = tamaño_celda},
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
   
                new PdfPCell(new Phrase("\n\n", fontColumnValueBold)) { Colspan = 20, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, BorderColor = BaseColor.WHITE, MinimumHeight = tamaño_celda},


                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase(Doc +"/"+ fechaServicio[1] +"-"+fechaServicio[2], fontColumnValueBold_LINE)){ Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("Sr(a). ", fontColumnValueBold)){ Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase(": " + CotizacionDto.v_RepresentanteLegal, fontColumnValue)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("" + CotizacionDto.v_RazonSocial.Split('/')[0], fontColumnValue)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    

                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("" + CotizacionDto.v_DireccionEmpresa, fontColumnValue)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("ASUNTO ", fontColumnValueBold)){ Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase(": " + CotizacionDto.v_Asunto, fontColumnValue)){ Colspan = 14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    

                //new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                //new PdfPCell(new Phrase("Estimado (s) Sr (s).", fontColumnValue)){ Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                //new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
             
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 200f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("\n\t\t\t "+CotizacionDto.v_Description, fontColumnValue)) { Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 200f , BorderColor=BaseColor.WHITE}, 
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 200f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    

                //new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                ////new PdfPCell(new Phrase("\nCc. Archivo", fontColumnValue)){ Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                //new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    

            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(filiationWorker);
            #endregion
            #region Fecha / Firmma

            PdfPCell cellFirmaTrabajador = null;

            if (CotizacionDto.FirmaUsuario != null)
                cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(CotizacionDto.FirmaUsuario, null, null, 170, 70));
            else
                cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirmaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellFirmaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;


            cells = new List<PdfPCell>()
            {          
          
                new PdfPCell(cellFirmaTrabajador ) {Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM, FixedHeight=160, BorderColor=BaseColor.WHITE},

                new PdfPCell(new Phrase(infoEmpresaPropietaria.v_Name, fontColumnValue)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = 15 , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("Tel. 076 340201 Anexo 29", fontColumnValueBold)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = 15 , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("Cel. 976220538 / " + CotizacionDto.celularUsuarioGraba, fontColumnValueBold)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,FixedHeight = 15 , BorderColor=BaseColor.WHITE},
                new PdfPCell(new Phrase("clinica.sanlorenzo@gmail.com\ncesar.medina@clinicasanlorenzo.com.pe" , fontColumnValueBold)){ Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,MinimumHeight = 15f , BorderColor=BaseColor.WHITE},
                //+ CotizacionDto.EmailUsuarioGraba
              };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            #endregion
            #endregion
            document.NewPage();


            #region SEGUNDA PAGINA
            #region TÍTULO

            //if (infoEmpresaPropietaria.b_Image != null)
            //{
            //    iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image));
            //    imagenEmpresa.ScalePercent(25);
            //    imagenEmpresa.SetAbsolutePosition(40, 790);
            //    document.Add(imagenEmpresa);
            //}
            #endregion
            #region empresa y servicio

            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("\n\n", fontColumnValue)){ Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("PROPUESTA ECONOMICA " , fontColumnValueBold)){ Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase(": " + CotizacionDto.v_RazonSocial.Split('/')[0], fontColumnValue)){ Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(filiationWorker);

            if (CotizacionDto.i_NumberOfWorker != 0)
            {
                cells = new List<PdfPCell>()
                {

                    new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    new PdfPCell(new Phrase("N° Trabajadores " , fontColumnValueBold )){ Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                    new PdfPCell(new Phrase(": " + CotizacionDto.i_NumberOfWorker, fontColumnValue)){ Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                    new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                };
                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
                document.Add(filiationWorker);
            }

            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("Metodo de Pago " , fontColumnValueBold )){ Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase(": " + CotizacionDto.LineaCredito, fontColumnValue)){ Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    


                new PdfPCell(new Phrase("", fontColumnValue)){ Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f, Border = PdfPCell.NO_BORDER},    

            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(filiationWorker);
            


            #region
            #region Parte Dinámica
            

            if (TipoReport == 1)
            {
                cells = new List<PdfPCell>();

                cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("TIPO EXAMEN", fontColumnValueNegrita1)) { BackgroundColor = new BaseColor(41, 66, 106), Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("COSTO", fontColumnValueNegrita1)) { BackgroundColor = new BaseColor(41, 66, 106), Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER };
                cells.Add(cell);
                decimal precioTotal = 0;

                var CotizacionListNew = CotizacionList.FindAll(p => p.i_EMOTypeD != 0);

                var CotizacionList_ = CotizacionListNew.GroupBy(
                    p => new
                    {
                        p.v_EMOTypeD,
                    }
                    ).Select 
                    ( x=> new CotizacionocdetaillList()
                    {
                        v_EMOTypeD = x.Key.v_EMOTypeD,
                        
                    });
                //var CotizacionList_ = CotizacionList.GroupBy(p => p.v_EMOTypeD).ToList();

                foreach (var Cabecera in CotizacionList_)
                {
                    cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan =1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(Cabecera.v_EMOTypeD, fontColumnValueNegrita)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER };
                    cells.Add(cell);

                    var ListaFiltrada = CotizacionListNew.FindAll(p => p.v_EMOTypeD == Cabecera.v_EMOTypeD);

                    string Valor = "";

                    var precio = ListaFiltrada.Sum(p => p.r_Price);
                    
                    precioTotal += decimal.Parse(precio.ToString());

                    Valor = precio.ToString("N2") == "0" ? "0" : "S/." + precio.ToString("N2");

                    cell = new PdfPCell(new Phrase(Valor, fontColumnValueNegrita)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER };
                    cells.Add(cell);

                }

                cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER };
                cells.Add(cell);

                cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("TOTAL: S/. ", fontColumnValueNegrita)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, Border = PdfPCell.NO_BORDER };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(precioTotal.ToString("N2"), fontColumnValueNegrita)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER };
                cells.Add(cell);


                cell = new PdfPCell(new Phrase("Los precios incluyen I.G.V.", fontColumnValueNegrita)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER };
                cells.Add(cell);

                cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER };
                cells.Add(cell);

                columnWidths = new float[] { 10f, 60f, 10f, 15f, 5f };

                //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, null, fontTitleTable);

                document.Add(filiationWorker);
            }
            else if (TipoReport == 2)
            {
                decimal precioTotal = 0;
                var CotizacionListNew = CotizacionList.FindAll(p => p.i_EMOTypeD != 0);

                var CotizacionList_ = CotizacionListNew.GroupBy(
                    p => new
                    {
                        p.v_EMOTypeD,
                    }).Select 
                ( x=> new CotizacionocdetaillList()
                    {
                        v_EMOTypeD = x.Key.v_EMOTypeD,
                    });
                cells = new List<PdfPCell>();
                foreach (var Cabecera in CotizacionList_)
                {

                    cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f};
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase("TIPO EXAMEN", fontColumnValueNegrita1)) { BackgroundColor = new BaseColor(41, 66, 106), Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(Cabecera.v_EMOTypeD, fontColumnValueNegrita1)) { BackgroundColor = new BaseColor(41, 66, 106), Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                    cells.Add(cell);

                    var ListaFiltrada = CotizacionListNew.FindAll(p => p.v_EMOTypeD == Cabecera.v_EMOTypeD);
                    string Valor = "";

                    foreach (var item in ListaFiltrada)
                    {
                        cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase(item.v_Component, fontColumnValueNormal)) { Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight = 13f };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                        cells.Add(cell);
                    }
                    var precio = ListaFiltrada.Sum(p => p.r_Price);
                    
                    precioTotal += decimal.Parse(precio.ToString());

                    Valor = precio.ToString("N2") == "0" ? "0" : "S/." + precio.ToString("N2");

                    cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase("Sub Total =>        S/.   ", fontColumnValueNegrita)) {Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, MinimumHeight = 13f  };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(Valor, fontColumnValueNegrita)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, MinimumHeight = 13f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1,  HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 2f};
                    cells.Add(cell);
                }

                cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("TOTAL: S/. ", fontColumnValueNegrita)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(precioTotal.ToString("N2"), fontColumnValueNegrita)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, MinimumHeight = 13f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                cells.Add(cell);


                cell = new PdfPCell(new Phrase("Los precios incluyen I.G.V.", fontColumnValueNegrita)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                cells.Add(cell);



                columnWidths = new float[] { 10f, 60f, 10f, 15f, 5f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, null, fontTitleTable);

                document.Add(filiationWorker);
            }
            else if (TipoReport == 3)
            {
                decimal precioTotal = 0;
                var CotizacionListNew = CotizacionList.FindAll(p => p.i_EMOTypeD != 0);

                var CotizacionList_ = CotizacionListNew.GroupBy(
                    p => new
                    {
                        p.v_EMOTypeD,
                    }).Select
                (x => new CotizacionocdetaillList()
                {
                    v_EMOTypeD = x.Key.v_EMOTypeD,
                });
                cells = new List<PdfPCell>();
                foreach (var Cabecera in CotizacionList_)
                {

                    cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase("TIPO EXAMEN", fontColumnValueNegrita1)) { BackgroundColor = new BaseColor(41, 66, 106), Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(Cabecera.v_EMOTypeD, fontColumnValueNegrita1)) { BackgroundColor = new BaseColor(41, 66, 106), Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                    cells.Add(cell);

                    var ListaFiltrada = CotizacionListNew.FindAll(p => p.v_EMOTypeD == Cabecera.v_EMOTypeD);
                    string Valor = "";

                    foreach (var item in ListaFiltrada)
                    {
                        cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase(item.v_Component, fontColumnValueNormal)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_LEFT, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase("S/. ", fontColumnValueNormal)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase(item.r_Price.ToString("N2"), fontColumnValueNormal)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE };
                        cells.Add(cell);
                        cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                        cells.Add(cell);
                    }
                    var precio = ListaFiltrada.Sum(p => p.r_Price);

                    precioTotal += decimal.Parse(precio.ToString());

                    Valor = precio.ToString("N2") == "0" ? "0" : "S/." + precio.ToString("N2");

                    cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase("Sub Total =>        S/.   ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, MinimumHeight = 13f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(Valor, fontColumnValueNegrita)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, MinimumHeight = 13f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                    cells.Add(cell);

                    cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 2f };
                    cells.Add(cell);
                }

                cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("TOTAL: S/. ", fontColumnValueNegrita)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(precioTotal.ToString("N2"), fontColumnValueNegrita)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, MinimumHeight = 13f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                cells.Add(cell);


                cell = new PdfPCell(new Phrase("Los precios incluyen I.G.V.", fontColumnValueNegrita)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                cells.Add(cell);



                columnWidths = new float[] { 10f, 60f, 10f, 15f, 5f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, null, fontTitleTable);

                document.Add(filiationWorker);
            }

            var CotizacionListN = CotizacionList.FindAll(p => p.i_EMOTypeD == 0);

            var CotizacionListN_ = CotizacionListN.GroupBy(
                p => new
                {
                    p.v_EMOTypeD,
                }).Select
            (x => new CotizacionocdetaillList()
            {
                v_EMOTypeD = x.Key.v_EMOTypeD,
            });
            cells = new List<PdfPCell>();
            foreach (var Cabecera in CotizacionListN_)
            {
                cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 25f };
                cells.Add(cell);

                cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("OTROS", fontColumnValueNegrita1)) { BackgroundColor = new BaseColor(212, 30, 38), Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                cells.Add(cell);

                var ListaFiltrada = CotizacionListN.FindAll(p => p.v_EMOTypeD == Cabecera.v_EMOTypeD);
                string Valor = "";

                foreach (var item in ListaFiltrada)
                {
                    cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.DescripcionOtros, fontColumnValueNormal)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase("S/. ", fontColumnValueNormal)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(item.r_Price.ToString("N2"), fontColumnValueNormal)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 13f, UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK };
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 1, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 13f };
                    cells.Add(cell);
                }


                cell = new PdfPCell(new Phrase(" ", fontColumnValueNegrita)) { Colspan = 5, HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.NO_BORDER, MinimumHeight = 2f };
                cells.Add(cell);
            }



            columnWidths = new float[] { 10f, 60f, 10f, 15f, 5f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            //filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, null, fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #endregion
            #endregion
            #endregion

            document.NewPage();
            #region TERCERA HOJA
            #region TÍTULO

            //if (infoEmpresaPropietaria.b_Image != null)
            //{
            //    iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image));
            //    imagenEmpresa.ScalePercent(25);
            //    imagenEmpresa.SetAbsolutePosition(40, 790);
            //    document.Add(imagenEmpresa);
            //}
            #endregion
            #region Texto en duro

            cells = new List<PdfPCell>()
            {
             new PdfPCell(new Phrase("\n", fontColumnValue))
             { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
              
                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase(CotizacionDto.v_Interconsultas, fontColumnValue )){ Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
    
                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("NOTA:", fontColumnValueBold )){ Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    

                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase(CotizacionDto.v_Anotaciones, fontColumnValue )){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               
                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                 new PdfPCell(new Phrase("\nNUESTROS HORARIOS DE ATENCIÓN", fontColumnValueBold )){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
          
                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("De Lunes a Sábado de 7:30 am a 12:00, y de 14:00 pm a 18:00 pm en nuestra para Ocupacional. ", fontColumnValue )){ Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    

                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("NÚMEROS DE CUENTA:", fontColumnValueBold )){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                
                new PdfPCell(new Phrase("", fontColumnValue))
             { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
             

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(filiationWorker);

            cells = new List<PdfPCell>()
                {   
                    new PdfPCell(new Phrase("", fontColumnValueBold )){ HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                    new PdfPCell(new Phrase("ENTIDAD BANCARIA", fontColumnValueBold2)) {BackgroundColor = new BaseColor(41,66,106), HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("NÚMERO DE CUENTA", fontColumnValueBold2)) {BackgroundColor = new BaseColor(41,66,106), HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("CUENTA INTERBANCARIA SOLES", fontColumnValueBold2)) {BackgroundColor = new BaseColor(41,66,106), HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("NÚMERO DE CUENTA DOLARES", fontColumnValueBold2)) {BackgroundColor = new BaseColor(41,66,106), HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("", fontColumnValueBold )){ HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                    
                    new PdfPCell(new Phrase("", fontColumnValueBold )){ HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                    new PdfPCell(new Phrase("BANCO CONTINENTAL\n(Cuenta Corriente)", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("0011-0277-0100054539-10", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("011-277-000100054539-10", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("0011-0277-0100054547-13", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("", fontColumnValueBold )){ HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                    new PdfPCell(new Phrase("", fontColumnValueBold )){ HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                    new PdfPCell(new Phrase("BANCO CONTINENTAL\n(Cuenta de Ahorro)", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("0011-0277-11-0200278043", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("0011-0277-11-0200278043 11", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("-", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("", fontColumnValueBold )){ HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                    new PdfPCell(new Phrase("", fontColumnValueBold )){ HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                    new PdfPCell(new Phrase("BANCO DE CREDITO\n(Cuenta Corriente)", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("245-1810834-0-98", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("002 245 0018 1083 409897", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("-", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("", fontColumnValueBold )){ HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                    new PdfPCell(new Phrase("", fontColumnValueBold )){ HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                    new PdfPCell(new Phrase("BANCO INTERBANK\n(Cuenta Corriente)", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("702-3000888410", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("-", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("-", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("", fontColumnValueBold )){ HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    

                    new PdfPCell(new Phrase("", fontColumnValueBold )){ HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                    new PdfPCell(new Phrase("", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },
                    new PdfPCell(new Phrase("", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },
                    new PdfPCell(new Phrase("", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK },
                    new PdfPCell(new Phrase("", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK },
                    new PdfPCell(new Phrase("", fontColumnValueBold )){ HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    

                    new PdfPCell(new Phrase("", fontColumnValueBold )){ HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                    new PdfPCell(new Phrase("CUENTA DE DETRACCIONES", fontColumnValueBold2)) {BackgroundColor = new BaseColor(41,66,106), Colspan= 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase("", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase("", fontColumnValueBold )){ HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, Border = PdfPCell.NO_BORDER},    

                    new PdfPCell(new Phrase("", fontColumnValueBold )){ HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                    new PdfPCell(new Phrase("BANCO DE LA NACION", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("00-774-002413", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda },
                    new PdfPCell(new Phrase("", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase("", fontColumnValue1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                    new PdfPCell(new Phrase("", fontColumnValueBold )){ HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    

                };

            columnWidths = new float[] { 10f, 20f, 20f, 25f, 20f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion

            cells = new List<PdfPCell>()
            {
           
                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("PROGRAMACIONES:", fontColumnValueBold )){ Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
    
                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("   * recepcion@clinicasanlorenzo.com.pe", fontColumnValue2 )){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
               
                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("   * clinica.sanlorenzo@gmail.com", fontColumnValue2 )){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    

                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("   * cesar.medina@clinicasanlorenzo.com.pe", fontColumnValue2 )){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    

                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("   * ruth.quispe@clinicasanlorenzo.com.pe", fontColumnValue2 )){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    

                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("Celular: 976220506 – 976496243", fontColumnValue )){ Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    

                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("Atentamente.", fontColumnValue )){ Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    


                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("CESAR AUGUSTO MEDINA ROJAS", fontColumnValueBold )){ Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    

                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("GERENTE GENERAL CLINICA SAN LORENZO SRL", fontColumnValueBold )){ Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    

                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("MÉDICO ESPECIALISTA EN MEDICINA OCUPACIONAL Y DEL MEDIO AMBIENTE.", fontColumnValueBold )){ Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    

                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("CMP: 38512; RNE: 39166", fontColumnValueBold )){ Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    

                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("CELULAR: 976220538", fontColumnValueBold )){ Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    

                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("cesar.medina@clinicasanlorenzo.com.pe", fontColumnValue2 )){ Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                new PdfPCell(new Phrase("", fontColumnValueBold )){ Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    




                new PdfPCell(new Phrase("", fontColumnValue))
             { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 10, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
             

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(filiationWorker);
            #endregion
            document.Close();
            writer.Close();
            writer.Dispose();
            RunFile(filePDF);
        }
    }
}
