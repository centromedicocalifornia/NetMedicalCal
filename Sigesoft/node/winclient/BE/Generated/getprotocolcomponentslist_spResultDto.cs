//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:07:56
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
    public partial class getprotocolcomponentslist_spResultDto
    {
        [DataMember()]
        public String v_ComponentId { get; set; }

        [DataMember()]
        public String v_ComponentName { get; set; }

        [DataMember()]
        public String v_Porcentajes { get; set; }

        [DataMember()]
        public String v_ProtocolComponentId { get; set; }

        [DataMember()]
        public Nullable<Single> r_Price { get; set; }

        [DataMember()]
        public String v_Operator { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Age { get; set; }

        [DataMember()]
        public String v_Gender { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsConditionalIMC { get; set; }

        [DataMember()]
        public Nullable<Decimal> r_Imc { get; set; }

        [DataMember()]
        public String v_IsConditional { get; set; }

        [DataMember()]
        public Nullable<Int32> i_isAdditional { get; set; }

        [DataMember()]
        public String v_ComponentTypeName { get; set; }

        [DataMember()]
        public Int32 i_RecordStatus { get; set; }

        [DataMember()]
        public Int32 i_RecordType { get; set; }

        [DataMember()]
        public Nullable<Int32> i_GenderId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_GrupoEtarioId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsConditionalId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_OperatorId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsDeleted { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_CreationDate { get; set; }

        [DataMember()]
        public String v_CategoryName { get; set; }

        [DataMember()]
        public Nullable<Int32> i_CategoryId { get; set; }

        public getprotocolcomponentslist_spResultDto()
        {
        }

        public getprotocolcomponentslist_spResultDto(String v_ComponentId, String v_ComponentName, String v_Porcentajes, String v_ProtocolComponentId, Nullable<Single> r_Price, String v_Operator, Nullable<Int32> i_Age, String v_Gender, Nullable<Int32> i_IsConditionalIMC, Nullable<Decimal> r_Imc, String v_IsConditional, Nullable<Int32> i_isAdditional, String v_ComponentTypeName, Int32 i_RecordStatus, Int32 i_RecordType, Nullable<Int32> i_GenderId, Nullable<Int32> i_GrupoEtarioId, Nullable<Int32> i_IsConditionalId, Nullable<Int32> i_OperatorId, Nullable<Int32> i_IsDeleted, Nullable<DateTime> d_CreationDate, String v_CategoryName, Nullable<Int32> i_CategoryId)
        {
			this.v_ComponentId = v_ComponentId;
			this.v_ComponentName = v_ComponentName;
			this.v_Porcentajes = v_Porcentajes;
			this.v_ProtocolComponentId = v_ProtocolComponentId;
			this.r_Price = r_Price;
			this.v_Operator = v_Operator;
			this.i_Age = i_Age;
			this.v_Gender = v_Gender;
			this.i_IsConditionalIMC = i_IsConditionalIMC;
			this.r_Imc = r_Imc;
			this.v_IsConditional = v_IsConditional;
			this.i_isAdditional = i_isAdditional;
			this.v_ComponentTypeName = v_ComponentTypeName;
			this.i_RecordStatus = i_RecordStatus;
			this.i_RecordType = i_RecordType;
			this.i_GenderId = i_GenderId;
			this.i_GrupoEtarioId = i_GrupoEtarioId;
			this.i_IsConditionalId = i_IsConditionalId;
			this.i_OperatorId = i_OperatorId;
			this.i_IsDeleted = i_IsDeleted;
			this.d_CreationDate = d_CreationDate;
			this.v_CategoryName = v_CategoryName;
			this.i_CategoryId = i_CategoryId;
        }
    }
}
