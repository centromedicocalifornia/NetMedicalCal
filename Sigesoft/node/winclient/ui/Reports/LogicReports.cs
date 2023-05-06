using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using NetPdf;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Win.UltraWinSchedule;
using System.Threading.Tasks;
using Microsoft.Vbe.Interop;
using Application = System.Windows.Forms.Application;
using System.Data.SqlClient;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Node.Contasol.Integration;

namespace Sigesoft.Node.WinClient.UI.Reports
{
    public class LogicReports
    {
        ServiceBL _serviceBL = new ServiceBL();
        private string _customerOrganizationName;
        OrganizationBL _organizationBL = new OrganizationBL();
        OperationResult _objOperationResult = new OperationResult();
        private MergeExPDF _mergeExPDF = new MergeExPDF();
        public List<string> _filesNameToMerge = new List<string>();
        private string _serviceId;
        private string _pacientId;
        private string _ruta = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();
        private string _tempSourcePath = Path.Combine(Application.StartupPath, "TempMerge");
        DataSet dsGetRepo = null;
        DataSet dsGetRepo1 = null;
        PacientBL _pacientBL = new PacientBL();
        HistoryBL _historyBL = new HistoryBL();
        private string _dni;
        private string _pacientName;
        AtencionIntegralBL atencionIntegralBL = new AtencionIntegralBL();

        private static List<Task<string>> tasks = new List<Task<string>>();

        private readonly int _edad;
        RecetaBl objRecetaBl = new RecetaBl();


        private int GetIdCrystal(string com)
        {
            int IdCrystal = 0;
            var array = com.Split('|');

            if (array.Count() == 1)
            {
                IdCrystal = 0;
            }
            else if (array[1] == "")
            {
                IdCrystal = 0;
            }
            else
            {
                IdCrystal = int.Parse(array[1].ToString());
            }

            return IdCrystal;
        }


