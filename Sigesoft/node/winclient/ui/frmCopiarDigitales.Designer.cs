namespace Sigesoft.Node.WinClient.UI
{
    partial class frmCopiarDigitales
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Ruta");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Extension");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("InformacionArchivo");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            this.grData = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.rbDigitales = new System.Windows.Forms.RadioButton();
            this.rbExpedientes = new System.Windows.Forms.RadioButton();
            this.btnExportExamenes = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grData)).BeginInit();
            this.SuspendLayout();
            // 
            // grData
            // 
            this.grData.CausesValidation = false;
            appearance1.BackColor2 = System.Drawing.Color.White;
            appearance1.BorderColor = System.Drawing.Color.DarkGray;
            appearance1.ForeColor = System.Drawing.Color.Black;
            this.grData.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 870;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3});
            appearance2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance2.BackColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.Color.DarkBlue;
            appearance2.TextHAlignAsString = "Left";
            ultraGridBand1.Header.Appearance = appearance2;
            this.grData.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grData.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grData.DisplayLayout.DefaultSelectedBackColor = System.Drawing.Color.Empty;
            this.grData.DisplayLayout.DefaultSelectedForeColor = System.Drawing.Color.White;
            appearance3.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            this.grData.DisplayLayout.EmptyRowSettings.CellAppearance = appearance3;
            appearance4.BackGradientAlignment = Infragistics.Win.GradientAlignment.Container;
            appearance4.BackHatchStyle = Infragistics.Win.BackHatchStyle.None;
            appearance4.BorderAlpha = Infragistics.Win.Alpha.Opaque;
            this.grData.DisplayLayout.EmptyRowSettings.RowAppearance = appearance4;
            this.grData.DisplayLayout.InterBandSpacing = 10;
            this.grData.DisplayLayout.MaxColScrollRegions = 1;
            this.grData.DisplayLayout.MaxRowScrollRegions = 1;
            this.grData.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grData.DisplayLayout.Override.ActiveAppearancesEnabled = Infragistics.Win.DefaultableBoolean.True;
            this.grData.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grData.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grData.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grData.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grData.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grData.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance5.BackColor = System.Drawing.Color.Transparent;
            this.grData.DisplayLayout.Override.CardAreaAppearance = appearance5;
            appearance6.BackColor = System.Drawing.Color.White;
            appearance6.BackColor2 = System.Drawing.Color.White;
            appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance6.BorderAlpha = Infragistics.Win.Alpha.Transparent;
            this.grData.DisplayLayout.Override.CellAppearance = appearance6;
            this.grData.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance7.BackColor = System.Drawing.Color.White;
            appearance7.BackColor2 = System.Drawing.Color.LightGray;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance7.BorderAlpha = Infragistics.Win.Alpha.Opaque;
            appearance7.BorderColor = System.Drawing.Color.DarkGray;
            appearance7.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grData.DisplayLayout.Override.HeaderAppearance = appearance7;
            this.grData.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance8.AlphaLevel = ((short)(187));
            appearance8.BackColor = System.Drawing.Color.Gainsboro;
            appearance8.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance8.ForeColor = System.Drawing.Color.Black;
            appearance8.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grData.DisplayLayout.Override.RowAlternateAppearance = appearance8;
            this.grData.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance9.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance9.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance9.BackColorAlpha = Infragistics.Win.Alpha.UseAlphaLevel;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance9.BorderAlpha = Infragistics.Win.Alpha.Transparent;
            appearance9.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance9.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance9.FontData.BoldAsString = "False";
            appearance9.ForeColor = System.Drawing.Color.Black;
            this.grData.DisplayLayout.Override.SelectedRowAppearance = appearance9;
            this.grData.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grData.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grData.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grData.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grData.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grData.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grData.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grData.Location = new System.Drawing.Point(11, 47);
            this.grData.Margin = new System.Windows.Forms.Padding(2);
            this.grData.Name = "grData";
            this.grData.Size = new System.Drawing.Size(934, 439);
            this.grData.TabIndex = 107;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.Image = global::Sigesoft.Node.WinClient.UI.Resources.brick_go;
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(872, 9);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(73, 29);
            this.btnExport.TabIndex = 111;
            this.btnExport.Text = "Copiar";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCount.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRecordCount.Location = new System.Drawing.Point(564, 14);
            this.lblRecordCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(231, 19);
            this.lblRecordCount.TabIndex = 110;
            this.lblRecordCount.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rbDigitales
            // 
            this.rbDigitales.AutoSize = true;
            this.rbDigitales.Checked = true;
            this.rbDigitales.Location = new System.Drawing.Point(13, 13);
            this.rbDigitales.Name = "rbDigitales";
            this.rbDigitales.Size = new System.Drawing.Size(100, 17);
            this.rbDigitales.TabIndex = 112;
            this.rbDigitales.TabStop = true;
            this.rbDigitales.Text = "Placas Digitales";
            this.rbDigitales.UseVisualStyleBackColor = true;
            this.rbDigitales.CheckedChanged += new System.EventHandler(this.rbDigitales_CheckedChanged);
            // 
            // rbExpedientes
            // 
            this.rbExpedientes.AutoSize = true;
            this.rbExpedientes.Location = new System.Drawing.Point(176, 13);
            this.rbExpedientes.Name = "rbExpedientes";
            this.rbExpedientes.Size = new System.Drawing.Size(107, 17);
            this.rbExpedientes.TabIndex = 113;
            this.rbExpedientes.TabStop = true;
            this.rbExpedientes.Text = "Expedientes PDF";
            this.rbExpedientes.UseVisualStyleBackColor = true;
            this.rbExpedientes.CheckedChanged += new System.EventHandler(this.rbExpedientes_CheckedChanged);
            // 
            // btnExportExamenes
            // 
            this.btnExportExamenes.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnExportExamenes.Image = global::Sigesoft.Node.WinClient.UI.Resources.page_excel;
            this.btnExportExamenes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExamenes.Location = new System.Drawing.Point(840, 13);
            this.btnExportExamenes.Name = "btnExportExamenes";
            this.btnExportExamenes.Size = new System.Drawing.Size(26, 24);
            this.btnExportExamenes.TabIndex = 114;
            this.btnExportExamenes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportExamenes.UseVisualStyleBackColor = true;
            this.btnExportExamenes.Click += new System.EventHandler(this.btnExportExamenes_Click);
            // 
            // frmCopiarDigitales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 497);
            this.Controls.Add(this.btnExportExamenes);
            this.Controls.Add(this.rbExpedientes);
            this.Controls.Add(this.rbDigitales);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lblRecordCount);
            this.Controls.Add(this.grData);
            this.Name = "frmCopiarDigitales";
            this.Text = "frmCopiarDigitales";
            this.Load += new System.EventHandler(this.frmCopiarDigitales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid grData;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblRecordCount;
        private System.Windows.Forms.RadioButton rbDigitales;
        private System.Windows.Forms.RadioButton rbExpedientes;
        private System.Windows.Forms.Button btnExportExamenes;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
    }
}