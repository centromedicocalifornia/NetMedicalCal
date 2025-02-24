//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:16:26
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
    /// Assembler for <see cref="getfirmamedicos_spResult"/> and <see cref="getfirmamedicos_spResultDto"/>.
    /// </summary>
    public static partial class getfirmamedicos_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getfirmamedicos_spResultDto"/> converted from <see cref="getfirmamedicos_spResult"/>.</param>
        static partial void OnDTO(this getfirmamedicos_spResult entity, getfirmamedicos_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getfirmamedicos_spResult"/> converted from <see cref="getfirmamedicos_spResultDto"/>.</param>
        static partial void OnEntity(this getfirmamedicos_spResultDto dto, getfirmamedicos_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getfirmamedicos_spResultDto"/> to an instance of <see cref="getfirmamedicos_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getfirmamedicos_spResultDto"/> to convert.</param>
        public static getfirmamedicos_spResult ToEntity(this getfirmamedicos_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getfirmamedicos_spResult();

            entity.Firma = dto.Firma;
            entity.Nombre = dto.Nombre;
            entity.CMP = dto.CMP;
            entity.ApPaterno = dto.ApPaterno;
            entity.ApMaterno = dto.ApMaterno;
            entity.Nombres = dto.Nombres;
            entity.Dni = dto.Dni;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getfirmamedicos_spResult"/> to an instance of <see cref="getfirmamedicos_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getfirmamedicos_spResult"/> to convert.</param>
        public static getfirmamedicos_spResultDto ToDTO(this getfirmamedicos_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getfirmamedicos_spResultDto();

            dto.Firma = entity.Firma;
            dto.Nombre = entity.Nombre;
            dto.CMP = entity.CMP;
            dto.ApPaterno = entity.ApPaterno;
            dto.ApMaterno = entity.ApMaterno;
            dto.Nombres = entity.Nombres;
            dto.Dni = entity.Dni;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getfirmamedicos_spResultDto"/> to an instance of <see cref="getfirmamedicos_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getfirmamedicos_spResult> ToEntities(this IEnumerable<getfirmamedicos_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getfirmamedicos_spResult"/> to an instance of <see cref="getfirmamedicos_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getfirmamedicos_spResultDto> ToDTOs(this IEnumerable<getfirmamedicos_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
