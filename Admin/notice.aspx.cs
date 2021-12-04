using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_notice : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] != null)
        {
            hdfDept_Id.Value = "0";
            div_headTitle.InnerText = "Admin > Notice";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Convert.ToString(Session["deprt_name"]) + " > Notice";
            hdfDept_Id.Value = Session["DeptID"].ToString();
        }
        else
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {
            //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_banner");
            //menuli.Attributes["class"] = "active";

            panelAddNew.Visible = false;
            panelView.Visible = true;

            FillNotice();
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
        hdfNoticeId.Value = "";
        hdfImage_UploadedPath.Value = "No";
        txtNotice.Text = "";
        txtNoticeHindi.Text = "";
        txtNoticeDate.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtNoticeDate.Text.Trim() == "")
                    displayMessage("Please select notice date", "error");
                else if (txtNotice.Text.Trim()=="")
                    displayMessage("Please enter notice", "error");
                else if (txtNoticeHindi.Text.Trim() == "")
                    displayMessage("Please enter notice", "error");
                else
                {
                    bool flagValidImage = true;
                    bool flagHasImage = false;
                    string ImageFileName = "";
                    string ImagePath = "NA";
                    if (fupNotice.HasFile)
                    {
                        flagHasImage = true;
                        string ext = Path.GetExtension(fupNotice.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        ImageFileName = "Notice_" + datevalue + ext;
                        ImagePath = "Uploads/Notice/" + ImageFileName;
                        hdfImage_UploadedPath.Value = ImagePath;
                        if (ext == ".docx" || ext == ".DOCX" || ext == ".pdf" || ext == ".PDF")
                        {
                            flagValidImage = true;
                        }
                        else
                        {
                            flagValidImage = false;
                        }
                        if (flagHasImage && flagValidImage == false)
                        {
                            displayMessage("Please attach only doc and pdf file ", "error");
                            return;
                        }
                    }
                        string[] parameter = { "@Flag", "@DeptID", "@NoticeDate", "@NoticeEnglish", "@NoticeHindi", "@FilePath" };
                        string[] value = { "Add", hdfDept_Id.Value,obj.makedate(txtNoticeDate.Text.Trim()), txtNotice.Text.Trim(),txtNoticeHindi.Text.Trim(), hdfImage_UploadedPath.Value};
                        DB_Status dbs = dba.sp_populateDataSet("SP_Notice", 6, parameter, value);
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
                            if (flagHasImage && flagValidImage)
                                fupNotice.SaveAs(Server.MapPath("~/" + hdfImage_UploadedPath.Value));

                            displayMessage("Record successfully added", "info");
                            FillNotice();
                            hdfNoticeId.Value = "0";
                            hdfImage_UploadedPath.Value = "No";
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;
                        }
                        else if (result == "AlreadyExists")
                        {
                            displayMessage("Record already exists", "error");
                        }
                    }
                }
            else if (btnSave.Text == "Update")
            {
                if (txtNoticeDate.Text.Trim() == "")
                    displayMessage("Please select notice date", "error");
                else if (txtNotice.Text.Trim() == "")
                    displayMessage("Please enter notice", "error");
                else
                {
                    bool flagValidImage = true;
                    bool flagHasImage = false;
                    string ImageFileName = "";
                    string ImagePath = "NA";

                    if (fupNotice.HasFile) 
                    {
                        flagHasImage = true;
                        string ext = Path.GetExtension(fupNotice.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        ImageFileName = "Notice_" + datevalue + ext;

                        ImagePath = "Uploads/Notice/" + ImageFileName;
                        hdfUpdateImagePath.Value = ImagePath;

                        if (ext == ".docx" || ext == ".DOCX" || ext == ".pdf" || ext == ".PDF")
                            flagValidImage = true;
                        else
                            flagValidImage = false;

                    }
                    if (flagHasImage && flagValidImage == false)
                    {
                        displayMessage("Please attach only doc and pdf file ", "error");
                        return;
                    }
                    else
                    {
                        string[] parameter = { "@Flag", "@NoticeId", "@NoticeDate", "@NoticeEnglish", "@NoticeHindi", "@FilePath"};
                        string[] value = { "Update", hdfNoticeId.Value,obj.makedate(txtNoticeDate.Text.Trim()), txtNotice.Text.Trim(),txtNoticeHindi.Text.Trim(), hdfUpdateImagePath.Value };
                        DB_Status dbs = dba.sp_populateDataSet("SP_Notice",6, parameter, value);
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
                        if (result == "Updated")
                        {
                            if (flagHasImage && flagValidImage)
                            {
                                FileInfo file = new FileInfo(Server.MapPath("~/"+hdfImage_UploadedPath.Value));
                                if (file.Exists)
                                    file.Delete();

                                fupNotice.SaveAs(Server.MapPath("~/" + ImagePath));
                            }
                            displayMessage("Record successfully updated", "info");
                            FillNotice();
                            hdfNoticeId.Value = "0";
                            hdfImage_UploadedPath.Value = "No";
                            hdfUpdateImagePath.Value = "No";
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;
                        }
                        else if (result == "AlreadyExists")
                        {
                            displayMessage("Record already exists", "error");
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
        hdfNoticeId.Value = "0";
        hdfImage_UploadedPath.Value = "No";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void FillNotice()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "LoadForAdmin",hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Notice",2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    grdNotice.DataSource = dt;
                    grdNotice.DataBind();
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
            string noticeId = (sender as LinkButton).CommandArgument;
            hdfNoticeId.Value = noticeId;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@NoticeId" };
            string[] value = { "LoadbyID", hdfNoticeId.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Notice", 2, parameter, value);
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
                        hdfImage_UploadedPath.Value =Convert.ToString(dt.Rows[0]["FilePath"]);
                        hdfUpdateImagePath.Value = Convert.ToString(dt.Rows[0]["FilePath"]);
                        txtNoticeDate.Text = Convert.ToString(dt.Rows[0]["NoticeDate"]);
                        txtNotice.Text = Convert.ToString(dt.Rows[0]["NoticeEnglish"]);
                        txtNoticeHindi.Text= Convert.ToString(dt.Rows[0]["NoticeHindi"]);
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
            string noticeId = (sender as LinkButton).CommandArgument;
            hdfNoticeId.Value = noticeId;

            string[] parameter = { "@Flag", "@NoticeId" };
            string[] value = { "Delete", hdfNoticeId.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Notice", 2, parameter, value);
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
                hdfNoticeId.Value = "0";
                FillNotice();
            }
        }
        catch (Exception)
        {
        }
    }
    protected void cbSwitch_CheckedChanged(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        CheckBox ckkIsactive = (CheckBox)sender;
        string noticeId =((HiddenField)ckkIsactive.Parent.FindControl("hdfNoticeIdGrd")).Value;
        string Flag = "";

        if (ckkIsactive.Checked)
        {
            Flag = "Activate";
        }
        else
        {
            Flag = "Deactivate";
        }

        string[] parameter = { "@Flag", "@NoticeId" };
        string[] value = { Flag, noticeId };
        DB_Status dbs = dba.sp_populateDataSet("SP_Notice", 2, parameter, value);
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
                        displayGridMessage("Record successfully activated", "info");
                    else if (status == "Deactivated")
                        displayGridMessage("Record successfully deactivated", "info");
                }
            }
        }
    }
}