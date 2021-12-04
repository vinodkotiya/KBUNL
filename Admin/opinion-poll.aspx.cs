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

            Fill_OpinionPoll();
            displayGridMessage("", "");
        }
    }
    /* Add/Update/Opinion Poll*/
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

        HDN_PollID.Value = "";

        txtTitle.Text = "";
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
                if (txtTitle.Text=="")
                {
                    displayMessage("Please enter Topic Title", "error");
                }
                else if (txtBeginDate.Text == "")
                {
                    displayMessage("Please enter Begin Date", "error");
                }
                else if (txtEndDate.Text == "")
                {
                    displayMessage("Please enter Last Date", "error");
                }
                else
                {
                    string BeginDate = mod.makedate(txtBeginDate.Text);
                    string EndDate = mod.makedate(txtEndDate.Text);

                    string[] param = { "@Flag", "@PollTitle", "@BeginDate", "@LastDate"};
                    string[] value = { "Add",txtTitle.Text, BeginDate, EndDate};
                    DB_Status DBS = dba.sp_readSingleData("Sp_OpinionPoll",4, param, value);
                    string status = DBS.SingleResult;
                    if (DBS.OperationStatus.ToString() == "Success")
                    {
                        if (status == "success")
                        {
                            displayMessage("Added successfully", "info");
                            Fill_OpinionPoll();
                            clearfields();
                            HDN_PollID.Value = "";
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
                        else if(status== "Last Date should not be less than Begin Date")
                        {
                            displayMessage("Last Date should not be less than Begin Date", "error");
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
                if (txtTitle.Text == "")
                {
                    displayMessage("Please enter Topic Title", "error");
                }
                else if (txtBeginDate.Text == "")
                {
                    displayMessage("Please enter Begin Date", "error");
                }
                else if (txtEndDate.Text == "")
                {
                    displayMessage("Please enter Last Date", "error");
                }
                else
                {
                    string BeginDate = mod.makedate(txtBeginDate.Text.Trim());
                    string EndDate = mod.makedate(txtEndDate.Text.Trim());

                    string[] param = { "@Flag","@PollID", "@PollTitle", "@BeginDate", "@LastDate" };
                    string[] value = { "Update", HDN_PollID.Value, txtTitle.Text, BeginDate, EndDate};
                    DB_Status DBS = dba.sp_readSingleData("Sp_OpinionPoll",5, param, value);
                    string status = DBS.SingleResult;
                    if (DBS.OperationStatus.ToString() == "Success")
                    {
                        if (status == "success")
                        {
                            displayMessage("Updated successfully", "info");
                            Fill_OpinionPoll();
                            clearfields();
                            
                            txtBeginDate.Enabled = true;
                            txtEndDate.Enabled = true;
                            HDN_PollID.Value = "";
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
                        else if (status == "Last Date should not be less than Begin Date")
                        {
                            displayMessage("Last Date should not be less than Begin Date", "error");
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
        HDN_PollID.Value = "";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }


    protected void Fill_OpinionPoll()
    {
        try
        {
            string[] parameter = { "@Flag" };
            string[] value = { "View" };
            DB_Status dbs = dba.sp_populateDataSet("Sp_OpinionPoll",1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        grdOpinionMaster.DataSource = dt;
                        grdOpinionMaster.DataBind();
                    }
                    else
                    {
                        grdOpinionMaster.DataSource = null;
                        grdOpinionMaster.DataBind();
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
            HDN_PollID.Value = QuestionID;

            string[] param = { "@Flag", "@PollID" };
            string[] value = { "OpinionPollById",HDN_PollID.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_OpinionPoll",2, param, value);
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
                        txtTitle.Text = ds.Tables[0].Rows[0]["PollTitle"].ToString();
                        txtBeginDate.Text = ds.Tables[0].Rows[0]["BeginDate"].ToString();
                        txtEndDate.Text = ds.Tables[0].Rows[0]["LastDate"].ToString();
                       
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
            string PollId = (sender as LinkButton).CommandArgument;


            HDN_PollID.Value = PollId;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;
            Label grid_lblSurveyTitle = (Label)grdOpinionMaster.Rows[rowindex].FindControl("lbl_Title");


            Session["PollId"] = HDN_PollID.Value;
            Session["PollTitle"] = grid_lblSurveyTitle.Text;
            Response.Redirect("opinion-poll-option.aspx");
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
            HDN_PollID.Value = QuestionID;

            string[] parameter = { "@Flag","@PollID" };
            string[] value = { "Delete",HDN_PollID.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_OpinionPoll",2, parameter, value);
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
                HDN_PollID.Value = "0";
                Fill_OpinionPoll();               
            }
        }
        catch (Exception)
        {
        }
    }
    


    protected void grdOpinionMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label BeginDate = (Label)e.Row.FindControl("lbl_Begindate");
                Label LastDate = (Label)e.Row.FindControl("lbl_LastDate");

                LinkButton LinkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                LinkButton LinkAddOption = (LinkButton)e.Row.FindControl("lnkActionViewList");
                LinkButton LinkViewVote = (LinkButton)e.Row.FindControl("lnkViewVote");
                LinkButton LinkDelete = (LinkButton)e.Row.FindControl("lnkActionDelete");

                if (mod.IsDateBeforeToday(BeginDate.Text) == false)
                {
                    //LinkEdit.Visible = false;
                    //LinkAddOption.Visible = false;
                    //LinkViewVote.Visible = true;
                    //LinkDelete.Visible = false;

                    LinkEdit.Visible = true;
                    LinkAddOption.Visible = true;
                    LinkViewVote.Visible = true;
                    LinkDelete.Visible = true;
                }
                else
                {
                    LinkEdit.Visible = true;
                    LinkAddOption.Visible = true;
                    LinkViewVote.Visible = true;
                    LinkDelete.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
          //  DisplayEmployeeMessage(ex.Message, "error");
        }
    }


    protected void lnkViewVote_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
        try
        {
            string PollID = (sender as LinkButton).CommandArgument;

            // ModalPopupExtender2.Show();
            LinkButton lnk = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnk.Parent.Parent;
            int rowindex = grdrow.RowIndex;
           

            string charthtml = "";
            DataTable dtResult = new DataTable();
            dtResult = null;
            string[] parameters = { "@Flag","@PollID" };
            string[] values = { "View",PollID };
            DB_Status dbs = dba.sp_populateDataSet("Sp_OpinionPoll_Votes",2, parameters, values);
            string result = "";
            if (dbs.OperationStatus.ToString() == "Success")
            {
                string TotalVotes = "0";
                
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        TotalVotes = dt.Rows[0]["TotalVotes"].ToString();                        
                        dtResult = ds.Tables[1];
                        charthtml += "<h4>Total Votes: " + TotalVotes + "</h4>";
                        for (int i = 0; i < dtResult.Rows.Count; i++)
                        {
                            double chart_TotalVotes = 0;
                            if (TotalVotes != "")
                                chart_TotalVotes = Convert.ToDouble(TotalVotes);

                            double chart_OptionVote = 0;
                            if (dtResult.Rows[i]["TotalVotes"].ToString() != "")
                                chart_OptionVote = Convert.ToDouble(dtResult.Rows[i]["TotalVotes"].ToString());

                            double percent = Math.Round((chart_OptionVote / chart_TotalVotes) * 100, 0);
                            
                            charthtml += "<div class='chartbarrhs bar" + ((i % 5) + 1).ToString() + "'>";
                            charthtml += "  <table cellspacing='0' cellpadding='0'>";
                            charthtml += "      <td width='80px'><div class='axistitle'>" + dtResult.Rows[i]["OptionText"].ToString() + "</div></td>";
                            charthtml += "      <td>";
                            charthtml += "          <div class='barbox'>";
                            charthtml += "              <div class='bar' style='width:" + percent + "%;'>&nbsp;</div>";
                            charthtml += "          </div>";
                            charthtml += "      </td>";
                            charthtml += "      <td width='50px'><div class='value'>" + percent + "%</div></td>";
                            charthtml += "  </table>";
                            charthtml += "</div>";
                        }                       
                    }
                }
                chart.InnerHtml = charthtml;
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void clearfields()
    {
        txtTitle.Text = "";
        txtBeginDate.Text = "";
        txtEndDate.Text = "";
    }


 
}