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
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] == null)
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {
            System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_hospitals");
            menuli.Attributes["class"] = "active";

            panelAddNew.Visible = false;
            panelView.Visible = true;
            //Fill Articles

            FillRecords();
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
        hfRID.Value = "";

        txtHospitalName.Text = "";
        txtLocation.Text = "";
        txtAddress.Text = "";
        txtContactNo.Text = "";
        txtEmailID.Text = "";
        txtWebsite.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtHospitalName.Text.Trim() == "")
                    displayMessage("Please enter Hospital Name", "error");
                else if (txtLocation.Text.Trim() == "")
                    displayMessage("Please enter Location", "error");
                else if (txtAddress.Text.Trim() == "")
                    displayMessage("Please enter Address", "error");
                else if (txtContactNo.Text.Trim() == "")
                    displayMessage("Please enter Contact No.", "error");
                else if (txtEmailID.Text.Trim() == "")
                    displayMessage("Please enter Email ID", "error");
                else if (txtWebsite.Text.Trim() == "")
                    displayMessage("Please enter Website", "error");
                else
                {
                    string[] parameter = { "@Flag", "@HospitalName", "@Location", "@Address", "@ContactNo", "@EmailID", "@Website" };
                    string[] value = { "Insert", txtHospitalName.Text.Trim(), txtLocation.Text.Trim(), txtAddress.Text.Trim(), txtContactNo.Text.Trim(), txtEmailID.Text.Trim(), txtWebsite.Text.Trim() };
                    DB_Status dbs = dba.sp_populateDataSet("SP_EmpanelledHospitals", 7, parameter, value);
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
                        displayMessage("Sorry! Record already exists", "error");
                    }
                    else if (result == "Success")
                    {
                        displayMessage("Record successfully added", "info");
                        FillRecords();
                        hfRID.Value = "";
                        btnSave.Text = "Save";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
                    }
                }
            }
            else if (btnSave.Text == "Update")
            {
                if (txtHospitalName.Text.Trim() == "")
                    displayMessage("Please enter Hospital Name", "error");
                else if (txtLocation.Text.Trim() == "")
                    displayMessage("Please enter Location", "error");
                else if (txtAddress.Text.Trim() == "")
                    displayMessage("Please enter Address", "error");
                else if (txtContactNo.Text.Trim() == "")
                    displayMessage("Please enter Contact No.", "error");
                else if (txtEmailID.Text.Trim() == "")
                    displayMessage("Please enter Email ID", "error");
                else if (txtWebsite.Text.Trim() == "")
                    displayMessage("Please enter Website", "error");
                else
                {
                    string[] parameter = { "@Flag", "@RID", "@HospitalName", "@Location", "@Address", "@ContactNo", "@EmailID", "@Website" };
                    string[] value = { "Update", hfRID.Value, txtHospitalName.Text.Trim(), txtLocation.Text.Trim(), txtAddress.Text.Trim(), txtContactNo.Text.Trim(), txtEmailID.Text.Trim(), txtWebsite.Text.Trim() };
                    DB_Status dbs = dba.sp_populateDataSet("SP_EmpanelledHospitals", 8, parameter, value);
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
                        displayMessage("Sorry! Record already exists", "error");
                    }
                    else if (result == "Success")
                    {
                        displayMessage("Record successfully updated", "info");
                        FillRecords();
                        hfRID.Value = "";
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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hfRID.Value = "";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void FillRecords()
    {
        try
        {
            string[] parameter = { "@Flag" };
            string[] value = { "View" };
            DB_Status dbs = dba.sp_populateDataSet("SP_EmpanelledHospitals", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    gridRecords.DataSource = dt;
                    gridRecords.DataBind();
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
            string RID = (sender as LinkButton).CommandArgument;
            hfRID.Value = RID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@RID" };
            string[] value = { "ViewByID", hfRID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_EmpanelledHospitals", 2, parameter, value);
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
                        txtHospitalName.Text = dt.Rows[0]["HospitalName"].ToString();
                        txtLocation.Text = dt.Rows[0]["Location"].ToString();
                        txtAddress.Text = dt.Rows[0]["Address"].ToString();
                        txtContactNo.Text = dt.Rows[0]["ContactNo"].ToString();
                        txtEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                        txtWebsite.Text = dt.Rows[0]["Website"].ToString();
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
            hfRID.Value = RID;

            string[] parameter = { "@Flag", "@RID" };
            string[] value = { "Delete", hfRID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_EmpanelledHospitals", 2, parameter, value);
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
                hfRID.Value = "";
                FillRecords();
            }
        }
        catch (Exception)
        {
        }
    }
    
 
}