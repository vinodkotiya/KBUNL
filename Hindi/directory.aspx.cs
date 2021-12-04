using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using OfficeOpenXml.Style;

public partial class directory : System.Web.UI.Page
{
    DB_Access dba = new DB_Access();
    private int PageSize = 18;
    private int CurrentPage
    {
        get
        {
            object objPage = ViewState["_CurrentPage"];
            int _CurrentPage = 0;
            if (objPage == null)
            {
                _CurrentPage = 0;
            }
            else
            {
                _CurrentPage = (int)objPage;
            }
            return _CurrentPage;
        }
        set { ViewState["_CurrentPage"] = value; }
    }
    private int fistIndex
    {
        get
        {

            int _FirstIndex = 0;
            if (ViewState["_FirstIndex"] == null)
            {
                _FirstIndex = 0;
            }
            else
            {
                _FirstIndex = Convert.ToInt32(ViewState["_FirstIndex"]);
            }
            return _FirstIndex;
        }
        set { ViewState["_FirstIndex"] = value; }
    }
    private int lastIndex
    {
        get
        {

            int _LastIndex = 0;
            if (ViewState["_LastIndex"] == null)
            {
                _LastIndex = 0;
            }
            else
            {
                _LastIndex = Convert.ToInt32(ViewState["_LastIndex"]);
            }
            return _LastIndex;
        }
        set { ViewState["_LastIndex"] = value; }
    }

