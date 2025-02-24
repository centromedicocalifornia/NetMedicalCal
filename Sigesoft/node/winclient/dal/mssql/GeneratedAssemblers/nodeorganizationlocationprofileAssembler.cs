//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:21:33
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
    /// Assembler for <see cref="nodeorganizationlocationprofile"/> and <see cref="nodeorganizationlocationprofileDto"/>.
    /// </summary>
    public static partial class nodeorganizationlocationprofileAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="nodeorganizationlocationprofileDto"/> converted from <see cref="nodeorganizationlocationprofile"/>.</param>
        static partial void OnDTO(this nodeorganizationlocationprofile entity, nodeorganizationlocationprofileDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="nodeorganizationlocationprofile"/> converted from <see cref="nodeorganizationlocationprofileDto"/>.</param>
        static partial void OnEntity(this nodeorganizationlocationprofileDto dto, nodeorganizationlocationprofile entity);

        /// <summary>
        /// Converts this instance of <see cref="nodeorganizationlocationprofileDto"/> to an instance of <see cref="nodeorganizationlocationprofile"/>.
        /// </summary>
        /// <param name="dto"><see cref="nodeorganizationlocationprofileDto"/> to convert.</param>
        public static nodeorganizationlocationprofile ToEntity(this nodeorganizationlocationprofileDto dto)
        {
            if (dto == null) return null;

            var entity = new nodeorganizationlocationprofile();

            entity.i_NodeId = dto.i_NodeId;
            entity.v_OrganizationId = dto.v_OrganizationId;
            entity.v_LocationId = dto.v_LocationId;
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
        /// Converts this instance of <see cref="nodeorganizationlocationprofile"/> to an instance of <see cref="nodeorganizationlocationprofileDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="nodeorganizationlocationprofile"/> to convert.</param>
        public static nodeorganizationlocationprofileDto ToDTO(this nodeorganizationlocationprofile entity)
        {
            if (entity == null) return null;

            var dto = new nodeorganizationlocationprofileDto();

            dto.i_NodeId = entity.i_NodeId;
            dto.v_OrganizationId = entity.v_OrganizationId;
            dto.v_LocationId = entity.v_LocationId;
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
        /// Converts each instance of <see cref="nodeorganizationlocationprofileDto"/> to an instance of <see cref="nodeorganizationlocationprofile"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<nodeorganizationlocationprofile> ToEntities(this IEnumerable<nodeorganizationlocationprofileDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="nodeorganizationlocationprofile"/> to an instance of <see cref="nodeorganizationlocationprofileDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<nodeorganizationlocationprofileDto> ToDTOs(this IEnumerable<nodeorganizationlocationprofile> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
