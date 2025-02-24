//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:23:39
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
    /// Assembler for <see cref="servicecomponentfieldvalues"/> and <see cref="servicecomponentfieldvaluesDto"/>.
    /// </summary>
    public static partial class servicecomponentfieldvaluesAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="servicecomponentfieldvaluesDto"/> converted from <see cref="servicecomponentfieldvalues"/>.</param>
        static partial void OnDTO(this servicecomponentfieldvalues entity, servicecomponentfieldvaluesDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="servicecomponentfieldvalues"/> converted from <see cref="servicecomponentfieldvaluesDto"/>.</param>
        static partial void OnEntity(this servicecomponentfieldvaluesDto dto, servicecomponentfieldvalues entity);

        /// <summary>
        /// Converts this instance of <see cref="servicecomponentfieldvaluesDto"/> to an instance of <see cref="servicecomponentfieldvalues"/>.
        /// </summary>
        /// <param name="dto"><see cref="servicecomponentfieldvaluesDto"/> to convert.</param>
        public static servicecomponentfieldvalues ToEntity(this servicecomponentfieldvaluesDto dto)
        {
            if (dto == null) return null;

            var entity = new servicecomponentfieldvalues();

            entity.v_ServiceComponentFieldValuesId = dto.v_ServiceComponentFieldValuesId;
            entity.v_ComponentFieldValuesId = dto.v_ComponentFieldValuesId;
            entity.v_ServiceComponentFieldsId = dto.v_ServiceComponentFieldsId;
            entity.v_Value1 = dto.v_Value1;
            entity.v_Value2 = dto.v_Value2;
            entity.i_Index = dto.i_Index;
            entity.i_Value1 = dto.i_Value1;
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
        /// Converts this instance of <see cref="servicecomponentfieldvalues"/> to an instance of <see cref="servicecomponentfieldvaluesDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="servicecomponentfieldvalues"/> to convert.</param>
        public static servicecomponentfieldvaluesDto ToDTO(this servicecomponentfieldvalues entity)
        {
            if (entity == null) return null;

            var dto = new servicecomponentfieldvaluesDto();

            dto.v_ServiceComponentFieldValuesId = entity.v_ServiceComponentFieldValuesId;
            dto.v_ComponentFieldValuesId = entity.v_ComponentFieldValuesId;
            dto.v_ServiceComponentFieldsId = entity.v_ServiceComponentFieldsId;
            dto.v_Value1 = entity.v_Value1;
            dto.v_Value2 = entity.v_Value2;
            dto.i_Index = entity.i_Index;
            dto.i_Value1 = entity.i_Value1;
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
        /// Converts each instance of <see cref="servicecomponentfieldvaluesDto"/> to an instance of <see cref="servicecomponentfieldvalues"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<servicecomponentfieldvalues> ToEntities(this IEnumerable<servicecomponentfieldvaluesDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="servicecomponentfieldvalues"/> to an instance of <see cref="servicecomponentfieldvaluesDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<servicecomponentfieldvaluesDto> ToDTOs(this IEnumerable<servicecomponentfieldvalues> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
