//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:09:15
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
    public partial class getservicescomponentarchivo_spResultDto
    {
        [DataMember()]
        public String v_FileName { get; set; }

        public getservicescomponentarchivo_spResultDto()
        {
        }

        public getservicescomponentarchivo_spResultDto(String v_FileName)
        {
			this.v_FileName = v_FileName;
        }
    }
}
