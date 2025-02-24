//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:06:43
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    [DataContract()]
    public partial class gerenciafarmaciasegurosResultDto
    {
        [DataMember()]
        public String Servicio { get; set; }

        [DataMember()]
        public String Medicina { get; set; }

        [DataMember()]
        public Nullable<Int32> Cantidad { get; set; }

        [DataMember()]
        public Nullable<Decimal> SubTotal { get; set; }

        [DataMember()]
        public Nullable<Decimal> Igv { get; set; }

        [DataMember()]
        public Nullable<Decimal> Total { get; set; }

        [DataMember()]
        public Nullable<Decimal> Comision { get; set; }

        [DataMember()]
        public Nullable<Decimal> SaldoPaciente { get; set; }

        [DataMember()]
        public Nullable<Decimal> SaldoAseguradora { get; set; }

        [DataMember()]
        public String Medico { get; set; }

        [DataMember()]
        public String Protocolo { get; set; }

        [DataMember()]
        public String Trabajador { get; set; }

        [DataMember()]
        public String Paciente { get; set; }

        [DataMember()]
        public Nullable<DateTime> Fecha { get; set; }

        [DataMember()]
        public String Plan { get; set; }

        [DataMember()]
        public Nullable<Double> Factor { get; set; }

        [DataMember()]
        public String Descuento_PPS { get; set; }

        [DataMember()]
        public String Deducible { get; set; }

        [DataMember()]
        public String Coaseguro { get; set; }

        [DataMember()]
        public String Value1 { get; set; }

        [DataMember()]
        public String Value2 { get; set; }

        public gerenciafarmaciasegurosResultDto()
        {
        }

        public gerenciafarmaciasegurosResultDto(String servicio, String medicina, Nullable<Int32> cantidad, Nullable<Decimal> subTotal, Nullable<Decimal> igv, Nullable<Decimal> total, Nullable<Decimal> comision, Nullable<Decimal> saldoPaciente, Nullable<Decimal> saldoAseguradora, String medico, String protocolo, String trabajador, String paciente, Nullable<DateTime> fecha, String plan, Nullable<Double> factor, String descuento_PPS, String deducible, String coaseguro, String value1, String value2)
        {
			this.Servicio = servicio;
			this.Medicina = medicina;
			this.Cantidad = cantidad;
			this.SubTotal = subTotal;
			this.Igv = igv;
			this.Total = total;
			this.Comision = comision;
			this.SaldoPaciente = saldoPaciente;
			this.SaldoAseguradora = saldoAseguradora;
			this.Medico = medico;
			this.Protocolo = protocolo;
			this.Trabajador = trabajador;
			this.Paciente = paciente;
			this.Fecha = fecha;
			this.Plan = plan;
			this.Factor = factor;
			this.Descuento_PPS = descuento_PPS;
			this.Deducible = deducible;
			this.Coaseguro = coaseguro;
			this.Value1 = value1;
			this.Value2 = value2;
        }
    }
}
