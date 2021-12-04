using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WCCommon_YearCalendar : System.Web.UI.UserControl
{
    int pos, year;
     PagedDataSource adsource;
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ViewState["ViewStateYears"] = 0;
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("Year", typeof(string));

            year = System.DateTime.Now.Year;
            for (int i = 1900; i <= year; i++)
            {
                dr = dt.NewRow();
                dr[0] = Convert.ToString(i);
                dt.Rows.Add(dr);
            }
            adsource = new PagedDataSource();
            adsource.DataSource = dt.DefaultView;
            adsource.PageSize = 12;
            adsource.AllowPaging = true;

            int pageindex = 0, CurrentYear = System.DateTime.Now.Year; int CountCheck = 0;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                int GetYear = Convert.ToInt32(dt.Rows[j]["Year"]);
                if (GetYear == CurrentYear)
                {
                    pos = pageindex;
                    this.ViewState["ViewStateYears"] = pos;
                }
                else
                {
                    if (CountCheck == 11)
                    {
                        pageindex = pageindex + 1;
                        CountCheck = 0;
                    }
                    else
                        CountCheck = CountCheck + 1;
                }
            }
            adsource.CurrentPageIndex = pos;
            bool val= !adsource.IsFirstPage;
            if (adsource.IsFirstPage == true)
                lnkbtnPrevious.Enabled = false;
            else
                lnkbtnPrevious.Enabled = true;
            if (adsource.IsLastPage == true)
                lnkbtnNext.Enabled = false;
            else
                lnkbtnNext.Enabled = true;
            dtlstYear.DataSource = adsource;
            dtlstYear.DataBind();
        }
        pos = (int)this.ViewState["ViewStateYears"];
    }
    public void BindYears()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Year", typeof(string));
        
        year = System.DateTime.Now.Year;
        for (int i = 1900; i <= year; i++)
        {
            dr = dt.NewRow();
            dr[0] = Convert.ToString(i);
            dt.Rows.Add(dr);
        }
        adsource = new PagedDataSource();
        adsource.DataSource = dt.DefaultView;
        adsource.PageSize =12;
        adsource.AllowPaging = true;

        adsource.CurrentPageIndex = pos;
        if (adsource.IsFirstPage == true)
            lnkbtnPrevious.Enabled = false;
        else
            lnkbtnPrevious.Enabled = true;
        if (adsource.IsLastPage == true)
            lnkbtnNext.Enabled = false;
        else
            lnkbtnNext.Enabled = true;
        dtlstYear.DataSource = adsource;
        dtlstYear.DataBind();
        //string val = "";
    }
    protected void lnkbtnNext_Click(object sender, EventArgs e)
    {
        pos = (int)this.ViewState["ViewStateYears"];
        pos += 1;
        this.ViewState["ViewStateYears"] = pos;
        BindYears(); ;
    }

    protected void lnkbtnPrevious_Click(object sender, EventArgs e)
    {
        pos = (int)this.ViewState["ViewStateYears"];
        pos -= 1;
        this.ViewState["ViewStateYears"] = pos;
        BindYears();
    }

    protected void YearlyReportSummary(int Year, LinkButton lnkbtnCalendarYear)
    {
        try
        {
            string[] parameter = { "@Flag", "@ReportType", "@ReportYear" };
            string[] value = { "YearlyReportSummary", "Yearly",Convert.ToString(Year) };
            DB_Status dbs = dba.sp_populateDataSet("SP_Report", 3, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                System.Data.DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    if (dt !=null && dt.Rows.Count >0)
                        lnkbtnCalendarYear.BackColor = System.Drawing.Color.MediumSpringGreen;
                    else
                        lnkbtnCalendarYear.BackColor = System.Drawing.Color.Empty;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void dtlstYear_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnkbtnCalendarYear = (LinkButton)e.Item.FindControl("lnkbtnCalendarYear");
            YearlyReportSummary(Convert.ToInt32(lnkbtnCalendarYear.Text), lnkbtnCalendarYear);
        }
    }
    protected void lnkbtnCalendarYear_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtnCalendarYear = sender as LinkButton;

        Literal ltrReports = this.Parent.FindControl("ltrReports") as Literal;
        Label lblType = this.Parent.FindControl("lblType") as Label;
        Label lblDate = this.Parent.FindControl("lblDate") as Label;
        lblType.Text = "Yearly";
        lblDate.Text = lnkbtnCalendarYear.Text.Trim();
        ltrReports.Text = "";
        LoadYearWiseReport(ltrReports,Convert.ToInt32(lnkbtnCalendarYear.Text.Trim()));
    }

    protected void LoadYearWiseReport(Literal ltrReports,int ReportYear)
    {
        try
        {
            string[] parameter = { "@Flag", "@ReportType","@ReportYear" };
            string[] value = { "YearWiseReport", "Yearly",Convert.ToString(ReportYear) };
            DB_Status dbs = dba.sp_populateDataSet("SP_Report",3, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ltrReports.Text = "";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ltrReports.Text = ltrReports.Text + "<table class='downloadbox'>";
                            ltrReports.Text = ltrReports.Text + "<tr>";
                            ltrReports.Text = ltrReports.Text + "<td class='title'>" + Convert.ToString(dt.Rows[i]["ReportTitleEnglish"]) + "</td>";
                            ltrReports.Text = ltrReports.Text + "<td class='download'><a target='_blank' href='" + Convert.ToString(dt.Rows[i]["AttachmentUrl"]) + "'><img src='images/download-white.png' alt='' /></a></td>";
                            ltrReports.Text = ltrReports.Text + "</tr>";
                            ltrReports.Text = ltrReports.Text + "</table>";
                        }
                    }
                    else
                    {
                        ltrReports.Text = "Record not found";
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

}