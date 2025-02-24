//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:22:35
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
    /// Assembler for <see cref="productwarehouse"/> and <see cref="productwarehouseDto"/>.
    /// </summary>
    public static partial class productwarehouseAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="productwarehouseDto"/> converted from <see cref="productwarehouse"/>.</param>
        static partial void OnDTO(this productwarehouse entity, productwarehouseDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="productwarehouse"/> converted from <see cref="productwarehouseDto"/>.</param>
        static partial void OnEntity(this productwarehouseDto dto, productwarehouse entity);

        /// <summary>
        /// Converts this instance of <see cref="productwarehouseDto"/> to an instance of <see cref="productwarehouse"/>.
        /// </summary>
        /// <param name="dto"><see cref="productwarehouseDto"/> to convert.</param>
        public static productwarehouse ToEntity(this productwarehouseDto dto)
        {
            if (dto == null) return null;

            var entity = new productwarehouse();

            entity.v_WarehouseId = dto.v_WarehouseId;
            entity.v_ProductId = dto.v_ProductId;
            entity.r_StockMin = dto.r_StockMin;
            entity.r_StockMax = dto.r_StockMax;
            entity.r_StockActual = dto.r_StockActual;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.v_ComentaryUpdate = dto.v_ComentaryUpdate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="productwarehouse"/> to an instance of <see cref="productwarehouseDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="productwarehouse"/> to convert.</param>
        public static productwarehouseDto ToDTO(this productwarehouse entity)
        {
            if (entity == null) return null;

            var dto = new productwarehouseDto();

            dto.v_WarehouseId = entity.v_WarehouseId;
            dto.v_ProductId = entity.v_ProductId;
            dto.r_StockMin = entity.r_StockMin;
            dto.r_StockMax = entity.r_StockMax;
            dto.r_StockActual = entity.r_StockActual;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.v_ComentaryUpdate = entity.v_ComentaryUpdate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="productwarehouseDto"/> to an instance of <see cref="productwarehouse"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<productwarehouse> ToEntities(this IEnumerable<productwarehouseDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="productwarehouse"/> to an instance of <see cref="productwarehouseDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<productwarehouseDto> ToDTOs(this IEnumerable<productwarehouse> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
