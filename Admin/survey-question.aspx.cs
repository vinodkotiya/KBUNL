using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_survey_question : System.Web.UI.Page
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
            if (Session["SurveyId"] != null)
            {
                hdfSurveyId.Value = Convert.ToString(Session["SurveyId"]);
                SurveyTitle.InnerText =Convert.ToString(Session["SurveyName"]);
            }
            else
            {
                Response.Redirect("survey.aspx");
            }
            panelAddNew.Visible = false;
            panelView.Visible = true;
            //Fill Articles

            Fill_SurveyQuestion();
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

        hdfSurveyQuestionId.Value = "0";

        txtSurveyQuestion.Text = "";
        ddlOptions.SelectedIndex = 0;
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtSurveyQuestion.Text == "")
                {
                    displayMessage("Please enter survey question", "error");
                }
                else if (ddlOptions.SelectedIndex == 0)
                {
                    displayMessage("Please select option", "error");
                }
                else
                {
                    string[] parameter = { "@Flag", "@SurveyId", "@SurveyQuestion", "@SurveyQuestionOption" };
                    string[] value = { "Add", hdfSurveyId.Value, txtSurveyQuestion.Text.Trim(), Convert.ToString(ddlOptions.SelectedValue) };
                    DB_Status dbs = dba.sp_populateDataSet("Sp_Survey_Question", 4, parameter, value);
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
                        displayMessage("Added successfully", "info");
                        Fill_SurveyQuestion();
                        clearfields();
                        hdfSurveyQuestionId.Value = "0";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
                    }
                    else
                    {
                        displayMessage("Record already exists", "error");
                    }
                }
            }
            else if (btnSave.Text == "Update")
            {
                if (txtSurveyQuestion.Text == "")
                {
                    displayMessage("Please enter survey question", "error");
                }
                else if (ddlOptions.SelectedIndex == 0)
                {
                    displayMessage("Please select option", "error");
                }
                else
                {
                    string[] param = { "@Flag", "@SurveyQuestionId", "@SurveyId", "@SurveyQuestion", "@SurveyQuestionOption" };
                    string[] value = { "Update", hdfSurveyQuestionId.Value, hdfSurveyId.Value, txtSurveyQuestion.Text.Trim(), ddlOptions.SelectedValue };
                    DB_Status DBS = dba.sp_readSingleData("Sp_Survey_Question", 5, param, value);
                    string status = DBS.SingleResult;
                    if (DBS.OperationStatus.ToString() == "Success")
                    {
                        if (status == "success")
                        {
                            displayMessage("Updated successfully", "info");
                            Fill_SurveyQuestion();
                            clearfields();
                            hdfSurveyQuestionId.Value = "0";
                            txtSurveyQuestion.Text = "";
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;
                        }
                        else if (status == "exits")
                        {
                            displayMessage("Record already exists", "error");
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
        hdfSurveyQuestionId.Value = "0";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }


    protected void Fill_SurveyQuestion()
    {
        try
        {
            string[] parameter = { "@Flag", "@SurveyId" };
            string[] value = { "View", hdfSurveyId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Survey_Question", 2, parameter, value);
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

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string SurveyQuestionID = (sender as LinkButton).CommandArgument;
            hdfSurveyQuestionId.Value = SurveyQuestionID;

            string[] param = { "@Flag", "@SurveyQuestionId" };
            string[] value = { "SurveyQuestionById", hdfSurveyQuestionId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Survey_Question", 2, param, value);
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
                        txtSurveyQuestion.Text = Convert.ToString(ds.Tables[0].Rows[0]["SurveyQuestion"]);
                        ddlOptions.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["SurveyQuestionOption"]);

                        if (flag)
                        {
                            panelAddNew.Visible = true;
                            panelView.Visible = false;

                            displayMessage("", "");
                            btnSave.Text = "Update";
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
    protected void ViewList_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string SurveyQuestionId = (sender as LinkButton).CommandArgument;


            hdfSurveyQuestionId.Value = SurveyQuestionId;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            Label lblSurveyQuestion = (Label)grdrow.FindControl("lblSurveyQuestion");
            int rowindex = grdrow.RowIndex;
            Session["SurveyQuestionId"] = hdfSurveyQuestionId.Value;
            Session["SurveyQuestionName"] = lblSurveyQuestion.Text.Trim();
            Response.Redirect("survey-question-options.aspx");
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
            string QuestionID = (sender as LinkButton).CommandArgument;
            hdfSurveyQuestionId.Value = QuestionID;

            string[] parameter = { "@Flag", "@SurveyQuestionId" };
            string[] value = { "Delete", hdfSurveyQuestionId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Survey_Question", 2, parameter, value);
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
                hdfSurveyQuestionId.Value = "0";
                Fill_SurveyQuestion();
            }
        }
        catch (Exception)
        {
        }
    }

    protected void clearfields()
    {
        txtSurveyQuestion.Text = "";
        ddlOptions.SelectedIndex = 0;
        hdfSurveyQuestionId.Value = "0";
    }

    protected void grdview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkSurveyDetails = (LinkButton)e.Row.FindControl("lnkSurveyDetails");
            HiddenField hdfQuestionOption = (HiddenField)e.Row.FindControl("hdfQuestionOption");
            if (!string.IsNullOrEmpty(hdfQuestionOption.Value))
            {
                if (hdfQuestionOption.Value == "3")
                    lnkSurveyDetails.Visible = false;
                else
                    lnkSurveyDetails.Visible = true;
            }
        }
    }
}