//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:07:58
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
    public partial class getprotocolpagedandfiltered_filt_spResultDto
    {
        [DataMember()]
        public String v_ProtocolId { get; set; }

        [DataMember()]
        public String v_Protocol { get; set; }

        [DataMember()]
        public String v_Organization { get; set; }

        [DataMember()]
        public String v_Location { get; set; }

        [DataMember()]
        public String v_EsoType { get; set; }

        [DataMember()]
        public String v_GroupOccupation { get; set; }

        [DataMember()]
        public String v_OrganizationInvoice { get; set; }

        [DataMember()]
        public String v_CostCenter { get; set; }

        [DataMember()]
        public String v_IntermediaryOrganization { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ServiceTypeId { get; set; }

        [DataMember()]
        public String v_MasterServiceName { get; set; }

        [DataMember()]
        public Nullable<Int32> i_MasterServiceId { get; set; }

        [DataMember()]
        public String v_OrganizationId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_EsoTypeId { get; set; }

        [DataMember()]
        public String v_WorkingOrganizationId { get; set; }

        [DataMember()]
        public String v_OrganizationInvoiceId { get; set; }

        [DataMember()]
        public String v_GroupOccupationId { get; set; }

        [DataMember()]
        public String v_CreationUser { get; set; }

        [DataMember()]
        public String v_UpdateUser { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_CreationDate { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_UpdateDate { get; set; }

        [DataMember()]
        public String v_LocationId { get; set; }

        [DataMember()]
        public String v_CustomerLocationId { get; set; }

        [DataMember()]
        public String v_WorkingLocationId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsActive { get; set; }

        [DataMember()]
        public String v_ComponenteNombre { get; set; }

        [DataMember()]
        public String AseguradoraId { get; set; }

        public getprotocolpagedandfiltered_filt_spResultDto()
        {
        }

        public getprotocolpagedandfiltered_filt_spResultDto(String v_ProtocolId, String v_Protocol, String v_Organization, String v_Location, String v_EsoType, String v_GroupOccupation, String v_OrganizationInvoice, String v_CostCenter, String v_IntermediaryOrganization, Nullable<Int32> i_ServiceTypeId, String v_MasterServiceName, Nullable<Int32> i_MasterServiceId, String v_OrganizationId, Nullable<Int32> i_EsoTypeId, String v_WorkingOrganizationId, String v_OrganizationInvoiceId, String v_GroupOccupationId, String v_CreationUser, String v_UpdateUser, Nullable<DateTime> d_CreationDate, Nullable<DateTime> d_UpdateDate, String v_LocationId, String v_CustomerLocationId, String v_WorkingLocationId, Nullable<Int32> i_IsActive, String v_ComponenteNombre, String aseguradoraId)
        {
			this.v_ProtocolId = v_ProtocolId;
			this.v_Protocol = v_Protocol;
			this.v_Organization = v_Organization;
			this.v_Location = v_Location;
			this.v_EsoType = v_EsoType;
			this.v_GroupOccupation = v_GroupOccupation;
			this.v_OrganizationInvoice = v_OrganizationInvoice;
			this.v_CostCenter = v_CostCenter;
			this.v_IntermediaryOrganization = v_IntermediaryOrganization;
			this.i_ServiceTypeId = i_ServiceTypeId;
			this.v_MasterServiceName = v_MasterServiceName;
			this.i_MasterServiceId = i_MasterServiceId;
			this.v_OrganizationId = v_OrganizationId;
			this.i_EsoTypeId = i_EsoTypeId;
			this.v_WorkingOrganizationId = v_WorkingOrganizationId;
			this.v_OrganizationInvoiceId = v_OrganizationInvoiceId;
			this.v_GroupOccupationId = v_GroupOccupationId;
			this.v_CreationUser = v_CreationUser;
			this.v_UpdateUser = v_UpdateUser;
			this.d_CreationDate = d_CreationDate;
			this.d_UpdateDate = d_UpdateDate;
			this.v_LocationId = v_LocationId;
			this.v_CustomerLocationId = v_CustomerLocationId;
			this.v_WorkingLocationId = v_WorkingLocationId;
			this.i_IsActive = i_IsActive;
			this.v_ComponenteNombre = v_ComponenteNombre;
			this.AseguradoraId = aseguradoraId;
        }
    }
}
