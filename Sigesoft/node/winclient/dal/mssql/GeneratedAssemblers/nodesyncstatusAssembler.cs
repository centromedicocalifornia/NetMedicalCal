//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:21:40
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
    /// Assembler for <see cref="nodesyncstatus"/> and <see cref="nodesyncstatusDto"/>.
    /// </summary>
    public static partial class nodesyncstatusAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="nodesyncstatusDto"/> converted from <see cref="nodesyncstatus"/>.</param>
        static partial void OnDTO(this nodesyncstatus entity, nodesyncstatusDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="nodesyncstatus"/> converted from <see cref="nodesyncstatusDto"/>.</param>
        static partial void OnEntity(this nodesyncstatusDto dto, nodesyncstatus entity);

        /// <summary>
        /// Converts this instance of <see cref="nodesyncstatusDto"/> to an instance of <see cref="nodesyncstatus"/>.
        /// </summary>
        /// <param name="dto"><see cref="nodesyncstatusDto"/> to convert.</param>
        public static nodesyncstatus ToEntity(this nodesyncstatusDto dto)
        {
            if (dto == null) return null;

            var entity = new nodesyncstatus();

            entity.i_NodeId = dto.i_NodeId;
            entity.v_DataSyncVersion = dto.v_DataSyncVersion;
            entity.i_DataSyncFrecuency = dto.i_DataSyncFrecuency;
            entity.d_LastSuccessfulDataSync = dto.d_LastSuccessfulDataSync;
            entity.i_LastServerProcessStatus = dto.i_LastServerProcessStatus;
            entity.i_LastNodeProcessStatus = dto.i_LastNodeProcessStatus;
            entity.d_NextDataSync = dto.d_NextDataSync;
            entity.v_LastServerPackageFileName = dto.v_LastServerPackageFileName;
            entity.v_LastServerProcessErrorMessage = dto.v_LastServerProcessErrorMessage;
            entity.v_LastNodePackageFileName = dto.v_LastNodePackageFileName;
            entity.v_LastNodeProcessErrorMessage = dto.v_LastNodeProcessErrorMessage;
            entity.v_ComentaryUpdate = dto.v_ComentaryUpdate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="nodesyncstatus"/> to an instance of <see cref="nodesyncstatusDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="nodesyncstatus"/> to convert.</param>
        public static nodesyncstatusDto ToDTO(this nodesyncstatus entity)
        {
            if (entity == null) return null;

            var dto = new nodesyncstatusDto();

            dto.i_NodeId = entity.i_NodeId;
            dto.v_DataSyncVersion = entity.v_DataSyncVersion;
            dto.i_DataSyncFrecuency = entity.i_DataSyncFrecuency;
            dto.d_LastSuccessfulDataSync = entity.d_LastSuccessfulDataSync;
            dto.i_LastServerProcessStatus = entity.i_LastServerProcessStatus;
            dto.i_LastNodeProcessStatus = entity.i_LastNodeProcessStatus;
            dto.d_NextDataSync = entity.d_NextDataSync;
            dto.v_LastServerPackageFileName = entity.v_LastServerPackageFileName;
            dto.v_LastServerProcessErrorMessage = entity.v_LastServerProcessErrorMessage;
            dto.v_LastNodePackageFileName = entity.v_LastNodePackageFileName;
            dto.v_LastNodeProcessErrorMessage = entity.v_LastNodeProcessErrorMessage;
            dto.v_ComentaryUpdate = entity.v_ComentaryUpdate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="nodesyncstatusDto"/> to an instance of <see cref="nodesyncstatus"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<nodesyncstatus> ToEntities(this IEnumerable<nodesyncstatusDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="nodesyncstatus"/> to an instance of <see cref="nodesyncstatusDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<nodesyncstatusDto> ToDTOs(this IEnumerable<nodesyncstatus> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
