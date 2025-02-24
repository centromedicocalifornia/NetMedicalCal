//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:22:13
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
    /// Assembler for <see cref="personmedicalhistory"/> and <see cref="personmedicalhistoryDto"/>.
    /// </summary>
    public static partial class personmedicalhistoryAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="personmedicalhistoryDto"/> converted from <see cref="personmedicalhistory"/>.</param>
        static partial void OnDTO(this personmedicalhistory entity, personmedicalhistoryDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="personmedicalhistory"/> converted from <see cref="personmedicalhistoryDto"/>.</param>
        static partial void OnEntity(this personmedicalhistoryDto dto, personmedicalhistory entity);

        /// <summary>
        /// Converts this instance of <see cref="personmedicalhistoryDto"/> to an instance of <see cref="personmedicalhistory"/>.
        /// </summary>
        /// <param name="dto"><see cref="personmedicalhistoryDto"/> to convert.</param>
        public static personmedicalhistory ToEntity(this personmedicalhistoryDto dto)
        {
            if (dto == null) return null;

            var entity = new personmedicalhistory();

            entity.v_PersonMedicalHistoryId = dto.v_PersonMedicalHistoryId;
            entity.v_PersonId = dto.v_PersonId;
            entity.v_DiseasesId = dto.v_DiseasesId;
            entity.i_TypeDiagnosticId = dto.i_TypeDiagnosticId;
            entity.d_StartDate = dto.d_StartDate;
            entity.v_DiagnosticDetail = dto.v_DiagnosticDetail;
            entity.v_TreatmentSite = dto.v_TreatmentSite;
            entity.i_AnswerId = dto.i_AnswerId;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.i_SoloAnio = dto.i_SoloAnio;
            entity.NombreHospital = dto.NombreHospital;
            entity.v_Complicaciones = dto.v_Complicaciones;
            entity.v_ComentaryUpdate = dto.v_ComentaryUpdate;
            entity.v_Tratamiento = dto.v_Tratamiento;
            entity.v_Comentarios = dto.v_Comentarios;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="personmedicalhistory"/> to an instance of <see cref="personmedicalhistoryDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="personmedicalhistory"/> to convert.</param>
        public static personmedicalhistoryDto ToDTO(this personmedicalhistory entity)
        {
            if (entity == null) return null;

            var dto = new personmedicalhistoryDto();

            dto.v_PersonMedicalHistoryId = entity.v_PersonMedicalHistoryId;
            dto.v_PersonId = entity.v_PersonId;
            dto.v_DiseasesId = entity.v_DiseasesId;
            dto.i_TypeDiagnosticId = entity.i_TypeDiagnosticId;
            dto.d_StartDate = entity.d_StartDate;
            dto.v_DiagnosticDetail = entity.v_DiagnosticDetail;
            dto.v_TreatmentSite = entity.v_TreatmentSite;
            dto.i_AnswerId = entity.i_AnswerId;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.i_SoloAnio = entity.i_SoloAnio;
            dto.NombreHospital = entity.NombreHospital;
            dto.v_Complicaciones = entity.v_Complicaciones;
            dto.v_ComentaryUpdate = entity.v_ComentaryUpdate;
            dto.v_Tratamiento = entity.v_Tratamiento;
            dto.v_Comentarios = entity.v_Comentarios;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="personmedicalhistoryDto"/> to an instance of <see cref="personmedicalhistory"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<personmedicalhistory> ToEntities(this IEnumerable<personmedicalhistoryDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="personmedicalhistory"/> to an instance of <see cref="personmedicalhistoryDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<personmedicalhistoryDto> ToDTOs(this IEnumerable<personmedicalhistory> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
