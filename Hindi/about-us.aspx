<%@ Page Language="C#" AutoEventWireup="true" CodeFile="about-us.aspx.cs" Inherits="about_us" %>

<%@ Register Src="~/Hindi/WCCommon/WcHeadTags.ascx" TagPrefix="uc1" TagName="WcHeadTags" %>
<%@ Register Src="~/Hindi/WCCommon/WCTopBar.ascx" TagPrefix="uc1" TagName="WCTopBar" %>
<%@ Register Src="~/Hindi/WCCommon/WCFooter.ascx" TagPrefix="uc1" TagName="WCFooter" %>
<%@ Register Src="~/Hindi/WCCommon/WCEmployeeServices.ascx" TagPrefix="uc1" TagName="WCEmpServices" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" href="favicon.ico">
    <title>About Us</title>
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
                                <img src="../images/banner-about.jpg" alt="" />
                                <div class="bannerpatch">
                                    <h4>हमारे बारे में</h4>
                                    <div class="breadcrumb">
                                        <a href="default.aspx" class="home">होम</a>
                                        <a class="active">हमारे बारे में</a>
                                    </div>
                                </div>
                            </div>
                            <!-- Banner End -->
                            <!--About Us Content - Start -->
                            <asp:UpdatePanel ID="updatepnl" runat="server">
                                <ContentTemplate>
                                    <div class="aboutuscontent">                                       
                                        <h4>हमारे बारे में</h4>
                                        <div class="aboutdepartment">
                                            <p>
                                                <asp:Label ID="lblProjectHistory" runat="server" Text=""></asp:Label>
                                            </p>
                                            <asp:Image ID="image1" style="width:300px; height: 190px;" runat="server" ImageUrl="~/images/default-photo.png"/>
                                            <asp:Image ID="image2" style="width:300px; height: 190px;" runat="server" ImageUrl="~/images/default-photo.png"/>
                                            <asp:Image ID="image3" style="width:300px; height: 190px;" runat="server" ImageUrl="~/images/default-photo.png"/>
                                        </div>                                        

                                        <h4>स्थान</h4>
                                        <div class="projectlocation">
                                            <table cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td class="locationdata">
                                                        <p>
                                                            <img src="../images/address.png" alt="" />
                                                            <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></p>
                                                        <p>
                                                            <img src="../images/flite.png" alt="" />
                                                            <asp:Label ID="lblFlite" runat="server" Text=""></asp:Label></p>
                                                        <p>
                                                            <img src="../images/highway.png" alt="" />
                                                            <asp:Label ID="lblHigWay" runat="server" Text=""></asp:Label></p>
                                                        <p>
                                                            <img src="../images/train.png" alt="" />
                                                            <asp:Label ID="lblTrain" runat="server" Text=""></asp:Label></p>
                                                    </td>
                                                    <!-- <td class="locationmap">
                                                        <div id="divmap" runat="server"></div>
                                                       
                                                    </td>-->
                                                    <td class="locationmap">
                                                        <div class="mapouter"><div class="gmap_canvas"><iframe width="600" height="500" id="gmap_canvas" src="https://maps.google.com/maps?q=kanti%20thermal&t=&z=13&ie=UTF8&iwloc=&output=embed" frameborder="0" scrolling="no" marginheight="0" marginwidth="0"></iframe><br><style>.mapouter{position:relative;text-align:right;height:500px;width:600px;}</style><style>.gmap_canvas {overflow:hidden;background:none!important;height:500px;width:600px;}</style></div></div>
                                                    </td>
                                                </tr>
                                            </table>

                                        </div>
                                    </div>
                                    <!--About Us Content - End -->
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
