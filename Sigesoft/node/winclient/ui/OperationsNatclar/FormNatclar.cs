using Infragistics.Win.UltraWinGrid;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.UI.NatclarXML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using System.Threading.Tasks;

namespace Sigesoft.Node.WinClient.UI.OperationsNatclar
{
    public partial class FormNatclar : Form
    {
        OperationsNatclarBl oNataclarBl = new OperationsNatclarBl();
        List<OperationsNatclarBe> data = new List<OperationsNatclarBe>();
        WSRIProveedorExternoClient client = new WSRIProveedorExternoClient();
        Natclar oNatclarBL = new Natclar();
        OperationsNatclarBl oOperationsNatclarBl = new OperationsNatclarBl();

        public FormNatclar()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            data = oNataclarBl.Filter(dtpDateTimeStar.Value, dptDateTimeEnd.Value);

            grdDataService.DataSource = data.GroupBy(x => x.v_ServiceId).Select(x => x.First()).ToList();
        }

        private void grdDataService_KeyDown(object sender, KeyEventArgs e)
        {


        }

        private void grdDataService_ClickCell(object sender, ClickCellEventArgs e)
        {
            //var grid = (UltraGrid)sender;
            //if (grid.ActiveCell == null) return;

            //var column = grid.ActiveCell.Column.Key;

            //if (column == "DatosPersonales")
            //{
            //    MessageBox.Show("ssss");
            //}
            if ((e.Cell.Column.Key == "b_select"))
            {
                if ((e.Cell.Value.ToString() == "False"))
                {
                    e.Cell.Value = true;

                    //btnFechaEntrega.Enabled = true;
                    //btnAdjuntarArchivo.Enabled = true;
                }
                else
                {
                    e.Cell.Value = false;
                    //btnFechaEntrega.Enabled = false;
                    //btnAdjuntarArchivo.Enabled = false;
                }

            }

        }

        private void grdDataService_ClickCellButton(object sender, CellEventArgs e)
        {
            var serviceId = e.Cell.Row.Cells["v_ServiceId"].Value.ToString();
            var personId = e.Cell.Row.Cells["v_PersonId"].Value.ToString();
            var nameColumn = e.Cell.Column.Key;

            Task.Factory.StartNew(() =>
            {
                e.Cell.ButtonAppearance.Image = Resources.cog_stop;
                ProcessInformation(nameColumn, serviceId, personId, e);
            }, TaskCreationOptions.LongRunning);

        }

        private string ObtenerAnormalidadPleural(List<ServiceComponentFieldValuesList> listaValoresOit)
        {
            string[] Ids = new string[] 
            { 
                Constants.RX_SIMBOLO_SI_ID,
                "N009-MF000003194"
            };
            var idsPleural = listaValoresOit.FindAll(p => Ids.Contains(p.v_ComponentFieldId));
            var ResultCode = idsPleural.Find(p => p.v_Value1 == "1").v_ComponentFielName.Substring(0, 1);

            return ResultCode;

        }

        private string ObtenerOpacidadesPequenias(List<ServiceComponentFieldValuesList> listaValoresOit)
        {
            string[] Ids = new string[] 
            { 
                Constants.RX_INFERIOR_DERECHO_ID,
                Constants.RX_INFERIOR_IZQUIERDO_ID,
                Constants.RX_MEDIO_DERECHO_ID,

                Constants.RX_MEDIO_IZQUIERDO_ID,
                Constants.RX_SUPERIOR_DERECHO_ID,
                Constants.RX_SUPERIOR_IZQUIERDO_ID

            };

            var idsOpacidadPeque = listaValoresOit.FindAll(p => Ids.Contains(p.v_ComponentFieldId));
            var ResultCode = idsOpacidadPeque.Find(p => p.v_Value1 == "1").v_ComponentFielName;

            return ResultCode;
        }

        private string ObtenerOpacidadesAbundancia(List<ServiceComponentFieldValuesList> listaValoresOit)
        {
            string[] Ids = new string[] 
            { 
                Constants.RX_0_NADA_ID,
                Constants.RX_0_0_ID,
                Constants.RX_0_1_ID,

                Constants.RX_1_0_ID,
                Constants.RX_1_1_ID,
                Constants.RX_1_2_ID,

                Constants.RX_2_1_ID,
                Constants.RX_2_2_ID,
                Constants.RX_2_3_ID,

                Constants.RX_3_2_ID,
                Constants.RX_3_3_ID,
                Constants.RX_3_MAS_ID
            };

            var idsOpacidadPeque = listaValoresOit.FindAll(p => Ids.Contains(p.v_ComponentFieldId));
            var ResultCode = idsOpacidadPeque.Find(p => p.v_Value1 == "1").v_ComponentFielName.Trim().Replace('-', '/');

            if (ResultCode == "0//")
            {
                ResultCode = "01";
            }
            else if (ResultCode == "0/0")
            {
                ResultCode = "02";
            }
            //WALTER
            return ResultCode;
        }

        private string CodigoNatclarLaboratorio(string Id)
        {
            var prefijo = Id.Split('-')[1].Substring(0, 2);

            var codigoNat = "";
            if (prefijo == "ME")
            {
                #region Codigos Perfiles
                if (Id == Constants.PERFIL_LIPIDICO)
                {
                    codigoNat = "000001";
                }
                else if (Id == Constants.PERFIL_HEPATICO_ID)
                {
                    codigoNat = "000002";
                }
                else if (Id == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID)
                {
                    codigoNat = "GRUPO Y FACTOR";
                }
                #endregion

                #region Codigos Pruebas Laboratorio
                else if (Id == Constants.COLESTEROL_ID)
                {
                    codigoNat = "LB0001";
                }
                else if (Id == Constants.METADONA_ID)
                {
                    codigoNat = "LT0065";
                }
                #endregion
            }
            else if (prefijo == "MF")
            {
                if (Id == Constants.GRUPO_SANGUINEO_ID)
                {
                    codigoNat = "LH0026";
                }
                else if (Id == Constants.FACTOR_SANGUINEO_ID)
                {
                    codigoNat = "LH0027";
                }
            }

            return codigoNat;
        }

        private void ProcessInformation(string column, string serviceId, string personId, CellEventArgs e)
        {
            var lError = "";
            try
            {

                #region Casos
                ServiceBL _ServiceBL = new ServiceBL();
                HistoryBL _HistoryBL = new HistoryBL();
                OperationResult objOperationResult = new OperationResult();
                switch (column)
                {
                    case "DatosPersonales":
                        lError = "oNatclarBL.DatosXmlNatclar";
                        var datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        if (datosPaciente == null)
                        {
                            MessageBox.Show("No se pudo mandar datos a Natclar", "Validación!", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            return;
                        }

                        lError = "oNatclarBL.DevolverUbigue";
                        var ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);
                        var obj = new EstructuraDatosAPMedicos();
                        obj.DatosPaciente = new XmlDatosPaciente();
                        lError = "datosPaciente.Nombre";
                        obj.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        obj.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        obj.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        obj.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        obj.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        obj.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        obj.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        obj.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        obj.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        obj.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        obj.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        obj.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        obj.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        obj.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());
                        lError = "datosPaciente.EnviarDatosAPMedicos";

                        lError = "datosPaciente.LanzarDatos";
                        LanzarDatos(client.EnviarDatosAPMedicos(obj), e, column, serviceId);

                        break;
                    case "DatosApQuirurgicos":

                        var objQuiru = new EstructuraDatosAPQuirurgicos();
                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);

                        objQuiru.APQUIRURGICOS = new XmlAntecedentesPatologicosQuirurgicos();

                        #region DatosPaciente

                        objQuiru.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objQuiru.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objQuiru.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objQuiru.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objQuiru.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objQuiru.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objQuiru.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objQuiru.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objQuiru.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objQuiru.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objQuiru.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objQuiru.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objQuiru.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objQuiru.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objQuiru.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objQuiru.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objQuiru.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objQuiru.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objQuiru.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objQuiru.DatosExamen.FechaRegistro = datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objQuiru.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objQuiru.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objQuiru.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objQuiru.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion

                        #region Antecedentes Quirurgico

                        var antecedentesQuirurgicos = new HistoryBL()
                            .GetPersonMedicalHistoryPagedAndFilteredByPersonId(ref objOperationResult, 0, null,
                                "d_StartDate DESC", null, personId).FindAll(p => p.v_DiseasesId == "N009-DD000000637");

                        //if (antecedentesQuirurgicos.Count == 0)
                        //{
                        //    MessageBox.Show(
                        //        "El paciente " + datosPaciente.Nombre + " " + datosPaciente.PrimerApellido + " " +
                        //        datosPaciente.SegundoApellido + " no cuenta con exámenes quirúrgicos.", "Aviso!",
                        //        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}

                        if (antecedentesQuirurgicos.Count > 0)
                        {
                            foreach (var item in antecedentesQuirurgicos)
                            {
                                lError = "APQUIRURGICOS.CodigoCIE";
                                objQuiru.APQUIRURGICOS.CodigoCIE = item.v_CIE10Id;
                                lError = "APQUIRURGICOS.Descripcion";
                                objQuiru.APQUIRURGICOS.Descripcion = item.v_TreatmentSite;
                                lError = "APQUIRURGICOS.FechaInicio";
                                if (item.d_StartDate != null)
                                    objQuiru.APQUIRURGICOS.FechaInicio = item.d_StartDate.Value.ToString("dd/MM/yyyy");
                                lError = "APQUIRURGICOS.FechaFin";
                                if (item.d_StartDate != null)
                                    objQuiru.APQUIRURGICOS.FechaFin = item.d_StartDate.Value.ToString("dd/MM/yyyy");
                                lError = "APQUIRURGICOS.AntecedenteLaboral";
                                objQuiru.APQUIRURGICOS.AntecedenteLaboral = item.i_TypeDiagnosticId == 4 ? "S" : "N";
                                lError = "APQUIRURGICOS.LanzarDatos";
                                LanzarDatos(client.EnviarDatosAPQuirurgicos(objQuiru), e, column, serviceId);
                            }
                        }
                        else
                        {

                            objQuiru.APQUIRURGICOS.CodigoCIE = "---";
                            objQuiru.APQUIRURGICOS.Descripcion = "---";
                            objQuiru.APQUIRURGICOS.FechaInicio = "---";
                            objQuiru.APQUIRURGICOS.FechaFin = "---";
                            objQuiru.APQUIRURGICOS.AntecedenteLaboral = "-";
                            LanzarDatos(client.EnviarDatosAPQuirurgicos(objQuiru), e, column, serviceId);
                        }


                        #endregion

                        break;
                    case "DatosExamen":
                        var objDatosExamen = new XmlDatosExamen();

                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);

