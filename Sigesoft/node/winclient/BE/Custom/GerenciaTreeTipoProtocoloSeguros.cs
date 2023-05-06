using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class GerenciaTreeTipoProtocoloSeguros
    {
        public string Agrupador { get; set; }
        public decimal? Total { get; set; }
        public int Cantidad { get; set; }
        public List<ProtocoloSeguros_> Perfiles { get; set; }
    }

    public class ProtocoloSeguros_
    {
        public int Cantidad { get; set; }
        public string TipoEso { get; set; }
        public decimal? Total { get; set; }
        public List<Tipo_Perfil_Seguro> Tipo_Perfil_Seguro { get; set; }
    }

    public class Tipo_Perfil_Seguro
    {
        public string TipoEso { get; set; }
        public int Cantidad { get; set; }
        public string Nombre { get; set; }
        public decimal? Total { get; set; }
    }

}
