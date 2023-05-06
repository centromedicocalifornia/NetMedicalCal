using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmPayment : Form
    {
        List<Payment> payments = new List<Payment>();
        List<PaymentDetail> paymentDetails = new List<PaymentDetail>();
        private bool _focus;

        public frmPayment()
        {
            InitializeComponent();
        }

        private void frmPayment_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            payments = new List<Payment>();
            payments = ObtenerPayment(txtUserName.Text.Trim());
            grdPayment.DataSource = payments;
            grdPaymentDetail.DataSource = new List<PaymentDetail>();
            btnEdit.Enabled = false;
            btnEliminar.Enabled = false;
            btnAdd.Enabled = false;
        }

        private List<Payment> ObtenerPayment(string user)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            string where = "where PY.i_IsDeleted=0 ";
            if (user !="")
            {
                where = where + " and SU.v_UserName = '"+user+"'";
            }

            string cadena =
                "select v_Payment_Id,i_User_Id, SU.v_UserName, i_TypeAttention, r_PaymentPercentage from payment PY " +
                "inner join systemuser SU on PY.i_User_Id=SU.i_SystemUserId " + where;

            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector =comando.ExecuteReader();
            List<Payment> pay = new List<Payment>();
            while (lector.Read())
            {
                Payment pp = new Payment();
                pp.v_Payment_Id = lector.GetValue(0).ToString();
                pp.i_User_Id = Convert.ToInt32(lector.GetValue(1).ToString());
                pp.v_UserName = lector.GetValue(2).ToString();
                pp.i_TypeAttention = Convert.ToInt32(lector.GetValue(3).ToString());
                pp.v_AtencionName = lector.GetValue(3).ToString()=="1"?"POR INDICACION":lector.GetValue(3).ToString()=="2"?"MEDICO TRATANTE":"FARMACIA";
                pp.r_PaymentPercentage = Convert.ToDecimal(lector.GetValue(4).ToString());
                pay.Add(pp);
            }

            return pay;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmPaymentHeader frm = new frmPaymentHeader("new","");
            frm.ShowDialog();
            btnBuscar_Click(sender, e);
        }

        private void grdPayment_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
           
            
        }

        private List<PaymentDetail> ObtenerPaymentDetails(string paymentId)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();

            string cadena =
                "select v_PaymentDetail_Id, v_Payment_Id, PD.v_ComponentId, CP.v_Name, CP.i_CategoryId, SP.v_Value1 from paymentDetail PD " +
                "inner join component CP on PD.v_ComponentId=CP.v_ComponentId " +
                "inner join systemparameter SP on CP.i_CategoryId=Sp.i_ParameterId and SP.i_GroupId=116 " +
                "where PD.i_IsDeleted=0 and v_Payment_Id='"+paymentId+"'";

            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            List<PaymentDetail> Details = new List<PaymentDetail>();
            while (lector.Read())
            {
                PaymentDetail pp = new PaymentDetail();
                pp.v_PaymentDetail_Id = lector.GetValue(0).ToString();
                pp.v_Payment_Id = lector.GetValue(1).ToString();
                pp.v_ComponentId = lector.GetValue(2).ToString();
                pp.v_ComponentName = lector.GetValue(3).ToString();
                pp.i_CategoryId = lector.GetValue(4).ToString();
                pp.v_CategoryName = lector.GetValue(5).ToString();
                Details.Add(pp);
            }

            return Details;
        }

        private void grdPayment_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            var paymentId = grdPayment.Selected.Rows[0].Cells["v_Payment_Id"].Value.ToString();
            if (paymentId == null)
            {
                return;
            }
            paymentDetails = new List<PaymentDetail>();
            paymentDetails = ObtenerPaymentDetails(paymentId);
            grdPaymentDetail.DataSource = paymentDetails;
            btnEdit.Enabled = true;
            btnEliminar.Enabled = true;
            btnAdd.Enabled = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var paymentId = grdPayment.Selected.Rows[0].Cells["v_Payment_Id"].Value.ToString();
            frmPaymentHeader frm = new frmPaymentHeader("edit", paymentId);
            frm.ShowDialog();
            btnBuscar_Click(sender, e);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("¿Esta seguro de eliminar el registro?", "Precaución",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (this.DialogResult == DialogResult.OK)
            {
                var paymentId = grdPayment.Selected.Rows[0].Cells["v_Payment_Id"].Value.ToString();
                EliminarRegistro(paymentId);
                MessageBox.Show("Eliminado...", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnBuscar_Click(sender, e);
            }
            
        }

        private void EliminarRegistro(string paymentId)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            string cadena =
                "update payment set " +
                " i_IsDeleted=1 " +
                " where v_Payment_Id='" + paymentId + "'";
            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            lector.Close();
            conexion.closesigesoft();
            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string paymentId = grdPayment.Selected.Rows[0].Cells["v_Payment_Id"].Value.ToString();

            frmPaymentAdd frm = new frmPaymentAdd(paymentId);
            frm.ShowDialog();
        }
    }
}
