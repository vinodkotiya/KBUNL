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
        if (Session["AdminUserID"] == null)
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {
            System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_policy");
            menuli.Attributes["class"] = "active";

            panelAddNew.Visible = false;
            panelView.Visible = true;

            FillPolicy();
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
        hfRID_Policy.Value = "";
        txtPolicyName.Text = "";
        txtLink.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtPolicyName.Text.Trim() == "")
                    displayMessage("Please enter Policy Name", "error");
                else if (txtLink.Text.Trim() == "" && FileUploaderAttch.HasFile == false)
                    displayMessage("Please Choose one link or attachment file", "error");
                else if (txtLink.Text.Trim() != "" && FileUploaderAttch.HasFile == true)
                {
                    displayMessage("Please Choose one link or attachment file", "error");
                }
                else
                {
                    bool flagValidFile = true;
                    bool flagHasFile = true;
                    string Attachment_FileName = "NA";
                    string Attachment_FilePath = "NA";
                    if (FileUploaderAttch.HasFile)
                    {
                        string ext = Path.GetExtension(FileUploaderAttch.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        Attachment_FileName = "Policy_" + datevalue + ext;
                        Attachment_FilePath = "Uploads/Policy/" + Attachment_FileName;
                        if (!(ext.ToUpper() == ".JPG" || ext.ToUpper() == ".PNG" || ext.ToUpper() == ".DOC" || ext.ToUpper() == ".DOCX" || ext.ToUpper() == ".PDF" || ext.ToUpper() == ".XLS" || ext.ToUpper() == ".XLSX"))
                        {
                            flagValidFile = false;
                            displayMessage("Please attach valid file", "error");
                        }
                        else
                            flagValidFile = true;
                    }
                    if (flagHasFile && flagValidFile)
                    {
                        string[] parameter = { "@Flag", "@PolicyName", "@PolicyNameH","@PolicyLink", "@PolicyAttachment" };
                        string[] value = { "insert", txtPolicyName.Text.Trim(), txtPolicyNameH.Text.Trim(), txtLink.Text.Trim(), Attachment_FilePath };
                        DB_Status dbs = dba.sp_populateDataSet("Sp_Policy", 5, parameter, value);
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
                            FileUploaderAttch.SaveAs(Server.MapPath("~/" + Attachment_FilePath));
                            FillPolicy();
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;
                        }
                        else if (result == "exist")
                        {
                            displayMessage("Already Policy Name Exist", "error");
                        }
                    }
                }

            }

            else if (btnSave.Text == "Update")
            {
                if (txtPolicyName.Text.Trim() == "")
                    displayMessage("Please enter Policy Name", "error");
                else if (hfAttachment.Value == "NA" && txtLink.Text == "" && FileUploaderAttch.HasFile==false)
                    displayMessage("Please Create Link (http:// or https://)", "error");
                else if (txtLink.Text != "" && FileUploaderAttch.HasFile == true)
                {
                    displayMessage("Please Choose one link or attachment file", "error");
                }

                else
                {
                    if (txtLink.Text != "")
                    {
                        hfAttachment.Value = "NA";
                    }
                    bool flagValidFile = true;
                    bool flagHasFile = true;
                    string Attachment_FileName = "NA";
                    string Attachment_FilePath = hfAttachment.Value;
                    if (FileUploaderAttch.HasFile)
                    {
                        string ext = Path.GetExtension(FileUploaderAttch.FileName);
                        string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        Attachment_FileName = "Policy_" + datevalue + ext;
                        Attachment_FilePath = "Uploads/Policy/" + Attachment_FileName;
                        if (!(ext.ToUpper() == ".JPG" || ext.ToUpper() == ".PNG" || ext.ToUpper() == ".DOC" || ext.ToUpper() == ".DOCX" || ext.ToUpper() == ".PDF" || ext.ToUpper() == ".XLS" || ext.ToUpper() == ".XLSX"))
                        {
                            flagValidFile = false;
                            displayMessage("Please attach valid file", "error");
                        }
                        else
                            flagValidFile = true;
                    }
                    if (flagHasFile && flagValidFile)
                    {
                        string[] parameter = { "@Flag", "@RID", "@PolicyName", "@PolicyNameH", "@PolicyLink", "@PolicyAttachment" };
                        string[] value = { "Update", hfRID_Policy.Value, txtPolicyName.Text.Trim(), txtPolicyNameH.Text.Trim(), txtLink.Text.Trim(), Attachment_FilePath };
                        DB_Status dbs = dba.sp_populateDataSet("Sp_Policy", 6, parameter, value);
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
                            if (Attachment_FileName != "NA" && flagHasFile && flagValidFile)
                            {
                                try
                                {
                                    File.Delete(Request.PhysicalApplicationPath + hfAttachment.Value);
                                }
                                catch (Exception)
                                {
                                }
                                FileUploaderAttch.SaveAs(Server.MapPath("~/" + Attachment_FilePath));
                            }
                            FillPolicy();
                            hfRID_Policy.Value = "";
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;
                        }
                        else if (result == "exist")
                        {
                            displayMessage("Already Policy Name Exist", "error");
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
        hfRID_Policy.Value = "";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void FillPolicy()
    {
        try
        {
            string[] parameter = { "@Flag" };
            string[] value = { "View" };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Policy", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    gridPolicy.DataSource = dt;
                    gridPolicy.DataBind();
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
            hfRID_Policy.Value = RID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@RID" };
            string[] value = { "ViewByID", hfRID_Policy.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Policy", 2, parameter, value);
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

                        txtLink.Text = dt.Rows[0]["link"].ToString();
                        hfAttachment.Value = dt.Rows[0]["attachment"].ToString();
                        txtPolicyName.Text = dt.Rows[0]["PolicyName"].ToString();
                        txtPolicyNameH.Text = dt.Rows[0]["PolicyNameH"].ToString();

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
            hfRID_Policy.Value = RID;

            string[] parameter = { "@Flag", "@RID" };
            string[] value = { "Delete", hfRID_Policy.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Policy", 2, parameter, value);
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
                hfRID_Policy.Value = "";
                FillPolicy();
            }
        }
        catch (Exception)
        {
        }
    }


    protected void lnkAcvtiveStatus_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");

        string RID = (sender as LinkButton).CommandArgument;
        hfRID_Policy.Value = RID;

        LinkButton lnkbtn_edit = (LinkButton)sender;
        Label lbl_status = (Label)lnkbtn_edit.Parent.FindControl("lblActiveStatus");

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
        string[] parameter = { "@Flag", "@RID" };
        string[] value = { Flag, hfRID_Policy.Value };
        DB_Status dbs = dba.sp_populateDataSet("Sp_Policy", 2, parameter, value);
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
                hfRID_Policy.Value = "";
                FillPolicy();
            }
        }
    }

    protected void gridPolicy_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridPolicy.PageIndex = e.NewPageIndex;
        FillPolicy();
    }
}