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
            div_headTitle.InnerText = "Admin > Messages";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Convert.ToString(Session["deprt_name"]) + " > Banner";
            hdfDept_Id.Value = Session["DeptID"].ToString();
        }
        else
        {
            Response.Redirect("../login.aspx");
        }

        if (!IsPostBack)
        {
            panelAddNew.Visible = false;
            panelView.Visible = true;

            FillMessages();
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
    protected void FillMessages()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "Load", hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Directors_Message", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    gridMessages.DataSource = dt;
                    gridMessages.DataBind();
                }
            }
        }
        catch (Exception)
        {
        }
    }

    protected void lbtn_AddNew_Click(object sender, EventArgs e)
    {
        panelAddNew.Visible = true;
        panelView.Visible = false;

        displayMessage("", "");
        displayGridMessage("", "");

        hdfMsgID.Value = "";
        hdfPhoto.Value = "No";
        hdfuploadmessage.Value = "";
        hdfuploadschedule.Value = "";
        txtNameEnglish.Text = "";
        txtNameHindi.Text = "";
        txtDesignationE.Text = "";
        txtDesignationH.Text = "";
        txtMsgEng.Text = "";
        txtMsgHin.Text = "";
        imgProfile.ImageUrl = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                try
                {
                    if (txtNameEnglish.Text == "")
                        displayMessage("Please Enter Name", "error");
                    else if (txtNameHindi.Text == "")
                        displayMessage("Please Enter Name", "error");
                    else if (txtDesignationE.Text == "")
                        displayMessage("Please Enter Designation", "error");
                    else if (txtDesignationH.Text == "")
                        displayMessage("Please Enter Designation", "error");
                    else
                    {
                        bool flagValidImage = true;
                        bool flagHasImage = false;
                        string ImageFileName = "";
                        string ImagePath = "NA";

                        string MeesageFileName = "";
                        string MeesageFilePath = "";

                        string ScheduleFileName = "";
                        string ScheduleFilePath = "";

                        if (FileUpload1.HasFile)
                        {
                            flagHasImage = true;
                            string ext = Path.GetExtension(FileUpload1.FileName);
                            string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                            ImageFileName = "Message_" + datevalue + ext;
                            ImagePath = "Uploads/MSGPhoto/" + ImageFileName;
                            hdfPhoto.Value = ImagePath;
                            if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                                flagValidImage = true;
                            else
                                flagValidImage = false;
                            if (flagValidImage == false)
                            {
                                displayMessage("Please browse jpg or png image", "error");
                                hdfPhoto.Value = "No";
                                return;
                            }
                        }
                        else
                        {
                            if (hdfPhoto.Value == "No")
                            {
                                displayMessage("Please browse image", "error");
                                return;
                            }
                        }

                        if (fileUploadMessage.HasFile)
                        {
                            string ext = Path.GetExtension(fileUploadMessage.FileName);
                            string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                            MeesageFileName = "Message_" + datevalue + ext;
                            MeesageFilePath = "Uploads/" + MeesageFileName;
                            hdfuploadmessage.Value = MeesageFilePath;
                        }
                        if (fileUploadSchedule.HasFile)
                        {
                            string ext = Path.GetExtension(fileUploadSchedule.FileName);
                            string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                            ScheduleFileName = "Schedule_" + datevalue + ext;
                            ScheduleFilePath = "Uploads/" + ScheduleFileName;
                            hdfuploadschedule.Value = ScheduleFilePath;
                        }



                        string[] parameter = { "@Flag", "@Name", "@NameH", "@DeptID", "@Designation", "@DesignationH", "@PhotoPath", "@MessageHindi", "@MessageEnglish", "@Type", "@Sequence", "@MessageAttachment", "@ScheduleAttachment" };
                        string[] value = { "Add", txtNameEnglish.Text.Trim(), txtNameHindi.Text.Trim(), hdfDept_Id.Value, txtDesignationE.Text.Trim(), txtDesignationH.Text.Trim(), hdfPhoto.Value, txtMsgHin.Text.Trim(), txtMsgEng.Text.Trim(), "", "1", hdfuploadmessage.Value, hdfuploadschedule.Value };
                        DB_Status dbs = dba.sp_populateDataSet("SP_Directors_Message", 13, parameter, value);
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
                        else
                        {
                            displayMessage(dbs.Title, "error");
                        }

                        if (result == "Inserted" || result == "Updated")
                        {
                            if (FileUpload1.HasFile)
                                FileUpload1.SaveAs(Server.MapPath("~/" + hdfPhoto.Value));

                            if (fileUploadMessage.HasFile)
                                fileUploadMessage.SaveAs(Server.MapPath("~/" + hdfuploadmessage.Value));

                            if (fileUploadSchedule.HasFile)
                                fileUploadSchedule.SaveAs(Server.MapPath("~/" + hdfuploadschedule.Value));

                            displayMessage("Successfully Saved", "info");
                            FillMessages();
                            panelView.Visible = true;
                            panelAddNew.Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Attributes["class"] = "error";
                    lblMsg.Text = ex.Message.ToString();
                }
            }
            else if (btnSave.Text == "Update")
            {
                try
                {
                    if (txtNameEnglish.Text == "")
                        displayMessage("Please Enter Name", "error");
                    else if (txtNameHindi.Text == "")
                        displayMessage("Please Enter Name", "error");
                    else if (txtDesignationE.Text == "")
                        displayMessage("Please Enter Designation", "error");
                    else if (txtDesignationH.Text == "")
                        displayMessage("Please Enter Designation", "error");
                    else
                    {
                        bool flagValidImage = true;
                        bool flagHasImage = false;
                        string ImageFileName = "";
                        string ImagePath = "NA";

                        string MeesageFileName = "";
                        string MeesageFilePath = "";

                        string ScheduleFileName = "";
                        string ScheduleFilePath = "";

                        if (FileUpload1.HasFile)
                        {
                            flagHasImage = true;
                            string ext = Path.GetExtension(FileUpload1.FileName);
                            string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                            ImageFileName = "Message_" + datevalue + ext;
                            ImagePath = "Uploads/MSGPhoto/" + ImageFileName;
                            hdfPhoto.Value = ImagePath;
                            if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                                flagValidImage = true;
                            else
                                flagValidImage = false;
                            if (flagValidImage == false)
                            {
                                displayMessage("Please browse jpg or png image", "error");
                                hdfPhoto.Value = "No";
                                return;
                            }
                        }
                        else
                        {
                            if (hdfPhoto.Value == "No")
                            {
                                displayMessage("Please browse image", "error");
                                return;
                            }
                        }

                        if (fileUploadMessage.HasFile)
                        {
                            string ext = Path.GetExtension(fileUploadMessage.FileName);
                            string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                            MeesageFileName = "Message_" + datevalue + ext;
                            MeesageFilePath = "Uploads/" + MeesageFileName;
                            hdfuploadmessage.Value = MeesageFilePath;
                        }
                        if (fileUploadSchedule.HasFile)
                        {
                            string ext = Path.GetExtension(fileUploadSchedule.FileName);
                            string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                            ScheduleFileName = "Schedule_" + datevalue + ext;
                            ScheduleFilePath = "Uploads/" + ScheduleFileName;
                            hdfuploadschedule.Value = ScheduleFilePath;
                        }

                        string[] parameter = { "@Flag","@MsgID", "@Name", "@NameH", "@DeptID", "@Designation", "@DesignationH", "@PhotoPath", "@MessageHindi", "@MessageEnglish", "@Type","@Sequence","@MessageAttachment","@ScheduleAttachment" };
                        string[] value = { "Update", hdfMsgID.Value, txtNameEnglish.Text.Trim(), txtNameHindi.Text.Trim(), hdfDept_Id.Value, txtDesignationE.Text.Trim(), txtDesignationH.Text.Trim(), hdfPhoto.Value, txtMsgHin.Text.Trim(), txtMsgEng.Text.Trim(), "", "1", hdfuploadmessage.Value, hdfuploadschedule.Value };
                        DB_Status dbs = dba.sp_populateDataSet("SP_Directors_Message", 14, parameter, value);
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
                        else
                        {
                            displayMessage(dbs.Title, "error");
                        }

                        if (result == "Inserted" || result == "Updated")
                        {
                            if (FileUpload1.HasFile)
                                FileUpload1.SaveAs(Server.MapPath("~/" + hdfPhoto.Value));

                            if (fileUploadMessage.HasFile)
                                fileUploadMessage.SaveAs(Server.MapPath("~/" + hdfuploadmessage.Value));

                            if (fileUploadSchedule.HasFile)
                                fileUploadSchedule.SaveAs(Server.MapPath("~/" + hdfuploadschedule.Value));

                            displayMessage("Successfully Saved", "info");
                            FillMessages();
                            panelView.Visible = true;
                            panelAddNew.Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Attributes["class"] = "error";
                    lblMsg.Text = ex.Message.ToString();
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
        hdfMsgID.Value = "0";
        hdfPhoto.Value = "No";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    
    protected void Update_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string MsgID = (sender as LinkButton).CommandArgument;
            hdfMsgID.Value = MsgID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@MsgID" };
            string[] value = { "LoadbyID", hdfMsgID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Directors_Message", 2, parameter, value);
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
                        txtNameEnglish.Text= dt.Rows[0]["Name"].ToString();
                        txtNameHindi.Text = dt.Rows[0]["NameH"].ToString();
                        txtDesignationE.Text = dt.Rows[0]["Designation"].ToString();
                        txtDesignationH.Text = dt.Rows[0]["DesignationH"].ToString();
                        txtMsgEng.Text = dt.Rows[0]["MessageEnglish"].ToString(); 
                        txtMsgHin.Text = dt.Rows[0]["MessageHindi"].ToString();
                        hdfPhoto.Value = dt.Rows[0]["PhotoPath"].ToString();
                        imgProfile.ImageUrl="../" + dt.Rows[0]["PhotoPath"].ToString();
                        if (!string.IsNullOrEmpty(hdfPhoto.Value) && hdfPhoto.Value != "No")
                            RFV_ProfilePhoto.Enabled = false;
                        else
                            RFV_ProfilePhoto.Enabled = true;

                        hdfuploadmessage.Value = dt.Rows[0]["MessageAttachment"].ToString();
                        hdfuploadschedule.Value = dt.Rows[0]["ScheduleAttachment"].ToString();
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
            string MsgID = (sender as LinkButton).CommandArgument;
            hdfMsgID.Value = MsgID;

            string[] parameter = { "@Flag", "@MsgID" };
            string[] value = { "Delete", hdfMsgID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Directors_Message", 2, parameter, value);
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
                hdfMsgID.Value = "0";
                FillMessages();
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
        int MsgID = Convert.ToInt16(((Label)ckkIsactive.Parent.FindControl("lblMsgID")).Text);
        string Flag = "";

        if (ckkIsactive.Checked)
        {
            Flag = "Activate";
        }
        else
        {
            Flag = "Deactivate";
        }

        string[] parameter = { "@Flag", "@MsgID" };
        string[] value = { Flag, MsgID.ToString() };
        DB_Status dbs = dba.sp_populateDataSet("SP_Directors_Message", 2, parameter, value);
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
                        displayGridMessage("Message successfully activated", "info");
                    else if (status == "Deactivated")
                        displayGridMessage("Message successfully deactivated", "info");
                }
            }
        }
    }
}