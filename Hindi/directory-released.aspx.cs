using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using OfficeOpenXml.Style;

public partial class directory : System.Web.UI.Page
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
            string search = "all";
            if (Request.QueryString["search"] != null)
            {
                search = Request.QueryString["search"];
            }
            if (search == "")
                search = "all";

            hfsearch.Value = search;
            LoadEmployeeDirectory();
        }
    }
    public void LoadEmployeeDirectory()
    {
        try
        {
            DataTable dt = new DataTable();
            string[] parameter = { "@Search" };
            string[] value = { hfsearch.Value.Trim()};
            DB_Status dbs = dba.sp_populateDataSet("SP_Directory_Released", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];                    
                }
            }

            gridDirectory.DataSource = dt;
            gridDirectory.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    protected void gridDirectory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridDirectory.PageIndex = e.NewPageIndex;
        LoadEmployeeDirectory();
    }

    protected void btnDirectorySearch_Click(object sender, EventArgs e)
    {
        string search = "all";
        if (txtDirectorySearch.Text.Trim() != "")
            search = txtDirectorySearch.Text.Trim();
        hfsearch.Value = search;
        LoadEmployeeDirectory();
    }
}