using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class about_us : System.Web.UI.Page
{
    Class1 mod = new Class1();
    DB_Access obj = new DB_Access();

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
            if (Request.QueryString["Link"] != null)
            {
                string ApplicationLink = Request.QueryString["Link"];
                if (ApplicationLink == "Sipat Turbine Differential")
                {
                    divApplicationName.InnerHtml = "सीपत टरबाइन डिफरेंशियल";
                    divIframe.Src = "http://10.1.215.70/korbapi/chkpasswd.asp?progname=tickerunit-1-sipat.asp";
                }
                if (ApplicationLink == "Sipat Running Params")
                {
                    divApplicationName.InnerHtml = "सीपत रनिंग परम";
                    divIframe.Src = "http://10.1.215.70/korbapi/chkpasswd.asp?progname=tickersipatpi.asp";
                }
                if (ApplicationLink == "Rajgarh Daily Generation Report")
                {
                    divApplicationName.InnerHtml = "राजगढ़ डेली जनरेशन रिपोर्ट";
                    divIframe.Src = "http://10.1.215.70/sipat-korba/rajgarh/chparchive4.asp";
                }
                if (ApplicationLink == "Mandsaur Daily Generation Report")
                {
                    divApplicationName.InnerHtml = "मंदसौर डेली जनरेशन रिपोर्ट";
                    divIframe.Src = "http://10.1.215.70/sipat-korba/mandsaur1/mandsaurdaily.asp";
                }
                if (ApplicationLink == "PI Aberation Water Chem Data")
                {
                    divApplicationName.InnerHtml = "पीआई एबरेशन वॉटर केम डेटा";
                    divIframe.Src = "http://10.1.215.70/korbapi/chkpasswd.asp?progname=datefilteraberationwater.asp";
                }
                if (ApplicationLink == "PI Aberation Statistical Datal")
                {
                    divApplicationName.InnerHtml = "पीआई एबरेशन सांख्यिकीय डेटा";
                    divIframe.Src = "http://10.1.215.70/korbapi/chkpasswd.asp?progname=datefilteraberationwater.asp";
                }
                if (ApplicationLink == "PI Aberation Data")
                {
                    divApplicationName.InnerHtml = "पीआई एबरेशन डेटा";
                    divIframe.Src = "http://10.1.215.70/korbapi/chkpasswd.asp?progname=datefilteraberationnew.asp";
                }                
                if (ApplicationLink == "Korba Running Params")
                {
                    divApplicationName.InnerHtml = "कोरबा रनिंग परम";
                    divIframe.Src = "http://10.1.215.70/korbapi/chkpasswd.asp?progname=tickerkorbapi.asp";
                }                
                if (ApplicationLink == "Auxillary Power Consumption")
                {
                    divApplicationName.InnerHtml = "सहायक बिजली की खपत";
                    divIframe.Src = "http://10.1.215.65/mis/apc.asp";
                }
                if (ApplicationLink == "Abberation Statistical Data")
                {
                    divApplicationName.InnerHtml = "एबरेशन सांख्यिकीय डेटा";
                    divIframe.Src = "http://10.1.215.65/mis/com/stationabberationsummary.asp";
                }                
            }
        }
    }
}