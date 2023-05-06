using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI.UserControls
{
    public partial class ucCalculoSTS : UserControl
    {
        ServiceComponentFieldValuesList _sts;
        List<ServiceComponentFieldValuesList> _liststs = new List<ServiceComponentFieldValuesList>();
        string _valueOld = String.Empty;

        private string path;
        public string PersonId { get; set; }
        public string ServiceComponentId { get; set; }
        public string Servicio { get; set; }
        bool _isChangueValueControl = false;

        public ucCalculoSTS()
        {
            InitializeComponent();

            txtOidoDerecho.Name = Constants.txt_VA_OD;
            txtOidoIzquierdo.Name = Constants.txt_VA_OI;

            //SearchControlAndSetEvents(this);
            timer1.Start();

        }

        private void SearchControlAndSetEvents(Control ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is TextBox)
                {
                    var field = (TextBox)ctrl;
                    //var tag = (Audio)field.Tag;

                    //if (tag != null)
                    //{
                        ctrl.TextChanged += new EventHandler(txt_TextChanged);
                        ctrl.Validating += new CancelEventHandler(txt_Validating);
                        ctrl.Enter += new EventHandler(txt_Enter);
                        ((TextBox)ctrl).Leave += new EventHandler(txt_Leave); 
                    //}
                    //agrego al diccionario los textbox para tenerlo en memoria
                    //dictionary.Add(ctrl.Name, (TextBox)ctrl);
                }

                if (ctrl.HasChildren)
                    SearchControlAndSetEvents(ctrl);
            }
        }

        private void txt_TextChanged(object sender, System.EventArgs e)
        {
            TextBox senderCtrl = (TextBox)sender;
            if (senderCtrl.Text == "") senderCtrl.Text = null;
            if (!IsNumeric(senderCtrl.Text)) return;
            _sts = new ServiceComponentFieldValuesList();
            int var_rango;

            if (!IsNumeric(senderCtrl.Text)) return;

            if (senderCtrl.Text != string.Empty && !senderCtrl.Text.Contains("No Determinado"))
            {
                var res = int.TryParse(senderCtrl.Text, out var_rango);

                if (var_rango >= -10 && var_rango <= 120)
                {
                    //e.Cancel = false;
                }
                else
                {
                    //e.Cancel = true;
                    senderCtrl.Select(0, 3);
                    MessageBox.Show("El valor : " + senderCtrl.Text + " está fuera del rango [-10 - 120]. ", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                var_rango = -100;   // valor que significa vacio para el sistema
            }



            _liststs.RemoveAll(p => p.v_ComponentFieldId == senderCtrl.Name);

            _sts.v_ComponentFieldId = senderCtrl.Name;
            _sts.v_Value1 = var_rango == -100 ? string.Empty : var_rango.ToString(); // senderCtrl.Text;

            _liststs.Add(_sts);

            DataSource = _liststs;

        }


        private void txt_Validating(object sender, CancelEventArgs e)
        {
            TextBox senderCtrl = (TextBox)sender;

            if (senderCtrl.Text != "")
            {
                if (!IsNumeric(senderCtrl.Text))
                {
                    e.Cancel = true;
                    MessageBox.Show("El valor : " + senderCtrl.Text + " no es un valor numérico ", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            int var_rango;
            if (senderCtrl.Text != string.Empty)
            {
                var_rango = int.Parse(senderCtrl.Text);
            }
            else
            {
                var_rango = 0;
            }

            if (var_rango >= -10 && var_rango <= 120)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("El valor : " + senderCtrl.Text + " está fuera del rango (-10-120). ", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_Enter(object sender, System.EventArgs e)
        {
            TextBox senderCtrl = (TextBox)sender;
            _valueOld = senderCtrl.Text;
        }

        private void txt_Leave(object sender, System.EventArgs e)
        {
            TextBox senderCtrl = (TextBox)sender;
            if (_valueOld != senderCtrl.Text)
            {
                _isChangueValueControl = true;
            }
            else
            {
                _isChangueValueControl = false;
            }
        }

        public bool IsNumeric(object Expression)
        {

            bool isNum;

            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;

        }

        public List<ServiceComponentFieldValuesList> DataSource
        {
            get
            {
                return _liststs;
            }
            set
            {
                if (value != _liststs)
                {
                    ClearValueControl();
                    _liststs = value;
                    SearchControlAndFill(value);
                }
            }
        }

        public void ClearValueControl()
        {
            _isChangueValueControl = false;

            txtOidoDerecho.Text = "0";
            txtOidoIzquierdo.Text = "0";
        }

        private void SearchControlAndFill(List<ServiceComponentFieldValuesList> DataSource)
        {
            if (DataSource == null || DataSource.Count == 0) return;
            // Ordenar Lista Datasource
            var DataSourceOrdenado = DataSource.OrderBy(p => p.v_ComponentFieldId).ToList();

            // recorrer la lista que viene de la BD
            foreach (var item in DataSourceOrdenado)
            {
                var matchedFields = this.Controls.Find(item.v_ComponentFieldId, true);

                if (matchedFields.Length > 0)
                {
                    var field = matchedFields[0];

                    if (field is TextBox)
                    {
                        if (field.Name == item.v_ComponentFieldId)
                        {
                            ((TextBox)field).Text = item.v_Value1;
                        }
                    }
                }
            }
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            var datos = new ServiceBL().ObtenerCalculoSTS(Servicio);

            txt_AuB_OD_2000.Text = datos.AB_OD_2000;
            txt_AuB_OD_3000.Text = datos.AB_OD_3000;
            txt_AuB_OD_4000.Text = datos.AB_OD_4000;
            txt_AuB_OD_PROM.Text = datos.AUB_OD;

            txt_AuB_OI_2000.Text = datos.AB_OI_2000;
            txt_AuB_OI_3000.Text = datos.AB_OI_3000;
            txt_AuB_OI_4000.Text = datos.AB_OI_4000;
            txt_AuB_OI_PROM.Text = datos.AUB_OI;

            txt_AuA_OD_2000.Text = datos.AA_OD_2000;
            txt_AuA_OD_3000.Text = datos.AA_OD_3000;
            txt_AuA_OD_4000.Text = datos.AA_OD_4000;
            txt_AuA_OD_PROM.Text = datos.AUA_OD;

            txt_AuA_OI_2000.Text = datos.AA_OI_2000;
            txt_AuA_OI_3000.Text = datos.AA_OI_3000;
            txt_AuA_OI_4000.Text = datos.AA_OI_4000;
            txt_AuA_OI_PROM.Text = datos.AUA_OI;

            
            txtOidoDerecho.Text = datos.STS_OD;
            txtOidoIzquierdo.Text = datos.STS_OI;

            if (datos.STS_OD != "" && decimal.Parse(datos.STS_OD) >= 10)
            {
                txtOidoDerecho.ForeColor = Color.Red;
                txtOidoDerecho.Font = new Font(txtOidoDerecho.Font, FontStyle.Bold);
            }

            if (datos.STS_OI != "" && decimal.Parse(datos.STS_OI) >= 10)
            {
                txtOidoIzquierdo.ForeColor = Color.Red;
                txtOidoIzquierdo.Font = new Font(txtOidoIzquierdo.Font, FontStyle.Bold);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtOidoDerecho_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var datos = new ServiceBL().ObtenerCalculoSTS(Servicio);

            txt_AuB_OD_2000.Text = datos.AB_OD_2000;
            txt_AuB_OD_3000.Text = datos.AB_OD_3000;
            txt_AuB_OD_4000.Text = datos.AB_OD_4000;
            txt_AuB_OD_PROM.Text = datos.AUB_OD;

            txt_AuB_OI_2000.Text = datos.AB_OI_2000;
            txt_AuB_OI_3000.Text = datos.AB_OI_3000;
            txt_AuB_OI_4000.Text = datos.AB_OI_4000;
            txt_AuB_OI_PROM.Text = datos.AUB_OI;

            txt_AuA_OD_2000.Text = datos.AA_OD_2000;
            txt_AuA_OD_3000.Text = datos.AA_OD_3000;
            txt_AuA_OD_4000.Text = datos.AA_OD_4000;
            txt_AuA_OD_PROM.Text = datos.AUA_OD;

            txt_AuA_OI_2000.Text = datos.AA_OI_2000;
            txt_AuA_OI_3000.Text = datos.AA_OI_3000;
            txt_AuA_OI_4000.Text = datos.AA_OI_4000;
            txt_AuA_OI_PROM.Text = datos.AUA_OI;


            txtOidoDerecho.Text = datos.STS_OD;
            txtOidoIzquierdo.Text = datos.STS_OI;

            if (datos.STS_OD != "" && decimal.Parse(datos.STS_OD) >= 10)
            {
                txtOidoDerecho.ForeColor = Color.Red;
                txtOidoDerecho.Font = new Font(txtOidoDerecho.Font, FontStyle.Bold);
            }

            if (datos.STS_OI != "" && decimal.Parse(datos.STS_OI) >= 10)
            {
                txtOidoIzquierdo.ForeColor = Color.Red;
                txtOidoIzquierdo.Font = new Font(txtOidoIzquierdo.Font, FontStyle.Bold);
            }
        }

        private void ucCalculoSTS_Load(object sender, EventArgs e)
        {

        }
    }
}
