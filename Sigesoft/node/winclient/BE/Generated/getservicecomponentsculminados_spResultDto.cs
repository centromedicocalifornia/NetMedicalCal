//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:08:42
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
    public partial class getservicecomponentsculminados_spResultDto
    {
        [DataMember()]
        public String v_ComponentId { get; set; }

        [DataMember()]
        public String v_ComponentName { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ServiceComponentStatusId { get; set; }

        [DataMember()]
        public String v_ServiceComponentStatusName { get; set; }

        [DataMember()]
        public Nullable<Int32> i_CategoryId { get; set; }

        public getservicecomponentsculminados_spResultDto()
        {
        }

        public getservicecomponentsculminados_spResultDto(String v_ComponentId, String v_ComponentName, Nullable<Int32> i_ServiceComponentStatusId, String v_ServiceComponentStatusName, Nullable<Int32> i_CategoryId)
        {
			this.v_ComponentId = v_ComponentId;
			this.v_ComponentName = v_ComponentName;
			this.i_ServiceComponentStatusId = i_ServiceComponentStatusId;
			this.v_ServiceComponentStatusName = v_ServiceComponentStatusName;
			this.i_CategoryId = i_CategoryId;
        }
    }
}
