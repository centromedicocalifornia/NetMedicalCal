//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:20:06
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
    /// Assembler for <see cref="ciiui"/> and <see cref="ciiuiDto"/>.
    /// </summary>
    public static partial class ciiuiAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="ciiuiDto"/> converted from <see cref="ciiui"/>.</param>
        static partial void OnDTO(this ciiui entity, ciiuiDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="ciiui"/> converted from <see cref="ciiuiDto"/>.</param>
        static partial void OnEntity(this ciiuiDto dto, ciiui entity);

        /// <summary>
        /// Converts this instance of <see cref="ciiuiDto"/> to an instance of <see cref="ciiui"/>.
        /// </summary>
        /// <param name="dto"><see cref="ciiuiDto"/> to convert.</param>
        public static ciiui ToEntity(this ciiuiDto dto)
        {
            if (dto == null) return null;

            var entity = new ciiui();

            entity.v_CIIUId = dto.v_CIIUId;
            entity.v_CIIUDescription1 = dto.v_CIIUDescription1;
            entity.v_CIIUDescription2 = dto.v_CIIUDescription2;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.v_ComentaryUpdate = dto.v_ComentaryUpdate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="ciiui"/> to an instance of <see cref="ciiuiDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="ciiui"/> to convert.</param>
        public static ciiuiDto ToDTO(this ciiui entity)
        {
            if (entity == null) return null;

            var dto = new ciiuiDto();

            dto.v_CIIUId = entity.v_CIIUId;
            dto.v_CIIUDescription1 = entity.v_CIIUDescription1;
            dto.v_CIIUDescription2 = entity.v_CIIUDescription2;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.v_ComentaryUpdate = entity.v_ComentaryUpdate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="ciiuiDto"/> to an instance of <see cref="ciiui"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ciiui> ToEntities(this IEnumerable<ciiuiDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="ciiui"/> to an instance of <see cref="ciiuiDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ciiuiDto> ToDTOs(this IEnumerable<ciiui> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
