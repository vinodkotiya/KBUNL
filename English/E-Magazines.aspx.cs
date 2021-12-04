using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    Class1 obj = new Class1();
    DB_Access dba = new DB_Access();
    private int PageSize = 9;
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindYearDropdownlist();
            LoadEMagazine();
        }
    }

    public void BindYearDropdownlist()
    {
        int currentYear = DateTime.Now.Year;
        int j = 2010;
        for (int i=currentYear; i >= j; i--)
        {
            ddlYear.Items.Add(Convert.ToString(i));
        }
    }
    StringBuilder SearchCondition()
    {
        StringBuilder sb = new StringBuilder();
        //sb.Append(" AND DeptID="+Convert.ToInt32(hdfDeptId.Value));
        sb.Append(" AND A.Year='" + Convert.ToString(ddlYear.SelectedItem.Text) + "' ");
        if (ddlMonth.SelectedIndex > 0)
            sb.Append(" AND A.Month='" + Convert.ToString(ddlMonth.SelectedValue) + "' ");
        return sb;
    }
    protected void LoadEMagazine()
    {
        try
        {
            string[] parameter = { "@Search", "@Flag" };
            string[] value = { Convert.ToString(SearchCondition()), "PublicView" };
            DB_Status dbs = dba.sp_populateDataSet("SP_EMagazine", 2, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataTable dataTable = dt;
                    _PageDataSource.DataSource = dt.DefaultView;
                    _PageDataSource.AllowPaging = true;
                    _PageDataSource.PageSize = 9;
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
        this.LoadEMagazine();

    }
    protected void lbtnPrevious_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        this.LoadEMagazine();

    }
    protected void dlPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.Equals("Paging"))
        {
            CurrentPage = Convert.ToInt16(e.CommandArgument.ToString());
            this.LoadEMagazine();
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
        this.LoadEMagazine();

    }
    protected void lbtnFirst_Click(object sender, EventArgs e)
    {
        CurrentPage = 0;
        this.LoadEMagazine();
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadEMagazine();
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadEMagazine();
    }
}