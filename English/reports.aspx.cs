using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class reports : System.Web.UI.Page
{
    Class1 obj = new Class1();
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
        lnkbtnBack.Visible = false;
        if (Request.QueryString["Level"] != null && Request.QueryString["ParentReportID"] != null)
        {
            hdfReportLevel.Value = Convert.ToString(Request.QueryString["Level"]);
            hdfParentReportId.Value = Convert.ToString(Request.QueryString["ParentReportID"]);
            if (hdfReportLevel.Value == "1" || hdfReportLevel.Value == "0")
            {
                lnkbtnBack.Visible = false;
            }
            else
            {
                lnkbtnBack.Visible = true;
                lnkbtnBack.HRef = "reports.aspx?Level=" + (Convert.ToInt16(hdfReportLevel.Value) - 1).ToString() + "&ParentReportID=" + GetParentID(hdfParentReportId.Value);
            }
        }
        if (!IsPostBack)
        {
            BindDepartment();
            LoadReports();
            lblReportTitle.Text = "Report Title";
            lblDate.Text = "";
        }
    }

    protected void BindDepartment()
    {
        try
        {
            string[] param = { "@Flag" };
            string[] values = { "LoadDropDownValues" };
            DB_Status DBS = dba.sp_populateDataSet("SP_Employees_Admin", 1, param, values);
            if (DBS.OperationStatus.ToString() == "Success")
            {
                DataSet ds = DBS.ResultDataSet;
                //Table index 0 for Department
                ddlDepartment.DataSource = ds.Tables[0];
                ddlDepartment.DataTextField = "Department";
                ddlDepartment.DataValueField = "DeptID";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, "All");
                ddlDepartment.Items[0].Value = "0";
            }
        }
        catch (Exception ex)
        {

        }
    }

    StringBuilder SearchCondition()
    {
        //sb.Append(" AND CONVERT(NCHAR(10),ReportDate,126)='" + obj.makedate(txtDate.Text.Trim()) + "'");
        StringBuilder sb = new StringBuilder();
        try
        {
            if (Request.QueryString["Level"] != null && Request.QueryString["ParentReportID"] != null)
            {
                sb.Append(" AND ReportLevel=" + hdfReportLevel.Value + " AND ParentReportID=" + Convert.ToInt32(hdfParentReportId.Value));
                ddlRptType.SelectedIndex = 3;
                lblType.Text = "Other";
                calendar.Visible = false;
                WCYearCalendar.Visible = false;
                WCMonthCalendar.Visible = false;
            }
            else
            {
                sb.Append(" AND (ReportLevel = 1 OR ReportLevel IS NULL)  AND ( ParentReportID = 0 OR ParentReportID IS NULL)");
                if (ddlRptType.SelectedIndex == 0)
                    lblType.Text = "Daily";
                else if (ddlRptType.SelectedIndex == 1)
                    lblType.Text = "Monthly";
                else if (ddlRptType.SelectedIndex == 2)
                    lblType.Text = "Yearly";
                else if (ddlRptType.SelectedIndex == 3)
                    lblType.Text = "Others";
            }
            if (ddlDepartment.SelectedIndex > 0)
                sb.Append(" AND DeptId=" + Convert.ToInt32(ddlDepartment.SelectedValue));
            //if (ddlRptType.SelectedIndex > 0)
                sb.Append(" AND ReportType='" + Convert.ToString(ddlRptType.SelectedValue) + "'");
            if (ddlRptType.SelectedIndex == 1)
            {
                if (!string.IsNullOrEmpty(txtDate.Text.Trim()))
                {
                    string SelectedDate = txtDate.Text.Trim();
                    string dd = SelectedDate.Substring(0, 2);
                    string mm = SelectedDate.Substring(3, 2);
                    string yy = SelectedDate.Substring(6, 4);
                    sb.Append(" and CONVERT(NCHAR(10),ReportDate,126) >= '" + (yy + "-" + mm + "-" + dd) + "' ");
                }
            }
            if (ddlRptType.SelectedIndex == 2)
            {
                string dateMonth = txtMonth.Text.Trim();
                string Month = dateMonth.Substring(0, 2);
                string year = dateMonth.Substring(3, 4);
                if (!string.IsNullOrEmpty(Month) && !string.IsNullOrEmpty(year))
                    sb.Append(" AND ReportMonth='" + Month + "' AND ReportYear=" + Convert.ToInt32(year));
            }
            if (ddlRptType.SelectedIndex == 3)
            {
                sb.Append(" AND ReportYear=" + Convert.ToInt32(txtYear.Text.Trim()));
            }
        }
        catch (Exception ex)
        {

        }
        return sb;
    }
    protected void LoadReports()
    {
        try
        {
            string[] parameter = { "@Flag", "@Search" };
            string[] value = { "PublicView", Convert.ToString(SearchCondition()) };
            DB_Status dbs = dba.sp_populateDataSet("SP_Report", 2, parameter, value);
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
                            
                            string ReportGroup = Convert.ToString(dt.Rows[i]["ReportGroup"]);
                            if (ReportGroup == "True")
                            {
                                ltrReports.Text = ltrReports.Text + "<td class='title'>" + Convert.ToString(dt.Rows[i]["ReportTitleEnglish"]) + "</td>";
                                ltrReports.Text = ltrReports.Text + "<td class='download'><a href='" + Convert.ToString(dt.Rows[i]["NextLevelLink"]) + "'><img src='../images/Report.png' alt='' /></a></td>";
                            }
                            else if (ReportGroup == "False" || ReportGroup == "")
                            {
                                ltrReports.Text = ltrReports.Text + "<td class='title'><a target='_blank' href='../" + Convert.ToString(dt.Rows[i]["AttachmentUrl"]) + "'>" + Convert.ToString(dt.Rows[i]["ReportTitleEnglish"]) + "</a></td>";
                                ltrReports.Text = ltrReports.Text + "<td class='download'><a target='_blank' href='../" + Convert.ToString(dt.Rows[i]["AttachmentUrl"]) + "'><img src='../images/download-white.png' alt='' /></a></td>";
                            }
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

    protected void ddlRptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRptType.SelectedIndex == 0)
        {
            lblreportType.Text = "Report Date:";
            lblDate.Text = "";
            calendar.Visible = true;
            WCYearCalendar.Visible = false;
            WCMonthCalendar.Visible = false;
        }
        else if (ddlRptType.SelectedIndex == 1)
        {
            lblreportType.Text = "Report Month:";
            lblDate.Text = "";
            calendar.Visible = false;
            WCYearCalendar.Visible = false;
            WCMonthCalendar.Visible = true;
        }
        else if (ddlRptType.SelectedIndex == 2)
        {
            lblreportType.Text = "Report Year:";
            lblDate.Text = "";

            calendar.Visible = false;
            WCYearCalendar.Visible = true;
            WCMonthCalendar.Visible = false;
        }
        else
        {
            txtDate.Visible = false;
            txtMonth.Visible = false;
            txtYear.Visible = false;

            calendar.Visible = false;
            WCYearCalendar.Visible = false;
            WCMonthCalendar.Visible = false;
            lblType.Text = "Others";
        }
        txtDate.Text = "";
        txtMonth.Text = "";
        txtYear.Text = "";
        LoadReports();
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        LoadReports();
        if (ddlDepartment.SelectedIndex > 0)
            lblReportTitle.Text = ddlDepartment.SelectedItem.Text + " Department";
        else
            lblReportTitle.Text = "Report Title";
        lblType.Text = "Daily";
        lblDate.Text = txtDate.Text.Trim();
    }
    protected void txtMonth_TextChanged(object sender, EventArgs e)
    {
        LoadReports();
        if (ddlDepartment.SelectedIndex > 0)
            lblReportTitle.Text = ddlDepartment.SelectedItem.Text + " Department";
        else
            lblReportTitle.Text = "Report Title";
        lblType.Text = "Monthly";
        lblDate.Text = txtMonth.Text.Trim();
    }
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        LoadReports();
        if (ddlDepartment.SelectedIndex > 0)
            lblReportTitle.Text = ddlDepartment.SelectedItem.Text + " Department";
        else
            lblReportTitle.Text = "Report Title";
        lblType.Text = "Yearly";
        lblDate.Text = txtYear.Text.Trim();
    }
    //------------------- Control Calendar Functionality--------------
    protected void calendar_DayRender(object sender, DayRenderEventArgs e)
    {
        try
        {
            Literal ltr = new Literal(); //Creating a literal  
            ltr.Visible = true;
            ltr.Text = "<br/>"; //for breaking the line in cell  
            e.Cell.Controls.Add(ltr); //adding in all cell  
            string[] parameter = { "@Flag", "@Search" };
            string[] value = { "PublicView", Convert.ToString(SearchCondition()) };
            DB_Status dbs = dba.sp_populateDataSet("SP_Report", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; dt.Rows.Count > i; i++)
                        {
                            string x = Convert.ToString(dt.Rows[i]["ReportDate"]).Trim();
                            string y = e.Day.Date.ToString("dd/MM/yyyy");
                            if (x == y)
                            {
                                e.Cell.BackColor = System.Drawing.Color.MediumSpringGreen;
                                e.Cell.ToolTip = Convert.ToString(dt.Rows[i]["ReportTitleEnglish"]);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void calendar_SelectionChanged(object sender, EventArgs e)
    {
        try
        {
            string Search = "";
            string SelectedDate = calendar.SelectedDate.ToString("dd/MM/yyyy");
            string ddisplay= calendar.SelectedDate.ToString("dd/MMM/yyyy");
            string displaydate = ddisplay.Replace("/", " ");
            string dd = SelectedDate.Substring(0, 2);
            string mm = SelectedDate.Substring(3, 2);
            string yy = SelectedDate.Substring(6, 4);
            lblDate.Text = displaydate.Trim();
            if (!string.IsNullOrEmpty(dd) && !string.IsNullOrEmpty(mm) && !string.IsNullOrEmpty(yy))
            {
              Search=(" and CONVERT(NCHAR(10),ReportDate,126) = '" + (yy + "-" + mm + "-" + dd) + "' ");
            }

            string[] parameter = { "@Flag", "@Search" };
            string[] value = { "PublicView", Search };
            DB_Status dbs = dba.sp_populateDataSet("SP_Report", 2, parameter, value);
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

                            string ReportGroup = Convert.ToString(dt.Rows[i]["ReportGroup"]);
                            if (ReportGroup == "True")
                            {
                                ltrReports.Text = ltrReports.Text + "<td class='title'>" + Convert.ToString(dt.Rows[i]["ReportTitleEnglish"]) + "</td>";
                                ltrReports.Text = ltrReports.Text + "<td class='download'><a href='" + Convert.ToString(dt.Rows[i]["NextLevelLink"]) + "'><img src='../images/Report.png' alt='' /></a></td>";
                            }
                            else if (ReportGroup == "False" || ReportGroup == "")
                            {
                                ltrReports.Text = ltrReports.Text + "<td class='title'><a target='_blank' href='../" + Convert.ToString(dt.Rows[i]["AttachmentUrl"]) + "'>" + Convert.ToString(dt.Rows[i]["ReportTitleEnglish"]) + "</a></td>";
                                ltrReports.Text = ltrReports.Text + "<td class='download'><a target='_blank' href='../" + Convert.ToString(dt.Rows[i]["AttachmentUrl"]) + "'><img src='../images/download-white.png' alt='' /></a></td>";
                            }
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
    protected string GetParentID(string ReportID)
    {
        string ParentID = "0";
        try
        {
            string[] parameter = { "@Flag", "@ReportId" };
            string[] value = { "GetParentID", ReportID };
            DB_Status dbs = dba.sp_readSingleData("SP_Report", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                ParentID = dbs.SingleResult;
            }
        }
        catch (Exception ex)
        {

        }
        return ParentID;
    }
}