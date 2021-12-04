using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login2 : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    private void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["Theme"] == null)
            Session["Theme"] = "theme_blue";
        Page.Theme = Session["Theme"].ToString();        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
        }
    }

    protected void DisplaySugMessage(string msg, string type)
    {
        lblSugMsg.Text = msg;
        if (type.ToUpper() == "ERROR")
            lblSugMsg.ForeColor = System.Drawing.Color.Red;
        else if (type.ToUpper() == "INFO")
            lblSugMsg.ForeColor = System.Drawing.Color.Green;
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtUserName.Text == "" && txtPassword.Text == "")
            {
                DisplaySugMessage("Please enter Username and Password", "error");
            }
            else if (txtUserName.Text == "")
            {
                DisplaySugMessage("Please enter Username", "error");
            }
            else if (txtPassword.Text == "")
            {
                DisplaySugMessage("Please enter Password", "error");
            }
            else
            {
                if (txtUserName.Text == "admin")
                {
                    DataTable dt = new DataTable();
                    #region--admin login
                    string result = "";
                    string[] parameter = { "@UserName", "@Password" };
                    string[] value = { txtUserName.Text, obj.Encrypt(txtPassword.Text) };
                    DB_Status dbs = dba.sp_populateDataSet("SP_Admin_Login", 2, parameter, value);
                    if (dbs.OperationStatus.ToString() == "Success")
                    {
                        DataSet ds = dbs.ResultDataSet;
                        if (ds.Tables.Count > 0)
                        {
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                result = dt.Rows[0]["Result"].ToString();
                            }
                        }
                    }
                    if (result == "NotExists")
                    {

                        DisplaySugMessage("User Not Exists", "error");
                    }
                    else if (result == "IncorrectPassword")
                    {
                        DisplaySugMessage("Incorrect Password", "error");
                    }
                    else if (result == "ok")
                    {
                        Session["AdminUserID"] = "admin";
                        Session["AdminName"] = "admin";
                        Session["username"] = "admin";
                        Session["EmpID"] = Convert.ToString(dt.Rows[0]["RID"]);
                        Session["DocLevelCode"] = null;
                        Response.Redirect("Admin/dashboard.aspx");
                    }
                    else
                    {

                        DisplaySugMessage("Error", "error");
                    }
                    #endregion
                }
                else
                {
                    #region--DeptAdmin or Employee
                    string result = "";
                    string EID = "";
                    string EmpCode = "";
                    string username = "";
                    string EmpName = "";
                    string UserRole = "";
                    string EmpDept = "";
                    string DeptID = "";
                    string department = "";

                    string EncryptPassword = obj.Encrypt(txtPassword.Text);
                    string[] parameter = { "@Username", "@Password" };
                    string[] value = { txtUserName.Text, EncryptPassword };
                    DB_Status dbs = dba.sp_populateDataSet("SP_Employee_Login", 2, parameter, value);
                    if (dbs.OperationStatus.ToString() == "Success")
                    {
                        DataSet ds = dbs.ResultDataSet;
                        if (ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];

                            if (dt.Rows.Count > 0)
                            {
                                result = dt.Rows[0]["Result"].ToString();
                                if (result == "ok")
                                {
                                    DataTable dtUsers = ds.Tables[1];
                                    EID = dtUsers.Rows[0]["EID"].ToString();
                                    username = dtUsers.Rows[0]["UserName"].ToString();
                                    EmpName = dtUsers.Rows[0]["EmpName"].ToString();
                                    EmpCode = dtUsers.Rows[0]["EmpCode"].ToString();
                                    EmpDept = dtUsers.Rows[0]["Department"].ToString();
                                    UserRole = dtUsers.Rows[0]["UserRole"].ToString();
                                    DeptID = dtUsers.Rows[0]["DepartmentID"].ToString();
                                    department = dtUsers.Rows[0]["Department"].ToString();
                                }
                            }
                        }
                    }

                    if (result == "NotExists")
                    {
                        DisplaySugMessage("User Not Exists", "error");

                    }
                    else if (result == "IncorrectPassword")
                    {
                        DisplaySugMessage("Incorrect Password", "error");

                    }
                    else if (result == "ok")
                    {
                        Session["EmpID"] = EID;
                        Session["EmpCode"] = EmpCode;
                        Session["EmpName"] = EmpName;
                        Session["username"] = username;
                        Session["EmpName"] = EmpName;
                        Session["EmpDept"] = EmpDept;
                        Session["DeptID"] = DeptID;
                        Session["UserRole"] = UserRole;
                        Session["deprt_name"] = department;
                        Session["DocLevelCode"] = null;
                        if (UserRole == "DeptAdmin")
                            Response.Redirect("Admin/dashboard.aspx");
                        else if (UserRole == "Employee")
                            Response.Redirect("Employee/emp-profile-update.aspx");
                        else
                            Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        DisplaySugMessage("Error", "error");
                    }
                    #endregion
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnForgotPwdSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtUserName.Text == "")
            {
                DisplaySugMessage("Please Enter Your UserName", "error");
            }
            else
            {
                if (txtUserName.Text != "admin")
                {
                    string name = "";
                    string pwd = "";
                    string email = "";
                    string[] param = { "@UserName" };
                    string[] values = { txtUserName.Text };
                    DB_Status dbs = dba.sp_populateDataSet("Sp_Forgot_Password", 1, param, values);
                    if (dbs.OperationStatus.ToString() == "Success")
                    {
                        DataSet ds = dbs.ResultDataSet;
                        if (ds.Tables[0].Rows[0]["Result"].ToString() != "NotExists")
                        {
                            name = ds.Tables[0].Rows[0]["EmpName"].ToString();
                            pwd = obj.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                            email = ds.Tables[0].Rows[0]["EmailID"].ToString();
                            //if (push_mail(email, name, pwd))
                            //{
                            //    DisplaySugMessage("We have sent your current password in your e-mail. Please check.", "Info");
                            //    txtPassword.Text = "";
                            //}
                        }
                        else
                        {
                            DisplaySugMessage("Error", "Invalid UserID");
                        }
                    }
                }
                else
                {
                    string name = "";
                    string pwd = "";
                    string email = "";
                    string[] param = { };
                    string[] values = { };
                    DB_Status dbs = dba.sp_populateDataSet("Sp_Forgot_Password_Admin", 0, param, values);
                    if (dbs.OperationStatus.ToString() == "Success")
                    {
                        DataSet ds = dbs.ResultDataSet;

                        name = "Admin";
                        pwd = ds.Tables[0].Rows[0]["Password"].ToString();
                        email = ds.Tables[0].Rows[0]["EmailID"].ToString();

                        //if (push_mail(email, name, pwd))
                        //{
                        //    DisplaySugMessage("We have sent your current password in your e-mail. Please check.", "Info");
                        //    txtPassword.Text = "";
                        //}
                    }
                }

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void lbkbtnForgotPassword_Click(object sender, EventArgs e)
    {
        btnLogin.Visible = false;
        txtPassword.Visible = false;
        btnForgotPwdSubmit.Visible = true;
        lnkbtnCancel.Visible = true;
        lbkbtnForgotPassword.Visible = false;
        txtPassword.Text = "";
        txtUserName.Text = "";
    }
    protected void lnkbtnCancel_Click(object sender, EventArgs e)
    {
        btnLogin.Visible = true;
        btnForgotPwdSubmit.Visible = false;
        btnLogin.Visible = true;
        lnkbtnCancel.Visible = false;
        txtPassword.Visible = true;
        lbkbtnForgotPassword.Visible = true;
        txtPassword.Text = "";
        txtUserName.Text = "";
    }

    protected bool push_mail(string UserEmailid, string Name, string Password)
    {
        bool flag = false;
        try
        {
            string mailSubject = "Your Password at NTPC WR-II Region's Intranet Portal";

            string mailHTMLmsg = "";
            mailHTMLmsg += "<table width='670px' cellpadding='0' cellspacing='0'>";
            mailHTMLmsg += "<tr>";
            mailHTMLmsg += "<td>";
            mailHTMLmsg += "    <p style='font-family:Calibri; font-size:16px; font-weight:bold; margin:0; padding:0; margin-bottom:10px; color:#333333;'>Dear " + Name + ",</p>";
            mailHTMLmsg += "    <p style='font-family:Calibri; font-size:16px; margin:0; padding:0; line-height:20px; margin-bottom:35px; color:#333333;'>Your current password is " + Password + " </p>";
            mailHTMLmsg += "    <p style='font-family:Calibri; font-size:14px; margin:0; padding:0; line-height:18px; color:#333333;'>This is system generated mail. please don't reply.</p>";
            mailHTMLmsg += "</td>";
            mailHTMLmsg += "</tr>";
            mailHTMLmsg += "</table>";

            string EMailID = System.Configuration.ConfigurationManager.AppSettings["EMailID"];
            string EMailIDUserName = System.Configuration.ConfigurationManager.AppSettings["EMailIDUserName"];
            string EMailIDPassword = System.Configuration.ConfigurationManager.AppSettings["EMailIDPassword"];
            string EMailIDHost = System.Configuration.ConfigurationManager.AppSettings["EMailIDHost"];
            string EMailIDPort = System.Configuration.ConfigurationManager.AppSettings["EMailIDPort"];
            string EMailIDSSL = System.Configuration.ConfigurationManager.AppSettings["EMailIDSSL"];

            MailMessage mail = new MailMessage();
            mail.To.Add(UserEmailid);

            mail.From = new MailAddress(EMailID, "NTPC WR-II Region's Intranet Portal");
            mail.Subject = mailSubject;
            mail.Body = mailHTMLmsg;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = EMailIDHost;
            smtp.Port = Convert.ToInt16(EMailIDPort);


            if (EMailIDUserName.Trim() != "" && EMailIDPassword != "")
            {
                smtp.Credentials = new System.Net.NetworkCredential(EMailIDUserName, EMailIDPassword);
            }

            if (EMailIDSSL.Trim().ToUpper() == "TRUE")
                smtp.EnableSsl = true;
            else if (EMailIDSSL.Trim().ToUpper() == "FALSE")
                smtp.EnableSsl = false;

            smtp.Send(mail);
            flag = true;
        }
        catch (Exception ex)
        {

        }
        return flag;
    }
}