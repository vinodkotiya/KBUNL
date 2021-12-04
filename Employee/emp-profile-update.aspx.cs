using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_emp_profile_update : System.Web.UI.Page
{
    Class1 mod = new Class1();
    DB_Access obj = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
        Response.Cache.SetNoStore();
        Response.Cache.SetNoStore();

        if (Session["username"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            System.Web.UI.HtmlControls.HtmlControl menuli = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("menu_companyusers");
            menuli.Attributes["class"] = "active";
            if (Session["EmpID"] != null)
            {
                hdfEmployeeId.Value = Convert.ToString(Session["EmpID"]);
                fill_dropdown();
                BindEmployeeInformation();
            }

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
    protected void fill_dropdown()
    {
        string[] param = { "@Flag" };
        string[] values = { "LoadDropDownValues" };
        DB_Status DBS = obj.sp_populateDataSet("SP_Employees_Admin", 1, param, values);
        if (DBS.OperationStatus.ToString() == "Success")
        {
            DataSet ds = DBS.ResultDataSet;
            //Table index 1 for Designation
            ddlDesignation.DataSource = ds.Tables[1];
            ddlDesignation.DataTextField = "Designation";
            ddlDesignation.DataValueField = "DesignationID";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, "--Select--");
            ddlDesignation.Items[0].Value = "0";
            //Table index 2 for Level
            ddlLevel.DataSource = ds.Tables[2];
            ddlLevel.DataTextField = "Level";
            ddlLevel.DataValueField = "LevelID";
            ddlLevel.DataBind();
            ddlLevel.Items.Insert(0, "--Select--");
            ddlLevel.Items[0].Value = "0";
            //Table index 4 for Blood Group
            ddlBloodGroup.DataSource = ds.Tables[4];
            ddlBloodGroup.DataTextField = "BloodGroup";
            ddlBloodGroup.DataValueField = "BloodGroupID";
            ddlBloodGroup.DataBind();
            ddlBloodGroup.Items.Insert(0, "--Select--");
            ddlBloodGroup.Items[0].Value = "0";
        }
    }
    public void BindEmployeeInformation()
    {
        try
        {
            string[] parameter = { "@Flag", "@EID" };
            string[] value = { "Profile_View", hdfEmployeeId.Value };
            DB_Status dbs = obj.sp_populateDataSet("SP_Employees_Admin", 2, parameter, value);
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
                        lblEmpCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["EmpCode"]);
                        lblEmpName.Text = Convert.ToString(ds.Tables[0].Rows[0]["EmpName"]);
                        lblEmpDept.Text = Convert.ToString(ds.Tables[0].Rows[0]["Department"]);
                        try
                        {
                            ddlDesignation.SelectedValue = ds.Tables[0].Rows[0]["DesignationID"].ToString();
                        }
                        catch (Exception)
                        {
                            ddlDesignation.SelectedValue = "0";
                        }
                        try
                        {
                            ddlLevel.SelectedValue = ds.Tables[0].Rows[0]["LevelID"].ToString();
                        }
                        catch (Exception)
                        {
                            ddlLevel.SelectedValue = "0";
                        }
                        try
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Cadre"])))
                                ddlCadre.SelectedValue = ds.Tables[0].Rows[0]["Cadre"].ToString();
                            else
                                ddlCadre.SelectedIndex = 0;
                        }
                        catch (Exception)
                        {
                            ddlCadre.SelectedIndex = 0;
                        }
                        txtEmailID.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                        txtMobile.Text = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        txtAlternateMobile.Text = ds.Tables[0].Rows[0]["AlternateMobileNumber"].ToString();
                        txtIntercomOfc.Text = ds.Tables[0].Rows[0]["IntercomOffice"].ToString();
                        txtIntercomRes.Text = ds.Tables[0].Rows[0]["IntercomResidence"].ToString();
                        try
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Quarterloc"])))
                                ddlArea.SelectedValue = ds.Tables[0].Rows[0]["Quarterloc"].ToString();
                            else
                                ddlArea.SelectedValue = "";
                        }
                        catch (Exception)
                        {
                            ddlArea.SelectedValue = "";
                        }
                        try
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["QuarterType"])))
                                ddlQuarterType.SelectedValue = ds.Tables[0].Rows[0]["QuarterType"].ToString();
                            else
                                ddlQuarterType.SelectedValue = "";
                        }
                        catch (Exception)
                        {
                            ddlQuarterType.SelectedValue = "";
                        }
                        txtQuarterNumber.Text = ds.Tables[0].Rows[0]["QuarterNo"].ToString();

                        txtPanNo.Text = ds.Tables[0].Rows[0]["PANNo"].ToString();

                        string gender = ds.Tables[0].Rows[0]["Gender"].ToString();
                        if (gender != "")
                        {
                            rbtGender.SelectedValue = gender;
                        }

                        txtDOB.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                        txtDOR.Text = ds.Tables[0].Rows[0]["DOR"].ToString();
                        txtDOJP.Text = ds.Tables[0].Rows[0]["DOJ_atProject"].ToString();
                        txtDOPLast.Text = ds.Tables[0].Rows[0]["DOP_Last"].ToString();
                        txtDOJNTPC.Text = ds.Tables[0].Rows[0]["DOJ_atNTPC"].ToString();

                        string bloodgroup = ds.Tables[0].Rows[0]["BloodGroupID"].ToString();
                        if (bloodgroup != "" && bloodgroup != "0")
                        {
                            ddlBloodGroup.SelectedValue = bloodgroup;
                        }

                        hdfImagePath.Value = Convert.ToString(ds.Tables[0].Rows[0]["PhotoPath"]);
                        imgEmpPhoto.ImageUrl = "../" + Convert.ToString(ds.Tables[0].Rows[0]["PhotoPath"]);

                        txtPanNo.Text = ds.Tables[0].Rows[0]["PANNo"].ToString();
                        btnSave.Text = "Update";
                    }
                }
            }

        }
        catch (Exception ex)
        {
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCadre.SelectedValue == "0")
                displayMessage("Please select Cadre", "error");
            else if (ddlLevel.SelectedValue == "")
                displayMessage("Please select Level", "error");
            else if (ddlDesignation.SelectedValue == "")
                displayMessage("Please select Designation", "error");
            else if (txtEmailID.Text == "")
                displayMessage("Please enter EmailID", "error");
            else if (mod.IsValidEmail(txtEmailID.Text.Trim()) == false)
                displayMessage("Please enter Valid EmailID", "error");
            else if (txtMobile.Text == "")
                displayMessage("Please enter Mobile No.", "error");
            else if (mod.IsValidPhone(txtMobile.Text.Trim()) == false)
                displayMessage("Please enter Valid Mobile No.", "error");
            else if (rbtGender.SelectedValue == "")
                displayMessage("Please choose Gender", "error");
            else if (txtDOB.Text == "")
                displayMessage("Please enter Date of Birth", "error");
            else if (txtDOR.Text == "")
                displayMessage("Please enter Date of Retirement", "error");
            else if (txtDOJP.Text == "")
                displayMessage("Please enter Date of Joining at Project", "error");
            else if (txtDOPLast.Text == "")
                displayMessage("Please enter Date of Last Promotion", "error");
            else
            {
                string dob = "";
                if (txtDOB.Text != "")
                    dob = mod.makedate(txtDOB.Text);
                string dor = "";
                if (txtDOR.Text != "")
                    dor = mod.makedate(txtDOR.Text);
                string dojp = "";
                if (txtDOJP.Text != "")
                    dojp = mod.makedate(txtDOJP.Text);
                string doplast = "";
                if (txtDOPLast.Text != "")
                    doplast = mod.makedate(txtDOPLast.Text);
                string dojntpc = "";
                if (txtDOJNTPC.Text != "")
                    dojntpc = mod.makedate(txtDOJNTPC.Text);

                string IPAdd;
                IPAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(IPAdd))
                    IPAdd = Request.ServerVariables["REMOTE_ADDR"];

                bool flagValidImage = true;
                bool flagHasImage = true;
                string ImageFileName = "NA";
                string ImagePath = "../images/profilepic.png";

                if (btnSave.Text == "Update")
                {
                    ImagePath = hdfImagePath.Value;
                }

                string bloodgroup = "0";
                if (ddlBloodGroup.SelectedValue != "")
                    bloodgroup = ddlBloodGroup.SelectedValue;

                //Check Image is attached or not
                if (FileUploadImage.HasFile)
                {
                    string ext = Path.GetExtension(FileUploadImage.FileName);
                    ImageFileName = "Emp_" + lblEmpCode.Text.Trim() + ext;
                    ImagePath = "Uploads/EmpPhoto/" + ImageFileName;

                    if (!(ext == ".jpg" || ext == ".JPG" || ext == ".png" || ext == ".PNG"))
                    {
                        flagValidImage = false;
                        displayMessage("Please attach only image file for profile picture", "error");
                    }
                    else if (Math.Round(((decimal)FileUploadImage.PostedFile.ContentLength / (decimal)1024), 2) > 200)
                    {
                        flagValidImage = false;
                        displayMessage("Image Size should be Less than 200kb", "error");
                    }
                    else
                        flagValidImage = true;
                }
                if (flagHasImage && flagValidImage)
                {

                    string[] param = { "@Flag","@IPAddress", "@EID", "@DesignationID", "@LevelID", "@EmailID", "@Mobile", "@AlternateMobileNumber", "@IntercomOffice", "@IntercomRes", "@Area", "@Gender", "@DOB", "@DOR", "@BloodGroupID", "@Cadre", "@PANNo", "@PhotoPath", "@ImageFileName", "@DOJ_atProject", "@DOP_Last", "@QuarterType", "@QuarterNo", "@DOJ_atNTPC",  "@UpdatedBy"};
                    string[] value = { "Profile_Update",IPAdd, hdfEmployeeId.Value, ddlDesignation.SelectedValue, ddlLevel.SelectedValue, txtEmailID.Text, txtMobile.Text, txtAlternateMobile.Text.Trim(), txtIntercomOfc.Text.Trim(), txtIntercomRes.Text.Trim(), ddlArea.SelectedValue, rbtGender.SelectedValue, dob, dor, bloodgroup, ddlCadre.SelectedValue, txtPanNo.Text.Trim(), ImagePath, ImageFileName, dojp, doplast, ddlQuarterType.SelectedValue, txtQuarterNumber.Text.Trim(), dojntpc, hdfEmployeeId.Value };
                    DB_Status dbs = obj.sp_populateDataSet("SP_Employees_Admin", 25, param, value);
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
                        if (ImageFileName != "NA" && flagHasImage && flagValidImage)
                            FileUploadImage.SaveAs(Server.MapPath("~/" + ImagePath));
                        displayMessage("Employee Profile Successfully Updated", "info");
                        BindEmployeeInformation();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            displayMessage(ex.Message, "error");
        }
    }

    protected void txtDOB_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string dob = txtDOB.Text.Trim();
            string[] dobarr = null;
            if (dob != "" && dob.Length == 10)
            {
                if (dob.Contains('/'))
                    dobarr = dob.Split('/');
                else if (dob.Contains('-'))
                    dobarr = dob.Split('-');

                if (dobarr.Length > 0)
                {
                    int dd = Convert.ToInt32(dobarr[0]);
                    int mm = Convert.ToInt32(dobarr[1]);
                    int yy = Convert.ToInt32(dobarr[2]);

                    DateTime dtold = new DateTime(yy, mm, dd);
                    if (dtold.Day == 1)
                    {
                        DateTime dttemp = dtold.AddYears(60);
                        DateTime dor = new DateTime(dttemp.Year, dttemp.Month, 1);
                        dor = dor.AddDays(-1);
                        txtDOR.Text = dor.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        DateTime dttemp = dtold.AddYears(60);
                        DateTime dor = new DateTime(dttemp.Year, dttemp.Month, 1);
                        dor = dor.AddMonths(1);
                        dor = dor.AddDays(-1);
                        txtDOR.Text = dor.ToString("dd/MM/yyyy");
                    }
                }
            }
        }
        catch (Exception)
        {
        }
    }
}