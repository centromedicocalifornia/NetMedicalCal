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
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Cotizaciones
{
    public partial class frmCotizacionOc : Form
    {
        ServiceBL objServiceBL = new ServiceBL();
        ServiceOrderBL _oServiceOrderBL = new ServiceOrderBL();

        List<Categoria> ListaCategoria = new List<Categoria>();
        List<Categoria> ListaCategoriaTemp = new List<Categoria>();
        ServiceOrderBL _objServicOrderBL = new ServiceOrderBL();

        List<ListaTemporalComponentesCotizacion> ListaTemporalComponentesCotizacion_List = new List<ListaTemporalComponentesCotizacion>();
        List<ListaTemporalComponentesCotizacion> ListaTemporalComponentesCotizacion_ListOtros = new List<ListaTemporalComponentesCotizacion>();

        List<ListaTemporalComponentesCotizacion> ListaTemporalComponentesCotizacion_List_Temp = new List<ListaTemporalComponentesCotizacion>();
        List<ListaTemporalComponentesCotizacion> ListaTemporalComponentesCotizacion_List_Temp_Otros = new List<ListaTemporalComponentesCotizacion>();

        private int Numerador = 1;
        private string CotizacionId;
        private string CotizacionIdEdit;
        private string mode;
        private int userInsert;
        private DateTime d_DateInsert;
        public frmCotizacionOc(string CotizacionId_, string mode_)
        {
            CotizacionIdEdit = CotizacionId_;
            mode = mode_;
            InitializeComponent();
        }

        private void frmCotizacionOc_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            if (checkBox1.Checked == true)
            {
                txtEmpresaManual.Enabled = true;
                cbEmpresaCliente.Enabled = false;
            }
            else
            {
                txtEmpresaManual.Enabled = false;
                cbEmpresaCliente.Enabled = true;
            }

            var ListServiceComponent = objServiceBL.GetAllComponentsNewOcupacional(ref objOperationResult, null, "");
            ListaCategoria = ListServiceComponent;
            gdDataExamsNew.DataSource = ListServiceComponent;

            Utils.LoadDropDownList(cbTipoExamen, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 118, null), DropDownListAction.Select);

            if (mode == "New")
            {
                
                Utils.LoadDropDownList(cbEmpresaCliente, "Value1", "Id", BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"))), DropDownListAction.Select);
                cbEmpresaCliente.SelectedValue = "-1";

                Utils.LoadDropDownList(ddlStatusOrderServiceId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 194, null), DropDownListAction.Select);
                ddlStatusOrderServiceId.SelectedValue = ((int)Common.ServiceOrderStatus.Iniciado).ToString();

                Utils.LoadDropDownList(cbLineaCredito, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 122, null), DropDownListAction.Select);
                cbLineaCredito.SelectedValue = "2";

            }
            else if (mode == "Edit")
	        {
                Utils.LoadDropDownList(cbEmpresaCliente, "Value1", "Id", BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"))), DropDownListAction.Select);
                Utils.LoadDropDownList(cbLineaCredito, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 122, null), DropDownListAction.Select);
                Utils.LoadDropDownList(ddlStatusOrderServiceId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 194, null), DropDownListAction.Select);
                


                var CotizacionDto = _objServicOrderBL.GetCotizacionesbj(ref objOperationResult, CotizacionIdEdit);

                userInsert = CotizacionDto.i_InsertUserId;
                d_DateInsert = CotizacionDto.d_InsertDate;


                ddlStatusOrderServiceId.SelectedValue = CotizacionDto.i_MostrarPrecio == 0 ? "-1" : CotizacionDto.i_MostrarPrecio.ToString();
                cbLineaCredito.SelectedValue = CotizacionDto.i_LineaCreditoId.ToString();
                dtpDelirevy.Checked = true;
                dtpDelirevy.Value = CotizacionDto.d_DeliveryDate.Date;
                if (CotizacionDto.i_EmpresaId != null)
                {
                    cbEmpresaCliente.SelectedValue = CotizacionDto.i_EmpresaId == null ? "-1" : string.Format("{0}|{1}", CotizacionDto.i_EmpresaId, CotizacionDto.i_Locationd); ;
                    checkBox1.Checked = false;
                }
                else
                {
                    checkBox1.Checked = true;
                    txtEmpresaManual.Text = CotizacionDto.v_RazonSocial;
                }

                txtContact.Text = CotizacionDto.v_RepresentanteLegal;
                txtAdress.Text = CotizacionDto.v_DireccionEmpresa;
                txtCodificacion.Text = CotizacionDto.v_Sumilla;
                txtContactAsunto.Text = CotizacionDto.v_Asunto;
                txtNroTrabajadores.Text = CotizacionDto.i_NumberOfWorker.ToString();
                txtComentary.Text = CotizacionDto.v_Description;
                CotizacionId = CotizacionDto.i_CotizacionIdOc;
                textBox2.Text = CotizacionDto.v_Interconsultas;
                textBox3.Text = CotizacionDto.v_Anotaciones;

                var CotizacionList = _objServicOrderBL.GetCotizacionesListTemp(ref objOperationResult, CotizacionIdEdit);
                
                ListaTemporalComponentesCotizacion_List = CotizacionList.FindAll(p => p.TipoExamenId != 0);
                grdData1.DataSource = ListaTemporalComponentesCotizacion_List;

                ListaTemporalComponentesCotizacion_ListOtros = CotizacionList.FindAll(p => p.TipoExamenId == 0);
                grdData2.DataSource = ListaTemporalComponentesCotizacion_ListOtros;

            }
            else if (mode == "Clone")
            {
                Utils.LoadDropDownList(cbEmpresaCliente, "Value1", "Id", BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"))), DropDownListAction.Select);
                Utils.LoadDropDownList(cbLineaCredito, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 122, null), DropDownListAction.Select);
                Utils.LoadDropDownList(ddlStatusOrderServiceId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 194, null), DropDownListAction.Select);



                var CotizacionDto = _objServicOrderBL.GetCotizacionesClonebj(ref objOperationResult, CotizacionIdEdit);

                userInsert = CotizacionDto.i_InsertUserId;
                d_DateInsert = CotizacionDto.d_InsertDate;


                ddlStatusOrderServiceId.SelectedValue = CotizacionDto.i_MostrarPrecio == 0 ? "-1" : CotizacionDto.i_MostrarPrecio.ToString();
                cbLineaCredito.SelectedValue = CotizacionDto.i_LineaCreditoId.ToString();
                dtpDelirevy.Checked = true;
                dtpDelirevy.Value = CotizacionDto.d_DeliveryDate.Date;
                if (CotizacionDto.i_EmpresaId != null)
                {
                    cbEmpresaCliente.SelectedValue = CotizacionDto.i_EmpresaId == null ? "-1" : string.Format("{0}|{1}", CotizacionDto.i_EmpresaId, CotizacionDto.i_Locationd); ;
                    checkBox1.Checked = false;
                }
                else
                {
                    checkBox1.Checked = true;
                    txtEmpresaManual.Text = CotizacionDto.v_RazonSocial;
                }

                txtContact.Text = CotizacionDto.v_RepresentanteLegal;
                txtAdress.Text = CotizacionDto.v_DireccionEmpresa;
                txtCodificacion.Text = CotizacionDto.v_Sumilla;
                txtContactAsunto.Text = CotizacionDto.v_Asunto;
                txtNroTrabajadores.Text = CotizacionDto.i_NumberOfWorker.ToString();
                txtComentary.Text = CotizacionDto.v_Description;
                CotizacionId = CotizacionDto.i_CotizacionIdOc;
                textBox2.Text = CotizacionDto.v_Interconsultas;
                textBox3.Text = CotizacionDto.v_Anotaciones;

                var CotizacionList = _objServicOrderBL.GetCotizacionesListTemp(ref objOperationResult, CotizacionIdEdit);

                ListaTemporalComponentesCotizacion_List = CotizacionList.FindAll(p => p.TipoExamenId != 0);
                grdData1.DataSource = ListaTemporalComponentesCotizacion_List;

                ListaTemporalComponentesCotizacion_ListOtros = CotizacionList.FindAll(p => p.TipoExamenId == 0);
                grdData2.DataSource = ListaTemporalComponentesCotizacion_ListOtros;
            }
            
        }

        private void gdDataExamsNew_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            txtExamen.Text = gdDataExamsNew.Selected.Rows[0].Cells["v_ComponentName"].Value.ToString();
            txtPrecio.Text = gdDataExamsNew.Selected.Rows[0].Cells["r_Price"].Value.ToString();
        }

        private void gdDataExamsNew_DoubleClickCell(object sender, Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs e)
        {
            //txtExamen.Text = gdDataExamsNew.Selected.Rows[0].Cells["v_ComponentName"].Value.ToString();
            //txtPrecio.Text = gdDataExamsNew.Selected.Rows[0].Cells["r_Price"].Value.ToString();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                ListaCategoriaTemp = new List<Categoria>(ListaCategoria.Where(p => p.v_ComponentName.Contains(textBox1.Text)));

                gdDataExamsNew.DataSource = ListaCategoriaTemp;

            }
            else
            {
                gdDataExamsNew.DataSource = ListaCategoria;
            }
        }

        private void btnAgregarEmpresaContrata_Click(object sender, EventArgs e)
        {
            var frm = new frmEmpresa();
            frm.ShowDialog();
            OperationResult objOperationResult = new OperationResult();

            Utils.LoadDropDownList(cbEmpresaCliente, "Value1", "Id", BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"))), DropDownListAction.Select);

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtEmpresaManual.Enabled = true;
                cbEmpresaCliente.Enabled = false;
            }
            else
            {
                txtEmpresaManual.Enabled = false;
                cbEmpresaCliente.Enabled = true;
            }
        }

        private void cbEmpresaCliente_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbEmpresaCliente.SelectedValue != null && cbEmpresaCliente.SelectedValue.ToString() != "-1")
            {
                string idEmpresa = cbEmpresaCliente.SelectedValue.ToString().Split('|')[0];
                var empresaDto = new ProtocolBL().GetEmpresaByEmpresaId(idEmpresa);
                txtContact.Text = empresaDto.Representante;
                txtAdress.Text = empresaDto.Direccion;
                lblIdEmpresa.Text = empresaDto.EmpresaId;
            }
           
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            List<ListaTemporalComponentesCotizacion> ListaTemporalComponentesCotizacion_List_ = new List<ListaTemporalComponentesCotizacion>();
            grdData1.DataSource = ListaTemporalComponentesCotizacion_List_;

            ListaTemporalComponentesCotizacion ListaTemporalComponentesCotizacionObj = new ListaTemporalComponentesCotizacion();
            

            ListaTemporalComponentesCotizacionObj.ComponentId = gdDataExamsNew.Selected.Rows[0].Cells["v_ComponentId"].Value.ToString();
            ListaTemporalComponentesCotizacionObj.Examen = gdDataExamsNew.Selected.Rows[0].Cells["v_ComponentName"].Value.ToString();
            ListaTemporalComponentesCotizacionObj.TipoExamenId = int.Parse(cbTipoExamen.SelectedValue.ToString());
            ListaTemporalComponentesCotizacionObj.Tipo_Examen = cbTipoExamen.Text;
            ListaTemporalComponentesCotizacionObj.Precio = float.Parse(txtPrecio.Text);
            ListaTemporalComponentesCotizacionObj.NumeroTemp = Numerador;
            ListaTemporalComponentesCotizacion_List.Add(ListaTemporalComponentesCotizacionObj);
            Numerador++;
            ActualizarDataSource();

        }

        private void ActualizarDataSource()
        {

            grdData1.DataSource = ListaTemporalComponentesCotizacion_List;
            
        }

        private void ActualizarDataSourceOtros()
        {

            grdData2.DataSource = ListaTemporalComponentesCotizacion_ListOtros;

        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (mode == "New")
            {

                foreach (var item in ListaTemporalComponentesCotizacion_List)
                {
                    var Elminar = grdData1.Selected.Rows[0].Cells["NumeroTemp"].Value.ToString();

                    if (int.Parse(Elminar) == item.NumeroTemp)
                    {
                        List<ListaTemporalComponentesCotizacion> ListaTemporalComponentesCotizacion_List_ =
                            new List<ListaTemporalComponentesCotizacion>();
                        grdData1.DataSource = ListaTemporalComponentesCotizacion_List_;

                        ListaTemporalComponentesCotizacion_List.Remove(item);
                        ActualizarDataSource();
                        break;

                    }
                }
            }
            else if (mode == "Edit")
            {
                foreach (var item in ListaTemporalComponentesCotizacion_List)
                {
                    if (grdData1.Selected.Rows[0].Cells["i_CotizacionIdOcDetalle"].Value.ToString() == null)
	                {
                        var Elminar = grdData1.Selected.Rows[0].Cells["NumeroTemp"].Value.ToString();

                        if (int.Parse(Elminar) == item.NumeroTemp)
                        {
                            List<ListaTemporalComponentesCotizacion> ListaTemporalComponentesCotizacion_List_ =
                                new List<ListaTemporalComponentesCotizacion>();
                            grdData1.DataSource = ListaTemporalComponentesCotizacion_List_;
                            item.i_IsDeleted = 0;
                            ListaTemporalComponentesCotizacion_List_Temp.Add(item);
                            ListaTemporalComponentesCotizacion_List.Remove(item);
                            ActualizarDataSource();
                            break;

                        }
	                }
                    else if (grdData1.Selected.Rows[0].Cells["i_CotizacionIdOcDetalle"].Value.ToString() == item.i_CotizacionIdOcDetalle)
                    {
                        ListaTemporalComponentesCotizacion cotizacionocdetaillDto_Obj = new ListaTemporalComponentesCotizacion();
                        cotizacionocdetaillDto_Obj.ComponentId = item.ComponentId;
                        cotizacionocdetaillDto_Obj.TipoExamenId = int.Parse(item.TipoExamenId.ToString());
                        cotizacionocdetaillDto_Obj.Precio = float.Parse(item.Precio.ToString());
                        cotizacionocdetaillDto_Obj.i_CotizacionIdOc = item.i_CotizacionIdOc;
                        cotizacionocdetaillDto_Obj.i_IsDeleted = 1;
                        cotizacionocdetaillDto_Obj.i_CotizacionIdOcDetalle = item.i_CotizacionIdOcDetalle;

                        ListaTemporalComponentesCotizacion_List_Temp.Add(cotizacionocdetaillDto_Obj);


                            List<ListaTemporalComponentesCotizacion> ListaTemporalComponentesCotizacion_List_ =
                                new List<ListaTemporalComponentesCotizacion>();

                            grdData1.DataSource = ListaTemporalComponentesCotizacion_List_;
                            item.i_IsDeleted = 0;
                            
                            ListaTemporalComponentesCotizacion_List.Remove(item);
                            ActualizarDataSource();
                            break;
                    }                   
                }
            }
            else if (mode == "Clone")
            {

                foreach (var item in ListaTemporalComponentesCotizacion_List)
                {
                    var Elminar = grdData1.Selected.Rows[0].Cells["NumeroTemp"].Value.ToString();

                    if (int.Parse(Elminar) == item.NumeroTemp)
                    {
                        List<ListaTemporalComponentesCotizacion> ListaTemporalComponentesCotizacion_List_ =
                            new List<ListaTemporalComponentesCotizacion>();
                        grdData1.DataSource = ListaTemporalComponentesCotizacion_List_;

                        ListaTemporalComponentesCotizacion_List.Remove(item);
                        ActualizarDataSource();
                        break;

                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            cotizacionocDto cotizacionocDto_ = new cotizacionocDto();
            if (checkBox1.Checked == false)
            {
                cotizacionocDto_.i_EmpresaId = cbEmpresaCliente.SelectedValue.ToString().Split('|')[0];
                cotizacionocDto_.v_RazonSocial = cbEmpresaCliente.Text;
            }
            else
            {
                cotizacionocDto_.v_RazonSocial = txtEmpresaManual.Text;
                cotizacionocDto_.i_EmpresaId = "-1";
            }

            cotizacionocDto_.v_Description = txtComentary.Text;
            if (chkNTrabajadores.Checked != true)
            {
                cotizacionocDto_.i_NumberOfWorker = int.Parse(txtNroTrabajadores.Text);
            }
            else
            {
                cotizacionocDto_.i_NumberOfWorker = 0;
            }

            float sum = 0;
            

            if (dtpDelirevy.Checked == false)
            {
                cotizacionocDto_.d_DeliveryDate = DateTime.Now;
            }
            else
            {
                cotizacionocDto_.d_DeliveryDate = dtpDelirevy.Value;
            }

            cotizacionocDto_.i_LineaCreditoId = int.Parse(cbLineaCredito.SelectedValue.ToString());
            cotizacionocDto_.i_EMOType = -1;
            cotizacionocDto_.i_MostrarPrecio = -1;
            cotizacionocDto_.v_RepresentanteLegal = txtContact.Text;
            cotizacionocDto_.v_DireccionEmpresa = txtAdress.Text;
            cotizacionocDto_.v_Sumilla = txtCodificacion.Text;
            cotizacionocDto_.v_Asunto = txtContactAsunto.Text;
            cotizacionocDto_.i_MostrarPrecio = int.Parse(ddlStatusOrderServiceId.SelectedValue.ToString());
            cotizacionocDto_.v_Interconsultas = textBox2.Text;
            cotizacionocDto_.v_Anotaciones = textBox3.Text;

            if (mode == "New")
            {
                ListaTemporalComponentesCotizacion_List.AddRange(ListaTemporalComponentesCotizacion_ListOtros);


                foreach (var item in ListaTemporalComponentesCotizacion_List)
                {
                    sum += float.Parse(item.Precio.ToString());
                }

                cotizacionocDto_.r_TotalCost = sum;

                List<cotizacionocdetaillDto> cotizacionocdetaillDto_List = new List<cotizacionocdetaillDto>();
                foreach (var item in ListaTemporalComponentesCotizacion_List)
                {
                    cotizacionocdetaillDto cotizacionocdetaillDto_Obj = new cotizacionocdetaillDto();
                    cotizacionocdetaillDto_Obj.v_ComponentId = item.ComponentId;
                    cotizacionocdetaillDto_Obj.i_EMOTypeD = int.Parse(item.TipoExamenId.ToString());
                    cotizacionocdetaillDto_Obj.r_Price = float.Parse(item.Precio.ToString());
                    cotizacionocdetaillDto_Obj.DescripcionOtros = item.DescripcionOtros;
                    cotizacionocdetaillDto_List.Add(cotizacionocdetaillDto_Obj);
                }

                OperationResult objOperationResult = new OperationResult();
                CotizacionId = _oServiceOrderBL.AddCotizacionOc(ref objOperationResult, cotizacionocDto_,
                    cotizacionocdetaillDto_List, Globals.ClientSession.GetAsList());
                if (CotizacionId == "(No generado)")
                {
                    MessageBox.Show("Registro no Guardado, verifique campos al llenar", "ERROR!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show("¡Guardado exitosamente!", "CONFORME", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    //this.Close();
                }
            }
            else if (mode == "Edit")
            {
                


                foreach (var item in ListaTemporalComponentesCotizacion_List)
                {
                    sum += float.Parse(item.Precio.ToString());
                }

                ListaTemporalComponentesCotizacion_List.AddRange(ListaTemporalComponentesCotizacion_ListOtros);
                ListaTemporalComponentesCotizacion_List_Temp.AddRange(ListaTemporalComponentesCotizacion_List_Temp_Otros);
                ListaTemporalComponentesCotizacion_List.AddRange(ListaTemporalComponentesCotizacion_List_Temp);


                cotizacionocDto_.i_CotizacionIdOc = CotizacionIdEdit;
                cotizacionocDto_.r_TotalCost = sum;

                cotizacionocDto_.i_InsertUserId = userInsert;
                cotizacionocDto_.d_InsertDate = d_DateInsert;

                List<cotizacionocdetaillDto> cotizacionocdetaillDto_List = new List<cotizacionocdetaillDto>();
                foreach (var item in ListaTemporalComponentesCotizacion_List)
                {
                    cotizacionocdetaillDto cotizacionocdetaillDto_Obj = new cotizacionocdetaillDto();
                    cotizacionocdetaillDto_Obj.v_ComponentId = item.ComponentId;
                    cotizacionocdetaillDto_Obj.i_EMOTypeD = int.Parse(item.TipoExamenId.ToString());
                    cotizacionocdetaillDto_Obj.r_Price = float.Parse(item.Precio.ToString());
                    cotizacionocdetaillDto_Obj.i_CotizacionIdOc = item.i_CotizacionIdOc;
                    cotizacionocdetaillDto_Obj.i_IsDeleted = item.i_IsDeleted;
                    cotizacionocdetaillDto_Obj.i_CotizacionIdOcDetalle = item.i_CotizacionIdOcDetalle;
                    cotizacionocdetaillDto_Obj.DescripcionOtros = item.DescripcionOtros;

                    cotizacionocdetaillDto_List.Add(cotizacionocdetaillDto_Obj);
                }

                OperationResult objOperationResult = new OperationResult();
                _oServiceOrderBL.UpdateCotizacion(ref objOperationResult, cotizacionocDto_,
                    cotizacionocdetaillDto_List, Globals.ClientSession.GetAsList());
                //if (CotizacionId == "(No generado)")
                //{
                //    MessageBox.Show("Registro no Guardado, verifique campos al llenar", "ERROR!", MessageBoxButtons.OK,
                //        MessageBoxIcon.Error);
                //    return;
                //}
                //else
                //{
                    MessageBox.Show("¡Guardado exitosamente!", "CONFORME", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    //this.Close();
                //}
            }
            else if (mode == "Clone")
            {
                ListaTemporalComponentesCotizacion_List.AddRange(ListaTemporalComponentesCotizacion_ListOtros);

                foreach (var item in ListaTemporalComponentesCotizacion_List)
                {
                    sum += float.Parse(item.Precio.ToString());
                }

                cotizacionocDto_.r_TotalCost = sum;

                List<cotizacionocdetaillDto> cotizacionocdetaillDto_List = new List<cotizacionocdetaillDto>();
                foreach (var item in ListaTemporalComponentesCotizacion_List)
                {
                    cotizacionocdetaillDto cotizacionocdetaillDto_Obj = new cotizacionocdetaillDto();
                    cotizacionocdetaillDto_Obj.v_ComponentId = item.ComponentId;
                    cotizacionocdetaillDto_Obj.i_EMOTypeD = int.Parse(item.TipoExamenId.ToString());
                    cotizacionocdetaillDto_Obj.r_Price = float.Parse(item.Precio.ToString());
                    cotizacionocdetaillDto_Obj.DescripcionOtros = item.DescripcionOtros;

                    cotizacionocdetaillDto_List.Add(cotizacionocdetaillDto_Obj);
                }

                OperationResult objOperationResult = new OperationResult();
                CotizacionId = _oServiceOrderBL.AddCotizacionOc(ref objOperationResult, cotizacionocDto_,
                    cotizacionocdetaillDto_List, Globals.ClientSession.GetAsList());
                if (CotizacionId == "(No generado)")
                {
                    MessageBox.Show("Registro no Guardado, verifique campos al llenar", "ERROR!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show("¡Guardado exitosamente!", "CONFORME", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    mode = "Edit";

                    //this.Close();
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            CotizacionocDtoList CotizacionDto = new CotizacionocDtoList();
            List<CotizacionocdetaillList> CotizacionList = new List<CotizacionocdetaillList>();

            int tipoReport = 1;
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;
                var MedicalCenter = new ServiceBL().GetInfoMedicalCenterSede();
                CotizacionDto = _objServicOrderBL.GetCotizacionesbj(ref objOperationResult, CotizacionId);
                CotizacionList = _objServicOrderBL.GetCotizacionesListabj(ref objOperationResult, CotizacionId);

                string ruta = Common.Utils.GetApplicationConfigValue("rutaCotizacion").ToString();

                CotizacionesOc.CrearCotizacionesOc(tipoReport, CotizacionList, CotizacionDto, MedicalCenter, ruta + CotizacionId + "_" + tipoReport + ".pdf");
                this.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            CotizacionocDtoList CotizacionDto = new CotizacionocDtoList();
            List<CotizacionocdetaillList> CotizacionList = new List<CotizacionocdetaillList>();

            int tipoReport = 2;
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;
                var MedicalCenter = new ServiceBL().GetInfoMedicalCenterSede();
                CotizacionDto = _objServicOrderBL.GetCotizacionesbj(ref objOperationResult, CotizacionId);
                CotizacionList = _objServicOrderBL.GetCotizacionesListabj(ref objOperationResult, CotizacionId);

                string ruta = Common.Utils.GetApplicationConfigValue("rutaCotizacion").ToString();

                CotizacionesOc.CrearCotizacionesOc(tipoReport, CotizacionList, CotizacionDto, MedicalCenter, ruta + CotizacionId + "_" + tipoReport + ".pdf");
                this.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            CotizacionocDtoList CotizacionDto = new CotizacionocDtoList();
            List<CotizacionocdetaillList> CotizacionList = new List<CotizacionocdetaillList>();

            int tipoReport = 3;
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;
                var MedicalCenter = new ServiceBL().GetInfoMedicalCenterSede();
                CotizacionDto = _objServicOrderBL.GetCotizacionesbj(ref objOperationResult, CotizacionId);
                CotizacionList = _objServicOrderBL.GetCotizacionesListabj(ref objOperationResult, CotizacionId);

                string ruta = Common.Utils.GetApplicationConfigValue("rutaCotizacion").ToString();

                CotizacionesOc.CrearCotizacionesOc(tipoReport, CotizacionList, CotizacionDto, MedicalCenter, ruta + CotizacionId + "_" + tipoReport + ".pdf");
                this.Enabled = true;
            }
        }

        private void chkNTrabajadores_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNTrabajadores.Checked == true)
            {
                txtNroTrabajadores.Enabled = false;
                txtNroTrabajadores.Text = "0";
            }
            else
            {
                txtNroTrabajadores.Enabled = true;
                txtNroTrabajadores.Text = "1";
            }
        }

        private void btnAgregarOtros_Click(object sender, EventArgs e)
        {
            List<ListaTemporalComponentesCotizacion> ListaTemporalComponentesCotizacion_List_ = new List<ListaTemporalComponentesCotizacion>();
            grdData2.DataSource = ListaTemporalComponentesCotizacion_List_;

            ListaTemporalComponentesCotizacion ListaTemporalComponentesCotizacionObj = new ListaTemporalComponentesCotizacion();

            //ListaTemporalComponentesCotizacionObj.ComponentId = gdDataExamsNew.Selected.Rows[0].Cells["v_ComponentId"].Value.ToString();
            ListaTemporalComponentesCotizacionObj.DescripcionOtros = txtExamenOtros.Text;
            ListaTemporalComponentesCotizacionObj.TipoExamenId = 0;
            ListaTemporalComponentesCotizacionObj.Tipo_Examen = "OTROS";
            ListaTemporalComponentesCotizacionObj.Precio = float.Parse(txtPrecioOtros.Text);
            ListaTemporalComponentesCotizacionObj.NumeroTemp = Numerador;
            ListaTemporalComponentesCotizacion_ListOtros.Add(ListaTemporalComponentesCotizacionObj);
            Numerador++;
            ActualizarDataSourceOtros();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (mode == "New")
            {

                foreach (var item in ListaTemporalComponentesCotizacion_ListOtros)
                {
                    var Elminar = grdData2.Selected.Rows[0].Cells["NumeroTemp"].Value.ToString();

                    if (int.Parse(Elminar) == item.NumeroTemp)
                    {
                        List<ListaTemporalComponentesCotizacion> ListaTemporalComponentesCotizacion_List_ =
                            new List<ListaTemporalComponentesCotizacion>();
                        grdData2.DataSource = ListaTemporalComponentesCotizacion_List_;

                        ListaTemporalComponentesCotizacion_ListOtros.Remove(item);
                        ActualizarDataSourceOtros();
                        break;

                    }
                }
            }
            else if (mode == "Edit")
            {
                foreach (var item in ListaTemporalComponentesCotizacion_ListOtros)
                {
                    if (grdData2.Selected.Rows[0].Cells["i_CotizacionIdOcDetalle"].Value.ToString() == null)
                    {
                        var Elminar = grdData2.Selected.Rows[0].Cells["NumeroTemp"].Value.ToString();

                        if (int.Parse(Elminar) == item.NumeroTemp)
                        {
                            List<ListaTemporalComponentesCotizacion> ListaTemporalComponentesCotizacion_List_ =
                                new List<ListaTemporalComponentesCotizacion>();
                            grdData2.DataSource = ListaTemporalComponentesCotizacion_List_;
                            item.i_IsDeleted = 0;
                            ListaTemporalComponentesCotizacion_List_Temp_Otros.Add(item);
                            ListaTemporalComponentesCotizacion_ListOtros.Remove(item);
                            ActualizarDataSourceOtros();
                            break;

                        }
                    }
                    else if (grdData2.Selected.Rows[0].Cells["i_CotizacionIdOcDetalle"].Value.ToString() == item.i_CotizacionIdOcDetalle)
                    {
                        ListaTemporalComponentesCotizacion cotizacionocdetaillDto_Obj = new ListaTemporalComponentesCotizacion();
                        //cotizacionocdetaillDto_Obj.ComponentId = item.ComponentId;
                        cotizacionocdetaillDto_Obj.DescripcionOtros = item.DescripcionOtros;
                        cotizacionocdetaillDto_Obj.TipoExamenId = int.Parse(item.TipoExamenId.ToString());
                        cotizacionocdetaillDto_Obj.Precio = float.Parse(item.Precio.ToString());
                        cotizacionocdetaillDto_Obj.i_CotizacionIdOc = item.i_CotizacionIdOc;
                        cotizacionocdetaillDto_Obj.i_IsDeleted = 1;
                        cotizacionocdetaillDto_Obj.i_CotizacionIdOcDetalle = item.i_CotizacionIdOcDetalle;

                        ListaTemporalComponentesCotizacion_List_Temp_Otros.Add(cotizacionocdetaillDto_Obj);


                        List<ListaTemporalComponentesCotizacion> ListaTemporalComponentesCotizacion_List_ =
                            new List<ListaTemporalComponentesCotizacion>();

                        grdData2.DataSource = ListaTemporalComponentesCotizacion_List_;
                        item.i_IsDeleted = 0;

                        ListaTemporalComponentesCotizacion_ListOtros.Remove(item);
                        ActualizarDataSourceOtros();
                        break;
                    }
                }
            }
            else if (mode == "Clone")
            {

                foreach (var item in ListaTemporalComponentesCotizacion_ListOtros)
                {
                    var Elminar = grdData2.Selected.Rows[0].Cells["NumeroTemp"].Value.ToString();

                    if (int.Parse(Elminar) == item.NumeroTemp)
                    {
                        List<ListaTemporalComponentesCotizacion> ListaTemporalComponentesCotizacion_List_ =
                            new List<ListaTemporalComponentesCotizacion>();
                        grdData2.DataSource = ListaTemporalComponentesCotizacion_List_;

                        ListaTemporalComponentesCotizacion_ListOtros.Remove(item);
                        ActualizarDataSourceOtros();
                        break;

                    }
                }
            }
        }

    }
}
