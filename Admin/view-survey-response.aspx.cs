using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_view_survey_response : System.Web.UI.Page
{
    Class1 mod = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] == null)
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {

            panelAddNew.Visible = false;
            panelView.Visible = true;
            if (Session["SurveyId"] == null)
            {
                Response.Redirect("survey.aspx");
            }
            else
            {
                hdfSurveyId.Value = Convert.ToString(Session["SurveyId"]);
                SurveyTitle.InnerText = Convert.ToString(Session["SurveyName"]);
            }

            FillSurveyResponse();
            displayGridMessage("", "");
        }
    }
    /* Add/Update/Survey*/
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

        hdfSurveyResponseId.Value = "0";

        txtSurveyName.Text = "";
        txtBeginDate.Text = "";
        txtEndDate.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtSurveyName.Text == "")
                {
                    displayMessage("Please enter survey name", "error");
                }
                else if (txtBeginDate.Text == "")
                {
                    displayMessage("Please enter start date", "error");
                }
                else if (mod.IsValidDate(txtBeginDate.Text) == false)
                {
                    displayMessage("Please enter valid start date", "error");
                }
                else if (txtEndDate.Text == "")
                {
                    displayMessage("Please enter valid date", "error");
                }
                else
                {
                    string BeginDate = mod.makedate(txtBeginDate.Text);
                    string EndDate = mod.makedate(txtEndDate.Text);


                    string[] param = { "@Flag", "@DeptID", "@SurveyName", "@StartDate", "@EndDate" };
                    string[] value = { "Add", hdfDeptId.Value, txtSurveyName.Text, BeginDate, EndDate };
                    DB_Status DBS = dba.sp_readSingleData("Sp_Survey", 5, param, value);
                    string status = DBS.SingleResult;
                    if (DBS.OperationStatus.ToString() == "Success")
                    {
                        if (status == "success")
                        {
                            displayMessage("Added successfully", "info");
                            FillSurveyResponse();
                            clearfields();
                            hdfSurveyId.Value = "0";

                        }
                        else if (status == "exits")
                        {
                            displayMessage("Please use different date", "error");
                        }
                        else if (status == "fail")
                        {
                            displayMessage("Server error", "error");
                        }
                    }
                    else
                    {
                        displayMessage(DBS.Title + "-" + DBS.Description, "error");
                    }
                }
            }
            else if (btnSave.Text == "Update")
            {
                if (txtSurveyName.Text == "")
                {
                    displayMessage("Please enter survey name", "error");
                }
                else if (txtBeginDate.Text == "")
                {
                    displayMessage("Please enter start date", "error");
                }
                else if (txtEndDate.Text == "")
                {
                    displayMessage("Please enter end date", "error");
                }
                else
                {
                    string BeginDate = mod.makedate(txtBeginDate.Text.Trim());
                    string EndDate = mod.makedate(txtEndDate.Text.Trim());


                    string[] param = { "@Flag", "@SurveyId", "@SurveyName", "@StartDate", "@EndDate" };
                    string[] value = { "Update", hdfSurveyId.Value, txtSurveyName.Text, BeginDate, EndDate };
                    DB_Status DBS = dba.sp_readSingleData("Sp_Survey", 5, param, value);
                    string status = DBS.SingleResult;
                    if (DBS.OperationStatus.ToString() == "Success")
                    {
                        if (status == "success")
                        {
                            displayMessage("Updated successfully", "info");
                            FillSurveyResponse();
                            clearfields();

                            txtBeginDate.Enabled = true;
                            txtEndDate.Enabled = true;
                            hdfSurveyResponseId.Value = "0";
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;
                        }
                        else if (status == "exits")
                        {
                            displayMessage("Please Use Different Date", "error");
                        }
                        else if (status == "fail")
                        {
                            displayMessage("Server Error", "error");
                        }
                    }
                    else
                    {
                        displayMessage(DBS.Title + "-" + DBS.Description, "error");
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
        hdfSurveyResponseId.Value = "0";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }

    protected void FillSurveyResponse()
    {
        try
        {
            string[] parameter = { "@Flag", "@SurveyId" };
            string[] value = { "View", hdfSurveyId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Survey_Response", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        grdview.DataSource = dt;
                        grdview.DataBind();
                    }
                    else
                    {
                        grdview.DataSource = null;
                        grdview.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // DisplayEmployeeMessage(ex.Message, "error");
        }
    }

    protected void lnkPrintResponse_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string SurveyResponseId = (sender as LinkButton).CommandArgument;


            hdfSurveyResponseId.Value = SurveyResponseId;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;
            Label lblSurveyName = (Label)grdview.Rows[rowindex].FindControl("lblSurveyName");
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
            string SurveyResponseId = (sender as LinkButton).CommandArgument;
            hdfSurveyResponseId.Value = SurveyResponseId;

            string[] parameter = { "@Flag", "@SurveyResponseId" };
            string[] value = { "Delete", hdfSurveyResponseId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Survey_Response", 2, parameter, value);
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
            if (result == "success")
            {
                hdfSurveyResponseId.Value = "0";
                FillSurveyResponse();
            }
        }
        catch (Exception)
        {
        }
    }

    protected void clearfields()
    {
        txtSurveyName.Text = "";
        txtBeginDate.Text = "";
        txtEndDate.Text = "";
        hdfSurveyResponseId.Value = "0";
    }
}