    PagedDataSource _PageDataSource = new PagedDataSource();
    public DataTable dtEmpServices { get; set; }

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
            BindDropdownlist();
            if (Request.QueryString["dptId"] != null)
            {
                hdfDeptId.Value = Convert.ToString(Request.QueryString["dptId"]);
                ddlDepartment.SelectedValue = hdfDeptId.Value;
            }
            LoadEmployeeDirectory();
        }
        LoadEmployeeServices();
    }

    protected void BindDropdownlist()
    {
        string[] param = { "@Flag" };
        string[] values = { "LoadDropDownValues" };
        DB_Status DBS = dba.sp_populateDataSet("SP_Employees_Admin", 1, param, values);
        if (DBS.OperationStatus.ToString() == "Success")
        {
            DataSet ds = DBS.ResultDataSet;
            //Table index 0 for Department
            ddlDepartment.DataSource = ds.Tables[0];
            ddlDepartment.DataTextField = "DepartmentH";
            ddlDepartment.DataValueField = "DeptID";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, "सभी");
            ddlDepartment.Items[0].Value = "0";

            //Table index 1 for Designation
            ddlDesignation.DataSource = ds.Tables[1];
            ddlDesignation.DataTextField = "DesignationH";
            ddlDesignation.DataValueField = "DesignationID";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, "सभी");
            ddlDesignation.Items[0].Value = "0";

            //Table index 2 for Level
            ddlGrade.DataSource = ds.Tables[2];
            ddlGrade.DataTextField = "Level";
            ddlGrade.DataValueField = "LevelID";
            ddlGrade.DataBind();
            ddlGrade.Items.Insert(0, "सभी");
            ddlGrade.Items[0].Value = "0";

            //Table index 4 for Blood Group
            ddlBloodGroup.DataSource = ds.Tables[4];
            ddlBloodGroup.DataTextField = "BloodGroup";
            ddlBloodGroup.DataValueField = "BloodGroupID";
            ddlBloodGroup.DataBind();
            ddlBloodGroup.Items.Insert(0, "सभी");
            ddlBloodGroup.Items[0].Value = "0";


        }
    }
    public void LoadEmployeeServices()
    {
        try
        {
            string[] parameter = { "@Flag" };
            string[] value = { "Load" };
            DB_Status dbs = dba.sp_populateDataSet("SP_Employee_Service", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    dtEmpServices = ds.Tables[0];
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    StringBuilder SearchCondition()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" AND 1=1");
        if (ddlDepartment.SelectedIndex > 0)
            sb.Append(" AND E.DeptID="+Convert.ToInt32(ddlDepartment.SelectedValue));
        else if(Request.QueryString["DeptID"]!=null)
        {
            sb.Append(" AND E.DeptID=" + Convert.ToInt32(Request.QueryString["DeptID"].ToString()));
            ddlDepartment.SelectedValue = Request.QueryString["DeptID"].ToString();
        }

        if (ddlGrade.SelectedIndex > 0) 
            sb.Append(" AND E.LevelID=" + Convert.ToInt32(ddlGrade.SelectedValue));

        if (ddlDesignation.SelectedIndex > 0) 
            sb.Append(" AND E.DesignationID=" + Convert.ToInt32(ddlDesignation.SelectedValue));

        if (ddlBloodGroup.SelectedIndex > 0)
            sb.Append(" AND E.BloodGroupID=" + Convert.ToInt32(ddlBloodGroup.SelectedValue));

        if(txtEmpNo.Text.Trim()!="")
            sb.Append(" AND E.EmpCode Like '%" + txtEmpNo.Text.Trim() + "%'");

        if (!string.IsNullOrEmpty(txtname.Text.Trim()))
            sb.Append(" AND E.EmpName Like '%"+txtname.Text.Trim()+"%'");

        return sb;
    }

    public void LoadEmployeeDirectory()
    {
        try
        {
            string[] parameter = { "@Search" };
            string[] value = { Convert.ToString(SearchCondition()) };
            DB_Status dbs = dba.sp_populateDataSet("SP_Directory_Load", 1, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataTable dataTable = dt;
                    //dtlstDirectory.DataSource = dt;
                    //dtlstDirectory.DataBind();

                    _PageDataSource.DataSource = dt.DefaultView;
                    _PageDataSource.AllowPaging = true;
                    _PageDataSource.PageSize = PageSize;
                    _PageDataSource.CurrentPageIndex = CurrentPage;
                    ViewState["TotalPages"] = _PageDataSource.PageCount;

                    this.lbtnNext.Enabled = !_PageDataSource.IsLastPage;
                    //this.lbtnLast.Enabled = !_PageDataSource.IsFirstPage;
                    this.repeaterEmployee.DataSource = _PageDataSource;
                    this.repeaterEmployee.DataBind();
                    if (dt.Rows.Count > 8)
                    {
                        this.doPaging();
                        paging.Visible = true;
                    }
                    else
                    {
                        paging.Visible = false;
                    }

                    repeaterEmployee.DataSource = _PageDataSource;
                    repeaterEmployee.DataBind();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void doPaging()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PageIndex");
        dt.Columns.Add("PageText");

        fistIndex = CurrentPage - 5;


        if (CurrentPage > 5)
        {
            lastIndex = CurrentPage + 5;
        }
        else
        {
            lastIndex = 10;
        }
        if (lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
        {
            lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
            fistIndex = lastIndex - 10;
        }

        if (fistIndex < 0)
        {
            fistIndex = 0;
        }

        for (int i = fistIndex; i < lastIndex; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = i;
            dr[1] = i + 1;
            dt.Rows.Add(dr);
        }

        this.dlPaging.DataSource = dt;
        this.dlPaging.DataBind();
    }

    protected void lbtnNext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        this.LoadEmployeeDirectory();
    }
    protected void lbtnPrevious_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        this.LoadEmployeeDirectory();
    }
    protected void dlPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.Equals("Paging"))
        {
            CurrentPage = Convert.ToInt16(e.CommandArgument.ToString());
            this.LoadEmployeeDirectory();
        }
    }
    protected void dlPaging_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton lnkbtnPage = (LinkButton)e.Item.FindControl("lnkbtnPaging");
        if (lnkbtnPage.CommandArgument.ToString() == CurrentPage.ToString())
        {
            lnkbtnPage.Enabled = false;
            lnkbtnPage.Style.Add("fone-size", "14px");
            lnkbtnPage.Font.Bold = true;
            lnkbtnPage.CssClass = "active";
        }
    }
    protected void lbtnLast_Click(object sender, EventArgs e)
    {
        CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
        this.LoadEmployeeDirectory();
    }
    protected void lbtnFirst_Click(object sender, EventArgs e)
    {
        CurrentPage = 0;
        this.LoadEmployeeDirectory();
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadEmployeeDirectory();
    }
    protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadEmployeeDirectory();
    }
    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadEmployeeDirectory();
    }
    protected void txtEmpNo_TextChanged(object sender, EventArgs e)
    {
        LoadEmployeeDirectory();
    }
    protected void txtname_TextChanged(object sender, EventArgs e)
    {
        LoadEmployeeDirectory();
    }
    protected void ddlBloodGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadEmployeeDirectory();
    }
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string SheetName = "EmployeeDirectory";
            string strFileName = "EmployeeDirectory.xlsx";
            int totalrecords = 0;
            DataTable tbl = null;
            string[] parameter = { };
            string[] value = { };
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
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
            }
        }
        catch (Exception)
        {

        }
    }
}