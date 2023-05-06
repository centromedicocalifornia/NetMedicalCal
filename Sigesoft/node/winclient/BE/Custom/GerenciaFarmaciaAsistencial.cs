using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class GerenciaFarmaciaAsistencial
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
        public List<GerenciaFarmaciaAsistencialLista> ListaFarmacia { get; set; }
        public int N_Med { get; set; }
        public decimal? TotalMedicinas { get; set; }
    }
    public class GerenciaFarmaciaAsistencialLista
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
    } 
}
