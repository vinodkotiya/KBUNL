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
        if (Session["AdminUserID"] == null)
        {
            Response.Redirect("../login.aspx");
        }
       else
        {
            if (!IsPostBack)
            {
                System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_ggmdesk");
                menuli.Attributes["class"] = "active";

                panelAddNew.Visible = false;
                panelView.Visible = true;
                //Fill Articles
                FillRecord();
                displayGridMessage("", "");
            }

        }

        
    }
    /* Add/Update/Deactivate Banner*/
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
        txtTitle.Text = "";
        txtUrl.Text = "";
        hfFile_UploadedPath.Value = "";
        btnSave.Text = "Save";
        txtSequence.Text = "0";
        span_FileStatus.Style.Add("display", "none");
        span_FileStatus.InnerText = "";
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hfRID.Value = "";
        hfFile_UploadedPath.Value = "";
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
                if (txtTitle.Text.Trim() == "")
                    displayMessage("Please enter Title", "error");
                else if (File_Upload.HasFile == false && txtUrl.Text.Trim() == "")
                    displayMessage("Please Attach File Or Enter URL", "error");
                else if (File_Upload.HasFile == true && txtUrl.Text.Trim() != "")
                    displayMessage("Please Choose One Option", "error");
                else
                {
                    bool flagValid = true;

                    string fileName = "";
                    string filePath = "NA";
                    if (File_Upload.HasFile)
                    {
                        string ext = Path.GetExtension(File_Upload.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        fileName = "GGMDesk_" + datevalue + ext;

                        filePath = "Uploads/GGMDesk/" + fileName;

                        if (ext == ".pdf" || ext == ".doc" || ext == ".docx" || ext == ".xlsx" || ext == ".xls" || ext == ".pptx" || ext == ".ppt" || ext == ".ppsx")
                            flagValid = true;
                        else
                            flagValid = false;
                    }

                    if (flagValid == false)
                    {
                        displayMessage("Please attach only Valid file ", "error");
                    }
                    else
                    {
                        string[] parameter = { "@Flag", "@Title", "@Sequence", "@Link", "@Attachment" };
                        string[] value = { "insert", txtTitle.Text.Trim(),txtSequence.Text, txtUrl.Text.Trim(), filePath };
                        DB_Status dbs = dba.sp_populateDataSet("SP_GGM_Desk", 5, parameter, value);
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
                        if (result == "TitleExist")
                        {
                            displayMessage("Sorry! Title already exists", "error");
                        }
                        else if (result == "Success")
                        {
                            if (filePath != "NA")
                                File_Upload.SaveAs(Server.MapPath("~/" + filePath));


                            FillRecord();
                            hfRID.Value = "";
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;
                        }
                    }

                }
            }
            else if (btnSave.Text == "Update")
            {
                if (txtTitle.Text.Trim() == "")
                    displayMessage("Please enter Title", "error");
                if (txtTitle.Text.Trim() == "")
                    displayMessage("Please enter Title", "error");
                else if (File_Upload.HasFile == false && txtUrl.Text.Trim() == "" && hfFile_UploadedPath.Value == "NA")
                {displayMessage("Please Attach File Or Enter URL", "error");
                }
                    
                else if (File_Upload.HasFile == true && txtUrl.Text.Trim() != "")
                    displayMessage("Please Choose One", "error");
                else
                {
                    if (txtUrl.Text != "")
                    {
                        hfFile_UploadedPath.Value = "NA";
                    }
                    bool flagValid = true;
                    string fileName = "NA";
                    string filePath = hfFile_UploadedPath.Value;
                    if (File_Upload.HasFile)
                    {
                        string ext = Path.GetExtension(File_Upload.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        fileName = "GGMDesk_" + datevalue + ext;
                        filePath = "Uploads/GGMDesk/" + fileName;

                        if (ext == ".pdf" || ext == ".doc" || ext == ".docx" || ext == ".xlsx" || ext == ".xls" || ext == ".pptx" || ext == ".ppt" || ext == ".ppsx")
                            flagValid = true;
                        else
                            flagValid = false;

                    }
                    if (flagValid == false)
                    {
                        displayMessage("Please attach only valid File", "error");
                    }
                    else
                    {
                        string[] parameter = { "@Flag", "@RID", "@Title", "@Sequence", "@Link", "@Attachment" };
                        string[] value = { "Update", hfRID.Value, txtTitle.Text.Trim(),txtSequence.Text, txtUrl.Text.Trim(), filePath };
                        DB_Status dbs = dba.sp_populateDataSet("SP_GGM_Desk", 6, parameter, value);
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
                        if (result == "TitleExist")
                        {
                            displayMessage("Sorry! Title already exists", "error");
                        }
                        else if (result == "Success")
                        {
                            if (fileName != "NA")
                            {
                                if (hfFile_UploadedPath.Value.Length > 10)
                                    File.Delete(Request.PhysicalApplicationPath + hfFile_UploadedPath.Value);

                                File_Upload.SaveAs(Server.MapPath("~/" + filePath));
                            }
                            FillRecord();
                            hfRID.Value = "";
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
   
    protected void FillRecord()
    {
        try
        {
            string[] parameter = { "@Flag" };
            string[] value = { "View" };
            DB_Status dbs = dba.sp_populateDataSet("SP_GGM_Desk", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    gridRecord.DataSource = dt;
                    gridRecord.DataBind();
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
            hfRID.Value = ID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@RID" };
            string[] value = { "ViewByID", hfRID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_GGM_Desk", 2, parameter, value);
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
                        txtTitle.Text = dt.Rows[0]["Title"].ToString();
                        txtSequence.Text = dt.Rows[0]["Sequence"].ToString();
                        hfFile_UploadedPath.Value = dt.Rows[0]["Attachment"].ToString();
                        txtUrl.Text = dt.Rows[0]["Link"].ToString();
                        if(hfFile_UploadedPath.Value!="NA")
                        {
                            span_FileStatus.Style.Add("display","block");
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
            string QuestionID = (sender as LinkButton).CommandArgument;
            hfRID.Value = QuestionID;

            string[] parameter = { "@Flag", "@RID"};
            string[] value = { "Delete", hfRID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_GGM_Desk", 2, parameter, value);
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
                FillRecord();
            }
        }
        catch (Exception)
        {
        }
    }



    protected void lnkActive_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string ID = (sender as LinkButton).CommandArgument;
            hfRID.Value = ID;
            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;
            Label lblActive= (Label)grdrow.Cells[rowindex].FindControl("lblActive");
            string flag = "";
                if(lblActive.Text=="Yes")
            {
                flag = "Deactivate";
            }
            else
            {
                flag = "Activate";
            }
            string[] parameter = { "@Flag", "@RID" };
            string[] value = { flag, hfRID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_GGM_Desk", 2, parameter, value);

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
            if(result=="Success")
            {
                hfRID.Value = "";
                FillRecord();
            }
            
        }
        catch (Exception)
        {
        }
    }

    protected void gridRecord_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridRecord.PageIndex = e.NewPageIndex;
        FillRecord();
    }
}