//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:06:47
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
    public partial class getaccessusermonthResultDto
    {
        [DataMember()]
        public Nullable<Int32> Enero { get; set; }

        [DataMember()]
        public Nullable<Int32> Febrero { get; set; }

        [DataMember()]
        public Nullable<Int32> Marzo { get; set; }

        [DataMember()]
        public Nullable<Int32> Abril { get; set; }

        [DataMember()]
        public Nullable<Int32> Mayo { get; set; }

        [DataMember()]
        public Nullable<Int32> Junio { get; set; }

        [DataMember()]
        public Nullable<Int32> Julio { get; set; }

        [DataMember()]
        public Nullable<Int32> Agosto { get; set; }

        [DataMember()]
        public Nullable<Int32> Septiembre { get; set; }

        [DataMember()]
        public Nullable<Int32> Octubre { get; set; }

        [DataMember()]
        public Nullable<Int32> Noviembre { get; set; }

        [DataMember()]
        public Nullable<Int32> Diciembre { get; set; }

        public getaccessusermonthResultDto()
        {
        }

        public getaccessusermonthResultDto(Nullable<Int32> enero, Nullable<Int32> febrero, Nullable<Int32> marzo, Nullable<Int32> abril, Nullable<Int32> mayo, Nullable<Int32> junio, Nullable<Int32> julio, Nullable<Int32> agosto, Nullable<Int32> septiembre, Nullable<Int32> octubre, Nullable<Int32> noviembre, Nullable<Int32> diciembre)
        {
			this.Enero = enero;
			this.Febrero = febrero;
			this.Marzo = marzo;
			this.Abril = abril;
			this.Mayo = mayo;
			this.Junio = junio;
			this.Julio = julio;
			this.Agosto = agosto;
			this.Septiembre = septiembre;
			this.Octubre = octubre;
			this.Noviembre = noviembre;
			this.Diciembre = diciembre;
        }
    }
}
