using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Web.UI.WebControls;

public partial class Admin_news_post : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] != null)
        {
            check();
            div_headTitle.InnerHtml = " Admin ><a href='gallery-album.aspx'>Gallery Album</a> > Gallery Album Photo";
            div_headTitle.InnerHtml = " Admin ><a href='gallery-album.aspx'>Gallery Album</a> > Gallery Album Photo";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            check();
            div_headTitle.InnerHtml = "Department : " + Session["deprt_name"].ToString() + " > <a href='gallery-album.aspx'><b>Gallery-Photo</b></a> : " + Session["Selected_AlbumTitle"].ToString();
        }
        else
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {
            //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_gallery");
            //menuli.Attributes["class"] = "active";

            //panelAddNew.Visible = false;
            panelView.Visible = true;
            //Fill Articles

            FillPhotos();
            displayGridMessage("", "");

            hfPhotoID.Value = "";

            btnSave.Text = "Save";
        }
    }
    protected void check()
    {
        if (Session["Selected_AlbumID"] == null)
        {
            Response.Redirect("gallery-album.aspx");
        }
        else
        {
            hfAlbumID.Value = Session["Selected_AlbumID"].ToString();
            spanAlbumTitle.InnerText = Session["Selected_AlbumTitle"].ToString();
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
        hfPhotoID.Value = "";


        //ddlIsRightOption.SelectedIndex = -1;
        btnSave.Text = "Save";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            bool flagValidImage1 = true;
            bool flagHasImage1 = false;
            string ImageFileName1 = "";
            string ImagePath1 = "NA";
            string ThumbnailImagePath1 = "NA";
            if (FileUploadImage1.HasFile)
            {
                //---------------------Image 1------------------------------------
                flagHasImage1 = true;
                string ext = Path.GetExtension(FileUploadImage1.FileName);
                string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                ImageFileName1 = "Gallery1" + datevalue + ext;
                ImagePath1 = "Uploads/Gallery/" + ImageFileName1;
                ThumbnailImagePath1 = "Uploads/Gallery/Thumbnails/" + ImageFileName1;
                if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                    flagValidImage1 = true;
                else
                    flagValidImage1 = false;
                //Validate Image
                if (flagHasImage1 && flagValidImage1 == false)
                    displayMessage("Please attach only Image file for Album Image", "error");
                string[] parameter = { "@AlbumID", "@PhotoPath", "@PhotoThumbnailPath" };
                string[] value = { hfAlbumID.Value, ImagePath1, ThumbnailImagePath1 };
                DB_Status dbs = dba.sp_populateDataSet("Sp_Gallery_Photo_Insert", 3, parameter, value);
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
                    //---------------Image 1 Save In Folder
                    if (flagHasImage1 && flagValidImage1)
                    {
                        Stream strm1 = FileUploadImage1.PostedFile.InputStream;
                        ImageResize(ImageFileName1, strm1);
                        Stream strm2 = FileUploadImage1.PostedFile.InputStream;
                        GenerateThumbnail(ImageFileName1, strm2);
                        Thread.Sleep(1000);
                        displayMessage("Image successfully added", "info");
                    }
                }
            }
            //---------------------Image 2------------------------------------
            bool flagValidImage2 = true;
            bool flagHasImage2 = false;
            string ImageFileName2 = "";
            string ImagePath2 = "NA";
            string ThumbnailImagePath2 = "NA";
            if (FileUploadImage2.HasFile)
            {
                flagHasImage2 = true;
                string ext = Path.GetExtension(FileUploadImage2.FileName);
                string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                ImageFileName2 = "Gallery2" + datevalue + ext;
                ImagePath2 = "Uploads/Gallery/" + ImageFileName2;
                ThumbnailImagePath2 = "Uploads/Gallery/Thumbnails/" + ImageFileName2;

                if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                    flagValidImage2 = true;
                else
                    flagValidImage2 = false;
                //Validate Image
                if (flagHasImage2 && flagValidImage2 == false)
                    displayMessage("Please attach only Image file for Album Image", "error");
                string[] parameter = { "@AlbumID", "@PhotoPath", "@PhotoThumbnailPath" };
                string[] value = { hfAlbumID.Value, ImagePath2, ThumbnailImagePath2 };
                DB_Status dbs = dba.sp_populateDataSet("Sp_Gallery_Photo_Insert", 3, parameter, value);
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
                    //---------------Image 1 Save In Folder
                    if (flagHasImage2 && flagValidImage2)
                    {
                        Stream strm1 = FileUploadImage2.PostedFile.InputStream;
                        ImageResize(ImageFileName2, strm1);
                        Stream strm2 = FileUploadImage2.PostedFile.InputStream;
                        GenerateThumbnail(ImageFileName2, strm2);
                        displayMessage("Image successfully added", "info");
                        Thread.Sleep(1000);
                    }
                }
            }
            //---------------------Image 3------------------------------------
            bool flagValidImage3 = true;
            bool flagHasImage3 = false;
            string ImageFileName3 = "";
            string ImagePath3 = "NA";
            string ThumbnailImagePath3 = "NA";
            if (FileUploadImage3.HasFile)
            {
                flagHasImage3 = true;
                string ext = Path.GetExtension(FileUploadImage3.FileName);
                string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                ImageFileName3 = "Gallery2" + datevalue + ext;
                ImagePath3 = "Uploads/Gallery/" + ImageFileName3;
                ThumbnailImagePath3 = "Uploads/Gallery/Thumbnails/" + ImageFileName3;

                if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                    flagValidImage3 = true;
                else
                    flagValidImage3 = false;
                //Validate Image
                if (flagHasImage3 && flagValidImage3 == false)
                    displayMessage("Please attach only Image file for Album Image", "error");
                string[] parameter = { "@AlbumID", "@PhotoPath", "@PhotoThumbnailPath" };
                string[] value = { hfAlbumID.Value, ImagePath3, ThumbnailImagePath3 };
                DB_Status dbs = dba.sp_populateDataSet("Sp_Gallery_Photo_Insert", 3, parameter, value);
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
                    //---------------Image 1 Save In Folder
                    if (flagHasImage3 && flagValidImage3)
                    {
                        Stream strm3 = FileUploadImage3.PostedFile.InputStream;
                        ImageResize(ImageFileName3, strm3);
                        GenerateThumbnail(ImageFileName3, strm3);
                        displayMessage("Image successfully added", "info");
                        Thread.Sleep(1000);
                    }
                }
            }
            //---------------------Image 4------------------------------------
            bool flagValidImage4 = true;
            bool flagHasImage4 = false;
            string ImageFileName4 = "";
            string ImagePath4 = "NA";
            string ThumbnailImagePath4 = "NA";
            if (FileUploadImage4.HasFile)
            {
                flagHasImage4 = true;
                string ext = Path.GetExtension(FileUploadImage4.FileName);
                string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                ImageFileName4 = "Gallery" + datevalue + ext;
                ImagePath4 = "Uploads/Gallery/" + ImageFileName4;
                ThumbnailImagePath4 = "Uploads/Gallery/Thumbnails/" + ImageFileName4;

                if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                    flagValidImage4 = true;
                else
                    flagValidImage4 = false;
                //Validate Image
                if (flagHasImage4 && flagValidImage4 == false)
                    displayMessage("Please attach only Image file for Album Image", "error");
                string[] parameter = { "@AlbumID", "@PhotoPath", "@PhotoThumbnailPath" };
                string[] value = { hfAlbumID.Value, ImagePath4, ThumbnailImagePath4 };
                DB_Status dbs = dba.sp_populateDataSet("Sp_Gallery_Photo_Insert", 3, parameter, value);
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
                    //---------------Image 1 Save In Folder
                    if (flagHasImage4 && flagValidImage4)
                    {
                        Stream strm4 = FileUploadImage4.PostedFile.InputStream;
                        ImageResize(ImageFileName4, strm4);
                        GenerateThumbnail(ImageFileName4, strm4);
                        displayMessage("Image successfully added", "info");
                        Thread.Sleep(1000);
                    }
                }
            }
            //---------------------Image 5------------------------------------
            bool flagValidImage5 = true;
            bool flagHasImage5 = false;
            string ImageFileName5 = "";
            string ImagePath5 = "NA";
            string ThumbnailImagePath5 = "NA";
            if (FileUploadImage5.HasFile)
            {
                flagHasImage5 = true;
                string ext = Path.GetExtension(FileUploadImage5.FileName);
                string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                ImageFileName5 = "Gallery" + datevalue + ext;
                ImagePath5 = "Uploads/Gallery/" + ImageFileName5;
                ThumbnailImagePath5 = "Uploads/Gallery/Thumbnails/" + ImageFileName5;

                if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                    flagValidImage5 = true;
                else
                    flagValidImage5 = false;
                //Validate Image
                if (flagHasImage5 && flagValidImage5 == false)
                    displayMessage("Please attach only Image file for Album Image", "error");
                string[] parameter = { "@AlbumID", "@PhotoPath", "@PhotoThumbnailPath" };
                string[] value = { hfAlbumID.Value, ImagePath5, ThumbnailImagePath5 };
                DB_Status dbs = dba.sp_populateDataSet("Sp_Gallery_Photo_Insert", 3, parameter, value);
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
                    //---------------Image 1 Save In Folder
                    if (flagHasImage5 && flagValidImage5)
                    {
                        Stream strm5 = FileUploadImage5.PostedFile.InputStream;
                        ImageResize(ImageFileName5, strm5);
                        GenerateThumbnail(ImageFileName5, strm5);
                        displayMessage("Image successfully added", "info");
                        Thread.Sleep(1000);
                    }
                }
            }


            //---------------------Image 6------------------------------------
            bool flagValidImage6 = true;
            bool flagHasImage6 = false;
            string ImageFileName6 = "";
            string ImagePath6 = "NA";
            string ThumbnailImagePath6 = "NA";
            if (FileUploadImage6.HasFile)
            {
                flagHasImage6 = true;
                string ext = Path.GetExtension(FileUploadImage6.FileName);
                string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                ImageFileName6 = "Gallery" + datevalue + ext;
                ImagePath6 = "Uploads/Gallery/" + ImageFileName6;
                ThumbnailImagePath6 = "Uploads/Gallery/Thumbnails/" + ImageFileName6;

                if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                    flagValidImage6 = true;
                else
                    flagValidImage6 = false;
                //Validate Image
                if (flagHasImage6 && flagValidImage6 == false)
                    displayMessage("Please attach only Image file for Album Image", "error");
                string[] parameter = { "@AlbumID", "@PhotoPath", "@PhotoThumbnailPath" };
                string[] value = { hfAlbumID.Value, ImagePath6, ThumbnailImagePath6 };
                DB_Status dbs = dba.sp_populateDataSet("Sp_Gallery_Photo_Insert", 3, parameter, value);
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
                    //---------------Image 1 Save In Folder
                    if (flagHasImage6 && flagValidImage6)
                    {
                        Stream strm6 = FileUploadImage6.PostedFile.InputStream;
                        ImageResize(ImageFileName6, strm6);
                        GenerateThumbnail(ImageFileName6, strm6);
                        displayMessage("Image successfully added", "info");
                        Thread.Sleep(1000);
                    }
                }
            }

            //---------------------Image 7------------------------------------
            bool flagValidImage7 = true;
            bool flagHasImage7 = false;
            string ImageFileName7 = "";
            string ImagePath7 = "NA";
            string ThumbnailImagePath7 = "NA";
            if (FileUploadImage7.HasFile)
            {
                flagHasImage7 = true;
                string ext = Path.GetExtension(FileUploadImage7.FileName);
                string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                ImageFileName7 = "Gallery" + datevalue + ext;
                ImagePath7 = "Uploads/Gallery/" + ImageFileName7;
                ThumbnailImagePath7 = "Uploads/Gallery/Thumbnails/" + ImageFileName7;

                if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                    flagValidImage7 = true;
                else
                    flagValidImage7 = false;
                //Validate Image
                if (flagHasImage7 && flagValidImage7 == false)
                    displayMessage("Please attach only Image file for Album Image", "error");
                string[] parameter = { "@AlbumID", "@PhotoPath", "@PhotoThumbnailPath" };
                string[] value = { hfAlbumID.Value, ImagePath7, ThumbnailImagePath7 };
                DB_Status dbs = dba.sp_populateDataSet("Sp_Gallery_Photo_Insert", 3, parameter, value);
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
                    //---------------Image 1 Save In Folder
                    if (flagHasImage7 && flagValidImage7)
                    {
                        Stream strm7 = FileUploadImage7.PostedFile.InputStream;
                        ImageResize(ImageFileName7, strm7);
                        GenerateThumbnail(ImageFileName7, strm7);
                        displayMessage("Image successfully added", "info");
                        Thread.Sleep(1000);
                    }
                }
            }

            //---------------------Image 8------------------------------------
            bool flagValidImage8 = true;
            bool flagHasImage8 = false;
            string ImageFileName8 = "";
            string ImagePath8 = "NA";
            string ThumbnailImagePath8 = "NA";
            if (FileUploadImage8.HasFile)
            {
                flagHasImage8 = true;
                string ext = Path.GetExtension(FileUploadImage8.FileName);
                string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                ImageFileName8 = "Gallery" + datevalue + ext;
                ImagePath8 = "Uploads/Gallery/" + ImageFileName8;
                ThumbnailImagePath8 = "Uploads/Gallery/Thumbnails/" + ImageFileName8;

                if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                    flagValidImage8 = true;
                else
                    flagValidImage8 = false;
                //Validate Image
                if (flagHasImage8 && flagValidImage8 == false)
                    displayMessage("Please attach only Image file for Album Image", "error");
                string[] parameter = { "@AlbumID", "@PhotoPath", "@PhotoThumbnailPath" };
                string[] value = { hfAlbumID.Value, ImagePath8, ThumbnailImagePath8 };
                DB_Status dbs = dba.sp_populateDataSet("Sp_Gallery_Photo_Insert", 3, parameter, value);
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
                    //---------------Image 1 Save In Folder
                    if (flagHasImage8 && flagValidImage8)
                    {
                        Stream strm8 = FileUploadImage8.PostedFile.InputStream;
                        ImageResize(ImageFileName8, strm8);
                        GenerateThumbnail(ImageFileName8, strm8);
                        displayMessage("Image successfully added", "info");
                        Thread.Sleep(1000);
                    }
                }
            }


            //---------------------Image 9------------------------------------
            bool flagValidImage9 = true;
            bool flagHasImage9 = false;
            string ImageFileName9 = "";
            string ImagePath9 = "NA";
            string ThumbnailImagePath9 = "NA";
            if (FileUploadImage9.HasFile)
            {
                flagHasImage9 = true;
                string ext = Path.GetExtension(FileUploadImage9.FileName);
                string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                ImageFileName9 = "Gallery" + datevalue + ext;
                ImagePath9 = "Uploads/Gallery/" + ImageFileName9;
                ThumbnailImagePath9 = "Uploads/Gallery/Thumbnails/" + ImageFileName9;

                if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                    flagValidImage9 = true;
                else
                    flagValidImage9 = false;
                //Validate Image
                if (flagHasImage9 && flagValidImage9 == false)
                    displayMessage("Please attach only Image file for Album Image", "error");
                string[] parameter = { "@AlbumID", "@PhotoPath", "@PhotoThumbnailPath" };
                string[] value = { hfAlbumID.Value, ImagePath9, ThumbnailImagePath9 };
                DB_Status dbs = dba.sp_populateDataSet("Sp_Gallery_Photo_Insert", 3, parameter, value);
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
                    //---------------Image 1 Save In Folder
                    if (flagHasImage9 && flagValidImage9)
                    {
                        Stream strm9 = FileUploadImage9.PostedFile.InputStream;
                        ImageResize(ImageFileName9, strm9);
                        GenerateThumbnail(ImageFileName9, strm9);
                        displayMessage("Image successfully added", "info");
                        Thread.Sleep(1000);
                    }
                }
            }

            //---------------------Image 10------------------------------------
            bool flagValidImage10 = true;
            bool flagHasImage10 = false;
            string ImageFileName10 = "";
            string ImagePath10 = "NA";
            string ThumbnailImagePath10 = "NA";
            if (FileUploadImage10.HasFile)
            {
                flagHasImage10 = true;
                string ext = Path.GetExtension(FileUploadImage10.FileName);
                string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                ImageFileName10 = "Gallery" + datevalue + ext;
                ImagePath10 = "Uploads/Gallery/" + ImageFileName10;
                ThumbnailImagePath10 = "Uploads/Gallery/Thumbnails/" + ImageFileName10;

                if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                    flagValidImage10 = true;
                else
                    flagValidImage10 = false;
                //Validate Image
                if (flagHasImage10 && flagValidImage10 == false)
                    displayMessage("Please attach only Image file for Album Image", "error");
                string[] parameter = { "@AlbumID", "@PhotoPath", "@PhotoThumbnailPath" };
                string[] value = { hfAlbumID.Value, ImagePath10, ThumbnailImagePath10 };
                DB_Status dbs = dba.sp_populateDataSet("Sp_Gallery_Photo_Insert", 3, parameter, value);
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
                    //---------------Image 1 Save In Folder
                    if (flagHasImage10 && flagValidImage10)
                    {
                        Stream strm10 = FileUploadImage10.PostedFile.InputStream;
                        ImageResize(ImageFileName10, strm10);
                        GenerateThumbnail(ImageFileName10, strm10);
                        displayMessage("Image successfully added", "info");
                        Thread.Sleep(1000);
                    }
                }
            }


            FillPhotos();
            hfPhotoID.Value = "0";
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            displayMessage(ex.Message, "error");
        }
    }

    public void ImageResize(string ImageFileName, Stream strm1)
    {
        string targetPath = Server.MapPath("~/Uploads/Gallery/" + ImageFileName);
        var targetFile = targetPath;

        using (var image = System.Drawing.Image.FromStream(strm1))
        {
            int newWidth, newHeight;
            double scalefactor = (double)1000 / image.Width;
            if (scalefactor < 1)
            {
                string d = scalefactor.ToString("F");
                newWidth = (int)(image.Width * Convert.ToDecimal(d));
                newHeight = (int)(image.Height * Convert.ToDecimal(d));
            }
            else
            {
                newWidth = (int)(image.Width);
                newHeight = (int)(image.Height);
            }
            var thumbnailImg = new Bitmap(newWidth, newHeight);
            var thumbGraph = Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbnailImg.Save(targetPath, image.RawFormat);
        }
    }
    public void GenerateThumbnail(string ImageFileName, Stream strm1)
    {
        string ThumbImagetargetPath = Server.MapPath("~/Uploads/Gallery/Thumbnails/" + ImageFileName);
        var targetThumbFile = ThumbImagetargetPath;
        using (var image = System.Drawing.Image.FromStream(strm1))
        {
            int newTWidth1, newTHeight1;
            double scalefactor = (double)200 / image.Width;
            if (scalefactor < 1)
            {
                string d = scalefactor.ToString("F");
                decimal scale = Convert.ToDecimal(d);
                newTWidth1 = (int)(image.Width * scale);
                newTHeight1 = (int)(image.Height * scale);
            }
            else
            {
                newTWidth1 = (int)(image.Width);
                newTHeight1 = (int)(image.Height);
            }
            Bitmap thumbnailImg1 = new Bitmap(newTWidth1, newTHeight1);
            var thumbGraph1 = Graphics.FromImage(thumbnailImg1);
            thumbGraph1.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraph1.SmoothingMode = SmoothingMode.HighQuality;
            thumbGraph1.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imageThumbRectangle1 = new Rectangle(0, 0, newTWidth1, newTHeight1);
            thumbGraph1.DrawImage(image, imageThumbRectangle1);
            thumbnailImg1.Save(ThumbImagetargetPath, image.RawFormat);
            thumbGraph1.Dispose();
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        displayMessage("", "");
        displayGridMessage("", "");
        hfPhotoID.Value = "";

        btnSave.Text = "Save";
        Response.Redirect("gallery-album.aspx", false);
    }
    protected void FillPhotos()
    {
        try
        {
            string[] parameter = { "@AlbumID" };
            string[] value = { hfAlbumID.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Gallery_Photo_View", 1, parameter, value);
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
        catch (Exception ex)
        {

        }
    }

    protected void Delete_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string SurveyID = (sender as LinkButton).CommandArgument;
            hfPhotoID.Value = SurveyID;

            string[] parameter = { "@PhotoID" };
            string[] value = { hfPhotoID.Value };
            DB_Status dbs = dba.sp_populateDataSet("Sp_Gallery_Photo_Delete", 1, parameter, value);
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
                hfPhotoID.Value = "";
                FillPhotos();
            }
        }
        catch (Exception ex)
        {

        }
    }

}