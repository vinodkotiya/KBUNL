using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;

public partial class admin_Admin : System.Web.UI.MasterPage
{
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            adminchangepassword.Visible = false;
            empchangepassword.Visible = false;
            empprofileupdate.Visible = false;
            hopchangepass.Visible = false;
            if (Session["EmpID"] != null)
            {
                hdfEmployee_Id.Value = Convert.ToString(Session["EmpID"]);
                if (!string.IsNullOrEmpty(hdfEmployee_Id.Value))
                    BindEmployeeModulePermission();
                if (Session["username"] != null)
                {
                    hdfUserName.Value = Convert.ToString(Session["username"]);
                    if (hdfUserName.Value != "admin")
                    {
                        hdfDepartmentName.Value = Convert.ToString(Session["deprt_name"]);
                        lblEmpName.Text = ": " + hdfDepartmentName.Value + " Department";
                        empchangepassword.Visible = true;
                        empprofileupdate.Visible = true;
                    }
                    else
                    {
                        lblEmpName.Text = ": Admin";
                        adminchangepassword.Visible = true;
                        hopchangepass.Visible = true;
                    }
                }
            }
            if ((Session["ActiveLink"]) != null)
                GetActiveLink();            
        }
    }

    public void BindEmployeeModulePermission()
    {
        string result = "";
        string[] parameter = { "@EID","@Username" };
        string[] value = { hdfEmployee_Id.Value, Session["username"].ToString() };
        DB_Status dbs = dba.sp_populateDataSet("SP_Get_Employee_Module_Permission", 2, parameter, value);
        if (dbs.OperationStatus.ToString() == "Success")
        {
            DataSet ds = dbs.ResultDataSet;
            if (ds.Tables.Count > 0)
            {
                DataTable dtGroup = ds.Tables[0];
                rptModuleGroup.DataSource = dtGroup;
                rptModuleGroup.DataBind();                
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["AdminName"] = null;
        Session["username"] = null;
        Session["EID"] = null;
        Session["EmpName"] = null;
        Session["DeptID"] = null;
        Session["UserRole"] = null;
        Session.Abandon();//Abandon session
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Redirect("../login.aspx");
    }


    protected void rptModuleGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblModuleGroup = (Label)e.Item.FindControl("lblModuleGroup");
            Repeater rptModule = (Repeater)e.Item.FindControl("rptModule");
            if (!string.IsNullOrEmpty(lblModuleGroup.Text.Trim()))
                BindGroupModule(lblModuleGroup.Text, rptModule);

        }
    }

    public void BindGroupModule(string GroupName, Repeater rptModule)
    {
        string[] parameter1 = { "@EID", "@Group", "@Username" };
        string[] value1 = { hdfEmployee_Id.Value, GroupName, Session["username"].ToString() };
        DB_Status dbs1 = dba.sp_populateDataSet("SP_Get_Employee_Module", 3, parameter1, value1);
        if (dbs1.OperationStatus.ToString() == "Success")
        {
            DataSet ds1 = dbs1.ResultDataSet;
            if (ds1.Tables.Count > 0)
            {
                DataTable dtModule = ds1.Tables[0];
                rptModule.DataSource = dtModule;
                rptModule.DataBind();
            }
        }
    }
    
     protected void lnkbtnModule_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtnModule = sender as LinkButton;
        RepeaterItem rptitem = (RepeaterItem)lnkbtnModule.NamingContainer;
        Repeater rptModule = (Repeater)rptitem.Parent.Parent.FindControl("rptModule");
        HiddenField hdfLinkURL = (HiddenField)rptitem.FindControl("hdfLinkURL");
        string ModuleName = lnkbtnModule.Text.Trim();
        Session["ActiveLink"] = ModuleName;
        GetActiveLink();
        Response.Redirect(hdfLinkURL.Value);
    }

    public void GetActiveLink()
    {
        string ActiveLink = Convert.ToString(Session["ActiveLink"]);
        for (int i = 0; i < rptModuleGroup.Items.Count; i++)
        {
            Repeater rptModule = (Repeater)rptModuleGroup.Items[i].FindControl("rptModule");
            for (int j = 0; j < rptModule.Items.Count; j++)
            {
                LinkButton rptlnkbtnModule = (LinkButton)rptModule.Items[j].FindControl("lnkbtnModule");
                System.Web.UI.HtmlControls.HtmlControl menuli=(HtmlControl)rptModule.Items[j].FindControl("li1");
                if (ActiveLink == rptlnkbtnModule.Text.Trim())
                    menuli.Attributes["class"] = "active";
                else
                    menuli.Attributes["class"] = "";
            }
        }       
    }

}
