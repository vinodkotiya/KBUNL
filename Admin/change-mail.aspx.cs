using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class Admin_news_post : System.Web.UI.Page
{
    Class1 mod = new Class1();
    DB_Access obj = new DB_Access();
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
                System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_email");
                menuli.Attributes["class"] = "active";
                Fill_REDMessage();
            }
        }
    }

    protected void Fill_REDMessage()
    {
        try
        {
            string[] parameter = { };
            string[] value = { };
            DB_Status dbs = obj.sp_populateDataSet("Sp_AdminEmail_View", 0, parameter, value);
            string str = "";
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                       // pow.Visible = true;
                       
                     
                        txtEmail.Text = dt.Rows[0]["EmailID"].ToString();
                        
                    }
                   
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void DisplayEventMessage(string msg, string type)
    {
        lblMsg.Text = msg;
        if (type == "error")
            lblMsg.ForeColor = System.Drawing.Color.Red;
        else if (type == "info")
            lblMsg.ForeColor = System.Drawing.Color.Green;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if(txtEmail.Text=="")
            {
                DisplayEventMessage("Please Enter Email", "error");
            }
           
            else
            {
            
                string[] param1 = { "@EmailID" };
                string[] values1 = {txtEmail.Text.Trim()};
                DB_Status DBS1 = obj.sp_readSingleData("Sp_AdminEmail_Update", 1, param1, values1);
                string status = DBS1.SingleResult;
                if (status == "success")
                {
                    DisplayEventMessage("Updated Successfully", "info");
                    Fill_REDMessage();
                }
                else if (status == "fail")
                {
                    DisplayEventMessage("Server Error", "error");
                }         
            }
        }
        catch (Exception ex)
        {
            lblMsg.Attributes["class"] = "error";
            lblMsg.Text = ex.Message.ToString();
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtEmail.Text == "")
            {
                DisplayEventMessage("Please Enter Email", "error");
            }
            else
            {
                string[] param1 = { "@EmailID" };
                string[] values1 = {txtEmail.Text.Trim() };
                DB_Status DBS1 = obj.sp_readSingleData("Sp_AdminEmail_Update", 1, param1, values1);
                string status = DBS1.SingleResult;
                if (status == "success")
                {
                    DisplayEventMessage("Updated Successfully", "info");
                    Fill_REDMessage();
                }
                else if (status == "fail")
                {
                    DisplayEventMessage("Server Error", "error");
                }       
            }
        }
        catch (Exception ex)
        {
            lblMsg.Attributes["class"] = "error";
            lblMsg.Text = ex.Message.ToString();
        }
    }
}