using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    public DataTable dtBanner { get; set; }
    public DataTable dtMessage { get; set; }
    public DataTable dtEmpServices { get; set; }
    public DataTable dtHolidays { get; set; }
    public DataTable dtNotices { get; set; }
    public DataTable dtBirthday { get; set; }
    public DataTable dtSuperannuations { get; set; }
    public DataTable dtCircular { get; set; }
    public DataTable dtAnnouncement { get; set; }
    public DataTable dtEvent { get; set; }
    public DataTable dtNews { get; set; }
    public DataTable dtSchedule { get; set; }
	public DataTable dtInformation { get; set; }
    	public DataTable dtDeptUpdate { get; set; }
    

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
            string IPAdd;
            IPAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAdd))
                IPAdd = Request.ServerVariables["REMOTE_ADDR"];
            hdf_IPAddress.Value = IPAdd;
            LoadHomePageData();

            hidepanels();
            divInformation.Visible = true;
            linkInformation.CssClass = "active";

            hiderightpanels();
            divBirthdays.Visible = true;
            linkBirthdays.CssClass = "active";

            hideschedulepanels();
            divHalloffame.Visible = true;
            linkHalloffame.CssClass = "active";

            BindPopupData();
        }
    }

    protected void LoadHomePageData()
    {
        try
        {
			bool flag_bday = false;
            bool flag_superannuation = false;
            bool flag_welcome = false;
			
            string HTMLCode_Announcements = "";
            string HTMLCode_Birthday = "";
            string HTMLCode_SupperAnnuation = "";
            string HTMLCode_Welcomes = "";
            HTMLCode_Birthday = "<h4>Upcoming B'day This Week<h4>";
            HTMLCode_Birthday += "<img src='../images/wish.png' alt='' style='margin-top:-10px; width:100%;     height: 30px;'/>";
            HTMLCode_SupperAnnuation = "<h4>We will miss you<h4>";
            HTMLCode_Welcomes = "<h4>Welcome to KBUNL Family<h4>";
            HTMLCode_Welcomes += "<img src='../images/wish.png' alt='' style='margin-top:-5px; width:100%; height: 30px; '  />";

            string HTMLCode_safetytrg = "";
            string HTMLCode_medicalexam = "";
            string HTMLCode_halloffame = "";
            string HTMLCode_vision = "";
            string HTMLCode_mission = "";
            string HTMLCode_corevalues = "";
            string HTMLCode_deptUpdate = "";

            string[] parameter = { "@IPAddress" };
            string[] value = { hdf_IPAddress.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_LoadData_forHomePage", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    dtBanner = ds.Tables[0];
                    dtMessage = ds.Tables[1];
                    dtEmpServices = ds.Tables[2];
                    dtHolidays = ds.Tables[3];

                    dtNotices = ds.Tables[4];
                    if (dtNotices != null && dtNotices.Rows.Count > 0)
                    {
                        ltrNotices.Text = "";
                        for (int i = 0; i < dtNotices.Rows.Count; i++)
                        {
                            string Attachment = Convert.ToString(dtNotices.Rows[i]["FilePath"]);
                            string Department = dtNotices.Rows[i]["PostedBy"].ToString();
                            string PostedOn = dtNotices.Rows[i]["PostedOn"].ToString();

                            string PostedBy = "";
                            if (Department != "" && Department != "Admin")
                                PostedBy += "<span class='postedby'>Posted by " + Department + " on " + PostedOn + "</span>";
                            else
                                PostedBy += "<span class='postedby'>Posted on " + PostedOn + "</span>";

                            ltrNotices.Text = ltrNotices.Text +"<div class='box'>";
                            ltrNotices.Text = ltrNotices.Text + "<table>";
                            ltrNotices.Text = ltrNotices.Text + "<tr>";
                            ltrNotices.Text = ltrNotices.Text + "<td class='tddate'>";
                            ltrNotices.Text = ltrNotices.Text + "<div class='date'>"+ Convert.ToString(dtNotices.Rows[i]["DateMonth"]) + "</div>";
                            ltrNotices.Text = ltrNotices.Text + "</td>";
                            ltrNotices.Text = ltrNotices.Text + "<td>";

                            if (!string.IsNullOrEmpty(Attachment) && Attachment != "No")
                                ltrNotices.Text = ltrNotices.Text + "<h5><p><a href='../" + Convert.ToString(dtNotices.Rows[i]["FilePath"]) + "' target='_blank'>" + Convert.ToString(dtNotices.Rows[i]["NoticeEnglish"]) + "</a></p></h5>";
                            else
                                ltrNotices.Text = ltrNotices.Text + "<h5><p>" + Convert.ToString(dtNotices.Rows[i]["NoticeEnglish"]) + "</p></h5>";

                            ltrNotices.Text = ltrNotices.Text + PostedBy;
                            ltrNotices.Text = ltrNotices.Text + "</td>";
                            ltrNotices.Text = ltrNotices.Text + "<td class='download'>";
                            
                            if (!string.IsNullOrEmpty(Attachment) && Attachment!="No")
                                ltrNotices.Text = ltrNotices.Text + "<a href='../" + Convert.ToString(dtNotices.Rows[i]["FilePath"]) + "' target='_blank'><img src ='../images/download.png' alt='' /></a>";
                            ltrNotices.Text = ltrNotices.Text + "</td>";
                            ltrNotices.Text = ltrNotices.Text + "</tr>";
                            ltrNotices.Text = ltrNotices.Text + "</table>";
                            ltrNotices.Text = ltrNotices.Text + "</div>";
                        }
                    }

                    dtBirthday = ds.Tables[5];
                    if (dtBirthday != null)
                    {
                        HTMLCode_Birthday += "<marquee direction='up' scrolldelay='150' onmouseover='this.stop();' onmouseout='this.start();' style='height:201px;'>";
                        for (int i = 0; i < dtBirthday.Rows.Count; i++)
                        {
							flag_bday = true;
                            HTMLCode_Birthday += "<div class='box'>" +
                                                "<div class='imgbox'><img src='../" + dtBirthday.Rows[i]["PhotoPath"].ToString() + "' alt=''/></div>" +
                                                "<div class='name'>" + dtBirthday.Rows[i]["EmpName"].ToString() + "</div>" +
                                                "<div class='designation'>" + dtBirthday.Rows[i]["Designation"].ToString() + " (" + dtBirthday.Rows[i]["Department"].ToString() + ")</div>" +
                                                "<div class='empno'>" + dtBirthday.Rows[i]["Mobile"].ToString() + "</div>" +
                                                "</div>";
                        }
                        HTMLCode_Birthday += "</marquee>";
                    }

                    dtSuperannuations = ds.Tables[6];
                    if (dtSuperannuations != null)
                    {
                        HTMLCode_SupperAnnuation += "<marquee direction='up' scrolldelay='150' onmouseover='this.stop();' onmouseout='this.start();' style='height:201px;'>";
                        for (int i = 0; i < dtSuperannuations.Rows.Count; i++)
                        {
							flag_superannuation = true;
                            HTMLCode_SupperAnnuation += "<div class='box'>" +
                                                "<div class='imgbox'><img src='../" + dtSuperannuations.Rows[i]["PhotoPath"].ToString() + "' alt=''/></div>" +
                                                "<div class='name'>" + dtSuperannuations.Rows[i]["EmpName"].ToString() + "</div>" +
                                                "<div class='designation'>" + dtSuperannuations.Rows[i]["Designation"].ToString() + " (" + dtSuperannuations.Rows[i]["Department"].ToString() + ")</div>" +
                                                "<div class='empno'>" + dtSuperannuations.Rows[i]["Mobile"].ToString() + "</div>" +
                                                "</div>";
                        }
                        HTMLCode_SupperAnnuation += "</marquee>";
                    }

                    DataTable dtWelcome = ds.Tables[14];
                    if (dtWelcome != null)
                    {
                        HTMLCode_Welcomes += "<marquee direction='up' scrolldelay='150' onmouseover='this.stop();' onmouseout='this.start();' style='height:201px;'>";
                        for (int i = 0; i < dtWelcome.Rows.Count; i++)
                        {
							flag_welcome = true;   //onerror=" & Chr(34) & "this.src='../images/user.png';" & Chr(34) & " 
                            HTMLCode_Welcomes += "<div class='box'>" +
                                                "<div class='imgbox'><img src='../" + dtWelcome.Rows[i]["PhotoPath"].ToString() + "' alt=''  /></div>" +
                                                "<div class='name'>" + dtWelcome.Rows[i]["EmpName"].ToString() + "</div>" +
                                                "<div class='designation'>" + dtWelcome.Rows[i]["Designation"].ToString() + " (" + dtWelcome.Rows[i]["Department"].ToString() + ")</div>" +
                                                "<div class='empno'>" + dtWelcome.Rows[i]["Joined"].ToString() + "</div>" +
                                                "</div>";
                        }
                        HTMLCode_Welcomes += "</marquee>";
                    }

                    dtCircular = ds.Tables[7];
                    if (dtCircular != null && dtCircular.Rows.Count > 0)
                    {
                        ltrCircular.Text = "";
                        for (int i = 0; i < dtCircular.Rows.Count; i++)
                        {
                            string LinkURL = Convert.ToString(dtCircular.Rows[i]["LinkURL"]);
                            string DocLinkType = dtCircular.Rows[i]["DocLinkType"].ToString();
                            string Department = dtCircular.Rows[i]["Department"].ToString();
                            string PostedOn = dtCircular.Rows[i]["PostedOn"].ToString();

                            string AttachmentTitle1 = dtCircular.Rows[i]["AttachmentTitle1"].ToString();
                            string AttachmentTitle2 = dtCircular.Rows[i]["AttachmentTitle2"].ToString();
                            string AttachmentTitle3 = dtCircular.Rows[i]["AttachmentTitle3"].ToString();
                            string AttachmentTitle4 = dtCircular.Rows[i]["AttachmentTitle4"].ToString();
                            string AttachmentTitle5 = dtCircular.Rows[i]["AttachmentTitle5"].ToString();
                            string AttachmentURL1 = dtCircular.Rows[i]["AttachmentURL1"].ToString();
                            string AttachmentURL2 = dtCircular.Rows[i]["AttachmentURL2"].ToString();
                            string AttachmentURL3 = dtCircular.Rows[i]["AttachmentURL3"].ToString();
                            string AttachmentURL4 = dtCircular.Rows[i]["AttachmentURL4"].ToString();
                            string AttachmentURL5 = dtCircular.Rows[i]["AttachmentURL5"].ToString();
                            string bg = " style = 'background-color: #e8f3fd;' ";
                             if (i%2 == 1)
                            {
                                bg = " style = 'background-color: #f2ffef;' ";
                            }
                            string links = "<div class='clinks'>";
                            if (AttachmentTitle1 != "" && AttachmentURL1 != "NA")
                                links += "<a href='../" + AttachmentURL1 + "' target='_blank'>" + AttachmentTitle1 + "</a>";
                            if (AttachmentTitle2 != "" && AttachmentURL2 != "NA")
                                links += "<a href='../" + AttachmentURL2 + "' target='_blank'>" + AttachmentTitle2 + "</a>";
                            if (AttachmentTitle3 != "" && AttachmentURL3 != "NA")
                                links += "<a href='../" + AttachmentURL3 + "' target='_blank'>" + AttachmentTitle3 + "</a>";
                            if (AttachmentTitle4 != "" && AttachmentURL4 != "NA")
                                links += "<a href='../" + AttachmentURL4 + "' target='_blank'>" + AttachmentTitle4 + "</a>";
                            if (AttachmentTitle5 != "" && AttachmentURL5 != "NA")
                                links += "<a href='../" + AttachmentURL5 + "' target='_blank'>" + AttachmentTitle5 + "</a>";
                            if (LinkURL != "")
                            {
                                links += "<a href='" + LinkURL + "' target='_blank'>Link</a>";
                            }
                            links += "</div>";

                            string PostedBy = "";
                            if (Department != "" && Department != "Admin")
                                PostedBy += "<span class='postedby'>Posted by " + Department ;//+ " on " + PostedOn + "</span>";
                           // else
                            //    PostedBy += "<span class='postedby'>Posted on " + PostedOn + "</span>";

                            ltrCircular.Text = ltrCircular.Text + "<div class='box circular'>";
                            ltrCircular.Text = ltrCircular.Text + "<table>";
                            ltrCircular.Text = ltrCircular.Text + "<tr"+ bg + ">";
                            ltrCircular.Text = ltrCircular.Text + "<td class='tddate'>";
                            ltrCircular.Text = ltrCircular.Text + "<div class='date'>" + Convert.ToString(dtCircular.Rows[i]["DateMonth"]) + "<div class='circulartype'>" + Convert.ToString(dtCircular.Rows[i]["Type"]) + "</div></div>";
                            ltrCircular.Text = ltrCircular.Text + "</td>";
                            ltrCircular.Text = ltrCircular.Text + "<td>";

                            if (AttachmentTitle1 != "" && AttachmentURL1 != "NA")
                                ltrCircular.Text = ltrCircular.Text + "<h5><a href='../" + Convert.ToString(dtCircular.Rows[i]["AttachmentURL1"]) + "' target='_blank'>" + Convert.ToString(dtCircular.Rows[i]["TitleEnglish"]) + "</a><span style='font-size:9px; color:grey;'>" + PostedBy + "</span></h5>";
                            else if (AttachmentTitle1 == "" && LinkURL != "")
                                ltrCircular.Text = ltrCircular.Text + "<h5><a href='" + Convert.ToString(dtCircular.Rows[i]["LinkURL"]) + "' target='_blank'>" + Convert.ToString(dtCircular.Rows[i]["TitleEnglish"]) + "</a><span style='font-size:9px; color:grey;'>" + PostedBy + "</span></h5>";
                            else
                                ltrCircular.Text = ltrCircular.Text + "<h5>" + Convert.ToString(dtCircular.Rows[i]["TitleEnglish"]) + "<span style='font-size:9px; color:grey;'>" + PostedBy + "</span></h5>";

                            ltrCircular.Text = ltrCircular.Text + "<p>" + Convert.ToString(dtCircular.Rows[i]["DescriptionEnglish"]) + "</p>";
                            ltrCircular.Text = ltrCircular.Text + links;
                           // ltrCircular.Text = ltrCircular.Text + PostedBy;
                            ltrCircular.Text = ltrCircular.Text + "</td>";

                            ltrCircular.Text = ltrCircular.Text + "<td class='download'>";

                            if (AttachmentTitle1 != "" && AttachmentURL1 != "NA")
                                ltrCircular.Text = ltrCircular.Text + "<a href='../" + Convert.ToString(dtCircular.Rows[i]["AttachmentURL1"]) + "' target='_blank'><img src ='../images/download.png' alt='' /></a>";
                            else if (AttachmentTitle1 == "" && LinkURL != "")
                                ltrCircular.Text = ltrCircular.Text + "<a href='" + Convert.ToString(dtCircular.Rows[i]["LinkURL"]) + "' target='_blank'><img src ='../images/weblink.png' width='22' alt='' /></a>";
                            ltrCircular.Text = ltrCircular.Text + "</td>";

                            ltrCircular.Text = ltrCircular.Text + "</tr>";
                            ltrCircular.Text = ltrCircular.Text + "</table>";

                            string IsNew = dtCircular.Rows[i]["IsNew"].ToString();
                            if (IsNew == "True")
                            {
                                ltrCircular.Text = ltrCircular.Text + "<span class='new'></span>";
                            }

                            ltrCircular.Text = ltrCircular.Text + "</div>";
                        }
                    }

                    dtEvent = ds.Tables[9];
                    if (dtEvent != null && dtEvent.Rows.Count > 0)
                    {
                        ltrEvents.Text = "";
                        for (int i = 0; i < dtEvent.Rows.Count; i++)
                        {
                            string Attachment = Convert.ToString(dtEvent.Rows[i]["Attachment"]);
                            string Link = Convert.ToString(dtEvent.Rows[i]["Link"]);
                            string Department = dtEvent.Rows[i]["PostedBy"].ToString();
                            string PostedOn = dtEvent.Rows[i]["PostedOn"].ToString();

                            string PostedBy = "";
                            if (Department != "" && Department != "Admin")
                                PostedBy += "<span class='postedby'>Posted by " + Department + " on " + PostedOn + "</span>";
                            else
                                PostedBy += "<span class='postedby'>Posted on " + PostedOn + "</span>";

                            ltrEvents.Text = ltrEvents.Text + "<div class='box'>";
                            ltrEvents.Text = ltrEvents.Text + "<table>";
                            ltrEvents.Text = ltrEvents.Text + "<tr>";
                            ltrEvents.Text = ltrEvents.Text + "<td class='tddate'>";
                            ltrEvents.Text = ltrEvents.Text + "<div class='date'>"+ Convert.ToString(dtEvent.Rows[i]["DateMonth"]) + "</div>";
                            ltrEvents.Text = ltrEvents.Text + "</td>";
                            ltrEvents.Text = ltrEvents.Text + "<td>";

                            if (!string.IsNullOrEmpty(Attachment))
                                ltrEvents.Text = ltrEvents.Text + "<h5><a href='../" + Convert.ToString(dtEvent.Rows[i]["Attachment"]) + "' target='_blank'>" + Convert.ToString(dtEvent.Rows[i]["TitleEnglish"]) + "</a></h5>";
                            else if (!string.IsNullOrEmpty(Link))
                                ltrEvents.Text = ltrEvents.Text + "<h5><a href='" + Convert.ToString(dtEvent.Rows[i]["Link"]) + "' target='_blank'>" + Convert.ToString(dtEvent.Rows[i]["TitleEnglish"]) + "</a></h5>";
                            else
                                ltrEvents.Text = ltrEvents.Text + "<h5>" + Convert.ToString(dtEvent.Rows[i]["TitleEnglish"]) + "</h5>";

                            ltrEvents.Text = ltrEvents.Text + "<p>" + Convert.ToString(dtEvent.Rows[i]["DescriptionEnglish"]) + "</p>";
                            ltrEvents.Text = ltrEvents.Text + PostedBy;
                            ltrEvents.Text = ltrEvents.Text + "</td>";
                            ltrEvents.Text = ltrEvents.Text + "<td class='download'>";
                            
                            if (!string.IsNullOrEmpty(Attachment))
                                ltrEvents.Text = ltrEvents.Text + "<a href='../" + Convert.ToString(dtEvent.Rows[i]["Attachment"]) + "' target='_blank'><img src='../images/download.png' alt=''/></a>";
                            if (!string.IsNullOrEmpty(Link))
                                ltrEvents.Text = ltrEvents.Text + "<a href='" + Convert.ToString(dtEvent.Rows[i]["Link"]) + "' target='_blank'><img src ='../images/weblink.png' width='22' alt='' /></a>";
                            ltrEvents.Text = ltrEvents.Text + "</td>";
                            ltrEvents.Text = ltrEvents.Text + "</tr>";
                            ltrEvents.Text = ltrEvents.Text + "</table>";
                            ltrEvents.Text = ltrEvents.Text + "</div>";
                        }
                    }

                    dtNews = ds.Tables[10];
                    if (dtNews != null && dtNews.Rows.Count > 0)
                    {
                        ltrNews.Text = "";
                        for (int i = 0; i < dtNews.Rows.Count; i++)
                        {
                            string Attachment = Convert.ToString(dtNews.Rows[i]["Attachment"]);
                            string Link = Convert.ToString(dtNews.Rows[i]["Link"]);
                            string Department = dtNews.Rows[i]["PostedBy"].ToString();
                            string PostedOn = dtNews.Rows[i]["PostedOn"].ToString();

                            string PostedBy = "";
                            if (Department != "" && Department != "Admin")
                                PostedBy += "<span class='postedby'>Posted by " + Department + " on " + PostedOn + "</span>";
                            else
                                PostedBy += "<span class='postedby'>Posted on " + PostedOn + "</span>";

                            ltrNews.Text = ltrNews.Text+"<div class='box'>";
                            ltrNews.Text = ltrNews.Text + "<table>";
                            ltrNews.Text = ltrNews.Text + "<tr>";
                            ltrNews.Text = ltrNews.Text + "<td class='tddate'>";
							ltrNews.Text = ltrNews.Text+ "<div class='date'>" + Convert.ToString(dtNews.Rows[i]["DateMonth"]) + "</div>";
                            ltrNews.Text = ltrNews.Text + "</td>";
                            ltrNews.Text = ltrNews.Text + "<td>";

                            if (!string.IsNullOrEmpty(Attachment))
                                ltrNews.Text = ltrNews.Text + "<h5><a href='../" + Convert.ToString(dtNews.Rows[i]["Attachment"]) + "' target='_blank'>" + Convert.ToString(dtNews.Rows[i]["TitleEnglish"]) + "</a><span style='font-size:9px; color:grey;'>" + PostedBy + "</span></h5>";
                            else if (!string.IsNullOrEmpty(Link))
                                ltrNews.Text = ltrNews.Text + "<h5><a href='" + Convert.ToString(dtNews.Rows[i]["Link"]) + "' target='_blank'>" + Convert.ToString(dtNews.Rows[i]["TitleEnglish"]) + "</a><span style='font-size:9px; color:grey;'>" + PostedBy + "</span></h5>";
                            else
                                ltrNews.Text = ltrNews.Text + "<h5>" + Convert.ToString(dtNews.Rows[i]["TitleEnglish"]) + "<span style='font-size:9px; color:grey;'>" + PostedBy + "</span></h5>";

                            ltrNews.Text = ltrNews.Text + "<p>" + Convert.ToString(dtNews.Rows[i]["DescriptionEnglish"]) + "</p>";
                           // ltrNews.Text = ltrNews.Text + PostedBy;
                            ltrNews.Text = ltrNews.Text + "</td>";
                            ltrNews.Text = ltrNews.Text + "<td class='download'>";
                            
                            if (!string.IsNullOrEmpty(Attachment))
                                ltrNews.Text = ltrNews.Text + "<a href='../" + Convert.ToString(dtNews.Rows[i]["Attachment"]) + "' target='_blank'><img src='../images/download.png' alt=''/></a>";
                            if (!string.IsNullOrEmpty(Link))
                                ltrNews.Text = ltrNews.Text + "<a href='" + Convert.ToString(dtNews.Rows[i]["Link"]) + "' target='_blank'><img src ='../images/weblink.png' width='22' alt='' /></a>";
                            ltrNews.Text = ltrNews.Text + "</td>";
                            ltrNews.Text = ltrNews.Text + "</tr>";
                            ltrNews.Text = ltrNews.Text + "</table>";
                            ltrNews.Text = ltrNews.Text + "</div>";
                        }
                    }
					
					/*dtInformation = ds.Tables[18];
                    if (dtInformation != null && dtInformation.Rows.Count > 0)
                    {
                        ltrInformation.Text = "";
                        for (int i = 0; i < dtInformation.Rows.Count; i++)
                        {
                            string Attachment = Convert.ToString(dtInformation.Rows[i]["Attachment"]);
                            string Link = Convert.ToString(dtInformation.Rows[i]["Link"]);
                            string Department = dtInformation.Rows[i]["PostedBy"].ToString();
                            string PostedOn = dtInformation.Rows[i]["PostedOn"].ToString();

                            string PostedBy = "";
                            if (Department != "" && Department != "Admin")
                                PostedBy += "<span class='postedby'>Posted by " + Department + " on " + PostedOn + "</span>";
                            else
                                PostedBy += "<span class='postedby'>Posted on " + PostedOn + "</span>";

                            ltrInformation.Text = ltrInformation.Text+"<div class='box'>";
                            ltrInformation.Text = ltrInformation.Text + "<table>";
                            ltrInformation.Text = ltrInformation.Text + "<tr>";
                            ltrInformation.Text = ltrInformation.Text + "<td class='tddate'>";
							ltrInformation.Text = ltrInformation.Text+ "<div class='date'>" + Convert.ToString(dtInformation.Rows[i]["DateMonth"]) + "</div>";
                            ltrInformation.Text = ltrInformation.Text + "</td>";
                            ltrInformation.Text = ltrInformation.Text + "<td>";

                            if (!string.IsNullOrEmpty(Attachment))
                                ltrInformation.Text = ltrInformation.Text + "<h5><a href='../" + Convert.ToString(dtInformation.Rows[i]["Attachment"]) + "' target='_blank'>" + Convert.ToString(dtInformation.Rows[i]["TitleEnglish"]) + "</a></h5>";
                            else if (!string.IsNullOrEmpty(Link))
                                ltrInformation.Text = ltrInformation.Text + "<h5><a href='" + Convert.ToString(dtInformation.Rows[i]["Link"]) + "' target='_blank'>" + Convert.ToString(dtInformation.Rows[i]["TitleEnglish"]) + "</a></h5>";
                            else
                                ltrInformation.Text = ltrInformation.Text + "<h5>" + Convert.ToString(dtInformation.Rows[i]["TitleEnglish"]) + "</h5>";

                            ltrInformation.Text = ltrInformation.Text + "<p>" + Convert.ToString(dtInformation.Rows[i]["DescriptionEnglish"]) + "</p>";
                            ltrInformation.Text = ltrInformation.Text + PostedBy;
                            ltrInformation.Text = ltrInformation.Text + "</td>";
                            ltrInformation.Text = ltrInformation.Text + "<td class='download'>";
                            
                            if (!string.IsNullOrEmpty(Attachment))
                                ltrInformation.Text = ltrInformation.Text + "<a href='../" + Convert.ToString(dtInformation.Rows[i]["Attachment"]) + "' target='_blank'><img src='../images/download.png' alt=''/></a>";
                            if (!string.IsNullOrEmpty(Link))
                                ltrInformation.Text = ltrInformation.Text + "<a href='" + Convert.ToString(dtInformation.Rows[i]["Link"]) + "' target='_blank'><img src ='../images/weblink.png' width='22' alt='' /></a>";
                            ltrInformation.Text = ltrInformation.Text + "</td>";
                            ltrInformation.Text = ltrInformation.Text + "</tr>";
                            ltrInformation.Text = ltrInformation.Text + "</table>";
                            ltrInformation.Text = ltrInformation.Text + "</div>";
                        }
                    } */
                     dtInformation = ds.Tables[18];
                    if (dtInformation != null && dtInformation.Rows.Count > 0)
                    {
                        ltrInformation.Text = "";
                        for (int i = 0; i < dtInformation.Rows.Count; i++)
                        {
                            string LinkURL = Convert.ToString(dtInformation.Rows[i]["LinkURL"]);
                            string DocLinkType = dtInformation.Rows[i]["DocLinkType"].ToString();
                            string Department = dtInformation.Rows[i]["Department"].ToString();
                            string PostedOn = dtInformation.Rows[i]["PostedOn"].ToString();

                            string AttachmentTitle1 = dtInformation.Rows[i]["AttachmentTitle1"].ToString();
                            string AttachmentTitle2 = dtInformation.Rows[i]["AttachmentTitle2"].ToString();
                            string AttachmentTitle3 = dtInformation.Rows[i]["AttachmentTitle3"].ToString();
                            string AttachmentTitle4 = dtInformation.Rows[i]["AttachmentTitle4"].ToString();
                            string AttachmentTitle5 = dtInformation.Rows[i]["AttachmentTitle5"].ToString();
                            string AttachmentURL1 = dtInformation.Rows[i]["AttachmentURL1"].ToString();
                            string AttachmentURL2 = dtInformation.Rows[i]["AttachmentURL2"].ToString();
                            string AttachmentURL3 = dtInformation.Rows[i]["AttachmentURL3"].ToString();
                            string AttachmentURL4 = dtInformation.Rows[i]["AttachmentURL4"].ToString();
                            string AttachmentURL5 = dtInformation.Rows[i]["AttachmentURL5"].ToString();
                            string bg = " style = 'background-color: #e8f3fd;' ";
                            

                            string links = "<div class='clinks'>";
                            if (AttachmentTitle1 != "" && AttachmentURL1 != "NA")
                                links += "<a href='../" + AttachmentURL1 + "' target='_blank'>" + AttachmentTitle1 + "</a>";
                            if (AttachmentTitle2 != "" && AttachmentURL2 != "NA")
                                links += "<a href='../" + AttachmentURL2 + "' target='_blank'>" + AttachmentTitle2 + "</a>";
                            if (AttachmentTitle3 != "" && AttachmentURL3 != "NA")
                                links += "<a href='../" + AttachmentURL3 + "' target='_blank'>" + AttachmentTitle3 + "</a>";
                            if (AttachmentTitle4 != "" && AttachmentURL4 != "NA")
                                links += "<a href='../" + AttachmentURL4 + "' target='_blank'>" + AttachmentTitle4 + "</a>";
                            if (AttachmentTitle5 != "" && AttachmentURL5 != "NA")
                                links += "<a href='../" + AttachmentURL5 + "' target='_blank'>" + AttachmentTitle5 + "</a>";
                            if (LinkURL != "")
                            {
                                links += "<a href='" + LinkURL + "' target='_blank'>Link</a>";
                            }
                            links += "</div>";

                            string PostedBy = "";
                            if (Department != "" && Department != "Admin")
                                PostedBy += "<span class='postedby'>Posted by " + Department; // + " on " + PostedOn + "</span>";
                            else
                                PostedBy += "<span class='postedby'>Posted by Admin </span>";

                            ltrInformation.Text = ltrInformation.Text + "<div class='box circular'>";
                            ltrInformation.Text = ltrInformation.Text + "<table>";
                            if (i%2 == 1)
                            {
                                bg = " style = 'background-color: #f2ffef;' ";
                            }

                            ltrInformation.Text = ltrInformation.Text + "<tr " + bg +">";
                            ltrInformation.Text = ltrInformation.Text + "<td class='tddate' >";
                            ltrInformation.Text = ltrInformation.Text + "<div class='date'>" + Convert.ToString(dtInformation.Rows[i]["DateMonth"]) + "</div>"; //<div class='circulartype'>" + Convert.ToString(dtInformation.Rows[i]["Type"]) + "</div>
                            ltrInformation.Text = ltrInformation.Text + "</td>";
                            ltrInformation.Text = ltrInformation.Text + "<td>";

                            if (AttachmentTitle1 != "" && AttachmentURL1 != "NA")
                                ltrInformation.Text = ltrInformation.Text + "<h5><a href='../" + Convert.ToString(dtInformation.Rows[i]["AttachmentURL1"]) + "' target='_blank'>" + Convert.ToString(dtInformation.Rows[i]["TitleEnglish"]) + "</a> <span style='font-size:9px; color:grey;'>" + PostedBy + "</span></h5>";
                            else if (AttachmentTitle1 == "" && LinkURL != "")
                                ltrInformation.Text = ltrInformation.Text + "<h5><a href='" + Convert.ToString(dtInformation.Rows[i]["LinkURL"]) + "' target='_blank'>" + Convert.ToString(dtInformation.Rows[i]["TitleEnglish"]) + "</a><span style='font-size:9px; color:grey;'>" + PostedBy + "</span></h5>";
                            else
                                ltrInformation.Text = ltrInformation.Text + "<h5>" + Convert.ToString(dtInformation.Rows[i]["TitleEnglish"]) + "<span style='font-size:9px; color:grey;'>" + PostedBy + "</span></h5>";

                            ltrInformation.Text = ltrInformation.Text + "<p>" + Convert.ToString(dtInformation.Rows[i]["DescriptionEnglish"]) + "</p>" ;
                            ltrInformation.Text = ltrInformation.Text + links;
                         //  ltrInformation.Text = ltrInformation.Text + PostedBy;
                            ltrInformation.Text = ltrInformation.Text + "</td>";

                            ltrInformation.Text = ltrInformation.Text + "<td class='download'>";

                            if (AttachmentTitle1 != "" && AttachmentURL1 != "NA")
                                ltrInformation.Text = ltrInformation.Text + "<a href='../" + Convert.ToString(dtInformation.Rows[i]["AttachmentURL1"]) + "' target='_blank'><img src ='../images/download.png' alt='' /></a>";
                            else if (AttachmentTitle1 == "" && LinkURL != "")
                                ltrInformation.Text = ltrInformation.Text + "<a href='" + Convert.ToString(dtInformation.Rows[i]["LinkURL"]) + "' target='_blank'><img src ='../images/weblink.png' width='22' alt='' /></a>";
                            ltrInformation.Text = ltrInformation.Text + "</td>";

                            ltrInformation.Text = ltrInformation.Text + "</tr>";
                            ltrInformation.Text = ltrInformation.Text + "</table>";

                            string IsNew = dtInformation.Rows[i]["IsNew"].ToString();
                            if (IsNew == "True")
                            {
                                ltrInformation.Text = ltrInformation.Text + "<span class='new'></span>";
                            }

                            ltrInformation.Text = ltrInformation.Text + "</div>";
                        }
                    }

                    dtSchedule = ds.Tables[11];
                    if (dtSchedule != null && dtSchedule.Rows.Count > 0)
                    {
                        ltrSchedule.Text = "";
                        for (int i = 0; i < dtSchedule.Rows.Count; i++)
                        {
                            ltrSchedule.Text = ltrSchedule.Text + "<div class='box'>";
                            ltrSchedule.Text = ltrSchedule.Text + "<table>";
                            ltrSchedule.Text = ltrSchedule.Text + "<tr>";
                            ltrSchedule.Text = ltrSchedule.Text + "<td class='tddate'>";
                            ltrSchedule.Text = ltrSchedule.Text + "<div class='date'>" + Convert.ToString(dtSchedule.Rows[i]["DateMonth"]) + "</div>";
                            ltrSchedule.Text = ltrSchedule.Text + "</td>";
                            ltrSchedule.Text = ltrSchedule.Text + "<td>";
                            ltrSchedule.Text = ltrSchedule.Text + "<h5>" + Convert.ToString(dtSchedule.Rows[i]["TitleEnglish"]) + "</h5>";
                            ltrSchedule.Text = ltrSchedule.Text + "<p>Description: " + Convert.ToString(dtSchedule.Rows[i]["DescEnglish"]) + "</p>";
                            ltrSchedule.Text = ltrSchedule.Text + "<p>Time: " + Convert.ToString(dtSchedule.Rows[i]["EventTime"]) + "</p>";
                            ltrSchedule.Text = ltrSchedule.Text + "</td>";
                            ltrSchedule.Text = ltrSchedule.Text + "<td class='download'>";
                            string Attachment = Convert.ToString(dtSchedule.Rows[i]["Attachment"]);
                            if (!string.IsNullOrEmpty(Attachment))
                                ltrSchedule.Text = ltrSchedule.Text + "<a href='../" + Convert.ToString(dtSchedule.Rows[i]["Attachment"]) + "' target='_blank'><img src='../images/download.png' alt=''/></a>";
                            ltrSchedule.Text = ltrSchedule.Text + "</td>";
                            ltrSchedule.Text = ltrSchedule.Text + "</tr>";
                            ltrSchedule.Text = ltrSchedule.Text + "</table>";
                            ltrSchedule.Text = ltrSchedule.Text + "</div>";
                        }
                    }
                    ltrSchedule.Visible = false;

                    dtAnnouncement = ds.Tables[12];      
                    if(dtAnnouncement!=null)
                    {
                        if(dtAnnouncement.Rows.Count>0)
                        {
                            HTMLCode_Announcements += "<marquee scrolldelay='150' onmouseover='this.stop();' onmouseout='this.start();'>";
                            for(int i=0;i< dtAnnouncement.Rows.Count;i++)
                            {
                                string AttachmentURL = dtAnnouncement.Rows[i]["AttachmentURL"].ToString();
                                string LinkURL = dtAnnouncement.Rows[i]["LinkURL"].ToString();
                                string TitleE = dtAnnouncement.Rows[i]["TitleE"].ToString();
                                string TitleH = dtAnnouncement.Rows[i]["TitleH"].ToString();

                                string IsNew = dtAnnouncement.Rows[i]["IsNew"].ToString();
                                string IsNewHTML = "";
                                if (IsNew == "True")
                                {
                                    IsNewHTML = "<img src='../images/new.png'/>";
                                }

                                if (AttachmentURL != "NA")
                                    HTMLCode_Announcements += "<a href='../" + AttachmentURL + "' target='_blank'><span>"+ IsNewHTML + "" + TitleE + "</span></a>";
                                else if (LinkURL != "NA")
                                    HTMLCode_Announcements += "<a href='" + LinkURL + "' target='_blank'><span>"+ IsNewHTML + "" + TitleE + "</span></a>";
                                else
                                    HTMLCode_Announcements += "<span>"+ IsNewHTML + ""+ TitleE + "</span>";
                            }
                            HTMLCode_Announcements += "</marquee>";
                        }
                    }

                    DataTable dtThoughts = ds.Tables[15];
                    if (dtThoughts.Rows.Count > 0)
                    {
                        divThought.InnerHtml = dtThoughts.Rows[0]["ThoughtOfTheDayEnglish"].ToString();
                    }

                    DataTable dtHindiWord = ds.Tables[16];
                    if (dtHindiWord.Rows.Count > 0)
                    {
                        divHWH.InnerHtml = dtHindiWord.Rows[0]["WordOfTheDayHindi"].ToString();
                        divHWE.InnerHtml = dtHindiWord.Rows[0]["WordOfTheDayEnglish"].ToString();
                    }

                    DataTable dtOtherUpdates = ds.Tables[17];
                    if(dtOtherUpdates.Rows.Count>0)
                    {
                        for(int i=0;i<dtOtherUpdates.Rows.Count;i++)
                        {
                            string Section = dtOtherUpdates.Rows[i]["Section"].ToString();
                            string TextEnglish = dtOtherUpdates.Rows[i]["TextEnglish"].ToString();
                            string TextHindi = dtOtherUpdates.Rows[i]["TextHindi"].ToString();
                            if (Section== "Safety Training Schedule")
                            {
                                HTMLCode_safetytrg += "<marquee direction='up' scrolldelay='150' onmouseover='this.stop();' onmouseout='this.start();' style='height:201px;'>";
                                HTMLCode_safetytrg += "<div class='safetytrg'>" + TextEnglish + "</div>";
                                HTMLCode_safetytrg += "</marquee>";
                            }
                            if (Section == "Medical Examination")
                            {
                                HTMLCode_medicalexam += "<marquee direction='up' scrolldelay='150' onmouseover='this.stop();' onmouseout='this.start();' style='height:201px;'>";
                                HTMLCode_medicalexam += "<div class='medicalexam'>" + TextEnglish + "</div>";
                                HTMLCode_medicalexam += "</marquee>";
                            }
                            if (Section == "Hall of the Fame")
                            {
                                HTMLCode_halloffame += "<marquee direction='up' scrolldelay='150' onmouseover='this.stop();' onmouseout='this.start();' style='height:201px;'>";
                                HTMLCode_halloffame += "<div class='safetytrg'>" + TextEnglish + "</div>";
                                HTMLCode_halloffame += "</marquee>";
                            }
                            if (Section == "Vision")
                            {
                                HTMLCode_vision = "<div class='vision'>" + TextEnglish + "</div>";
                            }
                            if (Section == "Mission")
                            {
                                HTMLCode_mission = "<div class='mission'>" + TextEnglish + "</div>";
                            }
                            if (Section == "Core Values")
                            {
                                HTMLCode_corevalues = "<div class='corevalues'>" + TextEnglish + "</div>";
                            }
                        }
                    }
                    dtDeptUpdate = ds.Tables[19];
                    if (dtDeptUpdate != null)
                    {
                        
                        try
                        {
                        HTMLCode_deptUpdate += "<marquee direction='up' scrolldelay='350' onmouseover='this.stop();' onmouseout='this.start();' style='height:201px;'><div>Following departments are regularly contributing for updation of KBUNL Intranet </div> <hr/>";
                        for (int i = 0; i < dtDeptUpdate.Rows.Count; i++)
                        {
							//flag_bday = true;
                            HTMLCode_deptUpdate += "<div class='name'> - " +
                                                 "<a target='_blank' href=Department.aspx?dptId=" + dtDeptUpdate.Rows[i]["DeptID"].ToString() + ">" + dtDeptUpdate.Rows[i]["Department"].ToString() + "</a> ("+ dtDeptUpdate.Rows[i]["files"].ToString() +" files) :"   + dtDeptUpdate.Rows[i]["descr"].ToString() + " on " + dtDeptUpdate.Rows[i]["dt"].ToString() + " </div>" ;
                                               
                        }
                        HTMLCode_deptUpdate += "</marquee>";
                        }
                         catch (Exception ex)
                         {
                                HTMLCode_deptUpdate += "</marquee>"+ ex +"</marquee>";
                         }
                    }
                }
            }

            divSafetyTrining.InnerHtml = HTMLCode_safetytrg;
            divMedicalExam.InnerHtml = HTMLCode_medicalexam;
            divHalloffame.InnerHtml = HTMLCode_halloffame;
             divDeptUpdate.InnerHtml = HTMLCode_deptUpdate;


            if (flag_bday)
                divBirthdays.InnerHtml = HTMLCode_Birthday;
            else
                divBirthdays.InnerHtml = "<h4>No birthday this week<h4>";

            if(flag_superannuation)
                divSuperannuation.InnerHtml = HTMLCode_SupperAnnuation;
            else
                divSuperannuation.InnerHtml= "<h4>No superannuation this year<h4>";

            if(flag_welcome)
                divWelcomes.InnerHtml = HTMLCode_Welcomes;
            else
                divWelcomes.InnerHtml = "<h4>No new member joined in last 2 Months<h4>";
			
            divAnnouncements.InnerHtml = HTMLCode_Announcements;

            #region--MessageBox
            string MessageBoxHTML = "";
            if (dtMessage != null && dtMessage.Rows.Count > 0)
            {
                for (int i = 0; i < dtMessage.Rows.Count; i++)
                {
                    string msghtml = "";
                    if (dtMessage.Rows[i]["MessageAttachment"].ToString() != "")
                    {
                        msghtml += "<a href='" + dtMessage.Rows[i]["MessageAttachment_Admin"].ToString() + "' target='_blank' style='font-size:14px; color:#000000;'>CEO Messsage</a>";
                    }
                    string schdhtml = "";
                    if (dtMessage.Rows[i]["ScheduleAttachment"].ToString() != "")
                    {
                        schdhtml += "<a href='" + dtMessage.Rows[i]["ScheduleAttachment_Admin"].ToString() + "' target='_blank' style='font-size:14px; color:#000000;'>CEO Schedule</a>";
                    }

                    MessageBoxHTML += @"
                        <li>
                            <a href='#slide" + (i + 1).ToString() + @"'>
                                <div class='hopbox hopbox2'>
                                    <img src='" + dtMessage.Rows[i]["PhotoPath"].ToString() + @"' alt='' class='hopimg' style='width:auto; max-height:250px;     border: 5px solid #bdbcbc;'/>
								    <div class='name'>" + dtMessage.Rows[i]["Name"].ToString() + @"</div>
								    <div class='designation'>" + dtMessage.Rows[i]["Designation"].ToString() + @"</div>
                                    <table>
                                        <tr>
                                            <td style='text-align:center'>" + msghtml + @"</td>
                                            <td style='text-align:center'>" + schdhtml + @"</td>
                                        </tr>
                                    </table>
								    <div class='message' style='padding-top:10px;     border-bottom: 2px solid #8cd0c3;'>" + dtMessage.Rows[i]["MessageEnglish"].ToString() + @"</div>
                               
                                </div>
                            </a>
                        </li>
                    ";
                }
            }
            divmessagebox.InnerHtml = MessageBoxHTML;
            #endregion
        }
        catch (Exception ex)
        {

        }
    }

    protected void MyDayRenderer(object sender, DayRenderEventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = null;

            string[] parameter = { "@Flag" };
            string[] value = { "Holidays" };
            DB_Status dbs = dba.sp_populateDataSet("SP_Holidays", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int dd = Convert.ToInt16(dt.Rows[i]["dd"].ToString());
                int mm = Convert.ToInt16(dt.Rows[i]["mm"].ToString());
                int yyyy = Convert.ToInt16(dt.Rows[i]["yyyy"].ToString());
                string HT = dt.Rows[i]["HolidayType"].ToString();
                string HolidayEng = dt.Rows[i]["HolidayNameEnglish"].ToString();
                string HolidayHin = dt.Rows[i]["HolidayNameHindi"].ToString();

                if (e.Day.Date == new DateTime(yyyy, mm, dd) && HT == "GH")
                {
                    e.Cell.BackColor = System.Drawing.Color.Red;
                    e.Cell.ToolTip = HolidayEng;
                    e.Cell.Text = e.Day.DayNumberText + "\n<br/>" + HolidayEng;
                }

                if (e.Day.Date == new DateTime(yyyy, mm, dd) && HT == "RH")
                {
                    e.Cell.BackColor = System.Drawing.Color.RoyalBlue;
                    e.Cell.ToolTip = HolidayEng;
                    e.Cell.Text = e.Day.DayNumberText + "\n<br/>" + HolidayEng;
                }

               
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ScheduleRenderer(object sender, DayRenderEventArgs e)
    {
        try
        {
            DataTable dtDates = new DataTable();
            dtDates = null;
            DataTable dtSchedules = new DataTable();
            dtSchedules = null;

            string[] parameter = { "@Flag" };
            string[] value = { "Schedule_forHomePage" };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Event_calendar", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    dtDates = ds.Tables[0];
                    dtSchedules = ds.Tables[1];
                }
            }

            string Schedule = "";
            string Tooltip = "";
            if (e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
                 {
                    
                      e.Cell.BackColor=System.Drawing.Color.Pink ;
                        e.Cell.ForeColor =System.Drawing.Color.Blue ;
                 }
            for (int i = 0; i < dtDates.Rows.Count; i++)
            {
                int dd = Convert.ToInt16(dtDates.Rows[i]["dd"].ToString());
                int mm = Convert.ToInt16(dtDates.Rows[i]["mm"].ToString());
                int yyyy = Convert.ToInt16(dtDates.Rows[i]["yyyy"].ToString());
                DateTime  myDay = new DateTime(yyyy, mm, dd);
                string HTMLPopup = "";
                //string
                
                 if (e.Day.Date == myDay)
                {
                    string[] Schedules = DisplaySchedules(dd, mm, yyyy, dtSchedules);
                    e.Cell.BackColor = System.Drawing.Color.Red;
                    e.Cell.ForeColor =System.Drawing.Color.White ;
                    //e.Cell.ToolTip = Schedules[0];
                    e.Cell.Text = e.Day.DayNumberText + "\n<br/> GH" + Schedules[1] + Schedules[0];
                    
                }
           
            
            }
            
              
        }
        catch (Exception ex)
        {

        }
    }
    protected string[] DisplaySchedules(int dd, int mm, int yyyy, DataTable dtSchedules)
    {
        string[] Schedule = new string[2]; ;
        string HTMLPopup = "<div class='schedulepopup'>";
        string CellText = "";
        int count = 0;
        for (int i = 0; i < dtSchedules.Rows.Count; i++)
        {
            int dds = Convert.ToInt16(dtSchedules.Rows[i]["dd"].ToString());
            int mms = Convert.ToInt16(dtSchedules.Rows[i]["mm"].ToString());
            int yyyys = Convert.ToInt16(dtSchedules.Rows[i]["yyyy"].ToString());
            if (dds == dd && mms == mm && yyyys == yyyy)
            {
                string TitleEnglish = dtSchedules.Rows[i]["TitleEnglish"].ToString();
                string TitleHindi = dtSchedules.Rows[i]["TitleHindi"].ToString();
                string EventTime = dtSchedules.Rows[i]["EventTime"].ToString();
                string DescEnglish = dtSchedules.Rows[i]["DescEnglish"].ToString();
                string DescHindi = dtSchedules.Rows[i]["DescHindi"].ToString();
                string Attachment = dtSchedules.Rows[i]["Attachment"].ToString();

                HTMLPopup += "<div class='box'>";
                HTMLPopup += "<img src='../images/arrowup.png' class='arrow'/>";
                HTMLPopup += "  <table width='100%'><tr>";
               // HTMLPopup += "  <td width='60px'>" + EventTime + "</td>";
                HTMLPopup += "  <td align='left'>" + TitleEnglish + "</td>";
                if (Attachment != "" && Attachment != "NA")
                    HTMLPopup += "  <td width='20px'><a href='" + Attachment + "' target='_blank'><img src='../images/download-green.png' alt='' width='20px'/></a></td>";
                else
                    HTMLPopup += "  <td width='20px'>&nbsp;</td>";
                HTMLPopup += "  </tr></table>";
                HTMLPopup += "<p>" + DescEnglish + "</p>";
                HTMLPopup += "</div>";

                if (CellText == "")
                {
                    if (DescEnglish.Length > 20)
                        CellText = DescEnglish.Substring(0, 20) + "...";
                    else
                        CellText = DescEnglish;
                    count = 0;
                }
                else
                {
                    count += 1;
                }
            }
        }
        HTMLPopup += "</div>";
        Schedule[0] = HTMLPopup;

        if (count > 0)
        {
            CellText = CellText + " +" + count.ToString();
        }
        Schedule[1] = CellText;
        return Schedule;
    }

    protected void hidepanels()
    {
        divCirculars.Visible = false;
        divEvents.Visible = false;
        divNotices.Visible = false;
        divNews.Visible = false;
		divInformation.Visible = false;
        divGGMSchedule.Visible = false;

        linkCirculars.CssClass = "";
        linkEvents.CssClass = "";
        //linkNotices.CssClass = "";
        //linkNews.CssClass = "";
		linkInformation.CssClass = "";
        linkGGMSchedule.CssClass = "";
    }
    protected void show_circulars(object sender, EventArgs e)
    {
        hidepanels();
        divCirculars.Visible = true;
        linkCirculars.CssClass = "active";
    }
    protected void show_events(object sender, EventArgs e)
    {
        hidepanels();
        divEvents.Visible = true;
        linkEvents.CssClass = "active";
    }
    protected void show_notices(object sender, EventArgs e)
    {
        hidepanels();
        divNotices.Visible = true;
        //linkNotices.CssClass = "active";
    }
    protected void show_news(object sender, EventArgs e)
    {
        hidepanels();
        divNews.Visible = true;
        //linkNews.CssClass = "active";
    }
	protected void show_information(object sender, EventArgs e)
    {
        hidepanels();
        divInformation.Visible = true;
        linkInformation.CssClass = "active";
    }
    protected void show_ggmschedule(object sender, EventArgs e)
    {
        hidepanels();
        divGGMSchedule.Visible = true;
        linkGGMSchedule.CssClass = "active";
    }
    protected void hiderightpanels()
    {
        divBirthdays.Visible = false;
        divSuperannuation.Visible = false;
        divWelcomes.Visible = false;

        linkBirthdays.CssClass = "";
        linkSuperannuation.CssClass = "";
        linkWelcome.CssClass = "";
    }
    protected void show_birthdays(object sender, EventArgs e)
    {
        hiderightpanels();
        divBirthdays.Visible = true;
        linkBirthdays.CssClass = "active";
    }
    protected void show_superannuations(object sender, EventArgs e)
    {
        hiderightpanels();
        divSuperannuation.Visible = true;
        linkSuperannuation.CssClass = "active";
    }
    protected void show_welcomes(object sender, EventArgs e)
    {
        hiderightpanels();
        divWelcomes.Visible = true;
        linkWelcome.CssClass = "active";
    }


    //Popup
    public void BindPopupData()
    {
        string PopupHTML = "";
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "PublicView", "0" };
            DB_Status dbs = dba.sp_populateDataSet("SP_Popup", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string TitleEnglish = dt.Rows[0]["TitleEnglish"].ToString();
                        string TitleHindi = dt.Rows[0]["TitleHindi"].ToString();
                        string PopupType = dt.Rows[0]["PopupType"].ToString();
                        string PopupImage = dt.Rows[0]["PopupImage"].ToString();
                        string PopupVideo = dt.Rows[0]["PopupVideo"].ToString();
                        string PopupContentEnglish = dt.Rows[0]["PopUpContentEnglish"].ToString();
                        string PopupContentHindi = dt.Rows[0]["PopUpContentHindi"].ToString();
                        string LinkAttachmentURL = dt.Rows[0]["LinkAndAttachment"].ToString();

                        PopupHTML += "<div class='popup'>";
                        PopupHTML += "  <div class='title'>" + TitleEnglish + "</div>";
                        PopupHTML += "  <div class='popupcontent'>";
                        if (PopupType == "Image")
                        {
                            if (PopupImage != "" && PopupImage != "NA")
                            {
                                PopupHTML += "  <img src='" + PopupImage + "' alt=''/>";
                            }
                        }
                        if (PopupType == "Video")
                        {
                            if (PopupVideo != "" && PopupVideo != "NA")
                            {
                                PopupHTML += @" <video width='100%' controls=''>
                                                <source src='" + PopupVideo + @"' type='video/mp4'>
                                                Your browser does not support the video tag.
                                                </video>";
                            }
                        }
                        if (PopupContentEnglish != "")
                        {
                            PopupHTML += "  <p>" + PopupContentEnglish + "</p>";
                        }
                        if (LinkAttachmentURL != "" && LinkAttachmentURL != "NA")
                        {
                            PopupHTML += "  <a href='" + LinkAttachmentURL + "' target='_blank'>Read more</a>";
                        }
                        PopupHTML += "  </div>";
                        PopupHTML += "<span class='close' onclick='closepopup()'><img src='../images/close.png' alt=''/></span>";
                        PopupHTML += "</div>";
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        if (PopupHTML != "")
        {
            divPopupContainer.InnerHtml = PopupHTML;
            divPopupContainer.Visible = true;
        }
        else
        {
            divPopupContainer.InnerHtml = "";
            divPopupContainer.Visible = false;
        }
    }

    protected void hideschedulepanels()
    {
        divSafetyTrining.Visible = false;
        divMedicalExam.Visible = false;
        divHalloffame.Visible = false;

        linkSafetyTrining.CssClass = "";
        linkMedicalExam.CssClass = "";
        linkHalloffame.CssClass = "";
    }
    protected void linkSafetyTrining_Click(object sender, EventArgs e)
    {
        hideschedulepanels();
        divSafetyTrining.Visible = true;
        linkSafetyTrining.CssClass = "active";
    }

    protected void linkMedicalExam_Click(object sender, EventArgs e)
    {
        hideschedulepanels();
        divMedicalExam.Visible = true;
        linkMedicalExam.CssClass = "active";
    }

    protected void linkHalloffame_Click(object sender, EventArgs e)
    {
        hideschedulepanels();
        divHalloffame.Visible = true;
        linkHalloffame.CssClass = "active";
    }
}