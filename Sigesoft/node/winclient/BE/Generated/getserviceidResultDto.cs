//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:09:00
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
    public partial class getserviceidResultDto
    {
        [DataMember()]
        public String v_ServiceId { get; set; }

        public getserviceidResultDto()
        {
        }

        public getserviceidResultDto(String v_ServiceId)
        {
			this.v_ServiceId = v_ServiceId;
        }
    }
}
