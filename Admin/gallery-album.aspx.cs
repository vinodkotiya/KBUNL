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
        if (Session["AdminUserID"] != null)
        {
            hfUserID.Value = "0"; // 0 for Admin
            hdfDept_Id.Value = hfUserID.Value;
           div_headTitle.InnerText= "Admin > Gallery Album";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            hfUserID.Value =Convert.ToString(Session["DeptID"]);
            hdfDept_Id.Value = hfUserID.Value;
            div_headTitle.InnerText ="Department : "+ Session["deprt_name"].ToString()+" > Gallery";
        }
        else
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {
            //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_gallery");
            //menuli.Attributes["class"] = "active";

            panelAddNew.Visible = false;
            panelView.Visible = true;
            //Fill Articles

            FillAlbums();
            displayGridMessage("", "");
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
        hfAlbumID.Value = "";

        txtTitle.Text = "";
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                if (txtTitle.Text.Trim() == "")
                    displayMessage("Please enter Title", "error");
                else
                {
                    string[] parameter = { "@AlubumName" , "@Deprt_ID" };
                    string[] value = { txtTitle.Text.Trim(), hdfDept_Id.Value };
                    DB_Status dbs = dba.sp_populateDataSet("Sp_Gallery_Master_Insert", 2, parameter, value);
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
                        displayMessage("Sorry! Album already exists", "error");
                    }
                    else if (result == "success")
                    {
                        displayMessage("Album successfully added", "info");
                        FillAlbums();
                        hfAlbumID.Value = "";
                        btnSave.Text = "Save";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
                    }
                }
            }
            else if (btnSave.Text == "Update")
            {
                if (txtTitle.Text.Trim() == "")
                    displayMessage("Please enter Title", "error");
                else
                {
                    string[] parameter = { "@AlbumID", "@AlubumName", "@Deprt_ID" };
                    string[] value = { hfAlbumID.Value, txtTitle.Text.Trim(), hdfDept_Id.Value };
                    DB_Status dbs = dba.sp_populateDataSet("Sp_Gallery_Master_Update", 3, parameter, value);
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
                        displayMessage("Sorry! Category already exists", "error");
                    }
                    else if (result == "success")
                    {
                        displayMessage("Album successfully updated", "info");
                        FillAlbums();
                        hfAlbumID.Value = "";
                        btnSave.Text = "Save";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
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
        hfAlbumID.Value = "";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void FillAlbums()
    {
        try
        {
            string[] parameter = { "@Deprt_ID" };
            string[] value = { hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Gallery_Master_View", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    gridAlbum.DataSource = dt;
                    gridAlbum.DataBind();
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
            string ID = (sender as LinkButton).CommandArgument;
            hfAlbumID.Value = ID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@AlbumID","@Deprt_ID" };
            string[] value = { hfAlbumID.Value,hdfDept_Id.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Gallery_Master_ViewByAlbumID", 2, parameter, value);
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
                        txtTitle.Text = dt.Rows[0]["AlubumName"].ToString();
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
            string ID = (sender as LinkButton).CommandArgument;
            hfAlbumID.Value = ID;

            string[] parameter = { "@AlbumID", "@Deprt_ID" };
            string[] value = { hfAlbumID.Value,hdfDept_Id.Value};
            DB_Status dbs = dba.sp_populateDataSet("Sp_Gallery_Master_Delete", 2, parameter, value);
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
                hfAlbumID.Value = "";
                FillAlbums();
            }
        }
        catch (Exception)
        {
        }
    }
    protected void ViewList_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string QuestionID = (sender as LinkButton).CommandArgument;
            hfAlbumID.Value = QuestionID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;
            Label grid_lblTitle = (Label)gridAlbum.Rows[rowindex].FindControl("lblAlbumTitle");

            Session["Selected_AlbumID"] = hfAlbumID.Value;
            Session["Selected_AlbumTitle"] = grid_lblTitle.Text;
            Response.Redirect("gallery-album-photos.aspx");
        }
        catch (Exception)
        {
        }
    }
 
}