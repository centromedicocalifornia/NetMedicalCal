using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE.Custom;

namespace Sigesoft.Node.WinClient.UI
{

    public class paymentmedicDAL
    {
        ConexionSigesoft conexion = new ConexionSigesoft();
        private string sql = "";
        private SqlCommand comando;
        private SqlDataReader lector;

        private object returnObj(string sql)
        {
            conexion.opensigesoft();
            object retornar = null;
            comando = new SqlCommand(sql, conexion.conectarsigesoft);
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                retornar = lector.GetValue(0);
            }
            lector.Close();
            conexion.closesigesoft();
            return retornar;
        }

        private void Executequery(string sql)
        {
            conexion.opensigesoft();
            SqlCommand comando = new SqlCommand(sql, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            lector.Close();
            conexion.closesigesoft();
        }

        public int getUserId(string userName)
        {
            sql = "SELECT i_SystemUserId FROM dbo.systemuser WHERE v_UserName='" + userName + "' and i_SystemUserTypeId = 1 ";
            int user = (int)returnObj(sql);
            return user;
        }



        internal void InsertPay(PaymentMedic _pay)
        {
            sql = "INSERT INTO dbo.paymentmedic(    i_CategoryId,    i_TypePay,    i_UserId,    r_PayPercentage,    i_IsDeleted,    i_InsertUserId,    d_InsertDate, r_QuotaMonth) " +
                         "VALUES " + "(" + _pay.i_CategoryId + ", " + _pay.i_TypePay + ", " + _pay.i_UserId + ", " + _pay.r_PayPercentage + ", 0 , " + Globals.ClientSession.i_SystemUserId + ", GETDATE(), " + _pay.r_QuotaMonth +" )";
            Executequery(sql);
        }



        internal void UpdatePay(PaymentMedic _pay)
        {
            sql = "UPDATE dbo.paymentmedic SET  i_CategoryId=" + _pay.i_CategoryId + ", i_TypePay=" + _pay.i_TypePay + ", r_PayPercentage=" + _pay.r_PayPercentage + ", i_UpdateUserId=" + Globals.ClientSession.i_SystemUserId + ", d_UpdateDate=GETDATE(),  r_QuotaMonth = "+ _pay.r_QuotaMonth +" WHERE i_PaymetId=" + _pay.i_PaymetId;
            Executequery(sql);
        }

        internal List<CategoryComp> getCategory()
        {
            List<CategoryComp> cn = new List<CategoryComp>();
            conexion.opensigesoft();
            sql = "SELECT CP.i_CategoryId, SP.v_Value1 AS CategoryName FROM dbo.component CP " +
                  "INNER JOIN dbo.systemparameter SP ON CP.i_CategoryId=SP.i_ParameterId AND SP.i_GroupId=116 " +
                  "GROUP BY CP.i_CategoryId, SP.v_Value1";
            comando = new SqlCommand(sql, conexion.conectarsigesoft);
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                CategoryComp c = new CategoryComp();
                c.i_CategoryId = Convert.ToInt32(lector.GetValue(0).ToString());
                c.CategoryName = lector.GetValue(1).ToString();
                cn.Add(c);
            }
            lector.Close();
            conexion.closesigesoft();
            return cn;
        }

        internal bool RegisterExist(int i_CategoryId, string typeAtx, string userName)
        {
            sql = "SELECT PM.i_PaymetId FROM dbo.paymentmedic PM " +
                  "INNER JOIN dbo.systemparameter SP ON PM.i_TypePay=SP.i_ParameterId AND SP.i_GroupId=373 " +
                  "INNER JOIN dbo.systemuser SU ON PM.i_UserId=SU.i_SystemUserId " +
                  "WHERE PM.i_CategoryId=" + i_CategoryId + " AND SP.v_Value1='" + typeAtx + "' AND SU.v_UserName='" + userName + "' and PM.i_IsDeleted=0";
            if (returnObj(sql) != null){return true;}
            else{return false;}
        }

