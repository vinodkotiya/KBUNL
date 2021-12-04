using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    DB_Access dba = new DB_Access();
    public StringBuilder sb_links = new StringBuilder();
    public StringBuilder sb_report = new StringBuilder();

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
        if (!IsPostBack)
        {
            linkBack.Visible = false;
            if (Request.QueryString["dptId"] != null)
            {
                hdfDeptId.Value = Convert.ToString(Request.QueryString["dptId"]);
                divMoreinformation.Visible = false;
                LoadDepartmentHomePageData();
                LoadDocumentTabs();
                linkOurTeam.HRef = "directory.aspx?dptId=" + hdfDeptId.Value;
            }
            else
            {
                Response.Redirect("~/Hindi/Default.aspx");
            }
        }
    }

    public void LoadDepartmentHomePageData()
    {
        try
        {
            string IPAdd;
            IPAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAdd))
                IPAdd = Request.ServerVariables["REMOTE_ADDR"];

            string[] parameter = { "@DeptID","@IPAddress" };
            string[] value = { hdfDeptId.Value, IPAdd };
            DB_Status dbs = dba.sp_populateDataSet("SP_LoadData_For_Dept_HomePage", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    #region Department
                    DataTable dtDept = ds.Tables[0];
                    if (dtDept != null && dtDept.Rows.Count > 0)
                    {
                        lblDepartmentName.Text = Convert.ToString(dtDept.Rows[0]["DepartmentH"]) + " ";
                        lblDptNameBrdCrum.Text = Convert.ToString(dtDept.Rows[0]["DepartmentH"]) + " ";
                    }
                    #endregion

                    #region Department Details
                    DataTable dtDeptDetails = ds.Tables[1];
                    if (dtDeptDetails != null && dtDeptDetails.Rows.Count > 0)
                    {
                        if (dtDeptDetails.Rows[0]["AboutDeptHindi"].ToString() != "")
                        {
                            divAboutDepartment.InnerHtml = Convert.ToString(dtDeptDetails.Rows[0]["AboutDeptHindi"]);
                            divAboutDepartment.Visible = true;
                        }
                        else
                        {
                            divAboutDepartment.Visible = false;
                        }

                        if (dtDeptDetails.Rows[0]["MoreInfoHindi"].ToString() != "")
                        {
                            divMoreinformation.InnerHtml = "<h4>विभाग के बारे में जानकारी</h4><div style='padding-top:10px;'>" + Convert.ToString(dtDeptDetails.Rows[0]["MoreInfoHindi"]) + "</div>";
                            divMoreinformation.Visible = true;
                        }
                        else
                        {
                            divMoreinformation.Visible = false;
                        }
                    }
                    #endregion

                    #region Department Photo Gallery
                    //DataTable dtPhotoGallery = ds.Tables[2];
                    //if (dtPhotoGallery != null && dtPhotoGallery.Rows.Count > 0)
                    //{
                    //    ltrPhotoGallery.Text = "<div id='owl-project' class='owl-carousel'>";
                    //    for (int i = 0; i < dtPhotoGallery.Rows.Count; i++)
                    //    {
                    //        ltrPhotoGallery.Text = ltrPhotoGallery.Text + "<div class='item'>";
                    //        ltrPhotoGallery.Text = ltrPhotoGallery.Text + "<img src='../" + Convert.ToString(dtPhotoGallery.Rows[i]["PhotoPath"]) + "' alt='' />";
                    //        ltrPhotoGallery.Text = ltrPhotoGallery.Text + "</div>";
                    //    }
                    //    ltrPhotoGallery.Text = ltrPhotoGallery.Text + "</div>";
                    //    lnkbtnViewAll.Visible = true;
                    //}
                    //else
                    //{
                    //    lnkbtnViewAll.Visible = false;
                    //}
                    #endregion

                    #region Department Highlight
                    DataTable dtHighLight = ds.Tables[3];
                    ltrHighlight.Text = "<table>";
                    for (int j = 0; j < dtHighLight.Rows.Count; j++)
                    {
                        string ImagePath = dtHighLight.Rows[j]["HighlightImage"].ToString();

                        ltrHighlight.Text = ltrHighlight.Text + "<tr>";
                        ltrHighlight.Text = ltrHighlight.Text + "<td class='content'>";
                        ltrHighlight.Text = ltrHighlight.Text + "<h5>" + Convert.ToString(dtHighLight.Rows[j]["HighlightDate"]) + "</h5>";
                        if (ImagePath != "NA" && ImagePath != "")
                        {
                            ltrHighlight.Text = ltrHighlight.Text + "<img src='" + Convert.ToString(dtHighLight.Rows[j]["HighlightImage"]) + "' alt='' style='width:140px; float:left; margin-right:10px; margin-bottom:0px;'/>";
                        }
                        ltrHighlight.Text = ltrHighlight.Text + "<p>" + Convert.ToString(dtHighLight.Rows[j]["DescriptionH"]) + " ";
                        ltrHighlight.Text = ltrHighlight.Text + "</td>";
                        ltrHighlight.Text = ltrHighlight.Text + "</tr>";
                    }
                    ltrHighlight.Text = ltrHighlight.Text + "</table>";
                    #endregion

                    #region Department News
                    DataTable dtNews = ds.Tables[4];
                    //ltrNews.Text = "";
                    //for (int j = 0; j < dtNews.Rows.Count; j++)
                    //{
                    //    ltrNews.Text = ltrNews.Text + "<table class='box'>";
                    //    ltrNews.Text = ltrNews.Text + "<tr>";
                    //    ltrNews.Text = ltrNews.Text + "<td>";
                    //    ltrNews.Text = ltrNews.Text + "<div class='title'>" + Convert.ToString(dtNews.Rows[j]["TitleHindi"]) + "</div>";
                    //    ltrNews.Text = ltrNews.Text + "<div class='postdate'>" + Convert.ToString(dtNews.Rows[j]["UploadedOn"]) + "</div>";
                    //    ltrNews.Text = ltrNews.Text + "</td>";
                    //    ltrNews.Text = ltrNews.Text + "<td class='download'>";
                    //    string Attachment = Convert.ToString(dtNews.Rows[j]["Attachment"]);
                    //    if (!string.IsNullOrEmpty(Attachment))
                    //        ltrNews.Text = ltrNews.Text + "<a href='../" + Convert.ToString(dtNews.Rows[j]["Attachment"]) + "' target='_blank' ><img src='../images/download.png' alt=''></a>";
                    //    ltrNews.Text = ltrNews.Text + "</td>";
                    //    ltrNews.Text = ltrNews.Text + "</tr>";
                    //    ltrNews.Text = ltrNews.Text + "</table>";
                    //}

                    #endregion

                    #region Department Report
                    //DataTable dtReport = ds.Tables[5];
                    //ltrReport.Text = "";
                    //if (dtReport != null && dtReport.Rows.Count > 0)
                    //{
                    //for (int j = 0; j < dtReport.Rows.Count; j++)
                    //{
                    //    ltrReport.Text = ltrReport.Text + "<table class='box'>";
                    //    ltrReport.Text = ltrReport.Text + "<tr>";
                    //    ltrReport.Text = ltrReport.Text + "<td>";
                    //    ltrReport.Text = ltrReport.Text + "<div class='title'>" + Convert.ToString(dtReport.Rows[j]["ReportTitleHindi"]) + "</div>";
                    //    ltrReport.Text = ltrReport.Text + "<div class='postdate'>" + Convert.ToString(dtReport.Rows[j]["ReportDate"]) + "</div>";
                    //    ltrReport.Text = ltrReport.Text + "</td>";
                    //    ltrReport.Text = ltrReport.Text + "<td class='download'>";
                    //    ltrReport.Text = ltrReport.Text + "<a href='../" + Convert.ToString(dtReport.Rows[j]["AttachmentUrl"]) + "' target ='_blank'><img src='../images/download.png' alt=''/></a>";
                    //    ltrReport.Text = ltrReport.Text + "</td>";
                    //    ltrReport.Text = ltrReport.Text + "</tr>";
                    //    ltrReport.Text = ltrReport.Text + "</table>";
                    //}
                    //    lnkbtnViewReport.Visible = true;
                    //}
                    //else
                    //{
                    //    lnkbtnViewReport.Visible = false;
                    //}
                    #endregion

                    #region Department Report
                    DataTable dtBanner = ds.Tables[6];
                    string HTMLCode = "";
                    if (dtBanner != null && dtBanner.Rows.Count > 0)
                    {
                        HTMLCode += "<ul class='slider'>";
                        for (int j = 0; j < dtBanner.Rows.Count; j++)
                        {
                            HTMLCode += "<li><img src='" + dtBanner.Rows[j]["BannerPath"].ToString() + "' title='" + dtBanner.Rows[j]["BannerTextH"] + "'></li>";
                        }
                        HTMLCode += "</ul>";
                    }
                    ntpcbanner.InnerHtml = HTMLCode;
                    #endregion

                    divDeptVisitors.InnerHtml = "विभाग के कुल विजिटर : " + ds.Tables[7].Rows[0]["TotalVisitors"].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void LoadDocumentTabs()
    {
        try
        {
            string[] parameter = {"@Flag", "@DeptID" };
            string[] value = { "PublicPage_DocumentCategory", hdfDeptId.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_DocLibrary", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    repeater_DocumentTab.DataSource = dt;
                    repeater_DocumentTab.DataBind();

                    if(dt.Rows.Count>0)
                    {
                        HiddenField hfDocLevelCode_forChild = (HiddenField)repeater_DocumentTab.Items[0].FindControl("hfDocLevelCode_forChild");
                        LoadDocuments(hfDocLevelCode_forChild.Value);
                    }
                }
            }
            LinkButton lbtnDocumentTab = (LinkButton)repeater_DocumentTab.Items[0].FindControl("linktab");
            lbtnDocumentTab.CssClass = "active";
            hfActiveTabIndex.Value = "0";
        }
        catch (Exception ex)
        {

        }
    }
    protected void LoadDocuments(string DocLevelCode_forChild)
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID", "@DocLevelCode" };
            string[] value = { "PublicPage_Documents", hdfDeptId.Value, DocLevelCode_forChild };
            DB_Status dbs = dba.sp_populateDataSet("SP_DocLibrary", 3, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    repeater_Document.DataSource = dt;
                    repeater_Document.DataBind();
                }
            }

            if (DocLevelCode_forChild.Contains("Level:1"))
                linkBack.Visible = false;
            else if (DocLevelCode_forChild.Contains("Level:2"))
                linkBack.Visible = false;
            else if (DocLevelCode_forChild.Contains("Level:0"))
                linkBack.Visible = false;
            else
                linkBack.Visible = true;

            int activetabindex = Convert.ToInt16(hfActiveTabIndex.Value);
            LinkButton lbtnDocumentTab = (LinkButton)repeater_DocumentTab.Items[activetabindex].FindControl("linktab");
            lbtnDocumentTab.CssClass = "active";
        }
        catch (Exception ex)
        {

        }
    }
    protected void View_Click(object sender, EventArgs e)
    {
        try
        {
            string DocID = (sender as LinkButton).CommandArgument;
            LinkButton linktab = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)linktab.NamingContainer;
            HiddenField hfDocLevelCode = (HiddenField)rptItem.FindControl("hfDocLevelCode");
            hfDocLevelCode_forParent_tmp.Value = hfDocLevelCode.Value;                  

            for (int i = 0; i < repeater_DocumentTab.Items.Count; i++)
            {
                LinkButton lbtnDocumentTab = (LinkButton)repeater_DocumentTab.Items[i].FindControl("linktab");
                lbtnDocumentTab.CssClass = "";
                if (linktab.Text == lbtnDocumentTab.Text)
                {
                    hfActiveTabIndex.Value = i.ToString();
                }
            }
            linktab.CssClass = "active";

            HiddenField hfDocLevelCode_forChild = (HiddenField)rptItem.FindControl("hfDocLevelCode_forChild");
            LoadDocuments(hfDocLevelCode_forChild.Value);
        }
        catch (Exception ex)
        {
        }
    }

    protected void ViewChild_Click(object sender, EventArgs e)
    {
        try
        {
            string DocID = (sender as LinkButton).CommandArgument;
            LinkButton linktab = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)linktab.NamingContainer;

            HiddenField hfDocLevelCode = (HiddenField)rptItem.FindControl("hfDocLevelCode");
            hfDocLevelCode_forParent_tmp.Value = hfDocLevelCode.Value;

            HiddenField hfDocLevelCode_forChild = (HiddenField)rptItem.FindControl("hfDocLevelCode_forChild");
            LoadDocuments(hfDocLevelCode_forChild.Value);
        }
        catch (Exception ex)
        {
        }
    }


    protected void lnkbtnViewAll_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(hdfDeptId.Value))
            Response.Redirect("Dept-Gallery.aspx?dptId=" + Convert.ToString(hdfDeptId.Value));
    }

    protected void lnkbtnViewReport_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(hdfDeptId.Value))
            Response.Redirect("Dept-Report.aspx?dptId=" + Convert.ToString(hdfDeptId.Value));
    }
    protected void linkBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (repeater_Document.Items.Count > 0)
            {
                HiddenField hfDocLevelCode_forParent = (HiddenField)repeater_Document.Items[0].FindControl("hfDocLevelCode_forParent");
                LoadDocuments(hfDocLevelCode_forParent.Value);
            }
            else
            {
                LoadDocuments(hfDocLevelCode_forParent_tmp.Value);
            }
        }
        catch(Exception ex)
        {
            LoadDocuments(hfDocLevelCode_forParent_tmp.Value);
        }
    }
}