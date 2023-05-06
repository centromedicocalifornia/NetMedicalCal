using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.UI
{
    public class PaymentDetail
    {
        public string v_PaymentDetail_Id { get; set; }
        public string v_Payment_Id { get; set; }
        public string v_ComponentId { get; set; }
        public string v_ComponentName { get; set; }
        public string i_CategoryId { get; set; }
        public string v_CategoryName { get; set; }
        public int i_IsDeleted { get; set; }
        public int i_InsertUserId { get; set; }
        public DateTime d_InsertDate { get; set; }
        public int i_UpdateUserId { get; set; }
        public DateTime d_UpdateDate { get; set; }
    }
}
