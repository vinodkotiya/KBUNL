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
        else
        {
            if (!IsPostBack)
            {
                hdfUserName.Value = Convert.ToString(Session["username"]);
                LoadEmailID();
            }
            //HtmlAnchor link = (HtmlAnchor)Master.FindControl("lhslink3");
            //link.Attributes.Add("class", "active");
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

    protected void LoadEmailID()
    {
        try
        {
            string[] parameter = { "@count", "@UserName" };
            string[] value = { "1",hdfUserName.Value};
            DB_Status dbs = dba.sp_populateDataSet("SP_Admin_ChangeEmailID", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
            lblmsg.Attributes["class"] = "alert1-error";
        }
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtEmailID.Text.Trim() == "") 
            {
                lblmsg.Text = "Please enter Email ID";
                lblmsg.Attributes["class"] = "alert1-error";
            }
            else if (!obj.IsValidEmail(txtEmailID.Text.Trim()))
            {
                lblmsg.Text = "Please enter valid Email ID";
                lblmsg.Attributes["class"] = "alert1-error";
            }
            else
            {
                string[] parameter = { "@count", "@EmailID", "@UserName" };
                string[] value = { "2", txtEmailID.Text.Trim(),hdfUserName.Value};
                DB_Status dbs = dba.sp_populateDataSet("SP_Admin_ChangeEmailID",3, parameter, value);
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
                if (str == "Success")
                {
                    lblmsg.Text = "Email ID successfully changed.";
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
