using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class ServiceListComprobante
    {
        public string v_PersonId { get; set; }
        public string v_ServiceId { get; set; }
        public string v_Pacient { get; set; }
        public string v_PacientDocument { get; set; }
        public DateTime? d_ServiceDate { get; set; }
        public int? i_ServiceStatusId { get; set; }
        public int? i_StatusLiquidation { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }
        public int? i_MasterServiceId { get; set; }
        public int? i_ServiceTypeId { get; set; }
        public int? i_EsoTypeId { get; set; }
        public string v_ProtocolId { get; set; }
        public string v_ProtocolName { get; set; }
        public string CompMinera { get; set; }
        public string Tercero { get; set; }
        public string v_OrganizationName { get; set; }
        public int? i_ServiceId { get; set; }
        public string v_AptitudeStatusName { get; set; }
        public string UsuarioCrea { get; set; }
        public int? i_AptitudeStatusId { get; set; }

        public string TipoServicio { get; set; }
        public string TipoServicioMaster { get; set; }
        public string TipoServicioESO { get; set; }
        public string Fecha { get; set; }
        public string v_ComprobantePago { get; set; }

        public float Costo { get; set; }
        
    }
}
