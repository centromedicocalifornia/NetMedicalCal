//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:15:25
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
    public partial class typeofeepDto
    {
        [DataMember()]
        public String v_TypeofEEPId { get; set; }

        [DataMember()]
        public String v_HistoryId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_TypeofEEPId { get; set; }

        [DataMember()]
        public Nullable<Single> r_Percentage { get; set; }

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
        public String v_ComentaryUpdate { get; set; }

        [DataMember()]
        public historyDto history { get; set; }

        public typeofeepDto()
        {
        }

        public typeofeepDto(String v_TypeofEEPId, String v_HistoryId, Nullable<Int32> i_TypeofEEPId, Nullable<Single> r_Percentage, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, String v_ComentaryUpdate, historyDto history)
        {
			this.v_TypeofEEPId = v_TypeofEEPId;
			this.v_HistoryId = v_HistoryId;
			this.i_TypeofEEPId = i_TypeofEEPId;
			this.r_Percentage = r_Percentage;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.v_ComentaryUpdate = v_ComentaryUpdate;
			this.history = history;
        }
    }
}
