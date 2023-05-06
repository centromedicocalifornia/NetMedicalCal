using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Linq.Dynamic;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Common;
using System.Collections;
using Sigesoft.Node.WinClient.BE.Custom;

namespace Sigesoft.Node.WinClient.BLL
{
    public class ServiceOrderBL
    {
        public List<ServiceOrderList> GetServiceOrderPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, DateTime? pdatBeginDate, DateTime? pdatEndDate)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.serviceorder
                            join B in dbContext.systemparameter on new { a = A.i_ServiceOrderStatusId.Value, b = 194 } equals new { a = B.i_ParameterId, b = B.i_GroupId }
                            //join C in dbContext.serviceorderdetail on  A.v_ServiceOrderId equals C.v_ServiceOrderId

                            //join E in dbContext.protocol on C.v_ProtocolId equals E.v_ProtocolId

                            // Empresa / Sede Cliente ******************************************************
                            //join oc in dbContext.organization on new { a = E.v_CustomerOrganizationId }
                            //        equals new { a = oc.v_OrganizationId } into oc_join
                            //from oc in oc_join.DefaultIfEmpty()

                            //join lc in dbContext.location on new { a = E.v_CustomerOrganizationId, b = E.v_CustomerLocationId }
                            //      equals new { a = lc.v_OrganizationId, b = lc.v_LocationId } into lc_join
                            //from lc in lc_join.DefaultIfEmpty()

                            //// Empresa / Sede Empleadora *******************************************
                            //join oe in dbContext.organization on new { a = E.v_EmployerOrganizationId }
                            //      equals new { a = oe.v_OrganizationId } into oe_join
                            //from oe in oe_join.DefaultIfEmpty()

                            //join le in dbContext.location on new { a = E.v_EmployerOrganizationId, b = E.v_EmployerLocationId }
                            //       equals new { a = le.v_OrganizationId, b = le.v_LocationId } into le_join
                            //from le in le_join.DefaultIfEmpty()

                            //// Empresa / Sede Trabajo *******************************************
                            //join ow in dbContext.organization on new { a = E.v_WorkingOrganizationId }
                            //      equals new { a = ow.v_OrganizationId } into ow_join
                            //from ow in ow_join.DefaultIfEmpty()

                            //join lw in dbContext.location on new { a = E.v_WorkingOrganizationId, b = E.v_WorkingLocationId }
                            //       equals new { a = lw.v_OrganizationId, b = lw.v_LocationId } into lw_join
                            //from lw in lw_join.DefaultIfEmpty()
                            //// *****************************************************************************

                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                            equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()
                            where A.i_IsDeleted == 0
                            select new ServiceOrderList
                            {
                             v_ServiceOrderId = A.v_ServiceOrderId,
                             v_CustomServiceOrderId = A.v_CustomServiceOrderId,
                             v_Description = A.v_Description,
                             v_Comentary = A.v_Comentary,
                             i_NumberOfWorker = A.i_NumberOfWorker,
                             r_TotalCost = A.r_TotalCost,
                             d_DeliveryDate= A.d_DeliveryDate,

                             i_ServiceOrderStatusId = A.i_ServiceOrderStatusId,
                             v_ServiceOrderStatusName = B.v_Value1,
                             v_CreationUser = J1.v_UserName,
                             v_UpdateUser = J2.v_UserName,
                             d_CreationDate = A.d_InsertDate,
                             d_UpdateDate = A.d_UpdateDate,
                             //v_ProtocolId = C.v_ProtocolId,

                             //v_CustomerOrganizationId = E.v_CustomerOrganizationId,
                             //v_CustomerLocationId = E.v_CustomerLocationId,
                             //v_EmployerOrganizationId = E.v_EmployerOrganizationId,
                             //v_EmployerLocationId = E.v_EmployerLocationId,
                             //v_WorkingOrganizationId = E.v_WorkingOrganizationId,
                             //v_WorkingLocationId = E.v_WorkingLocationId
                            };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (pdatBeginDate.HasValue && pdatEndDate.HasValue)
                {
                    query = query.Where("d_DeliveryDate >= @0 && d_DeliveryDate <= @1 || d_DeliveryDate == NULL", pdatBeginDate.Value, pdatEndDate.Value);
                }
                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                if (pintPageIndex.HasValue && pintResultsPerPage.HasValue)
                {
                    int intStartRowIndex = pintPageIndex.Value * pintResultsPerPage.Value;
                    query = query.Skip(intStartRowIndex);
                }
                if (pintResultsPerPage.HasValue)
                {
                    query = query.Take(pintResultsPerPage.Value);
                }

