using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_survey_question_options : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] == null)
        {
            Response.Redirect("../login.aspx");
        }

        if (Session["SurveyQuestionId"] == null)
        {
            Response.Redirect("survey-question.aspx");
        }
        else
        {
            hdfSurveyQuestionId.Value =Convert.ToString(Session["SurveyQuestionId"]);
            SurveyTitle.InnerText=Convert.ToString(Session["SurveyName"]);
            SurveyQuestionTitle.InnerText =Convert.ToString(Session["SurveyQuestionName"]);
        }

        if (!IsPostBack)
        {

            //panelAddNew.Visible = false;
            panelView.Visible = true;
            //Fill Articles

            FillSurveyQuestionOptions();
            displayGridMessage("", "");

            hdfQuestionOptionId.Value = "0";
            txtOptionValue.Text = "";
            btnSave.Text = "Save";
        }
    }
    /* Add/Update/Deactivate Banner*/
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
        hdfQuestionOptionId.Value = "0";

        txtOptionValue.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtOptionValue.Text.Trim() == "")
                    displayMessage("Please enter option value", "error");
                else
                {
                    string[] parameter = { "@Flag", "@SurveyQuestionId", "@OptionValue" };
                    string[] value = { "Add", hdfSurveyQuestionId.Value, txtOptionValue.Text.Trim() };
                    DB_Status dbs = dba.sp_populateDataSet("Sp_Survey_Question_Options", 3, parameter, value);
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
                        displayMessage("Option successfully added", "info");
                        FillSurveyQuestionOptions();
                        hdfQuestionOptionId.Value = "0";
                        txtOptionValue.Text = "";
                        btnSave.Text = "Save";
                    }
                    if (result == "exits")
                    {
                        displayMessage("Record Already Exists", "error");
                    }
                }
            }
            else if (btnSave.Text == "Update")
            {
                if (txtOptionValue.Text.Trim() == "")
                    displayMessage("Please enter option value", "error");
                else
                {
                    string[] param = { "@Flag", "@QuestionOptionId", "@SurveyQuestionId", "@OptionValue" };
                    string[] value = { "Update", hdfQuestionOptionId.Value, hdfSurveyQuestionId.Value, txtOptionValue.Text.Trim() };
                    DB_Status dbs = dba.sp_populateDataSet("Sp_Survey_Question_Options", 4, param, value);
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
                        displayMessage("Option successfully updated", "info");
                        FillSurveyQuestionOptions();
                        hdfQuestionOptionId.Value = "0";
                        txtOptionValue.Text = "";
                        btnSave.Text = "Save";
                    }
                    if (result == "exits")
                    {
                        displayMessage("Record Already Exists", "error");
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
        displayMessage("", "");
        displayGridMessage("", "");
        hdfQuestionOptionId.Value = "";
        txtOptionValue.Text = "";

        btnSave.Text = "Save";
        Response.Redirect("survey-question.aspx");
    }
    protected void FillSurveyQuestionOptions()
    {
        try
        {
            string[] parameter = { "@Flag", "@SurveyQuestionId" };
            string[] value = { "View", hdfSurveyQuestionId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Survey_Question_Options", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    grdview.DataSource = dt;
                    grdview.DataBind();
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
            string QuestionOptionId = (sender as LinkButton).CommandArgument;
            hdfQuestionOptionId.Value = QuestionOptionId;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@QuestionOptionId" };
            string[] value = { "SurveyQuestionOptionById", hdfQuestionOptionId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Survey_Question_Options", 2, parameter, value);
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
                        txtOptionValue.Text = dt.Rows[0]["OptionValue"].ToString();
                    }
                }
            }
            if (flag)
            {
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
            string SurveyQustionOptionId = (sender as LinkButton).CommandArgument;
            hdfQuestionOptionId.Value = SurveyQustionOptionId;

            string[] parameter = { "@Flag", "@QuestionOptionId" };
            string[] value = { "Delete", hdfQuestionOptionId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Survey_Question_Options", 2, parameter, value);
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
                hdfQuestionOptionId.Value = "0";
                FillSurveyQuestionOptions();
            }
        }
        catch (Exception)
        {
        }
    }
}