                        lError = "datosPaciente.IDActuacion";
                        objDatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objDatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objDatosExamen.FechaRegistro = datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objDatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objDatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objDatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objDatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        lError = "datosPaciente.LanzarDatos";
                        LanzarDatos(client.EnviarDatosExamen(objDatosExamen), e, column, serviceId);
                        //var objDatosExmane = new EstructuraDatosExamen();


                        break;
                    case "DatosAudiometria":


                        var objAud = new EstructuraDatosAudiometria();

                        var audiometriaValores =
                            _ServiceBL.ValoresComponentesUserControl(serviceId, Constants.AUDIOMETRIA_ID);

                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);


                        #region DatosPaciente

                        objAud.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objAud.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objAud.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objAud.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objAud.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objAud.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objAud.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objAud.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objAud.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objAud.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objAud.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objAud.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objAud.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objAud.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objAud.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objAud.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objAud.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objAud.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objAud.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objAud.DatosExamen.FechaRegistro = datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objAud.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objAud.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objAud.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objAud.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion

                        #region DatosAudiometria

                        objAud.Audiometria = objAudiometriaValues(audiometriaValores);

                        #endregion

                        lError = "datosAudiometría.LanzarDatos";
                        LanzarDatos(client.EnviarDatosAudiometria(objAud), e, column, serviceId);

                        break;
                    case "DatosConstantes":

                        var objConstantes = new EstructuraDatosConstantes();

                        var constantes = _ServiceBL.ValoresComponenteconstantes(serviceId);


                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);


                        #region DatosPaciente

                        objConstantes.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objConstantes.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objConstantes.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objConstantes.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objConstantes.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objConstantes.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objConstantes.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objConstantes.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objConstantes.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objConstantes.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objConstantes.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objConstantes.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objConstantes.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objConstantes.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objConstantes.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objConstantes.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objConstantes.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objConstantes.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objConstantes.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objConstantes.DatosExamen.FechaRegistro =
                            datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objConstantes.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objConstantes.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objConstantes.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objConstantes.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion

                        #region DatosConstantes

                        objConstantes.Constantes = new XmlConstantes();

                        foreach (var item in constantes)
                        {
                            if (item.v_ComponentFieldId == Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID)
                            {
                                objConstantes.Constantes.Cintura = item.v_Value1;
                            }
                            else if (item.v_ComponentFieldId == Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID)
                            {
                                objConstantes.Constantes.Cadera = item.v_Value1;
                            }
                            else if (item.v_ComponentFieldId == Constants.FUNCIONES_VITALES_SAT_O2_ID)
                            {
                                objConstantes.Constantes.SaturacionOxigeno = item.v_Value1;
                            }
                            else if (item.v_ComponentFieldId == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID)
                            {
                                objConstantes.Constantes.Pulso = item.v_Value1;
                            }
                            else if (item.v_ComponentFieldId == Constants.FUNCIONES_VITALES_TEMPERATURA_ID)
                            {
                                objConstantes.Constantes.Temperatura = item.v_Value1;
                            }
                            else if (item.v_ComponentFieldId == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID)
                            {
                                objConstantes.Constantes.Respiracion = item.v_Value1;
                            }
                            else if (item.v_ComponentFieldId == Constants.ANTROPOMETRIA_INDICE_CINTURA_ID)
                            {
                                objConstantes.Constantes.ICC = item.v_Value1;
                            }
                            else if (item.v_ComponentFieldId == Constants.EXCEPCIONES_RX_FECHA_ULTIMA_REGLA)
                            {
                                objConstantes.Constantes.FechaUltimaRegla = item.v_Value1;
                            }
                        }

                        #endregion

                        lError = "datosConstantes.LanzarDatos";
                        LanzarDatos(client.EnviarDatosConstantes(objConstantes), e, column, serviceId);
                        break;

                    case "DatosExamenOcularVB":

                        var objExaOcular = new EstructuraDatosExamenOcularVB();

                        var examenOcular = _ServiceBL.ValoresComponenteExamenOcular(serviceId);

                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);


                        #region DatosPaciente

                        objExaOcular.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objExaOcular.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objExaOcular.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objExaOcular.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objExaOcular.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objExaOcular.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objExaOcular.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objExaOcular.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objExaOcular.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objExaOcular.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objExaOcular.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objExaOcular.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objExaOcular.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objExaOcular.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objExaOcular.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objExaOcular.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objExaOcular.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objExaOcular.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objExaOcular.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objExaOcular.DatosExamen.FechaRegistro =
                            datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objExaOcular.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objExaOcular.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objExaOcular.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objExaOcular.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion


                        #region ExamenOcular

                        foreach (var item in examenOcular)
                        {
                            objExaOcular.ExamenOcularVB = new XmlExamenOcularVB();

                            if (item.v_ComponentFieldId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCSCOD)
                            {
                                objExaOcular.ExamenOcularVB.VisionCercaODSCVB = item.v_Value1;
                            }
                            else if (item.v_ComponentFieldId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCSCOI)
                            {
                                objExaOcular.ExamenOcularVB.VisionCercaOISCVB = item.v_Value1;
                            }
                            else if (item.v_ComponentFieldId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLSCOD)
                            {
                                objExaOcular.ExamenOcularVB.VisionLejosODSCVB = item.v_Value1;
                            }
                            else if (item.v_ComponentFieldId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLSCOI)
                            {
                                objExaOcular.ExamenOcularVB.VisionLejosOISCVB = item.v_Value1;
                            }
                            else if (item.v_ComponentFieldId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCCCOD)
                            {
                                objExaOcular.ExamenOcularVB.VisionCercaODCVB = item.v_Value1;
                            }
                            else if (item.v_ComponentFieldId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCCCOI)
                            {
                                objExaOcular.ExamenOcularVB.VisionCercaOICVB = item.v_Value1;
                            }
                            else if (item.v_ComponentFieldId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLCCOD)
                            {
                                objExaOcular.ExamenOcularVB.VisionLejosODCVB = item.v_Value1;
                            }
                            else if (item.v_ComponentFieldId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLCCOI)
                            {
                                objExaOcular.ExamenOcularVB.VisionLejosOICVB = item.v_Value1;
                            }
                            else if (item.v_ComponentFieldId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TEST_ISHIHARA)
                            {
                                objExaOcular.ExamenOcularVB.TestColores = item.v_Value1;
                            }

                            //objExaOcular.ExamenOcularVB.RestriccionActual
                            //objExaOcular.ExamenOcularVB.TipoRestriccion
                        }

                        #endregion

                        lError = "datosExamenOcular.LanzarDatos";
                        LanzarDatos(client.EnviarDatosExamenOcularVB(objExaOcular), e, column, serviceId);

                        break;
                    case "DatosHabitosToxicos":

                        var objHabToxicos = new EstructuraDatosHabitosToxicos();

                        var habitos = _HistoryBL.GetNoxiousHabitsPagedAndFilteredByPersonId(ref objOperationResult, 0,
                            null, "i_TypeHabitsId DESC", null, personId);


                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);

                        #region DatosPaciente

                        objHabToxicos.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objHabToxicos.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objHabToxicos.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objHabToxicos.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objHabToxicos.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objHabToxicos.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objHabToxicos.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objHabToxicos.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objHabToxicos.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objHabToxicos.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objHabToxicos.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objHabToxicos.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objHabToxicos.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objHabToxicos.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objHabToxicos.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objHabToxicos.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objHabToxicos.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objHabToxicos.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objHabToxicos.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objHabToxicos.DatosExamen.FechaRegistro =
                            datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objHabToxicos.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objHabToxicos.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objHabToxicos.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objHabToxicos.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion

                        #region Habitos Toxicos
                        if (habitos.Count > 0)
                        {
                            foreach (var item in habitos)
                            {
                                objHabToxicos.HabitosToxicos = new XmlHabitosToxicos();
                                objHabToxicos.HabitosToxicos.Descripcion = item.v_DescriptionHabit;
                                objHabToxicos.HabitosToxicos.Vigencia = "---";
                                objHabToxicos.HabitosToxicos.TipoHabito = item.v_TypeHabitsName;
                                objHabToxicos.HabitosToxicos.Fechainicio = "---";
                                objHabToxicos.HabitosToxicos.FechaFin = "---";
                                lError = "datosConstantes.LanzarDatos";
                                LanzarDatos(client.EnviarDatosHabitosToxicos(objHabToxicos), e, column, serviceId);
                            }
                        }
                        else
                        {
                            objHabToxicos.HabitosToxicos = new XmlHabitosToxicos();
                            objHabToxicos.HabitosToxicos.Descripcion = "---";
                            objHabToxicos.HabitosToxicos.Vigencia = "---";
                            objHabToxicos.HabitosToxicos.TipoHabito = "-";
                            objHabToxicos.HabitosToxicos.Fechainicio = "---";
                            objHabToxicos.HabitosToxicos.FechaFin = "---";
                            LanzarDatos(client.EnviarDatosHabitosToxicos(objHabToxicos), e, column, serviceId);
                        }


                        #endregion

                        break;
                    case "DatosHistoriaFamiliar":
                        var objHistFamiliar = new EstructuraDatosHistoriaFamiliar();

                        var antecedentesFamiliares =
                            _HistoryBL.GetFamilyMedicalAntecedentsPagedAndFilteredByPersonId(ref objOperationResult, 0,
                                null, "v_DiseasesId DESC", null, personId);


                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);

                        #region DatosPaciente

                        objHistFamiliar.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objHistFamiliar.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objHistFamiliar.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objHistFamiliar.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objHistFamiliar.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objHistFamiliar.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objHistFamiliar.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objHistFamiliar.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objHistFamiliar.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objHistFamiliar.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objHistFamiliar.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objHistFamiliar.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objHistFamiliar.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objHistFamiliar.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objHistFamiliar.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objHistFamiliar.DatosPaciente.TipoDocumento =
                            short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objHistFamiliar.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objHistFamiliar.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objHistFamiliar.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objHistFamiliar.DatosExamen.FechaRegistro =
                            datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objHistFamiliar.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objHistFamiliar.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objHistFamiliar.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objHistFamiliar.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion


                        #region DatosHistoriaFamiliar
                        if (antecedentesFamiliares.Count > 0)
                        {
                            foreach (var item in antecedentesFamiliares)
                            {
                                objHistFamiliar.HistoriaFamiliar = new XmlHistoriaFamiliar();

                                objHistFamiliar.HistoriaFamiliar.CodigoCIE = item.v_CIE10Id;
                                objHistFamiliar.HistoriaFamiliar.Descripcion = item.DxAndComment;
                                objHistFamiliar.HistoriaFamiliar.Fechainicio = "---";
                                objHistFamiliar.HistoriaFamiliar.FechaFin = "---";

                                lError = "datosHistoriaFamiliar.LanzarDatos";
                                LanzarDatos(client.EnviarDatosHistoriaFamiliar(objHistFamiliar), e, column, serviceId);
                            }
                        }
                        else
                        {
                            objHistFamiliar.HistoriaFamiliar = new XmlHistoriaFamiliar();

                            objHistFamiliar.HistoriaFamiliar.CodigoCIE = "---";
                            objHistFamiliar.HistoriaFamiliar.Descripcion = "---";
                            objHistFamiliar.HistoriaFamiliar.Fechainicio = "---";
                            objHistFamiliar.HistoriaFamiliar.FechaFin = "---";

                            lError = "datosHistoriaFamiliar.LanzarDatos";
                            LanzarDatos(client.EnviarDatosHistoriaFamiliar(objHistFamiliar), e, column, serviceId);
                        }


                        #endregion


                        break;

                    case "DatosHistoriaLaboral":

                        var objHistLaboral = new EstructuraDatosHistoriaLaboral();

                        var historiaLaboral = _HistoryBL.GetHistoryPagedAndFiltered(ref objOperationResult, 0, null,
                            "d_StartDate DESC", null, personId);




                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);

                        #region DatosPaciente

                        objHistLaboral.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objHistLaboral.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objHistLaboral.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objHistLaboral.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objHistLaboral.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objHistLaboral.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objHistLaboral.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objHistLaboral.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objHistLaboral.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objHistLaboral.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objHistLaboral.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objHistLaboral.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objHistLaboral.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objHistLaboral.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objHistLaboral.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objHistLaboral.DatosPaciente.TipoDocumento =
                            short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objHistLaboral.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objHistLaboral.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objHistLaboral.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objHistLaboral.DatosExamen.FechaRegistro =
                            datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objHistLaboral.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objHistLaboral.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objHistLaboral.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objHistLaboral.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion


                        #region DatosExamen
                        if (historiaLaboral.Count > 0)
                        {
                            foreach (var item in historiaLaboral)
                            {

                                objHistLaboral.HistoriaLaboral = new XmlHistoriaLaboral();

                                objHistLaboral.HistoriaLaboral.EmpresaLaboral = item.v_Organization;
                                objHistLaboral.HistoriaLaboral.ZonaLaboral = item.v_workstation;
                                //objHistLaboral.HistoriaLaboral.RUCEmpresa = item.;
                                //objHistLaboral.HistoriaLaboral.PuestosHistorial = item;
                                //objHistLaboral.HistoriaLaboral.DuracionPuestoA = item.;
                                //objHistLaboral.HistoriaLaboral.DuracionPuestoM = item.;
                                //objHistLaboral.HistoriaLaboral.FechaInicioPuesto = item.;
                                //objHistLaboral.HistoriaLaboral.FechaFinPuesto = item.;
                                //objHistLaboral.HistoriaLaboral.ActividadCNAE = item.;
                                //objHistLaboral.HistoriaLaboral.PuestoActual = item.;
                                //objHistLaboral.HistoriaLaboral.DescripcionTareas = item.;
                                //objHistLaboral.HistoriaLaboral.ValoracionRiesgo = item.;
                                //objHistLaboral.HistoriaLaboral.AreaTrabajo = item.;                        
                                //objHistLaboral.HistoriaLaboral.AlturaLaboral = item.;
                                //objHistLaboral.HistoriaLaboral.DuracionTrabajosAlturaA = item.;
                                //objHistLaboral.HistoriaLaboral.DuracionPuestoM = item.;
                                //objHistLaboral.HistoriaLaboral.PostulaPuesto = item.;

                                LanzarDatos(client.EnviarDatosHistoriaLaboral(objHistLaboral), e, column, serviceId);
                            }
                        }
                        else
                        {
                            objHistLaboral.HistoriaLaboral = new XmlHistoriaLaboral();

                            objHistLaboral.HistoriaLaboral.EmpresaLaboral = "---";
                            objHistLaboral.HistoriaLaboral.ZonaLaboral = "---";
                            objHistLaboral.HistoriaLaboral.RUCEmpresa = "---";
                            objHistLaboral.HistoriaLaboral.PuestosHistorial = "---";
                            objHistLaboral.HistoriaLaboral.DuracionPuestoA = "---";
                            objHistLaboral.HistoriaLaboral.DuracionPuestoM = "---";
                            objHistLaboral.HistoriaLaboral.FechaInicioPuesto = "---";
                            objHistLaboral.HistoriaLaboral.FechaFinPuesto = "---";
                            objHistLaboral.HistoriaLaboral.ActividadCNAE = "---";
                            objHistLaboral.HistoriaLaboral.PuestoActual = "---";
                            objHistLaboral.HistoriaLaboral.DescripcionTareas = "---";
                            objHistLaboral.HistoriaLaboral.ValoracionRiesgo = "---";
                            objHistLaboral.HistoriaLaboral.AreaTrabajo = "---";
                            objHistLaboral.HistoriaLaboral.AlturaLaboral = "---";
                            objHistLaboral.HistoriaLaboral.DuracionTrabajosAlturaA = "---";
                            objHistLaboral.HistoriaLaboral.DuracionPuestoM = "---";
                            objHistLaboral.HistoriaLaboral.PostulaPuesto = "---";

                            LanzarDatos(client.EnviarDatosHistoriaLaboral(objHistLaboral), e, column, serviceId);

                        }


                        #endregion

                        break;
                    case "DatosAnalitica":


                        var objAnalitica = new EstructuraDatosAnalitica();

                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);

                        #region DatosPaciente

                        objAnalitica.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objAnalitica.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objAnalitica.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objAnalitica.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objAnalitica.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objAnalitica.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objAnalitica.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objAnalitica.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objAnalitica.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objAnalitica.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objAnalitica.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objAnalitica.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objAnalitica.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objAnalitica.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objAnalitica.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objAnalitica.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objAnalitica.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objAnalitica.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objAnalitica.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objAnalitica.DatosExamen.FechaRegistro =
                            datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objAnalitica.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objAnalitica.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objAnalitica.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objAnalitica.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion

                        #region DatosAnalitica

                        List<string> ListServie = new List<string>();
                        ListServie.Add(serviceId);
                        var consolidadoDatos = new ServiceBL().DevolverValorCampoPorServicioMejorado(ListServie);
                        var datosServicioCompleto = consolidadoDatos.Find(p => p.ServicioId == serviceId);
                        var perfiles = datosServicioCompleto.CampoValores
                            .FindAll(p => p.CategoryId == (int) CategoryTypeExam.Laboratorio)
                            .GroupBy(p => p.IdComponente).Select(group => group.First()).ToList();

                        objAnalitica.Analitica = new XmlAnalitica();
                        var contador = 1;
                        //objAnalitica.Analitica.ListaPerfil =  XmlAnaliticaPerfil[];
                        //objAnalitica.Analitica.ListaPruebas =  XmlAnaliticaPruebas[];
                        foreach (var item in perfiles)
                        {
                            /////
                            var Value = CodigoNatclarLaboratorio(item.IdComponente);
                            var objXml = new XmlAnaliticaPerfil();
                            objAnalitica.Analitica.ListaPerfil = new XmlAnaliticaPerfil[1];
                            objXml.Perfil = "Perfil: " + contador.ToString() + ", Valor: " + Value;
                            objAnalitica.Analitica.ListaPerfil.ToList().Add(objXml);
                            /////

                            int contadorPrueb = 1;
                            var campos = datosServicioCompleto.CampoValores
                                .FindAll(p => p.IdComponente == item.IdComponente).ToList();
                            foreach (var prueba in campos)
                            {
                                string valorPrueb = CodigoNatclarLaboratorio(prueba.IdCampo);
                                var objPruebas = new XmlAnaliticaPruebas();
                                objAnalitica.Analitica.ListaPruebas = new XmlAnaliticaPruebas[1];
                                objPruebas.Pruebas = "Prueba: " + contadorPrueb.ToString() + ", Valor: " + valorPrueb;
                                objAnalitica.Analitica.ListaPruebas.ToList().Add(objPruebas);
                                contadorPrueb++;
                            }

                            LanzarDatos(client.EnviarDatosAnalitica(objAnalitica), e, column, serviceId);

                            contador++;

                        }
                        if (perfiles.Count == 0)
                        {
                            /////

                            var objXml = new XmlAnaliticaPerfil();
                            objXml.Perfil = "Perfil: ---, Valor: ---";
                            objAnalitica.Analitica.ListaPerfil = new XmlAnaliticaPerfil[1];
                            objAnalitica.Analitica.ListaPerfil.ToList().Add(objXml);

                            var objPruebas = new XmlAnaliticaPruebas();
                            objPruebas.Pruebas = "Prueba: ---, Valor: ---";
                            objAnalitica.Analitica.ListaPruebas = new XmlAnaliticaPruebas[1];
                            objAnalitica.Analitica.ListaPruebas.ToList().Add(objPruebas);

                            LanzarDatos(client.EnviarDatosAnalitica(objAnalitica), e, column, serviceId);
                            /////
                        }
                        #endregion

                        break;



                    case "DatosHistoriaLaboralRl":
                        var objHistLaboralRl = new EstructuraDatosHistoriaLaboralRI();

                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);

                        #region DatosPaciente

                        objHistLaboralRl.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objHistLaboralRl.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objHistLaboralRl.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objHistLaboralRl.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objHistLaboralRl.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objHistLaboralRl.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objHistLaboralRl.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objHistLaboralRl.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objHistLaboralRl.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objHistLaboralRl.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objHistLaboralRl.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objHistLaboralRl.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objHistLaboralRl.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objHistLaboralRl.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objHistLaboralRl.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objHistLaboralRl.DatosPaciente.TipoDocumento =
                            short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objHistLaboralRl.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objHistLaboralRl.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objHistLaboralRl.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objHistLaboralRl.DatosExamen.FechaRegistro =
                            datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objHistLaboralRl.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objHistLaboralRl.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objHistLaboralRl.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objHistLaboralRl.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion

                        #region HistoriaLaboralRI

                        objHistLaboralRl.HistoriaLaboralRI = new XmlHistoriaLaboralRI();
                        LanzarDatos(client.EnviarDatosHistoriaLaboralRI(objHistLaboralRl), e, column, serviceId);
                        ////Falta la consulta

                        #endregion


                        break;

                    case "DatosLecturaOIT":
                        var objLecturaOIT = new EstructuraDatosLecturaOIT();

                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);

                        #region DatosPaciente

                        objLecturaOIT.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objLecturaOIT.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objLecturaOIT.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objLecturaOIT.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objLecturaOIT.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objLecturaOIT.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objLecturaOIT.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objLecturaOIT.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objLecturaOIT.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objLecturaOIT.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objLecturaOIT.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objLecturaOIT.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objLecturaOIT.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objLecturaOIT.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objLecturaOIT.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objLecturaOIT.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objLecturaOIT.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objLecturaOIT.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objLecturaOIT.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objLecturaOIT.DatosExamen.FechaRegistro =
                            datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objLecturaOIT.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objLecturaOIT.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objLecturaOIT.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objLecturaOIT.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion

                        #region LecturaOIT

                        var examenOIT = new ServiceBL().ValoresComponente(serviceId, Constants.OIT_ID);
                        if (examenOIT.Count > 0)
                        {
                            foreach (var item in examenOIT)
                            {
                                objLecturaOIT.LecturaOIT = new XmlLecturaOIT();

                                objLecturaOIT.LecturaOIT.AnormalidadParenquitamosa =
                                    examenOIT.Find(p => p.v_ComponentFieldId == Constants.RX_0_0_ID) == null ? "" :
                                    examenOIT.Find(p => p.v_ComponentFieldId == Constants.RX_0_0_ID).v_Value1 != "1" ? "S" :
                                    "N";
                                objLecturaOIT.LecturaOIT.AnormalidadPleural = ObtenerAnormalidadPleural(examenOIT);
                                objLecturaOIT.LecturaOIT.Calidad =
                                    examenOIT.Find(p => p.v_ComponentFieldId == Constants.RX_CALIDAD_ID) == null
                                        ? ""
                                        : examenOIT.Find(p => p.v_ComponentFieldId == Constants.RX_CALIDAD_ID).v_Value1Name;
                                objLecturaOIT.LecturaOIT.DescripcionAnormalidadPleural = ""; //WALTER FALTA CREAR CAMPO;
                                objLecturaOIT.LecturaOIT.DescripcionOtrasAnomalias = ""; //WALTER FALTA CREAR CAMPO;
                                objLecturaOIT.LecturaOIT.ObservacionCalidad =
                                    examenOIT.Find(p => p.v_ComponentFieldId == Constants.RX_COMENTARIOS_ID) == null
                                        ? ""
                                        : examenOIT.Find(p => p.v_ComponentFieldId == Constants.RX_COMENTARIOS_ID).v_Value1;
                                objLecturaOIT.LecturaOIT.OpacidadesAbundancia = ObtenerOpacidadesAbundancia(examenOIT);
                                objLecturaOIT.LecturaOIT.OpacidadesPequeñas = ObtenerOpacidadesPequenias(examenOIT);
                                objLecturaOIT.LecturaOIT.OtrasAnomalias = ""; //WALTER FALTA CREAR CAMPO;

                                LanzarDatos(client.EnviarDatosLecturaOIT(objLecturaOIT), e, column, serviceId);

                            }
                        }
                        else
                        {
                            objLecturaOIT.LecturaOIT = new XmlLecturaOIT();

                            objLecturaOIT.LecturaOIT.AnormalidadParenquitamosa = "---";

                            objLecturaOIT.LecturaOIT.AnormalidadPleural = examenOIT.Count == 0 ? "---" :  ObtenerAnormalidadPleural(examenOIT);
                            objLecturaOIT.LecturaOIT.Calidad = "---";
                            objLecturaOIT.LecturaOIT.DescripcionAnormalidadPleural = "---";
                            objLecturaOIT.LecturaOIT.DescripcionOtrasAnomalias = "---";
                            objLecturaOIT.LecturaOIT.ObservacionCalidad = "---";
                            objLecturaOIT.LecturaOIT.OpacidadesAbundancia = "---";
                            objLecturaOIT.LecturaOIT.OpacidadesPequeñas = "---";
                            objLecturaOIT.LecturaOIT.OtrasAnomalias = "---";

                            LanzarDatos(client.EnviarDatosLecturaOIT(objLecturaOIT), e, column, serviceId);
                        }


                        #endregion

                        break;
                    case "DatosLecturaPsicologica":

                        var objLecturaPsicologia = new EstructuraDatosPsicologia();
                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);

                        #region DatosPaciente

                        objLecturaPsicologia.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objLecturaPsicologia.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objLecturaPsicologia.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objLecturaPsicologia.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objLecturaPsicologia.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objLecturaPsicologia.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objLecturaPsicologia.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objLecturaPsicologia.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objLecturaPsicologia.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objLecturaPsicologia.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objLecturaPsicologia.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objLecturaPsicologia.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objLecturaPsicologia.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objLecturaPsicologia.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objLecturaPsicologia.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objLecturaPsicologia.DatosPaciente.TipoDocumento =
                            short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objLecturaPsicologia.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objLecturaPsicologia.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objLecturaPsicologia.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objLecturaPsicologia.DatosExamen.FechaRegistro =
                            datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objLecturaPsicologia.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objLecturaPsicologia.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objLecturaPsicologia.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objLecturaPsicologia.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion

                        #region DatosPsicologia

                        ListServie = new List<string>();
                        ListServie.Add(serviceId);
                        consolidadoDatos = new ServiceBL().DevolverValorCampoPorServicioMejorado(ListServie);
                        string areaCog = consolidadoDatos.Count > 0 ? "---" :
                            consolidadoDatos[0].CampoValores
                                .Find(x => x.IdCampo == Constants.PSICOLOGIA_AREA_COGNITIVA_ID) == null ? "---" :
                            consolidadoDatos[0].CampoValores
                                .Find(x => x.IdCampo == Constants.PSICOLOGIA_AREA_COGNITIVA_ID).Valor;
                        string areaEmocional = consolidadoDatos.Count > 0 ? "---" :
                            consolidadoDatos[0].CampoValores
                                .Find(x => x.IdCampo == Constants.PSICOLOGIA_AREA_EMOCIONAL_ID) == null ? "---" :
                            consolidadoDatos[0].CampoValores
                                .Find(x => x.IdCampo == Constants.PSICOLOGIA_AREA_EMOCIONAL_ID).Valor;
                        string areaPersonal = consolidadoDatos.Count > 0 ? "---" :
                            consolidadoDatos[0].CampoValores
                                .Find(x => x.IdCampo == Constants.PSICOLOGIA_AREA_PERSONAL_ID) == null ? "---" :
                            consolidadoDatos[0].CampoValores
                                .Find(x => x.IdCampo == Constants.PSICOLOGIA_AREA_PERSONAL_ID).Valor;
                        objLecturaPsicologia.Psicologia = new XmlPsicologia();
                        objLecturaPsicologia.Psicologia.Aptitud = consolidadoDatos.Count > 0 ? "---" :
                            consolidadoDatos[0].CampoValores
                                .Find(x => x.IdCampo == Constants.PSICOLOGIA_APTITUD_PSICOLOGICA_ID) == null ? "---" :
                            consolidadoDatos[0].CampoValores
                                .Find(x => x.IdCampo == Constants.PSICOLOGIA_APTITUD_PSICOLOGICA_ID).Valor;
                        objLecturaPsicologia.Psicologia.Informes =
                            "Área Cognitiva: " + areaCog + ", Área Emocional: " + areaEmocional + ", Área Personal: " +
                            areaPersonal + ".";
                        objLecturaPsicologia.Psicologia.Recomendaciones = consolidadoDatos.Count > 0 ? "---" :
                            consolidadoDatos[0].CampoValores.Find(x =>
                                x.IdCampo == Constants.PSICOLOGIA_RECOMENDACIONES_ESPECIFICAS_ID) == null ? "---" :
                            consolidadoDatos[0].CampoValores.Find(x =>
                                x.IdCampo == Constants.PSICOLOGIA_RECOMENDACIONES_ESPECIFICAS_ID).Valor;

                        #endregion

                        LanzarDatos(client.EnviarDatosLecturaPsicologia(objLecturaPsicologia), e, column, serviceId);
                        break;

                    case "DatosApMedicos":

                        var objApMedicos = new EstructuraDatosAPMedicos();

                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);

                        #region DatosPaciente

                        objApMedicos.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objApMedicos.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objApMedicos.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objApMedicos.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objApMedicos.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objApMedicos.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objApMedicos.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objApMedicos.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objApMedicos.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objApMedicos.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objApMedicos.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objApMedicos.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objApMedicos.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objApMedicos.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objApMedicos.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objApMedicos.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objApMedicos.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objApMedicos.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objApMedicos.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objApMedicos.DatosExamen.FechaRegistro =
                            datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objApMedicos.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objApMedicos.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objApMedicos.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objApMedicos.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion

                        #region DatosApMedicos

                        objApMedicos.APMEDICOS = new XmlAntecedentesPatologicosMedicos();

                        OperationResult res = new OperationResult();
                        var listAntecedentes = new ServiceBL().GetAntecedentConsolidateForService(ref res, serviceId);
                        string CodigoCIE = "---------", Descripcion = "-----", FechaFin = "----------", FechaInicio = "----------", AntecedenteLaboral = "-";
                        if (listAntecedentes.Count > 0)
                        {

                            foreach (var labo in listAntecedentes)
                            {
                                string laboral = "N";
                                if (labo.v_AntecedentTypeName.Contains("Ocupacionales"))
                                {
                                    laboral = "S";
                                }
                                objApMedicos.APMEDICOS.AntecedenteLaboral = laboral;
                                objApMedicos.APMEDICOS.CodigoCIE = labo.v_CIE10Id == null ? "----------" : labo.v_CIE10Id;
                                objApMedicos.APMEDICOS.Descripcion = labo.v_DiseasesName == null ? "----------" : labo.v_DiseasesName;
                                objApMedicos.APMEDICOS.FechaFin = labo.d_EndDate == null ? "---" : labo.d_EndDate.Value.ToShortDateString();
                                objApMedicos.APMEDICOS.FechaInicio = labo.d_StartDate == null ? "---" : labo.d_StartDate.Value.ToShortDateString();

                                LanzarDatos(client.EnviarDatosAPMedicos(objApMedicos), e, column, serviceId);
                            }
                        }
                        else
                        {
                            objApMedicos.APMEDICOS.AntecedenteLaboral = "-----";
                            objApMedicos.APMEDICOS.CodigoCIE = "-----";
                            objApMedicos.APMEDICOS.Descripcion = "-----";
                            objApMedicos.APMEDICOS.FechaFin = "-----";
                            objApMedicos.APMEDICOS.FechaInicio = "-----";

                            LanzarDatos(client.EnviarDatosAPMedicos(objApMedicos), e, column, serviceId);

                        }

                        #endregion


                        break;
                    case "DatosEpisodio":
                        var objepisodio = new EstructuraDatosEpisodio();
                        
                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);

                        #region DatosPaciente

                        objepisodio.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objepisodio.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objepisodio.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objepisodio.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objepisodio.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objepisodio.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objepisodio.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objepisodio.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objepisodio.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objepisodio.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objepisodio.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objepisodio.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objepisodio.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objepisodio.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objepisodio.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objepisodio.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objepisodio.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objepisodio.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objepisodio.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objepisodio.DatosExamen.FechaRegistro =
                            datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objepisodio.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objepisodio.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objepisodio.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objepisodio.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion

                        #region DatosEspisodio

                        objepisodio.Episodio = new XmlEpisodio();

                        var objDataEpisodio = ServiceBL.GetDataEpisodioByServiceId(serviceId);
                        if (objDataEpisodio != null)
                        {
                            objepisodio.Episodio.Caducidad = objDataEpisodio.Caducidad == null ? "---" : objDataEpisodio.Caducidad.Value.ToShortDateString();
                            objepisodio.Episodio.ContratanteCodigo = objDataEpisodio.WorkingOrganizationRuc == null ? "---" : objDataEpisodio.WorkingOrganizationRuc;
                            objepisodio.Episodio.Contratista = objDataEpisodio.WorkingOrganizationName == null ? "---" : objDataEpisodio.WorkingOrganizationName;
                            objepisodio.Episodio.EmpTitularRUC = objDataEpisodio.CustomerOrganizationRuc == null ? "---" : objDataEpisodio.CustomerOrganizationRuc;
                            objepisodio.Episodio.EmpresaTitular = objDataEpisodio.CustomerOrganizationName == null ? "---" : objDataEpisodio.CustomerOrganizationName;
                            objepisodio.Episodio.FechaExamen = objDataEpisodio.FechaExamen == null ? "---" : objDataEpisodio.FechaExamen.Value.ToShortDateString();
                            objepisodio.Episodio.GradoInstruccion = objDataEpisodio.GradoInstruccion == null ? "---" : objDataEpisodio.GradoInstruccion;
                            objepisodio.Episodio.Observaciones = "---";
                            objepisodio.Episodio.Ocupacion = objDataEpisodio.Occupation == null ? "---" : objDataEpisodio.Occupation;
                            objepisodio.Episodio.TipoDeExamen = objDataEpisodio.TipoExamen == null ? "---" : objDataEpisodio.TipoExamen;
                            objepisodio.Episodio.TipoTarea = "---";
                            objepisodio.Episodio.Unidad = "---";
                            objepisodio.Episodio.UnidadCodigo = "--";
                            objepisodio.Episodio.Vigencia = "---";
                            objepisodio.Episodio.AreaTrabajo = "---";
                            objepisodio.Episodio.ZonaTrabajo = objDataEpisodio.ZonaTrabajo == null ? "---" : objDataEpisodio.ZonaTrabajo;

                            LanzarDatos(client.EnviarDatosEpisodio(objepisodio), e, column, serviceId);
                        }
                        else
                        {
                            objepisodio.Episodio.Caducidad = "---";
                            objepisodio.Episodio.ContratanteCodigo = "---";
                            objepisodio.Episodio.Contratista = "---";
                            objepisodio.Episodio.EmpTitularRUC = "---";
                            objepisodio.Episodio.EmpresaTitular = "---";
                            objepisodio.Episodio.FechaExamen = "---";
                            objepisodio.Episodio.GradoInstruccion = "---";
                            objepisodio.Episodio.Observaciones = "---";
                            objepisodio.Episodio.Ocupacion = "---";
                            objepisodio.Episodio.TipoDeExamen = "---";
                            objepisodio.Episodio.TipoTarea = "---";
                            objepisodio.Episodio.Unidad = "---";
                            objepisodio.Episodio.UnidadCodigo = "--";
                            objepisodio.Episodio.Vigencia = "---";
                            objepisodio.Episodio.AreaTrabajo = "---";
                            objepisodio.Episodio.ZonaTrabajo = "---";

                            LanzarDatos(client.EnviarDatosEpisodio(objepisodio), e, column, serviceId);
                        }

                        
                        #endregion

                        break;
                    case "DatosMedicaciones":
                        var objMedicaciones = new EstructuraDatosMedicaciones();
                         datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);

                        #region DatosPaciente

                        objMedicaciones.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objMedicaciones.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objMedicaciones.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objMedicaciones.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objMedicaciones.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objMedicaciones.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objMedicaciones.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objMedicaciones.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objMedicaciones.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objMedicaciones.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objMedicaciones.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objMedicaciones.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objMedicaciones.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objMedicaciones.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objMedicaciones.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objMedicaciones.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objMedicaciones.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objMedicaciones.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objMedicaciones.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objMedicaciones.DatosExamen.FechaRegistro =
                            datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objMedicaciones.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objMedicaciones.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objMedicaciones.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objMedicaciones.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion

                        #region DatosMedicaciones

                        objMedicaciones.Medicaciones = new XmlMedicaciones();

                        objMedicaciones.Medicaciones.Vigencia = "---";
                        objMedicaciones.Medicaciones.Descripción = "---";
                        objMedicaciones.Medicaciones.FechaFin = "---";
                        objMedicaciones.Medicaciones.Fechainicio = "---";
                        objMedicaciones.Medicaciones.TipoDeMedicamento = "---";
                        ///////noxioushabits : no lo usan.....
                        LanzarDatos(client.EnviarDatosMedicaciones(objMedicaciones), e, column, serviceId);
                        #endregion

                        break;
                    case "DatosVacunas":
                        
                        var objDatosVacunas = new EstructuraDatosVacunas();

                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);

                        #region DatosPaciente

                        objDatosVacunas.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objDatosVacunas.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objDatosVacunas.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objDatosVacunas.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objDatosVacunas.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objDatosVacunas.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objDatosVacunas.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objDatosVacunas.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objDatosVacunas.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objDatosVacunas.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objDatosVacunas.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objDatosVacunas.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objDatosVacunas.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objDatosVacunas.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objDatosVacunas.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objDatosVacunas.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objDatosVacunas.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objDatosVacunas.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objDatosVacunas.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objDatosVacunas.DatosExamen.FechaRegistro =
                            datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objDatosVacunas.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objDatosVacunas.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objDatosVacunas.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objDatosVacunas.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion

                        #region DatosVacunas


                        string[] Vacunas =
                        {
                            "N009-ME000000063", //VACUNA FIEBRE AMARILLA
                            "N009-ME000000064", //VACUNA INFLUENZA
                            "N009-ME000000065", //VACUNA DIFTERIA/TETANOS
                            "N009-ME000000066", //VACUNA HEPATITIS A
                            "N009-ME000000067", //VACUNA HEPATITIS B
                            "N009-ME000000068", //VACUNA ANTIRRÁBICA
                            "N009-ME000000070", //VACUNA TRIPLE
                            "N009-ME000000071", //VACUNA VARICELA
                            "N010-ME000000601", //VACUNA VAXIGRIP
                            "N010-ME000000602" //VACUNA DE RECIEN NACIDO
                        };

                        bool envio = false;
                        foreach (var item in Vacunas)
                        {
                            var dataVacunas = new ServiceBL().ObtenerDetalleVacuna(serviceId, item);
                            if (dataVacunas.Count > 0)
                            {
                                envio = true;
                                foreach (var objVac in dataVacunas)
                                {
                                    objDatosVacunas.Vacunas = new XmlVacunas();
                                    objDatosVacunas.Vacunas.Código = "";
                                    objDatosVacunas.Vacunas.Descripción = objVac.v_Value1 == null ? "---" : objVac.v_Value1;
                                    objDatosVacunas.Vacunas.Dosis = objVac.Dosis == null ? "---" : objVac.Dosis;
                                    objDatosVacunas.Vacunas.FechaInicio = objVac.FechaVacuna == null ? "---" : objVac.FechaVacuna.Value.ToShortDateString();
                                    objDatosVacunas.Vacunas.FechaFin = "";

                                    LanzarDatos(client.EnviarDatosVacunas(objDatosVacunas), e, column, serviceId);
                                }

                            }
                            
                        }
                        if (!envio)
                        {
                            objDatosVacunas.Vacunas = new XmlVacunas();
                            objDatosVacunas.Vacunas.Código = "---";
                            objDatosVacunas.Vacunas.Descripción = "---";
                            objDatosVacunas.Vacunas.Dosis = "---";
                            objDatosVacunas.Vacunas.FechaInicio = "---";
                            objDatosVacunas.Vacunas.FechaFin = "---";

                            LanzarDatos(client.EnviarDatosVacunas(objDatosVacunas), e, column, serviceId);
                        }
                        

                        #endregion

                        break;
                    case "DatosExamenFisico":
                        var objDatosExamenFisico = new EstructuraDatosExamenFisico();

                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);

                        #region DatosPaciente

                        objDatosExamenFisico.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objDatosExamenFisico.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objDatosExamenFisico.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objDatosExamenFisico.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objDatosExamenFisico.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objDatosExamenFisico.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objDatosExamenFisico.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objDatosExamenFisico.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objDatosExamenFisico.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objDatosExamenFisico.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objDatosExamenFisico.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objDatosExamenFisico.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objDatosExamenFisico.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objDatosExamenFisico.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objDatosExamenFisico.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objDatosExamenFisico.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objDatosExamenFisico.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objDatosExamenFisico.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objDatosExamenFisico.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objDatosExamenFisico.DatosExamen.FechaRegistro =
                            datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objDatosExamenFisico.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objDatosExamenFisico.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objDatosExamenFisico.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objDatosExamenFisico.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion

                        #region DatosExamenFisico

                        objDatosExamenFisico.ExamenFisico = new XmlExamenFisico();
                        //No tiene propiedades
                        LanzarDatos(client.EnviarDatosExamenFisico(objDatosExamenFisico), e, column, serviceId);
                        #endregion

                        break;
                    case "DatosHistoriaFisiologico":
                        var objDatosHistoriaFisiologico = new EstructuraDatosHistorialFisiologico();

                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);

                        #region DatosPaciente

                        objDatosHistoriaFisiologico.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objDatosHistoriaFisiologico.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objDatosHistoriaFisiologico.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objDatosHistoriaFisiologico.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objDatosHistoriaFisiologico.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objDatosHistoriaFisiologico.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objDatosHistoriaFisiologico.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objDatosHistoriaFisiologico.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objDatosHistoriaFisiologico.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objDatosHistoriaFisiologico.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objDatosHistoriaFisiologico.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objDatosHistoriaFisiologico.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objDatosHistoriaFisiologico.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objDatosHistoriaFisiologico.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objDatosHistoriaFisiologico.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objDatosHistoriaFisiologico.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objDatosHistoriaFisiologico.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objDatosHistoriaFisiologico.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objDatosHistoriaFisiologico.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objDatosHistoriaFisiologico.DatosExamen.FechaRegistro =
                            datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objDatosHistoriaFisiologico.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objDatosHistoriaFisiologico.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objDatosHistoriaFisiologico.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objDatosHistoriaFisiologico.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion

                        #region DatosHistoriaFisiologico

                        objDatosHistoriaFisiologico.HistorialFisiologico = new XmlHistorialFisiologico();

                        objDatosHistoriaFisiologico.HistorialFisiologico.Vigencia = "---";
                        objDatosHistoriaFisiologico.HistorialFisiologico.Descripción = "---";                        
                        objDatosHistoriaFisiologico.HistorialFisiologico.Tipo = "---";
                        objDatosHistoriaFisiologico.HistorialFisiologico.Fechainicio = "---";
                        objDatosHistoriaFisiologico.HistorialFisiologico.FechaFin = "---";
                        LanzarDatos(client.EnviarDatosHistorialFisiologico(objDatosHistoriaFisiologico), e, column, serviceId);
                        #endregion

                        break;
                    case "DatosAlergias":

                        var objDatosAlergias = new EstructuraDatosAlergias();

                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);

                        #region DatosPaciente

                        objDatosAlergias.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objDatosAlergias.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objDatosAlergias.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objDatosAlergias.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objDatosAlergias.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objDatosAlergias.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objDatosAlergias.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objDatosAlergias.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objDatosAlergias.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objDatosAlergias.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objDatosAlergias.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objDatosAlergias.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objDatosAlergias.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objDatosAlergias.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objDatosAlergias.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objDatosAlergias.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objDatosAlergias.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objDatosAlergias.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objDatosAlergias.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objDatosAlergias.DatosExamen.FechaRegistro =
                            datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objDatosAlergias.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objDatosAlergias.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objDatosAlergias.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objDatosAlergias.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion

                        #region MyRegion

                        var listAlergias = oNatclarBL.DataAlergias(serviceId);
                        if (listAlergias.Count > 0)
	                    {
                            foreach (var item in listAlergias)
                            {
                                objDatosAlergias.Alergias = new XmlDatosAlergias();
                                objDatosAlergias.Alergias.Descripcion = item.v_Name;
                                objDatosAlergias.Alergias.FechaInicio = item.d_CreationDate == null ? "---" : item.d_CreationDate.ToString();
                                objDatosAlergias.Alergias.Tipo = item.v_DiseasesId == Constants.ALERGIA_DERIVADOS_LACTEOS ? "001" : item.v_DiseasesId == Constants.ALERGIA_FRUTOS_SECOS ? "002" : item.v_DiseasesId == Constants.ALERGIA_PELO_ANIMAL ? "478" : "---" ;

                                LanzarDatos(client.EnviarDatosAlergias(objDatosAlergias), e, column, serviceId);
                            }
	                    }
                        else
                        {
                            objDatosAlergias.Alergias = new XmlDatosAlergias();
                            objDatosAlergias.Alergias.Descripcion = "---";
                            objDatosAlergias.Alergias.FechaInicio = "---";
                            objDatosAlergias.Alergias.Tipo = "---";

                            LanzarDatos(client.EnviarDatosAlergias(objDatosAlergias), e, column, serviceId);
                        }



                        
                        #endregion


                        break;
                    case "DatosVigilanciaSalud":
                        var objDatosVigilanciaSalud = new EstructuraDatosVigilanciaSalud();
                        
                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento,
                            datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);

                        #region DatosPaciente

                        objDatosVigilanciaSalud.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objDatosVigilanciaSalud.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objDatosVigilanciaSalud.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objDatosVigilanciaSalud.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objDatosVigilanciaSalud.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objDatosVigilanciaSalud.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objDatosVigilanciaSalud.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objDatosVigilanciaSalud.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objDatosVigilanciaSalud.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objDatosVigilanciaSalud.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objDatosVigilanciaSalud.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objDatosVigilanciaSalud.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objDatosVigilanciaSalud.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objDatosVigilanciaSalud.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objDatosVigilanciaSalud.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objDatosVigilanciaSalud.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objDatosVigilanciaSalud.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objDatosVigilanciaSalud.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objDatosVigilanciaSalud.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objDatosVigilanciaSalud.DatosExamen.FechaRegistro =
                            datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objDatosVigilanciaSalud.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objDatosVigilanciaSalud.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objDatosVigilanciaSalud.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objDatosVigilanciaSalud.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion

                        #region DatosVigilancia

                        
                        objDatosVigilanciaSalud.VigilanciaSalud = new XmlVigilanciaSalud();


                        #region Diagnosticos
                        var diagnosticos = oNatclarBL.DataDiagnosticos(serviceId);
		                objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos = new XmlDiagnosticos();

                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnóstico01 = diagnosticos.Count < 1 ? "---" : diagnosticos[0].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.FechaRegistro01 = diagnosticos.Count < 1 ? "---" : diagnosticos[0].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();

                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnostico02 = diagnosticos.Count < 2 ? "---" : diagnosticos[1].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.FechaRegistro02 = diagnosticos.Count < 2 ? "---" : diagnosticos[1].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();

                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnostico03 = diagnosticos.Count < 3 ? "---" : diagnosticos[2].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.FechaRegistro03 = diagnosticos.Count < 3 ? "---" : diagnosticos[2].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();
                        
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnostico04 = diagnosticos.Count < 4 ? "---" : diagnosticos[3].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.FechaRegistro04 = diagnosticos.Count < 4 ? "---" : diagnosticos[3].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();

                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnostico05 = diagnosticos.Count < 5 ? "---" : diagnosticos[4].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.FechaRegistro05 = diagnosticos.Count < 5 ? "---" : diagnosticos[4].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();

                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnostico06 = diagnosticos.Count < 6 ? "---" : diagnosticos[5].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.FechaRegistro06 = diagnosticos.Count < 6 ? "---" : diagnosticos[5].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();

                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnostico07 = diagnosticos.Count < 7 ? "---" : diagnosticos[6].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.FechaRegistro07 = diagnosticos.Count < 7 ? "---" : diagnosticos[6].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();
                        
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnostico08 = diagnosticos.Count < 8 ? "---" : diagnosticos[7].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.FechaRegistro08 = diagnosticos.Count < 8 ? "---" : diagnosticos[7].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();

                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnostico09 = diagnosticos.Count < 9 ? "---" : diagnosticos[8].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.FechaRegistro09 = diagnosticos.Count < 9 ? "---" : diagnosticos[8].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();
                        
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnostico10 = diagnosticos.Count < 10 ? "---" : diagnosticos[9].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.FechaRegistro10 = diagnosticos.Count < 10 ? "---" : diagnosticos[9].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();

                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnostico11 = diagnosticos.Count < 11 ? "---" : diagnosticos[10].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.FechaRegistro11 = diagnosticos.Count < 11 ? "---" : diagnosticos[10].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();
                        
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnostico12 = diagnosticos.Count < 12 ? "---" : diagnosticos[11].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.FechaRegistro12 = diagnosticos.Count < 12 ? "---" : diagnosticos[11].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();

                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnostico13 = diagnosticos.Count < 13 ? "---" : diagnosticos[12].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.FechaRegistro13 = diagnosticos.Count < 13 ? "---" : diagnosticos[12].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();
                        
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnostico14 = diagnosticos.Count < 14 ? "---" : diagnosticos[13].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.FechaRegistro14 = diagnosticos.Count < 14 ? "---" : diagnosticos[13].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();

                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnostico15 = diagnosticos.Count < 15 ? "---" : diagnosticos[14].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.FechaRegistro15 = diagnosticos.Count < 15 ? "---" : diagnosticos[14].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();
                        
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnostico16 = diagnosticos.Count < 16 ? "---" : diagnosticos[15].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.FechaRegistro16 = diagnosticos.Count < 16 ? "---" : diagnosticos[15].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();

                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnostico17 = diagnosticos.Count < 17 ? "---" : diagnosticos[16].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.FechaRegistro17 = diagnosticos.Count < 17 ? "---" : diagnosticos[16].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();
                        
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnostico18 = diagnosticos.Count < 18 ? "---" : diagnosticos[17].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.FechaRegistro18 = diagnosticos.Count < 18 ? "---" : diagnosticos[17].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();

                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnostico19 = diagnosticos.Count < 19 ? "---" : diagnosticos[18].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Fecha19 = diagnosticos.Count < 19 ? "---" : diagnosticos[18].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();
                        
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.Diagnostico20 = diagnosticos.Count < 20 ? "---" : diagnosticos[19].v_Name;
                        objDatosVigilanciaSalud.VigilanciaSalud.Diagnosticos.FechaRegistro20 = diagnosticos.Count < 20 ? "---" : diagnosticos[19].d_CreationDate == null ? "---" : diagnosticos[0].d_CreationDate.ToString();
    
	                    #endregion

                        #region Recomendaciones
		                    
                        var recomendaciones = oNatclarBL.DataRecomendaciones(serviceId);

                        objDatosVigilanciaSalud.VigilanciaSalud.Recomendaciones = new XmlRecomendaciones();
                        objDatosVigilanciaSalud.VigilanciaSalud.Recomendaciones.Recomendaciones01 = recomendaciones.Count < 1 ? "---" : recomendaciones[0].v_RecommendationName;
                        objDatosVigilanciaSalud.VigilanciaSalud.Recomendaciones.Recomendaciones02 = recomendaciones.Count < 2 ? "---" : recomendaciones[1].v_RecommendationName;
                        objDatosVigilanciaSalud.VigilanciaSalud.Recomendaciones.Recomendaciones03 = recomendaciones.Count < 3 ? "---" : recomendaciones[2].v_RecommendationName;
                        objDatosVigilanciaSalud.VigilanciaSalud.Recomendaciones.Recomendaciones04 = recomendaciones.Count < 4 ? "---" : recomendaciones[3].v_RecommendationName;
                        objDatosVigilanciaSalud.VigilanciaSalud.Recomendaciones.Recomendaciones05 = recomendaciones.Count < 5 ? "---" : recomendaciones[4].v_RecommendationName;
                        objDatosVigilanciaSalud.VigilanciaSalud.Recomendaciones.Recomendaciones06 = recomendaciones.Count < 6 ? "---" : recomendaciones[5].v_RecommendationName;
                        objDatosVigilanciaSalud.VigilanciaSalud.Recomendaciones.Recomendaciones07 = recomendaciones.Count < 7 ? "---" : recomendaciones[6].v_RecommendationName;
                        objDatosVigilanciaSalud.VigilanciaSalud.Recomendaciones.Recomendaciones08 = recomendaciones.Count < 8 ? "---" : recomendaciones[7].v_RecommendationName;
                        objDatosVigilanciaSalud.VigilanciaSalud.Recomendaciones.Recomendaciones09 = recomendaciones.Count < 9 ? "---" : recomendaciones[8].v_RecommendationName;
                        objDatosVigilanciaSalud.VigilanciaSalud.Recomendaciones.Recomendaciones10 = recomendaciones.Count < 10 ? "---" : recomendaciones[9].v_RecommendationName;

 
	                    #endregion

                        #region VigilanciaSalud
		                     
                        objDatosVigilanciaSalud.VigilanciaSalud.VigilanciaSalud = new XmlVigilanciSalud();

                        objDatosVigilanciaSalud.VigilanciaSalud.VigilanciaSalud.Calificacion = "---";
                        objDatosVigilanciaSalud.VigilanciaSalud.VigilanciaSalud.Dictamen = "---";
                        objDatosVigilanciaSalud.VigilanciaSalud.VigilanciaSalud.ObservacionesCalificacion = "---";
                        objDatosVigilanciaSalud.VigilanciaSalud.VigilanciaSalud.ObservacionesDictamen = "---";




	                    #endregion


                        LanzarDatos(client.EnviarDatosVigilanciaSalud(objDatosVigilanciaSalud), e, column, serviceId);

                        #endregion

                        
                        
                        break;
                    case "DocumentosImagenes":

                        var objDocsImgs = new EstructuraDatosDocseImagenes();

                        var docimgs = _ServiceBL.GetFilePdfsByServiceId(ref objOperationResult, serviceId);


                        datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento, datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);

                        #region DatosPaciente
                        objDocsImgs.DatosPaciente = new XmlDatosPaciente();

                        lError = "datosPaciente.Nombre";
                        objDocsImgs.DatosPaciente.Nombre = datosPaciente.Nombre;
                        lError = "datosPaciente.Dni";
                        objDocsImgs.DatosPaciente.DNI = datosPaciente.Dni;
                        lError = "datosPaciente.DepartamentoNacimiento ";
                        objDocsImgs.DatosPaciente.DepartamentoNacimiento = short.Parse(ubigeoPaciente.depar);
                        lError = "datosPaciente.Direccion";
                        objDocsImgs.DatosPaciente.Direccion = datosPaciente.Direccion;
                        lError = "datosPaciente.DistritoNacimiento";
                        objDocsImgs.DatosPaciente.DistritoNacimiento = short.Parse(ubigeoPaciente.distr);
                        lError = "datosPaciente.Email";
                        objDocsImgs.DatosPaciente.Email = datosPaciente.Email;
                        lError = "datosPaciente.EstadoCivil";
                        objDocsImgs.DatosPaciente.EstadoCivil = "S";
                        lError = "datosPaciente.FechaNacimiento";
                        objDocsImgs.DatosPaciente.FechaNacimiento = datosPaciente.FechaNacimiento;
                        lError = "datosPaciente.Hc";
                        objDocsImgs.DatosPaciente.HC = datosPaciente.Hc;
                        lError = "datosPaciente.PrimerApellido";
                        objDocsImgs.DatosPaciente.PrimerApellido = datosPaciente.PrimerApellido;
                        lError = "datosPaciente.SegundoApellido";
                        objDocsImgs.DatosPaciente.SegundoApellido = datosPaciente.SegundoApellido;
                        lError = "datosPaciente.ProvinciaNacimiento";
                        objDocsImgs.DatosPaciente.ProvinciaNacimiento = short.Parse(ubigeoPaciente.prov);
                        lError = "datosPaciente.ResidenciaActual";
                        objDocsImgs.DatosPaciente.ResidenciaActual = datosPaciente.ResidenciaActual;
                        lError = "datosPaciente.Sexo";
                        objDocsImgs.DatosPaciente.Sexo = "M";
                        lError = "datosPaciente.TipoDocumento";
                        objDocsImgs.DatosPaciente.TipoDocumento = short.Parse(datosPaciente.TipoDocumento.ToString());

                        #endregion

                        #region DatosExamen

                        objDocsImgs.DatosExamen = new XmlDatosExamen();

                        lError = "datosPaciente.IDActuacion";
                        objDocsImgs.DatosExamen.IDActuacion = datosPaciente.IDActuacion;
                        lError = "datosPaciente.IDCentro";
                        objDocsImgs.DatosExamen.IDCentro = datosPaciente.IDCentro;
                        lError = "datosPaciente.FechaRegistro";
                        objDocsImgs.DatosExamen.FechaRegistro = datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                        lError = "datosPaciente.IDEstado";
                        objDocsImgs.DatosExamen.IDEstado = datosPaciente.IDEstado.Value.ToString();
                        lError = "datosPaciente.IDEstructura";
                        objDocsImgs.DatosExamen.IDEstructura = datosPaciente.IDEstructura;
                        lError = "datosPaciente.IDExamen";
                        objDocsImgs.DatosExamen.IDExamen = datosPaciente.IDExamen;
                        lError = "datosPaciente.TipoExamen";
                        objDocsImgs.DatosExamen.TipoExamen = datosPaciente.TipoExamen.Value.ToString();

                        #endregion

                        foreach (var item in docimgs)
                        {
                            objDocsImgs.DocumentoseImagenes = new XmlDatosDocumentoseImagenes();
                            objDocsImgs.DocumentoseImagenes.Fecha = item.FechaServicio.Value.Date.ToString();
                            objDocsImgs.DocumentoseImagenes.Tipo = item.CategoryId.ToString();
                            objDocsImgs.DocumentoseImagenes.IdentificadorDocumento = item.v_MultimediaFileId;
                            objDocsImgs.DocumentoseImagenes.Titulo = item.v_FileName;
                            objDocsImgs.DocumentoseImagenes.Observaciones = "----";
                            objDocsImgs.DocumentoseImagenes.Codigo = "----";
                            
                            LanzarDatos(client.EnviarDatosDocumentoseImagenes(objDocsImgs), e, column, serviceId);
                            new GoogleDriveFilesRepository().SaveFileOnGoogleDrive(item.v_FileName, item.b_File, item.v_MultimediaFileId);
                        }

                        if (docimgs.Count == 0)
                        {
                            objDocsImgs.DocumentoseImagenes = new XmlDatosDocumentoseImagenes();
                            objDocsImgs.DocumentoseImagenes.Fecha = "---";
                            objDocsImgs.DocumentoseImagenes.Tipo = "---";
                            objDocsImgs.DocumentoseImagenes.IdentificadorDocumento = "---";
                            objDocsImgs.DocumentoseImagenes.Titulo = "---";
                            objDocsImgs.DocumentoseImagenes.Observaciones = "----";
                            objDocsImgs.DocumentoseImagenes.Codigo = "----";
                            

                            LanzarDatos(client.EnviarDatosDocumentoseImagenes(objDocsImgs), e, column, serviceId);
                        }

                        break;
                }

                #endregion

            }
            catch (Exception exception)
            {
                e.Cell.ButtonAppearance.Image = Resources.cog;
                MessageBox.Show(lError + "\n" + exception.Message, "Validación!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void LanzarDatos(Resultado resultado, CellEventArgs e, string column, string serviceId)
        {
            envionatclarDto oenvionatclarDto = new envionatclarDto();
            if (resultado.EstadoXML == "OK")
            {
                e.Cell.ButtonAppearance.Image = Resources.accept;
                oenvionatclarDto.v_ServiceId = serviceId;
                oenvionatclarDto.v_Paquete = column;
                oenvionatclarDto.i_EstadoId = (int)EstadoEnvioNatclar.Ok;

            }
            else
            {
                e.Cell.ButtonAppearance.Image = Resources.system_close;
                oenvionatclarDto.v_ServiceId = serviceId;
                oenvionatclarDto.v_Paquete = column;
                oenvionatclarDto.i_EstadoId = (int)EstadoEnvioNatclar.Fail;
            }


            oOperationsNatclarBl.GrabarEnvio(oenvionatclarDto, Globals.ClientSession.GetAsList());

        }

        private void FormNatclar_Load(object sender, EventArgs e)
        {
            UltraGridColumn c = grdDataService.DisplayLayout.Bands[0].Columns["b_select"];
            c.CellActivation = Activation.AllowEdit;
            c.CellClickAction = CellClickAction.Edit;

            dtpDateTimeStar.Focus();
        }

        private void grdDataService_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
        }

        private void grdDataService_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            var serviceId = e.Row.Cells["v_ServiceId"].Value.ToString();
            
            
            var findList = data.FindAll(x => x.v_ServiceId == serviceId).ToList();


            foreach (var itemServ in findList)
            {
                for (int i = 5; i < e.Row.Cells.Count; i++)
                {
                    var paquete = e.Row.Cells[i].Column.Key;
                    //if (paquete.Column.Key != "v_Pacient" && paquete.Column.Key != "v_ServiceId" &&
                    //    paquete.Column.Key != "d_ServiceDate")
                    //{

                    if (itemServ.v_Paquete == paquete)
                    {
                        evaluarCelda(itemServ.v_Paquete, itemServ.i_EstadoId.ToString(), e);
                    }
                    //ERROR EN PAQUETE NULL

                    //var x = data.Find(p => p.v_ServiceId == serviceId);
                    //if (x.v_Paquete == null)
                    //{
                    //    evaluarCelda(paquete, "null", e);
                    //}
                    //else
                    //{
                    //    var estado = x.i_EstadoId;
                    //    paquete = data.Find(p => p.i_EstadoId == estado && p.v_ServiceId == serviceId).v_Paquete;
                    //    //data.Find(p => p.v_ServiceId == serviceId && p.v_Paquete.ToString() == paquete.ToString())
                    //    //    .i_EstadoId == null
                    //    //    ? ""
                    //    //    : data.Find(p => p.v_ServiceId == serviceId && p.v_Paquete.ToString() == paquete.ToString())
                    //    //        .i_EstadoId.ToString();
                    //    evaluarCelda(paquete, estado.ToString(), e);
                    //}

                    //}
                    //else
                    //{
                    //    return;
                    //}
                }
            }
            





            #region ...
            //if (paquete.Column.Key == "DatosPersonales")
            //{
            //    evaluarCelda(paquete.Column.Key, estado,e);

            //}
            //else if (paquete.Column.Key == "DatosApMedicos")
            //{

            //}
            //else if (paquete.Column.Key == "DatosApQuirurgicos")
            //{

            //}
            //else if (paquete.Column.Key == "DatosExamen")
            //{

            //}
            //else if (paquete.Column.Key == "DatosAudiometria")
            //{

            //}
            //else if (paquete.Column.Key == "DatosConstantes")
            //{

            //}
            //else if (paquete.Column.Key == "DatosEpisodios")
            //{

            //}
            //else if (paquete.Column.Key == "DatosExamenFisico")
            //{

            //}
            //else if (paquete.Column.Key == "DatosExamenOcularVB")
            //{

            //}
            //else if (paquete.Column.Key == "DatosHabitosToxicos")
            //{

            //}
            //else if (paquete.Column.Key == "DatosHistoriaFamiliar")
            //{

            //}
            //else if (paquete.Column.Key == "DatosHistoriaFisiologico")
            //{

            //}
            //else if (paquete.Column.Key == "DatosHistoriaLaboral")
            //{

            //}
            //else if (paquete.Column.Key == "DatosHistoriaLaboralRl")
            //{

            //}
            //else if (paquete.Column.Key == "DatosMedicaciones")
            //{

            //}
            //else if (paquete.Column.Key == "DatosVacunas")
            //{

            //}
            //else if (paquete.Column.Key == "DatosVigilanciaSalus")
            //{

            //}
            //else if (paquete.Column.Key == "DatosLecturaOIT")
            //{

            //}
            //else if (paquete.Column.Key == "DatosLecturaPsicologia")
            //{

            //}
            //else if (paquete.Column.Key == "DatosAnalitica")
            //{

            //}


            #endregion
        }

        private void evaluarCelda(string p, string estado, InitializeRowEventArgs e)
        {
            //e.Cell.ButtonAppearance.Image = Resources.accept;
            if (estado == "null")
                e.Row.Cells[p].ButtonAppearance.Image = Resources.cog;
            else if (estado == "1")
                e.Row.Cells[p].ButtonAppearance.Image = Resources.accept;
            else
                e.Row.Cells[p].ButtonAppearance.Image = Resources.system_close;
        }

        private void btnEnviarBloque_Click(object sender, EventArgs e)
        {
            var arry = new List<string>();
            foreach (var item in grdDataService.Rows)
            {
                if ((bool)item.Cells["b_select"].Value)
                {
                    string serviceId = item.Cells["v_ServiceId"].Value.ToString();

                    arry.Add(serviceId);
                }
            }

        }

        private XmlAudiometria objAudiometriaValues(List<ServiceComponentFieldValuesList> data)
        {

            var objAud = new XmlAudiometria();
            foreach (var item in data)
            {

                #region ComponentsFieldAudiometri

                switch (item.v_ComponentFieldId)
                {
                    #region Tonal AÉREA no enmascarada OD

                    case Constants.txt_VA_OD_125:

                        objAud.ATAND125 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OD_250:

                        objAud.ATAND250 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OD_500:

                        objAud.ATAND500 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OD_1000:

                        objAud.ATAND1000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OD_2000:

                        objAud.ATAND2000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OD_3000:

                        objAud.ATAND3000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OD_4000:

                        objAud.ATAND4000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OD_6000:

                        objAud.ATAND6000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OD_8000:

                        objAud.ATAND8000 = item.v_Value1;

                        break;

                    #endregion

                    #region Tonal AÉREA no enmascarada OI

                    case Constants.txt_VA_OI_125:

                        objAud.ATANI125 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OI_250:

                        objAud.ATANI250 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OI_500:

                        objAud.ATANI500 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OI_1000:

                        objAud.ATANI1000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OI_2000:

                        objAud.ATANI2000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OI_3000:

                        objAud.ATANI3000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OI_4000:

                        objAud.ATANI4000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OI_6000:

                        objAud.ATANI6000 = item.v_Value1;

                        break;
                    case Constants.txt_VA_OI_8000:

                        objAud.ATANI8000 = item.v_Value1;

                        break;

                    #endregion

                    #region Tonal AÉREA enmascarada OD

                    case Constants.txt_EM_OD_125:

                        objAud.ATAED125 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OD_250:

                        objAud.ATAED250 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OD_500:

                        objAud.ATAED500 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OD_1000:

                        objAud.ATAED1000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OD_2000:

                        objAud.ATAED2000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OD_3000:

                        objAud.ATAED3000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OD_4000:

                        objAud.ATAED4000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OD_6000:

                        objAud.ATAED6000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OD_8000:

                        objAud.ATAED8000 = item.v_Value1;

                        break;

                    #endregion

                    #region Tonal AÉREA enmascarada OI

                    case Constants.txt_EM_OI_125:

                        objAud.ATAEI125 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OI_250:

                        objAud.ATAEI250 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OI_500:

                        objAud.ATAEI500 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OI_1000:

                        objAud.ATAEI1000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OI_2000:

                        objAud.ATAEI2000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OI_3000:

                        objAud.ATAEI3000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OI_4000:

                        objAud.ATAEI4000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OI_6000:

                        objAud.ATAEI6000 = item.v_Value1;

                        break;
                    case Constants.txt_EM_OI_8000:

                        objAud.ATAEI8000 = item.v_Value1;

                        break;

                    #endregion

                    #region Tonal OSEA no enmascarada OD

                    case Constants.txt_VO_OD_125:

                        objAud.ATOND125 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OD_250:

                        objAud.ATOND250 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OD_500:

                        objAud.ATOND500 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OD_1000:

                        objAud.ATOND1000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OD_2000:

                        objAud.ATOND2000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OD_3000:

                        objAud.ATOND3000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OD_4000:

                        objAud.ATOND4000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OD_6000:

                        objAud.ATOND6000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OD_8000:

                        objAud.ATOND8000 = item.v_Value1;

                        break;

                    #endregion

                    #region Tonal OSEA no enmascarada OI

                    case Constants.txt_VO_OI_125:

                        objAud.ATONI125 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OI_250:

                        objAud.ATONI250 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OI_500:

                        objAud.ATONI500 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OI_1000:

                        objAud.ATONI1000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OI_2000:

                        objAud.ATONI2000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OI_3000:

                        objAud.ATONI3000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OI_4000:

                        objAud.ATONI4000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OI_6000:

                        objAud.ATONI6000 = item.v_Value1;

                        break;
                    case Constants.txt_VO_OI_8000:

                        objAud.ATONI8000 = item.v_Value1;

                        break;

                    #endregion

                    #region Descripcion - Observaciones OD-OI

                    case Constants.txt_AUD_DX_OCUPACIONAL_AUTO_OD:

                        objAud.DESCATOTO = item.v_Value1;

                        break;
                    case Constants.txt_AUD_DX_OCUPACIONAL_AUTO_OI:

                        objAud.DESCATOTOI = item.v_Value1;

                        break;
                    case Constants.txt_AUD_DX_CLINICO_AUTO_OD:

                        objAud.OBSATOTOD = item.v_Value1;

                        break;
                    case Constants.txt_AUD_DX_CLINICO_AUTO_OI:

                        objAud.OBSATOTOI = item.v_Value1;

                        break;

                    #endregion

                }

                #endregion

            }
            return objAud;
        }

    }
}
