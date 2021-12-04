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
                loaddepartmentgroup();
                FillDepartment();
                displayGridMessage("", "");
            }
        }

    }
    protected void loaddepartmentgroup()
    {
        string[] param = { "@Flag" };
        string[] values = { "LoadDeptGroup" };
        DB_Status DBS = dba.sp_populateDataSet("SP_Department", 1, param, values);
        if (DBS.OperationStatus.ToString() == "Success")
        {
            DataSet ds = DBS.ResultDataSet;
            //Table index 0 for Department
            ddlDeptGroup.DataSource = ds.Tables[0];
            ddlDeptGroup.DataTextField = "DeptGroup";
            ddlDeptGroup.DataValueField = "DeptGroupID";
            ddlDeptGroup.DataBind();
            
        }
        else
        {
            displayMessage(DBS.Title, "error");
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
        hdfDepartment_Id.Value = "0";
        hdfBanner_Image.Value = "No";

        txtDepartmentE.Text = "";
        txtDepartmentH.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtDepartmentE.Text == "")
                    displayMessage("Please enter department name", "error");
                else if (txtDepartmentH.Text == "")
                    displayMessage("Please enter department name", "error");
                else if (ddlDeptGroup.SelectedValue=="0")
                    displayMessage("Please select department group/reporting to", "error");
                else
                {
                        string[] parameter = { "@Flag", "@Department","@DepartmentH","@DeptGroupID"};
                        string[] value = { "Add",txtDepartmentE.Text.Trim(), txtDepartmentH.Text.Trim(),ddlDeptGroup.SelectedValue };
                        DB_Status dbs = dba.sp_populateDataSet("SP_Department",4, parameter, value);
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
                            FillDepartment();
                            hdfDepartment_Id.Value = "0";
                            hdfBanner_Image.Value = "No";
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
                if (txtDepartmentE.Text == "")
                    displayMessage("Please enter department name", "error");
                else if (txtDepartmentH.Text == "")
                    displayMessage("Please enter department name", "error");
                else if (ddlDeptGroup.SelectedValue == "0")
                    displayMessage("Please select department group/reporting to", "error");
                else
                {
                        string[] parameter = { "@Flag", "@DeptID", "@Department", "@DepartmentH" , "@DeptGroupID" };
                        string[] value = { "Update", hdfDepartment_Id.Value, txtDepartmentE.Text.Trim(),txtDepartmentH.Text.Trim(), ddlDeptGroup.SelectedValue };
                        DB_Status dbs = dba.sp_populateDataSet("SP_Department",5, parameter, value);
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
                            FillDepartment();
                            hdfDepartment_Id.Value = "0";
                            hdfBanner_Image.Value = "No";
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
        hdfDepartment_Id.Value = "0";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void FillDepartment()
    {
        try
        {
            string[] parameter = { "@Flag"};
            string[] value = { "LoadForAdmin"};
            DB_Status dbs = dba.sp_populateDataSet("SP_Department",1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    grddept.DataSource = dt;
                    grddept.DataBind();
                }
            }
        }
        catch (Exception)
        {
        }
    }

    protected void ViewList_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string deptId = (sender as LinkButton).CommandArgument;
            hdfDepartment_Id.Value = deptId;
            Response.Redirect("department-highlights.aspx?dptId="+Convert.ToInt32(hdfDepartment_Id.Value));

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "Load_byID", hdfDepartment_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Department", 2, parameter, value);
            bool flag = false;

            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtDepartmentE.Text = Convert.ToString(dt.Rows[0]["Department"]);
                        txtDepartmentH.Text = Convert.ToString(dt.Rows[0]["DepartmentH"]);
                        
                    }
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
            string deptId = (sender as LinkButton).CommandArgument;
            hdfDepartment_Id.Value = deptId;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "LoadbyID", hdfDepartment_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Department", 2, parameter, value);
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
                        txtDepartmentE.Text =Convert.ToString(dt.Rows[0]["Department"]);
                        txtDepartmentH.Text = Convert.ToString(dt.Rows[0]["DepartmentH"]);
                        ddlDeptGroup.SelectedValue = Convert.ToString(dt.Rows[0]["DeptGroupID"]);
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
            hdfDepartment_Id.Value = RID;

            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "Delete", hdfDepartment_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Department", 2, parameter, value);
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
                hdfDepartment_Id.Value = "";
                FillDepartment();
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
        string dept_Id =((HiddenField)ckkIsactive.Parent.FindControl("hdfDept_IdGrd")).Value;
        string Flag = "";

        if (ckkIsactive.Checked)
        {
            Flag = "Activate";
        }
        else
        {
            Flag = "Deactivate";
        }

        string[] parameter = { "@Flag", "@DeptID" };
        string[] value = { Flag, dept_Id.ToString() };
        DB_Status dbs = dba.sp_populateDataSet("SP_Department", 2, parameter, value);
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
    protected void grddept_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grddept.PageIndex = e.NewPageIndex;
        FillDepartment();
    }
}