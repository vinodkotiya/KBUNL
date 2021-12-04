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
            div_headTitle.InnerText = "Admin > Circulars";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Convert.ToString(Session["deprt_name"]) + " > Circulars";
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
            FillDepartments();
            FillCirculars();
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

        if (hdfDept_Id.Value == "0")
        {
            ddlDepartment.Enabled = true;
            ddlDepartment.SelectedValue = "0";
        }
        divOtherSource.Visible = false;
        ddlType.SelectedValue = "Circular";
        txtTitleEnglish.Text = "";
        txtTitleHindi.Text = "";
        txtAttachmentTitle1.Text = "";
        txtAttachmentTitle2.Text = "";
        txtAttachmentTitle3.Text = "";
        txtAttachmentTitle4.Text = "";
        txtAttachmentTitle5.Text = "";
        txtLinkURL.Text = "";
        txtValidDate.Text = "";
        btnSave.Text = "Save";
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue == "9999")
        {
            divOtherSource.Visible = true;
        }
        else
        {
            divOtherSource.Visible = false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTitleEnglish.Text == "")
                displayMessage("Please enter title in english", "error");
            else if (txtTitleHindi.Text == "")
                displayMessage("Please enter title in hindi", "error");
            else if(txtAttachmentTitle1.Text.Trim()!="" && !FileUploadAttachment1.HasFile && hfAttachment1_UploadedPath.Value=="NA")
                displayMessage("Please browse file for attachment1", "error");
            else if (txtAttachmentTitle2.Text.Trim() != "" && !FileUploadAttachment2.HasFile && hfAttachment2_UploadedPath.Value == "NA")
                displayMessage("Please browse file for attachment2", "error");
            else if (txtAttachmentTitle3.Text.Trim() != "" && !FileUploadAttachment3.HasFile && hfAttachment3_UploadedPath.Value == "NA")
                displayMessage("Please browse file for attachment3", "error");
            else if (txtAttachmentTitle4.Text.Trim() != "" && !FileUploadAttachment4.HasFile && hfAttachment4_UploadedPath.Value == "NA")
                displayMessage("Please browse file for attachment4", "error");
            else if (txtAttachmentTitle5.Text.Trim() != "" && !FileUploadAttachment5.HasFile && hfAttachment5_UploadedPath.Value == "NA")
                displayMessage("Please browse file for attachment5", "error");
            else
            {
                //Check Save or Update
                string Flag = "";
                if (btnSave.Text == "Save")
                    Flag = "Add";
                else if (btnSave.Text == "Update")
                    Flag = "Update";

                string LinkURL = "NA";
                if (txtLinkURL.Text.Trim() != "")
                    LinkURL = txtLinkURL.Text.Trim();

                //Check File1 is attached or not
                string Attachment_FileName1 = "NA";
                string Attachment_FilePath1 = "NA";
                if (FileUploadAttachment1.HasFile)
                {
                    string ext = Path.GetExtension(FileUploadAttachment1.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                    Attachment_FileName1 = FileUploadAttachment1.FileName;
                    try
                    {
                        if (File.Exists(Request.PhysicalApplicationPath + Attachment_FilePath1))
                        {
                            Attachment_FileName1 = Attachment_FileName1.Replace("." + ext, "") + datevalue + ext;
                        }
                    }
                    catch (Exception)
                    {
                    }

                    Attachment_FilePath1 = "Uploads/Circulars/" + Attachment_FileName1;
                    if (!(ext.ToUpper() == ".JPG" || ext.ToUpper() == ".PNG" || ext.ToUpper() == ".JPEG" || ext.ToUpper() == ".GIF" || ext.ToUpper() == ".DOC" || ext.ToUpper() == ".DOCX" || ext.ToUpper() == ".PDF" || ext.ToUpper() == ".XLS" || ext.ToUpper() == ".XLSX" || ext.ToUpper() == ".PPTX" || ext.ToUpper() == ".PPT" || ext.ToUpper() == ".MP4" || ext.ToUpper() == ".AVI" || ext.ToUpper() == ".MOV"))
                    {
                        displayMessage("Please attach valid file for attachment 1", "error");
                        return;
                    }
                    hfAttachment1_UploadedPath.Value = Attachment_FilePath1;
                }

                //Check File2 is attached or not
                string Attachment_FileName2 = "NA";
                string Attachment_FilePath2 = "NA";
                if (FileUploadAttachment2.HasFile)
                {
                    string ext = Path.GetExtension(FileUploadAttachment2.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                    Attachment_FileName2 = FileUploadAttachment2.FileName;
                    try
                    {
                        if (File.Exists(Request.PhysicalApplicationPath + Attachment_FilePath2))
                        {
                            Attachment_FileName2 = Attachment_FileName2.Replace("." + ext, "") + datevalue + ext;
                        }
                    }
                    catch (Exception)
                    {
                    }

                    Attachment_FilePath2 = "Uploads/Circulars/" + Attachment_FileName2;
                    if (!(ext.ToUpper() == ".JPG" || ext.ToUpper() == ".PNG" || ext.ToUpper() == ".JPEG" || ext.ToUpper() == ".GIF" || ext.ToUpper() == ".DOC" || ext.ToUpper() == ".DOCX" || ext.ToUpper() == ".PDF" || ext.ToUpper() == ".XLS" || ext.ToUpper() == ".XLSX" || ext.ToUpper() == ".PPTX" || ext.ToUpper() == ".PPT" || ext.ToUpper() == ".MP4" || ext.ToUpper() == ".AVI" || ext.ToUpper() == ".MOV"))
                    {
                        displayMessage("Please attach valid file for attachment 2", "error");
                        return;
                    }

                    hfAttachment2_UploadedPath.Value = Attachment_FilePath2;
                }

                //Check File3 is attached or not
                string Attachment_FileName3 = "NA";
                string Attachment_FilePath3 = "NA";
                if (FileUploadAttachment3.HasFile)
                {
                    string ext = Path.GetExtension(FileUploadAttachment3.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                    Attachment_FileName3 = FileUploadAttachment3.FileName;
                    try
                    {
                        if (File.Exists(Request.PhysicalApplicationPath + Attachment_FilePath3))
                        {
                            Attachment_FileName3 = Attachment_FileName3.Replace("." + ext, "") + datevalue + ext;
                        }
                    }
                    catch (Exception)
                    {
                    }

                    Attachment_FilePath3 = "Uploads/Circulars/" + Attachment_FileName3;
                    if (!(ext.ToUpper() == ".JPG" || ext.ToUpper() == ".PNG" || ext.ToUpper() == ".JPEG" || ext.ToUpper() == ".GIF" || ext.ToUpper() == ".DOC" || ext.ToUpper() == ".DOCX" || ext.ToUpper() == ".PDF" || ext.ToUpper() == ".XLS" || ext.ToUpper() == ".XLSX" || ext.ToUpper() == ".PPTX" || ext.ToUpper() == ".PPT" || ext.ToUpper() == ".MP4" || ext.ToUpper() == ".AVI" || ext.ToUpper() == ".MOV"))
                    {
                        displayMessage("Please attach valid file for attachment 3", "error");
                        return;
                    }

                    hfAttachment3_UploadedPath.Value = Attachment_FilePath3;
                }

                //Check File4 is attached or not
                string Attachment_FileName4 = "NA";
                string Attachment_FilePath4 = "NA";
                if (FileUploadAttachment4.HasFile)
                {
                    string ext = Path.GetExtension(FileUploadAttachment4.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                    Attachment_FileName4 = FileUploadAttachment4.FileName;
                    try
                    {
                        if (File.Exists(Request.PhysicalApplicationPath + Attachment_FilePath4))
                        {
                            Attachment_FileName4 = Attachment_FileName4.Replace("." + ext, "") + datevalue + ext;
                        }
                    }
                    catch (Exception)
                    {
                    }

                    Attachment_FilePath4 = "Uploads/Circulars/" + Attachment_FileName4;
                    if (!(ext.ToUpper() == ".JPG" || ext.ToUpper() == ".PNG" || ext.ToUpper() == ".JPEG" || ext.ToUpper() == ".GIF" || ext.ToUpper() == ".DOC" || ext.ToUpper() == ".DOCX" || ext.ToUpper() == ".PDF" || ext.ToUpper() == ".XLS" || ext.ToUpper() == ".XLSX" || ext.ToUpper() == ".PPTX" || ext.ToUpper() == ".PPT" || ext.ToUpper() == ".MP4" || ext.ToUpper() == ".AVI" || ext.ToUpper() == ".MOV"))
                    {
                        displayMessage("Please attach valid file for attachment 4", "error");
                        return;
                    }

                    hfAttachment4_UploadedPath.Value = Attachment_FilePath4;
                }

                //Check File5 is attached or not
                string Attachment_FileName5 = "NA";
                string Attachment_FilePath5 = "NA";
                if (FileUploadAttachment5.HasFile)
                {
                    string ext = Path.GetExtension(FileUploadAttachment5.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
                    Attachment_FileName5 = FileUploadAttachment5.FileName;
                    try
                    {
                        if (File.Exists(Request.PhysicalApplicationPath + Attachment_FilePath5))
                        {
                            Attachment_FileName5 = Attachment_FileName5.Replace("." + ext, "") + datevalue + ext;
                        }
                    }
                    catch (Exception)
                    {
                    }

                    Attachment_FilePath5 = "Uploads/Circulars/" + Attachment_FileName5;
                    if (!(ext.ToUpper() == ".JPG" || ext.ToUpper() == ".PNG" || ext.ToUpper() == ".JPEG" || ext.ToUpper() == ".GIF" || ext.ToUpper() == ".DOC" || ext.ToUpper() == ".DOCX" || ext.ToUpper() == ".PDF" || ext.ToUpper() == ".XLS" || ext.ToUpper() == ".XLSX" || ext.ToUpper() == ".PPTX" || ext.ToUpper() == ".PPT" || ext.ToUpper() == ".MP4" || ext.ToUpper() == ".AVI" || ext.ToUpper() == ".MOV"))
                    {
                        displayMessage("Please attach valid file for attachment 5", "error");
                        return;
                    }

                    hfAttachment5_UploadedPath.Value = Attachment_FilePath5;
                }

                //DeptID 0 for Admin
                string[] param = { "@Flag", "@RID", "@DeptID", "@Type", "@TitleEnglish", "@TitleHindi", "@DescriptionEnglish", "@DescriptionHindi", "@ValidDate", "@DocLinkType", "@AttachmentTitle1", "@AttachmentTitle2", "@AttachmentTitle3", "@AttachmentTitle4", "@AttachmentTitle5", "@AttachmentURL1", "@AttachmentURL2", "@AttachmentURL3", "@AttachmentURL4", "@AttachmentURL5", "@LinkURL", "@PostedByOtherSource", "@IsNew" };
                string[] value = { Flag, hfRID.Value, ddlDepartment.SelectedValue, ddlType.SelectedValue, txtTitleEnglish.Text.Trim(), txtTitleHindi.Text.Trim(), "", "", obj.makedate(txtValidDate.Text), ddlLinkType.SelectedValue, txtAttachmentTitle1.Text.Trim(), txtAttachmentTitle2.Text.Trim(), txtAttachmentTitle3.Text.Trim(), txtAttachmentTitle4.Text.Trim(), txtAttachmentTitle5.Text.Trim(), hfAttachment1_UploadedPath.Value, hfAttachment2_UploadedPath.Value, hfAttachment3_UploadedPath.Value, hfAttachment4_UploadedPath.Value, hfAttachment5_UploadedPath.Value, LinkURL, txtOthers.Text.Trim(), ddlNew.SelectedValue };
                DB_Status dbs = dba.sp_populateDataSet("SP_Circulars", 23, param, value);
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
                else if (dbs.OperationStatus.ToString() == "Error")
                {
                    displayMessage(dbs.Title, "error");
                }

                if (result == "Inserted")
                {
                    if (Attachment_FileName1 != "NA")
                        FileUploadAttachment1.SaveAs(Server.MapPath("~/" + Attachment_FilePath1));
                    if (Attachment_FileName2 != "NA")
                        FileUploadAttachment2.SaveAs(Server.MapPath("~/" + Attachment_FilePath2));
                    if (Attachment_FileName3 != "NA")
                        FileUploadAttachment3.SaveAs(Server.MapPath("~/" + Attachment_FilePath3));
                    if (Attachment_FileName4 != "NA")
                        FileUploadAttachment4.SaveAs(Server.MapPath("~/" + Attachment_FilePath4));
                    if (Attachment_FileName5 != "NA")
                        FileUploadAttachment5.SaveAs(Server.MapPath("~/" + Attachment_FilePath5));
                    hfRID.Value = "";
                    ddlNew.SelectedIndex = 0;
                    btnSave.Text = "Save";
                    panelAddNew.Visible = false;
                    panelView.Visible = true;
                    FillCirculars();
                    txtTitleEnglish.Text = "";
                    txtLinkURL.Text = "";
                    txtValidDate.Text = "";
                    displayMessage("Circular Successfully Added", "info");
                }
                else if (result == "Updated")
                {
                    if (Attachment_FileName1 != "NA")
                    {
                        try
                        {
                            File.Delete(Request.PhysicalApplicationPath + Attachment_FilePath1);
                        }
                        catch (Exception)
                        {
                        }
                        FileUploadAttachment1.SaveAs(Server.MapPath("~/" + Attachment_FilePath1));
                    }
                    if (Attachment_FileName2 != "NA")
                    {
                        try
                        {
                            File.Delete(Request.PhysicalApplicationPath + Attachment_FilePath2);
                        }
                        catch (Exception)
                        {
                        }
                        FileUploadAttachment2.SaveAs(Server.MapPath("~/" + Attachment_FilePath2));
                    }
                    if (Attachment_FileName3 != "NA")
                    {
                        try
                        {
                            File.Delete(Request.PhysicalApplicationPath + Attachment_FilePath3);
                        }
                        catch (Exception)
                        {
                        }
                        FileUploadAttachment3.SaveAs(Server.MapPath("~/" + Attachment_FilePath3));
                    }
                    if (Attachment_FileName4 != "NA")
                    {
                        try
                        {
                            File.Delete(Request.PhysicalApplicationPath + Attachment_FilePath4);
                        }
                        catch (Exception)
                        {
                        }
                        FileUploadAttachment4.SaveAs(Server.MapPath("~/" + Attachment_FilePath4));
                    }
                    if (Attachment_FileName5 != "NA")
                    {
                        try
                        {
                            File.Delete(Request.PhysicalApplicationPath + Attachment_FilePath5);
                        }
                        catch (Exception)
                        {
                        }
                        FileUploadAttachment5.SaveAs(Server.MapPath("~/" + Attachment_FilePath5));
                    }
                    hfRID.Value = "";
                    ddlNew.SelectedIndex = 0;
                    btnSave.Text = "Save";
                    panelAddNew.Visible = false;
                    panelView.Visible = true;
                    FillCirculars();
                    txtTitleEnglish.Text = "";
                    txtTitleHindi.Text = "";
                    txtLinkURL.Text = "";
                    txtValidDate.Text = "";
                    displayMessage("Circular Successfully Updated", "info");
                }
                else if (result == "AlreadyExists")
                {
                    displayMessage("Sorry! Circular Already Exists", "error");
                }
                else if (result == "Fail")
                {
                    displayMessage("Server Error", "error");
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
    protected void FillDepartments()
    {
        try
        {
            string[] parameter = { "@Flag"};
            string[] value = { "LoadDepartments"};
            DB_Status dbs = dba.sp_populateDataSet("SP_Circulars", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    ddlDepartment.DataSource = dt;
                    ddlDepartment.DataValueField = "DeptID";
                    ddlDepartment.DataTextField = "Department";
                    ddlDepartment.DataBind();
                }
            }
            if(hdfDept_Id.Value=="0")
            {
                ddlDepartment.Enabled = true;
                ddlDepartment.SelectedValue = "0";
            }
            else
            {
                ddlDepartment.SelectedValue = hdfDept_Id.Value;
                ddlDepartment.Enabled = false;                
            }
        }
        catch (Exception)
        {
        }
    }
    protected void FillCirculars()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "Load",hdfDept_Id.Value};
            DB_Status dbs = dba.sp_populateDataSet("SP_Circulars", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    grdCirculars.DataSource = dt;
                    grdCirculars.DataBind();
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
            string[] value = { "Load_byID", hfRID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Circulars", 2, parameter, value);
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
                        ddlDepartment.SelectedValue = dt.Rows[0]["DeptID"].ToString();
                        ddlType.SelectedValue = dt.Rows[0]["Type"].ToString();
                        txtOthers.Text = dt.Rows[0]["PostedByOtherSource"].ToString();
                        if (ddlDepartment.SelectedValue == "9999")
                        {
                            divOtherSource.Visible = true;
                        }
                        else
                        {
                            divOtherSource.Visible = false;
                        }
                        txtTitleEnglish.Text =Convert.ToString(dt.Rows[0]["TitleEnglish"]);
                        txtTitleHindi.Text = Convert.ToString(dt.Rows[0]["TitleHindi"]);
                        ddlLinkType.SelectedValue = dt.Rows[0]["DocLinkType"].ToString();
                        txtLinkURL.Text = Convert.ToString(dt.Rows[0]["LinkURL"]);
                        txtValidDate.Text = Convert.ToString(dt.Rows[0]["ValidDate"]);
                        txtAttachmentTitle1.Text = Convert.ToString(dt.Rows[0]["AttachmentTitle1"]);
                        txtAttachmentTitle2.Text = Convert.ToString(dt.Rows[0]["AttachmentTitle2"]);
                        txtAttachmentTitle3.Text = Convert.ToString(dt.Rows[0]["AttachmentTitle3"]);
                        txtAttachmentTitle4.Text = Convert.ToString(dt.Rows[0]["AttachmentTitle4"]);
                        txtAttachmentTitle5.Text = Convert.ToString(dt.Rows[0]["AttachmentTitle5"]);
                        hfAttachment1_UploadedPath.Value = Convert.ToString(dt.Rows[0]["AttachmentURL1"]);
                        hfAttachment2_UploadedPath.Value = Convert.ToString(dt.Rows[0]["AttachmentURL2"]);
                        hfAttachment3_UploadedPath.Value = Convert.ToString(dt.Rows[0]["AttachmentURL3"]);
                        hfAttachment4_UploadedPath.Value = Convert.ToString(dt.Rows[0]["AttachmentURL4"]);
                        hfAttachment5_UploadedPath.Value = Convert.ToString(dt.Rows[0]["AttachmentURL5"]);

                        ddlNew.SelectedValue = Convert.ToString(dt.Rows[0]["IsNew"]);
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
            string RID = (sender as LinkButton).CommandArgument;
            hfRID.Value = RID;

            string[] parameter = { "@Flag", "@RID" };
            string[] value = { "Delete", hfRID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Circulars", 2, parameter, value);
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
                FillCirculars();
            }
        }
        catch (Exception)
        {
        }
    }
    protected void grdCirculars_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCirculars.PageIndex = e.NewPageIndex;
        FillCirculars();
    }
    protected void ddlLinkType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLinkType.SelectedValue == "Attachment")
        {
            divAttachment.Visible = true;
            divURL.Visible = false;
        }
        else if (ddlLinkType.SelectedValue == "Link")
        {
            divAttachment.Visible = false;
            divURL.Visible = true;
        }
    }
}