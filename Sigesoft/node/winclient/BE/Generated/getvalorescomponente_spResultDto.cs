//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:09:51
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
    public partial class getvalorescomponente_spResultDto
    {
        [DataMember()]
        public String v_ComponentFieldId { get; set; }

        [DataMember()]
        public String v_ComponentFielName { get; set; }

        [DataMember()]
        public String v_ServiceComponentFieldsId { get; set; }

        [DataMember()]
        public String v_Value1 { get; set; }

        [DataMember()]
        public Nullable<Int32> i_GroupId { get; set; }

        [DataMember()]
        public String v_Value1Name { get; set; }

        public getvalorescomponente_spResultDto()
        {
        }

        public getvalorescomponente_spResultDto(String v_ComponentFieldId, String v_ComponentFielName, String v_ServiceComponentFieldsId, String v_Value1, Nullable<Int32> i_GroupId, String v_Value1Name)
        {
			this.v_ComponentFieldId = v_ComponentFieldId;
			this.v_ComponentFielName = v_ComponentFielName;
			this.v_ServiceComponentFieldsId = v_ServiceComponentFieldsId;
			this.v_Value1 = v_Value1;
			this.i_GroupId = i_GroupId;
			this.v_Value1Name = v_Value1Name;
        }
    }
}
