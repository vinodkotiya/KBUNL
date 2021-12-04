using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Globalization;

public partial class Admin_news_post : System.Web.UI.Page
{
    Class1 mod = new Class1();
    DB_Access obj = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] == null)
        {
            Response.Redirect("../login.aspx");
        }
        else
        {
            //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_feedback");
            //menuli.Attributes["class"] = "active";
            if (!IsPostBack)
            {
                Fill_Logs();
            }
        }
    }

    protected void Fill_Logs()
    {
        try
        {
            string[] parameter = {};
            string[] value = {};
            DB_Status dbs = obj.sp_populateDataSet("SP_ViewLog", 0, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                grd_logs.DataSource = ds.Tables[0];
                grd_logs.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void grd_logs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_logs.PageIndex = e.NewPageIndex;
        Fill_Logs();
    }  
 
}