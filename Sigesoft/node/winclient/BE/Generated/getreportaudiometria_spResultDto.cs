//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:08:04
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
    public partial class getreportaudiometria_spResultDto
    {
        [DataMember()]
        public String v_PersonId { get; set; }

        [DataMember()]
        public String v_FullPersonName { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_BirthDate { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_ServiceDate { get; set; }

        [DataMember()]
        public String v_DocNumber { get; set; }

        [DataMember()]
        public Nullable<Int32> i_SexTypeId { get; set; }

        [DataMember()]
        public Byte[] FirmaTecnologo { get; set; }

        [DataMember()]
        public Byte[] FirmaMedico { get; set; }

        [DataMember()]
        public String Puesto { get; set; }

        [DataMember()]
        public String v_SexType { get; set; }

        [DataMember()]
        public String v_EsoTypeName { get; set; }

        [DataMember()]
        public String v_ServiceComponentId { get; set; }

        [DataMember()]
        public String v_CustomerOrganizationName { get; set; }

        [DataMember()]
        public String v_EmployerOrganizationName { get; set; }

        [DataMember()]
        public String EmpresaPropietaria { get; set; }

        [DataMember()]
        public String EmpresaPropietariaDireccion { get; set; }

        [DataMember()]
        public Byte[] FirmaTrabajador { get; set; }

        [DataMember()]
        public Byte[] HuellaTrabajador { get; set; }

        [DataMember()]
        public String NombreUsuarioGraba { get; set; }

        [DataMember()]
        public String Departamento { get; set; }

        [DataMember()]
        public String Provincia { get; set; }

        [DataMember()]
        public String Distrito { get; set; }

        [DataMember()]
        public String DireccionPaciente { get; set; }

        [DataMember()]
        public String Telefono { get; set; }

        public getreportaudiometria_spResultDto()
        {
        }

        public getreportaudiometria_spResultDto(String v_PersonId, String v_FullPersonName, Nullable<DateTime> d_BirthDate, Nullable<DateTime> d_ServiceDate, String v_DocNumber, Nullable<Int32> i_SexTypeId, Byte[] firmaTecnologo, Byte[] firmaMedico, String puesto, String v_SexType, String v_EsoTypeName, String v_ServiceComponentId, String v_CustomerOrganizationName, String v_EmployerOrganizationName, String empresaPropietaria, String empresaPropietariaDireccion, Byte[] firmaTrabajador, Byte[] huellaTrabajador, String nombreUsuarioGraba, String departamento, String provincia, String distrito, String direccionPaciente, String telefono)
        {
			this.v_PersonId = v_PersonId;
			this.v_FullPersonName = v_FullPersonName;
			this.d_BirthDate = d_BirthDate;
			this.d_ServiceDate = d_ServiceDate;
			this.v_DocNumber = v_DocNumber;
			this.i_SexTypeId = i_SexTypeId;
			this.FirmaTecnologo = firmaTecnologo;
			this.FirmaMedico = firmaMedico;
			this.Puesto = puesto;
			this.v_SexType = v_SexType;
			this.v_EsoTypeName = v_EsoTypeName;
			this.v_ServiceComponentId = v_ServiceComponentId;
			this.v_CustomerOrganizationName = v_CustomerOrganizationName;
			this.v_EmployerOrganizationName = v_EmployerOrganizationName;
			this.EmpresaPropietaria = empresaPropietaria;
			this.EmpresaPropietariaDireccion = empresaPropietariaDireccion;
			this.FirmaTrabajador = firmaTrabajador;
			this.HuellaTrabajador = huellaTrabajador;
			this.NombreUsuarioGraba = nombreUsuarioGraba;
			this.Departamento = departamento;
			this.Provincia = provincia;
			this.Distrito = distrito;
			this.DireccionPaciente = direccionPaciente;
			this.Telefono = telefono;
        }
    }
}
