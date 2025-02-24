//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:17:26
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
    /// Assembler for <see cref="getreportodontograma_spResult"/> and <see cref="getreportodontograma_spResultDto"/>.
    /// </summary>
    public static partial class getreportodontograma_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getreportodontograma_spResultDto"/> converted from <see cref="getreportodontograma_spResult"/>.</param>
        static partial void OnDTO(this getreportodontograma_spResult entity, getreportodontograma_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getreportodontograma_spResult"/> converted from <see cref="getreportodontograma_spResultDto"/>.</param>
        static partial void OnEntity(this getreportodontograma_spResultDto dto, getreportodontograma_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getreportodontograma_spResultDto"/> to an instance of <see cref="getreportodontograma_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getreportodontograma_spResultDto"/> to convert.</param>
        public static getreportodontograma_spResult ToEntity(this getreportodontograma_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getreportodontograma_spResult();

            entity.IdServicio = dto.IdServicio;
            entity.Trabajador = dto.Trabajador;
            entity.Fecha = dto.Fecha;
            entity.Puesto = dto.Puesto;
            entity.Ficha = dto.Ficha;
            entity.FirmaMedico = dto.FirmaMedico;
            entity.FechaCumpleaņos = dto.FechaCumpleaņos;
            entity.Corona = dto.Corona;
            entity.EmpresaPropietaria = dto.EmpresaPropietaria;
            entity.Empresa = dto.Empresa;
            entity.EmpresaPropietariaEmail = dto.EmpresaPropietariaEmail;
            entity.EmpresaPropietariaDireccion = dto.EmpresaPropietariaDireccion;
            entity.NombreUsuarioGraba = dto.NombreUsuarioGraba;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getreportodontograma_spResult"/> to an instance of <see cref="getreportodontograma_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getreportodontograma_spResult"/> to convert.</param>
        public static getreportodontograma_spResultDto ToDTO(this getreportodontograma_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getreportodontograma_spResultDto();

            dto.IdServicio = entity.IdServicio;
            dto.Trabajador = entity.Trabajador;
            dto.Fecha = entity.Fecha;
            dto.Puesto = entity.Puesto;
            dto.Ficha = entity.Ficha;
            dto.FirmaMedico = entity.FirmaMedico;
            dto.FechaCumpleaņos = entity.FechaCumpleaņos;
            dto.Corona = entity.Corona;
            dto.EmpresaPropietaria = entity.EmpresaPropietaria;
            dto.Empresa = entity.Empresa;
            dto.EmpresaPropietariaEmail = entity.EmpresaPropietariaEmail;
            dto.EmpresaPropietariaDireccion = entity.EmpresaPropietariaDireccion;
            dto.NombreUsuarioGraba = entity.NombreUsuarioGraba;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getreportodontograma_spResultDto"/> to an instance of <see cref="getreportodontograma_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getreportodontograma_spResult> ToEntities(this IEnumerable<getreportodontograma_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getreportodontograma_spResult"/> to an instance of <see cref="getreportodontograma_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getreportodontograma_spResultDto> ToDTOs(this IEnumerable<getreportodontograma_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
