using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.MKT_DIGITAL
{
    public partial class frmEditarEstado : Form
    {
        DigitalContactCenterBL _DigitalContactCenterBL = new DigitalContactCenterBL();

        private string _estadoId;
        private string _comentariosDescp;
        private string _DigitalId;
        private string _modo;
        private string _ServiceId;
        public frmEditarEstado(string estadoId, string comentariosDescp, string modo, string DigitalId, string ServiceId)
        {
            _estadoId = estadoId;
            _comentariosDescp = comentariosDescp;
            _DigitalId = DigitalId;
            _modo = modo;
            _ServiceId = ServiceId;
            InitializeComponent();
        }

        private void frmEditarEstado_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            Utils.LoadDropDownList(cboEstadoAtencion, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 95, null), DropDownListAction.Select);
            cboEstadoAtencion.SelectedValue = _estadoId;

            txtComentario.Text = _comentariosDescp;


        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            if (cboEstadoAtencion.SelectedValue == "-1")
            {
                MessageBox.Show("Seleccione correctamente el estado de REGISTRO.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtComentario.Text == string.Empty)
            {
                MessageBox.Show("Ingrese un motivo contundente para editar estado de REGISTRO.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            digitalcontactcenterDto _DigitalContactCenterObjEdit = new digitalcontactcenterDto();

            _DigitalContactCenterObjEdit = _DigitalContactCenterBL.GetPersonDigContCent(ref objOperationResult, _DigitalId);

            _DigitalContactCenterObjEdit.i_EstadoAtencion = int.Parse(cboEstadoAtencion.SelectedValue.ToString());
            _DigitalContactCenterObjEdit.v_Comentarios = txtComentario.Text;


            if (int.Parse(cboEstadoAtencion.SelectedValue.ToString()) == 4 || 
                          int.Parse(cboEstadoAtencion.SelectedValue.ToString()) == 5 ||
                                    int.Parse(cboEstadoAtencion.SelectedValue.ToString()) == 6)
            {
                _DigitalContactCenterBL.CancelarAtencion(ref objOperationResult, _ServiceId, Globals.ClientSession.GetAsList());
            }


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
}
