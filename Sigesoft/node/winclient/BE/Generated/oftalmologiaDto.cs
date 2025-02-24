//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:13:26
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
    public partial class oftalmologiaDto
    {
        [DataMember()]
        public String v_OftalmologiaId { get; set; }

        [DataMember()]
        public String v_ServicioId { get; set; }

        [DataMember()]
        public String v_vcscOD { get; set; }

        [DataMember()]
        public String v_vcscOI { get; set; }

        [DataMember()]
        public String v_vcccOD { get; set; }

        [DataMember()]
        public String v_vcccOI { get; set; }

        [DataMember()]
        public String v_vlscOD { get; set; }

        [DataMember()]
        public String v_vlscOI { get; set; }

        [DataMember()]
        public String v_vlccOD { get; set; }

        [DataMember()]
        public String v_vlccOI { get; set; }

        [DataMember()]
        public String v_VisionColores { get; set; }

        [DataMember()]
        public String v_EnfermedadesOculares { get; set; }

        [DataMember()]
        public String v_ReflejosPupilares { get; set; }

        [DataMember()]
        public String v_ConclusionesOftalmologia { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_FechaInsercion { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UsuarioInsertaId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_FechaActualizacion { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UsuarioActualizaId { get; set; }

        [DataMember()]
        public Nullable<Boolean> i_EsEliminado { get; set; }

        [DataMember()]
        public servicioDto servicio { get; set; }

        public oftalmologiaDto()
        {
        }

        public oftalmologiaDto(String v_OftalmologiaId, String v_ServicioId, String v_vcscOD, String v_vcscOI, String v_vcccOD, String v_vcccOI, String v_vlscOD, String v_vlscOI, String v_vlccOD, String v_vlccOI, String v_VisionColores, String v_EnfermedadesOculares, String v_ReflejosPupilares, String v_ConclusionesOftalmologia, Nullable<DateTime> d_FechaInsercion, Nullable<Int32> i_UsuarioInsertaId, Nullable<DateTime> d_FechaActualizacion, Nullable<Int32> i_UsuarioActualizaId, Nullable<Boolean> i_EsEliminado, servicioDto servicio)
        {
			this.v_OftalmologiaId = v_OftalmologiaId;
			this.v_ServicioId = v_ServicioId;
			this.v_vcscOD = v_vcscOD;
			this.v_vcscOI = v_vcscOI;
			this.v_vcccOD = v_vcccOD;
			this.v_vcccOI = v_vcccOI;
			this.v_vlscOD = v_vlscOD;
			this.v_vlscOI = v_vlscOI;
			this.v_vlccOD = v_vlccOD;
			this.v_vlccOI = v_vlccOI;
			this.v_VisionColores = v_VisionColores;
			this.v_EnfermedadesOculares = v_EnfermedadesOculares;
			this.v_ReflejosPupilares = v_ReflejosPupilares;
			this.v_ConclusionesOftalmologia = v_ConclusionesOftalmologia;
			this.d_FechaInsercion = d_FechaInsercion;
			this.i_UsuarioInsertaId = i_UsuarioInsertaId;
			this.d_FechaActualizacion = d_FechaActualizacion;
			this.i_UsuarioActualizaId = i_UsuarioActualizaId;
			this.i_EsEliminado = i_EsEliminado;
			this.servicio = servicio;
        }
    }
}
