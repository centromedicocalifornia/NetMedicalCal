using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.BE;

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmEditServiceComponent : Form
    {
        private servicecomponentDto objserviceComponent = null;
        private string _serviceComponentId = "", _serviceComponentName = "";
        public frmEditServiceComponent(string serviceComponentId, string serviceComponentName)
        {
            _serviceComponentName = serviceComponentName;
            _serviceComponentId = serviceComponentId;
            InitializeComponent();
        }

        private void frmEditServiceComponent_Load(object sender, EventArgs e)
        {
            OperationResult objOperation = new OperationResult();
            lblNombreExamen.Text = _serviceComponentName;
            objserviceComponent = new ServiceBL().GetServiceComponent(ref objOperation, _serviceComponentId);
            if (objserviceComponent.i_ConCargoA == 1)
            {
                cboPagador.SelectedText = "Médico";
            }
            else
            {
                cboPagador.SelectedText = "Paciente";
            }

            txtPrecio.Text = objserviceComponent.r_Price.ToString();
            calcular();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            OperationResult objOperation = new OperationResult();
            try
            {
                var conCargoA = 2;
                if (cboPagador.Text == "Médico")
                {
                    conCargoA = 1;
                }

                float total = float.Parse(txtTotal.Text.ToString());
                objserviceComponent.i_ConCargoA = conCargoA;
                objserviceComponent.r_Price = total;
                bool exit = new ServiceBL().UpdateServiceComponent(ref objOperation, objserviceComponent, Globals.ClientSession.GetAsList());
                if (exit)
                {
                    DialogResult mess =  MessageBox.Show("Se guardaron los cambios correctamente.", "HECHO", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    if (mess == DialogResult.OK)
                    {
                        this.Close();
                    }
                    
                }
                else
                {
                    MessageBox.Show("Sucedió un error, por favor vuelva a intentar.", "EROR", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifique que los datos ingresados sean número.", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            
        }

        private void txtFactor_TextChanged(object sender, EventArgs e)
        {
            calcular();
        }

        void calcular()
        {
            if (txtPrecio.Text == "")
            {
                txtPrecio.Text = "0";
            }
            if (txtFactor.Text == "")
	        {
                txtFactor.Text = "0"; 
	        }
            double Precio = double.Parse(txtPrecio.Text.ToString());
            double Factor = double.Parse(txtFactor.Text.ToString());
            txtTotal.Text = (Precio * Factor).ToString();
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            calcular();
        }

        private void txtPrecio_KeyUp(object sender, KeyEventArgs e)
        {
            calcular();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
