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
            BindAboutUsInformation();
        }
    }

    public void BindAboutUsInformation()
    {
        try
        {
            string maphtmlcode = "";
            string[] parameter = { "@Flag" };
            string[] value = { "View" };
            DB_Status dbs = obj.sp_populateDataSet("SP_AboutUs", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        lblProjectHistory.Text = Convert.ToString(dt.Rows[0]["AboutUsEnglish"]);
                        lblAddress.Text = Convert.ToString(dt.Rows[0]["AddressEnglish"]);
                        lblFlite.Text = Convert.ToString(dt.Rows[0]["FliteEnglish"]);
                        lblHigWay.Text = Convert.ToString(dt.Rows[0]["RoadEnglish"]);
                        lblTrain.Text = Convert.ToString(dt.Rows[0]["TrainEnglish"]);
                        //imgLinkUrl.ImageUrl =Convert.ToString(dt.Rows[0]["GoogleMapLink"]);
                        string maplocation = Convert.ToString(dt.Rows[0]["GoogleMapLink"]);
                        if (maplocation != "")
                        {
                            maphtmlcode += "<iframe width='400' height='235' frameborder='0' scrolling='no' marginheight='0' id='iframemap' runat='server' src='" + maplocation + "' marginwidth='0' style='margin-left:10px;'></iframe>";
                        }
                        if (dt.Rows[0]["AboutUsImage1"].ToString() != null && dt.Rows[0]["AboutUsImage1"].ToString() != "")
                        {
                            image1.ImageUrl = "../" + Convert.ToString(dt.Rows[0]["AboutUsImage1"]);
                            image1.Visible = true;
                        }
                        else
                        {
                            image1.Visible = false;
                        }
                        if (dt.Rows[0]["AboutUsImage2"].ToString() != null && dt.Rows[0]["AboutUsImage2"].ToString() != "")
                        {
                            image2.ImageUrl = "../" + Convert.ToString(dt.Rows[0]["AboutUsImage2"]);
                            image2.Visible = true;
                        }
                        else
                        {
                            image2.Visible = false;
                        }
                        if (dt.Rows[0]["AboutUsImage3"].ToString() != null && dt.Rows[0]["AboutUsImage3"].ToString() != "")
                        {
                            image3.ImageUrl = "../" + Convert.ToString(dt.Rows[0]["AboutUsImage3"]);
                            image3.Visible = true;
                        }
                        else
                        {
                            image3.Visible = false;
                        }
                    }
                }
            }
          //  divmap.InnerHtml = maphtmlcode;
        }
        catch (Exception ex)
        {

        }
    }
}