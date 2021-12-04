using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_print_survey_response : System.Web.UI.Page
{
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Sid"] == null && Request.QueryString["ECode"] == null)
            {
                Response.Redirect("view-survey-response.aspx");
            }
            else
            {
                hdfSurveyId.Value = Convert.ToString(Request.QueryString["Sid"]);
                hdfEmpCode.Value = Convert.ToString(Request.QueryString["ECode"]);
                BindSurvey();
                BindSurveyResponse();
                BindSurveyQuestion();
            }
        }
    }

    public void BindSurvey()
    {
        try
        {
            string[] parameter = { "@Flag", "@SurveyId" };
            string[] value = { "SurveyById", hdfSurveyId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Survey", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lblSurveyTitle.Text = Convert.ToString(dt.Rows[0]["SurveyName"]);
                    }
                    //else
                    //lnkbtnSubmitVote.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void BindSurveyResponse()
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
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lblEmpCode.Text = Convert.ToString(dt.Rows[0]["EmployeeCode"]);
                        lblEmpName.Text = Convert.ToString(dt.Rows[0]["EmployeeName"]);
                        lblMobileNo.Text = Convert.ToString(dt.Rows[0]["MobileNo"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void BindSurveyQuestion()
    {
        try
        {
            string[] parameter = { "@Flag", "@SurveyId" };
            string[] value = { "PublicView", hdfSurveyId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Survey_Question", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dtlstSurveyQuestion.DataSource = dt;
                        dtlstSurveyQuestion.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void dtlstSurveyQuestion_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hdfSurveyQuestionId = (HiddenField)e.Item.FindControl("hdfSurveyQuestionId");
            HiddenField hdfSurveyQuestionOption = (HiddenField)e.Item.FindControl("hdfSurveyQuestionOption"); 
            DataList dtlstSurveyQuestionSingleChoice = (DataList)e.Item.FindControl("dtlstSurveyQuestionSingleChoice");
            DataList dtlstSurveyQuestionMultiChoice = (DataList)e.Item.FindControl("dtlstSurveyQuestionMultiChoice");
            Label lblSurveyResponseText = (Label)e.Item.FindControl("lblSurveyResponseText");
            if (!string.IsNullOrEmpty(hdfSurveyQuestionOption.Value))
            {
                if (hdfSurveyQuestionOption.Value == "1")
                {
                    dtlstSurveyQuestionSingleChoice.Visible = true;
                    dtlstSurveyQuestionMultiChoice.Visible = false;
                    lblSurveyResponseText.Visible = false;
                    BindSurveyQuestionResponseOptions(hdfSurveyQuestionId.Value, lblSurveyResponseText, dtlstSurveyQuestionSingleChoice, hdfSurveyQuestionOption.Value);
                }
                if (hdfSurveyQuestionOption.Value == "2")
                {
                    dtlstSurveyQuestionSingleChoice.Visible = false;
                    dtlstSurveyQuestionMultiChoice.Visible = true;
                    lblSurveyResponseText.Visible = false;
                    BindSurveyQuestionResponseOptions(hdfSurveyQuestionId.Value, lblSurveyResponseText, dtlstSurveyQuestionMultiChoice, hdfSurveyQuestionOption.Value);
                }
                if (hdfSurveyQuestionOption.Value == "3")
                {
                    dtlstSurveyQuestionSingleChoice.Visible = false;
                    dtlstSurveyQuestionMultiChoice.Visible = false;
                    lblSurveyResponseText.Visible = true;
                    BindSurveyQuestionResponseOptions(hdfSurveyQuestionId.Value, lblSurveyResponseText, dtlstSurveyQuestionMultiChoice, hdfSurveyQuestionOption.Value);
                }
            }

        }
    }
    public void BindSurveyQuestionResponseOptions(string SurveyQuestionId,Label lblSurveyResponseText, DataList dtlstSurveyQuestionOptions,string SurveyQuestionOption)
    {
        try
        {
            DataTable dtResult = new DataTable();
            dtResult = null;
            string[] parameters = { "@Flag", "@SurveyQuestionId", "@SurveyId", "@EmployeeCode" };
            string[] values = { "PrintResponse", SurveyQuestionId,hdfSurveyId.Value,hdfEmpCode.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Survey_Response_Options", 4, parameters, values);
            string result = "";
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        if (SurveyQuestionOption == "1")
                        {
                            dtlstSurveyQuestionOptions.DataSource = dt;
                            dtlstSurveyQuestionOptions.DataBind();
                        }
                        else if (SurveyQuestionOption == "2")
                        {
                            dtlstSurveyQuestionOptions.DataSource = dt;
                            dtlstSurveyQuestionOptions.DataBind();
                        }
                        else if (SurveyQuestionOption == "3")
                        {
                            lblSurveyResponseText.Text = Convert.ToString(dt.Rows[0]["ResponseText"]);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkbtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("view-survey-response.aspx");
    }
}