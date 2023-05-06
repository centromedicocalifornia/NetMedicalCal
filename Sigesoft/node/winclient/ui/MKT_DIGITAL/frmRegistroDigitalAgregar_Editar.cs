using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI.MKT_DIGITAL
{
    public partial class frmRegistroDigitalAgregar_Editar : Form
    {
        private DigitalContactCenter DigitalContactCenterObj;
        PacientBL _objPacientBL = new PacientBL();
        DigitalContactCenterBL _DigitalContactCenterBL = new DigitalContactCenterBL();
        personDto _PersonObjEditar = new personDto();

        private string Estado;

        public frmRegistroDigitalAgregar_Editar(DigitalContactCenter _DigitalContactCenterObj, string _Estado)
        {
            DigitalContactCenterObj = _DigitalContactCenterObj;
            Estado = _Estado;

            InitializeComponent();
        }

        private void frmRegistroDigitalAgregar_Editar_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();


            Utils.LoadDropDownList(cboTipoDoc, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 106, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cboMedioMkt, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 413, null), DropDownListAction.Select);
            //Utils.LoadDropDownList(cboMedioMkt, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 98, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cboSexo, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 100, null), DropDownListAction.Select);

            Utils.LoadDropDownList(cboProtocolo, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForComboDCC(ref objOperationResult, "N009-OO000000052", "N009-OL000000417", null), DropDownListAction.All);
            Utils.LoadDropDownList(cboEstadoAtencion, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 95, null), DropDownListAction.Select);

            if (Estado == "Editar")
            {
                _PersonObjEditar = _objPacientBL.GetPersonDigContCent(ref objOperationResult, DigitalContactCenterObj.DOC);


                txtIdContactCenter.Text = DigitalContactCenterObj.ID_DCC;
                txtIdPerson.Text = DigitalContactCenterObj.ID_Person;
                cboTipoDoc.SelectedValue = DigitalContactCenterObj.TIPO_DOC_ID.ToString();
                txtN_Doc.Text = DigitalContactCenterObj.DOC;
                txtNombres.Text = DigitalContactCenterObj.NOMBRES;
                txtApe_Paterno.Text = DigitalContactCenterObj.AP_PATERNO;
                txtApe_Materno.Text = DigitalContactCenterObj.AP_MATERNO;
                txtCelular.Text = DigitalContactCenterObj.CELULAR;
                txtEmail.Text = DigitalContactCenterObj.EMAIL;
                txtDireccion.Text = DigitalContactCenterObj.DIRECCION;
                dtFechaNacimiento.Value = (DateTime)DigitalContactCenterObj.FECHA_NACIMIENTO;
                cboSexo.SelectedValue = DigitalContactCenterObj.SEXO.ToString();

                txtDireccion.Text = DigitalContactCenterObj.DIRECCION;
                cboMedioMkt.SelectedValue = DigitalContactCenterObj.MEDIO_MKT_ID.ToString();
                dtFechaCita.Value = (DateTime)DigitalContactCenterObj.FECHA_CITA;
                txtServicioID.Text = DigitalContactCenterObj.SERVICIO_ENLAZADO == null ? "" : DigitalContactCenterObj.SERVICIO_ENLAZADO;
                cboProtocolo.SelectedValue = DigitalContactCenterObj.PROTOCOL_ID;
                txtMotivo.Text = DigitalContactCenterObj.MOTIVO;
                if (DigitalContactCenterObj.METODO_PAGO_ID == 1)
                {
                    rbDeposito.Checked = true;
                }
                else if (DigitalContactCenterObj.METODO_PAGO_ID == 2)
                {
                    rbVisa.Checked = true;
                }
                else if (DigitalContactCenterObj.METODO_PAGO_ID == 3)
                {
                    rbEfectivo.Checked = true;
                }
                else if (DigitalContactCenterObj.METODO_PAGO_ID == 4)
                {
                    rbPaginaWeb.Checked = true;
                }

                cboEstadoAtencion.SelectedValue = DigitalContactCenterObj.ESTADO_DCC_ID.ToString();
                lblReAgendado.Text = DigitalContactCenterObj.DCCIdReAgenda;
                lblComentarios.Text = DigitalContactCenterObj.COMENTARIOS;
            }
            else if (Estado == "ReProgramar")
            {
                 _PersonObjEditar = _objPacientBL.GetPersonDigContCent(ref objOperationResult, DigitalContactCenterObj.DOC);


                //txtIdContactCenter.Text = DigitalContactCenterObj.ID_DCC;
                txtIdPerson.Text = DigitalContactCenterObj.ID_Person;
                cboTipoDoc.SelectedValue = DigitalContactCenterObj.TIPO_DOC_ID.ToString();
                txtN_Doc.Text = DigitalContactCenterObj.DOC;
                txtNombres.Text = DigitalContactCenterObj.NOMBRES;
                txtApe_Paterno.Text = DigitalContactCenterObj.AP_PATERNO;
                txtApe_Materno.Text = DigitalContactCenterObj.AP_MATERNO;
                txtCelular.Text = DigitalContactCenterObj.CELULAR;
                txtEmail.Text = DigitalContactCenterObj.EMAIL;
                txtDireccion.Text = DigitalContactCenterObj.DIRECCION;
                dtFechaNacimiento.Value = (DateTime)DigitalContactCenterObj.FECHA_NACIMIENTO;
                cboSexo.SelectedValue = DigitalContactCenterObj.SEXO.ToString();

                txtDireccion.Text = DigitalContactCenterObj.DIRECCION;
                cboMedioMkt.SelectedValue = DigitalContactCenterObj.MEDIO_MKT_ID.ToString();
                dtFechaCita.Value = (DateTime)DigitalContactCenterObj.FECHA_CITA;
                txtServicioID.Text = DigitalContactCenterObj.SERVICIO_ENLAZADO == null ? "" : DigitalContactCenterObj.SERVICIO_ENLAZADO;
                cboProtocolo.SelectedValue = DigitalContactCenterObj.PROTOCOL_ID;
                txtMotivo.Text = DigitalContactCenterObj.MOTIVO;
                if (DigitalContactCenterObj.METODO_PAGO_ID == 1)
                {
                    rbDeposito.Checked = true;
                }
                else if (DigitalContactCenterObj.METODO_PAGO_ID == 2)
                {
                    rbVisa.Checked = true;
                }
                else if (DigitalContactCenterObj.METODO_PAGO_ID == 3)
                {
                    rbEfectivo.Checked = true;
                }
                else if (DigitalContactCenterObj.METODO_PAGO_ID == 4)
                {
                    rbPaginaWeb.Checked = true;
                }

                cboEstadoAtencion.SelectedValue = "1";
                lblReAgendado.Text = DigitalContactCenterObj.DCCIdReAgenda;
                //lblComentarios.Text = DigitalContactCenterObj.COMENTARIOS;
            }
         
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            string Result = "";

            #region Validaciones

            if (txtNombres.Text.Trim() == "")
            {
                MessageBox.Show("Por favor ingrese un nombre apropiado para Nombres.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtApe_Paterno.Text.Trim() == "")
            {
                MessageBox.Show("Por favor ingrese un nombre apropiado para Apellido Paterno.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtApe_Paterno.Text.Trim() == "")
            {
                MessageBox.Show("Por favor ingrese un nombre apropiado para Apellido Materno.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtDireccion.Text.Trim() == "")
            {
                MessageBox.Show("Por favor ingrese una dirección apropiado.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } 

            if (txtN_Doc.Text.Trim() == "")
            {
                MessageBox.Show("Por favor ingrese un nombre apropiado para Número Documento.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtEmail.Text.Trim() != "")
            {

                if (!Sigesoft.Common.Utils.email_bien_escrito(txtEmail.Text.Trim()))
                {
                    MessageBox.Show("Por favor ingrese un Email con formato correcto.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            int caracteres = txtN_Doc.TextLength;
            if (int.Parse(cboTipoDoc.SelectedValue.ToString()) == (int)Common.Document.DNI)
            {
                if (caracteres != 8)
                {
                    MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (int.Parse(cboTipoDoc.SelectedValue.ToString()) == (int)Common.Document.PASAPORTE)
            {
                if (caracteres != 9)
                {
                    MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (int.Parse(cboTipoDoc.SelectedValue.ToString()) == (int)Common.Document.LICENCIACONDUCIR)
            {
                if (caracteres != 9)
                {
                    MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (int.Parse(cboTipoDoc.SelectedValue.ToString()) == (int)Common.Document.CARNETEXTRANJ)
            {
                if (caracteres < 9)
                {
                    MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (int.Parse(cboMedioMkt.SelectedValue.ToString()) == -1)
            {
                MessageBox.Show("Seleccione medio MKT.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cboProtocolo.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show("Seleccione protocolo a Atender", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (txtMotivo.Text.Trim() == "")
            {
                MessageBox.Show("Por favor ingrese motivo de atención y anotaciones adicionales.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int metPago = 0;
            if (rbDeposito.Checked == true)
            {
                metPago = 1;
            }
            else if (rbVisa.Checked == true)
            {
                metPago = 1;
            }
            else if (rbEfectivo.Checked == true)
            {
                metPago = 1;
            }
            else if (rbPaginaWeb.Checked == true)
            {
                metPago = 1;
            }

            if (int.Parse(cboEstadoAtencion.SelectedValue.ToString()) == -1)
            {
                MessageBox.Show("Seleccione estado de Atención.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion


            if (Estado == "Nuevo" || Estado == "ReProgramar")
            {


                string NEWIdPerson = "";

                if (txtIdPerson.Text == "")
                {
                    personDto _PersonObjNew = new personDto();
                    //_PersonObjNew.v_PersonId = txtIdPerson.Text;

                    _PersonObjNew.i_DocTypeId = int.Parse(cboTipoDoc.SelectedValue.ToString());
                    _PersonObjNew.v_DocNumber = txtN_Doc.Text;
                    _PersonObjNew.v_FirstName = txtNombres.Text;
                    _PersonObjNew.v_FirstLastName = txtApe_Paterno.Text;
                    _PersonObjNew.v_SecondLastName = txtApe_Materno.Text;
                    _PersonObjNew.i_SexTypeId = int.Parse(cboSexo.SelectedValue.ToString());
                    _PersonObjNew.d_Birthdate = dtFechaNacimiento.Value;
                    _PersonObjNew.v_TelephoneNumber = txtCelular.Text;
                    _PersonObjNew.v_Mail = txtEmail.Text;
                    _PersonObjNew.v_AdressLocation = txtDireccion.Text;
                    _PersonObjNew.i_MaritalStatusId = -1;
                    _PersonObjNew.i_LevelOfId = -1;
                    _PersonObjNew.v_ContactName = "";
                    _PersonObjNew.v_EmergencyPhone = "";
                    _PersonObjNew.i_BloodFactorId = -1;
                    _PersonObjNew.i_BloodGroupId = -1;
                    _PersonObjNew.v_CurrentOccupation = "";
                    _PersonObjNew.i_DepartmentId = 609;
                    _PersonObjNew.i_ProvinceId = 610;
                    _PersonObjNew.i_DistrictId = 609;
                    _PersonObjNew.i_ResidenceInWorkplaceId = 0;
                    _PersonObjNew.v_ResidenceTimeInWorkplace = "";
                    _PersonObjNew.i_TypeOfInsuranceId = 1;
                    _PersonObjNew.i_NumberLivingChildren = 0;
                    _PersonObjNew.i_NumberDependentChildren = 0;
                    _PersonObjNew.v_OwnerName = "";
                    _PersonObjNew.i_IsDeleted = 0;
                    _PersonObjNew.i_InsertUserId = DigitalContactCenterObj.ID_INSERT_USER;
                    _PersonObjNew.d_InsertDate = DigitalContactCenterObj.FECHA_INGRESO;
                    _PersonObjNew.i_Relationship = -1;
                    _PersonObjNew.v_ExploitedMineral = "- - -";
                    _PersonObjNew.i_AltitudeWorkId = 1;
                    _PersonObjNew.i_PlaceWorkId = 1;
                    _PersonObjNew.i_NroHermanos = 0;
                    _PersonObjNew.v_Religion = "CATOLICO";
                    _PersonObjNew.v_Nacionalidad = "PERUANO";
                    _PersonObjNew.v_ResidenciaAnterior = "CAJAMARCA";
                    _PersonObjNew.i_EtniaRaza = 0;
                    _PersonObjNew.i_Migrante = 0;
                    _PersonObjNew.v_Migrante = "PERU";

                    NEWIdPerson = _objPacientBL.AddPacient(ref objOperationResult, _PersonObjNew, Globals.ClientSession.GetAsList());
                    txtIdPerson.Text = NEWIdPerson;
                    if (NEWIdPerson == "")
                    {
                        MessageBox.Show("VERIFIQUE QUE LOS DATOS DEL PACIENTE SEAN CORRECTOS.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (NEWIdPerson == "-1")
                    {
                        MessageBox.Show("FAVOR DE BUSCAR AL PACIENTE EN LISTA DE PACIENTES (BOTON LUPA)", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    //personDto _PersonObjNew = new personDto();
                    _PersonObjEditar.v_PersonId = txtIdPerson.Text;

                    _PersonObjEditar.i_DocTypeId = int.Parse(cboTipoDoc.SelectedValue.ToString());
                    _PersonObjEditar.v_DocNumber = txtN_Doc.Text;
                    _PersonObjEditar.v_FirstName = txtNombres.Text;
                    _PersonObjEditar.v_FirstLastName = txtApe_Paterno.Text;
                    _PersonObjEditar.v_SecondLastName = txtApe_Materno.Text;
                    _PersonObjEditar.i_SexTypeId = int.Parse(cboSexo.SelectedValue.ToString());
                    _PersonObjEditar.d_Birthdate = dtFechaNacimiento.Value;
                    _PersonObjEditar.v_TelephoneNumber = txtCelular.Text;
                    _PersonObjEditar.v_Mail = txtEmail.Text;
                    _PersonObjEditar.v_AdressLocation = txtDireccion.Text;
                    _PersonObjEditar.i_UpdateUserId = DigitalContactCenterObj.ID_INSERT_USER;
                    _PersonObjEditar.d_UpdateDate = DigitalContactCenterObj.FECHA_INGRESO;

                    var guardado = _objPacientBL.UpdatePacient(ref objOperationResult, _PersonObjEditar, Globals.ClientSession.GetAsList(), txtN_Doc.Text, txtN_Doc.Text);

                    if (guardado != "1")
                    {
                        MessageBox.Show("VERIFIQUE QUE LOS DATOS DEL PACIENTE SEAN CORRECTOS.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (guardado == "-1")
                    {
                        MessageBox.Show("FAVOR DE BUSCAR AL PACIENTE EN LISTA DE PACIENTES (BOTON LUPA)", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                

                //_DigitalContactCenterObjNew.ID_DCC = txtIdContactCenter.Text;
                //_DigitalContactCenterObjNew.ID_Person = txtIdContactCenter.Text;
                string digitalcontactcenterId = "";

                digitalcontactcenterDto _DigitalContactCenterObjNew = new digitalcontactcenterDto();

                _DigitalContactCenterObjNew.v_PersonId = txtIdPerson.Text;
                
                _DigitalContactCenterObjNew.i_MedioMkt = int.Parse(cboMedioMkt.SelectedValue.ToString());
                _DigitalContactCenterObjNew.d_FechaCita = dtFechaCita.Value;
                _DigitalContactCenterObjNew.v_ServiceId = txtServicioID.Text;

                _DigitalContactCenterObjNew.v_ProtocolId = cboProtocolo.SelectedValue.ToString();
                _DigitalContactCenterObjNew.v_MotivoConsulta = txtMotivo.Text;
                
                if (rbDeposito.Checked == true)
                {
                    _DigitalContactCenterObjNew.i_MetodoPago = 1;
                }
                else if (rbVisa.Checked == true)
                {
                    _DigitalContactCenterObjNew.i_MetodoPago = 2;
                }
                else if (rbEfectivo.Checked == true)
                {
                    _DigitalContactCenterObjNew.i_MetodoPago = 3;
                }
                else if (rbPaginaWeb.Checked == true)
                {
                    _DigitalContactCenterObjNew.i_MetodoPago = 4;
                }

                _DigitalContactCenterObjNew.i_EstadoAtencion = int.Parse(cboEstadoAtencion.SelectedValue.ToString());
                _DigitalContactCenterObjNew.v_DCCIdReAgenda = lblReAgendado.Text;
                _DigitalContactCenterObjNew.v_Comentarios = lblComentarios.Text;


                digitalcontactcenterId = _DigitalContactCenterBL.AddDigitalContactCenter(ref objOperationResult, _DigitalContactCenterObjNew, Globals.ClientSession.GetAsList());


                if (digitalcontactcenterId != String.Empty)
                {
                    MessageBox.Show("Se grabó correctamente.", "! INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else  
                {
                    MessageBox.Show("Verifica a detalle los datos ingresados.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else if (Estado == "Editar")
            {
                _PersonObjEditar.v_PersonId = txtIdPerson.Text;

                _PersonObjEditar.i_DocTypeId = int.Parse(cboTipoDoc.SelectedValue.ToString());
                _PersonObjEditar.v_DocNumber = txtN_Doc.Text;
                _PersonObjEditar.v_FirstName = txtNombres.Text;
                _PersonObjEditar.v_FirstLastName = txtApe_Paterno.Text;
                _PersonObjEditar.v_SecondLastName = txtApe_Materno.Text;
                _PersonObjEditar.i_SexTypeId = int.Parse(cboSexo.SelectedValue.ToString());
                _PersonObjEditar.d_Birthdate = dtFechaNacimiento.Value;
                _PersonObjEditar.v_TelephoneNumber = txtCelular.Text;
                _PersonObjEditar.v_Mail = txtEmail.Text;
                _PersonObjEditar.v_AdressLocation = txtDireccion.Text;
                _PersonObjEditar.i_UpdateUserId = DigitalContactCenterObj.ID_INSERT_USER;
                _PersonObjEditar.d_UpdateDate = DigitalContactCenterObj.FECHA_INGRESO;
              
                var guardado = _objPacientBL.UpdatePacient(ref objOperationResult, _PersonObjEditar, Globals.ClientSession.GetAsList(), txtN_Doc.Text, txtN_Doc.Text);

                if (guardado != "1")
                {
                    MessageBox.Show("VERIFIQUE QUE LOS DATOS DEL PACIENTE SEAN CORRECTOS.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (guardado == "-1")
                {
                    MessageBox.Show("FAVOR DE BUSCAR AL PACIENTE EN LISTA DE PACIENTES (BOTON LUPA)", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                digitalcontactcenterDto _DigitalContactCenterObjEdit = new digitalcontactcenterDto();

                _DigitalContactCenterObjEdit = _DigitalContactCenterBL.GetPersonDigContCent(ref objOperationResult, txtIdContactCenter.Text);


                _DigitalContactCenterObjEdit.v_DigitalContactCenterId = txtIdContactCenter.Text;
                _DigitalContactCenterObjEdit.v_PersonId = txtIdPerson.Text;

                _DigitalContactCenterObjEdit.i_MedioMkt = int.Parse(cboMedioMkt.SelectedValue.ToString());
                _DigitalContactCenterObjEdit.d_FechaCita = dtFechaCita.Value;
                _DigitalContactCenterObjEdit.v_ServiceId = txtServicioID.Text;

                _DigitalContactCenterObjEdit.v_ProtocolId = cboProtocolo.SelectedValue.ToString();
                _DigitalContactCenterObjEdit.v_MotivoConsulta = txtMotivo.Text;

                if (rbDeposito.Checked == true)
                {
                    _DigitalContactCenterObjEdit.i_MetodoPago = 1;
                }
                else if (rbVisa.Checked == true)
                {
                    _DigitalContactCenterObjEdit.i_MetodoPago = 2;
                }
                else if (rbEfectivo.Checked == true)
                {
                    _DigitalContactCenterObjEdit.i_MetodoPago = 3;
                }
                else if (rbPaginaWeb.Checked == true)
                {
                    _DigitalContactCenterObjEdit.i_MetodoPago = 4;
                }

                _DigitalContactCenterObjEdit.i_EstadoAtencion = int.Parse(cboEstadoAtencion.SelectedValue.ToString());
                _DigitalContactCenterObjEdit.v_DCCIdReAgenda = lblReAgendado.Text;
                _DigitalContactCenterObjEdit.v_Comentarios = lblComentarios.Text;
                
                var digitalcontactcenterId = _DigitalContactCenterBL.UpdateDigitalContactCenter(ref objOperationResult, _DigitalContactCenterObjEdit, Globals.ClientSession.GetAsList());


                if (objOperationResult.Success == 1)
                {
                    MessageBox.Show("Se grabó correctamente.", "! INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Verifica a detalle los datos ingresados.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
        }

        private void btnBuscarPersona_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();


            frmListarPacientes frm = new frmListarPacientes();
            frm.ShowDialog();
            txtIdPerson.Text = frm.PacienteId;

            if (txtIdPerson.Text != String.Empty)
            {
                _PersonObjEditar = _objPacientBL.GetPersonDigContCent(ref objOperationResult, txtIdPerson.Text);

                txtIdPerson.Text = _PersonObjEditar.v_PersonId;
                cboTipoDoc.SelectedValue = _PersonObjEditar.i_DocTypeId.ToString();
                txtN_Doc.Text = _PersonObjEditar.v_DocNumber;
                txtNombres.Text = _PersonObjEditar.v_FirstName;
                txtApe_Paterno.Text = _PersonObjEditar.v_FirstLastName;
                txtApe_Materno.Text = _PersonObjEditar.v_SecondLastName;
                txtCelular.Text = _PersonObjEditar.v_TelephoneNumber;
                txtEmail.Text = _PersonObjEditar.v_Mail;
                txtDireccion.Text = _PersonObjEditar.v_AdressLocation;
                dtFechaNacimiento.Value = _PersonObjEditar.d_Birthdate == null ? DateTime.Now : _PersonObjEditar.d_Birthdate.Value;
                cboSexo.SelectedValue = _PersonObjEditar.i_SexTypeId.ToString();


            }
        }
    }
}
