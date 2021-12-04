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
        if (Session["AdminUserID"] != null)
        {
            Response.Redirect("dashboard.aspx");
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Session["deprt_name"].ToString()+ " > Link";
            hfUserID.Value = Session["DeptID"].ToString();
        }
        else
        {
            Response.Redirect("../login.aspx");
        }

        if (!IsPostBack)
        {
            System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_links");
            menuli.Attributes["class"] = "active";

            panelAddNew.Visible = false;
            panelView.Visible = true;
            //Fill Articles
            FillLinks();
            displayGridMessage("", "");
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
        hfLinkID.Value = "";
        txtTitle.Text = "";
        txtUrl.Text = "";
        hfFile_UploadedPath.Value = "";
        btnSave.Text = "Save";
      
        span_FileStatus.Style.Add("display", "none");
        span_FileStatus.InnerText = "";
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hfLinkID.Value = "";
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

                    string ImageFileName = "";
                    string ImagePath = "NA";
                    if (File_Upload.HasFile)
                    {
                        string ext = Path.GetExtension(File_Upload.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        ImageFileName = "URL_" + datevalue + ext;

                        ImagePath = "Uploads/URL/" + ImageFileName;

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
                        string[] parameter = { "@Flag", "@DptID", "@LinkTitle", "@LinkUrl", "@AttachmentUrl" };
                        string[] value = { "Add", hfUserID.Value, txtTitle.Text.Trim(), txtUrl.Text.Trim(), ImagePath };
                        DB_Status dbs = dba.sp_populateDataSet("SP_Deprt_Links", 5, parameter, value);
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
                        else if (result == "Success")
                        {
                            if (ImagePath != "NA")
                                File_Upload.SaveAs(Server.MapPath("~/" + ImagePath));


                            FillLinks();
                            hfLinkID.Value = "";
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
                    string ImageFileName = "NA";
                    string ImagePath = hfFile_UploadedPath.Value;
                    if (File_Upload.HasFile)
                    {
                        string ext = Path.GetExtension(File_Upload.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        ImageFileName = "URL_" + datevalue + ext;
                        ImagePath = "Uploads/URL/" + ImageFileName;

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
                        string[] parameter = { "@Flag", "@LinkID", "@DptID", "@LinkTitle", "@LinkUrl", "@AttachmentURL" };
                        string[] value = { "Update", hfLinkID.Value, hfUserID.Value, txtTitle.Text.Trim(), txtUrl.Text.Trim(), ImagePath };
                        DB_Status dbs = dba.sp_populateDataSet("SP_Deprt_Links", 6, parameter, value);
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
                        else if (result == "Success")
                        {
                            if (ImageFileName != "NA")
                            {
                                if (hfFile_UploadedPath.Value.Length > 10)
                                    File.Delete(Request.PhysicalApplicationPath + hfFile_UploadedPath.Value);

                                File_Upload.SaveAs(Server.MapPath("~/" + ImagePath));
                            }
                            FillLinks();
                            hfLinkID.Value = "";
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
   
    protected void FillLinks()
    {
        try
        {
            string[] parameter = { "@Flag", "@DptID" };
            string[] value = { "View", hfUserID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Deprt_Links", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    gridLink.DataSource = dt;
                    gridLink.DataBind();
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
            hfLinkID.Value = ID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@LinkID", "@DptID" };
            string[] value = { "ViewByID", hfLinkID.Value, hfUserID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Deprt_Links", 3, parameter, value);
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
                        hfFile_UploadedPath.Value = dt.Rows[0]["AttachmentUrl"].ToString();
                        txtUrl.Text = dt.Rows[0]["LinkURL"].ToString();
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
            hfLinkID.Value = QuestionID;

            string[] parameter = { "@Flag", "@LinkID", "@DptID" };
            string[] value = { "Delete", hfLinkID.Value, hfUserID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Deprt_Links", 3, parameter, value);
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
                hfLinkID.Value = "";
                FillLinks();
            }
        }
        catch (Exception)
        {
        }
    }


}