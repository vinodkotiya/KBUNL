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
            hdfDept_Id.Value = "0";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Session["deprt_name"].ToString() + " > Report";
            hdfDept_Id.Value = Session["DeptID"].ToString();
        }
        else
        {
            Response.Redirect("../login.aspx");
        }
        lnkbtnBack.Visible = false;
        if (Request.QueryString["Level"] != null && Request.QueryString["ParentReportID"] != null)
        {
            hdfReportLevel.Value = Convert.ToString(Request.QueryString["Level"]);
            hdfParentReportId.Value = Convert.ToString(Request.QueryString["ParentReportID"]);
            if (hdfReportLevel.Value == "1" || hdfReportLevel.Value == "0")
            {
                lnkbtnBack.Visible = false;
            }
            else
            {
                lnkbtnBack.Visible = true;
                lnkbtnBack.HRef = "report.aspx?Level=" + (Convert.ToInt16(hdfReportLevel.Value) - 1).ToString() + "&ParentReportID=" + GetParentID(hdfParentReportId.Value);
            }
        }
        if (!IsPostBack)
        {
            
            panelAddNew.Visible = false;
            panelView.Visible = true;
            //Fill Articles
            BindYearDropdownlist();
            FillReport();
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
        hdfReportId.Value = "0";
        hdfFile_UploadedPath.Value = "NA";
        ddlReportType.SelectedIndex = 0;
        txtReportDate.Text = "";
        ddlYear.SelectedIndex = 0;
        ddlmonth.SelectedIndex = 0;
        divRptYear.Visible = false;
        divRptMonth.Visible = false;
        txtTitleEnglish.Text = "";
        txtTitleHindi.Text = "";
        btnSave.Text = "Save";
        span_FileStatus.Style.Add("display", "none");
        RequiredFieldValidatorFileUploadImage.Enabled = true;
        span_FileStatus.InnerText = "";

        divAttachment.Visible = true;
        divURL.Visible = false;
        divrptGroup.Visible = false;
        divlnkType.Visible = false;
        chkReportGroup.Checked = true;
        txtSeqNo.Text = "";
        rbtnAttachment.Checked = true;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hdfReportId.Value = "0";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
        hdfFile_UploadedPath.Value = "NA";
        span_FileStatus.Style.Add("display", "none");
        span_FileStatus.InnerText = "";
        RequiredFieldValidatorFileUploadImage.Enabled = true;
    }
    public void BindYearDropdownlist()
    {
        int currentYear = DateTime.Now.Year;
        int j = 2010;
        for (int i = currentYear; i >= j; i--)
        {
            ddlYear.Items.Add(Convert.ToString(i));
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (ddlReportType.SelectedIndex == 0)
                    displayMessage("Please select report type", "error");
                else if (txtTitleEnglish.Text.Trim() == "")
                    displayMessage("Please enter report title", "error");
                else if (txtTitleHindi.Text.Trim() == "")
                    displayMessage("Please enter report title", "error");
                else
                {
                    bool flagValidFile = true;
                    bool flagHasImage = false;
                    string ImageFileName = "";
                    string ImagePath = "NA";
                    if (FileUploadImage.HasFile)
                    {
                        flagHasImage = true;
                        string ext = Path.GetExtension(FileUploadImage.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        ImageFileName = "Report_" + datevalue + ext;

                        ImagePath = "Uploads/Report/" + ImageFileName;
                        hdfFile_UploadedPath.Value = ImagePath;
                        if (!(ext.ToUpper() == ".JPG" || ext.ToUpper() == ".PNG" || ext.ToUpper() == ".JPEG" || ext.ToUpper() == ".GIF" || ext.ToUpper() == ".DOC" || ext.ToUpper() == ".DOCX" || ext.ToUpper() == ".PDF" || ext.ToUpper() == ".XLS" || ext.ToUpper() == ".XLSX"))
                        {
                            flagValidFile = false;
                            displayMessage("Please attach valid file", "error");
                            return;
                        }
                        else
                            flagValidFile = true;
                    }
                    DB_Status dbs;
                    if (ddlReportType.SelectedIndex==4)
                    {
                        string ReportGroup = "", linkType = "", AttachmentFileAndURL = "";
                        if (chkReportGroup.Checked)
                            ReportGroup = "True";
                        else
                            ReportGroup = "False";
                        if (chkReportGroup.Checked)
                            linkType = null;
                        else
                        {
                            if (rbtnAttachment.Checked)
                                linkType = "Attachment";
                            else if (rbtnURL.Checked)
                                linkType = "URL";
                        }
                        if (linkType == "Attachment")
                            AttachmentFileAndURL = hdfFile_UploadedPath.Value;
                        else if (linkType == "URL")
                            AttachmentFileAndURL = txtUrl.Text.Trim();
                        else
                            AttachmentFileAndURL = "NA";
                        string[] parameter = { "@Flag", "@DeptId","@ReportLevel","@ParentReportId","@ReportType", "@ReportDate", "@ReportYear", "@ReportMonth", "@ReportTitleEnglish", "@ReportTitleHindi", "@ReportGroup", "@SeqNo", "@LinkType","@AttachmentUrl" };
                        string[] value = { "Insert",hdfDept_Id.Value, hdfReportLevel.Value, hdfParentReportId.Value, ddlReportType.SelectedValue,null, ddlYear.SelectedValue, ddlmonth.SelectedValue, txtTitleEnglish.Text.Trim(), txtTitleHindi.Text.Trim(),ReportGroup,txtSeqNo.Text.Trim(), linkType,AttachmentFileAndURL };
                        dbs = dba.sp_populateDataSet("SP_Report",14, parameter, value);
                    }
                    else
                    {
                        string ReportDate = "";
                        if (!string.IsNullOrEmpty(txtReportDate.Text.Trim()))
                            ReportDate = obj.makedate(txtReportDate.Text.Trim());
                        else
                            ReportDate = null;
                        string[] parameter = { "@Flag", "@DeptId","@ReportType", "@ReportLevel","@ParentReportId","@ReportYear", "@ReportMonth", "@ReportDate","@ReportTitleEnglish", "@ReportTitleHindi","@AttachmentUrl"};
                        string[] value = { "Insert", hdfDept_Id.Value,ddlReportType.SelectedValue,"1","0",ddlYear.SelectedValue, ddlmonth.SelectedValue,ReportDate ,txtTitleEnglish.Text.Trim(), txtTitleHindi.Text.Trim(), hdfFile_UploadedPath.Value };
                        dbs = dba.sp_populateDataSet("SP_Report",11, parameter, value);
                    }
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
                        displayMessage("Sorry! Report already exists", "error");
                    }
                    else if (result == "Success")
                    {
                        if (flagHasImage && flagValidFile)
                            FileUploadImage.SaveAs(Server.MapPath("~/" + ImagePath));

                        displayMessage("Report successfully added", "info");
                        FillReport();
                        btnSave.Text = "Save";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
                    }
                }
            }
            else if (btnSave.Text == "Update")
            {
                if (ddlReportType.SelectedIndex == 0)
                    displayMessage("Please select report type", "error");
                else if (txtTitleEnglish.Text.Trim() == "")
                    displayMessage("Please enter report title", "error");
                else if (txtTitleHindi.Text.Trim() == "")
                    displayMessage("Please enter report title", "error");
                else
                {
                    bool flagValidFile = true;
                    bool flagHasImage = false;
                    string ImageFileName = "";
                    string ImagePath = "NA";

                    if (FileUploadImage.HasFile)
                    {
                        flagHasImage = true;
                        string ext = Path.GetExtension(FileUploadImage.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        ImageFileName = "Report_" + datevalue + ext;
                        ImagePath = "Uploads/Report/" + ImageFileName;
                        if (!(ext.ToUpper() == ".JPG" || ext.ToUpper() == ".PNG" || ext.ToUpper() == ".JPEG" || ext.ToUpper() == ".GIF" || ext.ToUpper() == ".DOC" || ext.ToUpper() == ".DOCX" || ext.ToUpper() == ".PDF" || ext.ToUpper() == ".XLS" || ext.ToUpper() == ".XLSX"))
                        {
                            flagValidFile = false;
                            displayMessage("Please attach valid file", "error");
                            return;
                        }
                        else
                            flagValidFile = true;
                    }
                    else
                    {
                        ImagePath = hdfFile_UploadedPath.Value;
                    }
                    DB_Status dbs;
                    if (ddlReportType.SelectedIndex == 4)
                    {
                        string ReportGroup = "", linkType = "", AttachmentFileAndURL = "";
                        if (chkReportGroup.Checked)
                            ReportGroup = "True";
                        else
                            ReportGroup = "False";
                        if (chkReportGroup.Checked)
                            linkType = null;
                        else
                        {
                            if (rbtnAttachment.Checked)
                                linkType = "Attachment";
                            else if (rbtnURL.Checked)
                                linkType = "URL";
                        }
                        if (linkType == "Attachment")
                            AttachmentFileAndURL = ImagePath;
                        else if (linkType == "URL")
                            AttachmentFileAndURL = txtUrl.Text.Trim();
                        else
                            AttachmentFileAndURL = "NA";
                        string[] parameter = { "@Flag", "@ReportId","@DeptId", "@ReportLevel", "@ParentReportId", "@ReportType", "@ReportDate", "@ReportYear", "@ReportMonth", "@ReportTitleEnglish", "@ReportTitleHindi", "@ReportGroup", "@SeqNo", "@LinkType", "@AttachmentUrl" };
                        string[] value = { "Update", hdfReportId.Value,hdfDept_Id.Value, hdfReportLevel.Value, hdfParentReportId.Value, ddlReportType.SelectedValue, null, ddlYear.SelectedValue, ddlmonth.SelectedValue, txtTitleEnglish.Text.Trim(), txtTitleHindi.Text.Trim(), ReportGroup, txtSeqNo.Text.Trim(), linkType, AttachmentFileAndURL };
                        dbs = dba.sp_populateDataSet("SP_Report", 15, parameter, value);
                    }
                    else
                    {
                        string ReportDate = "";
                        if (!string.IsNullOrEmpty(txtReportDate.Text.Trim()))
                            ReportDate = obj.makedate(txtReportDate.Text.Trim());
                        else
                            ReportDate = null;
                        string[] parameter = { "@Flag", "@ReportId", "@DeptId", "@ReportType", "@ReportYear", "@ReportMonth", "@ReportDate", "@ReportTitleEnglish", "@ReportTitleHindi", "@AttachmentUrl" };
                        string[] value = { "Update", hdfReportId.Value,hdfDept_Id.Value, ddlReportType.SelectedValue, ddlYear.SelectedValue, ddlmonth.SelectedValue, ReportDate, txtTitleEnglish.Text.Trim(), txtTitleHindi.Text.Trim(),ImagePath };
                        dbs = dba.sp_populateDataSet("SP_Report",10, parameter, value);
                    }
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
                        displayMessage("Sorry! Report already exists", "error");
                    }
                    else if (result == "Success")
                    {
                        if (flagHasImage && flagValidFile)
                        {
                            FileInfo file = new FileInfo(Server.MapPath("~/"+hdfFile_UploadedPath.Value));
                            if(file.Exists)
                                file.Delete();
                            FileUploadImage.SaveAs(Server.MapPath("~/" + ImagePath));
                        }
                        FillReport();
                        hdfReportId.Value = "0";
                        hdfFile_UploadedPath.Value = "NA";
                        btnSave.Text = "Save";
                        txtTitleEnglish.Text = "";
                        txtTitleHindi.Text = "";
                        ddlYear.SelectedIndex = 0;
                        ddlmonth.SelectedIndex = 0;
                        divRptYear.Visible = false;
                        divRptMonth.Visible = false;
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

    StringBuilder SearchCondition()
    {
        StringBuilder sb = new StringBuilder();
        if (Request.QueryString["Level"] != null && Request.QueryString["ParentReportID"] != null)
        {
            sb.Append(" AND ReportLevel=" + hdfReportLevel.Value + " AND ParentReportID=" + Convert.ToInt32(hdfParentReportId.Value));
        }
        else
            sb.Append(" AND (ReportLevel = 1 OR ReportLevel IS NULL)  AND ( ParentReportID = 0 OR ParentReportID IS NULL)");
            return sb;
    } 
    protected void FillReport()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptId", "@Search" };
            string[] value = { "View", hdfDept_Id.Value,Convert.ToString(SearchCondition()) };
            DB_Status dbs = dba.sp_populateDataSet("SP_Report", 3, parameter, value);
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
            hdfReportId.Value = ID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@ReportId", "@DeptID" };
            string[] value = { "ViewByID", hdfReportId.Value, hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Report", 3, parameter, value);
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
                        ddlReportType.SelectedValue = Convert.ToString(dt.Rows[0]["ReportType"]);
                        if (ddlReportType.SelectedIndex == 1)
                        {
                            divRptYear.Visible = false;
                            divRptMonth.Visible = false;
                            divRptDate.Visible = true;
                            RequiredFieldValidatorddlYear.Enabled = false;
                            RequiredFieldValidatorddlmonth.Enabled = false;
                            RequiredFieldValidatortxtReportDate.Enabled = true;
                        }
                        else if (ddlReportType.SelectedIndex == 2)
                        {
                            divRptYear.Visible = true;
                            divRptMonth.Visible = true;
                            divRptDate.Visible = false;
                            RequiredFieldValidatorddlYear.Enabled = true;
                            RequiredFieldValidatorddlmonth.Enabled = true;
                            RequiredFieldValidatortxtReportDate.Enabled = false;
                        }
                        else if (ddlReportType.SelectedIndex == 3)
                        {
                            divRptYear.Visible = true;
                            divRptMonth.Visible = false;
                            divRptDate.Visible = false;
                            RequiredFieldValidatorddlYear.Enabled = true;
                            RequiredFieldValidatorddlmonth.Enabled = false;
                            RequiredFieldValidatortxtReportDate.Enabled = false;
                        }
                        else if (ddlReportType.SelectedIndex ==4)
                        {
                            divRptYear.Visible = false;
                            divRptMonth.Visible = false;
                            divRptDate.Visible = false;
                            RequiredFieldValidatorddlYear.Enabled = false;
                            RequiredFieldValidatorddlmonth.Enabled = false;
                            RequiredFieldValidatortxtReportDate.Enabled = false;
                            string reportGroup = "", linkType = "";
                            reportGroup= Convert.ToString(dt.Rows[0]["ReportGroup"]);
                            if (reportGroup == "True")
                            {
                                divURL.Visible = false;
                                divAttachment.Visible = false;
                                divlnkType.Visible = false;
                                divrptGroup.Visible = true;
                                chkReportGroup.Checked = true;
                                
                            }
                            else if (reportGroup == "False")
                            {
                                divrptGroup.Visible = true;
                                divlnkType.Visible = true;
                                chkReportGroup.Checked = false;
                                linkType = Convert.ToString(dt.Rows[0]["LinkType"]);
                                if (linkType == "Attachment")
                                {
                                    rbtnAttachment.Checked = true;
                                    rbtnURL.Checked = false;
                                    txtUrl.Text = "";
                                    divAttachment.Visible = true;
                                    divURL.Visible = false;
                                }
                               else if (linkType == "URL")
                                {
                                    rbtnAttachment.Checked =false;
                                    rbtnURL.Checked = true;
                                    txtUrl.Text = Convert.ToString(dt.Rows[0]["AttachmentUrl"]);
                                    divAttachment.Visible = false;
                                    divURL.Visible = true;
                                }
                            }
                        }
                        txtSeqNo.Text = Convert.ToString(dt.Rows[0]["SeqNo"]);
                        txtReportDate.Text = Convert.ToString(dt.Rows[0]["ReportDate"]);
                        ddlYear.SelectedValue = Convert.ToString(dt.Rows[0]["ReportYear"]);
                        ddlmonth.SelectedValue = Convert.ToString(dt.Rows[0]["ReportMonth"]);
                        txtTitleEnglish.Text = Convert.ToString(dt.Rows[0]["ReportTitleEnglish"]);
                        txtTitleHindi.Text = Convert.ToString(dt.Rows[0]["ReportTitleHindi"]);
                        hdfFile_UploadedPath.Value = Convert.ToString(dt.Rows[0]["AttachmentUrl"]);
                        if (!string.IsNullOrEmpty(hdfFile_UploadedPath.Value) && hdfFile_UploadedPath.Value != "No")
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
            string ReportId = (sender as LinkButton).CommandArgument;
            hdfReportId.Value = ReportId;

            string[] parameter = { "@Flag", "@ReportId", "@DeptID" };
            string[] value = { "Delete", hdfReportId.Value, hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Report", 3, parameter, value);
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
                hdfReportId.Value = "0";
                FillReport();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReportType.SelectedIndex == 1)
        {
            divRptYear.Visible = false;
            divRptMonth.Visible = false;
            divRptDate.Visible = true;
            RequiredFieldValidatorddlYear.Enabled = false;
            RequiredFieldValidatorddlmonth.Enabled = false;
            RequiredFieldValidatortxtReportDate.Enabled = true;
            divrptGroup.Visible = false;
            divAttachment.Visible = true;
        }
        else if (ddlReportType.SelectedIndex == 2)
        {
            divRptYear.Visible = true;
            divRptMonth.Visible = true;
            divRptDate.Visible = false;
            RequiredFieldValidatorddlYear.Enabled = true;
            RequiredFieldValidatorddlmonth.Enabled = true;
            RequiredFieldValidatortxtReportDate.Enabled = false;
            divrptGroup.Visible = false;
            divAttachment.Visible = true;
        }
        else if (ddlReportType.SelectedIndex == 3)
        {
            divRptYear.Visible = true;
            divRptMonth.Visible = false;
            divRptDate.Visible = false;
            RequiredFieldValidatorddlYear.Enabled = true;
            RequiredFieldValidatorddlmonth.Enabled = false;
            RequiredFieldValidatortxtReportDate.Enabled = false;
            divrptGroup.Visible = false;
            divAttachment.Visible = true;
        }
        else
        {
            divRptYear.Visible = false;
            divRptMonth.Visible = false;
            divRptDate.Visible = false;
            RequiredFieldValidatorddlYear.Enabled = false;
            RequiredFieldValidatorddlmonth.Enabled = false;
            RequiredFieldValidatortxtReportDate.Enabled = false;
            divrptGroup.Visible = true;
            divAttachment.Visible = false;
        }
    }

    protected void rbtnURL_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnURL.Checked)
        {
            divURL.Visible = true;
            RequiredFieldValidatortxtUrl.Enabled = true;
            divAttachment.Visible = false;
            RequiredFieldValidatorFileUploadImage.Enabled = false;
        }
        else
        {
            divURL.Visible = false;
            RequiredFieldValidatortxtUrl.Enabled = false;
            divAttachment.Visible = true;
            RequiredFieldValidatorFileUploadImage.Enabled = true;
        }
        if (btnSave.Text == "Save")
            txtUrl.Text = "";
    }

    protected void rbtnAttachment_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnAttachment.Checked)
        {
            divURL.Visible = false;
            RequiredFieldValidatortxtUrl.Enabled = false;
            divAttachment.Visible = true;
            RequiredFieldValidatorFileUploadImage.Enabled = true;
        }
        else
        {
            divURL.Visible = true;
            RequiredFieldValidatortxtUrl.Enabled = true;
            divAttachment.Visible = false;
            RequiredFieldValidatorFileUploadImage.Enabled = false;
        }
        if (btnSave.Text == "Save")
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
        if (btnSave.Text =="Save")
            txtUrl.Text = "";
        rbtnAttachment.Checked = true;
        rbtnURL.Checked = false;
    }
    protected string GetParentID(string ReportID)
    {
        string ParentID = "0";
        try
        {
            string[] parameter = { "@Flag", "@ReportId" };
            string[] value = { "GetParentID", ReportID };
            DB_Status dbs = dba.sp_readSingleData("SP_Report", 2, parameter, value);
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