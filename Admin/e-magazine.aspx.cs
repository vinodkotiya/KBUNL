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
            div_headTitle.InnerText = " Admin > E-Magazine";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Convert.ToString(Session["deprt_name"]) + " > E-Magazine";
            hdfDept_Id.Value = Session["DeptID"].ToString();
        }
        else
        {
            Response.Redirect("../login.aspx");
        }

        if (!IsPostBack)
        {
            //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_magazine");
            //menuli.Attributes["class"] = "active";
            BindYear();
            panelAddNew.Visible = false;
            panelView.Visible = true;
            FillEMagazine();
            displayGridMessage("", "");
        }
    }

    public void BindYear()
    {
        ddlyear.Items.Clear();
        ddlyear.Items.Insert(0, "--Select--");
        ddlyear.Items[0].Value = "0";
        for (int i = DateTime.Now.Year + 1; i >= 2015; i--)
        {
            ddlyear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        ddlyear.SelectedValue = DateTime.Now.Year.ToString();
    }
    /* Add/Update/Deactivate Magazine*/
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
        hfEMagazineID.Value = "";
        RequiredFieldValidatorFileUploadImage.Enabled = true;
        ddlyear.SelectedValue = DateTime.Now.Year.ToString();
        ddlMonth.ClearSelection();
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (ddlyear.SelectedValue == "")
                {
                    displayMessage("Please select Year", "error");
                }
                else if(ddlMonth.SelectedValue=="")
                {
                    displayMessage("Please select Month", "error");
                }
                else if (!fileUploadCover.HasFile)
                {
                    displayMessage("Please upload cover image", "error");
                }
                else if (!FileUploadImage.HasFile)
                {
                    displayMessage("Please upload E-Magazine file", "error");
                }
               
                else
                {
                    bool flagValidImage = true;
                    bool flagHasImage = false;
                    string ImageFileName = "";
                    string ImagePath = "NA";

                    bool CoverValidImage = true;
                    bool CoverHasImage = false;
                    string CoverImageFileName = "";
                    string CoverImagePath = "NA";


                    flagHasImage = true;
                    string ext = Path.GetExtension(FileUploadImage.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    ImageFileName = "EMagazine_" + datevalue + ext;
                    ImagePath = "Uploads/EMagazine/" + ImageFileName;


                    CoverHasImage = true;
                    string ext1 = Path.GetExtension(fileUploadCover.FileName);
                    CoverImageFileName = "EMagazine_Cover" + datevalue + ext1;
                    CoverImagePath = "Uploads/EMagazine/" + CoverImageFileName;
                    hdfCoverImage_UploadedPath.Value = CoverImagePath;

                    if (ext1 == ".jpg" || ext1 == ".JPG" || ext1 == ".jpeg" || ext1 == ".JPEG" || ext1 == ".gif" || ext1 == ".GIF" || ext1 == ".png" || ext1 == ".PNG")
                    {
                        CoverValidImage = true;
                    }
                    else
                    {
                        CoverValidImage = false;
                    }
                    if (flagHasImage && flagValidImage == false)
                    {
                        displayMessage("Please attach only Valid file ", "error");
                    }
                    else if (CoverHasImage && CoverValidImage == false)
                    {
                        displayMessage("Please attach only image file for cover image ", "error");
                    }
                    else
                    {
                        string[] parameter = {"@Flag", "@DeptID" ,"@Month", "@Year","@CoverImage", "@AttachmentPath","" };
                        string[] value = {"Add",hdfDept_Id.Value, ddlMonth.SelectedValue, ddlyear.SelectedValue,CoverImagePath, ImagePath };
                        DB_Status dbs = dba.sp_populateDataSet("SP_EMagazine",6, parameter, value);
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
                            if (flagHasImage && flagValidImage)
                                FileUploadImage.SaveAs(Server.MapPath("~/" + ImagePath));
                            if (CoverHasImage && CoverValidImage)
                                fileUploadCover.SaveAs(Server.MapPath("~/" + CoverImagePath));

                            displayMessage("E-Magazine successfully added", "info");
                            FillEMagazine();
                            hfEMagazineID.Value = "";
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;
                        }
                        else if (result == "Exists")
                        {
                            displayMessage("E-Magazine already exists", "error");
                        }
                    }

                }
            }
            else if (btnSave.Text == "Update")
            {
                if (ddlyear.SelectedValue == "")
                {
                    displayMessage("Please select Year", "error");
                }
                else if (ddlMonth.SelectedValue == "")
                {
                    displayMessage("Please select Month", "error");
                }
                else
                {
                    bool flagValidImage = true;
                    bool flagHasImage = false;
                    string ImageFileName = "";
                    string ImagePath = "NA";


                    bool CoverValidImage = true;
                    bool CoverHasImage = false;
                    string CoverImageFileName = "";
                    string CoverImagePath = "NA";


                    if (FileUploadImage.HasFile)
                    {
                        flagHasImage = true;
                        string ext = Path.GetExtension(FileUploadImage.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        ImageFileName = "EMagazine_" + datevalue + ext;

                        ImagePath = "Uploads/EMagazine/" + ImageFileName;
                        FileInfo file = new FileInfo(Server.MapPath("~/"+hdfImage_UploadedPath.Value));
                        if (file.Exists)
                            file.Delete();
                        hdfImage_UploadedPath.Value = ImagePath;
                        //if (ext == ".jpg" || ext == ".JPG" || ext == ".png" || ext == ".PNG")
                        //    flagValidImage = true;
                        //else
                        //    flagValidImage = false;

                    }
                    if (fileUploadCover.HasFile)
                    {
                        CoverHasImage = true;
                        string ext = Path.GetExtension(fileUploadCover.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        CoverImageFileName = "EMagazine_Cover" + datevalue + ext;

                        CoverImagePath = "Uploads/EMagazine/" + CoverImageFileName;
                        FileInfo file = new FileInfo(Server.MapPath("~/"+ hdfCoverImage_UploadedPath.Value));
                        if (file.Exists)
                            file.Delete();
                        hdfCoverImage_UploadedPath.Value = CoverImagePath;

                        if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                            CoverValidImage = true;
                        else
                            CoverValidImage = false;
                    }


                    if (flagHasImage && flagValidImage == false)
                    {
                        displayMessage("Please attach only valid File", "error");
                    }
                    else if (CoverHasImage && CoverValidImage == false)
                    {
                        displayMessage("Please attach only image file for cover image ", "error");
                    }
                    else
                    {
                        string[] parameter = { "@Flag", "@EMagazineID", "@DeptID","@Month", "@Year", "@CoverImage", "@AttachmentPath" };
                        string[] value = { "Update", hfEMagazineID.Value,hdfDept_Id.Value,ddlMonth.SelectedValue, ddlyear.SelectedValue, hdfCoverImage_UploadedPath.Value, hdfImage_UploadedPath.Value };
                        DB_Status dbs = dba.sp_populateDataSet("SP_EMagazine", 7, parameter, value);
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
                            if (flagHasImage && flagValidImage)
                            {
                                if (hdfImage_UploadedPath.Value.Length > 10)
                                    File.Delete(Request.PhysicalApplicationPath + hdfImage_UploadedPath.Value);

                                FileUploadImage.SaveAs(Server.MapPath("~/" + ImagePath));
                            }

                            if (CoverHasImage && CoverValidImage)
                            {
                                if(hdfCoverImage_UploadedPath.Value.Length>10)
                            
                                File.Delete(Request.PhysicalApplicationPath + hdfCoverImage_UploadedPath.Value);
                                fileUploadCover.SaveAs(Server.MapPath("~/" + CoverImagePath));
                            }


                            displayMessage("E-Magazine successfully updated", "info");
                            FillEMagazine();
                            hfEMagazineID.Value = "";
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;
                        }
                        else if (result == "Exists")
                        {
                            displayMessage("E-Magazine already exists", "error");
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
        hfEMagazineID.Value = "";
        btnSave.Text = "Save";
        RequiredFieldValidatorFileUploadImage.Enabled = true;
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void FillEMagazine()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = {"View",hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_EMagazine", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    gridMagazine.DataSource = dt;
                    gridMagazine.DataBind();
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
            string EMagazineID = (sender as LinkButton).CommandArgument;
            hfEMagazineID.Value = EMagazineID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = {"@Flag", "@EMagazineID" };
            string[] value = { "ViewbyID", hfEMagazineID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_EMagazine", 2, parameter, value);
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
                        ddlMonth.SelectedValue = dt.Rows[0]["Month"].ToString();
                        ddlyear.SelectedValue = dt.Rows[0]["Year"].ToString();
                        hdfCoverImage_UploadedPath.Value = dt.Rows[0]["CoverImage"].ToString();
                        hdfImage_UploadedPath.Value = dt.Rows[0]["AttachmentPath"].ToString();
                        
                        if (!string.IsNullOrEmpty(hdfImage_UploadedPath.Value) && hdfImage_UploadedPath.Value != "No")
                            RequiredFieldValidatorFileUploadImage.Enabled = false;
                        else
                            RequiredFieldValidatorFileUploadImage.Enabled = true;

                        if (!string.IsNullOrEmpty(hdfCoverImage_UploadedPath.Value) && hdfCoverImage_UploadedPath.Value != "No")
                            RequiredFieldValidatorfileUploadCover.Enabled = false;
                        else
                            RequiredFieldValidatorfileUploadCover.Enabled = true;
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
            string EMagazineID = (sender as LinkButton).CommandArgument;
            hfEMagazineID.Value = EMagazineID;

            string[] parameter = { "@Flag", "@EMagazineID" };
            string[] value = {"Delete", hfEMagazineID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_EMagazine", 2, parameter, value);
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
                hfEMagazineID.Value = "";
                FillEMagazine();
            }
        }
        catch (Exception)
        {
        }
    }    
}