//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:16:15
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
    /// Assembler for <see cref="getdevolverdatosusuariograboexamen_spResult"/> and <see cref="getdevolverdatosusuariograboexamen_spResultDto"/>.
    /// </summary>
    public static partial class getdevolverdatosusuariograboexamen_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getdevolverdatosusuariograboexamen_spResultDto"/> converted from <see cref="getdevolverdatosusuariograboexamen_spResult"/>.</param>
        static partial void OnDTO(this getdevolverdatosusuariograboexamen_spResult entity, getdevolverdatosusuariograboexamen_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getdevolverdatosusuariograboexamen_spResult"/> converted from <see cref="getdevolverdatosusuariograboexamen_spResultDto"/>.</param>
        static partial void OnEntity(this getdevolverdatosusuariograboexamen_spResultDto dto, getdevolverdatosusuariograboexamen_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getdevolverdatosusuariograboexamen_spResultDto"/> to an instance of <see cref="getdevolverdatosusuariograboexamen_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getdevolverdatosusuariograboexamen_spResultDto"/> to convert.</param>
        public static getdevolverdatosusuariograboexamen_spResult ToEntity(this getdevolverdatosusuariograboexamen_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getdevolverdatosusuariograboexamen_spResult();

            entity.Firma = dto.Firma;
            entity.Nombre = dto.Nombre;
            entity.CMP = dto.CMP;
            entity.ApPaterno = dto.ApPaterno;
            entity.ApMaterno = dto.ApMaterno;
            entity.Nombres = dto.Nombres;
            entity.Dni = dto.Dni;
            entity.Huella = dto.Huella;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getdevolverdatosusuariograboexamen_spResult"/> to an instance of <see cref="getdevolverdatosusuariograboexamen_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getdevolverdatosusuariograboexamen_spResult"/> to convert.</param>
        public static getdevolverdatosusuariograboexamen_spResultDto ToDTO(this getdevolverdatosusuariograboexamen_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getdevolverdatosusuariograboexamen_spResultDto();

            dto.Firma = entity.Firma;
            dto.Nombre = entity.Nombre;
            dto.CMP = entity.CMP;
            dto.ApPaterno = entity.ApPaterno;
            dto.ApMaterno = entity.ApMaterno;
            dto.Nombres = entity.Nombres;
            dto.Dni = entity.Dni;
            dto.Huella = entity.Huella;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getdevolverdatosusuariograboexamen_spResultDto"/> to an instance of <see cref="getdevolverdatosusuariograboexamen_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getdevolverdatosusuariograboexamen_spResult> ToEntities(this IEnumerable<getdevolverdatosusuariograboexamen_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getdevolverdatosusuariograboexamen_spResult"/> to an instance of <see cref="getdevolverdatosusuariograboexamen_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getdevolverdatosusuariograboexamen_spResultDto> ToDTOs(this IEnumerable<getdevolverdatosusuariograboexamen_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
