//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2023/03/31 - 10:18:10
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
    public partial class devolverhabitos_personales_listajerarquizada_spResultDto
    {
        [DataMember()]
        public String v_PersonId { get; set; }

        public devolverhabitos_personales_listajerarquizada_spResultDto()
        {
        }

        public devolverhabitos_personales_listajerarquizada_spResultDto(String v_PersonId)
        {
			this.v_PersonId = v_PersonId;
        }
    }
}
