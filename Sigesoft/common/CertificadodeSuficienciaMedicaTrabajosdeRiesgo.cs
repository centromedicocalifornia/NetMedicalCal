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
    public class CertificadodeSuficienciaMedicaTrabajosdeRiesgo
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateCertificadodeSuficienciaMedicaTrabajosdeRiesgo(string filePDF,
            DatosDoctorMedicina medico,
            PacientList datosPac,
            organizationDto infoEmpresaPropietaria,
            List<ServiceComponentList> exams,
            List<DiagnosticRepositoryList> Diagnosticos,
            MedicoTratanteAtenciones medicoo,
            UsuarioGrabo DatosGrabo, List<Categoria> DataSource, string edadActual, PacientList filiationData)
        {
            //Document document = new Document(PageSize.A4, 30f, 30f, 45f, 41f);
            //Document document = new Document(PageSize.A4, 40f, 40f, 50f, 50f);
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
            Font fontTitle1 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

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

            Font fontColumnValueBold_SubTitulo = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValueBold1 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));

            Font fontColumnValueBold_Dx = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE, new BaseColor(System.Drawing.Color.Blue));

            Font fontColumnValueBold_2 = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendiceRed = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Red));
            Font fontColumnValueApendiceBlue = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Blue));


            #endregion

            #region TÍTULO

            cells = new List<PdfPCell>();

            //if (infoEmpresaPropietaria.b_Image != null)
            //{
            //    iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image));
            //    imagenEmpresa.ScalePercent(25);
            //    imagenEmpresa.SetAbsolutePosition(40, 790);
            //    document.Add(imagenEmpresa);
            //}


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
                new PdfPCell(new Phrase("CERTIFICADO DE SUFICIENCIA MÉDICA PARA TRABAJOS DE RIESGO", fontTitle1)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 20f, Border = PdfPCell.NO_BORDER},
               
            };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Valores
            var tamaño_celda = 16f;
            #endregion

            #region Datos del Servicio

            string fechaInforme = DateTime.Now.ToString().Split(' ')[0];
            string[] fechaNac = datosPac.d_Birthdate.ToString().Split(' ');
            
            string empresageneral = filiationData.empresa_;
            string empresacontrata = filiationData.contrata;
            string empresasubcontrata = filiationData.subcontrata;

            string empr_Conct = "";
            if (empresageneral != empresasubcontrata) empr_Conct = empresageneral + " / " + empresacontrata + " / " + empresasubcontrata;
            else empr_Conct = empresageneral + " / " + empresacontrata;


            cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=2f, Border = PdfPCell.NO_BORDER},
                    
                    new PdfPCell(new Phrase("APELLIDOS Y NOMBRES", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                    new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan = 9 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    
                    new PdfPCell(new Phrase("N° DNI / CE", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                    new PdfPCell(new Phrase(datosPac.v_DocNumber, fontColumnValue)) { Colspan = 4 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    

                    new PdfPCell(new Phrase("PUESTO DE TRABAJO", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                    new PdfPCell(new Phrase(datosPac.v_CurrentOccupation, fontColumnValue)) { Colspan = 9 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                    new PdfPCell(new Phrase("EDAD", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                    new PdfPCell(new Phrase(edadActual.Split(' ')[0] + " Años", fontColumnValue)) { Colspan = 4 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    

                    new PdfPCell(new Phrase("EMPRESA", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                    new PdfPCell(new Phrase(empr_Conct, fontColumnValue)) { Colspan = 9 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    
                    new PdfPCell(new Phrase("FECHA", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                    new PdfPCell(new Phrase(datosPac.FechaServicio.ToString().Split(' ')[0], fontColumnValue)) { Colspan = 4 , HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK },    

                };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region EVALUACION 1

            ServiceComponentList certificadoRiesgo = exams.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_ID);

            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_1 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_1) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_1).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_2 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_2) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_2).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_3 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_3) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_3).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_4 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_4) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_4).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_5 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_5) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_5).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_6 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_6) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_6).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_7 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_7) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_7).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_8 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_8) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_8).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_9 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_9) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_9).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_10 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_10) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_10).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_11 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_11) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_11).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_12 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_12) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_12).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_13 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_13) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_13).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_14 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_14) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_14).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_15 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_15) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_15).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_16 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_16) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_16).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_17 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_17) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_17).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_18 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_18) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_18).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_19 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_19) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_19).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_20 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_20) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_20).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_21 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_21) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_21).v_Value1;


            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_3 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_3) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_3).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_4 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_4) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_4).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_5 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_5) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_5).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_6 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_6) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_6).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_7 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_7) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_7).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_8 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_8) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_8).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_9 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_9) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_9).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_10 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_10) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_10).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_11 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_11) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_11).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_12 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_12) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_12).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_13 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_13) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_13).v_Value1;
            
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_1 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_1) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_1).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_2 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_2) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_2).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_3 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_3) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_3).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_4 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_4) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_4).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_5 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_5) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_5).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_6 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_6) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_6).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_7 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_7) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_7).v_Value1;
            
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_1 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_1) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_1).v_Value1Name;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_2 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_2) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_2).v_Value1Name;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_3 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_3) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_3).v_Value1Name;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_4 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_4) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_4).v_Value1Name;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_5 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_5) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_5).v_Value1Name;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_6 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_6) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_6).v_Value1;

            ServiceComponentList antro = exams.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ANTROPOMETRIA_ID);
            string peso = "", peso_unidad = "", talla = "", talla_unidad = "", imc = "", imc_unidad = "";
            if (antro != null)
            {
                peso = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID).v_Value1;
                peso_unidad = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID).v_MeasurementUnitName;
                talla = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID).v_Value1;
                talla_unidad = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID).v_MeasurementUnitName;
                imc = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID).v_Value1;
                imc_unidad = antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID) == null ? "" : antro.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID).v_MeasurementUnitName;

            }
            else
            {
                peso = "";
                peso_unidad = "";
                talla = "";
                talla_unidad = "";
                imc = "";
                imc_unidad = "";
            }

            ServiceComponentList funcVit = exams.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FUNCIONES_VITALES_ID);
            string temp = "", temp_unidad = "", pres_Sist = "", pres_Diast = "", pres_Diast_unidad = "", frecCard = "", frecCard_unidad = "", frecResp = "", frecResp_unidad = "", spo2 = "", spo2_unidad = "";
            if (funcVit != null)
            {
                temp = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID).v_Value1;
                temp_unidad = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID).v_MeasurementUnitName;
                pres_Sist = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID).v_Value1;
                pres_Diast = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID).v_Value1;
                pres_Diast_unidad = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID).v_MeasurementUnitName;
                frecCard = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).v_Value1;
                frecCard_unidad = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).v_MeasurementUnitName;
                frecResp = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).v_Value1;
                frecResp_unidad = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).v_MeasurementUnitName;
                spo2 = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID).v_Value1;
                spo2_unidad = funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID) == null ? "" : funcVit.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID).v_MeasurementUnitName;

            }
            else
            {
                temp = "";
                temp_unidad = "";
                pres_Diast = "";
                pres_Diast_unidad = "";
                pres_Sist = "";
                frecCard = "";
                frecCard_unidad = "";
                frecResp = "";
                frecResp_unidad = "";
                spo2 = "";
                spo2_unidad = "";
            }



            cellsTit = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=2f, Border = PdfPCell.NO_BORDER},

                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("1. ANAMNESIS (la información que se brinda tendrá el carácter de declaración jurada)", fontColumnValueBold)) {Colspan = 18,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
     
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Ha presentado o padece: ", fontColumnValueBold)) {Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("SI", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Ha presentado o padece:", fontColumnValueBold)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase("SI", fontColumnValueBold)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Alteración de la conciencia (pérdida del conocimiento) ", fontColumnValue)) {Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_1 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_1 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Cirugía cardiaca", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_2 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_2 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Convulsiones / epilepsia ", fontColumnValue)) {Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_3 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_3 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Arritmias cardiacas", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_4 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_4 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Apnea del sueño ", fontColumnValue)) {Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_5 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_5 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Portador de marcapasos/ prótesis valvulares", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_6 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_6 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Movimientos involuntarios / Temblores", fontColumnValue)) {Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_7 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_7 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Diabetes Mellitus", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_8 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_8 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Vértigo", fontColumnValue)) {Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_9 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_9 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Asma Bronquial / EPOC", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_10 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_10 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Alteraciones de la visión", fontColumnValue)) {Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_11 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_11 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Insuficiencia renal crónica", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_12 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_12 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Disminución de la audición", fontColumnValue)) {Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_13 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_13 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Secuela de fracturas", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_14 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_14 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Hipertensión Arterial", fontColumnValue)) {Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_15 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_15 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Abuso de Alcohol", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_16 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_16 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Insuficiencia Cardiaca", fontColumnValue)) {Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_17 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_17 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Consumo de drogas", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_18 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_18 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Anginas / Insuficiencia coronaria", fontColumnValue)) {Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_19 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_19 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Depresión / ansiedad / fobias / ataques de pánico", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_20 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_20 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Uso actual de medicamentos que alteren la atención, habilidad motriz, la percepción u otro:", fontColumnValueBold)) {Colspan = 18,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_1_21, fontColumnValue)) { Colspan=18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=tamaño_celda, Border = PdfPCell.BOTTOM_BORDER},
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=5f, Border = PdfPCell.NO_BORDER},

                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("2. EXAMEN FÍSICO", fontColumnValueBold)) {Colspan = 18,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("FC: ", fontColumnValueBold)) {Colspan = 1,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(frecCard, fontColumnValueBold)) {Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER},    
                new PdfPCell(new Phrase("FR: ", fontColumnValueBold)) {Colspan = 1,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(frecResp, fontColumnValueBold)) {Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER},    
                new PdfPCell(new Phrase("PA: ", fontColumnValueBold)) {Colspan = 1,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(pres_Sist+"/"+pres_Diast, fontColumnValueBold)) {Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER},    
                new PdfPCell(new Phrase("Peso: ", fontColumnValueBold)) {Colspan = 1,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(peso, fontColumnValueBold)) {Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER},    
                new PdfPCell(new Phrase("Talla: ", fontColumnValueBold)) {Colspan = 1,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(talla, fontColumnValueBold)) {Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER},    
                new PdfPCell(new Phrase("IMC: ", fontColumnValueBold)) {Colspan = 1,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(imc, fontColumnValueBold)) {Colspan = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.BOTTOM_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=5f, Border = PdfPCell.NO_BORDER},

                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("SI", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase("SI", fontColumnValueBold)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Ausencia de Nistagmus", fontColumnValue)) {Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Diadocoquinesia indirecta", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Sustentación en un pier por 15\"", fontColumnValue)) {Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_3 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_3 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Sinetría facial", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_4 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_4 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Prueba de Romberg negativa", fontColumnValue)) {Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_5 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_5 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Fuerza y movilidad de extremidades conservadas", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_6 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_6 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Coordinación conservada (índice-índice / índice-nariz)", fontColumnValue)) {Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_7 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_7 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Ausencia de movimientos involuntarios (Corea/coreo atetosis/parkinsinismo)", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_8 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_8 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Marcha normal con los ojos abiertos y/o cerrados.", fontColumnValue)) {Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_9 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_9 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Ausencia de lesiones deformantes en columna vertebral", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_10 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_10 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Diadoquinesia directa", fontColumnValue)) {Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_11 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_11 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Ausencia de lesiones deformantes en extremidades", fontColumnValue)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda},    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_12 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_12 == "0"?"X":string.Empty, fontColumnValue)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Otros hallazgos: ", fontColumnValue)) {Colspan = 7,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_13, fontColumnValue)) {Colspan = 11, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    


                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("El médico que suscribe certifica que después de la revisión del expediente médico: ", fontColumnValueBold)) {Colspan = 18,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_1 == "1"?"(X) Examen Clinico (F-01)":"( ) Examen Clinico (F-01)", fontColumnValue)) {Colspan = 9,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_2 == "1"?"(X) Evaluación Psicológica (F-06)":"( ) Evaluación Psicológica (F-06)", fontColumnValue)) {Colspan = 9,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_3 == "1"?"(X Examen oftalmológico (F-04)":"( ) Examen oftalmológico (F-04)", fontColumnValue)) {Colspan = 9,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_4 == "1"?"(X) Test de Acrofobia (en trabajador nuevo)":"( ) Test de Acrofobia (en trabajador nuevo)", fontColumnValue)) {Colspan = 9,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_5 == "1"?"(X) Examen audiológico (F-05)":"( ) Examen audiológico (F-05)", fontColumnValue)) {Colspan = 9,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_6 == "1"?"(X) Test de Claustrofobia (en trabajador nuevo)":"( ) Test de Claustrofobia (en trabajador nuevo)", fontColumnValue)) {Colspan = 9,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_1_5 == "1"?"(X) Coordinación motriz (Ex. Psicosensométrico)":"( ) Coordinación motriz (Ex. Psicosensométrico)", fontColumnValue)) {Colspan = 18,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("El / La postulante / trabajador(a), se encuentra: ", fontColumnValueBold)) {Colspan = 18,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_1 == string.Empty?string.Empty:CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_1, fontColumnValue)) {Colspan = 3,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Para realizar trabajos en altura y en distintos niveles", fontColumnValue)) {Colspan = 15,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_2 == string.Empty?string.Empty:CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_2, fontColumnValue)) {Colspan = 3,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Para realizar trabajos en espacios confinados", fontColumnValue)) {Colspan = 15,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_3 == string.Empty?string.Empty:CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_3, fontColumnValue)) {Colspan = 3,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Para realizar trabajos en Alta tensión", fontColumnValue)) {Colspan = 15,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_4 == string.Empty?string.Empty:CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_4, fontColumnValue)) {Colspan = 3,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Para realizar trabajos en caliente", fontColumnValue)) {Colspan = 15,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_5 == string.Empty?string.Empty:CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_5, fontColumnValue)) {Colspan = 3,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Para realizar trabajos de instalación, operación, manejo de equipos y materiales radioactivos.", fontColumnValue)) {Colspan = 15,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 3,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("Otros: " + CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_2_2_6, fontColumnValue)) {Colspan = 15,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },    

            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);

            document.Add(table);
            #endregion

            document.NewPage();
            #region TÍTULO

            cells = new List<PdfPCell>();

            if (infoEmpresaPropietaria.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image));
                imagenEmpresa.ScalePercent(25);
                imagenEmpresa.SetAbsolutePosition(40, 790);
                document.Add(imagenEmpresa);
            }
            
            #endregion

            #region EVALUACION 2
            var tamaño_celda_2 = 16f;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_1_1 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_1_1) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_1_1).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_1_2 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_1_2) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_1_2).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_1_3 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_1_3) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_1_3).v_Value1;

            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_1 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_1) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_1).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_2 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_2) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_2).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_3 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_3) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_3).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_4 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_4) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_4).v_Value1;

            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_4_1 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_4_1) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_4_1).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_4_2 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_4_2) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_4_2).v_Value1;

            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_1 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_1) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_1).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_2 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_2) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_2).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_3 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_3) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_3).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_4 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_4) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_4).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_5 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_5) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_5).v_Value1;
            var CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_6 = certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_6) == null ? "" : certificadoRiesgo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_6).v_Value1;


            cellsTit = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=2f, Border = PdfPCell.NO_BORDER},

                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("3. EXÁMENES AUXILIARES RELACIONADOS", fontColumnValueBold)) {Colspan = 18,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
     
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("RESULTADOS DE LABORATORIO", fontColumnValueBold)) {Colspan = 16,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("SI", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Glicemia en ayunas normal", fontColumnValue)) {Colspan = 16,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_1_1=="1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_1_1=="0"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Hemoglobina normal", fontColumnValue)) {Colspan = 16,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_1_2=="1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_1_2=="0"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Toxicológico negativo", fontColumnValue)) {Colspan = 16,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_1_3=="1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_1_3=="0"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=5f, Border = PdfPCell.NO_BORDER},

                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("AUDIOMETRÍA EN CABINA", fontColumnValueBold)) {Colspan = 16,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("SI", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("AGUDEZA VISUAL: 20/20 y 20/30 o mejor", fontColumnValue)) {Colspan = 16,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_1=="1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_1=="0"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("AUDIOMETRIA: No presenta hipoacusia avanzada bilateral", fontColumnValue)) {Colspan = 16,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_2=="1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_2=="0"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("PSICOSENSOMÉTRICO: Coordinación motriz aprobada", fontColumnValue)) {Colspan = 16,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_3=="1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_3=="0"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("ERGOMETRÍA: sin alteraciones patológicas", fontColumnValue)) {Colspan = 14,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_4=="2"?"NA (X)":"NA ( )", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_4=="1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_3_2_4=="0"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=5f, Border = PdfPCell.NO_BORDER},

                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("4. EVALUACIÓN PSICOLÓGICA", fontColumnValueBold)) {Colspan = 18,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
     
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("Test de Acrofobia:", fontColumnValueBold)) {Colspan = 4,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_4_1=="0"?"Aprobado ( ) Desaprobado (X)\n NA ( )":CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_4_1=="1"?"Aprobado (X) Desaprobado ( )\n NA ( )":CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_4_1=="2"?"Aprobado ( ) Desaprobado (X)\n NA (X)":"Aprobado ( ) Desaprobado ( )\n NA ( )", fontColumnValue)) {Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("Test de Claustrofobia:", fontColumnValueBold)) {Colspan = 4,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_4_2=="0"?"Aprobado ( ) Desaprobado (X)\n NA ( )":CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_4_2=="1"?"Aprobado (X) Desaprobado ( )\n NA ( )":CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_4_2=="2"?"Aprobado ( ) Desaprobado (X)\n NA (X)":"Aprobado ( ) Desaprobado ( )\n NA ( )", fontColumnValue)) {Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=2f, Border = PdfPCell.NO_BORDER},

                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("5. APTITUD PARA REALIZAR TRABAJOS CRÍTICOS (de acuerdo con el item 4.7 Estándar SSOstO021)", fontColumnValueBold)) {Colspan = 18,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
     
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1, Rowspan  = 2 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 14f, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 15, Rowspan  = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 14f },    
                new PdfPCell(new Phrase("APTITUD", fontColumnValueBold)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 14f },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 , Rowspan  = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 14f, Border = PdfPCell.NO_BORDER },    


                new PdfPCell(new Phrase("SI", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 14f },    
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 14f },    
                new PdfPCell(new Phrase("NA", fontColumnValueBold)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 14f },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("ALTURA O DISTINTOS NIVELES A 1.8m. ", fontColumnValue)) {Colspan = 15,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_1 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_1 == "0"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_1 == "2"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("ESPACIOS CONINADOS / INMERSIÓN BAJO EL AGUA ", fontColumnValue)) {Colspan = 15,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_2 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_2 == "0"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_2 == "2"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("BRIGADISTA", fontColumnValue)) {Colspan = 15,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_3 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_3 == "0"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_3 == "2"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan=20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight=2f, Border = PdfPCell.NO_BORDER},

                 new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1, Rowspan  = 2 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 14f, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 15, Rowspan  = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 14f },    
                new PdfPCell(new Phrase("APTITUD", fontColumnValueBold)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 14f },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1 , Rowspan  = 2,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 14f, Border = PdfPCell.NO_BORDER },    


                new PdfPCell(new Phrase("SI", fontColumnValueBold)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 14f },    
                new PdfPCell(new Phrase("NO", fontColumnValueBold)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 14f },    
                new PdfPCell(new Phrase("NA", fontColumnValueBold)) {  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = 14f },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("MANIOBRISTA DE CARGA / RIGGER", fontColumnValue)) {Colspan = 15,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_4 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_4 == "0"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_4 == "2"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("TRABAJO CON TENSIÓN ", fontColumnValue)) {Colspan = 15,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_5 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_5 == "0"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_5 == "2"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    

                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    
                new PdfPCell(new Phrase("TRABAJOS EN CALIENTE", fontColumnValue)) {Colspan = 15,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_6 == "1"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_6 == "0"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase(CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_5_6 == "2"?"X":string.Empty, fontColumnValue)) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2 },    
                new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 1 ,  HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda_2, Border = PdfPCell.NO_BORDER },    


            };

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);

            document.Add(table);
            #endregion


            #region DIAGNOSTICOS


            var filterDiagnosticRepository = Diagnosticos.FindAll(p => p.Categoria == "MEDICINA");
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("", fontColumnValueBold_SubTitulo)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
                new PdfPCell(new Phrase("6. DIAGNÓSTICO:", fontColumnValueBold_SubTitulo)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
                new PdfPCell(new Phrase("", fontColumnValueBold_SubTitulo)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
            };
            columnWidths = new float[] { 5f, 90f, 5f };
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
                columnWidths = new float[] { 5f, 90f, 5f };
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
                new PdfPCell(new Phrase("7. RECOMENDACIONES:", fontColumnValueBold_SubTitulo)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
                new PdfPCell(new Phrase("", fontColumnValueBold_SubTitulo)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, Border = PdfPCell.NO_BORDER },       
 
            };
            columnWidths = new float[] { 5f, 90f, 5f };
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
                cell = new PdfPCell(new Phrase("", fontColumnValueBold_Dx)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, MinimumHeight = 20f, Border = PdfPCell.NO_BORDER };
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
                columnWidths = new float[] { 5f, 90f, 5f };
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

            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
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
                new PdfPCell(cellFirmaTrabajador){ HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = PdfPCell.BOTTOM_BORDER},
                new PdfPCell(new Phrase("" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 30f},
                new PdfPCell(cellHuellaTrabajador){ HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                new PdfPCell(new Phrase("" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_LEFT,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 30f},
                new PdfPCell(new Phrase("FIRMA DEL MEDICO EVALUADOR" , fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_CENTER,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  Border = PdfPCell.NO_BORDER, MinimumHeight = 30f},
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
            document.Close();
            writer.Close();
            writer.Dispose();
        }
    }
}
