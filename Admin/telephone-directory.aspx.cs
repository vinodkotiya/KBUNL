using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using OfficeOpenXml.Style;

public partial class Admin_group_data : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] != null)
        {
            hdfDept_Id.Value = "0";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Convert.ToString(Session["deprt_name"]) + " > Group Data";
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
            if (Session["GroupId"] != null)
            {
                hdfGroupId.Value = Convert.ToString(Session["GroupId"]);
                hdfGroupName.Value = Convert.ToString(Session["GroupName"]);
                lblGroupName.Text = hdfGroupName.Value;
            }
            else
                Response.Redirect("telephone-directory-group.aspx");
            panelAddNew.Visible = false;
            panelView.Visible = true;

            FillGroupsData();
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
        hdfGroupDataId.Value = "0";

        txtGroupDataEnglish.Text = "";
        txtGroupDataHindi.Text = "";
        txtIntercome.Text = "";
        txtPhone.Text = "";
        txtEmail.Text = "";
        txtAddress.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtGroupDataEnglish.Text.Trim() == "")
                    displayMessage("Please enter Name", "error");
                else if (txtGroupDataHindi.Text.Trim() == "")
                    displayMessage("Please enter Name in Hindi", "error");
                else if (txtIntercome.Text.Trim() == "")
                    displayMessage("Please enter Intercom Number", "error");
                else
                {
                    string[] parameter = { "@Flag", "@DeptID", "@GroupId", "@LocationOrNameEnglish", "@LocationOrNameHindi", "@Phone", "@Intercome", "@Email","@Address" };
                    string[] value = { "Add",hdfDept_Id.Value,hdfGroupId.Value,txtGroupDataEnglish.Text.Trim(), txtGroupDataHindi.Text.Trim(),txtPhone.Text.Trim(),txtIntercome.Text.Trim(),txtEmail.Text.Trim(),txtAddress.Text.Trim() };
                    DB_Status dbs = dba.sp_populateDataSet("SP_TelephoneDirectory", 9, parameter, value);
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
                        FillGroupsData();
                        hdfGroupDataId.Value = "0";
                        btnSave.Text = "Save";
                        txtGroupDataEnglish.Text = "";
                        txtGroupDataHindi.Text = "";
                        txtIntercome.Text = "";
                        txtPhone.Text = "";
                        txtEmail.Text = "";
                        txtAddress.Text = "";
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
                if (txtGroupDataEnglish.Text.Trim() == "")
                    displayMessage("Please enter Name", "error");
                else if (txtGroupDataHindi.Text.Trim() == "")
                    displayMessage("Please enter Name in Hindi", "error");
                else if (txtIntercome.Text.Trim() == "")
                    displayMessage("Please enter Intercom Number", "error");
                else
                {
                    string[] parameter = { "@Flag", "@GroupDataId", "@GroupId", "@LocationOrNameEnglish", "@LocationOrNameHindi", "@Phone", "@Intercome", "@Email", "@Address" };
                    string[] value = { "Update", hdfGroupDataId.Value, hdfGroupId.Value, txtGroupDataEnglish.Text.Trim(), txtGroupDataHindi.Text.Trim(), txtPhone.Text.Trim(), txtIntercome.Text.Trim(), txtEmail.Text.Trim(), txtAddress.Text.Trim() };
                    DB_Status dbs = dba.sp_populateDataSet("SP_TelephoneDirectory", 9, parameter, value);
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
                        FillGroupsData();
                        hdfGroupDataId.Value = "0";
                        txtGroupDataEnglish.Text = "";
                        txtGroupDataHindi.Text = "";
                        txtIntercome.Text = "";
                        txtPhone.Text = "";
                        txtEmail.Text = "";
                        txtAddress.Text = "";
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
    protected void FillGroupsData()
    {
        try
        {
            string[] parameter = { "@Flag", "@GroupId", "@DeptID" };
            string[] value = { "View",hdfGroupId.Value,hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_TelephoneDirectory", 3, parameter, value);
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
            string GroupDataId = (sender as LinkButton).CommandArgument;
            hdfGroupDataId.Value = GroupDataId;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@GroupDataId" };
            string[] value = { "GroupDataById", hdfGroupDataId.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_TelephoneDirectory", 2, parameter, value);
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
                        txtGroupDataEnglish.Text = Convert.ToString(dt.Rows[0]["LocationOrNameEnglish"]);
                        txtGroupDataHindi.Text = Convert.ToString(dt.Rows[0]["LocationOrNameHindi"]);
                        txtPhone.Text = Convert.ToString(dt.Rows[0]["Phone"]);
                        txtIntercome.Text = Convert.ToString(dt.Rows[0]["Intercome"]);
                        txtEmail.Text = Convert.ToString(dt.Rows[0]["Email"]);
                        txtAddress.Text = Convert.ToString(dt.Rows[0]["Address"]);
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
            string GroupDataId = (sender as LinkButton).CommandArgument;
            hdfGroupDataId.Value = GroupDataId;

            string[] parameter = { "@Flag", "@GroupDataId" };
            string[] value = { "Delete", hdfGroupDataId.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_TelephoneDirectory", 2, parameter, value);
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
                hdfGroupDataId.Value = "0";
                FillGroupsData();
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
        int SID = Convert.ToInt16(((Label)ckkIsactive.Parent.FindControl("lblGroupDataId")).Text);
        string Flag = "";

        if (ckkIsactive.Checked)
        {
            Flag = "Activate";
        }
        else
        {
            Flag = "Deactivate";
        }

        string[] parameter = { "@Flag", "@GroupDataId" };
        string[] value = { Flag, SID.ToString() };
        DB_Status dbs = dba.sp_populateDataSet("SP_TelephoneDirectory", 2, parameter, value);
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
    protected void lnkActionDetails_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string GroupDataId = (sender as LinkButton).CommandArgument;
            LinkButton lnkActionDetails = sender as LinkButton;
            GridViewRow grdro = (GridViewRow)lnkActionDetails.NamingContainer;
            HiddenField hdfGroupDataEnglishName = (HiddenField)grdro.FindControl("hdfGroupDataEnglishName");
            Session["GroupId"] = hdfGroupId.Value;
            Session["GroupName"] = hdfGroupName.Value;
            Session["GroupDataId"] = GroupDataId;
            Session["GroupDataName"] = hdfGroupDataEnglishName.Value;
            Response.Redirect("group-data-details.aspx");
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
                ws.Cells["G1"].Value = "Address";
                ws.Cells["H1"].Value = "केटेगरी";
                ws.Cells["I1"].Value = "स्थान/नाम";
                for (int i = 1; i <= totalrecords; i++)
                {
                    ws.Cells[i + 1, 1].Value = i.ToString();
                    ws.Cells[i + 1, 2].Value = tbl.Rows[i - 1]["GroupNameEnglish"].ToString();
                    ws.Cells[i + 1, 3].Value = tbl.Rows[i - 1]["LocationOrNameEnglish"].ToString();
                    ws.Cells[i + 1, 4].Value = tbl.Rows[i - 1]["Intercome"].ToString();
                    ws.Cells[i + 1, 5].Value = tbl.Rows[i - 1]["Phone"].ToString();
                    ws.Cells[i + 1, 6].Value = tbl.Rows[i - 1]["Email"].ToString();
                    ws.Cells[i + 1, 7].Value = tbl.Rows[i - 1]["Address"].ToString();
                    ws.Cells[i + 1, 8].Value = tbl.Rows[i - 1]["GroupNameHindi"].ToString();
                    ws.Cells[i + 1, 9].Value = tbl.Rows[i - 1]["LocationOrNameHindi"].ToString();
                }

                ws.Column(1).AutoFit();
                ws.Column(2).AutoFit();
                ws.Column(3).AutoFit();
                ws.Column(4).AutoFit();
                ws.Column(5).AutoFit();
                ws.Column(6).AutoFit();
                ws.Column(7).AutoFit();
                ws.Column(8).AutoFit();
                ws.Column(9).AutoFit();

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
        catch (Exception ex)
        {

        }
    }
}