using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class GerenciaTreeProcedenciaAsistencial
    {
        public string Agrupador { get; set; }
        public decimal? TotalVenta { get; set; }
        public decimal? TotalProduccion { get; set; }
        public int Cantidad { get; set; }
        public List<PerfilProcedencia> Perfiles { get; set; }
    }

    public class PerfilProcedencia
    {
        public int Cantidad { get; set; }
        public string TipoEso { get; set; }
        public decimal? TotalVenta { get; set; }
        public decimal? TotalProduccionn { get; set; }
        public List<TipoPago> Empresas { get; set; }
    }

    public class TipoPago
    {
        public string TipoEso { get; set; }
        public int Cantidad { get; set; }
        public string Nombre { get; set; }
        public decimal? Total { get; set; }
    }
}
