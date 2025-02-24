//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:16:21
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
    /// Assembler for <see cref="getedadpersonResult"/> and <see cref="getedadpersonResultDto"/>.
    /// </summary>
    public static partial class getedadpersonResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getedadpersonResultDto"/> converted from <see cref="getedadpersonResult"/>.</param>
        static partial void OnDTO(this getedadpersonResult entity, getedadpersonResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getedadpersonResult"/> converted from <see cref="getedadpersonResultDto"/>.</param>
        static partial void OnEntity(this getedadpersonResultDto dto, getedadpersonResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getedadpersonResultDto"/> to an instance of <see cref="getedadpersonResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getedadpersonResultDto"/> to convert.</param>
        public static getedadpersonResult ToEntity(this getedadpersonResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getedadpersonResult();

            entity.Edad = dto.Edad;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getedadpersonResult"/> to an instance of <see cref="getedadpersonResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getedadpersonResult"/> to convert.</param>
        public static getedadpersonResultDto ToDTO(this getedadpersonResult entity)
        {
            if (entity == null) return null;

            var dto = new getedadpersonResultDto();

            dto.Edad = entity.Edad;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getedadpersonResultDto"/> to an instance of <see cref="getedadpersonResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getedadpersonResult> ToEntities(this IEnumerable<getedadpersonResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getedadpersonResult"/> to an instance of <see cref="getedadpersonResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getedadpersonResultDto> ToDTOs(this IEnumerable<getedadpersonResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
