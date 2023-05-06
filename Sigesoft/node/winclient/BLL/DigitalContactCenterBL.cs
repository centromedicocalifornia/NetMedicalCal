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
using System.Threading.Tasks;

namespace Sigesoft.Node.WinClient.BLL
{
    public class DigitalContactCenterBL
    {
        PacientBL _PacientBL = new PacientBL();
        public List<DigitalContactCenter> GetDigitalContactCenterFiltered(ref OperationResult pobjOperationResult, DateTime? pdatBeginDate, DateTime? pdatEndDate)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                dbContext.CommandTimeout = 100000;

                var query = (from a in dbContext.getdigitalcontactcenter_sp(pdatBeginDate, pdatEndDate)
                             select new DigitalContactCenter
                             {
                                 ID_DCC = a.ID_DCC,
                                 ID_Person = a.ID_Person,
                                 TIPO_DOC_ID = a.TIPO_DOC_ID,
                                 TIPO_DOC = a.TIPO_DOC,
                                 DOC = a.DOC,
                                 NOMBRES = a.NOMBRES,
                                 AP_PATERNO = a.AP_PATERNO,
                                 AP_MATERNO = a.AP_MATERNO,
                                 CELULAR = a.CELULAR,
                                 EMAIL = a.EMAIL,
                                 DIRECCION = a.DIRECCION,
                                 MEDIO_MKT_ID = a.MEDIO_MKT_ID,
                                 MEDIO_MKT = a.MEDIO_MKT,
                                 FECHA_CITA = a.FECHA_CITA.Value,
                                 PROTOCOL_ID = a.PROTOCOL_ID,
                                 PROTOCOL_NAME = a.PROTOCOL_NAME,
                                 METODO_PAGO_ID = a.METODO_PAGO_ID,
                                 MOTIVO = a.MOTIVO,
                                 METODO_PAGO = a.METODO_PAGO,
                                 ESTADO_DCC_ID = a.ESTADO_DCC_ID.Value,
                                 ESTADO_DCC = a.ESTADO_DCC,
                                 COMPROBANTE_ADJUNTO = a.COMPROBANTE_ADJUNTO,
                                 SERVICIO_ENLAZADO = a.SERVICIO_ENLAZADO == null ? "" : a.SERVICIO_ENLAZADO,
                                 MOTIVO_ELIMINACION = a.MOTIVO_ELIMINACION,
                                 ELIMINADO = a.ELIMINADO.Value,
                                 ID_INSERT_USER = a.ID_INSERT_USER,
                                 INSERT_USER = a.INSERT_USER,
                                 FECHA_INGRESO = a.FECHA_INGRESO,
                                 ID_UPDATE_USER = a.ID_UPDATE_USER,
                                 UPDATE_USER = a.UPDATE_USER,
                                 FECHA_ACTUALIZACION = a.FECHA_ACTUALIZACION ,
                                 FECHA_NACIMIENTO = a.F_NACIMIENTO,
                                 SEXO =  a.SEXO.Value,
                                 SEXO_ = a.SEXO.ToString() == "1" ?"MASC":"FEM",
                                 EDAD = DiferenciaFechas(a.FECHA_INGRESO.Value, a.F_NACIMIENTO.Value),
                                 DCCIdReAgenda = a.DCCIdReAgenda == null ? "" : a.DCCIdReAgenda,
                                 COMENTARIOS = a.COMENTARIOS == null ? "" : a.COMENTARIOS

                                 
                             }).ToList();

                //var result = query.GroupBy(g => g.v_ServiceId).Select(s => s.First()).ToList();
                var bindingList = new List<DigitalContactCenter>(query);
                pobjOperationResult.Success = 1;
                //return bindingList;

                return bindingList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
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


        public string AddDigitalContactCenter(ref OperationResult pobjOperationResult, digitalcontactcenterDto pobjdigitalcontactcenterDto, List<string> ClientSession)
        {
            //mon.IsActive = true;
            int SecuentialId = -1;
            string newId = string.Empty;
            digitalcontactcenter objEntity1 = null;

            try
            {
              
                // Grabar Persona
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                objEntity1 = digitalcontactcenterAssembler.ToEntity(pobjdigitalcontactcenterDto);
                if (pobjdigitalcontactcenterDto.v_ServiceId == "")
                {
                    objEntity1.v_ServiceId = null;
                }
                objEntity1.d_InsertDate = DateTime.Now;
                objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity1.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                SecuentialId = Utils.GetNextSecuentialId(Int32.Parse(ClientSession[0]), 702);
                newId = Common.Utils.GetNewId(int.Parse(ClientSession[0]), SecuentialId, "DC");
                objEntity1.v_DigitalContactCenterId = newId;

                dbContext.AddTodigitalcontactcenter(objEntity1);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "DIGITAL_CONTACT_CENTER", "v_DigitalContactCenterId=" + objEntity1.v_DigitalContactCenterId, Success.Ok, null);
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "DIGITAL_CONTACT_CENTER", "v_DigitalContactCenterId=" + objEntity1.v_DigitalContactCenterId, Success.Failed, ex.Message);
            }

