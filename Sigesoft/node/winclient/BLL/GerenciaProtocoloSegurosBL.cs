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
    public class GerenciaProtocoloSegurosBL
    {
        public List<GerenciaProtocoloSeguros> Filter(DateTime startDate, DateTime endDate)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();
                dbContext.CommandTimeout = 100000;
                //9 = particulares
                var query = (from a in dbContext.gerenciatipoprotocoloseguros(startDate, endDate, 11)
                             select new GerenciaProtocoloSeguros
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
                                 Plan = a.Plan,
                                 Factor = decimal.Round(decimal.Parse(a.Factor.ToString()),2),
                                 Descuento_PPS = a.Descuento_PPS,
                                 Deducible = a.Deducible,
                                 Coaseguro = a.Coaseguro,
                                 Value1 = a.Value1,
                                 Value2 = a.Value2

                             }).ToList();

                var lista1 = query.ToList().GroupBy(g => g.DOC).Select(s => s.First()).ToList();

                List<GerenciaProtocoloSeguros> _ListaGerenciaProt1 = new List<GerenciaProtocoloSeguros>();

                GerenciaProtocoloSeguros oListaProtocolo;

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
                                oListaProtocolo = new GerenciaProtocoloSeguros();
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
                                oListaProtocolo.Plan = item.Plan;
                                oListaProtocolo.Factor = decimal.Round(decimal.Parse(item.Factor.ToString()), 2);
                                oListaProtocolo.Descuento_PPS = item.Descuento_PPS;
                                oListaProtocolo.Deducible = item.Deducible;
                                oListaProtocolo.Coaseguro = item.Coaseguro;
                                oListaProtocolo.Value1 = item.Value1;
                                oListaProtocolo.Value2 = item.Value2;
                                _ListaGerenciaProt1.Add(oListaProtocolo);
                            }
                            else
                            {
                                oListaProtocolo = new GerenciaProtocoloSeguros();
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
                                oListaProtocolo.Plan = item.Plan;
                                oListaProtocolo.Factor = decimal.Round(decimal.Parse(item.Factor.ToString()), 2);
                                oListaProtocolo.Descuento_PPS = item.Descuento_PPS;
                                oListaProtocolo.Deducible = item.Deducible;
                                oListaProtocolo.Coaseguro = item.Coaseguro;
                                oListaProtocolo.Value1 = item.Value1;
                                oListaProtocolo.Value2 = item.Value2;
                                _ListaGerenciaProt1.Add(oListaProtocolo);
                            }
                            count++;
                        }
                    }
                    else
                    {
                        foreach (var item in medicinasMedico)
                        {
                            oListaProtocolo = new GerenciaProtocoloSeguros();
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
                            oListaProtocolo.Plan = item.Plan;
                            oListaProtocolo.Factor = decimal.Round(decimal.Parse(item.Factor.ToString()), 2);
                            oListaProtocolo.Descuento_PPS = item.Descuento_PPS;
                            oListaProtocolo.Deducible = item.Deducible;
                            oListaProtocolo.Coaseguro = item.Coaseguro;
                            oListaProtocolo.Value1 = item.Value1;
                            oListaProtocolo.Value2 = item.Value2;
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

        public List<GerenciaTreeTipoProtocoloSeguros> ProcessDataTreeView(List<GerenciaProtocoloSeguros> data)
        {
            var list = Agrupador(data);
            Perfiles(data, list);
            //Perfiles(data, list);
            return list;
        }

        public List<GerenciaTreeTipoProtocoloSeguros> Agrupador(List<GerenciaProtocoloSeguros> data)
        {
            var list = new List<GerenciaTreeTipoProtocoloSeguros>();
            var oGerenciaTreeTipoExamen = new GerenciaTreeTipoProtocoloSeguros();
            oGerenciaTreeTipoExamen.Cantidad = data.Count;
            oGerenciaTreeTipoExamen.Agrupador = "SEGUROS";
            oGerenciaTreeTipoExamen.Total = decimal.Round((decimal.Parse(data.Sum(s => s.Total).ToString())), 2);
            list.Add(oGerenciaTreeTipoExamen);

            return list;
        }

        public List<GerenciaTreeTipoProtocoloSeguros> Perfiles(List<GerenciaProtocoloSeguros> data, List<GerenciaTreeTipoProtocoloSeguros> list)
        {
            var perfiles = new List<ProtocoloSeguros_>();
            var protocolo = data.GroupBy(g => g.Protocolo).Select(s => s.First()).ToList();
            foreach (var prot in protocolo)
            {
                var oGerenciaTipoExamen = new Sigesoft.Node.WinClient.BE.Custom.ProtocoloSeguros_();
                oGerenciaTipoExamen.TipoEso = prot.Protocolo;
                oGerenciaTipoExamen.Cantidad = data.FindAll(p => p.Protocolo == prot.Protocolo).Count;
                oGerenciaTipoExamen.Total = decimal.Round((decimal.Parse(data.FindAll(p => p.Protocolo == prot.Protocolo).Sum(s => s.Total).ToString())), 2);
                perfiles.Add(oGerenciaTipoExamen);


                var tipo = data.FindAll(p => p.Protocolo == prot.Protocolo).GroupBy(g => g.Tipo).Select(s => s.First()).ToList();
                var emp = new List<Tipo_Perfil_Seguro>();
                foreach (var tipo_v in tipo)
                {
                    var oemp = new Tipo_Perfil_Seguro();
                    oemp.TipoEso = prot.Protocolo;
                    oemp.Nombre = tipo_v.Tipo;
                    oemp.Cantidad = data.FindAll(p => p.Tipo == tipo_v.Tipo && p.Protocolo == prot.Protocolo).Count;
                    oemp.Total = decimal.Round((decimal.Parse(data.FindAll(p => p.Tipo == tipo_v.Tipo && p.Protocolo == prot.Protocolo).Sum(s => s.Total).ToString())), 2);
                    emp.Add(oemp);
                }

                oGerenciaTipoExamen.Tipo_Perfil_Seguro = emp;
            }
            list[0].Perfiles = perfiles;

            return list;

        }
    }
}
