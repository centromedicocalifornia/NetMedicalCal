//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:18:11
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
    /// Assembler for <see cref="getservicerestrictionsforgridview_spResult"/> and <see cref="getservicerestrictionsforgridview_spResultDto"/>.
    /// </summary>
    public static partial class getservicerestrictionsforgridview_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getservicerestrictionsforgridview_spResultDto"/> converted from <see cref="getservicerestrictionsforgridview_spResult"/>.</param>
        static partial void OnDTO(this getservicerestrictionsforgridview_spResult entity, getservicerestrictionsforgridview_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getservicerestrictionsforgridview_spResult"/> converted from <see cref="getservicerestrictionsforgridview_spResultDto"/>.</param>
        static partial void OnEntity(this getservicerestrictionsforgridview_spResultDto dto, getservicerestrictionsforgridview_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getservicerestrictionsforgridview_spResultDto"/> to an instance of <see cref="getservicerestrictionsforgridview_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getservicerestrictionsforgridview_spResultDto"/> to convert.</param>
        public static getservicerestrictionsforgridview_spResult ToEntity(this getservicerestrictionsforgridview_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getservicerestrictionsforgridview_spResult();

            entity.v_DiagnosticRepositoryId = dto.v_DiagnosticRepositoryId;
            entity.v_RestrictionByDiagnosticId = dto.v_RestrictionByDiagnosticId;
            entity.v_MasterRestrictionId = dto.v_MasterRestrictionId;
            entity.v_RestrictionName = dto.v_RestrictionName;
            entity.v_ComponentId = dto.v_ComponentId;
            entity.i_RecordStatus = dto.i_RecordStatus;
            entity.i_RecordType = dto.i_RecordType;
            entity.d_StartDateRestriction = dto.d_StartDateRestriction;
            entity.d_EndDateRestriction = dto.d_EndDateRestriction;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getservicerestrictionsforgridview_spResult"/> to an instance of <see cref="getservicerestrictionsforgridview_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getservicerestrictionsforgridview_spResult"/> to convert.</param>
        public static getservicerestrictionsforgridview_spResultDto ToDTO(this getservicerestrictionsforgridview_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getservicerestrictionsforgridview_spResultDto();

            dto.v_DiagnosticRepositoryId = entity.v_DiagnosticRepositoryId;
            dto.v_RestrictionByDiagnosticId = entity.v_RestrictionByDiagnosticId;
            dto.v_MasterRestrictionId = entity.v_MasterRestrictionId;
            dto.v_RestrictionName = entity.v_RestrictionName;
            dto.v_ComponentId = entity.v_ComponentId;
            dto.i_RecordStatus = entity.i_RecordStatus;
            dto.i_RecordType = entity.i_RecordType;
            dto.d_StartDateRestriction = entity.d_StartDateRestriction;
            dto.d_EndDateRestriction = entity.d_EndDateRestriction;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getservicerestrictionsforgridview_spResultDto"/> to an instance of <see cref="getservicerestrictionsforgridview_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getservicerestrictionsforgridview_spResult> ToEntities(this IEnumerable<getservicerestrictionsforgridview_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getservicerestrictionsforgridview_spResult"/> to an instance of <see cref="getservicerestrictionsforgridview_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getservicerestrictionsforgridview_spResultDto> ToDTOs(this IEnumerable<getservicerestrictionsforgridview_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
