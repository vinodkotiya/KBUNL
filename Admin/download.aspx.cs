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
        if (Session["AdminUserID"] != null && Session["DownloadCategoryId"] != null)
        {
            div_headTitle.InnerHtml = " Admin > Download Category: <a href='download_category.aspx'>" + Convert.ToString(Session["DownloadCategory"]) + "</a> > Download";
            hdfDept_Id.Value = "0";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null && Session["DownloadCategoryId"] != null)
        {
            //div_headTitle.InnerText = "Department : " + Session["deprt_name"].ToString() + " > Download";
            div_headTitle.InnerHtml = " Department : " + Convert.ToString(Session["deprt_name"]) + " > Download Category: <a href='download_category.aspx'>" + Convert.ToString(Session["DownloadCategory"]) + "</a> > Download";
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
            hdfDownloadCategoryId.Value = Convert.ToString(Session["DownloadCategoryId"]);

            panelAddNew.Visible = false;
            panelView.Visible = true;
            //Fill Articles

            FillDownloads();
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
        hfDownloadID.Value = "";
        hfFile_UploadedPath.Value = "";
        txtTitleEnglish.Text = "";
        txtTitleHindi.Text = "";
        btnSave.Text = "Save";
        span_FileStatus.Style.Add("display", "none");
        RequiredFieldValidatorFileUploadImage.Enabled = true ;
        span_FileStatus.InnerText = "";
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hfDownloadID.Value = "";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
        hfFile_UploadedPath.Value = "";
        span_FileStatus.Style.Add("display", "none");
        span_FileStatus.InnerText = "";
        RequiredFieldValidatorFileUploadImage.Enabled = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtTitleEnglish.Text.Trim() == "")
                    displayMessage("Please enter Title", "error");
                else if (txtTitleHindi.Text.Trim() == "")
                    displayMessage("Please enter Title", "error");
                else if (!FileUploadImage.HasFile)
                {
                    displayMessage("Please upload File", "error");
                }
                else
                {
                    bool flagValidImage = true;
                    bool flagHasImage = false;
                    string ImageFileName = "";
                    string ImagePath = "NA";

                    flagHasImage = true;
                    string ext = Path.GetExtension(FileUploadImage.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    ImageFileName = "Download_" + datevalue + ext;

                    ImagePath = "Uploads/Downloads/" + ImageFileName;
                  

                    if (flagHasImage && flagValidImage == false)
                    {
                        displayMessage("Please attach only Valid file ", "error");
                    }
                    else
                    {
                        string[] parameter = { "@Flag", "@TitleEnglish", "@TitleHindi", "@AttachmentUrl", "@DeptID", "@DownloadCategoryId" };
                        string[] value = { "Insert", txtTitleEnglish.Text.Trim(), txtTitleHindi.Text.Trim(), ImagePath,hdfDept_Id.Value, hdfDownloadCategoryId.Value };
                        DB_Status dbs = dba.sp_populateDataSet("SP_Downloads",6, parameter, value);
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
                            displayMessage("Sorry! Download already exists", "error");
                        }
                        else if (result == "Success")
                        {
                            if (flagHasImage && flagValidImage)
                                FileUploadImage.SaveAs(Server.MapPath("~/" + ImagePath));

                            displayMessage("Download successfully added", "info");
                            FillDownloads();
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;
                        }
                    }
                    
                }
            }
            else if (btnSave.Text == "Update")
            {
                if (txtTitleEnglish.Text.Trim() == "")
                    displayMessage("Please enter Title", "error");
                else if (txtTitleHindi.Text.Trim() == "")
                    displayMessage("Please enter Title", "error");
                else
                {
                    bool flagValidImage = true;
                    bool flagHasImage = false;
                    string ImageFileName = "";
                    string ImagePath = hfFile_UploadedPath.Value;

                    if (FileUploadImage.HasFile)
                    {
                        flagHasImage = true;
                        string ext = Path.GetExtension(FileUploadImage.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        ImageFileName = "Download_" + datevalue + ext;

                        ImagePath = "Uploads/Downloads/" + ImageFileName;

                        //if (ext == ".jpg" || ext == ".JPG" || ext == ".png" || ext == ".PNG")
                        //    flagValidImage = true;
                        //else
                        //   flagValidImage = false;
                    }
                    if (flagHasImage && flagValidImage == false)
                    {
                        displayMessage("Please attach only valid File", "error");
                    }
                    else
                    {
                        string[] parameter = {"@Flag", "@DownloadID", "@TitleEnglish", "@TitleHindi", "@AttachmentUrl", "@DeptID", "@DownloadCategoryId" };
                        string[] value = {"Update", hfDownloadID.Value, txtTitleEnglish.Text.Trim(), txtTitleHindi.Text.Trim(), ImagePath,hdfDept_Id.Value,hdfDownloadCategoryId.Value };
                        DB_Status dbs = dba.sp_populateDataSet("SP_Downloads",7, parameter, value);
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
                            displayMessage("Sorry! Download already exists", "error");
                        }
                        else if (result == "Success")
                        {
                            if (flagHasImage && flagValidImage)
                            {
                                if (hfFile_UploadedPath.Value.Length > 10)
                                    File.Delete(Request.PhysicalApplicationPath + hfFile_UploadedPath.Value);

                                FileUploadImage.SaveAs(Server.MapPath("~/" + ImagePath));
                            }
                            FillDownloads();
                            hfDownloadID.Value = "";
                            hfFile_UploadedPath.Value = "";
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;
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
    
    protected void FillDownloads()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID", "@DownloadCategoryId" };
            string[] value = { "View",hdfDept_Id.Value,hdfDownloadCategoryId.Value};
            DB_Status dbs = dba.sp_populateDataSet("SP_Downloads",3, parameter, value);
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
            hfDownloadID.Value = ID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@DownloadID", "@DeptID" };
            string[] value = { "ViewByID", hfDownloadID.Value,hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Downloads", 3, parameter, value);
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
                        txtTitleEnglish.Text =Convert.ToString(dt.Rows[0]["TitleEnglish"]);
                        txtTitleHindi.Text = Convert.ToString(dt.Rows[0]["TitleHindi"]);
                        hfFile_UploadedPath.Value = Convert.ToString(dt.Rows[0]["AttachmentUrl"]);
                        if (!string.IsNullOrEmpty(hfFile_UploadedPath.Value) && hfFile_UploadedPath.Value != "No")
                            RequiredFieldValidatorFileUploadImage.Enabled = false;
                        else
                            RequiredFieldValidatorFileUploadImage.Enabled = false;
                        span_FileStatus.Style.Add("display", "block");
                        span_FileStatus.InnerText = "File Selected";
                        span_FileStatus.Style.Add("color", "green");
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
            string QuestionID = (sender as LinkButton).CommandArgument;
            hfDownloadID.Value = QuestionID;

            string[] parameter = { "@Flag", "@DownloadID", "@DeptID" };
            string[] value = { "Delete", hfDownloadID.Value,hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Downloads", 3, parameter, value);
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
                hfDownloadID.Value = "";
                FillDownloads();
            }
        }
        catch (Exception ex)
        {

        }
    }
    
 
}