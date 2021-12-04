using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class Admin_pop_up_images : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    string type = "";
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
                panelView.Visible = true;
                FillDepartments();
                FillDptAdmin();
                displayGridMessage("", "");
                hfRID.Value = "";
               
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

    protected void FillDepartments()
    {
        try
        {
            string[] parameter = { "@Flag" };
            string[] value = { "LoadDepartments" };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Department_Admin", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    ddlDepartment.DataSource = dt;
                    ddlDepartment.DataValueField = "DeptID";
                    ddlDepartment.DataTextField = "Department";
                    ddlDepartment.DataBind();
                }
            }
        }
        catch (Exception)
        {
        }
    }
    protected void FillDptAdmin()
    {
        try
        {
            string[] parameter = { "@Flag", "@Type" };
            string[] value = { "load","Admin" };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Department_Admin", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0]; // dpt admin
                   
                    gridDptAdmin.DataSource = dt;
                    gridDptAdmin.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
  
    protected void DeleteAdmin_Click(object sender, EventArgs e)
    {
        try
        {
            displayMessage("", "");
            displayGridMessage("", "");
            string EmpID = (sender as LinkButton).CommandArgument;
            string[] parameter = { "@Flag", "@Type", "@EmpCode", "@DeptID" };
            string[] value = { "Delete", "Admin", EmpID, ddlDepartment.SelectedValue };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Department_Admin", 4, parameter, value);
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
            if (result == "Success")
            {
                FillDptAdmin();
                
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnSaveAdmin_Click(object sender, EventArgs e)
    {
        try
        {
            displayMessage("", "");
            string[] parameter = { "@Flag", "@Type", "@EmpCode", "@DeptID" };
            string[] value = { "Add", "Admin",txtEmpIdAdmin.Text,ddlDepartment.SelectedValue };
            DB_Status dbs = dba.sp_readSingleData("Sp_Department_Admin", 4, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                string result = dbs.SingleResult;
                if (result== "Success")
                {
                    FillDptAdmin();
                }
                else if(result== "exist")
                {
                    lblAdmin.Text = "Employee is already department admin";
                }
                else if(result== "AdminOfOtherDept")
                {
                    lblAdmin.Text = "Employee is already department admin of other department";
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

   
    protected void ViewModule_Click(object sender, EventArgs e)
    {
        try
        {
            displayMessage("", "");
            displayGridMessage("", "");
            string code = (sender as LinkButton).CommandArgument;
            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;
            Label lbl = (Label)gridDptAdmin.Rows[rowindex].FindControl("lblAdminDept");
            HiddenField hdfEmpIdAdmin = (HiddenField)gridDptAdmin.Rows[rowindex].FindControl("hdfEmployeeIdAdmin");
            Session["DeptAdminEmpCode"] = code;
            Session["DeptAdminEmpId"] = hdfEmpIdAdmin.Value;
            Response.Redirect("form-module.aspx?dept="+lbl.Text);
        }
        catch (Exception ex)
        {
        }
    }

    protected void ResetPassword_Click(object sender, EventArgs e)
    {
        try
        {
            displayMessage("", "");
            displayGridMessage("", "");
            string EmployeeID = (sender as LinkButton).CommandArgument;
            HDN_EID.Value = EmployeeID;
             Class1 mod = new Class1();
           

            LinkButton lnkbtn_reset = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_reset.Parent.Parent;
            int rowindex = grdrow.RowIndex;
          //  HiddenField hdfEmpIdAdmin = (HiddenField)gridDptAdmin.Rows[rowindex].FindControl("hdfEmployeeIdAdmin");
        string nPassword = mod.Encrypt("123456");


            string[] parameter = { "@Flag", "@EID", "@NewPassword" };
            string[] value = { "ResetPassword", HDN_EID.Value, nPassword };
            DB_Status dbs = dba.sp_populateDataSet("SP_Employees_Admin", 3, parameter, value);
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
            if (result == "Success")
            {
                HDN_EID.Value = "";
                lnkbtn_reset.Text = "Done";
                lnkbtn_reset.CssClass = "btn btn-success btn-rounded bs-actionsbox";
            }
        }
        catch (Exception)
        {
        }
    }

}