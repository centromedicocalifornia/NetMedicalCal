//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:06:32
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
    public partial class buscartickets_1ResultDto
    {
        [DataMember()]
        public String v_ServiceId { get; set; }

        [DataMember()]
        public String v_TicketId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_Fecha { get; set; }

        [DataMember()]
        public Nullable<Int32> i_conCargoA { get; set; }

        [DataMember()]
        public Nullable<Int32> i_tipoCuenta { get; set; }

        [DataMember()]
        public Nullable<Int32> i_TicketInterno { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_FechaAlta { get; set; }

        public buscartickets_1ResultDto()
        {
        }

        public buscartickets_1ResultDto(String v_ServiceId, String v_TicketId, Nullable<DateTime> d_Fecha, Nullable<Int32> i_conCargoA, Nullable<Int32> i_tipoCuenta, Nullable<Int32> i_TicketInterno, Nullable<DateTime> d_FechaAlta)
        {
			this.v_ServiceId = v_ServiceId;
			this.v_TicketId = v_TicketId;
			this.d_Fecha = d_Fecha;
			this.i_conCargoA = i_conCargoA;
			this.i_tipoCuenta = i_tipoCuenta;
			this.i_TicketInterno = i_TicketInterno;
			this.d_FechaAlta = d_FechaAlta;
        }
    }
}
