//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:19:11
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
    /// Assembler for <see cref="obteneresoantecedentesporgrupoid2__spResult"/> and <see cref="obteneresoantecedentesporgrupoid2__spResultDto"/>.
    /// </summary>
    public static partial class obteneresoantecedentesporgrupoid2__spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="obteneresoantecedentesporgrupoid2__spResultDto"/> converted from <see cref="obteneresoantecedentesporgrupoid2__spResult"/>.</param>
        static partial void OnDTO(this obteneresoantecedentesporgrupoid2__spResult entity, obteneresoantecedentesporgrupoid2__spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="obteneresoantecedentesporgrupoid2__spResult"/> converted from <see cref="obteneresoantecedentesporgrupoid2__spResultDto"/>.</param>
        static partial void OnEntity(this obteneresoantecedentesporgrupoid2__spResultDto dto, obteneresoantecedentesporgrupoid2__spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="obteneresoantecedentesporgrupoid2__spResultDto"/> to an instance of <see cref="obteneresoantecedentesporgrupoid2__spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="obteneresoantecedentesporgrupoid2__spResultDto"/> to convert.</param>
        public static obteneresoantecedentesporgrupoid2__spResult ToEntity(this obteneresoantecedentesporgrupoid2__spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new obteneresoantecedentesporgrupoid2__spResult();

            entity.Nombre = dto.Nombre;
            entity.GrupoId = dto.GrupoId;
            entity.ParametroId = dto.ParametroId;
            entity.SI = dto.SI;
            entity.NO = dto.NO;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="obteneresoantecedentesporgrupoid2__spResult"/> to an instance of <see cref="obteneresoantecedentesporgrupoid2__spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="obteneresoantecedentesporgrupoid2__spResult"/> to convert.</param>
        public static obteneresoantecedentesporgrupoid2__spResultDto ToDTO(this obteneresoantecedentesporgrupoid2__spResult entity)
        {
            if (entity == null) return null;

            var dto = new obteneresoantecedentesporgrupoid2__spResultDto();

            dto.Nombre = entity.Nombre;
            dto.GrupoId = entity.GrupoId;
            dto.ParametroId = entity.ParametroId;
            dto.SI = entity.SI;
            dto.NO = entity.NO;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="obteneresoantecedentesporgrupoid2__spResultDto"/> to an instance of <see cref="obteneresoantecedentesporgrupoid2__spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<obteneresoantecedentesporgrupoid2__spResult> ToEntities(this IEnumerable<obteneresoantecedentesporgrupoid2__spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="obteneresoantecedentesporgrupoid2__spResult"/> to an instance of <see cref="obteneresoantecedentesporgrupoid2__spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<obteneresoantecedentesporgrupoid2__spResultDto> ToDTOs(this IEnumerable<obteneresoantecedentesporgrupoid2__spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
