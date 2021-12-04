﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dept-Gallery.aspx.cs" Inherits="_Default" %>
<%@ Register Src="~/English/WCCommon/WcHeadTags.ascx" TagPrefix="uc1" TagName="WcHeadTags" %>
<%@ Register Src="~/English/WCCommon/WCTopBar.ascx" TagPrefix="uc1" TagName="WCTopBar" %>
<%@ Register Src="~/English/WCCommon/WCFooter.ascx" TagPrefix="uc1" TagName="WCFooter" %>
<%@ Register Src="~/English/WCCommon/WCEmployeeServices.ascx" TagPrefix="uc1" TagName="WCEmpServices" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,initial-scale=1">
    <title>WR2HQ - Department Gallery</title>
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
                            <asp:HiddenField ID="hdfDeptId" runat="server" Value="0" />
                            <!-- Banner Start -->
                            <div class="innerbanner">
                                <img src="../images/banner-gallery.jpg" alt="" />
                                <div class="bannerpatch">
                                    <h4>Gallery</h4>
                                    <div class="breadcrumb">
                                        <a href="Default.aspx" class="home">Home</a>
                                        <a class="active">Gallery</a>
                                    </div>
                                </div>
                            </div>
                            <!-- Banner End -->
                            <!--Magazine Content - Start -->
                            <asp:UpdatePanel ID="updatepnl" runat="server">
                                <ContentTemplate>
                                    <div class="magazinecontainer">
                                        <div class="dropdownbox">
                                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="--All--"></asp:ListItem>
                                                <asp:ListItem Value="January" Text="JAN"></asp:ListItem>
                                                <asp:ListItem Value="Feburary" Text="FEB"></asp:ListItem>
                                                <asp:ListItem Value="March" Text="MAR"></asp:ListItem>
                                                <asp:ListItem Value="April" Text="APR"></asp:ListItem>
                                                <asp:ListItem Value="May" Text="MAY"></asp:ListItem>
                                                <asp:ListItem Value="June" Text="JUN"></asp:ListItem>
                                                <asp:ListItem Value="July" Text="JUL"></asp:ListItem>
                                                <asp:ListItem Value="August" Text="AUG"></asp:ListItem>
                                                <asp:ListItem Value="September" Text="SEP"></asp:ListItem>
                                                <asp:ListItem Value="October" Text="OCT"></asp:ListItem>
                                                <asp:ListItem Value="November" Text="NOV"></asp:ListItem>
                                                <asp:ListItem Value="December" Text="DEC"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="magazinecontent">
                                        <asp:Repeater ID="repeaterEmployee" runat="server">
                                            <ItemTemplate>
                                                <div class="box">
                                                    <div class="imgbox">
                                                        <%--<asp:Image ID="empimg" runat="server" ImageUrl='<%#Eval("PhotoPath")%>' />--%>
                                                        <img src='../<%#Eval("PhotoPath")%>' alt="" />
                                                    </div>
                                                    <div class="albumtitle"><%#Eval("AlubumName")%>&nbsp;&nbsp;&nbsp;<%#Eval("DateMonth")%></div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="paging" runat="server" id="paging">
                                        <ul>
                                            <li>
                                                <asp:Repeater ID="dlPaging" runat="server" OnItemCommand="dlPaging_ItemCommand" OnItemDataBound="dlPaging_ItemDataBound">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:LinkButton ID="lnkbtnPaging" runat="server" CommandArgument='<%# Eval("PageIndex") %>' CommandName="Paging" Text='<%# Eval("PageText") %>'></asp:LinkButton>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <li></li>
                                                <li>
                                                    <asp:LinkButton ID="lbtnNext" runat="server" CausesValidation="false" CssClass="next" OnClick="lbtnNext_Click"><i class="ion-chevron-right"></i><i class="ion-chevron-right"></i></asp:LinkButton>
                                                </li>
                                            </li>

                                        </ul>
                                        <div class="border"></div>
                                    </div>
                                    <!--Magazine Content - End -->
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
