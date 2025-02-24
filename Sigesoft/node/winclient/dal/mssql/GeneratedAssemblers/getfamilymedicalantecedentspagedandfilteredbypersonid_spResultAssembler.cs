//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:16:22
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
    /// Assembler for <see cref="getfamilymedicalantecedentspagedandfilteredbypersonid_spResult"/> and <see cref="getfamilymedicalantecedentspagedandfilteredbypersonid_spResultDto"/>.
    /// </summary>
    public static partial class getfamilymedicalantecedentspagedandfilteredbypersonid_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getfamilymedicalantecedentspagedandfilteredbypersonid_spResultDto"/> converted from <see cref="getfamilymedicalantecedentspagedandfilteredbypersonid_spResult"/>.</param>
        static partial void OnDTO(this getfamilymedicalantecedentspagedandfilteredbypersonid_spResult entity, getfamilymedicalantecedentspagedandfilteredbypersonid_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getfamilymedicalantecedentspagedandfilteredbypersonid_spResult"/> converted from <see cref="getfamilymedicalantecedentspagedandfilteredbypersonid_spResultDto"/>.</param>
        static partial void OnEntity(this getfamilymedicalantecedentspagedandfilteredbypersonid_spResultDto dto, getfamilymedicalantecedentspagedandfilteredbypersonid_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getfamilymedicalantecedentspagedandfilteredbypersonid_spResultDto"/> to an instance of <see cref="getfamilymedicalantecedentspagedandfilteredbypersonid_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getfamilymedicalantecedentspagedandfilteredbypersonid_spResultDto"/> to convert.</param>
        public static getfamilymedicalantecedentspagedandfilteredbypersonid_spResult ToEntity(this getfamilymedicalantecedentspagedandfilteredbypersonid_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getfamilymedicalantecedentspagedandfilteredbypersonid_spResult();

            entity.v_FamilyMedicalAntecedentsId = dto.v_FamilyMedicalAntecedentsId;
            entity.v_PersonId = dto.v_PersonId;
            entity.v_DiseasesId = dto.v_DiseasesId;
            entity.v_DiseaseName = dto.v_DiseaseName;
            entity.i_TypeFamilyId = dto.i_TypeFamilyId;
            entity.v_TypeFamilyName = dto.v_TypeFamilyName;
            entity.v_Comment = dto.v_Comment;
            entity.i_RecordStatus = dto.i_RecordStatus;
            entity.i_RecordType = dto.i_RecordType;
            entity.v_CreationUser = dto.v_CreationUser;
            entity.v_UpdateUser = dto.v_UpdateUser;
            entity.d_CreationDate = dto.d_CreationDate;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.i_ParameterId = dto.i_ParameterId;
            entity.i_ParentParameterId = dto.i_ParentParameterId;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getfamilymedicalantecedentspagedandfilteredbypersonid_spResult"/> to an instance of <see cref="getfamilymedicalantecedentspagedandfilteredbypersonid_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getfamilymedicalantecedentspagedandfilteredbypersonid_spResult"/> to convert.</param>
        public static getfamilymedicalantecedentspagedandfilteredbypersonid_spResultDto ToDTO(this getfamilymedicalantecedentspagedandfilteredbypersonid_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getfamilymedicalantecedentspagedandfilteredbypersonid_spResultDto();

            dto.v_FamilyMedicalAntecedentsId = entity.v_FamilyMedicalAntecedentsId;
            dto.v_PersonId = entity.v_PersonId;
            dto.v_DiseasesId = entity.v_DiseasesId;
            dto.v_DiseaseName = entity.v_DiseaseName;
            dto.i_TypeFamilyId = entity.i_TypeFamilyId;
            dto.v_TypeFamilyName = entity.v_TypeFamilyName;
            dto.v_Comment = entity.v_Comment;
            dto.i_RecordStatus = entity.i_RecordStatus;
            dto.i_RecordType = entity.i_RecordType;
            dto.v_CreationUser = entity.v_CreationUser;
            dto.v_UpdateUser = entity.v_UpdateUser;
            dto.d_CreationDate = entity.d_CreationDate;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.i_ParameterId = entity.i_ParameterId;
            dto.i_ParentParameterId = entity.i_ParentParameterId;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getfamilymedicalantecedentspagedandfilteredbypersonid_spResultDto"/> to an instance of <see cref="getfamilymedicalantecedentspagedandfilteredbypersonid_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getfamilymedicalantecedentspagedandfilteredbypersonid_spResult> ToEntities(this IEnumerable<getfamilymedicalantecedentspagedandfilteredbypersonid_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getfamilymedicalantecedentspagedandfilteredbypersonid_spResult"/> to an instance of <see cref="getfamilymedicalantecedentspagedandfilteredbypersonid_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getfamilymedicalantecedentspagedandfilteredbypersonid_spResultDto> ToDTOs(this IEnumerable<getfamilymedicalantecedentspagedandfilteredbypersonid_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
