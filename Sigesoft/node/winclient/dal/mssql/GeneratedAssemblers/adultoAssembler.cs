//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:19:44
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
    /// Assembler for <see cref="adulto"/> and <see cref="adultoDto"/>.
    /// </summary>
    public static partial class adultoAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="adultoDto"/> converted from <see cref="adulto"/>.</param>
        static partial void OnDTO(this adulto entity, adultoDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="adulto"/> converted from <see cref="adultoDto"/>.</param>
        static partial void OnEntity(this adultoDto dto, adulto entity);

        /// <summary>
        /// Converts this instance of <see cref="adultoDto"/> to an instance of <see cref="adulto"/>.
        /// </summary>
        /// <param name="dto"><see cref="adultoDto"/> to convert.</param>
        public static adulto ToEntity(this adultoDto dto)
        {
            if (dto == null) return null;

            var entity = new adulto();

            entity.v_AdultoId = dto.v_AdultoId;
            entity.v_PersonId = dto.v_PersonId;
            entity.v_NombreCuidador = dto.v_NombreCuidador;
            entity.v_EdadCuidador = dto.v_EdadCuidador;
            entity.v_DniCuidador = dto.v_DniCuidador;
            entity.v_MedicamentoFrecuente = dto.v_MedicamentoFrecuente;
            entity.v_ReaccionAlergica = dto.v_ReaccionAlergica;
            entity.v_InicioRS = dto.v_InicioRS;
            entity.v_NroPs = dto.v_NroPs;
            entity.v_FechaUR = dto.v_FechaUR;
            entity.v_RC = dto.v_RC;
            entity.v_Parto = dto.v_Parto;
            entity.v_Prematuro = dto.v_Prematuro;
            entity.v_Aborto = dto.v_Aborto;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.v_DescripcionAntecedentes = dto.v_DescripcionAntecedentes;
            entity.v_OtrosAntecedentes = dto.v_OtrosAntecedentes;
            entity.v_FlujoVaginal = dto.v_FlujoVaginal;
            entity.v_ObservacionesEmbarazo = dto.v_ObservacionesEmbarazo;
            entity.v_ComentaryUpdate = dto.v_ComentaryUpdate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="adulto"/> to an instance of <see cref="adultoDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="adulto"/> to convert.</param>
        public static adultoDto ToDTO(this adulto entity)
        {
            if (entity == null) return null;

            var dto = new adultoDto();

            dto.v_AdultoId = entity.v_AdultoId;
            dto.v_PersonId = entity.v_PersonId;
            dto.v_NombreCuidador = entity.v_NombreCuidador;
            dto.v_EdadCuidador = entity.v_EdadCuidador;
            dto.v_DniCuidador = entity.v_DniCuidador;
            dto.v_MedicamentoFrecuente = entity.v_MedicamentoFrecuente;
            dto.v_ReaccionAlergica = entity.v_ReaccionAlergica;
            dto.v_InicioRS = entity.v_InicioRS;
            dto.v_NroPs = entity.v_NroPs;
            dto.v_FechaUR = entity.v_FechaUR;
            dto.v_RC = entity.v_RC;
            dto.v_Parto = entity.v_Parto;
            dto.v_Prematuro = entity.v_Prematuro;
            dto.v_Aborto = entity.v_Aborto;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.v_DescripcionAntecedentes = entity.v_DescripcionAntecedentes;
            dto.v_OtrosAntecedentes = entity.v_OtrosAntecedentes;
            dto.v_FlujoVaginal = entity.v_FlujoVaginal;
            dto.v_ObservacionesEmbarazo = entity.v_ObservacionesEmbarazo;
            dto.v_ComentaryUpdate = entity.v_ComentaryUpdate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="adultoDto"/> to an instance of <see cref="adulto"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<adulto> ToEntities(this IEnumerable<adultoDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="adulto"/> to an instance of <see cref="adultoDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<adultoDto> ToDTOs(this IEnumerable<adulto> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
