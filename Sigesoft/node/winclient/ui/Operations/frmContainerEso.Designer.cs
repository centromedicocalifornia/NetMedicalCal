namespace Sigesoft.Node.WinClient.UI.Operations
{
    partial class frmContainerEso
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
            this.tcContEso = new System.Windows.Forms.TabControl();
            this.ddlExamenesAnterioes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAptitud = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tcContEso
            // 
            this.tcContEso.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcContEso.Location = new System.Drawing.Point(9, 36);
            this.tcContEso.Name = "tcContEso";
            this.tcContEso.SelectedIndex = 0;
            this.tcContEso.Size = new System.Drawing.Size(1325, 650);
            this.tcContEso.TabIndex = 2;
            // 
            // ddlExamenesAnterioes
            // 
            this.ddlExamenesAnterioes.FormattingEnabled = true;
            this.ddlExamenesAnterioes.Location = new System.Drawing.Point(131, 10);
            this.ddlExamenesAnterioes.Name = "ddlExamenesAnterioes";
            this.ddlExamenesAnterioes.Size = new System.Drawing.Size(786, 21);
            this.ddlExamenesAnterioes.TabIndex = 3;
            this.ddlExamenesAnterioes.SelectedIndexChanged += new System.EventHandler(this.ddlExamenesAnterioes_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Exámenes anterioes";
            // 
            // lblAptitud
            // 
            this.lblAptitud.AutoSize = true;
            this.lblAptitud.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAptitud.ForeColor = System.Drawing.Color.Blue;
            this.lblAptitud.Location = new System.Drawing.Point(399, 11);
            this.lblAptitud.Name = "lblAptitud";
            this.lblAptitud.Size = new System.Drawing.Size(0, 19);
            this.lblAptitud.TabIndex = 5;
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.BackColor = System.Drawing.SystemColors.Control;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.Black;
            this.button5.Image = global::Sigesoft.Node.WinClient.UI.Resources.page_white_acrobat;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(1145, 7);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(189, 25);
            this.button5.TabIndex = 169;
            this.button5.Text = "HISTORIAL DE ATENCIONES";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // frmContainerEso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1346, 705);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.lblAptitud);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ddlExamenesAnterioes);
            this.Controls.Add(this.tcContEso);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmContainerEso";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Exámenes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmContainerEso_FormClosing);
            this.Load += new System.EventHandler(this.frmContainerEso_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcContEso;
        private System.Windows.Forms.ComboBox ddlExamenesAnterioes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAptitud;
        private System.Windows.Forms.Button button5;
    }
}