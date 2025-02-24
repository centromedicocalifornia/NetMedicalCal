//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:22:20
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
    /// Assembler for <see cref="planintegral"/> and <see cref="planintegralDto"/>.
    /// </summary>
    public static partial class planintegralAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="planintegralDto"/> converted from <see cref="planintegral"/>.</param>
        static partial void OnDTO(this planintegral entity, planintegralDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="planintegral"/> converted from <see cref="planintegralDto"/>.</param>
        static partial void OnEntity(this planintegralDto dto, planintegral entity);

        /// <summary>
        /// Converts this instance of <see cref="planintegralDto"/> to an instance of <see cref="planintegral"/>.
        /// </summary>
        /// <param name="dto"><see cref="planintegralDto"/> to convert.</param>
        public static planintegral ToEntity(this planintegralDto dto)
        {
            if (dto == null) return null;

            var entity = new planintegral();

            entity.v_PlanIntegral = dto.v_PlanIntegral;
            entity.v_PersonId = dto.v_PersonId;
            entity.i_TipoId = dto.i_TipoId;
            entity.v_Descripcion = dto.v_Descripcion;
            entity.d_Fecha = dto.d_Fecha;
            entity.v_Lugar = dto.v_Lugar;
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
        /// Converts this instance of <see cref="planintegral"/> to an instance of <see cref="planintegralDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="planintegral"/> to convert.</param>
        public static planintegralDto ToDTO(this planintegral entity)
        {
            if (entity == null) return null;

            var dto = new planintegralDto();

            dto.v_PlanIntegral = entity.v_PlanIntegral;
            dto.v_PersonId = entity.v_PersonId;
            dto.i_TipoId = entity.i_TipoId;
            dto.v_Descripcion = entity.v_Descripcion;
            dto.d_Fecha = entity.d_Fecha;
            dto.v_Lugar = entity.v_Lugar;
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
        /// Converts each instance of <see cref="planintegralDto"/> to an instance of <see cref="planintegral"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<planintegral> ToEntities(this IEnumerable<planintegralDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="planintegral"/> to an instance of <see cref="planintegralDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<planintegralDto> ToDTOs(this IEnumerable<planintegral> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
