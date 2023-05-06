using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class ServiciosDetalle
    {
        public string Servicio { get; set; }
        public string Componente { get; set; }
        public string NombreComponente { get; set; }
        //debe ser decimal, se debe convertir
        public float Precio { get; set; }
        public string Segus { get; set; }
        public decimal SaldoPaciente { get; set; }
        public decimal SaldoSeguro { get; set; }
        public DateTime Fecha { get; set; }
        public int TipoId { get; set; }
        public string Tipo { get; set; }
        public string Grupo { get; set; }
        public string UsuarioInserta { get; set; }
        public int? i_conCargoA { get; set; }
        public decimal Cantidad { get; set; }
        

        public List<ServiciosDetalle_1> Lista1 { get; set; }

        public List<ServiciosDetalle_1> Lista2 { get; set; }
    }

    public class ServiciosDetalle_1
    {


        public string Servicio { get; set; }
        public string Componente { get; set; }
        public string NombreComponente { get; set; }
        //debe ser decimal, se debe convertir
        public decimal Precio { get; set; }
        public string Segus { get; set; }
        public decimal SaldoPaciente { get; set; }
        public decimal SaldoSeguro { get; set; }
        public DateTime Fecha { get; set; }
        public int TipoId { get; set; }
        public string Tipo { get; set; }
        public string Grupo { get; set; }
        public string UsuarioInserta { get; set; }
        public int? i_conCargoA { get; set; }
        public decimal Cantidad { get; set; }

        public List<ServiciosDetalle_2> Lista2 { get; set; }
    }

    public class ServiciosDetalle_2
    {
        public int? i_conCargoA { get; set; }

        public string Servicio { get; set; }
        public string Componente { get; set; }
        public string NombreComponente { get; set; }
        //debe ser decimal, se debe convertir
        public decimal Precio { get; set; }
        public string Segus { get; set; }
        public decimal SaldoPaciente { get; set; }
        public decimal SaldoSeguro { get; set; }
        public DateTime Fecha { get; set; }
        public int TipoId { get; set; }
        public string Tipo { get; set; }
        public string Grupo { get; set; }
        public string UsuarioInserta { get; set; }
        public decimal Cantidad { get; set; }

    }

}
