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
            hdfDept_Id.Value = "0";
            div_headTitle.InnerText = "Admin > Banner";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " +Convert.ToString(Session["deprt_name"])+" > Banner";
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

            FillBanners();
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
        hdfRID_Banner.Value = "";

        ddlSequence.ClearSelection();
        txtBannerText.Text = "";
        txtBnrTextH.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if(txtBannerText.Text.Trim()=="")
                    displayMessage("Please enter Banner Text", "error");          
                else if (ddlSequence.SelectedValue=="")
                    displayMessage("Please select Sequence", "error");                    
                else if (!fileUploadBanner.HasFile)
                    displayMessage("Please browse Banner Image", "error");
                else
                {
                    bool flagValidImage = true;
                    bool flagHasImage = false;
                    string ImageFileName = "";
                    string ImagePath = "NA";                

                    flagHasImage = true;
                    string ext = Path.GetExtension(fileUploadBanner.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    ImageFileName = "Banner_" + datevalue + ext;
                    ImagePath = "Uploads/Banner/" + ImageFileName;
                    hdfImage_UploadedPath.Value = ImagePath;
                    if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                    {
                        flagValidImage = true;
                    }
                    else
                    {
                        flagValidImage = false;
                    }

                    if (flagHasImage && flagValidImage == false)
                    {
                        displayMessage("Please attach only image file ", "error");
                    }
                    else
                    {
                        string[] parameter = {"@Flag", "@BannerPath", "@DeptID", "@BannerText", "@BannerTextH", "@SequenceID" };
                        string[] value = { "Add", hdfImage_UploadedPath.Value, hdfDept_Id.Value, txtBannerText.Text.Trim(),txtBnrTextH.Text.Trim(), ddlSequence.SelectedValue };
                        DB_Status dbs = dba.sp_populateDataSet("SP_Banner", 6, parameter, value);
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
                                fileUploadBanner.SaveAs(Server.MapPath("~/" + hdfImage_UploadedPath.Value));

                            displayMessage("Banner successfully added", "info");
                            FillBanners();
                            hdfRID_Banner.Value = "0";
                            hdfImage_UploadedPath.Value = "No";
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;
                        }
                        else if(result == "AlreadyExists")
                        {
                            displayMessage("Record already exists", "error");
                        }
                    }

                }
            }
            else if (btnSave.Text == "Update")
            {
                if (txtBannerText.Text.Trim() == "")
                    displayMessage("Please enter Banner Text", "error");
                else if (ddlSequence.SelectedValue == "")
                    displayMessage("Please select Sequence", "error");
                else
                {
                    bool flagValidImage = true;
                    bool flagHasImage = false;
                    string ImageFileName = "";
                    string ImagePath = "NA";
               
                    if (fileUploadBanner.HasFile)
                    {
                        flagHasImage = true;
                        string ext = Path.GetExtension(fileUploadBanner.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        ImageFileName = "Banner_" + datevalue + ext;

                        ImagePath = "Uploads/Banner/" + ImageFileName;
                        hdfImage_UploadedPath.Value = ImagePath;

                        if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                            flagValidImage = true;
                        else
                            flagValidImage = false;
                        
                    }
                    if (flagHasImage && flagValidImage == false)
                    {
                        displayMessage("Please attach only image file", "error");
                    }
                    else
                    {
                        string[] parameter = { "@Flag", "@SID", "@BannerPath", "@BannerText","@BannerTextH", "@SequenceID" };
                        string[] value = { "Update",hdfRID_Banner.Value,ImagePath,txtBannerText.Text.Trim(),txtBnrTextH.Text.Trim(), ddlSequence.SelectedValue };
                        DB_Status dbs = dba.sp_populateDataSet("SP_Banner", 6, parameter, value);
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
                                if (hdfImage_UploadedPath.Value.Length > 10)
                                    File.Delete(Request.PhysicalApplicationPath + hdfImage_UploadedPath.Value);

                                fileUploadBanner.SaveAs(Server.MapPath("~/" + ImagePath));                            }
        
                            displayMessage("Banner successfully updated", "info");
                            FillBanners();
                            hdfRID_Banner.Value = "0";
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
        hdfRID_Banner.Value = "0";
        hdfImage_UploadedPath.Value = "No";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void FillBanners()
    {
        try
        {
            string[] parameter = {"@Flag", "@DeptID" };
            string[] value = {"LoadForAdmin",hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Banner", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    gridBanner.DataSource = dt;
                    gridBanner.DataBind();
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
            hdfRID_Banner.Value = QuestionID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = {"@Flag", "@SID" };
            string[] value = {"LoadbyID", hdfRID_Banner.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Banner", 2, parameter, value);
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
                        hdfImage_UploadedPath.Value = dt.Rows[0]["BannerPath"].ToString();
                        txtBannerText.Text = dt.Rows[0]["BannerText"].ToString();
                        txtBnrTextH.Text = dt.Rows[0]["BannerTextH"].ToString();
                        ddlSequence.SelectedValue = dt.Rows[0]["SequenceID"].ToString();
                        if (!string.IsNullOrEmpty(hdfImage_UploadedPath.Value) && hdfImage_UploadedPath.Value !="No")
                            RequiredFieldValidatorFileUpload.Enabled = false;
                        else
                            RequiredFieldValidatorFileUpload.Enabled = true;
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
            hdfRID_Banner.Value = QuestionID;

            string[] parameter = {"@Flag", "@SID" };
            string[] value = {"Delete", hdfRID_Banner.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Banner", 2, parameter, value);
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
                hdfRID_Banner.Value = "0";
                FillBanners();
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
        int SID = Convert.ToInt16(((Label)ckkIsactive.Parent.FindControl("lbQuizID")).Text);
        string Flag = "";

        if (ckkIsactive.Checked)
        {
            Flag = "Activate";
        }
        else
        {
            Flag = "Deactivate";
        }

        string[] parameter = { "@Flag", "@SID" };
        string[] value = { Flag, SID.ToString() };
        DB_Status dbs = dba.sp_populateDataSet("SP_Banner", 2, parameter, value);
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
                        displayGridMessage("Banner successfully activated", "info");
                    else if (status == "Deactivated")
                        displayGridMessage("Banner successfully deactivated", "info");
                }
            }
        }
    }    
}