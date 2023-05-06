using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Common;
using System.Collections;
using System.Transactions;
using System.Data.Linq.SqlClient;
using System.Threading;
using Sigesoft.Node.WinClient.BE.Custom;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using NetPdf;

namespace Sigesoft.Node.WinClient.BLL
{
    public class ServiciosDetalleBL
    {
        public List<ServiceComponentList> GetServiceComponents_(ref OperationResult pobjOperationResult, string pstrServiceId)
        {


            int isDeleted = (int)SiNo.NO;
            int isRequired = (int)SiNo.SI;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.servicecomponent
                             join C in dbContext.systemuser on A.i_MedicoTratanteId equals C.i_SystemUserId into C_join
                             from C in C_join.DefaultIfEmpty()
                             join D in dbContext.person on C.v_PersonId equals D.v_PersonId
                             join B in dbContext.component on A.v_ComponentId equals B.v_ComponentId
                             join F in dbContext.systemparameter on new { a = B.i_CategoryId.Value, b = 116 }
                                     equals new { a = F.i_ParameterId, b = F.i_GroupId } into F_join
                             from F in F_join.DefaultIfEmpty()

                             join G in dbContext.hospitalizacionservice on A.v_ServiceId equals G.v_ServiceId
                             join H in dbContext.hospitalizacion on G.v_HopitalizacionId equals H.v_HopitalizacionId
                             where A.v_ServiceId == pstrServiceId &&
                                   A.i_IsDeleted == isDeleted &&
                                   A.i_IsRequiredId == isRequired
                             //&& (A.r_Price != 0 || A.r_Price != 0.00 )

                             select new ServiceComponentList
                             {
                                 v_ServiceComponentId = A.v_ServiceComponentId,
                                 v_ComponentId = A.v_ComponentId,
                                 r_Price = A.r_Price,
                                 v_ComponentName = B.v_Name,
                                 v_CategoryName = F.v_Value1,
                                 i_ConCargoA = A.i_ConCargoA,
                                 MedicoTratante = D.v_FirstName + " " + D.v_FirstLastName + " " + D.v_SecondLastName,
                                 d_InsertDate = A.d_InsertDate,
                                 d_FechaAlta = H.d_FechaAlta
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

        public List<ServiciosDetalle> ServiciosServicioDetalle(string v_ServicioId)
        {
            //mon.IsActive = true;
            var isDeleted = 0;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                //var query = (from A in dbContext.servicecomponent
                //             join C in dbContext.systemuser on A.i_MedicoTratanteId equals C.i_SystemUserId into C_join
                //             from C in C_join.DefaultIfEmpty()
                //             join D in dbContext.person on C.v_PersonId equals D.v_PersonId
                //             join B in dbContext.component on A.v_ComponentId equals B.v_ComponentId
                //             join F in dbContext.systemparameter on new { a = B.i_CategoryId.Value, b = 116 }
                //                     equals new { a = F.i_ParameterId, b = F.i_GroupId } into F_join
                //             from F in F_join.DefaultIfEmpty()

                //             join G in dbContext.hospitalizacionservice on A.v_ServiceId equals G.v_ServiceId
                //             join H in dbContext.hospitalizacion on G.v_HopitalizacionId equals H.v_HopitalizacionId

                List<ServiciosDetalle> Lista = new List<ServiciosDetalle>();
                var query = (from hs in dbContext.hospitalizacionservice 
                            join s in dbContext.service  on hs.v_ServiceId equals s.v_ServiceId
                            join hs2 in dbContext.hospitalizacionservice  on hs.v_HopitalizacionId equals hs2.v_HopitalizacionId
                             join sc in dbContext.servicecomponent on hs2.v_ServiceId equals sc.v_ServiceId
                             join c in dbContext.component on sc.v_ComponentId equals c.v_ComponentId
                             join sy in dbContext.systemparameter on new { a = c.i_KindOfService.Value, b = 358 }
                                                 equals new { a = sy.i_ParameterId, b = sy.i_GroupId }

                             join su in dbContext.systemuser on sc.i_InsertUserId equals su.i_SystemUserId
                             join p in dbContext.person on su.v_PersonId equals p.v_PersonId
                             join sy1 in dbContext.systemparameter on new { a = c.i_CategoryId.Value, b = 116 }
                                                 equals new { a = sy1.i_ParameterId, b = sy1.i_GroupId }

                             where sc.i_IsRequiredId == 1 && sc.r_Price != 0
                             && s.v_ServiceId == v_ServicioId && sc.i_IsDeleted == 0

                             select new ServiciosDetalle
                             {
                                 Servicio = sc.v_ServiceId,
                                 Componente = sc.v_ComponentId,
                                 NombreComponente = c.v_Name,
                                 Precio = sc.r_Price.Value,
                                 Segus = c.v_CodigoSegus,
                                 SaldoPaciente = sc.d_SaldoPaciente.Value,
                                 SaldoSeguro = sc.d_SaldoAseguradora.Value,
                                 Fecha = sc.d_InsertDate.Value,
                                 TipoId = c.i_KindOfService.Value,
                                 Tipo = sy.v_Value1,
                                 Grupo = sy1.v_Value1,
                                 i_conCargoA = sc.i_ConCargoA,
                                 UsuarioInserta = p.v_FirstName + " " + p.v_SecondLastName + " " + p.v_FirstName,
                                 Cantidad = sc.i_Cantidad.Value

                             }).ToList().OrderBy(p => p.Tipo);
                Lista = query.ToList().GroupBy(g => g.Tipo).Select(s => s.First()).ToList();

                List<ServiciosDetalle> _lista1 = new List<ServiciosDetalle>();
                ServiciosDetalle oLista1;

                int Contadorlista = Lista.Count;
                if (Contadorlista == 0)
                {
                    var query2 = (from s in dbContext.service
                                  join sc in dbContext.servicecomponent on s.v_ServiceId equals sc.v_ServiceId
                                  join c in dbContext.component on sc.v_ComponentId equals c.v_ComponentId
                                  join sy in dbContext.systemparameter on new { a = c.i_KindOfService.Value, b = 358 }
                                                      equals new { a = sy.i_ParameterId, b = sy.i_GroupId }

                                  join su in dbContext.systemuser on sc.i_InsertUserId equals su.i_SystemUserId
                                  join p in dbContext.person on su.v_PersonId equals p.v_PersonId
                                  join sy1 in dbContext.systemparameter on new { a = c.i_CategoryId.Value, b = 116 }
                                                      equals new { a = sy1.i_ParameterId, b = sy1.i_GroupId }

                                  where sc.i_IsRequiredId == 1 && sc.r_Price != 0
                                  && s.v_ServiceId == v_ServicioId && sc.i_IsDeleted == 0

                                  select new ServiciosDetalle
                                  {
                                      Servicio = sc.v_ServiceId,
                                      Componente = sc.v_ComponentId,
                                      NombreComponente = c.v_Name,
                                      Precio = sc.r_Price.Value,
                                      Segus = c.v_CodigoSegus,
                                      SaldoPaciente = sc.d_SaldoPaciente.Value,
                                      SaldoSeguro = sc.d_SaldoAseguradora.Value,
                                      Fecha = sc.d_InsertDate.Value,
                                      TipoId = c.i_KindOfService.Value,
                                      Tipo = sy.v_Value1,
                                      Grupo = sy1.v_Value1,
                                      i_conCargoA = sc.i_ConCargoA,
                                      UsuarioInserta = p.v_FirstName + " " + p.v_SecondLastName + " " + p.v_FirstName,
                                      Cantidad = sc.i_Cantidad.Value

                                  }).ToList().OrderBy(p => p.Tipo);

                    Lista = query2.ToList().GroupBy(g => g.Tipo).Select(s => s.First()).ToList();
                    foreach (var item in Lista)
                    {
                        oLista1 = new ServiciosDetalle();
                        oLista1.Tipo = item.Tipo;

                        //lista sin categoria
                        var Lista2 = query2.ToList().FindAll(p => p.Tipo == item.Tipo).ToList();
                        var _lista2 = new List<ServiciosDetalle_1>();

                        foreach (var item2 in Lista2)
                        {
                            if (item2.TipoId != 2)
                            {
                                var oLista2 = new ServiciosDetalle_1();
                                oLista2.TipoId = item2.TipoId;
                                oLista2.Tipo = item2.Tipo;
                                oLista2.Grupo = item2.Grupo;
                                oLista2.Servicio = item2.Servicio;
                                oLista2.Componente = item2.Componente;
                                oLista2.NombreComponente = item2.NombreComponente;
                                oLista2.Precio = decimal.Parse(item2.Precio.ToString());
                                oLista2.Segus = item2.Segus;
                                oLista2.SaldoPaciente = item2.SaldoPaciente;
                                oLista2.SaldoSeguro = item2.SaldoSeguro;
                                oLista2.Fecha = item2.Fecha;
                                oLista2.UsuarioInserta = item2.UsuarioInserta;
                                oLista2.i_conCargoA = item2.i_conCargoA;
                                oLista2.Cantidad = item.Cantidad;
                                _lista2.Add(oLista2);
                            }
                            else
                            {
                                //lista con categoria

                                var listado2 = query2.ToList().FindAll(p => p.TipoId == 2).ToList();

                                var Lista3 = listado2.ToList().GroupBy(g => g.Grupo).Select(s => s.First()).ToList();

                                List<ServiciosDetalle_1> _lista3 = new List<ServiciosDetalle_1>();
                                ServiciosDetalle_1 oLista3;

                                foreach (var item3 in Lista3)
                                {
                                    if (item3.TipoId == 2)
                                    {
                                        oLista3 = new ServiciosDetalle_1();
                                        oLista3.Grupo = item3.Grupo;

                                        var Lista4 = query2.ToList().FindAll(p => p.Grupo == item3.Grupo && p.TipoId == 2).ToList();
                                        var _lista4 = new List<ServiciosDetalle_2>();
                                        foreach (var item4 in Lista4)
                                        {
                                            var oLista4 = new ServiciosDetalle_2();
                                            oLista4.Tipo = item4.Tipo;
                                            oLista4.Grupo = item4.Grupo;
                                            oLista4.Servicio = item4.Servicio;
                                            oLista4.Componente = item4.Componente;
                                            oLista4.NombreComponente = item4.NombreComponente;
                                            oLista4.Precio = decimal.Parse(item4.Precio.ToString());
                                            oLista4.Segus = item4.Segus;
                                            oLista4.SaldoPaciente = item4.SaldoPaciente;
                                            oLista4.SaldoSeguro = item4.SaldoSeguro;
                                            oLista4.Fecha = item4.Fecha;
                                            oLista4.UsuarioInserta = item4.UsuarioInserta;
                                            oLista4.i_conCargoA = item4.i_conCargoA;
                                            oLista4.Cantidad = item4.Cantidad;

                                            _lista4.Add(oLista4);
                                        }
                                        oLista3.Lista2 = _lista4;

                                        _lista3.Add(oLista3);

                                    }

                                }

                                if (Lista3 != null)
                                {
                                    oLista1.Lista2 = _lista3;
                                }
                            }
                        }


                        if (_lista2 != null)
                        {
                            oLista1.Lista1 = _lista2;
                        }




                        _lista1.Add(oLista1);

                    }
                }

                else
                {
                    foreach (var item in Lista)
                    {
                        oLista1 = new ServiciosDetalle();
                        oLista1.Tipo = item.Tipo;

                        //lista sin categoria
                        var Lista2 = query.ToList().FindAll(p => p.Tipo == item.Tipo).ToList();
                        var _lista2 = new List<ServiciosDetalle_1>();

                        foreach (var item2 in Lista2)
                        {
                            if (item2.TipoId != 2)
                            {
                                var oLista2 = new ServiciosDetalle_1();
                                oLista2.TipoId = item2.TipoId;
                                oLista2.Tipo = item2.Tipo;
                                oLista2.Grupo = item2.Grupo;
                                oLista2.Servicio = item2.Servicio;
                                oLista2.Componente = item2.Componente;
                                oLista2.NombreComponente = item2.NombreComponente;
                                oLista2.Precio = decimal.Parse(item2.Precio.ToString());
                                oLista2.Segus = item2.Segus;
                                oLista2.SaldoPaciente = item2.SaldoPaciente;
                                oLista2.SaldoSeguro = item2.SaldoSeguro;
                                oLista2.Fecha = item2.Fecha;
                                oLista2.UsuarioInserta = item2.UsuarioInserta;
                                oLista2.i_conCargoA = item2.i_conCargoA;
                                oLista2.Cantidad = item2.Cantidad;
                                _lista2.Add(oLista2);
                            }
                            else
                            {
                                //lista con categoria

                                var Lista3 = query.ToList().GroupBy(g => g.Grupo).Select(s => s.First()).ToList();

                                List<ServiciosDetalle_1> _lista3 = new List<ServiciosDetalle_1>();
                                ServiciosDetalle_1 oLista3;

                                foreach (var item3 in Lista3)
                                {
                                    if (item3.TipoId == 2)
                                    {
                                        oLista3 = new ServiciosDetalle_1();
                                        oLista3.Grupo = item3.Grupo;

                                        var Lista4 = query.ToList().FindAll(p => p.Grupo == item3.Grupo).ToList();
                                        var _lista4 = new List<ServiciosDetalle_2>();
                                        foreach (var item4 in Lista4)
                                        {
                                            var oLista4 = new ServiciosDetalle_2();
                                            oLista4.Tipo = item4.Tipo;
                                            oLista4.Grupo = item4.Grupo;
                                            oLista4.Servicio = item4.Servicio;
                                            oLista4.Componente = item4.Componente;
                                            oLista4.NombreComponente = item4.NombreComponente;
                                            oLista4.Precio = decimal.Parse(item4.Precio.ToString());
                                            oLista4.Segus = item4.Segus;
                                            oLista4.SaldoPaciente = item4.SaldoPaciente;
                                            oLista4.SaldoSeguro = item4.SaldoSeguro;
                                            oLista4.Fecha = item4.Fecha;
                                            oLista4.UsuarioInserta = item4.UsuarioInserta;
                                            oLista4.i_conCargoA = item4.i_conCargoA;
                                            oLista4.Cantidad = item4.Cantidad;
                                            _lista4.Add(oLista4);
                                        }
                                        oLista3.Lista2 = _lista4;

                                        _lista3.Add(oLista3);

                                    }

                                }

                                if (Lista3 != null)
                                {
                                    oLista1.Lista2 = _lista3;
                                }
                            }
                        }


                        if (_lista2 != null)
                        {
                            oLista1.Lista1 = _lista2;
                        }




                        _lista1.Add(oLista1);

                    }
                }

                return _lista1;

                //return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ServiciosDetalle> ServiciosServicioDetalleMedicoYPaciente(string v_ServicioId)
        {
            //mon.IsActive = true;
            var isDeleted = 0;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

         
                List<ServiciosDetalle> Lista = new List<ServiciosDetalle>();
                var query = (from hs in dbContext.hospitalizacionservice
                             join s in dbContext.service on hs.v_ServiceId equals s.v_ServiceId
                             join hs2 in dbContext.hospitalizacionservice on hs.v_HopitalizacionId equals hs2.v_HopitalizacionId
                             join sc in dbContext.servicecomponent on hs2.v_ServiceId equals sc.v_ServiceId
                             join c in dbContext.component on sc.v_ComponentId equals c.v_ComponentId
                             join sy in dbContext.systemparameter on new { a = c.i_KindOfService.Value, b = 358 }
                                                 equals new { a = sy.i_ParameterId, b = sy.i_GroupId }

                             join su in dbContext.systemuser on sc.i_InsertUserId equals su.i_SystemUserId
                             join p in dbContext.person on su.v_PersonId equals p.v_PersonId
                             join sy1 in dbContext.systemparameter on new { a = c.i_CategoryId.Value, b = 116 }
                                                 equals new { a = sy1.i_ParameterId, b = sy1.i_GroupId }

                             where sc.i_IsRequiredId == 1 && sc.r_Price != 0
                             && s.v_ServiceId == v_ServicioId && sc.i_IsDeleted == 0 && (sc.i_ConCargoA != 0 && sc.i_ConCargoA != null)

                             select new ServiciosDetalle
                             {
                                 Servicio = sc.v_ServiceId,
                                 Componente = sc.v_ComponentId,
                                 NombreComponente = c.v_Name,
                                 Precio = sc.r_Price.Value,
                                 Segus = c.v_CodigoSegus,
                                 SaldoPaciente = sc.d_SaldoPaciente.Value,
                                 SaldoSeguro = sc.d_SaldoAseguradora.Value,
                                 Fecha = sc.d_InsertDate.Value,
                                 TipoId = c.i_KindOfService.Value,
                                 Tipo = sy.v_Value1,
                                 Grupo = sy1.v_Value1,
                                 i_conCargoA = sc.i_ConCargoA,
                                 UsuarioInserta = p.v_FirstName + " " + p.v_SecondLastName + " " + p.v_FirstName,
                                 Cantidad = sc.i_Cantidad.Value

                             }).ToList().OrderBy(p => p.Tipo);
                Lista = query.ToList().GroupBy(g => g.Tipo).Select(s => s.First()).ToList();

                List<ServiciosDetalle> _lista1 = new List<ServiciosDetalle>();
                ServiciosDetalle oLista1;

                int Contadorlista = Lista.Count;
                if (Contadorlista == 0)
                {
                    var query2 = (from s in dbContext.service
                                  join sc in dbContext.servicecomponent on s.v_ServiceId equals sc.v_ServiceId
                                  join c in dbContext.component on sc.v_ComponentId equals c.v_ComponentId
                                  join sy in dbContext.systemparameter on new { a = c.i_KindOfService.Value, b = 358 }
                                                      equals new { a = sy.i_ParameterId, b = sy.i_GroupId }

                                  join su in dbContext.systemuser on sc.i_InsertUserId equals su.i_SystemUserId
                                  join p in dbContext.person on su.v_PersonId equals p.v_PersonId
                                  join sy1 in dbContext.systemparameter on new { a = c.i_CategoryId.Value, b = 116 }
                                                      equals new { a = sy1.i_ParameterId, b = sy1.i_GroupId }

                                  where sc.i_IsRequiredId == 1 && sc.r_Price != 0
                                  && s.v_ServiceId == v_ServicioId && sc.i_IsDeleted == 0

                                  select new ServiciosDetalle
                                  {
                                      Servicio = sc.v_ServiceId,
                                      Componente = sc.v_ComponentId,
                                      NombreComponente = c.v_Name,
                                      Precio = sc.r_Price.Value,
                                      Segus = c.v_CodigoSegus,
                                      SaldoPaciente = sc.d_SaldoPaciente.Value,
                                      SaldoSeguro = sc.d_SaldoAseguradora.Value,
                                      Fecha = sc.d_InsertDate.Value,
                                      TipoId = c.i_KindOfService.Value,
                                      Tipo = sy.v_Value1,
                                      Grupo = sy1.v_Value1,
                                      i_conCargoA = sc.i_ConCargoA,
                                      UsuarioInserta = p.v_FirstName + " " + p.v_SecondLastName + " " + p.v_FirstName,
                                      Cantidad = sc.i_Cantidad.Value

                                  }).ToList().OrderBy(p => p.Tipo);

                    Lista = query2.ToList().GroupBy(g => g.Tipo).Select(s => s.First()).ToList();
                    foreach (var item in Lista)
                    {
                        oLista1 = new ServiciosDetalle();
                        oLista1.Tipo = item.Tipo;

                        //lista sin categoria
                        var Lista2 = query2.ToList().FindAll(p => p.Tipo == item.Tipo).ToList();
                        var _lista2 = new List<ServiciosDetalle_1>();

                        foreach (var item2 in Lista2)
                        {
                            if (item2.TipoId != 2)
                            {
                                var oLista2 = new ServiciosDetalle_1();
                                oLista2.TipoId = item2.TipoId;
                                oLista2.Tipo = item2.Tipo;
                                oLista2.Grupo = item2.Grupo;
                                oLista2.Servicio = item2.Servicio;
                                oLista2.Componente = item2.Componente;
                                oLista2.NombreComponente = item2.NombreComponente;
                                oLista2.Precio = decimal.Parse(item2.Precio.ToString());
                                oLista2.Segus = item2.Segus;
                                oLista2.SaldoPaciente = item2.SaldoPaciente;
                                oLista2.SaldoSeguro = item2.SaldoSeguro;
                                oLista2.Fecha = item2.Fecha;
                                oLista2.UsuarioInserta = item2.UsuarioInserta;
                                oLista2.i_conCargoA = item2.i_conCargoA;
                                oLista2.Cantidad = item.Cantidad;
                                _lista2.Add(oLista2);
                            }
                            else
                            {
                                //lista con categoria

                                var listado2 = query2.ToList().FindAll(p => p.TipoId == 2).ToList();

                                var Lista3 = listado2.ToList().GroupBy(g => g.Grupo).Select(s => s.First()).ToList();

                                List<ServiciosDetalle_1> _lista3 = new List<ServiciosDetalle_1>();
                                ServiciosDetalle_1 oLista3;

                                foreach (var item3 in Lista3)
                                {
                                    if (item3.TipoId == 2)
                                    {
                                        oLista3 = new ServiciosDetalle_1();
                                        oLista3.Grupo = item3.Grupo;

                                        var Lista4 = query2.ToList().FindAll(p => p.Grupo == item3.Grupo && p.TipoId == 2).ToList();
                                        var _lista4 = new List<ServiciosDetalle_2>();
                                        foreach (var item4 in Lista4)
                                        {
                                            var oLista4 = new ServiciosDetalle_2();
                                            oLista4.Tipo = item4.Tipo;
                                            oLista4.Grupo = item4.Grupo;
                                            oLista4.Servicio = item4.Servicio;
                                            oLista4.Componente = item4.Componente;
                                            oLista4.NombreComponente = item4.NombreComponente;
                                            oLista4.Precio = decimal.Parse(item4.Precio.ToString());
                                            oLista4.Segus = item4.Segus;
                                            oLista4.SaldoPaciente = item4.SaldoPaciente;
                                            oLista4.SaldoSeguro = item4.SaldoSeguro;
                                            oLista4.Fecha = item4.Fecha;
                                            oLista4.UsuarioInserta = item4.UsuarioInserta;
                                            oLista4.i_conCargoA = item4.i_conCargoA;
                                            oLista4.Cantidad = item4.Cantidad;

                                            _lista4.Add(oLista4);
                                        }
                                        oLista3.Lista2 = _lista4;

                                        _lista3.Add(oLista3);

                                    }

                                }

                                if (Lista3 != null)
                                {
                                    oLista1.Lista2 = _lista3;
                                }
                            }
                        }


                        if (_lista2 != null)
                        {
                            oLista1.Lista1 = _lista2;
                        }




                        _lista1.Add(oLista1);

                    }
                }

                else
                {
                    foreach (var item in Lista)
                    {
                        oLista1 = new ServiciosDetalle();
                        oLista1.Tipo = item.Tipo;

                        //lista sin categoria
                        var Lista2 = query.ToList().FindAll(p => p.Tipo == item.Tipo).ToList();
                        var _lista2 = new List<ServiciosDetalle_1>();

                        foreach (var item2 in Lista2)
                        {
                            if (item2.TipoId != 2)
                            {
                                var oLista2 = new ServiciosDetalle_1();
                                oLista2.TipoId = item2.TipoId;
                                oLista2.Tipo = item2.Tipo;
                                oLista2.Grupo = item2.Grupo;
                                oLista2.Servicio = item2.Servicio;
                                oLista2.Componente = item2.Componente;
                                oLista2.NombreComponente = item2.NombreComponente;
                                oLista2.Precio = decimal.Parse(item2.Precio.ToString());
                                oLista2.Segus = item2.Segus;
                                oLista2.SaldoPaciente = item2.SaldoPaciente;
                                oLista2.SaldoSeguro = item2.SaldoSeguro;
                                oLista2.Fecha = item2.Fecha;
                                oLista2.UsuarioInserta = item2.UsuarioInserta;
                                oLista2.i_conCargoA = item2.i_conCargoA;
                                oLista2.Cantidad = item2.Cantidad;
                                _lista2.Add(oLista2);
                            }
                            else
                            {
                                //lista con categoria

                                var Lista3 = query.ToList().GroupBy(g => g.Grupo).Select(s => s.First()).ToList();

                                List<ServiciosDetalle_1> _lista3 = new List<ServiciosDetalle_1>();
                                ServiciosDetalle_1 oLista3;

                                foreach (var item3 in Lista3)
                                {
                                    if (item3.TipoId == 2)
                                    {
                                        oLista3 = new ServiciosDetalle_1();
                                        oLista3.Grupo = item3.Grupo;

                                        var Lista4 = query.ToList().FindAll(p => p.Grupo == item3.Grupo).ToList();
                                        var _lista4 = new List<ServiciosDetalle_2>();
                                        foreach (var item4 in Lista4)
                                        {
                                            var oLista4 = new ServiciosDetalle_2();
                                            oLista4.Tipo = item4.Tipo;
                                            oLista4.Grupo = item4.Grupo;
                                            oLista4.Servicio = item4.Servicio;
                                            oLista4.Componente = item4.Componente;
                                            oLista4.NombreComponente = item4.NombreComponente;
                                            oLista4.Precio = decimal.Parse(item4.Precio.ToString());
                                            oLista4.Segus = item4.Segus;
                                            oLista4.SaldoPaciente = item4.SaldoPaciente;
                                            oLista4.SaldoSeguro = item4.SaldoSeguro;
                                            oLista4.Fecha = item4.Fecha;
                                            oLista4.UsuarioInserta = item4.UsuarioInserta;
                                            oLista4.i_conCargoA = item4.i_conCargoA;
                                            oLista4.Cantidad = item4.Cantidad;
                                            _lista4.Add(oLista4);
                                        }
                                        oLista3.Lista2 = _lista4;

                                        _lista3.Add(oLista3);

                                    }

                                }

                                if (Lista3 != null)
                                {
                                    oLista1.Lista2 = _lista3;
                                }
                            }
                        }


                        if (_lista2 != null)
                        {
                            oLista1.Lista1 = _lista2;
                        }




                        _lista1.Add(oLista1);

                    }
                }

                return _lista1;

                //return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<MeidicinasTicketsLista> Tickets_Detalle(string serviceId)
        {
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena1 =
                " SELECT sy.v_Value1 AS 'Tipo', " +
                " t.v_TicketId AS 'Ticket', " +
                " t.v_ServiceId AS 'Servicio', " +
                " t.d_InsertDate AS 'Fecha', " +
                " td.v_Descripcion AS 'Medicina', " +
                " CONVERT(int, td.d_Cantidad) AS 'Cantidad', " +
                " CONVERT(DECIMAL(14,2),td.d_PrecioVenta) AS 'P_Unitario', " +
                " CONVERT(DECIMAL(14,2),td.d_PrecioVenta * CONVERT(int, td.d_Cantidad)) AS 'P_Venta', " + 
                " CONVERT(DECIMAL(14,2), CASE WHEN td.d_SaldoPaciente IS NULL THEN 0.00 ELSE td.d_SaldoPaciente END ) AS 'SaldoPaciente', " + 
                " CONVERT(DECIMAL(14,2), CASE WHEN td.d_SaldoAseguradora IS NULL THEN 0.00 ELSE td.d_SaldoAseguradora END) AS 'SaldoSeguro', " +
                " CASE WHEN t.i_InsertUserId IS NULL THEN '- - -' ELSE p.v_FirstName + ' ' + p.v_FirstLastName + ' ' + p.v_SecondLastName END AS 'UserCrea',  " +
                " CASE WHEN t.i_ConCargoA IS NULL THEN 0 ELSE t.i_ConCargoA END AS 'CargoA'  " +
                " from hospitalizacionservice hs  " +
                " JOIN service s on hs.v_ServiceId = s.v_ServiceId  " +
                " JOIN hospitalizacionservice hs2 on hs.v_HopitalizacionId = hs2.v_HopitalizacionId  " +
                " JOIN ticket as t on hs2.v_ServiceId = t.v_ServiceId  " +
                "JOIN ticketdetalle AS td ON t.v_TicketId = td.v_TicketId " +
                "JOIN systemparameter AS sy ON sy.i_GroupId = 310 and t.i_TipoCuentaId = sy.i_ParameterId " +
                "LEFT JOIN systemuser AS su ON t.i_InsertUserId = su.i_SystemUserId " +
                "LEFT JOIN person AS p ON su.v_PersonId = p.v_PersonId " +
                "where s.v_ServiceId = '" + serviceId + "' and t.i_IsDeleted = 0 and td.i_IsDeleted = 0 and td.d_PrecioVenta != 0";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector1 = comando.ExecuteReader();
            MeidicinasTicketsLista _oTicketDetalle = new MeidicinasTicketsLista();
            List<MeidicinasTicketsLista> _Lista = new List<MeidicinasTicketsLista>();
            while (lector1.Read())
            {
                _oTicketDetalle = new MeidicinasTicketsLista();
                _oTicketDetalle.Tipo = lector1.GetValue(0).ToString();
                _oTicketDetalle.Ticket = lector1.GetValue(1).ToString();
                _oTicketDetalle.Servicio = lector1.GetValue(2).ToString();
                _oTicketDetalle.Fecha = DateTime.Parse(lector1.GetValue(3).ToString());
                _oTicketDetalle.Medicina = lector1.GetValue(4).ToString();
                _oTicketDetalle.Cantidad = int.Parse(lector1.GetValue(5).ToString());
                _oTicketDetalle.P_Unitario = decimal.Parse(lector1.GetValue(6).ToString());
                _oTicketDetalle.P_Venta = decimal.Parse(lector1.GetValue(7).ToString());
                _oTicketDetalle.SaldoPaciente = decimal.Parse(lector1.GetValue(8).ToString());
                _oTicketDetalle.SaldoSeguro = decimal.Parse(lector1.GetValue(9).ToString());
                _oTicketDetalle.UserCrea = lector1.GetValue(10).ToString();
                _oTicketDetalle.i_conCargoA = int.Parse(lector1.GetValue(11).ToString());
                _Lista.Add(_oTicketDetalle);
            }
            lector1.Close();
            conectasam.closesigesoft();

            var ListaTipo = _Lista.ToList().GroupBy(g => g.Tipo).Select(s => s.First()).ToList();

            var ListaTickt_ = _Lista.ToList().GroupBy(g => g.Ticket).Select(s => s.First()).ToList();

            MeidicinasTicketsLista _oTicketTipo = new MeidicinasTicketsLista();
            List<MeidicinasTicketsLista> _Lista1 = new List<MeidicinasTicketsLista>();
            foreach (var item in ListaTipo)
            {
                _oTicketTipo = new MeidicinasTicketsLista();
                _oTicketTipo.Tipo = item.Tipo;
                decimal TotalSuma = 0;
                int Cantidad = 0;
                int Medicinas = 0;
                var ListaTicket = ListaTickt_.ToList().FindAll(p => p.Tipo == item.Tipo).ToList();

                var listatickets = new List<MeidicinasTicketsLista_1>();
                foreach (var item2 in ListaTicket)
                {
                    var oTicket = new MeidicinasTicketsLista_1();
                    oTicket.Ticket = item2.Ticket;
                    oTicket.Fecha = item2.Fecha;
                    oTicket.UserCrea = item2.UserCrea;
                    oTicket.i_conCargoA = item2.i_conCargoA;

                    var ListaticketDetalle = _Lista.ToList().FindAll(p => p.Ticket == item2.Ticket).ToList();
                    var listadetalleVenta = new List<MeidicinasTicketsLista_2>();

                    foreach (var item3 in ListaticketDetalle)
                    {
                        var oDetalleTicket = new MeidicinasTicketsLista_2();
                        oDetalleTicket.Medicina = item3.Medicina;
                        oDetalleTicket.Cantidad = item3.Cantidad;
                        oDetalleTicket.P_Unitario = item3.P_Unitario;
                        oDetalleTicket.P_Venta = item3.P_Venta;
                        oDetalleTicket.SaldoPaciente = item3.SaldoPaciente;
                        oDetalleTicket.SaldoSeguro = item3.SaldoSeguro;
                        listadetalleVenta.Add(oDetalleTicket);
                    }
                    oTicket.Lista_2 = listadetalleVenta;

                    listatickets.Add(oTicket);
                }
                _oTicketTipo.Lista_1 = listatickets;

                _Lista1.Add(_oTicketTipo);
            }

            return _Lista1;
        }

        
        public List<RecetasDetalle> Receta_Detalle(string serviceId)
        {
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena1 =
                " select r.v_ServiceId as 'Servicio', " +
                " prod.v_Descripcion as 'Medicina', " +
                " CONVERT (int, r.d_Cantidad) as 'Cantidad', " +
                " CONVERT(DECIMAL(16,2),(CASE WHEN r.d_SaldoAseguradora IS NULL THEN 0.00 ELSE r.d_SaldoAseguradora END + r.d_SaldoPaciente) / r.d_Cantidad) as 'P_Unitario', " +
                " CONVERT(DECIMAL(16,2),(CASE WHEN r.d_SaldoPaciente IS NULL THEN 0.00 ELSE r.d_SaldoPaciente END )) as 'SaldoPaciente', " +
                " CONVERT(DECIMAL(16,2),(CASE WHEN r.d_SaldoAseguradora IS NULL THEN 0.00 ELSE r.d_SaldoAseguradora END)) as 'SaldoAseguradora', " +
                " CONVERT(DECIMAL(16,2),(CASE WHEN r.d_SaldoAseguradora IS NULL THEN 0.00 ELSE r.d_SaldoAseguradora END + r.d_SaldoPaciente)) as 'Total', " +
                " p.v_FirstName + ' ' + p.v_FirstLastName + ' ' + p.v_SecondLastName as 'Medico', " +
                " pt.v_FirstName + ' ' + pt.v_FirstLastName + ' ' + pt.v_SecondLastName as 'Trabajador', " +
                " r.d_InsertDate as 'Fecha' " +
                " from receta as r " +
                " JOIN receipHeader as rh on r.v_ReceipId = rh.v_ReceipId" +
                " LEFT JOIN systemuser as sy on r.v_MedicoTratante = sy.i_SystemUserId " +
                " LEFT JOIN person as p on sy.v_PersonId = p.v_PersonId " +
                " LEFT JOIN [20505310072].dbo.productodetalle as pd on r.v_IdProductoDetalle = pd.v_IdProductoDetalle " + 
                " LEFT JOIN [20505310072].dbo.producto as prod on pd.v_IdProducto = prod.v_IdProducto " +
                " LEFT JOIN systemuser as syt on r.i_InsertUserId = syt.i_SystemUserId " +
                " LEFT JOIN person as pt on syt.v_PersonId = pt.v_PersonId " +
                "where rh.v_ServiceId = '" + serviceId + "' and r.i_Lleva = 1 and  rh.i_IsDeleted = 0 and r.i_IsDeleted = 0 ";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector1 = comando.ExecuteReader();
            RecetasDetalle _oRecetaMedicina = new RecetasDetalle();
            List<RecetasDetalle> _Lista = new List<RecetasDetalle>();
            while (lector1.Read())
            {
                _oRecetaMedicina = new RecetasDetalle();
                _oRecetaMedicina.Servicio = lector1.GetValue(0).ToString();
                _oRecetaMedicina.Medicina = lector1.GetValue(1).ToString();
                _oRecetaMedicina.Cantidad = int.Parse(lector1.GetValue(2).ToString());
                _oRecetaMedicina.P_Unitario = decimal.Parse(lector1.GetValue(3).ToString());
                _oRecetaMedicina.SaldoPaciente = decimal.Parse(lector1.GetValue(4).ToString());
                _oRecetaMedicina.SaldoAseguradora = decimal.Parse(lector1.GetValue(5).ToString());
                _oRecetaMedicina.Total = decimal.Parse(lector1.GetValue(6).ToString());
                _oRecetaMedicina.Medico = lector1.GetValue(7).ToString();
                _oRecetaMedicina.Trabajador = lector1.GetValue(8).ToString();
                _oRecetaMedicina.Fecha = DateTime.Parse(lector1.GetValue(9).ToString());
                _Lista.Add(_oRecetaMedicina);
            }
            lector1.Close();
            conectasam.closesigesoft();

            var lista = _Lista.ToList().GroupBy(g => g.Servicio).Select(s => s.First()).ToList();

            RecetasDetalle _oReceta1 = new RecetasDetalle();
            List<RecetasDetalle> _Lista1 = new List<RecetasDetalle>();
            foreach (var item in lista)
            {
                _oReceta1 = new RecetasDetalle();
                _oReceta1.Receta = "RECETA X TTO";
                decimal TotalSuma = 0;
                int Cantidad = 0;
                int Medicinas = 0;
                var ListaTip = _Lista.ToList().FindAll(p => p.Servicio == item.Servicio).ToList();

                var listarecetaDetalle = new List<RecetasDetalle>();
                foreach (var item2 in ListaTip)
                {
                    var oReceta = new RecetasDetalle();
                    oReceta.Servicio = item2.Servicio;
                    oReceta.Medicina = item2.Medicina;
                    oReceta.Cantidad = item2.Cantidad;
                    oReceta.P_Unitario = item2.P_Unitario;
                    oReceta.SaldoPaciente = item2.SaldoPaciente;
                    oReceta.SaldoAseguradora = item2.SaldoAseguradora;
                    oReceta.Total = item2.Total;
                    oReceta.Medico = item2.Medico;
                    oReceta.Trabajador = item2.Trabajador;
                    oReceta.Fecha = item2.Fecha;
                    listarecetaDetalle.Add(oReceta);
                }
                _oReceta1.Lista = listarecetaDetalle;
                _oReceta1.Total = TotalSuma;
                _Lista1.Add(_oReceta1);
            }

            return _Lista1;
        }

        public decimal PrecioDedudicbleSeguro(string serviceId)
        {
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena1 =
                @"select ISNULL(ROUND(SUM(sc.r_Price),2),0) as 'PRECIO' 
                from service s
                join servicecomponent sc on s.v_ServiceId = sc.v_ServiceId
                join component comp on sc.v_ComponentId = comp.v_ComponentId " +
                " where s.v_ServiceId = '" + serviceId + "' and sc.i_IsRequiredId = 1 and i_DeduciblePay = 1 ";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector1 = comando.ExecuteReader();
            decimal PRECIO = 0;
            while (lector1.Read())
            {
                PRECIO = decimal.Parse(lector1.GetValue(0).ToString());
            }
            lector1.Close();
            conectasam.closesigesoft();

            return PRECIO;
        }

    }
}
