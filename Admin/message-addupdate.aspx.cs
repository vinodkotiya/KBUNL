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
    Class1 mod = new Class1();
    DB_Access obj = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] != null)
        {
            hdfDept_Id.Value = "0";
            div_headTitle.InnerText = "Admin > Message";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Convert.ToString(Session["deprt_name"]) + " > Message";
            hdfDept_Id.Value = Session["DeptID"].ToString();
        }
        else
        {
            Response.Redirect("../login.aspx");
        }         
            if (!IsPostBack)
            {
                //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_redmessage");
                //menuli.Attributes["class"] = "active";
                Fill_Message();
            }
    }
    protected void Fill_Message()
    {
        try
        {
            string[] parameter = { "@Flag","@Type", "@DeptID" };
            string[] value = { "GetByType", "HOP",hdfDept_Id.Value };
            DB_Status dbs = obj.sp_populateDataSet("SP_Directors_Message", 3, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                       // pow.Visible = true;
                        btnSubmit.Visible = false;
                        btnUpdate.Visible = true;
                        txtNameEnglish.Text =Convert.ToString(dt.Rows[0]["Name"]);
                        txtNameHindi.Text = Convert.ToString(dt.Rows[0]["NameH"]);
                        txtDesignationE.Text = Convert.ToString(dt.Rows[0]["Designation"]);
                        txtDesignationH.Text = Convert.ToString(dt.Rows[0]["DesignationH"]);
                        hdfPhoto.Value = Convert.ToString(dt.Rows[0]["PhotoPath"]);
                        if (hdfPhoto.Value !="No")
                        {
                            imgProfile.ImageUrl = Convert.ToString(dt.Rows[0]["PhotoPath_Admin"]);
                            imgProfile.Visible = true;
                            RFV_ProfilePhoto.Enabled = false;
                        }
                        else
                        {
                            RFV_ProfilePhoto.Enabled = true;
                            imgProfile.Visible = false;
                        }
                        txtMsgEng.Text = Convert.ToString(dt.Rows[0]["MessageEnglish"]);
                        txtMsgHin.Text = Convert.ToString(dt.Rows[0]["MessageHindi"]);
                    }
                    else
                    {
                        txtNameEnglish.Text = "";
                        txtNameHindi.Text = "";
                        txtDesignationE.Text = "";
                        txtDesignationH.Text = "";
                        txtMsgEng.Text = "";
                        txtMsgHin.Text = "";
                        imgProfile.ImageUrl = "";
                        btnSubmit.Visible = true;
                        btnUpdate.Visible = false;
                        DisplayEventMessage("", "");
                    }
                }
                else
                {
                    txtNameEnglish.Text = "";
                    txtNameHindi.Text = "";
                    txtDesignationE.Text = "";
                    txtDesignationH.Text = "";
                    txtMsgEng.Text = "";
                    txtMsgHin.Text = "";
                    imgProfile.ImageUrl = "";
                    btnSubmit.Visible = true;
                    btnUpdate.Visible = false;
                    DisplayEventMessage("", "");
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void DisplayEventMessage(string msg, string type)
    {
        lblMsg.Text = msg;
        if (type == "error")
            lblMsg.ForeColor = System.Drawing.Color.Red;
        else if (type == "info")
            lblMsg.ForeColor = System.Drawing.Color.Green;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SaveRecord();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        SaveRecord();
    }
    protected void SaveRecord()
    {
        try
        {
            if (txtNameEnglish.Text == "")
                DisplayEventMessage("Please Enter Name", "error");
           else if (txtNameHindi.Text == "")
                DisplayEventMessage("Please Enter Name", "error");
            else if (txtDesignationE.Text == "")
                DisplayEventMessage("Please Enter Designation", "error");
            else if (txtDesignationH.Text == "")
                DisplayEventMessage("Please Enter Designation", "error");
            else if (txtMsgEng.Text == "")
                DisplayEventMessage("Please Enter Message in English", "error");
            else if (txtMsgHin.Text == "")
                DisplayEventMessage("Please Enter Message in Hindi", "error");
            else
            {
                bool flagValidImage = true;
                bool flagHasImage = false;
                string ImageFileName = "";
                string ImagePath = "NA";
                if (FileUpload1.HasFile)
                {
                    flagHasImage = true;
                    string ext = Path.GetExtension(FileUpload1.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    ImageFileName = "HOP_" + datevalue + ext;
                    ImagePath = "Uploads/MSGPhoto/" + ImageFileName;
                    hdfPhoto.Value = ImagePath;
                    if (ext == ".jpg" || ext == ".JPG" || ext == ".png" || ext == ".PNG")
                        flagValidImage = true;
                    else
                        flagValidImage = false;
                    if (flagValidImage == false)
                    {
                        DisplayEventMessage("Please browse .jpg,.JPG,.png,.PNG image", "error");
                        hdfPhoto.Value = "No";
                        return;
                    }
                }
                else
                {
                    if (hdfPhoto.Value == "No")
                    {
                        DisplayEventMessage("Please browse image", "error");
                        return;
                    }
                   
                }
                    string[] parameter = { "@Flag", "@Name", "@NameH", "@DeptID", "@Designation", "@DesignationH", "@PhotoPath", "@MessageHindi", "@MessageEnglish","@Type" };
                    string[] value = { "Update", txtNameEnglish.Text.Trim(),txtNameHindi.Text.Trim(), hdfDept_Id.Value,txtDesignationE.Text.Trim(),txtDesignationH.Text.Trim(), hdfPhoto.Value, txtMsgHin.Text.Trim(), txtMsgEng.Text.Trim(), "HOP" };
                    DB_Status dbs = obj.sp_populateDataSet("SP_Directors_Message",10, parameter, value);
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
                    if (result == "Inserted" || result == "Updated")
                    {
                        if (FileUpload1.HasFile)
                            FileUpload1.SaveAs(Server.MapPath("~/" + hdfPhoto.Value));

                        DisplayEventMessage("Successfully Saved", "info");
                    Fill_Message();
                    }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Attributes["class"] = "error";
            lblMsg.Text = ex.Message.ToString();
        }
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fill_Message();
    }
}