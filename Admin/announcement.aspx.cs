using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UploadType : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] != null)
        {
            hdfDept_Id.Value = "0";
            div_headTitle.InnerText = "Admin > Announcements";
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
            //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_announcements");
            //menuli.Attributes["class"] = "active";

            panelAddNew.Visible = false;
            panelView.Visible = true;

            FillAnnouncements();
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
        hdfRID.Value = "0";
        hdfAttachment_UploadedPath.Value = "No";
        ddlNew.SelectedIndex = 0;
        txtTitleE.Text = "";
        txtTitleH.Text = "";
        txtDescriptionE.Text = "";
        txtDescriptionH.Text = "";
        txtLinkURL.Text = "";
        txtValidDate.Text = "";
        txtSequenceNo.Text = "0";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTitleE.Text == "")
                displayMessage("Please enter Announcement Text", "error");
            else if (txtTitleH.Text == "")
                displayMessage("Please enter Announcement Text", "error");
            else if (txtSequenceNo.Text == "" || txtSequenceNo.Text=="0")
                displayMessage("Please enter Sequence", "error");
            else
            {
                bool flagValidFile = true;
                bool flagHasFile = true;
                string Attachment_FileName = "NA";
                string Attachment_FilePath = "NA";
                string LinkURL = "NA";
                if (txtLinkURL.Text.Trim() != "")
                    LinkURL = txtLinkURL.Text.Trim();


                //Check File is attached or not
                if (FileUploader1.HasFile)
                {
                    string ext = Path.GetExtension(FileUploader1.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    Attachment_FileName = "Announcement_" + datevalue + ext;
                    Attachment_FilePath = "Uploads/Announcements/" + Attachment_FileName;
                    hdfAttachment_UploadedPath.Value = Attachment_FilePath;
                    if (!(ext.ToUpper() == ".JPG" || ext.ToUpper() == ".PNG" || ext.ToUpper() == ".JPEG" || ext.ToUpper() == ".GIF" || ext.ToUpper() == ".DOC" || ext.ToUpper() == ".DOCX" || ext.ToUpper() == ".PDF" || ext.ToUpper() == ".XLS" || ext.ToUpper() == ".XLSX"))
                    {
                        flagValidFile = false;
                        displayMessage("Please attach valid file", "error");
                    }
                    else
                        flagValidFile = true;
                }

                //Check Save or Update
                string Flag = "";
                if (btnSave.Text == "Save")
                    Flag = "Add";
                else if (btnSave.Text == "Update")
                    Flag = "Update";


                if (flagHasFile && flagValidFile)
                {
                    //DeptID 0 for Admin
                    string[] param = { "@Flag", "@RID", "@DeptID", "@TitleE", "@TitleH", "@DescriptionE", "@DescriptionH", "@ValidDate", "@AttachmentURL", "@LinkURL", "@IsNew" ,"@Sequence"};
                    string[] value = { Flag, hdfRID.Value, hdfDept_Id.Value, txtTitleE.Text.Trim(), txtTitleH.Text.Trim(), txtDescriptionE.Text.Trim(), txtDescriptionH.Text.Trim(), obj.makedate(txtValidDate.Text), hdfAttachment_UploadedPath.Value, LinkURL, ddlNew.SelectedValue, txtSequenceNo.Text.Trim() };
                    DB_Status dbs = dba.sp_populateDataSet("SP_Announcements",12, param, value);
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
                    if (result == "Inserted")
                    {
                        if (Attachment_FileName != "NA" && flagHasFile && flagValidFile)
                            FileUploader1.SaveAs(Server.MapPath("~/" + hdfAttachment_UploadedPath.Value));
                        hdfRID.Value = "0";
                        btnSave.Text = "Save";
                        hdfAttachment_UploadedPath.Value = "No";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
                        FillAnnouncements();
                        ddlNew.SelectedIndex = 0;
                        txtTitleE.Text = "";
                        txtTitleH.Text = "";
                        txtDescriptionE.Text = "";
                        txtDescriptionH.Text = "";
                        txtLinkURL.Text = "";
                        txtValidDate.Text = "";
                        txtSequenceNo.Text = "";

                        displayMessage("Event Successfully Added", "info");
                    }
                    else if (result == "Updated")
                    {
                        if (Attachment_FileName != "NA" && flagHasFile && flagValidFile)
                        {
                            try
                            {
                                File.Delete(Request.PhysicalApplicationPath + Attachment_FilePath);
                            }
                            catch (Exception)
                            {
                            }
                            FileUploader1.SaveAs(Server.MapPath("~/" + hdfAttachment_UploadedPath.Value));
                        }
                        hdfRID.Value = "0";
                        hdfAttachment_UploadedPath.Value = "No";
                        btnSave.Text = "Save";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
                        FillAnnouncements();
                        txtTitleE.Text = "";
                        txtTitleH.Text = "";
                        txtDescriptionE.Text = "";
                        txtDescriptionH.Text = "";
                        txtLinkURL.Text = "";
                        txtValidDate.Text = "";
                        ddlNew.SelectedIndex = 0;
                        displayMessage("Event Successfully Updated", "info");
                    }
                    else if (result == "AlreadyExists")
                    {
                        displayMessage("Sorry! Event Already Exists", "error");
                    }
                    else if (result == "Fail")
                    {
                        displayMessage("Server Error", "error");
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
    protected void FillAnnouncements()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "Load",hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Announcements", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    grdAnnouncements.DataSource = dt;
                    grdAnnouncements.DataBind();
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
            string AnnouncementId = (sender as LinkButton).CommandArgument;
            hdfRID.Value = AnnouncementId;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = {"@Flag", "@RID" };
            string[] value = {"Load_byID", hdfRID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Announcements", 2, parameter, value);
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
                        txtTitleE.Text =Convert.ToString(dt.Rows[0]["TitleE"]);
                        txtTitleH.Text = Convert.ToString(dt.Rows[0]["TitleH"]);
                        txtDescriptionE.Text = Convert.ToString(dt.Rows[0]["DescriptionE"]);
                        txtDescriptionH.Text = Convert.ToString(dt.Rows[0]["DescriptionE"]);
                        txtLinkURL.Text = Convert.ToString(dt.Rows[0]["LinkURL"]);
                        txtValidDate.Text= Convert.ToString(dt.Rows[0]["ValidDate"]);
                        hdfAttachment_UploadedPath.Value = Convert.ToString(dt.Rows[0]["AttachmentURL"]);
                        ddlNew.SelectedValue= Convert.ToString(dt.Rows[0]["IsNew"]);
                        txtSequenceNo.Text = dt.Rows[0]["Sequence"].ToString();
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
            hdfRID.Value = RID;

            string[] parameter = {"@Flag", "@RID" };
            string[] value = {"Delete", hdfRID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Announcements", 2, parameter, value);
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
                hdfRID.Value = "0";
                FillAnnouncements();
            }
        }
        catch (Exception)
        {
        }
    }
    protected void grdAnnouncements_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAnnouncements.PageIndex = e.NewPageIndex;
        FillAnnouncements();
    }
}