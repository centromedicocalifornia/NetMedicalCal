namespace Sigesoft.Node.WinClient.UI
{
    partial class frmAct_Fact
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
            this.txt_v_LiquidacionId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_v_NroFactura = new System.Windows.Forms.TextBox();
            this.btnAct = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nro Liquidacion";
            // 
            // txt_v_LiquidacionId
            // 
            this.txt_v_LiquidacionId.Location = new System.Drawing.Point(113, 12);
            this.txt_v_LiquidacionId.Name = "txt_v_LiquidacionId";
            this.txt_v_LiquidacionId.Size = new System.Drawing.Size(156, 20);
            this.txt_v_LiquidacionId.TabIndex = 1;
            this.txt_v_LiquidacionId.Text = "N009-LQ00";
            this.txt_v_LiquidacionId.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Nro Factura";
            // 
            // txt_v_NroFactura
            // 
            this.txt_v_NroFactura.Location = new System.Drawing.Point(113, 45);
            this.txt_v_NroFactura.Name = "txt_v_NroFactura";
            this.txt_v_NroFactura.Size = new System.Drawing.Size(156, 20);
            this.txt_v_NroFactura.TabIndex = 1;
            // 
            // btnAct
            // 
            this.btnAct.Location = new System.Drawing.Point(113, 82);
            this.btnAct.Name = "btnAct";
            this.btnAct.Size = new System.Drawing.Size(75, 23);
            this.btnAct.TabIndex = 2;
            this.btnAct.Text = "Actualizar";
            this.btnAct.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAct.UseVisualStyleBackColor = true;
            this.btnAct.Click += new System.EventHandler(this.btnAct_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(194, 82);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Salir";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmAct_Fact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 117);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAct);
            this.Controls.Add(this.txt_v_NroFactura);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_v_LiquidacionId);
            this.Controls.Add(this.label1);
            this.Name = "frmAct_Fact";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actualizar Registro";
            this.Load += new System.EventHandler(this.frmAct_Fact_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_v_LiquidacionId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_v_NroFactura;
        private System.Windows.Forms.Button btnAct;
        private System.Windows.Forms.Button btnClose;
    }
}