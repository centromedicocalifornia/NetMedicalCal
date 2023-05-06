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
    public class Cardiologia_Evaluacion_Cardiologica
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateCardiologia_Evaluacion_Cardiologica(string filePDF,
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
            Font fontColumnValue3 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));

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
                new PdfPCell(new Phrase("EVALUACIÓN CARDIOLÓGICA", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, Border = PdfPCell.NO_BORDER},
               
            };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Valores
            var tamaño_celda = 19f;
            #endregion

            #region Datos del Servicio

            string fechaInforme = DateTime.Now.ToString().Split(' ')[0];
            string[] fechaNac = datosPac.d_Birthdate.ToString().Split(' ');
            ServiceComponentList evaluacioncardiologica = exams.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ID);

            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_EMPRESA = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_EMPRESA) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_EMPRESA).v_Value1;

            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=10f, Border = PdfPCell.NO_BORDER},
                    
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("NOMBRES", fontColumnValueBold)) { Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan=15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("EDAD", fontColumnValueBold)) { Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " +edadActual.Split(' ')[0] + " Años", fontColumnValue)) { Colspan=15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("SEXO", fontColumnValueBold)) { Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + datosPac.Genero, fontColumnValue)) { Colspan=15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("EMPRESA", fontColumnValueBold)) { Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + CARDIOLOGIA_EVALUACION_CARDIOLOGICA_EMPRESA, fontColumnValue)) { Colspan=15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + datosPac.FechaServicio.ToString().Split(' ')[0] , fontColumnValue)) { Colspan=15, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},     

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion


            var tamaño_celda2 = 20f;

            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_1 = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_1) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_1).v_Value1;
            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_2 = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_2) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_2).v_Value1;
            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_3 = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_3) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_3).v_Value1;
            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_4 = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_4) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_4).v_Value1;
            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_5 = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_5) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_5).v_Value1;
            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_6 = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_6) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_6).v_Value1;
            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_7 = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_7) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_7).v_Value1;
            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_8 = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_8) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_8).v_Value1;
            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_9 = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_9) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_9).v_Value1;
            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_10 = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_10) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_10).v_Value1;
            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_11 = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_11) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_11).v_Value1;

            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_EDAD = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_EDAD) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_EDAD).v_Value1;
            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_COL_TOTAL = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_COL_TOTAL) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_COL_TOTAL).v_Value1;
            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_HDL = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_HDL) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_HDL).v_Value1;
            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_FUMADOR = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_FUMADOR) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_FUMADOR).v_Value1;
            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_PUNTAJE_TOTAL = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_PUNTAJE_TOTAL) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_PUNTAJE_TOTAL).v_Value1;
            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_PORCENTAJE_RIESGO = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_PORCENTAJE_RIESGO) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_PORCENTAJE_RIESGO).v_Value1;
            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_CLASIFICACION = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_CLASIFICACION) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_CLASIFICACION).v_Value1;

            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_EVAL_EX_FISICO = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_EVAL_EX_FISICO) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_EVAL_EX_FISICO).v_Value1;
            var CARDIOLOGIA_EVALUACION_CARDIOLOGICA_EVAL_EKG = evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_EVAL_EKG) == null ? "" : evaluacioncardiologica.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_EVAL_EKG).v_Value1;



            cellsTit = new List<PdfPCell>()
                {
                    
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=10f, Border = PdfPCell.NO_BORDER},
                    
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("ANTECEDENTES", fontColumnValueBold_SubTitulo)) { Colspan=18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("HTA", fontColumnValue)) { Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_1 == "1"?"( X )":"( "+string.Empty+" )", fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase("TABAQUISMO", fontColumnValue)) { Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_2 == "1"?"( X )":"( "+string.Empty+" )", fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase("DISLIPIDEMIA", fontColumnValue)) { Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_3 == "1"?"( X )":"( "+string.Empty+" )", fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("OBESIDAD", fontColumnValue)) { Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_4 == "1"?"( X )":"( "+string.Empty+" )", fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase("SEDENTARISMO", fontColumnValue)) { Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_5 == "1"?"( X )":"( "+string.Empty+" )", fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase("STRESS", fontColumnValue)) { Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_6 == "1"?"( X )":"( "+string.Empty+" )", fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("ALCOHOLISMO", fontColumnValue)) { Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_7 == "1"?"( X )":"( "+string.Empty+" )", fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase("HIPERURICEMIA", fontColumnValue)) { Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_8 == "1"?"( X )":"( "+string.Empty+" )", fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase("IMA previo", fontColumnValue)) { Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_9 == "1"?"( X )":"( "+string.Empty+" )", fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("DCM", fontColumnValue)) { Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_10 == "1"?"( X )":"( "+string.Empty+" )", fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase("OTROS", fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ANT_11, fontColumnValue)) { Colspan=10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     


                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("CLASIFICACIÓN DE RIESGO C.V. A 10 AÑOS (PUNTAJE FRAMINGHAN) > DE 40 AÑOS", fontColumnValueBold_SubTitulo)) { Colspan=18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     


                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("EDAD :", fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_EDAD + " Años", fontColumnValue)) { Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("COLESTEROL TOTAL :", fontColumnValue)) { Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_COL_TOTAL, fontColumnValue)) { Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("HDL:", fontColumnValue)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_HDL, fontColumnValue)) { Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("FUMADOR", fontColumnValue)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_FUMADOR, fontColumnValue)) { Colspan=13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("PUNTAJE TOTAL", fontColumnValue)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_PUNTAJE_TOTAL, fontColumnValue)) { Colspan=13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("% DE RIESGO A 10 AÑOS", fontColumnValue)) { Colspan=5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_PORCENTAJE_RIESGO, fontColumnValue)) { Colspan=13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("CLASIFICACIÓN DE RIESGO:", fontColumnValueBold)) { Colspan=6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase("BAJO", fontColumnValue)) { Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase(CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_CLASIFICACION == "1"?"( X )":"( "+string.Empty+" )", fontColumnValue)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("MODERADO", fontColumnValue)) { Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase(CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_CLASIFICACION == "2"?"( X )":"( "+string.Empty+" )", fontColumnValue)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("ALTO", fontColumnValue)) { Colspan=3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase(CARDIOLOGIA_EVALUACION_CARDIOLOGICA_CLAS_CLASIFICACION == "3"?"( X )":"( "+string.Empty+" )", fontColumnValue)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("EXAMEN FÍSICO", fontColumnValueBold_SubTitulo)) { Colspan=18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase(string.Empty, fontColumnValueBold)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(CARDIOLOGIA_EVALUACION_CARDIOLOGICA_EVAL_EX_FISICO, fontColumnValue3)) { Colspan=16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase("EKG", fontColumnValueBold_SubTitulo)) { Colspan=18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     

                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                    new PdfPCell(new Phrase(string.Empty, fontColumnValueBold)) { Colspan=2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(CARDIOLOGIA_EVALUACION_CARDIOLOGICA_EVAL_EKG, fontColumnValue)) { Colspan=16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.BOTTOM_BORDER},
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda2, Border = PdfPCell.NO_BORDER},     
                  
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
                    new PdfPCell(new Phrase("CONCLUSIONES:", fontColumnValueBold_SubTitulo)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
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

                columnWidths = new float[] { 15f, 80f, 5f };
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




            List<ListaComun> Listacomun = new List<ListaComun>();

            //if (filterDiagnosticRepository != null && filterDiagnosticRepository.Count > 0)
            //{
            cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("", fontColumnValueBold_SubTitulo)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
                    new PdfPCell(new Phrase("RECOMENDACIONES:", fontColumnValueBold_SubTitulo)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
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
                columnWidths = new float[] { 15f, 80f, 5f };
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
