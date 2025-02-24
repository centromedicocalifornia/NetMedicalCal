//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:16:03
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
    /// Assembler for <see cref="getaudiometriacoimolache_spResult"/> and <see cref="getaudiometriacoimolache_spResultDto"/>.
    /// </summary>
    public static partial class getaudiometriacoimolache_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getaudiometriacoimolache_spResultDto"/> converted from <see cref="getaudiometriacoimolache_spResult"/>.</param>
        static partial void OnDTO(this getaudiometriacoimolache_spResult entity, getaudiometriacoimolache_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getaudiometriacoimolache_spResult"/> converted from <see cref="getaudiometriacoimolache_spResultDto"/>.</param>
        static partial void OnEntity(this getaudiometriacoimolache_spResultDto dto, getaudiometriacoimolache_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getaudiometriacoimolache_spResultDto"/> to an instance of <see cref="getaudiometriacoimolache_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getaudiometriacoimolache_spResultDto"/> to convert.</param>
        public static getaudiometriacoimolache_spResult ToEntity(this getaudiometriacoimolache_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getaudiometriacoimolache_spResult();

            entity.FECHA = dto.FECHA;
            entity.EMP_CLIENTE = dto.EMP_CLIENTE;
            entity.EMP_CONTRATISTA = dto.EMP_CONTRATISTA;
            entity.NOMBRE_PACIENTE = dto.NOMBRE_PACIENTE;
            entity.GENERO = dto.GENERO;
            entity.PUESTO = dto.PUESTO;
            entity.FECHA_NACIMIENTO = dto.FECHA_NACIMIENTO;
            entity.FIRMA_TECNICO = dto.FIRMA_TECNICO;
            entity.FIRMA_MEDICO = dto.FIRMA_MEDICO;
            entity.FIRMA_PACIENTE = dto.FIRMA_PACIENTE;
            entity.Huella_PACIENTE = dto.Huella_PACIENTE;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getaudiometriacoimolache_spResult"/> to an instance of <see cref="getaudiometriacoimolache_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getaudiometriacoimolache_spResult"/> to convert.</param>
        public static getaudiometriacoimolache_spResultDto ToDTO(this getaudiometriacoimolache_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getaudiometriacoimolache_spResultDto();

            dto.FECHA = entity.FECHA;
            dto.EMP_CLIENTE = entity.EMP_CLIENTE;
            dto.EMP_CONTRATISTA = entity.EMP_CONTRATISTA;
            dto.NOMBRE_PACIENTE = entity.NOMBRE_PACIENTE;
            dto.GENERO = entity.GENERO;
            dto.PUESTO = entity.PUESTO;
            dto.FECHA_NACIMIENTO = entity.FECHA_NACIMIENTO;
            dto.FIRMA_TECNICO = entity.FIRMA_TECNICO;
            dto.FIRMA_MEDICO = entity.FIRMA_MEDICO;
            dto.FIRMA_PACIENTE = entity.FIRMA_PACIENTE;
            dto.Huella_PACIENTE = entity.Huella_PACIENTE;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getaudiometriacoimolache_spResultDto"/> to an instance of <see cref="getaudiometriacoimolache_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getaudiometriacoimolache_spResult> ToEntities(this IEnumerable<getaudiometriacoimolache_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getaudiometriacoimolache_spResult"/> to an instance of <see cref="getaudiometriacoimolache_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getaudiometriacoimolache_spResultDto> ToDTOs(this IEnumerable<getaudiometriacoimolache_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
