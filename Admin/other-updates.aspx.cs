using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_thought_of_the_day : System.Web.UI.Page
{
    Class1 mod = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] != null)
        {
            hdfDept_Id.Value = "0";
            div_headTitle.InnerText = "Admin > Other Updates";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Convert.ToString(Session["deprt_name"]) + " > Other Updates";
            hdfDept_Id.Value = Session["DeptID"].ToString();
        }
        else
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {
            panelAddNew.Visible = false;
            panelView.Visible = true;

            Fill_Updates();
            displayGridMessage("", "");
        }
    }
    /*Update*/
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Update")
            {
                if (txtTextEnglish.Content == "")
                {
                    displayMessage("Please enter text (English)", "error");
                }
                else if (txtTextHindi.Content == "")
                {
                    displayMessage("Please enter text (Hindi)", "error");
                }
                else
                {
                    string[] param = {"@Flag","@RID", "@TextHindi", "@TextEnglish" };
                    string[] value = {"Update",hdfRID.Value, txtTextHindi.Content.Trim(), txtTextEnglish.Content.Trim()};
                    DB_Status DBS = dba.sp_readSingleData("SP_OtherUpdates", 4, param, value);
                    string status = DBS.SingleResult;
                    if (DBS.OperationStatus.ToString() == "Success")
                    {
                        if (status == "Success")
                        {
                            displayMessage("Record successfully updated", "info");
                            Fill_Updates();
                            
                            hdfRID.Value = "0";
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;

                        }
                        else if (status == "Exists")
                        {
                            displayMessage("Sorry! Record already exists", "error");
                        }
                        else if (status == "Fail")
                        {
                            displayMessage("Server Error", "error");
                        }
                    }
                    else
                    {
                        displayMessage(DBS.Title + "-" + DBS.Description, "error");
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
        hdfRID.Value = "0";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }

    protected void Fill_Updates()
    {
        try
        {
            string[] parameter = { "@Flag"};
            string[] value = {"Load" };
            DB_Status dbs = dba.sp_populateDataSet("SP_OtherUpdates", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        grdUpdates.DataSource = dt;
                        grdUpdates.DataBind();
                    }
                    else
                    {
                        grdUpdates.DataSource = null;
                        grdUpdates.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // DisplayEmployeeMessage(ex.Message, "error");
        }
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string DailyActivityId = (sender as LinkButton).CommandArgument;
            hdfRID.Value = DailyActivityId;
          
            string[] param = {"@Flag", "@RID" };
            string[] value = { "LoadbyID", hdfRID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_OtherUpdates", 2, param, value);
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
                        txtTextEnglish.Content = ds.Tables[0].Rows[0]["TextEnglish"].ToString();
                        txtTextHindi.Content = ds.Tables[0].Rows[0]["TextHindi"].ToString();
                        if (flag)
                        {
                            panelAddNew.Visible = true;
                            panelView.Visible = false;

                            displayMessage("", "");
                            btnSave.Text = "Update";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            displayMessage(ex.Message, "error");
        }
    }     
  
    protected void clearfields()
    {
        txtTextEnglish.Content = "";
        txtTextHindi.Content = "";
        hdfRID.Value = "0";
    }
}