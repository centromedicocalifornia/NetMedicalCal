//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:15:49
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
    /// Assembler for <see cref="devolverhabitos_personales_listaantecedentesfamiliares_spResult"/> and <see cref="devolverhabitos_personales_listaantecedentesfamiliares_spResultDto"/>.
    /// </summary>
    public static partial class devolverhabitos_personales_listaantecedentesfamiliares_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="devolverhabitos_personales_listaantecedentesfamiliares_spResultDto"/> converted from <see cref="devolverhabitos_personales_listaantecedentesfamiliares_spResult"/>.</param>
        static partial void OnDTO(this devolverhabitos_personales_listaantecedentesfamiliares_spResult entity, devolverhabitos_personales_listaantecedentesfamiliares_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="devolverhabitos_personales_listaantecedentesfamiliares_spResult"/> converted from <see cref="devolverhabitos_personales_listaantecedentesfamiliares_spResultDto"/>.</param>
        static partial void OnEntity(this devolverhabitos_personales_listaantecedentesfamiliares_spResultDto dto, devolverhabitos_personales_listaantecedentesfamiliares_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="devolverhabitos_personales_listaantecedentesfamiliares_spResultDto"/> to an instance of <see cref="devolverhabitos_personales_listaantecedentesfamiliares_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="devolverhabitos_personales_listaantecedentesfamiliares_spResultDto"/> to convert.</param>
        public static devolverhabitos_personales_listaantecedentesfamiliares_spResult ToEntity(this devolverhabitos_personales_listaantecedentesfamiliares_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new devolverhabitos_personales_listaantecedentesfamiliares_spResult();

            entity.v_PersonId = dto.v_PersonId;
            entity.v_DiseaseName = dto.v_DiseaseName;
            entity.v_TypeFamilyName = dto.v_TypeFamilyName;
            entity.v_Comment = dto.v_Comment;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="devolverhabitos_personales_listaantecedentesfamiliares_spResult"/> to an instance of <see cref="devolverhabitos_personales_listaantecedentesfamiliares_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="devolverhabitos_personales_listaantecedentesfamiliares_spResult"/> to convert.</param>
        public static devolverhabitos_personales_listaantecedentesfamiliares_spResultDto ToDTO(this devolverhabitos_personales_listaantecedentesfamiliares_spResult entity)
        {
            if (entity == null) return null;

            var dto = new devolverhabitos_personales_listaantecedentesfamiliares_spResultDto();

            dto.v_PersonId = entity.v_PersonId;
            dto.v_DiseaseName = entity.v_DiseaseName;
            dto.v_TypeFamilyName = entity.v_TypeFamilyName;
            dto.v_Comment = entity.v_Comment;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="devolverhabitos_personales_listaantecedentesfamiliares_spResultDto"/> to an instance of <see cref="devolverhabitos_personales_listaantecedentesfamiliares_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<devolverhabitos_personales_listaantecedentesfamiliares_spResult> ToEntities(this IEnumerable<devolverhabitos_personales_listaantecedentesfamiliares_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="devolverhabitos_personales_listaantecedentesfamiliares_spResult"/> to an instance of <see cref="devolverhabitos_personales_listaantecedentesfamiliares_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<devolverhabitos_personales_listaantecedentesfamiliares_spResultDto> ToDTOs(this IEnumerable<devolverhabitos_personales_listaantecedentesfamiliares_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