                List<ServiceOrderList> objData = query.ToList();
                pobjOperationResult.Success = 1;
                return objData;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public List<ServiceOrderList> GetServiceOrderPagedAndFilteredCotizaciones(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, DateTime? pdatBeginDate, DateTime? pdatEndDate)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.cotizacionoc
                            join A1 in dbContext.organization on new { v_OrganizationId = A.i_EmpresaId }
                                equals new { v_OrganizationId = A1.v_OrganizationId } into A1_join
                            join B in dbContext.systemparameter on new { a = A.i_MostrarPrecio.Value, b = 194 } equals new { a = B.i_ParameterId, b = B.i_GroupId }
                            
                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            where A.i_IsDeleted == 0
                            select new ServiceOrderList
                            {
                                v_ServiceOrderId = A.i_CotizacionIdOc,
                                d_DeliveryDate = A.d_DeliveryDate.Value,
                                v_CustomServiceOrderId = A.v_Sumilla.Replace("(XXX)", A.i_CotizacionIdOc),
                                i_NumberOfWorker = A.i_NumberOfWorker,
                                r_TotalCost = A.r_TotalCost,
                                v_ServiceOrderStatusName = B.v_Value1,
                                v_Comentary = A.v_RazonSocial,
                                v_Description = A.v_Description,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate,
                             
                            };

                if (pdatBeginDate.HasValue && pdatEndDate.HasValue)
                {
                    query = query.Where("d_DeliveryDate >= @0 && d_DeliveryDate <= @1 || d_DeliveryDate == NULL", pdatBeginDate.Value, pdatEndDate.Value);
                }
              
                if (pintPageIndex.HasValue && pintResultsPerPage.HasValue)
                {
                    int intStartRowIndex = pintPageIndex.Value * pintResultsPerPage.Value;
                    query = query.Skip(intStartRowIndex);
                }
                if (pintResultsPerPage.HasValue)
                {
                    query = query.Take(pintResultsPerPage.Value);
                }

                List<ServiceOrderList> objData = query.ToList();
                pobjOperationResult.Success = 1;
                return objData;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public CotizacionocDtoList GetCotizacionesbj(ref OperationResult pobjOperationResult, string CotizacionId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from A in dbContext.cotizacionoc
                            join A1 in dbContext.organization on new { v_OrganizationId = A.i_EmpresaId }
                                equals new { v_OrganizationId = A1.v_OrganizationId } into A1_join
                             from A1 in A1_join.DefaultIfEmpty()

                            join A2 in dbContext.location on new { v_OrganizationId = A1.v_OrganizationId }
                                equals new { v_OrganizationId = A2.v_OrganizationId } into A2_join
                            from A2 in A2_join.DefaultIfEmpty()

                             join B in dbContext.datahierarchy on new { a = A.i_LineaCreditoId.Value, b = 122 } equals new { a = B.i_ItemId, b = B.i_GroupId }

                             join J1 in dbContext.systemuser on new { i_InsertUserId = 175 } //A.i_InsertUserId.Value
                                equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            join J3 in dbContext.person on new { UsuarioGraba = J1.v_PersonId }
                                equals new { UsuarioGraba = J3.v_PersonId } into J3_join
                            from J3 in J3_join.DefaultIfEmpty()

                             join J4 in dbContext.professional on new { v_PersonId = J1.v_PersonId }//
                                equals new { v_PersonId = J4.v_PersonId } into J4_join
                            from J4 in J4_join.DefaultIfEmpty()

                            where A.i_IsDeleted == 0 && A.i_CotizacionIdOc == CotizacionId
                            select new CotizacionocDtoList
                            {
                                i_CotizacionIdOc = A.i_CotizacionIdOc,
                                i_EmpresaId = A.i_EmpresaId,
                                i_PersonId = A.i_PersonId,
                                v_Description = A.v_Description,
                                v_Comentary = A.v_Comentary,
                                i_NumberOfWorker = A.i_NumberOfWorker.Value,
                                r_TotalCost = A.r_TotalCost,
                                d_DeliveryDate = A.d_DeliveryDate.Value,
                                i_LineaCreditoId = A.i_LineaCreditoId.Value,
                                i_MostrarPrecio = A.i_MostrarPrecio.Value,
                                LineaCredito = B.v_Value1,
                                i_InsertUserId = A.i_InsertUserId.Value,
                                Usuario = J3.v_FirstName + " " + J3.v_FirstLastName + " " + J3.v_SecondLastName,
                                celularUsuarioGraba = J3.v_TelephoneNumber,
                                EmailUsuarioGraba = J3.v_Mail,
                                d_InsertDate = A.d_InsertDate.Value,
                                v_RazonSocial = A.v_RazonSocial,
                                v_RepresentanteLegal = A.v_RepresentanteLegal,
                                v_DireccionEmpresa = A.v_DireccionEmpresa,
                                v_Sumilla = A.v_Sumilla.Replace("(XXX)", A.i_CotizacionIdOc),
                                v_Asunto = A.v_Asunto,
                                i_IsDeleted = A.i_IsDeleted.Value,
                                FirmaUsuario = J4.b_SignatureImage,
                                i_Locationd = A2.v_LocationId,
                                v_Interconsultas = A.v_Interconsultas,
                                v_Anotaciones = A.v_Anotaciones
                            }).FirstOrDefault();
                
