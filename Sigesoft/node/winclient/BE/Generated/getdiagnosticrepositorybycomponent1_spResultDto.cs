//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:07:04
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
    public partial class getdiagnosticrepositorybycomponent1_spResultDto
    {
        [DataMember()]
        public String v_DiagnosticRepositoryId { get; set; }

        [DataMember()]
        public String v_RecommendationName { get; set; }

        [DataMember()]
        public String v_ServiceId { get; set; }

        public getdiagnosticrepositorybycomponent1_spResultDto()
        {
        }

        public getdiagnosticrepositorybycomponent1_spResultDto(String v_DiagnosticRepositoryId, String v_RecommendationName, String v_ServiceId)
        {
			this.v_DiagnosticRepositoryId = v_DiagnosticRepositoryId;
			this.v_RecommendationName = v_RecommendationName;
			this.v_ServiceId = v_ServiceId;
        }
    }
}
