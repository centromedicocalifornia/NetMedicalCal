using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class HistorialAtencionesPac
    {
        public string Servicio { get; set; }
        public string Paciente { get; set; }
        public string Protocolo { get; set; }
        public DateTime Atencion { get; set; }
        public string Historia { get; set; }
        public string Interconsultas { get; set; }
        public string Recetas { get; set; }
        public string Ordenes { get; set; }
        public string Anexos { get; set; }
    }
    public class ListaArchivosPac
    {
        public string Archivo { get; set; }
        public string Ruta { get; set; }
        public string Origen { get; set; }
    }
}
