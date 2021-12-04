using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_department_info_update : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["DeptID"] == null)
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {
            //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_banner");
            //menuli.Attributes["class"] = "active";
            hdfDeptId.Value = Convert.ToString(Session["DeptID"]);
            hdfEmpployee_Id.Value = Convert.ToString(Session["EmpID"]);
            FillDepartmentInformation();
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
            if (btnSave.Text == "Submit")
            {
                if(txtDescriptionEnglish.Text.Trim() == "")
                    displayMessage("Description cannot be blank", "error");
                else if(txtDescriptionHindi.Text.Trim() == "")
                    displayMessage("Description cannot be blank", "error");
                else
                {
                    bool flagValidFile = true;
                    bool flagHasFile = true;
                    string Attachment_FileName = "NA";
                    string Attachment_FilePath = "NA";

                    if (flagHasFile && flagValidFile)
                    {
                        string[] parameter = { "@Flag", "@DeptID", "@PageTitleEnglish", "@PageTitleHindi", "@BannerImage", "@AboutDeptEnglish", "@AboutDeptHindi", "@VisionEnglish", "@VisionHindi","@MoreInfoEnglish","@MoreInfoHindi" };
                        string[] value = { "Add", hdfDeptId.Value, "", "", hdfImage_UploadedPath.Value, txtDescriptionEnglish.Text.Trim(), txtDescriptionHindi.Text.Trim(), "", "" ,txtTextEnglish.Content,txtTextHindi.Content};
                        DB_Status dbs = dba.sp_populateDataSet("SP_Department_Details", 11, parameter, value);
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
                            displayMessage("Record successfully added", "info");
                            FillDepartmentInformation();
                            hdfDept_Details_Id.Value = "0";
                            hdfImage_UploadedPath.Value = "No";
                            btnSave.Text = "Save";
                        }
                        else if (result == "AlreadyExists")
                        {
                            displayMessage("Record already exists", "error");
                        }
                    }
                 }
            }
            else if (btnSave.Text == "Update")
            {
                if (txtDescriptionEnglish.Text.Trim() == "")
                    displayMessage("Description cannot be blank", "error");
                else if (txtDescriptionHindi.Text.Trim() == "")
                    displayMessage("Description cannot be blank", "error");
                else
                {
                    bool flagValidFile = true;
                    bool flagHasFile = true;
                    string Attachment_FileName = "NA";
                    string Attachment_FilePath = "NA";

                    string[] parameter = { "@Flag", "@DeptID", "@PageTitleEnglish", "@PageTitleHindi", "@BannerImage", "@AboutDeptEnglish", "@AboutDeptHindi", "@VisionEnglish", "@VisionHindi","@MoreInfoEnglish", "@MoreInfoHindi" };
                    string[] value = { "Update", hdfDeptId.Value, "", "", Attachment_FilePath, txtDescriptionEnglish.Text.Trim(), txtDescriptionHindi.Text.Trim(), "", "", txtTextEnglish.Content, txtTextHindi.Content };
                    DB_Status dbs = dba.sp_populateDataSet("SP_Department_Details", 11, parameter, value);
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
                        displayMessage("Record successfully updated", "info");
                        FillDepartmentInformation();
                        hdfDept_Details_Id.Value = "0";
                        hdfImage_UploadedPath.Value = "No";
                    }
                    else if (result == "AlreadyExists")
                    {
                        displayMessage("Record already exists", "error");
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
        hdfDept_Details_Id.Value = "0";
        hdfImage_UploadedPath.Value = "No";
        btnSave.Text = "Save";
    }
    protected void FillDepartmentInformation()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "LoadbyID",hdfDeptId.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Department_Details",2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txtDescriptionEnglish.Text = Convert.ToString(dt.Rows[0]["AboutDeptEnglish"]);
                        txtDescriptionHindi.Text = Convert.ToString(dt.Rows[0]["AboutDeptHindi"]);
                        hdfImage_UploadedPath.Value= Convert.ToString(dt.Rows[0]["BannerImage"]);
                        hdfRetrieveImageFile.Value = Convert.ToString(dt.Rows[0]["BannerImage"]);
                        txtTextEnglish.Content = Convert.ToString(dt.Rows[0]["MoreInfoEnglish"]);
                        txtTextHindi.Content = Convert.ToString(dt.Rows[0]["MoreInfoHindi"]);
                        btnSave.Text = "Update";
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
   
}