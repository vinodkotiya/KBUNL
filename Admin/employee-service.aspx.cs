using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_employee_service : System.Web.UI.Page
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
            //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_banner");
            //menuli.Attributes["class"] = "active";

            panelAddNew.Visible = false;
            panelView.Visible = true;

            FillEmployeeServices();
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
        hdfEmployeeService_Id.Value = "";
        hdfImage_UploadedPath.Value = "No";
        txtServiceTitleEnglish.Text = "";
        txtServiceTitleHindi.Text = "";
        txtServiceLink.Text = "";
        txtSeqNo.Text = "";
        btnSave.Text = "Save";
        ddlUrlOpen.SelectedIndex = 0;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtServiceTitleEnglish.Text.Trim() == "")
                    displayMessage("Please enter service title (English)", "error");
                else if (txtServiceTitleHindi.Text.Trim() == "")
                    displayMessage("Please enter service title (Hindi)", "error");
                else
                {
                    bool flagValidImage = true;
                    bool flagHasImage = false;
                    string ImageFileName = "";
                    string ImagePath = "NA";
                    if (fupIcon.HasFile)
                    {
                        flagHasImage = true;
                        string ext = Path.GetExtension(fupIcon.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        ImageFileName = "ServiceIcon_" + datevalue + ext;
                        ImagePath = "Uploads/EmployeeServiceIcon/" + ImageFileName;
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
                            return;
                        }
                    }
                    string[] parameter = { "@Flag", "@ServiceTitleHindi", "@ServiceTitleEnglish", "@SeqNo", "@ServiceLink", "@Icon", "@URLOpenIn" };
                    string[] value = { "Add", txtServiceTitleHindi.Text.Trim(), txtServiceTitleEnglish.Text.Trim(),txtSeqNo.Text.Trim(),txtServiceLink.Text.Trim(), hdfImage_UploadedPath.Value,ddlUrlOpen.SelectedValue };
                    DB_Status dbs = dba.sp_populateDataSet("SP_Employee_Service",7, parameter, value);
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
                            fupIcon.SaveAs(Server.MapPath("~/" + hdfImage_UploadedPath.Value));

                        displayMessage("Record successfully added", "info");
                        FillEmployeeServices();
                        hdfEmployeeService_Id.Value = "0";
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
                if (txtServiceTitleEnglish.Text.Trim() == "")
                    displayMessage("Please enter service title (English)", "error");
                else if (txtServiceTitleHindi.Text.Trim() == "")
                    displayMessage("Please enter service title (Hindi)", "error");
                else
                {
                    bool flagValidImage = true;
                    bool flagHasImage = false;
                    string ImageFileName = "";
                    string ImagePath = "NA";

                    if (fupIcon.HasFile)
                    {
                        flagHasImage = true;
                        string ext = Path.GetExtension(fupIcon.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        ImageFileName = "ServiceIcon_" + datevalue + ext;

                        ImagePath = "Uploads/EmployeeServiceIcon/" + ImageFileName;
                        hdfUpdateImagePath.Value = ImagePath;
                        if (ext == ".jpg" || ext == ".JPG" || ext == ".png" || ext == ".PNG")
                            flagValidImage = true;
                        else
                            flagValidImage = false;

                    }
                    if (flagHasImage && flagValidImage == false)
                    {
                        displayMessage("Please attach only image file ", "error");
                        return;
                    }
                    else
                    {
                        string[] parameter = { "@Flag", "@EmployeeServiceId","@ServiceTitleHindi", "@ServiceTitleEnglish", "@SeqNo", "@ServiceLink", "@Icon", "@URLOpenIn" };
                        string[] value = { "Update", hdfEmployeeService_Id.Value, txtServiceTitleHindi.Text.Trim(), txtServiceTitleEnglish.Text.Trim(), txtSeqNo.Text.Trim(),txtServiceLink.Text.Trim(), hdfUpdateImagePath.Value,ddlUrlOpen.SelectedValue };
                        DB_Status dbs = dba.sp_populateDataSet("SP_Employee_Service",8, parameter, value);
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
                                FileInfo file = new FileInfo(Server.MapPath("~/" + hdfImage_UploadedPath.Value));
                                if (file.Exists)
                                    file.Delete();
                                fupIcon.SaveAs(Server.MapPath("~/" + ImagePath));
                            }

                            displayMessage("Record successfully updated", "info");
                            FillEmployeeServices();
                            hdfEmployeeService_Id.Value = "0";
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
        hdfEmployeeService_Id.Value = "0";
        hdfImage_UploadedPath.Value = "No";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void FillEmployeeServices()
    {
        try
        {
            string[] parameter = { "@Flag" };
            string[] value = { "LoadForAdmin" };
            DB_Status dbs = dba.sp_populateDataSet("SP_Employee_Service", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    grdService.DataSource = dt;
                    grdService.DataBind();
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
            hdfEmployeeService_Id.Value = noticeId;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@EmployeeServiceId" };
            string[] value = { "LoadbyID", hdfEmployeeService_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Employee_Service", 2, parameter, value);
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
                        hdfImage_UploadedPath.Value = Convert.ToString(dt.Rows[0]["Icon"]);
                        hdfUpdateImagePath.Value= Convert.ToString(dt.Rows[0]["Icon"]);
                        if (!string.IsNullOrEmpty(hdfImage_UploadedPath.Value) && hdfImage_UploadedPath.Value != "No")
                            RequiredFieldValidatorfupIcon.Enabled = false;
                        else
                            RequiredFieldValidatorfupIcon.Enabled = true;
                        txtServiceTitleEnglish.Text = Convert.ToString(dt.Rows[0]["ServiceTitleEnglish"]);
                        txtServiceTitleHindi.Text = Convert.ToString(dt.Rows[0]["ServiceTitleHindi"]);
                        txtServiceLink.Text = Convert.ToString(dt.Rows[0]["ServiceLink"]);
                        txtSeqNo.Text = Convert.ToString(dt.Rows[0]["SeqNo"]);
                        ddlUrlOpen.SelectedValue= Convert.ToString(dt.Rows[0]["URLOpenIn"]);
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
        catch (Exception ex)
        {
        }
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string serviceId = (sender as LinkButton).CommandArgument;
            hdfEmployeeService_Id.Value = serviceId;

            string[] parameter = { "@Flag", "@EmployeeServiceId" };
            string[] value = { "Delete", hdfEmployeeService_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Employee_Service", 2, parameter, value);
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
                hdfEmployeeService_Id.Value = "0";
                FillEmployeeServices();
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
        string serviceId = ((HiddenField)ckkIsactive.Parent.FindControl("hdfServiceIdGrd")).Value;
        string Flag = "";

        if (ckkIsactive.Checked)
        {
            Flag = "Activate";
        }
        else
        {
            Flag = "Deactivate";
        }

        string[] parameter = { "@Flag", "@EmployeeServiceId" };
        string[] value = { Flag, serviceId };
        DB_Status dbs = dba.sp_populateDataSet("SP_Employee_Service", 2, parameter, value);
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