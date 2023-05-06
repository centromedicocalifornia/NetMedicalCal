using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class CotizacionocdetaillList
    {
        public string i_CotizacionIdOcDetalle { get; set; }

        public string i_CotizacionIdOc { get; set; }

        public string v_ComponentId { get; set; }
        public string v_Component { get; set; }
        public float r_Price { get; set; }

        public int i_EMOTypeD { get; set; }

        public string v_EMOTypeD { get; set; }

        public int i_IsDeleted { get; set; }

        public int i_InsertUserId { get; set; }

        public DateTime d_InsertDate { get; set; }

        public int i_UpdateUserId { get; set; }

        public DateTime d_UpdateDate { get; set; }

        public string DescripcionOtros { get; set; }
    }
}
