//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:06:29
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
    public partial class accessuserResultDto
    {
        [DataMember()]
        public String namePerson { get; set; }

        [DataMember()]
        public String lastNamePerson { get; set; }

        [DataMember()]
        public Byte[] b_PersonImage { get; set; }

        public accessuserResultDto()
        {
        }

        public accessuserResultDto(String namePerson, String lastNamePerson, Byte[] b_PersonImage)
        {
			this.namePerson = namePerson;
			this.lastNamePerson = lastNamePerson;
			this.b_PersonImage = b_PersonImage;
        }
    }
}
