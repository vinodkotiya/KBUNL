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
    string type = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] != null)
        {
            div_headTitle.InnerText = "Admin ><a href='news-event.aspx'><b>News & Event</b></a> : News & Event Images";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            check();
            div_headTitle.InnerHtml = "Department : " + Session["deprt_name"].ToString() + " > <a href='news-event.aspx'><b>News & Event</b></a> : " + Session["Selected_Title"].ToString();
        }

        if (!IsPostBack)
        {
            panelView.Visible = true;
            FillPhotos();
            displayGridMessage("", "");
            hfRID.Value = "";
            btnSave.Text = "Save";
        }
    }
    protected void check()
    {
        type = Request.QueryString["type"];
        if (Session["Selected_UpdtType_ID"] == null)
        {
            Response.Redirect("news-event.aspx");
        }
        else if(type != "News" && type != "Event")
        {
            Response.Redirect("news-event.aspx");
        }
        else
        {

            hfUpdtTypeID.Value = Session["Selected_UpdtType_ID"].ToString();
            spanTitle.InnerText = Session["Selected_Title"].ToString();
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
    
    protected void btnClose_Click(object sender, EventArgs e)
    {
        displayMessage("", "");
        displayGridMessage("", "");
        hfRID.Value = "";
        hfUpdtTypeID.Value = "";
        hfImage_UploadedPath.Value = "";
        divShowImage.Src = "";
        btnSave.Text = "Save";
        Response.Redirect("news-event.aspx", false);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (!FileUploadImage.HasFile)
                    displayMessage("Please upload image", "error");

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
                        ImageFileName = type + "_" + datevalue + ext;

                        ImagePath = "Uploads/NewsOrEvent/Images/" + ImageFileName;
                        hfImage_UploadedPath.Value = ImagePath;

                        if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                            flagValidImage = true;
                        else
                            flagValidImage = false;
                    }

                    //Validate Image
                    if (flagHasImage && flagValidImage == false)
                        displayMessage("Please attach only Image", "error");

                    else
                    {
                        string[] parameter = { "Flag", "@UpdateType_ID", "@ImagePath" };
                        string[] value = { "Add", hfUpdtTypeID.Value, ImagePath, };
                        DB_Status dbs = dba.sp_populateDataSet("SP_Dept_Update_Image", 3, parameter, value);
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
                            displayMessage("Sorry! Image already exists", "error");
                        }
                        else if (result == "Success")
                        {
                            if (flagHasImage && flagValidImage)
                                FileUploadImage.SaveAs(Server.MapPath("~/" + ImagePath));

                            displayMessage("Image successfully added", "info");
                            FillPhotos();
                            hfRID.Value = "";
                            btnSave.Text = "Save";
                        }
                    }
                }
            }
            else if (btnSave.Text == "Update")
            {
                bool flagValidImage = true;
                bool flagHasImage = false;
                string ImageFileName = "";
                string ImagePath = hfImage_UploadedPath.Value;
                if (FileUploadImage.HasFile)
                {
                    flagHasImage = true;
                    string ext = Path.GetExtension(FileUploadImage.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    ImageFileName = type + "_" + datevalue + ext;
                    ImagePath = "Uploads/NewsOrEvent/Images/" + ImageFileName;

                    if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                        flagValidImage = true;
                    else
                        flagValidImage = false;
                }


                if (flagValidImage == true)
                {

                    string[] parameter = {"@Flag","@RID", "@UpdateType_ID", "@ImagePath" };
                    string[] value = { "Update", hfRID.Value, hfUpdtTypeID.Value, ImagePath,};
                    DB_Status dbs = dba.sp_populateDataSet("SP_Dept_Update_Image", 4, parameter, value);
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
                        displayMessage("Sorry! Links already exists", "error");
                    }
                    else if (result == "Success")
                    {
                        if (flagHasImage && flagValidImage)
                        {
                            if (hfImage_UploadedPath.Value.Length > 10)
                                File.Delete(Request.PhysicalApplicationPath + hfImage_UploadedPath.Value);
                            FileUploadImage.SaveAs(Server.MapPath("~/" + ImagePath));
                        }

                        displayMessage("Image successfully updated", "info");
                        FillPhotos();
                        hfRID.Value = "";
                        divShowImage.Src = "";
                        hfImage_UploadedPath.Value = "";
                        btnSave.Text = "Save";
                    }
                }

            }
        }
        catch (Exception ex)
        {
            displayMessage(ex.Message, "error");
        }
    }
 
    protected void FillPhotos()
    {
        try
        {
            string[] parameter = { "@Flag","@UpdateType_ID" };
            string[] value = { "View", hfUpdtTypeID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Dept_Update_Image", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    gridImages.DataSource = dt;
                    gridImages.DataBind();
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
            string RID = (sender as LinkButton).CommandArgument;
            hfRID.Value = RID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@RID", "@UpdateType_ID" };
            string[] value = { "ViewByID", hfRID.Value,hfUpdtTypeID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Dept_Update_Image", 3, parameter, value);
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
                        hfImage_UploadedPath.Value = dt.Rows[0]["ImagePath"].ToString();

                        divShowImage.Src = "../" + dt.Rows[0]["ImagePath"].ToString();

                    }
                }
            }
            if (flag)
            {
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
            string SurveyID = (sender as LinkButton).CommandArgument;
            hfRID.Value = SurveyID;

            string[] parameter = { "@Flag","@RID", "@UpdateType_ID" };
            string[] value = { "Delete",hfRID.Value,hfUpdtTypeID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Dept_Update_Image", 3, parameter, value);
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
                FillPhotos();
            }
        }
        catch (Exception)
        {
        }
    }


    protected void lnkAcvtiveStatus_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        displayMessage("", "");
        string RID = (sender as LinkButton).CommandArgument;
        hfRID.Value = RID;

        LinkButton lnkbtn_edit = (LinkButton)sender;
        Label lbl_status = (Label)lnkbtn_edit.Parent.FindControl("lblStatus");

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
        string[] parameter = { "@Flag", "@RID", "@UpdateType_ID" };
        string[] value = { Flag, hfRID.Value,hfUpdtTypeID.Value };
        DB_Status dbs = dba.sp_populateDataSet("SP_Dept_Update_Image", 3, parameter, value);
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
                FillPhotos();
            }
            else if (result == "alreadyActive")
            {
                displayGridMessage("Already image active for this title", "error");
            }
        }
    }
}