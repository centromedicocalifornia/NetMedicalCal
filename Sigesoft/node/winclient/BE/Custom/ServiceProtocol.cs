using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class ServiceProtocol
    {
        public string ServicioId { get; set; }
        public string PersonId { get; set; }
        public string ProtocoloId { get; set; }
        public string Protocolo { get; set; }
        public int? ESOId { get; set; }
        public string ESOName { get; set; }
        public int? MasterServiceTipoID { get; set; }
        public string MasterServiceTipoName { get; set; }
        public int? MasterServiceID { get; set; }
        public string MasterServiceName { get; set; }
        public int? AptitudStatusId { get; set; }
        public string AptitudStatusName { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
