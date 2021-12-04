<%@ Page Language="C#" AutoEventWireup="true" CodeFile="opinion-poll.aspx.cs" Inherits="English_opinion_poll" %>

<%@ Register Src="~/Hindi/WCCommon/WcHeadTags.ascx" TagPrefix="uc1" TagName="WcHeadTags" %>
<%@ Register Src="~/Hindi/WCCommon/WCTopBar.ascx" TagPrefix="uc1" TagName="WCTopBar" %>
<%@ Register Src="~/Hindi/WCCommon/WCFooter.ascx" TagPrefix="uc1" TagName="WCFooter" %>
<%@ Register Src="~/Hindi/WCCommon/WCEmployeeServices.ascx" TagPrefix="uc1" TagName="WCEmpServices" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                            <!--Inner Banner - Start -->
                            <div class="innerbanner">
                                 <img src="../images/banner-opinion.jpg" alt="" />
                                <div class="bannerpatch">
                                    <h4>जनमत सर्वेक्षण</h4>
                                    <div class="breadcrumb">
                                        <a href="Default.aspx" class="home">होम</a>
                                        <a class="active">जनमत सर्वेक्षण</a>
                                    </div>
                                </div>
                            </div>
                            <!--Inner Banner - End -->
                            <!--About Department - Start -->
                           <asp:UpdatePanel ID="updatepnl" runat="server">
                                <ContentTemplate>
                                    <div class="opinionpollcontainer">
                                        <div class="opinionpollcontent">
                                            <asp:HiddenField ID="hdfOpinioPollId" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdfIPAddress" runat="server" Value="0" />
                                             <p><asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></p>
                                            <asp:Panel ID="pnlnotavailable" runat="server" Visible="false" CssClass="notavailable">
                                                <span>
                                                    वर्तमान में कोई राय सर्वेक्षण उपलब्ध नहीं है।<br/>
                                                    कृपया बाद में देखें।
                                                </span>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlVoting" runat="server">
                                                <div class="polloptions">
                                                <asp:RadioButtonList runat="server" ID="rbtOptions">
                                                </asp:RadioButtonList>
                                                <asp:LinkButton ID="lnkbtnSubmitVote" runat="server" Text="Submit" OnClick="btnSubmitVote_Click"></asp:LinkButton>
                                                </div>
                                            </asp:Panel>
                                             <asp:Panel ID="pnlSummary" runat="server" Visible="false">
                                                 <div class="opinionpollchart" id="chart" runat="server">
                                                 </div>
                                                 <p style="color:#8faf5c;font-size:15px;text-align:center;" id="pnlAlready" runat="server" visible="false">You have already voted for this opinion poll.</p>
                                            </asp:Panel>
                                            <div style="text-align: center;">
                                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
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
