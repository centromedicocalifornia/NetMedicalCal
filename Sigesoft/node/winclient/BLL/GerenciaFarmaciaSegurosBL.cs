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
    public class GerenciaFarmaciaSegurosBL
    {
        public List<GerenciaFarmaciaSeguros> Filter(DateTime startDate, DateTime endDate)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();
                dbContext.CommandTimeout = 100000;
                //9 = particulares
                var query = (from a in dbContext.gerenciafarmaciaseguros(startDate, endDate, 11)
                             select new GerenciaFarmaciaSeguros
                             {
                                 Servicio = a.Servicio,
                                 Medicina = a.Medicina,
                                 Cantidad = a.Cantidad,
                                 SubTotal = a.SubTotal,
                                 Igv = a.Igv,
                                 Total = a.Total,
                                 Comision = a.Comision,
                                 SaldoPaciente = decimal.Parse(a.SaldoPaciente.ToString()),
                                 SaldoAseguradora = decimal.Parse(a.SaldoAseguradora.ToString()),
                                 Medico = a.Medico,
                                 Protocolo = a.Protocolo,
                                 Trabajador = a.Trabajador,
                                 Paciente = a.Paciente,
                                 FechaReceta = a.Fecha.Value,
                                 Plan = a.Plan,
                                 Factor = decimal.Parse(a.Factor.ToString()),
                                 Descuento_PPS = a.Descuento_PPS,
                                 Deducible = a.Deducible,
                                 Coaseguro = a.Coaseguro
                             }).ToList();

                var medicos = query.ToList().GroupBy(g => g.Medico).Select(s => s.First()).ToList();

                List<GerenciaFarmaciaSeguros> farmaciaMedico = new List<GerenciaFarmaciaSeguros>();
                GerenciaFarmaciaSeguros oListaMedicinas;

                foreach (var item in medicos)
                {
                    decimal total = 0;
                    oListaMedicinas = new GerenciaFarmaciaSeguros();
                    oListaMedicinas.Medico = item.Medico;

                    var medicinasMedico = query.ToList().FindAll(p => p.Medico == item.Medico).ToList();
                    var listamedicinas = new List<GerenciaFarmaciaSegurosLista>();
                    foreach (var medicinas in medicinasMedico)
                    {
                        var oMedicoMedicinas = new GerenciaFarmaciaSegurosLista();
                        oMedicoMedicinas.Paciente = medicinas.Paciente;
                        oMedicoMedicinas.Medicina = medicinas.Medicina;
                        oMedicoMedicinas.Cantidad = medicinas.Cantidad;
                        oMedicoMedicinas.Igv = medicinas.Igv;
                        oMedicoMedicinas.SubTotal = medicinas.SubTotal;
                        oMedicoMedicinas.Total = medicinas.Total;
                        oMedicoMedicinas.SaldoPaciente = medicinas.SaldoPaciente;
                        oMedicoMedicinas.SaldoAseguradora = medicinas.SaldoAseguradora;
                        oMedicoMedicinas.Protocolo = medicinas.Protocolo;
                        oMedicoMedicinas.Comision = medicinas.Comision;
                        oMedicoMedicinas.FechaReceta = medicinas.FechaReceta;
                        oMedicoMedicinas.Trabajador = medicinas.Trabajador;
                        oMedicoMedicinas.Servicio = medicinas.Servicio;
                        oMedicoMedicinas.Plan = medicinas.Plan;
                        oMedicoMedicinas.Factor = medicinas.Factor;
                        oMedicoMedicinas.Descuento_PPS = medicinas.Descuento_PPS;
                        oMedicoMedicinas.Deducible = medicinas.Deducible;
                        oMedicoMedicinas.Coaseguro = medicinas.Coaseguro;
                        total += decimal.Parse(medicinas.Comision.ToString());
                        listamedicinas.Add(oMedicoMedicinas);
                    }
                    oListaMedicinas.TotalComision = decimal.Round(total, 2).ToString();

                    oListaMedicinas.ListaFarmaciaSeguros = listamedicinas;

                    farmaciaMedico.Add(oListaMedicinas);
                }

                return farmaciaMedico;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
