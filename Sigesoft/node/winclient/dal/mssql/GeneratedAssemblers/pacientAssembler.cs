//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:21:58
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
    /// Assembler for <see cref="pacient"/> and <see cref="pacientDto"/>.
    /// </summary>
    public static partial class pacientAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="pacientDto"/> converted from <see cref="pacient"/>.</param>
        static partial void OnDTO(this pacient entity, pacientDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="pacient"/> converted from <see cref="pacientDto"/>.</param>
        static partial void OnEntity(this pacientDto dto, pacient entity);

        /// <summary>
        /// Converts this instance of <see cref="pacientDto"/> to an instance of <see cref="pacient"/>.
        /// </summary>
        /// <param name="dto"><see cref="pacientDto"/> to convert.</param>
        public static pacient ToEntity(this pacientDto dto)
        {
            if (dto == null) return null;

            var entity = new pacient();

            entity.v_PersonId = dto.v_PersonId;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.i_UpdateNodeId = dto.i_UpdateNodeId;
            entity.v_ComentaryUpdate = dto.v_ComentaryUpdate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="pacient"/> to an instance of <see cref="pacientDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="pacient"/> to convert.</param>
        public static pacientDto ToDTO(this pacient entity)
        {
            if (entity == null) return null;

            var dto = new pacientDto();

            dto.v_PersonId = entity.v_PersonId;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.i_UpdateNodeId = entity.i_UpdateNodeId;
            dto.v_ComentaryUpdate = entity.v_ComentaryUpdate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="pacientDto"/> to an instance of <see cref="pacient"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<pacient> ToEntities(this IEnumerable<pacientDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="pacient"/> to an instance of <see cref="pacientDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<pacientDto> ToDTOs(this IEnumerable<pacient> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
