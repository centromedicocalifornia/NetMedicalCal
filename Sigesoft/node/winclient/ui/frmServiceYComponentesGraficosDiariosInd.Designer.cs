namespace Sigesoft.Node.WinClient.UI
{
    partial class frmServiceYComponentesGraficosDiariosInd
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chartGrafxDia = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnDescargarCurvas = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.chartGrafxDia)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartGrafxDia
            // 
            chartArea1.AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal;
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.MaximumAutoSize = 60F;
            chartArea1.AxisX.ScaleBreakStyle.CollapsibleSpaceThreshold = 15;
            chartArea1.AxisX.ScaleBreakStyle.Spacing = 1D;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.chartGrafxDia.ChartAreas.Add(chartArea1);
            this.chartGrafxDia.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.DockedToChartArea = "ChartArea1";
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Enabled = false;
            legend1.IsDockedInsideChartArea = false;
            legend1.Name = "Legend1";
            this.chartGrafxDia.Legends.Add(legend1);
            this.chartGrafxDia.Location = new System.Drawing.Point(3, 3);
            this.chartGrafxDia.Name = "chartGrafxDia";
            this.chartGrafxDia.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.IsValueShownAsLabel = true;
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "POSITIVOS";
            series1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            this.chartGrafxDia.Series.Add(series1);
            this.chartGrafxDia.Size = new System.Drawing.Size(1226, 1480);
            this.chartGrafxDia.TabIndex = 14;
            this.chartGrafxDia.Text = "chart1";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.ForeColor = System.Drawing.Color.DarkBlue;
            title1.Name = "Title1";
            title1.Text = "-";
            this.chartGrafxDia.Titles.Add(title1);
            this.chartGrafxDia.Click += new System.EventHandler(this.chartGrafxDia_Click);
            // 
            // btnDescargarCurvas
            // 
            this.btnDescargarCurvas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDescargarCurvas.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDescargarCurvas.Location = new System.Drawing.Point(1154, 1489);
            this.btnDescargarCurvas.Name = "btnDescargarCurvas";
            this.btnDescargarCurvas.Size = new System.Drawing.Size(75, 38);
            this.btnDescargarCurvas.TabIndex = 15;
            this.btnDescargarCurvas.Text = "Descargar";
            this.btnDescargarCurvas.UseVisualStyleBackColor = true;
            this.btnDescargarCurvas.Click += new System.EventHandler(this.btnDescargarCurvas_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.chartGrafxDia, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDescargarCurvas, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 97.15302F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.846975F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1232, 1530);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // frmServiceYComponentesGraficosDiariosInd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1387, 749);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmServiceYComponentesGraficosDiariosInd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmServiceYComponentesGraficosDiariosInd";
            this.Load += new System.EventHandler(this.frmServiceYComponentesGraficosDiariosInd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartGrafxDia)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartGrafxDia;
        private System.Windows.Forms.Button btnDescargarCurvas;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}