using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_other_links : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] != null)
        {
            hdfDept_Id.Value = "0";
            div_headTitle.InnerText = " Admin > Other Links";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Convert.ToString(Session["deprt_name"]) + " > Other Links";
            hdfDept_Id.Value = Session["DeptID"].ToString();
        }
        else
        {
            Response.Redirect("../login.aspx");
        }

        if (!IsPostBack)
        {
            //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_banner");
            //menuli.Attributes["class"] = "active";

            panelAddNew.Visible = false;
            panelView.Visible = true;

            FillLinks();
            displayGridMessage("", "");
        }
    }
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
        hdfLink_Id.Value = "";
        txtLinkNameEnglish.Text = "";
        txtLinkNameHindi.Text = "";
        txtLinkURL.Text = "";
        btnSave.Text = "Save";
        ddlUrlOpen.SelectedIndex = 0;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtLinkNameEnglish.Text.Trim() == "")
                    displayMessage("Please enter link name", "error");
                else if (txtLinkNameHindi.Text.Trim() == "")
                    displayMessage("Please enter link name", "error");
                else if (txtLinkURL.Text.Trim() == "")
                    displayMessage("Please enter link URL", "error");
                else
                {
                    string[] parameter = { "@Flag", "@DeptID", "@LinkType", "@LinkNameEnglish", "@LinkNameHindi", "@LinkURL", "@URLOpenIn" };
                    string[] value = { "Add",hdfDept_Id.Value, "Other", txtLinkNameEnglish.Text.Trim(), txtLinkNameHindi.Text.Trim(), txtLinkURL.Text.Trim(),ddlUrlOpen.SelectedValue };
                    DB_Status dbs = dba.sp_populateDataSet("SP_Links",7, parameter, value);
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

                    if (result == "Inserted")
                    {
                        displayMessage("Record successfully added", "info");
                        FillLinks();
                        hdfLink_Id.Value = "0";
                        btnSave.Text = "Save";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
                    }
                    else if (result == "AlreadyExists")
                    {
                        displayMessage("Record already exists", "error");
                    }
                }
            }
            else if (btnSave.Text == "Update")
            {
                if (txtLinkNameEnglish.Text.Trim() == "")
                    displayMessage("Please enter link name", "error");
                else if (txtLinkNameHindi.Text.Trim() == "")
                    displayMessage("Please enter link name", "error");
                else if (txtLinkURL.Text.Trim() == "")
                    displayMessage("Please enter link URL", "error");
                else
                {
                    string[] parameter = { "@Flag", "@DeptID", "@LinkId", "@LinkNameEnglish", "@LinkNameHindi", "@LinkURL", "@URLOpenIn" };
                    string[] value = { "Update",hdfDept_Id.Value, hdfLink_Id.Value, txtLinkNameEnglish.Text.Trim(), txtLinkNameHindi.Text.Trim(), txtLinkURL.Text.Trim(),ddlUrlOpen.SelectedValue };
                    DB_Status dbs = dba.sp_populateDataSet("SP_Links",7, parameter, value);
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
                    if (result == "Updated")
                    {
                        displayMessage("Record successfully updated", "info");
                        FillLinks();
                        hdfLink_Id.Value = "0";
                        btnSave.Text = "Save";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
                    }
                    else if (result == "AlreadyExists")
                    {
                        displayMessage("Record already exists", "error");
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
        hdfLink_Id.Value = "0";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void FillLinks()
    {
        try
        {
            string[] parameter = { "@Flag", "@LinkType", "@DeptID" };
            string[] value = { "LoadForAdmin", "Other",hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Links", 3, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    grdLinks.DataSource = dt;
                    grdLinks.DataBind();
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
            string linkId = (sender as LinkButton).CommandArgument;
            hdfLink_Id.Value = linkId;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@LinkId" };
            string[] value = { "LoadbyID", hdfLink_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Links", 2, parameter, value);
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
                        txtLinkNameEnglish.Text = Convert.ToString(dt.Rows[0]["LinkNameEnglish"]);
                        txtLinkNameHindi.Text = Convert.ToString(dt.Rows[0]["LinkNameHindi"]);
                        txtLinkURL.Text = Convert.ToString(dt.Rows[0]["LinkURL"]);
                        ddlUrlOpen.SelectedValue = Convert.ToString(dt.Rows[0]["URLOpenIn"]);
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
        catch (Exception)
        {
        }
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string link_id = (sender as LinkButton).CommandArgument;
            hdfLink_Id.Value = link_id;

            string[] parameter = { "@Flag", "@LinkId" };
            string[] value = { "Delete", hdfLink_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Links", 2, parameter, value);
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
            if (result == "Deleted")
            {
                hdfLink_Id.Value = "0";
                FillLinks();
            }
        }
        catch (Exception)
        {
        }
    }
    protected void cbSwitch_CheckedChanged(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        CheckBox ckkIsactive = (CheckBox)sender;
        string link_Id = ((HiddenField)ckkIsactive.Parent.FindControl("hdf_LinkIdGrd")).Value;
        string Flag = "";

        if (ckkIsactive.Checked)
        {
            Flag = "Activate";
        }
        else
        {
            Flag = "Deactivate";
        }

        string[] parameter = { "@Flag", "@LinkId" };
        string[] value = { Flag, link_Id };
        DB_Status dbs = dba.sp_populateDataSet("SP_Links", 2, parameter, value);
        if (dbs.OperationStatus.ToString() == "Success")
        {
            DataSet ds = dbs.ResultDataSet;
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string status = dt.Rows[0]["Result"].ToString();
                    if (status == "Activated")
                        displayGridMessage("Record successfully activated", "info");
                    else if (status == "Deactivated")
                        displayGridMessage("Record successfully deactivated", "info");
                }
            }
        }
    }
}