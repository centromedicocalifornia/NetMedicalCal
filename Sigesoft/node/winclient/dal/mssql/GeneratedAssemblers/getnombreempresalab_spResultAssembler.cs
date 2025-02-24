//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:16:44
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
    /// Assembler for <see cref="getnombreempresalab_spResult"/> and <see cref="getnombreempresalab_spResultDto"/>.
    /// </summary>
    public static partial class getnombreempresalab_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getnombreempresalab_spResultDto"/> converted from <see cref="getnombreempresalab_spResult"/>.</param>
        static partial void OnDTO(this getnombreempresalab_spResult entity, getnombreempresalab_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getnombreempresalab_spResult"/> converted from <see cref="getnombreempresalab_spResultDto"/>.</param>
        static partial void OnEntity(this getnombreempresalab_spResultDto dto, getnombreempresalab_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getnombreempresalab_spResultDto"/> to an instance of <see cref="getnombreempresalab_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getnombreempresalab_spResultDto"/> to convert.</param>
        public static getnombreempresalab_spResult ToEntity(this getnombreempresalab_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getnombreempresalab_spResult();

            entity.EMPRESA = dto.EMPRESA;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getnombreempresalab_spResult"/> to an instance of <see cref="getnombreempresalab_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getnombreempresalab_spResult"/> to convert.</param>
        public static getnombreempresalab_spResultDto ToDTO(this getnombreempresalab_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getnombreempresalab_spResultDto();

            dto.EMPRESA = entity.EMPRESA;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getnombreempresalab_spResultDto"/> to an instance of <see cref="getnombreempresalab_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getnombreempresalab_spResult> ToEntities(this IEnumerable<getnombreempresalab_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getnombreempresalab_spResult"/> to an instance of <see cref="getnombreempresalab_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getnombreempresalab_spResultDto> ToDTOs(this IEnumerable<getnombreempresalab_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
