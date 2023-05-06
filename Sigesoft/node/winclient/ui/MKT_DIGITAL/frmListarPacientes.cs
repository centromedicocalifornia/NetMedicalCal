using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE.Custom;
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
    public partial class frmListarPacientes : Form
    {
        private List<EsoDto> _listaPacientes = new List<EsoDto>();
        private List<EsoDto> _listaPacientesTemp = new List<EsoDto>();

        DigitalContactCenterBL listPersonBl = new DigitalContactCenterBL();

        public string PacienteId = "";

        public frmListarPacientes()
        {
            InitializeComponent();
        }

        private void frmListarPacientes_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            _listaPacientes = listPersonBl.GetPacientsPagedAndFiltered_(ref objOperationResult);

            grdDataCalendar.DataSource = _listaPacientes;
            if (_listaPacientes != null)
            {
                lblRecordCount.Text = string.Format("Se encontraron {0} registros.", _listaPacientes.Count());

            }
        }

        private void grdDataCalendar_DoubleClick(object sender, EventArgs e)
        {
            PacienteId = grdDataCalendar.Selected.Rows[0].Cells["Id"].Value.ToString();
            this.Close();
        }

        private void txtPacient_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPacient.Text != string.Empty)
            {
                _listaPacientesTemp = new List<EsoDto>(_listaPacientes.Where(p => p.Nombre.Contains(txtPacient.Text.ToUpper()) || p.Id.Contains(txtPacient.Text.ToUpper())));

                grdDataCalendar.DataSource = _listaPacientesTemp;

                if (_listaPacientesTemp != null)
                {
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", _listaPacientesTemp.Count());

                }

            }
            else
            {
                grdDataCalendar.DataSource = _listaPacientes;
                if (grdDataCalendar != null)
                {
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", _listaPacientes.Count());

                }
            }
        }

        private void grdDataCalendar_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }
    }
}
