//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:17:33
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
    /// Assembler for <see cref="getrestrictionbyserviceid_spResult"/> and <see cref="getrestrictionbyserviceid_spResultDto"/>.
    /// </summary>
    public static partial class getrestrictionbyserviceid_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getrestrictionbyserviceid_spResultDto"/> converted from <see cref="getrestrictionbyserviceid_spResult"/>.</param>
        static partial void OnDTO(this getrestrictionbyserviceid_spResult entity, getrestrictionbyserviceid_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getrestrictionbyserviceid_spResult"/> converted from <see cref="getrestrictionbyserviceid_spResultDto"/>.</param>
        static partial void OnEntity(this getrestrictionbyserviceid_spResultDto dto, getrestrictionbyserviceid_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getrestrictionbyserviceid_spResultDto"/> to an instance of <see cref="getrestrictionbyserviceid_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getrestrictionbyserviceid_spResultDto"/> to convert.</param>
        public static getrestrictionbyserviceid_spResult ToEntity(this getrestrictionbyserviceid_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getrestrictionbyserviceid_spResult();

            entity.v_RestriccitionName = dto.v_RestriccitionName;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getrestrictionbyserviceid_spResult"/> to an instance of <see cref="getrestrictionbyserviceid_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getrestrictionbyserviceid_spResult"/> to convert.</param>
        public static getrestrictionbyserviceid_spResultDto ToDTO(this getrestrictionbyserviceid_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getrestrictionbyserviceid_spResultDto();

            dto.v_RestriccitionName = entity.v_RestriccitionName;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getrestrictionbyserviceid_spResultDto"/> to an instance of <see cref="getrestrictionbyserviceid_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getrestrictionbyserviceid_spResult> ToEntities(this IEnumerable<getrestrictionbyserviceid_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getrestrictionbyserviceid_spResult"/> to an instance of <see cref="getrestrictionbyserviceid_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getrestrictionbyserviceid_spResultDto> ToDTOs(this IEnumerable<getrestrictionbyserviceid_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
