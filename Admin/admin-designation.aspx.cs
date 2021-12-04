using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_admin_department : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] == null)
        {
            Response.Redirect("../login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_circulars");
                //menuli.Attributes["class"] = "active";

                panelAddNew.Visible = false;
                panelView.Visible = true;

                FillDesignation();
                displayGridMessage("", "");
            }
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
        hdfDesignation_Id.Value = "0";

        txtDesignationE.Text = "";
        txtDesignationH.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtDesignationE.Text == "" || txtDesignationH.Text == "")
                    displayMessage("Please enter designation name", "error");
                else
                {
                    string[] parameter = { "@Flag", "@Designation", "@DesignationH"};
                    string[] value = { "Add", txtDesignationE.Text.Trim(), txtDesignationH.Text.Trim()};
                    DB_Status dbs = dba.sp_populateDataSet("SP_Designation", 3, parameter, value);
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
                        FillDesignation();
                        hdfDesignation_Id.Value = "0";
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
                if (txtDesignationE.Text == "" || txtDesignationH.Text == "")
                    displayMessage("Please enter designation name", "error");
                else
                {
                    string[] parameter = { "@Flag", "@DesignationID", "@Designation", "@DesignationH"};
                    string[] value = { "Update", hdfDesignation_Id.Value, txtDesignationE.Text.Trim(), txtDesignationH.Text.Trim()};
                    DB_Status dbs = dba.sp_populateDataSet("SP_Designation", 4, parameter, value);
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
                        FillDesignation();
                        hdfDesignation_Id.Value = "0";
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
        hdfDesignation_Id.Value = "0";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void FillDesignation()
    {
        try
        {
            string[] parameter = { "@Flag"};
            string[] value = { "LoadForAdmin"};
            DB_Status dbs = dba.sp_populateDataSet("SP_Designation", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    grdDesignation.DataSource = dt;
                    grdDesignation.DataBind();
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
            string DesignationId = (sender as LinkButton).CommandArgument;
            hdfDesignation_Id.Value = DesignationId;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@DesignationID" };
            string[] value = { "LoadbyID", hdfDesignation_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Designation", 2, parameter, value);
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
                        txtDesignationE.Text =Convert.ToString(dt.Rows[0]["Designation"]);
                        txtDesignationH.Text = Convert.ToString(dt.Rows[0]["DesignationH"]);
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
            string RID = (sender as LinkButton).CommandArgument;
            hdfDesignation_Id.Value = RID;

            string[] parameter = { "@Flag", "@DesignationID" };
            string[] value = { "Delete", hdfDesignation_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Designation", 2, parameter, value);
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
                hdfDesignation_Id.Value = "";
                FillDesignation();
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
        string DesignationId = ((HiddenField)ckkIsactive.Parent.FindControl("hdfDesignationIDGrd")).Value;
        string Flag = "";

        if (ckkIsactive.Checked)
        {
            Flag = "Activate";
        }
        else
        {
            Flag = "Deactivate";
        }

        string[] parameter = { "@Flag", "@DesignationID" };
        string[] value = { Flag, DesignationId.ToString() };
        DB_Status dbs = dba.sp_populateDataSet("SP_Designation", 2, parameter, value);
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
    protected void grdDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDesignation.PageIndex = e.NewPageIndex;
        FillDesignation();
    }
}