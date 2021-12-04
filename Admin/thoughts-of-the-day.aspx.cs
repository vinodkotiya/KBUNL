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
            div_headTitle.InnerText = "Admin > Thoughts of the day";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Convert.ToString(Session["deprt_name"]) + " > Announcements";
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

            Fill_Thoughts();
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

        hdfRID.Value = "0";

        txtThoughtEnglish.Text = "";
        txtThoughtHindi.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtThoughtEnglish.Text == "")
                {
                    displayMessage("Please enter thought of the day (English)", "error");
                }
                else if (txtThoughtHindi.Text == "")
                {
                    displayMessage("Please enter thought of the day (Hindi)", "error");
                }
                else
                {
                    string[] param = {"@Flag", "@ThoughtOfTheDayHindi", "@ThoughtOfTheDayEnglish" };
                    string[] value = {"Add", txtThoughtHindi.Text.Trim(), txtThoughtEnglish.Text.Trim()};
                    DB_Status DBS = dba.sp_readSingleData("SP_Thoughts", 3, param, value);
                    string status = DBS.SingleResult;
                    if (DBS.OperationStatus.ToString() == "Success")
                    {
                        if (status == "Success")
                        {
                            displayMessage("Thoughts successfully added", "info");
                            Fill_Thoughts();
                            clearfields();
                            hdfRID.Value = "0";
                        }
                        else if (status == "Exists")
                        {
                            displayMessage("Sorry! Thoughts already exists", "error");
                        }
                        else if (status == "Fail")
                        {
                            displayMessage("Server error", "error");
                        }
                    }
                    else
                    {
                        displayMessage(DBS.Title + "-" + DBS.Description, "error");
                    }
                }
            }
            else if (btnSave.Text == "Update")
            {
                if (txtThoughtEnglish.Text == "")
                {
                    displayMessage("Please enter thought of the day (English)", "error");
                }
                else if (txtThoughtHindi.Text == "")
                {
                    displayMessage("Please enter thought of the day (Hindi)", "error");
                }
                else
                {
                    string[] param = {"@Flag","@RID", "@ThoughtOfTheDayHindi", "@ThoughtOfTheDayEnglish" };
                    string[] value = {"Update",hdfRID.Value, txtThoughtHindi.Text.Trim(), txtThoughtEnglish.Text.Trim()};
                    DB_Status DBS = dba.sp_readSingleData("SP_Thoughts", 4, param, value);
                    string status = DBS.SingleResult;
                    if (DBS.OperationStatus.ToString() == "Success")
                    {
                        if (status == "Success")
                        {
                            displayMessage("Thoughts successfully updated", "info");
                            Fill_Thoughts();
                            clearfields();
                            
                            hdfRID.Value = "0";
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;

                        }
                        else if (status == "Exists")
                        {
                            displayMessage("Sorry! Thoughts already exists", "error");
                        }
                        else if (status == "Ffail")
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

    protected void Fill_Thoughts()
    {
        try
        {
            string[] parameter = { "@Flag"};
            string[] value = {"Load" };
            DB_Status dbs = dba.sp_populateDataSet("SP_Thoughts", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        grdThoughts.DataSource = dt;
                        grdThoughts.DataBind();
                    }
                    else
                    {
                        grdThoughts.DataSource = null;
                        grdThoughts.DataBind();
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
            string[] value = {"Load_byID" ,hdfRID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Thoughts", 2, param, value);
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
                        txtThoughtEnglish.Text = ds.Tables[0].Rows[0]["ThoughtOfTheDayEnglish"].ToString();
                        txtThoughtHindi.Text = ds.Tables[0].Rows[0]["ThoughtOfTheDayHindi"].ToString();
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
   
    protected void Delete_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string RID = (sender as LinkButton).CommandArgument;
            hdfRID.Value = RID;

            string[] parameter = {"@Flag", "@RID" };
            string[] value = {"Delete", hdfRID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Thoughts", 2, parameter, value);
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
                hdfRID.Value = "0";
                Fill_Thoughts();
            }
        }
        catch (Exception)
        {
        }
    }
    
  
    protected void clearfields()
    {
        txtThoughtEnglish.Text = "";
        txtThoughtHindi.Text = "";
        hdfRID.Value = "0";
    }

    protected void grdThoughts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdThoughts.PageIndex = e.NewPageIndex;
        Fill_Thoughts(); 
    }
}