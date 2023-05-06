using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Matriz
{
    
    public partial class frmMatrizMultiple : Form
    {
        string strFilterExpression = "";
        public frmMatrizMultiple()
        {
            InitializeComponent();
        }

        private void frmMatrizMultiple_Load(object sender, EventArgs e)
        {
            DateTime fechatemp = DateTime.Today;
            DateTime fecha1 = new DateTime(fechatemp.Year, fechatemp.Month, 1);

            int nodeId = int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"));
            OperationResult objOperationResult = new OperationResult();
            //var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);

            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, nodeId);
            var clientOrganization1 = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, nodeId);
            var clientOrganization2 = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, nodeId);
            var clientOrganization3 = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, nodeId);

            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);
            Utils.LoadDropDownList(ddlEmployerOrganization, "Value1", "Id", clientOrganization1, DropDownListAction.All);
            Utils.LoadDropDownList(ddlWorkingOrganization, "Value1", "Id", clientOrganization2, DropDownListAction.All);
            Utils.LoadDropDownList(ddlFacturacion, "Value1", "Id", clientOrganization3, DropDownListAction.All);


            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, "-1", "-1", null), DropDownListAction.All);

            dtpDateTimeStar.Value = fecha1;
            dptDateTimeEnd.Value = DateTime.Today;
            dtpDateTimeStar.CustomFormat = "dd/MM/yyyy";
            dptDateTimeEnd.CustomFormat = "dd/MM/yyyy";
        }


        private void ddlCustomerOrganization_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ddlCustomerOrganization.SelectedValue == null)
                return;

            if (ddlCustomerOrganization.SelectedValue.ToString() == "-1")
            {
                ddlProtocolId.SelectedValue = "-1";
                ddlProtocolId.Enabled = false;
                return;
            }
            ddlProtocolId.Enabled = true;
            OperationResult objOperationResult = new OperationResult();
            var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, id3[0], id3[1], null), DropDownListAction.All);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();

                List<string> Filters = new List<string>();
                DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
                DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

                if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
                {
                    var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
                    Filters.Add("v_CustomerOrganizationId==" + "\"" + id3[0] + "\"&&v_CustomerLocationId==" + "\"" + id3[1] + "\"");
                }

                if (ddlEmployerOrganization.SelectedValue.ToString() != "-1")
                {
                    var id3 = ddlEmployerOrganization.SelectedValue.ToString().Split('|');
                    Filters.Add("v_EmployerOrganizationId==" + "\"" + id3[0] + "\"&&v_EmployerLocationId==" + "\"" + id3[1] + "\"");
                }

                if (ddlWorkingOrganization.SelectedValue.ToString() != "-1")
                {
                    var id3 = ddlWorkingOrganization.SelectedValue.ToString().Split('|');
                    Filters.Add("v_WorkingOrganizationId==" + "\"" + id3[0] + "\"&&v_WorkingLocationId==" + "\"" + id3[1] + "\"");
                }

                if (ddlFacturacion.SelectedValue.ToString() != "-1")
                {
                    var id3 = ddlFacturacion.SelectedValue.ToString().Split('|');
                    Filters.Add("v_OrganizationId==" + "\"" + id3[0] + "\"");
                }

                if (ddlProtocolId.SelectedValue.ToString() != "-1")
                    Filters.Add("ProtocolId==" + "\"" + ddlProtocolId.SelectedValue + "\"");

                // Create the Filter Expression
                strFilterExpression = null;
                if (Filters.Count > 0)
                {
                    foreach (string item in Filters)
                    {
                        strFilterExpression = strFilterExpression + item + " && ";
                    }
                    strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
                }
                string empresa = ddlCustomerOrganization.SelectedValue.ToString();
                string contrata = ddlEmployerOrganization.SelectedValue.ToString(); ;
                string trabajo = ddlWorkingOrganization.SelectedValue.ToString();
                string protocolo = ddlProtocolId.SelectedValue.ToString();
                var facturacion = ddlFacturacion.Text;

                if (tabsMatriz.SelectedTab.Name == "tpShauindo")
                {
                    var objData = new PacientBL().ReporteMatrizShauindo(pdatBeginDate, pdatEndDate, empresa, contrata, trabajo, protocolo, facturacion);
                    grdShauindo.DataSource = objData;
                    lblShahuindo.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                    btnExportShauindo.Enabled = true;
                }
                else if (tabsMatriz.SelectedTab.Name == "tpCsl")
                {
                    var objData = new PacientBL().ReporteMatrizSanLorenzo(pdatBeginDate, pdatEndDate, empresa, contrata, trabajo, protocolo, facturacion);
                    grdSanLorenzo.DataSource = objData;
                    lblSanLorenzo.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                    btnExportSanLorenzo.Enabled = true;
                }
                else if (tabsMatriz.SelectedTab.Name == "tpLaZanja")
                {
                    var objData = new PacientBL().ReporteMatrizLaZanja(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    grdLaZanja.DataSource = objData;
                    lablLaZanja.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                    btnExportLaZanja.Enabled = true;
                }
                else if (tabsMatriz.SelectedTab.Name == "tpLaZanjaEspeciales")
                {
                    var objData = new PacientBL().ReporteMatrizLaZanjaEspeciales(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    grdLaZanjaEspeciales.DataSource = objData;
                    lablLaZanjaEspeciales.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                    btnExportLaZanjaEspeciales.Enabled = true;
                }
                else if (tabsMatriz.SelectedTab.Name == "tpGoldFields")
                {
                    var objData = new PacientBL().ReporteMatrizGoldFields(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    grdGolFields.DataSource = objData;
                    lblGoldfields.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                    btnExportGolFields.Enabled = true;
                }
                else if (tabsMatriz.SelectedTab.Name == "tpSolucManteIntegra")
                {
                    var objData = new PacientBL().ReporteMatrizSolucManteIntegra(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    grdSoluc.DataSource = objData;
                    lablSYMI.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                    btnExportSoluc.Enabled = true;
                }
                else if (tabsMatriz.SelectedTab.Name == "tpMiBanco")
                {
                    var objData = new PacientBL().ReporteMatrizMiBanco(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    grdMiBanco.DataSource = objData;
                    lblMiBanco.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                    btnExportMiBanco.Enabled = true;
                }
                else if (tabsMatriz.SelectedTab.Name == "tpTriajeCovid19")
                {
                    var objData = new PacientBL().ReporteMatrizCovid19(pdatBeginDate, pdatEndDate, empresa, contrata, trabajo, protocolo, facturacion);
                    grdTriajeCovid.DataSource = objData;
                    lblCovid.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                    btnExportShauindo.Enabled = true;
                }
                else if (tabsMatriz.SelectedTab.Name == "tpCoimolache")
                {
                    var objData = new PacientBL().ReporteMatrizCoimolache(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    grdCoimolache.DataSource = objData;
                    lblCoimolache.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                    btnExportCoimolache.Enabled = true;
                }
                else if (tabsMatriz.SelectedTab.Name == "tpHudbay")
                {
                    PacientBL pacienteBL = new PacientBL();
                    var objData = pacienteBL.ReporteHudbay(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    grHudbay.DataSource = objData;
                    label5.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                    button2.Enabled = true;
                }
                else if (tabsMatriz.SelectedTab.Name == "tabHB2022_1")
                {
                    PacientBL pacienteBL = new PacientBL();
                    var objData = pacienteBL.ReporteHudbay_Part_1(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    grHudbay_2022_1.DataSource = objData;
                    lbl_2022_1.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                    btnHB_2022_1.Enabled = true;
                }
                else if (tabsMatriz.SelectedTab.Name == "tabManucci")
                {
                    var objData = new PacientBL().ReporteMatrizManucciDiesel(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    grdManucci.DataSource = objData;
                    label8.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                    button3.Enabled = true;
                }
            }
        }

        private void grdLaZanja_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private void btnExportShauindo_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }
            else
            {
                NombreArchivo = "Matriz de datos de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            sfdShauindo.FileName = NombreArchivo;
            sfdShauindo.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sfdShauindo.ShowDialog() == DialogResult.OK)
            {
                this.ugeShauindo.Export(this.grdShauindo, sfdShauindo.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportLaZanja_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }
            else
            {
                NombreArchivo = "Matriz de datos de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            sfdLaZanja.FileName = NombreArchivo;
            sfdLaZanja.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sfdLaZanja.ShowDialog() == DialogResult.OK)
            {
                this.ugeLaZanja.Export(this.grdLaZanja, sfdLaZanja.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportGolFields_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }
            else
            {
                NombreArchivo = "Matriz de datos de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            sfdGoldFiels.FileName = NombreArchivo;
            sfdGoldFiels.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sfdGoldFiels.ShowDialog() == DialogResult.OK)
            {
                this.ugeGoldFiels.Export(this.grdGolFields, sfdGoldFiels.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportSoluc_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }
            else
            {
                NombreArchivo = "Matriz de datos de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            sdfSoluc.FileName = NombreArchivo;
            sdfSoluc.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sdfSoluc.ShowDialog() == DialogResult.OK)
            {
                this.ugeSoluc.Export(this.grdSoluc, sdfSoluc.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportMiBanco_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }
            else
            {
                NombreArchivo = "Matriz de datos de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            sfdMiBanco.FileName = NombreArchivo;
            sfdMiBanco.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sfdMiBanco.ShowDialog() == DialogResult.OK)
            {
                this.ugeMiBanco.Export(this.grdMiBanco, sfdMiBanco.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void grdShauindo_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }
            else
            {
                NombreArchivo = "Matriz de datos de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            sfdShauindo.FileName = NombreArchivo;
            sfdShauindo.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sfdShauindo.ShowDialog() == DialogResult.OK)
            {
                this.ugeShauindo.Export(this.grdTriajeCovid, sfdShauindo.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnExportCoimolache_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }
            else
            {
                NombreArchivo = "Matriz de datos de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            sfdMiBanco.FileName = NombreArchivo;
            sfdMiBanco.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sfdMiBanco.ShowDialog() == DialogResult.OK)
            {
                this.ugeMiBanco.Export(this.grdCoimolache, sfdMiBanco.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }
            else
            {
                NombreArchivo = "Matriz de datos de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            sfdMiBanco.FileName = NombreArchivo;
            sfdMiBanco.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sfdMiBanco.ShowDialog() == DialogResult.OK)
            {
                this.ugeMiBanco.Export(this.grHudbay, sfdMiBanco.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportSanLorenzo_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }
            else
            {
                NombreArchivo = "Matriz de datos de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            sfdShauindo.FileName = NombreArchivo;
            sfdShauindo.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sfdShauindo.ShowDialog() == DialogResult.OK)
            {
                this.ugeShauindo.Export(this.grdSanLorenzo, sfdShauindo.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnHB_2022_1_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }
            else
            {
                NombreArchivo = "Matriz de datos de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            sfdMiBanco.FileName = NombreArchivo;
            sfdMiBanco.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sfdMiBanco.ShowDialog() == DialogResult.OK)
            {
                this.ugeMiBanco.Export(this.grHudbay_2022_1 , sfdMiBanco.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportLaZanjaEspeciales_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text + " - Esp";
            }
            else
            {
                NombreArchivo = "Matriz de datos de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text + " - Esp";
            }

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            sfdLaZanja.FileName = NombreArchivo;
            sfdLaZanja.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sfdLaZanja.ShowDialog() == DialogResult.OK)
            {
                this.ugeLaZanja.Export(this.grdLaZanjaEspeciales, sfdLaZanja.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }
            else
            {
                NombreArchivo = "Matriz de datos de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            }

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            sfdMiBanco.FileName = NombreArchivo;
            sfdMiBanco.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sfdMiBanco.ShowDialog() == DialogResult.OK)
            {
                this.ugeMiBanco.Export(this.grdManucci, sfdMiBanco.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
