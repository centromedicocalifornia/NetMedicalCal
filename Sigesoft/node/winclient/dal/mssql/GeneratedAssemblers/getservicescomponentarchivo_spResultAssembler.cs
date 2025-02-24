//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:18:13
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
    /// Assembler for <see cref="getservicescomponentarchivo_spResult"/> and <see cref="getservicescomponentarchivo_spResultDto"/>.
    /// </summary>
    public static partial class getservicescomponentarchivo_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getservicescomponentarchivo_spResultDto"/> converted from <see cref="getservicescomponentarchivo_spResult"/>.</param>
        static partial void OnDTO(this getservicescomponentarchivo_spResult entity, getservicescomponentarchivo_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getservicescomponentarchivo_spResult"/> converted from <see cref="getservicescomponentarchivo_spResultDto"/>.</param>
        static partial void OnEntity(this getservicescomponentarchivo_spResultDto dto, getservicescomponentarchivo_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getservicescomponentarchivo_spResultDto"/> to an instance of <see cref="getservicescomponentarchivo_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getservicescomponentarchivo_spResultDto"/> to convert.</param>
        public static getservicescomponentarchivo_spResult ToEntity(this getservicescomponentarchivo_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getservicescomponentarchivo_spResult();

            entity.v_FileName = dto.v_FileName;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getservicescomponentarchivo_spResult"/> to an instance of <see cref="getservicescomponentarchivo_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getservicescomponentarchivo_spResult"/> to convert.</param>
        public static getservicescomponentarchivo_spResultDto ToDTO(this getservicescomponentarchivo_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getservicescomponentarchivo_spResultDto();

            dto.v_FileName = entity.v_FileName;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getservicescomponentarchivo_spResultDto"/> to an instance of <see cref="getservicescomponentarchivo_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getservicescomponentarchivo_spResult> ToEntities(this IEnumerable<getservicescomponentarchivo_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getservicescomponentarchivo_spResult"/> to an instance of <see cref="getservicescomponentarchivo_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getservicescomponentarchivo_spResultDto> ToDTOs(this IEnumerable<getservicescomponentarchivo_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
