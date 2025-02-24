//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:19:33
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
    /// Assembler for <see cref="resumentipopagoResult"/> and <see cref="resumentipopagoResultDto"/>.
    /// </summary>
    public static partial class resumentipopagoResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="resumentipopagoResultDto"/> converted from <see cref="resumentipopagoResult"/>.</param>
        static partial void OnDTO(this resumentipopagoResult entity, resumentipopagoResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="resumentipopagoResult"/> converted from <see cref="resumentipopagoResultDto"/>.</param>
        static partial void OnEntity(this resumentipopagoResultDto dto, resumentipopagoResult entity);

        /// <summary>
        /// Converts this instance of <see cref="resumentipopagoResultDto"/> to an instance of <see cref="resumentipopagoResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="resumentipopagoResultDto"/> to convert.</param>
        public static resumentipopagoResult ToEntity(this resumentipopagoResultDto dto)
        {
            if (dto == null) return null;

            var entity = new resumentipopagoResult();

            entity.IdCondicionPago = dto.IdCondicionPago;
            entity.CondicionPago = dto.CondicionPago;
            entity.IdFormaPago = dto.IdFormaPago;
            entity.FormaPago = dto.FormaPago;
            entity.Fecha = dto.Fecha;
            entity.Comprobante = dto.Comprobante;
            entity.Empresa = dto.Empresa;
            entity.Importe = dto.Importe;
            entity.v_Concepto = dto.v_Concepto;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="resumentipopagoResult"/> to an instance of <see cref="resumentipopagoResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="resumentipopagoResult"/> to convert.</param>
        public static resumentipopagoResultDto ToDTO(this resumentipopagoResult entity)
        {
            if (entity == null) return null;

            var dto = new resumentipopagoResultDto();

            dto.IdCondicionPago = entity.IdCondicionPago;
            dto.CondicionPago = entity.CondicionPago;
            dto.IdFormaPago = entity.IdFormaPago;
            dto.FormaPago = entity.FormaPago;
            dto.Fecha = entity.Fecha;
            dto.Comprobante = entity.Comprobante;
            dto.Empresa = entity.Empresa;
            dto.Importe = entity.Importe;
            dto.v_Concepto = entity.v_Concepto;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="resumentipopagoResultDto"/> to an instance of <see cref="resumentipopagoResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<resumentipopagoResult> ToEntities(this IEnumerable<resumentipopagoResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="resumentipopagoResult"/> to an instance of <see cref="resumentipopagoResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<resumentipopagoResultDto> ToDTOs(this IEnumerable<resumentipopagoResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
