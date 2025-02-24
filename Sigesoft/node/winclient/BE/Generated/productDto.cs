//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:14:01
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
    public partial class productDto
    {
        [DataMember()]
        public String v_ProductId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_CategoryId { get; set; }

        [DataMember()]
        public String v_Name { get; set; }

        [DataMember()]
        public String v_GenericName { get; set; }

        [DataMember()]
        public String v_BarCode { get; set; }

        [DataMember()]
        public String v_ProductCode { get; set; }

        [DataMember()]
        public String v_Brand { get; set; }

        [DataMember()]
        public String v_Model { get; set; }

        [DataMember()]
        public String v_SerialNumber { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_ExpirationDate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_MeasurementUnitId { get; set; }

        [DataMember()]
        public Nullable<Single> r_ReferentialCostPrice { get; set; }

        [DataMember()]
        public Nullable<Single> r_ReferentialSalesPrice { get; set; }

        [DataMember()]
        public String v_Presentation { get; set; }

        [DataMember()]
        public String v_AdditionalInformation { get; set; }

        [DataMember()]
        public Byte[] b_Image { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsDeleted { get; set; }

        [DataMember()]
        public Nullable<Int32> i_InsertUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_InsertDate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UpdateUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_UpdateDate { get; set; }

        [DataMember()]
        public String v_ComentaryUpdate { get; set; }

        [DataMember()]
        public List<movementdetailDto> movementdetail { get; set; }

        [DataMember()]
        public List<productwarehouseDto> productwarehouse { get; set; }

        public productDto()
        {
        }

        public productDto(String v_ProductId, Nullable<Int32> i_CategoryId, String v_Name, String v_GenericName, String v_BarCode, String v_ProductCode, String v_Brand, String v_Model, String v_SerialNumber, Nullable<DateTime> d_ExpirationDate, Nullable<Int32> i_MeasurementUnitId, Nullable<Single> r_ReferentialCostPrice, Nullable<Single> r_ReferentialSalesPrice, String v_Presentation, String v_AdditionalInformation, Byte[] b_Image, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, String v_ComentaryUpdate, List<movementdetailDto> movementdetail, List<productwarehouseDto> productwarehouse)
        {
			this.v_ProductId = v_ProductId;
			this.i_CategoryId = i_CategoryId;
			this.v_Name = v_Name;
			this.v_GenericName = v_GenericName;
			this.v_BarCode = v_BarCode;
			this.v_ProductCode = v_ProductCode;
			this.v_Brand = v_Brand;
			this.v_Model = v_Model;
			this.v_SerialNumber = v_SerialNumber;
			this.d_ExpirationDate = d_ExpirationDate;
			this.i_MeasurementUnitId = i_MeasurementUnitId;
			this.r_ReferentialCostPrice = r_ReferentialCostPrice;
			this.r_ReferentialSalesPrice = r_ReferentialSalesPrice;
			this.v_Presentation = v_Presentation;
			this.v_AdditionalInformation = v_AdditionalInformation;
			this.b_Image = b_Image;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.v_ComentaryUpdate = v_ComentaryUpdate;
			this.movementdetail = movementdetail;
			this.productwarehouse = productwarehouse;
        }
    }
}