        internal PaymentMedic GetPaynetById(int PaymetId)
        {
            conexion.opensigesoft();
            PaymentMedic p = new PaymentMedic();
            sql = "SELECT PM.i_PaymetId, PM.i_CategoryId, SP.v_Value1 AS CategoryName, SU.i_SystemUserId, " +
                  "SU.v_UserName, PM.i_TypePay, SP1.v_Value1 AS TypePayName, PM.r_PayPercentage, " +
                  "PM.i_InsertUserId, PM.d_InsertDate, PM.i_UpdateUserId, PM.d_UpdateDate FROM dbo.paymentmedic PM " +
                  "INNER JOIN dbo.component CP ON PM.i_CategoryId=CP.i_CategoryId " +
                  "INNER JOIN dbo.systemparameter SP ON CP.i_CategoryId=SP.i_ParameterId AND SP.i_GroupId=116 " +
                  "INNER JOIN dbo.systemparameter SP1 ON PM.i_TypePay=SP1.i_ParameterId AND SP1.i_GroupId=373 " +
                  "INNER JOIN dbo.systemuser SU ON PM.i_UserId=SU.i_SystemUserId " +
                  "where i_PaymetId= " + PaymetId;
            comando = new SqlCommand(sql, conexion.conectarsigesoft);
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                p = GetPP(lector);
            }
            lector.Close();
            conexion.closesigesoft();
            return p;
        }

        internal List<PaymentMedic> GetPayment()
        {
            conexion.opensigesoft();
            List<PaymentMedic> pay = new List<PaymentMedic>();
            string sql = "SELECT PM.i_PaymetId, PM.i_CategoryId, SP.v_Value1 AS CategoryName, SU.i_SystemUserId, " +
                         "SU.v_UserName, PM.i_TypePay, SP1.v_Value1 AS TypePayName, PM.r_PayPercentage, " +
                         "PM.i_InsertUserId, PM.d_InsertDate, PM.i_UpdateUserId, PM.d_UpdateDate FROM dbo.paymentmedic PM " +
                         "INNER JOIN dbo.component CP ON PM.i_CategoryId=CP.i_CategoryId " +
                         "INNER JOIN dbo.systemparameter SP ON CP.i_CategoryId=SP.i_ParameterId AND SP.i_GroupId=116 " +
                         "INNER JOIN dbo.systemparameter SP1 ON PM.i_TypePay=SP1.i_ParameterId AND SP1.i_GroupId=373 " +
                         "INNER JOIN dbo.systemuser SU ON PM.i_UserId=SU.i_SystemUserId  WHERE  PM.i_IsDeleted=0" +
                         "GROUP BY PM.i_PaymetId, PM.i_CategoryId, SP.v_Value1, SU.i_SystemUserId, " +
                         "SU.v_UserName, PM.i_TypePay, SP1.v_Value1, PM.r_PayPercentage, " +
                         "PM.i_InsertUserId, PM.d_InsertDate, PM.i_UpdateUserId, PM.d_UpdateDate";
            comando = new SqlCommand(sql, conexion.conectarsigesoft);
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                PaymentMedic p = new PaymentMedic();
                p = GetPP(lector);
                pay.Add(p);
            }
            lector.Close();
            conexion.closesigesoft();
            return pay;
        }

        private PaymentMedic GetPP(SqlDataReader lector)
        {
            PaymentMedic p = new PaymentMedic();
            p.i_PaymetId = Convert.ToInt32(lector.GetValue(0).ToString());
            p.i_CategoryId = Convert.ToInt32(lector.GetValue(1).ToString());
            p.CategoryName = lector.GetValue(2).ToString();
            p.i_UserId = Convert.ToInt32(lector.GetValue(3).ToString());
            p.v_UserName = lector.GetValue(4).ToString();
            p.i_TypePay = Convert.ToInt32(lector.GetValue(5).ToString());
            p.TypePayName = lector.GetValue(6).ToString();
            p.r_PayPercentage = float.Parse(lector.GetValue(7).ToString());
            p.i_InsertUserId = Convert.ToInt32(lector.GetValue(8).ToString());
            p.d_InsertDate = lector.GetValue(9).ToString();
            p.i_UpdateUserId = lector.GetValue(10).ToString() == "" ? 0 : Convert.ToInt32(lector.GetValue(10).ToString());
            p.d_UpdateDate = lector.GetValue(11).ToString() == "" ? "-" : lector.GetValue(11).ToString();
            return p;
        }

        internal void DeletePayment(int PaymetId)
        {
            sql = "UPDATE dbo.paymentmedic SET  i_IsDeleted= 1 WHERE i_PaymetId=" +PaymetId;
            Executequery(sql);
        }

        internal List<PaymentMedical_> getPaymentMedical(int user, string userPago, int pagado, int tipoPago, string wherefecha)
        {
            conexion.opensigesoft();
            List<PaymentMedical_> pays = new List<PaymentMedical_>();
            if (user != 0)
            {
                sql =
                "SELECT PP.v_FirstLastName+' '+PP.v_SecondLastName+', '+PP.v_FirstName , SC." + userPago + " as i_MedicoTratanteId, SC.i_InsertUserId, " +
                "CP.v_ComponentId , CP.v_Name, SC.v_ServiceComponentId, SR.v_ServiceId, SR.d_ServiceDate, PM1.i_TypePay, " +
                "SC.r_Price, PM1.r_PayPercentage, SC.i_PayMedic, PM.v_FirstLastName+' '+PM.v_SecondLastName+', '+PM.v_FirstName as Medico  FROM dbo.service SR " +
                "INNER JOIN dbo.servicecomponent SC ON SR.v_ServiceId=SC.v_ServiceId " +
                "INNER JOIN dbo.component CP ON SC.v_ComponentId=CP.v_ComponentId " +
                "INNER JOIN dbo.paymentmedic PM1 ON SC." + userPago + "=PM1.i_UserId " +
                "INNER JOIN dbo.person PP ON SR.v_PersonId=PP.v_PersonId  INNER JOIN dbo.systemuser SU ON SC." + userPago + " = SU.i_SystemUserId INNER JOIN dbo.person PM ON SU.v_PersonId = PM.v_PersonId " +
                "WHERE CP.i_CategoryId=PM1.i_CategoryId AND SC.i_IsRequiredId=1 AND SC.i_IsDeleted=0 AND SR.i_IsDeleted=0  " +
                "AND SC." + userPago + "=" + user + " AND SC.r_Price>0 AND PM1.i_IsDeleted=0 AND SC.i_PayMedic=" +
                pagado + " AND PM1.i_TypePay=" + tipoPago + " AND " + wherefecha +
                "GROUP BY PP.v_FirstLastName+' '+PP.v_SecondLastName+', '+PP.v_FirstName , SC." + userPago + ", SC.i_InsertUserId" + ", CP.v_ComponentId , CP.v_Name, " +
                "SC.v_ServiceComponentId, SR.v_ServiceId, SC.r_Price, PM1.r_PayPercentage, SC.i_PayMedic, SR.d_ServiceDate, PM1.i_TypePay , PM.v_FirstLastName+' '+PM.v_SecondLastName+', '+PM.v_FirstName";
                comando = new SqlCommand(sql, conexion.conectarsigesoft);
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    PaymentMedical_ pp = new PaymentMedical_();
                    pp.pacientName = lector.GetValue(0).ToString();
                    pp.i_MedicoTratanteId = lector.GetValue(1).ToString() == "" ? 0 : Convert.ToInt32(lector.GetValue(1).ToString());
                    pp.i_InsertUserId = Convert.ToInt32(lector.GetValue(2).ToString());
                    pp.v_ComponentId = lector.GetValue(3).ToString();
                    pp.v_ComponentName = lector.GetValue(4).ToString();
                    pp.v_serviceComponentId = lector.GetValue(5).ToString();
                    pp.v_ServiceId = lector.GetValue(6).ToString();
                    pp.d_ServiceDate = lector.GetValue(7).ToString();
                    pp.i_TypeAttention = Convert.ToInt32(lector.GetValue(8).ToString());
                    pp.r_Price = Convert.ToDecimal(lector.GetValue(9).ToString());
                    pp.r_PaymentPercentage = Convert.ToDecimal(lector.GetValue(10).ToString());
                    pp.i_PayMedic = Convert.ToInt32(lector.GetValue(11).ToString());
                    pp.Medico = lector.GetValue(12).ToString();
                    pays.Add(pp);
                }
                lector.Close();
                conexion.closesigesoft();
            }
            else
            {
                sql =
                "SELECT PP.v_FirstLastName+' '+PP.v_SecondLastName+', '+PP.v_FirstName , SC." + userPago + " as i_MedicoTratanteId, SC.i_InsertUserId, " +
                "CP.v_ComponentId , CP.v_Name, SC.v_ServiceComponentId, SR.v_ServiceId, SR.d_ServiceDate, PM1.i_TypePay, " +
                "SC.r_Price, PM1.r_PayPercentage, SC.i_PayMedic , PM.v_FirstLastName+' '+PM.v_SecondLastName+', '+PM.v_FirstName as Medico  FROM dbo.service SR " +
                "INNER JOIN dbo.servicecomponent SC ON SR.v_ServiceId=SC.v_ServiceId " +
                "INNER JOIN dbo.component CP ON SC.v_ComponentId=CP.v_ComponentId " +
                "INNER JOIN dbo.paymentmedic PM1 ON SC." + userPago + "=PM1.i_UserId " +
                "INNER JOIN dbo.person PP ON SR.v_PersonId=PP.v_PersonId INNER JOIN dbo.systemuser SU ON SC."+userPago+" = SU.i_SystemUserId INNER JOIN dbo.person PM ON SU.v_PersonId = PM.v_PersonId " +
                "WHERE CP.i_CategoryId=PM1.i_CategoryId AND SC.i_IsRequiredId=1 AND SC.i_IsDeleted=0 AND SR.i_IsDeleted=0  " +
                " AND SC.r_Price>0 AND PM1.i_IsDeleted=0 AND SC.i_PayMedic=" +
                pagado + " AND PM1.i_TypePay=" + tipoPago + " AND " + wherefecha +
                "GROUP BY PP.v_FirstLastName+' '+PP.v_SecondLastName+', '+PP.v_FirstName , SC." + userPago + ", SC.i_InsertUserId" + ", CP.v_ComponentId , CP.v_Name, " +
                "SC.v_ServiceComponentId, SR.v_ServiceId, SC.r_Price, PM1.r_PayPercentage, SC.i_PayMedic, SR.d_ServiceDate, PM1.i_TypePay , PM.v_FirstLastName+' '+PM.v_SecondLastName+', '+PM.v_FirstName";
                comando = new SqlCommand(sql, conexion.conectarsigesoft);
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    PaymentMedical_ pp = new PaymentMedical_();
                    pp.pacientName = lector.GetValue(0).ToString();
                    pp.i_MedicoTratanteId = lector.GetValue(1).ToString() == "" ? 0 : Convert.ToInt32(lector.GetValue(1).ToString());
                    pp.i_InsertUserId = Convert.ToInt32(lector.GetValue(2).ToString());
                    pp.v_ComponentId = lector.GetValue(3).ToString();
                    pp.v_ComponentName = lector.GetValue(4).ToString();
                    pp.v_serviceComponentId = lector.GetValue(5).ToString();
                    pp.v_ServiceId = lector.GetValue(6).ToString();
                    pp.d_ServiceDate = lector.GetValue(7).ToString();
                    pp.i_TypeAttention = Convert.ToInt32(lector.GetValue(8).ToString());
                    pp.r_Price = Convert.ToDecimal(lector.GetValue(9).ToString());
                    pp.r_PaymentPercentage = Convert.ToDecimal(lector.GetValue(10).ToString());
                    pp.i_PayMedic = Convert.ToInt32(lector.GetValue(11).ToString());
                    pp.Medico = lector.GetValue(12).ToString();
                    pays.Add(pp);
                }
                lector.Close();
                conexion.closesigesoft();
            }
            return pays;
        }

        internal void ActualizaPagado(string v_serviceComponentId)
        {
            sql = "UPDATE dbo.servicecomponent SET i_PayMedic=1 WHERE v_ServiceComponentId='"+v_serviceComponentId+"'";
            Executequery(sql);
        }

        internal List<PaymentMedical_> getFarmaciaPay(int user, int pagado, int tipoPago, string whereFecha)
        {
            conexion.opensigesoft();
            List<PaymentMedical_> pays = new List<PaymentMedical_>();
            sql =
                "SELECT PP.v_FirstLastName+' '+PP.v_SecondLastName+', '+PP.v_FirstName , RC.v_MedicoTratante,CP.v_ComponentId , CP.v_Name, " +
                "RC.v_ServiceId, SR.d_ServiceDate, PM.i_TypePay, RC.d_SaldoPaciente, RC.d_SaldoAseguradora,  " +
                "PM.r_PayPercentage, RC.i_PayMedic, RC.i_IdReceta FROM receta RC " +
                "INNER JOIN dbo.paymentmedic PM ON RC.v_MedicoTratante=PM.i_UserId " +
                "INNER JOIN service SR ON RC.v_ServiceId=SR.v_ServiceId " +
                "INNER JOIN person PP ON PP.v_PersonId = SR.v_PersonId " +
                "INNER JOIN dbo.diagnosticrepository DR ON DR.v_DiagnosticRepositoryId = RC.v_DiagnosticRepositoryId " +
                "INNER JOIN dbo.component CP ON CP.v_ComponentId = DR.v_ComponentId " +
                "WHERE PM.i_UserId = " + user + " AND PM.i_TypePay=" + tipoPago + " AND RC.i_PayMedic=" + pagado + " AND " + whereFecha;
            comando = new SqlCommand(sql, conexion.conectarsigesoft);
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                PaymentMedical_ pp = new PaymentMedical_();
                pp.pacientName = lector.GetValue(0).ToString();
                pp.i_MedicoTratanteId = lector.GetValue(1).ToString() == "" ? 0 : Convert.ToInt32(lector.GetValue(1).ToString());
                pp.i_InsertUserId = 0;
                pp.v_ComponentId = lector.GetValue(2).ToString();
                pp.v_ComponentName = lector.GetValue(3).ToString();
                pp.v_serviceComponentId = lector.GetValue(11).ToString();
                pp.v_ServiceId = lector.GetValue(4).ToString();
                pp.d_ServiceDate = lector.GetValue(5).ToString();
                pp.i_TypeAttention = Convert.ToInt32(lector.GetValue(6).ToString());
                decimal px = lector.GetValue(7).ToString() == "" ? 0 : Convert.ToDecimal(lector.GetValue(7).ToString());
                decimal ax = lector.GetValue(8).ToString() == "" ? 0 : Convert.ToDecimal(lector.GetValue(8).ToString());
                pp.r_Price = px + ax;
                pp.r_PaymentPercentage = Convert.ToDecimal(lector.GetValue(9).ToString());
                pp.i_PayMedic = Convert.ToInt32(lector.GetValue(10).ToString());
                pays.Add(pp);
            }
            lector.Close();
            conexion.closesigesoft();
            return pays;
        }

        internal string getQuota(int userId, int tipopago)
        {
            sql = "SELECT r_QuotaMonth FROM dbo.paymentmedic WHERE i_UserId="+userId+" AND i_TypePay="+tipopago;
            string quota = returnObj(sql).ToString();
            return quota;
        }
    }
}
