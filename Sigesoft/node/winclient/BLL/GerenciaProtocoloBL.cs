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
   
    public class GerenciaProtocoloBL
    {
        public List<GerenciaProtocolo> Filter(DateTime startDate, DateTime endDate)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();
                dbContext.CommandTimeout = 100000;
                //9 = particulares
                var query = (from a in dbContext.gerenciatipoprotocolo(startDate, endDate, 9, "-")
                             select new GerenciaProtocolo
                             {
                                 Servicio = a.Servicio,
                                 Paciente = a.Paciente,
                                 Protocolo = a.Protocolo,
                                 Fecha = a.Fecha,
                                 Trabajador = a.Trabajador,
                                 SRV = a.SRV,
                                 DOC = a.DOC,
                                 Total = a.Total,
                                 Saldo = a.Saldo,
                                 F_Comprobante = a.F_Comprobante,
                                 Condicion = a.Condicion,
                                 Procedencia = a.Procedencia,
                                 Medico = a.Medico,
                                 Tipo = a.Tipo,
                                 ProduccionHospi = a.ProduccionHospi,
                                 ProduccionServices = a.ProduccionServices,
                                 Value1 = a.Value1,
                                 Value2 = a.Value2
                             }).ToList();

                var lista1 = query.ToList().GroupBy(g => g.DOC).Select(s => s.First()).ToList();

                List<GerenciaProtocolo> _ListaGerenciaProt1 = new List<GerenciaProtocolo>();

                GerenciaProtocolo oListaProtocolo;

                string cadenaServicios = "";
                foreach (var item1 in lista1)
                {
                    var medicinasMedico = query.ToList().FindAll(p => p.DOC == item1.DOC).ToList();

                    if (medicinasMedico.Count() > 1)
                    {
                        int count = 1;
                        foreach (var item in medicinasMedico)
                        {
                            if (count == 1)
                            {
                                oListaProtocolo = new GerenciaProtocolo();
                                oListaProtocolo.Servicio = item.Servicio;
                                oListaProtocolo.Paciente = item.Paciente;
                                oListaProtocolo.Protocolo = item.Protocolo;
                                oListaProtocolo.Fecha = item.Fecha;
                                oListaProtocolo.Trabajador = item.Trabajador;
                                oListaProtocolo.SRV = item.SRV;
                                oListaProtocolo.DOC = item.DOC;
                                oListaProtocolo.Total = item.Total;
                                oListaProtocolo.Saldo = item.Saldo;
                                oListaProtocolo.F_Comprobante = item.F_Comprobante;
                                oListaProtocolo.Condicion = item.Condicion;
                                oListaProtocolo.Procedencia = item.Procedencia;
                                oListaProtocolo.Medico = item.Medico;
                                oListaProtocolo.Tipo = item.Tipo;
                                if (item.ProduccionHospi != 0 && item.ProduccionHospi != null)
                                {
                                    oListaProtocolo.Produccion = decimal.Parse(item.ProduccionHospi.ToString());
                                }
                                else
                                {
                                    oListaProtocolo.Produccion = decimal.Parse(item.ProduccionServices.ToString());
                                }
                                _ListaGerenciaProt1.Add(oListaProtocolo);
                            }
                            else
                            {
                                oListaProtocolo = new GerenciaProtocolo();
                                oListaProtocolo.Servicio = item.Servicio;
                                oListaProtocolo.Paciente = item.Paciente;
                                oListaProtocolo.Protocolo = item.Protocolo;
                                oListaProtocolo.Fecha = item.Fecha;
                                oListaProtocolo.Trabajador = item.Trabajador;
                                oListaProtocolo.SRV = item.SRV;
                                oListaProtocolo.DOC = item.DOC;
                                oListaProtocolo.Total = decimal.Parse("0.00");
                                oListaProtocolo.Saldo = decimal.Parse("0.00");
                                oListaProtocolo.F_Comprobante = item.F_Comprobante;
                                oListaProtocolo.Condicion = item.Condicion;
                                oListaProtocolo.Procedencia = item.Procedencia;
                                oListaProtocolo.Medico = item.Medico;
                                oListaProtocolo.Tipo = item.Tipo;
                                if (item.ProduccionHospi != 0 && item.ProduccionHospi != null)
                                {
                                    oListaProtocolo.Produccion = decimal.Parse(item.ProduccionHospi.ToString());
                                }
                                else
                                {
                                    oListaProtocolo.Produccion = decimal.Parse(item.ProduccionServices.ToString());
                                }
                                _ListaGerenciaProt1.Add(oListaProtocolo);
                            }
                            count++;
                        }
                    }
                    else
                    {
                        foreach (var item in medicinasMedico)
                        {
                            oListaProtocolo = new GerenciaProtocolo();
                            oListaProtocolo.Servicio = item.Servicio;
                            oListaProtocolo.Paciente = item.Paciente;
                            oListaProtocolo.Protocolo = item.Protocolo;
                            oListaProtocolo.Fecha = item.Fecha;
                            oListaProtocolo.Trabajador = item.Trabajador;
                            oListaProtocolo.SRV = item.SRV;
                            oListaProtocolo.DOC = item.DOC;
                            oListaProtocolo.Total = item.Total;
                            oListaProtocolo.Saldo = item.Saldo;
                            oListaProtocolo.F_Comprobante = item.F_Comprobante;
                            oListaProtocolo.Condicion = item.Condicion;
                            oListaProtocolo.Procedencia = item.Procedencia;
                            oListaProtocolo.Medico = item.Medico;
                            oListaProtocolo.Tipo = item.Tipo;
                            if (item.ProduccionHospi != 0 && item.ProduccionHospi != null)
                            {
                                oListaProtocolo.Produccion = decimal.Parse(item.ProduccionHospi.ToString());
                            }
                            else
                            {
                                oListaProtocolo.Produccion = decimal.Parse(item.ProduccionServices.ToString());
                            }
                            _ListaGerenciaProt1.Add(oListaProtocolo);
                        }
                    }
                    
                }

                        //cadenaServicios = cadenaServicios +" - " + item.Servicio;
                   
                return _ListaGerenciaProt1;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<GerenciaTreeTipoProtocolo> ProcessDataTreeView(List<GerenciaProtocolo> data)
        {
            var list = Agrupador(data);
            Perfiles(data, list);
            //Perfiles(data, list);
            return list;
        }

        public List<GerenciaTreeTipoProtocolo> Agrupador(List<GerenciaProtocolo> data)
        {
            var list = new List<GerenciaTreeTipoProtocolo>();
            var oGerenciaTreeTipoExamen = new GerenciaTreeTipoProtocolo();
            oGerenciaTreeTipoExamen.Cantidad = data.Count;
            oGerenciaTreeTipoExamen.Agrupador = "PARTICULARES";
            oGerenciaTreeTipoExamen.TotalVenta = decimal.Round((decimal.Parse(data.Sum(s => s.Total).ToString())), 2);
            oGerenciaTreeTipoExamen.TotalProduccion = decimal.Round((decimal.Parse(data.Sum(s => s.Produccion).ToString())), 2);
            list.Add(oGerenciaTreeTipoExamen);

            return list;
        }

        public List<GerenciaTreeTipoProtocolo> Perfiles(List<GerenciaProtocolo> data, List<GerenciaTreeTipoProtocolo> list)
        {
            var perfiles = new List<Perfil_1>();
            var protocolo = data.GroupBy(g => g.Protocolo).Select(s => s.First()).ToList();
            foreach (var prot in protocolo)
            {
                var oGerenciaTipoExamen = new Sigesoft.Node.WinClient.BE.Custom.Perfil_1();
                oGerenciaTipoExamen.TipoEso = prot.Protocolo;
                oGerenciaTipoExamen.Cantidad = data.FindAll(p => p.Protocolo == prot.Protocolo).Count;
                oGerenciaTipoExamen.TotalVenta = decimal.Round((decimal.Parse(data.FindAll(p => p.Protocolo == prot.Protocolo).Sum(s => s.Total).ToString())), 2);
                oGerenciaTipoExamen.TotalProduccionn = decimal.Round((decimal.Parse(data.FindAll(p => p.Protocolo == prot.Protocolo).Sum(s => s.Produccion).ToString())), 2);              
                perfiles.Add(oGerenciaTipoExamen);


                var tipo = data.FindAll(p => p.Protocolo == prot.Protocolo).GroupBy(g => g.Tipo).Select(s => s.First()).ToList();
                var emp = new List<EmpresaTipoEso_1>();
                foreach (var tipo_v in tipo)
                {
                    var oemp = new EmpresaTipoEso_1();
                    oemp.TipoEso = prot.Protocolo;
                    oemp.Nombre = tipo_v.Tipo;
                    oemp.Cantidad = data.FindAll(p => p.Tipo == tipo_v.Tipo && p.Protocolo == prot.Protocolo).Count;
                    oemp.Total = decimal.Round((decimal.Parse(data.FindAll(p => p.Tipo == tipo_v.Tipo && p.Protocolo == prot.Protocolo).Sum(s => s.Total).ToString())), 2);
                    emp.Add(oemp);
                }

                oGerenciaTipoExamen.Empresas = emp;
            }
            list[0].Perfiles = perfiles;

            return list;

        }
    }
}
