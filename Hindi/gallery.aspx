<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gallery.aspx.cs" Inherits="gallery" %>
<%@ Register Src="~/Hindi/WCCommon/WcHeadTags.ascx" TagPrefix="uc1" TagName="WcHeadTags" %>
<%@ Register Src="~/Hindi/WCCommon/WCTopBar.ascx" TagPrefix="uc1" TagName="WCTopBar" %>
<%@ Register Src="~/Hindi/WCCommon/WCFooter.ascx" TagPrefix="uc1" TagName="WCFooter" %>
<%@ Register Src="~/Hindi/WCCommon/WCEmployeeServices.ascx" TagPrefix="uc1" TagName="WCEmpServices" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,initial-scale=1">
    <title>WR2HQ - Gallery</title>
    <!--Header - Start-->
    <uc1:WcHeadTags runat="server" ID="WcHeadTags" />
    <!--Header - End-->
    <link href="../css/visuallightbox.css" rel="stylesheet" type="text/css" />
    <link href="../css/vlightbox.css" rel="stylesheet" type="text/css" />
    <style>
        .cont a {
        }

        .cont .lens {
            display: none;
            position: absolute;
            top: 11px;
            bottom: 10px;
            right: 10px;
            left: 10px;
            width: 95%;
            height: 92%;
            text-align: center;
            padding-top: 40%;
            padding-bottom: 40%;
            background-color: rgba(233, 219, 219, 0.6);
        }

        .cont:hover .lens {
            display: block;
            transition: all 0.8s ease-in-out;
            -webkit-transition: all 0.8s ease-in-out;
        }
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
                            <asp:HiddenField ID="hdfDeptId" runat="server" Value="0" />
                            <!-- Banner Start -->
                            <div class="innerbanner">
                                <img src="../images/banner-gallery.jpg" alt="" />
                                <div class="bannerpatch">
                                    <h4>गैलरी</h4>
                                    <div class="breadcrumb">
                                        <a href="default.aspx" class="home">होम</a>
                                        <a class="active">गैलरी</a>
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

                                    <div class="photogallery">
                                        <asp:DataList ID="DlistAlbum" runat="server" Width="100%" OnItemDataBound="DlistAlbum_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="powW">
                                                    <div class="album-detail wow fadeIn">
                                                        <div class="albumname">
                                                            <p><%# Eval("AlubumName")%><a href="gallery-details-view.aspx?AId=<%#Eval("AlbumID") %>&AName=<%# Eval("AlubumName")%>">सभी देखें</a></p>
                                                        </div>
                                                            <asp:Label ID="lblAlbumID" Visible="false" runat="server" Text='<%#Eval("AlbumID") %>'></asp:Label>
                                                            <asp:DataList ID="DlistAlbumPhoto" Style="border-collapse: separate; margin-bottom: 30px;" CssClass="pow-pics" runat="server" Width="100%" RepeatDirection="Horizontal" RepeatColumns="4">
                                                            <ItemTemplate>
                                                                <div class="cont box" style="position: relative;">
                                                                    <a href='<%# Eval("PhotoPath")%>' runat="server" class="vlightbox" style="position: relative; display:block;">
                                                                        <div class="imgbox">
                                                                        <img class="pic-url" src='<%# Eval("PhotoThumbnailPath")%>'/></div>
                                                                        <div class="lens" style="width:100%; height:100%; top:0;left:0; right:auto;bottom:auto;padding-top:30%; padding-bottom:0;">
                                                                            <img src="../images/zoom.png" />
                                                                        </div>
                                                                    </a>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
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

    <script src="../js/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        var jQuery_1_4_2 = jQuery.noConflict();
    </script>

    <script type="text/javascript">
        var $VisualLightBoxParams$ = { autoPlay: true, borderSize: 21, enableSlideshow: true, overlayOpacity: 0.4, startZoom: true };
    </script>
    <script src="../js/visuallightbox.js" type="text/javascript"></script>
</body>
</html>
