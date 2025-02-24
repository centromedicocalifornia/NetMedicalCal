//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:16:56
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
    /// Assembler for <see cref="getpacientreportepsfirmamedicoocupacional_spResult"/> and <see cref="getpacientreportepsfirmamedicoocupacional_spResultDto"/>.
    /// </summary>
    public static partial class getpacientreportepsfirmamedicoocupacional_spResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="getpacientreportepsfirmamedicoocupacional_spResultDto"/> converted from <see cref="getpacientreportepsfirmamedicoocupacional_spResult"/>.</param>
        static partial void OnDTO(this getpacientreportepsfirmamedicoocupacional_spResult entity, getpacientreportepsfirmamedicoocupacional_spResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="getpacientreportepsfirmamedicoocupacional_spResult"/> converted from <see cref="getpacientreportepsfirmamedicoocupacional_spResultDto"/>.</param>
        static partial void OnEntity(this getpacientreportepsfirmamedicoocupacional_spResultDto dto, getpacientreportepsfirmamedicoocupacional_spResult entity);

        /// <summary>
        /// Converts this instance of <see cref="getpacientreportepsfirmamedicoocupacional_spResultDto"/> to an instance of <see cref="getpacientreportepsfirmamedicoocupacional_spResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="getpacientreportepsfirmamedicoocupacional_spResultDto"/> to convert.</param>
        public static getpacientreportepsfirmamedicoocupacional_spResult ToEntity(this getpacientreportepsfirmamedicoocupacional_spResultDto dto)
        {
            if (dto == null) return null;

            var entity = new getpacientreportepsfirmamedicoocupacional_spResult();

            entity.TimeOfDisease = dto.TimeOfDisease;
            entity.v_CurrentOccupation = dto.v_CurrentOccupation;
            entity.TiempoEnfermedad = dto.TiempoEnfermedad;
            entity.InicioEnfermedad = dto.InicioEnfermedad;
            entity.CursoEnfermedad = dto.CursoEnfermedad;
            entity.v_PersonId = dto.v_PersonId;
            entity.v_FirstName = dto.v_FirstName;
            entity.v_FirstLastName = dto.v_FirstLastName;
            entity.v_SecondLastName = dto.v_SecondLastName;
            entity.b_Photo = dto.b_Photo;
            entity.v_TypeOfInsuranceName = dto.v_TypeOfInsuranceName;
            entity.v_FullWorkingOrganizationName = dto.v_FullWorkingOrganizationName;
            entity.v_RelationshipName = dto.v_RelationshipName;
            entity.v_OwnerName = dto.v_OwnerName;
            entity.d_ServiceDate = dto.d_ServiceDate;
            entity.d_Birthdate = dto.d_Birthdate;
            entity.i_DocTypeId = dto.i_DocTypeId;
            entity.i_NumberDependentChildren = dto.i_NumberDependentChildren;
            entity.i_NumberLivingChildren = dto.i_NumberLivingChildren;
            entity.FirmaTrabajador = dto.FirmaTrabajador;
            entity.HuellaTrabajador = dto.HuellaTrabajador;
            entity.v_BloodGroupName = dto.v_BloodGroupName;
            entity.v_BloodFactorName = dto.v_BloodFactorName;
            entity.v_SexTypeName = dto.v_SexTypeName;
            entity.v_TipoExamen = dto.v_TipoExamen;
            entity.v_NombreProtocolo = dto.v_NombreProtocolo;
            entity.v_DocNumber = dto.v_DocNumber;
            entity.v_IdService = dto.v_IdService;
            entity.v_Story = dto.v_Story;
            entity.v_MainSymptom = dto.v_MainSymptom;
            entity.FirmaDoctor = dto.FirmaDoctor;
            entity.v_ExaAuxResult = dto.v_ExaAuxResult;
            entity.NombreDoctor = dto.NombreDoctor;
            entity.CMP = dto.CMP;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="getpacientreportepsfirmamedicoocupacional_spResult"/> to an instance of <see cref="getpacientreportepsfirmamedicoocupacional_spResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="getpacientreportepsfirmamedicoocupacional_spResult"/> to convert.</param>
        public static getpacientreportepsfirmamedicoocupacional_spResultDto ToDTO(this getpacientreportepsfirmamedicoocupacional_spResult entity)
        {
            if (entity == null) return null;

            var dto = new getpacientreportepsfirmamedicoocupacional_spResultDto();

            dto.TimeOfDisease = entity.TimeOfDisease;
            dto.v_CurrentOccupation = entity.v_CurrentOccupation;
            dto.TiempoEnfermedad = entity.TiempoEnfermedad;
            dto.InicioEnfermedad = entity.InicioEnfermedad;
            dto.CursoEnfermedad = entity.CursoEnfermedad;
            dto.v_PersonId = entity.v_PersonId;
            dto.v_FirstName = entity.v_FirstName;
            dto.v_FirstLastName = entity.v_FirstLastName;
            dto.v_SecondLastName = entity.v_SecondLastName;
            dto.b_Photo = entity.b_Photo;
            dto.v_TypeOfInsuranceName = entity.v_TypeOfInsuranceName;
            dto.v_FullWorkingOrganizationName = entity.v_FullWorkingOrganizationName;
            dto.v_RelationshipName = entity.v_RelationshipName;
            dto.v_OwnerName = entity.v_OwnerName;
            dto.d_ServiceDate = entity.d_ServiceDate;
            dto.d_Birthdate = entity.d_Birthdate;
            dto.i_DocTypeId = entity.i_DocTypeId;
            dto.i_NumberDependentChildren = entity.i_NumberDependentChildren;
            dto.i_NumberLivingChildren = entity.i_NumberLivingChildren;
            dto.FirmaTrabajador = entity.FirmaTrabajador;
            dto.HuellaTrabajador = entity.HuellaTrabajador;
            dto.v_BloodGroupName = entity.v_BloodGroupName;
            dto.v_BloodFactorName = entity.v_BloodFactorName;
            dto.v_SexTypeName = entity.v_SexTypeName;
            dto.v_TipoExamen = entity.v_TipoExamen;
            dto.v_NombreProtocolo = entity.v_NombreProtocolo;
            dto.v_DocNumber = entity.v_DocNumber;
            dto.v_IdService = entity.v_IdService;
            dto.v_Story = entity.v_Story;
            dto.v_MainSymptom = entity.v_MainSymptom;
            dto.FirmaDoctor = entity.FirmaDoctor;
            dto.v_ExaAuxResult = entity.v_ExaAuxResult;
            dto.NombreDoctor = entity.NombreDoctor;
            dto.CMP = entity.CMP;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="getpacientreportepsfirmamedicoocupacional_spResultDto"/> to an instance of <see cref="getpacientreportepsfirmamedicoocupacional_spResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<getpacientreportepsfirmamedicoocupacional_spResult> ToEntities(this IEnumerable<getpacientreportepsfirmamedicoocupacional_spResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="getpacientreportepsfirmamedicoocupacional_spResult"/> to an instance of <see cref="getpacientreportepsfirmamedicoocupacional_spResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<getpacientreportepsfirmamedicoocupacional_spResultDto> ToDTOs(this IEnumerable<getpacientreportepsfirmamedicoocupacional_spResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
