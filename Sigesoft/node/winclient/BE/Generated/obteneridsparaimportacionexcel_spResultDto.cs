//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:10:24
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
    public partial class obteneridsparaimportacionexcel_spResultDto
    {
        [DataMember()]
        public String ServicioId { get; set; }

        [DataMember()]
        public String ServicioComponentId { get; set; }

        [DataMember()]
        public String ComponentId { get; set; }

        [DataMember()]
        public String PersonId { get; set; }

        [DataMember()]
        public String Paciente { get; set; }

        [DataMember()]
        public String DNI { get; set; }

        [DataMember()]
        public Nullable<Int32> CategoriaId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UIIndex { get; set; }

        public obteneridsparaimportacionexcel_spResultDto()
        {
        }

        public obteneridsparaimportacionexcel_spResultDto(String servicioId, String servicioComponentId, String componentId, String personId, String paciente, String dNI, Nullable<Int32> categoriaId, Nullable<Int32> i_UIIndex)
        {
			this.ServicioId = servicioId;
			this.ServicioComponentId = servicioComponentId;
			this.ComponentId = componentId;
			this.PersonId = personId;
			this.Paciente = paciente;
			this.DNI = dNI;
			this.CategoriaId = categoriaId;
			this.i_UIIndex = i_UIIndex;
        }
    }
}
