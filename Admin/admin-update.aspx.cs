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
        if (Session["AdminUserID"] == null)
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {
            System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_updates");
            menuli.Attributes["class"] = "active";

            panelAddNew.Visible = false;
            panelView.Visible = true;

          
            FillUpdates();
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
        txtTitle.Text = "";
        txtDesc.Text = "";
        divShowImage.Src = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTitle.Text == "")
                displayMessage("Please enter Title", "error");            
            else
            {
                

                bool flagValidFile = false;
                bool flagHasFile = false;
                string Attachment_FileName = "NA";
                string Attachment_FilePath = "NA";

                //Check Save or Update
                string Flag = "";
                if (btnSave.Text == "Save")
                {
                    Flag = "Add";
                    if (FileUploader1.HasFile)
                    {
                        flagHasFile = true;
                    }
                    else
                    {
                        displayMessage("Please attach Image", "error");
                    }
                }

                else if (btnSave.Text == "Update")
                {
                    flagHasFile = true;
                    flagValidFile = true;
                    Flag = "Update";
                }
                    
                //Check File is attached or not
                if (FileUploader1.HasFile)
                {
                    string ext = Path.GetExtension(FileUploader1.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    Attachment_FileName = "AdminUpdate_" + datevalue + ext;
                    Attachment_FilePath = "Uploads/AdminUpdate/" + Attachment_FileName;
                    if (!(ext.ToUpper() == ".JPG" || ext.ToUpper() == ".PNG"|| ext.ToUpper() == ".JPEG" || ext.ToUpper() == ".GIF"))
                    {
                        flagValidFile = false;
                        displayMessage("Please attach valid Image", "error");
                    }
                    else
                        flagValidFile = true;
                }

                


                if (flagHasFile && flagValidFile)
                {
                   
                    string[] param = { "@Flag", "@RID", "@Title", "@Description", "@ImagePath"};
                    string[] value = { Flag, hfRID.Value, txtTitle.Text.Trim(), txtDesc.Text.Trim(), Attachment_FilePath};
                    DB_Status dbs = dba.sp_populateDataSet("SP_Admin_Updates", 5, param, value);
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
                        if (Attachment_FileName != "NA" && flagHasFile && flagValidFile)
                            FileUploader1.SaveAs(Server.MapPath("~/" + Attachment_FilePath));
                        hfRID.Value = "";
                        btnSave.Text = "Save";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
                        FillUpdates();
                        txtTitle.Text = "";
                        
                        txtDesc.Text = "";
                        displayMessage("Record Successfully Added", "info");
                    }
                    else if (result == "Updated")
                    {
                        if (Attachment_FileName != "NA" && flagHasFile && flagValidFile)
                        {
                            try
                            {
                                File.Delete(Request.PhysicalApplicationPath + hfAttachment_UploadedPath.Value);
                            }
                            catch (Exception)
                            {
                            }
                            FileUploader1.SaveAs(Server.MapPath("~/" + Attachment_FilePath));
                        }
                        hfRID.Value = "";
                        btnSave.Text = "Save";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
                        FillUpdates();
                        txtTitle.Text = "";
                       
                        txtDesc.Text = "";
                        displayMessage("Record Successfully Updated", "info");
                    }
                    else if (result == "TitleExist")
                    {
                        displayMessage("Sorry! Title Already Exists", "error");
                    }
                    else if (result == "ImageExist")
                    {
                        displayMessage("Sorry! Image Already Exists", "error");
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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hfRID.Value = "";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void FillUpdates()
    {
        try
        {
            string[] parameter = { "@Flag" };
            string[] value = {"View"};
            DB_Status dbs = dba.sp_populateDataSet("SP_Admin_Updates", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    grdUpdates.DataSource = dt;
                    grdUpdates.DataBind();
                }
            }
        }
        catch (Exception ex)
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

            string[] parameter = {"@Flag", "@RID" };
            string[] value = { "ViewByRID",  hfRID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Admin_Updates", 2, parameter, value);
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
                        txtTitle.Text = dt.Rows[0]["Title"].ToString();
                        txtDesc.Text = dt.Rows[0]["Description"].ToString();
                        hfAttachment_UploadedPath.Value = dt.Rows[0]["ImagePath"].ToString();
                        divShowImage.Src="../"+ dt.Rows[0]["ImagePath"].ToString();
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

            string[] parameter = {"@Flag", "@RID" };
            string[] value = {"Delete", hfRID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Admin_Updates", 2, parameter, value);
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
                hfRID.Value = "";
                FillUpdates();
            }
        }
        catch (Exception)
        {
        }
    }
    protected void grdUpdates_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdUpdates.PageIndex = e.NewPageIndex;
        FillUpdates();
    }
}