using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class Admin_news_post : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] == null)
        {
            Response.Redirect("../login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_intranet");
                menuli.Attributes["class"] = "active";
                panelAddNew.Visible = false;
                panelView.Visible = true;
                //Fill Articles

                FillQuestions();
                displayGridMessage("", "");
            }

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
        hfQuestionID.Value = "";

        txtCategory.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtCategory.Text.Trim() == "")
                    displayMessage("Please enter Category", "error");
                else
                {
                    string[] parameter = { "@CategoryName" };
                    string[] value = { txtCategory.Text.Trim() };
                    DB_Status dbs = dba.sp_populateDataSet("Sp_Intranet_Category_Insert", 1, parameter, value);
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
                    if (result == "AlreadyExists")
                    {
                        displayMessage("Sorry! Category already exists", "error");
                    }
                    else if (result == "success")
                    {
                        displayMessage("Category successfully added", "info");
                        FillQuestions();
                        hfQuestionID.Value = "";
                        //btnSave.Text = "Save";
                        //panelAddNew.Visible = false;
                        //panelView.Visible = true;
                    }
                }
            }
            else if (btnSave.Text == "Update")
            {
                if (txtCategory.Text.Trim() == "")
                    displayMessage("Please enter Title", "error");
                else
                {
                    string[] parameter = { "@CategoryID", "@CategoryName" };
                    string[] value = { hfQuestionID.Value, txtCategory.Text.Trim() };
                    DB_Status dbs = dba.sp_populateDataSet("Sp_Intranet_Category_Update", 2, parameter, value);
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
                    if (result == "AlreadyExists")
                    {
                        displayMessage("Sorry! Category already exists", "error");
                    }
                    else if (result == "success")
                    {
                        displayMessage("Category successfully updated", "info");
                        FillQuestions();
                        hfQuestionID.Value = "";
                        btnSave.Text = "Save";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
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
        hfQuestionID.Value = "";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void FillQuestions()
    {
        try
        {
            string[] parameter = { };
            string[] value = { };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Intranet_Category_View", 0, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    gridQuestionBank.DataSource = dt;
                    gridQuestionBank.DataBind();
                }
            }
        }
        catch (Exception)
        {
        }
    }
    protected void Action_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string QuestionID = (sender as LinkButton).CommandArgument;
            hfQuestionID.Value = QuestionID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            Label grid_lblIsActive = (Label)gridQuestionBank.Rows[rowindex].FindControl("lblIsActive");
            string StoredProcedure = "";
            if (grid_lblIsActive.Text == "Yes")
                StoredProcedure = "SP_Admin_QuestionBank_Deactivate";
            else if (grid_lblIsActive.Text == "No")
                StoredProcedure = "SP_Admin_QuestionBank_Activate";

            if (StoredProcedure != "")
            {
                string[] parameter = { "@QuizID" };
                string[] value = { hfQuestionID.Value };
                DB_Status dbs = dba.sp_populateDataSet(StoredProcedure, 1, parameter, value);
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
                    hfQuestionID.Value = "";
                    FillQuestions();
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
            hfQuestionID.Value = QuestionID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@CategoryID" };
            string[] value = { hfQuestionID.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Intranet_Category_ViewByCategoryID", 1, parameter, value);
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
                        txtCategory.Text = dt.Rows[0]["CategoryName"].ToString();
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
            string QuestionID = (sender as LinkButton).CommandArgument;
            hfQuestionID.Value = QuestionID;

            string[] parameter = { "@CategoryID" };
            string[] value = { hfQuestionID.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Intranet_Category_Delete", 1, parameter, value);
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
                hfQuestionID.Value = "";
                FillQuestions();
            }
        }
        catch (Exception)
        {
        }
    }
    protected void ViewList_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string QuestionID = (sender as LinkButton).CommandArgument;
            hfQuestionID.Value = QuestionID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;
            Label grid_lblSurveyTitle = (Label)gridQuestionBank.Rows[rowindex].FindControl("lblQuizTitle");

            Session["Selected_QuizID"] = hfQuestionID.Value;
            Session["Selected_QuizTitle"] = grid_lblSurveyTitle.Text;
            Response.Redirect("web-links-items.aspx");
        }
        catch (Exception)
        {
        }
    }
 
}