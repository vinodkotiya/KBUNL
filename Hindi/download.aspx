<%@ Page Language="C#" AutoEventWireup="true" CodeFile="download.aspx.cs" Inherits="download" %>

<%@ Register Src="~/Hindi/WCCommon/WcHeadTags.ascx" TagPrefix="uc1" TagName="WcHeadTags" %>
<%@ Register Src="~/Hindi/WCCommon/WCTopBar.ascx" TagPrefix="uc1" TagName="WCTopBar" %>
<%@ Register Src="~/Hindi/WCCommon/WCFooter.ascx" TagPrefix="uc1" TagName="WCFooter" %>
<%@ Register Src="~/Hindi/WCCommon/WCEmployeeServices.ascx" TagPrefix="uc1" TagName="WCEmpServices" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,initial-scale=1">
    <title>WR2HQ -Downloads</title>
    <!--Header - Start-->
    <uc1:WcHeadTags runat="server" ID="WcHeadTags" />
    <!--Header - End-->
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
                            <!-- Banner Start -->
                            <div class="innerbanner">
                                <img src="../images/banner-downloads.jpg" alt="" />
                                <div class="bannerpatch">
                                    <h4>डाउनलोड</h4>
                                    <div class="breadcrumb">
                                        <a href="default.aspx" class="home">होम</a>
                                        <a class="active">डाउनलोड</a>
                                    </div>
                                </div>
                            </div>
                            <!-- Banner End -->
                            <!--Download Content - Start -->
                            <asp:UpdatePanel ID="updatepnl" runat="server">
                                <ContentTemplate>
                                    <asp:HiddenField ID="hdfCategoryId" runat="server" Value="0" />
                                    <div class="downloadcontent">
                                        <table>
                                            <tr><td colspan="2"><div id="divMsg" runat="server" visible="false"> <asp:Label ID="lblMsg" runat="server"></asp:Label></div></td></tr>
                                            <tr>
                                                <td class="contentleft">
                                                    <asp:GridView ID="grdviewCategory"  ShowHeader="False" runat="server" OnRowDataBound="grdviewCategory_rowDataBound" AutoGenerateColumns="false" GridLines="None" >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="" >
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdfDownloadCategoryId" runat="server" Value='<%#Eval("DownloadCategoryId")%>' />
                                                                    <asp:LinkButton id="lnkbtnCategory" runat="server" Text='<%#Eval("DownloadCategoryHindi")%>' OnClick="lnkbtnCategory_Click"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                                <td class="contentright">
                                                    <asp:Literal ID="ltrDownloads" runat="server"></asp:Literal>
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
