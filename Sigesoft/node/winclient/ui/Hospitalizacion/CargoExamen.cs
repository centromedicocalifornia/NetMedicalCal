using Sigesoft.Node.WinClient.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.UI.Reports;
using Sigesoft.Node.Contasol.Integration;
using System.Data.SqlClient;

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class CargoExamen : Form
    {
        public string serviceComponent { get; set; }
        private OperationResult _objOperationResult = new OperationResult();

        public CargoExamen(string _serviceComponent)
        {
            serviceComponent = _serviceComponent;
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (rbMedico.Checked == true)
            {
                ConexionSigesoft conectasam = new ConexionSigesoft();
                conectasam.opensigesoft();
                var cadena1 = "update [dbo].[servicecomponent] set i_ConCargoA = 1 where v_ServiceComponentId = '" + serviceComponent + "'";
                SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                SqlDataReader lector = comando.ExecuteReader();
                lector.Close();
                conectasam.closesigesoft();
                this.Close();
                MessageBox.Show("Guardado con éxito." + System.Environment.NewLine + _objOperationResult.ExceptionMessage, "Guardado!", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
            else
            {
                ConexionSigesoft conectasam = new ConexionSigesoft();
                conectasam.opensigesoft();
                var cadena1 = "update [dbo].[servicecomponent] set i_ConCargoA = 2 where v_ServiceComponentId = '" + serviceComponent + "'";
                SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                SqlDataReader lector = comando.ExecuteReader();
                lector.Close();
                conectasam.closesigesoft();
                this.Close();
                MessageBox.Show("Guardado con éxito." + System.Environment.NewLine + _objOperationResult.ExceptionMessage, "Guardado!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CargoExamen_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();


            #region Conexion SAM
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            #endregion
            var cadena1 = "select sc.v_ServiceComponentId, sc.v_ComponentId, sc.i_ConCargoA, c.v_Name from [dbo].[servicecomponent] sc join [dbo].[component] c on sc.v_ComponentId = c.v_ComponentId  where sc.v_ServiceComponentId = '" + serviceComponent + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            string ServiceComponent = "";
            string ComponentId = "";
            int ConCargoA = 0;
            string Componente = "";
            while (lector.Read())
            {
                ServiceComponent = lector.GetValue(0).ToString();
                ComponentId = lector.GetValue(1).ToString();
                ConCargoA = int.Parse(lector.GetValue(2).ToString());
                Componente = lector.GetValue(3).ToString();
            }
            lector.Close();
            conectasam.closesigesoft();

            lablExamen.Text = Componente;
            if (ConCargoA == 1)
            {
                rbMedico.Checked = true;
                rbPaciente.Checked = false;
            }
            else
            {
                //2 paciente
                rbMedico.Checked = false;
                rbPaciente.Checked = true;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void rbMedico_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)Keys.Enter)
            //{
            //    btnFilter_Click(null, null);
            //}
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void rbPaciente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
