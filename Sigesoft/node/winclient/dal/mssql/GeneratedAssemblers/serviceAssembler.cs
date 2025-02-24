//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:23:30
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
    /// Assembler for <see cref="service"/> and <see cref="serviceDto"/>.
    /// </summary>
    public static partial class serviceAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="serviceDto"/> converted from <see cref="service"/>.</param>
        static partial void OnDTO(this service entity, serviceDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="service"/> converted from <see cref="serviceDto"/>.</param>
        static partial void OnEntity(this serviceDto dto, service entity);

        /// <summary>
        /// Converts this instance of <see cref="serviceDto"/> to an instance of <see cref="service"/>.
        /// </summary>
        /// <param name="dto"><see cref="serviceDto"/> to convert.</param>
        public static service ToEntity(this serviceDto dto)
        {
            if (dto == null) return null;

            var entity = new service();

            entity.v_ServiceId = dto.v_ServiceId;
            entity.v_ProtocolId = dto.v_ProtocolId;
            entity.v_PersonId = dto.v_PersonId;
            entity.i_MasterServiceId = dto.i_MasterServiceId;
            entity.i_ServiceStatusId = dto.i_ServiceStatusId;
            entity.v_Motive = dto.v_Motive;
            entity.i_AptitudeStatusId = dto.i_AptitudeStatusId;
            entity.d_ServiceDate = dto.d_ServiceDate;
            entity.d_GlobalExpirationDate = dto.d_GlobalExpirationDate;
            entity.d_ObsExpirationDate = dto.d_ObsExpirationDate;
            entity.i_FlagAgentId = dto.i_FlagAgentId;
            entity.v_OrganizationId = dto.v_OrganizationId;
            entity.v_LocationId = dto.v_LocationId;
            entity.v_MainSymptom = dto.v_MainSymptom;
            entity.i_TimeOfDisease = dto.i_TimeOfDisease;
            entity.i_TimeOfDiseaseTypeId = dto.i_TimeOfDiseaseTypeId;
            entity.v_Story = dto.v_Story;
            entity.i_DreamId = dto.i_DreamId;
            entity.i_UrineId = dto.i_UrineId;
            entity.i_DepositionId = dto.i_DepositionId;
            entity.i_AppetiteId = dto.i_AppetiteId;
            entity.i_ThirstId = dto.i_ThirstId;
            entity.d_Fur = dto.d_Fur;
            entity.v_CatemenialRegime = dto.v_CatemenialRegime;
            entity.i_MacId = dto.i_MacId;
            entity.i_IsNewControl = dto.i_IsNewControl;
            entity.i_HasMedicalBreakId = dto.i_HasMedicalBreakId;
            entity.d_MedicalBreakStartDate = dto.d_MedicalBreakStartDate;
            entity.d_MedicalBreakEndDate = dto.d_MedicalBreakEndDate;
            entity.v_GeneralRecomendations = dto.v_GeneralRecomendations;
            entity.i_DestinationMedicationId = dto.i_DestinationMedicationId;
            entity.i_TransportMedicationId = dto.i_TransportMedicationId;
            entity.d_StartDateRestriction = dto.d_StartDateRestriction;
            entity.d_EndDateRestriction = dto.d_EndDateRestriction;
            entity.i_HasRestrictionId = dto.i_HasRestrictionId;
            entity.i_HasSymptomId = dto.i_HasSymptomId;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.d_NextAppointment = dto.d_NextAppointment;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.i_SendToTracking = dto.i_SendToTracking;
            entity.i_InsertUserMedicalAnalystId = dto.i_InsertUserMedicalAnalystId;
            entity.i_UpdateUserMedicalAnalystId = dto.i_UpdateUserMedicalAnalystId;
            entity.d_InsertDateMedicalAnalyst = dto.d_InsertDateMedicalAnalyst;
            entity.d_UpdateDateMedicalAnalyst = dto.d_UpdateDateMedicalAnalyst;
            entity.i_InsertUserOccupationalMedicalId = dto.i_InsertUserOccupationalMedicalId;
            entity.i_UpdateUserOccupationalMedicaltId = dto.i_UpdateUserOccupationalMedicaltId;
            entity.d_InsertDateOccupationalMedical = dto.d_InsertDateOccupationalMedical;
            entity.d_UpdateDateOccupationalMedical = dto.d_UpdateDateOccupationalMedical;
            entity.i_HazInterconsultationId = dto.i_HazInterconsultationId;
            entity.v_Gestapara = dto.v_Gestapara;
            entity.v_Menarquia = dto.v_Menarquia;
            entity.d_PAP = dto.d_PAP;
            entity.d_Mamografia = dto.d_Mamografia;
            entity.v_CiruGine = dto.v_CiruGine;
            entity.v_Findings = dto.v_Findings;
            entity.i_StatusLiquidation = dto.i_StatusLiquidation;
            entity.i_ServiceTypeOfInsurance = dto.i_ServiceTypeOfInsurance;
            entity.i_ModalityOfInsurance = dto.i_ModalityOfInsurance;
            entity.i_IsFac = dto.i_IsFac;
            entity.i_InicioEnf = dto.i_InicioEnf;
            entity.i_CursoEnf = dto.i_CursoEnf;
            entity.i_Evolucion = dto.i_Evolucion;
            entity.v_ExaAuxResult = dto.v_ExaAuxResult;
            entity.v_ObsStatusService = dto.v_ObsStatusService;
            entity.d_FechaEntrega = dto.d_FechaEntrega;
            entity.v_AreaId = dto.v_AreaId;
            entity.v_FechaUltimoPAP = dto.v_FechaUltimoPAP;
            entity.v_ResultadosPAP = dto.v_ResultadosPAP;
            entity.v_FechaUltimaMamo = dto.v_FechaUltimaMamo;
            entity.v_ResultadoMamo = dto.v_ResultadoMamo;
            entity.r_Costo = dto.r_Costo;
            entity.i_EnvioCertificado = dto.i_EnvioCertificado;
            entity.i_EnvioHistoria = dto.i_EnvioHistoria;
            entity.v_IdVentaCliente = dto.v_IdVentaCliente;
            entity.v_IdVentaAseguradora = dto.v_IdVentaAseguradora;
            entity.v_InicioVidaSexaul = dto.v_InicioVidaSexaul;
            entity.v_NroParejasActuales = dto.v_NroParejasActuales;
            entity.v_NroAbortos = dto.v_NroAbortos;
            entity.v_PrecisarCausas = dto.v_PrecisarCausas;
            entity.i_MedicoTratanteId = dto.i_MedicoTratanteId;
            entity.i_IsFacMedico = dto.i_IsFacMedico;
            entity.v_centrocosto = dto.v_centrocosto;
            entity.v_NroLiquidacion = dto.v_NroLiquidacion;
            entity.i_MedicoPagado = dto.i_MedicoPagado;
            entity.i_PagoEspecialista = dto.i_PagoEspecialista;
            entity.i_IsControl = dto.i_IsControl;
            entity.i_IsRevisedHistoryId = dto.i_IsRevisedHistoryId;
            entity.i_StatusVigilanciaId = dto.i_StatusVigilanciaId;
            entity.v_NroCartaSolicitud = dto.v_NroCartaSolicitud;
            entity.v_ComentaryUpdate = dto.v_ComentaryUpdate;
            entity.i_PlanId = dto.i_PlanId;
            entity.i_AptitudesStatusId_First = dto.i_AptitudesStatusId_First;
            entity.v_CommentAptitusStatus_First = dto.v_CommentAptitusStatus_First;
            entity.v_ComprobantePago = dto.v_ComprobantePago;
            entity.i_ServiceUserId = dto.i_ServiceUserId;
            entity.v_Area = dto.v_Area;
            entity.v_ComprobantePago_Anulado = dto.v_ComprobantePago_Anulado;
            entity.i_TramaCargada = dto.i_TramaCargada;
            entity.v_Puesto = dto.v_Puesto;
            entity.v_Cargo = dto.v_Cargo;
            entity.i_AltamedicaCovid19_1 = dto.i_AltamedicaCovid19_1;
            entity.i_AltamedicaCovid19_2 = dto.i_AltamedicaCovid19_2;
            entity.i_AltaMedica = dto.i_AltaMedica;
            entity.v_LicenciaConducir = dto.v_LicenciaConducir;
            entity.v_ObservacionesAdicionales = dto.v_ObservacionesAdicionales;
            entity.d_MacInicio = dto.d_MacInicio;
            entity.d_MacFin = dto.d_MacFin;
            entity.i_RelacionesS = dto.i_RelacionesS;
            entity.i_Fertilidad = dto.i_Fertilidad;
            entity.v_Fertilidad = dto.v_Fertilidad;
            entity.i_Dismenorrea = dto.i_Dismenorrea;
            entity.i_Dispareunia = dto.i_Dispareunia;
            entity.i_Coitorragia = dto.i_Coitorragia;
            entity.i_Disquesia = dto.i_Disquesia;
            entity.v_FUP = dto.v_FUP;
            entity.i_TipoFUP = dto.i_TipoFUP;
            entity.v_VolumenRC = dto.v_VolumenRC;
            entity.i_ModTrabajo = dto.i_ModTrabajo;
            entity.i_ProcedenciaPac_Mkt = dto.i_ProcedenciaPac_Mkt;
            entity.i_MedicoSolicitanteExterno = dto.i_MedicoSolicitanteExterno;
            entity.i_Establecimiento = dto.i_Establecimiento;
            entity.i_VendedorExterno = dto.i_VendedorExterno;
            entity.i_MedicoDerivado = dto.i_MedicoDerivado;
            entity.i_MedicoAtencion = dto.i_MedicoAtencion;
            entity.i_CodigoAtencion = dto.i_CodigoAtencion;
            entity.i_GrupoAtencion = dto.i_GrupoAtencion;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="service"/> to an instance of <see cref="serviceDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="service"/> to convert.</param>
        public static serviceDto ToDTO(this service entity)
        {
            if (entity == null) return null;

            var dto = new serviceDto();

            dto.v_ServiceId = entity.v_ServiceId;
            dto.v_ProtocolId = entity.v_ProtocolId;
            dto.v_PersonId = entity.v_PersonId;
            dto.i_MasterServiceId = entity.i_MasterServiceId;
            dto.i_ServiceStatusId = entity.i_ServiceStatusId;
            dto.v_Motive = entity.v_Motive;
            dto.i_AptitudeStatusId = entity.i_AptitudeStatusId;
            dto.d_ServiceDate = entity.d_ServiceDate;
            dto.d_GlobalExpirationDate = entity.d_GlobalExpirationDate;
            dto.d_ObsExpirationDate = entity.d_ObsExpirationDate;
            dto.i_FlagAgentId = entity.i_FlagAgentId;
            dto.v_OrganizationId = entity.v_OrganizationId;
            dto.v_LocationId = entity.v_LocationId;
            dto.v_MainSymptom = entity.v_MainSymptom;
            dto.i_TimeOfDisease = entity.i_TimeOfDisease;
            dto.i_TimeOfDiseaseTypeId = entity.i_TimeOfDiseaseTypeId;
            dto.v_Story = entity.v_Story;
            dto.i_DreamId = entity.i_DreamId;
            dto.i_UrineId = entity.i_UrineId;
            dto.i_DepositionId = entity.i_DepositionId;
            dto.i_AppetiteId = entity.i_AppetiteId;
            dto.i_ThirstId = entity.i_ThirstId;
            dto.d_Fur = entity.d_Fur;
            dto.v_CatemenialRegime = entity.v_CatemenialRegime;
            dto.i_MacId = entity.i_MacId;
            dto.i_IsNewControl = entity.i_IsNewControl;
            dto.i_HasMedicalBreakId = entity.i_HasMedicalBreakId;
            dto.d_MedicalBreakStartDate = entity.d_MedicalBreakStartDate;
            dto.d_MedicalBreakEndDate = entity.d_MedicalBreakEndDate;
            dto.v_GeneralRecomendations = entity.v_GeneralRecomendations;
            dto.i_DestinationMedicationId = entity.i_DestinationMedicationId;
            dto.i_TransportMedicationId = entity.i_TransportMedicationId;
            dto.d_StartDateRestriction = entity.d_StartDateRestriction;
            dto.d_EndDateRestriction = entity.d_EndDateRestriction;
            dto.i_HasRestrictionId = entity.i_HasRestrictionId;
            dto.i_HasSymptomId = entity.i_HasSymptomId;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.d_NextAppointment = entity.d_NextAppointment;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.i_SendToTracking = entity.i_SendToTracking;
            dto.i_InsertUserMedicalAnalystId = entity.i_InsertUserMedicalAnalystId;
            dto.i_UpdateUserMedicalAnalystId = entity.i_UpdateUserMedicalAnalystId;
            dto.d_InsertDateMedicalAnalyst = entity.d_InsertDateMedicalAnalyst;
            dto.d_UpdateDateMedicalAnalyst = entity.d_UpdateDateMedicalAnalyst;
            dto.i_InsertUserOccupationalMedicalId = entity.i_InsertUserOccupationalMedicalId;
            dto.i_UpdateUserOccupationalMedicaltId = entity.i_UpdateUserOccupationalMedicaltId;
            dto.d_InsertDateOccupationalMedical = entity.d_InsertDateOccupationalMedical;
            dto.d_UpdateDateOccupationalMedical = entity.d_UpdateDateOccupationalMedical;
            dto.i_HazInterconsultationId = entity.i_HazInterconsultationId;
            dto.v_Gestapara = entity.v_Gestapara;
            dto.v_Menarquia = entity.v_Menarquia;
            dto.d_PAP = entity.d_PAP;
            dto.d_Mamografia = entity.d_Mamografia;
            dto.v_CiruGine = entity.v_CiruGine;
            dto.v_Findings = entity.v_Findings;
            dto.i_StatusLiquidation = entity.i_StatusLiquidation;
            dto.i_ServiceTypeOfInsurance = entity.i_ServiceTypeOfInsurance;
            dto.i_ModalityOfInsurance = entity.i_ModalityOfInsurance;
            dto.i_IsFac = entity.i_IsFac;
            dto.i_InicioEnf = entity.i_InicioEnf;
            dto.i_CursoEnf = entity.i_CursoEnf;
            dto.i_Evolucion = entity.i_Evolucion;
            dto.v_ExaAuxResult = entity.v_ExaAuxResult;
            dto.v_ObsStatusService = entity.v_ObsStatusService;
            dto.d_FechaEntrega = entity.d_FechaEntrega;
            dto.v_AreaId = entity.v_AreaId;
            dto.v_FechaUltimoPAP = entity.v_FechaUltimoPAP;
            dto.v_ResultadosPAP = entity.v_ResultadosPAP;
            dto.v_FechaUltimaMamo = entity.v_FechaUltimaMamo;
            dto.v_ResultadoMamo = entity.v_ResultadoMamo;
            dto.r_Costo = entity.r_Costo;
            dto.i_EnvioCertificado = entity.i_EnvioCertificado;
            dto.i_EnvioHistoria = entity.i_EnvioHistoria;
            dto.v_IdVentaCliente = entity.v_IdVentaCliente;
            dto.v_IdVentaAseguradora = entity.v_IdVentaAseguradora;
            dto.v_InicioVidaSexaul = entity.v_InicioVidaSexaul;
            dto.v_NroParejasActuales = entity.v_NroParejasActuales;
            dto.v_NroAbortos = entity.v_NroAbortos;
            dto.v_PrecisarCausas = entity.v_PrecisarCausas;
            dto.i_MedicoTratanteId = entity.i_MedicoTratanteId;
            dto.i_IsFacMedico = entity.i_IsFacMedico;
            dto.v_centrocosto = entity.v_centrocosto;
            dto.v_NroLiquidacion = entity.v_NroLiquidacion;
            dto.i_MedicoPagado = entity.i_MedicoPagado;
            dto.i_PagoEspecialista = entity.i_PagoEspecialista;
            dto.i_IsControl = entity.i_IsControl;
            dto.i_IsRevisedHistoryId = entity.i_IsRevisedHistoryId;
            dto.i_StatusVigilanciaId = entity.i_StatusVigilanciaId;
            dto.v_NroCartaSolicitud = entity.v_NroCartaSolicitud;
            dto.v_ComentaryUpdate = entity.v_ComentaryUpdate;
            dto.i_PlanId = entity.i_PlanId;
            dto.i_AptitudesStatusId_First = entity.i_AptitudesStatusId_First;
            dto.v_CommentAptitusStatus_First = entity.v_CommentAptitusStatus_First;
            dto.v_ComprobantePago = entity.v_ComprobantePago;
            dto.i_ServiceUserId = entity.i_ServiceUserId;
            dto.v_Area = entity.v_Area;
            dto.v_ComprobantePago_Anulado = entity.v_ComprobantePago_Anulado;
            dto.i_TramaCargada = entity.i_TramaCargada;
            dto.v_Puesto = entity.v_Puesto;
            dto.v_Cargo = entity.v_Cargo;
            dto.i_AltamedicaCovid19_1 = entity.i_AltamedicaCovid19_1;
            dto.i_AltamedicaCovid19_2 = entity.i_AltamedicaCovid19_2;
            dto.i_AltaMedica = entity.i_AltaMedica;
            dto.v_LicenciaConducir = entity.v_LicenciaConducir;
            dto.v_ObservacionesAdicionales = entity.v_ObservacionesAdicionales;
            dto.d_MacInicio = entity.d_MacInicio;
            dto.d_MacFin = entity.d_MacFin;
            dto.i_RelacionesS = entity.i_RelacionesS;
            dto.i_Fertilidad = entity.i_Fertilidad;
            dto.v_Fertilidad = entity.v_Fertilidad;
            dto.i_Dismenorrea = entity.i_Dismenorrea;
            dto.i_Dispareunia = entity.i_Dispareunia;
            dto.i_Coitorragia = entity.i_Coitorragia;
            dto.i_Disquesia = entity.i_Disquesia;
            dto.v_FUP = entity.v_FUP;
            dto.i_TipoFUP = entity.i_TipoFUP;
            dto.v_VolumenRC = entity.v_VolumenRC;
            dto.i_ModTrabajo = entity.i_ModTrabajo;
            dto.i_ProcedenciaPac_Mkt = entity.i_ProcedenciaPac_Mkt;
            dto.i_MedicoSolicitanteExterno = entity.i_MedicoSolicitanteExterno;
            dto.i_Establecimiento = entity.i_Establecimiento;
            dto.i_VendedorExterno = entity.i_VendedorExterno;
            dto.i_MedicoDerivado = entity.i_MedicoDerivado;
            dto.i_MedicoAtencion = entity.i_MedicoAtencion;
            dto.i_CodigoAtencion = entity.i_CodigoAtencion;
            dto.i_GrupoAtencion = entity.i_GrupoAtencion;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="serviceDto"/> to an instance of <see cref="service"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<service> ToEntities(this IEnumerable<serviceDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="service"/> to an instance of <see cref="serviceDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<serviceDto> ToDTOs(this IEnumerable<service> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
