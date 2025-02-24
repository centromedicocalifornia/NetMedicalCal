//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:14:46
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    [DataContract()]
    public partial class servicecomponentDto
    {
        [DataMember()]
        public String v_ServiceComponentId { get; set; }

        [DataMember()]
        public String v_ServiceId { get; set; }

        [DataMember()]
        public String v_ComponentId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ServiceComponentStatusId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ExternalInternalId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ServiceComponentTypeId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsVisibleId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsInheritedId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_CalledDate { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_StartDate { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_EndDate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_index { get; set; }

        [DataMember()]
        public Nullable<Single> r_Price { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsInvoicedId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsRequiredId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsManuallyAddedId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_QueueStatusId { get; set; }

        [DataMember()]
        public String v_NameOfice { get; set; }

        [DataMember()]
        public String v_Comment { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Iscalling { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsApprovedId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsDeleted { get; set; }

        [DataMember()]
        public Nullable<Int32> i_InsertUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_InsertDate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UpdateUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_UpdateDate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ApprovedInsertUserId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ApprovedUpdateUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_ApprovedInsertDate { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_ApprovedUpdateDate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_InsertUserMedicalAnalystId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UpdateUserMedicalAnalystId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_InsertDateMedicalAnalyst { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_UpdateDateMedicalAnalyst { get; set; }

        [DataMember()]
        public Nullable<Int32> i_InsertUserTechnicalDataRegisterId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UpdateUserTechnicalDataRegisterId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_InsertDateTechnicalDataRegister { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_UpdateDateTechnicalDataRegister { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Iscalling_1 { get; set; }

        [DataMember()]
        public Nullable<Int32> i_AuditorInsertUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_AuditorInsertUser { get; set; }

        [DataMember()]
        public Nullable<Int32> i_AuditorUpdateUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_AuditorUpdateUser { get; set; }

        [DataMember()]
        public String v_IdUnidadProductiva { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_SaldoPaciente { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_SaldoAseguradora { get; set; }

        [DataMember()]
        public Nullable<Int32> i_MedicoTratanteId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_SystemUserEspecialistaId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ConCargoA { get; set; }

        [DataMember()]
        public Nullable<Int32> i_TipoDesc { get; set; }

        [DataMember()]
        public String v_ComentaryUpdate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UserTypeId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsFact { get; set; }

        [DataMember()]
        public String v_Area { get; set; }

        [DataMember()]
        public String v_Cargo { get; set; }

        [DataMember()]
        public Nullable<Int32> i_PayMedic { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ApplicantMedic { get; set; }

        [DataMember()]
        public Nullable<Decimal> i_Cantidad { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_Descuento { get; set; }

        [DataMember()]
        public componentDto component { get; set; }

        [DataMember()]
        public serviceDto service { get; set; }

        [DataMember()]
        public List<servicecomponentfieldsDto> servicecomponentfields { get; set; }

        [DataMember()]
        public List<servicecomponentmultimediaDto> servicecomponentmultimedia { get; set; }

        public servicecomponentDto()
        {
        }

        public servicecomponentDto(String v_ServiceComponentId, String v_ServiceId, String v_ComponentId, Nullable<Int32> i_ServiceComponentStatusId, Nullable<Int32> i_ExternalInternalId, Nullable<Int32> i_ServiceComponentTypeId, Nullable<Int32> i_IsVisibleId, Nullable<Int32> i_IsInheritedId, Nullable<DateTime> d_CalledDate, Nullable<DateTime> d_StartDate, Nullable<DateTime> d_EndDate, Nullable<Int32> i_index, Nullable<Single> r_Price, Nullable<Int32> i_IsInvoicedId, Nullable<Int32> i_IsRequiredId, Nullable<Int32> i_IsManuallyAddedId, Nullable<Int32> i_QueueStatusId, String v_NameOfice, String v_Comment, Nullable<Int32> i_Iscalling, Nullable<Int32> i_IsApprovedId, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, Nullable<Int32> i_ApprovedInsertUserId, Nullable<Int32> i_ApprovedUpdateUserId, Nullable<DateTime> d_ApprovedInsertDate, Nullable<DateTime> d_ApprovedUpdateDate, Nullable<Int32> i_InsertUserMedicalAnalystId, Nullable<Int32> i_UpdateUserMedicalAnalystId, Nullable<DateTime> d_InsertDateMedicalAnalyst, Nullable<DateTime> d_UpdateDateMedicalAnalyst, Nullable<Int32> i_InsertUserTechnicalDataRegisterId, Nullable<Int32> i_UpdateUserTechnicalDataRegisterId, Nullable<DateTime> d_InsertDateTechnicalDataRegister, Nullable<DateTime> d_UpdateDateTechnicalDataRegister, Nullable<Int32> i_Iscalling_1, Nullable<Int32> i_AuditorInsertUserId, Nullable<DateTime> d_AuditorInsertUser, Nullable<Int32> i_AuditorUpdateUserId, Nullable<DateTime> d_AuditorUpdateUser, String v_IdUnidadProductiva, Nullable<Decimal> d_SaldoPaciente, Nullable<Decimal> d_SaldoAseguradora, Nullable<Int32> i_MedicoTratanteId, Nullable<Int32> i_SystemUserEspecialistaId, Nullable<Int32> i_ConCargoA, Nullable<Int32> i_TipoDesc, String v_ComentaryUpdate, Nullable<Int32> i_UserTypeId, Nullable<Int32> i_IsFact, String v_Area, String v_Cargo, Nullable<Int32> i_PayMedic, Nullable<Int32> i_ApplicantMedic, Nullable<Decimal> i_Cantidad, Nullable<Decimal> d_Descuento, componentDto component, serviceDto service, List<servicecomponentfieldsDto> servicecomponentfields, List<servicecomponentmultimediaDto> servicecomponentmultimedia)
        {
			this.v_ServiceComponentId = v_ServiceComponentId;
			this.v_ServiceId = v_ServiceId;
			this.v_ComponentId = v_ComponentId;
			this.i_ServiceComponentStatusId = i_ServiceComponentStatusId;
			this.i_ExternalInternalId = i_ExternalInternalId;
			this.i_ServiceComponentTypeId = i_ServiceComponentTypeId;
			this.i_IsVisibleId = i_IsVisibleId;
			this.i_IsInheritedId = i_IsInheritedId;
			this.d_CalledDate = d_CalledDate;
			this.d_StartDate = d_StartDate;
			this.d_EndDate = d_EndDate;
			this.i_index = i_index;
			this.r_Price = r_Price;
			this.i_IsInvoicedId = i_IsInvoicedId;
			this.i_IsRequiredId = i_IsRequiredId;
			this.i_IsManuallyAddedId = i_IsManuallyAddedId;
			this.i_QueueStatusId = i_QueueStatusId;
			this.v_NameOfice = v_NameOfice;
			this.v_Comment = v_Comment;
			this.i_Iscalling = i_Iscalling;
			this.i_IsApprovedId = i_IsApprovedId;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.i_ApprovedInsertUserId = i_ApprovedInsertUserId;
			this.i_ApprovedUpdateUserId = i_ApprovedUpdateUserId;
			this.d_ApprovedInsertDate = d_ApprovedInsertDate;
			this.d_ApprovedUpdateDate = d_ApprovedUpdateDate;
			this.i_InsertUserMedicalAnalystId = i_InsertUserMedicalAnalystId;
			this.i_UpdateUserMedicalAnalystId = i_UpdateUserMedicalAnalystId;
			this.d_InsertDateMedicalAnalyst = d_InsertDateMedicalAnalyst;
			this.d_UpdateDateMedicalAnalyst = d_UpdateDateMedicalAnalyst;
			this.i_InsertUserTechnicalDataRegisterId = i_InsertUserTechnicalDataRegisterId;
			this.i_UpdateUserTechnicalDataRegisterId = i_UpdateUserTechnicalDataRegisterId;
			this.d_InsertDateTechnicalDataRegister = d_InsertDateTechnicalDataRegister;
			this.d_UpdateDateTechnicalDataRegister = d_UpdateDateTechnicalDataRegister;
			this.i_Iscalling_1 = i_Iscalling_1;
			this.i_AuditorInsertUserId = i_AuditorInsertUserId;
			this.d_AuditorInsertUser = d_AuditorInsertUser;
			this.i_AuditorUpdateUserId = i_AuditorUpdateUserId;
			this.d_AuditorUpdateUser = d_AuditorUpdateUser;
			this.v_IdUnidadProductiva = v_IdUnidadProductiva;
			this.d_SaldoPaciente = d_SaldoPaciente;
			this.d_SaldoAseguradora = d_SaldoAseguradora;
			this.i_MedicoTratanteId = i_MedicoTratanteId;
			this.i_SystemUserEspecialistaId = i_SystemUserEspecialistaId;
			this.i_ConCargoA = i_ConCargoA;
			this.i_TipoDesc = i_TipoDesc;
			this.v_ComentaryUpdate = v_ComentaryUpdate;
			this.i_UserTypeId = i_UserTypeId;
			this.i_IsFact = i_IsFact;
			this.v_Area = v_Area;
			this.v_Cargo = v_Cargo;
			this.i_PayMedic = i_PayMedic;
			this.i_ApplicantMedic = i_ApplicantMedic;
			this.i_Cantidad = i_Cantidad;
			this.d_Descuento = d_Descuento;
			this.component = component;
			this.service = service;
			this.servicecomponentfields = servicecomponentfields;
			this.servicecomponentmultimedia = servicecomponentmultimedia;
        }
    }
}
