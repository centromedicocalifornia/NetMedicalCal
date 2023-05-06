using System;
using System.Collections.Generic;
using System.Linq;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Node.WinClient.BE.Custom;
using ConnectionState = System.Data.ConnectionState;
using Sigesoft.Common;
namespace Sigesoft.Node.WinClient.BLL
{
    public class GerenciaResonanciasBL
    {
        public List<GerenciaResonancias> Filter(DateTime startDate, DateTime endDate)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();
                dbContext.CommandTimeout = 100000;
                //9 = particulares
                var query = (from a in dbContext.gerenciaresonancias(startDate, endDate, 282)
                             select new GerenciaResonancias
                             {
                                 Servicio = a.Servicio,
                                 Paciente = a.Paciente,
                                 F_Servicio = a.F_Servicio.Value,
                                 Identificador = a.Identificador,
                                 Resonancia = a.Resonancia,
                                 F_Resonancia = a.F_Resonancia.Value,
                                 Precio = decimal.Parse(a.Precio.ToString()),
                                 Saldo_Paciente = decimal.Parse(a.Saldo_Paciente.ToString()),
                                 SaldoAseguradora = decimal.Parse(a.SaldoAseguradora.ToString()),
                                 Medico = a.Medico,
                                 Protocolo = a.Protocolo,
                                 Plan = a.Plan,
                                 Procedencia = a.Procedencia,
                                 Factor = decimal.Parse(a.Factor.ToString()),
                                 Descuento_PPS = a.Descuento_PPS,
                                 Deducible = a.Deducible,
                                 Coaseguro = a.Coaseguro,
                                 Trabajador = a.Trabajador,
                                 Tipo = a.Tipo,
                                 Value1 = a.Value1,
                                 Value2 = a.Value2
                             }).ToList();

                var tipo = query.ToList().GroupBy(g => g.Tipo).Select(s => s.First()).ToList();

                List<GerenciaResonancias> Lista = new List<GerenciaResonancias>();
                GerenciaResonancias oServicioResonancias;

                foreach (var item in tipo)
                {
                    decimal total = 0;
                    oServicioResonancias = new GerenciaResonancias();
                    oServicioResonancias.Tipo = item.Tipo;

                    var tipo_ = query.ToList().FindAll(p => p.Tipo == item.Tipo).ToList();
                    var listresonancias = new List<GerenciaResonancias>();
                    foreach (var resonancias in tipo_)
                    {
                        var oMedicoMedicinas = new GerenciaResonancias();
                        oMedicoMedicinas.Servicio = resonancias.Servicio;
                        oMedicoMedicinas.Paciente = resonancias.Paciente;
                        oMedicoMedicinas.F_Servicio = resonancias.F_Servicio;
                        oMedicoMedicinas.Identificador = resonancias.Identificador;
                        oMedicoMedicinas.Resonancia = resonancias.Resonancia;
                        oMedicoMedicinas.F_Resonancia = resonancias.F_Resonancia;
                        oMedicoMedicinas.Precio = resonancias.Precio;
                        oMedicoMedicinas.Saldo_Paciente = resonancias.Saldo_Paciente;
                        oMedicoMedicinas.SaldoAseguradora = resonancias.SaldoAseguradora;
                        oMedicoMedicinas.Medico = resonancias.Medico;
                        oMedicoMedicinas.Protocolo = resonancias.Protocolo;
                        oMedicoMedicinas.Plan = resonancias.Plan;
                        oMedicoMedicinas.Procedencia = resonancias.Procedencia;
                        oMedicoMedicinas.Factor = resonancias.Factor;
                        oMedicoMedicinas.Descuento_PPS = resonancias.Descuento_PPS;
                        oMedicoMedicinas.Deducible = resonancias.Deducible;
                        oMedicoMedicinas.Coaseguro = resonancias.Coaseguro;
                        oMedicoMedicinas.Trabajador = resonancias.Trabajador;
                        oMedicoMedicinas.Value1 = resonancias.Value1;
                        oMedicoMedicinas.Value2 = resonancias.Value2;
                        //llenar
                        total += decimal.Parse(resonancias.Precio.ToString());
                        listresonancias.Add(oMedicoMedicinas);
                    }
                    oServicioResonancias.Total_Reonancias = decimal.Round(total, 2);

                    oServicioResonancias.ListaResonancias = listresonancias;

                    Lista.Add(oServicioResonancias);
                }

