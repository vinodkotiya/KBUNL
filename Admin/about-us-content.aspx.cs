using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_about_us_content : System.Web.UI.Page
{
    Class1 mod = new Class1();
    DB_Access obj = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] != null)
        {
            hdfDept_Id.Value = "0";
            div_headTitle.InnerText = "Admin > About us";
        }
        else if (Session["DeptID"] != null && Session["EmpName"] != null)
        {
            div_headTitle.InnerText = "Department : " + Convert.ToString(Session["deprt_name"]) + " > About us";
            hdfDept_Id.Value = Session["DeptID"].ToString();
        }
        else
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {
            //System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_redmessage");
            //menuli.Attributes["class"] = "active";
            Fill_AboutUs();
        }
    }
    protected void Fill_AboutUs()
    {
        try
        {
            string[] parameter = { "@Flag" };
            string[] value = { "View"};
            DB_Status dbs = obj.sp_populateDataSet("SP_AboutUs", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {

                        btnSubmit.Visible = false;
                        btnUpdate.Visible = true;
                        txtAboutUsE.Content = Convert.ToString(dt.Rows[0]["AboutUsEnglish"]);
                        txtAboutUsH.Content = Convert.ToString(dt.Rows[0]["AboutUsHindi"]);
                        txtAddresssE.Text = Convert.ToString(dt.Rows[0]["AddressEnglish"]);
                        txtAddressH.Text = Convert.ToString(dt.Rows[0]["AddressHindi"]);
                        txtFliteE.Text = Convert.ToString(dt.Rows[0]["FliteEnglish"]);
                                                      txtFliteH.Text = Convert.ToString(dt.Rows[0]["FliteHindi"]);
                        txtRoadE.Text = Convert.ToString(dt.Rows[0]["RoadEnglish"]);
                        txtRoadH.Text = Convert.ToString(dt.Rows[0]["RoadHindi"]);
                        txtTrainE.Text = Convert.ToString(dt.Rows[0]["TrainEnglish"]);
                        txtTrainH.Text = Convert.ToString(dt.Rows[0]["TrainHindi"]);
                        txtLinkURL.Text = Convert.ToString(dt.Rows[0]["GoogleMapLink"]);
                        hdfImg1.Value = Convert.ToString(dt.Rows[0]["AboutUsImage1"]);
                        hdfImg2.Value = Convert.ToString(dt.Rows[0]["AboutUsImage2"]);
                        hdfImg3.Value = Convert.ToString(dt.Rows[0]["AboutUsImage3"]);
                        if (hdfImg1.Value != "NA" && hdfImg1.Value != "")
                        {
                            RequiredFieldValidatorfUpload1.Enabled = false;
                            img1.ImageUrl ="../"+Convert.ToString(dt.Rows[0]["AboutUsImage1"]);
                        }
                        else
                        {
                            RequiredFieldValidatorfUpload1.Enabled = true ;
                            img1.ImageUrl = "~/Admin/images/default-photo.png";
                        }

                        if (hdfImg2.Value != "NA" && hdfImg2.Value != "")
                        {
                            RequiredFieldValidatorfUpload2.Enabled = false;
                            img2.ImageUrl = "../" + Convert.ToString(dt.Rows[0]["AboutUsImage2"]);
                        }
                        else
                        {
                            RequiredFieldValidatorfUpload2.Enabled = true;
                            img2.ImageUrl = "~/Admin/images/default-photo.png";
                        }
                        if (hdfImg3.Value != "NA" && hdfImg3.Value != "")
                        {
                            RequiredFieldValidatorfupload3.Enabled = false;
                            img3.ImageUrl = "../" + Convert.ToString(dt.Rows[0]["AboutUsImage3"]);
                        }
                        else
                        {
                            RequiredFieldValidatorfupload3.Enabled = true;
                            img3.ImageUrl = "~/Admin/images/default-photo.png";
                        }
                        
                    }
                    else
                    {
                        txtAboutUsE.Content = "";
                        txtAboutUsH.Content = "";
                        txtAddresssE.Text = "";
                        txtAddressH.Text = "";
                        txtFliteE.Text = "";
                        txtFliteH.Text = "";
                        txtRoadE.Text = "";
                        txtRoadH.Text = "";
                        txtTrainE.Text = "";
                        txtTrainH.Text = "";
                        hdfImg1.Value = "";
                        hdfImg2.Value = "";
                        hdfImg3.Value = "";
                        btnSubmit.Visible = true;
                        btnUpdate.Visible = false;
                        DisplayEventMessage("", "");
                    }
                }
                
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void DisplayEventMessage(string msg, string type)
    {
        lblMsg.Text = msg;
        if (type == "error")
            lblMsg.ForeColor = System.Drawing.Color.Red;
        else if (type == "info")
            lblMsg.ForeColor = System.Drawing.Color.Green;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SaveRecord("Add");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        SaveRecord("Update");
    }
    protected void SaveRecord(string flagValue)
    {
        try
        {
            if (txtAboutUsE.Content == "")
                DisplayEventMessage("Please Enter About Us-English", "error");
            else if (txtAboutUsH.Content == "")
                DisplayEventMessage("Please Enter About Us-Hindi", "error");
            else if (txtAddresssE.Text == "")
                DisplayEventMessage("Please Enter Address", "error");
            else if (txtAddressH.Text == "")
                DisplayEventMessage("Please Enter Address", "error");
            else if (txtFliteE.Text == "")
                DisplayEventMessage("Please Enter Flite", "error");
            else if (txtFliteH.Text == "")
                DisplayEventMessage("Please Enter Flite", "error");
            else if (txtRoadE.Text == "")
                DisplayEventMessage("Please Enter Road", "error");
            else if (txtRoadE.Text == "")
                DisplayEventMessage("Please Enter Road", "error");
            else if (txtTrainE.Text == "")
                DisplayEventMessage("Please Enter Train", "error");
            else if (txtTrainH.Text == "")
                DisplayEventMessage("Please Enter Train", "error");
            else
            {
                //------------------------Image 1------------------------------------------
                bool flagValidImage1 = true;
                bool flagHasImage1 = false;
                string ImageFileName1 = "";
                string ImagePath1 = "NA";
                if (fupload1.HasFile)
                {
                    flagHasImage1 = true;
                    string ext = Path.GetExtension(fupload1.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    ImageFileName1 = "AboutusImage1" + "_" + datevalue + ext;
                    ImagePath1 = "Uploads/Aboutus/" + ImageFileName1;
                    hdfImg1.Value = ImagePath1;
                    if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                        flagValidImage1 = true;
                    else
                        flagValidImage1 = false;
                    if (flagValidImage1 == false)
                    {
                        DisplayEventMessage("Please browse .jpg,.JPG,.png,.PNG image", "error");
                        hdfImg1.Value = "No";
                        return;
                    }
                }
                else
                {
                    if (hdfImg1.Value == "No")
                    {
                        DisplayEventMessage("Please browse image", "error");
                        return;
                    }
                }
                //------------------------------Image 2---------------------------------
                bool flagValidImage2 = true;
                bool flagHasImage2 = false;
                string ImageFileName2 = "";
                string ImagePath2 = "NA";
                if (fUpload2.HasFile)
                {
                    flagHasImage1 = true;
                    string ext = Path.GetExtension(fUpload2.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    ImageFileName2 = "AboutusImage2" + "_" + datevalue + ext;
                    ImagePath2 = "Uploads/Aboutus/" + ImageFileName2;
                    hdfImg2.Value = ImagePath2;
                    if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                        flagValidImage2 = true;
                    else
                        flagValidImage2 = false;
                    if (flagValidImage2 == false)
                    {
                        DisplayEventMessage("Please browse .jpg,.JPG,.png,.PNG image", "error");
                        hdfImg2.Value = "No";
                        return;
                    }
                }
                else
                {
                    if (hdfImg2.Value == "No")
                    {
                        DisplayEventMessage("Please browse image", "error");
                        return;
                    }

                }
                //-----------------------------Image 3--------------------------------------
                bool flagValidImage3 = true;
                bool flagHasImage3 = false;
                string ImageFileName3 = "";
                string ImagePath3 = "NA";
                if (fupload3.HasFile)
                {
                    flagHasImage3 = true;
                    string ext = Path.GetExtension(fupload3.FileName);
                    string datevalue = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    ImageFileName3 = "AboutusImage3" + "_" + datevalue + ext;
                    ImagePath3 = "Uploads/Aboutus/" + ImageFileName3;
                    hdfImg3.Value = ImagePath3;
                    if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG")
                        flagValidImage3 = true;
                    else
                        flagValidImage3 = false;
                    if (flagValidImage3 == false)
                    {
                        DisplayEventMessage("Please browse .jpg,.JPG,.png,.PNG image", "error");
                        hdfImg3.Value = "No";
                        return;
                    }
                }
                else
                {
                    if (hdfImg3.Value == "No")
                    {
                        DisplayEventMessage("Please browse image", "error");
                        return;
                    }

                }
                string[] parameter = { "@Flag", "@AboutUsEnglish","@AboutUsHindi", "@AddressEnglish",  "@AddressHindi", "@FliteEnglish", "@FliteHindi", "@RoadEnglish", "@RoadHindi", "@TrainEnglish", "@TrainHindi", "@GoogleMapLink", "@AboutUsImage1", "@AboutUsImage2", "@AboutUsImage3" };
                string[] value = { flagValue, txtAboutUsE.Content.Trim(),txtAboutUsH.Content.Trim(),txtAddresssE.Text.Trim(),txtAddressH.Text.Trim(),txtFliteE.Text.Trim(),txtFliteH.Text.Trim(),txtRoadE.Text.Trim(),txtRoadH.Text.Trim(),txtTrainE.Text.Trim(),txtTrainH.Text.Trim(),txtLinkURL.Text.Trim(),hdfImg1.Value,hdfImg2.Value,hdfImg3.Value};
                DB_Status dbs = obj.sp_populateDataSet("SP_AboutUs", 15, parameter, value);
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
                if (result == "Inserted" || result == "Updated")
                {
                    if (fupload1.HasFile)
                        fupload1.SaveAs(Server.MapPath("~/" + hdfImg1.Value));
                    if (fUpload2.HasFile)
                        fUpload2.SaveAs(Server.MapPath("~/" + hdfImg2.Value));
                    if (fupload3.HasFile)
                        fupload3.SaveAs(Server.MapPath("~/" + hdfImg3.Value));

                    DisplayEventMessage("Successfully Saved", "info");
                    Fill_AboutUs();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Attributes["class"] = "error";
            lblMsg.Text = ex.Message.ToString();
        }
    }
}