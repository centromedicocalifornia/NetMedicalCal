//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:07:54
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
    public partial class getpersonmedicalhistoryreport_spResultDto
    {
        [DataMember()]
        public Nullable<Int64> i_Item { get; set; }

        [DataMember()]
        public String v_PersonMedicalHistoryId { get; set; }

        [DataMember()]
        public String v_PersonId { get; set; }

        [DataMember()]
        public String v_DiseasesId { get; set; }

        [DataMember()]
        public String v_DiseasesName { get; set; }

        [DataMember()]
        public Nullable<Int32> i_TypeDiagnosticId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_StartDate { get; set; }

        [DataMember()]
        public String v_TreatmentSite { get; set; }

        [DataMember()]
        public String v_GroupName { get; set; }

        [DataMember()]
        public String v_TypeDiagnosticName { get; set; }

        [DataMember()]
        public String v_DiagnosticDetail { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Answer { get; set; }

        [DataMember()]
        public String NombreHospital { get; set; }

        [DataMember()]
        public String v_Complicaciones { get; set; }

        public getpersonmedicalhistoryreport_spResultDto()
        {
        }

        public getpersonmedicalhistoryreport_spResultDto(Nullable<Int64> i_Item, String v_PersonMedicalHistoryId, String v_PersonId, String v_DiseasesId, String v_DiseasesName, Nullable<Int32> i_TypeDiagnosticId, Nullable<DateTime> d_StartDate, String v_TreatmentSite, String v_GroupName, String v_TypeDiagnosticName, String v_DiagnosticDetail, Nullable<Int32> i_Answer, String nombreHospital, String v_Complicaciones)
        {
			this.i_Item = i_Item;
			this.v_PersonMedicalHistoryId = v_PersonMedicalHistoryId;
			this.v_PersonId = v_PersonId;
			this.v_DiseasesId = v_DiseasesId;
			this.v_DiseasesName = v_DiseasesName;
			this.i_TypeDiagnosticId = i_TypeDiagnosticId;
			this.d_StartDate = d_StartDate;
			this.v_TreatmentSite = v_TreatmentSite;
			this.v_GroupName = v_GroupName;
			this.v_TypeDiagnosticName = v_TypeDiagnosticName;
			this.v_DiagnosticDetail = v_DiagnosticDetail;
			this.i_Answer = i_Answer;
			this.NombreHospital = nombreHospital;
			this.v_Complicaciones = v_Complicaciones;
        }
    }
}