                return Lista;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<GerenciaResonancias> ExamenesAuxiliares(DateTime startDate, DateTime endDate, int tipoExamen)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();
                dbContext.CommandTimeout = 100000;
                //9 = particulares
                var query = (from a in dbContext.gerenciaresonancias(startDate, endDate, tipoExamen)
                             select new GerenciaResonancias
                             {
                                 Servicio = a.Servicio,
                                 Paciente = a.Paciente,
                                 F_Servicio = a.F_Servicio.Value,
                                 Identificador = a.Identificador,
                                 Resonancia = a.Resonancia,
                                 F_Resonancia = a.F_Resonancia.Value,
                                 Precio = decimal.Parse(a.Precio.ToString()),
                                 Saldo_Paciente = decimal.Parse(a.Saldo_Paciente.ToString()),
                                 SaldoAseguradora = decimal.Parse(a.SaldoAseguradora.ToString()),
                                 Medico = a.Medico,
                                 Protocolo = a.Protocolo,
                                 Plan = a.Plan,
                                 Procedencia = a.Procedencia,
                                 Factor = decimal.Parse(a.Factor.ToString()),
                                 Descuento_PPS = a.Descuento_PPS,
                                 Deducible = a.Deducible,
                                 Coaseguro = a.Coaseguro,
                                 Trabajador = a.Trabajador,
                                 Tipo = a.Tipo,
                                 Value1 = a.Value1,
                                 Value2 = a.Value2
                             }).ToList();

                var tipo = query.ToList().GroupBy(g => g.Tipo).Select(s => s.First()).ToList();

                List<GerenciaResonancias> Lista = new List<GerenciaResonancias>();
                GerenciaResonancias oServicioResonancias;

                foreach (var item in tipo)
                {
                    decimal total = 0;
                    decimal totalComision = 0;
                    oServicioResonancias = new GerenciaResonancias();
                    oServicioResonancias.Tipo = item.Tipo;

                    var tipo_ = query.ToList().FindAll(p => p.Tipo == item.Tipo).ToList();
                    var listresonancias = new List<GerenciaResonancias>();
                    foreach (var resonancias in tipo_)
                    {
                        var oMedicoMedicinas = new GerenciaResonancias();
                        oMedicoMedicinas.Servicio = resonancias.Servicio;
                        oMedicoMedicinas.Paciente = resonancias.Paciente;
                        oMedicoMedicinas.F_Servicio = resonancias.F_Servicio;
                        oMedicoMedicinas.Identificador = resonancias.Identificador;
                        oMedicoMedicinas.Resonancia = resonancias.Resonancia;
                        oMedicoMedicinas.F_Resonancia = resonancias.F_Resonancia;
                        oMedicoMedicinas.Precio = resonancias.Precio;
                        oMedicoMedicinas.Saldo_Paciente = resonancias.Saldo_Paciente;
                        oMedicoMedicinas.SaldoAseguradora = resonancias.SaldoAseguradora;
                        oMedicoMedicinas.Medico = resonancias.Medico;
                        oMedicoMedicinas.Protocolo = resonancias.Protocolo;
                        oMedicoMedicinas.Plan = resonancias.Plan;
                        oMedicoMedicinas.Procedencia = resonancias.Procedencia;
                        oMedicoMedicinas.Factor = resonancias.Factor;
                        oMedicoMedicinas.Descuento_PPS = resonancias.Descuento_PPS;
                        oMedicoMedicinas.Deducible = resonancias.Deducible;
                        oMedicoMedicinas.Coaseguro = resonancias.Coaseguro;
                        oMedicoMedicinas.Trabajador = resonancias.Trabajador;

                        if (resonancias.Value1 != "11" && resonancias.Value1 != null)
                        {
                            totalComision += decimal.Round(((decimal.Parse(resonancias.Precio.ToString())) / decimal.Parse("1.18")) * decimal.Parse("0.05"), 2);
                            oMedicoMedicinas.Comision = decimal.Round(((decimal.Parse(resonancias.Precio.ToString())) / decimal.Parse("1.18")) * decimal.Parse("0.05"), 2);
                        }
                        else
                        {
                            oMedicoMedicinas.Comision = decimal.Parse("0.00");
                        }
                        oMedicoMedicinas.Value1 = resonancias.Value1;
                        oMedicoMedicinas.Value2 = resonancias.Value2;
                        //llenar
                        total += decimal.Parse(resonancias.Precio.ToString());
                        listresonancias.Add(oMedicoMedicinas);
                    }
                    oServicioResonancias.Total_Reonancias = decimal.Round(total, 2);
                    oServicioResonancias.PagosMedicos = decimal.Round(totalComision, 2);

                    oServicioResonancias.ListaResonancias = listresonancias;

                    Lista.Add(oServicioResonancias);
                }

