using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Hindi_survey : System.Web.UI.Page
{
    DB_Access dba = new DB_Access();
    Class1 obj = new Class1();

    private void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["Theme"] == null)
            Session["Theme"] = "theme_green";
        Page.Theme = Session["Theme"].ToString();
        if (Session["Theme"].ToString() == "theme_blue")
        {
            LinkButton themebtn = WCTopBar1.GetThemeButton_Blue;
            themebtn.CssClass = "themebtn themebtn_blue active";
        }
        else if (Session["Theme"].ToString() == "theme_green")
        {
            LinkButton themebtn = WCTopBar1.GetThemeButton_Green;
            themebtn.CssClass = "themebtn themebtn_green active";
        }
        else if (Session["Theme"].ToString() == "theme_black")
        {
            LinkButton themebtn = WCTopBar1.GetThemeButton_Purple;
            themebtn.CssClass = "themebtn themebtn_purple active";
        }
        else if (Session["Theme"].ToString() == "theme_orange")
        {
            LinkButton themebtn = WCTopBar1.GetThemeButton_Orange;
            themebtn.CssClass = "themebtn themebtn_orange active";
        }
        else if (Session["Theme"].ToString() == "theme_red")
        {
            LinkButton themebtn = WCTopBar1.GetThemeButton_Red;
            themebtn.CssClass = "themebtn themebtn_red active";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string IPAdd;
        IPAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(IPAdd))
            IPAdd = Request.ServerVariables["REMOTE_ADDR"];
        hdfIPAddress.Value = IPAdd;
        if (!IsPostBack)
        {
            BindSurvey();
        }
    }

    public void CheckVotingForIP()
    {
        try
        {
            string[] parameter = { "@Flag", "@PollID", "@IPAddress" };
            string[] value = { "CheckIPForVoting", hdfSurveyId.Value, hdfIPAddress.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_OpinionPoll_Votes", 3, parameter, value);
            string Result = "";
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Result = Convert.ToString(dt.Rows[0]["Result"]);
                        if (Result == "Exists")
                        {
                            //pnlSummary.Visible = true;
                            //pnlVoting.Visible = false;

                        }
                        else if (Result == "NotExists")
                        {
                            //pnlSummary.Visible = false;
                            // pnlVoting.Visible = true;
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void BindSurvey()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "PublicView", hdfDeptId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Survey", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        hdfSurveyId.Value = Convert.ToString(dt.Rows[0]["SurveyId"]);
                        dtlstSurvey.DataSource = dt;
                        dtlstSurvey.DataBind();
                        pnlnotavailable.Visible = false;
                        pnlSurvey.Visible = true;
                    }
                    else
                    {
                        pnlSurvey.Visible = false;
                        pnlnotavailable.Visible = true;

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

    protected void lnkbtnParticipate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtEmpCode.Text == "")
            {
                lblMsg.Text = "Please enter employee number";
                lblMsg.ForeColor = Color.Red;
            }
            else if (txtPassword.Text == "")
            {
                lblMsg.Text = "Please enter password";
                lblMsg.ForeColor = Color.Red;
            }
            else
            {
                CheckValidEmployee();
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void CheckValidEmployee()
    {
        try
        {
            if (txtEmpCode.Text == "" && txtPassword.Text == "")
            {
                lblMsg.Text = "Please enter employee number and password";
                lblMsg.ForeColor = Color.Red;
            }
            else if (txtEmpCode.Text == "")
            {
                lblMsg.Text = "Please enter employee number";
                lblMsg.ForeColor = Color.Red;
            }
            else if (txtPassword.Text == "")
            {
                lblMsg.Text = "Please enter password";
                lblMsg.ForeColor = Color.Red;
            }
            else
            {
                #region--Employee
                string result = "";
                string EmpCode = "";
                string EmpName = "";
                string MobileNo = "";

                string EncryptPassword = obj.Encrypt(txtPassword.Text);
                string[] parameter = { "@Username", "@Password" };
                string[] value = { txtEmpCode.Text, EncryptPassword };
                DB_Status dbs = dba.sp_populateDataSet("SP_Employee_Login", 2, parameter, value);
                if (dbs.OperationStatus.ToString() == "Success")
                {
                    DataSet ds = dbs.ResultDataSet;
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];

                        if (dt.Rows.Count > 0)
                        {
                            result = dt.Rows[0]["Result"].ToString();
                            if (result == "ok")
                            {
                                DataTable dtUsers = ds.Tables[1];
                                EmpName = Convert.ToString(dtUsers.Rows[0]["EmpName"]);
                                EmpCode = Convert.ToString(dtUsers.Rows[0]["EmpCode"]);
                                MobileNo = Convert.ToString(dtUsers.Rows[0]["Mobile"]);

                            }
                        }
                    }
                }
                if (result == "NotExists")
                {
                    lblMsg.Text = "Employee Not Exists";
                    lblMsg.ForeColor = Color.Red;
                }
                else if (result == "IncorrectPassword")
                {
                    lblMsg.Text = "Incorrect Password";
                    lblMsg.ForeColor = Color.Red;
                }
                else if (result == "ok")
                {
                    hdfEmpCode.Value = EmpCode;
                    hdfEmpName.Value = EmpName;
                    hdfMobileNo.Value = MobileNo;
                    CheckEmployeeParticipateInSurvey();
                }
                else
                {
                    lblMsg.Text = "Error";
                    lblMsg.ForeColor = Color.Red;
                }
                #endregion
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void CheckEmployeeParticipateInSurvey()
    {
        try
        {
            DataTable dtResult = new DataTable();
            dtResult = null;
            string[] parameters = { "@Flag", "@SurveyId", "@EmployeeCode" };
            string[] values = { "CheckEmployeeParticipate", hdfSurveyId.Value, hdfEmpCode.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Survey_Response", 3, parameters, values);
            string result = "";
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        result = Convert.ToString(dt.Rows[0]["ParticipateStatus"]);
                        if (result == "Yes")
                        {
                            lblMsg.Text = "Sorry ! you have already participated in this survey.";
                            lblMsg.ForeColor = Color.Red;
                        }
                        else if (result == "No")
                        {
                            pnlEmp.Visible = false;
                            pnlSurvey.Visible = false;
                            pnlSurveyQuestion.Visible = true;
                            BindSurveyQuestion();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkbtnSelect_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkbtnSelect = sender as LinkButton;
            DataListItem lstItem = (DataListItem)lnkbtnSelect.NamingContainer;
            HiddenField hdfDataListSurveyId = (HiddenField)lstItem.FindControl("hdfDataListSurveyId");
            HiddenField hdfSurveyTitle = (HiddenField)lstItem.FindControl("hdfSurveyTitle");
            if (!string.IsNullOrEmpty(hdfDataListSurveyId.Value))
            {
                hdfSurveyId.Value = hdfDataListSurveyId.Value;
                lblSurveyTitle.Text = hdfSurveyTitle.Value;
                pnlSurvey.Visible = false;
                pnlEmp.Visible = true;
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
            RadioButtonList rbtnlstOptions = (RadioButtonList)e.Item.FindControl("rbtnlstOptions");
            CheckBoxList chklstOptions = (CheckBoxList)e.Item.FindControl("chklstOptions");
            TextBox txtQuestionReply = (TextBox)e.Item.FindControl("txtQuestionReply");
            if (!string.IsNullOrEmpty(hdfSurveyQuestionOption.Value))
            {
                if (hdfSurveyQuestionOption.Value == "1")
                {
                    rbtnlstOptions.Visible = true;
                    chklstOptions.Visible = false;
                    txtQuestionReply.Visible = false;
                    BindSurveyQuestionSingleChoiceOptions(hdfSurveyQuestionId.Value, rbtnlstOptions);
                }
                if (hdfSurveyQuestionOption.Value == "2")
                {
                    rbtnlstOptions.Visible = false;
                    chklstOptions.Visible = true;
                    txtQuestionReply.Visible = false;
                    BindSurveyQuestionMultiChoiceOptions(hdfSurveyQuestionId.Value, chklstOptions);
                }
                if (hdfSurveyQuestionOption.Value == "3")
                {
                    rbtnlstOptions.Visible = false;
                    chklstOptions.Visible = false;
                    txtQuestionReply.Visible = true;
                }
            }

        }
    }

    public void BindSurveyQuestionSingleChoiceOptions(string SurveyQuestionId, RadioButtonList rbtnlstOptions)
    {
        try
        {
            DataTable dtResult = new DataTable();
            dtResult = null;
            string[] parameters = { "@Flag", "@SurveyQuestionId" };
            string[] values = { "PublicView", SurveyQuestionId };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Survey_Question_Options", 2, parameters, values);
            string result = "";
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        rbtnlstOptions.DataSource = dt;
                        rbtnlstOptions.DataValueField = "QuestionOptionId";
                        rbtnlstOptions.DataTextField = "OptionValue";
                        rbtnlstOptions.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void BindSurveyQuestionMultiChoiceOptions(string SurveyQuestionId, CheckBoxList chklstOptions)
    {
        try
        {
            DataTable dtResult = new DataTable();
            dtResult = null;
            string[] parameters = { "@Flag", "@SurveyQuestionId" };
            string[] values = { "PublicView", SurveyQuestionId };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Survey_Question_Options", 2, parameters, values);
            string result = "";
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        chklstOptions.DataSource = dt;
                        chklstOptions.DataValueField = "QuestionOptionId";
                        chklstOptions.DataTextField = "OptionValue";
                        chklstOptions.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void lbkbtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int SingleCount = 0, MultiCount = 0;
            if (dtlstSurveyQuestion.Items.Count > 0)
            {
                for (int i = 0; i < dtlstSurveyQuestion.Items.Count; i++)
                {
                    SingleCount = 0; MultiCount = 0;
                    RadioButtonList rbtnlstOptions = (RadioButtonList)dtlstSurveyQuestion.Items[i].FindControl("rbtnlstOptions");
                    CheckBoxList chklstOptions = (CheckBoxList)dtlstSurveyQuestion.Items[i].FindControl("chklstOptions");
                    TextBox txtQuestionReply = (TextBox)dtlstSurveyQuestion.Items[i].FindControl("txtQuestionReply");
                    RequiredFieldValidator RequiredFieldValidatorQuestion = (RequiredFieldValidator)dtlstSurveyQuestion.Items[i].FindControl("RequiredFieldValidatorQuestion");
                    HiddenField hdfSurveyQuestionOption = (HiddenField)dtlstSurveyQuestion.Items[i].FindControl("hdfSurveyQuestionOption");
                    HiddenField hdfSurveyQuestionId = (HiddenField)dtlstSurveyQuestion.Items[i].FindControl("hdfSurveyQuestionId");


                    if (hdfSurveyQuestionOption.Value == "1")
                    {
                        if (rbtnlstOptions.Items.Count > 0)
                        {
                            for (int j = 0; j < rbtnlstOptions.Items.Count; j++)
                            {
                                if (rbtnlstOptions.Items[j].Selected == true)
                                    SingleCount = SingleCount + 1;
                            }
                            if (SingleCount <= 0)
                            {
                                lblMsg.Text = "Please select option for question " + (i + 1).ToString();
                                lblMsg.ForeColor = Color.Red;
                                return;
                            }
                            else
                            {
                                lblMsg.Text = "";
                            }
                        }
                    }
                    else if (hdfSurveyQuestionOption.Value == "2")
                    {
                        for (int k = 0; k < chklstOptions.Items.Count; k++)
                        {
                            if (chklstOptions.Items[k].Selected == true)
                                MultiCount = MultiCount + 1;
                        }
                        if (MultiCount <= 0)
                        {
                            lblMsg.Text = "Please select atleast one option for question " + (i + 1).ToString(); ;
                            lblMsg.ForeColor = Color.Red;
                            return;
                        }
                        else
                        {
                            lblMsg.Text = "";
                        }
                    }
                    else if (hdfSurveyQuestionOption.Value == "3")
                    {
                        if (txtQuestionReply.Text.Trim() == "")
                        {
                            lblMsg.Text = "Please enter your response for " + (i + 1).ToString(); ;
                            lblMsg.ForeColor = Color.Red;
                            return;
                        }
                        else
                        {
                            lblMsg.Text = "";
                        }
                    }
                }

                //-------------------------Save Record-----------------------
                for (int i = 0; i < dtlstSurveyQuestion.Items.Count; i++)
                {
                    RadioButtonList rbtnlstOptions = (RadioButtonList)dtlstSurveyQuestion.Items[i].FindControl("rbtnlstOptions");
                    CheckBoxList chklstOptions = (CheckBoxList)dtlstSurveyQuestion.Items[i].FindControl("chklstOptions");
                    TextBox txtQuestionReply = (TextBox)dtlstSurveyQuestion.Items[i].FindControl("txtQuestionReply");
                    RequiredFieldValidator RequiredFieldValidatorQuestion = (RequiredFieldValidator)dtlstSurveyQuestion.Items[i].FindControl("RequiredFieldValidatorQuestion");
                    HiddenField hdfSurveyQuestionOption = (HiddenField)dtlstSurveyQuestion.Items[i].FindControl("hdfSurveyQuestionOption");
                    HiddenField hdfSurveyQuestionId = (HiddenField)dtlstSurveyQuestion.Items[i].FindControl("hdfSurveyQuestionId");
                    //--------------Save Response---------------------
                    string[] param = { "@Flag", "@SurveyId", "@SurveyQuestionId", "@EmployeeCode", "@EmployeeName", "@MobileNo" };
                    string[] value = { "Add", hdfSurveyId.Value, hdfSurveyQuestionId.Value, hdfEmpCode.Value, hdfEmpName.Value, hdfMobileNo.Value };
                    DB_Status dbs = dba.sp_populateDataSet("Sp_Survey_Response", 6, param, value);
                    string result = "";
                    if (dbs.OperationStatus.ToString() == "Success")
                    {
                        DataSet ds = dbs.ResultDataSet;
                        if (ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                result = Convert.ToString(dt.Rows[0]["Result"]);
                                hdfSurveyResponseId.Value = Convert.ToString(dt.Rows[0]["SurveyResponseId"]);
                                //--------------------
                                if (hdfSurveyQuestionOption.Value == "1")
                                {
                                    if (rbtnlstOptions.SelectedValue != "")
                                    {
                                        string[] paramSingleChoice = { "@Flag", "@SurveyResponseId", "@SurveyQuestionId", "@QuestionOptionId" };
                                        string[] valueSingleChoice = { "Add", hdfSurveyResponseId.Value, hdfSurveyQuestionId.Value, rbtnlstOptions.SelectedValue };
                                        DB_Status DBS = dba.sp_readSingleData("Sp_Survey_Response_Options", 4, paramSingleChoice, valueSingleChoice);
                                        string status = DBS.SingleResult;
                                        if (DBS.OperationStatus.ToString() == "Success")
                                        {

                                        }
                                    }
                                    else
                                    {
                                        lblMsg.Text = "Please Select Option";
                                        lblMsg.ForeColor = Color.Red;
                                        return;
                                    }
                                }
                                else if (hdfSurveyQuestionOption.Value == "2")
                                {
                                    if (chklstOptions.SelectedValue != "")
                                    {
                                        for (int j = 0; j < chklstOptions.Items.Count; j++)
                                        {
                                            if (chklstOptions.Items[j].Selected == true)
                                            {
                                                string[] paramMultiChoice = { "@Flag", "@SurveyResponseId", "@SurveyQuestionId", "@QuestionOptionId" };
                                                string[] valueMultiChoice = { "Add", hdfSurveyResponseId.Value, hdfSurveyQuestionId.Value, chklstOptions.Items[j].Value };
                                                DB_Status DBS = dba.sp_readSingleData("Sp_Survey_Response_Options", 4, paramMultiChoice, valueMultiChoice);
                                                string status = DBS.SingleResult;
                                                if (DBS.OperationStatus.ToString() == "Success")
                                                {
                                                    if (status == "success")
                                                    {
                                                        //lblMsg.Text = "Record Successfully Submitted";
                                                        //lblMsg.ForeColor = Color.Green;
                                                    }
                                                    else if (status == "exits")
                                                    {
                                                        lblMsg.Text = "Record Already Exists";
                                                        lblMsg.ForeColor = Color.Red;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lblMsg.Text = "Please Select Option";
                                        lblMsg.ForeColor = Color.Red;
                                        return;
                                    }
                                }
                                else if (hdfSurveyQuestionOption.Value == "3")
                                {
                                    if (txtQuestionReply.Text != "")
                                    {
                                        string[] paramText = { "@Flag", "@SurveyResponseId", "@SurveyQuestionId", "@ResponseText" };
                                        string[] valueText = { "Add", hdfSurveyResponseId.Value, hdfSurveyQuestionId.Value, txtQuestionReply.Text.Trim() };
                                        DB_Status DBS = dba.sp_readSingleData("Sp_Survey_Response_Options", 4, paramText, valueText);
                                        string status = DBS.SingleResult;
                                        if (DBS.OperationStatus.ToString() == "Success")
                                        {
                                            if (status == "success")
                                            {

                                            }
                                            else if (status == "exits")
                                            {
                                                lblMsg.Text = "Record Already Exists";
                                                lblMsg.ForeColor = Color.Red;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lblMsg.Text = "Please Enter Your Response";
                                        lblMsg.ForeColor = Color.Red;
                                        return;
                                    }
                                }
                                if (result == "success")
                                {
                                    lblSuccess.Text = "Thank's for taking a part in this survey.";
                                    pnlSurveyQuestion.Visible = false;
                                    pnlSuccessfull.Visible = true;
                                }
                                else
                                {
                                    lblSuccess.Text = "Something is wrong.";
                                    lblSuccess.ForeColor = Color.Red;
                                }
                            }
                        }
                    }
                }
                //---------------------------------------------------
            }


        }
        catch (Exception ex)
        {

        }
    }
}