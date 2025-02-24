//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:18:46
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
    /// Assembler for <see cref="getverificarescritura_spResult"/> and <see cref="getverificarescritura_spResultDto"/>.
    /// </summary>
    public static partial class getverificarescritura_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getverificarescritura_spResultDto"/> converted from <see cref="getverificarescritura_spResult"/>.</param>
        static partial void OnDTO(this getverificarescritura_spResult entity, getverificarescritura_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getverificarescritura_spResult"/> converted from <see cref="getverificarescritura_spResultDto"/>.</param>
        static partial void OnEntity(this getverificarescritura_spResultDto dto, getverificarescritura_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getverificarescritura_spResultDto"/> to an instance of <see cref="getverificarescritura_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getverificarescritura_spResultDto"/> to convert.</param>
        public static getverificarescritura_spResult ToEntity(this getverificarescritura_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getverificarescritura_spResult();

            entity.i_Write = dto.i_Write;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getverificarescritura_spResult"/> to an instance of <see cref="getverificarescritura_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getverificarescritura_spResult"/> to convert.</param>
        public static getverificarescritura_spResultDto ToDTO(this getverificarescritura_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getverificarescritura_spResultDto();

            dto.i_Write = entity.i_Write;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getverificarescritura_spResultDto"/> to an instance of <see cref="getverificarescritura_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getverificarescritura_spResult> ToEntities(this IEnumerable<getverificarescritura_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getverificarescritura_spResult"/> to an instance of <see cref="getverificarescritura_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getverificarescritura_spResultDto> ToDTOs(this IEnumerable<getverificarescritura_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
