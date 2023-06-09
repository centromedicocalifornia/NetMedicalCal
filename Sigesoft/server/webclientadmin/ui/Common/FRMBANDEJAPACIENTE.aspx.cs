﻿using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Common;

namespace Sigesoft.Server.WebClientAdmin.UI.Common
{
    public partial class FRMBANDEJAPACIENTE : System.Web.UI.Page
    {
        PacientBL _objPacientBL = new PacientBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["strFilterExpression"] = null;
                btnNew.OnClientClick = winEditUser.GetShowReference("FRMPACIENTE.aspx?Mode=New");

                BindGrid();
            }
        }

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            grdData.RecordCount = GetTotalCount();
            var objUserList = GetData(grdData.PageIndex, grdData.PageSize, "i_SystemUserId ASC", strFilterExpression);

            if (objUserList.Count > 0)
            {
                grdData.DataSource = objUserList;
                grdData.DataBind();
            }
            else
            {
                Alert.ShowInTop("Información de operación:" + System.Environment.NewLine + "No existen registros.");
            }

        }

        private int GetTotalCount()
        {
            OperationResult objOperationResult = new OperationResult();
            string strFilterExpression = "i_IsDeleted=0";
            if (Session["strFilterExpression"] != null)
            {
                var ss = Convert.ToString(Session["strFilterExpression"]);
                if (ss.Length > 0)
                {
                    strFilterExpression += "&&" + ss;
                }

            }

            return _objPacientBL.GetPacientes(ref objOperationResult, strFilterExpression);

        }

        private List<Sigesoft.Node.WinClient.BE.PacientList> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            List<Sigesoft.Node.WinClient.BE.PacientList> objUserList = _objPacientBL.GetPacientsPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, txtUserNameFilter.Text);

            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }


            return objUserList;

        }

        protected void grdData_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            grdData.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string filter = string.Empty;
            //if (!string.IsNullOrEmpty(txtUserNameFilter.Text))
            //{
            //    filter = "v_UserName.Contains(\"" + txtUserNameFilter.Text.Trim() + "\")";
            //}

            Session["strFilterExpression"] = filter;

            // Refresh the grid
            grdData.PageIndex = 0;
            this.BindGrid();
        }

        protected void grdData_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
        }

        protected void winEditUser_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
    }
}