                pobjOperationResult.Success = 1;
                return query;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public CotizacionocDtoList GetCotizacionesClonebj(ref OperationResult pobjOperationResult, string CotizacionId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from A in dbContext.cotizacionoc
                             join A1 in dbContext.organization on new { v_OrganizationId = A.i_EmpresaId }
                                 equals new { v_OrganizationId = A1.v_OrganizationId } into A1_join
                             from A1 in A1_join.DefaultIfEmpty()

                             join A2 in dbContext.location on new { v_OrganizationId = A1.v_OrganizationId }
                                 equals new { v_OrganizationId = A2.v_OrganizationId } into A2_join
                             from A2 in A2_join.DefaultIfEmpty()

                             join B in dbContext.datahierarchy on new { a = A.i_LineaCreditoId.Value, b = 122 } equals new { a = B.i_ItemId, b = B.i_GroupId }

                             join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                 equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                             from J1 in J1_join.DefaultIfEmpty()

                             join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                 equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                             from J2 in J2_join.DefaultIfEmpty()

                             join J3 in dbContext.person on new { UsuarioGraba = J1.v_PersonId }
                                 equals new { UsuarioGraba = J3.v_PersonId } into J3_join
                             from J3 in J3_join.DefaultIfEmpty()

                             join J4 in dbContext.professional on new { v_PersonId = J1.v_PersonId }
                                equals new { v_PersonId = J4.v_PersonId } into J4_join
                             from J4 in J4_join.DefaultIfEmpty()

                             where A.i_IsDeleted == 0 && A.i_CotizacionIdOc == CotizacionId
                             select new CotizacionocDtoList
                             {
                                 //i_CotizacionIdOc = A.i_CotizacionIdOc,
                                 i_EmpresaId = A.i_EmpresaId,
                                 i_PersonId = A.i_PersonId,
                                 v_Description = A.v_Description,
                                 v_Comentary = A.v_Comentary,
                                 i_NumberOfWorker = A.i_NumberOfWorker.Value,
                                 r_TotalCost = A.r_TotalCost,
                                 d_DeliveryDate = A.d_DeliveryDate.Value,
                                 i_LineaCreditoId = A.i_LineaCreditoId.Value,
                                 i_MostrarPrecio = A.i_MostrarPrecio.Value,
                                 LineaCredito = B.v_Value1,
                                 i_InsertUserId = A.i_InsertUserId.Value,
                                 Usuario = J3.v_FirstName + " " + J3.v_FirstLastName + " " + J3.v_SecondLastName,
                                 celularUsuarioGraba = J3.v_TelephoneNumber,
                                 EmailUsuarioGraba = J3.v_Mail,
                                 d_InsertDate = A.d_InsertDate.Value,
                                 v_RazonSocial = A.v_RazonSocial,
                                 v_RepresentanteLegal = A.v_RepresentanteLegal,
                                 v_DireccionEmpresa = A.v_DireccionEmpresa,
                                 v_Sumilla = "CARTA: (XXX) -CSL S.R.L.-CAJ/GG.",
                                 v_Asunto = A.v_Asunto,
                                 i_IsDeleted = A.i_IsDeleted.Value,
                                 FirmaUsuario = J4.b_SignatureImage,
                                 i_Locationd = A2.v_LocationId
                             }).FirstOrDefault();

