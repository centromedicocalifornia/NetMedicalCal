//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:18:36
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
    /// Assembler for <see cref="getsystemuserexternalpagedandfiltered_spResult"/> and <see cref="getsystemuserexternalpagedandfiltered_spResultDto"/>.
    /// </summary>
    public static partial class getsystemuserexternalpagedandfiltered_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getsystemuserexternalpagedandfiltered_spResultDto"/> converted from <see cref="getsystemuserexternalpagedandfiltered_spResult"/>.</param>
        static partial void OnDTO(this getsystemuserexternalpagedandfiltered_spResult entity, getsystemuserexternalpagedandfiltered_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getsystemuserexternalpagedandfiltered_spResult"/> converted from <see cref="getsystemuserexternalpagedandfiltered_spResultDto"/>.</param>
        static partial void OnEntity(this getsystemuserexternalpagedandfiltered_spResultDto dto, getsystemuserexternalpagedandfiltered_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getsystemuserexternalpagedandfiltered_spResultDto"/> to an instance of <see cref="getsystemuserexternalpagedandfiltered_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getsystemuserexternalpagedandfiltered_spResultDto"/> to convert.</param>
        public static getsystemuserexternalpagedandfiltered_spResult ToEntity(this getsystemuserexternalpagedandfiltered_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getsystemuserexternalpagedandfiltered_spResult();

            entity.i_SystemUserId = dto.i_SystemUserId;
            entity.v_PersonId = dto.v_PersonId;
            entity.v_UserName = dto.v_UserName;
            entity.v_Password = dto.v_Password;
            entity.v_SecretQuestion = dto.v_SecretQuestion;
            entity.v_SecretAnswer = dto.v_SecretAnswer;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.v_InsertUser = dto.v_InsertUser;
            entity.v_UpdateUser = dto.v_UpdateUser;
            entity.v_PersonName = dto.v_PersonName;
            entity.v_DocNumber = dto.v_DocNumber;
            entity.d_ExpireDate = dto.d_ExpireDate;
            entity.v_ProtocolId = dto.v_ProtocolId;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getsystemuserexternalpagedandfiltered_spResult"/> to an instance of <see cref="getsystemuserexternalpagedandfiltered_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getsystemuserexternalpagedandfiltered_spResult"/> to convert.</param>
        public static getsystemuserexternalpagedandfiltered_spResultDto ToDTO(this getsystemuserexternalpagedandfiltered_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getsystemuserexternalpagedandfiltered_spResultDto();

            dto.i_SystemUserId = entity.i_SystemUserId;
            dto.v_PersonId = entity.v_PersonId;
            dto.v_UserName = entity.v_UserName;
            dto.v_Password = entity.v_Password;
            dto.v_SecretQuestion = entity.v_SecretQuestion;
            dto.v_SecretAnswer = entity.v_SecretAnswer;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.v_InsertUser = entity.v_InsertUser;
            dto.v_UpdateUser = entity.v_UpdateUser;
            dto.v_PersonName = entity.v_PersonName;
            dto.v_DocNumber = entity.v_DocNumber;
            dto.d_ExpireDate = entity.d_ExpireDate;
            dto.v_ProtocolId = entity.v_ProtocolId;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getsystemuserexternalpagedandfiltered_spResultDto"/> to an instance of <see cref="getsystemuserexternalpagedandfiltered_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getsystemuserexternalpagedandfiltered_spResult> ToEntities(this IEnumerable<getsystemuserexternalpagedandfiltered_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getsystemuserexternalpagedandfiltered_spResult"/> to an instance of <see cref="getsystemuserexternalpagedandfiltered_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getsystemuserexternalpagedandfiltered_spResultDto> ToDTOs(this IEnumerable<getsystemuserexternalpagedandfiltered_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
