//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:19:30
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
    /// Assembler for <see cref="reporteosteomuscular_spResult"/> and <see cref="reporteosteomuscular_spResultDto"/>.
    /// </summary>
    public static partial class reporteosteomuscular_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="reporteosteomuscular_spResultDto"/> converted from <see cref="reporteosteomuscular_spResult"/>.</param>
        static partial void OnDTO(this reporteosteomuscular_spResult entity, reporteosteomuscular_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="reporteosteomuscular_spResult"/> converted from <see cref="reporteosteomuscular_spResultDto"/>.</param>
        static partial void OnEntity(this reporteosteomuscular_spResultDto dto, reporteosteomuscular_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="reporteosteomuscular_spResultDto"/> to an instance of <see cref="reporteosteomuscular_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="reporteosteomuscular_spResultDto"/> to convert.</param>
        public static reporteosteomuscular_spResult ToEntity(this reporteosteomuscular_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new reporteosteomuscular_spResult();

            entity.Trabajador = dto.Trabajador;
            entity.FechaNacimiento = dto.FechaNacimiento;
            entity.FechaEvaluacion = dto.FechaEvaluacion;
            entity.Puesto = dto.Puesto;
            entity.ServicioId = dto.ServicioId;
            entity.FirmaUsuarioGraba = dto.FirmaUsuarioGraba;
            entity.NombreUsuarioGraba = dto.NombreUsuarioGraba;
            entity.HuellaTrabajador = dto.HuellaTrabajador;
            entity.FirmaTrabajador = dto.FirmaTrabajador;
            entity.TipoEso = dto.TipoEso;
            entity.Dni = dto.Dni;
            entity.Edad = dto.Edad;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="reporteosteomuscular_spResult"/> to an instance of <see cref="reporteosteomuscular_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="reporteosteomuscular_spResult"/> to convert.</param>
        public static reporteosteomuscular_spResultDto ToDTO(this reporteosteomuscular_spResult entity)
        {
            if (entity == null) return null;

            var dto = new reporteosteomuscular_spResultDto();

            dto.Trabajador = entity.Trabajador;
            dto.FechaNacimiento = entity.FechaNacimiento;
            dto.FechaEvaluacion = entity.FechaEvaluacion;
            dto.Puesto = entity.Puesto;
            dto.ServicioId = entity.ServicioId;
            dto.FirmaUsuarioGraba = entity.FirmaUsuarioGraba;
            dto.NombreUsuarioGraba = entity.NombreUsuarioGraba;
            dto.HuellaTrabajador = entity.HuellaTrabajador;
            dto.FirmaTrabajador = entity.FirmaTrabajador;
            dto.TipoEso = entity.TipoEso;
            dto.Dni = entity.Dni;
            dto.Edad = entity.Edad;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="reporteosteomuscular_spResultDto"/> to an instance of <see cref="reporteosteomuscular_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<reporteosteomuscular_spResult> ToEntities(this IEnumerable<reporteosteomuscular_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="reporteosteomuscular_spResult"/> to an instance of <see cref="reporteosteomuscular_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<reporteosteomuscular_spResultDto> ToDTOs(this IEnumerable<reporteosteomuscular_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
