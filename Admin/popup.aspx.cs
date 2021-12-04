using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_popup : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] != null)
        {
            hdfDept_Id.Value = "0";
            div_headTitle.InnerText = "Admin > Popup";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Convert.ToString(Session["deprt_name"]) + " > Popup";
            hdfDept_Id.Value = Session["DeptID"].ToString();
        }
        else
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {
            ListItem li;
            string hr = "";
            for (int i = 0; i < 24; i++)
            {
                if (i < 10)
                    hr = "0" + i + ":00";
                li = new ListItem(hr, hr);
                ddlDisplayFromTime.Items.Add(li);
            }

            panelAddNew.Visible = false;
            panelView.Visible = true;
            FillPopup();
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
        hdfRIDPopup.Value = "0";
        hdfPopupImagePath.Value = "NA";
        hdfAttachmentPath.Value = "NA";
        hdfPopupVideoPath.Value = "NA";

        txtTitleEnglish.Text = "";
        txtTitleHindi.Text = "";
        ddlPopupType.SelectedValue = "Image";
        divPopupImage.Visible = true;
        divPopupVideo.Visible = false;
        txtDetailsEnglish.Text = "";
        txtDetailsHindi.Text = "";
        ddlLinkType.SelectedIndex = 0;
        divURL.Visible = false;
        txtLinkURL.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtTitleEnglish.Text == "")
                    displayMessage("Please enter title", "error");
                else if (txtTitleHindi.Text == "")
                    displayMessage("Please enter title", "error");
                else if(ddlPopupType.SelectedValue=="Image" && !fupPopupImage.HasFile)
                    displayMessage("Please browse image for popup", "error");
                else if (ddlPopupType.SelectedValue == "Video" && !fupPopupVideo.HasFile)
                    displayMessage("Please browse video for popup", "error");
                else if (ddlPopupType.SelectedValue == "Text" && txtDetailsEnglish.Text.Trim()=="")
                    displayMessage("Please enter details in english", "error");
                else if (ddlPopupType.SelectedValue == "Text" && txtDetailsHindi.Text.Trim() == "")
                    displayMessage("Please enter details in hindi", "error");
                else
                {
                    bool flagValidFile = true;
                    bool flagHasFile = true;
                    string Popup_Image_FileName = "NA";
                    string Popup_Image_FilePath = "NA";
                    
                    //Check File is attached or not
                    if (fupPopupImage.HasFile)
                    {
                        string ext = Path.GetExtension(fupPopupImage.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                        Popup_Image_FileName = fupPopupImage.FileName;
                        Popup_Image_FilePath = "../Uploads/Popup/PopupImage/" + Popup_Image_FileName;
                        hdfPopupImagePath.Value = Popup_Image_FilePath;
                        if (!(ext.ToUpper() == ".JPG" || ext.ToUpper() == ".PNG" || ext.ToUpper() == ".JPEG" || ext.ToUpper() == ".GIF"))
                        {
                            flagValidFile = false;
                            displayMessage("Please attach valid image file", "error");
                        }
                        else
                            flagValidFile = true;
                    }

                    bool flagValidFile_video = true;
                    bool flagHasFile_video = true;
                    string Popup_Video_FileName = "NA";
                    string Popup_Video_FilePath = "NA";

                    //Check Video is attached or not
                    if (fupPopupVideo.HasFile)
                    {
                        string ext = Path.GetExtension(fupPopupVideo.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                        Popup_Video_FileName = fupPopupVideo.FileName;
                        Popup_Video_FilePath = "../Uploads/Popup/" + Popup_Video_FileName;
                        hdfPopupVideoPath.Value = Popup_Video_FilePath;
                        if (!(ext.ToUpper() == ".MP4" || ext.ToUpper() == ".MOV" || ext.ToUpper() == ".AVI"))
                        {
                            flagValidFile_video = false;
                            displayMessage("Please attach valid video file", "error");
                        }
                        else
                            flagValidFile_video = true;
                    }

                    bool flagAttachValidFile = true;
                    bool flagAttachHasFile = true;
                    string Popup_Attachment_FileName = "NA";
                    string Popup_Attachment_FilePath = "NA";

                    //Check File is attached or not
                    if (fupAttachment.HasFile)
                    {
                        string ext = Path.GetExtension(fupAttachment.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                        Popup_Attachment_FileName = fupAttachment.FileName;
                        Popup_Attachment_FilePath = "../Uploads/Popup/Attachment/" + Popup_Attachment_FileName;
                        hdfAttachmentPath.Value = Popup_Attachment_FilePath;
                        if (!(ext.ToUpper() == ".DOC" || ext.ToUpper() == ".DOCX" || ext.ToUpper() == ".PDF" || ext.ToUpper() == ".XLS" || ext.ToUpper() == ".XLSX"))
                        {
                            flagAttachValidFile = false;
                            displayMessage("Please attach valid file", "error");
                        }
                        else
                            flagAttachValidFile = true;
                    }

                    string LinkAndAttachment = "";
                    if (ddlLinkType.SelectedValue == "Attachment")
                    {
                        LinkAndAttachment = hdfAttachmentPath.Value;
                    }
                    else if (ddlLinkType.SelectedValue == "Link")
                    {
                        LinkAndAttachment = txtLinkURL.Text.Trim();
                    }
                    string[] param = { "@Flag", "@DeptID", "@TitleHindi", "@TitleEnglish", "@PopUpContentHindi", "@PopUpContentEnglish","@PopupType", "@PopupImage","@PopupVideo", "@LinkType", "@LinkAndAttachment" };
                    string[] value = { "Add", hdfDept_Id.Value, txtTitleHindi.Text.Trim(), txtTitleEnglish.Text.Trim(), txtDetailsHindi.Text.Trim(), txtDetailsEnglish.Text.Trim(), ddlPopupType.SelectedValue, hdfPopupImagePath.Value, hdfPopupVideoPath.Value, ddlLinkType.SelectedValue, LinkAndAttachment };
                    DB_Status dbs = dba.sp_populateDataSet("SP_Popup", 11, param, value);
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
                                if (result == "Success")
                                {
                                    if (Popup_Image_FileName != "NA" && flagHasFile && flagValidFile)
                                        fupPopupImage.SaveAs(Server.MapPath(Popup_Image_FilePath));
                                    if (Popup_Video_FileName != "NA" && flagHasFile_video && flagValidFile_video)
                                        fupPopupVideo.SaveAs(Server.MapPath(Popup_Video_FilePath));
                                    if (Popup_Attachment_FileName != "NA" && flagAttachHasFile && flagAttachValidFile)
                                        fupAttachment.SaveAs(Server.MapPath(Popup_Attachment_FilePath));
                                    hdfRIDPopup.Value = "0";
                                    btnSave.Text = "Save";
                                    panelAddNew.Visible = false;
                                    panelView.Visible = true;
                                    txtTitleEnglish.Text = "";
                                    txtTitleHindi.Text = "";
                                    ddlPopupType.SelectedValue = "Image";
                                    divPopupImage.Visible = true;
                                    divPopupVideo.Visible = false;
                                    txtDetailsEnglish.Text = "";
                                    txtDetailsHindi.Text = "";
                                    txtLinkURL.Text = "";
                                    FillPopup();
                                    displayGridMessage("Popup Content Successfully Added", "info");
                                }
                            }
                        }
                    }

                }
            }
            else if (btnSave.Text == "Update")
            {
                if (txtTitleEnglish.Text == "")
                    displayMessage("Please enter Title", "error");
                else if (txtTitleHindi.Text == "")
                    displayMessage("Please enter Title", "error");
                else if (ddlPopupType.SelectedValue == "Image" && !fupPopupImage.HasFile)
                    displayMessage("Please browse image for popup", "error");
                else if (ddlPopupType.SelectedValue == "Video" && !fupPopupVideo.HasFile)
                    displayMessage("Please browse video for popup", "error");
                else if (ddlPopupType.SelectedValue == "Text" && txtDetailsEnglish.Text.Trim() == "")
                    displayMessage("Please enter details in english", "error");
                else if (ddlPopupType.SelectedValue == "Text" && txtDetailsHindi.Text.Trim() == "")
                    displayMessage("Please enter details in hindi", "error");
                else
                {
                    bool flagValidFile = true;
                    bool flagHasFile = true;
                    string Popup_Image_FileName = "NA";
                    string Popup_Image_FilePath = "NA";
                    //Check File is attached or not
                    if (fupPopupImage.HasFile)
                    {
                        string ext = Path.GetExtension(fupPopupImage.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                        Popup_Image_FileName = fupPopupImage.FileName;
                        Popup_Image_FilePath = "../Uploads/Popup/PopupImage/" + Popup_Image_FileName;
                        hdfPopupImagePath.Value = Popup_Image_FilePath;
                        if (!(ext.ToUpper() == ".JPG" || ext.ToUpper() == ".PNG" || ext.ToUpper() == ".JPEG" || ext.ToUpper() == ".GIF"))
                        {
                            flagValidFile = false;
                            displayMessage("Please attach valid file", "error");
                        }
                        else
                            flagValidFile = true;
                    }

                    bool flagValidFile_video = true;
                    bool flagHasFile_video = true;
                    string Popup_Video_FileName = "NA";
                    string Popup_Video_FilePath = "NA";

                    //Check Video is attached or not
                    if (fupPopupVideo.HasFile)
                    {
                        string ext = Path.GetExtension(fupPopupVideo.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                        Popup_Video_FileName = fupPopupVideo.FileName;
                        Popup_Video_FilePath = "../Uploads/Popup/" + Popup_Video_FileName;
                        hdfPopupVideoPath.Value = Popup_Video_FilePath;
                        if (!(ext.ToUpper() == ".MP4" || ext.ToUpper() == ".MOV" || ext.ToUpper() == ".AVI"))
                        {
                            flagValidFile_video = false;
                            displayMessage("Please attach valid video file", "error");
                        }
                        else
                            flagValidFile_video = true;
                    }

                    bool flagAttachValidFile = true;
                    bool flagAttachHasFile = true;
                    string Popup_Attachment_FileName = "NA";
                    string Popup_Attachment_FilePath = "NA";
                    //Check File is attached or not
                    if (fupAttachment.HasFile)
                    {
                        string ext = Path.GetExtension(fupAttachment.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                        Popup_Attachment_FileName = fupAttachment.FileName;
                        Popup_Attachment_FilePath = "../Uploads/Popup/Attachment/" + Popup_Attachment_FileName;
                        hdfAttachmentPath.Value = Popup_Attachment_FilePath;
                        if (!(ext.ToUpper() == ".DOC" || ext.ToUpper() == ".DOCX" || ext.ToUpper() == ".PDF" || ext.ToUpper() == ".XLS" || ext.ToUpper() == ".XLSX"))
                        {
                            flagAttachValidFile = false;
                            displayMessage("Please attach valid file", "error");
                        }
                        else
                            flagAttachValidFile = true;
                    }

                    string LinkAndAttachment = "";
                    if (ddlLinkType.SelectedValue == "Attachment")
                    {
                        LinkAndAttachment = hdfAttachmentPath.Value;
                    }
                    else if (ddlLinkType.SelectedValue == "Link")
                    {
                        LinkAndAttachment = txtLinkURL.Text.Trim();
                    }
                    string[] param = { "@Flag", "@RID", "@DeptID", "@TitleHindi", "@TitleEnglish", "@PopUpContentHindi", "@PopUpContentEnglish","@PopupType", "@PopupImage","@PopupVideo", "@LinkType", "@LinkAndAttachment" };
                    string[] value = { "Update", hdfRIDPopup.Value, hdfDept_Id.Value, txtTitleHindi.Text.Trim(), txtTitleEnglish.Text.Trim(), txtDetailsHindi.Text.Trim(), txtDetailsEnglish.Text.Trim(), ddlPopupType.SelectedValue, hdfPopupImagePath.Value, hdfPopupVideoPath.Value, ddlLinkType.SelectedValue, LinkAndAttachment };
                    DB_Status dbs = dba.sp_populateDataSet("SP_Popup", 12, param, value);
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
                                if (result == "Success")
                                {
                                    if (Popup_Image_FileName != "NA" && flagHasFile && flagValidFile)
                                        fupPopupImage.SaveAs(Server.MapPath(Popup_Image_FilePath));
                                    if (Popup_Video_FileName != "NA" && flagHasFile_video && flagValidFile_video)
                                        fupPopupVideo.SaveAs(Server.MapPath(Popup_Video_FilePath));
                                    if (Popup_Attachment_FileName != "NA" && flagAttachHasFile && flagAttachValidFile)
                                        fupAttachment.SaveAs(Server.MapPath(Popup_Attachment_FilePath));
                                    hdfRIDPopup.Value = "0";
                                    btnSave.Text = "Save";
                                    panelAddNew.Visible = false;
                                    panelView.Visible = true;
                                    txtTitleEnglish.Text = "";
                                    txtTitleHindi.Text = "";
                                    ddlPopupType.SelectedValue = "Image";
                                    divPopupImage.Visible = true;
                                    divPopupVideo.Visible = false;
                                    txtDetailsEnglish.Text = "";
                                    txtDetailsHindi.Text = "";
                                    txtLinkURL.Text = "";
                                    FillPopup();
                                    displayGridMessage("Popup Content Successfully Updated", "info");
                                }
                            }
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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hdfRIDPopup.Value = "";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }

    protected void FillPopup()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "View", hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Popup", 2, parameter, value);
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
            string QuestionID = (sender as LinkButton).CommandArgument;
            hdfRIDPopup.Value = QuestionID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@RID" };
            string[] value = { "PopupById", hdfRIDPopup.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Popup", 2, parameter, value);
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
                        txtTitleEnglish.Text = Convert.ToString(dt.Rows[0]["TitleEnglish"]);
                        txtTitleHindi.Text = Convert.ToString(dt.Rows[0]["TitleHindi"]);
                        ddlLinkType.SelectedValue = Convert.ToString(dt.Rows[0]["LinkType"]);

                        string PopupType = Convert.ToString(dt.Rows[0]["PopupType"]);
                        if(PopupType=="Image")
                        {
                            divPopupImage.Visible = true;
                            divPopupVideo.Visible = false;
                        }
                        else if (PopupType == "Video")
                        {
                            divPopupImage.Visible = false;
                            divPopupVideo.Visible = true;
                        }
                        else if (PopupType == "Text")
                        {
                            divPopupImage.Visible = false;
                            divPopupVideo.Visible = false;
                        }

                        string Linktype = Convert.ToString(dt.Rows[0]["LinkType"]);
                        if (Linktype == "Attachment")
                        {
                            divAttachment.Visible = true;
                            divURL.Visible = false;
                            hdfAttachmentPath.Value = Convert.ToString(dt.Rows[0]["LinkAndAttachment"]);
                            txtLinkURL.Text = "NA";
                        }
                        else if (Linktype == "Link")
                        {
                            divAttachment.Visible = false;
                            divURL.Visible = true;
                            hdfAttachmentPath.Value = "NA";
                            txtLinkURL.Text = Convert.ToString(dt.Rows[0]["LinkAndAttachment"]);
                        }
                        txtDetailsEnglish.Text = Convert.ToString(dt.Rows[0]["PopUpContentEnglish"]);
                        txtDetailsHindi.Text = Convert.ToString(dt.Rows[0]["PopUpContentHindi"]);

                        string PopupImagePath = Convert.ToString(dt.Rows[0]["PopupImage"]);
                        if (!string.IsNullOrEmpty(PopupImagePath))
                            hdfPopupImagePath.Value = PopupImagePath;

                        string VideoImagePath = Convert.ToString(dt.Rows[0]["PopupVideo"]);
                        if (!string.IsNullOrEmpty(VideoImagePath))
                            hdfPopupVideoPath.Value = VideoImagePath;
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
            hdfRIDPopup.Value = RID;

            string[] parameter = { "@Flag", "@RID" };
            string[] value = { "Delete", hdfRIDPopup.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Popup", 2, parameter, value);
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
                hdfRIDPopup.Value = "0";
                FillPopup();
            }
        }
        catch (Exception)
        {
        }
    }
    protected void grdview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdview.PageIndex = e.NewPageIndex;
        FillPopup();
    }
    protected void ddlLinkType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLinkType.SelectedValue == "Attachment")
        {
            divAttachment.Visible = true;
            RequiredFieldValidatorfupAttachment.Enabled = true;
            RequiredFieldValidatortxtLinkURL.Enabled = false;
            divURL.Visible = false;
        }
        else if (ddlLinkType.SelectedValue == "Link")
        {
            divAttachment.Visible = false;
            divURL.Visible = true;
            RequiredFieldValidatorfupAttachment.Enabled = false;
            RequiredFieldValidatortxtLinkURL.Enabled = true;
        }
        else
        {
            RequiredFieldValidatorfupAttachment.Enabled = false;
            RequiredFieldValidatortxtLinkURL.Enabled = false;
            divAttachment.Visible = false;
            divURL.Visible = false;
        }
    }
    protected void cbSwitch_CheckedChanged(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        CheckBox ckkIsactive = (CheckBox)sender;
        int SID = Convert.ToInt16(((HiddenField)ckkIsactive.Parent.FindControl("hdfRIDPopupGrd")).Value);
        string Flag = "";

        if (ckkIsactive.Checked)
        {
            Flag = "Activate";
        }
        else
        {
            Flag = "Deactivate";
        }

        string[] parameter = { "@Flag", "@RID" };
        string[] value = { Flag, SID.ToString() };
        DB_Status dbs = dba.sp_populateDataSet("SP_Popup", 2, parameter, value);
        if (dbs.OperationStatus.ToString() == "Success")
        {
            DataSet ds = dbs.ResultDataSet;
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string status = dt.Rows[0]["Result"].ToString();
                    if (status == "Activated")
                        displayGridMessage("Popup successfully activated", "info");
                    else if (status == "Deactivated")
                        displayGridMessage("Popup successfully deactivated", "info");
                }
            }
        }
    }

    protected void ddlPopupType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlPopupType.SelectedValue=="Image")
        {
            divPopupImage.Visible = true;
            divPopupVideo.Visible = false;
        }
        else if (ddlPopupType.SelectedValue == "Video")
        {
            divPopupImage.Visible = false;
            divPopupVideo.Visible = true;
        }
        else
        {
            divPopupImage.Visible = false;
            divPopupVideo.Visible = false;
        }
    }
}