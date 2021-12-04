using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_department_highlights : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] != null)
        {
            hdfDeptId.Value = "0";
            div_headTitle.InnerText = " Admin > Department Highlights";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Convert.ToString(Session["deprt_name"]) + " > Department Highlights";
            hdfDeptId.Value = Session["DeptID"].ToString();
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
            FillDepartmentHighlight();
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
        hdfHighlightId.Value = "";
        hdfImage_UploadedPath.Value = "No";
        txtDescriptionE.Text = "";
        txtDescriptionH.Text = "";
        txtHighlightDate.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtHighlightDate.Text.Trim() == "")
                    displayMessage("Please select highlight date", "error");
                else if (txtDescriptionE.Text.Trim() == "")
                    displayMessage("Please enter description", "error");
                else if (txtDescriptionH.Text.Trim() == "")
                    displayMessage("Please enter description", "error");
                else
                {
                    bool flagValidImage = true;
                    bool flagHasImage = false;
                    string ImageFileName = "";
                    string ImagePath = "NA";
                    if (fupHighlight.HasFile)
                    {
                        flagHasImage = true;
                        string ext = Path.GetExtension(fupHighlight.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        ImageFileName = "Dept_Highlight_" + datevalue + ext;
                        ImagePath = "Uploads/DepartmentHighlight/" + ImageFileName;
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
                    string[] parameter = { "@Flag", "@DeptID", "@HighlightDate", "@HighlightImage", "@DescriptionE", "@DescriptionH" };
                    string[] value = { "Add", hdfDeptId.Value, obj.makedate(txtHighlightDate.Text.Trim()), ImagePath, txtDescriptionE.Text.Trim(), txtDescriptionH.Text.Trim() };
                    DB_Status dbs = dba.sp_populateDataSet("SP_Department_Highlights", 6, parameter, value);
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
                            fupHighlight.SaveAs(Server.MapPath("~/" + hdfImage_UploadedPath.Value));

                        displayMessage("Record successfully added", "info");
                        FillDepartmentHighlight();
                        hdfHighlightId.Value = "0";
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
                if (txtHighlightDate.Text.Trim() == "")
                    displayMessage("Please select highlight date", "error");
                else if (txtDescriptionE.Text.Trim() == "")
                    displayMessage("Please enter description", "error");
                else if (txtDescriptionH.Text.Trim() == "")
                    displayMessage("Please enter description", "error");
                else
                {
                    bool flagValidImage = true;
                    bool flagHasImage = false;
                    string ImageFileName = "";
                    string ImagePath = "NA";

                    if (fupHighlight.HasFile)
                    {
                        flagHasImage = true;
                        string ext = Path.GetExtension(fupHighlight.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        ImageFileName = "Dept_Highlight_" + datevalue + ext;

                        ImagePath = "Uploads/DepartmentHighlight/" + ImageFileName;
                        hdfUpdateImagePath.Value = ImagePath;

                        if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
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
                        string[] parameter = { "@Flag", "@HightlightsId", "@DeptID", "@HighlightDate", "@HighlightImage", "@DescriptionE", "@DescriptionH" };
                        string[] value = { "Update", hdfHighlightId.Value, hdfDeptId.Value, obj.makedate(txtHighlightDate.Text.Trim()), ImagePath, txtDescriptionE.Text.Trim(), txtDescriptionH.Text.Trim() };
                        DB_Status dbs = dba.sp_populateDataSet("SP_Department_Highlights",7, parameter, value);
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
                                FileInfo file = new FileInfo(Server.MapPath("~/"+hdfImage_UploadedPath.Value));
                                if (file.Exists)
                                    file.Delete();

                                fupHighlight.SaveAs(Server.MapPath("~/" + ImagePath));
                            }

                            displayMessage("Record successfully updated", "info");
                            FillDepartmentHighlight();
                            hdfHighlightId.Value = "0";
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
        hdfHighlightId.Value = "0";
        hdfImage_UploadedPath.Value = "No";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void FillDepartmentHighlight()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "LoadForAdmin",hdfDeptId.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Department_Highlights", 2, parameter, value);
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
            string highlightId = (sender as LinkButton).CommandArgument;
            hdfHighlightId.Value = highlightId;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@HightlightsId" };
            string[] value = { "LoadbyID", hdfHighlightId.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Department_Highlights", 2, parameter, value);
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
                        hdfImage_UploadedPath.Value = Convert.ToString(dt.Rows[0]["HighlightImage"]);
                        hdfUpdateImagePath.Value = Convert.ToString(dt.Rows[0]["HighlightImage"]);
                        txtHighlightDate.Text = Convert.ToString(dt.Rows[0]["HighlightDate"]);
                        txtDescriptionE.Text = Convert.ToString(dt.Rows[0]["DescriptionE"]);
                        txtDescriptionH.Text = Convert.ToString(dt.Rows[0]["DescriptionH"]);
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
            string highlightId = (sender as LinkButton).CommandArgument;
            hdfHighlightId.Value = highlightId;

            string[] parameter = { "@Flag", "@HightlightsId" };
            string[] value = { "Delete", hdfHighlightId.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Department_Highlights", 2, parameter, value);
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
                hdfHighlightId.Value = "0";
                FillDepartmentHighlight();
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
        string highlightId = ((HiddenField)ckkIsactive.Parent.FindControl("hdfHighlightIdGrd")).Value;
        string Flag = "";

        if (ckkIsactive.Checked)
        {
            Flag = "Activate";
        }
        else
        {
            Flag = "Deactivate";
        }

        string[] parameter = { "@Flag", "@HightlightsId" };
        string[] value = { Flag, highlightId };
        DB_Status dbs = dba.sp_populateDataSet("SP_Department_Highlights", 2, parameter, value);
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
                        displayGridMessage("Record successfully activated", "info");
                    else if (status == "Deactivated")
                        displayGridMessage("Record successfully deactivated", "info");
                }
            }
        }
    }
}