        public List<string> CrearReportesCrystal(string serviceId, string pPacienteId, List<string> reportesId, string customerOrganizationName, string dni, string pacientName, bool Publicar)
        {
            try
            {
                #region CrearRerport
                _serviceId = serviceId;
                _pacientId = pPacienteId;
                _customerOrganizationName = customerOrganizationName;
                _dni = dni;
                _pacientName = pacientName;


                OperationResult objOperationResult = new OperationResult();
                MultimediaFileBL _multimediaFileBL = new MultimediaFileBL();

                _filesNameToMerge = new List<string>();

                foreach (var com in reportesId)
                {
                    int IdCrystal = GetIdCrystal(com);
                    //ChooseReport(com.Split('|')[0], serviceId, pPacienteId, IdCrystal);
                    tasks.Add(Task<string>.Factory.StartNew(() => ChooseReport(com.Split('|')[0], serviceId, pPacienteId, IdCrystal), TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning));

                }

                if (Publicar)
                {
                    #region Adjuntar Archivos Adjuntos

                    string rutaInterconsulta = Common.Utils.GetApplicationConfigValue("Interconsulta").ToString();

                    List<string> files = Directory.GetFiles(rutaInterconsulta, "*.pdf").ToList();
                    var o = _serviceBL.GetServiceShort(serviceId);

                    var Resultado = files.Find(p => p == rutaInterconsulta + serviceId + "-" + o.Paciente + ".pdf");
                    if (Resultado != null)
                    {
                        _filesNameToMerge.Add(rutaInterconsulta + _serviceId + "-" + o.Paciente + ".pdf");
                    }

                    var ListaPdf = _serviceBL.GetFilePdfsByServiceId(ref objOperationResult, _serviceId);
                    if (ListaPdf != null)
                    {
                        if (ListaPdf.ToList().Count != 0)
                        {
                            foreach (var item in ListaPdf)
                            {
                                var multimediaFile = _multimediaFileBL.GetMultimediaFileById(ref objOperationResult, item.v_MultimediaFileId);
                                string rutaOrigenArchivo = "";
                                if (multimediaFile.ByteArrayFile == null)
                                {
                                    var a = multimediaFile.FileName.Split('-');
                                    var consultorio = a[2].Substring(0, a[2].Length - 0);
                                    if (consultorio == "ESPIROMETRÍA")
                                    {
                                        rutaOrigenArchivo = Common.Utils.GetApplicationConfigValue("ImgESPIROOrigen").ToString();
                                    }
                                    else if (consultorio == "RAYOS X")
                                    {
                                        rutaOrigenArchivo = Common.Utils.GetApplicationConfigValue("ImgRxOrigen").ToString();
                                    }
                                    else if (consultorio == "CARDIOLOGÍA")
                                    {
                                        rutaOrigenArchivo = Common.Utils.GetApplicationConfigValue("ImgEKGOrigen").ToString();
                                    }
                                    else if (consultorio == "LABORATORIO")
                                    {
                                        rutaOrigenArchivo = Common.Utils.GetApplicationConfigValue("ImgLABOrigen").ToString();
                                    }
                                    else if (consultorio == "PSICOLOGÍA")
                                    {
                                        rutaOrigenArchivo = Common.Utils.GetApplicationConfigValue("ImgPSICOOrigen").ToString();
                                    }
                                    else if (consultorio == "OFTALMOLOGÍA")
                                    {
                                        rutaOrigenArchivo = Common.Utils.GetApplicationConfigValue("ImgOftalmoOrigen").ToString();
                                    }
                                    else if (consultorio == "MEDICINA")
                                    {
                                        rutaOrigenArchivo = Common.Utils.GetApplicationConfigValue("ImgMedicinaOrigen").ToString();
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se ha configurado una _ruta para subir el archivo.", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return null;
                                    }
                                    var path = rutaOrigenArchivo + item.v_FileName;
                                    _filesNameToMerge.Add(path);
                                }
                                else
                                {
                                    var path = _ruta + _serviceId + "-" + item.v_FileName;
                                    File.WriteAllBytes(path, multimediaFile.ByteArrayFile);
                                    _filesNameToMerge.Add(path);
                                }
                            }
                        }
                    }
                    #endregion
                }

                return _filesNameToMerge;
                #endregion
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString() + "\nServicio: " + serviceId + "\nPacienteId: " + pPacienteId, "ALERTA!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }




        public string ChooseReport(string componentId, string serviceId, string pPacienteId, int pintIdCrystal)
        {
            try
            {
                #region Choose
                var _ruta = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();
                var _tempSourcePath = Path.Combine(Application.StartupPath, "TempMerge");
                _serviceId = serviceId;
                DataSet ds = null;
                DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
                OperationResult objOperationResult = new OperationResult();
                List<string> filesName = new List<string>();
                MergeExPDF mergeExPDF = new MergeExPDF();
                List<string> unirPdfS = new List<string>();
                _pacientId = pPacienteId;
                ReportDocument rp = null;
                switch (componentId)
                {
                        
                    case Constants.INFORME_CERTIFICADO_APTITUD:
                        //ARNOLD STORE (GetAptitudeCertificateRefactNew)
                        var INFORME_CERTIFICADO_APTITUD = _serviceBL.GetAptitudeCertificateRefact(ref objOperationResult, _serviceId);
                        if (INFORME_CERTIFICADO_APTITUD == null) { break; }
                        DataSet ds1 = new DataSet();
                        DataTable dtINFORME_CERTIFICADO_APTITUD = BLL.Utils.ConvertToDatatable(INFORME_CERTIFICADO_APTITUD);
                        dtINFORME_CERTIFICADO_APTITUD.TableName = "AptitudeCertificate";
                        ds1.Tables.Add(dtINFORME_CERTIFICADO_APTITUD);
                        var TipoServicio = INFORME_CERTIFICADO_APTITUD[0].i_EsoTypeId;
                        if (pintIdCrystal == 24)
                        {
                            if (TipoServicio == ((int)TypeESO.Retiro).ToString()) { rp = new crCertificadoDeAptitudEmpresarial(); }
                            else
                            {
                                if (INFORME_CERTIFICADO_APTITUD[0].i_AptitudeStatusId == (int)AptitudeStatus.AptoObs) { rp = new crCertficadoObservadoSinFirma(); }
                                else { rp = new crOccupationalCertificateSinFirma(); }
                            }
                        }
                        else if (pintIdCrystal == 23) { rp = new crOccupationalMedicalAptitudeCertificate(); }
                        else if (pintIdCrystal == 25) { rp = new crOccupationalMedicalAptitudeCertificate(); }
                        else if (pintIdCrystal == 26) { rp = new crOccupationalMedicalAptitudeCertificateSinDx(); }
                        else if (pintIdCrystal == 28) { rp = new crOccupationaCertificateSinDxSinFirma(); }
                        else if (pintIdCrystal == 66)
                        {
                            if (TipoServicio == ((int)TypeESO.Retiro).ToString()) { rp = new crOccupationalMedicalAptitudeCertificate_SinFirmaYanacocha(); }
                            else
                            {
                                if (INFORME_CERTIFICADO_APTITUD[0].i_AptitudeStatusId == (int)AptitudeStatus.AptoObs) { rp = new crCertficadoObservado_SinFirmaYanacocha(); }
                                else { rp = new crOccupationalMedicalAptitudeCertificate_SinFirmaYanacocha(); }
                            }
                        }
                        else
                        {
                            if (TipoServicio == ((int)TypeESO.Retiro).ToString()) { rp = new crOccupationalMedicalAptitudeCertificate(); }
                            else
                            {
                                if (INFORME_CERTIFICADO_APTITUD[0].i_AptitudeStatusId == (int)AptitudeStatus.AptoObs) { rp = new crCertficadoObservado(); }
                                else { rp = new crOccupationalMedicalAptitudeCertificate(); }
                            }
                        }
                        GenerarReporte(rp, ds1, _ruta, objDiskOpt, Constants.INFORME_CERTIFICADO_APTITUD);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.INFORME_CERTIFICADO_APTITUD_PARA_EMPRESA:
                        //ARNOLD STORE NEW 2
                        var INFORME_CERTIFICADO_APTITUD_EMP = _serviceBL.GetAptitudeCertificateRefact2(ref objOperationResult, _serviceId);
                        if (INFORME_CERTIFICADO_APTITUD_EMP == null) { break; }
                        DataSet ds1_EMP = new DataSet();
                        DataTable dtINFORME_CERTIFICADO_APTITUD_EMP = BLL.Utils.ConvertToDatatable(INFORME_CERTIFICADO_APTITUD_EMP);
                        dtINFORME_CERTIFICADO_APTITUD_EMP.TableName = "AptitudeCertificate";
                        ds1_EMP.Tables.Add(dtINFORME_CERTIFICADO_APTITUD_EMP);
                        var TipoServicio_EMP = INFORME_CERTIFICADO_APTITUD_EMP[0].i_EsoTypeId;
                        ReportDocument rp_EMP = null;
                        if (TipoServicio_EMP == ((int)TypeESO.Retiro).ToString()) 
                        {
                            rp_EMP = new crOccupationalMedicalAptitudeCertificateEMPRESA(); 
                        }
                        else
                        {
                            if (INFORME_CERTIFICADO_APTITUD_EMP[0].i_AptitudeStatusId == (int)AptitudeStatus.AptoObs) { rp_EMP = new crCertficadoObservadoEMPRESA(); }
                            else { rp_EMP = new crOccupationalMedicalAptitudeCertificateEMPRESA(); }
                        }
                        GenerarReporte(rp_EMP, ds1_EMP, _ruta, objDiskOpt, Constants.INFORME_CERTIFICADO_APTITUD_PARA_EMPRESA);
                        rp_EMP.Close();
                        rp_EMP.Dispose();
                        break;
                    case Constants.INFORME_CERTIFICADO_APTITUD_PARA_TRABAJADOR:
                        //ARNOLD STORE NEW 3
                        var INFORME_CERTIFICADO_APTITUD_TRAB = _serviceBL.GetAptitudeCertificateRefact3(ref objOperationResult, _serviceId);
                        if (INFORME_CERTIFICADO_APTITUD_TRAB == null) { break; }
                        DataSet ds1_TRAB = new DataSet();
                        DataTable dtINFORME_CERTIFICADO_APTITUD_TRAB = BLL.Utils.ConvertToDatatable(INFORME_CERTIFICADO_APTITUD_TRAB);
                        dtINFORME_CERTIFICADO_APTITUD_TRAB.TableName = "AptitudeCertificate";
                        ds1_TRAB.Tables.Add(dtINFORME_CERTIFICADO_APTITUD_TRAB);
                        var TipoServicio_TRAB = INFORME_CERTIFICADO_APTITUD_TRAB[0].i_EsoTypeId;
                        ReportDocument rp_TRAB;

                        if (TipoServicio_TRAB == ((int)TypeESO.Retiro).ToString()) { rp_TRAB = new crOccupationalMedicalAptitudeCertificateTRABAJADOR(); }
                        else
                        {
                            if (INFORME_CERTIFICADO_APTITUD_TRAB[0].i_AptitudeStatusId == (int)AptitudeStatus.AptoObs) { rp_TRAB = new crCertficadoObservadoTRABAJADOR(); }
                            else { rp_TRAB = new crOccupationalMedicalAptitudeCertificateTRABAJADOR(); }
                        }
                        GenerarReporte(rp_TRAB, ds1_TRAB, _ruta, objDiskOpt, Constants.INFORME_CERTIFICADO_APTITUD_PARA_TRABAJADOR);
                        rp_TRAB.Close();
                        rp_TRAB.Dispose();
                        break;
                    case Constants.INFORME_ANTECEDENTE_PATOLOGICO:
                        
                        var INFORME_ANTECEDENTE_PATOLOGICO = _serviceBL.GetReportAntecedentePatologico(_pacientId, _serviceId);
                        dsGetRepo = new DataSet();
                        DataTable dtANTECEDENTE_PATOLOGICO_ID = BLL.Utils.ConvertToDatatable(INFORME_ANTECEDENTE_PATOLOGICO);
                        dtANTECEDENTE_PATOLOGICO_ID.TableName = "dtFichaAntecedentePatologico";
                        dsGetRepo.Tables.Add(dtANTECEDENTE_PATOLOGICO_ID);
                        rp = new crFichaAntecedentePatologico01();
                        objDiskOpt = GenerarReporteUnido(rp, dsGetRepo, _ruta, objDiskOpt, Constants.INFORME_ANTECEDENTE_PATOLOGICO + "01.pdf");
                        filesName.Add(objDiskOpt.DiskFileName);
                        var CIRUGIAS = _serviceBL.GetCirugias(_pacientId, _serviceId);
                        dsGetRepo = new DataSet();
                        DataTable dtCirugias = BLL.Utils.ConvertToDatatable(CIRUGIAS);
                        dtCirugias.TableName = "dtCirugias";
                        dsGetRepo.Tables.Add(dtCirugias);
                        rp = new crFichaAntecedentePatologico02();
                        objDiskOpt = GenerarReporteUnido(rp, dsGetRepo, _ruta, objDiskOpt, Constants.INFORME_ANTECEDENTE_PATOLOGICO + "02.pdf");
                        filesName.Add(objDiskOpt.DiskFileName);
                        unirPdfS = filesName.ToList();
                        UnisReportes(unirPdfS, Constants.INFORME_ANTECEDENTE_PATOLOGICO, mergeExPDF);
                          rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.OSTEO_COIMO:
                        //ARNOLD STORE
                        var OSTEO_COIMO = _serviceBL.GetReportOsteoCoimalache(_serviceId, Constants.OSTEO_COIMO);
                        dsGetRepo = new DataSet();
                        DataTable dtOSTEO_COIMO = BLL.Utils.ConvertToDatatable(OSTEO_COIMO);
                        dtOSTEO_COIMO.TableName = "OstioCoimolache";
                        dsGetRepo.Tables.Add(dtOSTEO_COIMO);
                        rp = new crOstioCoimolache();
                        objDiskOpt = GenerarReporteUnido(rp, dsGetRepo, _ruta, objDiskOpt, Constants.OSTEO_COIMO + "01.pdf");
                        filesName.Add(objDiskOpt.DiskFileName);
                        var servicesId88 = new List<string>();
                        servicesId88.Add(_serviceId);
                        var componentReport88 = _serviceBL.ObtenerIdsParaImportacionExcel(servicesId88, 11);
                        //ARNOLD STORE
                        var UC_OSTEO_COIMA_ID = _serviceBL.ReporteUCOsteoCoimalache(_serviceId, componentReport88[0].ComponentId);

                        DataSet dsOsteomuscularCoima = new DataSet();
                        DataTable dt_UC_OSTEO_COIMA_ID = BLL.Utils.ConvertToDatatable(UC_OSTEO_COIMA_ID);
                        dt_UC_OSTEO_COIMA_ID.TableName = "dtUCOsteoMus";
                        dsGetRepo.Tables.Add(dt_UC_OSTEO_COIMA_ID);
                        rp = new crFichaEvaluacionMusc_OC();
                        objDiskOpt = GenerarReporteUnido(rp, dsGetRepo, _ruta, objDiskOpt, Constants.OSTEO_COIMO + "02.pdf");
                        filesName.Add(objDiskOpt.DiskFileName);
                        unirPdfS = filesName.ToList();
                        UnisReportes(unirPdfS, Constants.OSTEO_COIMO, mergeExPDF);
                           rp.Close();
                        rp.Dispose();
                        break;


                    case Constants.TOXICOLOGICO_ID:
                        //ARNOLD STORE
                        var TOXICOLOGICO_ID = _serviceBL.GetReportToxicologico(_serviceId, Constants.TOXICOLOGICO_ID);
                        dsGetRepo = new DataSet();
                        DataTable dtTOXICOLOGICO_ID = BLL.Utils.ConvertToDatatable(TOXICOLOGICO_ID);
                        dtTOXICOLOGICO_ID.TableName = "dtAutorizacionDosajeDrogas";
                        dsGetRepo.Tables.Add(dtTOXICOLOGICO_ID);
                        rp = new crAutorizacionDosajeDrogras();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.TOXICOLOGICO_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.INFORME_DECLARACION_CI:
                        //ARNOLD STORE
                        var DECLARACION_CI_INFORMADO = new PacientBL().GetReportConsentimiento(_serviceId);
                        dsGetRepo = new DataSet();
                        DataTable dtDECLARACION_CI = BLL.Utils.ConvertToDatatable(DECLARACION_CI_INFORMADO);
                        dtDECLARACION_CI.TableName = "dtConsentimiento";
                        dsGetRepo.Tables.Add(dtDECLARACION_CI);
                        if (pintIdCrystal == 51)
                        {
                            rp = new crDeclaracion_YanaGold();
                            GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.INFORME_DECLARACION_CI);
                        }
                         rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.CONSENTIMIENTO_INFORMADO:
                        //ARNOLD STORE
                        var CONSENTIMIENTO_INFORMADO = new PacientBL().GetReportConsentimiento(_serviceId);
                        dsGetRepo = new DataSet();
                        DataTable dtCONSENTIMIENTO_INFORMADO = BLL.Utils.ConvertToDatatable(CONSENTIMIENTO_INFORMADO);
                        dtCONSENTIMIENTO_INFORMADO.TableName = "dtConsentimiento";
                        dsGetRepo.Tables.Add(dtCONSENTIMIENTO_INFORMADO);
                        if (pintIdCrystal == 50) { rp = new crConsentimiento_YanaGold(); }
                        else { rp = new crConsentimiento(); }
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.CONSENTIMIENTO_INFORMADO);
                        rp.Close();
                        rp.Dispose();
                        break;

                    #region Audiomax
                    case Constants.AUDIOMETRIA_AUDIOMAX_ID:
                        var AUDIOMETRIA_AUDIOMAX_ID = new PacientBL().GetReportConsentimiento(_serviceId);

                        dsGetRepo = new DataSet();
                        DataTable dtAUDIOMETRIA_AUDIOMAX_ID = BLL.Utils.ConvertToDatatable(AUDIOMETRIA_AUDIOMAX_ID);
                        dtAUDIOMETRIA_AUDIOMAX_ID.TableName = "dtAudiometriaAudiomax";
                        dsGetRepo.Tables.Add(dtAUDIOMETRIA_AUDIOMAX_ID);
                        rp = new crAudiometriaAudioMax();
                        rp.SetDataSource(dsGetRepo);

                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.CONSENTIMIENTO_INFORMADO + ".pdf";
                        objDiskOpt.DiskFileName = _ruta + serviceId + "-" + Constants.AUDIOMETRIA_AUDIOMAX_ID + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                        break;
                    #endregion


                    case Constants.OSTEO_MUSCULAR_ID_1:
                        
                        DataSet dsOsteomuscularNuevo = new DataSet();
                        var servicesId7 = new List<string>();
                        servicesId7.Add(_serviceId);
                        var componentReportId7 = _serviceBL.ObtenerIdsParaImportacionExcel(servicesId7, 11);
                        //ARNOLD STORE
                        var OSTEO_MUSCULAR_ID_1 = new PacientBL().ReportOsteoMuscularNuevo(_serviceId, componentId, componentReportId7[0].ComponentId);
                        var UC_OSTEO_ID = _serviceBL.ReporteOsteomuscular(_serviceId, componentReportId7[0].ComponentId);
                        DataTable dt_UC_OSTEO_ID = BLL.Utils.ConvertToDatatable(UC_OSTEO_ID);
                        DataTable dtOSTEO_MUSCULAR_ID_1 = BLL.Utils.ConvertToDatatable(OSTEO_MUSCULAR_ID_1);
                        dtOSTEO_MUSCULAR_ID_1.TableName = "dtOsteomuscularNuevo";
                        dt_UC_OSTEO_ID.TableName = "dtOsteoMus";
                        dsOsteomuscularNuevo.Tables.Add(dtOSTEO_MUSCULAR_ID_1);
                        dsOsteomuscularNuevo.Tables.Add(dt_UC_OSTEO_ID);
                        rp = new crMuscoloEsqueletico();
                        objDiskOpt = GenerarReporteUnido(rp, dsOsteomuscularNuevo, _ruta, objDiskOpt, Constants.OSTEO_MUSCULAR_ID_1 + "01.pdf");
                        filesName.Add(objDiskOpt.DiskFileName);

                        rp = new crMuscoloEsqueletico2();
                        objDiskOpt = GenerarReporteUnido(rp, dsOsteomuscularNuevo, _ruta, objDiskOpt, Constants.OSTEO_MUSCULAR_ID_1 + "02.pdf");
                        filesName.Add(objDiskOpt.DiskFileName);
                        unirPdfS = filesName.ToList();
                        UnisReportes(unirPdfS, Constants.OSTEO_MUSCULAR_ID_1, mergeExPDF);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.OSTEO_MUSCULAR_ID_2:
                        //ARNOLD STORE
                        var OSTEO_MUSCULAR_ID_2 = new PacientBL().GetMusculoEsqueletico(_serviceId, Constants.OSTEO_MUSCULAR_ID_2);
                        dsGetRepo = new DataSet();
                        DataTable dtOSTEO_MUSCULAR_ID_2 = BLL.Utils.ConvertToDatatable(OSTEO_MUSCULAR_ID_2);
                        dtOSTEO_MUSCULAR_ID_2.TableName = "dtOstomuscular";
                        dsGetRepo.Tables.Add(dtOSTEO_MUSCULAR_ID_2);
                        rp = new crEvaluacionOsteomuscular();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.OSTEO_MUSCULAR_ID_2);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.INFORME_CERTIFICADO_APTITUD_EMPRESARIAL:
                        
                        var Path123 = Application.StartupPath;
                        //ARNOLD STORE
                        var INFORME_CERTIFICADO_APTITUD_EMPRESARIAL = _serviceBL.GetCAPE(_serviceId);
                        dsGetRepo = new DataSet();
                        DataTable dt_INFORME_CERTIFICADO_APTITUD_EMPRESARIAL = BLL.Utils.ConvertToDatatable(INFORME_CERTIFICADO_APTITUD_EMPRESARIAL);
                        dt_INFORME_CERTIFICADO_APTITUD_EMPRESARIAL.TableName = "AptitudeCertificate";
                        dsGetRepo.Tables.Add(dt_INFORME_CERTIFICADO_APTITUD_EMPRESARIAL);
                        TipoServicio = INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].i_EsoTypeId;
                        if (pintIdCrystal == 30)
                        {
                            if (TipoServicio == ((int)TypeESO.Retiro).ToString()) { rp = new crCertificadoDeAptitudEmpresarial(); }
                            else
                            {
                                if (INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].i_AptitudeStatusId == (int)AptitudeStatus.AptoObs) { rp = new crCertficadoObservado(); }
                                else { rp = new crCertificadoEmpresarialSinFirma(); }
                            }
                        }
                        else
                        {
                            if (TipoServicio == ((int)TypeESO.Retiro).ToString()) { rp = new crCertificadoDeAptitudEmpresarial(); }
                            else
                            {
                                if (INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].i_AptitudeStatusId == (int)AptitudeStatus.AptoObs) { rp = new crCertficadoObservado(); }
                                else { rp = new crCertificadoDeAptitudEmpresarial(); }
                            }
                        }
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.INFORME_CERTIFICADO_APTITUD_EMPRESARIAL);
                              
                        rp.Close();
                        rp.Dispose();

                        break;

                    case Constants.INFORME_HISTORIA_OCUPACIONAL:
                        //ARNOLD STORE 
                        var INFORME_HISTORIA_OCUPACIONAL = _serviceBL.ReportHistoriaOcupacional(_serviceId);
                        dsGetRepo = new DataSet();
                        DataTable dtINFORME_HISTORIA_OCUPACIONAL = BLL.Utils.ConvertToDatatable(INFORME_HISTORIA_OCUPACIONAL);
                        dtINFORME_HISTORIA_OCUPACIONAL.TableName = "HistoriaOcupacional";
                        dsGetRepo.Tables.Add(dtINFORME_HISTORIA_OCUPACIONAL);
                        if (pintIdCrystal == 37) { rp = new crApendice01_HistoriaOcupacional(); }
                        else if (pintIdCrystal == 52) { rp = new crFichaMedicaOcupacional_CHO(); }
                        else if (pintIdCrystal == 56) { rp = new crfichaComplementaria_HO(); }
                        else if (pintIdCrystal == 57) { rp = new crAnexo02_HistoriaOcupacional(); }
                        else if (pintIdCrystal == 65) { rp = new crHistoriaOcupacional_HUDBAY(); }

                        else { rp = new crHistoriaOcupacional(); }
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.INFORME_HISTORIA_OCUPACIONAL);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.ALTURA_ESTRUCTURAL_ID:
                        
                        var servicesId9 = new List<string>();
                        servicesId9.Add(_serviceId);
                        var componentReportId9 = _serviceBL.ObtenerIdsParaImportacionExcel(servicesId9, 7);
                        //ARNOLD STORE
                        var dataListForReport = new PacientBL().GetAlturaEstructural(_serviceId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ID);
                        dsGetRepo = new DataSet();
                        DataTable dt_ALTURA_ESTRUCTURAL_ID = BLL.Utils.ConvertToDatatable(dataListForReport);
                        dt_ALTURA_ESTRUCTURAL_ID.TableName = "dtAlturaEstructural";
                        dsGetRepo.Tables.Add(dt_ALTURA_ESTRUCTURAL_ID);
                        rp = new crAlturaMayor();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.ALTURA_ESTRUCTURAL_ID);
                         rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.APENDICE_ID:
                        //ARNOLD STORE
                        var APENDICE_ID = _serviceBL.GetReportEstudioElectrocardiografico(_serviceId, Constants.APENDICE_ID);
                        dsGetRepo = new DataSet();
                        DataTable dtAPENDICE_ID = BLL.Utils.ConvertToDatatable(APENDICE_ID);
                        dtAPENDICE_ID.TableName = "dtEstudioElectrocardiografico";
                        dsGetRepo.Tables.Add(dtAPENDICE_ID);
                        if (pintIdCrystal == 43) { rp = new crApendice05_EKG(); }
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.APENDICE_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.ELECTRO_GOLD:
                        //ARNOLD STORE
                        var electroGold = _serviceBL.GetReportElectroGold(_serviceId, Constants.ELECTRO_GOLD);
                        dsGetRepo = new DataSet();
                        DataTable dt_ELECTRO_GOLD = BLL.Utils.ConvertToDatatable(electroGold);
                        dt_ELECTRO_GOLD.TableName = "dtEstudioElectrocardiografico";
                        dsGetRepo.Tables.Add(dt_ELECTRO_GOLD);
                        rp = new crInformeElectroCardiografiaoGoldField_EKG();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.ELECTRO_GOLD);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.ELECTROCARDIOGRAMA_ID:
                        //ARNOLD STORE
                        var ELECTROCARDIOGRAMA_ID = _serviceBL.GetReportEstudioElectrocardiografico(_serviceId, Constants.ELECTROCARDIOGRAMA_ID);
                        dsGetRepo = new DataSet();
                        DataTable dt_ELECTROCARDIOGRAMA_ID = BLL.Utils.ConvertToDatatable(ELECTROCARDIOGRAMA_ID);
                        dt_ELECTROCARDIOGRAMA_ID.TableName = "dtEstudioElectrocardiografico";
                        dsGetRepo.Tables.Add(dt_ELECTROCARDIOGRAMA_ID);
                        if (pintIdCrystal == 15) { rp = new crEstudioElectrocardiograficoMedico(); }
                        else if (pintIdCrystal == 16) { rp = new crEstudioElectrocardiograficoSinFirma(); }
                        else { rp = new crEstudioElectrocardiografico(); }
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.ELECTROCARDIOGRAMA_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    #region Prueba de Esfuerzo
                    case Constants.PRUEBA_ESFUERZO_ID:
                        //OLD NO USO
                        var aptitudeCertificate = _serviceBL.GetReportPruebaEsfuerzo(_serviceId, Constants.PRUEBA_ESFUERZO_ID);
                        var FuncionesVitales1 = _serviceBL.ReportFuncionesVitales(_serviceId, Constants.FUNCIONES_VITALES_ID);
                        var Antropometria1 = _serviceBL.ReportAntropometria(_serviceId, Constants.ANTROPOMETRIA_ID);

                        dsGetRepo = new DataSet();
                        DataTable dt_PRUEBA_ESFUERZO_ID = BLL.Utils.ConvertToDatatable(aptitudeCertificate);
                        dt_PRUEBA_ESFUERZO_ID.TableName = "dtPruebaEsfuerzo";
                        dsGetRepo.Tables.Add(dt_PRUEBA_ESFUERZO_ID);

                        DataTable dt1_PRUEBA_ESFUERZO_ID = BLL.Utils.ConvertToDatatable(FuncionesVitales1);
                        dt1_PRUEBA_ESFUERZO_ID.TableName = "dtFuncionesVitales";
                        dsGetRepo.Tables.Add(dt1_PRUEBA_ESFUERZO_ID);

                        DataTable dt2_PRUEBA_ESFUERZO_ID = BLL.Utils.ConvertToDatatable(Antropometria1);
                        dt2_PRUEBA_ESFUERZO_ID.TableName = "dtAntropometria";
                        dsGetRepo.Tables.Add(dt2_PRUEBA_ESFUERZO_ID);

                        rp = new crPruebaEsfuerzo();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.PRUEBA_ESFUERZO_ID + ".pdf";
                        objDiskOpt.DiskFileName = _ruta + serviceId + "-" + Constants.PRUEBA_ESFUERZO_ID + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                        rp.Dispose();
                        
                        break;
                    #endregion

                    case Constants.ODONTOGRAMA_ID:
                        
                        var Path1 = Application.StartupPath;
                        //ARNOLD STORE
                        var ODONTOGRAMA_ID = _serviceBL.ReportOdontograma(_serviceId, Constants.ODONTOGRAMA_ID, Path1);
                        dsGetRepo = new DataSet();
                        DataTable dt_ODONTOGRAMA_ID = BLL.Utils.ConvertToDatatable(ODONTOGRAMA_ID);
                        dt_ODONTOGRAMA_ID.TableName = "dtOdontograma";
                        dsGetRepo.Tables.Add(dt_ODONTOGRAMA_ID);
                        if (pintIdCrystal == 19) { rp = new crOdontogramaSinFirma(); }
                        if (pintIdCrystal == 64) { rp = new crOdontograma_Hudbay(); }
                        else { rp = new crOdontograma(); }
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.ODONTOGRAMA_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.AUDIOMETRIA_ID:
                        
                        var serviceBL = _serviceBL;
                        DataSet dsAudiometria = new DataSet();
                        //ARNOLD STORE
                        var dxList = serviceBL.GetDiagnosticRepositoryByComponent(_serviceId, Constants.AUDIOMETRIA_ID);
                        if (dxList.Count == 0)
                        {
                            DiagnosticRepositoryList oDiagnosticRepositoryList = new DiagnosticRepositoryList();
                            List<DiagnosticRepositoryList> Lista = new List<DiagnosticRepositoryList>();
                            oDiagnosticRepositoryList.v_ServiceId = "Sin Id";
                            oDiagnosticRepositoryList.v_DiseasesName = "Sin Alteración";
                            oDiagnosticRepositoryList.v_DiagnosticRepositoryId = "Sin Id";
                            Lista.Add(oDiagnosticRepositoryList);
                            var dtDx = BLL.Utils.ConvertToDatatable(Lista);
                            dtDx.TableName = "dtDiagnostic";
                            dsAudiometria.Tables.Add(dtDx);
                        }
                        else
                        {
                            List<DiagnosticRepositoryList> ListaDxsAudio = new List<DiagnosticRepositoryList>();
                            DiagnosticRepositoryList oDiagnosticRepositoryList;
                            foreach (var item in dxList)
                            {
                                oDiagnosticRepositoryList = new DiagnosticRepositoryList();

                                oDiagnosticRepositoryList.v_DiseasesName = item.v_DiseasesName;
                                oDiagnosticRepositoryList.v_DiagnosticRepositoryId = item.v_DiagnosticRepositoryId;
                                oDiagnosticRepositoryList.v_ServiceId = item.v_ServiceId;
                                ListaDxsAudio.Add(oDiagnosticRepositoryList);
                            }
                            var dtDx = BLL.Utils.ConvertToDatatable(ListaDxsAudio);
                            dtDx.TableName = "dtDiagnostic";
                            dsAudiometria.Tables.Add(dtDx);
                        }
                        var recom = dxList.SelectMany(s1 => s1.Recomendations).ToList();
                        if (recom.Count == 0)
                        {
                            RecomendationList oRecomendationList = new RecomendationList();
                            List<RecomendationList> Lista = new List<RecomendationList>();
                            oRecomendationList.v_ServiceId = "Sin Id";
                            oRecomendationList.v_RecommendationName = "Sin Recomendaciones";
                            oRecomendationList.v_DiagnosticRepositoryId = "Sin Id";
                            Lista.Add(oRecomendationList);
                            var dtReco = BLL.Utils.ConvertToDatatable(Lista);
                            dtReco.TableName = "dtRecomendation";
                            dsAudiometria.Tables.Add(dtReco);

                        }
                        else
                        {
                            var dtReco = BLL.Utils.ConvertToDatatable(recom);
                            dtReco.TableName = "dtRecomendation";
                            dsAudiometria.Tables.Add(dtReco);
                        }

                        //-------******************************************************************************************
                        var audioUserControlList = serviceBL.ReportAudiometriaUserControl(_serviceId, Constants.AUDIOMETRIA_ID);
                        //ARNOLD STORE
                        var audioCabeceraList = serviceBL.ReportAudiometria(_serviceId, Constants.AUDIOMETRIA_ID);
                        var dtAudiometriaUserControl = BLL.Utils.ConvertToDatatable(audioUserControlList);
                        var dtCabecera = BLL.Utils.ConvertToDatatable(audioCabeceraList);

                        dtCabecera.TableName = "dtAudiometria";
                        dtAudiometriaUserControl.TableName = "dtAudiometriaUserControl";

                        dsAudiometria.Tables.Add(dtCabecera);
                        dsAudiometria.Tables.Add(dtAudiometriaUserControl);

                        var MedicalCenter1 = _serviceBL.GetInfoMedicalCenter();

                        if (MedicalCenter1.v_IdentificationNumber == "20506336245")
                        {
                            rp = new crFichaAudiometriaAudiomax01();
                            rp.SetDataSource(dsAudiometria);
                            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            objDiskOpt = new DiskFileDestinationOptions();
                            objDiskOpt.DiskFileName = _ruta + serviceId + "-" + Constants.AUDIOMETRIA_ID + ".pdf";
                            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                            rp.ExportOptions.DestinationOptions = objDiskOpt;
                            rp.Export();
                            rp.Close();

                            rp = new crFichaAudiometriaAudiomax02();
                            rp.SetDataSource(dsAudiometria);
                            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            objDiskOpt = new DiskFileDestinationOptions();
                            objDiskOpt.DiskFileName = _ruta + serviceId + "-" + Constants.AUDIOMETRIA_ID + ".pdf";
                            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                            rp.ExportOptions.DestinationOptions = objDiskOpt;
                            rp.Export();
                            rp.Close();
                        }
                        else if (pintIdCrystal == 40) { rp = new crApendice03_Audio(); }
                        else
                        {
                            if (pintIdCrystal == 11) { rp = new crFichaAudiometriaMedico(); }
                            else if (pintIdCrystal == 12) { rp = new crFichaAudiometriaSinFirma(); }
                            else { rp = new crFichaAudiometria(); }
                        }
                        GenerarReporte(rp, dsAudiometria, _ruta, objDiskOpt, Constants.AUDIOMETRIA_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    #region Ginecología
                    case Constants.GINECOLOGIA_ID:      // Falta implementar
                        
                        var GINECOLOGIA_ID = _serviceBL.GetReportEvaluacionGinecologico(_serviceId, Constants.GINECOLOGIA_ID);

                        dsGetRepo = new DataSet();
                        DataTable dt_GINECOLOGIA_ID = BLL.Utils.ConvertToDatatable(GINECOLOGIA_ID);
                        dt_GINECOLOGIA_ID.TableName = "dtEvaGinecologico";
                        dsGetRepo.Tables.Add(dt_GINECOLOGIA_ID);

                        rp = new crEvaluacionGenecologica();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.GINECOLOGIA_ID + ".pdf";
                        objDiskOpt.DiskFileName = _ruta + serviceId + "-" + Constants.GINECOLOGIA_ID + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                        rp.Dispose();
                        
                        break;
                    #endregion

                        //OLD NO USO
                    case Constants.OFTALMOLOGIA_ID:
                        
                        var OFTALMO_ANTIGUO = new PacientBL().GetOftalmologia(_serviceId, Constants.OFTALMOLOGIA_ID);

                        dsGetRepo = new DataSet();
                        DataTable dt_OFTALMO_ANTIGUO = BLL.Utils.ConvertToDatatable(OFTALMO_ANTIGUO);
                        dt_OFTALMO_ANTIGUO.TableName = "dtOftalmologia";
                        dsGetRepo.Tables.Add(dt_OFTALMO_ANTIGUO);

                        if (pintIdCrystal == 39) { rp = new crApendice02_Oftalmo(); }
                        else { rp = new crOftalmologia(); }
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.OFTALMOLOGIA_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.PSICOLOGIA_ID:
                        //ARNOLD STORE
                        var PSICOLOGIA_ID = new PacientBL().GetFichaPsicologicaOcupacional(_serviceId);
                        dsGetRepo = new DataSet();
                        DataTable dt_PSICOLOGIA_ID = BLL.Utils.ConvertToDatatable(PSICOLOGIA_ID);
                        dt_PSICOLOGIA_ID.TableName = "InformePsico";
                        dsGetRepo.Tables.Add(dt_PSICOLOGIA_ID);

                        if (pintIdCrystal == 62) { rp = new InformePsicologicoOcupacionalHudbay(); }
                        else { rp = new InformePsicologicoOcupacional(); }
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.PSICOLOGIA_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.RX_TORAX_ID:
                        //ARNOLD STORE
                        var RX_TORAX_ID = _serviceBL.ReportRadiologico(_serviceId, Constants.RX_TORAX_ID);
                        dsGetRepo = new DataSet();
                        DataTable dt_RX_TORAX_ID = BLL.Utils.ConvertToDatatable(RX_TORAX_ID);
                        dt_RX_TORAX_ID.TableName = "dtRadiologico";
                        dsGetRepo.Tables.Add(dt_RX_TORAX_ID);
                        if (pintIdCrystal == 7) { rp = new crInformeRadiologicoMedico(); }
                        else if (pintIdCrystal == 8) { rp = new crInformeRadiologicoSinFirma(); }
                        else { rp = new crInformeRadiologico(); }
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.RX_TORAX_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.OIT_ID:
                        //ARNOLD STORE
                        var OIT_ID = _serviceBL.ReportInformeRadiografico(_serviceId, Constants.OIT_ID);

                        dsGetRepo = new DataSet();
                        DataTable dt_OIT_ID = BLL.Utils.ConvertToDatatable(OIT_ID);
                        dt_OIT_ID.TableName = "dtInformeRadiografico";
                        dsGetRepo.Tables.Add(dt_OIT_ID);

                        if (pintIdCrystal == 45) { rp = new crApendice06_OIT(); }
                        else if (pintIdCrystal == 60) { rp = new crInformeRadiograficoOITHudbayNew(); }
                        else { rp = new crInformeRadiograficoOIT(); }
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.OIT_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    //case Constants.OIT_PARTICULAR_ID:
                    //    //ARNOLD STORE
                    //    var OIT_PARTICULAR_ID = _serviceBL.ReportInformeRadiografico(_serviceId, Constants.OIT_ID);

                    //    dsGetRepo = new DataSet();
                    //    DataTable dt_OIT_ID = BLL.Utils.ConvertToDatatable(OIT_PARTICULAR_ID);
                    //    dt_OIT_ID.TableName = "dtInformeRadiografico";
                    //    dsGetRepo.Tables.Add(dt_OIT_ID);

                    //    if (pintIdCrystal == 45) { rp = new crApendice06_OIT(); }
                    //    else if (pintIdCrystal == 60) { rp = new crInformeRadiograficoOITHudbayNew(); }
                    //    else { rp = new crInformeRadiograficoOIT(); }
                    //    GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.OIT_PARTICULAR_ID);
                    //    rp.Close();
                    //    rp.Dispose();
                    //    break;

                    case Constants.TAMIZAJE_DERMATOLOGIO_ID:
                        //ARNOLD STORE
                        var TAMIZAJE_DERMATOLOGIO_ID = _serviceBL.ReportTamizajeDermatologico(_serviceId, Constants.TAMIZAJE_DERMATOLOGIO_ID);

                        dsGetRepo = new DataSet();
                        DataTable dt_TAMIZAJE_DERMATOLOGIO_ID = BLL.Utils.ConvertToDatatable(TAMIZAJE_DERMATOLOGIO_ID);
                        dt_TAMIZAJE_DERMATOLOGIO_ID.TableName = "dtTamizajeDermatologico";
                        dsGetRepo.Tables.Add(dt_TAMIZAJE_DERMATOLOGIO_ID);

                        rp = new crTamizajeDermatologico();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.TAMIZAJE_DERMATOLOGIO_ID);
                        rp.Close();
                        rp.Dispose();
                        break;
                    case Constants.INFORME_ESPIROMETRIA:
                        //ARNOLD STORE
                        var INFORME_ESPIROMETRIA = _serviceBL.GetReportCuestionarioEspirometria(_serviceId, Constants.ESPIROMETRIA_ID);
                        dsGetRepo = new DataSet();
                        DataTable dtINFORME_ESPIROMETRIA = BLL.Utils.ConvertToDatatable(INFORME_ESPIROMETRIA);
                        dtINFORME_ESPIROMETRIA.TableName = "dtInformeEspirometria";
                        dsGetRepo.Tables.Add(dtINFORME_ESPIROMETRIA);
                        if (pintIdCrystal == 46) { rp = new crApendice08_Espiro02(); }
                        else { rp = new crApendice08_Espiro02(); }
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.INFORME_ESPIROMETRIA);
                        rp.Close();
                        rp.Dispose();
                        break;
                    case Constants.ESPIROMETRIA_ID:
                        //ARNOLD STORE
                        var ESPIROMETRIA_ID = _serviceBL.GetReportCuestionarioEspirometria(_serviceId, Constants.ESPIROMETRIA_ID);
                        dsGetRepo = new DataSet();
                        DataTable dt_ESPIROMETRIA_ID = BLL.Utils.ConvertToDatatable(ESPIROMETRIA_ID);
                        dt_ESPIROMETRIA_ID.TableName = "dtCuestionarioEspirometria";
                        dsGetRepo.Tables.Add(dt_ESPIROMETRIA_ID);

                        if (pintIdCrystal == 54)
                        {
                            rp = new crEspiroCuestionarioGoldField();
                        }
                        else
                            if (pintIdCrystal == 35) { rp = new crApendice08_Espiro01(); }
                            else if (pintIdCrystal == 58) { rp = new crAnexo05_Espiro(); }
                            else if (pintIdCrystal == 59) { rp = new crCuestionarioEspirometriHudbay(); }
                            else { rp = new crCuestionarioEspirometria(); }
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.ESPIROMETRIA_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.EVALUACION_PSICOLABORAL:
                        //ARNOLD STORE
                        var EVALUACION_PSICOLABORAL = _serviceBL.GetReportEvaluacionPsicolaborlaPersonal(_serviceId, Constants.EVALUACION_PSICOLABORAL);
                        dsGetRepo = new DataSet();
                        DataTable dt_EVALUACION_PSICOLABORAL = BLL.Utils.ConvertToDatatable(EVALUACION_PSICOLABORAL);
                        dt_EVALUACION_PSICOLABORAL.TableName = "dtEvaluacionPsicolaboralPersonal";
                        dsGetRepo.Tables.Add(dt_EVALUACION_PSICOLABORAL);
                        rp = new crEvaluacionPsicolaboralPersonal();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.EVALUACION_PSICOLABORAL);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.TESTOJOSECO_ID:
                        //REPORT OLD
                        var TESTOJOSECO_ID = _serviceBL.ReporteOjoSeco(_serviceId, Constants.TESTOJOSECO_ID);
                        dsGetRepo = new DataSet();
                        DataTable dt_TESTOJOSECO_ID = BLL.Utils.ConvertToDatatable(TESTOJOSECO_ID);
                        dt_TESTOJOSECO_ID.TableName = "dtOjoSeco";
                        dsGetRepo.Tables.Add(dt_TESTOJOSECO_ID);
                        rp = new crCuestionarioOjoSeco();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.TESTOJOSECO_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.C_N_ID:
                        //ARNOLD STORE
                        var C_N_ID = _serviceBL.GetReportCuestionarioNordico(_serviceId, Constants.C_N_ID);
                        dsGetRepo = new DataSet();
                        DataTable dt_C_N_ID = BLL.Utils.ConvertToDatatable(C_N_ID);
                        dt_C_N_ID.TableName = "dtCustionarioNordico";
                        dsGetRepo.Tables.Add(dt_C_N_ID);
                        rp = new crCuestionarioNordico();
                        objDiskOpt = GenerarReporteUnido(rp, dsGetRepo, _ruta, objDiskOpt, Constants.C_N_ID + "01.pdf");
                        
                        filesName.Add(objDiskOpt.DiskFileName);

                        
                        rp = new crCuestionarioNordico_02();
                        objDiskOpt = GenerarReporteUnido(rp, dsGetRepo, _ruta, objDiskOpt, Constants.C_N_ID + "02.pdf");
                        filesName.Add(objDiskOpt.DiskFileName);
                        unirPdfS = filesName.ToList();
                        UnisReportes(unirPdfS, Constants.OSTEO_MUSCULAR_ID_1, mergeExPDF);
                        rp.Close();
                        rp.Dispose();
                        break;
                    case Constants.CUESTIONARIO_ACTIVIDAD_FISICA:
                        //REPORT OLD
                        var CUESTIONARIO_ACTIVIDAD_FISICA = _serviceBL.GetReportCuestionarioActividadFisica(_serviceId, Constants.CUESTIONARIO_ACTIVIDAD_FISICA);

                        dsGetRepo = new DataSet();

                        DataTable dt_CUESTIONARIO_ACTIVIDAD_FISICA = BLL.Utils.ConvertToDatatable(CUESTIONARIO_ACTIVIDAD_FISICA);
                        dt_CUESTIONARIO_ACTIVIDAD_FISICA.TableName = "dtCustionarioActividadFisica";
                        dsGetRepo.Tables.Add(dt_CUESTIONARIO_ACTIVIDAD_FISICA);
                        rp = new crCuestionarioActividadFisica();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.CUESTIONARIO_ACTIVIDAD_FISICA);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.INFORME_ECOGRAFICO_PROSTATA_ID:
                        //REPORT OLD
                        var INFORME_ECOGRAFICO_PROSTATA_ID = _serviceBL.GetReportInformeEcograficoProstata(_serviceId, Constants.INFORME_ECOGRAFICO_PROSTATA_ID);
                        dsGetRepo = new DataSet();
                        DataTable dt_INFORME_ECOGRAFICO_PROSTATA_ID = BLL.Utils.ConvertToDatatable(INFORME_ECOGRAFICO_PROSTATA_ID);
                        dt_INFORME_ECOGRAFICO_PROSTATA_ID.TableName = "dtInformeEcograficoProstata";
                        dsGetRepo.Tables.Add(dt_INFORME_ECOGRAFICO_PROSTATA_ID);
                        rp = new crInformeEcograficoProstata();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.INFORME_ECOGRAFICO_PROSTATA_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.ECOGRAFIA_ABDOMINAL_ID:
                        //REPORT OLD
                        var ECOGRAFIA_ABDOMINAL_ID = _serviceBL.GetReportInformeEcograficoAbdominal(_serviceId, Constants.ECOGRAFIA_ABDOMINAL_ID);
                        dsGetRepo = new DataSet();
                        DataTable dt_ECOGRAFIA_ABDOMINAL_ID = BLL.Utils.ConvertToDatatable(ECOGRAFIA_ABDOMINAL_ID);
                        dt_ECOGRAFIA_ABDOMINAL_ID.TableName = "dtInformeEcograficoAbdominal";
                        dsGetRepo.Tables.Add(dt_ECOGRAFIA_ABDOMINAL_ID);
                        rp = new crInformeEcograficoAbdominal();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.ECOGRAFIA_ABDOMINAL_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.ECOGRAFIA_RENAL_ID:
                        //REPORT OLD
                        var ECOGRAFIA_RENAL_ID = _serviceBL.GetReportInformeEcograficoRenal(_serviceId, Constants.ECOGRAFIA_RENAL_ID);
                        dsGetRepo = new DataSet();
                        DataTable dt_ECOGRAFIA_RENAL_ID = BLL.Utils.ConvertToDatatable(ECOGRAFIA_RENAL_ID);
                        dt_ECOGRAFIA_RENAL_ID.TableName = "dtInformeEcograficoRenal";
                        dsGetRepo.Tables.Add(dt_ECOGRAFIA_RENAL_ID);
                        rp = new crInformeEcograficoRenal();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.ECOGRAFIA_RENAL_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    #region Certi San Martin
                    case Constants.INFORME_CERTIFICADO_APTITUD_SM:
                        //ARNOLD STORE
                        var INFORME_CERTIFICADO_APTITUD_SM = _serviceBL.GetCAPSM(_serviceId);

                        dsGetRepo = new DataSet();
                        DataTable dtINFORME_CERTIFICADO_APTITUD_SM = BLL.Utils.ConvertToDatatable(INFORME_CERTIFICADO_APTITUD_SM);
                        dtINFORME_CERTIFICADO_APTITUD_SM.TableName = "AptitudeCertificate";
                        dsGetRepo.Tables.Add(dtINFORME_CERTIFICADO_APTITUD_SM);
                        rp = new crCertficadoDeAptitudSM();
                        rp.SetDataSource(dsGetRepo);

                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.INFORME_CERTIFICADO_APTITUD_SM + ".pdf";
                        objDiskOpt.DiskFileName = _ruta + serviceId + "-" + Constants.INFORME_CERTIFICADO_APTITUD_SM + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                        rp.Dispose();
                        
                        break;
                    #endregion

                    case Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX:
                        //ARNOLD STORE
                        var Path3 = Application.StartupPath;
                        var INFORME_CERTIFICADO_APTITUD_SIN_DX = _serviceBL.GetCAPSD(_serviceId, Path3);
                        dsGetRepo = new DataSet();
                        DataTable dtINFORME_CERTIFICADO_APTITUD_SIN_DX = BLL.Utils.ConvertToDatatable(INFORME_CERTIFICADO_APTITUD_SIN_DX);
                        dtINFORME_CERTIFICADO_APTITUD_SIN_DX.TableName = "AptitudeCertificate";
                        dsGetRepo.Tables.Add(dtINFORME_CERTIFICADO_APTITUD_SIN_DX);
                        if (INFORME_CERTIFICADO_APTITUD_SIN_DX == null) { break; }
                        TipoServicio = INFORME_CERTIFICADO_APTITUD_SIN_DX[0].i_EsoTypeId;
                        if (pintIdCrystal == 28)
                        {
                            if (TipoServicio == ((int)TypeESO.Retiro).ToString()) { rp = new crCertificadoDeAptitudEmpresarial(); }
                            else
                            {
                                if (INFORME_CERTIFICADO_APTITUD_SIN_DX[0].i_AptitudeStatusId == (int)AptitudeStatus.AptoObs) { rp = new crCertficadoObservadoSinDxSinFirma(); }
                                else { rp = new crCertificadoSinDXSinFirma(); }
                            }
                        }
                        else
                        {
                            if (TipoServicio == ((int)TypeESO.Retiro).ToString()) { rp = new crOccupationalRetirosSinDx(); }
                            else
                            {
                                if (INFORME_CERTIFICADO_APTITUD_SIN_DX[0].i_AptitudeStatusId == (int)AptitudeStatus.AptoObs) { rp = new crCertficadoObservadoSinDx(); }
                                else { rp = new crCertificadoDeAptitudSinDX(); }
                            }
                        }
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX);
                        rp.Close();
                        rp.Dispose();
                        break;


                    case Constants.TEST_VERTIGO_ID:
                        
                        var servicesId3 = new List<string>();
                        servicesId3.Add(_serviceId);
                        var componentReportId = _serviceBL.ObtenerIdsParaImportacionExcel(servicesId3, 11);
                        //ARNOLD STORE
                        var TEST_VERTIGO_ID = _serviceBL.GetReportTestVertigo(_serviceId, componentId, componentReportId[0].ComponentId);
                        dsGetRepo = new DataSet();
                        DataTable dt_TEST_VERTIGO_ID = BLL.Utils.ConvertToDatatable(TEST_VERTIGO_ID);
                        dt_TEST_VERTIGO_ID.TableName = "dtTestVertigo";
                        dsGetRepo.Tables.Add(dt_TEST_VERTIGO_ID);
                        rp = new crTestDeVertigo();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.TEST_VERTIGO_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.EVA_CARDIOLOGICA_ID:
                        //ARNOLD STORE
                        var EVA_CARDIOLOGICA_ID = _serviceBL.GetReportEvaluacionCardiologia(_serviceId, Constants.EVA_CARDIOLOGICA_ID);
                        dsGetRepo = new DataSet();
                        DataTable dt_EVA_CARDIOLOGICA_ID = BLL.Utils.ConvertToDatatable(EVA_CARDIOLOGICA_ID);
                        dt_EVA_CARDIOLOGICA_ID.TableName = "dtEvaCardiologia";
                        dsGetRepo.Tables.Add(dt_EVA_CARDIOLOGICA_ID);
                        rp = new crEvaluacionCardiologicaSM();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.TEST_VERTIGO_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.EVA_OSTEO_ID:
                        //ARNOLD STORE
                        var EVA_OSTEO_ID = _serviceBL.GetReportEvaOsteoSanMartin(_serviceId, Constants.EVA_OSTEO_ID);
                        dsGetRepo = new DataSet();
                        DataTable dt_EVA_OSTEO_ID = BLL.Utils.ConvertToDatatable(EVA_OSTEO_ID);
                        dt_EVA_OSTEO_ID.TableName = "dtEvaOsteo";
                        dsGetRepo.Tables.Add(dt_EVA_OSTEO_ID);
                        rp = new crOsteoSanMartin();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.EVA_OSTEO_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.HISTORIA_CLINICA_PSICOLOGICA_ID:
                        //ARNOLD STORE
                        var HISTORIA_CLINICA_PSICOLOGICA_ID = _serviceBL.GetHistoriaClinicaPsicologica(_serviceId, Constants.HISTORIA_CLINICA_PSICOLOGICA_ID);
                        dsGetRepo = new DataSet();
                        dsGetRepo1 = new DataSet();
                        DataTable dt_HISTORIA_CLINICA_PSICOLOGICA_ID = BLL.Utils.ConvertToDatatable(HISTORIA_CLINICA_PSICOLOGICA_ID);
                        DataTable dt_HISTORIA_CLINICA_PSICOLOGICA_ID_2 = BLL.Utils.ConvertToDatatable(HISTORIA_CLINICA_PSICOLOGICA_ID);
                        if (pintIdCrystal == 42)
                        {
                            dt_HISTORIA_CLINICA_PSICOLOGICA_ID.TableName = "dtHistoriaClinicaPsicologica";
                            dsGetRepo.Tables.Add(dt_HISTORIA_CLINICA_PSICOLOGICA_ID);
                            rp = new crApendice04_Psico_01();
                            objDiskOpt = GenerarReporteUnido(rp, dsGetRepo, _ruta, objDiskOpt, Constants.HISTORIA_CLINICA_PSICOLOGICA_ID + "01.pdf");
                            filesName.Add(objDiskOpt.DiskFileName);


                            dt_HISTORIA_CLINICA_PSICOLOGICA_ID_2.TableName = "dtHistoriaClinicaPsicologica_Apendice04";
                            dsGetRepo1.Tables.Add(dt_HISTORIA_CLINICA_PSICOLOGICA_ID_2);
                            rp = new crApendice04_Psico_02();
                            objDiskOpt = GenerarReporteUnido(rp, dsGetRepo1, _ruta, objDiskOpt, Constants.HISTORIA_CLINICA_PSICOLOGICA_ID + "02.pdf");
                            filesName.Add(objDiskOpt.DiskFileName);
                            unirPdfS = filesName.ToList();
                            UnisReportes(unirPdfS, Constants.HISTORIA_CLINICA_PSICOLOGICA_ID, mergeExPDF);
                        }
                        else if (pintIdCrystal == 55)
                        {
                            dt_HISTORIA_CLINICA_PSICOLOGICA_ID.TableName = "dtHistoriaClinicaPsicologica";
                            dsGetRepo.Tables.Add(dt_HISTORIA_CLINICA_PSICOLOGICA_ID);
                            rp = new crHistoriaClinicaPsicologica_GOLD();
                            objDiskOpt = GenerarReporteUnido(rp, dsGetRepo, _ruta, objDiskOpt, Constants.PSICOLOGIA_ID + "01.pdf");
                            filesName.Add(objDiskOpt.DiskFileName);

                            dt_HISTORIA_CLINICA_PSICOLOGICA_ID_2.TableName = "dtHistoriaClinicaPsicologica2_GOLD";
                            dsGetRepo1.Tables.Add(dt_HISTORIA_CLINICA_PSICOLOGICA_ID_2);
                            rp = new crHistoriaClinicaPsicologica2_GOLD();
                            objDiskOpt = GenerarReporteUnido(rp, dsGetRepo1, _ruta, objDiskOpt, Constants.PSICOLOGIA_ID + "02.pdf");
                            filesName.Add(objDiskOpt.DiskFileName);
                            unirPdfS = filesName.ToList();
                            UnisReportes(unirPdfS, Constants.PSICOLOGIA_ID, mergeExPDF);

                        }
                        else if (pintIdCrystal == 61)
                        {
                            dt_HISTORIA_CLINICA_PSICOLOGICA_ID.TableName = "dtHistoriaClinicaPsicologica";
                            dsGetRepo.Tables.Add(dt_HISTORIA_CLINICA_PSICOLOGICA_ID);
                            rp = new crHistoriaClinicaPsicologicaHudbay1();
                            objDiskOpt = GenerarReporteUnido(rp, dsGetRepo, _ruta, objDiskOpt, Constants.PSICOLOGIA_ID + "01.pdf");
                            filesName.Add(objDiskOpt.DiskFileName);

                            dt_HISTORIA_CLINICA_PSICOLOGICA_ID_2.TableName = "dtHistoriaClinicaPsicologica_2";
                            dsGetRepo1.Tables.Add(dt_HISTORIA_CLINICA_PSICOLOGICA_ID_2);
                            rp = new crHistoriaClinicaPsicologicaHudbay2();
                            objDiskOpt = GenerarReporteUnido(rp, dsGetRepo1, _ruta, objDiskOpt, Constants.PSICOLOGIA_ID + "02.pdf");
                            filesName.Add(objDiskOpt.DiskFileName);
                            unirPdfS = filesName.ToList();
                            UnisReportes(unirPdfS, Constants.HISTORIA_CLINICA_PSICOLOGICA_ID, mergeExPDF);

                        }
                        else
                        {
                            dt_HISTORIA_CLINICA_PSICOLOGICA_ID.TableName = "dtHistoriaClinicaPsicologica";
                            dsGetRepo.Tables.Add(dt_HISTORIA_CLINICA_PSICOLOGICA_ID);
                            rp = new crHistoriaClinicaPsicologica();
                            objDiskOpt = GenerarReporteUnido(rp, dsGetRepo, _ruta, objDiskOpt, Constants.HISTORIA_CLINICA_PSICOLOGICA_ID + "01.pdf");
                            filesName.Add(objDiskOpt.DiskFileName);

                            dt_HISTORIA_CLINICA_PSICOLOGICA_ID_2.TableName = "dtHistoriaClinicaPsicologica_2";
                            dsGetRepo1.Tables.Add(dt_HISTORIA_CLINICA_PSICOLOGICA_ID_2);
                            rp = new crHistoriaClinicaPsicologica2();
                            objDiskOpt = GenerarReporteUnido(rp, dsGetRepo1, _ruta, objDiskOpt, Constants.HISTORIA_CLINICA_PSICOLOGICA_ID + "02.pdf");
                            filesName.Add(objDiskOpt.DiskFileName);
                            unirPdfS = filesName.ToList();
                            UnisReportes(unirPdfS, Constants.HISTORIA_CLINICA_PSICOLOGICA_ID, mergeExPDF);
                        }
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.EVA_NEUROLOGICA_ID:
                        //ARNOLD STORE
                        var EVA_NEUROLOGICA_ID = _serviceBL.GetReportEvaNeurologica(_serviceId, Constants.EVA_NEUROLOGICA_ID);
                        dsGetRepo = new DataSet();
                        DataTable dt_EVA_NEUROLOGICA_ID = BLL.Utils.ConvertToDatatable(EVA_NEUROLOGICA_ID);
                        dt_EVA_NEUROLOGICA_ID.TableName = "dtEvaNeurologica";
                        dsGetRepo.Tables.Add(dt_EVA_NEUROLOGICA_ID);
                        rp = new crEvaluacionNeurologica();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.EVA_NEUROLOGICA_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.SINTOMATICO_RESPIRATORIO:
                        //ARNOLD STORE
                        var SINTOMATICO_RESPIRATORIO = _serviceBL.ReporteSintomaticoRespiratorio(_serviceId, Constants.SINTOMATICO_RESPIRATORIO);
                        dsGetRepo = new DataSet();
                        DataTable dt_SINTOMATICO_RESPIRATORIO = BLL.Utils.ConvertToDatatable(SINTOMATICO_RESPIRATORIO);
                        dt_SINTOMATICO_RESPIRATORIO.TableName = "dtSintomaticoRes";
                        dsGetRepo.Tables.Add(dt_SINTOMATICO_RESPIRATORIO);
                        rp = new crSintomaticoResp();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.SINTOMATICO_RESPIRATORIO);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.FICHA_OTOSCOPIA:
                        //ARNOLD STORE
                        var FICHA_OTOSCOPIA = _serviceBL.GetReportFichaOtoscopia(_serviceId, Constants.FICHA_OTOSCOPIA);
                        dsGetRepo = new DataSet();
                        DataTable dt_FICHA_OTOSCOPIA = BLL.Utils.ConvertToDatatable(FICHA_OTOSCOPIA);
                        dt_FICHA_OTOSCOPIA.TableName = "dtOtoscopia";
                        dsGetRepo.Tables.Add(dt_FICHA_OTOSCOPIA);
                        rp = new crEvaluacionNeurologica();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.FICHA_OTOSCOPIA);
                        rp.Close();
                        rp.Dispose();
                        break;
                        
                    case Constants.SOMNOLENCIA_ID:
                        
                        var servicesId11 = new List<string>();
                        servicesId11.Add(_serviceId);
                        var componentReportId11 = _serviceBL.ObtenerIdsParaImportacionExcel(servicesId11, 11);
                        //ARNOLD STORE
                        var SOMNOLENCIA_ID = _serviceBL.ReporteSomnolencia(_serviceId, componentId, componentReportId11[0].ComponentId);
                        dsGetRepo = new DataSet();
                        DataTable dt_SOMNOLENCIA_ID = BLL.Utils.ConvertToDatatable(SOMNOLENCIA_ID);
                        dt_SOMNOLENCIA_ID.TableName = "dtSomnolencia";
                        dsGetRepo.Tables.Add(dt_SOMNOLENCIA_ID);
                        rp = new crTestEpwotrh();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.SOMNOLENCIA_ID);
                        rp.Close();
                        rp.Dispose();
                        break;
                        
                    case Constants.ACUMETRIA_ID:
                        //OLD
                        var ACUMETRIA_ID = _serviceBL.ReporteAcumetria(_serviceId, Constants.ACUMETRIA_ID);
                        dsGetRepo = new DataSet();
                        DataTable dt_ACUMETRIA_ID = BLL.Utils.ConvertToDatatable(ACUMETRIA_ID);
                        dt_ACUMETRIA_ID.TableName = "dtAcumetria";
                        dsGetRepo.Tables.Add(dt_ACUMETRIA_ID);
                        rp = new crFichaAcumetria();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.ACUMETRIA_ID);
                        rp.Close();
                        rp.Dispose();
                        break;
                        
                    case Constants.EVA_ERGONOMICA_ID:
                        //ARNOLD STORE
                        var EVA_ERGONOMICA_ID = _serviceBL.ReporteErgnomia(_serviceId, Constants.EVA_ERGONOMICA_ID);
                        dsGetRepo = new DataSet();
                        DataTable dt_EVA_ERGONOMICA_ID = BLL.Utils.ConvertToDatatable(EVA_ERGONOMICA_ID);
                        dt_EVA_ERGONOMICA_ID.TableName = "dtErgonomia";
                        dsGetRepo.Tables.Add(dt_EVA_ERGONOMICA_ID);
                        rp = new crEvaluacionErgonomica01();
                        objDiskOpt = GenerarReporteUnido(rp, dsGetRepo, _ruta, objDiskOpt, Constants.EVA_ERGONOMICA_ID + "01.pdf");
                        filesName.Add(objDiskOpt.DiskFileName);

                        rp = new crEvaluacionErgonomica02();
                        objDiskOpt = GenerarReporteUnido(rp, dsGetRepo, _ruta, objDiskOpt, Constants.EVA_ERGONOMICA_ID + "02.pdf");
                        filesName.Add(objDiskOpt.DiskFileName);

                        rp = new crEvaluacionErgonomica03();
                        objDiskOpt = GenerarReporteUnido(rp, dsGetRepo, _ruta, objDiskOpt, Constants.EVA_ERGONOMICA_ID + "03.pdf");
                        filesName.Add(objDiskOpt.DiskFileName);
                        unirPdfS = filesName.ToList();
                        UnisReportes(unirPdfS, Constants.EVA_ERGONOMICA_ID, mergeExPDF);
                        rp.Close();
                        rp.Dispose();
                        break;


                    case Constants.OTOSCOPIA_ID:
                        //ARNOLD STORE
                        var OTOSCOPIA_ID = _serviceBL.ReporteOtoscopia(_serviceId, Constants.OTOSCOPIA_ID);
                        dsGetRepo = new DataSet();
                        DataTable dt_OTOSCOPIA_ID = BLL.Utils.ConvertToDatatable(OTOSCOPIA_ID);
                        dt_OTOSCOPIA_ID.TableName = "dtOtoscopia";
                        dsGetRepo.Tables.Add(dt_OTOSCOPIA_ID);
                        rp = new crFichaOtoscopia();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.OTOSCOPIA_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.SINTOMATICO_ID:
                        //ARNOLD STORE
                        var SINTOMATICO_ID = _serviceBL.ReporteSintomaticoRespiratorio(_serviceId, Constants.SINTOMATICO_ID);
                        dsGetRepo = new DataSet();
                        DataTable dt_SINTOMATICO_ID = BLL.Utils.ConvertToDatatable(SINTOMATICO_ID);
                        dt_SINTOMATICO_ID.TableName = "dtSintomaticoRes";
                        dsGetRepo.Tables.Add(dt_SINTOMATICO_ID);
                        rp = new crSintomaticoResp();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.SINTOMATICO_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.LUMBOSACRA_ID:
                        //ARNOLD STORE
                        var LUMBOSACRA_ID = _serviceBL.ReporteLumboSaca(_serviceId, Constants.LUMBOSACRA_ID);
                        dsGetRepo = new DataSet();

                        DataTable dt_LUMBOSACRA_ID = BLL.Utils.ConvertToDatatable(LUMBOSACRA_ID);
                        dt_LUMBOSACRA_ID.TableName = "dtLumboSacra";
                        dsGetRepo.Tables.Add(dt_LUMBOSACRA_ID);
                        rp = new crInformeRadiologicoLumbar();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.LUMBOSACRA_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    #region Psico Holo
                    case Constants.AutoevaTrabEquipo_ID:
                        var AutoevaTrabEquipo = _serviceBL.ReporteAutoevaTrabEquipo(_serviceId, Constants.AutoevaTrabEquipo_ID);

                        dsGetRepo = new DataSet();
                        DataTable dt_AutoevaTrabEquipo = BLL.Utils.ConvertToDatatable(AutoevaTrabEquipo);
                        dt_AutoevaTrabEquipo.TableName = "dtAutoevaTrabEquipo";
                        dsGetRepo.Tables.Add(dt_AutoevaTrabEquipo);

                        rp = new crAutoevaTrabEquipo();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = _ruta + serviceId + "-" + Constants.AutoevaTrabEquipo_ID + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                        break;


                    case Constants.Cuestionariogradodeafectividad_ID:
                        var Cuestionariogradodeafectividad = _serviceBL.ReporteCuestionariogradodeafectividad(_serviceId, Constants.Cuestionariogradodeafectividad_ID);

                        dsGetRepo = new DataSet();
                        DataTable dt_Cuestionariogradodeafectividad = BLL.Utils.ConvertToDatatable(Cuestionariogradodeafectividad);
                        dt_Cuestionariogradodeafectividad.TableName = "dtCuestionariogradodeafectividad";
                        dsGetRepo.Tables.Add(dt_Cuestionariogradodeafectividad);

                        rp = new crCuestionariogradodeafectividad();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = _ruta + serviceId + "-" + Constants.Cuestionariogradodeafectividad_ID + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                        break;

                    case Constants.Fobiasocial01_ID:
                        var Fobiasocial01 = _serviceBL.ReporteFobiaSocial01(_serviceId, Constants.Fobiasocial01_ID);

                        dsGetRepo = new DataSet();
                        DataTable dt_Fobiasocial01 = BLL.Utils.ConvertToDatatable(Fobiasocial01);
                        dt_Fobiasocial01.TableName = "dtFobiasocial01";
                        dsGetRepo.Tables.Add(dt_Fobiasocial01);

                        rp = new crFobiasocial01();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = _ruta + serviceId + "-" + Constants.Fobiasocial01_ID + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                        break;

                    case Constants.Fobiasocial02_ID:
                        var Fobiasocial02 = _serviceBL.ReporteFobiaSocial02(_serviceId, Constants.Fobiasocial02_ID);

                        dsGetRepo = new DataSet();
                        DataTable dt_Fobiasocial02 = BLL.Utils.ConvertToDatatable(Fobiasocial02);
                        dt_Fobiasocial02.TableName = "dtFobiasocial02";
                        dsGetRepo.Tables.Add(dt_Fobiasocial02);

                        rp = new crFobiasocial02();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = _ruta + serviceId + "-" + Constants.Fobiasocial02_ID + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                        break;

                    case Constants.Testdepersonalldad_ID:
                        var Testdepersonalldad = _serviceBL.ReporteTestdepersonalldad(_serviceId, Constants.Testdepersonalldad_ID);

                        dsGetRepo = new DataSet();
                        DataTable dt_Testdepersonalldad = BLL.Utils.ConvertToDatatable(Testdepersonalldad);
                        dt_Testdepersonalldad.TableName = "dtTestdepersonalldad";
                        dsGetRepo.Tables.Add(dt_Testdepersonalldad);

                        rp = new crTestdepersonalldad();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = _ruta + serviceId + "-" + Constants.Testdepersonalldad_ID + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                        break;

                    case Constants.FobiasocialAdmin_ID:
                        var FobiasocialAdmin = _serviceBL.ReporteFobiasocialAdmin(_serviceId, Constants.FobiasocialAdmin_ID);

                        dsGetRepo = new DataSet();
                        DataTable dt_FobiasocialAdmin = BLL.Utils.ConvertToDatatable(FobiasocialAdmin);
                        dt_FobiasocialAdmin.TableName = "dtFobiasocialAdmin";
                        dsGetRepo.Tables.Add(dt_FobiasocialAdmin);

                        rp = new crFobiasocialAdmin();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = _ruta + serviceId + "-" + Constants.FobiasocialAdmin_ID + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                        break;
                    case Constants.Testdefatiga_ID:
                        var Testdefatiga = _serviceBL.ReporteTestdefatiga(_serviceId, Constants.Testdefatiga_ID);

                        dsGetRepo = new DataSet();
                        DataTable dt_Testdefatiga = BLL.Utils.ConvertToDatatable(Testdefatiga);
                        dt_Testdefatiga.TableName = "dtTestdefatiga";
                        dsGetRepo.Tables.Add(dt_Testdefatiga);

                        rp = new crTestdefatiga();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.Testdefatiga_ID);
                        break;

                    case Constants.Maslachestres_ID:
                        var Maslachestres = _serviceBL.ReporteMaslachestres(_serviceId, Constants.Maslachestres_ID);

                        dsGetRepo = new DataSet();
                        DataTable dt_Maslachestres = BLL.Utils.ConvertToDatatable(Maslachestres);
                        dt_Maslachestres.TableName = "dtMaslachestres";
                        dsGetRepo.Tables.Add(dt_Maslachestres);

                        rp = new crMaslachestres();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = _ruta + serviceId + "-" + Constants.Maslachestres_ID + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                        break;

                    case Constants.Testdeansiedad_ID:
                        var Testdeansiedad = _serviceBL.ReporteTestdeansiedad(_serviceId, Constants.Testdeansiedad_ID);

                        dsGetRepo = new DataSet();
                        DataTable dt_Testdeansiedad = BLL.Utils.ConvertToDatatable(Testdeansiedad);
                        dt_Testdeansiedad.TableName = "dtTestdeansiedad";
                        dsGetRepo.Tables.Add(dt_Testdeansiedad);

                        rp = new crTestdeansiedad();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = _ruta + serviceId + "-" + Constants.Testdeansiedad_ID + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                        break;

                    case Constants.Testdedepresion_ID:
                        var Testdedepresion = _serviceBL.ReporteTestdedepresion(_serviceId, Constants.Testdedepresion_ID);

                        dsGetRepo = new DataSet();
                        DataTable dt_Testdedepresion_ID = BLL.Utils.ConvertToDatatable(Testdedepresion);
                        dt_Testdedepresion_ID.TableName = "dtTestdedepresion";
                        dsGetRepo.Tables.Add(dt_Testdedepresion_ID);

                        rp = new crTestdedepresión();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = _ruta + serviceId + "-" + Constants.Testdedepresion_ID + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                        break;



                    case Constants.CuestionarioAutoeva_ID:
                        var CuestionarioAutoeva = _serviceBL.ReporteCuestionarioAutoeva(_serviceId, Constants.CuestionarioAutoeva_ID);

                        dsGetRepo = new DataSet();
                        DataTable dt_CuestionarioAutoeva_ID = BLL.Utils.ConvertToDatatable(CuestionarioAutoeva);
                        dt_CuestionarioAutoeva_ID.TableName = "dtCuestionarioAutoeva";
                        dsGetRepo.Tables.Add(dt_CuestionarioAutoeva_ID);

                        rp = new crCuestionarioAutoeva();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = _ruta + serviceId + "-" + Constants.CuestionarioAutoeva_ID + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                        break;


                    case Constants.CUESTIONARIO_ISTAS:
                        var CUESTIONARIO_ISTAS = _serviceBL.ReporteCuestionarioIstas(_serviceId, Constants.CUESTIONARIO_ISTAS);

                        dsGetRepo = new DataSet();

                        DataTable dt_CUESTIONARIO_ISTAS = BLL.Utils.ConvertToDatatable(CUESTIONARIO_ISTAS);
                        dt_CUESTIONARIO_ISTAS.TableName = "dtCuestionario_Istas";
                        dsGetRepo.Tables.Add(dt_CUESTIONARIO_ISTAS);

                        rp = new crCuestionario_Istas_1();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = _ruta + serviceId + "-" + Constants.CUESTIONARIO_ISTAS + "01.pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;

                        rp.Export();
                        rp.Close();

                        //rp = new Reports.crCuestionario_Istas_2();
                        //rp.SetDataSource(dsGetRepo);
                        //rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        //rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        //objDiskOpt = new DiskFileDestinationOptions();
                        //objDiskOpt.DiskFileName = _ruta + serviceId + "-" + Constants.CUESTIONARIO_ISTAS + "02.pdf";
                        //_filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        //rp.ExportOptions.DestinationOptions = objDiskOpt;

                        //rp.Export();
                        //rp.Close();
                        break;


                    #endregion

                    case Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID:
                        //ARNOLD STORE
                        var TOXICOLOGICO_COCAINA_MARIHUANA_ID = _serviceBL.GetReportCocainaMarihuana(_serviceId, Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID);
                        dsGetRepo = new DataSet();
                        DataTable dt_TOXICOLOGICO_COCAINA_MARIHUANA_ID = BLL.Utils.ConvertToDatatable(TOXICOLOGICO_COCAINA_MARIHUANA_ID);
                        dt_TOXICOLOGICO_COCAINA_MARIHUANA_ID.TableName = "dtAutorizacionDosajeDrogas";
                        dsGetRepo.Tables.Add(dt_TOXICOLOGICO_COCAINA_MARIHUANA_ID);

                        if (pintIdCrystal == 48) { rp = new crApendice09_Drogas(); }
                        else { rp = new crCocainaMarihuana01(); }
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.AUDIO_COIMOLACHE:
                        //ARNOLD STORE
                        var AUDIO_COIMOLACHE_ID = _serviceBL.GetAudiometriaCoimolache(_serviceId, Constants.AUDIO_COIMOLACHE);
                        dsGetRepo = new DataSet();
                        DataTable dtAUDIO_COIMOLACHE_ID = BLL.Utils.ConvertToDatatable(AUDIO_COIMOLACHE_ID);
                        dtAUDIO_COIMOLACHE_ID.TableName = "dtAudioCoimo";
                        dsGetRepo.Tables.Add(dtAUDIO_COIMOLACHE_ID);
                        rp = new crCuestionarioAudioCoimolache();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, Constants.AUDIO_COIMOLACHE);
                        rp.Close();
                        rp.Dispose();
                        break;

                    case "N009-ME000000337":
                        //ARNOLD STORE
                        var Cusestionario_audiometria = _serviceBL.GetCustionarioAudiometria(_serviceId, "N009-ME000000337");
                        dsGetRepo = new DataSet();
                        DataTable dtCuestionario_Audio = BLL.Utils.ConvertToDatatable(Cusestionario_audiometria);
                        dtCuestionario_Audio.TableName = "dtCuestAudio";
                        dsGetRepo.Tables.Add(dtCuestionario_Audio);
                        rp = new CuestionarioAudiometria();
                        objDiskOpt = GenerarReporteUnido(rp, dsGetRepo, _ruta, objDiskOpt, "N009-ME000000337" + "01.pdf");
                        filesName.Add(objDiskOpt.DiskFileName);
                        rp.Close();

                        rp = new CuestionarioAudiometria02();
                        objDiskOpt = GenerarReporteUnido(rp, dsGetRepo, _ruta, objDiskOpt, "N009-ME000000337" + "01.pdf");
                        filesName.Add(objDiskOpt.DiskFileName);
                        unirPdfS = filesName.ToList();
                        UnisReportes(unirPdfS, "N009-ME000000337", mergeExPDF);
                        rp.Close();
                        rp.Dispose();
                        break;


                    case "N009-ME000000407":
                        //ANOLD STORE
                        var Carne_Vacuna = _serviceBL.GetCarneVacuna(_serviceId, "N009-ME000000407");
                        dsGetRepo = new DataSet();
                        DataTable dtCarneVacuna = BLL.Utils.ConvertToDatatable(Carne_Vacuna);
                        dtCarneVacuna.TableName = "dtCarneVacuna";
                        dsGetRepo.Tables.Add(dtCarneVacuna);
                        rp = new crCarneDeVacunas();
                        GenerarReporte(rp, dsGetRepo, _ruta, objDiskOpt, "N009-ME000000407");
                        rp.Close();
                        rp.Dispose();
                        break;

                    case Constants.INFORME_ANEXO_312:
                        GenerateAnexo312(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_ANEXO_312))); //ARNOLD STORE
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_FICHA_MEDICA_TRABAJADOR:
                        var DatosServicio = _serviceBL.GetServiceShort(_serviceId); //ARNOLD STORE
                        var ruta1 = Common.Utils.GetApplicationConfigValue("InformeTrab1").ToString();
                        //ARNOLD STORE
                        GenerateInformeMedicoTrabajador(string.Format("{0}.pdf", Path.Combine(ruta1, DatosServicio.Empresa + "-" + DatosServicio.Paciente + "-" + Constants.INFORME_FICHA_MEDICA_TRABAJADOR + "-" + DatosServicio.FechaServicio.Value.ToString("dd MMMM,  yyyy"))));
                        GenerateInformeMedicoTrabajador(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_FICHA_MEDICA_TRABAJADOR)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_FICHA_MEDICA_TRABAJADOR_2:
                        var DatosServicio1 = _serviceBL.GetServiceShort(_serviceId);
                        var ruta2 = Common.Utils.GetApplicationConfigValue("InformeTrab2").ToString();
                        CreateFichaMedicaTrabajador2(string.Format("{0}.pdf", Path.Combine(ruta2, DatosServicio1.Empresa + "-" + DatosServicio1.Paciente + "-" + Constants.INFORME_FICHA_MEDICA_TRABAJADOR_2 + "-" + DatosServicio1.FechaServicio.Value.ToString("dd MMMM,  yyyy"))));
                        
                        //ARNOLD STORE
                        CreateFichaMedicaTrabajador2(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_FICHA_MEDICA_TRABAJADOR_2)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_FICHA_MEDICA_TRABAJADOR_3:
                        var DatosServicio3 = _serviceBL.GetServiceShort(_serviceId);
                        var ruta3 = Common.Utils.GetApplicationConfigValue("InformeTrab3").ToString();
                        //ARNOLD STORE
                        CreateFichaMedicaTrabajador3(string.Format("{0}.pdf", Path.Combine(ruta3, DatosServicio3.Empresa + "-" + DatosServicio3.Paciente + "-" + Constants.INFORME_FICHA_MEDICA_TRABAJADOR_3 + "-" + DatosServicio3.FechaServicio.Value.ToString("dd MMMM,  yyyy"))));

                        CreateFichaMedicaTrabajador3(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_FICHA_MEDICA_TRABAJADOR_3)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_ANEXO_7C:
                        GenerateAnexo7C(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_ANEXO_7C)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;

                    case Constants.APTITUD_YANACOCHA:
                        GenerateAptitudYanacocha(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.APTITUD_YANACOCHA)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    //Agregar case
                    case Constants.ANSIEDAD_ZUNG:
                        //ARNOLD STORE
                        GenerateAnsiedadZung(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.ANSIEDAD_ZUNG)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.ESCALA_FATIGA:
                        GenerateEscalafatiga(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.ESCALA_FATIGA)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INV_MASLACH:
                        GenerateInventarioMaslach(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INV_MASLACH)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.TEST_SOMNOLENCIA:
                        GenerateTestSomnolencia(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.TEST_SOMNOLENCIA)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_ANEXO_16_COIMOLACHE:
                        GenerateAnexo16Coimolache(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_ANEXO_16_COIMOLACHE)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_ANEXO_16_PACASMAYO:
                        GenerateAnexo16Pacasmayo(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_ANEXO_16_PACASMAYO)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_LABORATORIO_ID_ASIST:
                        //ARNOLD STORE
                        Generate_Laboratorio_Asistencial(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_LABORATORIO_ID_ASIST)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_ANEXO_16_MINSURSANRAFAEL:
                        GenerateAnexo16MinsurSanRafael(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_ANEXO_16_MINSURSANRAFAEL)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_ANEXO_16_YANACOCHA:
                        GenerateAnexo16Yanacocha(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_ANEXO_16_YANACOCHA)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_ANEXO_16_SHAHUINDO:
                        GenerateAnexo16Shahuindo(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_ANEXO_16_SHAHUINDO)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_ANEXO_16_GOLD_FIELD:
                        GenerateAnexo16GoldField(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_ANEXO_16_GOLD_FIELD)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_ANEXO_16_HUDBAY:
                        GenerateAnexo16Hudbay(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_ANEXO_16_HUDBAY)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FICHA_INVESTIGACION_COVID_ID:
                        //ARNOLD STORE
                        GenerateInvestigacionEpidemiologicaCOVID19(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_INVESTIGACION_COVID_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                        //NEW
                    case Constants.FICHA_DE_INVESTIGACIÓN_CLÍNICO_EPIDEMIOLÓGICO_COVID_19_ÚNICA_ID:
                        GenerateFicha_Investigacion_Clinica_Epidemiologica_Unica_COV(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_DE_INVESTIGACIÓN_CLÍNICO_EPIDEMIOLÓGICO_COVID_19_ÚNICA_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FICHA_INVESTIGACION_COVID_MOLECULAR_ID:
                        GenerateInvestigacionEpidemiologicaMOLECULARCOVID19(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_INVESTIGACION_COVID_MOLECULAR_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.RESULTADO_PRUEBA_DE_ANTIGENO_COVID_19_ID:
                        Generate_FICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_ANTIGENO_COVID_19(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.RESULTADO_PRUEBA_DE_ANTIGENO_COVID_19_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FICHA_INVESTIGACION_COVID_ANTIGENO_ID:
                        GenerateInvestigacionEpidemiologicaANTIGENOCOVID19(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_INVESTIGACION_COVID_ANTIGENO_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CONSENTIMIENTO_INFORMADO_PARA_EXAMENES_DE_PRUEBA_ANTIGENO_COVID_19_ID_OC:
                        GenerateConsentimientoInformadoExamenANTIGENOCovid19(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CONSENTIMIENTO_INFORMADO_PARA_EXAMENES_DE_PRUEBA_ANTIGENO_COVID_19_ID_OC)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CONSTANCIA_DE_SALUD_PARA_PRUEBA_COVID_ID:
                        GenerateConstancia_de_Salud_Para_Prueba_Covid(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CONSTANCIA_DE_SALUD_PARA_PRUEBA_COVID_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    //Constancia_de_Salud_Para_Prueba_Covid
                    case Constants.Anticuerpos_Neutralizantes_COVID_19_ID:
                        Generate_Anticuerpos_Neutralizantes_COVID_19_OCUP(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.Anticuerpos_Neutralizantes_COVID_19_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FICHA_SINTOMATOLOGIA_COVID_19_ID:
                        GenerateFichaSintomatologicaCovid19(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_SINTOMATOLOGIA_COVID_19_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CERTIFICADO_DE_EVALUACIÓN_DE_SALUD_ID:
                        GenerateCertificado_evaluacion_salud_Covid(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CERTIFICADO_DE_EVALUACIÓN_DE_SALUD_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.DECLARACION_JURADA_COVID_19_ID:
                        GenerateDeclaracionJuradaCovid19(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.DECLARACION_JURADA_COVID_19_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.PRUEBA_RAPIDA_COVID_19_ID:
                        GeneratePruebaRapidaCovid19(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.PRUEBA_RAPIDA_COVID_19_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_RÁPIDA_COVID_19_ID:
                        GenerateFICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_RÁPIDA_COVID_19(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_RÁPIDA_COVID_19_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_MOLECULAR_COVID_19_ID:
                        GenerateFICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_MOLECULAR_COVID_19(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_MOLECULAR_COVID_19_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CONSENTIMIENTO_INFORMADO_PARA_EXAMENES_DE_COVID_19_ID_OC:
                        GenerateConsentimientoInformadoExamenCovid19(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CONSENTIMIENTO_INFORMADO_PARA_EXAMENES_DE_COVID_19_ID_OC)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CONSENTIMIENTO_INFORMADO_PARA_EXAMENES_DE_PRUEBA_MOLECULAR_COVID_19_ID_OC:
                        GenerateConsentimientoInformadoExamenMOLECULARCovid19(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CONSENTIMIENTO_INFORMADO_PARA_EXAMENES_DE_PRUEBA_MOLECULAR_COVID_19_ID_OC)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CERTIFICADO_MEDICO_DE_BUEN_ESTADO_DE_SALUD_FÍSICA_ID:
                        GenerateCertificadoMédicodeBuenEstadodeSaludFísica(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CERTIFICADO_MEDICO_DE_BUEN_ESTADO_DE_SALUD_FÍSICA_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FICHA_SINTOMATOLOGÍA_COVID_19_REGRESO_TRABAJO_DJ_ID:
                        GenerateFichadeSintomatologiaCovid19ParaRegresoalTrabajoDeclaracionJurada(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_SINTOMATOLOGÍA_COVID_19_REGRESO_TRABAJO_DJ_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FORMATO_EVALUACIÓN_SALUD_RÁPIDA_FRENTE_COVID_19_ID:
                        GenerateFORMATO_DE_EVALUACIÓN_DE_SALUD_RÁPIDA_FRENTE_A_COVID_19(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FORMATO_EVALUACIÓN_SALUD_RÁPIDA_FRENTE_COVID_19_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_CLINICO:
                        GenerateInformeExamenClinico(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_CLINICO)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    //case Constants.INFORME_LABORATORIO_CLINICO:
                    //    GenerateLaboratorioReport(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_LABORATORIO_CLINICO)));
                    //    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                    //    break;
                    //// ARNOLD 
                    case Constants.CULTIVO_DE_SALMONELLA_Y_SHINGELLA_ID:
                        Generate_CULTIVO_DE_SALMONELLA_Y_SHINGELLA(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CULTIVO_DE_SALMONELLA_Y_SHINGELLA_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.SECRESION_NASOFARINGEA_ID:
                        Generate_SECRESION_NASOFARINGEA(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.SECRESION_NASOFARINGEA_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FICHA_SAS_ID:
                        GenerateInformeSAS(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_SAS_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.MI_EXAMEN:
                        GenerateMiExamen(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.MI_EXAMEN)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;

                    case Constants.PARASITOLOGICO_COPROCULTIVO_CIELO_AZUL:
                        Generate_PARASITOLOGICO_COPROCULTIVO_CIELO_AZUL(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.PARASITOLOGICO_COPROCULTIVO_CIELO_AZUL)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.AGLUTINACIONES_KOH_SECRECION:
                        Generate_AGLUTINACIONES_KOH_SECRECION(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.AGLUTINACIONES_KOH_SECRECION)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;

                    case Constants.PARASITOLOGICO_COPROCULTIVO_LUMINA:
                        Generate_PARASITOLOGICO_COPROCULTIVO_LUMINA(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.PARASITOLOGICO_COPROCULTIVO_LUMINA)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.AGLUTINACIONES_KOH_SECRECION_LUMINA:
                        Generate_AGLUTINACIONES_KOH_SECRECION_LUMINA(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.AGLUTINACIONES_KOH_SECRECION_LUMINA)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;

                    case Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_ID:
                        GenerateCertificadoPsicosensometricoDatos(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.T_A_1_8_ID:
                        GenerateAltura_Fisica_F_Yanacocha(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.T_A_1_8_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.OSTEO_MB_ID:
                        GenerateOsteMuscular_Mibanco(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.OSTEO_MB_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.ACCIDENTES_DE_TRABAJO_F1:
                        GenerateAccidentesTrabajoF1(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.ACCIDENTES_DE_TRABAJO_F1)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.ACCIDENTES_DE_TRABAJO_F2:
                        GenerateAccidentesTrabajoF2(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.ACCIDENTES_DE_TRABAJO_F2)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.EXAMEN_SUF_MED__OPERADORES_ID:
                        GenerateExamenSuficienciaMedicaOperadores(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.EXAMEN_SUF_MED__OPERADORES_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_MEDICO_OCUPACIONAL_COSAPI:
                        //ARNOLD STORE
                        GenerateInformeMedicoOcupacional_Cosapi(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_MEDICO_OCUPACIONAL_COSAPI)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.EVALUACION_NEUROLOGICA_INDIVIDUALIZADA_COSAPI_ID:
                        GenerateEvaluacionNEurologicaIndividualizada_Cosapi(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.EVALUACION_NEUROLOGICA_INDIVIDUALIZADA_COSAPI_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CERTIFICADO_APTITUD_MEDICO:
                        GenerateCertificadoAptitudMedicoOcupacional_Cosapi(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CERTIFICADO_APTITUD_MEDICO)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CERTIFICADO_DE_APTITUD_MEDICO_OCUPACIONAL_COSAPI_ID:
                        GenerateCertificadoAptitudMedicoOcupacional_Cosapi_New(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CERTIFICADO_DE_APTITUD_MEDICO_OCUPACIONAL_COSAPI_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.EXCEPCIONES_LABORATORIO_ID:
                        GenerateExoneraxionLaboratorio(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.EXCEPCIONES_LABORATORIO_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.EXCEPCIONES_RX_ID:
                        GenerateExoneraxionPlacaTorax(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.EXCEPCIONES_RX_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.EXCEPCIONES_ESPIROMETRIA_ID:
                        GenerateExoneracionEspirometria(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.EXCEPCIONES_ESPIROMETRIA_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_MEDICO_SALUD_OCUPACIONAL_EXAMEN_MEDICO_ANUAL:
                        GenerateInformeMedicoOcupacionalExamenMedicoAnual(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_MEDICO_SALUD_OCUPACIONAL_EXAMEN_MEDICO_ANUAL)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.ANEXO_8_INFORME_MEDICO_OCUPACIONAL:
                        GenerateAnexo8InformeMedicoOcupacional(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.ANEXO_8_INFORME_MEDICO_OCUPACIONAL)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;

                    case Constants.EXCEPCIONES_RX_AUTORIZACION_ID:
                        var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
                        if (datosP.Genero.ToUpper() == "FEMENINO")
                        {
                            GenerateDeclaracionJuradaRX(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.EXCEPCIONES_RX_AUTORIZACION_ID)));
                            _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        }
                        break;
                    case Constants.AUTORIZACION_PRUEBA_EMBARAZO_HUDBAY_ID:
                        GenerateAutorizacionPruebaEmbarazo(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.AUTORIZACION_PRUEBA_EMBARAZO_HUDBAY_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CONSENTIMIENTO_INFORMADO_HUDBAY:
                        GenerateConsentimientoInformadoAccesoHistoriaClinica(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CONSENTIMIENTO_INFORMADO_HUDBAY)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_MEDICO_APTITUD_OCUPACIONAL_EMPRESA_HUDBAY:
                        GenerateInformeMedicoAptitudOcupacional(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_MEDICO_APTITUD_OCUPACIONAL_EMPRESA_HUDBAY)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_RESULTADOS_EVALUACION_MEDICA:
                        GenerateInformeResultadosAutorizacion(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_RESULTADOS_EVALUACION_MEDICA)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CONSENTIMIENTO_INFORMADO_EXAMEN_MEDICO_COIMOLACHE:
                        GenerateInformeResultadosAutorizacionCoimolache(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CONSENTIMIENTO_INFORMADO_EXAMEN_MEDICO_COIMOLACHE)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CONSTANCIA_DE_SUFICIENCIA_MEDICA_PARA_BRIGADISTAS_ID:
                        //ARNOLD STORE
                        GenerateConstanciadeSuficienciaMedicaParaBrigadistas(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CONSTANCIA_DE_SUFICIENCIA_MEDICA_PARA_BRIGADISTAS_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.LABORATORIO_CONSENTIMIENTO_INFORMADO_VIH_ID:
                        GenerateConsentimientoInformadoCIH(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.LABORATORIO_CONSENTIMIENTO_INFORMADO_VIH_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.TRABAJO_ALTURA1_8_ID:
                        GenerateCertificado_Suficiencia_Medica_Trabajos_Altura(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.TRABAJO_ALTURA1_8_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.ENFERMEDAD_CHAGAS_ID:
                        GenerateEnfermedad_Chagas(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.ENFERMEDAD_CHAGAS_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.TEST_EXPOSICION_CALOR_ID:
                        GenerateTest_Exposicion_Al_Calor(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.TEST_EXPOSICION_CALOR_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CONSENTIMIENTO_RX_ID:
                        GenerateAutorizacion_Rx(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CONSENTIMIENTO_RX_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CONSENTIMIENTO_VIH_ID:
                        GenerateAutorizacion_VIH(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CONSENTIMIENTO_VIH_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FICHA_SUFICIENCIA_MEDICA_ID:
                        GenerateCertificadoSuficienciaMedicaTC(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_SUFICIENCIA_MEDICA_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;

                    case Constants.INFORME_PSICOLOGICO_OCUPACIONAL_GOLDFIELDS:
                        GenerateInformePsicologicoGoldfieds(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_PSICOLOGICO_OCUPACIONAL_GOLDFIELDS)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_ID:
                        GenerateReportePsicologicoBrigadistas(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.REPORTE_PSICOLOGICO_DE_BRIGADISTAS_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FICHA_PSICOLOGICA_OCUPACIONAL_GOLDFIELDS:
                        GenerateFichaPsicologicaGoldfies(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_PSICOLOGICA_OCUPACIONAL_GOLDFIELDS)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID:
                        GenerateExamenOftalmologicoSimple(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID:
                        GenerateExamenOftalmologicoCompleto(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.RADIOGRAFIA_DE_TORAX_SIMPLE_ID:
                        GenerateRADIOGRAFIA_DE_TORAX_SIMPLE(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.RADIOGRAFIA_DE_TORAX_SIMPLE_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID:
                        GenerateEvaluavionOftalmologicaYanacocha(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_OFTALMOLOGICO_HUDBAY_ID:
                        GenerateInformeOftalmologicoHudbay(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_OFTALMOLOGICO_HUDBAY_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_CARDILÓGICO_HUDBAY_ID:
                        GenerateInformeCardiologicoHudbay(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_CARDILÓGICO_HUDBAY_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.D_J_PSICOLOGIA_COIMOLACHE_LA_ZANJA_ID:
                        GenerateDeclaracionJuradaCoimolacheLaZanja(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.D_J_PSICOLOGIA_COIMOLACHE_LA_ZANJA_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.ANEXO_3_EXO_RESP_YANACOCHA:
                        //ARNOLD STORE
                        GenerateAnexo3_Exoneracion_ResponsabilidadYanacocha(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.ANEXO_3_EXO_RESP_YANACOCHA)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.EXAMEN_MEDICO_VISITANTES_GOLDFIELDS_ID:
                        GenerateExamen_Medico_Visitantes_GoldFields(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.EXAMEN_MEDICO_VISITANTES_GOLDFIELDS_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.MARCOBRE_PASE_MEDICO:
                        GenerateMarcobrePaseMedico(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.MARCOBRE_PASE_MEDICO)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.REGISTRO_ENTREGA_INFORME_RESULTADOS_BUENAVENTURA:
                        GenerateRegistroInformeEMOBuenaventura(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.REGISTRO_ENTREGA_INFORME_RESULTADOS_BUENAVENTURA)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.DECLARACION_JURADA:
                        GenerateDeclaracionJurada(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.DECLARACION_JURADA)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_ID:
                        GenerateDeclaracionJuradaAntecedentesPersonales(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.DECLARACION_JURADA_ANTECEDENTES_PERSONALES_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.TOXICOLOGICO_COCAINA_MARIHUANA_T:
                        //ARNOLD STORE
                        GenerateTOXICOLOGICO_COCAINA_MARIHUANA_TODOS(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.TOXICOLOGICO_COCAINA_MARIHUANA_T)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_ID:
                        GenerateNotificacionCualitativosParaDrogasenOrina(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY_10_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;

                    case Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY:
                        GenerateConsentimientoInformadoToxicologia_Hudbay(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.TOXICOLOGICO_COCAINA_MARIHUANA_HUDBAY)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_METALES_PESADOS_1_HUDBAY:
                        GenerateInformeMetalesPesadosHudbay(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_METALES_PESADOS_1_HUDBAY)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FICHA_INMUNIZACIONES_HUDBAY_ID:
                        GenerateFichaInmunizaciones_Hudbay(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_INMUNIZACIONES_HUDBAY_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FICHA_MINITEST_PSIQUIATRICO_HUDBAY_ID:
                        GenerateMIniTestPsiquiatricoHudbay(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_MINITEST_PSIQUIATRICO_HUDBAY_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FICHA_PARA_MANIPULADOR_ALIMENTOS_HUDBAY_ID:
                        GenerateCuestionarioManipulador_de_Alimentos_Hudbay(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_PARA_MANIPULADOR_ALIMENTOS_HUDBAY_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_MEDICO_DE_APTITUD_OCUPACIONAL_PARA_LA_EMPRESA_:
                        GenerateInformeMedicoAptitudOcupacionaParaEmpresa(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_MEDICO_DE_APTITUD_OCUPACIONAL_PARA_LA_EMPRESA_)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.EVALUACION_NEUROLOGICA_TRABAJOS_ALTURA_HUDBAY_ID:
                        GenerateEvaluacionNeurologicaTrabajosAlturaEstructural_Hudbay(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.EVALUACION_NEUROLOGICA_TRABAJOS_ALTURA_HUDBAY_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.DESCRIPCION_ESPECIFICA_PUESTO_TRABAJO_ID:
                        GenerateDescripcionEspecificaPuestoTrabajo(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.DESCRIPCION_ESPECIFICA_PUESTO_TRABAJO_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.TEST_AUDIT_HUDBAY_ID:
                        GenerateTestAudit_Hudbay(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.TEST_AUDIT_HUDBAY_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.DECLARACION_JURADA_DE_SALUD_HUDBAY_ID:
                        GenerateDeclaracionJuradaDeSalud_Hudbay(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.DECLARACION_JURADA_DE_SALUD_HUDBAY_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.DECLARACION_JURADA_MUSCULOESQUELETICA_HUDBAY_ID:
                        GenerateDeclaracionJuradaMusculoEsqueletica_Hudbay(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.DECLARACION_JURADA_MUSCULOESQUELETICA_HUDBAY_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.DECLARACION_JURADA_DE_CUMPLIMIENTO_INSTRUCCIONES_PREVIOS_EMO_HUDBAY:
                        GenerateDeclaracionJuradaCumplimientoInstrucciones_Hudbay(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.DECLARACION_JURADA_DE_CUMPLIMIENTO_INSTRUCCIONES_PREVIOS_EMO_HUDBAY)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.EXAMEN_MEDICO_VISITA_HUDBAY_ID:
                        GenerateExamenMedicoVisita_Hudbay(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.EXAMEN_MEDICO_VISITA_HUDBAY_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_APTITUD_OCUPACIONL_PARA_TRABAJADOR_ID:
                        GenerateInformeMedicoAptitudOcupacionalParaTrabajador(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_APTITUD_OCUPACIONL_PARA_TRABAJADOR_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FICHA_AUDIOMETRICA_HUDBAY_ID:
                        GenerateFichaAudiometrica_Hudbay(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_AUDIOMETRICA_HUDBAY_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.DECLARACION_COMPROMISO_PERSONAL_PERDIDA_PESO_HUDBAY:
                        GenerateDeclaracionCompromisoPerdidaPeso_Hudbay(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.DECLARACION_COMPROMISO_PERSONAL_PERDIDA_PESO_HUDBAY)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.ENTREGA_DE_XAMEN_MEDICO_OCUPACIONAL:
                        GenerateEntregaExamenMedicoOcupacional(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.ENTREGA_DE_XAMEN_MEDICO_OCUPACIONAL)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_OTORRINOLARINGOLOGICO:
                        GenerateInforme_Otorrinolaringologico(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_OTORRINOLARINGOLOGICO)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.DECLARACION_JURADA_CUESTIONARIO:
                        GenerateDeclaracionJurada_Encuesta(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.DECLARACION_JURADA_CUESTIONARIO)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.EVALUACION_MEDICO_SAN_MARTIN_INFORME:
                        GenerateInforme_Resultados_San_Martinm(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.EVALUACION_MEDICO_SAN_MARTIN_INFORME)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.ALTURA_7D_ID:
                        GenerateAnexo16A(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.ALTURA_7D_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.APTITUD_PSICOSOMATICA_PARA_CONDUCTORES_Y_OPERADORES_DE_EQUIPOS_ID:
                        GenerateAPTITUD_PSICOSOMATICA_PARA_CONDUCTORES_Y_OPERADORES_DE_EQUIPOS(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.APTITUD_PSICOSOMATICA_PARA_CONDUCTORES_Y_OPERADORES_DE_EQUIPOS_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.APTITUD_PARA_TRABAJOS_DE_CARGAS_SUSPENDIDAS_ID:
                        GenerateAPTITUD_PARA_TRABAJOS_DE_CARGAS_SUSPENDIDAS(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.APTITUD_PARA_TRABAJOS_DE_CARGAS_SUSPENDIDAS_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.ALTURA_FISICA_SHAHUINDO_ID:
                        GenerateAltura_Fisica_Shahuindo(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.ALTURA_FISICA_SHAHUINDO_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.EXAMEN_MEDICO_PARA_TRABAJOS_EN_ALTURA_GOLDFIELDS_ID:
                        GenerateExamen_Medico_Trabajos_Altura_GoldFields(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.EXAMEN_MEDICO_PARA_TRABAJOS_EN_ALTURA_GOLDFIELDS_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.EXAMEN_MEDICO_PARA_TRABAJOS_EN_ALTURA_AUDIT_GOLDFIELDS_ID:
                        GenerateExamen_Medico_Trabajos_Altura_AUDIT_GoldFields(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.EXAMEN_MEDICO_PARA_TRABAJOS_EN_ALTURA_AUDIT_GOLDFIELDS_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.SOLICITUD_EXAMEN_MEDICO_PARA_TRABAJOS_EN_ALTURA_AUDIT_GOLDFIELDS_ID:
                        GenerateExamen_Medico_Trabajos_Altura_SOLICITUD_GoldFields(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.SOLICITUD_EXAMEN_MEDICO_PARA_TRABAJOS_EN_ALTURA_AUDIT_GOLDFIELDS_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.Declaracion_Jurada_EMPO_YANACOCHA:
                        //ARNOLD STORE
                        GenerateDeclaracion_Jurada_EMPO_YANACOCHA(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.Declaracion_Jurada_EMPO_YANACOCHA)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.Declaracion_Jurada_EMO_SECURITAS:
                        GenerateDeclaracion_Jurada_EMO_Secutiras(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.Declaracion_Jurada_EMO_SECURITAS)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.EVALUACION_DERMATOLOGICA_OC_ID:
                        GenerateExamen_Dermatologico_Ocupacional(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.EVALUACION_DERMATOLOGICA_OC_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.DECLARACION_JURADA_SINTOMATICO_RESPIRATORIO_ID:
                        GenerateDeclaracion_Jurada_SINTOMATICO(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.DECLARACION_JURADA_SINTOMATICO_RESPIRATORIO_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CERT_SUF_MED_ALTURA_ID:
                        GenerateCertificado_Suficiencia_Medica_Trabajo_Altura_V4(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CERT_SUF_MED_ALTURA_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.EVALUACION_OTEOMUSCULAR_GOLDFIELDS_ID:
                        GenerateFicha_Evaluacion_Musculoesqueletica_GoldFields(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.EVALUACION_OTEOMUSCULAR_GOLDFIELDS_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.OSTEOMUSCULAR_HUDBAY_ID:
                        GenerateFichaEvaluacionMusculoEsqueleticaBasica_Hudbay(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.OSTEOMUSCULAR_HUDBAY_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.OSTEOMUSCULAR_HUDBAY_MIEMBROS_SUPERIORES_ID:
                        GenerateFichaEvaluacionMusculoesqueleticaMiembrosSuperioresHudbay(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.OSTEOMUSCULAR_HUDBAY_MIEMBROS_SUPERIORES_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CERTIFICADO_DE_APTITUD_MEDICA_PARA_TRABAJOS_ESPECIFICOS_HUDBAY_ID:
                        GenerateCERTIFICADO_APTITUD_MEDICA_PARA_TRABAJOS_ESPECIFICOS_HUDBAY(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CERTIFICADO_DE_APTITUD_MEDICA_PARA_TRABAJOS_ESPECIFICOS_HUDBAY_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CUESTIONARIO_NORDICO_MUSCULO_TENDINOSOS_ID:
                        GenerateCuestionarioNordico_EvaluacionSintomasMusculoTendinosos(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CUESTIONARIO_NORDICO_MUSCULO_TENDINOSOS_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;

                    case Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_ID:
                        GenerateFichadeEvaluacionParaConductoresHudbay(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_DE_EVALUACION_PARA_CONDUCTORES_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_PSICOLOGICO_RESUMEN_ID:
                        GenerateInforme_Psicologico_Resumen(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_PSICOLOGICO_RESUMEN_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.SUF_MED_BRIGADISTAS_ID:
                        GenerateCertificado_Suficiencia_Medica_Brigadistas(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.SUF_MED_BRIGADISTAS_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.IE_OBSTETRICO_ID:
                        Generate_Informe_Ecografico_Obstetrico(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.IE_OBSTETRICO_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.IE_GINECOLOGICO_ID:
                        Generate_Informe_Ecografico_Ginecologico(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.IE_GINECOLOGICO_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.IE_ADOMINAL_ID:
                        Generate_Informe_Ecografico_Abdominal(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.IE_ADOMINAL_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.IE_RENAL_ID:
                        Generate_Informe_Ecografico_Renal(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.IE_RENAL_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.IE_MAMAS_ID:
                        Generate_Informe_Ecografico_Mamas(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.IE_MAMAS_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.IE_PROSTATA_ID:
                        Generate_Informe_Ecografico_Prostata(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.IE_PROSTATA_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.IE_PARTES_BLANDAS_ID:
                        Generate_Informe_Ecografico_Partes_Blandas(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.IE_PARTES_BLANDAS_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.IE_OBSTETRICO_PELVICO_ID:
                        Generate_Informe_Ecografico_Obstetrico_Pelvico(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.IE_OBSTETRICO_PELVICO_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.AUTORIZACION_REALIZACION_EXAMEN_MEDICO_LIBERACION_INFORMACION:
                        GenerateAutorizacion_Realizacion_Ex_Lumina(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.AUTORIZACION_REALIZACION_EXAMEN_MEDICO_LIBERACION_INFORMACION)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CONSENTIMIENTO_INFORMADO_PARA_AUTORIZAR_EMO:
                        GeneConsentimientoInformadoParaAurotizarEMO(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CONSENTIMIENTO_INFORMADO_PARA_AUTORIZAR_EMO)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.AUTORIZACION_LIBERACION_SAN_MARTIN:
                        GenerateAutorizacion_Liberacion_SanMartin(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.AUTORIZACION_LIBERACION_SAN_MARTIN)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.ACTA_DE_ENTREGA_Y_LECTURA_RESULTADOS_EMO:
                        GenerateActaEntregaYLecturaResultadosEMO_GOLD_MUR(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.ACTA_DE_ENTREGA_Y_LECTURA_RESULTADOS_EMO)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    ///GenerateInforme_Resultados_San_Martinm
                    case Constants.INFORME_EXAMENES_ESPECIALES:
                        GenerateExamenesEspecialesReport(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_EXAMENES_ESPECIALES)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_MEDICO_RESUMEN:
                        //ARNOLD STORE
                        GenerateInformeMedicoResumen(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_MEDICO_RESUMEN)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_CERTIFICADO_APTITUD_COMPLETO:
                        //ARNOLD STORE
                        GenerateCertificadoAptitudCompleto(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_CERTIFICADO_APTITUD_COMPLETO)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.INFORME_FICHA_MEDICA_TRABAJADOR_CI:
                        //ARNOLD STORE
                        GenerateInformeMedicoTrabajadorInternacional(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_FICHA_MEDICA_TRABAJADOR_CI)), _serviceId, _pacientId);
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CALCULO_DE_STS:
                        GenerateCALCULO_DE_STS(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CALCULO_DE_STS)), _serviceId, _pacientId, Constants.CALCULO_DE_STS);
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.HOJA_RUTA_HUDBAY_ID:
                        //ARNOLD STORE
                        GenerateHOJA_RUTA_HUDBAY(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.HOJA_RUTA_HUDBAY_ID)), _serviceId, _pacientId, Constants.HOJA_RUTA_HUDBAY_ID);
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FOR_SSO_257_FACIAL:
                        GenerateFOR_SSO_257_FACIAL(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FOR_SSO_257_FACIAL)), _serviceId, _pacientId, Constants.FOR_SSO_257_FACIAL);
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    //MTC
                    case Constants.LABORATORIO_MTC_ID:
                        GenerateInformacion_Laboratorio(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.LABORATORIO_MTC_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.PSICOLOGIA_MTC_ID:
                        GenerateEvaluacionPsicologicaMTC(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.PSICOLOGIA_MTC_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.OFTALMOLOGIA_MTC_ID:
                        GenerateEvaluacionOftalmologicaMTC(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.OFTALMOLOGIA_MTC_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.AUDIOMETRIA_MTC_ID:
                        GenerateEvaluaciónAuditiva(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.AUDIOMETRIA_MTC_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.EVALUACION_CLINICA_MTC_ID:
                        GenerateEvaluacionClinica(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.EVALUACION_CLINICA_MTC_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CERTIFICADO_EVALUACION_MEDICA_PSICOLOGICA_CONDUCIR_ID:
                        GenerateCertificadoEvaluacionesMedicasPsicologicasConducir(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CERTIFICADO_EVALUACION_MEDICA_PSICOLOGICA_CONDUCIR_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                        //REPORTES ASISTENCIAL
                    case Constants.INFORME_LABORATORIO_CLINICO:
                        GenerateLaboratorioReport(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_LABORATORIO_CLINICO)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;

                    case Constants.FORMATO_ATENCION_INTEGRAL_ADULTO_MAYOR:
                        GenerateAtencionIntegralAdultoMayor(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FORMATO_ATENCION_INTEGRAL_ADULTO_MAYOR)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;

                    case Constants.FORMATO_ATENCION_INTEGRAL_ADULTO:
                        GenerateAtencionIntegralAdulto(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FORMATO_ATENCION_INTEGRAL_ADULTO)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;

                    case Constants.FORMATO_ATENCION_INTEGRAL_ADOLESCENTE:
                        GenerateAtencionIntegralAdolescente(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FORMATO_ATENCION_INTEGRAL_ADOLESCENTE)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;

                    case Constants.FORMATO_ATENCION_NINIO:
                        GenerateConsultaMedicaNinio(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FORMATO_ATENCION_NINIO)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    //case Constants.TOXICOLOGICO_COCAINA_MARIHUANA_T:
                    //    GenerateTOXICOLOGICO_COCAINA_MARIHUANA_TODOS(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.TOXICOLOGICO_COCAINA_MARIHUANA_T)));
                    //    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                    //    break;
                    //case Constants.INFORME_LABORATORIO_ID_ASIST:
                    //    Generate_Laboratorio_Asistencial(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.INFORME_LABORATORIO_ID_ASIST)));
                    //    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                    //    break;
                    #region COVID PARTICULARES
                    case Constants.FICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_RÁPIDA_COVID_19_ID_LC:
                        GenerateFICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_RÁPIDA_COVID_19_LC(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_RÁPIDA_COVID_19_ID_LC)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_MOLECULAR_COVID_LC_19_ID:
                        GenerateFICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_MOLECULAR_COVID_19_LC(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_MOLECULAR_COVID_LC_19_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FICHA_INVESTIGACION_COVID_ID_LC:
                        GenerateInvestigacionEpidemiologicaCOVID19LC(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_INVESTIGACION_COVID_ID_LC)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FICHA_INVESTIGACION_COVID_MOLECULAR_LC_ID:
                        GenerateInvestigacionEpidemiologicaCOVID19LCMOLECULAR(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_INVESTIGACION_COVID_MOLECULAR_LC_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CONSENTIMIENTO_INFORMADO_PARA_EXAMENES_DE_COVID_19_ID_LC:
                        GenerateConsentimientoInformadoExamenCovid19_LC(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CONSENTIMIENTO_INFORMADO_PARA_EXAMENES_DE_COVID_19_ID_LC)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CONSENTIMIENTO_INFORMADO_PARA_EXAMENES_DE_PRUEBA_MOLECULAR_COVID_19_LC_ID_OC:
                        GenerateConsentimientoInformadoExamenCovid19_LCMOLECULAR(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CONSENTIMIENTO_INFORMADO_PARA_EXAMENES_DE_PRUEBA_MOLECULAR_COVID_19_LC_ID_OC)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.RESULTADO_PRUEBA_DE_ANTIGENO_COVID_19_LC_ID:
                        Generate_FICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_ANTIGENO_COVID_19_LC(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.RESULTADO_PRUEBA_DE_ANTIGENO_COVID_19_LC_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    //ANTICUERPOS NEUTRALIZANTES 
                    case Constants.Anticuerpos_Neutralizantes_COVID_19_LC_ID:
                        Generate_Anticuerpos_Neutralizantes_COVID_19_ASIST(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.Anticuerpos_Neutralizantes_COVID_19_LC_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.FICHA_INVESTIGACION_COVID_ANTIGENO_ID_LC:
                        GenerateInvestigacionEpidemiologicaANTIGENOCOVID19_LC(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.FICHA_INVESTIGACION_COVID_ANTIGENO_ID_LC)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CONSENTIMIENTO_INFORMADO_PARA_EXAMENES_DE_PRUEBA_ANTIGENO_COVID_19_ID_LC:
                        GenerateConsentimientoInformadoExamenANTIGENOCovid19_LC(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CONSENTIMIENTO_INFORMADO_PARA_EXAMENES_DE_PRUEBA_ANTIGENO_COVID_19_ID_LC)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    #endregion
                    case Constants.ATENCION_INTEGRAL:
                        GenerateAtencionIntegral(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.ATENCION_INTEGRAL)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CARDIOLOGIA_RIESGO_QUIRURGICO_ID:
                        GenerateCardiologia_RiesgoQuirurgico(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CARDIOLOGIA_RIESGO_QUIRURGICO_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_ID:
                        GenerateCardiologia_Informe_ElectroCardiografico(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ID:
                        GenerateCardiologia_Evaluacion_Cardiologica(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CARDIOLOGIA_EVALUACION_CARDIOLOGICA_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.MEDICINA_INTERNA_INFORME_MEDICO_ID:
                        GenerateMedicina_Interna_Informe_Medico(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.MEDICINA_INTERNA_INFORME_MEDICO_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.NUTRICION_INFORME_MEDICO_ID:
                        GenerateNutricionInformeMedico(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.NUTRICION_INFORME_MEDICO_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.OTORRINO_EVALUACION_AUDIOMETRICA_ID:
                        GenerateOtorrino_Ficha_Audiometrica(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.OTORRINO_EVALUACION_AUDIOMETRICA_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.OTORRINO_INFORME_OTORRINO_ID:
                        GenerateOtorrino_Informe_Otorrino(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.OTORRINO_INFORME_OTORRINO_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;
                    case Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_ID:
                        GenerateCertificadodeSuficienciaMedicaTrabajosdeRiesgo(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_ID)));
                        _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(_ruta, _serviceId + "-" + componentId)));
                        break;


                    default:
                        break;
                }

                return "OK";
                #endregion
            }
            catch (Exception exception)
            {
                var t = new Thread(() =>
                {
                    MessageBox.Show(exception.ToString() + "\nComponente: " + componentId + "\nServicio: " + serviceId + "\nPacienteId: " + pPacienteId, "ALERTA!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                });
                t.Start();
                return "";
            }
        }

       
        private void GenerateAtencionIntegral(string pathFile)
        {
            //var _DataService = _serviceBL.GetServiceReport(_serviceId);
            // en esta variable va a traer todos los valores de los examenes
            var exams = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            int GrupoEtario = 0;
            int Grupo = 0;
            #region
            if (_edad <= 12)
            {
                GrupoEtario = 4;
                Grupo = 2824;
            }
            else if (13 <= _edad && _edad <= 17)
            {
                GrupoEtario = 2;
                Grupo = 2822;
            }
            else if (18 <= _edad && _edad <= 64)
            {
                GrupoEtario = 1;
                Grupo = 2821;
            }
            else
            {
                GrupoEtario = 3;
                Grupo = 2823;
            }

            #endregion
            var listAntecedentes = _serviceBL.ObtenerEsoAntecedentesPorGrupoId(Grupo, GrupoEtario, _pacientId);
            var datosNin = _pacientBL.DevolverNinio(_serviceId);
            var datosAdol = _pacientBL.DevolverAdolescente(_serviceId);
            var datosAdul = _pacientBL.DevolverAdulto(_serviceId);
            var listEmb = _pacientBL.GetEmbarazos(_pacientId);
            var datosAdulMay = _pacientBL.DevolverAdultoMayor(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var medico = _pacientBL.ObtenerDatosMedicoMedicina(_serviceId, Constants.ATENCION_INTEGRAL_ID, Constants.ATENCION_INTEGRAL_GINECOLOGICA_ID);
            var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(_serviceId);
            var medicina = objRecetaBl.GetReceta(_serviceId);
            var medicoTratante = new ServiceBL().GetMedicoTratante(_serviceId);
            var datosGrabo = new ServiceBL().DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.MEDICINA_C, _serviceId);

            //List<Categoria> AdditionalExam = new List<Categoria>();
            List<Categoria> DataSource = new List<Categoria>();
            List<string> ComponentList = new List<string>();
            var ListadditExam = new AdditionalExamBL().GetAdditionalExamByServiceId_Historia(_serviceId);

            foreach (var componenyId in ListadditExam)
            {
                ComponentList.Add(componenyId.ComponentId);
            }
            OperationResult objOperationResult = new OperationResult();

            foreach (var componentId in ComponentList)
            {
                var ListServiceComponent = new ServiceBL().GetAllComponents(ref objOperationResult, (int)TipoBusqueda.ComponentId, componentId);

                Categoria categoria = DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId);
                if (categoria != null)
                {
                    List<ComponentDetailList> componentDetail = new List<ComponentDetailList>();
                    componentDetail = ListServiceComponent[0].Componentes;
                    DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId).Componentes.AddRange(componentDetail);
                }
                else
                {
                    DataSource.AddRange(ListServiceComponent);
                }
            }
            var añosCompleto = DiferenciaFechas(DateTime.Now, datosP.d_Birthdate.Value);

            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);


            AtencionIntegral.CreateAtencionIntegral(pathFile, medico, datosP, listAntecedentes, MedicalCenter, 
                exams, datosNin, datosAdol, datosAdul, listEmb, datosAdulMay, diagnosticRepository, medicina, 
                _ExamenesServicio, medicoTratante, datosGrabo, DataSource, ListadditExam, añosCompleto,
                _listaPatologicosFamiliares, _listMedicoPersonales, _listaHabitoNocivos );
            
        }

        #region REPORTES MEDICOS

        private void GenerateCardiologia_RiesgoQuirurgico(string pathFile)
        {
            var exams = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
          
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var medico = _pacientBL.ObtenerDatosMedicoMedicina(_serviceId, Constants.ATENCION_INTEGRAL_ID, Constants.CARDIOLOGIA_RIESGO_QUIRURGICO_ID);
            var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(_serviceId);
            var medicoTratante = new ServiceBL().GetMedicoTratante(_serviceId);
            var datosGrabo = new ServiceBL().DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.CARDIOLOGIA_C, _serviceId);

            List<Categoria> AdditionalExam = new List<Categoria>();
            List<Categoria> DataSource = new List<Categoria>();
            List<string> ComponentList = new List<string>();
            var ListadditExam = new AdditionalExamBL().GetAdditionalExamByServiceId_Historia(_serviceId);

            foreach (var componenyId in ListadditExam)
            {
                ComponentList.Add(componenyId.ComponentId);
            }
            OperationResult objOperationResult = new OperationResult();

            foreach (var componentId in ComponentList)
            {
                var ListServiceComponent = new ServiceBL().GetAllComponents(ref objOperationResult, (int)TipoBusqueda.ComponentId, componentId);

                Categoria categoria = DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId);
                if (categoria != null)
                {
                    List<ComponentDetailList> componentDetail = new List<ComponentDetailList>();
                    componentDetail = ListServiceComponent[0].Componentes;
                    DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId).Componentes.AddRange(componentDetail);
                }
                else
                {
                    DataSource.AddRange(ListServiceComponent);
                }
            }
            var añosCompleto = DiferenciaFechas(DateTime.Now, datosP.d_Birthdate.Value);

            Cardiologia_RiesgoQuirurgico.CreateCardiologia_RiesgoQuirurgico(pathFile, medico, datosP, MedicalCenter,
                exams, diagnosticRepository,
                medicoTratante, datosGrabo, DataSource, añosCompleto);

        }

        private void GenerateMedicina_Interna_Informe_Medico(string pathFile)
        {
            var exams = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var medico = _pacientBL.ObtenerDatosMedicoMedicina(_serviceId, Constants.ATENCION_INTEGRAL_ID, Constants.MEDICINA_INTERNA_INFORME_MEDICO_ID);
            var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(_serviceId);
            var medicoTratante = new ServiceBL().GetMedicoTratante(_serviceId);
            var datosGrabo = new ServiceBL().DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.MEDICINA_INTERNA_c, _serviceId);

            List<Categoria> AdditionalExam = new List<Categoria>();
            List<Categoria> DataSource = new List<Categoria>();
            List<string> ComponentList = new List<string>();
            var ListadditExam = new AdditionalExamBL().GetAdditionalExamByServiceId_Historia(_serviceId);

            foreach (var componenyId in ListadditExam)
            {
                ComponentList.Add(componenyId.ComponentId);
            }
            OperationResult objOperationResult = new OperationResult();

            foreach (var componentId in ComponentList)
            {
                var ListServiceComponent = new ServiceBL().GetAllComponents(ref objOperationResult, (int)TipoBusqueda.ComponentId, componentId);

                Categoria categoria = DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId);
                if (categoria != null)
                {
                    List<ComponentDetailList> componentDetail = new List<ComponentDetailList>();
                    componentDetail = ListServiceComponent[0].Componentes;
                    DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId).Componentes.AddRange(componentDetail);
                }
                else
                {
                    DataSource.AddRange(ListServiceComponent);
                }
            }
            var añosCompleto = DiferenciaFechas(DateTime.Now, datosP.d_Birthdate.Value);

            Medicina_Interna_Informe_Medico.CreateMedicina_Interna_Informe_Medico(pathFile, medico, datosP, MedicalCenter,
                exams, diagnosticRepository,
                medicoTratante, datosGrabo, DataSource, añosCompleto);

        }


        private void GenerateNutricionInformeMedico(string pathFile)
        {
            var exams = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var medico = _pacientBL.ObtenerDatosMedicoMedicina(_serviceId, Constants.ATENCION_INTEGRAL_ID, Constants.NUTRICION_INFORME_MEDICO_ID);
            var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(_serviceId);
            var medicoTratante = new ServiceBL().GetMedicoTratante(_serviceId);
            var datosGrabo = new ServiceBL().DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.NUTRICION, _serviceId);

            List<Categoria> AdditionalExam = new List<Categoria>();
            List<Categoria> DataSource = new List<Categoria>();
            List<string> ComponentList = new List<string>();
            var ListadditExam = new AdditionalExamBL().GetAdditionalExamByServiceId_Historia(_serviceId);

            foreach (var componenyId in ListadditExam)
            {
                ComponentList.Add(componenyId.ComponentId);
            }
            OperationResult objOperationResult = new OperationResult();

            foreach (var componentId in ComponentList)
            {
                var ListServiceComponent = new ServiceBL().GetAllComponents(ref objOperationResult, (int)TipoBusqueda.ComponentId, componentId);

                Categoria categoria = DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId);
                if (categoria != null)
                {
                    List<ComponentDetailList> componentDetail = new List<ComponentDetailList>();
                    componentDetail = ListServiceComponent[0].Componentes;
                    DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId).Componentes.AddRange(componentDetail);
                }
                else
                {
                    DataSource.AddRange(ListServiceComponent);
                }
            }
            var añosCompleto = DiferenciaFechas(DateTime.Now, datosP.d_Birthdate.Value);

            NutricionInformeMedico.CreateNutricionInformeMedico(pathFile, medico, datosP, MedicalCenter,
                exams, diagnosticRepository,
                medicoTratante, datosGrabo, DataSource, añosCompleto);

        }


        private void GenerateCardiologia_Informe_ElectroCardiografico(string pathFile)
        {
            var exams = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var medico = _pacientBL.ObtenerDatosMedicoMedicina(_serviceId, Constants.ATENCION_INTEGRAL_ID, Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_ID);
            var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(_serviceId);
            var medicoTratante = new ServiceBL().GetMedicoTratante(_serviceId);
            var datosGrabo = new ServiceBL().DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.CARDIOLOGIA_C, _serviceId);

            List<Categoria> AdditionalExam = new List<Categoria>();
            List<Categoria> DataSource = new List<Categoria>();
            List<string> ComponentList = new List<string>();
            var ListadditExam = new AdditionalExamBL().GetAdditionalExamByServiceId_Historia(_serviceId);

            foreach (var componenyId in ListadditExam)
            {
                ComponentList.Add(componenyId.ComponentId);
            }
            OperationResult objOperationResult = new OperationResult();

            foreach (var componentId in ComponentList)
            {
                var ListServiceComponent = new ServiceBL().GetAllComponents(ref objOperationResult, (int)TipoBusqueda.ComponentId, componentId);

                Categoria categoria = DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId);
                if (categoria != null)
                {
                    List<ComponentDetailList> componentDetail = new List<ComponentDetailList>();
                    componentDetail = ListServiceComponent[0].Componentes;
                    DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId).Componentes.AddRange(componentDetail);
                }
                else
                {
                    DataSource.AddRange(ListServiceComponent);
                }
            }
            var añosCompleto = DiferenciaFechas(DateTime.Now, datosP.d_Birthdate.Value);

            Cardiologia_Informe_ElectroCardiografico.CreateCardiologia_Informe_ElectroCardiografico(pathFile, medico, datosP, MedicalCenter,
                exams, diagnosticRepository,
                medicoTratante, datosGrabo, DataSource, añosCompleto);

        }

        private void GenerateCardiologia_Evaluacion_Cardiologica(string pathFile)
        {
            var exams = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var medico = _pacientBL.ObtenerDatosMedicoMedicina(_serviceId, Constants.ATENCION_INTEGRAL_ID, Constants.CARDIOLOGIA_INFORME_ELECTRO_CARDIOGRÁFICO_ID);
            var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(_serviceId);
            var medicoTratante = new ServiceBL().GetMedicoTratante(_serviceId);
            var datosGrabo = new ServiceBL().DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.CARDIOLOGIA_C, _serviceId);

            List<Categoria> AdditionalExam = new List<Categoria>();
            List<Categoria> DataSource = new List<Categoria>();
            List<string> ComponentList = new List<string>();
            var ListadditExam = new AdditionalExamBL().GetAdditionalExamByServiceId_Historia(_serviceId);

            foreach (var componenyId in ListadditExam)
            {
                ComponentList.Add(componenyId.ComponentId);
            }
            OperationResult objOperationResult = new OperationResult();

            foreach (var componentId in ComponentList)
            {
                var ListServiceComponent = new ServiceBL().GetAllComponents(ref objOperationResult, (int)TipoBusqueda.ComponentId, componentId);

                Categoria categoria = DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId);
                if (categoria != null)
                {
                    List<ComponentDetailList> componentDetail = new List<ComponentDetailList>();
                    componentDetail = ListServiceComponent[0].Componentes;
                    DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId).Componentes.AddRange(componentDetail);
                }
                else
                {
                    DataSource.AddRange(ListServiceComponent);
                }
            }
            var añosCompleto = DiferenciaFechas(DateTime.Now, datosP.d_Birthdate.Value);

            Cardiologia_Evaluacion_Cardiologica.CreateCardiologia_Evaluacion_Cardiologica(pathFile, medico, datosP, MedicalCenter,
                exams, diagnosticRepository,
                medicoTratante, datosGrabo, DataSource, añosCompleto);

        }



        private void GenerateOtorrino_Ficha_Audiometrica(string pathFile)
        {
            var exams = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var medico = _pacientBL.ObtenerDatosMedicoMedicina(_serviceId, Constants.ATENCION_INTEGRAL_ID, Constants.MEDICINA_INTERNA_INFORME_MEDICO_ID);
            var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(_serviceId);
            var medicoTratante = new ServiceBL().GetMedicoTratante(_serviceId);
            var datosGrabo = new ServiceBL().DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.OTORRINOLARINGOLOGIA_c, _serviceId);

            List<Categoria> AdditionalExam = new List<Categoria>();
            List<Categoria> DataSource = new List<Categoria>();
            List<string> ComponentList = new List<string>();
            var ListadditExam = new AdditionalExamBL().GetAdditionalExamByServiceId_Historia(_serviceId);

            foreach (var componenyId in ListadditExam)
            {
                ComponentList.Add(componenyId.ComponentId);
            }
            OperationResult objOperationResult = new OperationResult();

            foreach (var componentId in ComponentList)
            {
                var ListServiceComponent = new ServiceBL().GetAllComponents(ref objOperationResult, (int)TipoBusqueda.ComponentId, componentId);

                Categoria categoria = DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId);
                if (categoria != null)
                {
                    List<ComponentDetailList> componentDetail = new List<ComponentDetailList>();
                    componentDetail = ListServiceComponent[0].Componentes;
                    DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId).Componentes.AddRange(componentDetail);
                }
                else
                {
                    DataSource.AddRange(ListServiceComponent);
                }
            }

            var añosCompleto = DiferenciaFechas(DateTime.Now, datosP.d_Birthdate.Value);

            var audioUserControlList = _serviceBL.ReportAudiometriaUserControl(_serviceId, Constants.OTORRINO_INFORME_OTORRINO_ID);

            Otorrino_Ficha_Audiometrica.CreateOtorrino_Ficha_Audiometrica(pathFile, medico, datosP, MedicalCenter,
                exams, diagnosticRepository,
                medicoTratante, datosGrabo, DataSource, añosCompleto, audioUserControlList);

        }

        private void GenerateOtorrino_Informe_Otorrino(string pathFile)
        {
            var exams = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var medico = _pacientBL.ObtenerDatosMedicoMedicina(_serviceId, Constants.ATENCION_INTEGRAL_ID, Constants.MEDICINA_INTERNA_INFORME_MEDICO_ID);
            var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(_serviceId);
            var medicoTratante = new ServiceBL().GetMedicoTratante(_serviceId);
            var datosGrabo = new ServiceBL().DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.OTORRINOLARINGOLOGIA_c, _serviceId);

            List<Categoria> AdditionalExam = new List<Categoria>();
            List<Categoria> DataSource = new List<Categoria>();
            List<string> ComponentList = new List<string>();
            var ListadditExam = new AdditionalExamBL().GetAdditionalExamByServiceId_Historia(_serviceId);

            foreach (var componenyId in ListadditExam)
            {
                ComponentList.Add(componenyId.ComponentId);
            }
            OperationResult objOperationResult = new OperationResult();

            foreach (var componentId in ComponentList)
            {
                var ListServiceComponent = new ServiceBL().GetAllComponents(ref objOperationResult, (int)TipoBusqueda.ComponentId, componentId);

                Categoria categoria = DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId);
                if (categoria != null)
                {
                    List<ComponentDetailList> componentDetail = new List<ComponentDetailList>();
                    componentDetail = ListServiceComponent[0].Componentes;
                    DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId).Componentes.AddRange(componentDetail);
                }
                else
                {
                    DataSource.AddRange(ListServiceComponent);
                }
            }

            var añosCompleto = DiferenciaFechas(DateTime.Now, datosP.d_Birthdate.Value);


            Otorrino_Informe_Otorrino.CreateOtorrino_Informe_Otorrino(pathFile, medico, datosP, MedicalCenter,
                exams, diagnosticRepository,
                medicoTratante, datosGrabo, DataSource, añosCompleto);

        }

        #endregion
        private void GenerateCertificadodeSuficienciaMedicaTrabajosdeRiesgo(string pathFile)
        {
            var exams = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var medico = _pacientBL.ObtenerDatosMedicoMedicina(_serviceId, Constants.ATENCION_INTEGRAL_ID, Constants.CERTIFICADO_SUFICIENCIA_MEDICA_TRABAJOS_RIESGO_ID);
            var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(_serviceId);
            var medicoTratante = new ServiceBL().GetMedicoTratante(_serviceId);
            var datosGrabo = new ServiceBL().DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);

            List<Categoria> AdditionalExam = new List<Categoria>();
            List<Categoria> DataSource = new List<Categoria>();
            List<string> ComponentList = new List<string>();
            var ListadditExam = new AdditionalExamBL().GetAdditionalExamByServiceId_Historia(_serviceId);

            foreach (var componenyId in ListadditExam)
            {
                ComponentList.Add(componenyId.ComponentId);
            }
            OperationResult objOperationResult = new OperationResult();

            foreach (var componentId in ComponentList)
            {
                var ListServiceComponent = new ServiceBL().GetAllComponents(ref objOperationResult, (int)TipoBusqueda.ComponentId, componentId);

                Categoria categoria = DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId);
                if (categoria != null)
                {
                    List<ComponentDetailList> componentDetail = new List<ComponentDetailList>();
                    componentDetail = ListServiceComponent[0].Componentes;
                    DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId).Componentes.AddRange(componentDetail);
                }
                else
                {
                    DataSource.AddRange(ListServiceComponent);
                }
            }

            var añosCompleto = DiferenciaFechas(DateTime.Now, datosP.d_Birthdate.Value);

            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);

            CertificadodeSuficienciaMedicaTrabajosdeRiesgo.CreateCertificadodeSuficienciaMedicaTrabajosdeRiesgo(pathFile, medico, datosP, MedicalCenter,
                exams, diagnosticRepository,
                medicoTratante, datosGrabo, DataSource, añosCompleto, filiationData);

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

        private void Generate_Laboratorio_Asistencial(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var _NombreMedico = _pacientBL.GetNombreMedicoLab(_serviceId);
            var _NombreEmpresa = _pacientBL.GetNombreEmpresaLab(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            //var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.LaboratorioC, _serviceId);

            InformeLaboratorioAsistencial.CreateExamen_InformeLaboratorioAsistencial(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, _NombreMedico, _NombreEmpresa);
        }
        #region COVID 19
        
        private void Generate_FICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_ANTIGENO_COVID_19_LC(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var _NombreMedico = _pacientBL.GetNombreMedicoLab(_serviceId);
            var _NombreEmpresa = _pacientBL.GetNombreEmpresaLab(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            //var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.LaboratorioC, _serviceId);

            FICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_ANTIGENO_COVID_19_LC.CreateFICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_ANTIGENO_COVID_19_LC(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, _NombreMedico, _NombreEmpresa);
        }

        private void Generate_Anticuerpos_Neutralizantes_COVID_19_ASIST(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var _NombreMedico = _pacientBL.GetNombreMedicoLab(_serviceId);
            var _NombreEmpresa = _pacientBL.GetNombreEmpresaLab(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            //var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.LaboratorioC, _serviceId);

            Anticuerpos_Neutralizantes_COVID_19_ASIST.CreateAnticuerpos_Neutralizantes_COVID_19_ASIST(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, _NombreMedico, _NombreEmpresa);
        }
        private void GenerateInvestigacionEpidemiologicaANTIGENOCOVID19_LC(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            //var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            var datosGrabo = _serviceBL.GetFirmaMedicos(175);
            InvestigacionEpidemiologicaPRUEBAANTIGENOCOVID19_LLCC.CreateInvestigacionEpidemiologicaPRUEBAANTIGENOCOVID19_LLCC(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo);
        }

        private void GenerateConsentimientoInformadoExamenANTIGENOCovid19_LC(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            ConsentimientoInformadoExamenPRUEBAANTIGENOCovid19_LC.CreateConsentimientoInformadoExamenPRUEBAANTIGENOCovid19_LC(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents);
        }

        private void GenerateFICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_RÁPIDA_COVID_19_LC(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.LaboratorioC, _serviceId);
            FICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_RÁPIDA_COVID_19_LC.CreateFICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_RÁPIDA_COVID_19_LC(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo);
        }

        private void GenerateFICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_MOLECULAR_COVID_19_LC(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.LaboratorioC, _serviceId);
            FICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_MOLECULAR_COVID_LC_19_LC.CreateFICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_MOLECULAR_COVID_LC_19_LC(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo);
        }

        private void GenerateInvestigacionEpidemiologicaCOVID19LC(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            //var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            var datosGrabo = _serviceBL.GetFirmaMedicos(175);
            InvestigacionEpidemiologicaCOVID19_LC.CreateInvestigacionEpidemiologicaCOVID19_LC(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo);
        }

        private void GenerateInvestigacionEpidemiologicaCOVID19LCMOLECULAR(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            //var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            var datosGrabo = _serviceBL.GetFirmaMedicos(175);
            InvestigacionEpidemiologicaPRUEBAMOLECULARCOVID19_LC.CreateInvestigacionEpidemiologicaPRUEBAMOLECULARCCOVID19_LC(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo);
        }

        private void GenerateConsentimientoInformadoExamenCovid19_LC(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            ConsentimientoInformadoExamenCovid19_LC.CreateConsentimientoInformadoExamenCovid19_LC(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents);
        }

        private void GenerateConsentimientoInformadoExamenCovid19_LCMOLECULAR(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            ConsentimientoInformadoExamenPRUEBAMOLECULARCovid19_LC.CreateConsentimientoInformadoExamenMOLECULARCovid19_LC(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents);
        }
        #endregion
        private void GenerateAtencionIntegralAdultoMayor(string pathFile)
        {
            var listaProblema = atencionIntegralBL.GetAtencionIntegral(_pacientId);
            var listPlanIntegral = atencionIntegralBL.GetPlanIntegral(_pacientId);
            var datosPersonales = _pacientBL.GetDatosPersonalesAtencion(_serviceId);

            var listEmb = _pacientBL.GetEmbarazos(_pacientId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);

            int GrupoEtario = 3;
            int Grupo = 2823;
            var listAntecedentes = _serviceBL.ObtenerEsoAntecedentesPorGrupoId(Grupo, GrupoEtario, _pacientId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            //datos adicionales adultomayor
            var datosAdulMay = _pacientBL.DevolverAdultoMayor(_serviceId);

            int GrupoBase = 286;

            List<frmEsoCuidadosPreventivosFechas> Fechas = _serviceBL.ObtenerFechasCuidadosPreventivos(GrupoBase, _pacientId);
            if (Fechas.Count > 6)
                Fechas = Fechas.Skip((Fechas.Count - 6)).ToList();

            List<frmEsoCuidadosPreventivosComentarios> Comentarios = _serviceBL.ObtenerComentariosCuidadosPreventivos(_pacientId);

            AtencionIntegralAdultoMayor.CreateAtencionIntegral(pathFile, listaProblema, listPlanIntegral, datosPersonales, datosP, listAntecedentes, Fechas, MedicalCenter, datosAdulMay, listEmb, Comentarios);
        }

        private void GenerateAtencionIntegralAdulto(string pathFile)
        {
            var listaProblema = atencionIntegralBL.GetAtencionIntegral(_pacientId);
            var listPlanIntegral = atencionIntegralBL.GetPlanIntegral(_pacientId);
            var datosPersonales = _pacientBL.GetDatosPersonalesAtencion(_serviceId);

            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);

            var listEmb = _pacientBL.GetEmbarazos(_pacientId);

            //datos adicionales adulto
            var datosAdul = _pacientBL.DevolverAdulto(_serviceId);

            int GrupoEtario = 1;
            int Grupo = 2821;
            var listAntecedentes = _serviceBL.ObtenerEsoAntecedentesPorGrupoId(Grupo, GrupoEtario, _pacientId);


            int GrupoBase = 284;

            if (datosPersonales.Genero == "FEMENINO")
            {
                GrupoBase = 283;

            }

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            List<frmEsoCuidadosPreventivosFechas> Fechas = _serviceBL.ObtenerFechasCuidadosPreventivos(GrupoBase, _pacientId);
            if (Fechas.Count > 6)
                Fechas = Fechas.Skip((Fechas.Count - 6)).ToList();

            List<frmEsoCuidadosPreventivosComentarios> Comentarios = _serviceBL.ObtenerComentariosCuidadosPreventivos(_pacientId);

            var datosPaciente = _pacientBL.GetPacientReport(_pacientId);

            AtencionIntegralAdulto.CreateAtencionIntegral(pathFile, listaProblema, listPlanIntegral, datosPersonales, datosP, listAntecedentes, Fechas, MedicalCenter, datosAdul, listEmb, Comentarios);
        }

        private void GenerateAtencionIntegralAdolescente(string pathFile)
        {
            var listaProblema = atencionIntegralBL.GetAtencionIntegral(_pacientId);
            var listPlanIntegral = atencionIntegralBL.GetPlanIntegral(_pacientId);
            var datosPaciente = _pacientBL.GetDatosPersonalesAtencion(_serviceId);

            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);

            //datos adicionales adolescente
            var datosAdol = _pacientBL.DevolverAdolescente(_serviceId);

            int GrupoEtario = 2;
            int Grupo = 2822;
            var listAntecedentes = _serviceBL.ObtenerEsoAntecedentesPorGrupoId(Grupo, GrupoEtario, _pacientId);

            int GrupoBase = 285;
            if (datosPaciente.Genero.ToUpper() == "MUJER")
                GrupoBase = 283;
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            List<frmEsoCuidadosPreventivosFechas> Fechas = _serviceBL.ObtenerFechasCuidadosPreventivos(GrupoBase, _pacientId);
            if (Fechas.Count > 6)
                Fechas = Fechas.Skip((Fechas.Count - 6)).ToList();

            List<frmEsoCuidadosPreventivosComentarios> Comentarios = _serviceBL.ObtenerComentariosCuidadosPreventivos(_pacientId);

            SAtencionIntegralAdolescente.CreateAtencionIntegral(pathFile, listaProblema, listPlanIntegral, datosPaciente, datosP, listAntecedentes, Fechas, MedicalCenter, datosAdol, Comentarios);
        }

        private void GenerateConsultaMedicaNinio(string pathFile)
        {
            var listaProblema = atencionIntegralBL.GetAtencionIntegral(_pacientId);
            var listPlanIntegral = atencionIntegralBL.GetPlanIntegral(_pacientId);
            var datosPaciente = _pacientBL.GetDatosPersonalesAtencion(_serviceId);

            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            int GrupoEtario = 4;
            int Grupo = 2824;
            var listAntecedentes = _serviceBL.ObtenerEsoAntecedentesPorGrupoId(Grupo, GrupoEtario, _pacientId);

            //datos adicionales ninio
            var datosNin = _pacientBL.DevolverNinio(_serviceId);

            int GrupoBase = 292;
            //if (datosPaciente.Genero.ToUpper() == "MUJER")
            //    GrupoBase = 283;

            List<frmEsoCuidadosPreventivosFechas> Fechas = _serviceBL.ObtenerFechasCuidadosPreventivos(GrupoBase, _pacientId);
            if (Fechas.Count > 6)
                Fechas = Fechas.Skip((Fechas.Count - 6)).ToList();

            List<frmEsoCuidadosPreventivosComentarios> Comentarios = _serviceBL.ObtenerComentariosCuidadosPreventivos(_pacientId);

            Ninio.CreateAtencionNinio(pathFile, listaProblema, listPlanIntegral, datosPaciente, datosP, listAntecedentes, Fechas, MedicalCenter, datosNin, Comentarios);
        }
        private void GenerateFOR_SSO_257_FACIAL(string pathFile, string serviceId, string pacientId, string componentId)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var fototipo = _serviceBL.getFototipo(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);

            FOR_SSO_275_EXAMEN_FACIAL.GenerarExamenFacial(pathFile, datosP, MedicalCenter, LogoEmpresa, datosGrabo, fototipo, serviceComponents, filiationData);
        }

        private datosFacial GetCamposFacial(string serviceId, string componentId, string pacientId)
        {
            try
            {
                ConexionSigesoft conexion = new ConexionSigesoft();
                conexion.opensigesoft();
                string cadena = "";
                SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
                SqlDataReader lector = comando.ExecuteReader();
                datosFacial facial = new datosFacial();
                while (lector.Read())
                {
                    facial.i_lunar = Convert.ToInt32(lector.GetValue(0).ToString());
                    facial.i_peca = Convert.ToInt32(lector.GetValue(1).ToString());
                    facial.i_mancha = Convert.ToInt32(lector.GetValue(2).ToString());
                    facial.i_cicatriz = Convert.ToInt32(lector.GetValue(3).ToString());
                    facial.i_otros = Convert.ToInt32(lector.GetValue(4).ToString());
                    facial.observaciones = lector.GetValue(5).ToString();
                    facial.i_fototipo = Convert.ToInt32(lector.GetValue(6).ToString());
                    facial.fpsRecomendado = lector.GetValue(7).ToString();
                    facial.imagenFototipo = lector.GetValue(8) as byte[];
                }
                lector.Close();
                conexion.closesigesoft();
                return facial;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        //private void Generate_Laboratorio_Asistencial(string pathFile)
        //{
        //    var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
        //    var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
        //    var _NombreMedico = _pacientBL.GetNombreMedicoLab(_serviceId);
        //    var _NombreEmpresa = _pacientBL.GetNombreEmpresaLab(_serviceId);
        //    var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
        //    var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
        //    //var _DataService = _serviceBL.GetServiceReport(_serviceId);
        //    var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.LaboratorioC, _serviceId); //ARNOLD STORE

        //    InformeLaboratorioAsistencial.CreateExamen_InformeLaboratorioAsistencial(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, _NombreMedico, _NombreEmpresa);
        //}
        private void GenerateHOJA_RUTA_HUDBAY(string pathFile, string serviceId, string _pacientId, string componentId)
        {

            var datosP = _pacientBL.DevolverDatosPaciente(serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var fototipo = _serviceBL.getFototipo(_serviceId);//ARNOLD STORE
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            var firmas = new List<FirmasHojaRuta>();

            FOR_SSO_207_HOJA_RUTA_HUDBAY.GenerarHojaRuta(pathFile, datosP, MedicalCenter, LogoEmpresa, serviceComponents, filiationData, firmas, datosGrabo);
        }

        private UsuarioGrabo GetAuditor(string serviceId)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            string cadena = "";
            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            UsuarioGrabo user = new UsuarioGrabo();
            while (lector.Read())
            {
                user.Nombre = lector.GetValue(0).ToString();
                user.Firma = lector.GetValue(1) as byte[];
                user.CMP = lector.GetValue(2).ToString();
            }
            lector.Close();
            conexion.closesigesoft();
            return user;
        }

        private List<FirmasHojaRuta> GetFirmas(string serviceId)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            string cadena = "";
            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            List<FirmasHojaRuta> firmas = new List<FirmasHojaRuta>();
            while (lector.Read())
            {
                FirmasHojaRuta ff = new FirmasHojaRuta();
                ff.Nombre = lector.GetValue(0).ToString();
                ff.Firma = lector.GetValue(1) as byte[];
                ff.CMP = lector.GetValue(2).ToString();
                ff.categoryName = lector.GetValue(3).ToString();
                firmas.Add(ff);
            }
            lector.Close();
            conexion.closesigesoft();
            return firmas;
        }

        private hojaRutaHudbay GetHojaRuta(string _serviceId, string componentId)
        {
            try
            {
                ConexionSigesoft conexion = new ConexionSigesoft();
                conexion.opensigesoft();
                string cadena = "";
                SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
                SqlDataReader lector = comando.ExecuteReader();
                hojaRutaHudbay hoja = new hojaRutaHudbay();
                while (lector.Read())
                {
                    hoja.i_1_estaAyunas = Convert.ToInt32(lector.GetValue(0).ToString());
                    hoja.i_2_realiadoEjercicio = Convert.ToInt32(lector.GetValue(1).ToString());
                    hoja.i_3_sustToxica = Convert.ToInt32(lector.GetValue(2).ToString());
                    hoja.i_4_turnoDia = Convert.ToInt32(lector.GetValue(3).ToString());
                    hoja.i_5_usandoMedicamento = Convert.ToInt32(lector.GetValue(4).ToString());
                    hoja.i_expuestoRuido = Convert.ToInt32(lector.GetValue(5).ToString());
                    hoja.i_procesoInflamatorio = Convert.ToInt32(lector.GetValue(6).ToString());
                    hoja.i_cambiosAltura = Convert.ToInt32(lector.GetValue(7).ToString());
                }
                lector.Close();
                conexion.closesigesoft();
                return hoja;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void GenerateFICHA_AUDIOMETRICA_HUDBAY(string pathFile, string _serviceId, string _pacientId, string componentId)
        {
            throw new NotImplementedException();
        }

        private void GenerateCALCULO_DE_STS(string pathFile, string _serviceId, string _pacientId, string componentId)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var audioUserControlList = _serviceBL.ReportAudiometriaUserControl(_serviceId, Constants.FICHA_AUDIOMETRICA_HUDBAY_ID);
            var audioUserControlListBasal = _serviceBL.ReportAudiometriaUserControl(_serviceId, Constants.FICHA_AUDIOMETRICA_BASAL_HUDBAY_ID);

            FOR_SSO_281_STS_HUDBAY.GenerarReporteSTS(pathFile, datosP, MedicalCenter, LogoEmpresa, datosGrabo, serviceComponents, filiationData, audioUserControlList, audioUserControlListBasal);
        }

        private stsValuesComponent ObtenerValoresDelComponenteSTS(string componentId, string _serviceId)
        {
            try
            {
                ConexionSigesoft conexion = new ConexionSigesoft();
                conexion.opensigesoft();
                string cadena = "select SV.v_Value1 from service SR " +
                                "inner join servicecomponent SC on SR.v_ServiceId=SC.v_ServiceId " +
                                "inner join servicecomponentfields SF on SC.v_ServiceComponentId=SF.v_ServiceComponentId " +
                                "inner join servicecomponentfieldvalues SV on SF.v_ServiceComponentFieldsId=SV.v_ServiceComponentFieldsId " +
                                "where SR.v_ServiceId='" + _serviceId + "' " +
                                "and SF.v_ComponentFieldId = 	'N009-MF000005302' and SF.v_ComponentFieldId = 	'N009-MF000005303' " +
                                "and SF.v_ComponentFieldId = 	'N009-MF000005304' and SF.v_ComponentFieldId = 	'N009-MF000005305' " +
                                "and SF.v_ComponentFieldId = 	'N009-MF000005306' and SF.v_ComponentFieldId = 	'N009-MF000005307' " +
                                "and SF.v_ComponentFieldId = 	'N009-MF000005308' and SF.v_ComponentFieldId = 	'N009-MF000005309' " +
                                "and SF.v_ComponentFieldId = 	'N009-MF000005310' and SF.v_ComponentFieldId = 	'N009-MF000005311' " +
                                "and SF.v_ComponentFieldId = 	'N009-MF000005312' and SF.v_ComponentFieldId = 	'N009-MF000005313' " +
                                "and SF.v_ComponentFieldId = 	'N009-MF000005314' and SF.v_ComponentFieldId = 	'N009-MF000005315' " +
                                "and SF.v_ComponentFieldId = 	'N009-MF000005316' and SF.v_ComponentFieldId = 	'N009-MF000005317' " +
                                "and SF.v_ComponentFieldId = 	'N009-MF000005318' and SF.v_ComponentFieldId = 	'N009-MF000005319' " +
                                "and SF.v_ComponentFieldId = 	'N009-MF000005320' and SF.v_ComponentFieldId = 	'N009-MF000005321' " +
                                "order by SF.v_ComponentFieldId ";
                SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
                SqlDataReader lector = comando.ExecuteReader();
                stsValuesComponent sts = new stsValuesComponent();
                while (lector.Read())
                {
                    sts.OD_Basal_2000_Hrz = lector.GetValue(0).ToString();
                    sts.OD_Actual_3000_Hrz = lector.GetValue(1).ToString();
                    sts.OD_Basal_4000_Hrz = lector.GetValue(2).ToString();
                    sts.OD_Basal_Promedio = lector.GetValue(3).ToString();
                    sts.fecha_Basal = lector.GetValue(4).ToString();
                    sts.OI_Basal_2000_Hrz = lector.GetValue(5).ToString();
                    sts.OI_Basal_3000_Hrz = lector.GetValue(6).ToString();
                    sts.OI_Basal_4000_Hrz = lector.GetValue(7).ToString();
                    sts.OI_Basal_Promedio = lector.GetValue(8).ToString();
                    sts.OD_Actual_2000_Hrz = lector.GetValue(9).ToString();
                    sts.OD_Actual_3000_Hrz = lector.GetValue(10).ToString();
                    sts.OD_Actual_4000_Hrz = lector.GetValue(11).ToString();
                    sts.OD_Actual_Promedio = lector.GetValue(12).ToString();
                    sts.OI_Actual_2000_Hrz = lector.GetValue(13).ToString();
                    sts.OI_Actual_3000_Hrz = lector.GetValue(14).ToString();
                    sts.OI_Actual_4000_Hrz = lector.GetValue(15).ToString();
                    sts.OI_Actual_Promedio = lector.GetValue(16).ToString();
                    sts.fecha_Actual = lector.GetValue(17).ToString();
                    sts.stsOD = lector.GetValue(18).ToString();
                    sts.stsOI = lector.GetValue(19).ToString();
                }
                lector.Close();
                conexion.closesigesoft();
                return sts;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }

        private void UnisReportes(List<string> unirPdfS, string Constante, MergeExPDF mergeExPDF)
        {
            mergeExPDF.FilesName = unirPdfS;
            mergeExPDF.DestinationFile = _ruta + _serviceId + "-" + Constante + ".pdf";
            mergeExPDF.Execute();
            _filesNameToMerge.Add(mergeExPDF.DestinationFile);
        }

        private DiskFileDestinationOptions GenerarReporteUnido(ReportDocument rp, DataSet ds1, string _ruta, DiskFileDestinationOptions objDiskOpt, string Constante)
        {
            rp.SetDataSource(ds1);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + _serviceId + "-" + Constante + ".pdf";
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            return objDiskOpt;
        }

        private void GenerarReporte(ReportDocument rp, DataSet ds1, string _ruta, DiskFileDestinationOptions objDiskOpt, string Constante)
        {
            rp.SetDataSource(ds1);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + _serviceId + "-" + Constante + ".pdf";
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
        }



        #region Methods

        private void GenerateAnexo312(string pathFile)
        {
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var _listAtecedentesOcupacionales = _historyBL.GetHistoryReport(_pacientId); //ARNOLD STORE
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);

            var Antropometria = _serviceBL.ValoresComponente(_serviceId, Constants.ANTROPOMETRIA_ID);
            var FuncionesVitales = _serviceBL.ValoresComponente(_serviceId, Constants.FUNCIONES_VITALES_ID);
            var ExamenFisico = _serviceBL.ValoresComponente(_serviceId, Constants.EXAMEN_FISICO_ID);
            var Oftalmologia = _serviceBL.ValoresComponente(_serviceId, Constants.OFTALMOLOGIA_ID);
            var Psicologia = _serviceBL.ValoresExamenComponete(_serviceId, Constants.PSICOLOGIA_ID, 195); //ARNOLD STORE
            var OIT = _serviceBL.ValoresExamenComponete(_serviceId, Constants.OIT_ID, 211);
            var RX = _serviceBL.ValoresExamenComponete(_serviceId, Constants.RX_TORAX_ID, 135);

            var Laboratorio = new List<ServiceComponentFieldValuesList>();
            var CentroMEdico = _serviceBL.GetInfoMedicalCenter();

            if (CentroMEdico.v_IdentificationNumber == "20519254086")// se hizo para mavimedic (loco cambio el id de informe médico)
            {
                Laboratorio = _serviceBL.ValoresComponente(_serviceId, "N001-ME000000000");
            }
            else
            {
                Laboratorio = _serviceBL.ValoresComponente(_serviceId, Constants.INFORME_LABORATORIO_ID);
            }

            var Audiometria = _serviceBL.GetDiagnosticForAudiometria(_serviceId, Constants.AUDIOMETRIA_ID); //ARNOLD STORE
            var Espirometria = _serviceBL.ValoresExamenComponete(_serviceId, Constants.ESPIROMETRIA_ID, 210); //ARNOLD STORE
            var _DiagnosticRepository = _serviceBL.GetServiceDisgnosticsReports(_serviceId);
            var _Recomendation = _serviceBL.GetServiceRecommendationByServiceId(_serviceId); //ARNOLD STORE
            var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(_serviceId);
            var ValoresDxLab = _serviceBL.ValoresComponenteAMC_(_serviceId, 1); //ARNOLD STORE
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport_New312(_serviceId); //ARNOLD STORE

            FichaMedicaOcupacional312.CreateFichaMedicalOcupacional312Report(_DataService,
                        filiationData, _listAtecedentesOcupacionales, _listaPatologicosFamiliares,
                        _listMedicoPersonales, _listaHabitoNocivos,
                        Audiometria,// Psicologia, OIT, RX,  , Espirometria,
                        _DiagnosticRepository, _Recomendation, _ExamenesServicio, ValoresDxLab, MedicalCenter, //TestIhihara, TestEstereopsis,
                        serviceComponents, pathFile);
        }

        private void CreateFichaMedicaTrabajador2(string pathFile)
        {
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var doctoPhisicalExam = _serviceBL.GetDoctoPhisicalExam(_serviceId);//ARNOLD STORE
            InformeTrabajador.CreateFichaMedicaTrabajador2(filiationData, doctoPhisicalExam, diagnosticRepository, MedicalCenter, pathFile);
        }

        private void GenerateInformeMedicoTrabajador(string pathFile)
        {
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var personMedicalHistory = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var noxiousHabit = _historyBL.GetNoxiousHabitsReport(_pacientId);
            var familyMedicalAntecedent = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
            var anamnesis = _serviceBL.GetAnamnesisReport(_serviceId); //ARNOLD STORE
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            ReportPDF.CreateMedicalReportForTheWorker(filiationData,
                                            personMedicalHistory,
                                            noxiousHabit,
                                            familyMedicalAntecedent,
                                            anamnesis,
                                            serviceComponents,
                                            diagnosticRepository,
                                            _customerOrganizationName,
                                            MedicalCenter,
                                            pathFile);


        }

        private void CreateFichaMedicaTrabajador3(string pathFile)
        {
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport_TODOS(_serviceId);//ARNOLD STORE
            var doctoPhisicalExam = _serviceBL.GetDoctoPhisicalExam(_serviceId);
            var ComponetesConcatenados = _pacientBL.DevolverComponentesConcatenados(_serviceId);//ARNOLD STORE
            var ComponentesLaboratorioConcatenados = _pacientBL.DevolverComponentesLaboratorioConcatenados(_serviceId);//ARNOLD STORE
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var Restricciton = _serviceBL.GetRestrictionByServiceId(_serviceId);//ARNOLD STORE
            InformeTrabajador3.CreateFichaMedicaTrabajador3(filiationData, doctoPhisicalExam, diagnosticRepository, MedicalCenter, ComponetesConcatenados, ComponentesLaboratorioConcatenados, serviceComponents, Restricciton, pathFile);
        }

        private void GenerateAnexo7C(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
            var _Valores = _serviceBL.GetServiceComponentsReport(_serviceId);
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);
            var _PiezasCaries = _serviceBL.GetCantidadCaries(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_CARIES_ID);
            var _PiezasAusentes = _serviceBL.GetCantidadAusentes(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_AUSENTES_ID);
            var CuadroVacio = Common.Utils.BitmapToByteArray(Resources.CuadradoVacio);
            var CuadroCheck = Common.Utils.BitmapToByteArray(Resources.CuadradoCheck);
            var Pulmones = Common.Utils.BitmapToByteArray(Resources.MisPulmones);
            var Audiometria = _serviceBL.ValoresComponenteOdontogramaValue1(_serviceId, Constants.AUDIOMETRIA_ID);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            ReportPDF.CreateAnexo7C(_DataService, filiationData, _Valores, _listMedicoPersonales,
                                    _listaPatologicosFamiliares, _listaHabitoNocivos,
                                    CuadroVacio, CuadroCheck, Pulmones, _PiezasCaries,
                                    _PiezasAusentes, Audiometria, diagnosticRepository, MedicalCenter,
                                    pathFile);

        }

        private void GenerateAptitudYanacocha(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
            var _Valores = _serviceBL.GetServiceComponentsReport(_serviceId);
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);
            var _PiezasCaries = _serviceBL.GetCantidadCaries(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_CARIES_ID);
            var _PiezasAusentes = _serviceBL.GetCantidadAusentes(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_AUSENTES_ID);
            var CuadroVacio = Common.Utils.BitmapToByteArray(Resources.CuadradoVacio);
            var CuadroCheck = Common.Utils.BitmapToByteArray(Resources.CuadradoCheck);
            var Pulmones = Common.Utils.BitmapToByteArray(Resources.MisPulmones);
            var Audiometria = _serviceBL.ValoresComponenteOdontogramaValue1(_serviceId, Constants.AUDIOMETRIA_ID);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            ReportPDF.CreateAptitudYanacocha(_DataService, filiationData, _Valores, _listMedicoPersonales,
                                    _listaPatologicosFamiliares, _listaHabitoNocivos,
                                    CuadroVacio, CuadroCheck, Pulmones, _PiezasCaries,
                                    _PiezasAusentes, Audiometria, diagnosticRepository, MedicalCenter,
                                    pathFile);

        }

        private void GenerateAnsiedadZung(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var _Valores = _serviceBL.ValoresComponente_ObservadoAMC(_serviceId, Constants.PSICOLOGIA_ID); //ARNOLD STORE
            InformeAnsiedadZung.CreateInformeAnsiedadZung(_DataService, MedicalCenter, _Valores, pathFile);
        }

        private void GenerateEscalafatiga(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var _Valores = _serviceBL.ValoresComponente_ObservadoAMC(_serviceId, Constants.PSICOLOGIA_ID);
            Informeintensidadfatiga.CreateInformeintensidadfatiga(_DataService, MedicalCenter, _Valores, pathFile);
        }

        private void GenerateInventarioMaslach(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var _Valores = _serviceBL.ValoresComponente_ObservadoAMC(_serviceId, Constants.PSICOLOGIA_ID);
            InformeMaslach.CreateInformeMaslach(_DataService, MedicalCenter, _Valores, pathFile);
        }

        private void GenerateTestSomnolencia(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var _Valores = _serviceBL.ValoresComponente_ObservadoAMC(_serviceId, Constants.PSICOLOGIA_ID);
            TestSomnolencia.CreateTestSomnolencia(_DataService, MedicalCenter, _Valores, pathFile);
        }

        private void GenerateAnexo16Coimolache(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
            var _Valores = _serviceBL.GetServiceComponentsReport(_serviceId);
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);
            var _PiezasCaries = _serviceBL.GetCantidadCaries(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_CARIES_ID);
            var _PiezasAusentes = _serviceBL.GetCantidadAusentes(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_AUSENTES_ID);
            var CuadroVacio = Common.Utils.BitmapToByteArray(Resources.CuadradoVacio);
            var CuadroCheck = Common.Utils.BitmapToByteArray(Resources.CuadradoCheck);
            var Pulmones = Common.Utils.BitmapToByteArray(Resources.MisPulmones);
            var Audiometria = _serviceBL.ValoresComponenteOdontograma1(_serviceId, Constants.AUDIOMETRIA_ID);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            ReportPDF.CreateAnexo16Coimolache(_DataService, filiationData, _Valores, _listMedicoPersonales,
                                    _listaPatologicosFamiliares, _listaHabitoNocivos,
                                    CuadroVacio, CuadroCheck, Pulmones, _PiezasCaries,
                                    _PiezasAusentes, Audiometria, diagnosticRepository, MedicalCenter,
                                    pathFile);

        }

        private void GenerateAnexo16Pacasmayo(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
            var _Valores = _serviceBL.GetServiceComponentsReport(_serviceId);
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);
            var _PiezasCaries = _serviceBL.GetCantidadCaries(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_CARIES_ID);
            var _PiezasAusentes = _serviceBL.GetCantidadAusentes(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_AUSENTES_ID);
            var CuadroVacio = Common.Utils.BitmapToByteArray(Resources.CuadradoVacio);
            var CuadroCheck = Common.Utils.BitmapToByteArray(Resources.CuadradoCheck);
            var Pulmones = Common.Utils.BitmapToByteArray(Resources.MisPulmones);
            var Audiometria = _serviceBL.ValoresComponenteOdontograma1(_serviceId, Constants.AUDIOMETRIA_ID);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            ReportPDF.CreateAnexo16Pacasmayo(_DataService, filiationData, _Valores, _listMedicoPersonales,
                                    _listaPatologicosFamiliares, _listaHabitoNocivos,
                                    CuadroVacio, CuadroCheck, Pulmones, _PiezasCaries,
                                    _PiezasAusentes, Audiometria, diagnosticRepository, MedicalCenter,
                                    pathFile);

        }

        private void GenerateAnexo16MinsurSanRafael(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
            var _Valores = _serviceBL.GetServiceComponentsReport(_serviceId);
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);
            var _PiezasCaries = _serviceBL.GetCantidadCaries(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_CARIES_ID);
            var _PiezasAusentes = _serviceBL.GetCantidadAusentes(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_AUSENTES_ID);
            var CuadroVacio = Common.Utils.BitmapToByteArray(Resources.CuadradoVacio);
            var CuadroCheck = Common.Utils.BitmapToByteArray(Resources.CuadradoCheck);
            var Pulmones = Common.Utils.BitmapToByteArray(Resources.MisPulmones);
            var Audiometria = _serviceBL.ValoresComponenteOdontograma1(_serviceId, Constants.AUDIOMETRIA_ID);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            ReportPDF.CreateAnexo16MinsurSanRafael(_DataService, filiationData, _Valores, _listMedicoPersonales,
                                    _listaPatologicosFamiliares, _listaHabitoNocivos,
                                    CuadroVacio, CuadroCheck, Pulmones, _PiezasCaries,
                                    _PiezasAusentes, Audiometria, diagnosticRepository, MedicalCenter,
                                    pathFile);

        }

        private void GenerateAnexo16Yanacocha(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
            var _Valores = _serviceBL.GetServiceComponentsReport(_serviceId);
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);
            var _PiezasCaries = _serviceBL.GetCantidadCaries(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_CARIES_ID);
            var _PiezasAusentes = _serviceBL.GetCantidadAusentes(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_AUSENTES_ID);
            var CuadroVacio = Common.Utils.BitmapToByteArray(Resources.CuadradoVacio);
            var CuadroCheck = Common.Utils.BitmapToByteArray(Resources.CuadradoCheck);
            var Pulmones = Common.Utils.BitmapToByteArray(Resources.MisPulmones);
            var Audiometria = _serviceBL.ValoresComponenteOdontograma1(_serviceId, Constants.AUDIOMETRIA_ID);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            ReportPDF.CreateAnexo16Yanacocha(_DataService, filiationData, _Valores, _listMedicoPersonales,
                                    _listaPatologicosFamiliares, _listaHabitoNocivos,
                                    CuadroVacio, CuadroCheck, Pulmones, _PiezasCaries,
                                    _PiezasAusentes, Audiometria, diagnosticRepository, MedicalCenter,
                                    pathFile);

        }

        private void GenerateAnexo16Shahuindo(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
            var _Valores = _serviceBL.GetServiceComponentsReport(_serviceId);
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);
            var _PiezasCaries = _serviceBL.GetCantidadCaries(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_CARIES_ID);
            var _PiezasAusentes = _serviceBL.GetCantidadAusentes(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_AUSENTES_ID);
            var CuadroVacio = Common.Utils.BitmapToByteArray(Resources.CuadradoVacio);
            var CuadroCheck = Common.Utils.BitmapToByteArray(Resources.CuadradoCheck);
            var Pulmones = Common.Utils.BitmapToByteArray(Resources.MisPulmones);
            var Audiometria = _serviceBL.ValoresComponenteOdontograma1(_serviceId, Constants.AUDIOMETRIA_ID);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            ReportPDF.CreateAnexo16Shahuindo(_DataService, filiationData, _Valores, _listMedicoPersonales,
                                    _listaPatologicosFamiliares, _listaHabitoNocivos,
                                    CuadroVacio, CuadroCheck, Pulmones, _PiezasCaries,
                                    _PiezasAusentes, Audiometria, diagnosticRepository, MedicalCenter,
                                    pathFile);

        }
           
        private void Generate_CULTIVO_DE_SALMONELLA_Y_SHINGELLA(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var _NombreMedico = _pacientBL.GetNombreMedicoLab(_serviceId);
            var _NombreEmpresa = _pacientBL.GetNombreEmpresaLab(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Laboratorio, _serviceId);
            CULTIVO_DE_SALMONELLA_Y_SHINGELLA.CreateExamen_CULTIVO_DE_SALMONELLA_Y_SHINGELLA(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, _NombreEmpresa, _NombreMedico);
        }
        private void Generate_SECRESION_NASOFARINGEA(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var _NombreMedico = _pacientBL.GetNombreMedicoLab(_serviceId);
            var _NombreEmpresa = _pacientBL.GetNombreEmpresaLab(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Laboratorio, _serviceId);
            SECRESION_NASOFARINGEA.Create_SECRESION_NASOFARINGEA(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, _NombreEmpresa, _NombreMedico);
        }
        #region COVID (ARNOLD)

        //NUEVA FICHA UNICA DE RESULTADOS

        private void GenerateFicha_Investigacion_Clinica_Epidemiologica_Unica_COV(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.GetFirmaMedicos(175);
            Ficha_Investigacion_Clinica_Epidemiologica_Unica_COV.CreateFicha_Investigacion_Clinica_Epidemiologica_Unica_COV(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo);
        }


        /// /////////////////////

        private void Generate_FICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_ANTIGENO_COVID_19(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var _NombreMedico = _pacientBL.GetNombreMedicoLab(_serviceId);
            var _NombreEmpresa = _pacientBL.GetNombreEmpresaLab(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            //var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.LaboCovid19, _serviceId);
            var medicoOcupacional = _serviceBL.GetFirmaMedicos(175);
            FICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_ANTIGENO_COVID_19.CreateFICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_ANTIGENO_COVID_19(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, _NombreMedico, _NombreEmpresa, medicoOcupacional);
        }

        private void Generate_Anticuerpos_Neutralizantes_COVID_19_OCUP(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var _NombreMedico = _pacientBL.GetNombreMedicoLab(_serviceId);
            var _NombreEmpresa = _pacientBL.GetNombreEmpresaLab(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            //var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.LaboCovid19, _serviceId);
            var medicoOcupacional = _serviceBL.GetFirmaMedicos(175);
            Anticuerpos_Neutralizantes_COVID_19_OCUP.CreateAnticuerpos_Neutralizantes_COVID_19_OCUP(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, _NombreMedico, _NombreEmpresa, medicoOcupacional);
        }

        private void GenerateInvestigacionEpidemiologicaCOVID19(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.GetFirmaMedicos(175); //ARNOLD STORE
            InvestigacionEpidemiologicaCOVID19.CreateInvestigacionEpidemiologicaCOVID19(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo);
        }
        private void GenerateInvestigacionEpidemiologicaMOLECULARCOVID19(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            //var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            var datosGrabo = _serviceBL.GetFirmaMedicos(175);
            InvestigacionEpidemiologicaPRUEBAMOLECULARCOVID19.CreateInvestigacionEpidemiologicaPRUEBAMOLECULARCCOVID19(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo);
        }

        private void GenerateInvestigacionEpidemiologicaANTIGENOCOVID19(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.GetFirmaMedicos(175);
            InvestigacionEpidemiologicaPRUEBAANTIGENOCOVID19_LC.CreateInvestigacionEpidemiologicaPRUEBAANTIGENOCOVID19_LC(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo);
        }
        private void GenerateFichaSintomatologicaCovid19(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            FichaSintomatologicaCovid19.CreateFichaSintomatologicaCovid19(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents);
        }
        private void GenerateCertificado_evaluacion_salud_Covid(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            Certificado_evaluacion_salud_Covid.CreateCertificado_evaluacion_salud_Covid(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo);
        }
        private void GenerateDeclaracionJuradaCovid19(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            DeclaracionJuradaCovid19.CreateDeclaracionJuradaCovid19(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents);
        }
        private void GeneratePruebaRapidaCovid19(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            PruebaRapidaCovid19.CreatePruebaRapidaCovid19(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo);
        }
        private void GenerateFICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_RÁPIDA_COVID_19(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.LaboCovid19, _serviceId);

            var MedicoOcupacional = _serviceBL.GetFirmaMedicos(175);
            FICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_RÁPIDA_COVID_19.CreateFICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_RÁPIDA_COVID_19(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo, MedicoOcupacional);
        }
        private void GenerateFICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_MOLECULAR_COVID_19(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.LaboCovid19, _serviceId);
            FICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_MOLECULAR_COVID_19.CreateFICHA_DE_REPORTE_DE_RESULTADOS_DE_PRUEBA_MOLECULAR_COVID_19(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo);
        }
        private void GenerateConsentimientoInformadoExamenCovid19(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            ConsentimientoInformadoExamenCovid19.CreateConsentimientoInformadoExamenCovid19(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents);
        }
        private void GenerateConsentimientoInformadoExamenMOLECULARCovid19(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            ConsentimientoInformadoExamenPRUEBAMOLECULARCovid19.CreateConsentimientoInformadoExamenMOLECULARCovid19(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents);
        }

        private void GenerateConsentimientoInformadoExamenANTIGENOCovid19(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            ConsentimientoInformadoExamenPRUEBAANTIGENOCovid19.CreateConsentimientoInformadoExamenPRUEBAANTIGENOCovid19(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents);
        }
        private void GenerateConstancia_de_Salud_Para_Prueba_Covid(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            Constancia_de_Salud_Para_Prueba_Covid.CreateConstancia_de_Salud_Para_Prueba_Covid(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents);
        }
        private void GenerateCertificadoMédicodeBuenEstadodeSaludFísica(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);

            CertificadoMédicodeBuenEstadodeSaludFísica.CreateCertificadoMédicodeBuenEstadodeSaludFísica(_DataService, pathFile, datosP, MedicalCenter, filiationData, datosGrabo);
        }

        private void GenerateFichadeSintomatologiaCovid19ParaRegresoalTrabajoDeclaracionJurada(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            FichadeSintomatologiaCovid19ParaRegresoalTrabajoDeclaracionJurada.CreateFichadeSintomatologiaCovid19ParaRegresoalTrabajoDeclaracionJurada(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo);
        }
        private void GenerateFORMATO_DE_EVALUACIÓN_DE_SALUD_RÁPIDA_FRENTE_A_COVID_19(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            FORMATO_DE_EVALUACIÓN_DE_SALUD_RÁPIDA_FRENTE_A_COVID_19.CreateFORMATO_DE_EVALUACIÓN_DE_SALUD_RÁPIDA_FRENTE_A_COVID_19(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo, diagnosticRepository);
        }
        #endregion
        private void GenerateAnexo16GoldField(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
            var _Valores = _serviceBL.GetServiceComponentsReport(_serviceId);
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);
            var _PiezasCaries = _serviceBL.GetCantidadCaries(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_CARIES_ID);
            var _PiezasAusentes = _serviceBL.GetCantidadAusentes(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_AUSENTES_ID);
            var CuadroVacio = Common.Utils.BitmapToByteArray(Resources.CuadradoVacio);
            var CuadroCheck = Common.Utils.BitmapToByteArray(Resources.CuadradoCheck);
            var Pulmones = Common.Utils.BitmapToByteArray(Resources.MisPulmones);
            var Audiometria = _serviceBL.ValoresComponenteOdontograma1(_serviceId, Constants.AUDIOMETRIA_ID);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            ReportPDF.CreateAnexo16GoldField(_DataService, filiationData, _Valores, _listMedicoPersonales,
                                    _listaPatologicosFamiliares, _listaHabitoNocivos,
                                    CuadroVacio, CuadroCheck, Pulmones, _PiezasCaries,
                                    _PiezasAusentes, Audiometria, diagnosticRepository, MedicalCenter,
                                    pathFile);

        }
        private void GenerateAnexo16Hudbay(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
            var _Valores = _serviceBL.GetServiceComponentsReport(_serviceId);
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);
            var _PiezasCaries = _serviceBL.GetCantidadCaries(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_CARIES_ID);
            var _PiezasAusentes = _serviceBL.GetCantidadAusentes(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_AUSENTES_ID);
            var CuadroVacio = Common.Utils.BitmapToByteArray(Resources.CuadradoVacio);
            var CuadroCheck = Common.Utils.BitmapToByteArray(Resources.CuadradoCheck);
            var Pulmones = Common.Utils.BitmapToByteArray(Resources.MisPulmones);
            var Audiometria = _serviceBL.ValoresComponenteOdontograma1(_serviceId, Constants.FICHA_AUDIOMETRICA_HUDBAY_ID);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var antecedentesOcupacionales = _serviceBL.AntecedentesOcupacionales(filiationData.v_PersonId);
            
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var antecedentes = _pacientBL.AntecedentesmEDICOS(datosP.v_PersonId);

            var servicesId7 = new List<string>();
            servicesId7.Add(_serviceId);
            var componentReportId7 = new ServiceBL().ObtenerIdsParaImportacionExcel(servicesId7, 11);
            var uc = new ServiceBL().ReporteOsteomuscular(_serviceId, componentReportId7[0].ComponentId);

            var audioUserControlListBasal = _serviceBL.ReportAudiometriaUserControl(_serviceId, Constants.FICHA_AUDIOMETRICA_BASAL_HUDBAY_ID);


            ReportPDF.CreateAnexo16Hudbay(_DataService, filiationData, _Valores, _listMedicoPersonales,
                                    _listaPatologicosFamiliares, _listaHabitoNocivos,
                                    CuadroVacio, CuadroCheck, Pulmones, _PiezasCaries,
                                    _PiezasAusentes, Audiometria, diagnosticRepository, MedicalCenter,
                                    pathFile, LogoEmpresa, antecedentesOcupacionales, antecedentes, uc, audioUserControlListBasal);

        }

        private void GenerateInformeExamenClinico(string pathFile)
        {
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var personMedicalHistory = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var noxiousHabit = _historyBL.GetNoxiousHabitsReport(_pacientId);
            var familyMedicalAntecedent = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
            var anamnesis = _serviceBL.GetAnamnesisReport(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var doctoPhisicalExam = _serviceBL.GetDoctoPhisicalExam(_serviceId);

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            ReportPDF.CreateMedicalReportForExamenClinico(filiationData,
                                            personMedicalHistory,
                                            noxiousHabit,
                                            familyMedicalAntecedent,
                                            anamnesis,
                                            serviceComponents,
                                            diagnosticRepository,
                                            _customerOrganizationName,
                                            MedicalCenter,
                                            pathFile,
                                            doctoPhisicalExam);


        }

        private void GenerateLaboratorioReport(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            LaboratorioReport.CreateLaboratorioReport(filiationData, serviceComponents, MedicalCenter, pathFile);
        }

        private void GenerateMiExamen(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);

            MiExamen.CreateMiExamen(serviceComponents, MedicalCenter, datosP, pathFile);
        }

        private void GenerateInformeSAS(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var _DataService = _serviceBL.GetServiceReport(_serviceId);

            INFORME_SAS_REPORT.CreateReportSAS(filiationData, _DataService, serviceComponents, MedicalCenter, datosP, pathFile);
        }

        private void GenerateExamenOftalmologicoSimple(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Oftalmología, _serviceId);

            Examen_Oftalmologico_Simple.CreateExamen_Oftalmologico_Simple(filiationData, _DataService, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, diagnosticRepository);
        }

        private void GenerateExamenOftalmologicoCompleto(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Oftalmología, _serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            Examen_Oftalmologico_Completo.CreateExamen_Oftalmologico_Completo(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, diagnosticRepository);
        }

        private void GenerateRADIOGRAFIA_DE_TORAX_SIMPLE(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Rx, _serviceId);

            RADIOGRAFIA_DE_TORAX_SIMPLE.CreateRADIOGRAFIA_DE_TORAX_SIMPLE(filiationData, _DataService, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, diagnosticRepository);
        }

        private void GenerateEvaluavionOftalmologicaYanacocha(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Oftalmología, _serviceId);
            var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            ApendiceN2_Evaluacion_Oftalmologica_Yanacocha.CreateApendiceN2_Evaluacion_Oftalmologica_Yanacocha(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, _ExamenesServicio, diagnosticRepository);
        }

        private void GenerateInformeOftalmologicoHudbay(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Oftalmología, _serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            Informe_Oftalmologico_Hudbay.CreateInforme_Oftalmologico_Hudbay(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, diagnosticRepository, LogoEmpresa);
        }

        private void GenerateInformeCardiologicoHudbay(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Cardiologia, _serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            InformeCardiologicoHudbay.CreateInformeCardiologicoHudbay(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, diagnosticRepository, LogoEmpresa);
        }

        public void GenerateFichaPsicologicaGoldfies(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Psicologia, _serviceId);
            FichaPsicologicaGoldfields.CreateFichaPsicologicaGoldfields(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo);
        }

        public void GenerateDeclaracionJuradaCoimolacheLaZanja(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);

            DeclaracionJuradaPsicologia_Coimolache_LaZanja.CreateDeclaracionJuradaCoimolacheLaZanja(filiationData, serviceComponents, MedicalCenter, datosP, pathFile);
        }

        public void GenerateInformePsicologicoGoldfieds(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var _InformacionHistoriaPsico = _serviceBL.GetHistoriaClinicaPsicologica(_serviceId, Constants.FICHA_PSICOLOGICA_OCUPACIONAL_GOLDFIELDS);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Psicologia, _serviceId);

            InformePsicologicoGoldfields.CreateInformePsicologicoGoldfields(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo);
        }
        public void GenerateReportePsicologicoBrigadistas(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var _InformacionHistoriaPsico = _serviceBL.GetHistoriaClinicaPsicologica(_serviceId, Constants.FICHA_PSICOLOGICA_OCUPACIONAL_GOLDFIELDS);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Psicologia, _serviceId);

            REPORTE_PSICOLOGICO_DE_BRIGADISTAS.CreateReportePsicologicoBrigadistas(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo);
        }

        private void GenerateCertificadoPsicosensometricoDatos(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Psicosensometrico, _serviceId);

            Certificado_Psicosensometrico_Datos.CreateCertificadoPsicosensometricoDatos(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo);
        }

        private void GenerateAltura_Fisica_F_Yanacocha(string pathFile)
        {
            var _DataService = _serviceBL.GetInformacion_OtrosExamenes(_serviceId);

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            //var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);

            Altura_Fisica_F_Yanacocha.CreateAltura_Fisica_F_Yanacocha(_DataService, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo);
        }

        private void GenerateOsteMuscular_Mibanco(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            Osteomuscular_MiBanco.CreateOsteoMuscularMibanco(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, diagnosticRepository);
        }

        private void GenerateAccidentesTrabajoF1(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);

            AccidentesTrabajo_F1.CreateAccidentesTrabajoF1(serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo);
        }

        private void GenerateAccidentesTrabajoF2(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);

            AccidentesTrabajo_F2.CreateAccidentesTrabajoF2(serviceComponents, MedicalCenter, pathFile, datosGrabo);
        }

        private void GenerateExamenSuficienciaMedicaOperadores(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);


            EXAMEN_SUF_MED_OPERADORES_EQUIPOS_MOVILES.CreateExamenSuficienciaMedicaOperadores(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo);
        }

        private void GenerateCertificadoSuficienciaMedicaTC(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);

            CERTIFICADO_SUFICIENCIA_MEDICA_TC.CreateCertificadoSuficienciaTC(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo);
        }

        private void GenerateExoneraxionLaboratorio(string pathFile)
        {
            var exams = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            Exoneracion_Laboratorio.CreateExoneracionLaboratorio(filiationData, pathFile, datosP, MedicalCenter, exams, diagnosticRepository);
        }

        private void GenerateInformeMedicoTrabajadorInternacional(string pathFile, string ServicioId, string PacienteId)
        {
            PacientBL oPacientBL = new PacientBL();

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var Cabecera = oPacientBL.DevolverDatosPaciente(ServicioId);
            var AntOcupacionales = _historyBL.GetHistoryReport(_pacientId);
            var HabitosNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);
            var AntPersonales = oPacientBL.DevolverAntecedentesPersonales(PacienteId);//ARNOLD STORE
            var Valores = _serviceBL.GetServiceComponentsReport(ServicioId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(ServicioId);
            var AntFami = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
            var Reco = _serviceBL.ConcatenateRecommendationByService(ServicioId);//ARNOLD STORE
            InformeMedicoTrabajadorInternacional.CreateInformeMedicoTrabajadorInternacional(pathFile, Cabecera, HabitosNocivos, AntFami, Valores, diagnosticRepository, AntPersonales, AntOcupacionales, MedicalCenter, Reco);
        }

        private void GenerateExoneraxionPlacaTorax(string pathFile)
        {
            var exams = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            Exoneracion_Placa_Torax_PA.CreateExoneracionPlacaTorax(filiationData, pathFile, datosP, MedicalCenter, exams, diagnosticRepository, serviceComponents);
        }

        private void GenerateExoneracionEspirometria(string pathFile)
        {
            var exams = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            ExoneracionEspirometria.CreateExoneracionEspirometria(filiationData, pathFile, datosP, MedicalCenter, exams, diagnosticRepository, serviceComponents);
        }

        public void GenerateRegistroInformeEMOBuenaventura(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);

            Registro_Entrega_Informe_Resultados_EMO_BUENAVENTURA.CreateRegistroInformeEMOBuenaventura(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo);
        }

        private void GenerateDeclaracionJuradaRX(string pathFile)
        {
            var exams = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);

            DeclaracionJuradaRX.CreateDeclaracionJurada(filiationData, pathFile, datosP, MedicalCenter, exams, diagnosticRepository, serviceComponents);
        }

        private void GenerateInformeResultadosAutorizacion(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);

            InformedeResultados_Autorización.CreateInformeResultadosAutorizacion(filiationData, pathFile, datosP, MedicalCenter);
        }
        private void GenerateAutorizacion_Realizacion_Ex_Lumina(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter_ExoLab();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);

            Autorizacion_Realizacion_Ex_Lumina.CreateAutorizacion_Realizacion_Ex_Lumina(pathFile, datosP, MedicalCenter, filiationData);
        }
        private void GeneConsentimientoInformadoParaAurotizarEMO(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter_ExoLab();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);

            CONSENTIMIENTO_INFORMADO_AUTORIZAR_EMO.CreateConseitimientoInformadoParaAutorizarEMO(pathFile, filiationData, datosP, MedicalCenter);
        }
        private void GenerateAutorizacion_Liberacion_SanMartin(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter_ExoLab();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);

            Aut_Liberacion_San_Martin.CreateAutorizacionLiberacionInformacionMedicaSanMartin(pathFile, datosP, MedicalCenter, filiationData);
        }
        private void GenerateActaEntregaYLecturaResultadosEMO_GOLD_MUR(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter_ExoLab();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);

            Acta_Entrega_Lectura_Resultados_EMO_GOLD_MUR.CreateActaEntregaYLecturaResultadosEMO_GOLD_MUR(pathFile, datosP, MedicalCenter, filiationData);
        }
        private void GenerateInformeResultadosAutorizacionCoimolache(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);

            Consentimiento_Informado_Ex_Med.CreateInformeResultadosAutorizacionCoimolache(filiationData, pathFile, datosP, MedicalCenter);
        }
        private void GenerateConstanciadeSuficienciaMedicaParaBrigadistas(string pathFile)
        {
            OperationResult objOperationResult = new OperationResult();
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var service = _serviceBL.GetService_brigadistas(ref objOperationResult, _serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioFirma(service.i_UpdateUserOccupationalMedicaltId.Value); //ARNOLD STORE

            CONSTANCIA_SUFICIENCIA_MEDICA_BRIGADISTAS.CreateConstanciadeSuficienciaMedicaParaBrigadistas(filiationData, pathFile, datosP, MedicalCenter, serviceComponents, service, datosGrabo);
        }
        private void GenerateConsentimientoInformadoCIH(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            
            ConsensimientoInformadoVIH.CreateConsentimientoInformadoCIH(pathFile, datosP, MedicalCenter, filiationData);
        }
        private void GenerateCertificado_Suficiencia_Medica_Trabajos_Altura(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            Certificado_Suficiencia_Medica_Trabajos_Altura.CreateCertificado_Suficiencia_Medica_Trabajos_Altura(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo);
        }
        private void GenerateEnfermedad_Chagas(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            Enfermedad_Chagas.CreateEnfermedad_Chagas(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo);
        }
        private void GenerateTest_Exposicion_Al_Calor(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            Test_Exposicion_Al_Calor.CreateTest_Exposicion_Al_Calor(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo);
        }
        private void GenerateAutorizacion_Rx(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            Autorizacion_Rx.CreateAutorizacion_Rx(pathFile, datosP, MedicalCenter, filiationData, serviceComponents);
        }
        private void GenerateAutorizacion_VIH(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            Autorizacion_VIH.CreateAutorizacion_VIH(pathFile, datosP, MedicalCenter, filiationData, serviceComponents);
        }
        private void Generate_PARASITOLOGICO_COPROCULTIVO_CIELO_AZUL(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Laboratorio, _serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            Coprocultivo_PSeriado.CreateExamen_PARASITOLOGICO_COPROCULTIVO_CIELO_AZUL(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, diagnosticRepository);
        }

        //private void Generate_Laboratorio_Asistencial(string pathFile)
        //{
        //    var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
        //    var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
        //    var _NombreMedico = _pacientBL.GetNombreMedicoLab(_serviceId);
        //    var _NombreEmpresa = _pacientBL.GetNombreEmpresaLab(_serviceId);
        //    var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
        //    var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
        //    //var _DataService = _serviceBL.GetServiceReport(_serviceId);
        //    var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Laboratorio, _serviceId);
        //    var Cia = _serviceBL.GetProtocoloSolo(_serviceId);

        //    var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

        //    InformeLaboratorioAsistencial.CreateExamen_InformeLaboratorioAsistencial(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, diagnosticRepository, _NombreMedico, _NombreEmpresa);
        //}
        private void Generate_AGLUTINACIONES_KOH_SECRECION(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Laboratorio, _serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            Coprocultivo_PSeriado.CreateExamen_AGLUTINACIONES_KOH_SECRECION(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, diagnosticRepository);
        }

        private void Generate_PARASITOLOGICO_COPROCULTIVO_LUMINA(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            //var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Laboratorio, _serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            Coprocultivo_PSeriado_Lumina.CreateExamen_PARASITOLOGICO_COPROCULTIVO_LUMINA(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, diagnosticRepository);
        }

        private void Generate_AGLUTINACIONES_KOH_SECRECION_LUMINA(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Laboratorio, _serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            Coprocultivo_PSeriado_Lumina.CreateExamen_AGLUTINACIONES_KOH_SECRECION_LUMINA(filiationData, serviceComponents, MedicalCenter, datosP, pathFile, datosGrabo, diagnosticRepository);
        }

        private void GenerateAnexo3_Exoneracion_ResponsabilidadYanacocha(string pathFile)
        {
            var _DataService = _serviceBL.GetInformacion_OtrosExamenes(_serviceId);//ARNOLD STORE
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);

            Anexo3_Exo_Resp_Yanacocha.CreateAnexo3_Exoneracion_ResponsabilidadYanacocha(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents);
        }

        private void GenerateDeclaracionJurada(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);

            DeclaracionJurada.CreateDeclaracionJurada(pathFile, datosP, MedicalCenter, filiationData, serviceComponents);
        }

        private void GenerateExamen_Medico_Visitantes_GoldFields(string pathFile)
        {
            var _DataService = _serviceBL.GetInformacion_OtrosExamenes(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            Examen_Medico_Visitantes_GoldFields.CreateExamen_Medico_Visitantes_GoldFields(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents);
        }

        private void GenerateMarcobrePaseMedico(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var habitosPersonales = new PacientBL().DevolverHabitos_PersonalesSolo(datosP.v_PersonId);

            var Restricciton = _serviceBL.GetRestrictionByServiceId(_serviceId);

            Marcobre_Pase_Medico.CreateMarcobrePaseMedico(_DataService, pathFile, datosP, MedicalCenter, serviceComponents, diagnosticRepository, habitosPersonales);
        }

        private void GenerateTOXICOLOGICO_COCAINA_MARIHUANA_TODOS(string pathFile)
        {
            var _DataService = _serviceBL.GetInformacion_Laboratorio(_serviceId); //ARNOLD STORE
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Laboratorio, _serviceId);
            TOXICOLOGICO_COCAINA_MARIHUANA_TODOS.CreateTOXICOLOGICO_COCAINA_MARIHUANA_TODOS(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo);
        }
        private void GenerateConsentimientoInformadoToxicologia_Hudbay(string pathFile)
        {
            var _DataService = _serviceBL.GetInformacion_Laboratorio(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Laboratorio, _serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            ConsentimientoInformadoToxicologia_Hudbay.CreateConsentimientoInformadoToxicologia_Hudbay(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo, LogoEmpresa);
        }

        private void GenerateNotificacionCualitativosParaDrogasenOrina(string pathFile)
        {
            var _DataService = _serviceBL.GetInformacion_Laboratorio(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Laboratorio, _serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            NotificacionCualitativosParaDrogasenOrina.CreateNotificacionCualitativosParaDrogasenOrina(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo, LogoEmpresa);
        }

        private void GenerateInformeMetalesPesadosHudbay(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Laboratorio, _serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            InformeMetalesPesadosHudbay.CreateInformeMetalesPesadosHudbay(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, LogoEmpresa, datosGrabo);
        }

        private void GenerateFichaInmunizaciones_Hudbay(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            FichaInmunizaciones_Hudbay.CreateFichaInmunizaciones_Hudbay(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, LogoEmpresa, datosGrabo);
        }

        private void GenerateMIniTestPsiquiatricoHudbay(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Psicologia, _serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            MIniTestPsiquiatricoHudbay.CreateMIniTestPsiquiatricoHudbay(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, LogoEmpresa, datosGrabo);
        }

        private void GenerateCuestionarioManipulador_de_Alimentos_Hudbay(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            CuestionarioManipulador_de_Alimentos_Hudbay.CreateCuestionarioManipulador_de_Alimentos_Hudbay(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, LogoEmpresa, datosGrabo);
        }

        private void GenerateInformeMedicoAptitudOcupacionaParaEmpresa(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var service = _serviceBL.GetServiceSolo(_serviceId);
            var protocolo = _serviceBL.GetProtocoloSolo(service.v_ProtocolId);

            InformeMedicoAptitudOcupacionaParaEmpresa.CreateInformeMedicoAptitudOcupacionaParaEmpresa(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, LogoEmpresa, datosGrabo, diagnosticRepository, service, protocolo);
        }

        private void GenerateEvaluacionNeurologicaTrabajosAlturaEstructural_Hudbay(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            EvaluacionNeurologicaTrabajosAlturaEstructural_Hudbay.CreateEvaluacionNeurologicaTrabajosAlturaEstructural_Hudbay(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, LogoEmpresa, datosGrabo);
        }

        private void GenerateDescripcionEspecificaPuestoTrabajo(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            DescripcionEspecificaPuestoTrabajo.CreateDescripcionEspecificaPuestoTrabajo(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, LogoEmpresa, datosGrabo);
        }

        private void GenerateTestAudit_Hudbay(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Psicologia, _serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            TestAudit_Hudbay.CreateTestAudit_Hudbay(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, LogoEmpresa, datosGrabo);
        }

        private void GenerateDeclaracionJuradaDeSalud_Hudbay(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            var antecedentes = _pacientBL.AntecedentesmEDICOS(datosP.v_PersonId);

            DeclaracionJuradaDeSalud_Hudbay.CreateDeclaracionJuradaDeSalud_Hudbay(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, LogoEmpresa, datosGrabo, antecedentes);
        }

        private void GenerateDeclaracionJuradaMusculoEsqueletica_Hudbay(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.MedicinaFisicaRehabilitacionOcupacional	, _serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            var antecedentes = _pacientBL.AntecedentesmEDICOS(datosP.v_PersonId);

            DeclaracionJuradaMusculoEsqueletica_Hudbay.CreateDeclaracionJuradaMusculoEsqueletica_Hudbay(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, LogoEmpresa, datosGrabo, antecedentes);
        }
        private void GenerateDeclaracionJuradaCumplimientoInstrucciones_Hudbay(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);

            DeclaracionJuradaCumplimientoInstrucciones_Hudbay.CreateDeclaracionJuradaCumplimientoInstrucciones_Hudbay(pathFile, datosP, MedicalCenter, filiationData, LogoEmpresa);
        }
        private void GenerateExamenMedicoVisita_Hudbay(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            ExamenMedicoVisita_Hudbay.CreateExamenMedicoVisita_Hudbay(pathFile, datosP, MedicalCenter, filiationData, LogoEmpresa, serviceComponents, datosGrabo, diagnosticRepository);
        }

        private void GenerateInformeMedicoAptitudOcupacionalParaTrabajador(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var audioUserControlList = _serviceBL.ReportAudiometriaUserControl(_serviceId, Constants.FICHA_AUDIOMETRICA_HUDBAY_ID);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            InformeMedicoAptitudOcupacionalParaTrabajador.CreateInformeMedicoAptitudOcupacionalParaTrabajador(pathFile, datosP, MedicalCenter, filiationData, LogoEmpresa, serviceComponents, datosGrabo, audioUserControlList, _listMedicoPersonales, diagnosticRepository);
        }

        private void GenerateFichaAudiometrica_Hudbay(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Audiometria, _serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            int enfermeraAudiometria = 207;
            var enfermera = _serviceBL.GetFirmaMedicos(enfermeraAudiometria);
            var audioUserControlList = _serviceBL.ReportAudiometriaUserControl(_serviceId, Constants.FICHA_AUDIOMETRICA_HUDBAY_ID);
            var audioUserControlListBasal = _serviceBL.ReportAudiometriaUserControl(_serviceId, Constants.FICHA_AUDIOMETRICA_BASAL_HUDBAY_ID);

            FichaAudiometrica_Hudbay.CreateFichaAudiometrica_Hudbay(pathFile, datosP, MedicalCenter, filiationData, LogoEmpresa, serviceComponents, datosGrabo, diagnosticRepository, audioUserControlList, audioUserControlListBasal, enfermera);
        }

        private void GenerateDeclaracionCompromisoPerdidaPeso_Hudbay(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);

            DeclaracionCompromisoPerdidaPeso_Hudbay.CreateDeclaracionCompromisoPerdidaPeso_Hudbay(pathFile, datosP, MedicalCenter, filiationData, LogoEmpresa);
        }


        private void GenerateConsentimientoInformadoAccesoHistoriaClinica(string pathFile)
        {
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            ConsentimientoInformadoAccesoClinica.CreateConsentimientoInformadoAccesoHistoriClinica(filiationData, pathFile, datosP, diagnosticRepository, LogoEmpresa, MedicalCenter);
        }

        private void GenerateInformeMedicoAptitudOcupacional(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var exams = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);

            InformeMedicoDeAptitudOcupacional_Empresa.CreateInformeMedicoAptitudOcupacionalEmpresa(_DataService, pathFile, datosP, MedicalCenter, exams, diagnosticRepository, serviceComponents, LogoEmpresa);
        }

        private void GenerateInformeMedicoOcupacional_Cosapi(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPSFirmaMedicoOcupacional(_serviceId); //ARNOLD STORE
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var RecoAudio = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.AUDIOMETRIA_ID);//ARNOLD STORE
            var RecoElectro = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ELECTROCARDIOGRAMA_ID);
            var RecoEspiro = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ESPIROMETRIA_ID);
            var RecoNeuro = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.EVAL_NEUROLOGICA_ID);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);

            var RecoAltEst = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ALTURA_ESTRUCTURAL_ID);
            var RecoActFis = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.CUESTIONARIO_ACTIVIDAD_FISICA);
            var RecoCustNor = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.C_N_ID);
            var RecoAlt7D = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ALTURA_7D_ID);
            var RecoExaFis = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.EXAMEN_FISICO_ID);
            var RecoExaFis7C = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.EXAMEN_FISICO_7C_ID);
            var RecoOsteoMus1 = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.OSTEO_MUSCULAR_ID_1);
            var RecoTamDer = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.TAMIZAJE_DERMATOLOGIO_ID);
            var RecoOdon = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ODONTOGRAMA_ID);
            var RecoPsico = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.PSICOLOGIA_ID);
            var RecoRx = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.RX_TORAX_ID);
            var RecoOit = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.OIT_ID);
            var RecoOft = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.OFTALMOLOGIA_ID);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);


            var Restricciton = _serviceBL.GetRestrictionByServiceId(_serviceId);
            var Aptitud = _serviceBL.DevolverAptitud(_serviceId); //ARNOLD STORE

            var _listAtecedentesOcupacionales = _historyBL.GetHistoryReport(_pacientId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);

            InformeMedicoOcupacional_Cosapi.CreateInformeMedicoOcupacional_Cosapi(_DataService,
                filiationData, diagnosticRepository, serviceComponents, MedicalCenter,
                datosP,
                pathFile,
                RecoAudio,
                RecoElectro,
                RecoEspiro,
                RecoNeuro, RecoAltEst, RecoActFis, RecoCustNor, RecoAlt7D, RecoExaFis, RecoExaFis7C, RecoOsteoMus1, RecoTamDer, RecoOdon,
                RecoPsico, RecoRx, RecoOit, RecoOft, Restricciton, Aptitud, _listAtecedentesOcupacionales, _listaPatologicosFamiliares, _listMedicoPersonales, _listaHabitoNocivos);
        }

        private void GenerateCertificadoAptitudMedicoOcupacional_Cosapi(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPSFirmaMedicoOcupacional(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var Restricciton = _serviceBL.GetRestrictionByServiceId(_serviceId);

            CertificadoAptitudMedico_Cosapi.CreateCertificadoMedicoOcupacional_Cosapi(_DataService,
                filiationData, diagnosticRepository, serviceComponents, MedicalCenter,
                datosP,
                pathFile);
        }

        private void GenerateEvaluacionNEurologicaIndividualizada_Cosapi(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var Restricciton = _serviceBL.GetRestrictionByServiceId(_serviceId);

            EvaluacionNeurologicaIndividualizada.CreateEvaluacionNeurologicaIndividualizada(_DataService,
                filiationData, diagnosticRepository, serviceComponents, MedicalCenter,
                datosP,
                pathFile);
        }

        private void GenerateCertificadoAptitudMedicoOcupacional_Cosapi_New(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var Restricciton = _serviceBL.GetRestrictionByServiceId(_serviceId);

            CertificadoDeAptitudMedicoOcupacional.CreateCertificadoMedicoOcupacionalCosapiNew(_DataService,
                filiationData, diagnosticRepository, serviceComponents, MedicalCenter,
                datosP,
                pathFile);
        }

        private void GenerateInformeMedicoOcupacionalExamenMedicoAnual(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPSFirmaMedicoOcupacional(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var RecoAudio = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.AUDIOMETRIA_ID);
            var RecoElectro = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ELECTROCARDIOGRAMA_ID);
            var RecoEspiro = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ESPIROMETRIA_ID);
            var RecoNeuro = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.EVAL_NEUROLOGICA_ID);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);

            var RecoAltEst = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ALTURA_ESTRUCTURAL_ID);
            var RecoActFis = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.CUESTIONARIO_ACTIVIDAD_FISICA);
            var RecoCustNor = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.C_N_ID);
            var RecoAlt7D = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ALTURA_7D_ID);
            var RecoExaFis = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.EXAMEN_FISICO_ID);
            var RecoExaFis7C = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.EXAMEN_FISICO_7C_ID);
            var RecoOsteoMus1 = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.OSTEO_MUSCULAR_ID_1);
            var RecoTamDer = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.TAMIZAJE_DERMATOLOGIO_ID);
            var RecoOdon = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ODONTOGRAMA_ID);
            var RecoPsico = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.PSICOLOGIA_ID);
            var RecoRx = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.RX_TORAX_ID);
            var RecoOit = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.OIT_ID);
            var RecoOft = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.OFTALMOLOGIA_ID);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);


            var Restricciton = _serviceBL.GetRestrictionByServiceId(_serviceId);
            var Aptitud = _serviceBL.DevolverAptitud(_serviceId);

            var _listAtecedentesOcupacionalesA = _historyBL.GetHistoryReportA(_pacientId); //ARNOLD STORE
            var _listAtecedentesOcupacionalesB = _historyBL.GetHistoryReportB(_pacientId); //ARNOLD STORE
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);
            var anamnesis = _serviceBL.GetAnamnesisReport(_serviceId);
            var exams = _serviceBL.GetServiceComponentsReport(_serviceId);
            var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(_serviceId);
            var ExamenFisico = _serviceBL.ValoresComponente(_serviceId, Constants.EXAMEN_FISICO_ID);
            var Oftalmologia = _serviceBL.ValoresComponente(_serviceId, Constants.OFTALMOLOGIA_ID);
            var TestIhihara = _serviceBL.ValoresComponente(_serviceId, Constants.TEST_ISHIHARA_ID);
            var TestEstereopsis = _serviceBL.ValoresComponente(_serviceId, Constants.TEST_ESTEREOPSIS_ID);
            InformeMedicoSaludOcupacional_ExamenAnual.CreateInformeMedicoOcupacionalExamenMedicoAnual(_DataService,
                filiationData, diagnosticRepository, serviceComponents, MedicalCenter,
                datosP,
                pathFile,
                RecoAudio,
                RecoElectro,
                RecoEspiro,
                RecoNeuro, RecoAltEst, RecoActFis, RecoCustNor, RecoAlt7D, RecoExaFis, RecoExaFis7C, RecoOsteoMus1, RecoTamDer, RecoOdon,
                RecoPsico, RecoRx, RecoOit, RecoOft, Restricciton, Aptitud, _listAtecedentesOcupacionalesA, _listAtecedentesOcupacionalesB, _listaPatologicosFamiliares,
                _listMedicoPersonales, _listaHabitoNocivos, anamnesis, exams, _ExamenesServicio, ExamenFisico, TestIhihara, TestEstereopsis, Oftalmologia);
        }

        private void GenerateAnexo8InformeMedicoOcupacional(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPSFirmaMedicoOcupacional(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var RecoAudio = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.AUDIOMETRIA_ID);
            var RecoElectro = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ELECTROCARDIOGRAMA_ID);
            var RecoEspiro = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ESPIROMETRIA_ID);
            var RecoNeuro = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.EVAL_NEUROLOGICA_ID);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);

            var RecoAltEst = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ALTURA_ESTRUCTURAL_ID);
            var RecoActFis = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.CUESTIONARIO_ACTIVIDAD_FISICA);
            var RecoCustNor = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.C_N_ID);
            var RecoAlt7D = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ALTURA_7D_ID);
            var RecoExaFis = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.EXAMEN_FISICO_ID);
            var RecoExaFis7C = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.EXAMEN_FISICO_7C_ID);
            var RecoOsteoMus1 = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.OSTEO_MUSCULAR_ID_1);
            var RecoTamDer = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.TAMIZAJE_DERMATOLOGIO_ID);
            var RecoOdon = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ODONTOGRAMA_ID);
            var RecoPsico = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.PSICOLOGIA_ID);
            var RecoRx = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.RX_TORAX_ID);
            var RecoOit = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.OIT_ID);
            var RecoOft = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.OFTALMOLOGIA_ID);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);


            var Restricciton = _serviceBL.GetRestrictionByServiceId(_serviceId);
            var Aptitud = _serviceBL.DevolverAptitud(_serviceId);

            var _listAtecedentesOcupacionales = _historyBL.GetHistoryReport(_pacientId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);

            Anexo8_InformeMedicoOcupacionalcs.CreateAnexo8InformeMedicoOcupacional(_DataService,
                filiationData, diagnosticRepository, serviceComponents, MedicalCenter,
                datosP,
                pathFile,
                RecoAudio,
                RecoElectro,
                RecoEspiro,
                RecoNeuro, RecoAltEst, RecoActFis, RecoCustNor, RecoAlt7D, RecoExaFis, RecoExaFis7C, RecoOsteoMus1, RecoTamDer, RecoOdon,
                RecoPsico, RecoRx, RecoOit, RecoOft, Restricciton, Aptitud, _listAtecedentesOcupacionales, _listaPatologicosFamiliares, _listMedicoPersonales, _listaHabitoNocivos);
        }

        private void GenerateDeclaracionJuradaAntecedentesPersonales(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var _DataService = _serviceBL.GetServiceReport(_serviceId);

            DeclaracionJuradaAntecedentesPersonales.CreateDeclaracionJuradaAntecedentesPersonales(_DataService, serviceComponents, MedicalCenter, datosP, pathFile);
        }
        private void GenerateAutorizacionPruebaEmbarazo(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);

            AutorizacionPruebaEmbarazo.CreateAutorizacionPruebaEmbarazo(filiationData, pathFile, datosP, MedicalCenter, serviceComponents, LogoEmpresa);

        }
        private void GenerateEntregaExamenMedicoOcupacional(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var _DataService = _serviceBL.GetServiceReport(_serviceId);

            EntregaExamenMedicoOcipacional.CreateEntregaExamenMedicoOcipacional(filiationData, _DataService, serviceComponents, MedicalCenter, datosP, pathFile);
        }

        private void GenerateInforme_Otorrinolaringologico(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Audiometria, _serviceId);

            Informe_Otorrinolaringologico.CreateInforme_Otorrinolaringologico(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, diagnosticRepository, datosGrabo);
        }

        private void GenerateDeclaracionJurada_Encuesta(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            DeclaracionJurada_Encuesta.CreateDeclaracionJurada_Encuesta(_DataService, pathFile, datosP, MedicalCenter, serviceComponents);
        }

        private void GenerateInforme_Resultados_San_Martinm(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var RecoAudio = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.AUDIOMETRIA_ID);
            var RecoElectro = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ELECTROCARDIOGRAMA_ID);
            var RecoEspiro = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ESPIROMETRIA_ID);
            var RecoNeuro = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.EVAL_NEUROLOGICA_ID);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);

            var RecoAltEst = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ALTURA_ESTRUCTURAL_ID);
            var RecoActFis = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.CUESTIONARIO_ACTIVIDAD_FISICA);
            var RecoCustNor = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.C_N_ID);
            var RecoAlt7D = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ALTURA_7D_ID);
            var RecoExaFis = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.EXAMEN_FISICO_ID);
            var RecoExaFis7C = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.EXAMEN_FISICO_7C_ID);
            var RecoOsteoMus1 = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.OSTEO_MUSCULAR_ID_1);
            var RecoTamDer = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.TAMIZAJE_DERMATOLOGIO_ID);
            var RecoOdon = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ODONTOGRAMA_ID);
            var RecoPsico = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.PSICOLOGIA_ID);
            var RecoRx = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.RX_TORAX_ID);
            var RecoOit = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.OIT_ID);
            var RecoOft = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.OFTALMOLOGIA_ID);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            var Restricciton = _serviceBL.GetRestrictionByServiceId(_serviceId);
            var Aptitud = _serviceBL.DevolverAptitud(_serviceId);

            var _listAtecedentesOcupacionalesA = _historyBL.GetHistoryReportA(_pacientId);
            var _listAtecedentesOcupacionalesB = _historyBL.GetHistoryReportB(_pacientId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);
            var anamnesis = _serviceBL.GetAnamnesisReport(_serviceId);
            var exams = _serviceBL.GetServiceComponentsReport(_serviceId);
            var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(_serviceId);
            var ExamenFisico = _serviceBL.ValoresComponente(_serviceId, Constants.EXAMEN_FISICO_ID);
            var Oftalmologia = _serviceBL.ValoresComponente(_serviceId, Constants.OFTALMOLOGIA_ID);
            var TestIhihara = _serviceBL.ValoresComponente(_serviceId, Constants.TEST_ISHIHARA_ID);
            var TestEstereopsis = _serviceBL.ValoresComponente(_serviceId, Constants.TEST_ESTEREOPSIS_ID);

            Informe_Resultados_San_Martinm.CreateInforme_Resultados_San_Martinm(_DataService,
                filiationData, diagnosticRepository, serviceComponents, MedicalCenter,
                datosP,
                pathFile,
                RecoAudio,
                RecoElectro,
                RecoEspiro,
                RecoNeuro, RecoAltEst, RecoActFis, RecoCustNor, RecoAlt7D, RecoExaFis, RecoExaFis7C, RecoOsteoMus1, RecoTamDer, RecoOdon,
                RecoPsico, RecoRx, RecoOit, RecoOft, Restricciton, Aptitud, _listAtecedentesOcupacionalesA, _listAtecedentesOcupacionalesB, _listaPatologicosFamiliares,
                _listMedicoPersonales, _listaHabitoNocivos, anamnesis, exams, _ExamenesServicio, ExamenFisico, TestIhihara, TestEstereopsis, Oftalmologia);
        }

        private void GenerateAnexo16A(string pathFile)
        {
            var _DataService = _serviceBL.GetInformacion_OtrosExamenes(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);

            Anexo16A.CreateAnexo16A(_DataService, pathFile, datosP, MedicalCenter, serviceComponents, diagnosticRepository, datosGrabo);
        }
        private void GenerateAPTITUD_PSICOSOMATICA_PARA_CONDUCTORES_Y_OPERADORES_DE_EQUIPOS(string pathFile)
        {
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);

            APTITUD_PSICOSOMATICA_PARA_CONDUCTORES_Y_OPERADORES_DE_EQUIPOS.CreateAPTITUD_PSICOSOMATICA_PARA_CONDUCTORES_Y_OPERADORES_DE_EQUIPOS(filiationData, pathFile, datosP, MedicalCenter, serviceComponents, diagnosticRepository, datosGrabo);
        }
        private void GenerateAPTITUD_PARA_TRABAJOS_DE_CARGAS_SUSPENDIDAS(string pathFile)
        {
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);

            APTITUD_PARA_TRABAJOS_DE_CARGAS_SUSPENDIDAS.CreateAPTITUD_PARA_TRABAJOS_DE_CARGAS_SUSPENDIDAS(filiationData, pathFile, datosP, MedicalCenter, serviceComponents, diagnosticRepository, datosGrabo);
        }

        private void GenerateAltura_Fisica_Shahuindo(string pathFile)
        {
            var _DataService = _serviceBL.GetInformacion_OtrosExamenes(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            Altura_Fisica_Shahuindo.CreateAltura_Fisica_Shahuindo(_DataService, pathFile, datosP, MedicalCenter, serviceComponents);
        }

        private void GenerateExamen_Medico_Trabajos_Altura_GoldFields(string pathFile)
        {
            var _DataService = _serviceBL.GetInformacion_OtrosExamenes(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            Examen_Medico_Trabajos_Altura_GoldFields.CreateExamen_Medico_Trabajos_Altura_GoldFields(_DataService, pathFile, datosP, MedicalCenter, serviceComponents);
        }

        private void GenerateExamen_Medico_Trabajos_Altura_AUDIT_GoldFields(string pathFile)
        {
            var _DataService = _serviceBL.GetInformacion_OtrosExamenes(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            Examen_Medico_Trabajos_Altura_AUDIT_GoldFields.CreateExamen_Medico_Trabajos_Altura_AUDIT_GoldFields(_DataService, pathFile, datosP, MedicalCenter, serviceComponents);
        }
        private void GenerateExamen_Medico_Trabajos_Altura_SOLICITUD_GoldFields(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Psicologia, _serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            Examen_Medico_Trabajos_Altura_SOLICITUD_GoldFields.CreateExamen_Medico_Trabajos_Altura_SOLICITUD_GoldFields(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, LogoEmpresa, datosGrabo);
        }

        private void GenerateDeclaracion_Jurada_EMPO_YANACOCHA(string pathFile)
        {
            var _DataService = _serviceBL.GetInformacion_OtrosExamenes_New(_serviceId); //ARNOLD STORE
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var CuadroVacio = Common.Utils.BitmapToByteArray(Resources.CuadradoVacio);
            var CuadroCheck = Common.Utils.BitmapToByteArray(Resources.CuadradoCheck);

            Declaracion_Jurada_EMPO_YANACOCHA.CreateDeclaracion_Jurada_EMPO_YANACOCHA(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, CuadroVacio, CuadroCheck);
        }

        private void GenerateDeclaracion_Jurada_EMO_Secutiras(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);

            Declaracion_Jurada_EMO_Secutiras.CreateDeclaracion_Jurada_EMO_Secutiras(pathFile, datosP, MedicalCenter, filiationData, serviceComponents);
        }

        #region  MTC 
        private void GenerateInformacion_Laboratorio(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.LABORATORIO_MTC, _serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            Informacion_Laboratorio.CreateInformacion_Laboratorio(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo, diagnosticRepository);
        }

        private void GenerateEvaluacionPsicologicaMTC(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.PSICOLOGIA_MTC, _serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            EvaluacionPsicologicaMTC.CreateEvaluacionPsicologicaMTC(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo, diagnosticRepository);
        }

        private void GenerateEvaluacionOftalmologicaMTC(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.OFTALMOLOGIA_MTC, _serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            EvaluacionOftalmologicaMTC.CreateEvaluacionOftalmologicaMTC(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo, diagnosticRepository);
        }

        private void GenerateEvaluaciónAuditiva(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.AUDIOMETRIA_MTC, _serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var audioUserControlList = _serviceBL.ReportAudiometriaUserControl(_serviceId, Constants.AUDIOMETRIA_MTC_ID);
            EvaluaciónAuditiva.CreateEvaluaciónAuditiva(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo, diagnosticRepository, audioUserControlList);
        }

        private void GenerateEvaluacionClinica(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var aptitudFirmas = _pacientBL.DevolverDatosAptitudMedico(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.MEDICINA_MTC, _serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);
            EvaluacionClinicaMTC.CreateEvaluacionClinicaMTC(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo, diagnosticRepository,
                _listMedicoPersonales, _listaHabitoNocivos, aptitudFirmas);
        }

        private void GenerateCertificadoEvaluacionesMedicasPsicologicasConducir(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var aptitudFirmas = _pacientBL.DevolverDatosAptitudMedico(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGraboPsicologia = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.PSICOLOGIA_MTC, _serviceId);
            var datosGraboLaboratorio = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.LABORATORIO_MTC, _serviceId);
            var datosGraboMedicina = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.MEDICINA_MTC, _serviceId);
            var datosGraboOftalmologia = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.OFTALMOLOGIA_MTC, _serviceId);
            var datosGraboAudiometria = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.AUDIOMETRIA_MTC, _serviceId);

            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);
            CertificadoEvaluacionesMedicasPsicologicasConducir.CreateCertificadoEvaluacionesMedicasPsicologicasConducir(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGraboMedicina, diagnosticRepository,
                _listMedicoPersonales, _listaHabitoNocivos, aptitudFirmas, datosGraboLaboratorio, datosGraboPsicologia,
                datosGraboOftalmologia, datosGraboAudiometria);
        }
        //

        #endregion
        

        private void GenerateExamen_Dermatologico_Ocupacional(string pathFile)
        {
            var _DataService = _serviceBL.GetInformacion_OtrosExamenes(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            Examen_Dermatologico_Ocupacional.CreateExamen_Dermatologico_Ocupacional(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, diagnosticRepository);
        }

        private void GenerateDeclaracion_Jurada_SINTOMATICO(string pathFile)
        {
            var _DataService = _serviceBL.GetInformacion_OtrosExamenes(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);

            Declaracion_Jurada_Sintomatico_Respiratorio.CreateDeclaracionJurada_Sintomatico(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents);
        }

        private void GenerateCertificado_Suficiencia_Medica_Trabajo_Altura_V4(string pathFile)
        {
            var _DataService = _serviceBL.GetInformacion_OtrosExamenes(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            Certificado_Suficiencia_Medica_Trabajo_Altura_V4.CreateCertificado_Suficiencia_Medica_Trabajo_Altura_V4(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, diagnosticRepository, datosGrabo);
        }

        private void GenerateFicha_Evaluacion_Musculoesqueletica_GoldFields(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            var servicesId7 = new List<string>();
            servicesId7.Add(_serviceId);
            var componentReportId7 = new ServiceBL().ObtenerIdsParaImportacionExcel(servicesId7, 11);

            var uc = new ServiceBL().ReporteOsteomuscular(_serviceId, componentReportId7[0].ComponentId);

            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            Ficha_Evaluacion_Musculoesqueletica_GoldFields.CreateFicha_Evaluacion_Musculoesqueletica_GoldFields(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, diagnosticRepository, uc, datosGrabo);
        }

        private void GenerateFichaEvaluacionMusculoEsqueleticaBasica_Hudbay(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

            var servicesId7 = new List<string>();
            servicesId7.Add(_serviceId);
            var componentReportId7 = new ServiceBL().ObtenerIdsParaImportacionExcel(servicesId7, 334);

            var uc = new ServiceBL().ReporteOsteomuscular(_serviceId, componentReportId7[0].ComponentId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.MedicinaFisicaRehabilitacionOcupacional, _serviceId);
            FichaEvaluacionMusculoEsqueleticaBasica_Hudbay.CreateFichaEvaluacionMusculoEsqueleticaBasica_Hudbay(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, diagnosticRepository, uc, datosGrabo, LogoEmpresa);
        }

        private void GenerateFichaEvaluacionMusculoesqueleticaMiembrosSuperioresHudbay(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.MedicinaFisicaRehabilitacionOcupacional, _serviceId);

            FichaEvaluacionMusculoesqueleticaMiembrosSuperioresHudbay.CreateFichaEvaluacionMusculoesqueleticaMiembrosSuperioresHudbay(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo, LogoEmpresa);
        }
        private void GenerateCERTIFICADO_APTITUD_MEDICA_PARA_TRABAJOS_ESPECIFICOS_HUDBAY(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);

            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);

            CERTIFICADO_APTITUD_MEDICA_PARA_TRABAJOS_ESPECIFICOS_HUDBAY.CreateCERTIFICADO_APTITUD_MEDICA_PARA_TRABAJOS_ESPECIFICOS_HUDBAY(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo, LogoEmpresa);
        }

        private void GenerateCuestionarioNordico_EvaluacionSintomasMusculoTendinosos(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);

            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);

            CuestionarioNordico_EvaluacionSintomasMusculoTendinosos.CreateCuestionarioNordico_EvaluacionSintomasMusculoTendinosos(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo, LogoEmpresa);
        }
        private void GenerateFichadeEvaluacionParaConductoresHudbay(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);

            var LogoEmpresa = _serviceBL.GetLogoEmpresa(datosP.v_WorkingOrganizationId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);

            FichadeEvaluacionParaConductoresHudbay.CreateFichadeEvaluacionParaConductoresHudbay(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo, LogoEmpresa);
        }

        public void GenerateInforme_Psicologico_Resumen(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport_NewLab(_serviceId); //ARNOLD STORE
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var _DataService = _serviceBL.GetServiceReport(_serviceId);

            var _InformacionHistoriaPsico = _serviceBL.GetHistoriaClinicaPsicologica(_serviceId, Constants.FICHA_PSICOLOGICA_OCUPACIONAL_GOLDFIELDS);


            Informe_Psicologico_Resumen.CreateInforme_Psicologico_Resumen(filiationData, _DataService, serviceComponents, MedicalCenter, datosP, pathFile);
        }

        private void GenerateCertificado_Suficiencia_Medica_Brigadistas(string pathFile)
        {
            var _DataService = _serviceBL.GetInformacion_OtrosExamenes(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            Certificado_Suficiencia_Medica_Brigadistas.CreateCertificado_Suficiencia_Medica_Brigadistas(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, diagnosticRepository, datosGrabo);
        }

        private void Generate_Informe_Ecografico_Obstetrico(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Ecografia, _serviceId);

            Ecografias_P_JP.Create_Informe_Ecografico_Obstetrico(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, diagnosticRepository, datosGrabo);
        }

        private void Generate_Informe_Ecografico_Ginecologico(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Ecografia, _serviceId);

            Ecografias_P_JP.Create_Informe_Ecografico_Ginecologico(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, diagnosticRepository, datosGrabo);
        }

        private void Generate_Informe_Ecografico_Abdominal(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Ecografia, _serviceId);

            Ecografias_P_JP.Create_Informe_Ecografico_Abdominal(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, diagnosticRepository, datosGrabo);
        }

        private void Generate_Informe_Ecografico_Renal(string pathFile)
        {
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Ecografia, _serviceId);

            Ecografias_P_JP.Create_Informe_Ecografico_Renal(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, diagnosticRepository, datosGrabo);
        }

        private void Generate_Informe_Ecografico_Mamas(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Ecografia, _serviceId);

            Ecografias_P_JP.Create_Informe_Ecografico_Mamas(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, diagnosticRepository, datosGrabo);
        }

        private void Generate_Informe_Ecografico_Prostata(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Ecografia, _serviceId);

            Ecografias_P_JP.Create_Informe_Ecografico_Prostata(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, diagnosticRepository, datosGrabo);
        }

        private void Generate_Informe_Ecografico_Partes_Blandas(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Ecografia, _serviceId);

            Ecografias_P_JP.Create_Informe_Ecografico_Partes_Blandas(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, diagnosticRepository, datosGrabo);
        }

        private void Generate_Informe_Ecografico_Obstetrico_Pelvico(string pathFile)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Ecografia, _serviceId);

            Ecografias_P_JP.Create_Informe_Ecografico_Obstetrico_Pelvico(pathFile, datosP, MedicalCenter, filiationData, serviceComponents, diagnosticRepository, datosGrabo);
        }

        private void GenerateExamenesEspecialesReport(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);

            ExamenesEspecialesReport.CreateLaboratorioReport(filiationData, serviceComponents, MedicalCenter, pathFile);
        }

        private void GenerateInformeMedicoResumen(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPSFirmaMedicoOcupacional(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport_(_serviceId); //ARNOLD STORE
            var RecoAudio = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.AUDIOMETRIA_ID);
            var RecoElectro = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ELECTROCARDIOGRAMA_ID);
            var RecoEspiro = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ESPIROMETRIA_ID);
            var RecoNeuro = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.EVAL_NEUROLOGICA_ID);

            var RecoAltEst = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ALTURA_ESTRUCTURAL_ID);
            var RecoActFis = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.CUESTIONARIO_ACTIVIDAD_FISICA);
            var RecoCustNor = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.C_N_ID);
            var RecoAlt7D = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ALTURA_7D_ID);
            var RecoExaFis = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.EXAMEN_FISICO_ID);
            var RecoExaFis7C = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.EXAMEN_FISICO_7C_ID);
            var RecoOsteoMus1 = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.OSTEO_MUSCULAR_ID_1);
            var RecoTamDer = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.TAMIZAJE_DERMATOLOGIO_ID);
            var RecoOdon = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.ODONTOGRAMA_ID);
            var RecoPsico = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.PSICOLOGIA_ID);
            var RecoRx = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.RX_TORAX_ID);
            var RecoOit = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.OIT_ID);
            var RecoOft = _serviceBL.GetListRecommendationByServiceIdAndComponent(_serviceId, Constants.OFTALMOLOGIA_ID);


            var Restricciton = _serviceBL.GetRestrictionByServiceId(_serviceId);
            var Aptitud = _serviceBL.DevolverAptitud(_serviceId);

            InformeMedicoOcupacional.CreateInformeMedicoOcupacional(filiationData, serviceComponents, MedicalCenter, pathFile,
                RecoAudio,
                RecoElectro,
                RecoEspiro,
                RecoNeuro, RecoAltEst, RecoActFis, RecoCustNor, RecoAlt7D, RecoExaFis, RecoExaFis7C, RecoOsteoMus1, RecoTamDer, RecoOdon,
                RecoPsico, RecoRx, RecoOit, RecoOft, Restricciton, Aptitud);
        }

        private void GenerateCertificadoAptitudCompleto(string pathFile)
        {

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var CAPC = _serviceBL.GetCAPC(_serviceId); //ARNOLD STORE
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var PathNegro = Application.StartupPath + "\\Resources\\cuadradonegro.jpg";
            var PathBlanco = Application.StartupPath + "\\Resources\\cuadroblanco.png";
            CertificadoAptitudCompleto.CreateCertificadoAptitudCompleto(
                CAPC,
                MedicalCenter,
                diagnosticRepository,
                pathFile,
                PathNegro,
                PathBlanco

                );
        }

        #endregion

    }
}