                return Lista;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<GerenciaVentasDetalle> Filter2(DateTime startDate, DateTime endDate)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();
                dbContext.CommandTimeout = 100000;
                //9 = particulares
                var query = (from a in dbContext.gerenciaventas(startDate, endDate, 2, "")
                             select new GerenciaVentasDetalle
                             {
                                 Venta = a.Venta,
                                 Serie = a.Serie,
                                 Correlativo = a.Correlativo,
                                 Cliente = a.Cliente,
                                 Total = decimal.Parse(a.Total.ToString()),
                                 Descripcion = a.Descripcion,
                                 Cantidad = int.Parse(a.Cantidad.ToString()),
                                 PrecioU = a.PrecioV.Value,
                                 PrecioV = decimal.Parse(a.PrecioU.ToString()),
                                 FechaEmision = a.FechaEmision.Value,
                                 Condicion = a.Condicion,
                                 Tipo = a.Tipo,
                                 Usuario1 = a.Usuario1,
                                 Servicio = a.Servicio,
                                 Paciente = a.Paciente,
                                 FechaServicio = a.FechaServicio.Value,
                                 Comprobante = a.Comprobante,
                                 Usuario2 = a.Usuario2,
                                 Value1 = a.Value1,
                                 Value2 = a.Value2
                             }).ToList();

                var tipo = query.ToList().GroupBy(g => g.Tipo).Select(s => s.First()).ToList();

                //List<GerenciaVentasDetalle> Lista = new List<GerenciaVentasDetalle>();
                //GerenciaVentasDetalle oServicioResonancias;

                //foreach (var item in tipo)
                //{
                //    decimal total = 0;
                //    oServicioResonancias = new GerenciaVentasDetalle();
                //    oServicioResonancias.Tipo = item.Tipo;

                //    var tipo_ = query.ToList().FindAll(p => p.Tipo == item.Tipo).ToList();
                //    var listresonancias = new List<GerenciaVentasDetalle>();
                //    foreach (var resonancias in tipo_)
                //    {
                //        var oMedicoMedicinas = new GerenciaVentasDetalle();
                //        oMedicoMedicinas.Cliente = resonancias.Cliente;
                //        oMedicoMedicinas.Total = resonancias.Total;

                //        oMedicoMedicinas.Value1 = resonancias.Value1;
                //        oMedicoMedicinas.Value2 = resonancias.Value2;
                //        //llenar
                //        total += decimal.Parse(resonancias.Total.ToString());
                //        listresonancias.Add(oMedicoMedicinas);
                //    }
                //    oServicioResonancias.TotalGrupo = decimal.Round(total, 2);

                //    oServicioResonancias.ListaVentas = listresonancias;

                //    Lista.Add(oServicioResonancias);
                //}

                return tipo;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
