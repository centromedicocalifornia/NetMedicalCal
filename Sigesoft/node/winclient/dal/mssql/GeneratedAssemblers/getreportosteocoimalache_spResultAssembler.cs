//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:17:27
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
    /// Assembler for <see cref="getreportosteocoimalache_spResult"/> and <see cref="getreportosteocoimalache_spResultDto"/>.
    /// </summary>
    public static partial class getreportosteocoimalache_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getreportosteocoimalache_spResultDto"/> converted from <see cref="getreportosteocoimalache_spResult"/>.</param>
        static partial void OnDTO(this getreportosteocoimalache_spResult entity, getreportosteocoimalache_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getreportosteocoimalache_spResult"/> converted from <see cref="getreportosteocoimalache_spResultDto"/>.</param>
        static partial void OnEntity(this getreportosteocoimalache_spResultDto dto, getreportosteocoimalache_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getreportosteocoimalache_spResultDto"/> to an instance of <see cref="getreportosteocoimalache_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getreportosteocoimalache_spResultDto"/> to convert.</param>
        public static getreportosteocoimalache_spResult ToEntity(this getreportosteocoimalache_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getreportosteocoimalache_spResult();

            entity.ServiceId = dto.ServiceId;
            entity.ServiceComponentId = dto.ServiceComponentId;
            entity.FechaServicio = dto.FechaServicio;
            entity.Nombres = dto.Nombres;
            entity.ApellidoPaterno = dto.ApellidoPaterno;
            entity.ApellidoMaterno = dto.ApellidoMaterno;
            entity.NombreCompleto = dto.NombreCompleto;
            entity.FechaNacimiento = dto.FechaNacimiento;
            entity.TipoDocumentoId = dto.TipoDocumentoId;
            entity.TipoDocumento = dto.TipoDocumento;
            entity.NroDocumento = dto.NroDocumento;
            entity.EmpresaCliente = dto.EmpresaCliente;
            entity.EmpresaTrabajo = dto.EmpresaTrabajo;
            entity.EmpresaEmpleadora = dto.EmpresaEmpleadora;
            entity.Puesto = dto.Puesto;
            entity.GeneroId = dto.GeneroId;
            entity.Genero = dto.Genero;
            entity.FirmaTrabajador = dto.FirmaTrabajador;
            entity.HuellaTrabajador = dto.HuellaTrabajador;
            entity.FirmaUsuarioGraba = dto.FirmaUsuarioGraba;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getreportosteocoimalache_spResult"/> to an instance of <see cref="getreportosteocoimalache_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getreportosteocoimalache_spResult"/> to convert.</param>
        public static getreportosteocoimalache_spResultDto ToDTO(this getreportosteocoimalache_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getreportosteocoimalache_spResultDto();

            dto.ServiceId = entity.ServiceId;
            dto.ServiceComponentId = entity.ServiceComponentId;
            dto.FechaServicio = entity.FechaServicio;
            dto.Nombres = entity.Nombres;
            dto.ApellidoPaterno = entity.ApellidoPaterno;
            dto.ApellidoMaterno = entity.ApellidoMaterno;
            dto.NombreCompleto = entity.NombreCompleto;
            dto.FechaNacimiento = entity.FechaNacimiento;
            dto.TipoDocumentoId = entity.TipoDocumentoId;
            dto.TipoDocumento = entity.TipoDocumento;
            dto.NroDocumento = entity.NroDocumento;
            dto.EmpresaCliente = entity.EmpresaCliente;
            dto.EmpresaTrabajo = entity.EmpresaTrabajo;
            dto.EmpresaEmpleadora = entity.EmpresaEmpleadora;
            dto.Puesto = entity.Puesto;
            dto.GeneroId = entity.GeneroId;
            dto.Genero = entity.Genero;
            dto.FirmaTrabajador = entity.FirmaTrabajador;
            dto.HuellaTrabajador = entity.HuellaTrabajador;
            dto.FirmaUsuarioGraba = entity.FirmaUsuarioGraba;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getreportosteocoimalache_spResultDto"/> to an instance of <see cref="getreportosteocoimalache_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getreportosteocoimalache_spResult> ToEntities(this IEnumerable<getreportosteocoimalache_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getreportosteocoimalache_spResult"/> to an instance of <see cref="getreportosteocoimalache_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getreportosteocoimalache_spResultDto> ToDTOs(this IEnumerable<getreportosteocoimalache_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
