//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:17:00
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
    /// Assembler for <see cref="getpersonmedicalhistorypagedandfilteredbypersonid_spResult"/> and <see cref="getpersonmedicalhistorypagedandfilteredbypersonid_spResultDto"/>.
    /// </summary>
    public static partial class getpersonmedicalhistorypagedandfilteredbypersonid_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getpersonmedicalhistorypagedandfilteredbypersonid_spResultDto"/> converted from <see cref="getpersonmedicalhistorypagedandfilteredbypersonid_spResult"/>.</param>
        static partial void OnDTO(this getpersonmedicalhistorypagedandfilteredbypersonid_spResult entity, getpersonmedicalhistorypagedandfilteredbypersonid_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getpersonmedicalhistorypagedandfilteredbypersonid_spResult"/> converted from <see cref="getpersonmedicalhistorypagedandfilteredbypersonid_spResultDto"/>.</param>
        static partial void OnEntity(this getpersonmedicalhistorypagedandfilteredbypersonid_spResultDto dto, getpersonmedicalhistorypagedandfilteredbypersonid_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getpersonmedicalhistorypagedandfilteredbypersonid_spResultDto"/> to an instance of <see cref="getpersonmedicalhistorypagedandfilteredbypersonid_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getpersonmedicalhistorypagedandfilteredbypersonid_spResultDto"/> to convert.</param>
        public static getpersonmedicalhistorypagedandfilteredbypersonid_spResult ToEntity(this getpersonmedicalhistorypagedandfilteredbypersonid_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getpersonmedicalhistorypagedandfilteredbypersonid_spResult();

            entity.v_PersonMedicalHistoryId = dto.v_PersonMedicalHistoryId;
            entity.v_PersonId = dto.v_PersonId;
            entity.v_DiseasesId = dto.v_DiseasesId;
            entity.v_DiseasesName = dto.v_DiseasesName;
            entity.i_TypeDiagnosticId = dto.i_TypeDiagnosticId;
            entity.d_StartDate = dto.d_StartDate;
            entity.v_TreatmentSite = dto.v_TreatmentSite;
            entity.i_RecordStatus = dto.i_RecordStatus;
            entity.i_RecordType = dto.i_RecordType;
            entity.v_GroupName = dto.v_GroupName;
            entity.v_TypeDiagnosticName = dto.v_TypeDiagnosticName;
            entity.v_DiagnosticDetail = dto.v_DiagnosticDetail;
            entity.v_CreationUser = dto.v_CreationUser;
            entity.v_UpdateUser = dto.v_UpdateUser;
            entity.d_CreationDate = dto.d_CreationDate;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.i_Answer = dto.i_Answer;
            entity.NombreHospital = dto.NombreHospital;
            entity.v_Complicaciones = dto.v_Complicaciones;
            entity.v_CIE10Id = dto.v_CIE10Id;
            entity.v_Tratamiento = dto.v_Tratamiento;
            entity.v_Comentarios = dto.v_Comentarios;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getpersonmedicalhistorypagedandfilteredbypersonid_spResult"/> to an instance of <see cref="getpersonmedicalhistorypagedandfilteredbypersonid_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getpersonmedicalhistorypagedandfilteredbypersonid_spResult"/> to convert.</param>
        public static getpersonmedicalhistorypagedandfilteredbypersonid_spResultDto ToDTO(this getpersonmedicalhistorypagedandfilteredbypersonid_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getpersonmedicalhistorypagedandfilteredbypersonid_spResultDto();

            dto.v_PersonMedicalHistoryId = entity.v_PersonMedicalHistoryId;
            dto.v_PersonId = entity.v_PersonId;
            dto.v_DiseasesId = entity.v_DiseasesId;
            dto.v_DiseasesName = entity.v_DiseasesName;
            dto.i_TypeDiagnosticId = entity.i_TypeDiagnosticId;
            dto.d_StartDate = entity.d_StartDate;
            dto.v_TreatmentSite = entity.v_TreatmentSite;
            dto.i_RecordStatus = entity.i_RecordStatus;
            dto.i_RecordType = entity.i_RecordType;
            dto.v_GroupName = entity.v_GroupName;
            dto.v_TypeDiagnosticName = entity.v_TypeDiagnosticName;
            dto.v_DiagnosticDetail = entity.v_DiagnosticDetail;
            dto.v_CreationUser = entity.v_CreationUser;
            dto.v_UpdateUser = entity.v_UpdateUser;
            dto.d_CreationDate = entity.d_CreationDate;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.i_Answer = entity.i_Answer;
            dto.NombreHospital = entity.NombreHospital;
            dto.v_Complicaciones = entity.v_Complicaciones;
            dto.v_CIE10Id = entity.v_CIE10Id;
            dto.v_Tratamiento = entity.v_Tratamiento;
            dto.v_Comentarios = entity.v_Comentarios;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getpersonmedicalhistorypagedandfilteredbypersonid_spResultDto"/> to an instance of <see cref="getpersonmedicalhistorypagedandfilteredbypersonid_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getpersonmedicalhistorypagedandfilteredbypersonid_spResult> ToEntities(this IEnumerable<getpersonmedicalhistorypagedandfilteredbypersonid_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getpersonmedicalhistorypagedandfilteredbypersonid_spResult"/> to an instance of <see cref="getpersonmedicalhistorypagedandfilteredbypersonid_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getpersonmedicalhistorypagedandfilteredbypersonid_spResultDto> ToDTOs(this IEnumerable<getpersonmedicalhistorypagedandfilteredbypersonid_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
