<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reports.aspx.cs" Inherits="reports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/English/WCCommon/WcHeadTags.ascx" TagPrefix="uc1" TagName="WcHeadTags" %>
<%@ Register Src="~/English/WCCommon/WCTopBar.ascx" TagPrefix="uc1" TagName="WCTopBar" %>
<%@ Register Src="~/English/WCCommon/MonthCalendar.ascx" TagPrefix="uc1" TagName="WCMonthlyCalendar" %>
<%@ Register Src="~/English/WCCommon/YearCalendar.ascx" TagPrefix="uc1" TagName="WCYearlyCalendar" %>
<%@ Register Src="~/English/WCCommon/WCFooter.ascx" TagPrefix="uc1" TagName="WCFooter" %>
<%@ Register Src="~/English/WCCommon/WCEmployeeServices.ascx" TagPrefix="uc1" TagName="WCEmpServices" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,initial-scale=1">
    <title>WR2HQ -Reports</title>
    <!--Header - Start-->
    <uc1:WcHeadTags runat="server" ID="WcHeadTags" />
    <!--Header - End-->
    <script type="text/javascript" language="javascript">

        // var e = document.getElementById("ddlRptType");
        //alert(e);
        // var strUser = e.options[e.selectedIndex].value;
        //alert(strUser);
        function onCalendarShown() {
            var cal = $find("calendar1");
            //Setting the default mode to year
            cal._switchMode("years", true);

            //Iterate every year Item and attach click event to it
            if (cal._yearsBody) {
                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                    var row = cal._yearsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }
        function onCalendarHidden() {
            var cal = $find("calendar1");
            //Iterate every month Item and remove click event from it
            if (cal._yearsBody) {
                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                    var row = cal._yearsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }
        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "year":
                    var cal = $find("calendar1");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }

        function onCalendarHiddenM() {
            var cal = $find("calendar2");

            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", callM);
                    }
                }
            }
        }

        function onCalendarShownM() {

            var cal = $find("calendar2");

            cal._switchMode("months", true);

            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", callM);
                    }
                }
            }
        }

        function callM(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar2");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    //cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptmgr" runat="server"></asp:ScriptManager>
        <div class="pagecontainer">
            <!--MenuBar - Start-->
            <uc1:WCTopBar runat="server" ID="WCTopBar1" />
            <!--MenuBar - End-->

            <!--Content - Start-->
            <div class="content-outer">

                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="contentbox1">
                            <!-- Employee Services - Start -->
                            <uc1:WCEmpServices runat="server" ID="WCEmpServices" />
                            <!-- Employee Services - End -->
                        </td>
                        <td class="contentboxinner">
                            <asp:HiddenField ID="hdfDeptId" runat="server" Value="0" />
                            <asp:HiddenField ID="hdfReportLevel" runat="server" Value="1" />
                            <asp:HiddenField ID="hdfParentReportId" runat="server" Value="0" />
                            <!-- Banner Start -->
                            <div class="innerbanner">
                                <img src="../images/banner-reports.jpg" alt="" />
                                <div class="bannerpatch">
                                    <h4>Reports</h4>
                                    <div class="breadcrumb">
                                        <a href="Default.aspx" class="home">Home</a>
                                        <a class="active">Reports</a>
                                    </div>
                                </div>
                            </div>
                            <!-- Banner End -->
                            <!--Download Content - Start -->
                            <asp:UpdatePanel ID="updatepnl" runat="server">
                                <ContentTemplate>
                                    <asp:HiddenField ID="hdfCategoryId" runat="server" Value="0" />
                                    <div class="reportcontent">
                                        <table cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td class="contentleft">
                                                    <div class="rptfilter">
                                                        Department:
                                                        <asp:DropDownList ID="ddlDepartment" CssClass="dropdownbox" runat="server"></asp:DropDownList>
                                                    </div>
                                                    <div class="rptfilter">
                                                        Report Type:
                                                        <asp:DropDownList ID="ddlRptType" runat="server" CssClass="dropdownbox" AutoPostBack="true" OnSelectedIndexChanged="ddlRptType_SelectedIndexChanged">
                                                            <asp:ListItem Text="Daily" Value="Daily"></asp:ListItem>
                                                            <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                                                            <asp:ListItem Text="Yearly" Value="Yearly"></asp:ListItem>
                                                            <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    </hr>
                                                    <div class="rptfilter" style="display: none;">
                                                        <%--------------------------Date----------------------------------------------%>
                                                        <asp:Label ID="lblreportType" runat="server" Text="Report Date:" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtDate" runat="server" MaxLength="10" CssClass="dropdownbox" AutoPostBack="true" OnTextChanged="txtDate_TextChanged" Visible="false"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtDate" runat="server" ValidChars="0123456789/" TargetControlID="txtDate"></asp:FilteredTextBoxExtender>
                                                        <asp:CalendarExtender ID="rptCalendarExtenderDate" runat="server" DefaultView="Days" Format="dd/MM/yyyy" Enabled="true" TargetControlID="txtDate"></asp:CalendarExtender>
                                                        <%--------------------------Month----------------------------------------------%>
                                                        <asp:TextBox ID="txtMonth" runat="server" MaxLength="7" CssClass="dropdownbox" Visible="false" AutoPostBack="true" OnTextChanged="txtMonth_TextChanged"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtMonth" runat="server" ValidChars="0123456789/" TargetControlID="txtMonth"></asp:FilteredTextBoxExtender>
                                                        <asp:CalendarExtender ID="rptCalendarExtenderMonth" ClientIDMode="Static" runat="server" TargetControlID="txtMonth" Format="MM/yyyy" BehaviorID="calendar2"
                                                            DefaultView="Months" OnClientShown="onCalendarShownM" OnClientHidden="onCalendarHiddenM" />
                                                        <%--------------------------Year----------------------------------------------%>
                                                        <asp:TextBox ID="txtYear" runat="server" MaxLength="4" CssClass="dropdownbox" Visible="false" AutoPostBack="true" OnTextChanged="txtYear_TextChanged"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender" runat="server" ValidChars="0123456789/" TargetControlID="txtYear"></asp:FilteredTextBoxExtender>
                                                        <asp:CalendarExtender ID="rptCalendarExtenderYear" runat="server" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown" BehaviorID="calendar1" Enabled="True" TargetControlID="txtYear" Format="yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                    <asp:Calendar ID="calendar" runat="server" BackColor="White" Font-Names="Roboto" Font-Size="12px" ForeColor="#5b5b5b" FirstDayOfWeek="Monday" Width="220px" OnDayRender="calendar_DayRender" OnSelectionChanged="calendar_SelectionChanged">
                                                        <NextPrevStyle Font-Size="9pt" ForeColor="#f0f0f0"  />
                                                        <TodayDayStyle BorderColor="#466522" BorderStyle="Solid" BorderWidth="1px" />
                                                        <TitleStyle Font-Bold="True" Font-Size="9pt" ForeColor="#f0f0f0" CssClass="caltitle"/>
                                                    </asp:Calendar>
                                                    <!--Month Calendar Start-->
                                                    <uc1:WCMonthlyCalendar runat="server" ID="WCMonthCalendar" Visible="false" />
                                                    <!--Month Calendar - End-->

                                                    <!--Year Calendar Start-->
                                                    <uc1:WCYearlyCalendar runat="server" ID="WCYearCalendar" Visible="false" />
                                                    <!--Year Calendar - End-->
                                                    <br />
                                                    <a class="btn btn-primary pagebutton" href="reports.aspx">Reset</a>
                                                </td>

                                                
                                                <td class="reportdata">
                                                    <div style="float:right;margin:5px;">
                                                    <a id="lnkbtnBack" runat="server" class="btn btn-primary pull-right pagebutton">Back</a></div>
                                                    <table class="reportheader">
                                                        <tr>
                                                            <td align="left" width="30%">
                                                                <asp:Label ID="lblReportTitle" runat="server" Text=""></asp:Label></td>
                                                            <td align="left" width="30%">
                                                                <asp:Label ID="lblType" runat="server" Text=""></asp:Label></td>
                                                            <td align="right">
                                                                <asp:Label ID="lblDate" runat="server" Text=""></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                    <asp:Literal ID="ltrReports" runat="server" Text=""></asp:Literal>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <!--Download Content - End -->
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <!--Content - End-->
            <!--Footer - Start-->
            <uc1:WCFooter runat="server" ID="WCFooter" />
            <!--Footer - End-->
        </div>
    </form>
</body>
</html>
