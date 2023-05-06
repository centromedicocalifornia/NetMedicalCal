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
    public partial class frmPaymentAdd : Form
    {
        private string _paymentId;
        private List<PayComponent> paymentDetails = new List<PayComponent>();
        private List<PayComponent> component = new List<PayComponent>();
        private PaymentDetail paydetail = new PaymentDetail();
        public frmPaymentAdd(string paymentId)
        {
            _paymentId = paymentId;
            InitializeComponent();
        }

        private void frmPaymentAdd_Load(object sender, EventArgs e)
        {
            component = new List<PayComponent>();
            component = ObtenerComponentes();
            grdDataComponent.DataSource = component;
            btnRefresh_Click(sender, e);

        }

        private List<PayComponent> ObtenerComponentes()
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();

            string cadena =
                "select CP.v_ComponentId, CP.v_Name, CP.i_CategoryId, SP.v_Value1 from component CP " +
                "inner join systemparameter SP on CP.i_CategoryId=Sp.i_ParameterId and SP.i_GroupId=116 where CP.i_IsDeleted=0";

            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            List<PayComponent> Details = new List<PayComponent>();
            while (lector.Read())
            {
                PayComponent pp = new PayComponent();
                pp.i_CategoryId = Convert.ToInt32(lector.GetValue(2).ToString());
                pp.v_CategoryName = lector.GetValue(3).ToString();
                Details.Add(pp);
            }

            return Details;
        }

        private List<PayComponent> ObtenerDataPayment(string _paymentId)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();

            string cadena =
                "select PD.v_PaymentDetail_Id,CP.v_ComponentId, CP.v_Name, CP.i_CategoryId, SP.v_Value1, SP.v_Value1 from payment PY " +
                "inner join paymentDetail PD on PY.v_Payment_Id=PD.v_Payment_Id " +
                "inner join component CP on PD.v_ComponentId=CP.v_ComponentId " +
                "inner join systemparameter SP on CP.i_CategoryId=Sp.i_ParameterId and SP.i_GroupId=116" +
                " where PD.i_IsDeleted=0 and PD.v_Payment_Id='" + _paymentId + "'";

            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            List<PayComponent> Details = new List<PayComponent>();
            while (lector.Read())
            {
                PayComponent pp = new PayComponent();
                pp.v_PaymentDetail_Id = lector.GetValue(0).ToString();
                pp.v_ComponentId = lector.GetValue(1).ToString();
                pp.v_ComponentName = lector.GetValue(2).ToString();
                pp.i_CategoryId = Convert.ToInt32(lector.GetValue(3).ToString());
                pp.v_CategoryName = lector.GetValue(4).ToString();
                Details.Add(pp);
            }

            return Details;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            paymentDetails = new List<PayComponent>();
            paymentDetails = ObtenerDataPayment(_paymentId);
            grdDataPayment.DataSource = paymentDetails;
            btnAgregar.Enabled = false;
        }

        private void grdDataComponent_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            btnAgregar.Enabled = true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
            if (grdDataComponent.Selected.Rows.Count>=1)
            {
                foreach (var row in grdDataComponent.Selected.Rows)
                {
                    var v_ComponentId = row.Cells["v_ComponentId"].Value.ToString();
                    paydetail = new PaymentDetail();
                    paydetail.v_PaymentDetail_Id = ObtenerId();
                    paydetail.v_Payment_Id = _paymentId;
                    paydetail.v_ComponentId = v_ComponentId;
                    paydetail.i_IsDeleted = 0;
                    paydetail.i_InsertUserId = Globals.ClientSession.i_SystemUserId;
                    InsertaPayDetail(paydetail);
                }
                btnRefresh_Click(sender, e);
            }
        }

        private void InsertaPayDetail(PaymentDetail pay)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            string cadena =
                "insert into paymentDetail(v_PaymentDetail_Id, v_Payment_Id, v_ComponentId, i_IsDeleted, i_InsertUserId, d_InsertDate) " +
                "values('"+pay.v_PaymentDetail_Id+"' , '"+pay.v_Payment_Id+"' , '"+pay.v_ComponentId+"' , 0, "+pay.i_InsertUserId+" , GETDATE())";
            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            lector.Close();
            conexion.closesigesoft();
        }

        private string ObtenerId()
        {
            int SecuentialId = GetNextSecuentialId(9, 362);
            string Id = "N009-PD" + String.Format("{0:000000000}", SecuentialId);
            return Id;
        }

        private int GetNextSecuentialId(int node, int table)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            int secuential = 0;
            string cadena = "select i_SecuentialId from secuential where i_NodeId=" + node + " and i_TableId=" + table;
            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                secuential = Convert.ToInt32(lector.GetValue(0).ToString());
            }
            lector.Close();
            secuential++;
            cadena = "update secuential set i_SecuentialId=" + secuential + " where i_NodeId=" + node + " and i_TableId=" + table;
            comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            lector = comando.ExecuteReader();
            lector.Close();
            conexion.closesigesoft();
            return secuential;
        }

        private void grdDataPayment_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            btnRemover.Enabled = true;
            btnAgregar.Enabled = false;
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("¿Esta seguro de eliminar el registro?", "Precaución", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (this.DialogResult == DialogResult.OK)
            {
                var PaymentDetail_Id = grdDataPayment.Selected.Rows[0].Cells["v_PaymentDetail_Id"].Value.ToString();
                EliminarRegistro(PaymentDetail_Id);
                MessageBox.Show("Eliminado...", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            btnRefresh_Click(sender, e);
        }

        private void EliminarRegistro(string PaymentDetail_Id)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            string cadena =
                "update paymentDetail set " +
                " i_IsDeleted=1 " +
                " where v_PaymentDetail_Id ='" + PaymentDetail_Id + "'";
            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            lector.Close();
            conexion.closesigesoft();
        }
    }
}
