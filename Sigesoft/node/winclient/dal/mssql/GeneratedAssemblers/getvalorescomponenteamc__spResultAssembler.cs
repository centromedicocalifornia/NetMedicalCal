//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:18:41
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
    /// Assembler for <see cref="getvalorescomponenteamc__spResult"/> and <see cref="getvalorescomponenteamc__spResultDto"/>.
    /// </summary>
    public static partial class getvalorescomponenteamc__spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getvalorescomponenteamc__spResultDto"/> converted from <see cref="getvalorescomponenteamc__spResult"/>.</param>
        static partial void OnDTO(this getvalorescomponenteamc__spResult entity, getvalorescomponenteamc__spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getvalorescomponenteamc__spResult"/> converted from <see cref="getvalorescomponenteamc__spResultDto"/>.</param>
        static partial void OnEntity(this getvalorescomponenteamc__spResultDto dto, getvalorescomponenteamc__spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getvalorescomponenteamc__spResultDto"/> to an instance of <see cref="getvalorescomponenteamc__spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getvalorescomponenteamc__spResultDto"/> to convert.</param>
        public static getvalorescomponenteamc__spResult ToEntity(this getvalorescomponenteamc__spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getvalorescomponenteamc__spResult();

            entity.v_ComponentFieldId = dto.v_ComponentFieldId;
            entity.v_ComponentFielName = dto.v_ComponentFielName;
            entity.v_Value1 = dto.v_Value1;
            entity.v_UnidadMedida = dto.v_UnidadMedida;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getvalorescomponenteamc__spResult"/> to an instance of <see cref="getvalorescomponenteamc__spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getvalorescomponenteamc__spResult"/> to convert.</param>
        public static getvalorescomponenteamc__spResultDto ToDTO(this getvalorescomponenteamc__spResult entity)
        {
            if (entity == null) return null;

            var dto = new getvalorescomponenteamc__spResultDto();

            dto.v_ComponentFieldId = entity.v_ComponentFieldId;
            dto.v_ComponentFielName = entity.v_ComponentFielName;
            dto.v_Value1 = entity.v_Value1;
            dto.v_UnidadMedida = entity.v_UnidadMedida;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getvalorescomponenteamc__spResultDto"/> to an instance of <see cref="getvalorescomponenteamc__spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getvalorescomponenteamc__spResult> ToEntities(this IEnumerable<getvalorescomponenteamc__spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getvalorescomponenteamc__spResult"/> to an instance of <see cref="getvalorescomponenteamc__spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getvalorescomponenteamc__spResultDto> ToDTOs(this IEnumerable<getvalorescomponenteamc__spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
