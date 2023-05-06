namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    partial class frmTipoPacHospSala
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
            this.label1 = new System.Windows.Forms.Label();
            this.rbclinica = new System.Windows.Forms.RadioButton();
            this.rbclinicaAbierta = new System.Windows.Forms.RadioButton();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.checkSop = new System.Windows.Forms.CheckBox();
            this.checkHosp = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "TIPO DE PACIENTE";
            // 
            // rbclinica
            // 
            this.rbclinica.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbclinica.AutoSize = true;
            this.rbclinica.Location = new System.Drawing.Point(199, 29);
            this.rbclinica.Name = "rbclinica";
            this.rbclinica.Size = new System.Drawing.Size(66, 17);
            this.rbclinica.TabIndex = 8;
            this.rbclinica.TabStop = true;
            this.rbclinica.Text = "CLÍNICA";
            this.rbclinica.UseVisualStyleBackColor = true;
            // 
            // rbclinicaAbierta
            // 
            this.rbclinicaAbierta.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbclinicaAbierta.AutoSize = true;
            this.rbclinicaAbierta.Location = new System.Drawing.Point(47, 29);
            this.rbclinicaAbierta.Name = "rbclinicaAbierta";
            this.rbclinicaAbierta.Size = new System.Drawing.Size(115, 17);
            this.rbclinicaAbierta.TabIndex = 7;
            this.rbclinicaAbierta.TabStop = true;
            this.rbclinicaAbierta.Text = "CLINICA ABIERTA";
            this.rbclinicaAbierta.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(104, 117);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(58, 23);
            this.btnCancelar.TabIndex = 14;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.Location = new System.Drawing.Point(184, 117);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(62, 23);
            this.btnAceptar.TabIndex = 13;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "SITUACIÓN DE PACIENTE";
            // 
            // checkSop
            // 
            this.checkSop.AutoSize = true;
            this.checkSop.Location = new System.Drawing.Point(47, 83);
            this.checkSop.Name = "checkSop";
            this.checkSop.Size = new System.Drawing.Size(98, 17);
            this.checkSop.TabIndex = 16;
            this.checkSop.Text = "Pasó por SALA";
            this.checkSop.UseVisualStyleBackColor = true;
            // 
            // checkHosp
            // 
            this.checkHosp.AutoSize = true;
            this.checkHosp.Location = new System.Drawing.Point(196, 83);
            this.checkHosp.Name = "checkHosp";
            this.checkHosp.Size = new System.Drawing.Size(142, 17);
            this.checkHosp.TabIndex = 17;
            this.checkHosp.Text = "Pasó por Hospitalización";
            this.checkHosp.UseVisualStyleBackColor = true;
            // 
            // frmTipoPacHospSala
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 152);
            this.Controls.Add(this.checkHosp);
            this.Controls.Add(this.checkSop);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbclinica);
            this.Controls.Add(this.rbclinicaAbierta);
            this.Name = "frmTipoPacHospSala";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmTipoPacHospSala";
            this.Load += new System.EventHandler(this.frmTipoPacHospSala_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbclinica;
        private System.Windows.Forms.RadioButton rbclinicaAbierta;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkSop;
        private System.Windows.Forms.CheckBox checkHosp;
    }
}