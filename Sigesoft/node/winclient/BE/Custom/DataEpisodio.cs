using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class DataEpisodio
    {
        public string CustomerOrganizationName { get; set; }
        public string CustomerOrganizationRuc { get; set; }
        public string WorkingOrganizationName { get; set; }
        public string WorkingOrganizationRuc { get; set; }
        public string Unidad { get; set; }
        public string UnidadCodigo { get; set; }
        public string Occupation { get; set; }
        public string GradoInstruccion { get; set; }
        public string ZonaTrabajo { get; set; }
        public string AreaTrabajo { get; set; }
        public string TipoExamen { get; set; }
        public string TipoTarea { get; set; }
        public string Observaciones { get; set; }
        public DateTime? FechaExamen { get; set; }
        public DateTime? Vigencia { get; set; }//Caducidad - FechaExamen
        public DateTime? Caducidad { get; set; }
    }
}
