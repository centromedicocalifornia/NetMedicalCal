using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using NetPdf;
using Sigesoft.Node.WinClient.BE.Custom;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmPaymentMedical : Form
    {
        List<PaymentMedical_> paymedicoTratante = new List<PaymentMedical_>();
        List<PaymentMedical_> paymedicoIndex = new List<PaymentMedical_>();
        OperationResult objOperationResult = new OperationResult();
        paymentmedicDAL paymentmedicDal = new paymentmedicDAL();
        private decimal acumulador = 0;
        public frmPaymentMedical()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            btnEditPorcentaje.Visible = false;
            txtQuota.Visible = false;
            lblQuota.Visible = false;
            valida val = FindValidator();
            if (val.state)
            {
                try
                {
                    paymedicoTratante = new List<PaymentMedical_>();
                    paymedicoIndex = new List<PaymentMedical_>();
                    decimal acumulador = 0;
                    string[] fechaInicio = dtpFechaInicio.Value.ToShortDateString().Split('/');
                    string fInicio = fechaInicio[2] + "/" + fechaInicio[1] + "/" + fechaInicio[0];
                    string[] fechaFin = dtpFechaFin.Value.ToShortDateString().Split('/');
                    string fFin = fechaFin[2] + "/" + fechaFin[1] + "/" + fechaFin[0];
                    string whereFecha = " (SR.d_ServiceDate >= '" + fInicio + "' AND SR.d_ServiceDate <= '" + fFin + "') ";
                    int user = ObtenerUserId(cboUserMed.SelectedValue.ToString());
                    int pagado;
                    int tipoPago;
                    if (chkPagado.Checked) { pagado = 1; }
                    else { pagado = 0; }

                    if (cboTipoAtx.Text == "POR INDICACIÓN")
                    {
                        tipoPago = 1;
                        paymedicoIndex = paymentmedicDal.getPaymentMedical(user, "i_ApplicantMedic", pagado, tipoPago, whereFecha);
                    }
                    else if (cboTipoAtx.Text == "MEDICO TRATANTE")
                    {
                        tipoPago = 2;
                        paymedicoIndex = paymentmedicDal.getPaymentMedical(user, "i_MedicoTratanteId", pagado, tipoPago, whereFecha);
                    }
                    else if (cboTipoAtx.Text == "TODOS")
                    {
                        paymedicoIndex = paymentmedicDal.getPaymentMedical(user, "i_ApplicantMedic", pagado, 1, whereFecha);
                        paymedicoTratante = paymentmedicDal.getPaymentMedical(user, "i_MedicoTratanteId", pagado, 2, whereFecha);
                        paymedicoIndex = paymedicoTratante.Union(paymedicoIndex).ToList();
                    }

                    if (chkFarmacia.Checked)
                    {
                        paymedicoIndex = paymentmedicDal.getFarmaciaPay(user, pagado, 3, whereFecha);
                        btnEditPorcentaje.Visible = true;
                        txtQuota.Visible = true;
                        lblQuota.Visible = true;
                        txtQuota.Text = paymentmedicDal.getQuota(ObtenerUserId(cboUserMed.SelectedValue.ToString()), 3);
                    }
                    foreach (var pay in paymedicoIndex)
                    {
                        if (pay.i_TypeAttention == 1) { pay.tipoAtx = "POR INDICACIÓN"; }
                        else if (pay.i_TypeAttention == 2) { pay.tipoAtx = "MEDICO TRATANTE"; }
                        else if (pay.i_TypeAttention == 3) { pay.tipoAtx = "FARMACIA"; }
                        pay.r_subTotal = (pay.r_Price/(decimal) 1.18) * pay.r_PaymentPercentage / 100;
                        acumulador = acumulador + pay.r_subTotal;
                    }

                    grdPayment.DataSource = paymedicoIndex;
                    txtTotal.Text = acumulador.ToString();
                    //if (paymedicoIndex!=null)
                    //{
                    //    btnPagar.Enabled = true;
                    //}
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString(), "¡¡¡ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(val.message, "¡¡¡ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private valida FindValidator()
        {
            valida val = new valida();
            if (cboUserMed.Text == "--Seleccionar--")
            {
                val.state = false;
                val.message = "Seleccione un usuario válido";
            }
            else if (cboTipoAtx.Text == "" && !chkFarmacia.Checked)
            {
                val.state = false;
                val.message = "Seleccione un tipo de atención";
            }
            else
            {
                val.state = true;
                val.message = "";
            }
            return val;
        }

        
        private int ObtenerUserId(string UserName)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            string cadena = "select i_SystemUserId from systemuser where i_SystemUserId ='" + UserName + "'";
            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            int id = 0;
            while (lector.Read())
            {
                id = Convert.ToInt32(lector.GetValue(0).ToString());
            }
            lector.Close();
            conexion.closesigesoft();
            return id;
        }

        private void frmPaymentMedical_Load(object sender, EventArgs e)
        {
            //Utils.LoadDropDownList(cboUserMed, "Value1", "Id", BLL.Utils.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);

            Utils.LoadDropDownList(cboUserMed, "Value1", "Id", BLL.Utils.GetProfessionalName(ref objOperationResult), DropDownListAction.All);
            cboUserMed.SelectedValue = "-1";
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            foreach (var pay in paymedicoIndex)
            {
                paymentmedicDal.ActualizaPagado(pay.v_serviceComponentId);
            }
            btnBuscar_Click(sender, e);
            btnPagar.Enabled = false;

            #region Reporte de pago



            #endregion
        }

        private void chkFarmacia_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFarmacia.Checked)
            {
                cboTipoAtx.Text = "";
                cboTipoAtx.Enabled = false;
            }
            else
            {
                cboTipoAtx.Enabled = true;
            }
        }

        private void btnEditPorcentaje_Click(object sender, EventArgs e)
        {
            decimal monto = 0;
            foreach (var pp in paymedicoIndex)
            {
                monto = monto + pp.r_Price;
            }
            frmPayPorcentaje frm = new frmPayPorcentaje(monto);
            frm.ShowDialog();
            txtTotal.Text = frm._newMonto.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cboUserMed.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show("Debe elegir un Medico a pagar.", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            foreach (var pay in paymedicoIndex)
            {
                paymentmedicDal.ActualizaPagado(pay.v_serviceComponentId);
            }

            #region Reporte de pago
            int validacionTipoPago = 0;
            if (chkFarmacia.Checked == true)
            {
                validacionTipoPago = 2;
            }
            else
            {
                validacionTipoPago = 1;
            }
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;

                var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
                OperationResult objOperationResult = new OperationResult();

                DateTime? fechaInicio = dtpFechaInicio.Value.Date;
                DateTime? fechaFin = dtpFechaFin.Value.Date;

                string fechaInicio_1 = fechaInicio.ToString().Split(' ')[0];
                string fechaFin_1 = fechaFin.ToString().Split(' ')[0];

                string ruta = Common.Utils.GetApplicationConfigValue("rutaPagoMedicos").ToString();

                string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                string nombre = "Pago Medico - CSL";

                var medico_info = new ServiceBL().GetSystemUser(ref objOperationResult, int.Parse(cboUserMed.SelectedValue.ToString()));

                PagoMedicoAsitencial_New.CreatePagoMedicoAsitencial_New(ruta + nombre + ".pdf", MedicalCenter, paymedicoIndex, fechaInicio_1, fechaFin_1, medico_info, validacionTipoPago);
                this.Enabled = true;
            }
            btnBuscar_Click(sender, e);
            btnPagar.Enabled = false;

            #endregion
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Liquidación de Médicos del " + dtpFechaInicio.Text + " al " + dtpFechaFin.Text;
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdPayment, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            int validacionTipoPago = 0;
            if (chkFarmacia.Checked == true)
            {
                validacionTipoPago = 2;
            }
            else
            {
                validacionTipoPago = 1;
            }
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;

                var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
                OperationResult objOperationResult = new OperationResult();

                DateTime? fechaInicio = dtpFechaInicio.Value.Date;
                DateTime? fechaFin = dtpFechaFin.Value.Date;

                string fechaInicio_1 = fechaInicio.ToString().Split(' ')[0];
                string fechaFin_1 = fechaFin.ToString().Split(' ')[0];

                string ruta = Common.Utils.GetApplicationConfigValue("rutaPagoMedicos").ToString();

                string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                string nombre = "Pago Medico - CSL";

                var medico_info = new ServiceBL().GetSystemUser(ref objOperationResult, int.Parse(cboUserMed.SelectedValue.ToString()));

                PagoMedicoAsitencial_New.CreatePagoMedicoAsitencial_New(ruta + nombre + ".pdf", MedicalCenter, paymedicoIndex, fechaInicio_1, fechaFin_1, medico_info, validacionTipoPago);
                this.Enabled = true;
            }
        }

        private void chkPagado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPagado.Checked == true)
            {
                btnPagar.Enabled = false;
                btnImprimir.Enabled = true;
            }
            else
            {
                btnPagar.Enabled = true;
                btnImprimir.Enabled = false;
            }
        }

        

       
    }
}
