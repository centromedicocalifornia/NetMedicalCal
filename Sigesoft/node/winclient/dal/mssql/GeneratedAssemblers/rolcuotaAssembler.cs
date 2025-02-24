//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:23:10
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
    /// Assembler for <see cref="rolcuota"/> and <see cref="rolcuotaDto"/>.
    /// </summary>
    public static partial class rolcuotaAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="rolcuotaDto"/> converted from <see cref="rolcuota"/>.</param>
        static partial void OnDTO(this rolcuota entity, rolcuotaDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="rolcuota"/> converted from <see cref="rolcuotaDto"/>.</param>
        static partial void OnEntity(this rolcuotaDto dto, rolcuota entity);

        /// <summary>
        /// Converts this instance of <see cref="rolcuotaDto"/> to an instance of <see cref="rolcuota"/>.
        /// </summary>
        /// <param name="dto"><see cref="rolcuotaDto"/> to convert.</param>
        public static rolcuota ToEntity(this rolcuotaDto dto)
        {
            if (dto == null) return null;

            var entity = new rolcuota();

            entity.v_RolCuotaId = dto.v_RolCuotaId;
            entity.i_RolId = dto.i_RolId;
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
        /// Converts this instance of <see cref="rolcuota"/> to an instance of <see cref="rolcuotaDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="rolcuota"/> to convert.</param>
        public static rolcuotaDto ToDTO(this rolcuota entity)
        {
            if (entity == null) return null;

            var dto = new rolcuotaDto();

            dto.v_RolCuotaId = entity.v_RolCuotaId;
            dto.i_RolId = entity.i_RolId;
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
        /// Converts each instance of <see cref="rolcuotaDto"/> to an instance of <see cref="rolcuota"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<rolcuota> ToEntities(this IEnumerable<rolcuotaDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="rolcuota"/> to an instance of <see cref="rolcuotaDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<rolcuotaDto> ToDTOs(this IEnumerable<rolcuota> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
