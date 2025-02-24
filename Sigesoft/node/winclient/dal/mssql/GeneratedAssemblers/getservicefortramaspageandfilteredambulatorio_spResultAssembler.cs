//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:18:00
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
    /// Assembler for <see cref="getservicefortramaspageandfilteredambulatorio_spResult"/> and <see cref="getservicefortramaspageandfilteredambulatorio_spResultDto"/>.
    /// </summary>
    public static partial class getservicefortramaspageandfilteredambulatorio_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getservicefortramaspageandfilteredambulatorio_spResultDto"/> converted from <see cref="getservicefortramaspageandfilteredambulatorio_spResult"/>.</param>
        static partial void OnDTO(this getservicefortramaspageandfilteredambulatorio_spResult entity, getservicefortramaspageandfilteredambulatorio_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getservicefortramaspageandfilteredambulatorio_spResult"/> converted from <see cref="getservicefortramaspageandfilteredambulatorio_spResultDto"/>.</param>
        static partial void OnEntity(this getservicefortramaspageandfilteredambulatorio_spResultDto dto, getservicefortramaspageandfilteredambulatorio_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getservicefortramaspageandfilteredambulatorio_spResultDto"/> to an instance of <see cref="getservicefortramaspageandfilteredambulatorio_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getservicefortramaspageandfilteredambulatorio_spResultDto"/> to convert.</param>
        public static getservicefortramaspageandfilteredambulatorio_spResult ToEntity(this getservicefortramaspageandfilteredambulatorio_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getservicefortramaspageandfilteredambulatorio_spResult();

            entity.v_ServiceId = dto.v_ServiceId;
            entity.nombre = dto.nombre;
            entity.genero = dto.genero;
            entity.fechaservicio = dto.fechaservicio;
            entity.edad = dto.edad;
            entity.d_BirthDate = dto.d_BirthDate;
            entity.tipoServicio = dto.tipoServicio;
            entity.i_TramaCargada = dto.i_TramaCargada;
            entity.Medico = dto.Medico;
            entity.CIE_10 = dto.CIE_10;
            entity.Diagnostico = dto.Diagnostico;
            entity.Value1 = dto.Value1;
            entity.Value2 = dto.Value2;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getservicefortramaspageandfilteredambulatorio_spResult"/> to an instance of <see cref="getservicefortramaspageandfilteredambulatorio_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getservicefortramaspageandfilteredambulatorio_spResult"/> to convert.</param>
        public static getservicefortramaspageandfilteredambulatorio_spResultDto ToDTO(this getservicefortramaspageandfilteredambulatorio_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getservicefortramaspageandfilteredambulatorio_spResultDto();

            dto.v_ServiceId = entity.v_ServiceId;
            dto.nombre = entity.nombre;
            dto.genero = entity.genero;
            dto.fechaservicio = entity.fechaservicio;
            dto.edad = entity.edad;
            dto.d_BirthDate = entity.d_BirthDate;
            dto.tipoServicio = entity.tipoServicio;
            dto.i_TramaCargada = entity.i_TramaCargada;
            dto.Medico = entity.Medico;
            dto.CIE_10 = entity.CIE_10;
            dto.Diagnostico = entity.Diagnostico;
            dto.Value1 = entity.Value1;
            dto.Value2 = entity.Value2;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getservicefortramaspageandfilteredambulatorio_spResultDto"/> to an instance of <see cref="getservicefortramaspageandfilteredambulatorio_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getservicefortramaspageandfilteredambulatorio_spResult> ToEntities(this IEnumerable<getservicefortramaspageandfilteredambulatorio_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getservicefortramaspageandfilteredambulatorio_spResult"/> to an instance of <see cref="getservicefortramaspageandfilteredambulatorio_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getservicefortramaspageandfilteredambulatorio_spResultDto> ToDTOs(this IEnumerable<getservicefortramaspageandfilteredambulatorio_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
