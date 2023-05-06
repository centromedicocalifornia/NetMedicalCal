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

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmTipoPacHospSala : Form
    {
        public string HospitalizacionId;
        private OperationResult _objOperationResult = new OperationResult();

        public frmTipoPacHospSala(string _HospitalizacionId)
        {
            HospitalizacionId = _HospitalizacionId;
            InitializeComponent();
        }

        private void frmTipoPacHospSala_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            
            #region Conexion 
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            #endregion
            var cadena1 = "select ISNULL(v_ProcedenciaPac,'0') as v_ProcedenciaPac,ISNULL(i_PasoSop,0) as Sop, ISNULL(i_PasoHosp,0) as Hosp from hospitalizacion where v_HopitalizacionId =  '" + HospitalizacionId + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            string tipoPac = "";
            int sop = 0;
            int hosp = 0;
            while (lector.Read())
            {
                tipoPac = lector.GetValue(0).ToString();
                sop = int.Parse(lector.GetValue(1).ToString());
                hosp = int.Parse(lector.GetValue(2).ToString());
            }
            lector.Close();
            conectasam.closesigesoft();

            if (tipoPac == "1")
            {
                rbclinicaAbierta.Checked = true;
                rbclinica.Checked = false;
            }
            else if (tipoPac == "2")
            {
                rbclinicaAbierta.Checked = false;
                rbclinica.Checked = true;
            }
            else
            {
                rbclinicaAbierta.Checked = false;
                rbclinica.Checked = false;
            }
            if (sop == 0)
                checkSop.Checked = false;
            else
                checkSop.Checked = true;

            if (hosp == 0)
                checkHosp.Checked = false;
            else
                checkHosp.Checked = true;


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string procedencia = "NULL";

            if (rbclinicaAbierta.Checked == true)
            {
                procedencia = "'1'";
            }
            else if (rbclinica.Checked == true)
            {
                procedencia = "'2'";
            }

            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            int sop = 0;
            int hosp = 0;
            if (checkSop.Checked == true)
                sop = 1;
            if (checkHosp.Checked == true)
                hosp = 1;

            var cadena1 = "update hospitalizacion set v_ProcedenciaPac = " + procedencia+" , i_PasoSop = " + sop + " , i_PasoHosp = " + hosp + " where v_HopitalizacionId = '" + HospitalizacionId + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            lector.Close();
            conectasam.closesigesoft();
            this.Close();
            MessageBox.Show("Guardado con éxito.", "Guardado!", MessageBoxButtons.OK, MessageBoxIcon.Information);    

          
        }
    }
}
