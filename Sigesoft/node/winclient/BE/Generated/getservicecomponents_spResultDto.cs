//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:08:41
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
    public partial class getservicecomponents_spResultDto
    {
        [DataMember()]
        public String v_ComponentId { get; set; }

        public getservicecomponents_spResultDto()
        {
        }

        public getservicecomponents_spResultDto(String v_ComponentId)
        {
			this.v_ComponentId = v_ComponentId;
        }
    }
}
