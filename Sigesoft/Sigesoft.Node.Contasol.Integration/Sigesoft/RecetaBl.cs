using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sigesoft.Common;
using Sigesoft.Node.Contasol.Integration.Contasol;
using Sigesoft.Node.Contasol.Integration.Contasol.Models;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.Contasol.Integration
{
    public class RecetaBl
    {
        public RecetaBl()
        {

        }

        public List<DiagnosticRepositoryList> GetHierarchycalData(ref OperationResult pobjOperationResult,
            List<DiagnosticRepositoryList> dataToIterate)
        {
            try
            {
                using (var dbContext = new SigesoftEntitiesModel())
                {
                    var diagnosticIds = dataToIterate.Select(p => p.v_DiagnosticRepositoryId).Distinct();
                    var medicamentos = MedicamentoDao.ObtenerContasolMedicamentos();
                    var recetas = (from n in dbContext.receta.Where(p => diagnosticIds.Contains(p.v_DiagnosticRepositoryId)).ToList()
                                   join m in medicamentos on n.v_IdProductoDetalle equals m.IdProductoDetalle into mJoin
                                   from m in mJoin.DefaultIfEmpty()
                                   where n.i_IsDeleted == 0
                                   select new recetaDto
                                   {
                                       v_IdProductoDetalle = n.v_IdProductoDetalle,
                                       NombreMedicamento = m.NombreCompleto,
                                       d_Cantidad = n.d_Cantidad,
                                       i_IdReceta = n.i_IdReceta,
                                       t_FechaFin = n.t_FechaFin,
                                       v_DiagnosticRepositoryId = n.v_DiagnosticRepositoryId,
                                       v_Duracion = n.v_Duracion,
                                       v_Posologia = n.v_Posologia
                                   }).ToList();

                    if (recetas.Any())
                    {
                        var agrupado = recetas.GroupBy(g => g.v_DiagnosticRepositoryId);
                        foreach (var grupo in agrupado)
                        {
                            var parent = dataToIterate.FirstOrDefault(p => p.v_DiagnosticRepositoryId.Equals(grupo.Key));
                            if (parent != null)
                            {
                                parent.RecipeDetail.AddRange(grupo);
                            }
                        }
                    }

                    pobjOperationResult.Success = 1;
                    return dataToIterate;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaBl.AddUpdateRecipe()";
                return null;
            }
        }

        public void AddUpdateRecipe(ref OperationResult pobjOperationResult, recetaDto pobjDto, int _usuarioIngresa)
        {
            try
            {
                using (var dbContext = new SigesoftEntitiesModel())
                {
                    var entidad = dbContext.receta.FirstOrDefault(p => p.i_IdReceta == pobjDto.i_IdReceta);
                    if (entidad == null)
                    {
                        pobjDto.d_InsertDate = DateTime.Now;
                        pobjDto.i_InsertUserId = _usuarioIngresa;
                        dbContext.receta.AddObject(pobjDto.ToEntity());
                    }
                    else
                    {
                        pobjDto.i_UpdateUserId = _usuarioIngresa;
                        pobjDto.d_UpdateDate = DateTime.Now;
                        entidad = pobjDto.ToEntity();
                        dbContext.receta.ApplyCurrentValues(entidad);
                    }
                    dbContext.SaveChanges();
                    pobjOperationResult.Success = 1;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaBl.AddUpdateRecipe()";
            }
        }

        public void DeleteRecipe(ref OperationResult pobjOperationResult, int recipeId)
        {
            try
            {
                using (var dbContext = new SigesoftEntitiesModel())
                {
                    var entidad = dbContext.receta.FirstOrDefault(p => p.i_IdReceta == recipeId);
                    if (entidad == null) throw new Exception("El objeto ya no existe!");
                    dbContext.receta.DeleteObject(entidad);
                    dbContext.SaveChanges();
                    pobjOperationResult.Success = 1;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaBl.DeleteRecipe()";
            }
        }

        public recetaDto GetRecipeById(ref OperationResult pobjOperationResult, int recipeId)
        {
            try
            {
                using (var dbContext = new SigesoftEntitiesModel())
                {
                    var medicamentos = MedicamentoDao.ObtenerContasolMedicamentos();

                    var entidad = (from r in dbContext.receta.Where(p => p.i_IdReceta == recipeId).ToList()
                                   join m in medicamentos on r.v_IdProductoDetalle equals m.IdProductoDetalle into mJoin
                                   from m in mJoin.DefaultIfEmpty()
                                   where r.i_IdReceta == recipeId
                                   select new recetaDto
                                   {
                                       v_IdProductoDetalle = r.v_IdProductoDetalle,
                                       i_IdReceta = r.i_IdReceta,
                                       NombreMedicamento = m.NombreCompleto,
                                       d_Cantidad = r.d_Cantidad,
                                       t_FechaFin = r.t_FechaFin,
                                       v_DiagnosticRepositoryId = r.v_DiagnosticRepositoryId,
                                       v_Duracion = r.v_Duracion,
                                       v_Posologia = r.v_Posologia,
                                       v_IdUnidadProductiva = r.v_IdUnidadProductiva,
                                       d_SaldoAseguradora = r.d_SaldoAseguradora,
                                       d_SaldoPaciente = r.d_SaldoPaciente,
                                       d_InsertDate = r.d_InsertDate,
                                       d_UpdateDate = r.d_UpdateDate
                                   }).FirstOrDefault();

                    if (entidad == null) throw new Exception("El objeto ya no existe!");
                    pobjOperationResult.Success = 1;
                    return entidad;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaBl.DeleteRecipe()";
                return null;
            }
        }

        public List<recetadespachoDto> GetRecetaToReport_old(ref OperationResult pobjOperationResult, string serviceId, string v_DiagnosticRepositoryId)
        {
            try
            {
                using (var dbContext = new SigesoftEntitiesModel())
                {
                    pobjOperationResult.Success = 1;
                    var medicamentos = MedicamentoDao.ObtenerContasolMedicamentos();
                    var FirmaMedicoMedicina = new ServiceBL().ObtenerFirmaMedicoExamen(serviceId, Constants.ATENCION_INTEGRAL_ID, Constants.EXAMEN_FISICO_7C_ID);

                    var consulta = (from r in dbContext.receta
                                    join d in dbContext.diagnosticrepository on r.v_DiagnosticRepositoryId equals d.v_DiagnosticRepositoryId into dJoin
                                    from d in dJoin.DefaultIfEmpty()
                                    join s in dbContext.service on d.v_ServiceId equals s.v_ServiceId into sJoin
                                    from s in sJoin.DefaultIfEmpty()
                                    join C in dbContext.organization on new { id = "N009-OO000000052" } equals new { id = C.v_OrganizationId } into C_join
                                    from C in C_join.DefaultIfEmpty()
                                    join p in dbContext.person on s.v_PersonId equals p.v_PersonId into pJoin
                                    from p in pJoin.DefaultIfEmpty()
                                    where s.v_ServiceId.Equals(serviceId)
                                    
                                    select new recetadespachoDto
                                    {
                                        RecetaId = r.i_IdReceta,
                                        CantidadRecetada = r.d_Cantidad ?? 0,
                                        //Medicamento = m.NombreCompleto,
                                        //Presentacion = m.Presentacion,
                                        //Ubicacion = m.Ubicacion,
                                        NombrePaciente = p.v_FirstLastName + " " + p.v_SecondLastName + " " + p.v_FirstName,
                                        //FechaFin = r.t_FechaFin ?? DateTime.Now,
                                        Duracion = r.v_Duracion,
                                        Dosis = r.v_Posologia,
                                        NombreMedico = FirmaMedicoMedicina.Value2,
                                        RubricaMedico = FirmaMedicoMedicina.Value5,
                                        MedicoNroCmp = FirmaMedicoMedicina.Value3,
                                        NombreClinica = C.v_Name,
                                        DireccionClinica = C.v_Address,
                                        LogoClinica = C.b_Image,
                                        Despacho = (r.i_Lleva ?? 0) == 1,
                                        MedicinaId = r.v_IdProductoDetalle,
                                        FechaNacimiento = p.d_Birthdate.Value
                                        //,
                                        //Edad = GetAge(p.d_Birthdate.Value)
                                    }).ToList();

                    //consulta = consulta.GroupBy(p => p.RecetaId).Select(p => p.FirstOrDefault()).ToList();
                    foreach (var item in consulta)
                    {
                        var Edad = GetAge(item.FechaNacimiento);
                        var prod = medicamentos.FirstOrDefault(p => p.IdProductoDetalle.Equals(item.MedicinaId));
                        if (prod == null) continue;
                        item.Medicamento = prod.NombreCompleto;
                        item.Presentacion = prod.Presentacion;
                        item.Ubicacion = prod.Ubicacion;
                        item.NombrePaciente = item.NombrePaciente;
                        //item.Edad = Int32.Parse(Edad);
                    }

                    return consulta;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaDespachoBl.GetDespacho()";
                return null;
            }
        }

        public List<recetadespachoDto> GetRecetaToReport(ref OperationResult pobjOperationResult, string serviceId, string v_DiagnosticRepositoryId)
        {
            try
            {
                using (var dbContext = new SigesoftEntitiesModel())
                {
                    pobjOperationResult.Success = 1;
                    //var medicamentos = MedicamentoDao.ObtenerContasolMedicamentos();
                    var FirmaMedicoMedicina = new ServiceBL().ObtenerFirmaMedicoExamen(serviceId, Constants.ATENCION_INTEGRAL_ID, Constants.EXAMEN_FISICO_7C_ID);
                    //var añosCompleto = DiferenciaFechas(DateTime.Now, datosP.d_Birthdate.Value);
                    //List<string> listsServicios = new List<string>();
                    //listsServicios.Add(serviceId);
                    var CampoValores = DevolverValorCampoPorServicioMejoradoNewUnServicio(serviceId);
                    var consulta = (from a in dbContext.getrecetatoreport_sp(serviceId, v_DiagnosticRepositoryId)
                                    select new recetadespachoDto
                                    {
                                        RecetaId = a.RecetaId,
                                        CantidadRecetada = a.CantidadRecetada,
                                        NombrePaciente = a.NombrePaciente,
                                        FechaFin = a.FechaFin.ToShortDateString(),
                                        Duracion = a.Duracion,
                                        Dosis = a.Dosis,
                                        NombreMedico = FirmaMedicoMedicina.Value2,
                                        RubricaMedico = FirmaMedicoMedicina.Value5,
                                        MedicoNroCmp = FirmaMedicoMedicina.Value3,
                                        //NombreClinica = a.NombreClinica,
                                        NombreClinica = CampoValores.Find(o => o.IdCampo == "N009-MF000003236") == null ? " " : CampoValores.Find(o => o.IdCampo == "N009-MF000003236").Valor,

                                        DireccionClinica = a.DireccionClinica,
                                        LogoClinica = a.LogoClinica,
                                        Despacho = a.Despacho == 1?true:false,
                                        MedicinaId = a.MedicinaId,
                                        FechaNacimiento = a.FechaNacimiento.Value,
                                        USUARIO = a.USUARIO,
                                        ATENCION = a.ATENCION,
                                        ESPECIALIDAD = a.ESPECIALIDAD,
                                        CAMA = a.CAMA ,
                                        Dx = a.Dx ,
                                        Cie10 = a.Cie10 ,
                                        FechaAtencion = a.FechaAtencion.Value.ToShortDateString(),
                                        Edad = DiferenciaFechas(a.FechaAtencion.Value, a.FechaNacimiento.Value),
                                        Medicamento = a.Medicamento,
                                        Presentacion = a.Presentacion,
                                        Ubicacion = a.Ubicacion,
                                        //FechaFin = FechaFin
                                    }).ToList();

                    //consulta = consulta.GroupBy(p => p.RecetaId).Select(p => p.FirstOrDefault()).ToList();
                    //foreach (var item in consulta)
                    //{
                    //    var prod = medicamentos.FirstOrDefault(p => p.IdProductoDetalle.Equals(item.MedicinaId));
                    //    if (prod == null) continue;
                    //    if (item.USUARIO != "PARTICULAR")
                    //    {
                    //        if (true)
                    //        {
                                
                    //        }
                    //        item.Medicamento = prod.NombreCompleto + "(" + prod.NombreGenerico + ")";
                    //    }
                    //    else
                    //    {
                    //        item.Medicamento = prod.NombreCompleto;
                    //    }
                    //    item.Presentacion = prod.Presentacion;
                    //    item.Ubicacion = prod.Ubicacion;
                    //    item.FechaFin = item.FechaFin;
                    //}

                    return consulta;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaDespachoBl.GetDespacho()";
                return null;
            }
        }

        public List<recetadespachoDto> GetRecetaToReportConsolidado(ref OperationResult pobjOperationResult, string serviceId)
        {
            try
            {
                using (var dbContext = new SigesoftEntitiesModel())
                {
                    pobjOperationResult.Success = 1;
                    //var medicamentos = MedicamentoDao.ObtenerContasolMedicamentos();
                    var FirmaMedicoMedicina = new ServiceBL().ObtenerFirmaMedicoExamen(serviceId, Constants.ATENCION_INTEGRAL_ID, Constants.EXAMEN_FISICO_7C_ID);
                    //var añosCompleto = DiferenciaFechas(DateTime.Now, datosP.d_Birthdate.Value);
                    //List<string> listsServicios = new List<string>();
                    //listsServicios.Add(serviceId);
                    var CampoValores = DevolverValorCampoPorServicioMejoradoNewUnServicio(serviceId);
                    var consulta = (from a in dbContext.getrecetatoreportconsolidado_sp(serviceId)
                                    select new recetadespachoDto
                                    {
                                        RecetaId = a.RecetaId,
                                        CantidadRecetada = a.CantidadRecetada,
                                        NombrePaciente = a.NombrePaciente,
                                        FechaFin = a.FechaFin.ToShortDateString(),
                                        Duracion = a.Duracion,
                                        Dosis = a.Dosis,
                                        NombreMedico = FirmaMedicoMedicina.Value2,
                                        RubricaMedico = FirmaMedicoMedicina.Value5,
                                        MedicoNroCmp = FirmaMedicoMedicina.Value3,
                                        //NombreClinica = a.NombreClinica,
                                        NombreClinica = CampoValores.Find(o => o.IdCampo == "N009-MF000003236") == null ? " " : CampoValores.Find(o => o.IdCampo == "N009-MF000003236").Valor,

                                        DireccionClinica = a.DireccionClinica,
                                        LogoClinica = a.LogoClinica,
                                        Despacho = a.Despacho == 1 ? true : false,
                                        MedicinaId = a.MedicinaId,
                                        FechaNacimiento = a.FechaNacimiento.Value,
                                        USUARIO = a.USUARIO,
                                        ATENCION = a.ATENCION,
                                        ESPECIALIDAD = a.ESPECIALIDAD,
                                        CAMA = a.CAMA,
                                        Dx = a.Dx,
                                        Cie10 = a.Cie10,
                                        FechaAtencion = a.FechaAtencion.Value.ToShortDateString(),
                                        Edad = DiferenciaFechas(a.FechaAtencion.Value, a.FechaNacimiento.Value),
                                        Medicamento = a.Medicamento,
                                        Presentacion = a.Presentacion,
                                        Ubicacion = a.Ubicacion,
                                    }).ToList();

                    //consulta = consulta.GroupBy(p => p.RecetaId).Select(p => p.FirstOrDefault()).ToList();
                    List<string> cie10 = new List<string>();

                    foreach (var item in consulta)
                    {
                        cie10.Add(item.Cie10);
                    }

                    List<string> cie10New = cie10.Distinct().ToList();

                    string concat = string.Join(" / ", cie10New);
                    string Dx = "";
                    if (cie10New.Count() > 1)
                    {
                        Dx = "";
                    }
                   
                    foreach (var item in consulta)
                    {

                        //var prod = medicamentos.FirstOrDefault(p => p.IdProductoDetalle.Equals(item.MedicinaId));
                        //if (prod == null) continue;

                        item.Cie10 = concat;
                        item.Dx = Dx;
                        //if (item.USUARIO != "PARTICULAR")
                        //{
                        //    item.Medicamento = prod.NombreCompleto + "(" + prod.NombreGenerico + ")";
                        //}
                        //else
                        //{
                        //    item.Medicamento = prod.NombreCompleto;
                        //}
                        //item.Presentacion = prod.Presentacion;
                        //item.Ubicacion = prod.Ubicacion;
                        //item.FechaFin = item.FechaFin;
                    }

                    return consulta;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaDespachoBl.GetDespacho()";
                return null;
            }
        }


        public List<JerarquiaServicioCamposValores> DevolverValorCampoPorServicioMejoradoNew(List<string> ListaServicioIds)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            string Servicios = "";
            ListaServicioIds = ListaServicioIds.OrderBy(p => p).ToList();
            foreach (var item in ListaServicioIds)
            {
                Servicios += item + ",";
            }
            try
            {
                int rpta = 0;
                var finalQuery = (from a in dbContext.devolvervalorcampoporserviciomejorado_sp(Servicios)
                                  select new ValorComponenteList
                                  {
                                      ServicioId = a.ServicioId,
                                      Valor = a.Valor,
                                      NombreComponente = a.NombreComponente,
                                      IdComponente = a.IdComponente,
                                      NombreCampo = a.NombreCampo,
                                      IdCampo = a.IdCampo,
                                      i_GroupId = a.i_GroupId.Value,
                                      ValorName = a.ValorName == null ? "" : a.ValorName
                                  }).ToList();

                var ListaJerarquizada = (from A in dbContext.service
                                         where ListaServicioIds.Contains(A.v_ServiceId)
                                         select new JerarquiaServicioCamposValores
                                         {
                                             ServicioId = A.v_ServiceId
                                         }).ToList();

                ListaJerarquizada.ForEach(a =>
                {
                    a.CampoValores = finalQuery.FindAll(p => p.ServicioId == a.ServicioId);
                });


                return ListaJerarquizada;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<ValorComponenteList> DevolverValorCampoPorServicioMejoradoNewUnServicio(string servicioId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
           
            try
            {
                int rpta = 0;
                var finalQuery = (from a in dbContext.devolvervalorcampoporserviciomejoradounservicio_sp(servicioId)
                                  select new ValorComponenteList
                                  {
                                      ServicioId = a.ServicioId,
                                      Valor = a.Valor,
                                      NombreComponente = a.NombreComponente,
                                      IdComponente = a.IdComponente,
                                      NombreCampo = a.NombreCampo,
                                      IdCampo = a.IdCampo,
                                      i_GroupId = a.i_GroupId.Value,
                                      ValorName = a.ValorName == null ? "" : a.ValorName
                                  }).ToList();

                //var ListaJerarquizada = (from A in dbContext.service
                //                         where A.v_ServiceId == servicioId
                //                         select new JerarquiaServicioCamposValores
                //                         {
                //                             ServicioId = A.v_ServiceId
                //                         }).ToList();

                //ListaJerarquizada.ForEach(a =>
                //{
                //    a.CampoValores = finalQuery.FindAll(p => p.ServicioId == a.ServicioId);
                //});


                return finalQuery;
            }
            catch (Exception)
            {

                throw;
            }

        }

        private string DiferenciaFechas(DateTime newdt, DateTime olddt)
        {
            int anios;
            int meses;
            int dias;
            string str = "";

            anios = (newdt.Year - olddt.Year);
            meses = (newdt.Month - olddt.Month);
            dias = (newdt.Day - olddt.Day);

            if (meses < 0)
            {
                anios -= 1;
                meses += 12;
            }
            if (dias < 0)
            {
                meses -= 1;
                dias += DateTime.DaysInMonth(newdt.Year, newdt.Month);
            }

            if (anios < 0)
            {
                return "La fecha inicial es mayor a la fecha final";
            }
            if (anios > 0)
            {
                if (anios == 1)
                    str = str + anios.ToString() + " año ";
                else
                    str = str + anios.ToString() + " años ";
            }
            if (meses > 0)
            {
                if (meses == 1)
                    str = str + meses.ToString() + " mes y ";
                else
                    str = str + meses.ToString() + " meses y ";
            }
            if (dias > 0)
            {
                if (dias == 1)
                    str = str + dias.ToString() + " día ";
                else
                    str = str + dias.ToString() + " días ";
            }
            return str;
        }


        public int GetAge(DateTime FechaNacimiento)
        {
            return (DateTime.Today.AddTicks(-FechaNacimiento.Ticks).Year - 1);

        }

        List<DiagnosticRepositoryList> dataToIterate;

        public List<recetadespachoDto> GetReceta(string serviceId)
        {
             using (var dbContext = new SigesoftEntitiesModel())
             {
                 DateTime fechaactual = DateTime.Now;
                 string fechaactual_ = fechaactual.ToShortDateString();
                    var medicamentos = MedicamentoDao.ObtenerContasolMedicamentos();
                    var consulta = (from r in dbContext.receta
                                    join d in dbContext.diagnosticrepository on r.v_DiagnosticRepositoryId equals d.v_DiagnosticRepositoryId into dJoin
                                    from d in dJoin.DefaultIfEmpty()
                                    join s in dbContext.service on d.v_ServiceId equals s.v_ServiceId into sJoin
                                    from s in sJoin.DefaultIfEmpty()
                                    
                                    where s.v_ServiceId.Equals(serviceId) && r.i_IsDeleted == 0
                                   
                                    select new recetadespachoDto
                                    {
                                        RecetaId = r.i_IdReceta,
                                        CantidadRecetada = r.d_Cantidad ?? 0,
                                        FechaFin_ = r.t_FechaFin ?? DateTime.Now,
                                        Duracion = r.v_Duracion,
                                        Dosis = r.v_Posologia,
                                        
                                        Despacho = (r.i_Lleva ?? 0) == 1,
                                        MedicinaId = r.v_IdProductoDetalle
                                    }).ToList();

                    foreach (var item in consulta)
                    {
                        var prod = medicamentos.FirstOrDefault(p => p.IdProductoDetalle.Equals(item.MedicinaId));
                        if (prod == null) continue;
                        item.Medicamento = prod.NombreCompleto;
                        item.Presentacion = prod.Presentacion;
                        item.Ubicacion = prod.Ubicacion;
                        item.FechaFin = item.FechaFin_.ToShortDateString();
                    }

                    return consulta;
                }
           
        }

        public void UpdateDespacho(ref OperationResult pobjOperationResult, List<recetadespachoDto> data)
        {
            try
            {
                using (var dbContext = new SigesoftEntitiesModel())
                {
                    foreach (var item in data)
                    {
                        var e = dbContext.receta.FirstOrDefault(p => p.i_IdReceta == item.RecetaId);
                        if (e != null)
                        {
                            e.i_Lleva = item.Despacho ? 1 : 0;
                            dbContext.receta.ApplyCurrentValues(e);
                        }
                    }

                    dbContext.SaveChanges();
                    pobjOperationResult.Success = 1;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaDespachoBl.UpdateDespacho()";
            }
        }

    }
}
