//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:15:59
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
    /// Assembler for <see cref="getaccessusermonthResult"/> and <see cref="getaccessusermonthResultDto"/>.
    /// </summary>
    public static partial class getaccessusermonthResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getaccessusermonthResultDto"/> converted from <see cref="getaccessusermonthResult"/>.</param>
        static partial void OnDTO(this getaccessusermonthResult entity, getaccessusermonthResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getaccessusermonthResult"/> converted from <see cref="getaccessusermonthResultDto"/>.</param>
        static partial void OnEntity(this getaccessusermonthResultDto dto, getaccessusermonthResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getaccessusermonthResultDto"/> to an instance of <see cref="getaccessusermonthResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getaccessusermonthResultDto"/> to convert.</param>
        public static getaccessusermonthResult ToEntity(this getaccessusermonthResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getaccessusermonthResult();

            entity.Enero = dto.Enero;
            entity.Febrero = dto.Febrero;
            entity.Marzo = dto.Marzo;
            entity.Abril = dto.Abril;
            entity.Mayo = dto.Mayo;
            entity.Junio = dto.Junio;
            entity.Julio = dto.Julio;
            entity.Agosto = dto.Agosto;
            entity.Septiembre = dto.Septiembre;
            entity.Octubre = dto.Octubre;
            entity.Noviembre = dto.Noviembre;
            entity.Diciembre = dto.Diciembre;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getaccessusermonthResult"/> to an instance of <see cref="getaccessusermonthResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getaccessusermonthResult"/> to convert.</param>
        public static getaccessusermonthResultDto ToDTO(this getaccessusermonthResult entity)
        {
            if (entity == null) return null;

            var dto = new getaccessusermonthResultDto();

            dto.Enero = entity.Enero;
            dto.Febrero = entity.Febrero;
            dto.Marzo = entity.Marzo;
            dto.Abril = entity.Abril;
            dto.Mayo = entity.Mayo;
            dto.Junio = entity.Junio;
            dto.Julio = entity.Julio;
            dto.Agosto = entity.Agosto;
            dto.Septiembre = entity.Septiembre;
            dto.Octubre = entity.Octubre;
            dto.Noviembre = entity.Noviembre;
            dto.Diciembre = entity.Diciembre;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getaccessusermonthResultDto"/> to an instance of <see cref="getaccessusermonthResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getaccessusermonthResult> ToEntities(this IEnumerable<getaccessusermonthResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getaccessusermonthResult"/> to an instance of <see cref="getaccessusermonthResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getaccessusermonthResultDto> ToDTOs(this IEnumerable<getaccessusermonthResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
