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
        if (!IsPostBack)
        {
            //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_department");
            //menuli.Attributes["class"] = "active";
            FillDeprtData();
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


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTitle.Text == "")
                displayMessage("Please enter Title", "error");
            else if (txtDescription.Text == "")
            {
                displayMessage("Please enter description", "error");
            }
            else if (hfImage_UploadedPath.Value == "NA" && FileUploader1.HasFile == false)
            {
                displayMessage("Please add image", "error");
            }
            else
            {
                bool flagValidFile = true;
                bool flagHasFile = true;
                string Attachment_FileName = "NA";
                string Attachment_FilePath = hfImage_UploadedPath.Value;

                //Check File is attached or not
                if (FileUploader1.HasFile)
                {
                    string ext = Path.GetExtension(FileUploader1.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    Attachment_FileName = Request.QueryString["deprt_name"] + datevalue + ext;
                    Attachment_FilePath = "Uploads/DprtAdminBanner/" + Attachment_FileName;
                    if (!(ext.ToUpper() == ".JPG" || ext.ToUpper() == ".PNG" || ext.ToUpper() == ".JPEG"))
                    {
                        flagValidFile = false;
                        displayMessage("Please attach image only", "error");
                    }
                    else
                        flagValidFile = true;
                }

                if (flagHasFile && flagValidFile)
                {
                    string[] param = { "@Flag", "@DeptID", "@Title", "@DescriptionH", "@Description", "@VisionE", "@VisionH", "@BannerImage" };
                    string[] value = { "Update", hfUserID.Value, txtTitle.Text.Trim(), txtDesH.Text.Trim(), txtDescription.Text.Trim(),txtVisionE.Text.Trim(),txtVisionH.Text.Trim(), Attachment_FilePath };
                    DB_Status dbs = dba.sp_populateDataSet("SP_Department_Admin", 8, param, value);
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
                        if (Attachment_FileName != "NA" && flagHasFile && flagValidFile)
                        {
                            try
                            {
                                File.Delete(Request.PhysicalApplicationPath + hfImage_UploadedPath.Value);
                            }
                            catch (Exception ex)
                            {
                            }
                            FileUploader1.SaveAs(Server.MapPath("~/" + Attachment_FilePath));
                        }
                        FillDeprtData();

                        displayMessage("Record Successfully Updated", "info");
                    }
                    else if (result == "AlreadyExists")
                    {
                        displayMessage("Sorry! Record Already Exists", "error");
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

    protected void FillDeprtData()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "LoadByID", hfUserID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Department_Admin", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtTitle.Text = dt.Rows[0]["Title"].ToString();
                        txtDescription.Text = dt.Rows[0]["Description"].ToString();
                        txtDesH.Text = dt.Rows[0]["DescriptionH"].ToString();
                        txtVisionE.Text = dt.Rows[0]["VisionE"].ToString();
                        txtVisionH.Text = dt.Rows[0]["VisionH"].ToString();
                        hfImage_UploadedPath.Value = dt.Rows[0]["BannerImage"].ToString();

                        if (txtTitle.Text == "")
                        {
                           
                            btnSave.Text = "Save";
                        }
                        else
                        {
                            divShowImage.Src="../"+dt.Rows[0]["BannerImage"].ToString();
                            btnSave.Text = "Update";
                        }

                    }

                }
            }
        }
        catch (Exception ex)
        {
        }
    }


}