                pobjOperationResult.Success = 1;
                return query;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }


        public List<CotizacionocdetaillList> GetCotizacionesListabj(ref OperationResult pobjOperationResult, string CotizacionId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from A in dbContext.cotizacionocdetaill

                                join A1 in dbContext.component on new { a = A.v_ComponentId }
                                    equals new { a = A1.v_ComponentId } into A1_join
                                from A1 in A1_join.DefaultIfEmpty()
                             
                                join B in dbContext.systemparameter on new { a = A.i_EMOTypeD.Value, b = 118 } equals new { a = B.i_ParameterId, b = B.i_GroupId }

                            where A.i_CotizacionIdOc == CotizacionId && A.i_IsDeleted == 0

                             select new CotizacionocdetaillList
                             {
                                 i_CotizacionIdOcDetalle = A.i_CotizacionIdOcDetalle,
                                 i_CotizacionIdOc = A.i_CotizacionIdOc,
                                 v_ComponentId = A.v_ComponentId,
                                 v_Component = A1.v_Name,
                                 r_Price = A.r_Price.Value,
                                 i_EMOTypeD = A.i_EMOTypeD.Value,
                                 v_EMOTypeD = B.v_Value1,
                                 i_IsDeleted = A.i_IsDeleted.Value,
                                 DescripcionOtros = A.DescripcionOtros
                             }).ToList();

                pobjOperationResult.Success = 1;
                return query;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public List<ListaTemporalComponentesCotizacion> GetCotizacionesListTemp(ref OperationResult pobjOperationResult, string CotizacionId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from A in dbContext.cotizacionocdetaill

                    join A1 in dbContext.component on new { a = A.v_ComponentId }
                        equals new { a = A1.v_ComponentId } into A1_join
                    from A1 in A1_join.DefaultIfEmpty()

                    join B in dbContext.systemparameter on new { a = A.i_EMOTypeD.Value, b = 118 } equals new { a = B.i_ParameterId, b = B.i_GroupId }

                    where A.i_CotizacionIdOc == CotizacionId && A.i_IsDeleted == 0

                    select new ListaTemporalComponentesCotizacion
                    {
                        i_CotizacionIdOcDetalle = A.i_CotizacionIdOcDetalle,
                        i_CotizacionIdOc = A.i_CotizacionIdOc,
                        ComponentId = A.v_ComponentId,
                        Examen = A1.v_Name,
                        Precio = A.r_Price.Value,
                        TipoExamenId = A.i_EMOTypeD.Value,
                        Tipo_Examen = B.v_Value1,
                        i_IsDeleted = A.i_IsDeleted.Value,
                        DescripcionOtros = A.DescripcionOtros
                    }).ToList();

                pobjOperationResult.Success = 1;
                return query;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }
        public List<ListaTemporalComponentesCotizacion> GetCotizacionesListTempClone(ref OperationResult pobjOperationResult, string CotizacionId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from A in dbContext.cotizacionocdetaill

                    join A1 in dbContext.component on new { a = A.v_ComponentId }
                        equals new { a = A1.v_ComponentId } into A1_join
                    from A1 in A1_join.DefaultIfEmpty()

                    join B in dbContext.systemparameter on new { a = A.i_EMOTypeD.Value, b = 118 } equals new { a = B.i_ParameterId, b = B.i_GroupId }

                    where A.i_CotizacionIdOc == CotizacionId && A.i_IsDeleted == 0

                    select new ListaTemporalComponentesCotizacion
                    {
                        //i_CotizacionIdOcDetalle = A.i_CotizacionIdOcDetalle,
                        //i_CotizacionIdOc = A.i_CotizacionIdOc,
                        ComponentId = A.v_ComponentId,
                        Examen = A1.v_Name,
                        Precio = A.r_Price.Value,
                        TipoExamenId = A.i_EMOTypeD.Value,
                        Tipo_Examen = B.v_Value1,
                        i_IsDeleted = A.i_IsDeleted.Value
                    }).ToList();

                pobjOperationResult.Success = 1;
                return query;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }


        public string AddCotizacionOc(ref OperationResult pobjOperationResult, cotizacionocDto pobjDtoEntity, List<cotizacionocdetaillDto> pobjDtoEntityDetail, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region Service Order
                cotizacionoc objEntity = cotizacionocAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 700), "CO");
                objEntity.i_CotizacionIdOc = NewId;

                dbContext.AddTocotizacionoc(objEntity);
                dbContext.SaveChanges();
                #endregion

                #region Service Order Detail

                if (pobjDtoEntityDetail != null)
                {
                    foreach (var item in pobjDtoEntityDetail)
                    {
                        cotizacionocdetaill objEntityDetail = cotizacionocdetaillAssembler.ToEntity(item);

                        objEntityDetail.d_InsertDate = DateTime.Now;
                        objEntityDetail.i_InsertUserId = Int32.Parse(ClientSession[2]);
                        objEntityDetail.i_IsDeleted = 0;
                        // Autogeneramos el Pk de la tabla
                        objEntityDetail.i_CotizacionIdOc = NewId;
                        objEntityDetail.i_CotizacionIdOcDetalle = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 701), "CD");

                        dbContext.AddTocotizacionocdetaill(objEntityDetail);
                        dbContext.SaveChanges();
                    }


                }
                pobjOperationResult.Success = 1;
                #endregion

                return NewId;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ORDEN SERVICIO", "v_ServiceOrderId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return null;
            }
        }


        public string AddServiceOrder(ref OperationResult pobjOperationResult, serviceorderDto pobjDtoEntity, List<serviceorderdetailDto> pobjDtoEntityDetail , List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region Service Order
                serviceorder objEntity = serviceorderAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 101), "YY");
                objEntity.v_ServiceOrderId = NewId;

                dbContext.AddToserviceorder(objEntity);
                dbContext.SaveChanges();
                #endregion

                #region Service Order Detail

                if (pobjDtoEntityDetail != null)
                {
                    foreach (var item in pobjDtoEntityDetail)
                    {
                        serviceorderdetail objEntityDetail = serviceorderdetailAssembler.ToEntity(item);

                        objEntityDetail.d_InsertDate = DateTime.Now;
                        objEntityDetail.i_InsertUserId = Int32.Parse(ClientSession[2]);
                        objEntityDetail.i_IsDeleted = 0;
                        // Autogeneramos el Pk de la tabla
                        objEntityDetail.v_ServiceOrderId = NewId;
                        objEntityDetail.v_ServiceOrderDetailId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 102), "YA");

                        dbContext.AddToserviceorderdetail(objEntityDetail);
                        dbContext.SaveChanges();
                    }
                    
               
                }
                pobjOperationResult.Success = 1;
                #endregion

            
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ORDERN SERVICIO", "v_ServiceOrderId=" + NewId.ToString(), Success.Ok, null);
                return NewId;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ORDEN SERVICIO", "v_ServiceOrderId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return null;
            }
        }

        public void UpdateCotizacion(ref OperationResult pobjOperationResult, cotizacionocDto pobjDtoEntity, List<cotizacionocdetaillDto> pobjDtoEntityDetail, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                //// Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.cotizacionoc
                                       where a.i_CotizacionIdOc == pobjDtoEntity.i_CotizacionIdOc
                                       select a).FirstOrDefault();

                pobjDtoEntity.i_IsDeleted = 0;
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                cotizacionoc objEntity = cotizacionocAssembler.ToEntity(pobjDtoEntity);

                dbContext.cotizacionoc.ApplyCurrentValues(objEntity);

                //dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                #region ServiceOrderDetail
                 //Obtener la entidad fuente

                if (pobjDtoEntityDetail != null)
                {
                    int intNodeId = int.Parse(ClientSession[0]);
                    foreach (var item in pobjDtoEntityDetail)
                    {
                        cotizacionocdetaill objDetailEntity = cotizacionocdetaillAssembler.ToEntity(item);

                        if (item.i_CotizacionIdOcDetalle == null)
                        {

                            cotizacionocdetaill objEntityDetail = cotizacionocdetaillAssembler.ToEntity(item);

                            objEntityDetail.d_InsertDate = DateTime.Now;
                            objEntityDetail.i_InsertUserId = Int32.Parse(ClientSession[2]);
                            objEntityDetail.i_IsDeleted = 0;

                            objEntityDetail.i_CotizacionIdOc = pobjDtoEntity.i_CotizacionIdOc;
                            objEntityDetail.i_CotizacionIdOcDetalle = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 701), "CD");

                            dbContext.AddTocotizacionocdetaill(objEntityDetail);
                            //dbContext.SaveChanges();
                        }
                        else
                        {
                            cotizacionocdetaillDto cotizacionocdetaillDto_ = new cotizacionocdetaillDto();

                            var objEntitySource_ = (from a in dbContext.cotizacionocdetaill
                                                   where a.i_CotizacionIdOcDetalle == item.i_CotizacionIdOcDetalle
                                select a).FirstOrDefault();

                            cotizacionocdetaillDto_.i_CotizacionIdOcDetalle = item.i_CotizacionIdOcDetalle;
                            cotizacionocdetaillDto_.i_CotizacionIdOc = pobjDtoEntity.i_CotizacionIdOc;
                            cotizacionocdetaillDto_.r_Price = item.r_Price;
                            cotizacionocdetaillDto_.v_ComponentId = item.v_ComponentId;
                            cotizacionocdetaillDto_.DescripcionOtros = item.DescripcionOtros;


                            cotizacionocdetaillDto_.i_IsDeleted = item.i_IsDeleted;
                            objDetailEntity.d_UpdateDate = DateTime.Now;
                            cotizacionocdetaillDto_.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                            cotizacionocdetaillDto_.i_EMOTypeD = item.i_EMOTypeD;

                            cotizacionocdetaill cotizacionocdetaillobjEntity = cotizacionocdetaillAssembler.ToEntity(cotizacionocdetaillDto_);

                             
                            dbContext.cotizacionocdetaill.ApplyCurrentValues(cotizacionocdetaillobjEntity);

                           // dbContext.SaveChanges();

                        }
                        
                    }
                    
                }
                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "SERVICIO", "v_ServiceOrderId=" + pobjDtoEntity.v_ServiceOrderId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }


        public void UpdateService(ref OperationResult pobjOperationResult, serviceorderDto pobjDtoEntity, List<serviceorderdetailDto> pobjDtoEntityDetail, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region ServiceOrder
                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.serviceorder
                                       where a.v_ServiceOrderId == pobjDtoEntity.v_ServiceOrderId && a.i_IsDeleted == 0
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                serviceorder objEntity = serviceorderAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.serviceorder.ApplyCurrentValues(objEntity);

                #endregion

                #region ServiceOrderDetail
                // Obtener la entidad fuente
                List<serviceorderdetail> pobjserviceorderdetailList = new List<serviceorderdetail>();

                var objEntitySourceDetail = (from a in dbContext.serviceorderdetail
                                             where a.v_ServiceOrderId == pobjDtoEntity.v_ServiceOrderId 
                                       select a).ToList();


                foreach (var item in objEntitySourceDetail)
                {
                    dbContext.serviceorderdetail.DeleteObject(item);
                    dbContext.SaveChanges();
                }

                if (pobjDtoEntityDetail != null)
                {
                      int intNodeId = int.Parse(ClientSession[0]);
                    foreach (var item in pobjDtoEntityDetail)
                    {
                        serviceorderdetail objDetailEntity = serviceorderdetailAssembler.ToEntity(item);

                        if (item.v_ServiceOrderDetailId == null)
                        {
                            objDetailEntity.v_ServiceOrderDetailId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 102), "YA");
                        }
                        else
                        {
                            objDetailEntity.v_ServiceOrderDetailId = item.v_ServiceOrderDetailId;
                        }
                      
                       
                        objDetailEntity.v_ProtocolId = item.v_ProtocolId;
                        objDetailEntity.v_ServiceOrderId = pobjDtoEntity.v_ServiceOrderId;
                        objDetailEntity.r_ProtocolPrice = item.r_ProtocolPrice;
                        objDetailEntity.i_NumberOfWorkerProtocol = item.i_NumberOfWorkerProtocol;

                        objDetailEntity.i_IsDeleted = 0;
                        objDetailEntity.d_UpdateDate = DateTime.Now;
                        objDetailEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                        // Agregar el detalle del movimiento a la BD
                        dbContext.AddToserviceorderdetail(objDetailEntity);

                    }
                    // Guardar los cambios
                    dbContext.SaveChanges();
                }
              
                #endregion
               
                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ORDEN SERVICIO", "v_ServiceOrderId=" + objEntity.v_ServiceOrderId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "SERVICIO", "v_ServiceOrderId=" + pobjDtoEntity.v_ServiceOrderId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public serviceorderDto GetServiceOrder(ref OperationResult pobjOperationResult, string pstrServiceId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                serviceorderDto objDtoEntity = null;

                var objEntity = (from a in dbContext.serviceorder
                                 where a.v_ServiceOrderId == pstrServiceId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = serviceorderAssembler.ToDTO(objEntity);

                pobjOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }
        
        public void DeleteServiceOrder(ref OperationResult pobjOperationResult, string v_ServiceOrderId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region Service Order
                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.serviceorder
                                       where a.v_ServiceOrderId == v_ServiceOrderId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                #endregion

                #region Service Order Detail


                var Lista = (from a in dbContext.serviceorderdetail
                             where a.v_ServiceOrderId == v_ServiceOrderId
                             select a).ToList();
                // Obtener la entidad fuente

                foreach (var item in Lista)
                {
                    var objEntitySourceDetail = (from a in dbContext.serviceorderdetail
                                                 where a.v_ServiceOrderDetailId == item.v_ServiceOrderDetailId
                                                 select a).FirstOrDefault();

                    // Crear la entidad con los datos actualizados
                    objEntitySourceDetail.d_UpdateDate = DateTime.Now;
                    objEntitySourceDetail.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                    objEntitySourceDetail.i_IsDeleted = 1;

                    dbContext.SaveChanges();
                }

               

                #endregion
               
                // Guardar los cambios
              

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "ORDEN DE SERVICIO", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "ORDEN DE SERVICIO", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void DeleteCotizacion(ref OperationResult pobjOperationResult, string v_ServiceOrderId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region Service Order
                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.cotizacionoc
                                       where a.i_CotizacionIdOc == v_ServiceOrderId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                #endregion

                #region Service Order Detail


                var Lista = (from a in dbContext.cotizacionoc
                             where a.i_CotizacionIdOc == v_ServiceOrderId
                             select a).ToList();
                // Obtener la entidad fuente

                foreach (var item in Lista)
                {
                    var objEntitySourceDetail = (from a in dbContext.cotizacionocdetaill
                                                 where a.i_CotizacionIdOc == item.i_CotizacionIdOc
                                                 select a).FirstOrDefault();

                    // Crear la entidad con los datos actualizados
                    objEntitySourceDetail.d_UpdateDate = DateTime.Now;
                    objEntitySourceDetail.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                    objEntitySourceDetail.i_IsDeleted = 1;

                    dbContext.SaveChanges();
                }



                #endregion

                // Guardar los cambios


                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "ORDEN DE SERVICIO", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "ORDEN DE SERVICIO", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }


        public serviceorderdetailDto GetServiceOrderDetail(ref OperationResult pobjOperationResult, string pstrServiceOderderId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                serviceorderdetailDto objDtoEntity = null;

                var objEntity = (from a in dbContext.serviceorderdetail
                                 where a.v_ServiceOrderId == pstrServiceOderderId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = serviceorderdetailAssembler.ToDTO(objEntity);

                pobjOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }
        public byte[] GetLogoMedicalCenter()
        {
            using (SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel())
            {

                var nameMedicalCenter = (from n in dbContext.organization
                                         where n.v_OrganizationId == Constants.OWNER_ORGNIZATION_ID
                                         select n.b_Image).SingleOrDefault();

                return nameMedicalCenter;
            }
        }
        public List<ServiceOrderList> GetReportServiceOrder(string pstServiceOrderId, string pstrProtocolId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                //var LogoEmpresa = GetLogoMedicalCenter();
                var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
                var query = from A in dbContext.serviceorder
                            join A1 in dbContext.serviceorderdetail on new { a = pstServiceOrderId, b = pstrProtocolId } equals new { a = A1.v_ServiceOrderId, b = A1.v_ProtocolId }
                             join B in dbContext.systemparameter on new { a = A.i_ServiceOrderStatusId.Value, b = 194 } equals new { a = B.i_ParameterId, b = B.i_GroupId }
                            join C in dbContext.serviceorderdetail on A.v_ServiceOrderId equals C.v_ServiceOrderId
                            join D in dbContext.protocol on C.v_ProtocolId equals D.v_ProtocolId
                            join E in dbContext.organization on D.v_CustomerOrganizationId equals E.v_OrganizationId
                            join F in dbContext.systemparameter on new { a = D.i_EsoTypeId.Value, b = 118 } equals new { a = F.i_ParameterId, b = F.i_GroupId }
                            join G in dbContext.protocolcomponent on D.v_ProtocolId equals G.v_ProtocolId
                            join H in dbContext.component on G.v_ComponentId equals H.v_ComponentId
                            join I in dbContext.groupoccupation on D.v_GroupOccupationId equals I.v_GroupOccupationId
                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }

                                                            equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()
                            join su in dbContext.systemuser on A.i_InsertUserId.Value equals su.i_SystemUserId into su_join
                            from su in su_join.DefaultIfEmpty()

                            join pr in dbContext.professional on su.v_PersonId equals pr.v_PersonId into pr_join
                            from pr in pr_join.DefaultIfEmpty()
                            where A.i_IsDeleted == 0 && A.v_ServiceOrderId == pstServiceOrderId && G.i_IsDeleted == 0
                            && D.v_ProtocolId == pstrProtocolId

                            select new ServiceOrderList
                            {
                                v_ServiceOrderId = A.v_ServiceOrderId,
                                v_CustomServiceOrderId = A.v_CustomServiceOrderId,
                                v_Description = A.v_Description,
                                v_Comentary = A.v_Comentary,
                                i_NumberOfWorker = A1.i_NumberOfWorkerProtocol,
                                r_TotalCost = A1.r_Total,
                                d_DeliveryDate = A.d_DeliveryDate,
                                RucCliente = E.v_IdentificationNumber,
                                i_ServiceOrderStatusId = A.i_ServiceOrderStatusId,
                                v_ServiceOrderStatusName = B.v_Value1,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate,
                                v_ProtocolId = C.v_ProtocolId,
                                v_Protocol = D.v_Name,
                                v_Organization = E.v_Name,
                                v_ContacName = E.v_ContacName,
                                v_Address = E.v_Address,
                                v_EsoType = F.v_Value1,
                                v_ComponentName = H.v_Name,
                                r_Price = G.r_Price, // H.r_BasePrice.Value,
                                d_InsertDate = A.d_InsertDate.Value,
                                Logo = MedicalCenter.b_Image,
                                EmpresaPropietaria = MedicalCenter.v_Name,
                                EmpresaPropietariaDireccion = MedicalCenter.v_Address,
                                EmpresaPropietariaTelefono = MedicalCenter.v_PhoneNumber,
                                EmpresaPropietariaEmail = MedicalCenter.v_Mail,
                                GESO = I.v_Name,
                                Firma = pr.b_SignatureImage,

                            };
                List<ServiceOrderList> objData = query.ToList();
                return objData;

            }
            catch (Exception ex)
            {
                 return null;
            }
        }

        public List<ServiceOrderList> GetReportCotizacionConsolidado(string pstServiceOrderId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                //var LogoEmpresa = GetLogoMedicalCenter();
                var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();

                var query = from A in dbContext.serviceorderdetail
                            join B in dbContext.protocol on A.v_ProtocolId equals B.v_ProtocolId
                            join C in dbContext.serviceorder on A.v_ServiceOrderId equals C.v_ServiceOrderId
                            join E in dbContext.organization on B.v_CustomerOrganizationId equals E.v_OrganizationId
                            join I in dbContext.groupoccupation on B.v_GroupOccupationId equals I.v_GroupOccupationId
                            join F in dbContext.systemparameter on new { a = B.i_EsoTypeId.Value, b = 118 } equals new { a = F.i_ParameterId, b = F.i_GroupId }
                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }

                                                         equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            join su in dbContext.systemuser on C.i_InsertUserId.Value equals su.i_SystemUserId into su_join
                            from su in su_join.DefaultIfEmpty()

                            join pr in dbContext.professional on su.v_PersonId equals pr.v_PersonId into pr_join
                            from pr in pr_join.DefaultIfEmpty()

                            where A.i_IsDeleted == 0 && A.v_ServiceOrderId== pstServiceOrderId
                            select new ServiceOrderList
                            {
                               
                                v_ServiceOrderId = A.v_ServiceOrderId,
                                v_ProtocolId = A.v_ProtocolId,
                                v_Protocol = B.v_Name,
                                r_Price = A.r_ProtocolPrice,
                                CantidadTrabajadores = A.i_NumberOfWorkerProtocol,
                                v_Organization = E.v_Name,
                                RucCliente = E.v_IdentificationNumber,
                                v_Address = E.v_Address,
                                d_InsertDate = C.d_InsertDate.Value,
                                EmpresaPropietaria = MedicalCenter.v_Name,
                                GESO = I.v_Name,
                                v_EsoType = F.v_Value1,
                                EmpresaPropietariaDireccion = MedicalCenter.v_Address,
                                EmpresaPropietariaTelefono = MedicalCenter.v_PhoneNumber,
                                EmpresaPropietariaEmail = MedicalCenter.v_Mail,
                                Firma = pr.b_SignatureImage,
                            };

                //var query = from A in dbContext.serviceorder
                //            join A1 in dbContext.serviceorderdetail on new { a = pstServiceOrderId} equals new { a = A1.v_ServiceOrderId}
                //            join B in dbContext.systemparameter on new { a = A.i_ServiceOrderStatusId.Value, b = 194 } equals new { a = B.i_ParameterId, b = B.i_GroupId }
                //            join C in dbContext.serviceorderdetail on A.v_ServiceOrderId equals C.v_ServiceOrderId
                //            join D in dbContext.protocol on C.v_ProtocolId equals D.v_ProtocolId
                //            join E in dbContext.organization on D.v_CustomerOrganizationId equals E.v_OrganizationId
                //            join F in dbContext.systemparameter on new { a = D.i_EsoTypeId.Value, b = 118 } equals new { a = F.i_ParameterId, b = F.i_GroupId }
                //            join G in dbContext.protocolcomponent on D.v_ProtocolId equals G.v_ProtocolId
                //            join H in dbContext.component on G.v_ComponentId equals H.v_ComponentId
                //            join I in dbContext.groupoccupation on D.v_GroupOccupationId equals I.v_GroupOccupationId
                //            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }

                //                                            equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                //            from J1 in J1_join.DefaultIfEmpty()

                //            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                //                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                //            from J2 in J2_join.DefaultIfEmpty()
                //            join su in dbContext.systemuser on A.i_InsertUserId.Value equals su.i_SystemUserId into su_join
                //            from su in su_join.DefaultIfEmpty()

                //            join pr in dbContext.professional on su.v_PersonId equals pr.v_PersonId into pr_join
                //            from pr in pr_join.DefaultIfEmpty()
                //            where A.i_IsDeleted == 0 && A.v_ServiceOrderId == pstServiceOrderId
                //            //&& G.i_IsDeleted == 0
                //            //&& D.v_ProtocolId == pstrProtocolId

                //            select new ServiceOrderList
                //            {
                //                v_ServiceOrderId = A.v_ServiceOrderId,
                //                v_CustomServiceOrderId = A.v_CustomServiceOrderId,
                //                v_Description = A.v_Description,
                //                v_Comentary = A.v_Comentary,
                //                i_NumberOfWorker = A1.i_NumberOfWorkerProtocol,
                //                r_TotalCost = A1.r_Total,
                //                d_DeliveryDate = A.d_DeliveryDate,
                //                RucCliente = E.v_IdentificationNumber,
                //                i_ServiceOrderStatusId = A.i_ServiceOrderStatusId,
                //                v_ServiceOrderStatusName = B.v_Value1,
                //                v_CreationUser = J1.v_UserName,
                //                v_UpdateUser = J2.v_UserName,
                //                d_CreationDate = A.d_InsertDate,
                //                d_UpdateDate = A.d_UpdateDate,
                //                v_ProtocolId = C.v_ProtocolId,
                //                v_Protocol = D.v_Name,
                //                v_Organization = E.v_Name,
                //                v_ContacName = E.v_ContacName,
                //                v_Address = E.v_Address,
                //                v_EsoType = F.v_Value1,
                //                v_ComponentName = H.v_Name,
                //                r_Price = G.r_Price, // H.r_BasePrice.Value,
                //                d_InsertDate = A.d_InsertDate.Value,
                //                Logo = MedicalCenter.b_Image,
                //                EmpresaPropietaria = MedicalCenter.v_Name,
                //                EmpresaPropietariaDireccion = MedicalCenter.v_Address,
                //                EmpresaPropietariaTelefono = MedicalCenter.v_PhoneNumber,
                //                EmpresaPropietariaEmail = MedicalCenter.v_Mail,
                //                GESO = I.v_Name,
                //                Firma = pr.b_SignatureImage,

                //            };
                List<ServiceOrderList> objData = query.ToList();
                return objData;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ServiceOrderDetailList GetServiceOrderDetailList(string pstrProtocolId)
        {
            try
            {
                   SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                ProtocolBL oProtocolBL =  new ProtocolBL();
                  OperationResult objOperationResult = new OperationResult();
                  var Precio = oProtocolBL.GetProtocolComponents(ref objOperationResult, pstrProtocolId).Sum(s => s.r_Price);
                   var query = (from A in dbContext.protocol
                               where A.v_ProtocolId == pstrProtocolId                               
                               select new ServiceOrderDetailList
                               {
                                   v_ProtocolName = A.v_Name,
                                  r_ProtocolPrice = Precio
                               }).FirstOrDefault();

                   return query;

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<ServiceOrderDetailList> GetServiceOrderPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.serviceorderdetail
                            join B in dbContext.protocol on A.v_ProtocolId equals B.v_ProtocolId
                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                         equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            where A.i_IsDeleted ==0
                            select new ServiceOrderDetailList
                            {
                                v_ServiceOrderDetailId =  A.v_ServiceOrderDetailId,
                                v_ServiceOrderId = A.v_ServiceOrderId,
                                v_ProtocolId =A.v_ProtocolId,
                                v_ProtocolName = B.v_Name,
                                r_ProtocolPrice = A.r_ProtocolPrice,
                                r_Total = A.r_Total,
                                i_NumberOfWorkerProtocol = A.i_NumberOfWorkerProtocol,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate
                            };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (pintPageIndex.HasValue && pintResultsPerPage.HasValue)
                {
                    int intStartRowIndex = pintPageIndex.Value * pintResultsPerPage.Value;
                    query = query.Skip(intStartRowIndex);
                }
                if (pintResultsPerPage.HasValue)
                {
                    query = query.Take(pintResultsPerPage.Value);
                }

                List<ServiceOrderDetailList> objData = query.ToList();
                pobjOperationResult.Success = 1;
                return objData;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public List<Valor> ObtenerProtocolos(string pstrServiceOrder)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            var query = (from A in dbContext.serviceorderdetail
                         where A.v_ServiceOrderId == pstrServiceOrder && A.i_IsDeleted == 0
                         select new Valor
                         {
                             Value1 = A.v_ProtocolId
                         }).ToList();

            return query;
        }

    }
}