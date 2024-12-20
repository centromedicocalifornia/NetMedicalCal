using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class DatosSeguuro
    {
        public string Servicio { get; set; }
        public string Solicitud { get; set; }
        public DateTime FechaServicio { get; set; }
        public string Plan { get; set; }
        public decimal Factor { get; set; }
        public decimal DescuentoPPS { get; set; }
        public decimal Deducible { get; set; }
        public decimal Coaseguro { get; set; }
        public decimal Habitacion { get; set; }
        public string Protocolo { get; set; }
        public string Medico { get; set; }

        public decimal CoaseguroFarGen { get; set; }
        public decimal CoaseguroFarCom { get; set; }

        public decimal Insumos { get; set; }

        public string ConvenioFijado { get; set; }

        public decimal ConvenioFijadoCosto { get; set; }


    }
}
