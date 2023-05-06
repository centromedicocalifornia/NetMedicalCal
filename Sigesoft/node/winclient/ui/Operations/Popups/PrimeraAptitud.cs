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

namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    public partial class PrimeraAptitud : Form
    {
        private readonly string _serviceId;
        private readonly int _nodeId;
        private readonly int _roleId;
        private readonly int _userId;
        private readonly int _aptitud;
        public PrimeraAptitud(string serviceId, int roleId, int nodeId, int userId, int aptitud)
        {
            _serviceId = serviceId;
            _nodeId = nodeId;
            _roleId = roleId;
            _userId = userId;
            _aptitud = aptitud;
            InitializeComponent();
        }

        private void PrimeraAptitud_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            var serviceDTO = new serviceDto();

            var graba = new ServiceBL().GetNombreGrabaAptitud((int)_userId);
            if (string.IsNullOrEmpty(txtComentarioObservacion.Text))
            {
                //btnGuardar.Enabled = true;
                //MessageBox.Show("Debe completar la información.");
                MessageBox.Show("Debe completar la información.", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } 

            serviceDTO.v_ServiceId = _serviceId;
            serviceDTO.i_AptitudesStatusId_First = _aptitud;
            serviceDTO.v_CommentAptitusStatus_First = txtComentarioObservacion.Text + " | " + graba.NombreDoctor + " | " + DateTime.Now.ToString();

            new ServiceBL().updateService(ref objOperationResult, serviceDTO, Globals.ClientSession.GetAsList());

            this.Close();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
