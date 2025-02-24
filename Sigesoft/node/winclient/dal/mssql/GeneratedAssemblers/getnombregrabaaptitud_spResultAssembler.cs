//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:16:45
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
    /// Assembler for <see cref="getnombregrabaaptitud_spResult"/> and <see cref="getnombregrabaaptitud_spResultDto"/>.
    /// </summary>
    public static partial class getnombregrabaaptitud_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getnombregrabaaptitud_spResultDto"/> converted from <see cref="getnombregrabaaptitud_spResult"/>.</param>
        static partial void OnDTO(this getnombregrabaaptitud_spResult entity, getnombregrabaaptitud_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getnombregrabaaptitud_spResult"/> converted from <see cref="getnombregrabaaptitud_spResultDto"/>.</param>
        static partial void OnEntity(this getnombregrabaaptitud_spResultDto dto, getnombregrabaaptitud_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getnombregrabaaptitud_spResultDto"/> to an instance of <see cref="getnombregrabaaptitud_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getnombregrabaaptitud_spResultDto"/> to convert.</param>
        public static getnombregrabaaptitud_spResult ToEntity(this getnombregrabaaptitud_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getnombregrabaaptitud_spResult();

            entity.NombreDoctor = dto.NombreDoctor;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getnombregrabaaptitud_spResult"/> to an instance of <see cref="getnombregrabaaptitud_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getnombregrabaaptitud_spResult"/> to convert.</param>
        public static getnombregrabaaptitud_spResultDto ToDTO(this getnombregrabaaptitud_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getnombregrabaaptitud_spResultDto();

            dto.NombreDoctor = entity.NombreDoctor;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getnombregrabaaptitud_spResultDto"/> to an instance of <see cref="getnombregrabaaptitud_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getnombregrabaaptitud_spResult> ToEntities(this IEnumerable<getnombregrabaaptitud_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getnombregrabaaptitud_spResult"/> to an instance of <see cref="getnombregrabaaptitud_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getnombregrabaaptitud_spResultDto> ToDTOs(this IEnumerable<getnombregrabaaptitud_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
