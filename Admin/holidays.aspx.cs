using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_holidays : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] != null)
        {
            hdfDept_Id.Value = "0";
            div_headTitle.InnerText = "Admin > Holiday";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Convert.ToString(Session["deprt_name"]) + " > Holiday";
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

            FillHolidays();
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
        hdfHoliday_Id.Value = "";
        ddlHolidayType.SelectedValue = "0";
        txtHolidayName.Text = "";
        txtHolidayNameHindi.Text = "";
        txtHolidayDate.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (ddlHolidayType.SelectedValue == "0")
                    displayMessage("Please select holiday type", "error");
                else if (txtHolidayName.Text.Trim() == "")
                    displayMessage("Please enter holiday name", "error");
                else if (txtHolidayDate.Text.Trim()=="")
                    displayMessage("Please select holiday date", "error");
                else
                {
                        string[] parameter = { "@Flag", "@DeptID","@HolidayType", "@HolidayNameEnglish", "@HolidayNameHindi", "@HolidayDate"};
                        string[] value = { "Add",hdfDept_Id.Value,ddlHolidayType.SelectedValue,txtHolidayName.Text.Trim(),txtHolidayNameHindi.Text.Trim(), obj.makedate(txtHolidayDate.Text.Trim())};
                        DB_Status dbs = dba.sp_populateDataSet("SP_Holidays",6, parameter, value);
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
                            displayMessage("Record successfully added", "info");
                            FillHolidays();
                            hdfHoliday_Id.Value = "0";
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
                if (txtHolidayName.Text.Trim() == "")
                    displayMessage("Please enter holiday name", "error");
                else if (txtHolidayDate.Text.Trim() == "")
                    displayMessage("Please select holiday date", "error");
                else
                {
                    string[] parameter = { "@Flag", "@HolidayId", "@HolidayType", "@HolidayNameEnglish", "@HolidayNameHindi", "@HolidayDate" };
                    string[] value = { "Update", hdfHoliday_Id.Value, ddlHolidayType.SelectedValue, txtHolidayName.Text.Trim(), txtHolidayNameHindi.Text.Trim(), obj.makedate(txtHolidayDate.Text.Trim()) };
                    DB_Status dbs = dba.sp_populateDataSet("SP_Holidays", 6, parameter, value);
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
                        displayMessage("Record successfully updated", "info");
                        FillHolidays();
                        hdfHoliday_Id.Value = "0";
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
        catch (Exception ex)
        {
            displayMessage(ex.Message, "error");
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hdfHoliday_Id.Value = "0";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void FillHolidays()
    {
        try
        {
            string[] parameter = { "@Flag" , "@DeptID" };
            string[] value = { "LoadForAdmin",hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Holidays",2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    grdHoliday.DataSource = dt;
                    grdHoliday.DataBind();
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
            hdfHoliday_Id.Value = QuestionID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@HolidayId" };
            string[] value = { "LoadbyID", hdfHoliday_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Holidays", 2, parameter, value);
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
                        ddlHolidayType.SelectedValue = dt.Rows[0]["HolidayType"].ToString();
                        txtHolidayName.Text =Convert.ToString(dt.Rows[0]["HolidayNameEnglish"]);
                        txtHolidayNameHindi.Text = Convert.ToString(dt.Rows[0]["HolidayNameHindi"]);
                        txtHolidayDate.Text =Convert.ToString(dt.Rows[0]["HolidayDate"]);
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
            string holiday_id = (sender as LinkButton).CommandArgument;
            hdfHoliday_Id.Value = holiday_id;

            string[] parameter = { "@Flag", "@HolidayId" };
            string[] value = { "Delete", hdfHoliday_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Holidays", 2, parameter, value);
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
                hdfHoliday_Id.Value = "0";
                FillHolidays();
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
        string Holiday_Id =((HiddenField)ckkIsactive.Parent.FindControl("hdf_HolidayIdGrd")).Value;
        string Flag = "";

        if (ckkIsactive.Checked)
        {
            Flag = "Activate";
        }
        else
        {
            Flag = "Deactivate";
        }

        string[] parameter = { "@Flag", "@HolidayId" };
        string[] value = { Flag, Holiday_Id};
        DB_Status dbs = dba.sp_populateDataSet("SP_Holidays", 2, parameter, value);
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