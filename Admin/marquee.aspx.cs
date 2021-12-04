using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_marquee : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] != null)
        {
            hdfDept_Id.Value = "0";
            div_headTitle.InnerText = "Admin > Marquee";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Convert.ToString(Session["deprt_name"]) + " > Marquee";
            hdfDept_Id.Value = Session["DeptID"].ToString();
        }
        else
        {
            Response.Redirect("../login.aspx");
        }

        if (!IsPostBack)
        {
            //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_banner");
            //menuli.Attributes["class"] = "active";

            panelAddNew.Visible = false;
            panelView.Visible = true;

            FillMarquee();
            displayGridMessage("", "");
        }
    }
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
        hdfMarqueeId.Value = "";
        txtMarqueeE.Text = "";
        txtMarqueeH.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtMarqueeE.Text.Trim() == "")
                    displayMessage("Please enter marquee", "error");
                else if (txtMarqueeH.Text.Trim() == "")
                    displayMessage("Please enter marquee", "error");
                else
                {
                        string[] parameter = { "@Flag","@DeptID", "@MarqueeEnglish", "@MarqueeHindi"};
                        string[] value = { "Add",hdfDept_Id.Value, txtMarqueeE.Text.Trim(), txtMarqueeH.Text.Trim()};
                        DB_Status dbs = dba.sp_populateDataSet("SP_Marquee",4, parameter, value);
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

                        if (result == "Inserted")
                        {
                            displayMessage("Marquee successfully added", "info");
                            FillMarquee();
                            hdfMarqueeId.Value = "0";
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;
                        }
                        else if (result == "AlreadyExists")
                        {
                            displayMessage("Record already exists", "error");
                        }
                    }
            }
            else if (btnSave.Text == "Update")
            {
                if (txtMarqueeE.Text.Trim() == "")
                    displayMessage("Please enter marquee", "error");
                else if (txtMarqueeH.Text.Trim() == "")
                    displayMessage("Please enter marquee", "error");
                else
                {
                        string[] parameter = { "@Flag", "@MarqueeId", "@MarqueeEnglish", "@MarqueeHindi" };
                        string[] value = { "Update", hdfMarqueeId.Value, txtMarqueeE.Text.Trim(), txtMarqueeH.Text.Trim()};
                        DB_Status dbs = dba.sp_populateDataSet("SP_Marquee", 4, parameter, value);
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
                        if (result == "Updated")
                        {
                            displayMessage("Marquee successfully updated", "info");
                            FillMarquee();
                            hdfMarqueeId.Value = "0";
                            btnSave.Text = "Save";
                            panelAddNew.Visible = false;
                            panelView.Visible = true;
                        }
                        else if (result == "AlreadyExists")
                        {
                            displayMessage("Record already exists", "error");
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
        displayGridMessage("", "");
        hdfMarqueeId.Value = "0";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void FillMarquee()
    {
        try
        {
            string[] parameter = { "@Flag", "@DeptID" };
            string[] value = { "LoadForAdmin", hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Marquee", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    gridMarquee.DataSource = dt;
                    gridMarquee.DataBind();
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
            string marqueeId = (sender as LinkButton).CommandArgument;
            hdfMarqueeId.Value = marqueeId;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@MarqueeId" };
            string[] value = { "LoadbyID", hdfMarqueeId.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Marquee", 2, parameter, value);
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
                        txtMarqueeE.Text =Convert.ToString(dt.Rows[0]["MarqueeEnglish"]);
                        txtMarqueeH.Text = Convert.ToString(dt.Rows[0]["MarqueeHindi"]);
                    }
                }
            }
            if (flag)
            {
                panelAddNew.Visible = true;
                panelView.Visible = false;

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
            string MarqueeId = (sender as LinkButton).CommandArgument;
            hdfMarqueeId.Value = MarqueeId;

            string[] parameter = { "@Flag", "@MarqueeId" };
            string[] value = { "Delete", hdfMarqueeId.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Marquee", 2, parameter, value);
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
            if (result == "Deleted")
            {
                hdfMarqueeId.Value = "0";
                FillMarquee();
            }
        }
        catch (Exception)
        {
        }
    }
    protected void cbSwitch_CheckedChanged(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        CheckBox ckkIsactive = (CheckBox)sender;
        string marqueeId =((HiddenField)ckkIsactive.Parent.FindControl("hdfMarqueeIdGrd")).Value;
        string Flag = "";

        if (ckkIsactive.Checked)
        {
            Flag = "Activate";
        }
        else
        {
            Flag = "Deactivate";
        }

        string[] parameter = { "@Flag", "@MarqueeId" };
        string[] value = { Flag, marqueeId.ToString() };
        DB_Status dbs = dba.sp_populateDataSet("SP_Marquee", 2, parameter, value);
        if (dbs.OperationStatus.ToString() == "Success")
        {
            DataSet ds = dbs.ResultDataSet;
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string status = dt.Rows[0]["Result"].ToString();
                    if (status == "Activated")
                        displayGridMessage("Marquee successfully activated", "info");
                    else if (status == "Deactivated")
                        displayGridMessage("Marquee successfully deactivated", "info");
                }
            }
        }
    }
}