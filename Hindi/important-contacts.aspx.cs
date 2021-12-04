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

public partial class English_telephone_directory : System.Web.UI.Page
{
    DB_Access dba = new DB_Access();
    private int PageSize = 50;
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

    public DataTable dtEmpServices { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropdownlist();
            LoadTelephoneDirectory();
        }
        LoadEmployeeServices();
    }

    protected void BindDropdownlist()
    {
        string[] param = { "@Flag" };
        string[] values = { "DropdownlistBind" };
        DB_Status DBS = dba.sp_populateDataSet("SP_TelephoneDirectory_Group", 1, param, values);
        if (DBS.OperationStatus.ToString() == "Success")
        {
            DataSet ds = DBS.ResultDataSet;
            //Table index 0 for Department
            ddlGroup.DataSource = ds.Tables[0];
            ddlGroup.DataTextField = "GroupNameHindi";
            ddlGroup.DataValueField = "GroupId";
            ddlGroup.DataBind();
            ddlGroup.Items.Insert(0, "सभी");
            ddlGroup.Items[0].Value = "0";
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
        { }
    }
    StringBuilder SearchCondition()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" AND 1=1");
        if (ddlGroup.SelectedIndex > 0)
            sb.Append(" AND GD.GroupId=" + Convert.ToInt32(ddlGroup.SelectedValue));
        if (!string.IsNullOrEmpty(txtname.Text.Trim()))
            sb.Append(" AND GD.LocationOrNameEnglish Like '%" + txtname.Text.Trim() + "%'");

        return sb;
    }

    public void LoadTelephoneDirectory()
    {
        try
        {
            string[] parameter = { "@Flag", "@Search" };
            string[] value = { "PublicView", Convert.ToString(SearchCondition()),"0" };
            DB_Status dbs = dba.sp_populateDataSet("SP_TelephoneDirectory", 2, parameter, value);
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
        this.LoadTelephoneDirectory(); ;

    }
    protected void lbtnPrevious_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        this.LoadTelephoneDirectory(); ;

    }
    protected void dlPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.Equals("Paging"))
        {
            CurrentPage = Convert.ToInt16(e.CommandArgument.ToString());
            this.LoadTelephoneDirectory(); ;
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
        this.LoadTelephoneDirectory(); ;

    }
    protected void lbtnFirst_Click(object sender, EventArgs e)
    {

        CurrentPage = 0;
        this.LoadTelephoneDirectory(); 
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadTelephoneDirectory();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddlGroup.SelectedIndex = 0;
        txtname.Text = "";
        LoadTelephoneDirectory();
    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string SheetName = "ImportantDirectory";
            string strFileName = "ImportantDirectory.xlsx";
            int totalrecords = 0;
            DataTable tbl = null;
            string[] parameter = { };
            string[] value = { };
            DB_Status dbs = dba.sp_populateDataSet("SP_Export_Important_Directory", 0, parameter, value);
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
                ws.Cells["B1"].Value = "Category";
                ws.Cells["C1"].Value = "Location/Name";
                ws.Cells["D1"].Value = "Intercom";
                ws.Cells["E1"].Value = "Phone";
                ws.Cells["F1"].Value = "Email";
                ws.Cells["G1"].Value = "Address";
                ws.Cells["H1"].Value = "केटेगरी";
                ws.Cells["I1"].Value = "स्थान/नाम";
                for (int i = 1; i <= totalrecords; i++)
                {
                    ws.Cells[i + 1, 1].Value = i.ToString();
                    ws.Cells[i + 1, 2].Value = tbl.Rows[i - 1]["GroupNameEnglish"].ToString();
                    ws.Cells[i + 1, 3].Value = tbl.Rows[i - 1]["LocationOrNameEnglish"].ToString();
                    ws.Cells[i + 1, 4].Value = tbl.Rows[i - 1]["Intercome"].ToString();
                    ws.Cells[i + 1, 5].Value = tbl.Rows[i - 1]["Phone"].ToString();
                    ws.Cells[i + 1, 6].Value = tbl.Rows[i - 1]["Email"].ToString();
                    ws.Cells[i + 1, 7].Value = tbl.Rows[i - 1]["Address"].ToString();
                    ws.Cells[i + 1, 8].Value = tbl.Rows[i - 1]["GroupNameHindi"].ToString();
                    ws.Cells[i + 1, 9].Value = tbl.Rows[i - 1]["LocationOrNameHindi"].ToString();
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

    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTelephoneDirectory();
    }

    protected void txtname_TextChanged(object sender, EventArgs e)
    {
        LoadTelephoneDirectory();
    }
}