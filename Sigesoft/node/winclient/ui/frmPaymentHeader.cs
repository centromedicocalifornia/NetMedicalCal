using Sigesoft.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmPaymentHeader : Form
    {
        OperationResult objOperationResult = new OperationResult();
        private string _modo = "";
        private string _paymentId = "";
        public frmPaymentHeader(string modo, string paymentId)
        {
            _paymentId = paymentId;
            _modo = modo;
            InitializeComponent();
        }

        private void frmPaymentHeader_Load(object sender, EventArgs e)
        {
            Utils.LoadDropDownList(cboUserMed, "Value1", "Id", BLL.Utils.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
            if (_modo=="edit")
            {
                LoadFrm(_paymentId);
                cboTipoAtx.Enabled = false;
                cboUserMed.Enabled = false;
            }
        }

        private void LoadFrm(string _paymentId)
        {
            Payment pay = new Payment();
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            string cadena = "select SU.v_UserName, i_TypeAttention, r_PaymentPercentage from payment PY " +
                            "inner join systemuser SU on PY.i_User_Id=SU.i_SystemUserId " +
                            "where v_Payment_Id='"+_paymentId+"'";
            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                cboUserMed.Text = lector.GetValue(0).ToString();
                cboTipoAtx.Text = lector.GetValue(1).ToString()=="1"?"POR INDICACIÓN":lector.GetValue(1).ToString()=="2"?"MEDICO TRATANTE":"FARMACIA";
                txtPorcentaje.Text = lector.GetValue(2).ToString();
            }
            lector.Close();
            conexion.closesigesoft();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtPorcentaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (Char.IsDigit(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Payment pay = new Payment();
                pay.i_User_Id = ObtenerIdMedico(cboUserMed.Text);
                pay.i_TypeAttention = cboTipoAtx.Text == "POR INDICACIÓN" ? 1 : cboTipoAtx.Text == "MEDICO TRATANTE" ? 2 : 3;
                pay.r_PaymentPercentage = Convert.ToDecimal(txtPorcentaje.Text);
                pay.i_IsDeleted = 0;
                pay.i_InsertUserId = Globals.ClientSession.i_SystemUserId;
                pay.d_InsertDate = DateTime.Now;
                if (_modo=="new")
                {
                    pay.v_Payment_Id = ObtenerId();
                    bool result = ValidarExiste(pay.i_User_Id, pay.i_TypeAttention);
                    if (result)
                    {
                        MessageBox.Show("El usuario ya cuenta con un registro...", "Precaución!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        InsertarPayment(pay);
                    }
                    
                }
                else
                {
                    pay.v_Payment_Id = _paymentId;
                    UpdatePayment(pay);
                }
                
                MessageBox.Show("Registrado...", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            
        }

        private bool ValidarExiste(int user, int atencion)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            bool result = false;
            string cadena = "select v_Payment_Id from payment where i_User_Id=" + user + " and i_TypeAttention=" + atencion + " and i_IsDeleted=0";
            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            string exist = "";
            while (lector.Read())
            {
                exist = lector.GetValue(0).ToString();
            }
            lector.Close();
            conexion.closesigesoft();
            if (exist != ""){result = true;}

            return result;
        }

        private void UpdatePayment(Payment pay)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            string cadena =
                "update payment set " +
                "i_User_Id=" + pay.i_User_Id + ", i_TypeAttention=" + pay.i_TypeAttention + ",r_PaymentPercentage=" +
                pay.r_PaymentPercentage + ",i_UpdateUserId=" + pay.i_InsertUserId + ",d_UpdateDate=GETDATE() " +
                " where v_Payment_Id='" + pay.v_Payment_Id + "'";
            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            lector.Close();
            conexion.closesigesoft();
        }

        private void InsertarPayment(Payment pay)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            string cadena =
                "Insert into payment(v_Payment_Id,i_User_Id,i_TypeAttention,r_PaymentPercentage,i_IsDeleted,i_InsertUserId,d_InsertDate) " +
                "values('" + pay.v_Payment_Id + "'," + pay.i_User_Id + "," + pay.i_TypeAttention + "," +
                pay.r_PaymentPercentage + ",0," + pay.i_InsertUserId + ",GETDATE())";
            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            lector.Close();
            conexion.closesigesoft();
        }

        private int ObtenerIdMedico(string username)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            int userId = 0;
            string cadena = "select i_SystemUserId from systemuser where v_UserName='"+username+"'";
            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                userId = Convert.ToInt32(lector.GetValue(0).ToString());
            }
            lector.Close();
            conexion.closesigesoft();
            return userId;
        }

        private string ObtenerId()
        {
            int SecuentialId = GetNextSecuentialId(9, 361);
            string Id = "N009-PY" + String.Format("{0:000000000}", SecuentialId);
            return Id;
        }

        private int GetNextSecuentialId(int node, int table)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            int secuential = 0;
            string cadena = "select i_SecuentialId from secuential where i_NodeId="+node+" and i_TableId="+table;
            SqlCommand comando =  new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                secuential = Convert.ToInt32(lector.GetValue(0).ToString());
            }
            lector.Close();
            secuential++;
            cadena = "update secuential set i_SecuentialId="+secuential+" where i_NodeId="+node+" and i_TableId="+table;
            comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            lector = comando.ExecuteReader();
            lector.Close();
            conexion.closesigesoft();
            return secuential;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
