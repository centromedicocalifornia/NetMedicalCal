//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:17:17
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
    /// Assembler for <see cref="getreportelumbosaca_spResult"/> and <see cref="getreportelumbosaca_spResultDto"/>.
    /// </summary>
    public static partial class getreportelumbosaca_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getreportelumbosaca_spResultDto"/> converted from <see cref="getreportelumbosaca_spResult"/>.</param>
        static partial void OnDTO(this getreportelumbosaca_spResult entity, getreportelumbosaca_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getreportelumbosaca_spResult"/> converted from <see cref="getreportelumbosaca_spResultDto"/>.</param>
        static partial void OnEntity(this getreportelumbosaca_spResultDto dto, getreportelumbosaca_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getreportelumbosaca_spResultDto"/> to an instance of <see cref="getreportelumbosaca_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getreportelumbosaca_spResultDto"/> to convert.</param>
        public static getreportelumbosaca_spResult ToEntity(this getreportelumbosaca_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getreportelumbosaca_spResult();

            entity.EmpresaCliente = dto.EmpresaCliente;
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

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getreportelumbosaca_spResult"/> to an instance of <see cref="getreportelumbosaca_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getreportelumbosaca_spResult"/> to convert.</param>
        public static getreportelumbosaca_spResultDto ToDTO(this getreportelumbosaca_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getreportelumbosaca_spResultDto();

            dto.EmpresaCliente = entity.EmpresaCliente;
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

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getreportelumbosaca_spResultDto"/> to an instance of <see cref="getreportelumbosaca_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getreportelumbosaca_spResult> ToEntities(this IEnumerable<getreportelumbosaca_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getreportelumbosaca_spResult"/> to an instance of <see cref="getreportelumbosaca_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getreportelumbosaca_spResultDto> ToDTOs(this IEnumerable<getreportelumbosaca_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
