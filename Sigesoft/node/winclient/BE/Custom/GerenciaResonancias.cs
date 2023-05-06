using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class GerenciaResonancias
    {
        public string Servicio { get; set; }
        public string Paciente { get; set; }
        public DateTime F_Servicio { get; set; }
        public string Identificador { get; set; }
        public string Resonancia { get; set; }
        public DateTime F_Resonancia { get; set; }
        public decimal Precio { get; set; }
        public decimal Saldo_Paciente { get; set; }
        public decimal SaldoAseguradora { get; set; }
        public string Medico { get; set; }
        public string Protocolo { get; set; }
        public string Plan { get; set; }
        public string Procedencia { get; set; }
        public decimal Factor { get; set; }
        public string Descuento_PPS { get; set; }
        public string Deducible { get; set; }
        public string Coaseguro { get; set; }
        public string Trabajador { get; set; }
        public string Tipo { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public decimal PagosMedicos { get; set; }
        public decimal Comision { get; set; }

        public decimal Total_Reonancias { get; set; }

        public List<GerenciaResonancias> ListaResonancias { get; set; }
    }
}
