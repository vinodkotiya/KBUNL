using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_menu_management_level2 : System.Web.UI.Page
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
            else
            {
                if(Session["MenuId"].ToString()=="1"&& Session["MenuTitle"].ToString()=="Department")
                {
                    divDptListE.Visible = true;
                    divDptListH.Visible = true;
                    divMenuTitleE.Visible = false;
                    divMenuTitleH.Visible = false;
                    divLinkURL.Visible = false;
                    divURLOpen.Visible = false;
                    bindDept();
                }
                hdfMenuId.Value = Convert.ToString(Session["MenuId"]);
                hdfMenuName.Value = Convert.ToString(Session["MenuTitle"]);
                lblMenuName.Text = hdfMenuName.Value;

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
        hdfMenuLevel2Id.Value = "";
        ddlDeptE.SelectedValue="0";
        ddlDeptH.SelectedValue = "0";
        ddlUrlOpen.SelectedValue = "0";
        txtLinkURL.Text = "";
        txtMenuTitle.Text = "";
        txtMenuTitleH.Text = "";
        txtSequenceNo.Text = "";
        btnSave.Text = "Save";
    }

    protected void bindDept()
    {
        string[] parameter = { };
        string[] value = {  };
        DB_Status dbs = dba.sp_populateDataSet("Sp_Department_View", 0, parameter, value);
       
        if (dbs.OperationStatus.ToString() == "Success")
        {
            DataSet ds = dbs.ResultDataSet;
            if(ds.Tables.Count>0)
            {
                ddlDeptE.DataSource = ds;
                ddlDeptE.DataTextField = "Department";
                ddlDeptE.DataValueField = "DeptID";
                ddlDeptE.DataBind();

                ddlDeptH.DataSource = ds;
                ddlDeptH.DataTextField = "DepartmentH";
                ddlDeptH.DataValueField = "DeptID";
                ddlDeptH.DataBind();
            }
            ddlDeptE.Items.Insert(0, new ListItem("Select Department", "0"));
           ddlDeptH.Items.Insert(0, new ListItem("विभाग चुने", "0"));
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (Session["MenuId"].ToString() == "1" && Session["MenuTitle"].ToString() == "Department")
                {
                    ddlUrlOpen.SelectedValue = "Open In New Page";
                    txtLinkURL.Text="#";
                }
                 if (ddlDeptE.SelectedValue != ddlDeptH.SelectedValue)
                    displayMessage("Please choose same department", "error");
               else if (txtLinkURL.Text.Trim() == "")
                    displayMessage("Please enter link url", "error");
                else if (ddlUrlOpen.SelectedIndex == 0)
                    displayMessage("Please select url open type", "error");
                else if (txtSequenceNo.Text.Trim() == "")
                    displayMessage("Please enter sequence no", "error");
                else
                {
                    string[] parameter = { "@RID_Menu", "@MenuTitle", "@MenuTitleH", "@LinkURL", "@URLOpenIn", "@Sequence", "@DeptID" };
                    string[] value = { hdfMenuId.Value,txtMenuTitle.Text.Trim(), txtMenuTitleH.Text.Trim(), txtLinkURL.Text.Trim(), ddlUrlOpen.SelectedValue, txtSequenceNo.Text.Trim(),ddlDeptE.SelectedValue };
                    DB_Status dbs = dba.sp_populateDataSet("Sp_Menu_ManagementLevel2_Insert", 7, parameter, value);
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
                        hdfMenuLevel2Id.Value = "";
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
                //if (txtMenuTitle.Text.Trim() == "")
                //    displayMessage("Please enter menu title", "error");
                //else if (txtMenuTitleH.Text.Trim() == "")
                //    displayMessage("Please enter menu title in hindi", "error");
                if(ddlDeptE.SelectedValue!=ddlDeptH.SelectedValue)
                    displayMessage("Please choose same department", "error");
                else if (txtLinkURL.Text.Trim() == "")
                    displayMessage("Please enter link url", "error");
                else if (ddlUrlOpen.SelectedIndex == 0)
                    displayMessage("Please select url open type", "error");
                else if (txtSequenceNo.Text.Trim() == "")
                    displayMessage("Please enter sequence no", "error");
                else
                {
                    string[] parameter = { "@RID", "@RID_Menu", "@MenuTitle", "@MenuTitleH", "@LinkURL", "@URLOpenIn", "@Sequence", "@DeptID" };
                    string[] value = { hdfMenuLevel2Id.Value, hdfMenuId.Value, txtMenuTitle.Text.Trim(), txtMenuTitleH.Text.Trim(), txtLinkURL.Text.Trim(), ddlUrlOpen.SelectedValue, txtSequenceNo.Text.Trim(), ddlDeptE.SelectedValue };
                    DB_Status dbs = dba.sp_populateDataSet("Sp_Menu_ManagementLevel2_Update", 8, parameter, value);
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
                            hdfMenuLevel2Id.Value = "";
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
            displayGridMessage(ex.Message, "error");
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hdfMenuLevel2Id.Value = "";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void Fill_MenuManagement()
    {
        try
        {
            string[] parameter = { "@RID_Menu" };
            string[] value = { hdfMenuId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Menu_Management_Level2_View", 1, parameter, value);
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
        catch (Exception)
        {
        }
    }

    protected void Update_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string QuestionID = (sender as LinkButton).CommandArgument;
            hdfMenuLevel2Id.Value = QuestionID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@RID" };
            string[] value = { hdfMenuLevel2Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Menu_Management_Level2_ViewBy_ID", 1, parameter, value);
            bool flag = false;

            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        flag = true;
                        string dd = Convert.ToString(dt.Rows[0]["URLOpenIn"]);
                        if (Convert.ToInt32(dt.Rows[0]["DeptID"])>0)
                        {
                            ddlDeptE.SelectedValue = Convert.ToString(dt.Rows[0]["DeptID"]);
                            ddlDeptH.SelectedValue = Convert.ToString(dt.Rows[0]["DeptID"]);
                        }
                        hdfMenuId.Value = Convert.ToString(dt.Rows[0]["RID_Menu"]);
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
            displayMessage(ex.Message, "error");
        }
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string RID = (sender as LinkButton).CommandArgument;
            hdfMenuLevel2Id.Value = RID;

            string[] parameter = { "@RID" };
            string[] value = { hdfMenuLevel2Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Menu_Management_Level2_Delete", 1, parameter, value);
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
                hdfMenuLevel2Id.Value = "";
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

    protected void ViewList_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string MenuLevel2Id = (sender as LinkButton).CommandArgument;
            hdfMenuLevel2Id.Value = MenuLevel2Id;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            Label lblMenuLevel2Title = (Label)grdrow.FindControl("lblMenuLevel2Title");

            int rowindex = grdrow.RowIndex;
            Session["MenuId"] = hdfMenuId.Value;
            Session["MenuTitle"] = hdfMenuName.Value;
            Session["MenuLevel2Id"] = hdfMenuLevel2Id.Value;
            Session["MenuLevel2Title"] = lblMenuLevel2Title.Text;
            Response.Redirect("menu-management-level3.aspx");
        }
        catch (Exception)
        {

        }
    }

    protected void ddlDeptE_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDeptH.SelectedValue = ddlDeptE.SelectedValue;
    }
}