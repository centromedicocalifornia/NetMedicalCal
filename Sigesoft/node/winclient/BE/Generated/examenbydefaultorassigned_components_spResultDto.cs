//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:06:39
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
    public partial class examenbydefaultorassigned_components_spResultDto
    {
        [DataMember()]
        public String v_ComponentId { get; set; }

        [DataMember()]
        public String v_Name { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UIIsVisibleId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ComponentTypeId { get; set; }

        [DataMember()]
        public String v_ServiceComponentId { get; set; }

        [DataMember()]
        public String v_CreationUser { get; set; }

        [DataMember()]
        public String v_UpdateUser { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_CreationDate { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_UpdateDate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsDeleted { get; set; }

        [DataMember()]
        public Nullable<Int32> i_CategoryId { get; set; }

        [DataMember()]
        public String v_CategoryName { get; set; }

        [DataMember()]
        public Nullable<Int32> i_GroupedComponentId { get; set; }

        [DataMember()]
        public String v_GroupedComponentName { get; set; }

        [DataMember()]
        public String v_ComponentCopyId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ServiceComponentStatusId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Index { get; set; }

        public examenbydefaultorassigned_components_spResultDto()
        {
        }

        public examenbydefaultorassigned_components_spResultDto(String v_ComponentId, String v_Name, Nullable<Int32> i_UIIsVisibleId, Nullable<Int32> i_ComponentTypeId, String v_ServiceComponentId, String v_CreationUser, String v_UpdateUser, Nullable<DateTime> d_CreationDate, Nullable<DateTime> d_UpdateDate, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_CategoryId, String v_CategoryName, Nullable<Int32> i_GroupedComponentId, String v_GroupedComponentName, String v_ComponentCopyId, Nullable<Int32> i_ServiceComponentStatusId, Nullable<Int32> i_Index)
        {
			this.v_ComponentId = v_ComponentId;
			this.v_Name = v_Name;
			this.i_UIIsVisibleId = i_UIIsVisibleId;
			this.i_ComponentTypeId = i_ComponentTypeId;
			this.v_ServiceComponentId = v_ServiceComponentId;
			this.v_CreationUser = v_CreationUser;
			this.v_UpdateUser = v_UpdateUser;
			this.d_CreationDate = d_CreationDate;
			this.d_UpdateDate = d_UpdateDate;
			this.i_IsDeleted = i_IsDeleted;
			this.i_CategoryId = i_CategoryId;
			this.v_CategoryName = v_CategoryName;
			this.i_GroupedComponentId = i_GroupedComponentId;
			this.v_GroupedComponentName = v_GroupedComponentName;
			this.v_ComponentCopyId = v_ComponentCopyId;
			this.i_ServiceComponentStatusId = i_ServiceComponentStatusId;
			this.i_Index = i_Index;
        }
    }
}
