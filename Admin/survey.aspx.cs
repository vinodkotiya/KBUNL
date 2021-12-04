using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_survey : System.Web.UI.Page
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
            //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_opinionpoll");
            //menuli.Attributes["class"] = "active";

            panelAddNew.Visible = false;
            panelView.Visible = true;
            //Fill Articles

            Fill_Survey();
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

        hdfSurveyId.Value = "0";

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


                    string[] param = { "@Flag", "@DeptID","@SurveyName", "@StartDate", "@EndDate" };
                    string[] value = { "Add",hdfDeptId.Value,txtSurveyName.Text, BeginDate, EndDate };
                    DB_Status DBS = dba.sp_readSingleData("Sp_Survey",5, param, value);
                    string status = DBS.SingleResult;
                    if (DBS.OperationStatus.ToString() == "Success")
                    {
                        if (status == "success")
                        {
                            displayMessage("Added successfully", "info");
                            Fill_Survey();
                            clearfields();
                            hdfSurveyId.Value = "0";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;
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
                            Fill_Survey();
                            clearfields();

                            txtBeginDate.Enabled = true;
                            txtEndDate.Enabled = true;
                            hdfSurveyId.Value = "";
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
        hdfSurveyId.Value = "";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }

    protected void Fill_Survey()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "View",hdfDeptId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Survey",2, parameter, value);
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
            string QuestionID = (sender as LinkButton).CommandArgument;
            hdfSurveyId.Value = QuestionID;

            string[] param = { "@Flag", "@SurveyId" };
            string[] value = { "SurveyById", hdfSurveyId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Survey", 2, param, value);
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
                        txtSurveyName.Text =Convert.ToString(ds.Tables[0].Rows[0]["SurveyName"]);
                        txtBeginDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["StartDate"]);
                        txtEndDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["EndDate"]);

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
            string SurveyId = (sender as LinkButton).CommandArgument;


            hdfSurveyId.Value = SurveyId;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;
            Label lblSurveyName = (Label)grdview.Rows[rowindex].FindControl("lblSurveyName");


            Session["SurveyId"] = hdfSurveyId.Value;
            Session["SurveyName"] = lblSurveyName.Text.Trim();
            Response.Redirect("survey-question.aspx");
        }
        catch (Exception)
        {

        }
    }

    protected void lnkSurveyResponse_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string SurveyId = (sender as LinkButton).CommandArgument;


            hdfSurveyId.Value = SurveyId;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;
            Label lblSurveyName = (Label)grdview.Rows[rowindex].FindControl("lblSurveyName");


            Session["SurveyId"] = hdfSurveyId.Value;
            Session["SurveyName"] = lblSurveyName.Text.Trim();
            Response.Redirect("view-survey-response.aspx");
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
            hdfSurveyId.Value = QuestionID;

            string[] parameter = { "@Flag", "@SurveyId" };
            string[] value = { "Delete", hdfSurveyId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Survey", 2, parameter, value);
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
                hdfSurveyId.Value = "0";
                Fill_Survey();
            }
            else
            {
                displayGridMessage("Record can not be delete", "error");
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
        hdfSurveyId.Value = "0";
    }
}