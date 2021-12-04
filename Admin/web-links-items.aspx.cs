using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class Admin_news_post : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Selected_QuizID"] == null)
        {
            Response.Redirect("web-links-category.aspx");
        }
        else
        {
        
            hfQuizID.Value = Session["Selected_QuizID"].ToString();
            spanQuestionTitle.InnerText = Session["Selected_QuizTitle"].ToString();
        }

        if (!IsPostBack)
        {
            //System.Web.UI.HtmlControls.HtmlControl menuli = WCLHSMenu1.Get_menu_questionbank;
            //menuli.Attributes["class"] = "active";

            //panelAddNew.Visible = false;
            panelView.Visible = true;
            //Fill Articles

            FillQuizOptions();
            displayGridMessage("", "");

            hfOptionID.Value = "";
            txtLinkName.Text = "";
            txtWebLinks.Text = "";
            btnSave.Text = "Save";
        }
    }
    /* Add/Update/Deactivate Banner*/
    protected void displayMessage(string msg, string msgtype)
    {
        lblMsg.Text = msg;
        if (msgtype == "error")
            alert.Attributes["class"] = "alert alert-error";
        else if (msgtype == "info")
            alert.Attributes["class"] = "alert alert-info";
        else
            alert.Attributes["class"] = "";
    }
    protected void displayGridMessage(string msg, string msgtype)
    {
        lblgridmsg.Text = msg;
        if (msgtype == "error")
            alertgrid.Attributes["class"] = "alert alert-error";
        else if (msgtype == "info")
            alertgrid.Attributes["class"] = "alert alert-info";
        else
            alertgrid.Attributes["class"] = "";
    }
    protected void lbtn_AddNew_Click(object sender, EventArgs e)
    {
        panelAddNew.Visible = true;
        panelView.Visible = false;

        displayMessage("", "");
        displayGridMessage("", "");
        hfOptionID.Value = "";

        txtLinkName.Text = "";
        txtWebLinks.Text = "";
        //ddlIsRightOption.SelectedIndex = -1;
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtLinkName.Text.Trim() == "")
                    displayMessage("Please enter Link Name", "error");
                else if (txtWebLinks.Text.Trim() == "")
                    displayMessage("Please enter Web Links", "error");
                else
                {
                    string[] parameter = { "@CategoryID", "@SubCategoryName", "@SubCategoryLink" };
                    string[] value = { hfQuizID.Value, txtLinkName.Text.Trim(), txtWebLinks.Text.Trim() };
                    DB_Status dbs = dba.sp_populateDataSet("Sp_Intranet_Sub_Category_Insert", 3, parameter, value);
                    string result = "";
                    if (dbs.OperationStatus.ToString() == "Success")
                    {
                        DataSet ds = dbs.ResultDataSet;
                        if (ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                result = dt.Rows[0]["Result"].ToString();
                            }
                        }
                    }
                    if (result == "AlreadyExists")
                    {
                        displayMessage("Sorry! Links already exists", "error");
                    }
                    else if (result == "success")
                    {
                        displayMessage("Links successfully added", "info");
                        FillQuizOptions();
                        hfOptionID.Value = "";
                        txtLinkName.Text = "";
                        txtWebLinks.Text = "";
                        
                        btnSave.Text = "Save";
                    }
                }
            }
            else if (btnSave.Text == "Update")
            {
                if (txtLinkName.Text.Trim() == "")
                    displayMessage("Please enter Link Name", "error");
                else if (txtWebLinks.Text.Trim() == "")
                    displayMessage("Please enter Web Links", "error");
                else
                {
                    string[] parameter = { "@SubCategoryID", "@CategoryID", "@SubCategoryName", "@SubCategoryLink" };
                    string[] value = { hfOptionID.Value, hfQuizID.Value, txtLinkName.Text.Trim(), txtWebLinks.Text.Trim() };
                    DB_Status dbs = dba.sp_populateDataSet("Sp_Intranet_Sub_Category_Update", 4, parameter, value);
                    string result = "";
                    if (dbs.OperationStatus.ToString() == "Success")
                    {
                        DataSet ds = dbs.ResultDataSet;
                        if (ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                result = dt.Rows[0]["Result"].ToString();
                            }
                        }
                    }
                    if (result == "AlreadyExists")
                    {
                        displayMessage("Sorry! Links already exists", "error");
                    }
                    else if (result == "success")
                    {
                        displayMessage("Links successfully updated", "info");
                        FillQuizOptions();
                        hfOptionID.Value = "";
                        txtLinkName.Text = "";
                        txtWebLinks.Text = "";
                        btnSave.Text = "Save";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            displayMessage(ex.Message, "error");
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        displayMessage("", "");
        displayGridMessage("", "");
        hfOptionID.Value = "";
        txtLinkName.Text = "";
        txtWebLinks.Text = "";
        btnSave.Text = "Save";
    }
    protected void FillQuizOptions()
    {
        try
        {
            string[] parameter = { "@CategoryID" };
            string[] value = { hfQuizID.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Intranet_Sub_Category_View", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    gridQuizOption.DataSource = dt;
                    gridQuizOption.DataBind();
                }
            }
        }
        catch (Exception)
        {
        }
    }
    protected void Update_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string SurveyID = (sender as LinkButton).CommandArgument;
            hfOptionID.Value = SurveyID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@SubCategoryID" };
            string[] value = { hfOptionID.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Intranet_Sub_Category_ViewBy_SubCategoryID", 1, parameter, value);
            bool flag = false;

            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        flag = true;
                        txtLinkName.Text = dt.Rows[0]["SubCategoryName"].ToString();
                        txtWebLinks.Text = dt.Rows[0]["SubCategoryLink"].ToString();

                    }
                }
            }
            if (flag)
            {
                displayMessage("", "");
                btnSave.Text = "Update";
            }
        }
        catch (Exception)
        {
        }
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string SurveyID = (sender as LinkButton).CommandArgument;
            hfOptionID.Value = SurveyID;

            string[] parameter = { "@SubCategoryID" };
            string[] value = { hfOptionID.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Intranet_Sub_Category_Delete", 1, parameter, value);
            string result = "";

            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        result = dt.Rows[0]["Result"].ToString();
                    }
                }
            }
            if (result == "success")
            {
                hfOptionID.Value = "";
                FillQuizOptions();
            }
        }
        catch (Exception)
        {
        }
    }    
 
}