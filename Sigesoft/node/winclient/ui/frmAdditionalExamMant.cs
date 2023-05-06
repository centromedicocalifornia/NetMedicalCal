using Infragistics.Win.UltraWinGrid;
using NetPdf;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BE.Custom;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmAdditionalExamMant : Form
    {
        private string _serviceId = "";
        private MergeExPDF _mergeExPDF = new MergeExPDF();
        private List<AdditionalExamUpdate> _DataSource = new List<AdditionalExamUpdate>();
        private string _ProtocolId;
        private string _personId;

        public frmAdditionalExamMant(string serviceId, List<AdditionalExamUpdate> data, string ProtocolId, string personId)
        {
            _ProtocolId = ProtocolId;
            _personId = personId;

            if (data != null)
            {
                _DataSource = data;
            }
            _serviceId = serviceId;
            InitializeComponent();
        }

        private void frmAdditionalExamMant_Load(object sender, EventArgs e)
        {
            grdDataAdditionalExam.DataSource = _DataSource;
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            if (_DataSource.Count > 0)
            {
                btnImprimir.Enabled = false;
                btnEliminar.Enabled = false;
                btnActualizar.Enabled = false;
                button1.Enabled = false;

                var data = _DataSource.FindAll(x => x.v_ComponentName.Contains(txtName.Text.ToUpper())).ToList();
                grdDataAdditionalExam.DataSource = data;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var Dialog = MessageBox.Show("¿ Seguro de eliminar las filas seleccionadas ?", "CONFIRMACIÓN",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Dialog == DialogResult.Yes)
                {
                    using (new LoadingClass.PleaseWait(this.Location, "Eliminando..."))
                    {
                        var newList = new List<AdditionalExamUpdate>();
                        foreach (var row in grdDataAdditionalExam.Selected.Rows)
                        {
                            var additionalExamId = row.Cells["v_AdditionalExamId"].Value.ToString();
                            new AdditionalExamBL().DeleteAdditionalExam(additionalExamId, Globals.ClientSession.i_SystemUserId);
                        }
                        BindingGrid();
                    }                   
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            
            
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (grdDataAdditionalExam.Selected.Rows.Count > 0)
            {
                var componentId = grdDataAdditionalExam.Selected.Rows[0].Cells["v_ComponentId"].Value.ToString();
                var additionalExamId = grdDataAdditionalExam.Selected.Rows[0].Cells["v_AdditionalExamId"].Value.ToString();
                var frm = new frmUpdateAdditionalExam(componentId, additionalExamId);
                frm.ShowDialog();
                BindingGrid();

            }
            
        }

        private void BindingGrid()
        {
            var Data = new AdditionalExamBL().GetAdditionalExamForUpdateByServiceId(_serviceId, Globals.ClientSession.i_SystemUserId);
            Data = Data.OrderBy(p => p.v_IdGrupoAdicional).ToList();
            grdDataAdditionalExam.DataSource = Data;
            grdDataAdditionalExam.DataBind();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (grdDataAdditionalExam.Rows.Count == 0)
            {
                MessageBox.Show("No hay exámenes para imprimir", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            var ruta = Common.Utils.GetApplicationConfigValue("rutaExamenesAdicionales").ToString();
            var rutaBasura = Common.Utils.GetApplicationConfigValue("rutaReportesBasura").ToString();
            string pathFile = "";
            string CMP = "";
            var openFile = false;

            var N_Orden = grdDataAdditionalExam.Selected.Rows[0].Cells["v_IdGrupoAdicional"].Value.ToString();

            if (N_Orden != string.Empty && N_Orden != null)
            {

                using (new LoadingClass.PleaseWait(this.Location, "Cargando..."))
                {
                    OperationResult objOperationResult = new OperationResult();

                    var datosGrabo = new ServiceBL().DevolverDatosUsuarioFirma(Globals.ClientSession.i_SystemUserId);
                    CMP = datosGrabo.CMP;
                    pathFile = string.Format("{0}.pdf",
                        Path.Combine(ruta, _serviceId + "-" + "ORDEN-EX-MED-ADICI-" + datosGrabo.CMP));


                    List<Categoria> AdditionalExam = new List<Categoria>();
                    List<Categoria> DataSource = new List<Categoria>();
                    List<string> ComponentList = new List<string>();
                    //var ListadditExam = new AdditionalExamBL().GetAdditionalExamByServiceId_all(_serviceId, Globals.ClientSession.i_SystemUserId);
                    var ListadditExam =
                        new AdditionalExamBL().GetAdditionalExamByServiceId_Por_Orden(_serviceId,
                            Globals.ClientSession.i_SystemUserId, N_Orden);

                    foreach (var componenyId in ListadditExam)
                    {
                        ComponentList.Add(componenyId.ComponentId);
                    }

                    foreach (var componentId in ComponentList)
                    {
                        var ListServiceComponent = new ServiceBL().GetAllComponents(ref objOperationResult,
                            (int) TipoBusqueda.ComponentId, componentId);

                        Categoria categoria =
                            DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId);
                        if (categoria != null)
                        {
                            List<ComponentDetailList> componentDetail = new List<ComponentDetailList>();
                            componentDetail = ListServiceComponent[0].Componentes;
                            DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId).Componentes
                                .AddRange(componentDetail);
                        }
                        else
                        {
                            DataSource.AddRange(ListServiceComponent);
                        }
                    }


                    var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
                    var DatosPaciente = new PacientBL().DevolverDatosPaciente(_serviceId);
                    var Edad = DiferenciaFechas(DatosPaciente.FechaServicio.Value, DatosPaciente.d_Birthdate.Value);

                    new PrintAdditionalExam().GenerateAdditionalexam(pathFile, MedicalCenter, DatosPaciente, datosGrabo,
                        txtComentario.Text, DataSource, ListadditExam, Edad);
                }
            }
            else
            {
                MessageBox.Show("Seleccione N° de Orden para imprimir correctamente", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return; 
            }

            List<string> pdfList = new List<string>();
            pdfList.Add(pathFile);
            _mergeExPDF.FilesName = pdfList;
            _mergeExPDF.DestinationFile = string.Format("{0}.pdf", Path.Combine(rutaBasura, _serviceId + "-" + "ORDEN-EX-MED-ADICI-" + CMP));
            _mergeExPDF.Execute();
            _mergeExPDF.RunFile();
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

        private void button1_Click(object sender, EventArgs e)
        {
            var N_Orden = grdDataAdditionalExam.Selected.Rows[0].Cells["v_IdGrupoAdicional"].Value.ToString();

            if (N_Orden != string.Empty && N_Orden != null)
            {
                ServiceBL oServiceBL = new ServiceBL();

                OperationResult objOperationResult = new OperationResult();
                List<string> Componentes = new List<string>();
                var listServicecomponents = oServiceBL.GetServiceComponents(ref objOperationResult, _serviceId);
                foreach (var obj in listServicecomponents)
                {
                    Componentes.Add(obj.v_ComponentId);
                }
                var frm = new AdditionalExam(Componentes, _ProtocolId, _serviceId, _personId, N_Orden);
                frm.ShowDialog();

                BindingGrid();

                if (frm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                    return; 
            }
            else
            {
                MessageBox.Show("Seleccione N° de Orden para adicionar examen", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            BindingGrid();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ServiceBL oServiceBL = new ServiceBL();

            OperationResult objOperationResult = new OperationResult();
            List<string> Componentes = new List<string>();
            var listServicecomponents = oServiceBL.GetServiceComponents(ref objOperationResult, _serviceId);
            foreach (var obj in listServicecomponents)
            {
                Componentes.Add(obj.v_ComponentId);
            }
            var frm = new AdditionalExam(Componentes, _ProtocolId, _serviceId, _personId, "");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;

            BindingGrid();

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";

            NombreArchivo = "Lista de Ordenes y Examenes del paciente " + _DataSource[0].v_PacientName;

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdDataAdditionalExam, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void grdDataAdditionalExam_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            foreach (UltraGridRow rowSelected in this.grdDataAdditionalExam.Selected.Rows)
            {
                if (rowSelected.Band.Index.ToString() == "0")
                {
                    if (grdDataAdditionalExam.Selected.Rows[0].Cells["v_IdGrupoAdicional"].Value != null)
                    {
                        btnImprimir.Enabled = true;
                        btnEliminar.Enabled = true;
                        btnActualizar.Enabled = true;
                        button1.Enabled = true;

                    }
                    else
                    {
                        btnImprimir.Enabled = false;
                        btnEliminar.Enabled = false;
                        btnActualizar.Enabled = false;
                        button1.Enabled = false;
                    }

                }
            }

            if (grdDataAdditionalExam.Selected.Rows.Count == 0)
                return;
        }
    }
}
