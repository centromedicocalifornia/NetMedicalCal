using Sigesoft.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Reports
{
    public partial class frmReportingServer : Form
    {
        private LogicReports _LogicReports = new LogicReports();
        private List<ServiciosReport> serviciosExcluidos = new List<ServiciosReport>();
        private List<ServiciosReport> serviciosCulminados = new List<ServiciosReport>();
        public frmReportingServer()
        {
            InitializeComponent();
        }

        private void frmReportingServer_Load(object sender, EventArgs e)
        {
            List<ServiciosReport> serviciosList = ObtenerServicios();
            if (serviciosExcluidos.Count > 0){foreach (var exc in serviciosExcluidos){serviciosList = serviciosList.FindAll(p => p.serviceId != exc.serviceId);}}
            if (serviciosCulminados.Count > 0) { foreach (var exc in serviciosCulminados) { serviciosList = serviciosList.FindAll(p => p.serviceId != exc.serviceId);}}

            foreach (var service in serviciosList)
            {
                bool error = false;
                Task[] tasks = new Task[3];
                try
                {
                    string personId = service.pacientId;
                    string serviceId = service.serviceId;
                    List<ServiceComponentListReport> serviceComponents = GetServiceComponentsForManagementReport(service.serviceId);
                    List<ServiceComponentListReport> ordenReportes = GetOrdenReportes(service.organizationId);
                    var componentIds = ordenReportes.Select(p => p.v_ComponentId).ToList();
                    var reportsCrystal = ordenReportes.FindAll(p => p.v_ComponentId.Contains("N00"));
                    var reportsPdf = ordenReportes.Except(reportsCrystal).ToList();
                    serviceComponents = serviceComponents.FindAll(p => componentIds.Contains(p.v_ComponentId)).ToList();
                    var list = serviceComponents.Union(reportsPdf).ToList();
                    List<ServiceComponentListReport> ListOrdenada = new List<ServiceComponentListReport>();
                    foreach (var item in ordenReportes)
                    {
                        var obj = new ServiceComponentListReport();
                        var exist = list.Find(p => p.v_ComponentId == item.v_ComponentId);
                        if (exist != null)
                        {
                            obj.v_ComponentId = item.v_ComponentId + "|" + item.i_NombreCrystalId;
                            obj.v_ComponentName = item.v_ComponentName;
                            obj.v_ServiceComponentId = exist.v_ServiceComponentId;
                            obj.i_CategoryId = exist.i_CategoryId;
                            obj.v_CategoryName = item.v_CategoryName;
                            obj.i_ServiceComponentStatusId = exist.i_ServiceComponentStatusId;
                            obj.v_ServiceId = exist.v_ServiceId;
                            obj.i_GenderId = exist.i_GenderId;
                            obj.i_Orden = item.i_Orden;
                            obj.v_NombreCrystal = exist.v_NombreCrystal;
                            obj.i_NombreCrystalId = exist.i_NombreCrystalId;
                            ListOrdenada.Add(obj);
                        }
                    }

                    List<ServiceComponentListReport> ReportComponent = ListOrdenada.FindAll(p => p.v_ComponentId.Contains("N00"));
                    List<ServiceComponentListReport> ReportCerti = ListOrdenada.FindAll(p => p.v_ComponentName.Contains("CERTI"));
                    List<ServiceComponentListReport> ReportInfo = ListOrdenada.FindAll(p => p.v_ComponentName.Contains("INFORME"));
                    List<ServiceComponentListReport> ReportAnexo = ListOrdenada.FindAll(p => p.v_ComponentName.Contains("ANEXO"));
                    List<ServiceComponentListReport> ReportCielo = ListOrdenada.FindAll(p => p.v_ComponentId.Contains("CIELO_AZUL"));
                    List<ServiceComponentListReport> ServCulminado = ReportCerti.Union(ReportInfo).ToList();
                    ServCulminado = ServCulminado.Union(ReportAnexo).ToList();
                    if (ReportCielo.Count>0){foreach (var cielo in ReportCielo){ListOrdenada = ListOrdenada.FindAll(p => p.v_ComponentId != cielo.v_ComponentId);}}

                    Task<string> firstTask = null;
                    Task<string> secondTask = null;
                    using (new LoadingClass.PleaseWait(this.Location, "Servidor de reportes..."))
                    {
                        
                            foreach (var lista in ReportComponent)
                            {
                                System.Threading.Tasks.Task.Factory.StartNew(() =>
                                {
                                    string[] arrayComp = lista.v_ComponentId.Split('|');
                                    if (arrayComp[1] == "") { arrayComp[1] = "0"; }

                                    if (lista.i_ServiceComponentStatusId == 3)
                                    {
                                        _LogicReports.ChooseReport(arrayComp[0], serviceId, personId,
                                            Convert.ToInt32(arrayComp[1]));
                                    }
                                    ListOrdenada = ListOrdenada.FindAll(p => p.v_ComponentId != lista.v_ComponentId);
                                }).Wait();
                            }
                        
                            foreach (var lista in ServCulminado)
                            {
                                if (service.i_ServiceStatusId == 3)
                                {
                                    string[] arrayComp = lista.v_ComponentId.Split('|');
                                    if (arrayComp[1] == null) { arrayComp[1] = "0"; }
                                    _LogicReports.ChooseReport(arrayComp[0], serviceId, personId,
                                        Convert.ToInt32(arrayComp[1]));
                                }
                                ListOrdenada = ListOrdenada.FindAll(p => p.v_ComponentId != lista.v_ComponentId);
                            }
                            foreach (var lista in ListOrdenada)
                            {
                                string[] arrayComp = lista.v_ComponentId.Split('|');
                                if (arrayComp[1] == "") { arrayComp[1] = "0"; }
                                _LogicReports.ChooseReport(arrayComp[0], serviceId, personId,
                                    Convert.ToInt32(arrayComp[1]));
                            }

                        
                    }
                }
                catch (Exception exception)
                {
                    serviciosExcluidos.Add(service);
                    frmReportingServer_Load(sender, e);
                    error = true;
                }

                if (!error){serviciosCulminados.Add(service);}
                
            }


        }

        private List<ServiceComponentListReport> GetOrdenReportes(string organizationId)
        {
            ConexionSigesoft conexionSigesoft = new ConexionSigesoft();
            conexionSigesoft.opensigesoft();
            var cadena1 =
                @"select v_ComponenteId, v_NombreReporte, i_Orden, v_NombreCrystal, i_NombreCrystalId from OrdenReporte where v_OrganizationId='" + organizationId + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conexionSigesoft.conectarsigesoft);
            SqlDataReader lector1 = comando.ExecuteReader();
            List<ServiceComponentListReport> objReport = new List<ServiceComponentListReport>();
            while (lector1.Read())
            {
                ServiceComponentListReport report = new ServiceComponentListReport();
                report.v_ComponentId = lector1.GetValue(0).ToString();
                report.v_ComponentName = lector1.GetValue(1).ToString();
                report.i_Orden = Convert.ToInt32(lector1.GetValue(2).ToString());
                report.v_NombreCrystal = lector1.GetValue(3).ToString();
                report.i_NombreCrystalId = (lector1.GetValue(4).ToString());
                objReport.Add(report);
            }
            lector1.Close();
            conexionSigesoft.closesigesoft();
            return objReport;
        }

        private List<ServiceComponentListReport> GetServiceComponentsForManagementReport(string serviceId)
        {
            ConexionSigesoft conexionSigesoft = new ConexionSigesoft();
            conexionSigesoft.opensigesoft();
            var cadena1 =
                @"select CP.v_ComponentId, CP.v_Name,SC.v_ServiceComponentId, CP.i_CategoryId, SC.i_ServiceComponentStatusId, SC.v_ServiceId, PP.i_SexTypeId from servicecomponent SC " +
                "inner join service SR on SC.v_ServiceId=SR.v_ServiceId " +
                "inner join person PP on SR.v_PersonId=PP.v_PersonId " +
                "inner join component CP on SC.v_ComponentId=CP.v_ComponentId " +
                "where SC.v_ServiceId='" + serviceId + "' and CP.i_ComponentTypeId=1 and SC.i_IsDeleted=0 and SC.i_IsRequiredId=1";
            SqlCommand comando = new SqlCommand(cadena1, connection: conexionSigesoft.conectarsigesoft);
            SqlDataReader lector1 = comando.ExecuteReader();
            List<ServiceComponentListReport> objServiceCp = new List<ServiceComponentListReport>();
            while (lector1.Read())
            {
                ServiceComponentListReport servicios = new ServiceComponentListReport();
                servicios.v_ComponentId = lector1.GetValue(0).ToString();
                servicios.v_ComponentName = lector1.GetValue(1).ToString();
                servicios.v_ServiceComponentId = lector1.GetValue(2).ToString();
                servicios.i_CategoryId = Convert.ToInt32(lector1.GetValue(3).ToString());
                servicios.i_ServiceComponentStatusId = Convert.ToInt32(lector1.GetValue(4).ToString());
                servicios.v_ServiceId = lector1.GetValue(5).ToString();
                servicios.i_GenderId = Convert.ToInt32(lector1.GetValue(6).ToString());
                objServiceCp.Add(servicios);
            }
            lector1.Close();
            conexionSigesoft.closesigesoft();
            return objServiceCp;
        }

        private List<ServiciosReport> ObtenerServicios()
        {
            ConexionSigesoft conexionSigesoft = new ConexionSigesoft();
            conexionSigesoft.opensigesoft();
            var cadena1 = @"select SR.v_ServiceId, OO.v_OrganizationId, PP.v_PersonId, OO.v_Name, " +
                          "PP.v_DocNumber, PP.v_FirstLastName+' '+PP.v_SecondLastName+', '+PP.v_FirstName as Name, SR.i_ServiceStatusId from service SR " +
                          "inner join person PP on SR.v_PersonId=PP.v_PersonId " +
                          "inner join protocol PR on SR.v_ProtocolId=PR.v_ProtocolId " +
                          "inner join organization OO on PR.v_CustomerOrganizationId=OO.v_OrganizationId " +
                          "where SR.i_IsDeleted=0 and SR.i_StatusLiquidation<>2 and SR.i_MasterServiceId=2 " +
                          "and (Year(SR.d_ServiceDate)=Year(GETDATE()) and Month(SR.d_ServiceDate)=Month(GETDATE()) and Day(SR.d_ServiceDate)=Day(GETDATE()))";
            SqlCommand comando = new SqlCommand(cadena1, connection: conexionSigesoft.conectarsigesoft);
            SqlDataReader lector1 = comando.ExecuteReader();
            List<ServiciosReport> objServices = new List<ServiciosReport>();
            while (lector1.Read())
            {
                ServiciosReport servicios = new ServiciosReport();
                servicios.serviceId = lector1.GetValue(0).ToString();
                servicios.organizationId = lector1.GetValue(1).ToString();
                servicios.pacientId = lector1.GetValue(2).ToString();
                servicios.organizationName = lector1.GetValue(3).ToString();
                servicios.dniPacient = lector1.GetValue(4).ToString();
                servicios.pacientname = lector1.GetValue(5).ToString();
                servicios.i_ServiceStatusId = Convert.ToInt32(lector1.GetValue(6).ToString());
                objServices.Add(servicios);
            }
            lector1.Close();
            conexionSigesoft.closesigesoft();
            return objServices;
        }
    }
}
