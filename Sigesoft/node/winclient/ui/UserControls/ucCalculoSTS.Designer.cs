namespace Sigesoft.Node.WinClient.UI.UserControls
{
    partial class ucCalculoSTS
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOidoDerecho = new System.Windows.Forms.TextBox();
            this.txtOidoIzquierdo = new System.Windows.Forms.TextBox();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_AuB_OD_2000 = new System.Windows.Forms.Label();
            this.txt_AuB_OD_3000 = new System.Windows.Forms.Label();
            this.txt_AuB_OD_4000 = new System.Windows.Forms.Label();
            this.txt_AuB_OD_PROM = new System.Windows.Forms.Label();
            this.txt_AuB_OI_PROM = new System.Windows.Forms.Label();
            this.txt_AuB_OI_4000 = new System.Windows.Forms.Label();
            this.txt_AuB_OI_3000 = new System.Windows.Forms.Label();
            this.txt_AuB_OI_2000 = new System.Windows.Forms.Label();
            this.txt_AuA_OI_PROM = new System.Windows.Forms.Label();
            this.txt_AuA_OI_4000 = new System.Windows.Forms.Label();
            this.txt_AuA_OI_3000 = new System.Windows.Forms.Label();
            this.txt_AuA_OI_2000 = new System.Windows.Forms.Label();
            this.txt_AuA_OD_PROM = new System.Windows.Forms.Label();
            this.txt_AuA_OD_4000 = new System.Windows.Forms.Label();
            this.txt_AuA_OD_3000 = new System.Windows.Forms.Label();
            this.txt_AuA_OD_2000 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label16 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(33, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "CALCULO DE STS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "OIDO DERECHO";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(33, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "OIDO IZQUIERDO";
            // 
            // txtOidoDerecho
            // 
            this.txtOidoDerecho.Location = new System.Drawing.Point(271, 196);
            this.txtOidoDerecho.Name = "txtOidoDerecho";
            this.txtOidoDerecho.Size = new System.Drawing.Size(121, 20);
            this.txtOidoDerecho.TabIndex = 3;
            this.txtOidoDerecho.Text = "0";
            this.txtOidoDerecho.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOidoDerecho.TextChanged += new System.EventHandler(this.txtOidoDerecho_TextChanged);
            // 
            // txtOidoIzquierdo
            // 
            this.txtOidoIzquierdo.Location = new System.Drawing.Point(557, 196);
            this.txtOidoIzquierdo.Name = "txtOidoIzquierdo";
            this.txtOidoIzquierdo.Size = new System.Drawing.Size(120, 20);
            this.txtOidoIzquierdo.TabIndex = 4;
            this.txtOidoIzquierdo.Text = "0";
            this.txtOidoIzquierdo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnCalcular
            // 
            this.btnCalcular.Location = new System.Drawing.Point(731, 196);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(75, 23);
            this.btnCalcular.TabIndex = 5;
            this.btnCalcular.Text = "CALCULAR STS";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Visible = false;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(181, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(660, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "** Para calcular el STS, primero debe estar guardado la AUDIOMETRIA BASAL y luego" +
    " la AUDIOMETRIA ACTUAL.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(33, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(167, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "AUDIOMETRÍA BASAL";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(175, 199);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "OIDO DERECHO";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(191, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "2000";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(268, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "3000";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(339, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "4000";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(412, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "PROMEDIO";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(929, 240);
            this.shapeContainer1.TabIndex = 13;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 526;
            this.lineShape1.X2 = 526;
            this.lineShape1.Y1 = 43;
            this.lineShape1.Y2 = 125;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Blue;
            this.label11.Location = new System.Drawing.Point(523, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(178, 17);
            this.label11.TabIndex = 14;
            this.label11.Text = "AUDIOMETRÍA ACTUAL";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(801, 39);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "PROMEDIO";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(728, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 13);
            this.label13.TabIndex = 17;
            this.label13.Text = "4000";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(657, 39);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 13);
            this.label14.TabIndex = 16;
            this.label14.Text = "3000";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(580, 39);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 13);
            this.label15.TabIndex = 15;
            this.label15.Text = "2000";
            // 
            // txt_AuB_OD_2000
            // 
            this.txt_AuB_OD_2000.AutoSize = true;
            this.txt_AuB_OD_2000.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AuB_OD_2000.Location = new System.Drawing.Point(195, 64);
            this.txt_AuB_OD_2000.Name = "txt_AuB_OD_2000";
            this.txt_AuB_OD_2000.Size = new System.Drawing.Size(21, 13);
            this.txt_AuB_OD_2000.TabIndex = 19;
            this.txt_AuB_OD_2000.Text = "__";
            // 
            // txt_AuB_OD_3000
            // 
            this.txt_AuB_OD_3000.AutoSize = true;
            this.txt_AuB_OD_3000.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AuB_OD_3000.Location = new System.Drawing.Point(270, 64);
            this.txt_AuB_OD_3000.Name = "txt_AuB_OD_3000";
            this.txt_AuB_OD_3000.Size = new System.Drawing.Size(21, 13);
            this.txt_AuB_OD_3000.TabIndex = 20;
            this.txt_AuB_OD_3000.Text = "__";
            // 
            // txt_AuB_OD_4000
            // 
            this.txt_AuB_OD_4000.AutoSize = true;
            this.txt_AuB_OD_4000.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AuB_OD_4000.Location = new System.Drawing.Point(347, 64);
            this.txt_AuB_OD_4000.Name = "txt_AuB_OD_4000";
            this.txt_AuB_OD_4000.Size = new System.Drawing.Size(21, 13);
            this.txt_AuB_OD_4000.TabIndex = 21;
            this.txt_AuB_OD_4000.Text = "__";
            // 
            // txt_AuB_OD_PROM
            // 
            this.txt_AuB_OD_PROM.AutoSize = true;
            this.txt_AuB_OD_PROM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AuB_OD_PROM.Location = new System.Drawing.Point(433, 64);
            this.txt_AuB_OD_PROM.Name = "txt_AuB_OD_PROM";
            this.txt_AuB_OD_PROM.Size = new System.Drawing.Size(21, 13);
            this.txt_AuB_OD_PROM.TabIndex = 22;
            this.txt_AuB_OD_PROM.Text = "__";
            // 
            // txt_AuB_OI_PROM
            // 
            this.txt_AuB_OI_PROM.AutoSize = true;
            this.txt_AuB_OI_PROM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AuB_OI_PROM.Location = new System.Drawing.Point(433, 99);
            this.txt_AuB_OI_PROM.Name = "txt_AuB_OI_PROM";
            this.txt_AuB_OI_PROM.Size = new System.Drawing.Size(21, 13);
            this.txt_AuB_OI_PROM.TabIndex = 26;
            this.txt_AuB_OI_PROM.Text = "__";
            // 
            // txt_AuB_OI_4000
            // 
            this.txt_AuB_OI_4000.AutoSize = true;
            this.txt_AuB_OI_4000.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AuB_OI_4000.Location = new System.Drawing.Point(347, 99);
            this.txt_AuB_OI_4000.Name = "txt_AuB_OI_4000";
            this.txt_AuB_OI_4000.Size = new System.Drawing.Size(21, 13);
            this.txt_AuB_OI_4000.TabIndex = 25;
            this.txt_AuB_OI_4000.Text = "__";
            // 
            // txt_AuB_OI_3000
            // 
            this.txt_AuB_OI_3000.AutoSize = true;
            this.txt_AuB_OI_3000.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AuB_OI_3000.Location = new System.Drawing.Point(270, 99);
            this.txt_AuB_OI_3000.Name = "txt_AuB_OI_3000";
            this.txt_AuB_OI_3000.Size = new System.Drawing.Size(21, 13);
            this.txt_AuB_OI_3000.TabIndex = 24;
            this.txt_AuB_OI_3000.Text = "__";
            // 
            // txt_AuB_OI_2000
            // 
            this.txt_AuB_OI_2000.AutoSize = true;
            this.txt_AuB_OI_2000.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AuB_OI_2000.Location = new System.Drawing.Point(195, 99);
            this.txt_AuB_OI_2000.Name = "txt_AuB_OI_2000";
            this.txt_AuB_OI_2000.Size = new System.Drawing.Size(21, 13);
            this.txt_AuB_OI_2000.TabIndex = 23;
            this.txt_AuB_OI_2000.Text = "__";
            // 
            // txt_AuA_OI_PROM
            // 
            this.txt_AuA_OI_PROM.AutoSize = true;
            this.txt_AuA_OI_PROM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AuA_OI_PROM.Location = new System.Drawing.Point(820, 99);
            this.txt_AuA_OI_PROM.Name = "txt_AuA_OI_PROM";
            this.txt_AuA_OI_PROM.Size = new System.Drawing.Size(21, 13);
            this.txt_AuA_OI_PROM.TabIndex = 34;
            this.txt_AuA_OI_PROM.Text = "__";
            // 
            // txt_AuA_OI_4000
            // 
            this.txt_AuA_OI_4000.AutoSize = true;
            this.txt_AuA_OI_4000.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AuA_OI_4000.Location = new System.Drawing.Point(734, 99);
            this.txt_AuA_OI_4000.Name = "txt_AuA_OI_4000";
            this.txt_AuA_OI_4000.Size = new System.Drawing.Size(21, 13);
            this.txt_AuA_OI_4000.TabIndex = 33;
            this.txt_AuA_OI_4000.Text = "__";
            // 
            // txt_AuA_OI_3000
            // 
            this.txt_AuA_OI_3000.AutoSize = true;
            this.txt_AuA_OI_3000.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AuA_OI_3000.Location = new System.Drawing.Point(657, 99);
            this.txt_AuA_OI_3000.Name = "txt_AuA_OI_3000";
            this.txt_AuA_OI_3000.Size = new System.Drawing.Size(21, 13);
            this.txt_AuA_OI_3000.TabIndex = 32;
            this.txt_AuA_OI_3000.Text = "__";
            // 
            // txt_AuA_OI_2000
            // 
            this.txt_AuA_OI_2000.AutoSize = true;
            this.txt_AuA_OI_2000.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AuA_OI_2000.Location = new System.Drawing.Point(582, 99);
            this.txt_AuA_OI_2000.Name = "txt_AuA_OI_2000";
            this.txt_AuA_OI_2000.Size = new System.Drawing.Size(21, 13);
            this.txt_AuA_OI_2000.TabIndex = 31;
            this.txt_AuA_OI_2000.Text = "__";
            // 
            // txt_AuA_OD_PROM
            // 
            this.txt_AuA_OD_PROM.AutoSize = true;
            this.txt_AuA_OD_PROM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AuA_OD_PROM.Location = new System.Drawing.Point(820, 64);
            this.txt_AuA_OD_PROM.Name = "txt_AuA_OD_PROM";
            this.txt_AuA_OD_PROM.Size = new System.Drawing.Size(21, 13);
            this.txt_AuA_OD_PROM.TabIndex = 30;
            this.txt_AuA_OD_PROM.Text = "__";
            // 
            // txt_AuA_OD_4000
            // 
            this.txt_AuA_OD_4000.AutoSize = true;
            this.txt_AuA_OD_4000.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AuA_OD_4000.Location = new System.Drawing.Point(734, 64);
            this.txt_AuA_OD_4000.Name = "txt_AuA_OD_4000";
            this.txt_AuA_OD_4000.Size = new System.Drawing.Size(21, 13);
            this.txt_AuA_OD_4000.TabIndex = 29;
            this.txt_AuA_OD_4000.Text = "__";
            // 
            // txt_AuA_OD_3000
            // 
            this.txt_AuA_OD_3000.AutoSize = true;
            this.txt_AuA_OD_3000.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AuA_OD_3000.Location = new System.Drawing.Point(657, 64);
            this.txt_AuA_OD_3000.Name = "txt_AuA_OD_3000";
            this.txt_AuA_OD_3000.Size = new System.Drawing.Size(21, 13);
            this.txt_AuA_OD_3000.TabIndex = 28;
            this.txt_AuA_OD_3000.Text = "__";
            // 
            // txt_AuA_OD_2000
            // 
            this.txt_AuA_OD_2000.AutoSize = true;
            this.txt_AuA_OD_2000.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AuA_OD_2000.Location = new System.Drawing.Point(582, 64);
            this.txt_AuA_OD_2000.Name = "txt_AuA_OD_2000";
            this.txt_AuA_OD_2000.Size = new System.Drawing.Size(21, 13);
            this.txt_AuA_OD_2000.TabIndex = 27;
            this.txt_AuA_OD_2000.Text = "__";
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(339, 165);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(271, 18);
            this.label16.TabIndex = 35;
            this.label16.Text = "** Se considera STS si valor >= 10";
            // 
            // ucCalculoSTS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txt_AuA_OI_PROM);
            this.Controls.Add(this.txt_AuA_OI_4000);
            this.Controls.Add(this.txt_AuA_OI_3000);
            this.Controls.Add(this.txt_AuA_OI_2000);
            this.Controls.Add(this.txt_AuA_OD_PROM);
            this.Controls.Add(this.txt_AuA_OD_4000);
            this.Controls.Add(this.txt_AuA_OD_3000);
            this.Controls.Add(this.txt_AuA_OD_2000);
            this.Controls.Add(this.txt_AuB_OI_PROM);
            this.Controls.Add(this.txt_AuB_OI_4000);
            this.Controls.Add(this.txt_AuB_OI_3000);
            this.Controls.Add(this.txt_AuB_OI_2000);
            this.Controls.Add(this.txt_AuB_OD_PROM);
            this.Controls.Add(this.txt_AuB_OD_4000);
            this.Controls.Add(this.txt_AuB_OD_3000);
            this.Controls.Add(this.txt_AuB_OD_2000);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.txtOidoIzquierdo);
            this.Controls.Add(this.txtOidoDerecho);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "ucCalculoSTS";
            this.Size = new System.Drawing.Size(929, 240);
            this.Load += new System.EventHandler(this.ucCalculoSTS_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOidoDerecho;
        private System.Windows.Forms.TextBox txtOidoIzquierdo;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label txt_AuB_OD_2000;
        private System.Windows.Forms.Label txt_AuB_OD_3000;
        private System.Windows.Forms.Label txt_AuB_OD_4000;
        private System.Windows.Forms.Label txt_AuB_OD_PROM;
        private System.Windows.Forms.Label txt_AuB_OI_PROM;
        private System.Windows.Forms.Label txt_AuB_OI_4000;
        private System.Windows.Forms.Label txt_AuB_OI_3000;
        private System.Windows.Forms.Label txt_AuB_OI_2000;
        private System.Windows.Forms.Label txt_AuA_OI_PROM;
        private System.Windows.Forms.Label txt_AuA_OI_4000;
        private System.Windows.Forms.Label txt_AuA_OI_3000;
        private System.Windows.Forms.Label txt_AuA_OI_2000;
        private System.Windows.Forms.Label txt_AuA_OD_PROM;
        private System.Windows.Forms.Label txt_AuA_OD_4000;
        private System.Windows.Forms.Label txt_AuA_OD_3000;
        private System.Windows.Forms.Label txt_AuA_OD_2000;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label16;
    }
}
