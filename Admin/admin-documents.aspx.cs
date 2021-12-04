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
                div_headTitle.InnerHtml = " Admin > Documents / Reports / Document Links";
                hfDeptID.Value = "0";
                if (Session["DocLevelCode"] == null)
                {
                    hfDocLevel.Value = "1";
                    Session["DocLevelCode"] = "[DeptID:0][Level:1][ParentDocID:0]";
                }
            }
            //else if (Session["DeptID"] != null && Session["EmpName"] != null)
            //{
            //    div_headTitle.InnerText = "Department : " + Session["deprt_name"].ToString() + " > Admin > Documents / Reports / Document Links";
            //    hfDeptID.Value = Session["DeptID"].ToString();
            //    hfDocLevel.Value = "1";
            //    if (Session["DocLevelCode"] == null)
            //    {
            //        hfDocLevel.Value = "1";
            //        Session["DocLevelCode"] = "[DeptID:" + hfDeptID.Value + "][Level:1][ParentDocID:0]";
            //    }
            //}
            else
            {
                Response.Redirect("../login.aspx");
            }

            hfDocLevelCode.Value = Session["DocLevelCode"].ToString();
            if (hfDocLevelCode.Value.Contains("[Level:1]"))
            {
                lbtn_AddNew_DocumentTab.Visible = true;
                lbtn_AddNew_Folder.Visible = false;
                lbtn_AddNew_Document.Visible = false;
                lnkbtnBack.Visible = false;
            }
            else
            {
                lbtn_AddNew_DocumentTab.Visible = false;
                lbtn_AddNew_Folder.Visible = true;
                lbtn_AddNew_Document.Visible = true;
                lnkbtnBack.Visible = true;
            }

            panelAddNew_DocumentTab.Visible = false;
            panelAddNew_Folder.Visible = false;
            panelAddNew_Document.Visible = false;
            panelView.Visible = true;
           
            fill_dropdown();
            displayGridMessage("", "");

        }        
    }
    protected void FillReports()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID", "@DocLevelCode" };
            string[] value = { "ViewAdmin", hfDeptID.Value, hfDocLevelCode.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_DocLibrary", 3, parameter, value);
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
    protected void fill_dropdown()
    {
        string[] param = { "@Flag" };
        string[] values = { "LoadDropDownValues" };
        DB_Status DBS = dba.sp_populateDataSet("SP_Employees_Admin", 1, param, values);
        if (DBS.OperationStatus.ToString() == "Success")
        {
            DataSet ds = DBS.ResultDataSet;
            //Table index 0 for Department
            ddlDept.DataSource = ds.Tables[0];
            ddlDept.DataTextField = "Department";
            ddlDept.DataValueField = "DeptID";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, "--Select--");
            ddlDept.Items[0].Value = "0";
            
        }
        else
        {
            displayGridMessage("", "Error");
        }
    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        hfDeptID.Value = "";
        hfDocLevelCode.Value = "";
        Session["DocLevelCode"] = "";
        Session["deprt_name"] = ddlDept.SelectedItem.Text.Trim();
        Session["DeptID"] = ddlDept.SelectedValue.Trim();
        displayGridMessage("", "");

        div_headTitle.InnerText = "Department : " + Session["deprt_name"].ToString() + " > Admin > Documents / Reports / Document Links";
                hfDeptID.Value = Session["DeptID"].ToString();
                hfDocLevel.Value = "1";
       
        if (Session["DocLevelCode"] == null || Session["DocLevelCode"].ToString()=="")
                {
                    hfDocLevel.Value = "1";
                    Session["DocLevelCode"] = "[DeptID:" + hfDeptID.Value + "][Level:1][ParentDocID:0]";
                    hfDocLevelCode.Value = Session["DocLevelCode"].ToString();
                }
        FillReports();
        if (hfDocLevelCode.Value.Contains("[Level:1]"))
        {
            lbtn_AddNew_DocumentTab.Visible = true;
            lbtn_AddNew_Folder.Visible = false;
            lbtn_AddNew_Document.Visible = false;
            lnkbtnBack.Visible = false;
        }
        else
        {
            lbtn_AddNew_DocumentTab.Visible = false;
            lbtn_AddNew_Folder.Visible = true;
            lbtn_AddNew_Document.Visible = true;
            lnkbtnBack.Visible = true;
        }
    }

    #region--Message
    protected void displayMessage_DocumentTab(string msg, string msgtype)
    {
        lblMsg_DocumentTab.Text = msg;
        if (msgtype == "error")
            alert_DocumentTab.Attributes["class"] = "alert alert-error";
        else if (msgtype == "info")
            alert_DocumentTab.Attributes["class"] = "alert alert-info";
        else
            alert_DocumentTab.Attributes["class"] = "";
    }
    protected void displayMessage_Folder(string msg, string msgtype)
    {
        lblMsg_Folder.Text = msg;
        if (msgtype == "error")
            alert_DocumentTab.Attributes["class"] = "alert alert-error";
        else if (msgtype == "info")
            alert_DocumentTab.Attributes["class"] = "alert alert-info";
        else
            alert_DocumentTab.Attributes["class"] = "";
    }
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

    #region--DocumentTab
    protected void lbtn_AddNew_DocumentTab_Click(object sender, EventArgs e)
    {
        if(ddlDept.SelectedItem.Text=="--Select--")
        {
             displayGridMessage("Please Select Department", "error");
            //displayMessage_DocumentTab("Please Select Department", "error");
        }
        else
        {
            panelAddNew_DocumentTab.Visible = true;
            panelView.Visible = false;

            displayMessage_DocumentTab("", "");
            displayGridMessage("", "");
            hfDocID_forEdit.Value = "0";

            txtDocumentTabEnglish.Text = "";
            txtDocumentTabHindi.Text = "";
            txtDocumentTabSeqNo.Text = "";
            btnSave_DocumentTab.Text = "Save";
        }
        
    }
    protected void btnClose_DocumentTab_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hfDocID_forEdit.Value = "0";
        btnSave_DocumentTab.Text = "Save";
        panelAddNew_DocumentTab.Visible = false;
        panelView.Visible = true;
    }
    protected void btnSave_DocumentTab_Click(object sender, EventArgs e)
    {
        try
        {
            string flag = "";
            if (btnSave_DocumentTab.Text == "Save")
                flag = "Add_DocumentTab";
            else if (btnSave_DocumentTab.Text == "Update")
                flag = "Update_DocumentTab";

            if (txtDocumentTabEnglish.Text.Trim() == "")
                displayMessage_DocumentTab("Please enter name of Document Tab in English", "error");
            else if (txtDocumentTabHindi.Text.Trim() == "")
                displayMessage_DocumentTab("Please enter name of Document Tab in Hindi", "error");
            else if (txtDocumentTabSeqNo.Text.Trim() == "")
                displayMessage_DocumentTab("Please enter Sequence", "error");
            else
            {
                string[] parameter = { "@Flag", "@DocID", "@DeptID", "@DocLevel", "@ParentDocRID", "@DocTitleEnglish", "@DocTitleHindi", "@IsDirectory", "@Sequence", "@DocLevelCode" };
                string[] value = { flag, hfDocID_forEdit.Value, hfDeptID.Value, "1", "0", txtDocumentTabEnglish.Text.Trim(), txtDocumentTabHindi.Text.Trim(), "1", txtDocumentTabSeqNo.Text.Trim(), hfDocLevelCode.Value };
                DB_Status dbs = dba.sp_populateDataSet("SP_DocLibrary", 10, parameter, value);
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
                    displayMessage_DocumentTab("Sorry! Document Tab already exists", "error");
                }
                else if (result == "CouldNotCreated")
                {
                    displayMessage_DocumentTab("Sorry! Only 7 Document Tab can be created", "error");
                }
                else if (result == "Inserted" || result== "Updated")
                {
                    FillReports();
                    hfDocID_forEdit.Value = "";
                    btnSave_DocumentTab.Text = "Save";
                    panelAddNew_DocumentTab.Visible = false;
                    panelView.Visible = true;
                }
            }      
        }
        catch (Exception ex)
        {
            displayMessage_DocumentTab(ex.Message, "error");
        }
    }
    #endregion

    #region--Folder
    protected void lbtn_AddNew_Folder_Click(object sender, EventArgs e)
    {
        panelAddNew_Folder.Visible = true; 
        panelView.Visible = false;

        displayMessage_Folder("", "");
        displayGridMessage("", "");
        hfDocID_forEdit.Value = "0";

        txtFolderNameEng.Text = "";
        txtFolderNameHin.Text = "";
        txtFolderSequence.Text = "0";
        btnSave_Folder.Text = "Save";
    }
    protected void btnClose_Folder_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hfDocID.Value = "0";
        btnSave_Folder.Text = "Save";
        panelAddNew_Folder.Visible = false;
        panelView.Visible = true;
    }
    protected void btnSave_Folder_Click(object sender, EventArgs e)
    {
        try
        {
            string flag = "";
            if (btnSave_Folder.Text == "Save")
                flag = "Add_Folder";
            else if (btnSave_Folder.Text == "Update")
                flag = "Update_Folder";

            if (txtFolderNameEng.Text.Trim() == "")
                displayMessage_Folder("Please enter name of Folder in English", "error");
            else if (txtFolderNameHin.Text.Trim() == "")
                displayMessage_Folder("Please enter name of Folder in Hindi", "error");
            else if (txtFolderSequence.Text.Trim() == "")
                displayMessage_Folder("Please enter Sequence", "error");
            else
            {
                string[] parameter = { "@Flag", "@DocID", "@DeptID", "@DocLevel", "@ParentDocRID", "@DocTitleEnglish", "@DocTitleHindi", "@IsDirectory", "@Sequence", "@DocLevelCode" };
                string[] value = { flag, hfDocID_forEdit.Value, hfDeptID.Value, hfDocLevel.Value, hfParentDocRID.Value, txtFolderNameEng.Text.Trim(), txtFolderNameHin.Text.Trim(), "1", txtFolderSequence.Text.Trim(), hfDocLevelCode.Value };
                DB_Status dbs = dba.sp_populateDataSet("SP_DocLibrary", 10, parameter, value);
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
                    displayMessage_Folder("Sorry! Folder already exists", "error");
                }
                else if (result == "Inserted" || result == "Updated")
                {
                    FillReports();
                    btnSave_Folder.Text = "Save";
                    panelAddNew_Folder.Visible = false;
                    panelView.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            displayMessage_DocumentTab(ex.Message, "error");
        }
    }
    #endregion

    #region--Document
    protected void lbtn_AddNew_Document_Click(object sender, EventArgs e)
    {
        panelAddNew_Document.Visible = true;
        panelView.Visible = false;

        displayMessage_Folder("", "");
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
            else if (ddlLinkType.SelectedValue == "Attachment" && (!FileUploader.HasFile) && flag== "Add_Document")
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
                string AttachmentDirectory = "Uploads/DocLibrary/" + hfDeptID.Value + "/" + hfParentDocRID.Value;
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

                string[] parameter = { "@Flag", "@DocID", "@DeptID", "@DocLevel", "@ParentDocRID", "@DocTitleEnglish", "@DocTitleHindi", "@IsDirectory", "@Sequence", "@IsAttachment", "@AttachmentURL", "@IsURL", "@URL", "@DocLevelCode", "@Extension" };
                string[] value = { flag, hfDocID_forEdit.Value, hfDeptID.Value, hfDocLevel.Value, hfParentDocRID.Value, txtDocumentTitleEnglish.Text.Trim(), txtDocumentTitleHindi.Text.Trim(), "0", txtDocumentSequence.Text.Trim(), IsAttachment, AttachmentFilePath, IsURL, txtUrl.Text.Trim(), hfDocLevelCode.Value, ext };
                DB_Status dbs = dba.sp_populateDataSet("SP_DocLibrary", 15, parameter, value);
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
                    displayMessage_DocumentTab("Sorry! Document already exists", "error");
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
            displayMessage_DocumentTab(ex.Message, "error");
        }
    }
    protected void ddlLinkType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLinkType.SelectedValue== "Attachment")
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
                DB_Status dbs = dba.sp_populateDataSet("SP_DocLibrary", 2, parameter, value);
                if (dbs.OperationStatus.ToString() == "Success")
                {
                    DataSet ds = dbs.ResultDataSet;
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            string DocumentType = dt.Rows[0]["DocumentType"].ToString();
                            if (DocumentType== "Document Tab")
                            {
                                panelView.Visible = false;
                                panelAddNew_DocumentTab.Visible = true; 
                                txtDocumentTabEnglish.Text = dt.Rows[0]["DocTitleEnglish"].ToString();
                                txtDocumentTabHindi.Text = dt.Rows[0]["DocTitleHindi"].ToString();
                                txtDocumentTabSeqNo.Text = dt.Rows[0]["Sequence"].ToString();
                                btnSave_DocumentTab.Text = "Update";
                            }
                            else if (DocumentType == "Folder")
                            {
                                panelView.Visible = false;
                                panelAddNew_Folder.Visible = true;
                                txtFolderNameEng.Text = dt.Rows[0]["DocTitleEnglish"].ToString();
                                txtFolderNameHin.Text = dt.Rows[0]["DocTitleHindi"].ToString();
                                txtFolderSequence.Text = dt.Rows[0]["Sequence"].ToString();
                                btnSave_Folder.Text = "Update";
                            }
                            else if (DocumentType == "File")
                            {
                                panelView.Visible = false;
                                panelAddNew_Document.Visible = true;
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
                                    txtUrl.Text= dt.Rows[0]["URL"].ToString();
                                }
                                btnSave_Document.Text = "Update";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                displayMessage_DocumentTab(ex.Message, "error");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void View_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string DocID = (sender as LinkButton).CommandArgument;
            hfDocID.Value = DocID; 
            LinkButton lnkbtnReport = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtnReport.NamingContainer;
            HiddenField hfGrid_DeptID = (HiddenField)grdrow.FindControl("hfGrid_DeptID");
            HiddenField hfGrid_ParentDocRID = (HiddenField)grdrow.FindControl("hfGrid_ParentDocRID");
            HiddenField hfGrid_DocLevel = (HiddenField)grdrow.FindControl("hfGrid_DocLevel");

            hfDeptID.Value = hfGrid_DeptID.Value;
            hfParentDocRID.Value = hfDocID.Value;
            hfDocLevel.Value = hfGrid_DocLevel.Value;

            int DocLevel = 0;
            if (!string.IsNullOrEmpty(hfDocLevel.Value))
            {
                DocLevel = Convert.ToInt32(hfDocLevel.Value);
                DocLevel = DocLevel + 1;
            }

            hfDocLevel.Value = DocLevel.ToString();
            Session["DocLevelCode"] = "[DeptID:" + hfDeptID.Value + "][Level:" + DocLevel.ToString() + "][ParentDocID:" + hfDocID.Value + "]";
            hfDocLevelCode.Value = Session["DocLevelCode"].ToString();

            if (hfDocLevelCode.Value.Contains("[Level:1]"))
            {
                lbtn_AddNew_DocumentTab.Visible = true;
                lbtn_AddNew_Folder.Visible = false;
                lbtn_AddNew_Document.Visible = false;
                lnkbtnBack.Visible = false;
            }
            else
            {
                lbtn_AddNew_DocumentTab.Visible = false;
                lbtn_AddNew_Folder.Visible = true;
                lbtn_AddNew_Document.Visible = true;
                lnkbtnBack.Visible = true;
            }

            FillReports();
        }
        catch (Exception ex)
        {
            displayGridMessage(ex.Message, "error");
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
                    DB_Status dbs = dba.sp_InsertUpdateDelete("SP_DocLibrary", 2, parameter, value);
                    FillReports();
                }
                catch (Exception ex)
                {
                    displayMessage_DocumentTab(ex.Message, "error");
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
    protected void lnkbtnBack_Click(object sender, EventArgs e)
    {
        try
        {
            hfParentDocRID.Value = GetParentID(hfParentDocRID.Value);
            int DocLevel = Convert.ToInt32(hfDocLevel.Value);
            if (hfParentDocRID.Value != "")
            {                
                if (!string.IsNullOrEmpty(hfDocLevel.Value))
                {
                    DocLevel = Convert.ToInt32(hfDocLevel.Value);
                    DocLevel = DocLevel - 1;
                }
            }
            else
            {
                hfParentDocRID.Value = "0";
            }
            hfDocLevel.Value = DocLevel.ToString();

            Session["DocLevelCode"] = "[DeptID:" + hfDeptID.Value + "][Level:" + DocLevel.ToString() + "][ParentDocID:" + hfParentDocRID.Value + "]";
            hfDocLevelCode.Value = Session["DocLevelCode"].ToString();

            if (hfDocLevelCode.Value.Contains("[Level:1]"))
            {
                lbtn_AddNew_DocumentTab.Visible = true;
                lbtn_AddNew_Folder.Visible = false;
                lbtn_AddNew_Document.Visible = false;
                lnkbtnBack.Visible = false;
            }
            else
            {
                lbtn_AddNew_DocumentTab.Visible = false;
                lbtn_AddNew_Folder.Visible = true;
                lbtn_AddNew_Document.Visible = true;
                lnkbtnBack.Visible = true;
            }

            FillReports();
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