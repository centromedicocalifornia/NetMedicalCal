using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class LiquidacionAseguradora
    {
        public string ServicioId { get; set; }
        public DateTime FechaServicio { get; set; }
        public string PersonId { get; set; }
        public string Paciente { get; set; }
        public string EmpresaId { get; set; }
        public string PacientDocument { get; set; }
        public string Aseguradora { get; set; }
        public string Protocolo { get; set; }
        public decimal? TotalAseguradora { get; set; }
        public double? Factor_ { get; set; }
        public decimal Factor { get; set; }
        public double? PPS_ { get; set; }
        public string PPS { get; set; }

        public List<LiquiAseguradoraDetalle> Detalle { get; set; }
        //NUEVO
    }
    public class LiquidacionesSeguro
    {
        public string ServicioId { get; set; }
        public string Paciente { get; set; }
        public string Protocolo { get; set; }
        public DateTime Fecha { get; set; }
        public string Trabajador { get; set; }
        public string SRV { get; set; }
        public string DOC { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
        public string F_Comprobante { get; set; }
        public string Condicion { get; set; }
        public string Tipo { get; set; }
        public string Medico { get; set; }
        public string Procedencia { get; set; }
        public string Plan { get; set; }
        public decimal Factor { get; set; }
        public string Descuento_PPS { get; set; }
        public string Deducible { get; set; }
        public string Coaseguro { get; set; }
        public string Liquidacion { get; set; }
        public string DOC_L { get; set; }
        public decimal Total_L { get; set; }
        public decimal Saldo_L { get; set; }
        public string F_Comprobante_L { get; set; }
        public string F_Venc_Comprobante_L { get; set; }
        public string Condicion_L { get; set; }
        public string Tipo_L { get; set; }
        public string Aseguradora { get; set; }

        public string USUARIO_MED { get; set; }
        public string ESPECIALIDAD_MED { get; set; }
    }
}
