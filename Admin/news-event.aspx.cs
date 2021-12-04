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
            div_headTitle.InnerText = "Admin > Events";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Convert.ToString(Session["deprt_name"]) + " > Events";
            hdfDept_Id.Value = Session["DeptID"].ToString();
        }
        else
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {
            panelAddNew.Visible = false;
            panelView.Visible = true;

            FillNewsOrEvents();
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
        hfRID.Value = "";
        txtTitleEnglish.Text = "";
        txtTitleHindi.Text = "";
        txtDescriptionEnglish.Text = "";
        txtDescriptionHindi.Text = "";
        txtNewsOrEvent.Text = "";
        txtLinkURL.Text = "";
        btnSave.Text = "Save";
        span_FileStatus.Style.Add("display", "none");
        span_FileStatus.InnerText = "";
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hfRID.Value = "";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
        hfAttachment_UploadedPath.Value = "NA";
        hdfUpdateImagePath.Value = "NA";
        span_FileStatus.Style.Add("display", "none");
        span_FileStatus.InnerText = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTitleEnglish.Text == "")
                displayMessage("Please enter title", "error");
            if (txtTitleHindi.Text == "")
                displayMessage("Please enter title", "error");
            //else if (txtDescriptionEnglish.Text == "")
            //    displayMessage("Please enter description", "error");
            //else if (txtDescriptionHindi.Text == "")
            //    displayMessage("Please enter description", "error");
            else if (FileUploader1.HasFile == true && txtLinkURL.Text.Trim() != "")
                displayMessage("Please choose one", "error");
            else
            {
                bool flagValidFile = true;
                bool flagHasFile = true;
                string Attachment_FileName = hfAttachment_UploadedPath.Value; // default value = NA
                string Attachment_FilePath = "NA";
                string LinkURL = "";
                if (txtLinkURL.Text.Trim() != "")
                    LinkURL = txtLinkURL.Text.Trim();
                if (txtNewsOrEvent.Text != "")
                    txtNewsOrEvent.Text = obj.makedate(txtNewsOrEvent.Text);


                //Check File is attached or not
                if (FileUploader1.HasFile)
                {
                    string ext = Path.GetExtension(FileUploader1.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    Attachment_FileName = "NewsOrEvent_" + datevalue + ext;
                    Attachment_FilePath = "Uploads/NewsOrEvent/Files/" + Attachment_FileName;
                    hdfUpdateImagePath.Value = Attachment_FilePath;
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
                    string[] param = { "@Flag", "@RID", "@DeptID", "@TitleEnglish", "@TitleHindi", "@DescriptionEnglish", "@DescriptionHindi", "@NewsOrEvent", "@Attachment", "@Link", "@Type" };
                    string[] value = { Flag, hfRID.Value, hdfDept_Id.Value, txtTitleEnglish.Text.Trim(),txtTitleHindi.Text.Trim(), txtDescriptionEnglish.Text.Trim(),txtDescriptionHindi.Text.Trim(), txtNewsOrEvent.Text, hdfUpdateImagePath.Value, LinkURL, ddlType.SelectedValue };
                    DB_Status dbs = dba.sp_populateDataSet("SP_Dept_Updates", 11, param, value);
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
                            FileUploader1.SaveAs(Server.MapPath("~/" + Attachment_FilePath));
                        hfRID.Value = "";
                        btnSave.Text = "Save";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
                        FillNewsOrEvents();
                        txtTitleEnglish.Text = "";
                        txtTitleHindi.Text = "";
                        txtDescriptionEnglish.Text = "";
                        txtDescriptionHindi.Text = "";
                        txtNewsOrEvent.Text = "";
                        hfAttachment_UploadedPath.Value = "NA";
                        txtLinkURL.Text = "";
                        displayMessage("Record Successfully Added", "info");
                    }
                    else if (result == "Updated")
                    {
                        if (Attachment_FileName != "NA" && flagHasFile && flagValidFile)
                        {
                            try
                            {
                                File.Delete(Request.PhysicalApplicationPath + hfAttachment_UploadedPath.Value);
                            }
                            catch (Exception)
                            {
                            }
                            FileUploader1.SaveAs(Server.MapPath("~/" + Attachment_FilePath));
                        }
                        hfRID.Value = "";
                        btnSave.Text = "Save";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
                        FillNewsOrEvents();
                        txtTitleEnglish.Text = "";
                        txtTitleHindi.Text = "";
                        txtDescriptionEnglish.Text = "";
                        txtDescriptionHindi.Text = "";
                        txtNewsOrEvent.Text = "";
                        txtLinkURL.Text = "";
                        hfAttachment_UploadedPath.Value = "NA";
                        displayMessage("Record Successfully Updated", "info");
                    }
                    else if (result == "AlreadyExists")
                    {
                        displayMessage("Sorry! Title Already Exists", "error");
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

    protected void FillNewsOrEvents()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "View", hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Dept_Updates", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    grdNewsOrEvents.DataSource = dt;
                    grdNewsOrEvents.DataBind();
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
            hfRID.Value = QuestionID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@RID", "@DeptID" };
            string[] value = { "ViewByID", hfRID.Value,hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Dept_Updates", 3, parameter, value);
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
                        txtDescriptionEnglish.Text = Convert.ToString(dt.Rows[0]["DescriptionEnglish"]);
                        txtDescriptionHindi.Text = Convert.ToString(dt.Rows[0]["DescriptionHindi"]);
                        txtNewsOrEvent.Text = Convert.ToString(dt.Rows[0]["NewsOrEvent"]);
                        if (dt.Rows[0]["Attachment"].ToString() != "NA")
                        {
                            hfAttachment_UploadedPath.Value = Convert.ToString(dt.Rows[0]["Attachment"]);
                            hdfUpdateImagePath.Value = Convert.ToString(dt.Rows[0]["Attachment"]);
                            span_FileStatus.Style.Add("display", "block");
                            span_FileStatus.InnerText = "File Selected";
                            span_FileStatus.Style.Add("color", "green");
                        }
                        else
                        {
                            span_FileStatus.Style.Add("display", "none");
                            span_FileStatus.InnerText = "";
                        }
                        txtLinkURL.Text = dt.Rows[0]["Link"].ToString();
                        ddlType.SelectedValue = dt.Rows[0]["Type"].ToString();

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

            string[] parameter = { "@Flag", "@RID", "@DeptID" };
            string[] value = { "Delete", hfRID.Value, hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Dept_Updates", 3, parameter, value);
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
                FillNewsOrEvents();
            }
        }
        catch (Exception)
        {
        }
    }

    protected void lnkAcvtiveStatus_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");

            string RID = (sender as LinkButton).CommandArgument;
            hfRID.Value = RID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            Label lbl_status = (Label)lnkbtn_edit.Parent.FindControl("lblActiveStatus");

            string Flag = lbl_status.Text;

            if (Flag == "No")
            {
                Flag = "Activate";
            }
            else
            {
                Flag = "Deactivate";
            }
            string result = "";
            string[] parameter = { "@Flag", "@RID", "@DeptID" };
            string[] value = { Flag, hfRID.Value, hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Dept_Updates", 3, parameter, value);
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
                if (result == "Success")
                {
                    hfRID.Value = "";
                    FillNewsOrEvents();
                }

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ViewList_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string ID = (sender as LinkButton).CommandArgument;
            hfRID.Value = ID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;
            Label grid_lblTitle = (Label)grdNewsOrEvents.Rows[rowindex].FindControl("lblTitle");
            Label grid_lblType = (Label)grdNewsOrEvents.Rows[rowindex].FindControl("lblType");

            Session["Selected_UpdtType_ID"] = hfRID.Value;
            Session["Selected_Title"] = grid_lblTitle.Text;
            Response.Redirect("news-event-images.aspx?type=" + grid_lblType.Text);
        }
        catch (Exception)
        {
        }
    }
    protected void grdEvents_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdNewsOrEvents.PageIndex = e.NewPageIndex;
        FillNewsOrEvents();
    }
}