            return newId;
        }


        public string UpdateDigitalContactCenter(ref OperationResult pobjOperationResult, digitalcontactcenterDto pobjdigitalcontactcenter, List<string> ClientSession)
        {

            try
            {
               
              
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
             
                var objEntitySource = (from a in dbContext.digitalcontactcenter
                                       where a.v_DigitalContactCenterId == pobjdigitalcontactcenter.v_DigitalContactCenterId
                                       select a).FirstOrDefault();
                
                if (pobjdigitalcontactcenter.v_ServiceId == "")
                {
                    pobjdigitalcontactcenter.v_ServiceId = null;
                }

                pobjdigitalcontactcenter.d_UpdateDate = DateTime.Now;
                pobjdigitalcontactcenter.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                digitalcontactcenter objEntity = digitalcontactcenterAssembler.ToEntity(pobjdigitalcontactcenter);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.digitalcontactcenter.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "DIGITAL_CONTACT_CENTER", "v_DigitalContactCenterId=" + pobjdigitalcontactcenter.v_DigitalContactCenterId, Success.Ok, null);
                return "1";
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "DIGITAL_CONTACT_CENTER", "v_DigitalContactCenterId=" + pobjdigitalcontactcenter.v_DigitalContactCenterId, Success.Failed, ex.Message);
                return "-1";
            }
        }

        //public List<EsoDto> LlenarPacientesNew()
        //{
        //    using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
        //    {
        //        var query = @"select ISNULL(v_DocNumber, '')  as 'Id' , v_FirstLastName + ' ' + v_SecondLastName + ', ' + v_FirstName as 'Nombre' from person where i_IsDeleted = 0 order by v_DocNumber";

        //        var listaPac = cnx.Query<EsoDto>(query).ToList();

        //        return listaPac;

        //    }

        //}


        public List<EsoDto> GetPacientsPagedAndFiltered_(ref OperationResult pobjOperationResult)
        {
            //mon.IsActive = true;
            try
            {
                
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.person
                   where A.i_IsDeleted == 0

                    select new EsoDto
                    {
                        Id = A.v_DocNumber == null ? "-" : A.v_DocNumber,
                        Nombre = A.v_FirstLastName + " " + A.v_SecondLastName + ", " + A.v_FirstName,
                       
                    });

                List<EsoDto> objData = query.ToList();
                pobjOperationResult.Success = 1;
                return objData;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public digitalcontactcenterDto GetPersonDigContCent(ref OperationResult pobjOperationResult, string iddigitalContact)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                digitalcontactcenterDto objDtoEntity = null;

                var objEntity = (from a in dbContext.digitalcontactcenter
                                 where a.v_DigitalContactCenterId == iddigitalContact && a.i_IsDeleted == 0
                    select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = digitalcontactcenterAssembler.ToDTO(objEntity);

                pobjOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public void DeleteCotizacion(ref OperationResult pobjOperationResult, string v_DigitalContactCenterId, List<string> ClientSession, string motivoEliminacion)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region Service Order
                var objEntitySource = (from a in dbContext.digitalcontactcenter
                                       where a.v_DigitalContactCenterId == v_DigitalContactCenterId
                                       select a).FirstOrDefault();
                objEntitySource.v_MotivoEliminación = motivoEliminacion;
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                dbContext.SaveChanges();

                #endregion

                pobjOperationResult.Success = 1;
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "DIGITAL CONTACT CENTER", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "DIGITAL CONTACT CENTER", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void CancelarAtencion(ref OperationResult pobjOperationResult, string v_ServiceId, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region Service Order
                var objCalendarSource = (from a in dbContext.calendar
                                       where a.v_ServiceId == v_ServiceId
                    select a).FirstOrDefault();
                objCalendarSource.i_LineStatusId = 2;
                objCalendarSource.i_CalendarStatusId = 4;
                objCalendarSource.d_UpdateDate = DateTime.Now;
                objCalendarSource.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                dbContext.SaveChanges();

                var objServiceSource = (from a in dbContext.service
                    where a.v_ServiceId == v_ServiceId
                    select a).FirstOrDefault();
                objServiceSource.i_IsDeleted = 1;
                objServiceSource.d_UpdateDate = DateTime.Now;
                objServiceSource.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                dbContext.SaveChanges();

                #endregion

                pobjOperationResult.Success = 1;
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "CANCELACION DE CALENDAR", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "DIGITAL CONTACT CENTER", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }



    }
}
