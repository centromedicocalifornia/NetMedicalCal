//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:17:59
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
    /// Assembler for <see cref="getservicedisgnosticsreports_spResult"/> and <see cref="getservicedisgnosticsreports_spResultDto"/>.
    /// </summary>
    public static partial class getservicedisgnosticsreports_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getservicedisgnosticsreports_spResultDto"/> converted from <see cref="getservicedisgnosticsreports_spResult"/>.</param>
        static partial void OnDTO(this getservicedisgnosticsreports_spResult entity, getservicedisgnosticsreports_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getservicedisgnosticsreports_spResult"/> converted from <see cref="getservicedisgnosticsreports_spResultDto"/>.</param>
        static partial void OnEntity(this getservicedisgnosticsreports_spResultDto dto, getservicedisgnosticsreports_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getservicedisgnosticsreports_spResultDto"/> to an instance of <see cref="getservicedisgnosticsreports_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getservicedisgnosticsreports_spResultDto"/> to convert.</param>
        public static getservicedisgnosticsreports_spResult ToEntity(this getservicedisgnosticsreports_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getservicedisgnosticsreports_spResult();

            entity.v_DiagnosticRepositoryId = dto.v_DiagnosticRepositoryId;
            entity.v_ServiceId = dto.v_ServiceId;
            entity.v_ComponentId = dto.v_ComponentId;
            entity.v_DiseasesId = dto.v_DiseasesId;
            entity.i_AutoManualId = dto.i_AutoManualId;
            entity.i_PreQualificationId = dto.i_PreQualificationId;
            entity.i_FinalQualificationId = dto.i_FinalQualificationId;
            entity.i_DiagnosticTypeId = dto.i_DiagnosticTypeId;
            entity.i_IsSentToAntecedent = dto.i_IsSentToAntecedent;
            entity.d_ExpirationDateDiagnostic = dto.d_ExpirationDateDiagnostic;
            entity.i_GenerateMedicalBreak = dto.i_GenerateMedicalBreak;
            entity.v_ComponentFieldsId = dto.v_ComponentFieldsId;
            entity.v_DiseasesName = dto.v_DiseasesName;
            entity.v_Cie10 = dto.v_Cie10;
            entity.v_AutoManualName = dto.v_AutoManualName;
            entity.v_PreQualificationName = dto.v_PreQualificationName;
            entity.v_FinalQualificationName = dto.v_FinalQualificationName;
            entity.v_DiagnosticTypeName = dto.v_DiagnosticTypeName;
            entity.v_IsSentToAntecedentName = dto.v_IsSentToAntecedentName;
            entity.i_RecordStatus = dto.i_RecordStatus;
            entity.i_RecordType = dto.i_RecordType;
            entity.v_CreationUser = dto.v_CreationUser;
            entity.v_UpdateUser = dto.v_UpdateUser;
            entity.d_CreationDate = dto.d_CreationDate;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_CategoryId = dto.i_CategoryId;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getservicedisgnosticsreports_spResult"/> to an instance of <see cref="getservicedisgnosticsreports_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getservicedisgnosticsreports_spResult"/> to convert.</param>
        public static getservicedisgnosticsreports_spResultDto ToDTO(this getservicedisgnosticsreports_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getservicedisgnosticsreports_spResultDto();

            dto.v_DiagnosticRepositoryId = entity.v_DiagnosticRepositoryId;
            dto.v_ServiceId = entity.v_ServiceId;
            dto.v_ComponentId = entity.v_ComponentId;
            dto.v_DiseasesId = entity.v_DiseasesId;
            dto.i_AutoManualId = entity.i_AutoManualId;
            dto.i_PreQualificationId = entity.i_PreQualificationId;
            dto.i_FinalQualificationId = entity.i_FinalQualificationId;
            dto.i_DiagnosticTypeId = entity.i_DiagnosticTypeId;
            dto.i_IsSentToAntecedent = entity.i_IsSentToAntecedent;
            dto.d_ExpirationDateDiagnostic = entity.d_ExpirationDateDiagnostic;
            dto.i_GenerateMedicalBreak = entity.i_GenerateMedicalBreak;
            dto.v_ComponentFieldsId = entity.v_ComponentFieldsId;
            dto.v_DiseasesName = entity.v_DiseasesName;
            dto.v_Cie10 = entity.v_Cie10;
            dto.v_AutoManualName = entity.v_AutoManualName;
            dto.v_PreQualificationName = entity.v_PreQualificationName;
            dto.v_FinalQualificationName = entity.v_FinalQualificationName;
            dto.v_DiagnosticTypeName = entity.v_DiagnosticTypeName;
            dto.v_IsSentToAntecedentName = entity.v_IsSentToAntecedentName;
            dto.i_RecordStatus = entity.i_RecordStatus;
            dto.i_RecordType = entity.i_RecordType;
            dto.v_CreationUser = entity.v_CreationUser;
            dto.v_UpdateUser = entity.v_UpdateUser;
            dto.d_CreationDate = entity.d_CreationDate;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_CategoryId = entity.i_CategoryId;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getservicedisgnosticsreports_spResultDto"/> to an instance of <see cref="getservicedisgnosticsreports_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getservicedisgnosticsreports_spResult> ToEntities(this IEnumerable<getservicedisgnosticsreports_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getservicedisgnosticsreports_spResult"/> to an instance of <see cref="getservicedisgnosticsreports_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getservicedisgnosticsreports_spResultDto> ToDTOs(this IEnumerable<getservicedisgnosticsreports_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
