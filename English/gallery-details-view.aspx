<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gallery-details-view.aspx.cs" Inherits="English_gallry_details_view" %>

<%@ Register Src="~/English/WCCommon/WcHeadTags.ascx" TagPrefix="uc1" TagName="WcHeadTags" %>
<%@ Register Src="~/English/WCCommon/WCTopBar.ascx" TagPrefix="uc1" TagName="WCTopBar" %>
<%@ Register Src="~/English/WCCommon/WCFooter.ascx" TagPrefix="uc1" TagName="WCFooter" %>
<%@ Register Src="~/English/WCCommon/WCEmployeeServices.ascx" TagPrefix="uc1" TagName="WCEmpServices" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                            
                            <!-- Banner Start -->
                            <div class="innerbanner">
                                <img src="../images/banner-gallery.jpg" alt="" />
                                <div class="bannerpatch">
                                    <h4>Gallery</h4>
                                    <div class="breadcrumb">
                                        <a href="Default.aspx" class="home">Home</a>
                                        <a href="gallery.aspx">Gallery</a>
                                        <a class="active"><asp:Label ID="lblAlbumName" runat="server" Text=""></asp:Label></a>
                                    </div>
                                </div>
                            </div>
                            <!-- Banner End -->
                            <!--Magazine Content - Start -->
                            <asp:UpdatePanel ID="updatepnl" runat="server">
                                <ContentTemplate>
                                     <asp:HiddenField ID="hdfAlbumId" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdfDeptId" runat="server" Value="0" />
                                     <asp:HiddenField ID="hdfAlbumName" runat="server" Value="" />
                                    <div class="photogallery" style="padding:30px 0px; margin-top:20px;">
                                        <div class="powW">
                                            <div class="album-detail wow fadeIn">                                               
                                                <asp:Repeater ID="rptPhoto" runat="server" >
                                                    <ItemTemplate>
                                                        <div class="cont  box" style="position: relative;">
                                                            <a href='<%# Eval("PhotoPath")%>' runat="server" class="vlightbox" style="position: relative; display: block;">
                                                                <div class="imgbox">
                                                                    <img class="pic-url" src='<%# Eval("PhotoThumbnailPath")%>' />
                                                                </div>
                                                                <div class="lens" style="width: 100%; height: 100%; top: 0; left: 0; right: auto; bottom: auto; padding-top:30%; padding-bottom: 0;">
                                                                    <img src="../images/zoom.png" />
                                                                </div>
                                                            </a>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
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
