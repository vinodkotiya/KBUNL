<%@ Page Language="C#" AutoEventWireup="true" CodeFile="search.aspx.cs" Inherits="directory" %>
<%@ Register Src="~/Hindi/WCCommon/WcHeadTags.ascx" TagPrefix="uc1" TagName="WcHeadTags" %>
<%@ Register Src="~/Hindi/WCCommon/WCTopBar.ascx" TagPrefix="uc1" TagName="WCTopBar" %>
<%@ Register Src="~/Hindi/WCCommon/WCFooter.ascx" TagPrefix="uc1" TagName="WCFooter" %>
<%@ Register Src="~/Hindi/WCCommon/WCEmployeeServices.ascx" TagPrefix="uc1" TagName="WCEmpServices" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>आर्काइव</title>
    <!--Header - Start-->
    <uc1:WcHeadTags runat="server" ID="WcHeadTags" />
    <!--Header - End-->

    <style type="text/css">
        .directorycontent td.file .clinks a{color:#6b6b6b; font-size:12px; margin-right:10px; transition:0.3s;}
        .directorycontent td.file .clinks a:hover{color:#000000;}
    </style>
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
                            <asp:HiddenField ID="hdfDeptId" runat="server" Value="2" />
                            <!--Inner Banner - Start -->
                            <div class="innerbanner">
                                <img src="../images/banner-reports.jpg" alt="" />
                                <div class="bannerpatch">
                                    <h4>आर्काइव</h4>
                                    <div class="breadcrumb">
                                        <a href="default.aspx" class="home">होम</a>
                                        <a class="active">आर्काइव</a>
                                    </div>
                                </div>
                            </div>
                            <!--Inner Banner - End -->

                            <asp:UpdatePanel ID="updatepnl" runat="server">
                                <ContentTemplate>
                                    <div class="directorycontainer">
                                        <div class="filter">
                                            <table class="search">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDepartment" CssClass="dropdown" runat="server" Width="150px"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDocType" CssClass="dropdown" runat="server" Width="150px">
                                                            <asp:ListItem Value="All" Text="सभी प्रकार"></asp:ListItem>
                                                            <asp:ListItem Value="Announcement" Text="एनाउंसमेंट्स"></asp:ListItem>
                                                            <asp:ListItem Value="Circulars" Text="सर्कुलर, समाचार, आयोजन"></asp:ListItem>
                                                            <asp:ListItem Value="Documents" Text="डाक्यूमेंट्स"></asp:ListItem>
                                                            <asp:ListItem Value="Downloads" Text="डाउनलोड"></asp:ListItem>
                                                            <%--<asp:ListItem Value="Notices" Text="नोटिस"></asp:ListItem>--%>
                                                            <asp:ListItem Value="Reports" Text="रिपोर्ट्स"></asp:ListItem>
                                                            <%--<asp:ListItem Value="News" Text="न्यूज़"></asp:ListItem>
                                                            <asp:ListItem Value="Events" Text="इवेंट्स"></asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtname" CssClass="txtbox" runat="server" Width="150px" placeholder="फाइल/डॉक्यूमेंट का नाम"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDateFrom" CssClass="txtbox" runat="server" Width="150px" placeholder="दिनांक से"></asp:TextBox>
                                                        <asp:CalendarExtender ID="calendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateFrom"></asp:CalendarExtender>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDateTo" CssClass="txtbox" runat="server" Width="150px" placeholder="दिनांक तक"></asp:TextBox>
                                                        <asp:CalendarExtender ID="calendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateTo"></asp:CalendarExtender>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnSearch" runat="server" Text="सर्च करें" CssClass="pagebutton" OnClick="btnSearch_Click" style="padding:10px 20px; margin-top:0px;"/>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="directorycontent">                                    
                                            <div id="divsearchresults" runat="server">

                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="100">
                                <ProgressTemplate>
                                    <asp:Panel ID="Pnl1" CssClass="overlay" runat="server" meta:resourcekey="Pnl1Resource1">
                                        <asp:Panel ID="Pnl2" CssClass="loader" runat="server" meta:resourcekey="Pnl2Resource1">
                                            <img alt="" src="../images/p1.gif" />
                                        </asp:Panel>
                                    </asp:Panel>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
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
