namespace Sigesoft.Node.WinClient.UI.Visor
{
    partial class frmVisorPdf
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Servicio");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Paciente");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Protocolo", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Atencion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Historia");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Interconsultas");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Recetas");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Ordenes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Anexos");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVisorPdf));
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Archivo", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Ruta");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Origen");
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnReportAsync = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.grData = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.lblPaciente = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.lbArchivos = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tabPage = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelMensaje = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pdfAtencion = new AxAcroPDFLib.AxAcroPDF();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panelInterconsultas = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.pdfInterconsultas = new AxAcroPDFLib.AxAcroPDF();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panelRecetas = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.pdfRecetas = new AxAcroPDFLib.AxAcroPDF();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panelOrdenes = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.pdfOrdenesMedicas = new AxAcroPDFLib.AxAcroPDF();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.pdfHistoriaAdjunta = new AxAcroPDFLib.AxAcroPDF();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabPage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panelMensaje.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pdfAtencion)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panelInterconsultas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pdfInterconsultas)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.panelRecetas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pdfRecetas)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.panelOrdenes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pdfOrdenesMedicas)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pdfHistoriaAdjunta)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnReportAsync);
            this.splitContainer1.Panel1.Controls.Add(this.btnExport);
            this.splitContainer1.Panel1.Controls.Add(this.grData);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel1.Controls.Add(this.lblRecordCount);
            this.splitContainer1.Panel1.Controls.Add(this.lblPaciente);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.txtRuta);
            this.splitContainer1.Panel1.Controls.Add(this.lbArchivos);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox2);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabPage);
            this.splitContainer1.Size = new System.Drawing.Size(1242, 657);
            this.splitContainer1.SplitterDistance = 598;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnReportAsync
            // 
            this.btnReportAsync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReportAsync.BackColor = System.Drawing.SystemColors.Control;
            this.btnReportAsync.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnReportAsync.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnReportAsync.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnReportAsync.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReportAsync.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportAsync.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnReportAsync.Image = global::Sigesoft.Node.WinClient.UI.Resources.color_swatch;
            this.btnReportAsync.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportAsync.Location = new System.Drawing.Point(85, 571);
            this.btnReportAsync.Margin = new System.Windows.Forms.Padding(2);
            this.btnReportAsync.Name = "btnReportAsync";
            this.btnReportAsync.Size = new System.Drawing.Size(55, 29);
            this.btnReportAsync.TabIndex = 156;
            this.btnReportAsync.Text = "C";
            this.btnReportAsync.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReportAsync.UseVisualStyleBackColor = false;
            this.btnReportAsync.Click += new System.EventHandler(this.btnReportAsync_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.Image = global::Sigesoft.Node.WinClient.UI.Resources.page_excel;
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(7, 571);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(73, 29);
            this.btnExport.TabIndex = 109;
            this.btnExport.Text = "Exportar";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // grData
            // 
            this.grData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grData.CausesValidation = false;
            appearance1.BackColor2 = System.Drawing.Color.White;
            appearance1.BorderColor = System.Drawing.Color.DarkGray;
            appearance1.ForeColor = System.Drawing.Color.Black;
            this.grData.DisplayLayout.Appearance = appearance1;
            ultraGridColumn12.Header.VisiblePosition = 0;
            ultraGridColumn12.Width = 83;
            ultraGridColumn13.Header.VisiblePosition = 1;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn13.Width = 194;
            ultraGridColumn14.Header.Caption = "PROTOCOLO ATENDIDO";
            ultraGridColumn14.Header.VisiblePosition = 2;
            ultraGridColumn14.Width = 342;
            ultraGridColumn15.Header.Caption = "FECHA DE ATENCION";
            ultraGridColumn15.Header.VisiblePosition = 3;
            ultraGridColumn16.Header.VisiblePosition = 4;
            ultraGridColumn17.Header.VisiblePosition = 5;
            ultraGridColumn17.Hidden = true;
            ultraGridColumn18.Header.VisiblePosition = 6;
            ultraGridColumn18.Hidden = true;
            ultraGridColumn19.Header.VisiblePosition = 7;
            ultraGridColumn19.Hidden = true;
            ultraGridColumn20.Header.VisiblePosition = 8;
            ultraGridColumn20.Hidden = true;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn19,
            ultraGridColumn20});
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
            this.grData.Location = new System.Drawing.Point(7, 84);
            this.grData.Margin = new System.Windows.Forms.Padding(2);
            this.grData.Name = "grData";
            this.grData.Size = new System.Drawing.Size(590, 482);
            this.grData.TabIndex = 106;
            this.grData.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.grData_AfterSelectChange);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = global::Sigesoft.Node.WinClient.UI.Resources.historias_1;
            this.pictureBox1.Location = new System.Drawing.Point(515, 571);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 88);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 105;
            this.pictureBox1.TabStop = false;
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCount.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRecordCount.Location = new System.Drawing.Point(355, 63);
            this.lblRecordCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(231, 19);
            this.lblRecordCount.TabIndex = 53;
            this.lblRecordCount.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPaciente
            // 
            this.lblPaciente.AutoSize = true;
            this.lblPaciente.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaciente.ForeColor = System.Drawing.Color.Red;
            this.lblPaciente.Location = new System.Drawing.Point(74, 28);
            this.lblPaciente.Name = "lblPaciente";
            this.lblPaciente.Size = new System.Drawing.Size(48, 25);
            this.lblPaciente.TabIndex = 50;
            this.lblPaciente.Text = "- - -";
            this.lblPaciente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(74, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(321, 25);
            this.label1.TabIndex = 49;
            this.label1.Text = "HISTORIAL DE ATENCIONES :";
            // 
            // txtRuta
            // 
            this.txtRuta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRuta.Location = new System.Drawing.Point(439, 14);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(156, 20);
            this.txtRuta.TabIndex = 2;
            this.txtRuta.Visible = false;
            // 
            // lbArchivos
            // 
            this.lbArchivos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbArchivos.FormattingEnabled = true;
            this.lbArchivos.Location = new System.Drawing.Point(439, 40);
            this.lbArchivos.Name = "lbArchivos";
            this.lbArchivos.Size = new System.Drawing.Size(156, 17);
            this.lbArchivos.TabIndex = 1;
            this.lbArchivos.Visible = false;
            this.lbArchivos.SelectedValueChanged += new System.EventHandler(this.lbArchivos_SelectedValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(439, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Sigesoft.Node.WinClient.UI.Resources.LOGO_SAN_LORENZO_SL;
            this.pictureBox2.Location = new System.Drawing.Point(7, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(39, 76);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 108;
            this.pictureBox2.TabStop = false;
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.tabPage1);
            this.tabPage.Controls.Add(this.tabPage2);
            this.tabPage.Controls.Add(this.tabPage3);
            this.tabPage.Controls.Add(this.tabPage4);
            this.tabPage.Controls.Add(this.tabPage5);
            this.tabPage.Controls.Add(this.tabPage6);
            this.tabPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage.Location = new System.Drawing.Point(0, 0);
            this.tabPage.Name = "tabPage";
            this.tabPage.SelectedIndex = 0;
            this.tabPage.Size = new System.Drawing.Size(640, 657);
            this.tabPage.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelMensaje);
            this.tabPage1.Controls.Add(this.pdfAtencion);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(632, 631);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Historia";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelMensaje
            // 
            this.panelMensaje.Controls.Add(this.label4);
            this.panelMensaje.Controls.Add(this.label3);
            this.panelMensaje.Location = new System.Drawing.Point(0, 3);
            this.panelMensaje.Name = "panelMensaje";
            this.panelMensaje.Size = new System.Drawing.Size(629, 629);
            this.panelMensaje.TabIndex = 2;
            this.panelMensaje.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(77, 346);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(489, 25);
            this.label4.TabIndex = 51;
            this.label4.Text = "DIRIGETE AL COMPAGINADOR DE EXAMENES";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(143, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(335, 25);
            this.label3.TabIndex = 50;
            this.label3.Text = "SIN DOCUMENTO A MOSTRAR.";
            // 
            // pdfAtencion
            // 
            this.pdfAtencion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfAtencion.Enabled = true;
            this.pdfAtencion.Location = new System.Drawing.Point(3, 3);
            this.pdfAtencion.Name = "pdfAtencion";
            this.pdfAtencion.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("pdfAtencion.OcxState")));
            this.pdfAtencion.Size = new System.Drawing.Size(626, 625);
            this.pdfAtencion.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panelInterconsultas);
            this.tabPage2.Controls.Add(this.pdfInterconsultas);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(632, 631);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Interconsultas - Adjuntos";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panelInterconsultas
            // 
            this.panelInterconsultas.Controls.Add(this.label6);
            this.panelInterconsultas.Location = new System.Drawing.Point(7, 7);
            this.panelInterconsultas.Name = "panelInterconsultas";
            this.panelInterconsultas.Size = new System.Drawing.Size(620, 617);
            this.panelInterconsultas.TabIndex = 3;
            this.panelInterconsultas.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(158, 288);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(335, 25);
            this.label6.TabIndex = 50;
            this.label6.Text = "SIN DOCUMENTO A MOSTRAR.";
            // 
            // pdfInterconsultas
            // 
            this.pdfInterconsultas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfInterconsultas.Enabled = true;
            this.pdfInterconsultas.Location = new System.Drawing.Point(3, 3);
            this.pdfInterconsultas.Name = "pdfInterconsultas";
            this.pdfInterconsultas.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("pdfInterconsultas.OcxState")));
            this.pdfInterconsultas.Size = new System.Drawing.Size(626, 625);
            this.pdfInterconsultas.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panelRecetas);
            this.tabPage3.Controls.Add(this.pdfRecetas);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(632, 631);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Recetas";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panelRecetas
            // 
            this.panelRecetas.Controls.Add(this.label8);
            this.panelRecetas.Location = new System.Drawing.Point(7, 7);
            this.panelRecetas.Name = "panelRecetas";
            this.panelRecetas.Size = new System.Drawing.Size(620, 617);
            this.panelRecetas.TabIndex = 4;
            this.panelRecetas.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(158, 288);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(335, 25);
            this.label8.TabIndex = 50;
            this.label8.Text = "SIN DOCUMENTO A MOSTRAR.";
            // 
            // pdfRecetas
            // 
            this.pdfRecetas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfRecetas.Enabled = true;
            this.pdfRecetas.Location = new System.Drawing.Point(3, 3);
            this.pdfRecetas.Name = "pdfRecetas";
            this.pdfRecetas.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("pdfRecetas.OcxState")));
            this.pdfRecetas.Size = new System.Drawing.Size(626, 625);
            this.pdfRecetas.TabIndex = 3;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panelOrdenes);
            this.tabPage4.Controls.Add(this.pdfOrdenesMedicas);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(632, 631);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Ordenes Médicas";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panelOrdenes
            // 
            this.panelOrdenes.Controls.Add(this.label10);
            this.panelOrdenes.Location = new System.Drawing.Point(7, 7);
            this.panelOrdenes.Name = "panelOrdenes";
            this.panelOrdenes.Size = new System.Drawing.Size(620, 617);
            this.panelOrdenes.TabIndex = 5;
            this.panelOrdenes.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(158, 288);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(335, 25);
            this.label10.TabIndex = 50;
            this.label10.Text = "SIN DOCUMENTO A MOSTRAR.";
            // 
            // pdfOrdenesMedicas
            // 
            this.pdfOrdenesMedicas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfOrdenesMedicas.Enabled = true;
            this.pdfOrdenesMedicas.Location = new System.Drawing.Point(3, 3);
            this.pdfOrdenesMedicas.Name = "pdfOrdenesMedicas";
            this.pdfOrdenesMedicas.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("pdfOrdenesMedicas.OcxState")));
            this.pdfOrdenesMedicas.Size = new System.Drawing.Size(626, 625);
            this.pdfOrdenesMedicas.TabIndex = 4;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.button3);
            this.tabPage5.Controls.Add(this.label2);
            this.tabPage5.Controls.Add(this.button2);
            this.tabPage5.Controls.Add(this.ultraGrid1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(632, 631);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Anexos";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.system_save;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(415, 484);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 29);
            this.button3.TabIndex = 112;
            this.button3.Text = "Descargar";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(23, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 25);
            this.label2.TabIndex = 111;
            this.label2.Text = "Adjuntos del Servicio:";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Image = global::Sigesoft.Node.WinClient.UI.Resources.page_white_find;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(527, 484);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(73, 29);
            this.button2.TabIndex = 110;
            this.button2.Text = "Abrir";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ultraGrid1
            // 
            this.ultraGrid1.CausesValidation = false;
            appearance10.BackColor2 = System.Drawing.Color.White;
            appearance10.BorderColor = System.Drawing.Color.DarkGray;
            appearance10.ForeColor = System.Drawing.Color.Black;
            this.ultraGrid1.DisplayLayout.Appearance = appearance10;
            ultraGridColumn10.Header.VisiblePosition = 1;
            ultraGridColumn10.Width = 420;
            ultraGridColumn11.Header.VisiblePosition = 2;
            ultraGridColumn11.Hidden = true;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 149;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn1});
            appearance11.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance11.BackColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance11.FontData.SizeInPoints = 8F;
            appearance11.ForeColor = System.Drawing.Color.DarkBlue;
            appearance11.TextHAlignAsString = "Left";
            ultraGridBand2.Header.Appearance = appearance11;
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.ultraGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGrid1.DisplayLayout.DefaultSelectedBackColor = System.Drawing.Color.Empty;
            this.ultraGrid1.DisplayLayout.DefaultSelectedForeColor = System.Drawing.Color.White;
            appearance12.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            this.ultraGrid1.DisplayLayout.EmptyRowSettings.CellAppearance = appearance12;
            appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Container;
            appearance13.BackHatchStyle = Infragistics.Win.BackHatchStyle.None;
            appearance13.BorderAlpha = Infragistics.Win.Alpha.Opaque;
            this.ultraGrid1.DisplayLayout.EmptyRowSettings.RowAppearance = appearance13;
            this.ultraGrid1.DisplayLayout.InterBandSpacing = 10;
            this.ultraGrid1.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGrid1.DisplayLayout.MaxRowScrollRegions = 1;
            this.ultraGrid1.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.ultraGrid1.DisplayLayout.Override.ActiveAppearancesEnabled = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGrid1.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.ultraGrid1.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid1.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGrid1.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.ultraGrid1.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid1.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance14.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid1.DisplayLayout.Override.CardAreaAppearance = appearance14;
            appearance15.BackColor = System.Drawing.Color.White;
            appearance15.BackColor2 = System.Drawing.Color.White;
            appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance15.BorderAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid1.DisplayLayout.Override.CellAppearance = appearance15;
            this.ultraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance16.BackColor = System.Drawing.Color.White;
            appearance16.BackColor2 = System.Drawing.Color.LightGray;
            appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance16.BorderAlpha = Infragistics.Win.Alpha.Opaque;
            appearance16.BorderColor = System.Drawing.Color.DarkGray;
            appearance16.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid1.DisplayLayout.Override.HeaderAppearance = appearance16;
            this.ultraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance17.AlphaLevel = ((short)(187));
            appearance17.BackColor = System.Drawing.Color.Gainsboro;
            appearance17.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance17.ForeColor = System.Drawing.Color.Black;
            appearance17.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.ultraGrid1.DisplayLayout.Override.RowAlternateAppearance = appearance17;
            this.ultraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance18.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance18.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance18.BackColorAlpha = Infragistics.Win.Alpha.UseAlphaLevel;
            appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance18.BorderAlpha = Infragistics.Win.Alpha.Transparent;
            appearance18.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance18.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance18.FontData.BoldAsString = "False";
            appearance18.ForeColor = System.Drawing.Color.Black;
            this.ultraGrid1.DisplayLayout.Override.SelectedRowAppearance = appearance18;
            this.ultraGrid1.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.ultraGrid1.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.ultraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGrid1.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.ultraGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraGrid1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.ultraGrid1.Location = new System.Drawing.Point(14, 41);
            this.ultraGrid1.Margin = new System.Windows.Forms.Padding(2);
            this.ultraGrid1.Name = "ultraGrid1";
            this.ultraGrid1.Size = new System.Drawing.Size(586, 438);
            this.ultraGrid1.TabIndex = 107;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.button4);
            this.tabPage6.Controls.Add(this.pdfHistoriaAdjunta);
            this.tabPage6.Controls.Add(this.btnAceptar);
            this.tabPage6.Controls.Add(this.txtFileName);
            this.tabPage6.Controls.Add(this.label5);
            this.tabPage6.Controls.Add(this.btnBrowser);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(632, 631);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "HISTORIA MANUAL ADJUNTA";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.delete;
            this.button4.Location = new System.Drawing.Point(597, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(32, 31);
            this.button4.TabIndex = 98;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // pdfHistoriaAdjunta
            // 
            this.pdfHistoriaAdjunta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pdfHistoriaAdjunta.Enabled = true;
            this.pdfHistoriaAdjunta.Location = new System.Drawing.Point(8, 42);
            this.pdfHistoriaAdjunta.Name = "pdfHistoriaAdjunta";
            this.pdfHistoriaAdjunta.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("pdfHistoriaAdjunta.OcxState")));
            this.pdfHistoriaAdjunta.Size = new System.Drawing.Size(626, 591);
            this.pdfHistoriaAdjunta.TabIndex = 97;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Enabled = false;
            this.btnAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptar.Image")));
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptar.Location = new System.Drawing.Point(479, 6);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(98, 31);
            this.btnAceptar.TabIndex = 96;
            this.btnAceptar.Text = "&Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.BackColor = System.Drawing.SystemColors.Control;
            this.txtFileName.Location = new System.Drawing.Point(53, 11);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(382, 20);
            this.txtFileName.TabIndex = 95;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 94;
            this.label5.Text = "Nombre";
            // 
            // btnBrowser
            // 
            this.btnBrowser.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowser.Image")));
            this.btnBrowser.Location = new System.Drawing.Point(441, 6);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(32, 31);
            this.btnBrowser.TabIndex = 93;
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmVisorPdf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 657);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmVisorPdf";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmVisorPdf";
            this.Load += new System.EventHandler(this.frmVisorPdf_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabPage.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panelMensaje.ResumeLayout(false);
            this.panelMensaje.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pdfAtencion)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panelInterconsultas.ResumeLayout(false);
            this.panelInterconsultas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pdfInterconsultas)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.panelRecetas.ResumeLayout(false);
            this.panelRecetas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pdfRecetas)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.panelOrdenes.ResumeLayout(false);
            this.panelOrdenes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pdfOrdenesMedicas)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pdfHistoriaAdjunta)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lbArchivos;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.TabControl tabPage;
        private System.Windows.Forms.TabPage tabPage1;
        private AxAcroPDFLib.AxAcroPDF pdfAtencion;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private AxAcroPDFLib.AxAcroPDF pdfInterconsultas;
        private AxAcroPDFLib.AxAcroPDF pdfRecetas;
        private System.Windows.Forms.TabPage tabPage4;
        private AxAcroPDFLib.AxAcroPDF pdfOrdenesMedicas;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label lblPaciente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRecordCount;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Infragistics.Win.UltraWinGrid.UltraGrid grData;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
        private System.Windows.Forms.Panel panelMensaje;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelInterconsultas;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panelRecetas;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panelOrdenes;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnReportAsync;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private AxAcroPDFLib.AxAcroPDF pdfHistoriaAdjunta;
        private System.Windows.Forms.Button button4;
    }
}