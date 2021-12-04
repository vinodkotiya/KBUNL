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
        if (Session["AdminUserID"] == null)
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {
            System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_videos");
            menuli.Attributes["class"] = "active";


            panelAddSafety.Visible = false;
            panelAddProgress.Visible = false;
            panelView.Visible = true;
            Fill();
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
    //protected void displaySeaftyMessage(string msg, string msgtype)
    //{
    //    lblMsgPro.Text = msg;
    //    if (msgtype == "error")
    //        alert.Attributes["class"] = "alert alert-error";
    //    else if (msgtype == "info")
    //        alert.Attributes["class"] = "alert alert-info";
    //    else
    //        alert.Attributes["class"] = "";
    //}
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
    protected void lbtn_AddPro_Click(object sender, EventArgs e)
    {
        panelAddProgress.Visible = true;
        panelView.Visible = false;
        panelAddSafety.Visible = false;
        displayMessage("", "");
        displayGridMessage("", "");
        hfVideoID.Value = "";

        btnSave.Text = "Save";
    }
    protected void lnkAddSafety_Click(object sender, EventArgs e)
    {
        panelAddProgress.Visible = false;
        panelView.Visible = false;
        panelAddSafety.Visible = true;
        displayMessage("", "");
        displayGridMessage("", "");
        hfVideoID.Value = "";

        btnSaveSafety.Text = "Save";
    }
    protected void btnProgressSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {

                if (!FileUploadProgressImg.HasFile && !FileUploadSafetyImg.HasFile)
                {
                    displayMessage("Please upload video cover image", "error");
                }
                else if (!FileUploadProgressLink.HasFile && !FileUploadSafetyLink.HasFile)
                {
                    displayMessage("Please upload video link", "error");
                }

                else
                {
                    bool flagValidLink = true;
                    bool flagHasLink = false;
                    string videoFileName = "";
                    string videoPath = "NA";

                    bool CoverValidImage = true;
                    bool CoverHasImage = false;
                    string CoverImageFileName = "";
                    string CoverImagePath = "NA";


                    flagHasLink = true;
                    string ext = Path.GetExtension(FileUploadProgressLink.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    videoFileName = "progress" + datevalue + ext;
                    videoPath = "Uploads/Videos/" + videoFileName;

                    //if (ext == ".mp4")
                    //    flagValidLink = true;
                    //else
                    //    flagValidLink = false;



                    CoverHasImage = true;
                    string ext1 = Path.GetExtension(FileUploadProgressImg.FileName);
                    CoverImageFileName = "progress_Cover" + datevalue + ext1;
                    CoverImagePath = "Uploads/Videos/Thumbnails/" + CoverImageFileName;

                    if (ext1 == ".jpg" || ext1 == ".JPG" || ext1 == ".png" || ext1 == ".PNG")
                    {
                        CoverValidImage = true;
                    }
                    else
                    {
                        CoverValidImage = false;
                    }


                    if (flagValidLink == false)
                    {
                        displayMessage("Please attach only Valid link ", "error");
                    }
                    else if (CoverValidImage == false)
                    {
                        displayMessage("Please attach only image file for video ", "error");
                    }
                    else
                    {

                        string[] parameter = { "@Flag", "@Title", "@V_CoverImg", "@V_link", "@V_type" };
                        string[] value = { "insert", txtProTitle.Text.Trim(), CoverImagePath, videoPath, "progress" };
                        DB_Status dbs = dba.sp_readSingleData("Sp_VideosMngByAdmin", 5, parameter, value);
                        string result = "";
                        if (dbs.OperationStatus.ToString() == "Success")
                        {
                            result = dbs.SingleResult;
                            if (result == "fail")
                            {
                                displayMessage("Somthing Wrong", "error");
                            }


                            if (result == "inserted")
                            {
                                FileUploadProgressLink.SaveAs(Server.MapPath("~/" + videoPath));
                                FileUploadProgressImg.SaveAs(Server.MapPath("~/" + CoverImagePath));

                                displayMessage("Videos successfully added", "info");
                                Fill();
                                hfVideoID.Value = "";
                                btnSave.Text = "Save";
                                panelAddProgress.Visible = false;
                                panelView.Visible = true;
                            }
                            else if (result == "exist")
                            {
                                displayMessage("Video link already exist", "error");
                            }
                        }
                    }

                }
            }
            else if (btnSave.Text == "Update")
            {

                if (!FileUploadProgressImg.HasFile && !FileUploadSafetyImg.HasFile)
                {
                    displayMessage("Please upload video cover image", "error");
                }
                else if (!FileUploadProgressLink.HasFile && !FileUploadSafetyLink.HasFile)
                {
                    displayMessage("Please upload video link", "error");
                }

                else
                {
                    bool flagValidLink = true;
           
                    string videoFileName = "";
                    string videoPath = "NA";

                    bool CoverValidImage = true;
                 
                    string CoverImageFileName = "";
                    string CoverImagePath = "NA";


                   
                    string ext = Path.GetExtension(FileUploadProgressLink.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    videoFileName = "progress" + datevalue + ext;
                    videoPath = "Uploads/Videos/" + videoFileName;

                    //if (ext == ".mp4")
                    //    flagValidLink = true;
                    //else
                    //    flagValidLink = false;
                    
                    string ext1 = Path.GetExtension(FileUploadProgressImg.FileName);
                    CoverImageFileName = "progress_Cover" + datevalue + ext1;
                    CoverImagePath = "Uploads/Videos/Thumbnails/" + CoverImageFileName;

                    if (ext1 == ".jpg" || ext1 == ".JPG" || ext1 == ".png" || ext1 == ".PNG")
                    {
                        CoverValidImage = true;
                    }
                    else
                    {
                        CoverValidImage = false;
                    }


                    if (flagValidLink == false)
                    {
                        displayMessage("Please attach only Valid link ", "error");
                    }
                    else if (CoverValidImage == false)
                    {
                        displayMessage("Please attach only image file for video ", "error");
                    }
                    else
                    {

                        string[] parameter = { "@Flag", "@Title", "@V_CoverImg", "@V_link", "@RID" };
                        string[] value = { "update", txtProTitle.Text.Trim(), CoverImagePath, videoPath,hfVideoID.Value };
                        DB_Status dbs = dba.sp_readSingleData("Sp_VideosMngByAdmin", 5, parameter, value);
                        string result = "";
                        if (dbs.OperationStatus.ToString() == "Success")
                        {
                            result = dbs.SingleResult;
                            if (result == "fail")
                            {
                                displayMessage("Somthing Wrong", "error");
                            }


                            if (result == "updated")
                            {
                                FileUploadProgressLink.SaveAs(Server.MapPath("~/" + videoPath));
                                FileUploadProgressImg.SaveAs(Server.MapPath("~/" + CoverImagePath));

                                displayMessage("Videos successfully added", "info");
                                Fill();
                                hfVideoID.Value = "";
                                btnSave.Text = "Save";
                                panelAddProgress.Visible = false;
                                panelView.Visible = true;
                            }
                            else if (result == "exist")
                            {
                                displayMessage("Video link already exist", "error");
                            }
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            displayMessage(ex.Message, "error");
        }
    }
    protected void btnClosePro_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hfVideoID.Value = "";
        btnSave.Text = "Save";
        panelAddProgress.Visible = false;
        panelView.Visible = true;
    }
    protected void btnClearSafety_Click(object sender, EventArgs e)
    {
        displayGridMessage("", "");
        hfVideoID.Value = "";
        btnSaveSafety.Text = "Save";
        panelAddSafety.Visible = false;
        panelView.Visible = true;
    }
    protected void Fill()
    {
        try
        {
            string[] parameter = { "@Flag" };
            string[] value = { "view" };
            DB_Status dbs = dba.sp_populateDataSet("Sp_VideosMngByAdmin", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    gridMagazine.DataSource = dt;
                    gridMagazine.DataBind();
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
            string id = (sender as LinkButton).CommandArgument;
            hfVideoID.Value = id;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            var item = (GridViewRow)lnkbtn_edit.NamingContainer;
            Label txt = (Label)item.FindControl("lblType");
            string v_type = txt.Text;

            string[] parameter = { "@Flag", "@RID" };
            string[] value = { "viewById", hfVideoID.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_VideosMngByAdmin", 2, parameter, value);

            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {


                        v_type = dt.Rows[0]["VideoType"].ToString();
                        if (v_type == "progress")
                        {
                            hfCoverImage_UploadedPath.Value = dt.Rows[0]["VideoCoverImg"].ToString();
                            hfLink_UploadedPath.Value = dt.Rows[0]["VideoLink"].ToString();
                            txtProTitle.Text = dt.Rows[0]["Title"].ToString();
                            panelAddProgress.Visible = true;
                            panelView.Visible = false;
                            btnSave.Text = "Update";
                        }
                        else if (v_type == "safety")
                        {
                            hfCoverImage_UploadedPath.Value = dt.Rows[0]["VideoCoverImg"].ToString();
                            hfLink_UploadedPath.Value = dt.Rows[0]["VideoLink"].ToString();
                            txtSafetyTitle.Text = dt.Rows[0]["Title"].ToString();
                            panelAddSafety.Visible = true;
                            panelView.Visible = false;
                            btnSaveSafety.Text = "Update";
                        }
                    }
                }
            }
            else
            {
                panelView.Visible = true;
                displayMessage("Error", "error");
                btnSave.Text = "Save";
                btnSaveSafety.Text = "Save";
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
            string id = (sender as LinkButton).CommandArgument;
            hfVideoID.Value = id;

            string[] parameter = { "@Flag", "@RID" };
            string[] value = { "delete", hfVideoID.Value };
            DB_Status dbs = dba.sp_readSingleData("Sp_VideosMngByAdmin", 2, parameter, value);
            string result = "";

            if (dbs.OperationStatus.ToString() == "Success")
            {
                result = dbs.SingleResult;
                if (result == "deleted")
                {
                    hfVideoID.Value = "";
                    Fill();
                }
                else if (result == "fail")
                {
                    displayMessage("Somthing wrong. Please Contact developer", "error");
                }
            }
            else
            {
                displayMessage("Error", "error");
            }
        }
        catch (Exception)
        {
        }
    }

    protected void btnSaveSafety_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSaveSafety.Text == "Save")
            {

                if (!FileUploadSafetyImg.HasFile)
                {
                    displayMessage("Please upload video cover image", "error");
                }
                else if (!FileUploadSafetyLink.HasFile)
                {
                    displayMessage("Please upload video link", "error");
                }

                else
                {
                    bool flagValidLink = true;
                    bool flagHasLink = false;
                    string videoFileName = "";
                    string videoPath = "NA";

                    bool CoverValidImage = true;
                    bool CoverHasImage = false;
                    string CoverImageFileName = "";
                    string CoverImagePath = "NA";


                    flagHasLink = true;
                    string ext = Path.GetExtension(FileUploadSafetyLink.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    videoFileName = "safety" + datevalue + ext;
                    videoPath = "Uploads/Videos/" + videoFileName;

                    //if (ext == ".mp4")
                    //flagValidLink = true;
                    // else
                    //flagValidLink = false;
                    CoverHasImage = true;
                    string ext1 = Path.GetExtension(FileUploadSafetyImg.FileName);
                    CoverImageFileName = "safety_Cover" + datevalue + ext1;
                    CoverImagePath = "Uploads/Videos/Thumbnails/" + CoverImageFileName;

                    if (ext1 == ".jpg" || ext1 == ".JPG" || ext1 == ".png" || ext1 == ".PNG")
                    {
                        CoverValidImage = true;
                    }
                    else
                    {
                        CoverValidImage = false;
                    }


                    if (flagValidLink == false)
                    {
                        displayMessage("Please attach only Valid link ", "error");
                    }
                    else if (CoverValidImage == false)
                    {
                        displayMessage("Please attach only image file for video ", "error");
                    }
                    else
                    {

                        string[] parameter = { "@Flag", "@Title", "@V_CoverImg", "@V_link", "@V_type" };
                        string[] value = { "insert", txtSafetyTitle.Text.Trim(), CoverImagePath, videoPath, "Safety" };
                        DB_Status dbs = dba.sp_readSingleData("Sp_VideosMngByAdmin", 5, parameter, value);
                        string result = "";
                        if (dbs.OperationStatus.ToString() == "Success")
                        {
                            result = dbs.SingleResult;
                            if (result == "fail")
                            {
                                displayMessage("Somthing Wrong", "error");
                            }


                            if (result == "inserted")
                            {

                                FileUploadSafetyLink.SaveAs(Server.MapPath("~/" + videoPath));
                                FileUploadSafetyImg.SaveAs(Server.MapPath("~/" + CoverImagePath));

                                displayMessage("Videos successfully added", "info");
                                Fill();
                                hfVideoID.Value = "";
                                txtSafetyTitle.Text = "";
                                btnSaveSafety.Text = "Save";
                                panelAddSafety.Visible = false;
                                panelView.Visible = true;
                            }
                            else if (result == "exist")
                            {
                                displayMessage("Video link already exist", "error");
                            }
                        }
                    }

                }
            }
            else if (btnSaveSafety.Text == "Update")
            {
                if (!FileUploadSafetyImg.HasFile)
                {
                    displayMessage("Please upload video cover image", "error");
                }
                else if (!FileUploadSafetyLink.HasFile)
                {
                    displayMessage("Please upload video link", "error");
                }
                else
                {

                    bool flagHasLink = false;
                    string videoFileName = "";
                    string videoPath = "NA";


                    bool CoverValidImage = true;
                    bool CoverHasImage = false;
                    string CoverImageFileName = "";
                    string CoverImagePath = "NA";

                    string ext = Path.GetExtension(FileUploadProgressLink.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    videoFileName = "Safety" + datevalue + ext;
                    videoPath = "Uploads/Videos/" + videoFileName;

                    //if (ext == ".jpg" || ext == ".JPG" || ext == ".png" || ext == ".PNG")
                    //    flagValidImage = true;
                    //else
                    //    flagValidImage = false;


                    string ext1 = Path.GetExtension(FileUploadProgressImg.FileName);
                    CoverImageFileName = "safety_Cover" + datevalue + ext;
                    CoverImagePath = "Uploads/Videos/Thumbnails/" + CoverImageFileName;

                    if (ext1 == ".jpg" || ext1 == ".JPG" || ext1 == ".png" || ext1 == ".PNG")
                        CoverValidImage = true;
                    else
                        CoverValidImage = false;



                    if (flagHasLink == false)
                    {
                        displayMessage("Please attach only valid File", "error");
                    }
                    else if (CoverHasImage && CoverValidImage == false)
                    {
                        displayMessage("Please attach only image file for cover image ", "error");
                    }
                    else
                    {
                        string[] parameter = { "@Flag", "@Title", "@V_CoverImg", "@V_link", "@RID" };
                        string[] value = { "update", txtSafetyTitle.Text.Trim(), CoverImagePath, videoPath, hfVideoID.Value };
                        DB_Status dbs = dba.sp_readSingleData("Sp_VideosMngByAdmin", 5, parameter, value);
                        string result = "";
                        if (dbs.OperationStatus.ToString() == "Success")
                        {
                            result = dbs.SingleResult;
                            if (result == "fail")
                            {
                                displayMessage("Somthing Wrong", "erro");
                            }


                            if (result == "updated")
                            {

                                FileUploadProgressLink.SaveAs(Server.MapPath("~/" + videoPath));

                                FileUploadProgressImg.SaveAs(Server.MapPath("~/" + CoverImagePath));

                                displayMessage("Video successfully Updated", "info");
                                Fill();
                                hfVideoID.Value = "";
                                txtSafetyTitle.Text = "";
                                btnSaveSafety.Text = "Save";
                                panelAddSafety.Visible = false;
                                panelView.Visible = true;
                            }
                            else if (result == "exist")
                            {
                                displayMessage("Video link already exist", "error");
                            }
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            displayMessage(ex.Message, "error");
        }
    }




}