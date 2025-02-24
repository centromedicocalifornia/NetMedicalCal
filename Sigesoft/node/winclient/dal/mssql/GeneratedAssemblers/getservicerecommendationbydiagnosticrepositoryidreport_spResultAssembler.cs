//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:18:06
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
    /// Assembler for <see cref="getservicerecommendationbydiagnosticrepositoryidreport_spResult"/> and <see cref="getservicerecommendationbydiagnosticrepositoryidreport_spResultDto"/>.
    /// </summary>
    public static partial class getservicerecommendationbydiagnosticrepositoryidreport_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getservicerecommendationbydiagnosticrepositoryidreport_spResultDto"/> converted from <see cref="getservicerecommendationbydiagnosticrepositoryidreport_spResult"/>.</param>
        static partial void OnDTO(this getservicerecommendationbydiagnosticrepositoryidreport_spResult entity, getservicerecommendationbydiagnosticrepositoryidreport_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getservicerecommendationbydiagnosticrepositoryidreport_spResult"/> converted from <see cref="getservicerecommendationbydiagnosticrepositoryidreport_spResultDto"/>.</param>
        static partial void OnEntity(this getservicerecommendationbydiagnosticrepositoryidreport_spResultDto dto, getservicerecommendationbydiagnosticrepositoryidreport_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getservicerecommendationbydiagnosticrepositoryidreport_spResultDto"/> to an instance of <see cref="getservicerecommendationbydiagnosticrepositoryidreport_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getservicerecommendationbydiagnosticrepositoryidreport_spResultDto"/> to convert.</param>
        public static getservicerecommendationbydiagnosticrepositoryidreport_spResult ToEntity(this getservicerecommendationbydiagnosticrepositoryidreport_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getservicerecommendationbydiagnosticrepositoryidreport_spResult();

            entity.i_Item = dto.i_Item;
            entity.v_RecommendationId = dto.v_RecommendationId;
            entity.v_DiagnosticRepositoryId = dto.v_DiagnosticRepositoryId;
            entity.v_ServiceId = dto.v_ServiceId;
            entity.v_ComponentId = dto.v_ComponentId;
            entity.v_MasterRecommendationId = dto.v_MasterRecommendationId;
            entity.v_RecommendationName = dto.v_RecommendationName;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getservicerecommendationbydiagnosticrepositoryidreport_spResult"/> to an instance of <see cref="getservicerecommendationbydiagnosticrepositoryidreport_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getservicerecommendationbydiagnosticrepositoryidreport_spResult"/> to convert.</param>
        public static getservicerecommendationbydiagnosticrepositoryidreport_spResultDto ToDTO(this getservicerecommendationbydiagnosticrepositoryidreport_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getservicerecommendationbydiagnosticrepositoryidreport_spResultDto();

            dto.i_Item = entity.i_Item;
            dto.v_RecommendationId = entity.v_RecommendationId;
            dto.v_DiagnosticRepositoryId = entity.v_DiagnosticRepositoryId;
            dto.v_ServiceId = entity.v_ServiceId;
            dto.v_ComponentId = entity.v_ComponentId;
            dto.v_MasterRecommendationId = entity.v_MasterRecommendationId;
            dto.v_RecommendationName = entity.v_RecommendationName;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getservicerecommendationbydiagnosticrepositoryidreport_spResultDto"/> to an instance of <see cref="getservicerecommendationbydiagnosticrepositoryidreport_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getservicerecommendationbydiagnosticrepositoryidreport_spResult> ToEntities(this IEnumerable<getservicerecommendationbydiagnosticrepositoryidreport_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getservicerecommendationbydiagnosticrepositoryidreport_spResult"/> to an instance of <see cref="getservicerecommendationbydiagnosticrepositoryidreport_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getservicerecommendationbydiagnosticrepositoryidreport_spResultDto> ToDTOs(this IEnumerable<getservicerecommendationbydiagnosticrepositoryidreport_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
