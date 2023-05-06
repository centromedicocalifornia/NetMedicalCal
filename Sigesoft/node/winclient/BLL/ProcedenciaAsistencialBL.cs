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
    public class ProcedenciaAsistencialBL
    {
        public List<GerenciaProcedenciaAsistencial> Filter(DateTime startDate, DateTime endDate)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();
                dbContext.CommandTimeout = 100000;
                //9 = particulares
                var query = (from a in dbContext.gerenciatipoprotocolo(startDate, endDate, 9, "-")
                             select new GerenciaProcedenciaAsistencial
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

                List<GerenciaProcedenciaAsistencial> _ListaGerenciaProt1 = new List<GerenciaProcedenciaAsistencial>();

                GerenciaProcedenciaAsistencial oListaProtocolo;

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
                                oListaProtocolo = new GerenciaProcedenciaAsistencial();
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
                                oListaProtocolo = new GerenciaProcedenciaAsistencial();
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
                            oListaProtocolo = new GerenciaProcedenciaAsistencial();
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

        public List<GerenciaTreeProcedenciaAsistencial> ProcessDataTreeView(List<GerenciaProcedenciaAsistencial> data)
        {
            var list = Agrupador(data);
            Perfiles(data, list);
            //Perfiles(data, list);
            return list;
        }

        public List<GerenciaTreeProcedenciaAsistencial> Agrupador(List<GerenciaProcedenciaAsistencial> data)
        {
            var list = new List<GerenciaTreeProcedenciaAsistencial>();
            var oGerenciaTreeTipoExamen = new GerenciaTreeProcedenciaAsistencial();
            oGerenciaTreeTipoExamen.Cantidad = data.Count;
            oGerenciaTreeTipoExamen.Agrupador = "PARTICULARES";
            oGerenciaTreeTipoExamen.TotalVenta = decimal.Round((decimal.Parse(data.Sum(s => s.Total).ToString())), 2);
            oGerenciaTreeTipoExamen.TotalProduccion = decimal.Round((decimal.Parse(data.Sum(s => s.Produccion).ToString())), 2);
            list.Add(oGerenciaTreeTipoExamen);

            return list;
        }

        public List<GerenciaTreeProcedenciaAsistencial> Perfiles(List<GerenciaProcedenciaAsistencial> data, List<GerenciaTreeProcedenciaAsistencial> list)
        {
            var perfiles = new List<PerfilProcedencia>();
            var procedencia = data.GroupBy(g => g.Procedencia).Select(s => s.First()).ToList();
            foreach (var prot in procedencia)
            {
                var oGerenciaTipoExamen = new PerfilProcedencia();
                oGerenciaTipoExamen.TipoEso = prot.Procedencia;
                oGerenciaTipoExamen.Cantidad = data.FindAll(p => p.Procedencia == prot.Procedencia).Count;
                oGerenciaTipoExamen.TotalVenta = decimal.Round((decimal.Parse(data.FindAll(p => p.Procedencia == prot.Procedencia).Sum(s => s.Total).ToString())), 2);
                oGerenciaTipoExamen.TotalProduccionn = decimal.Round((decimal.Parse(data.FindAll(p => p.Procedencia == prot.Procedencia).Sum(s => s.Produccion).ToString())), 2);
                perfiles.Add(oGerenciaTipoExamen);


                var tipo = data.FindAll(p => p.Procedencia == prot.Procedencia).GroupBy(g => g.Tipo).Select(s => s.First()).ToList();
                var emp = new List<TipoPago>();
                foreach (var tipo_v in tipo)
                {
                    var oemp = new TipoPago();
                    oemp.TipoEso = prot.Procedencia == "A"?"AMBULATORIO":prot.Procedencia == "H"?"HOSPITALARIO" : prot.Procedencia == "E"?"EMERGENCIA" : prot.Procedencia;
                    oemp.Nombre = tipo_v.Tipo;
                    oemp.Cantidad = data.FindAll(p => p.Tipo == tipo_v.Tipo && p.Procedencia == prot.Procedencia).Count;
                    oemp.Total = decimal.Round((decimal.Parse(data.FindAll(p => p.Tipo == tipo_v.Tipo && p.Procedencia == prot.Procedencia).Sum(s => s.Total).ToString())), 2);
                    emp.Add(oemp);
                }

                oGerenciaTipoExamen.Empresas = emp;
            }
            list[0].Perfiles = perfiles;

            return list;

        }
    }
}
