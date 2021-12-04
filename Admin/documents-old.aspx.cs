using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Admin_report : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] != null)
        {
            div_headTitle.InnerHtml = " Admin > Report";
            hfDeptID.Value = "0";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Session["deprt_name"].ToString() + " > Report";
            hfDeptID.Value = Session["DeptID"].ToString();
        }
        else
        {
            Response.Redirect("../login.aspx");
        }

        lnkbtnBack.Visible = false;
        if (Request.QueryString["ParentReportID"] != null)
        {
            hdfReportId.Value = Convert.ToString(Request.QueryString["ParentReportID"]);
            hdfReportLevel.Value = Convert.ToString(Request.QueryString["Level"]);
            if (hdfReportLevel.Value == "1" || hdfReportLevel.Value == "0")
            {
                lnkbtnBack.Visible = false;
            }
            else
            {
                lnkbtnBack.Visible = true;
                lnkbtnBack.HRef = "reports.aspx?Level=" + (Convert.ToInt16(hdfReportLevel.Value) - 1).ToString() + "&ParentReportID=" + GetParentID(hdfReportId.Value);
            }
        }
        if (!IsPostBack)
        {
            panelAddNew.Visible = false;
            panelView.Visible = true;
            FillReports();
            displayGridMessage("", "");
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
        hdfReportId.Value = "0";
        txtTitleEnglish.Text = "";
        txtTitleHindi.Text = "";
        txtSeqNo.Text = "";
        txtUrl.Text = "";
        hdfFile_UploadedPath.Value = "NA";
        btnSave.Text = "Save";

        span_FileStatus.Style.Add("display", "none");
        span_FileStatus.InnerText = "";
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hdfReportId.Value = "0";
        hdfFile_UploadedPath.Value = "NA";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;

        span_FileStatus.Style.Add("display", "none");
        span_FileStatus.InnerText = "";
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
                else
                {
                    bool flagValid = true;

                    string ImageFileName = "";
                    string ImagePath = "NA";
                    if (File_Upload.HasFile)
                    {
                        string ext = Path.GetExtension(File_Upload.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        ImageFileName = "URL_" + datevalue + ext;
                        ImagePath = "Uploads/Report/" + ImageFileName;
                        hdfFile_UploadedPath.Value = ImagePath;
                        if (ext == ".exe")
                            flagValid = false;
                        else
                            flagValid = true;
                    }
                    else
                    {
                        hdfFile_UploadedPath.Value = txtUrl.Text.Trim();
                    }
                    if (flagValid == false)
                    {
                        displayMessage("Please attach only Valid file ", "error");
                    }
                    else
                    {
                        string ReportGroup = "", linkType = "", AttachmentFileAndURL = "";
                        if (chkReportGroup.Checked)
                            ReportGroup = "True";
                        else
                            ReportGroup = "False";
                        if (rbtnAttachment.Checked)
                            linkType = "Attachment";
                        else if (rbtnURL.Checked)
                            linkType = "URL";
                        if (linkType == "Attachment")
                            AttachmentFileAndURL = hdfFile_UploadedPath.Value;
                        else if (linkType == "URL")
                            AttachmentFileAndURL = txtUrl.Text.Trim();

                        string[] parameter = { "@Flag", "@DeptID", "@ReportLevel", "@ParentReportId", "@ReportTitleEnglish", "@ReportTitleHindi", "@ReportGroup", "@SeqNo", "@LinkType", "@AttachmentAndURL" };
                        string[] value = { "Add", hfUserID.Value, hdfReportLevel.Value, hdfReportId.Value, txtTitleEnglish.Text.Trim(), txtTitleHindi.Text.Trim(), ReportGroup, txtSeqNo.Text.Trim(), linkType, AttachmentFileAndURL };
                        DB_Status dbs = dba.sp_populateDataSet("SP_Reports", 10, parameter, value);
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
                            displayMessage("Sorry! Title already exists", "error");
                        }
                        else if (result == "Inserted")
                        {
                            if (ImagePath != "NA")
                                File_Upload.SaveAs(Server.MapPath("~/" + ImagePath));
                            FillReports();
                            hdfReportId.Value = "";
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
                    bool flagValid = true;
                    string ImageFileName = "NA";
                    string ImagePath = hdfFile_UploadedPath.Value;
                    if (File_Upload.HasFile)
                    {
                        string ext = Path.GetExtension(File_Upload.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        ImageFileName = "URL_" + datevalue + ext;
                        hdfFile_UploadedPath.Value = "Uploads/Report/" + ImageFileName;

                        if (ext == ".exe")
                            flagValid = false;
                        else
                            flagValid = true;

                    }
                    if (flagValid == false)
                    {
                        displayMessage("Please attach only valid File", "error");
                    }
                    else
                    {
                        string ReportGroup = "", linkType = "", AttachmentFileAndURL = "";
                        if (chkReportGroup.Checked)
                            ReportGroup = "True";
                        else
                            ReportGroup = "False";
                        if (rbtnAttachment.Checked)
                            linkType = "Attachment";
                        else if (rbtnURL.Checked)
                            linkType = "URL";
                        if (linkType == "Attachment")
                            AttachmentFileAndURL = hdfFile_UploadedPath.Value;
                        else if (linkType == "URL")
                            AttachmentFileAndURL = txtUrl.Text.Trim();
                        string[] parameter = { "@Flag", "@ReportId", "@DeptID", "@ReportLevel", "@ParentReportId", "@ReportTitleEnglish", "@ReportTitleHindi", "@ReportGroup", "@SeqNo", "@LinkType", "@AttachmentAndURL" };
                        string[] value = { "Update", hdfReportUpdateId.Value, hfUserID.Value, hdfReportLevel.Value, hdfReportId.Value, txtTitleEnglish.Text.Trim(), txtTitleHindi.Text.Trim(), ReportGroup, txtSeqNo.Text.Trim(), linkType, AttachmentFileAndURL };
                        DB_Status dbs = dba.sp_populateDataSet("SP_Reports", 11, parameter, value);
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
                            displayMessage("Sorry! Title already exists", "error");
                        }
                        else if (result == "Updated")
                        {
                            if (ImageFileName != "NA")
                            {
                                if (hdfFile_UploadedPath.Value.Length > 10)
                                {
                                    FileInfo file = new FileInfo(Server.MapPath("~/" + ImagePath));
                                    if (file.Exists)
                                        file.Delete();
                                    File_Upload.SaveAs(Server.MapPath("~/" + hdfFile_UploadedPath.Value));
                                }
                            }
                            FillReports();
                            hdfReportId.Value = "0";
                            hdfReportUpdateId.Value = "0";
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

    protected void FillReports()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID", "@ReportLevel", "@ParentReportId" };
            string[] value = { "LoadForAdmin", hfUserID.Value, hdfReportLevel.Value, hdfReportId.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Reports", 4, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    grdview.DataSource = dt;
                    grdview.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void Update_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string ID = (sender as LinkButton).CommandArgument;
            hdfReportUpdateId.Value = ID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@ReportId", "@DeptID" };
            string[] value = { "LoadbyID", hdfReportUpdateId.Value, hfUserID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Reports", 3, parameter, value);
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
                        txtTitleEnglish.Text = Convert.ToString(dt.Rows[0]["ReportTitleEnglish"]);
                        txtTitleHindi.Text = Convert.ToString(dt.Rows[0]["ReportTitleHindi"]);
                        txtSeqNo.Text = Convert.ToString(dt.Rows[0]["SeqNo"]);
                        hdfFile_UploadedPath.Value = Convert.ToString(dt.Rows[0]["AttachmentAndURL"]);
                        if (!string.IsNullOrEmpty(hdfFile_UploadedPath.Value) && hdfFile_UploadedPath.Value != "NA")
                            RequiredFieldValidatorFile_Upload.Enabled = false;
                        else
                            RequiredFieldValidatorFile_Upload.Enabled = true;
                        string LinkType = Convert.ToString(dt.Rows[0]["LinkType"]);
                        string ReportGroup = Convert.ToString(dt.Rows[0]["ReportGroup"]);
                        if (ReportGroup == "True")
                        {
                            chkReportGroup.Checked = true;
                            divlnkType.Visible = false;
                        }
                        else
                        {
                            chkReportGroup.Checked = false;
                            divlnkType.Visible = true;
                        }
                        if (LinkType == "Attachment")
                        {
                            rbtnAttachment.Checked = true;
                            rbtnURL.Checked = false;

                            divAttachment.Visible = true;
                            RequiredFieldValidatortxtUrl.Enabled = false;
                            divURL.Visible = false;
                            hdfFile_UploadedPath.Value = Convert.ToString(dt.Rows[0]["AttachmentAndURL"]);
                            txtUrl.Text = "";
                        }
                        else if (LinkType == "URL")
                        {
                            rbtnAttachment.Checked = false;
                            rbtnURL.Checked = true;
                            RequiredFieldValidatorFile_Upload.Enabled = false;
                            divAttachment.Visible = false;
                            RequiredFieldValidatortxtUrl.Enabled = true;
                            divURL.Visible = true;
                            txtUrl.Text = Convert.ToString(dt.Rows[0]["AttachmentAndURL"]);
                            hdfFile_UploadedPath.Value = "NA";
                        }

                        if (hdfFile_UploadedPath.Value != "NA")
                        {
                            span_FileStatus.Style.Add("display", "block");
                            span_FileStatus.InnerText = "File Selected";
                            span_FileStatus.Style.Add("color", "green");
                        }
                        else
                        {
                            span_FileStatus.Style.Add("display", "none");
                            span_FileStatus.InnerText = "";
                        }

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
            string ReportId = (sender as LinkButton).CommandArgument;
            hdfReportId.Value = ReportId;

            string[] parameter = { "@Flag", "@ReportId", "@DeptID" };
            string[] value = { "Delete", hdfReportId.Value, hfUserID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Reports", 3, parameter, value);
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
            if (result == "Deleted")
            {
                hdfReportId.Value = "0";
                FillReports();
            }
        }
        catch (Exception)
        {
        }
    }
    protected void lnkbtnReport_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string ReportId = (sender as LinkButton).CommandArgument;
            hdfReportId.Value = ReportId;
            LinkButton lnkbtnReport = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtnReport.NamingContainer;
            HiddenField hdfReportIdGrd = (HiddenField)grdrow.FindControl("hdfReportIdGrd");
            HiddenField hdfParentReportIdGrd = (HiddenField)grdrow.FindControl("hdfParentReportIdGrd");
            HiddenField hdfReportLevelGrd = (HiddenField)grdrow.FindControl("hdfReportLevelGrd");
            HiddenField hdfReportTitleGrd = (HiddenField)grdrow.FindControl("hdfReportTitleGrd");
            int ReportLevel = 0;
            if (!string.IsNullOrEmpty(hdfReportLevelGrd.Value))
            {
                ReportLevel = Convert.ToInt32(hdfReportLevelGrd.Value);
                ReportLevel = ReportLevel + 1;

            }
            Session["ParentReportId"] = hdfParentReportIdGrd.Value;
            Session["ReportTitle"] = hdfReportTitleGrd.Value;
            Response.Redirect("reports.aspx?RId=" + hdfReportIdGrd.Value + "&RL=" + Convert.ToString(ReportLevel));
        }
        catch (Exception ex)
        {

        }
    }

    protected void rbtnURL_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnURL.Checked)
        {
            divURL.Visible = true;
            RequiredFieldValidatortxtUrl.Enabled = true;
            divAttachment.Visible = false;
            RequiredFieldValidatorFile_Upload.Enabled = false;
        }
        else
        {
            divURL.Visible = false;
            RequiredFieldValidatortxtUrl.Enabled = false;
            divAttachment.Visible = true;
            RequiredFieldValidatorFile_Upload.Enabled = true;
        }
        if (hdfReportUpdateId.Value == "0")
            txtUrl.Text = "";
    }

    protected void rbtnAttachment_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnAttachment.Checked)
        {
            divURL.Visible = false;
            RequiredFieldValidatortxtUrl.Enabled = false;
            divAttachment.Visible = true;
            RequiredFieldValidatorFile_Upload.Enabled = true;
        }
        else
        {
            divURL.Visible = true;
            RequiredFieldValidatortxtUrl.Enabled = true;
            divAttachment.Visible = false;
            RequiredFieldValidatorFile_Upload.Enabled = false;
        }
        if (hdfReportUpdateId.Value == "0")
            txtUrl.Text = "";
    }

    protected void chkReportGroup_CheckedChanged(object sender, EventArgs e)
    {
        if (chkReportGroup.Checked)
        {
            divlnkType.Visible = false;
            divURL.Visible = false;
            divAttachment.Visible = false;
        }
        else
        {
            divlnkType.Visible = true;
            divURL.Visible = false;
            divAttachment.Visible = true;
        }
        rbtnAttachment.Checked = true;
    }
    protected string GetParentID(string ReportID)
    {
        string ParentID = "0";
        try
        {
            string[] parameter = { "@Flag", "@ReportId" };
            string[] value = { "GetParentID", ReportID };
            DB_Status dbs = dba.sp_readSingleData("SP_Reports", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                ParentID = dbs.SingleResult;
            }
        }
        catch (Exception ex)
        {

        }
        return ParentID;
    }
}