using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using ClassLibrary;


public partial class WCCommon_WCTopBar : System.Web.UI.UserControl
{

    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    public StringBuilder MenuBar = new StringBuilder();
    public StringBuilder policy = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblDateTime.Text = System.DateTime.Now.ToString("dd MMM yyyy hh:mm tt");
        if (!IsPostBack)
        {
            string IPAdd;
            IPAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAdd))
                IPAdd = Request.ServerVariables["REMOTE_ADDR"];
            lblIPAddress.Text = IPAdd;

            divProjectInformation.InnerHtml = ATSPLLib.GetProjectInformation("kbunl", "English");
            BindAboutUsInformation();
            LoadMenu();
            if (Session["Medium"] != null)
            {
                hdfMedium.Value = Convert.ToString(Session["Medium"]);
                if (hdfMedium.Value == "Hindi")
                    imgMedium.ImageUrl = "../../images/toggle-hindi.png";
                else
                    imgMedium.ImageUrl = "../../images/toggle-english.png";
            }
            imgMedium.ImageUrl = "../../images/toggle-english.png";
        }
    }
    public void BindAboutUsInformation()
    {
        try
        {
            string[] parameter = { "@Flag" };
            string[] value = { "View" };
            DB_Status dbs = dba.sp_populateDataSet("SP_AboutUs", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        Page.Title= Convert.ToString(dt.Rows[0]["ProjectNameEnglish"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void LoadMenu()
    {
        try
        {
            string[] parameter = { "" };
            string[] value = { "" };
            DB_Status dbs = dba.sp_populateDataSet("SP_Menu_Load_For_HomePage", 0, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dtMenu = ds.Tables[0];
                    ltrMenu.Text = "<div id='smoothmenu1' align='left' class='ddsmoothmenu' style='z-index:1000;'>";
                    if (dtMenu.Rows.Count > 0)
                    {
                        ltrMenu.Text = ltrMenu.Text + "<ul>";
                        for (int i = 0; i < dtMenu.Rows.Count; i++)
                        {
                            if (Convert.ToString(dtMenu.Rows[i]["MenuLevel"]) == "Level1")
                            {
                                string target = "";
                                if (dtMenu.Rows[i]["URLOpenIn"].ToString() == "Open In New Page")
                                    target = " target='_blank' ";

                                ltrMenu.Text = ltrMenu.Text + "<li><a href='" + Convert.ToString(dtMenu.Rows[i]["LinkURL"]) + "' " + target + ">" + Convert.ToString(dtMenu.Rows[i]["MenuTitle"]) + "</a>";
                                string RID_Menu = Convert.ToString(dtMenu.Rows[i]["RID"]);
                                LoadMenuLevel2(RID_Menu, dtMenu);
                                ltrMenu.Text = ltrMenu.Text + "</li>";
                            }
                        }
                        ltrMenu.Text = ltrMenu.Text + "</ul>";
                    }
                    ltrMenu.Text = ltrMenu.Text + "<div style='clear: left'></div>";
                    ltrMenu.Text = ltrMenu.Text + "</div>";
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void LoadMenuLevel2(string RID_Menu, DataTable dtMenu)
    {
        try
        {
            bool isLevel2Found = false;
            for (int i = 0; i < dtMenu.Rows.Count; i++)
            {
                if (Convert.ToString(dtMenu.Rows[i]["RID_Menu"]) == RID_Menu && Convert.ToString(dtMenu.Rows[i]["MenuLevel"]) == "Level2")
                {
                    if (isLevel2Found == false)
                    {
                        ltrMenu.Text = ltrMenu.Text + "<ul>";
                        isLevel2Found = true;
                    }

                    string target = "";
                    if (dtMenu.Rows[i]["URLOpenIn"].ToString() == "Open In New Page")
                        target = " target='_blank' ";

                    ltrMenu.Text = ltrMenu.Text + "<li><a href ='" + Convert.ToString(dtMenu.Rows[i]["LinkURL"]) + "' " + target + ">" + Convert.ToString(dtMenu.Rows[i]["MenuTitle"]) + "</a>";
                    string RID_Menu_Level2 = Convert.ToString(dtMenu.Rows[i]["RID_Menu_Level2"]);
                    LoadMenuLevel3(RID_Menu, RID_Menu_Level2, dtMenu);
                    ltrMenu.Text = ltrMenu.Text + "</li>";
                }
            }
            if (isLevel2Found)
            {
                ltrMenu.Text = ltrMenu.Text + "</ul>";
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void LoadMenuLevel3(string RID_Menu, string RID_Menu_Level2, DataTable dtMenu)
    {
        try
        {
            bool isLevel3Found = false;

            for (int i = 0; i < dtMenu.Rows.Count; i++)
            {
                if (Convert.ToString(dtMenu.Rows[i]["RID_Menu"]) == RID_Menu && Convert.ToString(dtMenu.Rows[i]["RID_Menu_Level2"]) == RID_Menu_Level2 && Convert.ToString(dtMenu.Rows[i]["MenuLevel"]) == "Level3")
                {
                    if (isLevel3Found == false)
                    {
                        ltrMenu.Text = ltrMenu.Text + "<ul>";
                        isLevel3Found = true;
                    }

                    string target = "";
                    if (dtMenu.Rows[i]["URLOpenIn"].ToString() == "Open In New Page")
                        target = " target='_blank' ";

                    ltrMenu.Text = ltrMenu.Text + "<li><a href ='" + Convert.ToString(dtMenu.Rows[i]["LinkURL"]) + "' " + target + ">" + Convert.ToString(dtMenu.Rows[i]["MenuTitle"]) + "</a>";
                    ltrMenu.Text = ltrMenu.Text + "</li>";
                }
            }
            if (isLevel3Found)
            {
                ltrMenu.Text = ltrMenu.Text + "</ul>";
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkbtnMedium_Click(object sender, EventArgs e)
    {
        Session["Medium"] = "Hindi";
        Response.Redirect(Request.RawUrl.Replace("/English", "/Hindi"));
    }

    public LinkButton GetThemeButton_Blue
    {
        get { return lbtn_themeblue; }
    }
    public LinkButton GetThemeButton_Green
    {
        get { return lbtn_themegreen; }
    }
    public LinkButton GetThemeButton_Purple
    {
        get { return lbtn_themepurple; }
    }
    public LinkButton GetThemeButton_Orange
    {
        get { return lbtn_themeorange; }
    }
    public LinkButton GetThemeButton_Red
    {
        get { return lbtn_themered; }
    }

    protected void lbtn_themeblue_Click(object sender, EventArgs e)
    {
        Session["Theme"] = "theme_blue";
        Response.Redirect(Request.RawUrl);
    }
    protected void lbtn_themegreen_Click(object sender, EventArgs e)
    {
        Session["Theme"] = "theme_green";
        Response.Redirect(Request.RawUrl);
    }
    protected void lbtn_themepurple_Click(object sender, EventArgs e)
    {
        Session["Theme"] = "theme_purple";
        Response.Redirect(Request.RawUrl);
    }
    protected void lbtn_themeorange_Click(object sender, EventArgs e)
    {
        Session["Theme"] = "theme_orange";
        Response.Redirect(Request.RawUrl);
    }
    protected void lbtn_themered_Click(object sender, EventArgs e)
    {
        Session["Theme"] = "theme_red";
        Response.Redirect(Request.RawUrl);
    }

    protected void btnDirectorySearch_Click(object sender, EventArgs e)
    {
        string search = "all";
        if (txtDirectorySearch.Text.Trim() != "")
            search = txtDirectorySearch.Text.Trim();
        Response.Redirect("directory-listview.aspx?search=" + search, false);
    }
}