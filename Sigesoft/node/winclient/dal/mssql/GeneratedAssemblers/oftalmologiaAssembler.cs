//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:21:49
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BE
{

    /// <summary>
    /// Assembler for <see cref="oftalmologia"/> and <see cref="oftalmologiaDto"/>.
    /// </summary>
    public static partial class oftalmologiaAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="oftalmologiaDto"/> converted from <see cref="oftalmologia"/>.</param>
        static partial void OnDTO(this oftalmologia entity, oftalmologiaDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="oftalmologia"/> converted from <see cref="oftalmologiaDto"/>.</param>
        static partial void OnEntity(this oftalmologiaDto dto, oftalmologia entity);

        /// <summary>
        /// Converts this instance of <see cref="oftalmologiaDto"/> to an instance of <see cref="oftalmologia"/>.
        /// </summary>
        /// <param name="dto"><see cref="oftalmologiaDto"/> to convert.</param>
        public static oftalmologia ToEntity(this oftalmologiaDto dto)
        {
            if (dto == null) return null;

            var entity = new oftalmologia();

            entity.v_OftalmologiaId = dto.v_OftalmologiaId;
            entity.v_ServicioId = dto.v_ServicioId;
            entity.v_vcscOD = dto.v_vcscOD;
            entity.v_vcscOI = dto.v_vcscOI;
            entity.v_vcccOD = dto.v_vcccOD;
            entity.v_vcccOI = dto.v_vcccOI;
            entity.v_vlscOD = dto.v_vlscOD;
            entity.v_vlscOI = dto.v_vlscOI;
            entity.v_vlccOD = dto.v_vlccOD;
            entity.v_vlccOI = dto.v_vlccOI;
            entity.v_VisionColores = dto.v_VisionColores;
            entity.v_EnfermedadesOculares = dto.v_EnfermedadesOculares;
            entity.v_ReflejosPupilares = dto.v_ReflejosPupilares;
            entity.v_ConclusionesOftalmologia = dto.v_ConclusionesOftalmologia;
            entity.d_FechaInsercion = dto.d_FechaInsercion;
            entity.i_UsuarioInsertaId = dto.i_UsuarioInsertaId;
            entity.d_FechaActualizacion = dto.d_FechaActualizacion;
            entity.i_UsuarioActualizaId = dto.i_UsuarioActualizaId;
            entity.i_EsEliminado = dto.i_EsEliminado;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="oftalmologia"/> to an instance of <see cref="oftalmologiaDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="oftalmologia"/> to convert.</param>
        public static oftalmologiaDto ToDTO(this oftalmologia entity)
        {
            if (entity == null) return null;

            var dto = new oftalmologiaDto();

            dto.v_OftalmologiaId = entity.v_OftalmologiaId;
            dto.v_ServicioId = entity.v_ServicioId;
            dto.v_vcscOD = entity.v_vcscOD;
            dto.v_vcscOI = entity.v_vcscOI;
            dto.v_vcccOD = entity.v_vcccOD;
            dto.v_vcccOI = entity.v_vcccOI;
            dto.v_vlscOD = entity.v_vlscOD;
            dto.v_vlscOI = entity.v_vlscOI;
            dto.v_vlccOD = entity.v_vlccOD;
            dto.v_vlccOI = entity.v_vlccOI;
            dto.v_VisionColores = entity.v_VisionColores;
            dto.v_EnfermedadesOculares = entity.v_EnfermedadesOculares;
            dto.v_ReflejosPupilares = entity.v_ReflejosPupilares;
            dto.v_ConclusionesOftalmologia = entity.v_ConclusionesOftalmologia;
            dto.d_FechaInsercion = entity.d_FechaInsercion;
            dto.i_UsuarioInsertaId = entity.i_UsuarioInsertaId;
            dto.d_FechaActualizacion = entity.d_FechaActualizacion;
            dto.i_UsuarioActualizaId = entity.i_UsuarioActualizaId;
            dto.i_EsEliminado = entity.i_EsEliminado;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="oftalmologiaDto"/> to an instance of <see cref="oftalmologia"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<oftalmologia> ToEntities(this IEnumerable<oftalmologiaDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="oftalmologia"/> to an instance of <see cref="oftalmologiaDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<oftalmologiaDto> ToDTOs(this IEnumerable<oftalmologia> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
