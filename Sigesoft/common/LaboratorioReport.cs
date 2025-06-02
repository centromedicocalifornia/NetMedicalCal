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
    public class LaboratorioReport
    {
        #region Report de Laboratorio
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }
        public static void CreateLaboratorioReport(PacientList filiationData,
            List<ServiceComponentList> serviceComponent,
            organizationDto infoEmpresaPropietaria,
            string filePDF)
        {
            //Document document = new Document();
            // document.SetPageSize(iTextSharp.text.PageSize.A4);
            //Document document = new Document(PageSize.A4, 30f, 30f, 20f, 40f);
            Document document = new Document(PageSize.A4, 30f, 30f, 90f, 65f);

            try
            {
                //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
                //pdfPage page = new pdfPage();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
                pdfPage_NEW page = new pdfPage_NEW();

                page.Dato = "ILAB_CLINICO/" + filiationData.v_FirstName + " " + filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName;
                writer.PageEvent = page;
                page.Title = string.Empty;

                document.Open();

                #region Fonts
                Font fontTitle1 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontTitle2 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

                Font fontColumnValue = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontColumnValueBold = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontColumnValueBold2 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontColumnValueBold3 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));

                
                #endregion

                #region Declaration Tables
                var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.White);
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
                PdfPTable cabecera = null;
                PdfPCell cell = null;
                //document.Add(new Paragraph("\r"));
                #endregion
 
                cells = new List<PdfPCell>();
                var tamaño_celda = 13f;

                #region DATOS GENERALES
                string medico = "";
              
                string sexo = "";
                if (filiationData.v_SexTypeName == "MASCULINO") sexo = "M";
                else if (filiationData.v_SexTypeName == "FEMENINO") sexo = "F";

                string empresageneral = filiationData.empresa_;
                string empresacontrata = filiationData.contrata;
                string empresasubcontrata = filiationData.subcontrata;

                string empr_Conct = "";
                if (empresageneral != empresasubcontrata) empr_Conct = empresacontrata + " / " + empresasubcontrata;
                else empr_Conct = empresacontrata;

                cells = new List<PdfPCell>()
                {
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("LABORATORIO CLÍNICO", fontColumnValueBold2)) { Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValueBold2)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda, Border = PdfPCell.NO_BORDER},    

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },    
                new PdfPCell(new Phrase("PACIENTE : " + filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName + " " + filiationData.v_FirstName, fontColumnValueBold3)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },    
                new PdfPCell(new Phrase("Sexo : " + sexo, fontColumnValueBold3)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },    
                new PdfPCell(new Phrase(" ", fontColumnValueBold2)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },    

                new PdfPCell(new Phrase("", fontColumnValueBold2)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },    
                new PdfPCell(new Phrase("FECHA DE RECEPCIÓN : " + filiationData.d_ServiceDate.Value.ToShortDateString() , fontColumnValueBold3)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },    
                new PdfPCell(new Phrase("FECHA DE EMISIÓN : " + filiationData.d_ServiceDate.Value.ToShortDateString(), fontColumnValueBold3)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },    

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },    
                new PdfPCell(new Phrase("EDAD : " + filiationData.i_Age.Value.ToString() + " AÑOS" , fontColumnValueBold3)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },    
                new PdfPCell(new Phrase("DNI : " + filiationData.v_DocNumber, fontColumnValueBold3)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },    

                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },    
                new PdfPCell(new Phrase("CARGO : " + filiationData.v_CurrentOccupation, fontColumnValueBold3)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE },    
                new PdfPCell(new Phrase("CIA: " + empr_Conct, fontColumnValueBold3)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda,  UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },    
                
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 8f, Border = PdfPCell.NO_BORDER},    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 8f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 8f, Border = PdfPCell.NO_BORDER},    

            };

                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);
                #endregion

                float tamaño_caldas = 13f;
                var tamaño_celda_1 = 13f;
                var tamaño_cabecera = 15f;
                cells = new List<PdfPCell>()
                    {

                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("EXAMEN", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        new PdfPCell(new Phrase("VALOR DE REFERENCIA", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                     };

                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);

                #region Bioquimica
                var xTrigliceridos = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TRIGLICERIDOS_ID_ASIST);
                var xColesterol = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.COLESTEROL_ID);
                var xColesterolHDL = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.COLESTEROL_HDL_ID_OC);
                var xColesterolLDL = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.COLESTEROL_LDL_ID_OC);
                var xGlucosa = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GLUCOSA_ID);
                var xPerfilLipidico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PERFIL_LIPIDICO);
                var xPerfilHepatico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PERFIL_HEPATICO_ID);
                var xAcidoUrico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.BIOQUIMICA01_ID);

                var xUrea = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.UREA_ID_ASIST);
                var xCreatinina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CREATININA_ID);

                var tgoo = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.LABORATORIO_TGO_ID);
                var tgpp = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.LABORATORIO_TGP_ID);

                var fosfatasa_alcalina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FOSFATASA_ALCALINA_ID_OC);
                var bilirrubina_total_fraccionada = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.BILIRRUBINA_TOTAL_Y_FRACCIONADA_OC_ID);
                var proteina_total_fraccionada = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PROTEINAS_TOTAL_Y_FRACCIONADA_ID_OC);
                var ggt = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GAMMA_GLUTAMIL_ID);


                if (xTrigliceridos != null || xColesterol != null || xColesterolHDL != null || xColesterolLDL != null ||
                    xGlucosa != null || xPerfilLipidico != null || xPerfilHepatico != null || xAcidoUrico != null ||
                    xUrea != null || xCreatinina != null || tgoo != null || tgpp != null || fosfatasa_alcalina != null ||
                    bilirrubina_total_fraccionada != null || proteina_total_fraccionada != null || ggt != null)
                {
                    cells = new List<PdfPCell>()
                    {

                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("BIOQUÍMICA AUTOMATIZADA", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE },    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("METODOLOGIA: ENZIMATICO/COLORIMETRICO", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE },    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f, Border = PdfPCell.NO_BORDER},    

                     };

                    columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);

                    if (xGlucosa != null)
                    {
                        cells = new List<PdfPCell>();
                        var glucosa = xGlucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GLUCOSA_GLUCOSA_VALOR_RESULTADO_ID);
                        var glucosaValord = xGlucosa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GLUCOSA_GLUCOSA_VALOR_DESEABLE_ID);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("GLUCOSA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(glucosa == null ? string.Empty : glucosa.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(glucosa == null ? string.Empty : glucosa.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(glucosaValord == null ? string.Empty : glucosaValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                    if (xCreatinina != null)
                    {
                        cells = new List<PdfPCell>();
                        var creatinina = xCreatinina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CREATININA_BIOQUIMICA_CREATININA);
                        var creatininaDeseable = xCreatinina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CREATININA_BIOQUIMICA_CREATININA_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("CREATININA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(creatinina == null ? string.Empty : creatinina.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(creatinina == null ? string.Empty : creatinina.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(creatininaDeseable == null ? string.Empty : creatininaDeseable.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }


                    if (xUrea != null)
                    {
                        cells = new List<PdfPCell>();
                        var urea = xUrea.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.UREA_BIOQUIMICA_UREA);
                        var ureaDeseable = xUrea.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.UREA_BIOQUIMICA_UREA_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("UREA SERICA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(urea == null ? string.Empty : urea.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(urea == null ? string.Empty : urea.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ureaDeseable == null ? string.Empty : ureaDeseable.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                   
                    if (xAcidoUrico != null)
                    {
                        cells = new List<PdfPCell>();
                        var acidourico = xAcidoUrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ACIDO_URICO_BIOQUIMICA_ACIDO_URICO);
                        var acidouricoValord = xAcidoUrico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ACIDO_URICO_BIOQUIMICA_ACIDO_URICO_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("ÁCIDO ÚRICO", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(acidourico == null ? string.Empty : acidourico.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(acidourico == null ? string.Empty : acidourico.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(acidouricoValord == null ? string.Empty : acidouricoValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                    if (xColesterol != null)
                    {
                        cells = new List<PdfPCell>();
                        var colesterol = xColesterol.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_COLESTEROL_TOTAL_ID);
                        var colesterolValord = xColesterol.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_COLESTEROL_TOTAL_DESEABLE_ID);


                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("COLESTEROL", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesterol == null ? string.Empty : colesterol.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesterol == null ? string.Empty : colesterol.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesterolValord == null ? string.Empty : colesterolValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                    if (xTrigliceridos != null)
                    {
                        cells = new List<PdfPCell>();
                        var Triglicerido = xTrigliceridos.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS);
                        var TrigliceridoValord = xTrigliceridos.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("TRIGLICÉRIDOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Triglicerido == null ? string.Empty : Triglicerido.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Triglicerido == null ? string.Empty : Triglicerido.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(TrigliceridoValord == null ? string.Empty : TrigliceridoValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }



                    if (xColesterolHDL != null)
                    {
                        cells = new List<PdfPCell>();
                        var colesterol = xColesterolHDL.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_HDL_VALOR_OC);
                        var colesterolValord = xColesterolHDL.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_HDL_VALOR_DESEABLE_OC);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("COLESTEROL HDL", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesterol == null ? string.Empty : colesterol.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesterol == null ? string.Empty : colesterol.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesterolValord == null ? string.Empty : colesterolValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }
                    if (xColesterolLDL != null)
                    {
                        cells = new List<PdfPCell>();
                        var colesterol = xColesterolLDL.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_LDL_VALOR_OC);
                        var colesterolValord = xColesterolLDL.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_LDL_VALOR_DESEABLE_OC);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("COLESTEROL LDL", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesterol == null ? string.Empty : colesterol.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesterol == null ? string.Empty : colesterol.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesterolValord == null ? string.Empty : colesterolValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                    if (tgoo != null)
                    {
                        cells = new List<PdfPCell>();

                        var tgo_Val = tgoo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LABORATORIO_TGO_VALOR);
                        var tgoValord = tgoo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LABORATORIO_TGO_VALORDESEABLE_VALOR);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("TRANSAMINASA (TGO)", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(tgo_Val == null ? string.Empty : tgo_Val.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(tgo_Val == null ? string.Empty : tgo_Val.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(tgoValord == null ? "0 - 45" : tgoValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                    if (tgpp != null)
                    {
                        cells = new List<PdfPCell>();

                        var tgp_val = tgpp.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LABORATORIO_TGP_VALOR);
                        var tgp_Valord = tgpp.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LABORATORIO_TGP_VALORDESEABLE_VALOR);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("TRANSAMINASA (TGP)", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(tgp_val == null ? string.Empty : tgp_val.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(tgp_val == null ? string.Empty : tgp_val.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(tgp_Valord == null ? "0 - 45" : tgp_Valord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);

                    }




                    if (fosfatasa_alcalina != null)
                    {
                        var fosfatasa_alcalina_valor = fosfatasa_alcalina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FOSFATASA_ALCALINA_VALOR_OC) == null ? "- - -" : fosfatasa_alcalina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FOSFATASA_ALCALINA_VALOR_OC).v_Value1;
                        var fosfatasa_alcalina_valor_unidad = fosfatasa_alcalina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FOSFATASA_ALCALINA_VALOR_OC) == null ? "- - -" : fosfatasa_alcalina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FOSFATASA_ALCALINA_VALOR_OC).v_MeasurementUnitName;
                        var fosfatasa_alcalina_valor_deseado = fosfatasa_alcalina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FOSFATASA_ALCALINA_VALOR_DESEABLE_OC) == null ? "- - -" : fosfatasa_alcalina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FOSFATASA_ALCALINA_VALOR_DESEABLE_OC).v_Value1;

                        cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("FOSFATASA ALCALINA:", fontColumnValue)) {Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase(fosfatasa_alcalina_valor, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER  },    
                        new PdfPCell(new Phrase(fosfatasa_alcalina_valor_unidad, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase(fosfatasa_alcalina_valor_deseado, fontColumnValue)) {Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    

                     };

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                    if (bilirrubina_total_fraccionada != null)
                    {
                        var BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_DIRECTA_valor = bilirrubina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_DIRECTA_OC) == null ? "- - -" : bilirrubina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_DIRECTA_OC).v_Value1;
                        var BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_DIRECTA_valor_unidad = bilirrubina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_DIRECTA_OC) == null ? "- - -" : bilirrubina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_DIRECTA_OC).v_MeasurementUnitName;
                        var BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_DIRECTA_VALOR_DESEABLE_valor_deseado = bilirrubina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_DIRECTA_VALOR_DESEABLE_OC) == null ? "- - -" : bilirrubina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_DIRECTA_VALOR_DESEABLE_OC).v_Value1;

                        var BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_INDIRECTA_valor = bilirrubina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_INDIRECTA_OC) == null ? "- - -" : bilirrubina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_INDIRECTA_OC).v_Value1;
                        var BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_INDIRECTA_valor_unidad = bilirrubina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_INDIRECTA_OC) == null ? "- - -" : bilirrubina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_INDIRECTA_OC).v_MeasurementUnitName;
                        var BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_INDIRECTA_VALOR_DESEABLE_valor_deseado = bilirrubina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_INDIRECTA_VALOR_DESEABLE_OC) == null ? "- - -" : bilirrubina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_INDIRECTA_VALOR_DESEABLE_OC).v_Value1;

                        var BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_TOTAL_valor = bilirrubina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_TOTAL_OC) == null ? "- - -" : bilirrubina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_TOTAL_OC).v_Value1;
                        var BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_TOTAL_valor_unidad = bilirrubina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_TOTAL_OC) == null ? "- - -" : bilirrubina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_TOTAL_OC).v_MeasurementUnitName;
                        var BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_TOTAL_VALOR_DESEABLE_valor_deseado = bilirrubina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_TOTAL_VALOR_DESEABLE_OC) == null ? "- - -" : bilirrubina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_TOTAL_VALOR_DESEABLE_OC).v_Value1;

                        cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase("BILIRRUBINA DIRECTA:", fontColumnValue)) {Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase(BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_DIRECTA_valor, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase(BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_DIRECTA_valor_unidad, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                            new PdfPCell(new Phrase(BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_DIRECTA_VALOR_DESEABLE_valor_deseado, fontColumnValue)) {Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                            new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    

                            new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase("BILIRRUBINA INDIRECTA:", fontColumnValue)) {Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                            new PdfPCell(new Phrase(BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_INDIRECTA_valor, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                            new PdfPCell(new Phrase(BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_INDIRECTA_valor_unidad, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                            new PdfPCell(new Phrase(BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_INDIRECTA_VALOR_DESEABLE_valor_deseado, fontColumnValue)) {Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    

                            new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase("BILIRRUBINA TOTAL:", fontColumnValue)) {Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                            new PdfPCell(new Phrase(BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_TOTAL_valor, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase(BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_TOTAL_valor_unidad, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                            new PdfPCell(new Phrase(BILIRRUBINA_TOTAL_Y_FRACCIONADA_B_TOTAL_VALOR_DESEABLE_valor_deseado, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                            new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    

                         };

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                    if (proteina_total_fraccionada != null)
                    {
                        var PROTEINAS_TOTAL_Y_FRACCIONADA_ALBUMINA_valor = proteina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTEINAS_TOTAL_Y_FRACCIONADA_ALBUMINA_OC) == null ? "- - -" : proteina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTEINAS_TOTAL_Y_FRACCIONADA_ALBUMINA_OC).v_Value1;
                        var PROTEINAS_TOTAL_Y_FRACCIONADA_ALBUMINA_valor_unidad = proteina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTEINAS_TOTAL_Y_FRACCIONADA_ALBUMINA_OC) == null ? "- - -" : proteina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTEINAS_TOTAL_Y_FRACCIONADA_ALBUMINA_OC).v_MeasurementUnitName;
                        var PROTEINAS_TOTAL_Y_FRACCIONADA_ALBUMINA_VALOR_DESEABLE_valor_deseado = proteina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTEINAS_TOTAL_Y_FRACCIONADA_ALBUMINA_VALOR_DESEABLE_OC) == null ? "- - -" : proteina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTEINAS_TOTAL_Y_FRACCIONADA_ALBUMINA_VALOR_DESEABLE_OC).v_Value1;

                        var PROTEINAS_TOTAL_Y_FRACCIONADA_GLOBULINA_valor = proteina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTEINAS_TOTAL_Y_FRACCIONADA_GLOBULINA_OC) == null ? "- - -" : proteina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTEINAS_TOTAL_Y_FRACCIONADA_GLOBULINA_OC).v_Value1;
                        var PROTEINAS_TOTAL_Y_FRACCIONADA_GLOBULINA_valor_unidad = proteina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTEINAS_TOTAL_Y_FRACCIONADA_GLOBULINA_OC) == null ? "- - -" : proteina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTEINAS_TOTAL_Y_FRACCIONADA_GLOBULINA_OC).v_MeasurementUnitName;
                        var PROTEINAS_TOTAL_Y_FRACCIONADA_GLOBULINA_VALOR_DESEABLE_valor_deseado = proteina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTEINAS_TOTAL_Y_FRACCIONADA_GLOBULINA_VALOR_DESEABLE_OC) == null ? "- - -" : proteina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTEINAS_TOTAL_Y_FRACCIONADA_GLOBULINA_VALOR_DESEABLE_OC).v_Value1;

                        var PROTEINAS_TOTAL_Y_FRACCIONADA_PROTEINAS_TOT_valor = proteina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTEINAS_TOTAL_Y_FRACCIONADA_PROTEINAS_TOT_OC) == null ? "- - -" : proteina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTEINAS_TOTAL_Y_FRACCIONADA_PROTEINAS_TOT_OC).v_Value1;
                        var PROTEINAS_TOTAL_Y_FRACCIONADA_PROTEINAS_TOT_valor_unidad = proteina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTEINAS_TOTAL_Y_FRACCIONADA_PROTEINAS_TOT_OC) == null ? "- - -" : proteina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTEINAS_TOTAL_Y_FRACCIONADA_PROTEINAS_TOT_OC).v_MeasurementUnitName;
                        var PROTEINAS_TOTAL_Y_FRACCIONADA_PROTEINAS_TOT_VALOR_DESEABLE_valor_deseado = proteina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTEINAS_TOTAL_Y_FRACCIONADA_PROTEINAS_TOT_VALOR_DESEABLE_OC) == null ? "- - -" : proteina_total_fraccionada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTEINAS_TOTAL_Y_FRACCIONADA_PROTEINAS_TOT_VALOR_DESEABLE_OC).v_Value1;

                        cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase("PROTEINAS TOTALES:", fontColumnValue)) {Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase(PROTEINAS_TOTAL_Y_FRACCIONADA_PROTEINAS_TOT_valor, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase(PROTEINAS_TOTAL_Y_FRACCIONADA_PROTEINAS_TOT_valor_unidad, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                            new PdfPCell(new Phrase(PROTEINAS_TOTAL_Y_FRACCIONADA_PROTEINAS_TOT_VALOR_DESEABLE_valor_deseado, fontColumnValue)) {Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    

                            new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase("ALBUMINA:", fontColumnValue)) {Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase(PROTEINAS_TOTAL_Y_FRACCIONADA_ALBUMINA_valor, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase(PROTEINAS_TOTAL_Y_FRACCIONADA_ALBUMINA_valor_unidad, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase(PROTEINAS_TOTAL_Y_FRACCIONADA_ALBUMINA_VALOR_DESEABLE_valor_deseado, fontColumnValue)) {Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    

                            new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase("GLOBULINA:", fontColumnValue)) {Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                            new PdfPCell(new Phrase(PROTEINAS_TOTAL_Y_FRACCIONADA_GLOBULINA_valor, fontColumnValue)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                            new PdfPCell(new Phrase(PROTEINAS_TOTAL_Y_FRACCIONADA_GLOBULINA_valor_unidad, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase(PROTEINAS_TOTAL_Y_FRACCIONADA_GLOBULINA_VALOR_DESEABLE_valor_deseado, fontColumnValue)) {Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER },    
                            new PdfPCell(new Phrase("", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER},    

                            
                        };

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                    if (ggt != null)
                    {
                        cells = new List<PdfPCell>();


                        var ggt_val = ggt.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GAMMA_GLUTAMIL_VALOR_RESULTADO);
                        var ggt_Valord = ggt.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GAMMA_GLUTAMIL_VALOR_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("GAMMA GLUTAMIL TRANSPEPTIDASA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ggt_val == null ? string.Empty : ggt_val.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ggt_val == null ? string.Empty : ggt_val.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ggt_Valord == null ? "< 71" : ggt_Valord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }


                    if (xPerfilLipidico != null)
                    {
                        #region PERFIL_LIPIDICO

                        cells = new List<PdfPCell>();
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("PERFIL LIPIDICO:", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });


                        var colesteroltotal = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_TOTAL);
                        var colesteroltotalValord = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_TOTAL_DESEABLE);

                        var trigliceridos = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS);
                        var trigliceridosValord = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_DESEABLE);

                        var colesterolldl = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_LDL);
                        var colesterolldlValord = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_LDL_DESEABLE);

                        var colesterolhdl = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_HDL);
                        var colesterolhdlValord = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_HDL_DESEABLE);

                        var colesterolvldl = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_VLDL);
                        var colesterolvldlValord = xPerfilLipidico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_VLDL_DESEABLE);

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("COLESTEROL TOTAL", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesteroltotal == null ? string.Empty : colesteroltotal.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesteroltotal == null ? string.Empty : colesteroltotal.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesteroltotalValord == null ? string.Empty : colesteroltotalValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("TRIGLICÉRIDOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(trigliceridos == null ? string.Empty : trigliceridos.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(trigliceridos == null ? string.Empty : trigliceridos.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(trigliceridosValord == null ? string.Empty : trigliceridosValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("COLESTEROL HDL", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesterolhdl == null ? string.Empty : colesterolhdl.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesterolhdl == null ? string.Empty : colesterolhdl.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesterolhdlValord == null ? string.Empty : colesterolhdlValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("COLESTEROL LDL", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesterolldl == null ? string.Empty : colesterolldl.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesterolldl == null ? string.Empty : colesterolldl.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesterolldlValord == null ? string.Empty : colesterolldlValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        
                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("COLESTEROL VLDL", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesterolvldl == null ? string.Empty : colesterolvldl.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesterolvldl == null ? string.Empty : colesterolvldl.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(colesterolvldlValord == null ? string.Empty : colesterolvldlValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        #endregion
                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                    if (xPerfilHepatico != null)
                    {
                        #region PERFIL_HEPATICO_ID

                        if (xPerfilHepatico.ServiceComponentFields.Count > 0)
                        {
                            cells = new List<PdfPCell>();

                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("PERFIL HEPÁTICO", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                            var proteinastotales = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_PROTEINAS_TOTALES_ID);
                            var proteinastotalesValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_PROTEINA_TOTALES_DESEABLE_ID);

                            var albumina = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_ALBUMINA_ID);
                            var albuminaValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_ALBUMINA_DESEABLE_ID);

                            var globulina = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_GLOBULINA_ID);
                            var globulinaValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_GLOBULINA_DESEABLE_ID);

                            var tgo = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_TGO_ID);
                            var tgoValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_TGO_DESEABLE_ID);

                            var tgp = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_TGP_ID);
                            var tgpValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_TGP_DESEABLE_ID);

                            var fosfataalcalina = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_FOSFATASA_ALCALINA_ID);
                            var fosfataalcalinaValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_FOSFATASA_ALCALINA_DESEABLE_ID);

                            var gamma = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_GAMMA_GLUTAMIL_TRANSPEPTIDASA_ID);
                            var gammaValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_GGTP_DESEABLE_ID);

                            var btotal = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_BILIRRUBINA_TOTAL_ID);
                            var btotalValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_BILIRRUBINA_TOTAL_DESEABLE_ID);

                            var bdirecta = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_BILIRRUBINA_DIRECTA_ID);
                            var bdirectaValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_BILIRRUBINA_DIRECTA_DESEABLE_ID);

                            var bindirecta = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_BILIRRUBINA_INDIRECTA_ID);
                            var bindirectaValord = xPerfilHepatico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PERFIL_HEPATICO_BILIRRUBINA_INDIRECTA_DESEABLE_ID);

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("TGO", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(tgo == null ? string.Empty : tgo.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(tgo == null ? string.Empty : tgo.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(tgoValord == null ? string.Empty : tgoValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("TGP", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(tgp == null ? string.Empty : tgp.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(tgp == null ? string.Empty : tgp.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(tgpValord == null ? string.Empty : tgpValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("BILIRRUBINA DIRECTA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(bdirecta == null ? string.Empty : bdirecta.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(bdirecta == null ? string.Empty : bdirecta.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(bdirectaValord == null ? string.Empty : bdirectaValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("BILIRRUBINA INDIRECTA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(bindirecta == null ? string.Empty : bindirecta.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(bindirecta == null ? string.Empty : bindirecta.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(bindirectaValord == null ? string.Empty : bindirectaValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("BILIRRUBINA TOTAL", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(btotal == null ? string.Empty : btotal.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(btotal == null ? string.Empty : btotal.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(btotalValord == null ? string.Empty : btotalValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("FOSFATASA ALCALINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(fosfataalcalina == null ? string.Empty : fosfataalcalina.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(fosfataalcalina == null ? string.Empty : fosfataalcalina.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(fosfataalcalinaValord == null ? string.Empty : fosfataalcalinaValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("PROTEINAS TOTALES", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(proteinastotales == null ? string.Empty : proteinastotales.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(proteinastotales == null ? string.Empty : proteinastotales.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(proteinastotalesValord == null ? string.Empty : proteinastotalesValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("ALBÚMINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(albumina == null ? string.Empty : albumina.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(albumina == null ? string.Empty : albumina.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(albuminaValord == null ? string.Empty : albuminaValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                            // 1era fila
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("GLOBULINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(globulina == null ? string.Empty : globulina.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(globulina == null ? string.Empty : globulina.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(globulinaValord == null ? string.Empty : globulinaValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        }
                        else
                        {
                            cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            columnWidths = new float[] { 100f };
                        }
                        #endregion
                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }
                    

                    
                    

                }

                #endregion

                #region HEMATOLOGÍA
              
                var xSanguineo = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GRUPO_Y_FACTOR_SANGUINEO_ID);

                var xTiempoSangria = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TIPO_DE_SANGRIA_ID);
                var xTiempoCoagulacion = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TIEMPO_COAGULACION_ID);
                var xTiempoProtombina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PROTOMBINA_ID);
                var xINR = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.INR_ID);
                var tiempo_tromboplastina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TIEMPO_DE_TROMBOPLASTINA_ID_OC);
                var recuento_plaquetas = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.RECUENTO_DE_PLAQUETAS_ID_OC);
                var xHemograma = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HEMOGRAMA);

                var hemoglobinaa = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.LABORATORIO_HEMOGLOBINA_ID);
                var hematocritoo = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.LABORATORIO_HEMATOCRITO_ID);
                var hemoglobina_glicosilada = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HEMOGLOBINA_GLICOSILADA_ID_OCUP);
                var reticulosisS = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.LABORATORIO_RETICULOCITOS_ID);
                var xVSG = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.VSG_ID);


                if (xSanguineo != null || xTiempoSangria != null || xTiempoCoagulacion != null || xTiempoProtombina != null || xINR != null ||
                    tiempo_tromboplastina != null || recuento_plaquetas != null || xHemograma != null || hemoglobinaa != null || hematocritoo != null ||
                    hemoglobina_glicosilada != null || reticulosisS != null || xVSG != null)
                {
                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("HEMATOLOGIA AUTOMATIZADA", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE },    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("METODOLOGIA: IMPEDANCIA DE FLUJO", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE },    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f, Border = PdfPCell.NO_BORDER},    

                    };

                    columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);
               


                    if (tiempo_tromboplastina != null)
                    {
                        var tiempo_tromboplastina_valor = tiempo_tromboplastina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TIEMPO_DE_TROMBOPLASTINA_VALOR_OC) == null ? "- - -" : tiempo_tromboplastina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TIEMPO_DE_TROMBOPLASTINA_VALOR_OC).v_Value1;
                        var tiempo_tromboplastina_valor_unidad = tiempo_tromboplastina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TIEMPO_DE_TROMBOPLASTINA_VALOR_OC) == null ? "- - -" : tiempo_tromboplastina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TIEMPO_DE_TROMBOPLASTINA_VALOR_OC).v_MeasurementUnitName;
                        var tiempo_tromboplastina_valor_deseable = tiempo_tromboplastina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TIEMPO_DE_TROMBOPLASTINA_VALOR_DESEABLE_OC) == null ? "- - -" : tiempo_tromboplastina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TIEMPO_DE_TROMBOPLASTINA_VALOR_DESEABLE_OC).v_Value1;
                        cells = new List<PdfPCell>();

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("TTPA: (Tiempo de tromboplastina parcial activado.)", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(tiempo_tromboplastina_valor, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(tiempo_tromboplastina_valor_unidad, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(tiempo_tromboplastina_valor_deseable, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                    if (xTiempoSangria != null)
                    {
                        var TiempoSangria = xTiempoSangria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TIPO_DE_SANGRIA_TIEMPO);
                        var TiempoSangriaValord = xTiempoSangria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TIPO_DE_SANGRIA_DESEABLE);
                        cells = new List<PdfPCell>();

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("TIEMPO DE SANGRIA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(TiempoSangria == null ? string.Empty : TiempoSangria.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(TiempoSangria == null ? string.Empty : TiempoSangria.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(TiempoSangriaValord == null ? string.Empty : TiempoSangriaValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                    if (xTiempoCoagulacion != null)
                    {
                        var TiempoCoagulacion = xTiempoCoagulacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TIEMPO_COAGULACION_TIEMPO);
                        var TiempoCoagulacionValord = xTiempoCoagulacion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TIEMPO_COAGULACION_DESEABLE);
                        cells = new List<PdfPCell>();

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("TIEMPO DE COAGULACION", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(TiempoCoagulacion == null ? string.Empty : TiempoCoagulacion.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(TiempoCoagulacion == null ? string.Empty : TiempoCoagulacion.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(TiempoCoagulacionValord == null ? string.Empty : TiempoCoagulacionValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        
                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                    if (xTiempoProtombina != null)
                    {
                        var TiempoProtombina = xTiempoProtombina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTOMBINA_VALOR);
                        var TiempoProtombinaDeseable = xTiempoProtombina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROTOMBINA_VALOR_DESEABLE);
                        cells = new List<PdfPCell>();

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("TIEMPO DE PROTOMBINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(TiempoProtombina == null ? string.Empty : TiempoProtombina.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(TiempoProtombina == null ? string.Empty : TiempoProtombina.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(TiempoProtombinaDeseable == null ? string.Empty : TiempoProtombinaDeseable.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        
                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                    if (xINR != null)
                    {
                        var INR = xINR.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INR_VALOR);
                        var INRDeseable = xINR.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INR_VALOR_DESEABLE);
                        cells = new List<PdfPCell>();

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("INR", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(INR == null ? string.Empty : INR.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(INR == null ? string.Empty : INR.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(INRDeseable == null ? string.Empty : INRDeseable.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        
                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                    if (recuento_plaquetas != null)
                    {
                        var recuento_plaquetas_valor = recuento_plaquetas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RECUENTO_DE_PLAQUETAS_VALOR_OC) == null ? "- - -" : recuento_plaquetas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RECUENTO_DE_PLAQUETAS_VALOR_OC).v_Value1;
                        var recuento_plaquetas_valor_unidad = recuento_plaquetas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RECUENTO_DE_PLAQUETAS_VALOR_OC) == null ? "- - -" : recuento_plaquetas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RECUENTO_DE_PLAQUETAS_VALOR_OC).v_MeasurementUnitName;
                        var recuento_plaquetas_valor_deseable = recuento_plaquetas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RECUENTO_DE_PLAQUETAS_VALOR_DESEABLE_OC) == null ? "- - -" : recuento_plaquetas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RECUENTO_DE_PLAQUETAS_VALOR_DESEABLE_OC).v_Value1;
                        cells = new List<PdfPCell>();

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("RECUENTO DE PLAQUETAS:", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(recuento_plaquetas_valor, fontColumnValue)) { Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(recuento_plaquetas_valor_unidad, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(recuento_plaquetas_valor_deseable, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                    

                    #region Hemograma
                    if (xHemograma != null)
                    {
                        cells = new List<PdfPCell>();

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("HEMOGRAMA", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        var Hemoglobina = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA);
                        var HemoglobinaValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_DESEABLE);

                        var Hematocrito = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMATOCRITO);
                        var HematocritoValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMATOCRITO_DESEABLE);

                        var Hematies = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMATIES);
                        var HematiesValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMATIES_DESEABLE);

                        var LeucocitosTotales = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LEUCOCITOS_TOTALES);
                        var LeucocitosTotalesValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LEUCOCITOS_TOTALES_DESEABLE);

                        var Paquetas = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLAQUETAS);
                        var PaquetasValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLAQUETAS_DESEABLE);

                        var Basofilos = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MID_10_9);
                        var BasofilosValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MID_10_9_DESEABLE);

                        var Eosinofilos = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MID);
                        var EosinofilosValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MID_DESEABLE);

                        var Monocitos = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.NEUTROFILOS_10_9);
                        var MonocitosValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.NEUTROFILOS_10_9_DESEABLE);

                        var Linfocitos = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LINFOCITOS);
                        var LinfocitosValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LINFOCITOS_DESEABLE);

                        var NeutrofilosSegmentos = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.NEUTROFILOS_SEG);
                        var NeutrofilosSegmentosValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.NEUTROFILOS_SEG_DESEABLE);
                        
                        //
                        var Mieloblastos = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MIELOBLASTOS_VAL);
                        var MieloblastosValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MIELOBLASTOS_DESEABLE);

                        var Promielocitos = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROMIELOCITOS_VAL);
                        var PromielocitosValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PROMIELOCITOS_DESEABLE);

                        //
                        var Mielocitos = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VOL_PLAQUETARIO_MEDIO);
                        var MielocitosValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VOL_PLAQUETARIO_MEDIO_DESEABLE);

                        var Juveniles = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.JUVENILES);
                        var JuvenilesValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.JUVENILES_DESEABLE);

                        var Abastonados = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ABASTONADOS);
                        var AbastonadosValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ABASTONADOS_DESEABLE);

                        var Segmentados = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LINFOCITOS_10_9);
                        var SegmentadosValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LINFOCITOS_10_9_DESEABLE);

                        var VolCorpusMedio = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VOL_CORP_MEDIO);
                        var VolCorpusMedioValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VOL_CORP_MEDIO_DESEABLE);

                        var HemoglobCorpMedia = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HB_CORP_MEDIO);
                        var HemoglobCorpMediaValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HB_CORP_MEDIO_DESEABLE);

                        var ConcHBCorp = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CE_HB_MEDIO);
                        var ConcHBCorpValord = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CE_HB_MEDIO_DESEABLE);


                        var hemograma_otros = xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.Hemograma_Otros) == null ? "-" : xHemograma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.Hemograma_Otros).v_Value1;

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("SERIE ROJA", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("HEMOGLOBINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Hemoglobina == null ? string.Empty : Hemoglobina.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Hemoglobina == null ? string.Empty : Hemoglobina.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(HemoglobinaValord == null ? string.Empty : HemoglobinaValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("HEMATOCRITO", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Hematocrito == null ? string.Empty : Hematocrito.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Hematocrito == null ? string.Empty : Hematocrito.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(HematocritoValord == null ? string.Empty : HematocritoValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("HEMATIES", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Hematies == null ? string.Empty : Hematies.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Hematies == null ? string.Empty : Hematies.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(HematiesValord == null ? string.Empty : HematiesValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });


                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("FORMULA LEUCOCITARIA", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });


                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("LEUCOCITOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(LeucocitosTotales == null ? string.Empty : LeucocitosTotales.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(LeucocitosTotales == null ? string.Empty : LeucocitosTotales.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(LeucocitosTotalesValord == null ? string.Empty : LeucocitosTotalesValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("MIELOBLASTOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Mieloblastos == null ? string.Empty : Mieloblastos.v_Value1 == "0" ? "00" : Mielocitos.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Mieloblastos == null ? string.Empty : Mieloblastos.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(MieloblastosValord == null ? string.Empty : MieloblastosValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("PROMIELOCITOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Promielocitos == null ? string.Empty : Promielocitos.v_Value1 == "0" ? "00" : Mielocitos.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Promielocitos == null ? string.Empty : Promielocitos.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(PromielocitosValord == null ? string.Empty : PromielocitosValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("MIELOCITOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Mielocitos == null ? string.Empty : Mielocitos.v_Value1 == "0" ? "00" : Mielocitos.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Mielocitos == null ? string.Empty : Mielocitos.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(MielocitosValord == null ? string.Empty : MielocitosValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("JUVENILES", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Juveniles == null ? string.Empty : Juveniles.v_Value1 == "0" ? "00" : Juveniles.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Juveniles == null ? string.Empty : Juveniles.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(JuvenilesValord == null ? string.Empty : JuvenilesValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("ABASTONADOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Abastonados == null ? string.Empty : Abastonados.v_Value1 == "0" ? "00" : Abastonados.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Abastonados == null ? string.Empty : Abastonados.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(AbastonadosValord == null ? string.Empty : AbastonadosValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("SEGMENTADOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Segmentados == null ? string.Empty : Segmentados.v_Value1 == "0" ? "00" : Segmentados.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Segmentados == null ? string.Empty : Segmentados.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(SegmentadosValord == null ? string.Empty : SegmentadosValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("NEUTROFILOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(NeutrofilosSegmentos == null ? string.Empty : NeutrofilosSegmentos.v_Value1 == "0" ? "00" : NeutrofilosSegmentos.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(NeutrofilosSegmentos == null ? string.Empty : NeutrofilosSegmentos.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(NeutrofilosSegmentosValord == null ? string.Empty : NeutrofilosSegmentosValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("EOSINOFILOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Eosinofilos == null ? string.Empty : Eosinofilos.v_Value1 == "0" ? "00" : Eosinofilos.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Eosinofilos == null ? string.Empty : Eosinofilos.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(EosinofilosValord == null ? string.Empty : EosinofilosValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("BASOFILOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Basofilos == null ? string.Empty : Basofilos.v_Value1 == "0" ? "00" : Basofilos.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Basofilos == null ? string.Empty : Basofilos.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(BasofilosValord == null ? string.Empty : BasofilosValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("MONOCITOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Monocitos == null ? string.Empty : Monocitos.v_Value1 == "0" ? "00" : Monocitos.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Monocitos == null ? string.Empty : Monocitos.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(MonocitosValord == null ? string.Empty : MonocitosValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("LINFOCITOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Linfocitos == null ? string.Empty : Linfocitos.v_Value1 == "0" ? "00" : Linfocitos.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Linfocitos == null ? string.Empty : Linfocitos.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(LinfocitosValord == null ? string.Empty : LinfocitosValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("PLAQUETAS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Paquetas == null ? string.Empty : Paquetas.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Paquetas == null ? string.Empty : Paquetas.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(PaquetasValord == null ? string.Empty : PaquetasValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("CONSTANTES CORPUSCULARES", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });


                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("VOLUMEN CORPUSCULAR MEDIO (VCM):", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(VolCorpusMedio == null ? string.Empty : VolCorpusMedio.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(VolCorpusMedio == null ? string.Empty : VolCorpusMedio.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(VolCorpusMedioValord == null ? string.Empty : VolCorpusMedioValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("HEMOGLOBINA CORPUSCULAR MEDIA (HCM):", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(HemoglobCorpMedia == null ? string.Empty : HemoglobCorpMedia.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(HemoglobCorpMedia == null ? string.Empty : HemoglobCorpMedia.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(HemoglobCorpMediaValord == null ? string.Empty : HemoglobCorpMediaValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("CONCENTRACION DE HEMOGLOBINA CORPUSCULAR MEDIA (MHCM): ", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ConcHBCorp == null ? string.Empty : ConcHBCorp.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ConcHBCorp == null ? string.Empty : ConcHBCorp.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ConcHBCorpValord == null ? string.Empty : ConcHBCorpValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("OTROS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(hemograma_otros, fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                    if (xSanguineo != null)
                    {
                        cells = new List<PdfPCell>();

                        //cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        //cells.Add(new PdfPCell(new Phrase("GRUPO Y FACTOR SANGUÍNEO", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        //cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });


                        var Sanguineo = xSanguineo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GRUPO_SANGUINEO_ID);
                        var SanguineoValord = xSanguineo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FACTOR_SANGUINEO_ID);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("GRUPO SANGUÍNEO", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Sanguineo == null ? string.Empty : Sanguineo.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("FACTOR RH (D)", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(SanguineoValord == null ? string.Empty : SanguineoValord.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }
                    #endregion
                    #region Hemoglobina y hematocrito              
                    if (hemoglobinaa != null)
                    {
                       var Hemoglobina = hemoglobinaa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_ID);
                        var HemoglobinaValord = hemoglobinaa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LABORATORIO_HEMOGLOBINA_VALOR_DESEABLE_ID);
                        cells = new List<PdfPCell>();

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("HEMOGLOBINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Hemoglobina == null ? string.Empty : Hemoglobina.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Hemoglobina == null ? string.Empty : Hemoglobina.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(HemoglobinaValord == null ? "H: 13.0 - 18.0 / M: 12.0 - 16.0" : HemoglobinaValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                    if (hematocritoo != null)
                    {
                        cells = new List<PdfPCell>();

                      
                        var Henmatocrito = hematocritoo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMATOCRITO_ID);
                        var HenmatocritoValord = hematocritoo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMATOCRITO_HEMOGRAMA_HEMATOCRITO_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("HEMATOCRITO", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Henmatocrito == null ? string.Empty : Henmatocrito.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Henmatocrito == null ? string.Empty : Henmatocrito.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(HenmatocritoValord == null ? "H: 40.0 - 54.2 / M: 37.0 - 48.5" : HenmatocritoValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });


                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                    if (hemoglobina_glicosilada != null)
                    {
                        var hba1c_valor = hemoglobina_glicosilada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_GLICOSILADA_HBA1C_1) == null ? "- - -" : hemoglobina_glicosilada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_GLICOSILADA_HBA1C_1).v_Value1;
                        var hba1c_valor_unidad = hemoglobina_glicosilada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_GLICOSILADA_HBA1C_1) == null ? "- - -" : hemoglobina_glicosilada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_GLICOSILADA_HBA1C_1).v_MeasurementUnitName;
                        var hba1c_valor_deseado = hemoglobina_glicosilada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_GLICOSILADA_HBA1C_VALOR_DESEABLE_2) == null ? "- - -" : hemoglobina_glicosilada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_GLICOSILADA_HBA1C_VALOR_DESEABLE_2).v_Value1;

                        var eag_valor = hemoglobina_glicosilada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_GLICOSILADA_EAG_3) == null ? "- - -" : hemoglobina_glicosilada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_GLICOSILADA_EAG_3).v_Value1;
                        var eag_valor_unidad = hemoglobina_glicosilada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_GLICOSILADA_EAG_3) == null ? "- - -" : hemoglobina_glicosilada.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_GLICOSILADA_EAG_3).v_MeasurementUnitName;

                        cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase("INMUNOLOGÍA", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },    
                            new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                            new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase("METODOLOGIA: REFLECTOFOTOMETRIA", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },    
                            new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                            //new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                            //new PdfPCell(new Phrase("EXAMEN", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                            //new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                            //new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                            //new PdfPCell(new Phrase("VALOR DE REFERENCIA", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                            //new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        };

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);

                        cells = new List<PdfPCell>();

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("Hemoglobina Glicosilada (HbA1c):", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(hba1c_valor, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(hba1c_valor_unidad, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(hba1c_valor_deseado, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });    
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("Glucosa Media Estimada (eAG):", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(eag_valor, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(eag_valor_unidad, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("- - -", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }
                    #endregion

                    #region RETICULOSIS

                    if (reticulosisS != null)
                    {
                        //cells = new List<PdfPCell>()
                        //{
                        //    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        //    new PdfPCell(new Phrase("HEMATOLOGIA AUTOMATIZADA", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },    
                        //    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                        //    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        //    new PdfPCell(new Phrase("METODOLOGÍA: IMPERANCIA DE FLUJO", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },    
                        //    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                        //    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        //    new PdfPCell(new Phrase("EXAMEN", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        //    new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        //    new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        //    new PdfPCell(new Phrase("VALOR DE REFERENCIA", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        //    new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        //};

                        //columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        //table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        //document.Add(table);


                        var reticulosis = reticulosisS.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LABORATORIO_RETICULOCITOS_VALOR);
                        var reticulosisValord = reticulosisS.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LABORATORIO_RETICULOCITOS_VALORDESEABLE_VALOR);
                        cells = new List<PdfPCell>();

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("RETICULOSIS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(reticulosis == null ? string.Empty : reticulosis.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(reticulosis == null ? string.Empty : reticulosis.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(reticulosisValord == null ? "0.5% - 2 %" : reticulosisValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "", fontTitleTableNegro, null);
                        document.Add(table);
                    }

                    #endregion

                    #region VSG
                    if (xVSG != null)
                    {
                            var vsg = xVSG.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VSG_RESUL);
                            var vsgValord = xVSG.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VSG_DESEABLE);
                            cells = new List<PdfPCell>();

                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("VSG", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                            cells.Add(new PdfPCell(new Phrase(vsg == null ? string.Empty : vsg.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                            cells.Add(new PdfPCell(new Phrase(vsg == null ? string.Empty : vsg.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase(vsgValord == null ? string.Empty : vsgValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                            cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });


                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);

                    }

                    #endregion
                }

                #endregion

                #region INMUNOLOGIA INMUNOCROMATOGRAFIA



                var psa_total = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PSA_ID_OC);

                var xRPR = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.R_P_R_ID);
                var xExamenElisa = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_ELISA_ID);
                var xHepatitisA = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HEPATITIS_A_ID_ASIST);
                var xHepatitisB = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HEPATITIS_BB_ID);
                var xHepatitisC = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HEPATITIS_CC_ID);
                var xReaccSerol = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.VDRL_ID_ASIST);
                var xB_HCG = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.SUB_UNIDAD_BETA_CUALITATIVO_ID);

                var xAglutinaciones = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_ID);
                var xFReuma = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FACTOR_REMATOIDEO_ID);
                var xPCR = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PCR_ID);
                var xPSA = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ANTIGENO_PROSTATICO_ID);

               
                if (xRPR != null || xExamenElisa != null || xHepatitisA != null || xHepatitisB != null ||
                    xHepatitisC != null || xReaccSerol != null || xB_HCG != null || psa_total != null ||
                    xAglutinaciones != null || xFReuma != null || xPCR != null || xPSA != null )
                {
                   cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("INMUNOLOGÍA", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE },    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("METODOLOGÍA: INMUNOCROMATOGRAFÍA / PRUEBA RÁPIDA (RAPITEST)", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE },    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f, Border = PdfPCell.NO_BORDER},    

                        //new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        //new PdfPCell(new Phrase("EXAMEN", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        //new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        //new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        //new PdfPCell(new Phrase("VALOR DE REFERENCIA", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        //new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                       };

                    columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);

                    cells = new List<PdfPCell>();
                    

                    if (psa_total != null)
                    {
                        var psa_total_valor = psa_total.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PSA_VALOR_OC) == null ? "- - -" : psa_total.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PSA_VALOR_OC).v_Value1;
                        var psa_total_valor_unidad = psa_total.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PSA_VALOR_OC) == null ? "- - -" : psa_total.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PSA_VALOR_OC).v_MeasurementUnitName;
                        var psa_total_valor_deseado = psa_total.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PSA_VALOR_DESEABLE_OC) == null ? "- - -" : psa_total.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PSA_VALOR_DESEABLE_OC).v_Value1;

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("PSA TOTAL", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(psa_total_valor, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(psa_total_valor_unidad, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(psa_total_valor_deseado, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    if (xRPR != null)
                    {
                        var rpr = xRPR.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.INMUNOLOGIA_R_P_R_ID);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("R.P.R.", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(rpr == null ? string.Empty : rpr.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    
                    if (xExamenElisa != null)
                    {
                        var ExamenElisa = xExamenElisa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CAMPO_HIV);
                        var ExamenElisaValord = xExamenElisa.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_ELISA__REACTIVOS_EXAMEN_ELISA_DESEABLE);
                        
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("HIV (ANTIGENO - ANTICUERPO)", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ExamenElisa == null ? string.Empty : ExamenElisa.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    
                    if (xHepatitisA != null)
                    {
                        var HepatitisA = xHepatitisA.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEPATITIS_A_REACTIVOS_HEPATITIS_A);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("HEPATITS A - (IgM)", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(HepatitisA == null ? string.Empty : HepatitisA.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    
                    if (xHepatitisB != null)
                    {
                        var HepatitisB = xHepatitisB.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEPATITIS_BB_REACTIVOS_HEPATITIS_B);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("HEPATITS B (HBsAg)", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(HepatitisB == null ? string.Empty : HepatitisB.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    
                    if (xHepatitisC != null)
                    {
                        var HepatitisC = xHepatitisC.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEPATITIS_CC_REACTIVOS_HEPATITIS_C);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("HEPATITS C", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(HepatitisC == null ? string.Empty : HepatitisC.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    
                    if (xReaccSerol != null)
                    {
                        var ReaccSerol = xReaccSerol.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LABORATORIO_VDRL_ID);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("LUES - VDRL", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ReaccSerol == null ? string.Empty : ReaccSerol.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }
                    
                    if (xB_HCG != null)
                    {
                        var b_hcg = xB_HCG.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.SUB_UNIDAD_BETA_CUALITATIVO_RESULTADO);
                        var b_hcgValord = xB_HCG.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.SUB_UNIDAD_BETA_CUALITATIVO_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("B-HCG", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(b_hcg == null ? string.Empty : b_hcg.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    
                    if (xFReuma != null)
                    {
                        var FR = xFReuma.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FACTOR_REMATOIDEO_VALOR);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("FACTOR REMATOIDEO", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(FR == null ? string.Empty : FR.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    if (xPCR != null)
                    {
                        var PCR = xPCR.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PCR_VALOR);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("PROTEINA C REACTIVA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(PCR == null ? string.Empty : PCR.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    if (xAglutinaciones != null)
                    {
                        cells = new List<PdfPCell>();

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("AGLUTINACIONES", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });


                        var ParatificoA = xAglutinaciones.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_A);
                        var ParatificoB = xAglutinaciones.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_PARATIFICO_B);
                        var ParatificoO = xAglutinaciones.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_O);
                        var ParatificoH = xAglutinaciones.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_TIFICO_H);
                        var Brucella = xAglutinaciones.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.AGLUTINACIONES_LAMINA_REACTIVOS_BRUCELLA);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("FLAGELAR PARATYFICO A", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ParatificoA == null ? string.Empty : ParatificoA.v_Value1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("FLAGELAR PARATYFICO B", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ParatificoB == null ? string.Empty : ParatificoB.v_Value1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("FLAGELAR PARATYFICO O", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ParatificoO == null ? string.Empty : ParatificoO.v_Value1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("FLAGELAR PARATYFICO H", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ParatificoH == null ? string.Empty : ParatificoH.v_Value1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("BRUCELLA ABORTUS", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Brucella == null ? string.Empty : Brucella.v_Value1, fontColumnValue)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                    }

                    if (xPSA != null)
                    {
                        var PSA = xPSA.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTIGENO_PROSTATICO_ANTIGENO_PROSTATICO_VALOR);
                        var PSADeseable = xPSA.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTIGENO_PROSTATICO_VALOR_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("ANTIGENO PROSTATICO ESPECIFICO ( PSA )", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(PSA == null ? string.Empty : PSA.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(PSA == null ? string.Empty : PSA.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(PSADeseable == null ? string.Empty : PSADeseable.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }
                    columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);
                }
                #endregion

                #region MICROBIOLOGIA - BACILOSCOPIA
              
                var xBK = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.BK_DIRECTO_ID);
                var xCoprocultivo = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.COPROCULTIVO_ID_OC);

                var xHisopadoNasof = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HISOPADO_NASOFARINGEO_ID);
                //var examenesMicrobiologiaCopro = examenesLab.FindAll(p => groupMicrobiologiaCoprocultivo.Contains(p.v_ComponentId));  examenesMicrobiologiaCopro != null ||
                var xSalmonella = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.SALMONELLA_ID);
                var xExamenSereadoEnHeces = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_SEREADO_EN_HECES_ID);
                var xReaaccionInfHeces = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_ID);
                var xExamenCompletoDeOrina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_DE_ORINA_ID);

                var xParasitologiaSimple = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_ID);
                var xParasitologiaSeriado = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_ID);

                if (xBK != null || xHisopadoNasof != null || xParasitologiaSimple != null || xParasitologiaSeriado != null ||
                    xSalmonella != null || xExamenSereadoEnHeces != null || xReaaccionInfHeces != null ||
                    xExamenCompletoDeOrina != null || xCoprocultivo != null)
                {
                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("MICROBIOLOGIA", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE },    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE },    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f, Border = PdfPCell.NO_BORDER},    


                       };

                    columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);

                    //if (xBK != null || xHisopadoNasof != null ||
                    //    xSalmonella != null || xExamenSereadoEnHeces != null || xReaaccionInfHeces != null ||
                    //    xExamenCompletoDeOrina != null)
                    //{
                    //   cells = new List<PdfPCell>()
                    //   {
                    //        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                    //        new PdfPCell(new Phrase("EXAMEN", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                    //        new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                    //        new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                    //        new PdfPCell(new Phrase("VALOR DE REFERENCIA", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                    //        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                    //    };

                    //    columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                    //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    //    document.Add(table); 
                    //}

                    cells = new List<PdfPCell>();

                    if (xBK != null)
                    {
                        var bk = xBK.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BK_DIRECTO_MICROBIOLOGICO_RESULTADOS);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("BK (ESPUTO SIMPLE)", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(bk == null ? ": " + string.Empty : ": " + bk.v_Value1, fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        //cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        //cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    if (xCoprocultivo != null)
                    {
                        var COPROCULTIVO_COLOR_OC = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COPROCULTIVO_COLOR_OC);
                        var COPROCULTIVO_CONSISTENCIA_OC = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COPROCULTIVO_CONSISTENCIA_OC);
                        var COPROCULTIVO_MOCO_OC = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COPROCULTIVO_MOCO_OC);
                        var COPROCULTIVO_THEVENON_OC = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COPROCULTIVO_THEVENON_OC);
                        var COPROCULTIVO_RESTOS_ALIMENTICIOS_OC = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COPROCULTIVO_RESTOS_ALIMENTICIOS_OC);

                        var COPROCULTIVO_GRASAS_OC = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COPROCULTIVO_GRASAS_OC);
                        var COPROCULTIVO_LEUCOCITOS_OC = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COPROCULTIVO_LEUCOCITOS_OC);
                        var COPROCULTIVO_HEMATIES_OC = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COPROCULTIVO_HEMATIES_OC);
                        var COPROCULTIVO_LEVADURAS_OC = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COPROCULTIVO_LEVADURAS_OC);
                        var COPROCULTIVO_PARASITOS_OC = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COPROCULTIVO_PARASITOS_OC);

                        var COPROCULTIVO_RESULTDOS_OC = xCoprocultivo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COPROCULTIVO_RESULTDOS_OC);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("COPROCULTIVO", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 13, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("EXAMEN MACROSCOPICO:", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("   -COLOR", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(COPROCULTIVO_COLOR_OC == null ? ": " + string.Empty : ": " + COPROCULTIVO_COLOR_OC.v_Value1Name, fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("   -CONSISTENCIA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(COPROCULTIVO_CONSISTENCIA_OC == null ? ": " + string.Empty : ": " + COPROCULTIVO_CONSISTENCIA_OC.v_Value1Name, fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("   -MOCO", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(COPROCULTIVO_MOCO_OC == null ? ": " + string.Empty : ": " + COPROCULTIVO_MOCO_OC.v_Value1Name, fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("   -THEVENON", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(COPROCULTIVO_THEVENON_OC == null ? ": " + string.Empty : ": " + COPROCULTIVO_THEVENON_OC.v_Value1Name, fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("   -RESTOS ALIMENTICIOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(COPROCULTIVO_RESTOS_ALIMENTICIOS_OC == null ? ": " + string.Empty : ": " + COPROCULTIVO_RESTOS_ALIMENTICIOS_OC.v_Value1Name, fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("EXAMEN MICROSCOPICO:", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("   -GRASAS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(COPROCULTIVO_GRASAS_OC == null ? ": " + string.Empty : ": " + COPROCULTIVO_GRASAS_OC.v_Value1, fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("   -LEUCOCITOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(COPROCULTIVO_LEUCOCITOS_OC == null ? ": " + string.Empty : ": " + COPROCULTIVO_LEUCOCITOS_OC.v_Value1, fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("   -HEMATIES", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(COPROCULTIVO_HEMATIES_OC == null ? ": " + string.Empty : ": " + COPROCULTIVO_HEMATIES_OC.v_Value1, fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("   -LEVADURAS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(COPROCULTIVO_LEVADURAS_OC == null ? ": " + string.Empty : ": " + COPROCULTIVO_LEVADURAS_OC.v_Value1, fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("   -PARASITOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(COPROCULTIVO_PARASITOS_OC == null ? ": " + string.Empty : ": " + COPROCULTIVO_PARASITOS_OC.v_Value1, fontColumnValue)) { Colspan = 12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("COPROCULTIVO:", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("   -" + COPROCULTIVO_RESULTDOS_OC == null ? string.Empty : COPROCULTIVO_RESULTDOS_OC.v_Value1, fontColumnValue)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });


                    }
                    if (xHisopadoNasof != null)
                    {
                        var HisopadoNasof = xHisopadoNasof.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FROTIS_GRAM);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("HISOPADO NASOFARINGEO", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(HisopadoNasof == null ? string.Empty : HisopadoNasof.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }
                    #region MICROBIOLOGIA  - COPROCULTIVO
                    if (xSalmonella != null || xExamenSereadoEnHeces != null || xReaaccionInfHeces != null)
                    {
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("COPROCULTIVO", fontColumnValue)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }
                    if (xSalmonella != null)
                    {
                        var Salmonella = xSalmonella.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.SALMONELLA_RESULTADO_ID);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("CULTIVO DE HECES PARA SALMONELLA TYPHI", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Salmonella == null ? string.Empty : Salmonella.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Salmonella == null ? string.Empty : Salmonella.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }
                    
                    if (xExamenSereadoEnHeces != null)
                    {
                        var ExamenSereadoEnHeces = xExamenSereadoEnHeces.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_SEREADO_EN_HECES_REACTIVOS);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("EXAMEN SEREADO EN HECES", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ExamenSereadoEnHeces == null ? string.Empty : ExamenSereadoEnHeces.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ExamenSereadoEnHeces == null ? string.Empty : ExamenSereadoEnHeces.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }
                    
                    if (xReaaccionInfHeces != null)
                    {
                        var ReaaccionInfHeces = xReaaccionInfHeces.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.REACCION_INFLAMATORIA_RESULTADO_ID);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("REACCION INFLAMATORIA EN HECES", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ReaaccionInfHeces == null ? string.Empty : ReaaccionInfHeces.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }
                    #endregion

                    #region Examen de Orina

                    if (xExamenCompletoDeOrina != null)
                    {
                        var Color = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_COLOR);
                        var Aspecto = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_ASPECTO);
                        var Densidad = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_DENSIDAD);
                        var DensidadValord = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMP_ORINA_MACROSCOPICO_DENSIDAD_DESEABLE);
                        var ReaccionPH = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MACROSCOPICO_PH);
                        var ReaccionPHValord = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMP_ORINA_MACROSCOPICO_PH_DESEABLE);
                        var SangreOrina = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_HEMOGLOBINA);
                        var NitritosOrina = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_NITRITOS);
                        var ProteinasOrina = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_PROTEINAS);
                        var GlucosaOrina = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_GLUCOSA);
                        var PicmentosBiliares = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_PIGMENTOS_BILIARES);
                        var UrobinogenoOrina = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_UROBILINOGENO);
                        var Cetonas = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_CETONAS);
                        var AcidoAscorbico = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_FILAMENTO_MUCOIDE);
                        var bilirrubina = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_BILIRRUBINA);
                        var LeucocitosBIO = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_LEUCOCITOS);
                        var Leucocitos = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_LEUCOCITOS);
                        var Hematies = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_HEMATIES);
                        var Germenes = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_GERMENES);
                        var Cristales = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_CRISTALES);
                        var CristalesDeuratos = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_CRISTALES_DEURATOS_AMORFOS);
                        var FosfatosAmorfos = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_CRISTALES_FOSFATOS_AMORFOS);
                        var FosfatosTriples = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_CRISTALES_FOSFATOS_TRIPLES);
                        var Celulas = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_CELULAS_EPITELIALES);
                        var Cilindos = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_CILINDROS);
                        var CilindosGranulosos = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_CILINDROS_GRANULOSOS);
                        var CilindosLeuco = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_CILINDROS_LEUCOCITARIOS);
                        var CilindosHema = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_CILINDROS_HEMATICOS);
                        var levaduras = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_LEVADURAS);
                        var Pus = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_BIOQUIMICO_SANGRE);
                        var filamentos = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_FILAMENTOS_MUCOIDES);
                        var resultados = xExamenCompletoDeOrina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_COMPLETO_DE_ORINA_RESULTADOS_ID);


                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("EXAMEN COMPLETO DE ORINA", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK });    
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("-------EXAMEN FÍSICO - QUÍMICO-------", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("COLOR", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Color == null ? string.Empty : Color.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Color == null ? string.Empty : Color.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("ASPECTO", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Aspecto == null ? string.Empty : Aspecto.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Aspecto == null ? string.Empty : Aspecto.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("DENSIDAD", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Densidad == null ? string.Empty : Densidad.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Densidad == null ? string.Empty : Densidad.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(DensidadValord == null ? string.Empty : DensidadValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("REACCION PH", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ReaccionPH == null ? string.Empty : ReaccionPH.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ReaccionPH == null ? string.Empty : ReaccionPH.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ReaccionPHValord == null ? string.Empty : ReaccionPHValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("SANGRE EN ORINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(SangreOrina == null ? string.Empty : SangreOrina.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(SangreOrina == null ? string.Empty : SangreOrina.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("NITRITOS EN ORINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(NitritosOrina == null ? string.Empty : NitritosOrina.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(NitritosOrina == null ? string.Empty : NitritosOrina.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("PROTEINAS EN ORINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ProteinasOrina == null ? string.Empty : ProteinasOrina.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(ProteinasOrina == null ? string.Empty : ProteinasOrina.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("GLUCOSA EN ORINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(GlucosaOrina == null ? string.Empty : GlucosaOrina.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(GlucosaOrina == null ? string.Empty : GlucosaOrina.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("PIGMENTOS BILIARES", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(PicmentosBiliares == null ? string.Empty : PicmentosBiliares.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(PicmentosBiliares == null ? string.Empty : PicmentosBiliares.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("UROBILINOGENO EN ORINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(UrobinogenoOrina == null ? string.Empty : UrobinogenoOrina.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(UrobinogenoOrina == null ? string.Empty : UrobinogenoOrina.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("CETONAS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Cetonas == null ? string.Empty : Cetonas.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Cetonas == null ? string.Empty : Cetonas.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("ACIDO ASCORBICO", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(AcidoAscorbico == null ? string.Empty : AcidoAscorbico.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(AcidoAscorbico == null ? string.Empty : AcidoAscorbico.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("BILIRRUBINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(bilirrubina == null ? string.Empty : bilirrubina.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(bilirrubina == null ? string.Empty : bilirrubina.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("LEUCOCITOS EN ORINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(LeucocitosBIO == null ? string.Empty : LeucocitosBIO.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(LeucocitosBIO == null ? string.Empty : LeucocitosBIO.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("-------EXAMEN MICROSCOPICO-------", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("LEUCOCITOS EN ORINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Leucocitos == null ? string.Empty : Leucocitos.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Leucocitos == null ? string.Empty : Leucocitos.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("0 - 5 XC", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("HEMATIES EN ORINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Hematies == null ? string.Empty : Hematies.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Hematies == null ? string.Empty : Hematies.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(" 0 - 2 XC", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("GERMENES", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Germenes == null ? string.Empty : Germenes.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Germenes == null ? string.Empty : Germenes.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("CRISTALES DE OXALATOS DE CALCIO", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Cristales == null ? string.Empty : Cristales.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Cristales == null ? string.Empty : Cristales.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("CRISTALES DE URATOS AMORFOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(CristalesDeuratos == null ? string.Empty : CristalesDeuratos.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(CristalesDeuratos == null ? string.Empty : CristalesDeuratos.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("CRISTALES FOSFATOS AMORFOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(FosfatosAmorfos == null ? string.Empty : FosfatosAmorfos.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(FosfatosAmorfos == null ? string.Empty : FosfatosAmorfos.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("CRISTALES FOSFATOS TRIPLES", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(FosfatosTriples == null ? string.Empty : FosfatosTriples.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(FosfatosTriples == null ? string.Empty : FosfatosTriples.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("CELULAS EPITELIALES", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Celulas == null ? string.Empty : Celulas.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Celulas == null ? string.Empty : Celulas.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(" 0 - 6 XC", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("CILINDROS HIALINOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Cilindos == null ? string.Empty : Cilindos.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Cilindos == null ? string.Empty : Cilindos.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("CILINDROS GRANULOSOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(CilindosGranulosos == null ? string.Empty : CilindosGranulosos.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(CilindosGranulosos == null ? string.Empty : CilindosGranulosos.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("CILINDROS LEUCOCITARIOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(CilindosLeuco == null ? string.Empty : CilindosLeuco.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(CilindosLeuco == null ? string.Empty : CilindosLeuco.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("CILINDROS HEMATICOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(CilindosHema == null ? string.Empty : CilindosHema.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(CilindosHema == null ? string.Empty : CilindosHema.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("LEVADURAS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(levaduras == null ? string.Empty : levaduras.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(levaduras == null ? string.Empty : levaduras.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("PIOCITOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Pus == null ? string.Empty : Pus.v_Value1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(Pus == null ? string.Empty : Pus.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("FILAMENTOS MUCOIDES", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(filamentos == null ? string.Empty : filamentos.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(filamentos == null ? string.Empty : filamentos.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        if (resultados == null)
                        {
                            cells.Add(new PdfPCell(new Phrase("- - -", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        }
                        else if (resultados.v_Value1 == "0")
                        {
                            cells.Add(new PdfPCell(new Phrase("No Patológico", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        }
                        else
                        {
                            cells.Add(new PdfPCell(new Phrase("Patológico", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        }

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });



                        
                    }
                    #endregion

                    
                    columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);


                    if (xParasitologiaSimple != null || xParasitologiaSeriado != null)
                    {
                        cells = new List<PdfPCell>()
                       {
                            new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                            new PdfPCell(new Phrase("EXAMEN", fontColumnValueBold)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                            new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                            new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                        };

                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    }

                    cells = new List<PdfPCell>();


                    #region PARASITOLOGIA SIMPLE
                    if (xParasitologiaSimple != null)
                    {
                        #region ParasitologiaSimple
                        var color = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_COLOR);
                        var consistencia = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_CONSISTENCIA);
                        var restosAlimenticios = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_RESTOS_ALIMENTICIOS);
                        var moco = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_MOCO);
                        var hematies = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_HEMATIES);
                        var leucocitos = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_LEUCOCITOS);
                        var levaduras = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_LEVADURAS);
                        var grasas = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_GRASAS);
                        var resultado_Sedim = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_RESULTADOS_SEDIM);
                        var resultado = xParasitologiaSimple.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SIMPLE_EXAMEN_HECES_RESULTADOS);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("PARASITOLÓGICO SIMPLE", fontColumnValue)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("EXAMEN MACROSCÓPICO", fontColumnValue)) { Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        
                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("COLOR", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(": " + color == null ? string.Empty : color.v_Value1Name, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        // 2da fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("CONSISTENCIA", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(": " + consistencia == null ? string.Empty : consistencia.v_Value1Name, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        // 3era fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("MOCO", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(": " + moco == null ? string.Empty : moco.v_Value1Name, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        // 4ta fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("RESTOS ALIMENTICIOS", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(": " + restosAlimenticios == null ? string.Empty : restosAlimenticios.v_Value1Name, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("EXAMEN MICROSCÓPICO", fontColumnValueBold)) { Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        
                        // 5ta fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("HEMATIES", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(": " + hematies == null ? string.Empty : hematies.v_Value1, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        // 6xta fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("LEUCOCITOS", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(": " + leucocitos == null ? string.Empty : leucocitos.v_Value1, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        // 7ma fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("LEVADURAS", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(": " + levaduras == null ? string.Empty : levaduras.v_Value1, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        // 8tavo fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("GRASAS", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(": " + grasas == null ? string.Empty : grasas.v_Value1, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("RESULTADOS", fontColumnValue)) { Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        // 9na fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("METODO DIRECTO /(SOLUC.SALINA/LUGOL)", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(": " + resultado == null ? string.Empty : resultado.v_Value1, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        // 10ma fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("METODO DE SEDIMENTACION", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(": " + resultado_Sedim == null ? string.Empty : resultado_Sedim.v_Value1, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        #endregion

                    }
                    #endregion

                    #region PARASITOLOGIA SERIADO


                    if (xParasitologiaSeriado != null)
                    {
                        #region PRIMERA MUESTRA
                        
                        var color = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_COLOR);
                        var consistencia = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_CONSISTENCIA);
                        var restosAlimenticios = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_RESTOS_ALIMENTICIOS);
                        var hematies = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_HEMATIES);
                        var leucocitos = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_LEUCOCITOS);
                        var moco = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_MOCO);
                        var levaduras = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_LEVADURAS);
                        var directo = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_DIRECTO);
                        var grasas = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_GRASAS);
                        var sedimentacion = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_HECES_PRIMERA_SEDIMENTACION);
                        var observaciones = xParasitologiaSeriado.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PARASITOLOGICO_SERIADO_EXAMEN_OBSERVACIONES);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("PARASITOLÓGICO SERIADO", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("EXAMEN MACROSCÓPICO", fontColumnValueBold)) { Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });


                        // 1era fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("COLOR", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(": " + color == null ? string.Empty : color.v_Value1Name, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        // 2da fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("CONSISTENCIA", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(": " + consistencia == null ? string.Empty : consistencia.v_Value1Name, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        // 3era fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("MOCO", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(": " + moco == null ? string.Empty : moco.v_Value1Name, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        // 4ta fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("RESTOS ALIMENTICIOS", fontColumnValueBold)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(": " + restosAlimenticios == null ? string.Empty : restosAlimenticios.v_Value1Name, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("EXAMEN MICROSCÓPICO", fontColumnValueBold)) { Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        // 5ta fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("HEMATIES", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(": " + hematies == null ? string.Empty : hematies.v_Value1, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        // 6xta fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("LEUCOCITOS", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(": " + leucocitos == null ? string.Empty : leucocitos.v_Value1, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        // 7ma fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("LEVADURAS", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(": " + levaduras == null ? string.Empty : levaduras.v_Value1, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        // 8tavo fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("GRASAS", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(": " + grasas == null ? string.Empty : grasas.v_Value1, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("RESULTADOS", fontColumnValueBold)) { Colspan = 17, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        // 9na fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("METODO DIRECTO /(SOLUC.SALINA/LUGOL)", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(": " + directo == null ? string.Empty : directo.v_Value1, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        // 10ma fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("METODO DE SEDIMENTACION", fontColumnValue)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(": " + sedimentacion == null ? string.Empty : sedimentacion.v_Value1, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        // 10ma fila
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("OBSERVACIONES", fontColumnValueBold)) { Colspan = 8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(": " + observaciones == null ? string.Empty : observaciones.v_Value1, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        
                        #endregion
                    }
                    columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);
            
                #endregion

                }



                #endregion


                #region TOXICOLOGIA
                ServiceComponentList toxicologico_todos = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_T);

                ServiceComponentList toxicologico10_parametros_Hdbay = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_ID);

                

                ServiceComponentList xDosajeAlcohol = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_ALCOHOLEMIA);
                ServiceComponentList xDosajeAlcoholO = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ALCOHOL_EN_SALIVA_ID);

                ServiceComponentList xCocaina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID);
                ServiceComponentList xMarihuana = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID);
                ServiceComponentList xAnfetaminas = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_ANFETAMINAS);
                ServiceComponentList xBarbituricos = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.BARBITURICOS_ID);
                ServiceComponentList xBenzodiazepinas = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_BENZODIAZEPINAS);
                ServiceComponentList xMetanfetaminas = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.METANFETAMINAS_ID);
                ServiceComponentList xMorfina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.MORFINA_ID);
                ServiceComponentList xOpiaceos = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OPIACEOS_ID);
                ServiceComponentList xMetadona = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.METADONA_ID);
                ServiceComponentList xFenciclidina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FENCICLIDINA_ID);
                ServiceComponentList xExtasis = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXTASIS_ID);


                ServiceComponentList plomoSangre = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PLOMO_SANGRE_ID);

                //if (examenesToxicologia.Count > 0)
                if (xDosajeAlcohol != null || xCocaina != null || xMarihuana != null || xAnfetaminas != null || xBarbituricos != null || xBenzodiazepinas != null ||
                    xMetanfetaminas != null || xMorfina != null || xOpiaceos != null || xMetadona != null || xFenciclidina != null || xExtasis != null ||
                    toxicologico_todos != null || toxicologico10_parametros_Hdbay != null || xDosajeAlcoholO != null)
                {
                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("TOXICOLOGIA", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE },    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("METODOLOGÍA: INMUNOCROMATOGRAFÍA DE FLUJO LATERAL", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f, Border = PdfPCell.NO_BORDER},    

                        //new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        //new PdfPCell(new Phrase("EXAMEN", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        //new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        //new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        //new PdfPCell(new Phrase("VALOR DE REFERENCIA", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        //new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                       };

                    columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);

                    cells = new List<PdfPCell>();
                    if (xDosajeAlcoholO != null)
                    {
                        var DosajeAlcohol = xDosajeAlcoholO.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALCOHOL_EN_SALIVA_RESULTADO_ID);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("ALCOHOL EN SALIVA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(DosajeAlcohol == null ? string.Empty : DosajeAlcohol.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    if (xDosajeAlcohol != null)
                    {
                        var DosajeAlcohol = xDosajeAlcohol.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_ALCOHOLEMIA_RESULTADO);
                        var DosajeAlcoholValord = xDosajeAlcohol.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_ALCOHOLEMIA_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("ALCOHOL EN SALIVA / ALIENTO", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(DosajeAlcohol == null ? string.Empty : DosajeAlcohol.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(DosajeAlcohol == null ? string.Empty : DosajeAlcohol.v_MeasurementUnitName, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    if (xCocaina != null)
                    {
                        var Cocaina = xCocaina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_COCAINA);
                        
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("COCAINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Cocaina == null ? string.Empty : Cocaina.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Cocaina == null ? string.Empty : Cocaina.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    if (xMarihuana != null)
                    {
                        var Marihuana = xMarihuana.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_MARIHUANA);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("MARIHUANA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Marihuana == null ? string.Empty : Marihuana.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Marihuana == null ? string.Empty : Marihuana.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    if (toxicologico_todos != null)
                    {
                        var cocaina = toxicologico_todos.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_T_COCAINA) == null ? "" : toxicologico_todos.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_T_COCAINA).v_Value1Name;
                        var marihuana = toxicologico_todos.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_T_MARIHUANA) == null ? "" : toxicologico_todos.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_T_MARIHUANA).v_Value1Name;

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("COCAINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(cocaina, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("MARIHUANA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(marihuana, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    if (xAnfetaminas != null)
                    {
                        var Anfetaminas = xAnfetaminas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_ANFETAMINAS_RESULTADO);
                        var AnfetaminasValord = xAnfetaminas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_ANFETAMINAS_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("ANFETAMINAS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Anfetaminas == null ? string.Empty : Anfetaminas.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Anfetaminas == null ? string.Empty : Anfetaminas.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(AnfetaminasValord == null ? string.Empty : AnfetaminasValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                    }

                    if (xBarbituricos != null)
                    {
                        var Barbituricos = xBarbituricos.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.BARBITURICOS_RESULTADO_ID);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("BARBITURICOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Barbituricos == null ? string.Empty : Barbituricos.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Barbituricos == null ? string.Empty : Barbituricos.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    if (xBenzodiazepinas != null)
                    {
                        var Benzodiazepinas = xBenzodiazepinas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_BENZODIAZEPINAS_RESULTADO);
                        var BenzodiazepinasValord = xBenzodiazepinas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_BENZODIAZEPINAS_DESEABLE);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("BENZODIAZEPINAS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Benzodiazepinas == null ? string.Empty : Benzodiazepinas.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Benzodiazepinas == null ? string.Empty : Benzodiazepinas.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(BenzodiazepinasValord == null ? string.Empty : BenzodiazepinasValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }
                    
                    if (xMetanfetaminas != null)
                    {
                        var Metanfetaminas = xMetanfetaminas.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.METANFETAMINAS_RESULTADO_ID);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("METANFETAMINAS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Metanfetaminas == null ? string.Empty : Metanfetaminas.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Metanfetaminas == null ? string.Empty : Metanfetaminas.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    if (xMorfina != null)
                    {
                        var Morfina = xMorfina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MORFINA_RESULTADO_ID);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("MORFINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Morfina == null ? string.Empty : Morfina.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Morfina == null ? string.Empty : Morfina.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }
                    

                    if (xOpiaceos != null)
                    {
                        var Opiaceos = xOpiaceos.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OPIACEOS_RESULTADO_ID);
                        
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("OPIACEOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Opiaceos == null ? string.Empty : Opiaceos.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Opiaceos == null ? string.Empty : Opiaceos.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }
                    
                    if (xMetadona != null)
                    {
                        var Metadona = xMetadona.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.METADONA_RESULTADO_ID);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("METADONA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Metadona == null ? string.Empty : Metadona.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Metadona == null ? string.Empty : Metadona.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }
                    
                    if (xFenciclidina != null)
                    {
                        var Fenciclidina = xFenciclidina.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FENCICLIDINA_RESULTADO_ID);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("FENCICLIDINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Fenciclidina == null ? string.Empty : Fenciclidina.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Fenciclidina == null ? string.Empty : Fenciclidina.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }
                    if (xExtasis != null)
                    {
                        var Extasis = xExtasis.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXTASIS_RESULTADO_ID);

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("EXTASIS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Extasis == null ? string.Empty : Extasis.v_Value1Name, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(Extasis == null ? string.Empty : Extasis.v_MeasurementUnitName, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    if (toxicologico10_parametros_Hdbay != null)
                    {
                        var anfetamina = toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_ANFETAMINA) == null ? "" : toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_ANFETAMINA).v_Value1Name;
                        var barbiturico = toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_BARBITURICO) == null ? "" : toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_BARBITURICO).v_Value1Name;
                        var benzodiacepina = toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_BENZODIACEPINAS) == null ? "" : toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_BENZODIACEPINAS).v_Value1Name;
                        var cocaina = toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_COCAINA) == null ? "" : toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_COCAINA).v_Value1Name;
                        var metanfetamina = toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_METANFETAMINA) == null ? "" : toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_METANFETAMINA).v_Value1Name;
                        var morfina = toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_MORFINA) == null ? "" : toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_MORFINA).v_Value1Name;
                        var metadona = toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_METADONA) == null ? "" : toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_METADONA).v_Value1Name;
                        var oxicodona = toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_OXICODONA) == null ? "" : toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_OXICODONA).v_Value1Name;
                        var marihuana = toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_MARIHUANA) == null ? "" : toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_MARIHUANA).v_Value1Name;
                        var extasis = toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_EXTASIS) == null ? "" : toxicologico10_parametros_Hdbay.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_EXTASIS).v_Value1Name;

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("ANFETAMINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(anfetamina, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("BARBITÚRICOS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(barbiturico, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("BENZODIACEPINAS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(benzodiacepina, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("COCAÍNA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(cocaina, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("METANFETAMINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(metanfetamina, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("MORFINA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(morfina, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("METADONA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(metadona, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("OXICODONA", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(oxicodona, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("MARIHUANA / HACHÍS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(marihuana, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("MDMA O ÉXTASIS", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase(extasis, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("---", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table); 
                }



                if ( plomoSangre != null)
                {
                    cells = new List<PdfPCell>()
                    {
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("TOXICOLOGIA", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE },    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("METODOLOGÍA: ICP-MS", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f, Border = PdfPCell.NO_BORDER},    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.BLACK },    
                        new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 3f, Border = PdfPCell.NO_BORDER},    

                        //new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    
                        //new PdfPCell(new Phrase("EXAMEN", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        //new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        //new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        //new PdfPCell(new Phrase("VALOR DE REFERENCIA", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.BLACK },    
                        //new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_cabecera, Border = PdfPCell.NO_BORDER},    

                       };

                    columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);

                    cells = new List<PdfPCell>();
                    if (plomoSangre != null)
                    {

                        var PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE = plomoSangre.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE) == null ? "" : plomoSangre.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE).v_Value1;
                        var PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE_unidad = plomoSangre.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE) == null ? "" : plomoSangre.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE).v_MeasurementUnitName;

                        var PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE_DESEABLE = plomoSangre.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE_DESEABLE) == null ? "" : plomoSangre.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE_DESEABLE).v_Value1;

                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("PLOMO EN SANGRE", fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE_unidad, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase(PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE_DESEABLE, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });
                        cells.Add(new PdfPCell(new Phrase("", fontColumnValueBold)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER });

                    }

                    

                    columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                    document.Add(table);
                }

                #endregion

                #region METALES PESADOS  -- COMENTADO

                //string[] groupMetalesPesados = new string[]
                // {
                //    Sigesoft.Common.Constants.PLOMO_SANGRE_ID, 
                //    Sigesoft.Common.Constants.CADMIO_ID, 
                //    Sigesoft.Common.Constants.PLOMO_SANGRE_MAGNESIO_ID, 
                //    Sigesoft.Common.Constants.COBRE_ID, 
                //    Sigesoft.Common.Constants.PLOMO_ID, 
                //    Sigesoft.Common.Constants.CADMIO_EN_ORINA_ID, 
                //    Sigesoft.Common.Constants.MAGNESIO_ID, 
                // };

                //var examenesMetalesPesados = examenesLab.FindAll(p => groupMetalesPesados.Contains(p.v_ComponentId));
                //cells = new List<PdfPCell>();

                //if (examenesMetalesPesados.Count > 0)
                //{


                //    cells.Add(new PdfPCell(new Phrase("ANÁLISIS", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //    cells.Add(new PdfPCell(new Phrase("RESULTADO", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //    cells.Add(new PdfPCell(new Phrase("RANGO REFERENCIAL", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //    cells.Add(new PdfPCell(new Phrase("UNIDAD", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});

                //    var xPlomoSangre = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PLOMO_SANGRE_ID);

                //    if (xPlomoSangre != null)
                //    {
                //        var PlomoSangre = xPlomoSangre.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE);
                //        var PlomoSangreValord = xPlomoSangre.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_SANGRE_BIOQUIMICA_PLOMO_SANGRE_DESEABLE);

                //        cells.Add(new PdfPCell(new Phrase("PLOMO EN SANGRE", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //        cells.Add(new PdfPCell(new Phrase(PlomoSangre == null ? string.Empty : PlomoSangre.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //        cells.Add(new PdfPCell(new Phrase(PlomoSangreValord == null ? string.Empty : PlomoSangreValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, MinimumHeight = tamaño_caldas });
                //        cells.Add(new PdfPCell(new Phrase(PlomoSangre == null ? string.Empty : PlomoSangre.v_MeasurementUnitName, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});

                //    }
                //    var xCadmio = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CADMIO_ID);

                //    if (xCadmio != null)
                //    {
                //        var Cadmio = xCadmio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CADMIO_RESULTADO);
                //        var CadmioValord = xCadmio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CADMIO_DESEABLE);

                //        cells.Add(new PdfPCell(new Phrase("CADMIO", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //        cells.Add(new PdfPCell(new Phrase(Cadmio == null ? string.Empty : Cadmio.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //        cells.Add(new PdfPCell(new Phrase(Cadmio == null ? string.Empty : Cadmio.v_MeasurementUnitName, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //        cells.Add(new PdfPCell(new Phrase(CadmioValord == null ? string.Empty : CadmioValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});

                //    }
                //    var xMagnesio = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PLOMO_SANGRE_MAGNESIO_ID);

                //    if (xMagnesio != null)
                //    {
                //        var Magnesio = xMagnesio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_SANGRE_MAGNESIO_RESULTADO_ID);
                //        var MagnesioValord = xMagnesio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_SANGRE_MAGNESIO_DESEABLE_ID);

                //        cells.Add(new PdfPCell(new Phrase("MAGNESIO", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //        cells.Add(new PdfPCell(new Phrase(Magnesio == null ? string.Empty : Magnesio.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //        cells.Add(new PdfPCell(new Phrase(Magnesio == null ? string.Empty : Magnesio.v_MeasurementUnitName, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //        cells.Add(new PdfPCell(new Phrase(MagnesioValord == null ? string.Empty : MagnesioValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});

                //    }
                //    var xCobre = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.COBRE_ID);

                //    if (xCobre != null)
                //    {
                //        var Cobre = xCobre.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COBRE_RESULTADO_ID);
                //        var CobreValord = xCobre.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COBRE_DESEABLE_ID);

                //        cells.Add(new PdfPCell(new Phrase("COBRE", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //        cells.Add(new PdfPCell(new Phrase(Cobre == null ? string.Empty : Cobre.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //        cells.Add(new PdfPCell(new Phrase(Cobre == null ? string.Empty : Cobre.v_MeasurementUnitName, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //        cells.Add(new PdfPCell(new Phrase(CobreValord == null ? string.Empty : CobreValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});

                //    }
                //    var xPlomo = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PLOMO_ID);

                //    if (xPlomo != null)
                //    {
                //        var Plomo = xPlomo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_RESULTADO);
                //        var PlomoValord = xPlomo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PLOMO_DESEABLE);

                //        cells.Add(new PdfPCell(new Phrase("PLOMO", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //        cells.Add(new PdfPCell(new Phrase(Plomo == null ? string.Empty : Plomo.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //        cells.Add(new PdfPCell(new Phrase(Plomo == null ? string.Empty : Plomo.v_MeasurementUnitName, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //        cells.Add(new PdfPCell(new Phrase(PlomoValord == null ? string.Empty : PlomoValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});

                //    }

                //    var xCadmio1 = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CADMIO_EN_ORINA_ID);

                //    if (xCadmio1 != null)
                //    {
                //        var Cadmio = xCadmio1.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CADMIO_EN_ORINA_RESULTADO_ID);
                //        var CadmioValord = xCadmio1.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CADMIO_EN_ORINA_DESEABLE_ID);

                //        cells.Add(new PdfPCell(new Phrase("CADMIO EN ORINA", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //        cells.Add(new PdfPCell(new Phrase(Cadmio == null ? string.Empty : Cadmio.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //        cells.Add(new PdfPCell(new Phrase(CadmioValord == null ? string.Empty : CadmioValord.v_Value1, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, MinimumHeight = tamaño_caldas });
                //        cells.Add(new PdfPCell(new Phrase(Cadmio == null ? string.Empty : Cadmio.v_MeasurementUnitName, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //    }

                //    var xMagnesio1 = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.MAGNESIO_ID);

                //    if (xMagnesio1 != null)
                //    {
                //        var Magnesio = xMagnesio1.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MAGNESIO_RESULTADO_ID);
                //        var MagnesioValord = xMagnesio1.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MAGNESIO_DESEABLE_ID);

                //        cells.Add(new PdfPCell(new Phrase("MAGNESIO", fontColumnValueBold)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //        cells.Add(new PdfPCell(new Phrase(Magnesio == null ? string.Empty : Magnesio.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //        cells.Add(new PdfPCell(new Phrase(Magnesio == null ? string.Empty : Magnesio.v_MeasurementUnitName, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});
                //        cells.Add(new PdfPCell(new Phrase(MagnesioValord == null ? string.Empty : MagnesioValord.v_Value1, fontColumnValue)) { Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda_1, Border = PdfPCell.NO_BORDER});

                //    }
                //    columnWidths = new float[] { 25f, 25f, 25f, 25f };
                //    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "METALES PESADOS", fontTitleTableNegro, null);
                //    document.Add(table);
                //}



                #endregion

                #region Firma y sello Médico
                var lab = serviceComponent.Find(p => p.i_CategoryId == (int)Sigesoft.Common.Consultorio.Laboratorio);
                table = new PdfPTable(2);
                table.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.WidthPercentage = 40;

                columnWidths = new float[] { 15f, 25f };
                table.SetWidths(columnWidths);

                PdfPCell cellFirma = null;

                if (lab != null)
                {
                    if (lab.FirmaMedico != null)
                        cellFirma = new PdfPCell(HandlingItextSharp.GetImage(lab.FirmaMedico, null, null, 115, 38));
                    else
                        cellFirma = new PdfPCell(new Phrase(" ", fontColumnValue));
                }
                else
                {
                    cellFirma = new PdfPCell();
                }

                cellFirma.HorizontalAlignment = Element.ALIGN_RIGHT;
                cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellFirma.MinimumHeight = 50f;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("", fontColumnValueBold)) {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 8f,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE},    
                    new PdfPCell(cellFirma){MinimumHeight = 60f, HorizontalAlignment = PdfPCell.ALIGN_RIGHT,  UseVariableBorders = true, BorderColorLeft = BaseColor.WHITE, BorderColorRight = BaseColor.WHITE, BorderColorBottom = BaseColor.WHITE, BorderColorTop = BaseColor.WHITE},
                };
                columnWidths = new float[] { 100F };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);

                document.Add(filiationWorker);

                //cell = new PdfPCell(new Phrase("FIRMA Y SELLO MÉDICO", fontColumnValue));
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.VerticalAlignment = Element.ALIGN_MIDDLE;

                //table.AddCell(cell);
                //table.AddCell(cellFirma);

                //document.Add(table);

                #endregion

                document.Close();
                writer.Close();
                writer.Dispose();

            }
            catch (DocumentException)
            {
                throw;
            }

            catch (IOException)
            {
                throw;
            }
        }

        #endregion
    }
}
