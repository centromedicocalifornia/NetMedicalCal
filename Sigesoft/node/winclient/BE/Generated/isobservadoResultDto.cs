//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2023/03/31 - 10:23:53
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
    public partial class isobservadoResultDto
    {
        [DataMember()]
        public Nullable<Int32> i_AptitudeStatusId { get; set; }

        public isobservadoResultDto()
        {
        }

        public isobservadoResultDto(Nullable<Int32> i_AptitudeStatusId)
        {
			this.i_AptitudeStatusId = i_AptitudeStatusId;
        }
    }
}
