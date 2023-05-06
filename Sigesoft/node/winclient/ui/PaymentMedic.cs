using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.UI
{
    public class PaymentMedic
    {
        public int i_PaymetId { get; set; }
        public int i_CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int i_UserId { get; set; }
        public string v_UserName { get; set; }
        public int i_TypePay { get; set; }
        public string TypePayName { get; set; }
        public float r_PayPercentage { get; set; }
        public float r_QuotaMonth { get; set; }
        public int i_InsertUserId { get; set; }
        public string d_InsertDate { get; set; }
        public int i_UpdateUserId { get; set; }
        public string d_UpdateDate { get; set; }
    }
}
