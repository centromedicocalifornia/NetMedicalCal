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

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmEditatarHoras : Form
    {
        private string HospiId;
        object lista;
        private hospitalizacionDto _HospiDto = null;
        private hospitalizacionDto _hospitalizacionDto = null;
        private string DxT;

        public frmEditatarHoras(string _HospiId)
        {
            HospiId = _HospiId;
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditatarHoras_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            _HospiDto = new HospitalizacionBL().GetHospitalizacion(ref objOperationResult, HospiId);

            if (_HospiDto.d_FechaHoraInicioSop == null)
            {
                dptDateTimeInic.Value = DateTime.Now;
            }
            else
            {
                dptDateTimeInic.Value = _HospiDto.d_FechaHoraInicioSop.Value;
            }

            if (_HospiDto.d_FechaHoraFinSop == null)
            {
                dptDateTimeInic.Value = DateTime.Now;
            }
            else
            {
                dptDateTimeFin.Value = _HospiDto.d_FechaHoraFinSop.Value;
            }       
            

        }

        private void btnGuardarTicket_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            if (_hospitalizacionDto == null)
            {
                _hospitalizacionDto = new hospitalizacionDto();
            }

          
            _hospitalizacionDto = new HospitalizacionBL().GetHospitalizacion(ref objOperationResult, HospiId);

            _hospitalizacionDto.d_FechaHoraInicioSop = dptDateTimeInic.Value;
            _hospitalizacionDto.d_FechaHoraFinSop = dptDateTimeFin.Value;
         
            new HospitalizacionBL().UpdateHospitalizacion(ref objOperationResult, _hospitalizacionDto, Globals.ClientSession.GetAsList());

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
