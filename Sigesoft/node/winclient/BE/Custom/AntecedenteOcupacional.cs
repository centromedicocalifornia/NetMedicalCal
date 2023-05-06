using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class AntecedenteOcupacional
    {
        public DateTime? Fecha { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public string Comentarios { get; set; }
        public string CIE10 { get; set; }
        public string LugarTto { get; set; }
    }
}
