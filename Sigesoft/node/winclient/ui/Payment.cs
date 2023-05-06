using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.UI
{
    public class Payment
    {
        public string v_Payment_Id { get; set; }
        public int i_User_Id { get; set; }
        public string v_UserName { get; set; }
        public int i_TypeAttention { get; set; }
        public string v_AtencionName { get; set; }
        public decimal r_PaymentPercentage { get; set; }
        public int i_IsDeleted { get; set; }
        public int i_InsertUserId { get; set; }
        public DateTime d_InsertDate { get; set; }
        public int i_UpdateUserId { get; set; }
        public DateTime d_UpdateDate { get; set; }
    }
}
