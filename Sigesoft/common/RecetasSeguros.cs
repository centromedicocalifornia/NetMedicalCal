using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Common
{
    public class RecetasSeguros
    {
        public string Receta { get; set; }

        public string Servicio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public decimal SaldoPaciente { get; set; }
        public decimal SaldoAseguradora { get; set; }
        public string Producto { get; set; }
    }

    public class RecetasDetalle
    {
        public string Receta { get; set; }

        public string Servicio { get; set; }
        public string Medicina { get; set; }
        public int Cantidad { get; set; }
        public decimal P_Unitario { get; set; }
        public decimal SaldoPaciente { get; set; }
        public decimal SaldoAseguradora { get; set; }
        public decimal Total { get; set; }
        public string Medico { get; set; }
        public string Trabajador { get; set; }
        public DateTime Fecha { get; set; }
        public List<RecetasDetalle> Lista { get; set; }
    }

    //public class RecetasSegurosDetalle_1
    //{
    //    public string Receta { get; set; }

    //    public string Servicio { get; set; }
    //    public string Medicina { get; set; }
    //    public int Cantidad { get; set; }
    //    public decimal SaldoPaciente { get; set; }
    //    public decimal SaldoAseguradora { get; set; }
    //    public decimal Total { get; set; }
    //    public string Medico { get; set; }
    //    public string Trabajador { get; set; }
    //    public decimal Fecha { get; set; }
    //}
}
