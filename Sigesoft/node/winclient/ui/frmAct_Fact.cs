using Sigesoft.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmAct_Fact : Form
    {
        private string v_NroLiquidacion;
        private string v_LiquidacionId;
        private string v_NroFactura;
        public frmAct_Fact()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAct_Click(object sender, EventArgs e)
        {
            v_LiquidacionId = txt_v_LiquidacionId.Text;
            v_NroFactura = txt_v_NroFactura.Text;
            ActualizarRegistro(v_LiquidacionId, v_NroFactura);
        }

        private void ActualizarRegistro(string v_LiquidacionId, string v_NroFactura)
        {
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena1 = "select SR.v_NroLiquidacion from service SR " +
                          "inner join liquidacion LQ on SR.v_NroLiquidacion=LQ.v_NroLiquidacion " +
                          "where v_LiquidacionId='"+v_LiquidacionId+"' " +
                          "group by SR.v_NroLiquidacion";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            string v_NroLiquidacion = "";
            while (lector.Read())
            {
                v_NroLiquidacion = lector.GetValue(0).ToString();
            }
            cadena1 = "update liquidacion set v_NroFactura='"+v_NroFactura+"' where v_NroLiquidacion='"+v_NroLiquidacion+"'";
            comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            lector = comando.ExecuteReader();
            lector.Close();
            conectasam.closesigesoft();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAct_Fact_Load(object sender, EventArgs e)
        {

        }
    }
}
