using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Common
{
   //[DataContract]
   public class KeyValueDTO
    {
        //[DataMember]
        public string Id { get; set; }
        //[DataMember]
        public string Value1 { get; set; }
        //[DataMember]
        public string Value2 { get; set; }
        //[DataMember]
        public string Value3 { get; set; }

        public Single Value4 { get; set; }

        public byte[] Value5 { get; set; }

        public string Field { get; set; }

        public int IdI { get; set; }
        public int GrupoId { get; set; }

        public int ParameterId { get; set; }
       
        public int ParentId { get; set; }

        public string Nombre { get; set; }
    }

   public class KeyValueDTOCheck
   {
       public string Id { get; set; }
       public string Value1 { get; set; }
   }

   public class GroupParameter
   {
       public int GroupId { get; set; }
       public string Id { get; set; }
       public List<KeyValueDTO> Items { get; set; }
   }

   public class STS_Val
   {
       public string AUB_OD { get; set; }
       public string AUB_OI { get; set; }
       public string AUA_OD { get; set; }
       public string AUA_OI { get; set; }
       public string AB_OD_2000 { get; set; }
       public string AB_OD_3000 { get; set; }
       public string AB_OD_4000 { get; set; }
       public string AB_OI_2000 { get; set; }
       public string AB_OI_3000 { get; set; }
       public string AB_OI_4000 { get; set; }
       public string AA_OD_2000 { get; set; }
       public string AA_OD_3000 { get; set; }
       public string AA_OD_4000 { get; set; }
       public string AA_OI_2000 { get; set; }
       public string AA_OI_3000 { get; set; }
       public string AA_OI_4000 { get; set; }

       public string STS_OD { get; set; }
       public string STS_OI { get; set; }

   }
}
