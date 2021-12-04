using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class English_opinion_poll : System.Web.UI.Page
{
    DB_Access dba = new DB_Access();

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
            
            BindOpinionPoll();
            CheckVotingForIP();
            BindOpinionPollOptions();
        }
    }

    public void CheckVotingForIP()
    {
        try
        {
            string[] parameter = { "@Flag", "@PollID", "@IPAddress" };
            string[] value = { "CheckIPForVoting",hdfOpinioPollId.Value,hdfIPAddress.Value };
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
                            pnlSummary.Visible = true;
                            pnlAlready.Visible = true;
                            pnlVoting.Visible = false;
                            BindOpinoinPollSummary();
                        }
                        else if (Result == "NotExists")
                        {
                            pnlSummary.Visible = false;
                            pnlAlready.Visible = false;
                            pnlVoting.Visible = true;
                        }
                    }
                    
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void BindOpinionPoll()
    {
        try
        {
            string[] parameter = { "@Flag"};
            string[] value = { "PublicView"};
            DB_Status dbs = dba.sp_populateDataSet("Sp_OpinionPoll", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                  DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lblTitle.Text = Convert.ToString(dt.Rows[0]["PollTitle"]);
                        hdfOpinioPollId.Value = Convert.ToString(dt.Rows[0]["PollID"]);
                        lnkbtnSubmitVote.Visible = true;
                        pnlnotavailable.Visible = false;
                        pnlVoting.Visible = true;
                    }
                    else
                    {
                        lnkbtnSubmitVote.Visible = false;
                        pnlnotavailable.Visible = true;
                        pnlVoting.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void BindOpinionPollOptions()
    {
        try
        {
            string[] parameter = { "@Flag", "@PollID" };
            string[] value = { "View", hdfOpinioPollId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_OpinionPoll_Options",2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        rbtOptions.DataSource = dt;
                        rbtOptions.DataValueField = "OptionID";
                        rbtOptions.DataTextField = "OptionText";
                        rbtOptions.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnSubmitVote_Click(object sender, EventArgs e)
    {
        try
        {
            if (rbtOptions.SelectedValue != "")
            {
                string[] param = { "@Flag","@PollID", "@VoteByEID", "@IPAddress", "@VoteOptionID" };
                string[] value = { "Add", hdfOpinioPollId.Value, "0", hdfIPAddress.Value, rbtOptions.SelectedValue };
                DB_Status DBS = dba.sp_readSingleData("Sp_OpinionPoll_Votes", 5, param, value);
                string status = DBS.SingleResult;
                if (DBS.OperationStatus.ToString() == "Success")
                {
                    if (status == "success")
                    {
                        lblMsg.Text = "Record Successfully Submitted";
                        lblMsg.ForeColor = Color.Green;
                        pnlVoting.Visible = false;
                        pnlnotavailable.Visible = false;
                        pnlSummary.Visible = true;
                        BindOpinoinPollSummary();
                    }
                    else if (status == "exits")
                    {
                        lblMsg.Text = "Record Already Exists";
                        lblMsg.ForeColor = Color.Red;
                    }

                }
                else
                {
                    lblMsg.Text = DBS.Title + "-" + DBS.Description;
                }
            }
            else
            {
                lblMsg.Text = "Please Select Option";
                lblMsg.ForeColor = Color.Red;
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void BindOpinoinPollSummary()
    {
        try
        {
            string charthtml = "";
            DataTable dtResult = new DataTable();
            dtResult = null;
            string[] parameters = { "@Flag", "@PollID" };
            string[] values = { "View", hdfOpinioPollId.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_OpinionPoll_Votes", 2, parameters, values);
            string result = "";
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        string TotalVotes = dt.Rows[0]["TotalVotes"].ToString();
                        dtResult = ds.Tables[1];
                        for (int i = 0; i < dtResult.Rows.Count; i++)
                        {
                            double chart_TotalVotes = 0;
                            if (TotalVotes != "")
                                chart_TotalVotes = Convert.ToDouble(TotalVotes);
                            double chart_OptionVote = 0;
                            if (dtResult.Rows[i]["TotalVotes"].ToString() != "")
                                chart_OptionVote = Convert.ToDouble(dtResult.Rows[i]["TotalVotes"].ToString());

                            double percent = Math.Round((chart_OptionVote / chart_TotalVotes) * 100, 0);

                            charthtml += "<div class='chartbar bar" + ((i % 5) + 1).ToString() + "'>";
                            charthtml += "  <table cellspacing='0' cellpadding='0'><tr>";
                            charthtml += "      <td class='box1'><div class='axistitle'>" + dtResult.Rows[i]["OptionText"].ToString() + "</div></td>";
                            charthtml += "      <td class='box2'>";
                            charthtml += "          <div class='bar' style='width:" + percent*2 + "px;'>&nbsp;</div><span class='percent'>"+ percent.ToString() + "%</span>";
                            charthtml += "      </td>";
                            charthtml += "  </tr></table>";
                            charthtml += "</div>";
                        }

                        chart.InnerHtml = charthtml;

                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
}