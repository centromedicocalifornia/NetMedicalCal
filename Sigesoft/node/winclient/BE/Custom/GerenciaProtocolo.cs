using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class GerenciaProtocolo
    {
        public string Servicio { get; set; }
        public string Paciente { get; set; }
        public string Protocolo { get; set; }
        public DateTime? Fecha { get; set; }
        public string Trabajador { get; set; }
        public string SRV { get; set; }
        public string DOC { get; set; }
        public decimal? Total { get; set; }
        public decimal? Saldo { get; set; }
        public DateTime? F_Comprobante { get; set; }
        public string Condicion { get; set; }
        public string Tipo { get; set; }
        public string Medico { get; set; }
        public string Procedencia { get; set; }
        public decimal? ProduccionServices { get; set; }
        public decimal? ProduccionHospi { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }

        public decimal Produccion { get; set; }
    }
}
