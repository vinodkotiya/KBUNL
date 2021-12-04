using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WCCommon_MonthCalendar : System.Web.UI.UserControl
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string Year =Convert.ToString(System.DateTime.Now.Year);
            lblYear.Text = Year;
            MonthlyReportSummary();
        }
    }

    protected void lnkbtnNext_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(lblYear.Text.Trim()))
        {
            int Year = Convert.ToInt32(lblYear.Text.Trim());
            lblYear.Text =Convert.ToString(Year + 1);
            MonthlyReportSummary();
        }
    }

    protected void lnkbtnPrevious_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(lblYear.Text.Trim()))
        {
            int Year = Convert.ToInt32(lblYear.Text.Trim());
            if(Year >0)
             lblYear.Text = Convert.ToString(Year - 1);
            MonthlyReportSummary();
        }
    }

    protected void MonthlyReportSummary()
    {
        try
        {
            string[] parameter = { "@Flag", "@ReportType", "@ReportYear" };
            string[] value = { "MonthlyReportSummary", "Monthly",lblYear.Text.Trim() };
            DB_Status dbs = dba.sp_populateDataSet("SP_Report",3, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                System.Data.DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    lnkbtnJan.BackColor = System.Drawing.Color.Empty;
                    lnkbtnFeb.BackColor = System.Drawing.Color.Empty;
                    lnkbtnMar.BackColor = System.Drawing.Color.Empty;
                    lnkbtnApr.BackColor = System.Drawing.Color.Empty;
                    lnkbtnMay.BackColor = System.Drawing.Color.Empty;
                    lnkbtnJun.BackColor = System.Drawing.Color.Empty;
                    lnkbtnJul.BackColor = System.Drawing.Color.Empty;
                    lnkbtnAug.BackColor = System.Drawing.Color.Empty;
                    lnkbtnSep.BackColor = System.Drawing.Color.Empty;
                    lnkbtnOct.BackColor = System.Drawing.Color.Empty;
                    lnkbtnNov.BackColor = System.Drawing.Color.Empty;
                    lnkbtnDec.BackColor = System.Drawing.Color.Empty;
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string Month = Convert.ToString(dt.Rows[i]["ReportMonth"]);
                            if(Month==hdfJan.Value)
                                lnkbtnJan.BackColor= System.Drawing.Color.MediumSpringGreen; 
                            else if(Month==hdfFeb.Value)
                                lnkbtnFeb.BackColor = System.Drawing.Color.MediumSpringGreen;
                            else if (Month == hdfMar.Value)
                                lnkbtnMar.BackColor = System.Drawing.Color.MediumSpringGreen;
                            else if (Month == hdfApr.Value)
                                lnkbtnApr.BackColor = System.Drawing.Color.MediumSpringGreen;
                            else if (Month == hdfMay.Value)
                                lnkbtnMay.BackColor = System.Drawing.Color.MediumSpringGreen;
                            else if (Month == hdfjun.Value)
                                lnkbtnJun.BackColor = System.Drawing.Color.MediumSpringGreen;
                            else if (Month == hdfJul.Value)
                                lnkbtnJul.BackColor = System.Drawing.Color.MediumSpringGreen;
                            else if (Month == hdfAug.Value)
                                lnkbtnAug.BackColor = System.Drawing.Color.MediumSpringGreen;
                            else if (Month == hdfSep.Value)
                                lnkbtnSep.BackColor = System.Drawing.Color.MediumSpringGreen;
                            else if (Month == hdfOct.Value)
                                lnkbtnOct.BackColor = System.Drawing.Color.MediumSpringGreen;
                            else if (Month == hdfNov.Value)
                                lnkbtnNov.BackColor = System.Drawing.Color.MediumSpringGreen;
                            else if (Month == hdfDec.Value)
                                lnkbtnDec.BackColor = System.Drawing.Color.MediumSpringGreen;
                        }
                    }
                    else
                    {
                       
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void LoadMonthWiseReport(Literal ltrReports, string ReportMonth,int ReportYear)
    {
        try
        {
            string[] parameter = { "@Flag", "@ReportType", "@ReportMonth", "@ReportYear" };
            string[] value = { "MonthWiseReport", "Monthly", ReportMonth,Convert.ToString(ReportYear) };
            DB_Status dbs = dba.sp_populateDataSet("SP_Report",4, parameter, value);
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
    protected void lnkbtnJan_Click(object sender, EventArgs e)
    {
        Literal ltrReports = this.Parent.FindControl("ltrReports") as Literal;
        Label lblType = this.Parent.FindControl("lblType") as Label;
        Label lblDate = this.Parent.FindControl("lblDate") as Label;
        lblType.Text = "Monthly";
        lblDate.Text = "Jan " + lblYear.Text.Trim();
        ltrReports.Text = "";
        LoadMonthWiseReport(ltrReports,"01", Convert.ToInt32(lblYear.Text.Trim()));
    }

    protected void lnkbtnFeb_Click(object sender, EventArgs e)
    {
        Literal ltrReports = this.Parent.FindControl("ltrReports") as Literal;
        Label lblType = this.Parent.FindControl("lblType") as Label;
        Label lblDate = this.Parent.FindControl("lblDate") as Label;
        lblType.Text = "Monthly";
        lblDate.Text = "Feb " + lblYear.Text.Trim();
        ltrReports.Text = "";
        LoadMonthWiseReport(ltrReports, "02", Convert.ToInt32(lblYear.Text.Trim()));
    }

    protected void lnkbtnMar_Click(object sender, EventArgs e)
    {
        Literal ltrReports = this.Parent.FindControl("ltrReports") as Literal;
        Label lblType = this.Parent.FindControl("lblType") as Label;
        Label lblDate = this.Parent.FindControl("lblDate") as Label;
        lblType.Text = "Monthly";
        lblDate.Text = "Mar " + lblYear.Text.Trim();
        ltrReports.Text = "";
        LoadMonthWiseReport(ltrReports, "03", Convert.ToInt32(lblYear.Text.Trim()));
    }

    protected void lnkbtnApr_Click(object sender, EventArgs e)
    {
        Literal ltrReports = this.Parent.FindControl("ltrReports") as Literal;
        Label lblType = this.Parent.FindControl("lblType") as Label;
        Label lblDate = this.Parent.FindControl("lblDate") as Label;
        lblType.Text = "Monthly";
        lblDate.Text = "Apr " + lblYear.Text.Trim();
        ltrReports.Text = "";
        LoadMonthWiseReport(ltrReports, "04", Convert.ToInt32(lblYear.Text.Trim()));
    }

    protected void lnkbtnMay_Click(object sender, EventArgs e)
    {
        Literal ltrReports = this.Parent.FindControl("ltrReports") as Literal;
        Label lblType = this.Parent.FindControl("lblType") as Label;
        Label lblDate = this.Parent.FindControl("lblDate") as Label;
        lblType.Text = "Monthly";
        lblDate.Text = "May " + lblYear.Text.Trim();
        ltrReports.Text = "";
        LoadMonthWiseReport(ltrReports, "05", Convert.ToInt32(lblYear.Text.Trim()));
    }

    protected void lnkbtnJun_Click(object sender, EventArgs e)
    {
        Literal ltrReports = this.Parent.FindControl("ltrReports") as Literal;
        Label lblType = this.Parent.FindControl("lblType") as Label;
        Label lblDate = this.Parent.FindControl("lblDate") as Label;
        lblType.Text = "Monthly";
        lblDate.Text = "Jun " + lblYear.Text.Trim();
        ltrReports.Text = "";
        LoadMonthWiseReport(ltrReports, "06", Convert.ToInt32(lblYear.Text.Trim()));
    }

    protected void lnkbtnJul_Click(object sender, EventArgs e)
    {
        Literal ltrReports = this.Parent.FindControl("ltrReports") as Literal;
        Label lblType = this.Parent.FindControl("lblType") as Label;
        Label lblDate = this.Parent.FindControl("lblDate") as Label;
        lblType.Text = "Monthly";
        lblDate.Text = "Jul " + lblYear.Text.Trim();
        ltrReports.Text = "";
        LoadMonthWiseReport(ltrReports, "07", Convert.ToInt32(lblYear.Text.Trim()));
    }

    protected void lnkbtnAug_Click(object sender, EventArgs e)
    {
        Literal ltrReports = this.Parent.FindControl("ltrReports") as Literal;
        Label lblType = this.Parent.FindControl("lblType") as Label;
        Label lblDate = this.Parent.FindControl("lblDate") as Label;
        lblType.Text = "Monthly";
        lblDate.Text = "Aug " + lblYear.Text.Trim();
        ltrReports.Text = "";
        LoadMonthWiseReport(ltrReports, "08", Convert.ToInt32(lblYear.Text.Trim()));
    }

    protected void lnkbtnSep_Click(object sender, EventArgs e)
    {
        Literal ltrReports = this.Parent.FindControl("ltrReports") as Literal;
        Label lblType = this.Parent.FindControl("lblType") as Label;
        Label lblDate = this.Parent.FindControl("lblDate") as Label;
        lblType.Text = "Monthly";
        lblDate.Text = "Sep " + lblYear.Text.Trim();
        ltrReports.Text = "";
        LoadMonthWiseReport(ltrReports, "09", Convert.ToInt32(lblYear.Text.Trim()));
    }

    protected void lnkbtnOct_Click(object sender, EventArgs e)
    {
        Literal ltrReports = this.Parent.FindControl("ltrReports") as Literal;
        Label lblType = this.Parent.FindControl("lblType") as Label;
        Label lblDate = this.Parent.FindControl("lblDate") as Label;
        lblType.Text = "Monthly";
        lblDate.Text = "Oct " + lblYear.Text.Trim();
        ltrReports.Text = "";
        LoadMonthWiseReport(ltrReports, "10", Convert.ToInt32(lblYear.Text.Trim()));
    }

    protected void lnkbtnNov_Click(object sender, EventArgs e)
    {
        Literal ltrReports = this.Parent.FindControl("ltrReports") as Literal;
        Label lblType = this.Parent.FindControl("lblType") as Label;
        Label lblDate = this.Parent.FindControl("lblDate") as Label;
        lblType.Text = "Monthly";
        lblDate.Text = "Nov " + lblYear.Text.Trim();
        ltrReports.Text = "";
        LoadMonthWiseReport(ltrReports, "11", Convert.ToInt32(lblYear.Text.Trim()));
    }

    protected void lnkbtnDec_Click(object sender, EventArgs e)
    {
        Literal ltrReports = this.Parent.FindControl("ltrReports") as Literal;
        Label lblType = this.Parent.FindControl("lblType") as Label;
        Label lblDate = this.Parent.FindControl("lblDate") as Label;
        lblType.Text = "Monthly";
        lblDate.Text = "Dec " + lblYear.Text.Trim();
        ltrReports.Text = "";
        LoadMonthWiseReport(ltrReports, "12", Convert.ToInt32(lblYear.Text.Trim()));
    }
}