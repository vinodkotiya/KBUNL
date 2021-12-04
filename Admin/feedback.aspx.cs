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
                Fill_Feedback();
            }
        }
    }

    protected void Fill_Feedback()
    {
        try
        {
            string[] parameter = {"@Flag" };
            string[] value = { "View"};
            DB_Status dbs = obj.sp_populateDataSet("SP_Feedback", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                grd_feedback.DataSource = ds.Tables[0];
                grd_feedback.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void grd_feedback_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_feedback.PageIndex = e.NewPageIndex;
        Fill_Feedback();
    }  
 
}