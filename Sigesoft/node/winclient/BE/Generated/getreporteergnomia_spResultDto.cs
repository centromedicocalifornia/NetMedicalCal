//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:08:09
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
    public partial class getreporteergnomia_spResultDto
    {
        [DataMember()]
        public String EmpresaCliente { get; set; }

        [DataMember()]
        public String Trabajador { get; set; }

        [DataMember()]
        public Nullable<DateTime> FechaNacimiento { get; set; }

        [DataMember()]
        public Nullable<DateTime> FechaEvaluacion { get; set; }

        [DataMember()]
        public String Puesto { get; set; }

        [DataMember()]
        public String ServicioId { get; set; }

        [DataMember()]
        public Byte[] FirmaUsuarioGraba { get; set; }

        [DataMember()]
        public String NombreUsuarioGraba { get; set; }

        [DataMember()]
        public Byte[] HuellaTrabajador { get; set; }

        [DataMember()]
        public Byte[] FirmaTrabajador { get; set; }

        [DataMember()]
        public String Dni { get; set; }

        [DataMember()]
        public String TipoEso { get; set; }

        public getreporteergnomia_spResultDto()
        {
        }

        public getreporteergnomia_spResultDto(String empresaCliente, String trabajador, Nullable<DateTime> fechaNacimiento, Nullable<DateTime> fechaEvaluacion, String puesto, String servicioId, Byte[] firmaUsuarioGraba, String nombreUsuarioGraba, Byte[] huellaTrabajador, Byte[] firmaTrabajador, String dni, String tipoEso)
        {
			this.EmpresaCliente = empresaCliente;
			this.Trabajador = trabajador;
			this.FechaNacimiento = fechaNacimiento;
			this.FechaEvaluacion = fechaEvaluacion;
			this.Puesto = puesto;
			this.ServicioId = servicioId;
			this.FirmaUsuarioGraba = firmaUsuarioGraba;
			this.NombreUsuarioGraba = nombreUsuarioGraba;
			this.HuellaTrabajador = huellaTrabajador;
			this.FirmaTrabajador = firmaTrabajador;
			this.Dni = dni;
			this.TipoEso = tipoEso;
        }
    }
}
