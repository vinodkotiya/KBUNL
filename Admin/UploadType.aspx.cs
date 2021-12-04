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
        if (Session["AdminUserID"] == null)
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {
            System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_uploadType");
            menuli.Attributes["class"] = "active";

            panelAddNew.Visible = false;
            panelView.Visible = true;
            //Fill Articles

            Fill_UploadType();
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
        hdfUploadTypeId.Value = "";

        txtTitle.Text = "";
        txtContent.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (ddlUploadType.SelectedIndex ==0)
                    displayMessage("Please select upload type", "error");
                else if (txtTitle.Text.Trim() == "")
                    displayMessage("Please enter Title", "error");
                else
                {

                    bool flagValidImage = true;
                    bool flagHasImage = false;
                    string ImageFileName = "";
                    string ImagePath = "NA";
                    if (FileUploadImage.HasFile)
                    {
                        flagHasImage = true;
                        string ext = Path.GetExtension(FileUploadImage.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        ImageFileName = Convert.ToString(ddlUploadType.SelectedValue) + datevalue + ext;

                        ImagePath = "Uploads/UploadType/" + ImageFileName;

                        if (ext == ".pdf" || ext == ".doc" || ext == ".docx" || ext == ".xlsx" || ext == ".xls" || ext == ".pptx" || ext == ".ppt" || ext == ".ppsx")
                            flagValidImage = true;
                        else
                            flagValidImage = false;

                    }
                    if (flagHasImage && flagValidImage == false)
                    {
                        displayMessage("Please attach only pdf, word document, excel sheet or presentation for Circular File", "error");
                    }
                    else
                    {
                        string[] parameter = { "@Department", "@UploadType", "@Title", "@UploadTypeContent", "@AttachmentURL" };
                        string[] value = {Convert.ToString(Session["AdminUserID"]),ddlUploadType.SelectedValue, txtTitle.Text.Trim(), txtContent.Text.Trim(), ImagePath };
                        DB_Status dbs = dba.sp_populateDataSet("Sp_UploadType_Insert", 5, parameter, value);
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

                        if (result == "success")
                        {
                            if (flagHasImage && flagValidImage)
                                FileUploadImage.SaveAs(Server.MapPath("~/" + ImagePath));

                            displayMessage("Record successfully added", "info");
                            Fill_UploadType();
                            hdfUploadTypeId.Value = "";
                            txtTitle.Text = "";
                            txtContent.Text = "";
                            ddlUploadType.SelectedIndex = 0;
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
                else
                {
                    bool flagValidImage = true;
                    bool flagHasImage = false;
                    string ImageFileName = "";
                    string ImagePath = "NA";

                    if (FileUploadImage.HasFile)
                    {


                        flagHasImage = true;
                        string ext = Path.GetExtension(FileUploadImage.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        ImageFileName = Convert.ToString(ddlUploadType.SelectedValue) + datevalue + ext;

                        ImagePath = "Uploads/UploadType/" + ImageFileName;

                        if (ext == ".pdf" || ext == ".doc" || ext == ".docx" || ext == ".xlsx" || ext == ".xls" || ext == ".pptx" || ext == ".ppt" || ext == ".ppsx")
                            flagValidImage = true;
                        else
                            flagValidImage = false;
                    }
                    if (flagHasImage && flagValidImage == false)
                    {
                        displayMessage("Please attach only pdf, word document, excel sheet or presentation for Circular File", "error");
                    }
                    else
                    {
                        string[] parameter = { "@RID", "@Department", "@UploadType", "@Title", "@UploadTypeContent", "@AttachmentURL" };
                        string[] value = { hdfUploadTypeId.Value, Convert.ToString(Session["AdminUserID"]), ddlUploadType.SelectedValue, txtTitle.Text.Trim(), txtContent.Text.Trim(), ImagePath };
                        DB_Status dbs = dba.sp_populateDataSet("Sp_UploadType_Update", 6, parameter, value);
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
                        if (result == "success")
                        {
                            if (flagHasImage && flagValidImage)
                            {
                                if (hfImage_UploadedPath.Value.Length > 10)
                                    File.Delete(Request.PhysicalApplicationPath + hfImage_UploadedPath.Value);

                                FileUploadImage.SaveAs(Server.MapPath("~/" + ImagePath));
                            }

                            displayMessage("Record successfully updated", "info");
                            Fill_UploadType();
                            hdfUploadTypeId.Value = "";
                            txtTitle.Text = "";
                            txtContent.Text = "";
                            ddlUploadType.SelectedIndex = 0;
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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hdfUploadTypeId.Value = "";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void Fill_UploadType()
    {
        try
        {
            string[] parameter = { };
            string[] value = { };
            DB_Status dbs = dba.sp_populateDataSet("Sp_UploadType_View", 0, parameter, value);
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
            string QuestionID = (sender as LinkButton).CommandArgument;
            hdfUploadTypeId.Value = QuestionID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@RID" };
            string[] value = { hdfUploadTypeId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Upload_Type_ViewBy_ID", 1, parameter, value);
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
                        ddlUploadType.SelectedValue = Convert.ToString(dt.Rows[0]["UploadType"]);
                        txtTitle.Text = Convert.ToString(dt.Rows[0]["Title"]);
                        txtContent.Text = Convert.ToString(dt.Rows[0]["UploadTypeContent"]);
                        hfImage_UploadedPath.Value = Convert.ToString(dt.Rows[0]["AttachmentURL"]);
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
            hdfUploadTypeId.Value = RID;

            string[] parameter = { "@RID" };
            string[] value = { hdfUploadTypeId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_UploadType_Delete", 1, parameter, value);
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
            if (result == "success")
            {
                hdfUploadTypeId.Value = "";
                Fill_UploadType();
            }
        }
        catch (Exception)
        {
        }
    }

    protected void grdNotice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdNotice.PageIndex = e.NewPageIndex;
        Fill_UploadType();
    }


}