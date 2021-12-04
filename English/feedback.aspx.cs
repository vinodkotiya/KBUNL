using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class feedback : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();

    private void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["Theme"] == null)
            Session["Theme"] = "theme_green";
        Page.Theme = Session["Theme"].ToString();        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
        }
    }

    protected void DisplaySugMessage(string msg, string type)
    {
        lblSugMsg.Text = msg;
        if (type.ToUpper() == "ERROR")
            lblSugMsg.ForeColor = System.Drawing.Color.Red;
        else if (type.ToUpper() == "INFO")
            lblSugMsg.ForeColor = System.Drawing.Color.Green;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtName.Text.Trim()== "")
            {
                DisplaySugMessage("Please enter your name", "error");
            }
            else if (txtPhoneNo.Text.Trim() == "")
            {
                DisplaySugMessage("Please enter your phone no", "error");
            }
            else if (txtEmpCode.Text.Trim() == "")
            {
                DisplaySugMessage("Please enter employee code", "error");
            }
            else if (txtSuggestion.Text.Trim() == "")
            {
                DisplaySugMessage("Please enter your suggestion", "error");
            }
            else
            {
                    string result = "";
                    string[] parameter = { "@Flag", "@Name", "@Employee_no", "@Mobile", "@Feedback" };
                    string[] value = { "Add", txtName.Text.Trim(),txtEmpCode.Text.Trim(),txtPhoneNo.Text.Trim(),txtSuggestion.Text.Trim() };
                    DB_Status dbs = dba.sp_populateDataSet("SP_Feedback",5, parameter, value);
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
                    if (result == "inserted")
                    {
                        DisplaySugMessage("Record saved successfully", "INFO");
                        txtName.Text = "";
                        txtPhoneNo.Text = "";
                        txtEmpCode.Text = "";
                        txtSuggestion.Text = "";
                    }
            }
        }
        catch (Exception ex)
        {

        }
    }
   
}