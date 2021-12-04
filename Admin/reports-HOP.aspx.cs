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

        if (!IsPostBack)
        {
            Session["DocLevelCode"] = null;
            if (Session["AdminUserID"] != null)
            {
                div_headTitle.InnerHtml = "Admin > Documents / Reports / Document Links";
                hfDeptID.Value = "0";
                if (Session["DocLevelCode"] == null)
                {
                    hfDocLevel.Value = "1";
                    Session["DocLevelCode"] = "[DeptID:0][Level:1][ParentDocID:0]";
                }
            }
            else if (Session["DeptID"] != null && Session["EmpName"] != null)
            {
                div_headTitle.InnerText = "Department : " + Session["deprt_name"].ToString() + " > Admin > Documents / Reports / Document Links";
                hfDeptID.Value = Session["DeptID"].ToString();
                hfDocLevel.Value = "1";
                if (Session["DocLevelCode"] == null)
                {
                    hfDocLevel.Value = "1";
                    Session["DocLevelCode"] = "[DeptID:" + hfDeptID.Value + "][Level:1][ParentDocID:0]";
                }
            }
            else
            {
                Response.Redirect("../login.aspx");
            }

            hfDocLevelCode.Value = Session["DocLevelCode"].ToString();
            if (hfDocLevelCode.Value.Contains("[Level:1]"))
            {
                lbtn_AddNew_Document.Visible = true;
            }
            else
            {

                lbtn_AddNew_Document.Visible = true;
            }
            panelAddNew_Document.Visible = false;
            panelView.Visible = true;
            FillDepartments();
            FillReports();
            displayGridMessage("", "");

        }
    }

    protected void FillDepartments()
    {
        try
        {
            string[] parameter = { "@Flag" };
            string[] value = { "LoadDepartments" };
            DB_Status dbs = dba.sp_populateDataSet("SP_Circulars", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    ddlDepartment.DataSource = dt;
                    ddlDepartment.DataValueField = "DeptID";
                    ddlDepartment.DataTextField = "Department";
                    ddlDepartment.DataBind();
                }
            }
            if (hfDeptID.Value == "0")
            {
                ddlDepartment.Enabled = true;
                ddlDepartment.SelectedValue = "0";
            }
            else
            {
                ddlDepartment.SelectedValue = hfDeptID.Value;
                ddlDepartment.Enabled = false;
            }
        }
        catch (Exception)
        {
        }
    }

    protected void FillReports()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "ViewAdmin", hfDeptID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_HOPReports", 2, parameter, value);
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

    #region--Message

    protected void displayMessage_Document(string msg, string msgtype)
    {
        lblMsg_Document.Text = msg;
        if (msgtype == "error")
            alert_Document.Attributes["class"] = "alert alert-error";
        else if (msgtype == "info")
            alert_Document.Attributes["class"] = "alert alert-info";
        else
            alert_Document.Attributes["class"] = "";
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
    #endregion


    #region--Document
    protected void lbtn_AddNew_Document_Click(object sender, EventArgs e)
    {
        panelAddNew_Document.Visible = true;
        panelView.Visible = false;


        displayGridMessage("", "");
        hfDocID_forEdit.Value = "0";

        txtDocumentTitleEnglish.Text = "";
        txtDocumentTitleHindi.Text = "";
        txtDocumentSequence.Text = "0";
        ddlLinkType.SelectedValue = "Attachment";
        divAttachment.Visible = true;
        divURL.Visible = false;
        btnSave_Document.Text = "Save";
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue == "9999")
        {
            //divOtherSource.Visible = true;
        }
        else
        {
            //divOtherSource.Visible = false;
        }
    }
    protected void btnClose_Document_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hfDocID.Value = "0";
        btnSave_Document.Text = "Save";
        panelAddNew_Document.Visible = false;
        panelView.Visible = true;
    }
    protected void btnSave_Document_Click(object sender, EventArgs e)
    {
        try
        {
            string flag = "";
            if (btnSave_Document.Text == "Save")
                flag = "Add_Document";
            else if (btnSave_Document.Text == "Update")
                flag = "Update_Document";

            if (txtDocumentTitleEnglish.Text.Trim() == "")
                displayMessage_Document("Please enter Document Title in English", "error");
            else if (txtDocumentTitleHindi.Text.Trim() == "")
                displayMessage_Document("Please enter Document Title in Hindi", "error");
            else if (txtDocumentSequence.Text.Trim() == "")
                displayMessage_Document("Please enter Sequence", "error");
            else if (ddlLinkType.SelectedValue == "Attachment" && (!FileUploader.HasFile) && flag == "Add_Document")
                displayMessage_Document("Please browse file", "error");
            else if (ddlLinkType.SelectedValue == "Link" && txtUrl.Text.Trim() == "")
                displayMessage_Document("Please enter Link URL", "error");
            else
            {
                bool flagValidFile = true;
                bool flagHasFile = false;

                string IsAttachment = "0";
                string IsURL = "0";
                string AttachmentFileName = "";
                string AttachmentDirectory = "Uploads/HOPReports/" + hfDeptID.Value + "/" + hfParentDocRID.Value;
                string AttachmentFilePath = "NA";
                string ext = "";

                if (ddlLinkType.SelectedValue == "Attachment")
                    IsAttachment = "1";
                if (ddlLinkType.SelectedValue == "Link")
                    IsURL = "1";

                if (FileUploader.HasFile)
                {
                    flagHasFile = true;
                    ext = Path.GetExtension(FileUploader.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                    AttachmentFileName = FileUploader.FileName;
                    AttachmentFilePath = AttachmentDirectory + "/" + AttachmentFileName;
                    hfDocUploadPath.Value = AttachmentFilePath;
                    flagValidFile = true;
                }

                string[] parameter = { "@Flag", "@DocID", "@DeptID", "@DocTitleEnglish", "@DocTitleHindi", "@Sequence", "@IsAttachment", "@AttachmentURL", "@IsURL", "@URL", "@Extension" };
                string[] value = { flag, hfDocID_forEdit.Value, ddlDepartment.SelectedValue, txtDocumentTitleEnglish.Text.Trim(), txtDocumentTitleHindi.Text.Trim(), txtDocumentSequence.Text.Trim(), IsAttachment, AttachmentFilePath, IsURL, txtUrl.Text.Trim(), ext };
                DB_Status dbs = dba.sp_populateDataSet("SP_HOPReports", 11, parameter, value);
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
                    displayMessage_Document("Sorry! Document already exists", "error");
                }
                else if (result == "Inserted" || result == "Updated")
                {
                    if (!Directory.Exists(AttachmentDirectory))
                        Directory.CreateDirectory(Server.MapPath("~/" + AttachmentDirectory));
                    if (flagHasFile && flagValidFile)
                        FileUploader.SaveAs(Server.MapPath("~/" + AttachmentFilePath));

                    FillReports();
                    hfDocID.Value = "";
                    btnSave_Document.Text = "Save";
                    panelAddNew_Document.Visible = false;
                    panelView.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            displayMessage_Document(ex.Message, "error");
        }
    }
    protected void ddlLinkType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLinkType.SelectedValue == "Attachment")
        {
            divAttachment.Visible = true;
            divURL.Visible = false;
        }
        else if (ddlLinkType.SelectedValue == "Link")
        {
            divAttachment.Visible = false;
            divURL.Visible = true;
        }
    }
    #endregion

    #region--GridActions
    protected void Update_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string DocID = (sender as LinkButton).CommandArgument;
            hfDocID_forEdit.Value = DocID;
            try
            {
                string[] parameter = { "@Flag", "@DocID" };
                string[] value = { "LoadbyDocID", hfDocID_forEdit.Value };
                DB_Status dbs = dba.sp_populateDataSet("SP_HOPReports", 2, parameter, value);
                if (dbs.OperationStatus.ToString() == "Success")
                {
                    DataSet ds = dbs.ResultDataSet;
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            panelView.Visible = false;
                            panelAddNew_Document.Visible = true;
                            ddlDepartment.SelectedValue = dt.Rows[0]["DeptID"].ToString();
                            txtDocumentTitleEnglish.Text = dt.Rows[0]["DocTitleEnglish"].ToString();
                            txtDocumentTitleHindi.Text = dt.Rows[0]["DocTitleHindi"].ToString();
                            txtDocumentSequence.Text = dt.Rows[0]["Sequence"].ToString();
                            ddlLinkType.SelectedValue = dt.Rows[0]["LinkType"].ToString();
                            if (ddlLinkType.SelectedValue == "Attachment")
                            {
                                divAttachment.Visible = true;
                                divURL.Visible = false;
                            }
                            else if (ddlLinkType.SelectedValue == "Link")
                            {
                                divAttachment.Visible = false;
                                divURL.Visible = true;
                                txtUrl.Text = dt.Rows[0]["URL"].ToString();
                            }
                            btnSave_Document.Text = "Update";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //displayMessage_DocumentTab(ex.Message, "error");
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
            try
            {
                displayGridMessage("", "");
                string DocID = (sender as LinkButton).CommandArgument;
                hfDocID_forEdit.Value = DocID;
                try
                {
                    string[] parameter = { "@Flag", "@DocID" };
                    string[] value = { "Delete", hfDocID_forEdit.Value };
                    DB_Status dbs = dba.sp_InsertUpdateDelete("SP_HOPReports", 2, parameter, value);
                    FillReports();
                }
                catch (Exception ex)
                {
                    //displayMessage_DocumentTab(ex.Message, "error");
                }
            }
            catch (Exception ex)
            {

            }
        }
        catch (Exception ex)
        {

        }
    }

    protected string GetParentID(string DocID)
    {
        string ParentID = "0";
        try
        {
            string[] parameter = { "@Flag", "@DocID" };
            string[] value = { "GetParentID", DocID };
            DB_Status dbs = dba.sp_readSingleData("SP_DocLibrary", 2, parameter, value);
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
    #endregion


}