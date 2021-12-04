using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Script.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ClassLibrary;
public partial class WCCommon_WCTopBar : System.Web.UI.UserControl
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divCopyright.InnerHtml = ATSPLLib.GetFooterText("kbunl", "Hindi");

            string IPAdd;
            IPAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAdd))
                IPAdd = Request.ServerVariables["REMOTE_ADDR"];
            HDN_IPAddress.Value = IPAdd;
            lblIPAddress.Text = IPAdd;
            TotalVisitorCount();
            BindAboutUsInformation();
        }
    }

    public void TotalVisitorCount()
    {
        try
        {
            string[] parameter = { "" };
            string[] value = { "" };
            DB_Status dbs = dba.sp_populateDataSet("GetTotalVisitorCount", 0, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dtVisitor = ds.Tables[0];
                    if (dtVisitor != null && dtVisitor.Rows.Count > 0)
                    {
                        lblVisitor.Text =Convert.ToString(dtVisitor.Rows[0]["VisitorCount"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {

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
                        lblAddress.Text = Convert.ToString(dt.Rows[0]["AddressHindi"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

}