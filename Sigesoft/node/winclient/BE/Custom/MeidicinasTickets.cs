using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class MeidicinasTickets
    {
        public string Tipo { get; set; }
        public decimal Total { get; set; }
        public int Cantidad { get; set; }
        public int Medicinas { get; set; }
    }

    public class MeidicinasTicketsLista
    {
        public string Tipo { get; set; }
        public string Ticket { get; set; }
        public string Servicio { get; set; }
        public DateTime Fecha { get; set; }
        public string Medicina { get; set; }
        public int Cantidad { get; set; }
        public decimal P_Unitario { get; set; }
        public decimal P_Venta { get; set; }
        public decimal SaldoPaciente { get; set; }
        public decimal SaldoSeguro { get; set; }
        public string UserCrea { get; set; }
        public int? i_conCargoA { get; set; }
        public List<MeidicinasTicketsLista_1> Lista_1 { get; set; }
        //
        public decimal TotalVenta { get; set; }
        public decimal TotalSaldoPaciente { get; set; }
        public decimal TotalSaldoSeguro { get; set; }

        public int CantidadMedicinas { get; set; }
    }

    public class MeidicinasTicketsLista_1
    {
        public string Tipo { get; set; }
        public string Ticket { get; set; }
        public string Servicio { get; set; }
        public DateTime Fecha { get; set; }
        public int? i_conCargoA { get; set; }
        //public string Medicina { get; set; }
        //public int Cantidad { get; set; }
        //public decimal P_Unitario { get; set; }
        //public decimal P_Venta { get; set; }
        //public decimal SaldoPaciente { get; set; }
        //public decimal SaldoSeguro { get; set; }
        public string UserCrea { get; set; }
        public List<MeidicinasTicketsLista_2> Lista_2 { get; set; }
        //
        public decimal TotalVenta_TK { get; set; }
        public decimal TotalSaldoPaciente_TK { get; set; }
        public decimal TotalSaldoSeguro_TK { get; set; }

        public int CantidadMedicinas_TK { get; set; }
    }

    public class MeidicinasTicketsLista_2
    {
        //public string Tipo { get; set; }
        //public string Ticket { get; set; }
        //public string Servicio { get; set; }
        //public DateTime Fecha { get; set; }
        public string Medicina { get; set; }
        public int Cantidad { get; set; }
        public decimal P_Unitario { get; set; }
        public decimal P_Venta { get; set; }
        public decimal SaldoPaciente { get; set; }
        public decimal SaldoSeguro { get; set; }
        public string UserCrea { get; set; }
        public int? i_conCargoA { get; set; }
        ////
        //public decimal TotalVenta_TK { get; set; }
        //public decimal TotalSaldoPaciente_TK { get; set; }
        //public decimal TotalSaldoSeguro_TK { get; set; }

        //public int CantidadMedicinas_TK { get; set; }
    }

}
