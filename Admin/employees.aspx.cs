using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;

public partial class Admin_news_post : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    protected void Page_Load(object sender, EventArgs e)
    {      
        if (Session["AdminUserID"] == null && Session["DeptID"]==null && Session["EmpName"]==null)
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {
            fill_dropdown();
            panelAddNew.Visible = false;
            panelView.Visible = true;

            fill_Employee();
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
    protected void fill_dropdown()
    {
        string[] param = { "@Flag" };
        string[] values = { "LoadDropDownValues" };
        DB_Status DBS = dba.sp_populateDataSet("SP_Employees_Admin", 1, param, values);
        if (DBS.OperationStatus.ToString() == "Success")
        {
            DataSet ds = DBS.ResultDataSet;
            //Table index 0 for Department
            ddlDept.DataSource = ds.Tables[0]; 
            ddlDept.DataTextField = "Department";
            ddlDept.DataValueField = "DeptID";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, "--Select--");
            ddlDept.Items[0].Value = "0";
            //Table index 1 for Designation
            ddlDesignation.DataSource = ds.Tables[1]; 
            ddlDesignation.DataTextField = "Designation";
            ddlDesignation.DataValueField = "DesignationID";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0,"--Select--");
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
            ddlBloodGroup.Items[0].Value="0";
        }
        else
        {
            displayMessage(DBS.Title, "error");
        }
    }
    protected void lbtn_AddNew_Click(object sender, EventArgs e)
    {
        panelAddNew.Visible = true;
        panelView.Visible = false;
        txtEmpCode.Text = "";
        txtEmpName.Text = "";
        txtEmpNameH.Text = "";
        ddlDept.SelectedValue = "0";
        ddlCadre.SelectedValue = "0";
        ddlLevel.SelectedValue = "0";
        ddlDesignation.SelectedValue = "0";
        txtEmailID.Text = "";
        txtMobile.Text = "";
        txtAlternateMobile.Text = "";
        txtIntercomOfc.Text = "";
        txtIntercomRes.Text = "";
        txtDOB.Text = "";
        txtDOR.Text = "";
        txtDOJNTPC.Text = "";
        txtDOJP.Text = "";
        txtDOPLast.Text = "";
        rbtGender.SelectedValue = "Male";
        ddlBloodGroup.SelectedValue = "0";
        ddlArea.SelectedValue = "";
        ddlQuarterType.SelectedValue = "";
        txtQuarterNumber.Text = "";
        txtPanNo.Text = "";
        ddlReleased.SelectedValue = "0";
        divreleaseddate.Visible = false;
        divreleasedreason.Visible = false;
        txtReleaseDate.Text = "";
        ddlReleaseReason.SelectedValue = "";
        txtReleaseReason.Text = "";
        txtReleaseReason.Visible = false;

        displayMessage("", "");
        displayGridMessage("", "");

        HDN_EID.Value = "";
        clearfields();
        btnSave.Text = "Save";
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fill_Employee();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string IPAdd;
            IPAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAdd))
                IPAdd = Request.ServerVariables["REMOTE_ADDR"];

            string updatedby = "0";
            if (Session["AdminUserID"] == null)
                updatedby = Session["EmpID"].ToString();

            if (txtEmpCode.Text == "")
                displayMessage("Please enter Emp code", "error");
            else if (txtEmpName.Text == "")
                displayMessage("Please enter Employee Name", "error");
            else if (txtEmpNameH.Text == "")
                displayMessage("Please enter Employee Name in Hindi", "error");
            else if (ddlDept.SelectedValue == "")
                displayMessage("Please select Department", "error");
            else if(ddlCadre.SelectedValue=="0")
                displayMessage("Please select Cadre", "error");
            else if (ddlLevel.SelectedValue == "")
                displayMessage("Please select Level", "error");
            else if (ddlDesignation.SelectedValue == "")
                displayMessage("Please select Designation", "error");
            else if (txtEmailID.Text == "")
                displayMessage("Please enter EmailID", "error");
            else if (obj.IsValidEmail(txtEmailID.Text.Trim()) == false)
                displayMessage("Please enter Valid EmailID", "error");
            else if (txtMobile.Text == "")
                displayMessage("Please enter Mobile No.", "error");
            else if (obj.IsValidPhone(txtMobile.Text.Trim()) == false)
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
                    dob = obj.makedate(txtDOB.Text);
                string dor = "";
                if (txtDOR.Text != "")
                    dor = obj.makedate(txtDOR.Text);
                string dojp = "";
                if (txtDOJP.Text != "")
                    dojp = obj.makedate(txtDOJP.Text);
                string doplast = "";
                if (txtDOPLast.Text != "")
                    doplast = obj.makedate(txtDOPLast.Text);
                string dojntpc = "";
                if (txtDOJNTPC.Text != "")
                    dojntpc = obj.makedate(txtDOJNTPC.Text);
                string releasedate = "";
                if (txtReleaseDate.Text != "")
                    releasedate = obj.makedate(txtReleaseDate.Text);

                string releasereason = ddlReleaseReason.SelectedValue;
                string releaseremark = txtReleaseReason.Text.Trim();

                bool flagValidImage = true;
                bool flagHasImage = true;
                string ImageFileName = "NA";
                string ImagePath = "Uploads/EmpPhoto/blank.png";
                if(btnSave.Text == "Update")
                {
                    ImagePath = HDN_ImagePath.Value;
                }

                string bloodgroup = "0";
                if (ddlBloodGroup.SelectedValue != "")
                    bloodgroup = ddlBloodGroup.SelectedValue;

                //Check Image is attached or not
                if (FileUploadImage.HasFile)
                {
                    string ext = Path.GetExtension(FileUploadImage.FileName);
                    ImageFileName = "Emp_" + txtEmpCode.Text.Trim() + ext;
                    ImagePath = "Uploads/EmpPhoto/" + ImageFileName;
                    if (!(ext == ".jpg" || ext == ".JPG" || ext == ".jpeg" || ext == ".JPEG" || ext == ".gif" || ext == ".GIF" || ext == ".png" || ext == ".PNG"))
                    {
                        flagValidImage = false;
                        displayMessage("Please attach only image file for profile picture", "error");
                    }
                    else if (Math.Round(((decimal)FileUploadImage.PostedFile.ContentLength / (decimal)1024), 2) > 1000)
                    {
                        flagValidImage = false;
                        displayMessage("Image Size should be Less than 1000kb", "error");
                    }
                    else
                        flagValidImage = true;
                }

                //Check Save or Update
                string Flag = "";
                if (btnSave.Text == "Save")
                    Flag = "Add";
                else if (btnSave.Text == "Update")
                    Flag = "Update";


                if (flagHasImage && flagValidImage)
                {
                    string pwd = txtDOB.Text.Replace("/", "");
                    string encryptedpwd = obj.Encrypt(pwd);

                    string[] param = { "@Flag", "@EID", "@EmpCode", "@EmpName", "@DeptID", "@DesignationID", "@LevelID", "@SeniorityOrder", "@EmailID", "@Mobile", "@AlternateMobileNumber", "@IntercomOffice", "@IntercomRes", "@Area", "@Gender", "@DOB", "@DOR", "@BloodGroupID", "@Cadre", "@PANNo", "@PhotoPath", "@IsDepartmentAdmin", "@IsHOD", "@Password", "@ImageFileName", "@EmpNameHindi", "@DOJ_atProject", "@DOP_Last", "@QuarterType", "@QuarterNo", "@DOJ_atNTPC", "@IsReleased", "@ReleasedOn", "@ReleaseReason","@IPAddress","@UpdatedBy", "@ReleaseRemark" };
                    string[] value = { Flag, HDN_EID.Value, txtEmpCode.Text, obj.TextCapatlized(txtEmpName.Text), ddlDept.SelectedValue, ddlDesignation.SelectedValue, ddlLevel.SelectedValue, "0", txtEmailID.Text, txtMobile.Text, txtAlternateMobile.Text.Trim(), txtIntercomOfc.Text.Trim(), txtIntercomRes.Text.Trim(), ddlArea.SelectedValue, rbtGender.SelectedValue, dob, dor, bloodgroup, ddlCadre.SelectedValue, txtPanNo.Text.Trim(), ImagePath, chkIsDepartmentAdmin.Checked.ToString(), chkIsHOD.Checked.ToString(), encryptedpwd, ImageFileName, txtEmpNameH.Text.Trim(), dojp, doplast, ddlQuarterType.SelectedValue, txtQuarterNumber.Text.Trim(), dojntpc, ddlReleased.SelectedValue, releasedate, releasereason,IPAdd, updatedby, releaseremark };
                    DB_Status dbs = dba.sp_populateDataSet("SP_Employees_Admin", 37, param, value);
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
                    else
                    {
                        displayMessage(dbs.Title, "error");
                    }

                    if (result == "Inserted")
                    {
                        if (ImageFileName != "NA" && flagHasImage && flagValidImage)
                            FileUploadImage.SaveAs(Server.MapPath("~/" + ImagePath));
                        HDN_EID.Value = "";
                        btnSave.Text = "Save";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
                        fill_Employee();
                        clearfields();
                        displayMessage("Employee Successfully Added", "info");
                    }
                    else if (result == "Updated")
                    {
                        if (ImageFileName != "NA" && flagHasImage && flagValidImage)
                        {
                            try
                            {
                                File.Delete(Request.PhysicalApplicationPath + ImagePath);
                            }
                            catch (Exception)
                            {
                            }
                            FileUploadImage.SaveAs(Server.MapPath("~/" + ImagePath));
                        }
                        HDN_EID.Value = "";
                        btnSave.Text = "Save";
                        panelAddNew.Visible = false;
                        panelView.Visible = true;
                        fill_Employee();
                        clearfields();
                        displayMessage("Employee Successfully Updated", "info");
                    }
                    else if (result == "EmployeeAlreadyExists")
                    {
                        displayMessage("Sorry! Employee Already Exists", "error");
                    }
                    else if (result == "Fail")
                    {
                        displayMessage("Server Error", "error");
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
       // hfQuestionID.Value = "";
        HDN_EID.Value = "";
        btnSave.Text = "Save";
        panelAddNew.Visible = false;
        panelView.Visible = true;
    }
    protected void fill_Employee()
    {
        string[] param = {"@Flag", "@EmpCode", "@EmpName" };
        string[] values = {"View", txtSearch_EmpID.Text.Trim(), txtSearch_EmpName.Text.Trim() };
        DB_Status DBS = dba.sp_populateDataSet("SP_Employees_Admin", 3, param, values);
        if (DBS.OperationStatus.ToString() == "Success")
        {
            DataSet ds = DBS.ResultDataSet;
            gridEmployee.DataSource = ds.Tables[0];
            gridEmployee.DataBind();
        }
        else
        {
            displayMessage(DBS.Title, "error");
        }
    }  
    protected void Update_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string QuestionID = (sender as LinkButton).CommandArgument;
            HDN_EID.Value = QuestionID;

            LinkButton lnkbtn_edit = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_edit.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = {"@Flag", "@EID" };
            string[] value = {"View_byID", HDN_EID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Employees_Admin", 2, parameter, value);
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
                        txtEmpCode.Text = ds.Tables[0].Rows[0]["EmpCode"].ToString();
                        txtEmpName.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                        txtEmpNameH.Text = ds.Tables[0].Rows[0]["EmpNameHindi"].ToString();
                        try
                        {
                            ddlDept.SelectedValue = ds.Tables[0].Rows[0]["DeptID"].ToString();
                        }
                        catch(Exception)
                        {
                            ddlDept.SelectedValue = "0";
                        }
                        try
                        {
                            ddlDesignation.SelectedValue = ds.Tables[0].Rows[0]["DesignationID"].ToString();
                        }
                        catch(Exception)
                        {
                            ddlDesignation.SelectedValue = "0";
                        }
                        try
                        {
                            ddlLevel.SelectedValue = ds.Tables[0].Rows[0]["LevelID"].ToString();
                        }
                        catch(Exception)
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
                        catch(Exception)
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

                        string gender  = ds.Tables[0].Rows[0]["Gender"].ToString();
                        if (gender != "")
                        {
                            rbtGender.SelectedValue = gender;
                        }

                        txtDOB.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                        txtDOR.Text = ds.Tables[0].Rows[0]["DOR"].ToString();
                        txtDOJP.Text = ds.Tables[0].Rows[0]["DOJ_atProject"].ToString();
                        txtDOPLast.Text = ds.Tables[0].Rows[0]["DOP_Last"].ToString();
                        txtDOJNTPC.Text = ds.Tables[0].Rows[0]["DOJ_atNTPC"].ToString();
                        

                        ddlReleased.SelectedValue= ds.Tables[0].Rows[0]["IsReleased"].ToString();
                        if (ddlReleased.SelectedValue == "1")
                        {
                            divreleaseddate.Visible = true;
                            divreleasedreason.Visible = true;
                        }
                        else
                        {
                            divreleaseddate.Visible = false;
                            divreleasedreason.Visible = false;
                        }

                        txtReleaseDate.Text= ds.Tables[0].Rows[0]["ReleasedOn"].ToString();
                        string releasereason = ds.Tables[0].Rows[0]["ReleaseReason"].ToString();
                        if(releasereason=="")
                        {
                            ddlReleaseReason.SelectedValue = "";
                            txtReleaseReason.Text = "";
                            txtReleaseReason.Visible = false;
                        }
                        else
                        {
                            ddlReleaseReason.SelectedValue = releasereason;
                            txtReleaseReason.Text = ds.Tables[0].Rows[0]["ReleaseRemark"].ToString();
                            txtReleaseReason.Visible = true;
                        }

                        string bloodgroup = ds.Tables[0].Rows[0]["BloodGroupID"].ToString();
                        if (bloodgroup != "" && bloodgroup != "0")
                        {
                            ddlBloodGroup.SelectedValue = bloodgroup;
                        }

                        HDN_ImagePath.Value = ds.Tables[0].Rows[0]["PhotoPath"].ToString();
                        string IsDepartmentAdmin = ds.Tables[0].Rows[0]["IsDepartmentAdmin"].ToString();
                        if (IsDepartmentAdmin != "")
                        {
                            chkIsDepartmentAdmin.Checked = Convert.ToBoolean(IsDepartmentAdmin);
                        }
                        string IsHOD = ds.Tables[0].Rows[0]["IsHOD"].ToString();
                        if (IsHOD != "")
                        {
                            chkIsHOD.Checked = Convert.ToBoolean(IsHOD);
                        }
                    }
                }
            }
            else
            {
                displayGridMessage(dbs.Title, "error");
            }

            if (flag)
            {
                panelAddNew.Visible = true;
                panelView.Visible = false;

                displayMessage("", "");
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            displayMessage(ex.Message, "error");
        }
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string QuestionID = (sender as LinkButton).CommandArgument;
            HDN_EID.Value = QuestionID;

            string IPAdd;
            IPAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAdd))
                IPAdd = Request.ServerVariables["REMOTE_ADDR"];

            string updatedby = "0";
            if (Session["AdminUserID"] == null)
                updatedby = Session["EmpID"].ToString();

            string[] parameter = { "@Flag", "@EID","@UpdatedBy","@IPAddress" };
            string[] value = { "Delete", HDN_EID.Value, updatedby, IPAdd };
            DB_Status dbs = dba.sp_populateDataSet("SP_Employees_Admin", 4, parameter, value);
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
            else
            {
                displayMessage(dbs.Title, "error");
            }

            if (result == "Deleted")
            {
                HDN_EID.Value = "";
                fill_Employee();
            }
        }
        catch (Exception)
        {
        }
    }
    protected void ResetPassword_Click(object sender, EventArgs e)
    {
        try
        {
            displayGridMessage("", "");
            string EmployeeID = (sender as LinkButton).CommandArgument;
            HDN_EID.Value = EmployeeID;

            LinkButton lnkbtn_reset = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnkbtn_reset.Parent.Parent;
            int rowindex = grdrow.RowIndex;

            string[] parameter = { "@Flag", "@EID" };
            string[] value = { "ResetPassword", HDN_EID.Value };
            DB_Status dbs = dba.sp_populateDataSet("SP_Employees_Admin", 2, parameter, value);
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
            else
            {
                displayMessage(dbs.Title, "error");
            }

            if (result == "Success")
            {
                HDN_EID.Value = "";
                lnkbtn_reset.Text = "Done";
                lnkbtn_reset.CssClass = "btn btn-success btn-rounded bs-actionsbox";
            }
        }
        catch (Exception)
        {
        }
    }
    protected void ViewList_Click(object sender, EventArgs e)
    {
       
    }
    protected void gridEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridEmployee.PageIndex = e.NewPageIndex;
        fill_Employee();
    }
    protected void clearfields()
    {
        object sender = new object();
        EventArgs myargs = new EventArgs();
        txtEmpCode.Text = "";
        txtEmpName.Text = "";
        ddlDept.ClearSelection();
        ddlDesignation.ClearSelection();
        ddlLevel.ClearSelection();
        txtEmailID.Text = "";
        txtMobile.Text = "";
        txtAlternateMobile.Text = "";
        ddlArea.SelectedValue = "";
        txtIntercomOfc.Text = "";
        txtIntercomRes.Text = "";    
        txtDOB.Text = "";
        txtDOR.Text = "";
        txtDOJP.Text = "";
        txtDOPLast.Text = "";
        ddlQuarterType.SelectedValue = "";
        txtQuarterNumber.Text = "";
        ddlCadre.ClearSelection();
        ddlBloodGroup.ClearSelection();
        rbtGender.ClearSelection();
        txtPanNo.Text = "";
        chkIsDepartmentAdmin.Checked = false;
        chkIsDepartmentAdmin.Checked = false;
        ddlReleaseReason.SelectedValue = "";
        txtReleaseReason.Text = "";
        txtReleaseReason.Visible = false;
    }
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string SheetName = "EmployeeDirectory";
            string strFileName = "EmployeeDirectory.xlsx";
            int totalrecords = 0;
            DataTable tbl = null;
            string[] parameter = {};
            string[] value = {};
            DB_Status dbs = dba.sp_populateDataSet("SP_Export_Employee_Directory", 0, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    tbl = ds.Tables[0];
                }
            }
            totalrecords = tbl.Rows.Count;
            if (totalrecords > 0)
            {
                //xcptn code
                ExcelPackage pck = new ExcelPackage();
                //*****************************************************************************************************************
                //First worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(SheetName);

                //[Common Settings] Load Filters in Cells
                ws.Cells["A1"].Value = "SrNo";
                ws.Cells["B1"].Value = "EmpCode";
                ws.Cells["C1"].Value = "EmpName";
                ws.Cells["D1"].Value = "Department";
                ws.Cells["E1"].Value = "Designation";
                ws.Cells["F1"].Value = "Level";
                ws.Cells["G1"].Value = "EmailID";
                ws.Cells["H1"].Value = "Mobile";
                ws.Cells["I1"].Value = "AlternateMobile";
                ws.Cells["J1"].Value = "Intercom(O)";
                ws.Cells["K1"].Value = "Intercom(R)";
                ws.Cells["L1"].Value = "EmpName(H)";
                ws.Cells["M1"].Value = "Department(H)";
                ws.Cells["N1"].Value = "DesignationH(H)";
                for (int i = 1; i <= totalrecords; i++)
                {
                    ws.Cells[i + 1, 1].Value = i.ToString();
                    ws.Cells[i + 1, 2].Value = tbl.Rows[i - 1]["EmpCode"].ToString();
                    ws.Cells[i + 1, 3].Value = tbl.Rows[i - 1]["EmpName"].ToString();
                    ws.Cells[i + 1, 4].Value = tbl.Rows[i - 1]["Department"].ToString();
                    ws.Cells[i + 1, 5].Value = tbl.Rows[i - 1]["Designation"].ToString();
                    ws.Cells[i + 1, 6].Value = tbl.Rows[i - 1]["Level"].ToString();
                    ws.Cells[i + 1, 7].Value = tbl.Rows[i - 1]["EmailID"].ToString();
                    ws.Cells[i + 1, 8].Value = tbl.Rows[i - 1]["Mobile"].ToString();
                    ws.Cells[i + 1, 9].Value = tbl.Rows[i - 1]["AlternateMobileNumber"].ToString();
                    ws.Cells[i + 1, 10].Value = tbl.Rows[i - 1]["IntercomOffice"].ToString();
                    ws.Cells[i + 1, 11].Value = tbl.Rows[i - 1]["IntercomResidence"].ToString();
                    ws.Cells[i + 1, 12].Value = tbl.Rows[i - 1]["EmpNameHindi"].ToString();
                    ws.Cells[i + 1, 13].Value = tbl.Rows[i - 1]["DepartmentH"].ToString();
                    ws.Cells[i + 1, 14].Value = tbl.Rows[i - 1]["DesignationH"].ToString();
                }

                ws.Column(1).AutoFit();
                ws.Column(2).AutoFit();
                ws.Column(3).AutoFit();
                ws.Column(4).AutoFit();
                ws.Column(5).AutoFit();
                ws.Column(6).AutoFit();
                ws.Column(7).AutoFit();
                ws.Column(8).AutoFit();
                ws.Column(9).AutoFit();
                ws.Column(10).AutoFit();
                ws.Column(11).AutoFit();
                ws.Column(12).AutoFit();
                ws.Column(13).AutoFit();
                ws.Column(14).AutoFit();

                //Write it back to the client
                try
                {
                    displayMessage(" ", "info");
                    Response.Clear();
                    Response.ClearHeaders();

                    Response.AppendHeader("content-disposition", "attachment;  Filename=" + strFileName + "");
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.BinaryWrite(pck.GetAsByteArray());
                    Response.End();

                    //string path = Server.MapPath(".") + "/" + strFileName;
                    //Stream stream = File.Create(path);
                    //pck.SaveAs(stream);
                    //stream.Close();
                    displayGridMessage("Directory Successfully Downloaded", "info");
                }
                catch (Exception ex)
                {
                    displayGridMessage(ex.Message, "error");
                }
            }
            else
            {
                displayGridMessage("Sorry! No record present", "error");
            }
        }
        catch(Exception)
        {

        }
    }

    protected void ddlReleaseReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReleaseReason.SelectedValue != "")
            txtReleaseReason.Visible = true;
        else
            txtReleaseReason.Visible = false;
    }

    protected void ddlReleased_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlReleased.SelectedValue=="1")
        {
            divreleaseddate.Visible = true;
            divreleasedreason.Visible = true;
        }
        else
        {
            divreleaseddate.Visible = false;
            divreleasedreason.Visible = false;
        }
    }

    protected void txtDOB_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string dob = txtDOB.Text.Trim();
            string[] dobarr=null;
            if(dob!="" && dob.Length==10)
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
                    if(dtold.Day==1)
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
        catch(Exception)
        {
        }
    }
}