using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BE.Custom;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.MKT_DIGITAL
{
    public partial class frmRegistroDigital : Form
    {
        List<DigitalContactCenter> ListaGrilla = new List<DigitalContactCenter>();
        DigitalContactCenterBL digitalContactBl = new DigitalContactCenterBL(); 
        public frmRegistroDigital()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            DigitalContactCenter _DigitalContactCenterobj = new DigitalContactCenter();

            _DigitalContactCenterobj.ID_INSERT_USER = Globals.ClientSession.i_SystemUserId;
            //string _DigitalContactCenterobj.INSERT_USER = grdData.Selected.Rows[0].Cells["INSERT_USER"].Value.ToString();
            _DigitalContactCenterobj.FECHA_INGRESO = DateTime.Now;

            var frm = new MKT_DIGITAL.frmRegistroDigitalAgregar_Editar(_DigitalContactCenterobj, "Nuevo");
            frm.ShowDialog();

            btnFilter_Click(null, null);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            DigitalContactCenter _DigitalContactCenterobj = new DigitalContactCenter();


            _DigitalContactCenterobj.ID_DCC = grdDataDigitalContactCenter.Selected.Rows[0].Cells["ID_DCC"].Value.ToString();
            _DigitalContactCenterobj.ID_Person = grdDataDigitalContactCenter.Selected.Rows[0].Cells["ID_Person"].Value.ToString();
            _DigitalContactCenterobj.TIPO_DOC_ID = int.Parse(grdDataDigitalContactCenter.Selected.Rows[0].Cells["TIPO_DOC_ID"].Value.ToString());
            _DigitalContactCenterobj.TIPO_DOC = grdDataDigitalContactCenter.Selected.Rows[0].Cells["TIPO_DOC"].Value.ToString();
            _DigitalContactCenterobj.DOC = grdDataDigitalContactCenter.Selected.Rows[0].Cells["DOC"].Value.ToString();
            _DigitalContactCenterobj.NOMBRES = grdDataDigitalContactCenter.Selected.Rows[0].Cells["NOMBRES"].Value.ToString();
            _DigitalContactCenterobj.AP_PATERNO = grdDataDigitalContactCenter.Selected.Rows[0].Cells["AP_PATERNO"].Value.ToString();
            _DigitalContactCenterobj.AP_MATERNO = grdDataDigitalContactCenter.Selected.Rows[0].Cells["AP_MATERNO"].Value.ToString();
            _DigitalContactCenterobj.CELULAR = grdDataDigitalContactCenter.Selected.Rows[0].Cells["CELULAR"].Value.ToString();
            _DigitalContactCenterobj.EMAIL = grdDataDigitalContactCenter.Selected.Rows[0].Cells["EMAIL"].Value.ToString();
            _DigitalContactCenterobj.DIRECCION = grdDataDigitalContactCenter.Selected.Rows[0].Cells["DIRECCION"].Value.ToString();
            _DigitalContactCenterobj.MEDIO_MKT_ID = int.Parse(grdDataDigitalContactCenter.Selected.Rows[0].Cells["MEDIO_MKT_ID"].Value.ToString());

            _DigitalContactCenterobj.FECHA_NACIMIENTO = DateTime.Parse(grdDataDigitalContactCenter.Selected.Rows[0].Cells["FECHA_NACIMIENTO"].Value.ToString());
            _DigitalContactCenterobj.SEXO = int.Parse(grdDataDigitalContactCenter.Selected.Rows[0].Cells["SEXO"].Value.ToString());
           
            //_DigitalContactCenterobj.MEDIO_MKT = grdDataDigitalContactCenter.Selected.Rows[0].Cells["MEDIO_MKT"].Value.ToString();
            _DigitalContactCenterobj.FECHA_CITA = DateTime.Parse(grdDataDigitalContactCenter.Selected.Rows[0].Cells["FECHA_CITA"].Value.ToString());
            _DigitalContactCenterobj.PROTOCOL_ID = grdDataDigitalContactCenter.Selected.Rows[0].Cells["PROTOCOL_ID"].Value.ToString();
            //_DigitalContactCenterobj.PROTOCOL_NAME = grdDataDigitalContactCenter.Selected.Rows[0].Cells["PROTOCOL_NAME"].Value.ToString();
            _DigitalContactCenterobj.METODO_PAGO_ID = int.Parse(grdDataDigitalContactCenter.Selected.Rows[0].Cells["METODO_PAGO_ID"].Value.ToString());
            _DigitalContactCenterobj.MOTIVO = grdDataDigitalContactCenter.Selected.Rows[0].Cells["MOTIVO"].Value.ToString();
            //_DigitalContactCenterobj.METODO_PAGO = grdDataDigitalContactCenter.Selected.Rows[0].Cells["METODO_PAGO"].Value.ToString();
            _DigitalContactCenterobj.ESTADO_DCC_ID = int.Parse(grdDataDigitalContactCenter.Selected.Rows[0].Cells["ESTADO_DCC_ID"].Value.ToString());
            //_DigitalContactCenterobj.ESTADO_DCC = grdDataDigitalContactCenter.Selected.Rows[0].Cells["ESTADO_DCC"].Value.ToString();
            //_DigitalContactCenterobj.COMPROBANTE_ADJUNTO = grdDataDigitalContactCenter.Selected.Rows[0].Cells["COMPROBANTE_ADJUNTO"].Value.ToString();
            _DigitalContactCenterobj.SERVICIO_ENLAZADO = grdDataDigitalContactCenter.Selected.Rows[0].Cells["SERVICIO_ENLAZADO"].Value.ToString();
            _DigitalContactCenterobj.DCCIdReAgenda = grdDataDigitalContactCenter.Selected.Rows[0].Cells["DCCIdReAgenda"].Value.ToString();
            _DigitalContactCenterobj.COMENTARIOS = grdDataDigitalContactCenter.Selected.Rows[0].Cells["COMENTARIOS"].Value.ToString();

            //_DigitalContactCenterobj.MOTIVO_ELIMINACION = grdDataDigitalContactCenter.Selected.Rows[0].Cells["MOTIVO_ELIMINACION"].Value.ToString();
            //_DigitalContactCenterobj.ELIMINADO = grdDataDigitalContactCenter.Selected.Rows[0].Cells["ELIMINADO"].Value.ToString();
            //_DigitalContactCenterobj.ID_INSERT_USER = grdDataDigitalContactCenter.Selected.Rows[0].Cells["ID_INSERT_USER"].Value.ToString();
            //_DigitalContactCenterobj.INSERT_USER = grdDataDigitalContactCenter.Selected.Rows[0].Cells["INSERT_USER"].Value.ToString();
            //_DigitalContactCenterobj.FECHA_INGRESO = grdDataDigitalContactCenter.Selected.Rows[0].Cells["FECHA_INGRESO"].Value.ToString();
            _DigitalContactCenterobj.ID_UPDATE_USER = Globals.ClientSession.i_SystemUserId;
            //_DigitalContactCenterobj.UPDATE_USER = grdDataDigitalContactCenter.Selected.Rows[0].Cells["UPDATE_USER"].Value.ToString();
            _DigitalContactCenterobj.FECHA_ACTUALIZACION = DateTime.Now;

            var frm = new MKT_DIGITAL.frmRegistroDigitalAgregar_Editar(_DigitalContactCenterobj, "Editar");
            frm.ShowDialog();

            btnFilter_Click(null, null);

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {


            OperationResult objOperationResult = new OperationResult();

            DialogResult Result =
                MessageBox.Show(
                    "¿Está seguro de eliminar este registro?:" + System.Environment.NewLine +
                    objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                // Delete the item         
                var frm = new MKT_DIGITAL.frmMoivoEliminacionDigital();
                frm.ShowDialog(); 


                string pstrServiceOrderId = grdDataDigitalContactCenter.Selected.Rows[0].Cells["ID_DCC"].Value.ToString();

                if (frm.motivo != string.Empty)
                {
                    digitalContactBl.DeleteCotizacion(ref objOperationResult, pstrServiceOrderId, Globals.ClientSession.GetAsList(), frm.motivo);
                    btnFilter_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("NO SE PUDO ELIMINAR, COMPLETE LA DESCRIPCIÓN REQUERIDA", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //this.Close();
                }
                

            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            //var usuarioActual = Globals.ClientSession.i_SystemUserId;
            //var usuario_data = new ServiceBL().GetSystemUserId(usuarioActual);
            //var usuario_professional = new ServiceBL().GetProfessional(usuario_data.v_PersonId);

            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.BindGrid();
            };
        }

        private void BindGrid()
        {
            var objData = GetData(0, null, "");
            ListaGrilla = objData;
            grdDataDigitalContactCenter.DataSource = objData;
        }

        private List<DigitalContactCenter> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

           
            var usuarioActual = Globals.ClientSession.i_SystemUserId;

            var usuario_data = new ServiceBL().GetSystemUserId(usuarioActual);
            var usuario_professional = new ServiceBL().GetProfessional(usuario_data.v_PersonId);

            List<DigitalContactCenter> _objData = new List<DigitalContactCenter>();

            _objData = digitalContactBl.GetDigitalContactCenterFiltered(ref objOperationResult, pdatBeginDate, pdatEndDate);

            #region filtros
            if (checkDataTodos.Checked == true)
            {
                List<DigitalContactCenter> Data = _objData.Where(p => p.ELIMINADO == 1).ToList();
                _objData = new List<DigitalContactCenter>(Data);
            }
            else
            {
                List<DigitalContactCenter> Data = _objData.Where(p => p.ELIMINADO == 0).ToList();
                _objData = new List<DigitalContactCenter>(Data);
            }

            if (dboMedioPago.SelectedIndex != 0)
            {
                List<DigitalContactCenter> Data = _objData.Where(p => p.METODO_PAGO_ID == Convert.ToInt32(dboMedioPago.SelectedValue)).ToList();
                _objData = new List<DigitalContactCenter>(Data);
            }

            if (dboMedioMKT.SelectedIndex != 0)
            {
                List<DigitalContactCenter> Data = _objData.Where(p => p.MEDIO_MKT_ID == Convert.ToInt32(dboMedioMKT.SelectedValue)).ToList();
                _objData = new List<DigitalContactCenter>(Data);
            }

            if (cboEstadoAtencion.SelectedIndex != 0)
            {
                List<DigitalContactCenter> Data = _objData.Where(p => p.ESTADO_DCC_ID == Convert.ToInt32(cboEstadoAtencion.SelectedValue)).ToList();
                _objData = new List<DigitalContactCenter>(Data);
            }

            if (cboUserMed.SelectedIndex != 0 && cboUserMed.SelectedIndex != -1)
            {
                List<DigitalContactCenter> Data = _objData.Where(p => p.ID_INSERT_USER == Convert.ToInt32(cboUserMed.SelectedValue)).ToList();
                _objData = new List<DigitalContactCenter>(Data);
            }

            if (txtPacient.Text != string.Empty)
            {
                List<DigitalContactCenter> Data = _objData.Where(p => p.DOC.Contains(txtPacient.Text) || 
                                                                      p.NOMBRES.Contains(txtPacient.Text) || 
                                                                      p.AP_PATERNO.Contains(txtPacient.Text) || 
                                                                      p.AP_MATERNO.Contains(txtPacient.Text) ).ToList();
                _objData = new List<DigitalContactCenter>(Data);
            }
            #endregion



            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;

        }

        private void frmRegistroDigital_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            Utils.LoadDropDownList(dboMedioPago, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 98, null), DropDownListAction.Select);
            Utils.LoadDropDownList(dboMedioMKT, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 413, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cboEstadoAtencion, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 95, null), DropDownListAction.Select);

            Utils.LoadDropDownList(cboUserMed, "Value1", "Id", BLL.Utils.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);

        }

        private void txtPacient_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void dboMedioPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void dboMedioMKT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void cboEstadoAtencion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void dtpDateTimeStar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void dptDateTimeEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void cboUserMed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void checkDataTodos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void btnActualizarPerson_Click(object sender, EventArgs e)
        {
            string personId = grdDataDigitalContactCenter.Selected.Rows[0].Cells["ID_Person"].Value.ToString();

            if (personId != null)
            {
                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    var frm = new frmPacient(personId);
                    frm.ShowDialog();
                }
            }
            //else
            //{
            //    btnActualizarPerson.Enabled = false;
            //}
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string Paciente = "";

            var NOMBRES = grdDataDigitalContactCenter.Selected.Rows[0].Cells["NOMBRES"].Value.ToString();
            var AP_PATERNO = grdDataDigitalContactCenter.Selected.Rows[0].Cells["AP_PATERNO"].Value.ToString();
            var AP_MATERNO = grdDataDigitalContactCenter.Selected.Rows[0].Cells["AP_MATERNO"].Value.ToString();

            Paciente = NOMBRES + " " + AP_PATERNO + " " + AP_MATERNO; 

            Paciente = Paciente.Replace(" ", "%20");
            string numero = grdDataDigitalContactCenter.Selected.Rows[0].Cells["CELULAR"].Value.ToString();
            string urlCompleta = "";
            string urlws = Common.Utils.GetApplicationConfigValue("Urlwhatsapp").ToString();
            string msws = Common.Utils.GetApplicationConfigValue("Mensajewhatsapp").ToString();
            if (numero.Length > 8)
            {
                urlCompleta = urlws + numero + "?text=" + Paciente + "%20" + msws;
                ProcessStartInfo sInfo = new ProcessStartInfo(urlCompleta);
                Process.Start(sInfo);
            }
            else
            {
                return;
            }
        }

        private void btnReProgramar_Click(object sender, EventArgs e)
        {
            DigitalContactCenter _DigitalContactCenterobj = new DigitalContactCenter();


            //_DigitalContactCenterobj.ID_DCC = grdDataDigitalContactCenter.Selected.Rows[0].Cells["ID_DCC"].Value.ToString();
            _DigitalContactCenterobj.ID_Person = grdDataDigitalContactCenter.Selected.Rows[0].Cells["ID_Person"].Value.ToString();
            _DigitalContactCenterobj.TIPO_DOC_ID = int.Parse(grdDataDigitalContactCenter.Selected.Rows[0].Cells["TIPO_DOC_ID"].Value.ToString());
            _DigitalContactCenterobj.TIPO_DOC = grdDataDigitalContactCenter.Selected.Rows[0].Cells["TIPO_DOC"].Value.ToString();
            _DigitalContactCenterobj.DOC = grdDataDigitalContactCenter.Selected.Rows[0].Cells["DOC"].Value.ToString();
            _DigitalContactCenterobj.NOMBRES = grdDataDigitalContactCenter.Selected.Rows[0].Cells["NOMBRES"].Value.ToString();
            _DigitalContactCenterobj.AP_PATERNO = grdDataDigitalContactCenter.Selected.Rows[0].Cells["AP_PATERNO"].Value.ToString();
            _DigitalContactCenterobj.AP_MATERNO = grdDataDigitalContactCenter.Selected.Rows[0].Cells["AP_MATERNO"].Value.ToString();
            _DigitalContactCenterobj.CELULAR = grdDataDigitalContactCenter.Selected.Rows[0].Cells["CELULAR"].Value.ToString();
            _DigitalContactCenterobj.EMAIL = grdDataDigitalContactCenter.Selected.Rows[0].Cells["EMAIL"].Value.ToString();
            _DigitalContactCenterobj.DIRECCION = grdDataDigitalContactCenter.Selected.Rows[0].Cells["DIRECCION"].Value.ToString();
            _DigitalContactCenterobj.MEDIO_MKT_ID = int.Parse(grdDataDigitalContactCenter.Selected.Rows[0].Cells["MEDIO_MKT_ID"].Value.ToString());

            _DigitalContactCenterobj.FECHA_NACIMIENTO = DateTime.Parse(grdDataDigitalContactCenter.Selected.Rows[0].Cells["FECHA_NACIMIENTO"].Value.ToString());
            _DigitalContactCenterobj.SEXO = int.Parse(grdDataDigitalContactCenter.Selected.Rows[0].Cells["SEXO"].Value.ToString());
           
            //_DigitalContactCenterobj.MEDIO_MKT = grdDataDigitalContactCenter.Selected.Rows[0].Cells["MEDIO_MKT"].Value.ToString();
            _DigitalContactCenterobj.FECHA_CITA = DateTime.Parse(grdDataDigitalContactCenter.Selected.Rows[0].Cells["FECHA_CITA"].Value.ToString());
            _DigitalContactCenterobj.PROTOCOL_ID = grdDataDigitalContactCenter.Selected.Rows[0].Cells["PROTOCOL_ID"].Value.ToString();
            //_DigitalContactCenterobj.PROTOCOL_NAME = grdDataDigitalContactCenter.Selected.Rows[0].Cells["PROTOCOL_NAME"].Value.ToString();
            _DigitalContactCenterobj.METODO_PAGO_ID = int.Parse(grdDataDigitalContactCenter.Selected.Rows[0].Cells["METODO_PAGO_ID"].Value.ToString());
            _DigitalContactCenterobj.MOTIVO = grdDataDigitalContactCenter.Selected.Rows[0].Cells["MOTIVO"].Value.ToString();
            //_DigitalContactCenterobj.METODO_PAGO = grdDataDigitalContactCenter.Selected.Rows[0].Cells["METODO_PAGO"].Value.ToString();
            _DigitalContactCenterobj.ESTADO_DCC_ID = int.Parse(grdDataDigitalContactCenter.Selected.Rows[0].Cells["ESTADO_DCC_ID"].Value.ToString());
            //_DigitalContactCenterobj.ESTADO_DCC = grdDataDigitalContactCenter.Selected.Rows[0].Cells["ESTADO_DCC"].Value.ToString();
            //_DigitalContactCenterobj.COMPROBANTE_ADJUNTO = grdDataDigitalContactCenter.Selected.Rows[0].Cells["COMPROBANTE_ADJUNTO"].Value.ToString();
            _DigitalContactCenterobj.SERVICIO_ENLAZADO = grdDataDigitalContactCenter.Selected.Rows[0].Cells["SERVICIO_ENLAZADO"].Value.ToString();
            _DigitalContactCenterobj.DCCIdReAgenda = grdDataDigitalContactCenter.Selected.Rows[0].Cells["ID_DCC"].Value.ToString();
            _DigitalContactCenterobj.COMENTARIOS = grdDataDigitalContactCenter.Selected.Rows[0].Cells["COMENTARIOS"].Value.ToString();
            //_DigitalContactCenterobj.MOTIVO_ELIMINACION = grdDataDigitalContactCenter.Selected.Rows[0].Cells["MOTIVO_ELIMINACION"].Value.ToString();
            //_DigitalContactCenterobj.ELIMINADO = grdDataDigitalContactCenter.Selected.Rows[0].Cells["ELIMINADO"].Value.ToString();
            _DigitalContactCenterobj.ID_INSERT_USER = Globals.ClientSession.i_SystemUserId;
            //_DigitalContactCenterobj.INSERT_USER = grdDataDigitalContactCenter.Selected.Rows[0].Cells["INSERT_USER"].Value.ToString();
            _DigitalContactCenterobj.FECHA_INGRESO = DateTime.Now;
            //_DigitalContactCenterobj.ID_UPDATE_USER = Globals.ClientSession.i_SystemUserId;
            //_DigitalContactCenterobj.UPDATE_USER = grdDataDigitalContactCenter.Selected.Rows[0].Cells["UPDATE_USER"].Value.ToString();
            //_DigitalContactCenterobj.FECHA_ACTUALIZACION = DateTime.Now;

            var frm = new MKT_DIGITAL.frmRegistroDigitalAgregar_Editar(_DigitalContactCenterobj, "ReProgramar");
            frm.ShowDialog();

            btnFilter_Click(null, null);

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                string _EstadoDigital = grdDataDigitalContactCenter.Selected.Rows[0].Cells["ESTADO_DCC_ID"].Value.ToString();
                string _ComentarioDigital = grdDataDigitalContactCenter.Selected.Rows[0].Cells["COMENTARIOS"].Value == null ? "" : grdDataDigitalContactCenter.Selected.Rows[0].Cells["COMENTARIOS"].Value.ToString();
                string idccEditar = grdDataDigitalContactCenter.Selected.Rows[0].Cells["ID_DCC"].Value.ToString();
                string idccServicio = grdDataDigitalContactCenter.Selected.Rows[0].Cells["SERVICIO_ENLAZADO"].Value.ToString();

                frmEditarEstado frm = new frmEditarEstado(_EstadoDigital, _ComentarioDigital, "EDITAR", idccEditar, idccServicio);
                frm.ShowDialog();

                btnFilter_Click(sender, e);
            }
            catch
            {
                MessageBox.Show("Sucedió un error, por favor vuelva a intentar.", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string Paciente = "";

            var NOMBRES = grdDataDigitalContactCenter.Selected.Rows[0].Cells["NOMBRES"].Value.ToString();
            var AP_PATERNO = grdDataDigitalContactCenter.Selected.Rows[0].Cells["AP_PATERNO"].Value.ToString();
            var AP_MATERNO = grdDataDigitalContactCenter.Selected.Rows[0].Cells["AP_MATERNO"].Value.ToString();

            Paciente = NOMBRES + " " + AP_PATERNO + " " + AP_MATERNO;

            Paciente = Paciente.Replace(" ", "%20");
            string numero = grdDataDigitalContactCenter.Selected.Rows[0].Cells["CELULAR"].Value.ToString();
            string urlCompleta = "";
            string urlws = Common.Utils.GetApplicationConfigValue("Urlwhatsapp").ToString();
            string msws = Common.Utils.GetApplicationConfigValue("Mensajewhatsapp").ToString();
            if (numero.Length > 8)
            {
                urlCompleta = urlws + numero + "?text=" + Paciente + "%20" + msws;
                ProcessStartInfo sInfo = new ProcessStartInfo(urlCompleta);
                Process.Start(sInfo);
            }
            else
            {
                return;
            }
        }


    }
}
