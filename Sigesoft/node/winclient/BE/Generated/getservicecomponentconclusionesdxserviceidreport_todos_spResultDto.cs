//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:08:33
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
    public partial class getservicecomponentconclusionesdxserviceidreport_todos_spResultDto
    {
        [DataMember()]
        public Nullable<Int64> i_Item { get; set; }

        [DataMember()]
        public String v_DiagnosticRepositoryId { get; set; }

        [DataMember()]
        public String v_ServiceId { get; set; }

        [DataMember()]
        public String v_ComponentId { get; set; }

        [DataMember()]
        public String v_DiseasesId { get; set; }

        [DataMember()]
        public String v_DiseasesName { get; set; }

        [DataMember()]
        public String v_ComponentName { get; set; }

        [DataMember()]
        public String v_PreQualificationName { get; set; }

        [DataMember()]
        public String v_FinalQualificationName { get; set; }

        [DataMember()]
        public String v_DiagnosticTypeName { get; set; }

        [DataMember()]
        public String v_ComponentFieldsId { get; set; }

        [DataMember()]
        public String v_Dx_CIE10 { get; set; }

        [DataMember()]
        public Nullable<Int32> i_DiagnosticTypeId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_FinalQualificationId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_CategoryId { get; set; }

        [DataMember()]
        public String Categoria { get; set; }

        public getservicecomponentconclusionesdxserviceidreport_todos_spResultDto()
        {
        }

        public getservicecomponentconclusionesdxserviceidreport_todos_spResultDto(Nullable<Int64> i_Item, String v_DiagnosticRepositoryId, String v_ServiceId, String v_ComponentId, String v_DiseasesId, String v_DiseasesName, String v_ComponentName, String v_PreQualificationName, String v_FinalQualificationName, String v_DiagnosticTypeName, String v_ComponentFieldsId, String v_Dx_CIE10, Nullable<Int32> i_DiagnosticTypeId, Nullable<Int32> i_FinalQualificationId, Nullable<Int32> i_CategoryId, String categoria)
        {
			this.i_Item = i_Item;
			this.v_DiagnosticRepositoryId = v_DiagnosticRepositoryId;
			this.v_ServiceId = v_ServiceId;
			this.v_ComponentId = v_ComponentId;
			this.v_DiseasesId = v_DiseasesId;
			this.v_DiseasesName = v_DiseasesName;
			this.v_ComponentName = v_ComponentName;
			this.v_PreQualificationName = v_PreQualificationName;
			this.v_FinalQualificationName = v_FinalQualificationName;
			this.v_DiagnosticTypeName = v_DiagnosticTypeName;
			this.v_ComponentFieldsId = v_ComponentFieldsId;
			this.v_Dx_CIE10 = v_Dx_CIE10;
			this.i_DiagnosticTypeId = i_DiagnosticTypeId;
			this.i_FinalQualificationId = i_FinalQualificationId;
			this.i_CategoryId = i_CategoryId;
			this.Categoria = categoria;
        }
    }
}
