<%@ Page Language="C#" AutoEventWireup="true" CodeFile="employee-welfare.aspx.cs" Inherits="Hindi_employee_welfare" %>
<%@ Register Src="~/Hindi/WCCommon/WcHeadTags.ascx" TagPrefix="uc1" TagName="WcHeadTags" %>
<%@ Register Src="~/Hindi/WCCommon/WCTopBar.ascx" TagPrefix="uc1" TagName="WCTopBar" %>
<%@ Register Src="~/Hindi/WCCommon/WCFooter.ascx" TagPrefix="uc1" TagName="WCFooter" %>
<%@ Register Src="~/Hindi/WCCommon/WCEmployeeServices.ascx" TagPrefix="uc1" TagName="WCEmpServices" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,initial-scale=1">
    <title>WR2HQ - Department Home</title>
    <!--Header - Start-->
    <uc1:WcHeadTags runat="server" ID="WcHeadTags" />
    <!--Header - End-->
    <!-- OWL Corousel - Start -->
    <link href="../js/owl_corousel/owl.carousel.css" rel="stylesheet" type="text/css" />
    <link href="../js/owl_corousel/owl.theme.css" rel="stylesheet" type="text/css" />
    <script src="../js/owl_corousel/owl.carousel.js" type="text/javascript"></script>
    <link href="../js/owl_corousel/bootstrapTheme.css" rel="stylesheet" type="text/css" />
    <!-- OWL Corousel - End -->

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
                <asp:UpdatePanel ID="updatepnl" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="contentbox1">
                                    <!-- Employee Services - Start -->
                                    <uc1:WCEmpServices runat="server" ID="WCEmpServices" />
                                    <!-- Employee Services - End -->
                                </td>
                                <td class="contentboxinner">
                                    <asp:HiddenField ID="hdfDeptId" runat="server" Value="29" />
                                    <!--Inner Banner - Start -->
                                    <div class="innerbanner">
                                        <img src="../images/banner-finance_department.jpg" alt="" />
                                        <div class="bannerpatch">
                                            <h4>कर्मचारी कल्याण</h4>
                                            <div class="breadcrumb">
                                                <a href="default.aspx" class="home">होम</a>
                                                <a class="active">कर्मचारी कल्याण</a>
                                            </div>
                                        </div>
                                    </div>
                                    <!--Inner Banner - End -->

                                    <!--About Department - Start -->
                                    <div class="aboutdeptcontainer">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="adcol1">
                                                    <div class="aboutdepartment">
                                                        <h4>विभाग के बारे में</h4>
                                                        <div class="adcontent">
                                                            <p>
                                                                <asp:Literal ID="ltrAboutDept" runat="server"></asp:Literal>
                                                            </p>
                                                        </div>
                                                    </div>

                                                    <div class="highlights">
                                                        <h4>विशेषताएँ</h4>
                                                        <div class="hlcontent">
                                                            <asp:Literal ID="ltrHighlight" runat="server"></asp:Literal>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="adcol2">
                                                    <!-- Updates - Start -->
                                                    <div class="updates-container department">
                                                        <table class="filter">
                                                            <tr>
                                                                <td>
                                                                    <a style="border-right:none;" id="linkNews" onclick="show_news()">समाचार & अपडेट</a>
                                                                </td>
                                                            </tr>
                                                            
                                                        </table>
                                                        <div class="updates-content">
                                                            <div id="divNews">
                                                                <asp:Literal ID="ltrNews" runat="server"></asp:Literal>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!-- Updates - End -->
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <!--About Department - End -->

                                    <!--Photo Gallery - Start-->
                                    <div class="deptphotogallery">
                                        <h4>फोटो गैलरी</h4>
                                        <div class="deptgallerybox">
									<div class="gallerycontainer">
										<a class="btn prev" id="prev1"> <img prev" src="../images/owl-pre.png" style="width: 100%;   -webkit-box-shadow: inset 0 1px 0 rgba(255,255,255,.1), 0 1px 0 rgba(255,255,255,.1);     -moz-box-shadow: inset 0 1px 0 rgba(255,255,255,.1), 0 1px 0 rgba(255,255,255,.1);     box-shadow: inset 0 1px 0 rgba(255,255,255,.1), 0 1px 0 rgba(255,255,255,.1);" /> </a>
										<a class="btn next" id="next1"> <img  src="../images/owl-next.png" style="width: 100%;" /> </a>
										<asp:Literal ID="ltrPhotoGallery" runat="server"></asp:Literal>
									</div>
                                             <asp:LinkButton ID="lnkbtnViewAll" style="border:1px solid #6d6d6d;margin-bottom:10px;padding:5px;background-color:#f1f1f2;color:#333333;width:90px;" runat="server" Text="सभी देखें" OnClick="lnkbtnViewAll_Click" ></asp:LinkButton>
								</div>
                                    </div>
                                    <!--Photo Gallery - End-->
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!--Content - End-->

            <!--Footer - Start-->
            <uc1:WCFooter runat="server" ID="WCFooter" />
            <!--Footer - End-->
        </div>


        
        <script>
            $(document).ready(function () {
                var owl = $("#owl-project");
                owl.owlCarousel({
                    items: 3,
                    loop: true,
                    margin: 0,
                    autoplay: true,
                    autoplayTimeout: 1000,
                    autoplayHoverPause: true
                });
                $('.play').on('click', function () {
                    owl.trigger('autoplay.play.owl', [1000])
                })
                $('.stop').on('click', function () {
                    owl.trigger('autoplay.stop.owl')
                })
            });
        </script>


        <script>
            $(document).ready(function () {
                var owl = $("#owl-project");
                owl.owlCarousel({
                    items: 3, //10 items above 1000px browser width
                    itemsDesktop: [1300, 4], //5 items between 1000px and 901px
                    itemsDesktopSmall: [900, 3], // 3 items betweem 900px and 601px
                    itemsTablet: [880, 2], //2 items between 600 and 0;
                    itemsMobile: [440, 1] // itemsMobile disabled - inherit from itemsTablet option

                });

                // Custom Navigation Events
                $(".next").click(function () {
                    owl.trigger('owl.next');
                })
                $(".prev").click(function () {
                    owl.trigger('owl.prev');
                })
                $(".play").click(function () {
                    owl.trigger('owl.play', 1000);
                })
                $(".stop").click(function () {
                    owl.trigger('owl.stop');
                })
            });
        </script>
    </form>
</body>
</html>
