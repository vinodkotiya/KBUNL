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
            if (Session["DeptAdminEmpCode"] == null)
            {
                Response.Redirect("dept-admin-hod.aspx");
            }
            else
            {
               
                if(Request.QueryString["dept"] ==null)
                {
                    Session.Remove("EmpCode");
                    Response.Redirect("dept-admin-hod.aspx");
                }
                //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_admin_hod");
                //menuli.Attributes["class"] = "active";
               
                id_adminbreadcrumb.InnerHtml = "Admin > Form Module > Emp Code : " + Session["DeptAdminEmpCode"].ToString();
                if (!IsPostBack)
                {
                    hdfEmpCode.Value = Convert.ToString(Session["DeptAdminEmpCode"]);
                    hdfEmId.Value = Convert.ToString(Session["DeptAdminEmpId"]);
                    if(!string.IsNullOrEmpty(hdfEmId.Value) && hdfEmId.Value !="0")
                    FillModule();
                }
            }

        }
    }

    protected void FillModule()
    {
        try
        {
            string[] parameter = { "@Flag", "@EID" };
            string[] value = { "View", hdfEmId.Value};
            DB_Status dbs = obj.sp_populateDataSet("Sp_Assign_Module_Permission", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                grdModule.DataSource = ds.Tables[0];
                grdModule.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void grd_feedback_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdModule.PageIndex = e.NewPageIndex;
        FillModule();
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {

      
        try
        {
            foreach (GridViewRow row in grdModule.Rows)
            {
                CheckBox chk = row.Controls[0].FindControl("chkModule") as CheckBox;
                HiddenField hdfModuleRID = row.Controls[0].FindControl("hdfModuleRID") as HiddenField;
                bool v = chk.Checked;

                string[] parameter = { "@Flag", "@Type" };
                string[] value = { "module", "Admin" };
                DB_Status dbs = obj.sp_readSingleData("Sp_Assign_Module_Permission", 2, parameter, value);
                if (dbs.OperationStatus.ToString() == "Success")
                {
                    string result = dbs.SingleResult;
                   
                }
            }

        }
        catch (Exception ex)
        {
        }

    }

    protected void chkModule_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
           
            CheckBox chkBox = (CheckBox)sender;
            GridViewRow grdrow = (GridViewRow)chkBox.Parent.Parent;
            int rowindex = grdrow.RowIndex;
            CheckBox chkModule = (CheckBox)grdModule.Rows[rowindex].FindControl("chkModule");
            HiddenField hdfModuleIdGrd = (HiddenField)grdModule.Rows[rowindex].FindControl("hdfModuleIdGrd");

            string jsFunc = "";
            string permission="";
            if (chkModule.Checked)
            {
                permission ="true";
                jsFunc = "moduleAssgin('successfully assigned')";
            }
            else
            {
                permission ="false";
                jsFunc = "moduleAssgin('successfully unassigned')";
            }
            string result = "";
            string[] parameter = { "@Flag", "@ModuleId", "@EID", "@IsModulePermission" };
            string[] value = { "Permission", hdfModuleIdGrd.Value, hdfEmId.Value, permission };
            DB_Status dbs = obj.sp_readSingleData("Sp_Assign_Module_Permission", 4, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                result = dbs.SingleResult;
            }
            if (result == "success")
            {
               if(permission == "true")
                ShowMessage("Info", "Permission assigned successfully");
               else
                 ShowMessage("Info", "Permission unassigned successfully");
            }

        }
        catch(Exception ex)
        {

        }
    }
    protected void ShowMessage(string msgtype, string msg)
    {
        if (msgtype.ToUpper() == "INFO")
            divmsg.Attributes["class"] = "fixelement info";
        else if (msgtype.ToUpper() == "ERROR")
            divmsg.Attributes["class"] = "fixelement error";
        else
            divmsg.Attributes["class"] = "fixelement";
        divmsg.Visible = true;
        lblMsg.Text = msg;
    }
    protected void lnkbtnMsg_Click(object sender, EventArgs e)
    {
        divmsg.Visible = false;
        lblMsg.Text = "";
    }
}