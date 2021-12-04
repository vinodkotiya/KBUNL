using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class download : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            LoadDownloads();
        }
    }

    protected void LoadDownloads()
    {
        try
        {
            string[] parameter = { "@Flag" };
            string[] value = { "PublicView" };
            DB_Status dbs = dba.sp_populateDataSet("SP_Download_Category", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    grdviewCategory.DataSource = dt;
                    grdviewCategory.DataBind();
                    if (hdfCategoryId.Value == "0")
                    {
                        string DownloadCategoryId = Convert.ToString(dt.Rows[0]["DownloadCategoryId"]);
                        BindDownloads(DownloadCategoryId);
                    }
                    else
                    {
                        BindDownloads(hdfCategoryId.Value);
                    }

                }
            }
        }
        catch (Exception ex)
        { }
    }

    protected void grdviewCategory_rowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkbtnCategory = (LinkButton)e.Row.FindControl("lnkbtnCategory");
            if (e.Row.RowIndex == 0)
                lnkbtnCategory.CssClass = "linkbtn active";
            else
                lnkbtnCategory.CssClass = "linkbtn";
        }
    }
    protected void lnkbtnCategory_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkbtnCategory = sender as LinkButton;
            GridViewRow grdrow = (GridViewRow)lnkbtnCategory.NamingContainer;
            HiddenField hdfDownloadCategoryId = (HiddenField)grdrow.FindControl("hdfDownloadCategoryId");
            hdfCategoryId.Value = hdfDownloadCategoryId.Value;
            BindDownloads(hdfCategoryId.Value);
            for (int i = 0; i < grdviewCategory.Rows.Count; i++)
            {
                HiddenField hdfCatId = (HiddenField)grdviewCategory.Rows[i].FindControl("hdfDownloadCategoryId");
                LinkButton lnkbtn = (LinkButton)grdviewCategory.Rows[i].FindControl("lnkbtnCategory");
                if(hdfCatId.Value== hdfCategoryId.Value)
                    lnkbtn.CssClass = "linkbtn active";
                else
                    lnkbtn.CssClass = "linkbtn";
            }
        }
        catch (Exception ex)
        { }
    }

    public void BindDownloads(string DownloadCategoryId)
    {
        try
        {
            string[] parameter = { "@Flag", "@DownloadCategoryId" };
            string[] value = { "PublicView", DownloadCategoryId };
            DB_Status dbs = dba.sp_populateDataSet("SP_Downloads",2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ltrDownloads.Text = "";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ltrDownloads.Text = ltrDownloads.Text + "<table class='downloadbox'>";
                            ltrDownloads.Text = ltrDownloads.Text + "<tr>";
                            ltrDownloads.Text = ltrDownloads.Text + "<td class='title'>" + Convert.ToString(dt.Rows[i]["TitleHindi"]) + "</td>";
                            ltrDownloads.Text = ltrDownloads.Text + "<td class='download'><a target='_blank' href='../" + Convert.ToString(dt.Rows[i]["AttachmentUrl"]) + "'><img src='../images/download-white.png' alt='' /></a></td>";
                            ltrDownloads.Text = ltrDownloads.Text + "</tr>";
                            ltrDownloads.Text = ltrDownloads.Text + "</table>";
                        }
                        lblMsg.Text = "";
                        divMsg.Visible = false;
                        divMsg.Attributes["class"] = "";
                    }
                    else
                    {
                        divMsg.Attributes["class"] = "divmsgerror";
                        lblMsg.Text = "रिकॉर्ड नहीं मिला";
                        divMsg.Visible = true;
                        ltrDownloads.Text = "";
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
}