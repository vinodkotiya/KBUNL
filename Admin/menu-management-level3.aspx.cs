using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_menu_management_level3 : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] == null)
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {
            //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_addmenu");
            //menuli.Attributes["class"] = "active";

            if (Session["MenuId"] == null)
            {
                Response.Redirect("menu-management.aspx");
            }
            else if( Session["MenuLevel2Id"] ==null)
            {
                Response.Redirect("menu-management-level2.aspx");
            }
            else
            {
                hdfMenuId.Value = Convert.ToString(Session["MenuId"]);
                hdfMenuName.Value = Convert.ToString(Session["MenuTitle"]);
                hdfMenuLevel2Id.Value = Convert.ToString(Session["MenuLevel2Id"]);
                hdfMenuLevel2Name.Value = Convert.ToString(Session["MenuLevel2Title"]);
                lblMenuName.Text = hdfMenuName.Value;
                lblMenuLevel2.Text = hdfMenuLevel2Name.Value;

                panelAddNew.Visible = false;
                panelView.Visible = true;
                //Fill Articles

                Fill_MenuManagement();
                displayGridMessage("", "");
            }
            
        }
    }
    /* Add/Update/Deactivate Banner*/
    protected void displayMessage(string msg, string msgtype)
    {
        lblMsg.Text = msg;
        if (msgtype == "error")
            alert.Attributes["class"] = "alert alert-error";
        else if (msgtype == "info")
            alert.Attributes["class"] = "alert alert-info";
        else
            alert.Attributes["class"] = "";
    }
    protected void displayGridMessage(string msg, string msgtype)
    {
        lblgridmsg.Text = msg;
        if (msgtype == "error")
            alertgrid.Attributes["class"] = "alert alert-error";
        else if (msgtype == "info")
            alertgrid.Attributes["class"] = "alert alert-info";
        else
            alertgrid.Attributes["class"] = "";
    }
    protected void lbtn_AddNew_Click(object sender, EventArgs e)
    {
        panelAddNew.Visible = true;
        panelView.Visible = false;

        displayMessage("", "");
        displayGridMessage("", "");
        hdfMenuLevel3Id.Value = "0";

        txtLinkURL.Text = "";
        txtMenuTitle.Text = "";
        txtMenuTitleH.Text = "";
        txtSequenceNo.Text = "";
        ddlUrlOpen.SelectedIndex = 0;
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtMenuTitle.Text.Trim() == "")
                    displayMessage("Please enter menu title", "error");
                else if (txtMenuTitleH.Text.Trim() == "")
                    displayMessage("Please enter menu title in hindi", "error");
                else if (txtLinkURL.Text.Trim() == "")
                    displayMessage("Please enter link url", "error");
                else if (ddlUrlOpen.SelectedIndex == 0)
                    displayMessage("Please select url open type", "error");
                else if (txtSequenceNo.Text.Trim() == "")
                    displayMessage("Please enter sequence no", "error");
                else
                {
                    string[] parameter = { "@RID_Menu", "@RID_Menu_Level2", "@MenuTitle", "@MenuTitleH", "@LinkURL", "@URLOpenIn", "@Sequence", };
                    string[] value = { hdfMenuId.Value,hdfMenuLevel2Id.Value ,txtMenuTitle.Text.Trim(), txtMenuTitleH.Text.Trim(), txtLinkURL.Text.Trim(), ddlUrlOpen.SelectedValue, txtSequenceNo.Text.Trim() };
                    DB_Status dbs = dba.sp_populateDataSet("Sp_Menu_ManagementLevel3_Insert", 7, parameter, value);
                    string result = "";
                    if (dbs.OperationStatus.ToString() == "Success")
                    {
                        DataSet ds = dbs.ResultDataSet;
                        if (ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                result = dt.Rows[0]["Result"].ToString();
                            }
                        }
                    }

                    if (result == "success")
                    {
                        displayMessage("Record successfully added", "info");
                        Fill_MenuManagement();
                        hdfMenuLevel3Id.Value = "0";
                        txtMenuTitle.Text = "";
                        txtMenuTitleH.Text = "";
                        txtSequenceNo.Text = "";
                        ddlUrlOpen.SelectedIndex = 0;
                        txtLinkURL.Text = "";
                        btnSave.Text = "Save";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
                    }
                    else
                    {
                        displayMessage("Record already exists", "error");
                    }
                }
            }
            else if (btnSave.Text == "Update")
            {
                if (txtMenuTitle.Text.Trim() == "")
                    displayMessage("Please enter menu title", "error");
                else if (txtMenuTitleH.Text.Trim() == "")
                    displayMessage("Please enter menu title in hindi", "error");
                else if (txtLinkURL.Text.Trim() == "")
                    displayMessage("Please enter link url", "error");
                else if (ddlUrlOpen.SelectedIndex == 0)
                    displayMessage("Please select url open type", "error");
                else if (txtSequenceNo.Text.Trim() == "")
                    displayMessage("Please enter sequence no", "error");
                else
                {
                    string[] parameter = { "@RID", "@RID_Menu", "@RID_Menu_Level2", "@MenuTitle", "@MenuTitleH", "@LinkURL", "@URLOpenIn", "@Sequence", };
                    string[] value = { hdfMenuLevel3Id.Value, hdfMenuId.Value,hdfMenuLevel2Id.Value ,txtMenuTitle.Text.Trim(), txtMenuTitleH.Text.Trim(), txtLinkURL.Text.Trim(), ddlUrlOpen.SelectedValue, txtSequenceNo.Text.Trim() };
                    DB_Status dbs = dba.sp_populateDataSet("Sp_Menu_ManagementLevel3_Update", 8, parameter, value);
                    string result = "";
                    if (dbs.OperationStatus.ToString() == "Success")
                    {
                        DataSet ds = dbs.ResultDataSet;
                        if (ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                result = dt.Rows[0]["Result"].ToString();
                            }
                        }
                        if (result == "success")
                        {
                            displayMessage("Record successfully updated", "info");
                            Fill_MenuManagement();
                            hdfMenuLevel3Id.Value = "0";
                            txtMenuTitle.Text = "";
                            txtMenuTitleH.Text = "";
                            txtSequenceNo.Text = "";
                            txtLinkURL.Text = "";
                            ddlUrlOpen.SelectedIndex = 0;
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;
                        }
                        else
                        {
                            displayMessage("Record already exists", "error");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            displayMessage(ex.Message, "error");
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hdfMenuLevel3Id.Value = "0";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void Fill_MenuManagement()
    {
        try
        {
            string[] parameter = { "@RID_Menu", "@RID_Menu_Level2" };
            string[] value = { hdfMenuId.Value,hdfMenuLevel2Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Menu_Management_Level3_View", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    grdNotice.DataSource = dt;
                    grdNotice.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void Update_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string QuestionID = (sender as LinkButton).CommandArgument;
            hdfMenuLevel3Id.Value = QuestionID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@RID" };
            string[] value = { hdfMenuLevel3Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Menu_Management_Level3_ViewBy_ID", 1, parameter, value);
            bool flag = false;

            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        string dd= Convert.ToString(dt.Rows[0]["URLOpenIn"]);
                        flag = true;
                        txtMenuTitle.Text = Convert.ToString(dt.Rows[0]["MenuTitle"]);
                        txtMenuTitleH.Text = Convert.ToString(dt.Rows[0]["MenuTitleH"]);
                        ddlUrlOpen.SelectedValue = Convert.ToString(dt.Rows[0]["URLOpenIn"]);
                        txtLinkURL.Text = Convert.ToString(dt.Rows[0]["LinkURL"]);
                        txtSequenceNo.Text = Convert.ToString(dt.Rows[0]["Sequence"]);
                    }
                }
            }
            if (flag)
            {
                panelAddNew.Visible = true;
                panelView.Visible = false;

                displayMessage("", "");
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            displayGridMessage(ex.Message, "error");
        }
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string RID = (sender as LinkButton).CommandArgument;
            hdfMenuLevel3Id.Value = RID;

            string[] parameter = { "@RID" };
            string[] value = { hdfMenuLevel3Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Menu_Management_Level3_Delete", 1, parameter, value);
            string result = "";

            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        result = dt.Rows[0]["Result"].ToString();
                    }
                }
            }
            if (result == "success")
            {
                hdfMenuLevel3Id.Value = "0";
                Fill_MenuManagement();
            }
        }
        catch (Exception)
        {
        }
    }

    protected void grdNotice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdNotice.PageIndex = e.NewPageIndex;
        Fill_MenuManagement();
    }

  
}