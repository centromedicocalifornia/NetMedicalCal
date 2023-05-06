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
    public class Cardiologia_Informe_ElectroCardiografico
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateCardiologia_Informe_ElectroCardiografico(string filePDF,
            DatosDoctorMedicina medico,
            PacientList datosPac,
            organizationDto infoEmpresaPropietaria,
            List<ServiceComponentList> exams,
            List<DiagnosticRepositoryList> Diagnosticos,
            MedicoTratanteAtenciones medicoo,
            UsuarioGrabo DatosGrabo, List<Categoria> DataSource, string edadActual)
        {
            //Document document = new Document(PageSize.A4, 30f, 30f, 45f, 41f);
            Document document = new Document(PageSize.A4, 30f, 30f, 75f, 58f);


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
            Font fontTitle1 = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE, new BaseColor(System.Drawing.Color.Black));

            Font fontTitle1_1 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle1_2 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontTitle2 = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.White));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL | iTextSharp.text.Font.UNDERLINE, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValueBold_SubTitulo = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValueBold1 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));

            Font fontColumnValueBold_Dx = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE, new BaseColor(System.Drawing.Color.Blue));

            Font fontColumnValueBold_2 = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
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
                new PdfPCell(new Phrase("INFORME ELECTRO – CARDIOGRÁFICO", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, Border = PdfPCell.NO_BORDER},
               
            };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Valores
            var tamaño_celda = 22f;
            #endregion

            #region Datos del Servicio

            string fechaInforme = DateTime.Now.ToString().Split(' ')[0];
            string[] fechaNac = datosPac.d_Birthdate.ToString().Split(' ');
            ServiceComponentList informeelectroCardio = exams.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_ID);

            var CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_EMPRESA = informeelectroCardio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_EMPRESA) == null ? "" : informeelectroCardio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_EMPRESA).v_Value1;

            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=10f, Border = PdfPCell.NO_BORDER},
                    
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("APELLIDOS Y NOMBRES", fontColumnValueBold)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan=13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("EDAD", fontColumnValueBold)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " +edadActual.Split(' ')[0] + " Años", fontColumnValue)) { Colspan=13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("SEXO", fontColumnValueBold)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + datosPac.Genero, fontColumnValue)) { Colspan=13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("EMPRESA", fontColumnValueBold)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_EMPRESA, fontColumnValue)) { Colspan=13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + datosPac.FechaServicio.ToString().Split(' ')[0] , fontColumnValue)) { Colspan=13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight= 15f, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("" , fontColumnValue)) { Colspan=18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight= 15f, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight= 15f, Border = PdfPCell.NO_BORDER},     

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            
            var tamaño_celda2 = 25f;
            //if (MEDICINA_INTERNA_INFORME_MEDICO_DESCRIPCION != "-")
            //{
            //    tamaño_celda2 = 80f;
            //}
            var CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_FREC_CARD = informeelectroCardio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_FREC_CARD) == null ? "" : informeelectroCardio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_FREC_CARD).v_Value1;
            var CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_RITMO = informeelectroCardio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_RITMO) == null ? "" : informeelectroCardio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_RITMO).v_Value1;
            var CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_INTERVALO_PR = informeelectroCardio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_INTERVALO_PR) == null ? "" : informeelectroCardio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_INTERVALO_PR).v_Value1;
            var CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_COMPLEJO_QRS = informeelectroCardio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_COMPLEJO_QRS) == null ? "" : informeelectroCardio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_COMPLEJO_QRS).v_Value1;
            var CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_INTERVALO_QTC = informeelectroCardio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_INTERVALO_QTC) == null ? "" : informeelectroCardio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_INTERVALO_QTC).v_Value1;
            var CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_EJE_CARDIACO = informeelectroCardio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_EJE_CARDIACO) == null ? "" : informeelectroCardio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_EJE_CARDIACO).v_Value1;
            var CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_HALLAZGO = informeelectroCardio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_HALLAZGO) == null ? "" : informeelectroCardio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_HALLAZGO).v_Value1;



            cellsTit = new List<PdfPCell>()
                {
                    
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=10f, Border = PdfPCell.NO_BORDER},
                    
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("FRECUENCIA CARDIACA", fontColumnValueBold)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_FREC_CARD + "    Lat/min", fontColumnValue)) { Colspan=13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("RITMO", fontColumnValueBold)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_RITMO, fontColumnValue)) { Colspan=13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("INTERVALO PR", fontColumnValueBold)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_INTERVALO_PR + "  \"", fontColumnValue)) { Colspan=13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("COMPLEJO QRS", fontColumnValueBold)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_COMPLEJO_QRS + "  \"", fontColumnValue)) { Colspan=13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("INTERVALO Qtc", fontColumnValueBold)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_INTERVALO_QTC + "  \"", fontColumnValue)) { Colspan=13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("EJE CARDIACO", fontColumnValueBold)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_EJE_CARDIACO + "°", fontColumnValue)) { Colspan=13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=40f, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("HALLAZGO", fontColumnValueBold)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=40f, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_HALLAZGO, fontColumnValue)) { Colspan=13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=40f, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=40f, Border = PdfPCell.NO_BORDER},     


                  
                 };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);

            document.Add(table);



            #region ANTECEDENTES



            #region DIAGNOSTICOS


            var filterDiagnosticRepository = Diagnosticos.FindAll(p => p.i_CategoryId == (int)Sigesoft.Common.CategoryTypeExam.CARDIOLOGIA_C);

            
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("", fontColumnValueBold_SubTitulo)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
                    new PdfPCell(new Phrase("CONCLUSIONES:", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
                    new PdfPCell(new Phrase("", fontColumnValueBold_SubTitulo)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
 
                    //new PdfPCell(new Phrase("ESPECIFICACIONES", fontColumnValueBold1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.GRAY },       
                };
                columnWidths = new float[] { 5f, 90f, 5f };
                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                document.Add(filiationWorker);
                cells = new List<PdfPCell>();

                int Contador_Dx = 1;
                if (filterDiagnosticRepository != null && filterDiagnosticRepository.Count > 0)
                {
                    foreach (var item in filterDiagnosticRepository)
                    {
                        if (item.v_DiseasesId == "N009-DD000000029")
                        {
                            cell = new PdfPCell(new Phrase("")) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Border = PdfPCell.NO_BORDER };
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
                
                    columnWidths = new float[] { 30f, 65f, 5f };
                }
                else
                {
                    cell = new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER};     
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(string.Empty, fontColumnValueBold)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER};
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase(string.Empty, fontColumnValue)) { Colspan=13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.BOTTOM_BORDER};
                    cells.Add(cell);
                    cell = new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER};     
                    cells.Add(cell);

                    columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                }
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);




            List<ListaComun> Listacomun = new List<ListaComun>();

            //if (filterDiagnosticRepository != null && filterDiagnosticRepository.Count > 0)
            //{
                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("", fontColumnValueBold_SubTitulo)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
                    new PdfPCell(new Phrase("RECOMENDACIONES:", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
                    new PdfPCell(new Phrase("", fontColumnValueBold_SubTitulo)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
 
                };
                columnWidths = new float[] { 5f, 90f, 5f };
                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                document.Add(filiationWorker);
                cells = new List<PdfPCell>();

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
            //}
            //else
            //{
            //    cell = new PdfPCell(new Phrase("", fontColumnValueBold_Dx)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Border = PdfPCell.NO_BORDER, MinimumHeight = 40f };
            //    cells.Add(cell);
            //    columnWidths = new float[] { 100 };
            //}

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
                columnWidths = new float[] { 30f, 65f, 5f };
            }
            else
            {
                cell = new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, Border = PdfPCell.NO_BORDER };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(string.Empty, fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, Border = PdfPCell.NO_BORDER };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase(string.Empty, fontColumnValue)) { Colspan = 13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, Border = PdfPCell.BOTTOM_BORDER };
                cells.Add(cell);
                cell = new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, Border = PdfPCell.NO_BORDER };
                cells.Add(cell);

                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            }
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);

            #endregion

            #region OBSERVACIONES - SUGERENCIAS

            #region Firma

            #region Creando celdas de tipo Imagen y validando nulls
            PdfPCell cellFirma = null;

            if (DatosGrabo != null)
            {
                if (DatosGrabo.Firma != null)
                    cellFirma = new PdfPCell(HandlingItextSharp.GetImage(DatosGrabo.Firma, null, null, 140, 70)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER };
                else
                    cellFirma = new PdfPCell(new Phrase(" ", fontColumnValue));
            }
            else
                cellFirma = new PdfPCell(new Phrase(" ", fontColumnValue));

            cellFirma.HorizontalAlignment = Element.ALIGN_CENTER;
            cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellFirma.FixedHeight = 70f;
            #endregion

            string observaciones_new = "";






            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("" , fontColumnValueBold)){Colspan = 3, HorizontalAlignment = PdfPCell.ALIGN_LEFT,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 2f},

                new PdfPCell(new Phrase("" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 80f},
                new PdfPCell(new Phrase("" , fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, Border = PdfPCell.NO_BORDER, MinimumHeight = 80f},    
                new PdfPCell(new Phrase("" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 80f},

                new PdfPCell(new Phrase("" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER},
                new PdfPCell(new Phrase("FIRMA DEL MÉDICO" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_CENTER,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.TOP_BORDER},
                new PdfPCell(new Phrase("", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    


            };
            columnWidths = new float[] { 25f, 50f, 25f };

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
