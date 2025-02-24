//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:14:25
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
    public partial class restrictionDto
    {
        [DataMember()]
        public String v_RestrictionId { get; set; }

        [DataMember()]
        public String v_DiagnosticRepositoryId { get; set; }

        [DataMember()]
        public String v_ServiceId { get; set; }

        [DataMember()]
        public String v_ComponentId { get; set; }

        [DataMember()]
        public String v_MasterRestrictionId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_StartDateRestriction { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_EndDateRestriction { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsDeleted { get; set; }

        [DataMember()]
        public Nullable<Int32> i_InsertUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_InsertDate { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_UpdateDate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UpdateUserId { get; set; }

        [DataMember()]
        public String v_ComentaryUpdate { get; set; }

        [DataMember()]
        public componentDto component { get; set; }

        [DataMember()]
        public serviceDto service { get; set; }

        [DataMember()]
        public diagnosticrepositoryDto diagnosticrepository { get; set; }

        public restrictionDto()
        {
        }

        public restrictionDto(String v_RestrictionId, String v_DiagnosticRepositoryId, String v_ServiceId, String v_ComponentId, String v_MasterRestrictionId, Nullable<DateTime> d_StartDateRestriction, Nullable<DateTime> d_EndDateRestriction, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<DateTime> d_UpdateDate, Nullable<Int32> i_UpdateUserId, String v_ComentaryUpdate, componentDto component, serviceDto service, diagnosticrepositoryDto diagnosticrepository)
        {
			this.v_RestrictionId = v_RestrictionId;
			this.v_DiagnosticRepositoryId = v_DiagnosticRepositoryId;
			this.v_ServiceId = v_ServiceId;
			this.v_ComponentId = v_ComponentId;
			this.v_MasterRestrictionId = v_MasterRestrictionId;
			this.d_StartDateRestriction = d_StartDateRestriction;
			this.d_EndDateRestriction = d_EndDateRestriction;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.d_UpdateDate = d_UpdateDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.v_ComentaryUpdate = v_ComentaryUpdate;
			this.component = component;
			this.service = service;
			this.diagnosticrepository = diagnosticrepository;
        }
    }
}
