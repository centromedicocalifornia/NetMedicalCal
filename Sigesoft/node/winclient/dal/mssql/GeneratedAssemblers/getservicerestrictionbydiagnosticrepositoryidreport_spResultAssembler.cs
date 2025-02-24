//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:18:09
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
    /// Assembler for <see cref="getservicerestrictionbydiagnosticrepositoryidreport_spResult"/> and <see cref="getservicerestrictionbydiagnosticrepositoryidreport_spResultDto"/>.
    /// </summary>
    public static partial class getservicerestrictionbydiagnosticrepositoryidreport_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getservicerestrictionbydiagnosticrepositoryidreport_spResultDto"/> converted from <see cref="getservicerestrictionbydiagnosticrepositoryidreport_spResult"/>.</param>
        static partial void OnDTO(this getservicerestrictionbydiagnosticrepositoryidreport_spResult entity, getservicerestrictionbydiagnosticrepositoryidreport_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getservicerestrictionbydiagnosticrepositoryidreport_spResult"/> converted from <see cref="getservicerestrictionbydiagnosticrepositoryidreport_spResultDto"/>.</param>
        static partial void OnEntity(this getservicerestrictionbydiagnosticrepositoryidreport_spResultDto dto, getservicerestrictionbydiagnosticrepositoryidreport_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getservicerestrictionbydiagnosticrepositoryidreport_spResultDto"/> to an instance of <see cref="getservicerestrictionbydiagnosticrepositoryidreport_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getservicerestrictionbydiagnosticrepositoryidreport_spResultDto"/> to convert.</param>
        public static getservicerestrictionbydiagnosticrepositoryidreport_spResult ToEntity(this getservicerestrictionbydiagnosticrepositoryidreport_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getservicerestrictionbydiagnosticrepositoryidreport_spResult();

            entity.i_Item = dto.i_Item;
            entity.v_RestrictionId = dto.v_RestrictionId;
            entity.v_DiagnosticRepositoryId = dto.v_DiagnosticRepositoryId;
            entity.v_ServiceId = dto.v_ServiceId;
            entity.v_ComponentId = dto.v_ComponentId;
            entity.v_MasterRestrictionId = dto.v_MasterRestrictionId;
            entity.v_RestrictionName = dto.v_RestrictionName;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getservicerestrictionbydiagnosticrepositoryidreport_spResult"/> to an instance of <see cref="getservicerestrictionbydiagnosticrepositoryidreport_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getservicerestrictionbydiagnosticrepositoryidreport_spResult"/> to convert.</param>
        public static getservicerestrictionbydiagnosticrepositoryidreport_spResultDto ToDTO(this getservicerestrictionbydiagnosticrepositoryidreport_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getservicerestrictionbydiagnosticrepositoryidreport_spResultDto();

            dto.i_Item = entity.i_Item;
            dto.v_RestrictionId = entity.v_RestrictionId;
            dto.v_DiagnosticRepositoryId = entity.v_DiagnosticRepositoryId;
            dto.v_ServiceId = entity.v_ServiceId;
            dto.v_ComponentId = entity.v_ComponentId;
            dto.v_MasterRestrictionId = entity.v_MasterRestrictionId;
            dto.v_RestrictionName = entity.v_RestrictionName;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getservicerestrictionbydiagnosticrepositoryidreport_spResultDto"/> to an instance of <see cref="getservicerestrictionbydiagnosticrepositoryidreport_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getservicerestrictionbydiagnosticrepositoryidreport_spResult> ToEntities(this IEnumerable<getservicerestrictionbydiagnosticrepositoryidreport_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getservicerestrictionbydiagnosticrepositoryidreport_spResult"/> to an instance of <see cref="getservicerestrictionbydiagnosticrepositoryidreport_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getservicerestrictionbydiagnosticrepositoryidreport_spResultDto> ToDTOs(this IEnumerable<getservicerestrictionbydiagnosticrepositoryidreport_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
