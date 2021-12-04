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

        if (Session["PollId"] == null)
        {
            Response.Redirect("opinion-poll.aspx");
        }
        else
        {
        
            hdfPollId.Value = Session["PollId"].ToString();
            spanQuestionTitle.InnerText = Session["PollTitle"].ToString();
        }

        if (!IsPostBack)
        {
            //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_opinionpoll");
            //menuli.Attributes["class"] = "active";

            //panelAddNew.Visible = false;
            panelView.Visible = true;
            //Fill Articles

            FillQuizOptions();
            displayGridMessage("", "");

            hdfPollOptionId.Value = "0";
            txtLinkName.Text = "";
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
        hdfPollOptionId.Value = "0";

        txtLinkName.Text = "";
        //ddlIsRightOption.SelectedIndex = -1;
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtLinkName.Text.Trim() == "")
                    displayMessage("Please enter Option Title", "error");
                else
                {
                    string[] parameter = { "@Flag","@PollID", "@OptionText"};
                    string[] value = {"Add",hdfPollId.Value, txtLinkName.Text.Trim()};
                    DB_Status dbs = dba.sp_populateDataSet("Sp_OpinionPoll_Options",3, parameter, value);
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
                        FillQuizOptions();
                        hdfPollOptionId.Value = "0";
                        txtLinkName.Text = "";
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
                if (txtLinkName.Text.Trim() == "")
                    displayMessage("Please enter Option Title", "error");
                else
                {
                    string[] param = { "@Flag", "@OptionID", "@PollID", "@OptionText" };
                    string[] value = {"Update",hdfPollOptionId.Value, hdfPollId.Value, txtLinkName.Text.Trim() };
                    DB_Status dbs = dba.sp_populateDataSet("Sp_OpinionPoll_Options",4, param, value);
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
                        FillQuizOptions();
                        hdfPollOptionId.Value = "0";
                        txtLinkName.Text = "";
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
        hdfPollOptionId.Value = "";
        txtLinkName.Text = "";
      
        btnSave.Text = "Save";
        Response.Redirect("opinion-poll.aspx");
    }
    protected void FillQuizOptions()
    {
        try
        {
            string[] parameter = { "@Flag","@PollID" };
            string[] value = { "View",hdfPollId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_OpinionPoll_Options",2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    gridPollOption.DataSource = dt;
                    gridPollOption.DataBind();
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
            string SurveyID = (sender as LinkButton).CommandArgument;
            hdfPollOptionId.Value = SurveyID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag","@OptionID" };
            string[] value = { "OpinionPolloptionById",hdfPollOptionId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_OpinionPoll_Options",2, parameter, value);
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
                        txtLinkName.Text = dt.Rows[0]["OptionText"].ToString();
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
            string SurveyID = (sender as LinkButton).CommandArgument;
            hdfPollOptionId.Value = SurveyID;

            string[] parameter = { "@Flag","@OptionID" };
            string[] value = { "Delete",hdfPollOptionId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_OpinionPoll_Options",2, parameter, value);
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
                hdfPollOptionId.Value = "0";
                FillQuizOptions();
            }
        }
        catch (Exception)
        {
        }
    }    
 
}