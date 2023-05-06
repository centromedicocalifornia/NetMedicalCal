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
    public class GerenciaFarmaciaAsistencialBL
    {
        public List<GerenciaFarmaciaAsistencial> Filter(DateTime startDate, DateTime endDate)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();
                dbContext.CommandTimeout = 100000;
                //9 = particulares
                var query = (from a in dbContext.gerenciafarmaciaasistencial(startDate, endDate, 9)
                             select new GerenciaFarmaciaAsistencial
                             {
                                 Servicio = a.Servicio,
                                 Medicina = a.Medicina,
                                 Cantidad = a.Cantidad,
                                 SubTotal = a.SubTotal,
                                 Igv = a.Igv,
                                 Total = a.Total,
                                 Comision = a.Comision,
                                 Medico = a.Medico,
                                 Protocolo = a.Protocolo,
                                 Trabajador = a.Trabajador,
                                 Paciente = a.Paciente,
                                 FechaReceta = a.Fecha.Value
                             }).ToList();

                var medicos = query.ToList().GroupBy(g => g.Medico).Select(s => s.First()).ToList();

                List<GerenciaFarmaciaAsistencial> farmaciaMedico = new List<GerenciaFarmaciaAsistencial>();
                GerenciaFarmaciaAsistencial oListaMedicinas;
                
                foreach (var item in medicos)
                {
                    decimal totalComision = 0;
                    decimal totalFarm = 0;
                    oListaMedicinas = new GerenciaFarmaciaAsistencial();
                    oListaMedicinas.Medico = item.Medico;

                    var medicinasMedico = query.ToList().FindAll(p => p.Medico == item.Medico).ToList();
                    var listamedicinas = new List<GerenciaFarmaciaAsistencialLista>();
                    foreach (var medicinas in medicinasMedico)
                    {
                        var oMedicoMedicinas = new GerenciaFarmaciaAsistencialLista();
                        oMedicoMedicinas.Paciente = medicinas.Paciente;
                        oMedicoMedicinas.Medicina = medicinas.Medicina;
                        oMedicoMedicinas.Cantidad = medicinas.Cantidad;
                        oMedicoMedicinas.Igv = medicinas.Igv;
                        oMedicoMedicinas.SubTotal = medicinas.SubTotal;
                        oMedicoMedicinas.Total = medicinas.Total;
                        oMedicoMedicinas.Protocolo = medicinas.Protocolo;
                        oMedicoMedicinas.Comision = medicinas.Comision;
                        oMedicoMedicinas.FechaReceta = medicinas.FechaReceta;
                        oMedicoMedicinas.Trabajador = medicinas.Trabajador;
                        oMedicoMedicinas.Servicio = medicinas.Servicio;
                        totalComision += decimal.Parse(medicinas.Comision.ToString());
                        totalFarm += decimal.Parse(medicinas.Total.ToString());
                        listamedicinas.Add(oMedicoMedicinas);
                    }
                    oListaMedicinas.TotalComision = decimal.Round(totalComision, 2).ToString();
                    //oListaMedicinas.TotalMedicinas = decimal.Round(totalFarm, 2).ToString();

                    oListaMedicinas.ListaFarmacia = listamedicinas;

                    farmaciaMedico.Add(oListaMedicinas);
                }

                return farmaciaMedico;
            }
            catch (Exception)
            {
                return null;
            }
        }


        // ----
    }
}
