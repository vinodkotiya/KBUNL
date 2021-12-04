using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Hindi_employee_welfare : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            LoadDepartmentHomePageData();
        }
    }

    public void LoadDepartmentHomePageData()
    {
        try
        {
            string[] parameter = { "@DeptID" };
            string[] value = { hdfDeptId.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_LoadData_For_Dept_HomePage", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    #region Department
                    DataTable dtDept = ds.Tables[0];
                    if (dtDept != null && dtDept.Rows.Count > 0)
                    {
                        
                    }
                    #endregion

                    #region Department Details
                    DataTable dtDeptDetails = ds.Tables[1];
                    ltrAboutDept.Text = Convert.ToString(dtDeptDetails.Rows[0]["AboutDeptHindi"]);
                    #endregion

                    #region Department Photo Gallery
                    DataTable dtPhotoGallery = ds.Tables[2];
                    if (dtPhotoGallery != null && dtPhotoGallery.Rows.Count > 0)
                    {
                        ltrPhotoGallery.Text = "<div id='owl-project' class='owl-carousel'>";
                        for (int i = 0; i < dtPhotoGallery.Rows.Count; i++)
                        {
                            ltrPhotoGallery.Text = ltrPhotoGallery.Text + "<div class='item'>";
                            ltrPhotoGallery.Text = ltrPhotoGallery.Text + "<img src='../" + Convert.ToString(dtPhotoGallery.Rows[i]["PhotoPath"]) + "' alt='' />";
                            ltrPhotoGallery.Text = ltrPhotoGallery.Text + "</div>";
                        }
                        ltrPhotoGallery.Text = ltrPhotoGallery.Text + "</div>";
                        lnkbtnViewAll.Visible = true;
                    }
                    else
                    {
                        lnkbtnViewAll.Visible = false;
                    }
                    #endregion

                    #region Department Highlight
                    DataTable dtHighLight = ds.Tables[3];
                    ltrHighlight.Text = "<table>";
                    for (int j = 0; j < dtHighLight.Rows.Count; j++)
                    {
                        ltrHighlight.Text = ltrHighlight.Text + "<tr>";
                        ltrHighlight.Text = ltrHighlight.Text + "<td class='img'>";
                        ltrHighlight.Text = ltrHighlight.Text + "<img src='../" + Convert.ToString(dtHighLight.Rows[j]["HighlightImage"]) + "' alt='' />";
                        ltrHighlight.Text = ltrHighlight.Text + "</td>";
                        ltrHighlight.Text = ltrHighlight.Text + "<td class='content'>";
                        ltrHighlight.Text = ltrHighlight.Text + "<h5>" + Convert.ToString(dtHighLight.Rows[j]["HighlightDate"]) + "</h5>";
                        ltrHighlight.Text = ltrHighlight.Text + "<p>" + Convert.ToString(dtHighLight.Rows[j]["DescriptionH"]) + " ";
                        ltrHighlight.Text = ltrHighlight.Text + "</td>";
                        ltrHighlight.Text = ltrHighlight.Text + "</tr>";
                    }
                    ltrHighlight.Text = ltrHighlight.Text + "</table>";
                    #endregion

                    #region Department News
                    DataTable dtNews = ds.Tables[4];
                    ltrNews.Text = "";
                    for (int j = 0; j < dtNews.Rows.Count; j++)
                    {
                        ltrNews.Text = ltrNews.Text + "<table class='box'>";
                        ltrNews.Text = ltrNews.Text + "<tr>";
                        ltrNews.Text = ltrNews.Text + "<td>";
                        ltrNews.Text = ltrNews.Text + "<div class='title'>" + Convert.ToString(dtNews.Rows[j]["TitleHindi"]) + "</div>";
                        ltrNews.Text = ltrNews.Text + "<div class='postdate'>" + Convert.ToString(dtNews.Rows[j]["UploadedOn"]) + "</div>";
                        ltrNews.Text = ltrNews.Text + "</td>";
                        ltrNews.Text = ltrNews.Text + "<td class='download'>";
                        string Attachment = Convert.ToString(dtNews.Rows[j]["Attachment"]);
                        if (!string.IsNullOrEmpty(Attachment))
                            ltrNews.Text = ltrNews.Text + "<a href='../" + Convert.ToString(dtNews.Rows[j]["Attachment"]) + "' target='_blank' ><img src='../images/download.png' alt=''></a>";
                        ltrNews.Text = ltrNews.Text + "</td>";
                        ltrNews.Text = ltrNews.Text + "</tr>";
                        ltrNews.Text = ltrNews.Text + "</table>";
                    }

                    #endregion
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
}