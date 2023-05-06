namespace Sigesoft.Node.WinClient.UI
{
    partial class frmGraficosCovid19
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title5 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title7 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title8 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend9 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series15 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title9 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chartPastel = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartBarras = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartSexos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartEtarioResult = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartEtareo = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartPastel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBarras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSexos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartEtarioResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartEtareo)).BeginInit();
            this.SuspendLayout();
            // 
            // chartPastel
            // 
            chartArea5.Name = "ChartArea1";
            this.chartPastel.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chartPastel.Legends.Add(legend5);
            this.chartPastel.Location = new System.Drawing.Point(348, 434);
            this.chartPastel.Name = "chartPastel";
            this.chartPastel.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series8.IsValueShownAsLabel = true;
            series8.LabelForeColor = System.Drawing.Color.White;
            series8.Legend = "Legend1";
            series8.Name = "Series1";
            this.chartPastel.Series.Add(series8);
            this.chartPastel.Size = new System.Drawing.Size(335, 281);
            this.chartPastel.TabIndex = 1;
            this.chartPastel.Text = "chart2";
            title5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title5.ForeColor = System.Drawing.Color.MidnightBlue;
            title5.Name = "Title1";
            title5.Text = "AAA";
            this.chartPastel.Titles.Add(title5);
            // 
            // chartBarras
            // 
            chartArea6.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea6.AxisX.IsLabelAutoFit = false;
            chartArea6.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea6.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea6.AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea6.Name = "ChartArea1";
            this.chartBarras.ChartAreas.Add(chartArea6);
            legend6.BackColor = System.Drawing.Color.White;
            legend6.Enabled = false;
            legend6.Name = "Legend1";
            this.chartBarras.Legends.Add(legend6);
            this.chartBarras.Location = new System.Drawing.Point(12, 12);
            this.chartBarras.Name = "chartBarras";
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series9.IsValueShownAsLabel = true;
            series9.IsXValueIndexed = true;
            series9.LabelForeColor = System.Drawing.SystemColors.MenuHighlight;
            series9.Legend = "Legend1";
            series9.Name = "Series1";
            series9.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            this.chartBarras.Series.Add(series9);
            this.chartBarras.Size = new System.Drawing.Size(671, 416);
            this.chartBarras.TabIndex = 0;
            this.chartBarras.Text = "chart1";
            title6.BackColor = System.Drawing.Color.Transparent;
            title6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title6.ForeColor = System.Drawing.Color.MidnightBlue;
            title6.Name = "Title1";
            title6.Text = "AAA";
            this.chartBarras.Titles.Add(title6);
            // 
            // chartSexos
            // 
            chartArea7.BackColor = System.Drawing.Color.Transparent;
            chartArea7.Name = "ChartArea1";
            chartArea7.ShadowColor = System.Drawing.Color.Transparent;
            this.chartSexos.ChartAreas.Add(chartArea7);
            legend7.Name = "Legend1";
            this.chartSexos.Legends.Add(legend7);
            this.chartSexos.Location = new System.Drawing.Point(12, 434);
            this.chartSexos.Name = "chartSexos";
            this.chartSexos.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series10.ChartArea = "ChartArea1";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series10.IsValueShownAsLabel = true;
            series10.LabelForeColor = System.Drawing.Color.White;
            series10.Legend = "Legend1";
            series10.MarkerBorderColor = System.Drawing.Color.Transparent;
            series10.Name = "Series1";
            series10.YValuesPerPoint = 2;
            this.chartSexos.Series.Add(series10);
            this.chartSexos.Size = new System.Drawing.Size(330, 281);
            this.chartSexos.TabIndex = 2;
            this.chartSexos.Text = "chart1";
            title7.BackColor = System.Drawing.Color.Transparent;
            title7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title7.ForeColor = System.Drawing.Color.DarkBlue;
            title7.Name = "Title1";
            title7.Text = "PACIENTES POR SEXO";
            this.chartSexos.Titles.Add(title7);
            // 
            // chartEtarioResult
            // 
            chartArea8.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea8.AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea8.Name = "ChartArea1";
            this.chartEtarioResult.ChartAreas.Add(chartArea8);
            legend8.Alignment = System.Drawing.StringAlignment.Center;
            legend8.DockedToChartArea = "ChartArea1";
            legend8.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend8.IsDockedInsideChartArea = false;
            legend8.Name = "Legend1";
            this.chartEtarioResult.Legends.Add(legend8);
            this.chartEtarioResult.Location = new System.Drawing.Point(689, 282);
            this.chartEtarioResult.Name = "chartEtarioResult";
            this.chartEtarioResult.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series11.ChartArea = "ChartArea1";
            series11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series11.IsValueShownAsLabel = true;
            series11.Legend = "Legend1";
            series11.Name = "NEGATIVO";
            series12.ChartArea = "ChartArea1";
            series12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series12.IsValueShownAsLabel = true;
            series12.Legend = "Legend1";
            series12.Name = "IgM";
            series13.ChartArea = "ChartArea1";
            series13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series13.IsValueShownAsLabel = true;
            series13.Legend = "Legend1";
            series13.Name = "IgG e IgM";
            series14.ChartArea = "ChartArea1";
            series14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series14.IsValueShownAsLabel = true;
            series14.Legend = "Legend1";
            series14.Name = "IgG";
            this.chartEtarioResult.Series.Add(series11);
            this.chartEtarioResult.Series.Add(series12);
            this.chartEtarioResult.Series.Add(series13);
            this.chartEtarioResult.Series.Add(series14);
            this.chartEtarioResult.Size = new System.Drawing.Size(561, 433);
            this.chartEtarioResult.TabIndex = 4;
            this.chartEtarioResult.Text = "chart1";
            title8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title8.ForeColor = System.Drawing.Color.DarkBlue;
            title8.Name = "Title1";
            title8.Text = "RESULTADOS DE PACIENTES ATENDIDOS POR GRUPO ETARIO";
            this.chartEtarioResult.Titles.Add(title8);
            // 
            // chartEtareo
            // 
            chartArea9.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea9.AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea9.Name = "ChartArea1";
            this.chartEtareo.ChartAreas.Add(chartArea9);
            legend9.Enabled = false;
            legend9.Name = "Legend1";
            this.chartEtareo.Legends.Add(legend9);
            this.chartEtareo.Location = new System.Drawing.Point(689, 12);
            this.chartEtareo.Name = "chartEtareo";
            this.chartEtareo.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series15.ChartArea = "ChartArea1";
            series15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series15.IsValueShownAsLabel = true;
            series15.IsVisibleInLegend = false;
            series15.Legend = "Legend1";
            series15.Name = "Series1";
            this.chartEtareo.Series.Add(series15);
            this.chartEtareo.Size = new System.Drawing.Size(561, 264);
            this.chartEtareo.TabIndex = 3;
            this.chartEtareo.Text = "chart1";
            title9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title9.ForeColor = System.Drawing.Color.DarkBlue;
            title9.Name = "Title1";
            title9.Text = "PACIENTES ATENDIDOS POR GRUPO ETARIO";
            this.chartEtareo.Titles.Add(title9);
            // 
            // frmGraficosCovid19
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 727);
            this.Controls.Add(this.chartEtarioResult);
            this.Controls.Add(this.chartEtareo);
            this.Controls.Add(this.chartSexos);
            this.Controls.Add(this.chartPastel);
            this.Controls.Add(this.chartBarras);
            this.Name = "frmGraficosCovid19";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GRAFICOS COVID-19";
            this.Load += new System.EventHandler(this.frmGraficosCovid19_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartPastel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBarras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSexos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartEtarioResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartEtareo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartPastel;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBarras;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSexos;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEtarioResult;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEtareo;
    }
}