<%@ Page Language="C#" AutoEventWireup="true" CodeFile="directory-upcoming-superannuation.aspx.cs" Inherits="directory" %>

<%@ Register Src="~/English/WCCommon/WcHeadTags.ascx" TagPrefix="uc1" TagName="WcHeadTags" %>
<%@ Register Src="~/English/WCCommon/WCTopBar.ascx" TagPrefix="uc1" TagName="WCTopBar" %>
<%@ Register Src="~/English/WCCommon/WCFooter.ascx" TagPrefix="uc1" TagName="WCFooter" %>
<%@ Register Src="~/English/WCCommon/WCEmployeeServices.ascx" TagPrefix="uc1" TagName="WCEmpServices" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--Header - Start-->
    <uc1:WcHeadTags runat="server" ID="WcHeadTags" />
    <!--Header - End-->
    <style type="text/css">
        label { font-size: 12px !important; font-weight: 600; }
        .gridview{font-size:12px; border:1px solid #dbdbdb; border-collapse:collapse;}
        .gridview th{font-size:12px; border:1px solid #dbdbdb; border-collapse:collapse;}
        .gridview td{font-size:12px; border:1px solid #dbdbdb; border-collapse:collapse;}

        .gridviewpaging table{width:auto;}
        .gridviewpaging td{border:none; }
        .directorycontent td{border-bottom:none;}
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
                                <img src="../images/banner-directory.jpg" alt="" />
                                <div class="bannerpatch">
                                    <h4 style="line-height:24px;">Upcoming <br />Superannuation List</h4>
                                    <div class="breadcrumb">
                                        <a href="Default.aspx" class="home">Home</a>
                                        <a class="active">Upcoming Superannuation List</a>
                                    </div>
                                    <h5 style="padding-top:5px;"><a href="important-contacts.aspx" class="home">Important Contacts</a></h5>
                                </div>
                            </div>
                            <!--Inner Banner - End -->

                            <!--About Department - Start -->
                            <asp:UpdatePanel ID="updatepnl" runat="server">
                                <ContentTemplate>
                                    <div class="directorycontent" style="margin-top: 30px; background-color:#ffffff;">
                                        <asp:HiddenField ID="hfsearch" runat="server" />
                                        <table>
                                            <tr>
                                                <td style="border:none;">
                                                    <div class="direcrorysearch">
                                                        <table style="width:auto;">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtDirectorySearch" runat="server" placeholder="Search"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFromDate" runat="server" placeholder="From Date"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="cefd" runat="server" TargetControlID="txtFromDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtToDate" runat="server" placeholder="To Date"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="cetd" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnDirectorySearch" runat="server" Text="Search" OnClick="btnDirectorySearch_Click" CssClass="btn" />
                                                                </td>
                                                            </tr>
                                                        </table>						                        
							                        </div>
                                                </td>
                                                <td style="text-align:right; border:none; width:100px;">
                                                    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="../images/icons/excel.png" Width="30" ToolTip="Export to Excel" OnClick="btnExport_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:GridView ID="gridDirectory" EmptyDataText="No Record Found" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="50" CssClass="gridview" PagerStyle-CssClass="gridviewpaging" OnPageIndexChanging="gridDirectory_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Photo" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <img src='../<%#Eval("PhotoPath") %>' alt="" width="100" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EmpNo">
                                                    <ItemTemplate>
                                                        <%#Eval("EmpCode") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <%#Eval("EmpNameHindi") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department">
                                                    <ItemTemplate>
                                                        <%#Eval("DepartmentH") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <%#Eval("DesignationH") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Level">
                                                    <ItemTemplate>
                                                        <%#Eval("Level") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email">
                                                    <ItemTemplate>
                                                        <%#Eval("EmailID") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile<br/>Alt.Mob">
                                                    <ItemTemplate>
                                                        <%#Eval("Mobile") %><br />
                                                        <%#Eval("AlternateMobileNumber") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Intercom">
                                                    <ItemTemplate>
                                                        <%#Eval("IntercomOffice") %><br />
                                                        <%#Eval("IntercomResidence") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date of Retirement">
                                                    <ItemTemplate>
                                                        <%#Eval("DOR") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnExport" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <!--About Department - End -->
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
