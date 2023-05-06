using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class HospitalizacionCustom
    {
        public DateTime d_FechaAlta { get; set; }
        public DateTime d_FechaIngreso { get; set; }
        public string v_Habitacion { get; set; }
        public decimal d_Precio { get; set; }

        public decimal SaldoPaciente { get; set; }
        public decimal SaldoAseguradora { get; set; }
        public int Dias { get; set; }
        public string Hospitalizacion { get; set; }
    }
}
