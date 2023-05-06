using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class GerenciaTreeTipoProtocolo
    {
        public string Agrupador { get; set; }
        public decimal? TotalVenta { get; set; }
        public decimal? TotalProduccion { get; set; }
        public int Cantidad { get; set; }
        public List<Perfil_1> Perfiles { get; set; }
    }

    public class Perfil_1
    {
        public int Cantidad { get; set; }
        public string TipoEso { get; set; }
        public decimal? TotalVenta { get; set; }
        public decimal? TotalProduccionn { get; set; }
        public List<EmpresaTipoEso_1> Empresas { get; set; }
    }

    public class EmpresaTipoEso_1
    {
        public string TipoEso { get; set; }
        public int Cantidad { get; set; }
        public string Nombre { get; set; }
        public decimal? Total { get; set; }
    }

}
