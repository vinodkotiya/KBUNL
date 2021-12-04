using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WCCommon_WCEmployeeServices : System.Web.UI.UserControl
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    public DataTable dtEmpServices { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadEmployeeServices();
        }
    }


    public void LoadEmployeeServices()
    {
        try
        {
            string[] parameter = { "@Flag" };
            string[] value = { "Load" };
            DB_Status dbs = dba.sp_populateDataSet("SP_Employee_Service", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    dtEmpServices = ds.Tables[0];
                }
            }

            string HTMLCode = "";
            if (dtEmpServices!=null)
            {
                for(int i=0;i<dtEmpServices.Rows.Count;i++)
                {
                    if(dtEmpServices.Rows[i]["URLOpenIn"].ToString()== "Open In New Page")
                        HTMLCode += "<a href='" + dtEmpServices.Rows[i]["ServiceLink"].ToString() + "' target='_blank'><span class='imgbox'><img src='../" + dtEmpServices.Rows[i]["Icon"].ToString() + "' alt=''></span><span class='title'>" + dtEmpServices.Rows[i]["ServiceTitleHindi"].ToString() + "</span></a>";
                    else
                        HTMLCode += "<a href='" + dtEmpServices.Rows[i]["ServiceLink"].ToString() + "'><span class='imgbox'><img src='../" + dtEmpServices.Rows[i]["Icon"].ToString() + "' alt=''></span><span class='title'>" + dtEmpServices.Rows[i]["ServiceTitleHindi"].ToString() + "</span></a>";
                }
            }
            divEmployeeServices.InnerHtml = HTMLCode;
        }
        catch (Exception ex)
        { }
    }
}