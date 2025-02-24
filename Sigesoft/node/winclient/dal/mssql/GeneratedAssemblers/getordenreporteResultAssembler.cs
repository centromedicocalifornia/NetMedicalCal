//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:16:50
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
    /// Assembler for <see cref="getordenreporteResult"/> and <see cref="getordenreporteResultDto"/>.
    /// </summary>
    public static partial class getordenreporteResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getordenreporteResultDto"/> converted from <see cref="getordenreporteResult"/>.</param>
        static partial void OnDTO(this getordenreporteResult entity, getordenreporteResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getordenreporteResult"/> converted from <see cref="getordenreporteResultDto"/>.</param>
        static partial void OnEntity(this getordenreporteResultDto dto, getordenreporteResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getordenreporteResultDto"/> to an instance of <see cref="getordenreporteResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getordenreporteResultDto"/> to convert.</param>
        public static getordenreporteResult ToEntity(this getordenreporteResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getordenreporteResult();

            entity.v_ComponenteId = dto.v_ComponenteId;
            entity.v_NombreReporte = dto.v_NombreReporte;
            entity.i_Orden = dto.i_Orden;
            entity.v_NombreCrystal = dto.v_NombreCrystal;
            entity.i_NombreCrystalId = dto.i_NombreCrystalId;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getordenreporteResult"/> to an instance of <see cref="getordenreporteResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getordenreporteResult"/> to convert.</param>
        public static getordenreporteResultDto ToDTO(this getordenreporteResult entity)
        {
            if (entity == null) return null;

            var dto = new getordenreporteResultDto();

            dto.v_ComponenteId = entity.v_ComponenteId;
            dto.v_NombreReporte = entity.v_NombreReporte;
            dto.i_Orden = entity.i_Orden;
            dto.v_NombreCrystal = entity.v_NombreCrystal;
            dto.i_NombreCrystalId = entity.i_NombreCrystalId;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getordenreporteResultDto"/> to an instance of <see cref="getordenreporteResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getordenreporteResult> ToEntities(this IEnumerable<getordenreporteResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getordenreporteResult"/> to an instance of <see cref="getordenreporteResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getordenreporteResultDto> ToDTOs(this IEnumerable<getordenreporteResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
