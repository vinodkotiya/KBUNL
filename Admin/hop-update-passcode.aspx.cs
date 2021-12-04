using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class admin_ChangePass : System.Web.UI.Page
{
    
   public Class1 obj = new Class1();
   DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpID"] == null)
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {
            hdfUserName.Value = Convert.ToString(Session["username"]);
        }
    }

    protected void DisplayMessage(string msg, string type)
    {
        //Display error and info message on page using input parameter

        lblmsg.Text = msg;
        if (type == "error")
            lblmsg.ForeColor = System.Drawing.Color.Red;
        else if (type == "info")
            lblmsg.ForeColor = System.Drawing.Color.Green;
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtOP.Text.Trim() == "")
            {
                lblmsg.Text = "Please enter current password";
                lblmsg.Attributes["class"] = "alert1-error";
            }
            else if (txtNP.Text.Trim() == "")
            {
                lblmsg.Text = "Please enter new password";
                lblmsg.Attributes["class"] = "alert1-error";
            }
            else if (txtOP.Text.Trim() == txtNP.Text.Trim())
            {
                lblmsg.Text = "Sorry! Current and New password should be different";
                lblmsg.Attributes["class"] = "alert1-error";
            }
            else if (txtNP.Text.Trim() != txtCP.Text.Trim())
            {
                lblmsg.Text = "Sorry! New password not confirmed";
                lblmsg.Attributes["class"] = "alert1-error";
            }
            else
            {
                string[] parameter = { "@Flag", "@Password", "@NewPassword" };
                string[] value = { "UpdateHopPasscode",txtOP.Text.Trim(),txtNP.Text.Trim() };
                DB_Status dbs = dba.sp_populateDataSet("SP_HOPReports", 3, parameter, value);
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

                    lblmsg.Text = "Invalid Current Password";
                    lblmsg.Attributes["class"] = "alert1-error";
                }
                else if (str == "ok")
                {
                    lblmsg.Text = "Password successfully changed.";
                    lblmsg.Attributes["class"] = "alert1-sucess";
                }
            }

        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
            lblmsg.Attributes["class"] = "alert1-error";
        }
    }
}
