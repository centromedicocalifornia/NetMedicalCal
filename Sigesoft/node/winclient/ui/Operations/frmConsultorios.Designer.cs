namespace Sigesoft.Node.WinClient.UI.Operations
{
    partial class frmConsultorios
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DiagnosticRepositoryId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn89 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DiseasesId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DiseasesName", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.None, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_AutoManualName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_RestrictionsName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_RecomendationsName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_PreQualificationName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_AutoManualId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentFieldsId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_RecordStatus");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_RecordType");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConsultorios));
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.tcExamList = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.gbDiagnosticoExamen = new System.Windows.Forms.GroupBox();
            this.grdDiagnosticoPorExamenComponente = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnRemoverDxExamen = new System.Windows.Forms.Button();
            this.btnEditarDxExamen = new System.Windows.Forms.Button();
            this.btnAgregarDxExamen = new System.Windows.Forms.Button();
            this.lblRecordCountDiagnosticoPorExamenCom = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbEstadoComponente = new System.Windows.Forms.ComboBox();
            this.EXAMENES_lblEstadoComponente = new System.Windows.Forms.Label();
            this.btnVisorReporteExamen = new System.Windows.Forms.Button();
            this.btnReceta = new System.Windows.Forms.Button();
            this.btnGuardarExamen = new System.Windows.Forms.Button();
            this.btnCerrarESO = new System.Windows.Forms.Button();
            this.chkUtilizarFirma = new System.Windows.Forms.CheckBox();
            this.chkApproved = new System.Windows.Forms.CheckBox();
            this.EXAMENES_lblComentarios = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.cbTipoProcedenciaExamen = new System.Windows.Forms.ComboBox();
            this.txtComentario = new System.Windows.Forms.TextBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnPerson = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.lblTrabajador = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnViewWorker = new System.Windows.Forms.Button();
            this.lblServicio = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblTipoEso = new System.Windows.Forms.Label();
            this.lblProtocolName = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblView = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblFechaGraba = new System.Windows.Forms.Label();
            this.lblUsuAct = new System.Windows.Forms.Label();
            this.lblUsuGraba = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tcExamList)).BeginInit();
            this.tcExamList.SuspendLayout();
            this.gbDiagnosticoExamen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagnosticoPorExamenComponente)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Font = new System.Drawing.Font("Microsoft Tai Le", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(20, 1);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(1279, 394);
            // 
            // tcExamList
            // 
            this.tcExamList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcExamList.Controls.Add(this.ultraTabSharedControlsPage1);
            this.tcExamList.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcExamList.Location = new System.Drawing.Point(9, 4);
            this.tcExamList.Name = "tcExamList";
            this.tcExamList.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.tcExamList.Size = new System.Drawing.Size(1300, 396);
            this.tcExamList.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
            this.tcExamList.TabIndex = 52;
            this.tcExamList.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.LeftTop;
            this.tcExamList.TextOrientation = Infragistics.Win.UltraWinTabs.TextOrientation.Horizontal;
            this.tcExamList.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007;
            this.tcExamList.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(this.tcExamList_SelectedTabChanged);
            // 
            // gbDiagnosticoExamen
            // 
            this.gbDiagnosticoExamen.Controls.Add(this.grdDiagnosticoPorExamenComponente);
            this.gbDiagnosticoExamen.Controls.Add(this.btnRemoverDxExamen);
            this.gbDiagnosticoExamen.Controls.Add(this.btnEditarDxExamen);
            this.gbDiagnosticoExamen.Controls.Add(this.btnAgregarDxExamen);
            this.gbDiagnosticoExamen.Controls.Add(this.lblRecordCountDiagnosticoPorExamenCom);
            this.gbDiagnosticoExamen.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDiagnosticoExamen.ForeColor = System.Drawing.Color.MediumBlue;
            this.gbDiagnosticoExamen.Location = new System.Drawing.Point(12, 404);
            this.gbDiagnosticoExamen.Name = "gbDiagnosticoExamen";
            this.gbDiagnosticoExamen.Size = new System.Drawing.Size(693, 175);
            this.gbDiagnosticoExamen.TabIndex = 53;
            this.gbDiagnosticoExamen.TabStop = false;
            this.gbDiagnosticoExamen.Text = "Diagnosticos del Examen";
            // 
            // grdDiagnosticoPorExamenComponente
            // 
            this.grdDiagnosticoPorExamenComponente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDiagnosticoPorExamenComponente.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Appearance = appearance1;
            ultraGridColumn45.Header.VisiblePosition = 0;
            ultraGridColumn45.Hidden = true;
            ultraGridColumn89.Header.VisiblePosition = 1;
            ultraGridColumn89.Hidden = true;
            ultraGridColumn3.Header.Caption = "Diagnóstico";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 266;
            ultraGridColumn4.Header.Caption = "Automatico?";
            ultraGridColumn4.Header.VisiblePosition = 5;
            ultraGridColumn5.Header.Caption = "Restricciones";
            ultraGridColumn5.Header.VisiblePosition = 3;
            ultraGridColumn5.Width = 192;
            ultraGridColumn6.Header.Caption = "Recomendaciones";
            ultraGridColumn6.Header.VisiblePosition = 4;
            ultraGridColumn6.Width = 182;
            ultraGridColumn7.Header.Caption = "Pre-Calificación";
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.Hidden = true;
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.Hidden = true;
            ultraGridColumn10.Header.VisiblePosition = 9;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn11.Header.VisiblePosition = 10;
            ultraGridColumn11.Hidden = true;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn45,
            ultraGridColumn89,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11});
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.InterBandSpacing = 10;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.MaxColScrollRegions = 1;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.Color.White;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.BorderColor = System.Drawing.Color.DarkGray;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForeColor = System.Drawing.Color.Black;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Circular;
            appearance7.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDiagnosticoPorExamenComponente.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdDiagnosticoPorExamenComponente.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDiagnosticoPorExamenComponente.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdDiagnosticoPorExamenComponente.Location = new System.Drawing.Point(5, 22);
            this.grdDiagnosticoPorExamenComponente.Margin = new System.Windows.Forms.Padding(2);
            this.grdDiagnosticoPorExamenComponente.Name = "grdDiagnosticoPorExamenComponente";
            this.grdDiagnosticoPorExamenComponente.Size = new System.Drawing.Size(683, 118);
            this.grdDiagnosticoPorExamenComponente.TabIndex = 92;
            // 
            // btnRemoverDxExamen
            // 
            this.btnRemoverDxExamen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoverDxExamen.BackColor = System.Drawing.SystemColors.Control;
            this.btnRemoverDxExamen.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnRemoverDxExamen.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnRemoverDxExamen.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnRemoverDxExamen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoverDxExamen.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoverDxExamen.ForeColor = System.Drawing.Color.Black;
            this.btnRemoverDxExamen.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoverDxExamen.Image")));
            this.btnRemoverDxExamen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoverDxExamen.Location = new System.Drawing.Point(176, 144);
            this.btnRemoverDxExamen.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemoverDxExamen.Name = "btnRemoverDxExamen";
            this.btnRemoverDxExamen.Size = new System.Drawing.Size(80, 24);
            this.btnRemoverDxExamen.TabIndex = 91;
            this.btnRemoverDxExamen.Text = "     Eliminar";
            this.btnRemoverDxExamen.UseVisualStyleBackColor = false;
            // 
            // btnEditarDxExamen
            // 
            this.btnEditarDxExamen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditarDxExamen.BackColor = System.Drawing.SystemColors.Control;
            this.btnEditarDxExamen.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnEditarDxExamen.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnEditarDxExamen.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnEditarDxExamen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarDxExamen.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarDxExamen.ForeColor = System.Drawing.Color.Black;
            this.btnEditarDxExamen.Image = ((System.Drawing.Image)(resources.GetObject("btnEditarDxExamen.Image")));
            this.btnEditarDxExamen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditarDxExamen.Location = new System.Drawing.Point(92, 144);
            this.btnEditarDxExamen.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditarDxExamen.Name = "btnEditarDxExamen";
            this.btnEditarDxExamen.Size = new System.Drawing.Size(80, 24);
            this.btnEditarDxExamen.TabIndex = 90;
            this.btnEditarDxExamen.Text = "Modificar";
            this.btnEditarDxExamen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditarDxExamen.UseVisualStyleBackColor = false;
            // 
            // btnAgregarDxExamen
            // 
            this.btnAgregarDxExamen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregarDxExamen.BackColor = System.Drawing.SystemColors.Control;
            this.btnAgregarDxExamen.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnAgregarDxExamen.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnAgregarDxExamen.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnAgregarDxExamen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarDxExamen.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarDxExamen.ForeColor = System.Drawing.Color.Black;
            this.btnAgregarDxExamen.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarDxExamen.Image")));
            this.btnAgregarDxExamen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarDxExamen.Location = new System.Drawing.Point(8, 144);
            this.btnAgregarDxExamen.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarDxExamen.Name = "btnAgregarDxExamen";
            this.btnAgregarDxExamen.Size = new System.Drawing.Size(80, 24);
            this.btnAgregarDxExamen.TabIndex = 89;
            this.btnAgregarDxExamen.Text = "      Agregar";
            this.btnAgregarDxExamen.UseVisualStyleBackColor = false;
            // 
            // lblRecordCountDiagnosticoPorExamenCom
            // 
            this.lblRecordCountDiagnosticoPorExamenCom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRecordCountDiagnosticoPorExamenCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCountDiagnosticoPorExamenCom.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRecordCountDiagnosticoPorExamenCom.Location = new System.Drawing.Point(499, 4);
            this.lblRecordCountDiagnosticoPorExamenCom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCountDiagnosticoPorExamenCom.Name = "lblRecordCountDiagnosticoPorExamenCom";
            this.lblRecordCountDiagnosticoPorExamenCom.Size = new System.Drawing.Size(189, 19);
            this.lblRecordCountDiagnosticoPorExamenCom.TabIndex = 49;
            this.lblRecordCountDiagnosticoPorExamenCom.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCountDiagnosticoPorExamenCom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbEstadoComponente);
            this.groupBox4.Controls.Add(this.EXAMENES_lblEstadoComponente);
            this.groupBox4.Controls.Add(this.btnVisorReporteExamen);
            this.groupBox4.Controls.Add(this.btnReceta);
            this.groupBox4.Controls.Add(this.btnGuardarExamen);
            this.groupBox4.Controls.Add(this.btnCerrarESO);
            this.groupBox4.Controls.Add(this.chkUtilizarFirma);
            this.groupBox4.Controls.Add(this.chkApproved);
            this.groupBox4.Controls.Add(this.EXAMENES_lblComentarios);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.cbTipoProcedenciaExamen);
            this.groupBox4.Controls.Add(this.txtComentario);
            this.groupBox4.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox4.Location = new System.Drawing.Point(711, 401);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(598, 178);
            this.groupBox4.TabIndex = 50;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Grabar Examen";
            // 
            // cbEstadoComponente
            // 
            this.cbEstadoComponente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstadoComponente.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEstadoComponente.FormattingEnabled = true;
            this.cbEstadoComponente.Location = new System.Drawing.Point(6, 108);
            this.cbEstadoComponente.Name = "cbEstadoComponente";
            this.cbEstadoComponente.Size = new System.Drawing.Size(305, 21);
            this.cbEstadoComponente.TabIndex = 0;
            // 
            // EXAMENES_lblEstadoComponente
            // 
            this.EXAMENES_lblEstadoComponente.AutoSize = true;
            this.EXAMENES_lblEstadoComponente.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EXAMENES_lblEstadoComponente.ForeColor = System.Drawing.Color.Blue;
            this.EXAMENES_lblEstadoComponente.Location = new System.Drawing.Point(3, 89);
            this.EXAMENES_lblEstadoComponente.Name = "EXAMENES_lblEstadoComponente";
            this.EXAMENES_lblEstadoComponente.Size = new System.Drawing.Size(147, 17);
            this.EXAMENES_lblEstadoComponente.TabIndex = 51;
            this.EXAMENES_lblEstadoComponente.Text = "Estado del examen ({0})";
            // 
            // btnVisorReporteExamen
            // 
            this.btnVisorReporteExamen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVisorReporteExamen.BackColor = System.Drawing.SystemColors.Control;
            this.btnVisorReporteExamen.Enabled = false;
            this.btnVisorReporteExamen.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnVisorReporteExamen.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnVisorReporteExamen.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnVisorReporteExamen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVisorReporteExamen.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVisorReporteExamen.ForeColor = System.Drawing.Color.Black;
            this.btnVisorReporteExamen.Image = ((System.Drawing.Image)(resources.GetObject("btnVisorReporteExamen.Image")));
            this.btnVisorReporteExamen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVisorReporteExamen.Location = new System.Drawing.Point(332, 123);
            this.btnVisorReporteExamen.Margin = new System.Windows.Forms.Padding(2);
            this.btnVisorReporteExamen.Name = "btnVisorReporteExamen";
            this.btnVisorReporteExamen.Size = new System.Drawing.Size(255, 36);
            this.btnVisorReporteExamen.TabIndex = 101;
            this.btnVisorReporteExamen.Text = "Ver Reporte de";
            this.btnVisorReporteExamen.UseVisualStyleBackColor = false;
            this.btnVisorReporteExamen.Visible = false;
            // 
            // btnReceta
            // 
            this.btnReceta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReceta.BackColor = System.Drawing.SystemColors.Control;
            this.btnReceta.Enabled = false;
            this.btnReceta.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnReceta.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnReceta.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnReceta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReceta.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReceta.ForeColor = System.Drawing.Color.Black;
            this.btnReceta.Image = ((System.Drawing.Image)(resources.GetObject("btnReceta.Image")));
            this.btnReceta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReceta.Location = new System.Drawing.Point(332, 36);
            this.btnReceta.Margin = new System.Windows.Forms.Padding(2);
            this.btnReceta.Name = "btnReceta";
            this.btnReceta.Size = new System.Drawing.Size(255, 36);
            this.btnReceta.TabIndex = 100;
            this.btnReceta.Text = "Receta";
            this.btnReceta.UseVisualStyleBackColor = false;
            // 
            // btnGuardarExamen
            // 
            this.btnGuardarExamen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGuardarExamen.BackColor = System.Drawing.SystemColors.Control;
            this.btnGuardarExamen.Enabled = false;
            this.btnGuardarExamen.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnGuardarExamen.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnGuardarExamen.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnGuardarExamen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarExamen.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarExamen.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnGuardarExamen.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarExamen.Image")));
            this.btnGuardarExamen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardarExamen.Location = new System.Drawing.Point(332, 80);
            this.btnGuardarExamen.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardarExamen.Name = "btnGuardarExamen";
            this.btnGuardarExamen.Size = new System.Drawing.Size(255, 36);
            this.btnGuardarExamen.TabIndex = 64;
            this.btnGuardarExamen.Text = "      Guardar";
            this.btnGuardarExamen.UseVisualStyleBackColor = false;
            // 
            // btnCerrarESO
            // 
            this.btnCerrarESO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCerrarESO.BackColor = System.Drawing.SystemColors.Control;
            this.btnCerrarESO.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCerrarESO.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnCerrarESO.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnCerrarESO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarESO.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarESO.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnCerrarESO.Image = global::Sigesoft.Node.WinClient.UI.Resources.bullet_cross;
            this.btnCerrarESO.Location = new System.Drawing.Point(567, 9);
            this.btnCerrarESO.Margin = new System.Windows.Forms.Padding(2);
            this.btnCerrarESO.Name = "btnCerrarESO";
            this.btnCerrarESO.Size = new System.Drawing.Size(24, 18);
            this.btnCerrarESO.TabIndex = 75;
            this.btnCerrarESO.UseVisualStyleBackColor = false;
            this.btnCerrarESO.Visible = false;
            // 
            // chkUtilizarFirma
            // 
            this.chkUtilizarFirma.AutoSize = true;
            this.chkUtilizarFirma.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUtilizarFirma.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chkUtilizarFirma.Location = new System.Drawing.Point(6, 135);
            this.chkUtilizarFirma.Name = "chkUtilizarFirma";
            this.chkUtilizarFirma.Size = new System.Drawing.Size(96, 17);
            this.chkUtilizarFirma.TabIndex = 59;
            this.chkUtilizarFirma.Text = "SI Utilizar Firma";
            this.chkUtilizarFirma.UseVisualStyleBackColor = true;
            // 
            // chkApproved
            // 
            this.chkApproved.AutoSize = true;
            this.chkApproved.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkApproved.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chkApproved.Location = new System.Drawing.Point(174, 136);
            this.chkApproved.Name = "chkApproved";
            this.chkApproved.Size = new System.Drawing.Size(137, 17);
            this.chkApproved.TabIndex = 58;
            this.chkApproved.Text = "Aprobar por Especialista";
            this.chkApproved.UseVisualStyleBackColor = true;
            // 
            // EXAMENES_lblComentarios
            // 
            this.EXAMENES_lblComentarios.AutoSize = true;
            this.EXAMENES_lblComentarios.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EXAMENES_lblComentarios.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.EXAMENES_lblComentarios.Location = new System.Drawing.Point(6, 20);
            this.EXAMENES_lblComentarios.Name = "EXAMENES_lblComentarios";
            this.EXAMENES_lblComentarios.Size = new System.Drawing.Size(96, 13);
            this.EXAMENES_lblComentarios.TabIndex = 49;
            this.EXAMENES_lblComentarios.Text = "Comentarios de {0}";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.DarkGray;
            this.label27.Location = new System.Drawing.Point(519, 14);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(43, 13);
            this.label27.TabIndex = 52;
            this.label27.Text = "CERRAR";
            this.label27.Visible = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label23.Location = new System.Drawing.Point(247, 15);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(79, 13);
            this.label23.TabIndex = 52;
            this.label23.Text = "Tipo de Exámen";
            this.label23.Visible = false;
            // 
            // cbTipoProcedenciaExamen
            // 
            this.cbTipoProcedenciaExamen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoProcedenciaExamen.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTipoProcedenciaExamen.FormattingEnabled = true;
            this.cbTipoProcedenciaExamen.Location = new System.Drawing.Point(332, 12);
            this.cbTipoProcedenciaExamen.Name = "cbTipoProcedenciaExamen";
            this.cbTipoProcedenciaExamen.Size = new System.Drawing.Size(181, 21);
            this.cbTipoProcedenciaExamen.TabIndex = 53;
            this.cbTipoProcedenciaExamen.Visible = false;
            // 
            // txtComentario
            // 
            this.txtComentario.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComentario.Location = new System.Drawing.Point(6, 36);
            this.txtComentario.Multiline = true;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComentario.Size = new System.Drawing.Size(305, 45);
            this.txtComentario.TabIndex = 50;
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.Controls.Add(this.btnPerson);
            this.panel8.Controls.Add(this.btnSalir);
            this.panel8.Location = new System.Drawing.Point(1028, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(281, 43);
            this.panel8.TabIndex = 1;
            // 
            // btnPerson
            // 
            this.btnPerson.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPerson.BackColor = System.Drawing.SystemColors.Control;
            this.btnPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPerson.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPerson.Location = new System.Drawing.Point(20, 4);
            this.btnPerson.Name = "btnPerson";
            this.btnPerson.Size = new System.Drawing.Size(255, 36);
            this.btnPerson.TabIndex = 100;
            this.btnPerson.Text = "Ver Datos Generales";
            this.btnPerson.UseVisualStyleBackColor = false;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.SystemColors.Control;
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnSalir.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSalir.Image = global::Sigesoft.Node.WinClient.UI.Resources.bullet_cross;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(71, 13);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(2);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(161, 24);
            this.btnSalir.TabIndex = 68;
            this.btnSalir.Text = "Salir de Evaluación Medica";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Visible = false;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel7.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.12983F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.87017F));
            this.tableLayoutPanel7.Controls.Add(this.panel7, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.panel8, 1, 0);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(4, 574);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1312, 49);
            this.tableLayoutPanel7.TabIndex = 51;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.Controls.Add(this.label18);
            this.panel7.Controls.Add(this.lblTrabajador);
            this.panel7.Controls.Add(this.label1);
            this.panel7.Controls.Add(this.btnViewWorker);
            this.panel7.Controls.Add(this.lblServicio);
            this.panel7.Controls.Add(this.label16);
            this.panel7.Controls.Add(this.lblTipoEso);
            this.panel7.Controls.Add(this.lblProtocolName);
            this.panel7.Controls.Add(this.label21);
            this.panel7.Controls.Add(this.lblView);
            this.panel7.Controls.Add(this.label24);
            this.panel7.Controls.Add(this.lblFechaGraba);
            this.panel7.Controls.Add(this.lblUsuAct);
            this.panel7.Controls.Add(this.lblUsuGraba);
            this.panel7.Controls.Add(this.label17);
            this.panel7.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1019, 43);
            this.panel7.TabIndex = 0;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label18.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(293, 26);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(37, 13);
            this.label18.TabIndex = 89;
            this.label18.Text = "PERFIL";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTrabajador
            // 
            this.lblTrabajador.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblTrabajador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTrabajador.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrabajador.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTrabajador.Location = new System.Drawing.Point(422, 4);
            this.lblTrabajador.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTrabajador.Name = "lblTrabajador";
            this.lblTrabajador.Size = new System.Drawing.Size(268, 15);
            this.lblTrabajador.TabIndex = 77;
            this.lblTrabajador.Text = "lblTrabajador";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label1.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(361, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 78;
            this.label1.Text = "Pciente:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnViewWorker
            // 
            this.btnViewWorker.BackColor = System.Drawing.SystemColors.Control;
            this.btnViewWorker.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnViewWorker.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnViewWorker.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnViewWorker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewWorker.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewWorker.Image = global::Sigesoft.Node.WinClient.UI.Resources.user_suit;
            this.btnViewWorker.Location = new System.Drawing.Point(295, 2);
            this.btnViewWorker.Margin = new System.Windows.Forms.Padding(2);
            this.btnViewWorker.Name = "btnViewWorker";
            this.btnViewWorker.Size = new System.Drawing.Size(31, 26);
            this.btnViewWorker.TabIndex = 79;
            this.btnViewWorker.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnViewWorker.UseVisualStyleBackColor = false;
            // 
            // lblServicio
            // 
            this.lblServicio.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblServicio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblServicio.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServicio.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblServicio.Location = new System.Drawing.Point(759, 24);
            this.lblServicio.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblServicio.Name = "lblServicio";
            this.lblServicio.Size = new System.Drawing.Size(139, 15);
            this.lblServicio.TabIndex = 86;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label16.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(350, 26);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 13);
            this.label16.TabIndex = 75;
            this.label16.Text = "Protocolo:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTipoEso
            // 
            this.lblTipoEso.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblTipoEso.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTipoEso.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoEso.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTipoEso.Location = new System.Drawing.Point(759, 4);
            this.lblTipoEso.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTipoEso.Name = "lblTipoEso";
            this.lblTipoEso.Size = new System.Drawing.Size(139, 15);
            this.lblTipoEso.TabIndex = 87;
            // 
            // lblProtocolName
            // 
            this.lblProtocolName.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblProtocolName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProtocolName.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProtocolName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblProtocolName.Location = new System.Drawing.Point(422, 25);
            this.lblProtocolName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProtocolName.Name = "lblProtocolName";
            this.lblProtocolName.Size = new System.Drawing.Size(268, 15);
            this.lblProtocolName.TabIndex = 76;
            this.lblProtocolName.Text = "lblProtocolName";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label21.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(698, 25);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(47, 13);
            this.label21.TabIndex = 85;
            this.label21.Text = "Servicio:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblView
            // 
            this.lblView.AutoSize = true;
            this.lblView.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblView.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblView.ForeColor = System.Drawing.Color.Blue;
            this.lblView.Location = new System.Drawing.Point(903, 11);
            this.lblView.Name = "lblView";
            this.lblView.Size = new System.Drawing.Size(75, 18);
            this.lblView.TabIndex = 80;
            this.lblView.Text = "Ver Menos";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label24.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(694, 4);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(48, 13);
            this.label24.TabIndex = 88;
            this.label24.Text = "Tipo Eso:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFechaGraba
            // 
            this.lblFechaGraba.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblFechaGraba.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFechaGraba.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaGraba.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFechaGraba.Location = new System.Drawing.Point(96, 25);
            this.lblFechaGraba.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFechaGraba.Name = "lblFechaGraba";
            this.lblFechaGraba.Size = new System.Drawing.Size(169, 15);
            this.lblFechaGraba.TabIndex = 84;
            this.lblFechaGraba.Text = "lblFechaGraba";
            // 
            // lblUsuAct
            // 
            this.lblUsuAct.AutoSize = true;
            this.lblUsuAct.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblUsuAct.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuAct.Location = new System.Drawing.Point(0, 4);
            this.lblUsuAct.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUsuAct.Name = "lblUsuAct";
            this.lblUsuAct.Size = new System.Drawing.Size(78, 13);
            this.lblUsuAct.TabIndex = 81;
            this.lblUsuAct.Text = "Usuario Graba:";
            this.lblUsuAct.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblUsuGraba
            // 
            this.lblUsuGraba.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblUsuGraba.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUsuGraba.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuGraba.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblUsuGraba.Location = new System.Drawing.Point(96, 4);
            this.lblUsuGraba.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUsuGraba.Name = "lblUsuGraba";
            this.lblUsuGraba.Size = new System.Drawing.Size(169, 15);
            this.lblUsuGraba.TabIndex = 83;
            this.lblUsuGraba.Text = "lblUsuGraba";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label17.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(8, 27);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(69, 13);
            this.label17.TabIndex = 82;
            this.label17.Text = "Fecha Graba:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmConsultorios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1318, 624);
            this.Controls.Add(this.tableLayoutPanel7);
            this.Controls.Add(this.gbDiagnosticoExamen);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.tcExamList);
            this.Name = "frmConsultorios";
            this.Text = "frmConsultorios";
            this.Load += new System.EventHandler(this.frmConsultorios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tcExamList)).EndInit();
            this.tcExamList.ResumeLayout(false);
            this.gbDiagnosticoExamen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDiagnosticoPorExamenComponente)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl tcExamList;
        private System.Windows.Forms.GroupBox gbDiagnosticoExamen;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdDiagnosticoPorExamenComponente;
        private System.Windows.Forms.Button btnRemoverDxExamen;
        private System.Windows.Forms.Button btnEditarDxExamen;
        private System.Windows.Forms.Button btnAgregarDxExamen;
        private System.Windows.Forms.Label lblRecordCountDiagnosticoPorExamenCom;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbEstadoComponente;
        private System.Windows.Forms.Label EXAMENES_lblEstadoComponente;
        private System.Windows.Forms.Button btnVisorReporteExamen;
        private System.Windows.Forms.Button btnReceta;
        private System.Windows.Forms.Button btnGuardarExamen;
        private System.Windows.Forms.Button btnCerrarESO;
        private System.Windows.Forms.CheckBox chkUtilizarFirma;
        private System.Windows.Forms.CheckBox chkApproved;
        private System.Windows.Forms.Label EXAMENES_lblComentarios;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cbTipoProcedenciaExamen;
        private System.Windows.Forms.TextBox txtComentario;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnPerson;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblTrabajador;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnViewWorker;
        private System.Windows.Forms.Label lblServicio;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblTipoEso;
        private System.Windows.Forms.Label lblProtocolName;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblView;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lblFechaGraba;
        private System.Windows.Forms.Label lblUsuAct;
        private System.Windows.Forms.Label lblUsuGraba;
        private System.Windows.Forms.Label label17;
    }
}