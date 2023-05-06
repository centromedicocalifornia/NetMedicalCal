using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class GerenciaFarmaciaSeguros
    {
        public string Medico { get; set; }
        public string TotalComision { get; set; }
        public string Servicio { get; set; }
        public string Medicina { get; set; }
        public int? Cantidad { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Igv { get; set; }
        public decimal? Total { get; set; }
        public decimal? Comision { get; set; }
        public string Protocolo { get; set; }
        public string Trabajador { get; set; }
        public string Paciente { get; set; }
        public DateTime FechaReceta { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public decimal SaldoPaciente { get; set; }
        public decimal SaldoAseguradora { get; set; }
        public string Plan { get; set; }
        public decimal Factor { get; set; }
        public string Descuento_PPS { get; set; }
        public string Deducible { get; set; }
        public string Coaseguro { get; set; }
        public List<GerenciaFarmaciaSegurosLista> ListaFarmaciaSeguros { get; set; }

        public int N_Med { get; set; }
        public decimal? TotalMedicinas { get; set; }
    }

    public class GerenciaFarmaciaSegurosLista
    {
        public string Servicio { get; set; }
        public string Medicina { get; set; }
        public int? Cantidad { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Igv { get; set; }
        public decimal? Total { get; set; }
        public decimal? Comision { get; set; }
        public string Protocolo { get; set; }
        public string Trabajador { get; set; }
        public string Paciente { get; set; }
        public DateTime FechaReceta { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public decimal SaldoPaciente { get; set; }
        public decimal SaldoAseguradora { get; set; }
        public string Plan { get; set; }
        public decimal Factor { get; set; }
        public string Descuento_PPS { get; set; }
        public string Deducible { get; set; }
        public string Coaseguro { get; set; }
    } 
}
