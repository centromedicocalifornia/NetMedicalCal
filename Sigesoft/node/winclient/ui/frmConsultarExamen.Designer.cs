namespace Sigesoft.Node.WinClient.UI
{
    partial class frmConsultarExamen
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_MedicalExamId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Name", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_CategoryName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentTypeName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DiagnosableName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_CreationUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_CreationDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_UpdateUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_UpdateDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_UIIndex");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("r_BasePrice");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_Padre");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbMtc = new System.Windows.Forms.RadioButton();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.rbOcupacional = new System.Windows.Forms.RadioButton();
            this.rbAsistencial = new System.Windows.Forms.RadioButton();
            this.btnExportExamenes = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtExamen = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ddlCategoryId = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdDataMedicalExam = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataMedicalExam)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox1.Controls.Add(this.rbMtc);
            this.groupBox1.Controls.Add(this.rbTodos);
            this.groupBox1.Controls.Add(this.rbOcupacional);
            this.groupBox1.Controls.Add(this.rbAsistencial);
            this.groupBox1.Controls.Add(this.btnExportExamenes);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.txtExamen);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ddlCategoryId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(959, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Busqueda / Filtro";
            // 
            // rbMtc
            // 
            this.rbMtc.AutoSize = true;
            this.rbMtc.Location = new System.Drawing.Point(224, 21);
            this.rbMtc.Name = "rbMtc";
            this.rbMtc.Size = new System.Drawing.Size(51, 17);
            this.rbMtc.TabIndex = 55;
            this.rbMtc.Text = "MTC";
            this.rbMtc.UseVisualStyleBackColor = true;
            this.rbMtc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rbMtc_KeyPress);
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Location = new System.Drawing.Point(315, 21);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(60, 17);
            this.rbTodos.TabIndex = 54;
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseVisualStyleBackColor = true;
            // 
            // rbOcupacional
            // 
            this.rbOcupacional.AutoSize = true;
            this.rbOcupacional.Location = new System.Drawing.Point(112, 21);
            this.rbOcupacional.Name = "rbOcupacional";
            this.rbOcupacional.Size = new System.Drawing.Size(96, 17);
            this.rbOcupacional.TabIndex = 53;
            this.rbOcupacional.Text = "Ocupacional";
            this.rbOcupacional.UseVisualStyleBackColor = true;
            this.rbOcupacional.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rbOcupacional_KeyPress);
            this.rbOcupacional.KeyUp += new System.Windows.Forms.KeyEventHandler(this.rbOcupacional_KeyUp);
            // 
            // rbAsistencial
            // 
            this.rbAsistencial.AutoSize = true;
            this.rbAsistencial.Checked = true;
            this.rbAsistencial.Location = new System.Drawing.Point(9, 21);
            this.rbAsistencial.Name = "rbAsistencial";
            this.rbAsistencial.Size = new System.Drawing.Size(86, 17);
            this.rbAsistencial.TabIndex = 52;
            this.rbAsistencial.TabStop = true;
            this.rbAsistencial.Text = "Asistencial";
            this.rbAsistencial.UseVisualStyleBackColor = true;
            this.rbAsistencial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rbAsistencial_KeyPress);
            // 
            // btnExportExamenes
            // 
            this.btnExportExamenes.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnExportExamenes.Image = global::Sigesoft.Node.WinClient.UI.Resources.page_excel;
            this.btnExportExamenes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExamenes.Location = new System.Drawing.Point(927, 16);
            this.btnExportExamenes.Name = "btnExportExamenes";
            this.btnExportExamenes.Size = new System.Drawing.Size(26, 61);
            this.btnExportExamenes.TabIndex = 51;
            this.btnExportExamenes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportExamenes.UseVisualStyleBackColor = true;
            this.btnExportExamenes.Click += new System.EventHandler(this.btnExportExamenes_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.Black;
            this.btnBuscar.Image = global::Sigesoft.Node.WinClient.UI.Resources.find;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(846, 16);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 61);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Filtrar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtExamen
            // 
            this.txtExamen.Location = new System.Drawing.Point(462, 18);
            this.txtExamen.Name = "txtExamen";
            this.txtExamen.Size = new System.Drawing.Size(345, 20);
            this.txtExamen.TabIndex = 3;
            this.txtExamen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtExamen_KeyPress);
            this.txtExamen.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtExamen_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(412, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nombre";
            // 
            // ddlCategoryId
            // 
            this.ddlCategoryId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlCategoryId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlCategoryId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCategoryId.FormattingEnabled = true;
            this.ddlCategoryId.Location = new System.Drawing.Point(77, 61);
            this.ddlCategoryId.Name = "ddlCategoryId";
            this.ddlCategoryId.Size = new System.Drawing.Size(269, 21);
            this.ddlCategoryId.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Categoría";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox2.Controls.Add(this.grdDataMedicalExam);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Blue;
            this.groupBox2.Location = new System.Drawing.Point(12, 106);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(959, 501);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resultados de Examenes";
            // 
            // grdDataMedicalExam
            // 
            this.grdDataMedicalExam.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdDataMedicalExam.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.Caption = "Id Examen";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 121;
            ultraGridColumn2.Header.Caption = "Examen";
            ultraGridColumn2.Header.VisiblePosition = 2;
            ultraGridColumn2.Width = 458;
            ultraGridColumn4.Header.Caption = "Categoría";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 231;
            ultraGridColumn3.Header.Caption = "Tipo Componente";
            ultraGridColumn3.Header.VisiblePosition = 6;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn6.Header.Caption = "Diagnosticable";
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Hidden = true;
            ultraGridColumn5.Header.Caption = "Usuario Crea.";
            ultraGridColumn5.Header.VisiblePosition = 7;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn5.Width = 125;
            ultraGridColumn10.Format = "dd/MM/yyyy hh:mm tt";
            ultraGridColumn10.Header.Caption = "Fecha Crea.";
            ultraGridColumn10.Header.VisiblePosition = 8;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn10.Width = 150;
            ultraGridColumn12.Header.Caption = "Usuario Act.";
            ultraGridColumn12.Header.VisiblePosition = 9;
            ultraGridColumn12.Hidden = true;
            ultraGridColumn12.Width = 125;
            ultraGridColumn13.Format = "dd/MM/yyyy hh:mm tt";
            ultraGridColumn13.Header.Caption = "Fecha Act.";
            ultraGridColumn13.Header.VisiblePosition = 10;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn13.Width = 150;
            ultraGridColumn11.Header.Caption = "Orden";
            ultraGridColumn11.Header.VisiblePosition = 1;
            ultraGridColumn11.Hidden = true;
            ultraGridColumn7.Header.Caption = "Precio";
            ultraGridColumn7.Header.VisiblePosition = 4;
            ultraGridColumn7.Width = 138;
            ultraGridColumn8.Header.VisiblePosition = 11;
            ultraGridColumn8.Hidden = true;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn4,
            ultraGridColumn3,
            ultraGridColumn6,
            ultraGridColumn5,
            ultraGridColumn10,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn11,
            ultraGridColumn7,
            ultraGridColumn8});
            this.grdDataMedicalExam.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdDataMedicalExam.DisplayLayout.InterBandSpacing = 10;
            this.grdDataMedicalExam.DisplayLayout.MaxColScrollRegions = 1;
            this.grdDataMedicalExam.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdDataMedicalExam.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdDataMedicalExam.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDataMedicalExam.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataMedicalExam.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdDataMedicalExam.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdDataMedicalExam.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataMedicalExam.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdDataMedicalExam.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.Color.White;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdDataMedicalExam.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdDataMedicalExam.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.BorderColor = System.Drawing.Color.DarkGray;
            appearance4.ForeColor = System.Drawing.Color.Black;
            appearance4.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdDataMedicalExam.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdDataMedicalExam.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForeColor = System.Drawing.Color.Black;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdDataMedicalExam.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdDataMedicalExam.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdDataMedicalExam.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.BorderColor3DBase = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdDataMedicalExam.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdDataMedicalExam.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdDataMedicalExam.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdDataMedicalExam.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdDataMedicalExam.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDataMedicalExam.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDataMedicalExam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDataMedicalExam.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDataMedicalExam.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdDataMedicalExam.Location = new System.Drawing.Point(3, 16);
            this.grdDataMedicalExam.Margin = new System.Windows.Forms.Padding(2);
            this.grdDataMedicalExam.Name = "grdDataMedicalExam";
            this.grdDataMedicalExam.Size = new System.Drawing.Size(953, 482);
            this.grdDataMedicalExam.TabIndex = 44;
            // 
            // frmConsultarExamen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(983, 619);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "frmConsultarExamen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CONSULTAR PRECIO DE EXAMENES";
            this.Load += new System.EventHandler(this.frmConsultarExamen_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmConsultarExamen_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDataMedicalExam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtExamen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddlCategoryId;
        private System.Windows.Forms.GroupBox groupBox2;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdDataMedicalExam;
        private System.Windows.Forms.Button btnExportExamenes;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        private System.Windows.Forms.RadioButton rbTodos;
        private System.Windows.Forms.RadioButton rbOcupacional;
        private System.Windows.Forms.RadioButton rbAsistencial;
        private System.Windows.Forms.RadioButton rbMtc;
    }
}