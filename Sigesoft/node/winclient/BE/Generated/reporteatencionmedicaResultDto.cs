//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:10:34
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
    public partial class reporteatencionmedicaResultDto
    {
        [DataMember()]
        public String Trabajador { get; set; }

        [DataMember()]
        public Nullable<DateTime> FechaServicio { get; set; }

        [DataMember()]
        public String Producto { get; set; }

        [DataMember()]
        public Nullable<Single> CantidadRecetada { get; set; }

        [DataMember()]
        public Nullable<Single> CantidadVendida { get; set; }

        [DataMember()]
        public Nullable<Decimal> PrecioVenta { get; set; }

        [DataMember()]
        public Nullable<Single> Importe { get; set; }

        [DataMember()]
        public String TipoDocVenta { get; set; }

        [DataMember()]
        public String NroDocVenta { get; set; }

        [DataMember()]
        public String CondicionPago { get; set; }

        [DataMember()]
        public String Vendedor { get; set; }

        [DataMember()]
        public Nullable<Int32> ProfesionalID { get; set; }

        [DataMember()]
        public String Profesional { get; set; }

        public reporteatencionmedicaResultDto()
        {
        }

        public reporteatencionmedicaResultDto(String trabajador, Nullable<DateTime> fechaServicio, String producto, Nullable<Single> cantidadRecetada, Nullable<Single> cantidadVendida, Nullable<Decimal> precioVenta, Nullable<Single> importe, String tipoDocVenta, String nroDocVenta, String condicionPago, String vendedor, Nullable<Int32> profesionalID, String profesional)
        {
			this.Trabajador = trabajador;
			this.FechaServicio = fechaServicio;
			this.Producto = producto;
			this.CantidadRecetada = cantidadRecetada;
			this.CantidadVendida = cantidadVendida;
			this.PrecioVenta = precioVenta;
			this.Importe = importe;
			this.TipoDocVenta = tipoDocVenta;
			this.NroDocVenta = nroDocVenta;
			this.CondicionPago = condicionPago;
			this.Vendedor = vendedor;
			this.ProfesionalID = profesionalID;
			this.Profesional = profesional;
        }
    }
}
