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
        if (Session["AdminUserID"] != null)
        {
            hdfDept_Id.Value = "0";
            div_headTitle.InnerText = "Admin > Schedules";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Convert.ToString(Session["deprt_name"]) + " > Schedules";
            hdfDept_Id.Value = Session["DeptID"].ToString();
        }
        else
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {
            //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_events");
            //menuli.Attributes["class"] = "active";

            panelAddNew.Visible = false;
            panelView.Visible = true;

            FillEvents();
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
        hfRID.Value = "";
        hfFile_UploadedPath.Value = "";

        txtTitleEnglish.Text = "";
        txtDescEnglish.Text = "";
        txtTitleHindi.Text = "";
        txtDescHindi.Text = "";
        txtEventDate.Text = "";
        txtTime.Text = "";

        btnSave.Text = "Save";
        span_FileStatus.Style.Add("display", "none");
        span_FileStatus.InnerText = "";
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hfRID.Value = "";
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
            if (txtTitleEnglish.Text == "")
                displayMessage("Please enter event title", "error");
            else if(txtTitleHindi.Text == "")
                displayMessage("Please enter event title", "error");
            else
            {
                //Check Save or Update
                string Flag = "";
                if (btnSave.Text == "Save")
                    Flag = "insert";
                else if (btnSave.Text == "Update")
                    Flag = "Update";
                //DeptID 0 for Admin
                string filePath;
                bool flagHasFile = false;
                string fileName = "";
                if (hfFile_UploadedPath.Value=="")
                {
                    filePath = "NA";
                }
                else
                {
                    filePath = hfFile_UploadedPath.Value;
                }
                
                if (FileUpload1.HasFile)
                {
                    flagHasFile = true;
                    string ext = Path.GetExtension(FileUpload1.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    fileName = "Event_Calendar_" + datevalue + ext;

                    filePath = "Uploads/EventCalendar/" + fileName;
                }
                string[] param = { "@Flag", "@RID", "@DeptID", "@TitleEnglish", "@TitleHindi", "@DescEnglish", "@DescHindi", "@Attachment", "@Date", "@Time" };
                string[] value = { Flag, hfRID.Value,hdfDept_Id.Value,txtTitleEnglish.Text.Trim(), txtTitleHindi.Text.Trim(), txtDescEnglish.Text.Trim(),txtDescHindi.Text.Trim(), filePath, obj.makedate(txtEventDate.Text.Trim()),txtTime.Text };
                DB_Status dbs = dba.sp_populateDataSet("Sp_Event_calendar",10, param, value);
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

                    if (result == "Inserted")
                    {
                        btnSave.Text = "Save";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
                        FillEvents();
                        txtTitleEnglish.Text = "";
                        txtDescEnglish.Text = "";
                        txtTitleHindi.Text = "";
                        txtDescHindi.Text = "";
                        txtEventDate.Text = "";
                        txtTime.Text = "";
                        if (flagHasFile && fileName!="")
                            FileUpload1.SaveAs(Server.MapPath("~/" + filePath));

                        hfFile_UploadedPath.Value = "";

                    }
                    else if (result == "Updated")
                    {

                        hfRID.Value = "";
                        btnSave.Text = "Save";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
                        FillEvents();
                        txtTitleEnglish.Text = "";
                        txtDescEnglish.Text = "";
                        txtTitleHindi.Text = "";
                        txtDescHindi.Text = "";
                        txtEventDate.Text = "";
                        txtTime.Text = "";
                        if (flagHasFile)
                        {
                            if (hfFile_UploadedPath.Value.Length > 10)
                                File.Delete(Request.PhysicalApplicationPath + hfFile_UploadedPath.Value);

                            FileUpload1.SaveAs(Server.MapPath("~/" + filePath));
                        }

                        hfFile_UploadedPath.Value = "";

                    }
                    else if (result == "exist")
                    {
                        displayMessage("Sorry! Event Already Exists", "error");
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
  
    protected void FillEvents()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "View",hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Event_calendar",2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    grdEvents.DataSource = dt;
                    grdEvents.DataBind();
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
            hfRID.Value = QuestionID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@RID" };
            string[] value = { "ViewByID", hfRID.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Event_calendar", 2, parameter, value);
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
                        txtTitleEnglish.Text =Convert.ToString(dt.Rows[0]["TitleEnglish"]);
                        txtDescEnglish.Text = Convert.ToString(dt.Rows[0]["DescEnglish"]);
                        txtTitleHindi.Text = Convert.ToString(dt.Rows[0]["TitleHindi"]);
                        txtDescHindi.Text = Convert.ToString(dt.Rows[0]["DescHindi"]);
                        txtEventDate.Text = Convert.ToString(dt.Rows[0]["EventDate"]);
                        txtTime.Text = Convert.ToString(dt.Rows[0]["EventTime"]);
                        hfFile_UploadedPath.Value = Convert.ToString(dt.Rows[0]["Attachment"]);
                        if(hfFile_UploadedPath.Value!="")
                        {
                            span_FileStatus.Style.Add("display", "block");
                            span_FileStatus.InnerText = "File Selected";
                            span_FileStatus.Style.Add("color", "green");
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
            string RID = (sender as LinkButton).CommandArgument;
            hfRID.Value = RID;

            string[] parameter = { "@Flag", "@RID" };
            string[] value = { "Delete", hfRID.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Event_calendar", 2, parameter, value);
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
                FillEvents();
            }
        }
        catch (Exception)
        {
        }
    }
    protected void lnkAcvtiveStatus_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");

            string RID = (sender as LinkButton).CommandArgument;
            hfRID.Value = RID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            Label lbl_status = (Label)lnkbtn_edit.Parent.FindControl("lblActiveStatus");

            string Flag = lbl_status.Text;

            if (Flag == "No")
            {
                Flag = "Activate";
            }
            else
            {
                Flag = "Deavtivate";
            }
            string result = "";
            string[] parameter = { "@Flag", "@RID" };
            string[] value = { Flag, hfRID.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Event_calendar", 2, parameter, value);
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
                    FillEvents();
                }

            }
        }
        catch (Exception ex)
        {

        }
        
    }
    protected void grdEvents_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdEvents.PageIndex = e.NewPageIndex;
        FillEvents();
    }

    
}