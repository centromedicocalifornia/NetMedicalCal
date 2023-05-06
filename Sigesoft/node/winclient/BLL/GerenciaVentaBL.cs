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
    public class GerenciaVentaBL
    {
        public List<GerenciaVentasDetalle> BusquedaMTC(DateTime startDate, DateTime endDate)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();
                dbContext.CommandTimeout = 100000;

                var query = (from a in dbContext.gerenciaventas(startDate, endDate, 7, "")
                             select new GerenciaVentasDetalle
                             {
                                 Venta = a.Venta,
                                 Serie = a.Serie,
                                 Correlativo = a.Correlativo,
                                 Cliente = a.Cliente,
                                 Total = decimal.Parse(a.Total.ToString()),
                                 Descripcion = a.Descripcion,
                                 Cantidad = a.Cantidad.Value,
                                 PrecioU = decimal.Parse(a.PrecioV.ToString()),
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

                var medicos = query.ToList().GroupBy(g => g.Tipo).Select(s => s.First()).ToList();

                List<GerenciaVentasDetalle> _agrupadoTipo = new List<GerenciaVentasDetalle>();
                GerenciaVentasDetalle oagrupadoTipo;

                var _lista = query.ToList().GroupBy(g => g.Venta).Select(s => s.First()).ToList();

                foreach (var item in medicos)
                {
                    decimal total = 0;
                    int contador = 0;

                    oagrupadoTipo = new GerenciaVentasDetalle();
                    oagrupadoTipo.Tipo = item.Tipo;

                    var _ventas = _lista.ToList().FindAll(p => p.Tipo == item.Tipo).ToList();


                    var listaventas = new List<GerenciaVentasDetalleVent>();
                    foreach (var vent in _ventas)
                    {
                        var oventas = new GerenciaVentasDetalleVent();
                        oventas.Venta = vent.Venta;
                        oventas.Serie = vent.Serie;
                        oventas.Correlativo = vent.Correlativo;
                        oventas.Cliente = vent.Cliente;
                        oventas.Total = vent.Total;
                        oventas.FechaEmision = vent.FechaEmision;
                        oventas.Tipo = vent.Tipo;
                        oventas.Paciente = vent.Paciente;
                        oventas.Servicio = vent.Servicio;
                        oventas.Usuario1 = vent.Usuario1;

                        var v_detallev = query.ToList().FindAll(p => p.Venta == vent.Venta).ToList();
                        var v_detallevF = v_detallev.ToList().GroupBy(g => g.Descripcion).Select(s => s.First()).ToList();
                        var listadetalleVenta = new List<ListaVentaDetalle>();
                        foreach (var detalle in v_detallevF)
                        {
                            var odetalle = new ListaVentaDetalle();
                            odetalle.Descripcion = detalle.Descripcion;
                            odetalle.Cantidad = detalle.Cantidad;
                            odetalle.PrecioU = detalle.PrecioU;
                            odetalle.PrecioV = detalle.PrecioV;
                            listadetalleVenta.Add(odetalle);
                        }

                        oventas.ListaDetalle = listadetalleVenta;

                        total += decimal.Parse(vent.Total.ToString());

                        listaventas.Add(oventas);
                        contador++;

                    }
                    oagrupadoTipo.TotalGrupo = decimal.Round(total, 2);
                    oagrupadoTipo.CantidadGrupo = contador;

                    oagrupadoTipo.ListaVentas = listaventas;

                    _agrupadoTipo.Add(oagrupadoTipo);
                }

                return _agrupadoTipo;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<GerenciaVentasDetalle> Busqueda(DateTime startDate, DateTime endDate)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();
                dbContext.CommandTimeout = 100000;

                List<GerenciaVentasDetalle> query1 = (from a in dbContext.gerenciaventas(startDate, endDate, 2, "")
                             select new GerenciaVentasDetalle
                             {
                                 Venta = a.Venta,
                                 Serie = a.Serie,
                                 Correlativo = a.Correlativo,
                                 Cliente = a.Cliente,
                                 Total = decimal.Parse(a.Total.ToString()),
                                 Descripcion = a.Descripcion,
                                 Cantidad = a.Cantidad.Value,
                                 PrecioU = decimal.Parse(a.PrecioV.ToString()),
                                 PrecioV = decimal.Parse(a.PrecioU.ToString()),
                                 FechaEmision =a.FechaEmision.Value,
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

                List<GerenciaVentasDetalle> query2 = (from a in dbContext.gerenciaventas(startDate, endDate, 8, "")
                    select new GerenciaVentasDetalle
                    {
                        Venta = a.Venta,
                        Serie = a.Serie,
                        Correlativo = a.Correlativo,
                        Cliente = a.Cliente,
                        Total = decimal.Parse(a.Total.ToString()),
                        Descripcion = a.Descripcion,
                        Cantidad = a.Cantidad.Value,
                        PrecioU = decimal.Parse(a.PrecioV.ToString()),
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

                List<GerenciaVentasDetalle> query3 = (from a in dbContext.gerenciaventas(startDate, endDate, 9, "")
                    select new GerenciaVentasDetalle
                    {
                        Venta = a.Venta,
                        Serie = a.Serie,
                        Correlativo = a.Correlativo,
                        Cliente = a.Cliente,
                        Total = decimal.Parse(a.Total.ToString()),
                        Descripcion = a.Descripcion,
                        Cantidad = a.Cantidad.Value,
                        PrecioU = decimal.Parse(a.PrecioV.ToString()),
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

                var query = query1.Union(query2).Union(query3).ToList();
                
                var medicos = query.ToList().GroupBy(g => g.Tipo).Select(s => s.First()).ToList();

                List<GerenciaVentasDetalle> _agrupadoTipo = new List<GerenciaVentasDetalle>();
                GerenciaVentasDetalle oagrupadoTipo;

                var _lista = query.ToList().GroupBy(g => g.Venta).Select(s => s.First()).ToList();

                foreach (var item in medicos)
                {
                    decimal total = 0;
                    int contador = 0;

                    oagrupadoTipo = new GerenciaVentasDetalle();
                    oagrupadoTipo.Tipo = item.Tipo;

                    var _ventas = _lista.ToList().FindAll(p => p.Tipo == item.Tipo).ToList();


                    var listaventas = new List<GerenciaVentasDetalleVent>();
                    foreach (var vent in _ventas)
                    {
                        var oventas = new GerenciaVentasDetalleVent();
                        oventas.Venta = vent.Venta;
                        oventas.Serie = vent.Serie;
                        oventas.Correlativo = vent.Correlativo;
                        oventas.Cliente = vent.Cliente;
                        oventas.Total = vent.Total;
                        oventas.FechaEmision = vent.FechaEmision;
                        oventas.Tipo = vent.Tipo;
                        oventas.Paciente = vent.Paciente;
                        oventas.Servicio = vent.Servicio;
                        oventas.Usuario1 = vent.Usuario1;
                        oventas.FechaServicio = vent.FechaServicio;
                        oventas.Value1 = vent.Value1;
                        oventas.Value2 = vent.Value2;

                        var v_detallev = query.ToList().FindAll(p => p.Venta == vent.Venta).ToList();
                        var v_detallevF = v_detallev.ToList().GroupBy(g => g.Descripcion).Select(s => s.First()).ToList();
                        var listadetalleVenta = new List<ListaVentaDetalle>();
                        foreach (var detalle in v_detallevF)
                       {
                           var odetalle = new ListaVentaDetalle();
                            odetalle.Descripcion = detalle.Descripcion;
                            odetalle.Cantidad = detalle.Cantidad;
                            odetalle.PrecioU = detalle.PrecioU;
                            odetalle.PrecioV = detalle.PrecioV;
                            listadetalleVenta.Add(odetalle);

                        }

                        oventas.ListaDetalle = listadetalleVenta;

                        total += decimal.Parse(vent.Total.ToString());

                        listaventas.Add(oventas);
                        contador++;

                    }
                    oagrupadoTipo.TotalGrupo = decimal.Round(total, 2);
                    oagrupadoTipo.CantidadGrupo = contador;

                    oagrupadoTipo.ListaVentas = listaventas;

                    _agrupadoTipo.Add(oagrupadoTipo);
                }

                return _agrupadoTipo;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<GerenciaVentasDetalle> BusquedaOcupacional(DateTime startDate, DateTime endDate)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();
                dbContext.CommandTimeout = 100000;

                var query = (from a in dbContext.gerenciaventas(startDate, endDate, 1, "")
                             select new GerenciaVentasDetalle
                             {
                                 Venta = a.Venta,
                                 Serie = a.Serie,
                                 Correlativo = a.Correlativo,
                                 Cliente = a.Cliente,
                                 Total = decimal.Parse(a.Total.ToString()),
                                 Descripcion = a.Descripcion,
                                 Cantidad = a.Cantidad.Value,
                                 PrecioU = decimal.Parse(a.PrecioV.ToString()),
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

                var medicos = query.ToList().GroupBy(g => g.Tipo).Select(s => s.First()).ToList();

                List<GerenciaVentasDetalle> _agrupadoTipo = new List<GerenciaVentasDetalle>();
                GerenciaVentasDetalle oagrupadoTipo;

                var _lista = query.ToList().GroupBy(g => g.Venta).Select(s => s.First()).ToList();

                foreach (var item in medicos)
                {
                    decimal total = 0;
                    int contador = 0;

                    oagrupadoTipo = new GerenciaVentasDetalle();
                    oagrupadoTipo.Tipo = item.Tipo;

                    var _ventas = _lista.ToList().FindAll(p => p.Tipo == item.Tipo).ToList();


                    var listaventas = new List<GerenciaVentasDetalleVent>();
                    foreach (var vent in _ventas)
                    {
                        var oventas = new GerenciaVentasDetalleVent();
                        oventas.Venta = vent.Venta;
                        oventas.Serie = vent.Serie;
                        oventas.Correlativo = vent.Correlativo;
                        oventas.Cliente = vent.Cliente;
                        oventas.Total = vent.Total;
                        oventas.FechaEmision = vent.FechaEmision;
                        oventas.Tipo = vent.Tipo;
                        oventas.Paciente = vent.Paciente;
                        oventas.Servicio = vent.Servicio;
                        oventas.Usuario1 = vent.Usuario1;

                        var v_detallev = query.ToList().FindAll(p => p.Venta == vent.Venta).ToList();

                        var listadetalleVenta = new List<ListaVentaDetalle>();
                        foreach (var detalle in v_detallev)
                        {
                            var odetalle = new ListaVentaDetalle();
                            odetalle.Descripcion = detalle.Descripcion;
                            odetalle.Cantidad = detalle.Cantidad;
                            odetalle.PrecioU = detalle.PrecioU;
                            odetalle.PrecioV = detalle.PrecioV;
                            listadetalleVenta.Add(odetalle);
                        }

                        oventas.ListaDetalle = listadetalleVenta;

                        total += decimal.Parse(vent.Total.ToString());

                        listaventas.Add(oventas);
                        contador++;

                    }
                    oagrupadoTipo.TotalGrupo = decimal.Round(total, 2);
                    oagrupadoTipo.CantidadGrupo = contador;

                    oagrupadoTipo.ListaVentas = listaventas;

                    _agrupadoTipo.Add(oagrupadoTipo);
                }

                return _agrupadoTipo;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<GerenciaVentasDetalle> BusquedaFarmacia(DateTime startDate, DateTime endDate)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();
                dbContext.CommandTimeout = 100000;

                var query = (from a in dbContext.gerenciaventas(startDate, endDate, 3, "")
                             select new GerenciaVentasDetalle
                             {
                                 Venta = a.Venta,
                                 Serie = a.Serie,
                                 Correlativo = a.Correlativo,
                                 Cliente = a.Cliente,
                                 Total = decimal.Parse(a.Total.ToString()),
                                 Descripcion = a.Descripcion,
                                 Cantidad = a.Cantidad.Value,
                                 PrecioU = decimal.Parse(a.PrecioV.ToString()),
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

                var medicos = query.ToList().GroupBy(g => g.Tipo).Select(s => s.First()).ToList();

                List<GerenciaVentasDetalle> _agrupadoTipo = new List<GerenciaVentasDetalle>();
                GerenciaVentasDetalle oagrupadoTipo;

                var _lista = query.ToList().GroupBy(g => g.Venta).Select(s => s.First()).ToList();

                foreach (var item in medicos)
                {
                    decimal total = 0;
                    int contador = 0;

                    oagrupadoTipo = new GerenciaVentasDetalle();
                    oagrupadoTipo.Tipo = item.Tipo;

                    var _ventas = _lista.ToList().FindAll(p => p.Tipo == item.Tipo).ToList();


                    var listaventas = new List<GerenciaVentasDetalleVent>();
                    foreach (var vent in _ventas)
                    {
                        var oventas = new GerenciaVentasDetalleVent();
                        oventas.Venta = vent.Venta;
                        oventas.Serie = vent.Serie;
                        oventas.Correlativo = vent.Correlativo;
                        oventas.Cliente = vent.Cliente;
                        oventas.Total = vent.Total;
                        oventas.FechaEmision = vent.FechaEmision;
                        oventas.Tipo = vent.Tipo;
                        oventas.Paciente = vent.Paciente;
                        oventas.Servicio = vent.Servicio;
                        oventas.Usuario1 = vent.Usuario1;

                        var v_detallev = query.ToList().FindAll(p => p.Venta == vent.Venta).ToList();

                        var listadetalleVenta = new List<ListaVentaDetalle>();
                        foreach (var detalle in v_detallev)
                        {
                            var odetalle = new ListaVentaDetalle();
                            odetalle.Descripcion = detalle.Descripcion;
                            odetalle.Cantidad = detalle.Cantidad;
                            odetalle.PrecioU = detalle.PrecioU;
                            odetalle.PrecioV = detalle.PrecioV;
                            listadetalleVenta.Add(odetalle);
                        }

                        oventas.ListaDetalle = listadetalleVenta;

                        total += decimal.Parse(vent.Total.ToString());

                        listaventas.Add(oventas);
                        contador++;

                    }
                    oagrupadoTipo.TotalGrupo = decimal.Round(total, 2);
                    oagrupadoTipo.CantidadGrupo = contador;

                    oagrupadoTipo.ListaVentas = listaventas;

                    _agrupadoTipo.Add(oagrupadoTipo);
                }

                return _agrupadoTipo;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
