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
    Class1 mod = new Class1();
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
            string search = "all";
            if (Request.QueryString["search"] != null)
            {
                search = Request.QueryString["search"];
            }
            if (search == "")
                search = "all";

            hfsearch.Value = search;
            LoadSuperannuationList();
        }
    }
    public void LoadSuperannuationList()
    {
        try
        {
            string fromdate = "1/1/1900";
            string todate = "12/31/2199";
            if (txtFromDate.Text.Trim() != "")
                fromdate = mod.makedate(txtFromDate.Text.Trim());
            if (txtToDate.Text.Trim() != "")
                todate = mod.makedate(txtToDate.Text.Trim());

            DataTable dt = new DataTable();
            string[] parameter = { "@Search", "@fromdate", "@todate" };
            string[] value = { hfsearch.Value.Trim(), fromdate, todate };
            DB_Status dbs = dba.sp_populateDataSet("SP_Directory_Superannuation", 3, parameter, value);
            if (dbs.OperationStatus.ToString() == "Success")
            {
                DataSet ds = dbs.ResultDataSet;
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];                    
                }
            }

            gridDirectory.DataSource = dt;
            gridDirectory.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    protected void gridDirectory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridDirectory.PageIndex = e.NewPageIndex;
        LoadSuperannuationList();
    }

    protected void btnDirectorySearch_Click(object sender, EventArgs e)
    {
        string search = "all";
        if (txtDirectorySearch.Text.Trim() != "")
            search = txtDirectorySearch.Text.Trim();
        hfsearch.Value = search;
        LoadSuperannuationList();
    }
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string SheetName = "UpcomingSuperannuationList";
            string strFileName = "UpcomingSuperannuationList.xlsx";
            int totalrecords = 0;
            DataTable tbl = null;

            string fromdate = "1/1/1900";
            string todate = "12/31/2199";
            if (txtFromDate.Text.Trim() != "")
                fromdate = mod.makedate(txtFromDate.Text.Trim());
            if (txtToDate.Text.Trim() != "")
                todate = mod.makedate(txtToDate.Text.Trim());

            DataTable dt = new DataTable();
            string[] parameter = { "@Search", "@fromdate", "@todate" };
            string[] value = { hfsearch.Value.Trim(), fromdate, todate };
            DB_Status dbs = dba.sp_populateDataSet("SP_Directory_Superannuation", 3, parameter, value);

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
                ws.Cells["N1"].Value = "DOR";
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
                    ws.Cells[i + 1, 14].Value = tbl.Rows[i - 1]["DOR"].ToString();
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
                ws.Column(15).AutoFit();

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