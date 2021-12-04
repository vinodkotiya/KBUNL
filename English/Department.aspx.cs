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
    string DocLevelCodeChild = "";

    private void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["Theme"] == null)
            Session["Theme"] = "theme_blue";
        
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
                Session["Deptid"] = hdfDeptId.Value;
                LoadDepartmentHomePageData();
                LoadDocumentTabs();
                LoadDeptforHOP();
               if(hdfDeptId.Value!="21")
                {
                   
                    lblHopHeading.Text = "Docs for HOP DashBoard";
                    ViewDeptWiseHOPRpt();
                }
                
                linkOurTeam.HRef = "directory.aspx?dptId=" + hdfDeptId.Value;

                lblPassCodeMsg.Text = "";
                if (hdfDeptId.Value == "21")
                {
                     divSliderHigh.Visible = false;
                     WCEmpServices.Visible = false;
                     divGen.Visible = true;
                    divPassCode.Visible = true;
                    divGauge.Visible = true;
                    divDepartmentcontent.Visible = false;
                    
                }
                else
                {
                    divPassCode.Visible = false;
                    divDepartmentcontent.Visible = true;
                }
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

            string[] parameter = { "@DeptID", "@IPAddress" };
            string[] value = { hdfDeptId.Value, IPAdd };
            DB_Status dbs = dba.sp_populateDataSet("SP_LoadData_For_Dept_HomePage", 2, parameter, value);
             string[] parameter1 = { "@DeptID", "@flag" };
            string[] value1 = { hdfDeptId.Value, "show" };
            DB_Status dbs1= dba.sp_populateDataSet("Sp_Department_Admin", 2, parameter1, value1);
             divDeptAdmin.InnerHtml = "<h4>Department Homepage is Updated By:</h4><div style='padding-top:10px;'>" + dbs1.OperationStatus.ToString() + "</div>";
             if (dbs1.OperationStatus.ToString() == "Success")
            {
             
                             DataSet ds1 = dbs1.ResultDataSet;
                               if (ds1.Tables.Count > 0)
                               {
                                     DataTable dtAdmin = ds1.Tables[0];
                                      divDeptAdmin.InnerHtml = "<h4>Authorisation to Update Department Homepage:</h4><div style='padding-top:10px;'>" + "Authorisation Request Not Recieved from Dept" + "</div>";
                                 
                                     if (dtAdmin != null && dtAdmin.Rows.Count > 0)
                                          {
                                              string a = "";
                                              for (int i = 0; i < dtAdmin.Rows.Count; i++)
                                                 {
                                                        a = a +  Convert.ToString(dtAdmin.Rows[i]["EmpName"]) + " - " +  Convert.ToString(dtAdmin.Rows[i]["EmpCode"])  + "<br/>" ;
                                                 }
                                               divDeptAdmin.InnerHtml = "<h4>Authorisation to Update Department Homepage:</h4><div style='padding-top:10px;'>" + a + "</div>";
                                          }
                                 }
             }
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    #region Department
                    DataTable dtDept = ds.Tables[0];
                    if (dtDept != null && dtDept.Rows.Count > 0)
                    {
                        lblDepartmentName.Text = Convert.ToString(dtDept.Rows[0]["Department"]) + " ";
                        lblDptNameBrdCrum.Text = Convert.ToString(dtDept.Rows[0]["Department"]) + " ";
                    }
                    #endregion

                    #region Department Details
                    DataTable dtDeptDetails = ds.Tables[1];
                    if (dtDeptDetails != null && dtDeptDetails.Rows.Count > 0)
                    {
                        if (dtDeptDetails.Rows[0]["AboutDeptEnglish"].ToString() != "")
                        {
                            divAboutDepartment.InnerHtml = Convert.ToString(dtDeptDetails.Rows[0]["AboutDeptEnglish"]);
                            divAboutDepartment.Visible = true;
                        }
                        else
                        {
                            divAboutDepartment.Visible = false;
                        }
                        if (dtDeptDetails.Rows[0]["MoreInfoEnglish"].ToString() != "")
                        {
                            divMoreinformation.InnerHtml = "<h4>Information about department</h4><div style='padding-top:10px;'>" + Convert.ToString(dtDeptDetails.Rows[0]["MoreInfoEnglish"]) + "</div>";

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
                        ltrHighlight.Text = ltrHighlight.Text + "<p>" + Convert.ToString(dtHighLight.Rows[j]["DescriptionE"]) + " ";
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
                    //    ltrNews.Text = ltrNews.Text + "<div class='title'>" + Convert.ToString(dtNews.Rows[j]["TitleEnglish"]) + "</div>";
                    //    ltrNews.Text = ltrNews.Text + "<div class='postdate'>" + Convert.ToString(dtNews.Rows[j]["UploadedOn"]) + "</div>";
                    //    ltrNews.Text = ltrNews.Text + "</td>";
                    //    ltrNews.Text = ltrNews.Text + "<td class='download'>";
                    //    string Attachment = Convert.ToString(dtNews.Rows[j]["Attachment"]);
                    //    if (!string.IsNullOrEmpty(Attachment))
                    //        ltrNews.Text = ltrNews.Text + "<a href='../"+ Convert.ToString(dtNews.Rows[j]["Attachment"]) + "' target='_blank' ><img src='../images/download.png' alt=''></a>";
                    //    ltrNews.Text = ltrNews.Text + "</td>";
                    //    ltrNews.Text = ltrNews.Text + "</tr>";
                    //    ltrNews.Text = ltrNews.Text + "</table>";
                    //}

                    #endregion

                    #region Department Report
                    DataTable dtReport = ds.Tables[5];
                    //ltrReport.Text = "";
                    //if (dtReport != null && dtReport.Rows.Count > 0)
                    //{
                    //    for (int j = 0; j < dtReport.Rows.Count; j++)
                    //    {
                    //        ltrReport.Text = ltrReport.Text + "<table class='box'>";
                    //        ltrReport.Text = ltrReport.Text + "<tr>";
                    //        ltrReport.Text = ltrReport.Text + "<td>";
                    //        ltrReport.Text = ltrReport.Text + "<div class='title'>" + Convert.ToString(dtReport.Rows[j]["ReportTitleEnglish"]) + "</div>";
                    //        ltrReport.Text = ltrReport.Text + "<div class='postdate'>" + Convert.ToString(dtReport.Rows[j]["ReportDate"]) + "</div>";
                    //        ltrReport.Text = ltrReport.Text + "</td>";
                    //        ltrReport.Text = ltrReport.Text + "<td class='download'>";
                    //        ltrReport.Text = ltrReport.Text + "<a href='../" + Convert.ToString(dtReport.Rows[j]["AttachmentUrl"]) + "' target ='_blank'><img src='../images/download.png' alt=''/></a>";
                    //        ltrReport.Text = ltrReport.Text + "</td>";
                    //        ltrReport.Text = ltrReport.Text + "</tr>";
                    //        ltrReport.Text = ltrReport.Text + "</table>";
                    //    }
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
                            HTMLCode += "<li><img src='" + dtBanner.Rows[j]["BannerPath"].ToString() + "' title='" + dtBanner.Rows[j]["BannerText"] + "'></li>";
                        }
                        HTMLCode += "</ul>";
                    }
                    ntpcbanner.InnerHtml = HTMLCode;
                    #endregion

                    divDeptVisitors.InnerHtml = "Total Visitors of Department : " + ds.Tables[7].Rows[0]["TotalVisitors"].ToString();
                }
            }
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

    protected void LoadDocumentTabs()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
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

                    if (dt.Rows.Count > 0)
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
        DocLevelCodeChild = DocLevelCode_forChild;
        try
        {
            DataTable dt = new DataTable();
            string result = "";
            string[] parameter = { "@Flag", "@DeptID", "@DocLevelCode" };
            string[] value = { "TotalFile", hdfDeptId.Value, DocLevelCode_forChild };
            DB_Status dbs = dba.sp_populateDataSet("SP_DocLibrary", 3, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        result = dt.Rows[0]["totalfile"].ToString();
                        lblTotalFile.Text = dt.Rows[0]["totalfile"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
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
        catch (Exception ex)
        {
            LoadDocuments(hfDocLevelCode_forParent_tmp.Value);
        }
    }

    protected void LoadDeptforHOP()
    {

        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "PublicPage_DeptCategory", hdfDeptId.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_HOPReports", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    repeater_HOP.DataSource = dt;
                    repeater_HOP.DataBind();
                }
            }
            
        }
        catch (Exception ex)
        {

        }
    }
    protected void ViewDept_Click(object sender, EventArgs e)
    {
        pnlHopDept.Visible = false;
        pnlDept.Visible = true;
        linkBackHop.Visible = true;
        LinkButton linktab = (LinkButton)sender;
        RepeaterItem rptItem = (RepeaterItem)linktab.NamingContainer;
        HiddenField hdfDeptid = (HiddenField)rptItem.FindControl("hfDeptID");
       
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "PublicPage_DeptByReportHop", hdfDeptid.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_HOPReports", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    repeater_DocByDept.DataSource = dt;
                    repeater_DocByDept.DataBind();
                }
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void ViewDeptWiseHOPRpt()
    {
        pnlDept.Visible = false;
        pnlHopDept.Visible = false;
        pnlForAllDept.Visible = true;
        bool DeptLoginAccess = false;
        if (Session["username"] != null)  /// write code to check authorised emp of department will only get access
              {
                  if (divDeptAdmin.InnerHtml.Contains(Session["username"].ToString()) || Session["username"] == "admin"){
                         DeptLoginAccess = true;
                  }
                 
              }
         if (DeptLoginAccess)
              {
                   pnlDept.Visible = false;
                     pnlHopDept.Visible = false;
                    pnlForAllDept.Visible = false;
                    pnlForAllDeptLogin.Visible  = true;
              }

        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "PublicPage_DeptByReportHop", hdfDeptId.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_HOPReports", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (DeptLoginAccess)
                    {
                    repeater_allDeptLogin.DataSource = dt;
                    repeater_allDeptLogin.DataBind();
                    }
                    else
                    {
                         repeater_allDept.DataSource = dt;
                    repeater_allDept.DataBind();
                    }
                }
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void linkBackHop_Click(object sender, EventArgs e)
    {
        pnlHopDept.Visible = true;
        pnlDept.Visible = false;
        linkBackHop.Visible = false;

    }
    protected void lbtHopReports_Click(object sender, EventArgs e)
    {
        //ViewHOPChild_Click
        lblTotalFile.Text = "0";
        repeater_Document.DataSource = null;
        repeater_Document.DataBind();       
             

        string[] parameter = { "@Flag", "@DeptID", "@DocLevelCode" };
        string[] value = { "PublicPage_Documents", hdfDeptId.Value, DocLevelCodeChild };
        DB_Status dbs = dba.sp_populateDataSet("SP_DocLibrary", 3, parameter, value);
        if (dbs.OperationStatus.ToString() == "Success")
        {
            DataSet ds = dbs.ResultDataSet;
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                repeater_HOP.DataSource = dt;
                repeater_HOP.DataBind();
            }
        }
          
    }


   
    protected void ViewHOPChild_Click(object sender, EventArgs e)
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



    protected void btncontinue_Click(object sender, EventArgs e)
    {
          string pwd = "";
        string result = "";
        if (txtPassCode.Text == "")
        {
            lblPassCodeMsg.Text = "Please Enter Passcode";
        }
        else
        {
            try
            {
                DataTable dt = new DataTable();
                string[] param = { "@Flag", "@Password" };
                string[] values = { "HOPLogin", txtPassCode.Text.Trim() };
                DB_Status dbs = dba.sp_populateDataSet("SP_HOPReports", 2, param, values);
                if (dbs.OperationStatus.ToString() == "Success")
                {
                    DataSet ds = dbs.ResultDataSet;
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            result = dt.Rows[0]["Result"].ToString();
                            pwd = dt.Rows[0]["PassCode"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            if (txtPassCode.Text.Trim() == pwd)
            {
                lblPassCodeMsg.Text = "";
                divPassCode.Visible = false;
                divDepartmentcontent.Visible = true;
            }
            else
            {
                lblPassCodeMsg.Text = "Invalid Pass Code";
                divPassCode.Visible = true;
                divDepartmentcontent.Visible = false;
            }
        }
    }
}