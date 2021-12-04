using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using OfficeOpenXml.Style;

public partial class Admin_group : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] != null)
        {
            hdfDept_Id.Value = "0";
            div_headTitle.InnerText = "Admin > Group";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Convert.ToString(Session["deprt_name"]) + " > Group";
            hdfDept_Id.Value = Session["DeptID"].ToString();
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

            FillGroups();
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
        hdfGroupId.Value = "0";

        txtGroupEnglish.Text = "";
        txtGroupHindi.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtGroupEnglish.Text.Trim() == "")
                    displayMessage("Please enter group name", "error");
                else if (txtGroupHindi.Text.Trim() == "")
                    displayMessage("Please enter group name", "error");
                else if (txtSequenceNo.Text.Trim() == "")
                    displayMessage("Please enter sequence no", "error");
                else
                {
                        string[] parameter = { "@Flag", "@GroupNameEnglish", "@GroupNameHindi","@Sequence"};
                        string[] value = { "Add",txtGroupEnglish.Text.Trim(), txtGroupHindi.Text.Trim(),txtSequenceNo.Text.Trim() };
                        DB_Status dbs = dba.sp_populateDataSet("SP_TelephoneDirectory_Group", 4, parameter, value);
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
                            displayMessage("Record successfully added", "info");
                            FillGroups();
                            hdfGroupId.Value = "0";
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;
                        }
                        else if (result == "exits")
                        {
                            displayMessage("Record already exists", "error");
                        }
                    }

                }
            else if (btnSave.Text == "Update")
            {
                if (txtGroupEnglish.Text.Trim() == "")
                    displayMessage("Please enter group name", "error");
                else if (txtGroupHindi.Text.Trim() == "")
                    displayMessage("Please enter group name", "error");
                else
                {
                    string[] parameter = { "@Flag", "@GroupId","@GroupNameEnglish", "@GroupNameHindi", "@Sequence" };
                    string[] value = { "Update", hdfGroupId.Value,txtGroupEnglish.Text.Trim(), txtGroupHindi.Text.Trim(), txtSequenceNo.Text.Trim() };
                    DB_Status dbs = dba.sp_populateDataSet("SP_TelephoneDirectory_Group", 5, parameter, value);
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
                        displayMessage("Record successfully added", "info");
                        FillGroups();
                        hdfGroupId.Value = "0";
                        btnSave.Text = "Save";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
                    }
                    else if (result == "exits")
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
        displayGridMessage("", "");
        hdfGroupId.Value = "0";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void FillGroups()
    {
        try
        {
            string[] parameter = { "@Flag"};
            string[] value = { "View"};
            DB_Status dbs = dba.sp_populateDataSet("SP_TelephoneDirectory_Group", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    grdGroup.DataSource = dt;
                    grdGroup.DataBind();
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
            hdfGroupId.Value = QuestionID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@GroupId" };
            string[] value = { "GroupById", hdfGroupId.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_TelephoneDirectory_Group", 2, parameter, value);
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
                        txtGroupEnglish.Text =Convert.ToString(dt.Rows[0]["GroupNameEnglish"]);
                        txtGroupHindi.Text = Convert.ToString(dt.Rows[0]["GroupNameHindi"]);
                        txtSequenceNo.Text = Convert.ToString(dt.Rows[0]["Sequence"]);
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
            string GroupId = (sender as LinkButton).CommandArgument;
            hdfGroupId.Value = GroupId;

            string[] parameter = { "@Flag", "@GroupId" };
            string[] value = { "Delete", hdfGroupId.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_TelephoneDirectory_Group", 2, parameter, value);
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
                hdfGroupId.Value = "0";
                FillGroups();
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
        int SID = Convert.ToInt16(((Label)ckkIsactive.Parent.FindControl("lblGroupId")).Text);
        string Flag = "";

        if (ckkIsactive.Checked)
        {
            Flag = "Activate";
        }
        else
        {
            Flag = "Deactivate";
        }

        string[] parameter = { "@Flag", "@GroupId" };
        string[] value = { Flag, SID.ToString() };
        DB_Status dbs = dba.sp_populateDataSet("SP_TelephoneDirectory_Group", 2, parameter, value);
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
                        displayGridMessage("Group successfully activated", "info");
                    else if (status == "Deactivated")
                        displayGridMessage("Group successfully deactivated", "info");
                }
            }
        }
    }
    protected void lnkActionData_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string GroupId = (sender as LinkButton).CommandArgument;
            LinkButton lnkActionData = sender as LinkButton;
            GridViewRow grdro = (GridViewRow)lnkActionData.NamingContainer;
            HiddenField hdfGroupName = (HiddenField)grdro.FindControl("hdfGroupName");
            Session["GroupId"] = GroupId;
            Session["GroupName"] = hdfGroupName.Value;
            Response.Redirect("telephone-directory.aspx");
        }
        catch (Exception ex)
        {

        }
    }


    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string SheetName = "ImportantDirectory";
            string strFileName = "ImportantDirectory.xlsx";
            int totalrecords = 0;
            DataTable tbl = null;
            string[] parameter = { };
            string[] value = { };
            DB_Status dbs = dba.sp_populateDataSet("SP_Export_Important_Directory", 0, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    tbl = ds.Tables[0];
                }
            }
            totalrecords = tbl.Rows.Count;
            if (totalrecords > 0)
            {
                //xcptn code
                ExcelPackage pck = new ExcelPackage();
                //*****************************************************************************************************************
                //First worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(SheetName);

                //[Common Settings] Load Filters in Cells
                ws.Cells["A1"].Value = "SrNo";
                ws.Cells["B1"].Value = "Category";
                ws.Cells["C1"].Value = "Location/Name";
                ws.Cells["D1"].Value = "Intercom";
                ws.Cells["E1"].Value = "Phone";
                ws.Cells["F1"].Value = "Email";
                ws.Cells["G1"].Value = "केटेगरी";
                ws.Cells["H1"].Value = "स्थान/नाम";
                for (int i = 1; i <= totalrecords; i++)
                {
                    ws.Cells[i + 1, 1].Value = i.ToString();
                    ws.Cells[i + 1, 2].Value = tbl.Rows[i - 1]["GroupNameEnglish"].ToString();
                    ws.Cells[i + 1, 3].Value = tbl.Rows[i - 1]["LocationOrNameEnglish"].ToString();
                    ws.Cells[i + 1, 4].Value = tbl.Rows[i - 1]["Intercome"].ToString();
                    ws.Cells[i + 1, 5].Value = tbl.Rows[i - 1]["Phone"].ToString();
                    ws.Cells[i + 1, 6].Value = tbl.Rows[i - 1]["Email"].ToString();
                    ws.Cells[i + 1, 7].Value = tbl.Rows[i - 1]["GroupNameHindi"].ToString();
                    ws.Cells[i + 1, 8].Value = tbl.Rows[i - 1]["LocationOrNameHindi"].ToString();
                }

                ws.Column(1).AutoFit();
                ws.Column(2).AutoFit();
                ws.Column(3).AutoFit();
                ws.Column(4).AutoFit();
                ws.Column(5).AutoFit();
                ws.Column(6).AutoFit();
                ws.Column(7).AutoFit();
                ws.Column(8).AutoFit();

                //Write it back to the client
                try
                {
                    displayMessage(" ", "info");
                    Response.Clear();
                    Response.ClearHeaders();

                    Response.AppendHeader("content-disposition", "attachment;  Filename=" + strFileName + "");
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.BinaryWrite(pck.GetAsByteArray());
                    Response.End();

                    //string path = Server.MapPath(".") + "/" + strFileName;
                    //Stream stream = File.Create(path);
                    //pck.SaveAs(stream);
                    //stream.Close();
                    displayGridMessage("Directory Successfully Downloaded", "info");
                }
                catch (Exception ex)
                {
                    displayGridMessage(ex.Message, "error");
                }
            }
            else
            {
                displayGridMessage("Sorry! No record present", "error");
            }
        }
        catch (Exception)
        {

        }
    }
}