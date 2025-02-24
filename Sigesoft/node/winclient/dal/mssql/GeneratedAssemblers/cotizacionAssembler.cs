//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:20:19
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
    /// Assembler for <see cref="cotizacion"/> and <see cref="cotizacionDto"/>.
    /// </summary>
    public static partial class cotizacionAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="cotizacionDto"/> converted from <see cref="cotizacion"/>.</param>
        static partial void OnDTO(this cotizacion entity, cotizacionDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="cotizacion"/> converted from <see cref="cotizacionDto"/>.</param>
        static partial void OnEntity(this cotizacionDto dto, cotizacion entity);

        /// <summary>
        /// Converts this instance of <see cref="cotizacionDto"/> to an instance of <see cref="cotizacion"/>.
        /// </summary>
        /// <param name="dto"><see cref="cotizacionDto"/> to convert.</param>
        public static cotizacion ToEntity(this cotizacionDto dto)
        {
            if (dto == null) return null;

            var entity = new cotizacion();

            entity.v_CotizacionId = dto.v_CotizacionId;
            entity.v_PersonId = dto.v_PersonId;
            entity.v_ProtocolId = dto.v_ProtocolId;
            entity.d_CostoTotal = dto.d_CostoTotal;
            entity.d_aCuenta = dto.d_aCuenta;
            entity.d_Saldo = dto.d_Saldo;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.i_Procesado = dto.i_Procesado;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="cotizacion"/> to an instance of <see cref="cotizacionDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="cotizacion"/> to convert.</param>
        public static cotizacionDto ToDTO(this cotizacion entity)
        {
            if (entity == null) return null;

            var dto = new cotizacionDto();

            dto.v_CotizacionId = entity.v_CotizacionId;
            dto.v_PersonId = entity.v_PersonId;
            dto.v_ProtocolId = entity.v_ProtocolId;
            dto.d_CostoTotal = entity.d_CostoTotal;
            dto.d_aCuenta = entity.d_aCuenta;
            dto.d_Saldo = entity.d_Saldo;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.i_Procesado = entity.i_Procesado;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="cotizacionDto"/> to an instance of <see cref="cotizacion"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<cotizacion> ToEntities(this IEnumerable<cotizacionDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="cotizacion"/> to an instance of <see cref="cotizacionDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<cotizacionDto> ToDTOs(this IEnumerable<cotizacion> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
