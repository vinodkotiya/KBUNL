using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class directory : System.Web.UI.Page
{
    DB_Access dba = new DB_Access();
    Class1 obj = new Class1();
    private void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["Theme"] == null)
            Session["Theme"] = "theme_green";
        Page.Theme = Session["Theme"].ToString();
        if (Session["Theme"].ToString() == "theme_blue")
        {
            LinkButton themebtn = WCTopBar1.GetThemeButton_Blue;
            themebtn.CssClass = "themebtn themebtn_blue active";
        }
        else if (Session["Theme"].ToString() == "theme_green")
        {
            LinkButton themebtn = WCTopBar1.GetThemeButton_Green;
            themebtn.CssClass = "themebtn themebtn_green active";
        }
        else if (Session["Theme"].ToString() == "theme_black")
        {
            LinkButton themebtn = WCTopBar1.GetThemeButton_Purple;
            themebtn.CssClass = "themebtn themebtn_purple active";
        }
        else if (Session["Theme"].ToString() == "theme_orange")
        {
            LinkButton themebtn = WCTopBar1.GetThemeButton_Orange;
            themebtn.CssClass = "themebtn themebtn_orange active";
        }
        else if (Session["Theme"].ToString() == "theme_red")
        {
            LinkButton themebtn = WCTopBar1.GetThemeButton_Red;
            themebtn.CssClass = "themebtn themebtn_red active";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDepartments();
        }
    }

    protected void LoadDepartments()
    {
        string[] param = { "@Flag" };
        string[] values = { "LoadDepartments" };
        DB_Status DBS = dba.sp_populateDataSet("SP_Search", 1, param, values);
        if (DBS.OperationStatus.ToString() == "Success")
        {
            DataSet ds = DBS.ResultDataSet;
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataTextField = "Department";
                ddlDepartment.DataValueField = "DeptID";
                ddlDepartment.DataBind();
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string FromDate = "";
            string ToDate = "";
            if (txtDateFrom.Text.Trim() != "")
                FromDate = obj.makedate(txtDateFrom.Text.Trim()); 
            if (txtDateTo.Text.Trim() != "")
                ToDate = obj.makedate(txtDateTo.Text.Trim());
            string HTMLCode = "";
            string[] param = { "@Flag", "@DeptID", "@Title", "@FromDate", "@ToDate", "@DocType" };
            string[] values = { "Search", ddlDepartment.SelectedValue, txtname.Text.Trim(), FromDate, ToDate, ddlDocType.SelectedValue };
            DB_Status DBS = dba.sp_populateDataSet("SP_Search", 6, param, values);
            if (DBS.OperationStatus.ToString() == "Success")
            {
                DataSet ds = DBS.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        HTMLCode += "<h6>" + dt.Rows.Count.ToString() + " result(s) found</h6>";
                        HTMLCode += "<table cellspacing='0' cellpadding='0'>";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string filestring = "";
                            if (dt.Rows[i]["DocumentType"].ToString() == "Circulars")
                            {
                                string LinkURL = Convert.ToString(dt.Rows[i]["LinkURL"]);
                                string AttachmentTitle1 = dt.Rows[i]["AttachmentTitle1"].ToString();
                                string AttachmentTitle2 = dt.Rows[i]["AttachmentTitle2"].ToString();
                                string AttachmentTitle3 = dt.Rows[i]["AttachmentTitle3"].ToString();
                                string AttachmentTitle4 = dt.Rows[i]["AttachmentTitle4"].ToString();
                                string AttachmentTitle5 = dt.Rows[i]["AttachmentTitle5"].ToString();
                                string AttachmentURL1 = dt.Rows[i]["AttachmentURL1"].ToString();
                                string AttachmentURL2 = dt.Rows[i]["AttachmentURL2"].ToString();
                                string AttachmentURL3 = dt.Rows[i]["AttachmentURL3"].ToString();
                                string AttachmentURL4 = dt.Rows[i]["AttachmentURL4"].ToString();
                                string AttachmentURL5 = dt.Rows[i]["AttachmentURL5"].ToString();

                                string links = "<div class='clinks'>";
                                if (AttachmentTitle1 != "" && AttachmentURL1 != "NA")
                                    links += "<a href='../" + AttachmentURL1 + "' target='_blank'>" + AttachmentTitle1 + "</a>";
                                if (AttachmentTitle2 != "" && AttachmentURL2 != "NA")
                                    links += "<a href='../" + AttachmentURL2 + "' target='_blank'>" + AttachmentTitle2 + "</a>";
                                if (AttachmentTitle3 != "" && AttachmentURL3 != "NA")
                                    links += "<a href='../" + AttachmentURL3 + "' target='_blank'>" + AttachmentTitle3 + "</a>";
                                if (AttachmentTitle4 != "" && AttachmentURL4 != "NA")
                                    links += "<a href='../" + AttachmentURL4 + "' target='_blank'>" + AttachmentTitle4 + "</a>";
                                if (AttachmentTitle5 != "" && AttachmentURL5 != "NA")
                                    links += "<a href='../" + AttachmentURL5 + "' target='_blank'>" + AttachmentTitle5 + "</a>";

                                if (LinkURL != "")
                                {
                                    links += "<a href='" + LinkURL + "' target='_blank'>Link</a>";
                                }
                                links += "</div>";

                                filestring = dt.Rows[i]["TitleHin"].ToString();
                                filestring += links;
                            }
                            else
                            {
                                if (dt.Rows[i]["FilePathLinkURL"].ToString() != "")
                                    filestring = "<a href='" + dt.Rows[i]["FilePathLinkURL"].ToString() + "' target='_blank'>" + dt.Rows[i]["TitleEng"].ToString() + "</a>";
                                else
                                    filestring = dt.Rows[i]["TitleHin"].ToString();
                            }

                            string Department = dt.Rows[i]["Department"].ToString();
                            string PostedOn = dt.Rows[i]["PostedOn_DDMMYYYY"].ToString();
                            string PostedBy = "";
                            if (Department != "" && Department != "Admin")
                                PostedBy += "<span class='postedby'>Posted by " + Department + " on " + PostedOn + "</span>";
                            else
                                PostedBy += "<span class='postedby'>Posted on " + PostedOn + "</span>";

                            HTMLCode += "<tr>";
                            HTMLCode += "<td class='doctype'>" + dt.Rows[i]["DocumentType"].ToString() + "</td>";
                            HTMLCode += "<td class='file'>" + filestring + "</td>";
                            HTMLCode += "<td class='postedby'>" + PostedBy + "</td>";
                            HTMLCode += "</tr>";
                        }
                        HTMLCode += "</table>";
                    }
                }
            }
            if(HTMLCode=="")
            {
                HTMLCode += "<h6>No result found</h6>";
            }
            divsearchresults.InnerHtml = HTMLCode;
        }
        catch (Exception ex)
        {

        }
    }
}