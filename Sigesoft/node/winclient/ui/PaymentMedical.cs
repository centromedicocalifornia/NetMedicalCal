using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.UI
{
    public class PaymentMedical
    {
        public string pacientName { get; set; }
        public int i_MedicoTratanteId { get; set; }
        public int i_InsertUserId { get; set; }
        public string v_ComponentId { get; set; }
        public string v_ComponentName { get; set; }
        public string v_serviceComponentId { get; set; }
        public string v_ServiceId { get; set; }
        public string d_ServiceDate { get; set; }
        public decimal r_Price { get; set; }
        public int i_TypeAttention { get; set; }
        public string tipoAtx { get; set; }
        public decimal r_PaymentPercentage { get; set; }
        public decimal r_subTotal { get; set; }
        public int i_PayMedic { get; set; }
        public string Medico { get; set; }
    }
}
