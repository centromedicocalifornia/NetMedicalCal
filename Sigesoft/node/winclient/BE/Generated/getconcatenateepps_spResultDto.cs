//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:06:59
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
    public partial class getconcatenateepps_spResultDto
    {
        [DataMember()]
        public String v_Epps { get; set; }

        public getconcatenateepps_spResultDto()
        {
        }

        public getconcatenateepps_spResultDto(String v_Epps)
        {
			this.v_Epps = v_Epps;
        }
    }
}
