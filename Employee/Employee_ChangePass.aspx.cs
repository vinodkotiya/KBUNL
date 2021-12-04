using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_Employee_ChangePass : System.Web.UI.Page
{
    Class1 mod = new Class1();
    DB_Access obj = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("Employee_Login.aspx");
        }
        if (!IsPostBack)
        {
            System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_companyusers");
            menuli.Attributes["class"] = "active";
            if (Session["EmpID"] != null)
            {
                hdfEmployeeId.Value = Convert.ToString(Session["EmpID"]);
                //BindEmployeeInformation();
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
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtOP.Text.Trim() == "")
            {
                displayMessage("Please enter current password", "error");
            }
            else if (txtNP.Text.Trim() == "")
            {
                displayMessage("Please enter new password", "error");
            }
            else if (txtOP.Text.Trim() == txtNP.Text.Trim())
            {
                displayMessage("Sorry! Current and New password should be different", "error");
            }
            else if (txtNP.Text.Trim() != txtCP.Text.Trim())
            {
                displayMessage("Sorry! New password not confirmed", "error");
            }
            else
            {
                string[] parameter = { "@Flag", "@Password", "NewPassword", "@EID" };
                string[] value = { "Employee_Change_Password", mod.Encrypt(txtOP.Text.Trim()), mod.Encrypt(txtNP.Text.Trim()),hdfEmployeeId.Value};
                DB_Status dbs = obj.sp_populateDataSet("SP_Employees_Admin", 4, parameter, value);
                string str = "";
                if (dbs.OperationStatus.ToString() == "Success")
                {
                    DataSet ds = dbs.ResultDataSet;
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            str = dt.Rows[0]["Result"].ToString();
                        }
                    }
                }
                if (str == "NotExists")
                {
                    displayMessage("Invalid Current Password", "error");
                }
                else if (str == "ok")
                {
                    displayMessage("Password successfully changed.", "info");
                }
            }
        }
        catch (Exception ex)
        {
            displayMessage(ex.Message.ToString(), "error");
        }
    }
}