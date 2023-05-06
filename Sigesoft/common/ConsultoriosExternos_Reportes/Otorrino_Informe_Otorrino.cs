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
    public class Otorrino_Informe_Otorrino
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateOtorrino_Informe_Otorrino(string filePDF,
            DatosDoctorMedicina medico,
            PacientList datosPac,
            organizationDto infoEmpresaPropietaria,
            List<ServiceComponentList> exams,
            List<DiagnosticRepositoryList> Diagnosticos,
            MedicoTratanteAtenciones medicoo,
            UsuarioGrabo DatosGrabo, List<Categoria> DataSource, string edadActual)
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
                new PdfPCell(new Phrase("INFORME OTORRINOLARINGOLOGÍA", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, Border = PdfPCell.NO_BORDER},
               
            };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Valores
            var tamaño_celda = 17f;
            #endregion

            #region Datos del Servicio

            string fechaInforme = DateTime.Now.ToString().Split(' ')[0];
            string[] fechaNac = datosPac.d_Birthdate.ToString().Split(' ');
           
            cellsTit = new List<PdfPCell>()
                { 
                   new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=10f, Border = PdfPCell.NO_BORDER},
                    
                   
                    new PdfPCell(new Phrase("PACIENTE", fontColumnValueBold)) { Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " + datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan=16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},

                    
                    new PdfPCell(new Phrase("EDAD", fontColumnValueBold)) { Colspan=4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},
                    new PdfPCell(new Phrase(": " +edadActual.Split(' ')[0] + " Años", fontColumnValue)) { Colspan=16, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.NO_BORDER},

                    new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                    new PdfPCell(new Phrase(": " +datosPac.FechaServicio.ToString().Split(' ')[0], fontColumnValue)) { Colspan = 16 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region EVALUACION

            ServiceComponentList informeOtorrino = exams.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OTORRINO_INFORME_OTORRINO_ID);

            var OTORRINO_INFORME_OTORRINO_ANAMNESIS = informeOtorrino.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_INFORME_OTORRINO_ANAMNESIS) == null ? "" : informeOtorrino.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_INFORME_OTORRINO_ANAMNESIS).v_Value1;
            var OTORRINO_INFORME_OTORRINO_ANTECEDENTES = informeOtorrino.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_INFORME_OTORRINO_ANTECEDENTES) == null ? "" : informeOtorrino.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_INFORME_OTORRINO_ANTECEDENTES).v_Value1;

            var OTORRINO_INFORME_OTORRINO_TIMPANO_OD = informeOtorrino.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_INFORME_OTORRINO_TIMPANO_OD) == null ? "" : informeOtorrino.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_INFORME_OTORRINO_TIMPANO_OD).v_Value1;
            var OTORRINO_INFORME_OTORRINO_TIMPANO_OI = informeOtorrino.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_INFORME_OTORRINO_TIMPANO_OI) == null ? "" : informeOtorrino.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_INFORME_OTORRINO_TIMPANO_OI).v_Value1;
            var OTORRINO_INFORME_OTORRINO_NARIZ = informeOtorrino.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_INFORME_OTORRINO_NARIZ) == null ? "" : informeOtorrino.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_INFORME_OTORRINO_NARIZ).v_Value1;
            var OTORRINO_INFORME_OTORRINO_FARINGE = informeOtorrino.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_INFORME_OTORRINO_FARINGE) == null ? "" : informeOtorrino.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_INFORME_OTORRINO_FARINGE).v_Value1;

            var OTORRINO_INFORME_OTORRINO_Dx_CLINICO = informeOtorrino.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_INFORME_OTORRINO_Dx_CLINICO) == null ? "" : informeOtorrino.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_INFORME_OTORRINO_Dx_CLINICO).v_Value1;
            var OTORRINO_INFORME_OTORRINO_CONCLUSIONES = informeOtorrino.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_INFORME_OTORRINO_CONCLUSIONES) == null ? "" : informeOtorrino.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OTORRINO_INFORME_OTORRINO_CONCLUSIONES).v_Value1;


          
            cellsTit = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=2f, Border = PdfPCell.NO_BORDER},

                new PdfPCell(new Phrase("1. ANAMNESIS:", fontColumnValueBold)){ Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},
                new PdfPCell(new Phrase(OTORRINO_INFORME_OTORRINO_ANAMNESIS, fontColumnValue)){Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, Border = PdfPCell.NO_BORDER},

                new PdfPCell(new Phrase("2. ANTECEDENTES:", fontColumnValueBold)){ Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},
                new PdfPCell(new Phrase(OTORRINO_INFORME_OTORRINO_ANTECEDENTES, fontColumnValue)){Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, Border = PdfPCell.NO_BORDER},

                new PdfPCell(new Phrase("3. EXAMEN FÍSICO:", fontColumnValueBold)){ Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},
                
                new PdfPCell(new Phrase("", fontColumnValue)){Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},
                new PdfPCell(new Phrase("  3.1. OÍDOS:", fontColumnValueBold)){ Colspan = 19, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},

                new PdfPCell(new Phrase("", fontColumnValue)){Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},
                new PdfPCell(new Phrase("TÍMPANO DERECHO:", fontColumnValueBold)){ Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},
                new PdfPCell(new Phrase(OTORRINO_INFORME_OTORRINO_TIMPANO_OD, fontColumnValue)) { Colspan = 14,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)){Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},
                new PdfPCell(new Phrase("TÍMPANO IZQUIERDO:", fontColumnValueBold)){ Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},
                new PdfPCell(new Phrase(OTORRINO_INFORME_OTORRINO_TIMPANO_OI, fontColumnValue)) { Colspan = 14,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)){Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},
                new PdfPCell(new Phrase("  3.2. NARIZ:", fontColumnValueBold)){ Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},
                new PdfPCell(new Phrase(OTORRINO_INFORME_OTORRINO_NARIZ, fontColumnValue)) { Colspan = 16,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)){Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},
                new PdfPCell(new Phrase("  3.3. FARINGE:", fontColumnValueBold)){ Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},
                new PdfPCell(new Phrase(OTORRINO_INFORME_OTORRINO_FARINGE, fontColumnValue)) { Colspan = 16,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER },    
                
                new PdfPCell(new Phrase("´4. DIAGNÓSTICO CLÍNICO", fontColumnValueBold)){ Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},
                new PdfPCell(new Phrase(OTORRINO_INFORME_OTORRINO_Dx_CLINICO, fontColumnValue)){Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, Border = PdfPCell.NO_BORDER},




                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=5f, Border = PdfPCell.NO_BORDER},
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);

            document.Add(table);




            #region DIAGNOSTICOS


            var filterDiagnosticRepository = Diagnosticos.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.OTORRINO_EVALUACION_AUDIOMETRICA_ID);
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("5. DIAGNÓSTICO AUDIOLÓGICO: Audiometría realizada en Clínica San Lorenzo S.R.L. ", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
            };
            columnWidths = new float[] { 100f };
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
                        cell = new PdfPCell(new Phrase(Contador_Dx + ". " + item.v_DiseasesName + " (" + item.v_Dx_CIE10 + ")", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, Border = PdfPCell.NO_BORDER, MinimumHeight = tamaño_celda };
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



             cellsTit = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=2f, Border = PdfPCell.NO_BORDER},

                new PdfPCell(new Phrase("6. CONCLUSIONES:", fontColumnValueBold)){ Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},
                new PdfPCell(new Phrase(OTORRINO_INFORME_OTORRINO_CONCLUSIONES, fontColumnValue)){Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 30f, Border = PdfPCell.NO_BORDER},

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=5f, Border = PdfPCell.NO_BORDER},
            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);


            List<ListaComun> Listacomun = new List<ListaComun>();
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("7. SUGERENCIAS:", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
 
            };
            columnWidths = new float[] { 100f };
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
                    cell = new PdfPCell(new Phrase(item.i_Item + ". " + item.Valor1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, Border = PdfPCell.NO_BORDER, MinimumHeight = tamaño_celda };
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
                new PdfPCell(new Phrase("" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER},
                new PdfPCell(cellFirma){ HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.BOTTOM_BORDER},
                new PdfPCell(new Phrase("" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 30f},
                new PdfPCell(new Phrase("" , fontColumnValueBold)){ HorizontalAlignment = PdfPCell.ALIGN_CENTER,  Border = PdfPCell.NO_BORDER},
                new PdfPCell(new Phrase("" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 30f},
                new PdfPCell(new Phrase("" , fontColumnValueBold)){ HorizontalAlignment = PdfPCell.ALIGN_CENTER,  Border = PdfPCell.NO_BORDER},

                new PdfPCell(new Phrase("" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 30f},
                new PdfPCell(new Phrase("FIRMA " , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_CENTER,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 30f},
                new PdfPCell(new Phrase("" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 30f},
                new PdfPCell(new Phrase("" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_CENTER,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 30f},
                new PdfPCell(new Phrase("" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 30f},
                new PdfPCell(new Phrase("", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_CENTER,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 30f},



            };
            columnWidths = new float[] { 40f, 30f, 2f, 30f, 2f, 6f };

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
