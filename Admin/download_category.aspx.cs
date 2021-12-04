using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_download_category : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] != null)
        {
            div_headTitle.InnerText = "Admin > Download Category";
            hdfDept_Id.Value = "0";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Session["deprt_name"].ToString() + " > Download Category";
            hdfDept_Id.Value = Session["DeptID"].ToString();
        }
        else
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {
            //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("Downloads");
            // menuli.Attributes["class"] = "active";

            panelAddNew.Visible = false;
            panelView.Visible = true;
            //Fill Articles

            FillDownloadCategory();
            displayGridMessage("", "");
        }
    }
    /* Add/Update/Deactivate Download*/
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
        hdfDownloadCategoryId.Value = "0";
        txtCategoryEnglish.Text = "";
        txtCategoryHindi.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hdfDownloadCategoryId.Value = "0";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtCategoryEnglish.Text.Trim() == "")
                    displayMessage("Please enter category name", "error");
               else if (txtCategoryHindi.Text.Trim() == "")
                    displayMessage("Please enter category name", "error");
                else
                {
                    string[] parameter = { "@Flag", "@DownloadCategoryEnglish", "@DownloadCategoryHindi", "@DeptID" };
                    string[] value = { "Insert", txtCategoryEnglish.Text.Trim(),txtCategoryHindi.Text.Trim(), hdfDept_Id.Value };
                    DB_Status dbs = dba.sp_populateDataSet("SP_Download_Category", 4, parameter, value);
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
                        displayMessage("Sorry! Download category already exists", "error");
                    }
                    else if (result == "Success")
                    {
                        displayMessage("Download category successfully added", "info");
                        FillDownloadCategory();
                        btnSave.Text = "Save";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
                    }
                }
            }
            else if (btnSave.Text == "Update")
            {
                if (txtCategoryEnglish.Text.Trim() == "")
                    displayMessage("Please enter category name", "error");
                else if (txtCategoryHindi.Text.Trim() == "")
                    displayMessage("Please enter category name", "error");
                else
                {
                    string[] parameter = { "@Flag", "@DownloadCategoryId", "@DownloadCategoryEnglish", "@DownloadCategoryHindi", "@DeptID" };
                    string[] value = { "Update", hdfDownloadCategoryId.Value, txtCategoryEnglish.Text.Trim(), txtCategoryHindi.Text.Trim(), hdfDept_Id.Value };
                    DB_Status dbs = dba.sp_populateDataSet("SP_Download_Category",5, parameter, value);
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
                        displayMessage("Sorry! Download category already exists", "error");
                    }
                    else if (result == "Success")
                    {
                        FillDownloadCategory();
                        hdfDownloadCategoryId.Value = "0";
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

    protected void FillDownloadCategory()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "View", hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Download_Category", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    gridDownload.DataSource = dt;
                    gridDownload.DataBind();
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
            string ID = (sender as LinkButton).CommandArgument;
            hdfDownloadCategoryId.Value = ID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@DownloadCategoryId", "@DeptID" };
            string[] value = { "ViewByID", hdfDownloadCategoryId.Value, hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Download_Category", 3, parameter, value);
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
                        txtCategoryEnglish.Text =Convert.ToString(dt.Rows[0]["DownloadCategoryEnglish"]);
                        txtCategoryHindi.Text = Convert.ToString(dt.Rows[0]["DownloadCategoryHindi"]);
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
        catch (Exception ex)
        {
        }
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string DownloadCategoryId = (sender as LinkButton).CommandArgument;
            hdfDownloadCategoryId.Value = DownloadCategoryId;

            string[] parameter = { "@Flag", "@DownloadCategoryId", "@DeptID" };
            string[] value = { "Delete", hdfDownloadCategoryId.Value, hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Download_Category", 3, parameter, value);
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
                hdfDownloadCategoryId.Value = "";
                FillDownloadCategory();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ViewDownloads_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string DownloadCategoryId = (sender as LinkButton).CommandArgument;
            hdfDownloadCategoryId.Value = DownloadCategoryId;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            Label lblCategoryEnglish = (Label)grdrow.FindControl("lblCategoryEnglish");
            Label lblCategoryHindi = (Label)grdrow.FindControl("lblCategoryHindi");
            int rowindex = grdrow.RowIndex;
            Session["DownloadCategoryId"] = hdfDownloadCategoryId.Value;
            Session["DownloadCategory"] = lblCategoryEnglish.Text.Trim();
            Response.Redirect("download.aspx");
        }
        catch (Exception ex)
        {
        }
    }
}