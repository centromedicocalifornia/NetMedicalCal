//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:07:10
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
    public partial class getfamilymedicalantecedentspagedandfilteredbypersonid_spResultDto
    {
        [DataMember()]
        public String v_FamilyMedicalAntecedentsId { get; set; }

        [DataMember()]
        public String v_PersonId { get; set; }

        [DataMember()]
        public String v_DiseasesId { get; set; }

        [DataMember()]
        public String v_DiseaseName { get; set; }

        [DataMember()]
        public Nullable<Int32> i_TypeFamilyId { get; set; }

        [DataMember()]
        public String v_TypeFamilyName { get; set; }

        [DataMember()]
        public String v_Comment { get; set; }

        [DataMember()]
        public Int32 i_RecordStatus { get; set; }

        [DataMember()]
        public Int32 i_RecordType { get; set; }

        [DataMember()]
        public String v_CreationUser { get; set; }

        [DataMember()]
        public String v_UpdateUser { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_CreationDate { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_UpdateDate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ParameterId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ParentParameterId { get; set; }

        public getfamilymedicalantecedentspagedandfilteredbypersonid_spResultDto()
        {
        }

        public getfamilymedicalantecedentspagedandfilteredbypersonid_spResultDto(String v_FamilyMedicalAntecedentsId, String v_PersonId, String v_DiseasesId, String v_DiseaseName, Nullable<Int32> i_TypeFamilyId, String v_TypeFamilyName, String v_Comment, Int32 i_RecordStatus, Int32 i_RecordType, String v_CreationUser, String v_UpdateUser, Nullable<DateTime> d_CreationDate, Nullable<DateTime> d_UpdateDate, Nullable<Int32> i_ParameterId, Nullable<Int32> i_ParentParameterId)
        {
			this.v_FamilyMedicalAntecedentsId = v_FamilyMedicalAntecedentsId;
			this.v_PersonId = v_PersonId;
			this.v_DiseasesId = v_DiseasesId;
			this.v_DiseaseName = v_DiseaseName;
			this.i_TypeFamilyId = i_TypeFamilyId;
			this.v_TypeFamilyName = v_TypeFamilyName;
			this.v_Comment = v_Comment;
			this.i_RecordStatus = i_RecordStatus;
			this.i_RecordType = i_RecordType;
			this.v_CreationUser = v_CreationUser;
			this.v_UpdateUser = v_UpdateUser;
			this.d_CreationDate = d_CreationDate;
			this.d_UpdateDate = d_UpdateDate;
			this.i_ParameterId = i_ParameterId;
			this.i_ParentParameterId = i_ParentParameterId;
        }
    }
}
