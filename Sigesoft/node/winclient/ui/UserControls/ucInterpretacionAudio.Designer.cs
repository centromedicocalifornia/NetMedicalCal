namespace Sigesoft.Node.WinClient.UI.UserControls
{
    partial class ucInterpretacionAudio
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddClinica = new System.Windows.Forms.Button();
            this.txtClinica = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtKlockhoff = new System.Windows.Forms.TextBox();
            this.btnKlockhoff = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtClinica);
            this.groupBox1.Controls.Add(this.btnAddClinica);
            this.groupBox1.Location = new System.Drawing.Point(10, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(521, 71);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "9. - Interpretación Clínica:";
            // 
            // btnAddClinica
            // 
            this.btnAddClinica.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.add;
            this.btnAddClinica.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddClinica.Location = new System.Drawing.Point(64, 30);
            this.btnAddClinica.Name = "btnAddClinica";
            this.btnAddClinica.Size = new System.Drawing.Size(75, 23);
            this.btnAddClinica.TabIndex = 0;
            this.btnAddClinica.Text = "Agregar";
            this.btnAddClinica.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddClinica.UseVisualStyleBackColor = true;
            this.btnAddClinica.Click += new System.EventHandler(this.btnAddClinica_Click);
            // 
            // txtClinica
            // 
            this.txtClinica.Location = new System.Drawing.Point(192, 17);
            this.txtClinica.Multiline = true;
            this.txtClinica.Name = "txtClinica";
            this.txtClinica.Size = new System.Drawing.Size(297, 45);
            this.txtClinica.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnKlockhoff);
            this.groupBox2.Controls.Add(this.txtKlockhoff);
            this.groupBox2.Location = new System.Drawing.Point(543, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(521, 71);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "10. - Interpretación clasificación Klockhoff:";
            // 
            // txtKlockhoff
            // 
            this.txtKlockhoff.Location = new System.Drawing.Point(192, 17);
            this.txtKlockhoff.Multiline = true;
            this.txtKlockhoff.Name = "txtKlockhoff";
            this.txtKlockhoff.Size = new System.Drawing.Size(297, 45);
            this.txtKlockhoff.TabIndex = 1;
            // 
            // btnKlockhoff
            // 
            this.btnKlockhoff.Image = global::Sigesoft.Node.WinClient.UI.Properties.Resources.add;
            this.btnKlockhoff.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKlockhoff.Location = new System.Drawing.Point(70, 30);
            this.btnKlockhoff.Name = "btnKlockhoff";
            this.btnKlockhoff.Size = new System.Drawing.Size(75, 23);
            this.btnKlockhoff.TabIndex = 2;
            this.btnKlockhoff.Text = "Agregar";
            this.btnKlockhoff.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnKlockhoff.UseVisualStyleBackColor = true;
            this.btnKlockhoff.Click += new System.EventHandler(this.btnKlockhoff_Click);
            // 
            // ucInterpretacionAudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ucInterpretacionAudio";
            this.Size = new System.Drawing.Size(1074, 86);
            this.Load += new System.EventHandler(this.ucInterpretacionAudio_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddClinica;
        private System.Windows.Forms.TextBox txtClinica;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtKlockhoff;
        private System.Windows.Forms.Button btnKlockhoff;
    }
}
