using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_application : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] != null)
        {
            hdfDept_Id.Value = "0";
            div_headTitle.InnerText = "Admin > Application";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Convert.ToString(Session["deprt_name"]) + " > Application";
            hdfDept_Id.Value = Session["DeptID"].ToString();
        }
        else
        {
            Response.Redirect("../login.aspx");
        }

        if (!IsPostBack)
        {
            //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_links");
            //menuli.Attributes["class"] = "active";

            panelAddNew.Visible = false;
            panelView.Visible = true;
            //Fill Articles
            FillApplication();
            displayGridMessage("", "");
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
        hdfApplicationId.Value = "0";
        txtApplicationNameE.Text = "";
        txtApplicationNameH.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hdfApplicationId.Value = "0";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtApplicationNameE.Text.Trim() == "")
                    displayMessage("Please enter application name", "error");
                else if (txtApplicationNameH.Text.Trim() == "")
                    displayMessage("Please enter application name", "error");
                else
                {
                        string[] parameter = { "@Flag", "@DeptID","@ApplicationNameEnglish", "@ApplicationNameHindi", "@ApplicationURL"};
                        string[] value = { "Add",hdfDept_Id.Value,txtApplicationNameE.Text.Trim(), txtApplicationNameH.Text.Trim(),txtUrl.Text.Trim()};
                        DB_Status dbs = dba.sp_populateDataSet("SP_Application", 5, parameter, value);
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
                        if (result == "AlreadyExists")
                        {
                            displayMessage("Sorry! Title already exists", "error");
                        }
                        else if (result == "Inserted")
                        {
                            FillApplication();
                            hdfApplicationId.Value = "";
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;
                        }
                    }
                }
            else if (btnSave.Text == "Update")
            {
                if (txtApplicationNameE.Text.Trim() == "")
                    displayMessage("Please enter application name", "error");
                else if (txtApplicationNameH.Text.Trim() == "")
                    displayMessage("Please enter application name", "error");
                else
                {
                        string[] parameter = { "@Flag", "@DeptID","@ApplicationId", "@ApplicationNameEnglish", "@ApplicationNameHindi", "@ApplicationURL" };
                        string[] value = { "Update",hdfDept_Id.Value, hdfApplicationId.Value, txtApplicationNameE.Text.Trim(), txtApplicationNameH.Text.Trim(), txtUrl.Text.Trim() };
                        DB_Status dbs = dba.sp_populateDataSet("SP_Application",6, parameter, value);
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
                        if (result == "AlreadyExists")
                        {
                            displayMessage("Sorry! Title already exists", "error");
                        }
                        else if (result == "Updated")
                        {

                            FillApplication();
                            hdfApplicationId.Value = "";
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;
                        }
                    }
                }
        }
        catch (Exception ex)
        {
            displayMessage(ex.Message, "error");
        }
    }

    protected void FillApplication()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "LoadForAdmin",hdfDept_Id.Value};
            DB_Status dbs = dba.sp_populateDataSet("SP_Application",2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    grdApplication.DataSource = dt;
                    grdApplication.DataBind();
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
            string ID = (sender as LinkButton).CommandArgument;
            hdfApplicationId.Value = ID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@ApplicationId" };
            string[] value = { "LoadbyID", hdfApplicationId.Value};
            DB_Status dbs = dba.sp_populateDataSet("SP_Application",2, parameter, value);
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
                        txtApplicationNameE.Text =Convert.ToString(dt.Rows[0]["ApplicationNameEnglish"]);
                        txtApplicationNameH.Text= Convert.ToString(dt.Rows[0]["ApplicationNameHindi"]);
                        txtUrl.Text = dt.Rows[0]["ApplicationURL"].ToString();
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
            string applicationId = (sender as LinkButton).CommandArgument;
            hdfApplicationId.Value = applicationId;

            string[] parameter = { "@Flag", "@ApplicationId" };
            string[] value = { "Delete", hdfApplicationId.Value};
            DB_Status dbs = dba.sp_populateDataSet("SP_Application", 2, parameter, value);
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
                hdfApplicationId.Value = "0";
                FillApplication();
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
        string AppId =((HiddenField)ckkIsactive.Parent.FindControl("hdfApplicationIdGrd")).Value;
        string Flag = "";

        if (ckkIsactive.Checked)
        {
            Flag = "Activate";
        }
        else
        {
            Flag = "Deactivate";
        }

        string[] parameter = { "@Flag", "@ApplicationId" };
        string[] value = { Flag, AppId };
        DB_Status dbs = dba.sp_populateDataSet("SP_Application", 2, parameter, value);
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