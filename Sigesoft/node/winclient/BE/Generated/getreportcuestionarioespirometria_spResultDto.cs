//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:08:07
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
    public partial class getreportcuestionarioespirometria_spResultDto
    {
        [DataMember()]
        public String IdServicio { get; set; }

        [DataMember()]
        public String ServiceComponentId { get; set; }

        [DataMember()]
        public Nullable<DateTime> Fecha { get; set; }

        [DataMember()]
        public String NombreTrabajador { get; set; }

        [DataMember()]
        public Nullable<DateTime> FechaNacimineto { get; set; }

        [DataMember()]
        public Nullable<Int32> Genero { get; set; }

        [DataMember()]
        public Byte[] FirmaTrabajador { get; set; }

        [DataMember()]
        public Byte[] HuellaTrabajador { get; set; }

        [DataMember()]
        public Byte[] b_File { get; set; }

        [DataMember()]
        public String NombreUsuarioGraba { get; set; }

        [DataMember()]
        public String Dni { get; set; }

        [DataMember()]
        public String TipoExamen { get; set; }

        [DataMember()]
        public Nullable<Int32> TipoEso { get; set; }

        [DataMember()]
        public String RazonSocial { get; set; }

        [DataMember()]
        public String ActividadEconomica { get; set; }

        [DataMember()]
        public String PuestoTrabajo { get; set; }

        [DataMember()]
        public Byte[] LogoCliente { get; set; }

        [DataMember()]
        public String EmpresaCliente { get; set; }

        [DataMember()]
        public String EmpresaContratista { get; set; }

        [DataMember()]
        public String EmpresaPropietaria { get; set; }

        [DataMember()]
        public String EmpresaPropietariaDireccion { get; set; }

        public getreportcuestionarioespirometria_spResultDto()
        {
        }

        public getreportcuestionarioespirometria_spResultDto(String idServicio, String serviceComponentId, Nullable<DateTime> fecha, String nombreTrabajador, Nullable<DateTime> fechaNacimineto, Nullable<Int32> genero, Byte[] firmaTrabajador, Byte[] huellaTrabajador, Byte[] b_File, String nombreUsuarioGraba, String dni, String tipoExamen, Nullable<Int32> tipoEso, String razonSocial, String actividadEconomica, String puestoTrabajo, Byte[] logoCliente, String empresaCliente, String empresaContratista, String empresaPropietaria, String empresaPropietariaDireccion)
        {
			this.IdServicio = idServicio;
			this.ServiceComponentId = serviceComponentId;
			this.Fecha = fecha;
			this.NombreTrabajador = nombreTrabajador;
			this.FechaNacimineto = fechaNacimineto;
			this.Genero = genero;
			this.FirmaTrabajador = firmaTrabajador;
			this.HuellaTrabajador = huellaTrabajador;
			this.b_File = b_File;
			this.NombreUsuarioGraba = nombreUsuarioGraba;
			this.Dni = dni;
			this.TipoExamen = tipoExamen;
			this.TipoEso = tipoEso;
			this.RazonSocial = razonSocial;
			this.ActividadEconomica = actividadEconomica;
			this.PuestoTrabajo = puestoTrabajo;
			this.LogoCliente = logoCliente;
			this.EmpresaCliente = empresaCliente;
			this.EmpresaContratista = empresaContratista;
			this.EmpresaPropietaria = empresaPropietaria;
			this.EmpresaPropietariaDireccion = empresaPropietariaDireccion;
        }
    }
}
