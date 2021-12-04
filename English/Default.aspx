<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/English/WCCommon/WcHeadTags.ascx" TagPrefix="uc1" TagName="WcHeadTags" %>
<%@ Register Src="~/English/WCCommon/WCTopBar.ascx" TagPrefix="uc1" TagName="WCTopBar" %>
<%@ Register Src="~/English/WCCommon/WCFooter.ascx" TagPrefix="uc1" TagName="WCFooter" %>
<%@ Register Src="~/English/WCCommon/WCEmployeeServices.ascx" TagPrefix="uc1" TagName="WCEmpServices" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="refresh" content="900">
    <meta name="viewport" content="width=device-width,initial-scale=1">
    <link rel="icon" href="favicon.ico">
    <title></title>
    <!--Header - Start-->
    <uc1:WcHeadTags runat="server" ID="WcHeadTags" />
    <!--Header - End-->
	<script src="scripts/confetti.js"></script> 
  
</head>
<body>
 <!--<script>confetti.start(5000, 200)</script>-->
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptmgr" runat="server"></asp:ScriptManager>
        <div class="pagecontainer">
            <!--MenuBar - Start-->
            <uc1:WCTopBar runat="server" ID="WCTopBar1" />
            <!--MenuBar - End-->

            <!--Content - Start-->
            <div class="content-outer">
                <asp:HiddenField ID="hdf_IPAddress" runat="server" />
                <table>
                    <tr>
                        <td class="contentbox1">
                            <!-- Employee Services - Start -->
                            <uc1:WCEmpServices runat="server" ID="WCEmpServices" />
                            <!-- Employee Services - End -->
                            <!--Department Update-->
                            <div class="thoughtbox">
                                <table>
                                    <tr>
                                        <td>
                                            <img src="../images/update.png" width="56px" heigh="54px" alt="" />
                                        </td>
                                        <td>
                                            <h5>Department Updates</h5>
                                        </td>
                                    </tr>
                                </table>
                                <div id="divDeptUpdate" style="font: 400 14px 'Calibri', 'Arial' ;  " runat="server" />
                            </div>
                            <!--Department Update-->
							<!--Vision, Mission & Core Values-->
                            <div class="vmc" style="max-height: 260px; margin-bottom: 15px;">
                                <div class="bottomslider">
                                    <section class="demo_wrapper">
			                            <article class="demo_block">
			                                <ul id="divVMCBox" style="margin-top:0px;">
				                                <li><a href="#slide1"><span>Vision </span> To be the world's leading power company, energizing India's growth.</a></li>
				                                <li><a href="#slide2"><span>Mission </span> Provide reliable power and related solutions in an economical, efficient and environment friendly manner, driven by innovation and agility.</a></li>
				                                <li>
                                                    <a href="#slide3"><span>Core Values </span> </a>
                                                    <p><span class="blue">I</span>ntegrity</p>
                                                    <p><span class="blue">C</span>ustomer Focus</p>
                                                    <p><span class="blue">O</span>rganizational Pride</p>
                                                    <p><span class="blue">M</span>utual Respect and Trust</p>
                                                    <p><span class="blue">I</span>nnovation and Speed</p>
                                                    <p><span class="blue">T</span>otal quaility and safety</p>
				                                </li>
			                                </ul>
			                            </article>
		                            </section>
                                </div>
                            </div>
                            <!--Vision, Mission & Core Values End-->

                            <!--Thoughts of the day & Hindi Word Start-->
                            <div class="thoughtbox">
                                <table>
                                    <tr>
                                        <td>
                                            <img src="../images/Thought-of-the-day.png" alt="" />
                                        </td>
                                        <td>
                                            <h5>Thought of the Day</h5>
                                        </td>
                                    </tr>
                                </table>
                                <p id="divThought" runat="server" class="thought"></p>
                                <div class="hindiword">Hindi Word of the Day</div>
                                <div class="hwe" id="divHWE" runat="server"></div>
                                <div class="hwh" id="divHWH" runat="server"></div>
                            </div>
                            <!--Thoughts of the day & Hindi Word End-->
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td class="contentbox2" style="height:420px ! important;">
                                        <!-- Banner Start -->
                                       <div class="banner" id="ntpcbanner">
                                            <ul class="slider" style="height:420px ! important;">
                                                <% if (dtBanner != null && dtBanner.Rows.Count > 0)
                                                    {
                                                        foreach (System.Data.DataRow row in dtBanner.Rows)
                                                        {%>
                                                            <li><img height="420px" src="<%=row["BannerPath"] %>" title="<%=row["BannerText"] %>"" /></li>
                                                       <%}
                                                    }
                                                 %>
                                            </ul>
                                        </div>
                                                
                                       <!--   <div id="slider"> 
                                            <% if (dtBanner != null && dtBanner.Rows.Count > 0)
                                                    {
                                                        foreach (System.Data.DataRow row in dtBanner.Rows)
                                                        {%>
                                                            <div class="slides">  <img width="100%" height="420px"  src="<%=row["BannerPath"] %>" title="<%=row["BannerText"] %>"" /></div>
                                                       <%}
                                                    }
                                                 %>
                                                 <div id="dot"><span class="dot"></span><span class="dot"></span><span class="dot"></span><span class="dot"></span><span class="dot"></span></div>
                                        </div>-->
                                        <!-- Banner End -->

                                       
                                    </td>
                                    <td class="contentbox3">
                                        <!--HOP Message Box Start-->

                                        <div class="vmc" style="height:520px;">
                                            <div class="bottomslider">
                                                <section class="demo_wrapper">
                                                    <article class="demo_block">
                                                        <ul id="divmessagebox" runat="server" style="margin-top: 0px;">
                                                        </ul>
                                                    </article>
                                                    <div><q></q></div>
                                                </section>
                                            </div>
                                        </div>







                                        <!--HOP Message Box End-->
                                    </td>
                                </tr>
                            </table>

                            <table style="width: 100%;">
                                <tr>
                                    <td class="contentbox2">
                                         <!-- Announcement Start -->
                                         <div class="home-announcement">
                                            <div id="divAnnouncements" runat="server"></div>
                                        </div>
                                        <!-- Announcement End -->
                                        <div style="height: 135px; border: 1px solid #cbcbcb; background-color: #ffffff; margin-bottom: 5px; border-radius: 5px;  overflow: hidden; text-align: left; margin-bottom:10px;">
                                            
                                            <div>
                                                <iframe class="resp-iframe1" src="http://10.1.168.1/livedata1.aspx" gesture="media"  allow="encrypted-media" scrolling="no" border="0" width="100%" height="100%" frameborder="0" align="left" style="overflow-y:auto;overflow-x:hidden;"></iframe>
                                            </div>											
                                        </div>

                                        <!-- Updates - Start -->
                                        <asp:UpdatePanel ID="updatepanel2" runat="server">
                                            <ContentTemplate>
                                                <div class="updates-container">
                                                    <table class="filter">
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="linkInformation" runat="server" OnClick="show_information">Information</asp:LinkButton></td>
                                                            <td>
                                                                <asp:LinkButton ID="linkCirculars" runat="server" OnClick="show_circulars">Circulars</asp:LinkButton></td>
                                                            <td>
                                                                <asp:LinkButton ID="linkEvents" runat="server" OnClick="show_events">Events & Photo Gallery</asp:LinkButton></td>
                                                            <%--<td>
                                                                <asp:LinkButton ID="linkNotices" runat="server" OnClick="show_notices">Notices</asp:LinkButton></td>
                                                            <td>
                                                                <asp:LinkButton ID="linkNews" runat="server" OnClick="show_news">News</asp:LinkButton></td>
															--%>
                                                            <td>
                                                                <asp:LinkButton ID="linkGGMSchedule" runat="server" OnClick="show_ggmschedule">Calendar</asp:LinkButton></td>
                                                        </tr>
                                                    </table>
                                                    <div class="updates-content">
                                                        <div id="divInformation" runat="server">
                                                            <asp:Literal ID="ltrInformation" runat="server"></asp:Literal>
                                                        </div>
                                                        <div id="divCirculars" runat="server">
                                                            <asp:Literal ID="ltrCircular" runat="server"></asp:Literal>
                                                        </div>
                                                        <div id="divEvents" runat="server">
                                                            <div style="display: inline-block;">
                                                                <h5><a href="gallery.aspx" style="border-radius: 15px 50px;padding-left: 10px;padding-right: 10px;color: #f7f7f7;font-weight: 600;background-color: #6e94b9;">Photo Gallery (ViewAll)</a></h5>
                                                                <br/>
                                                            </div>
                                                            <asp:Literal ID="ltrEvents" runat="server"></asp:Literal>
                                                        </div>
                                                        <div id="divNotices" runat="server">
                                                            <asp:Literal ID="ltrNotices" runat="server"></asp:Literal>
                                                        </div>
                                                        <div id="divNews" runat="server">
                                                            <asp:Literal ID="ltrNews" runat="server"></asp:Literal>
                                                        </div>
														
                                                        <div id="divGGMSchedule" runat="server">
                                                            <table>
                                                                <tr>
                                                                    <td style="padding-left: 20px; padding-right: 20px;">
                                                                        <asp:Calendar ID="calendar_Schedule" runat="server" OnDayRender="ScheduleRenderer" ToolTip=" " UseAccessibleHeader="false">
                                                                            <DayStyle CssClass="myCalendarDay" />
                                                                            <NextPrevStyle CssClass="myCalendarNextPrev" />
                                                                            <TitleStyle CssClass="myCalendarTitle" />
                                                                            <DayHeaderStyle CssClass="myCalendarDayHeader" />
                                                                        </asp:Calendar>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:Literal ID="ltrSchedule" runat="server"></asp:Literal>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <!-- Updates - End -->
                                    </td>
                                    <td class="contentbox3">



                                        <!-- Birthdays & Superannuation - Start-->
                                        <asp:UpdatePanel ID="updatepanel1" style="margin-top: 10px;" runat="server">
                                            <ContentTemplate>
                                                <div class="birthday-container">
                                                    <table class="filter" style="width: 100%;" >
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="linkBirthdays" style="width: 100%;" runat="server" OnClick="show_birthdays">B'Day</asp:LinkButton></td>
                                                            <td>
                                                                <asp:LinkButton ID="linkSuperannuation" runat="server" OnClick="show_superannuations">Superannuation</asp:LinkButton></td>
                                                            <td>
                                                                <asp:LinkButton ID="linkWelcome" runat="server" OnClick="show_welcomes" Style="border-right: 0;">Welcome</asp:LinkButton></td>
                                                        </tr>
                                                    </table>
                                                    <div class="updates-content">
                                                        <div id="divBirthdays" runat="server" >
                                                        </div>
                                                        <div id="divSuperannuation" runat="server">
                                                        </div>
                                                        <div id="divWelcomes" runat="server">
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <!-- Birthdays & Superannuation - Start-->

                                        <!-- Schedule and Hall of Fame -- Start-->
                                        <asp:UpdatePanel ID="updatepanel3" runat="server">
                                            <ContentTemplate>
                                                <div class="schedule-container">
                                                    <table class="filter">
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="linkHalloffame" runat="server" OnClick="linkHalloffame_Click">Hall of the Fame</asp:LinkButton></td>
                                                            <td>
                                                                <asp:LinkButton ID="linkSafetyTrining" runat="server" OnClick="linkSafetyTrining_Click">Safety Training</asp:LinkButton></td>
                                                            <td>
                                                                <asp:LinkButton ID="linkMedicalExam" runat="server" OnClick="linkMedicalExam_Click" Style="border-right: 0;">Medical Examination</asp:LinkButton></td>
                                                        </tr>
                                                    </table>
                                                    <div class="updates-content" style="height: 250px;">
                                                        <div id="divHalloffame" runat="server">
                                                        </div>
                                                        <div id="divSafetyTrining" runat="server">
                                                        </div>
                                                        <div id="divMedicalExam" runat="server">
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <!-- Schedule and Hall of Fame - End-->
                                        <!-- Weather -- Start-->
                                        <div class="thoughtbox">
                                            
                                            <a class="weatherwidget-io" href="https://forecast7.com/en/26d2185d29/kanti/" data-label_1="KBUNL" data-label_2="WEATHER" data-theme="clear" >KBUNL WEATHER</a>
                                            <script>
                                            !function(d,s,id){var js,fjs=d.getElementsByTagName(s)[0];if(!d.getElementById(id)){js=d.createElement(s);js.id=id;js.src='https://weatherwidget.io/js/widget.min.js';fjs.parentNode.insertBefore(js,fjs);}}(document,'script','weatherwidget-io-js');
                                            </script>
                                        </div>
                                       <!-- Weather -- End-->

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <!--Content - End-->
            <!--Footer - Start-->
            <uc1:WCFooter runat="server" ID="WCFooter" />
            <!--Footer - End-->
        </div>

        <!--Popup Start-->
        <div class="popupcontainer" id="divPopupContainer" runat="server">
        </div>
        <!--Popup End-->
    </form>

    <!--vision-mission-slider-->
    <script src="../js/slippery.min.js"></script>
    <link href="../css/slippery.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            var demo2 = $("#divmessagebox").slippry({
                transition: 'fade',
                useCSS: true,
                speed: 2000,
                auto: true,
                autoHover: true,
            });

            var vmc = $("#divVMCBox").slippry({
                transition: 'fade',
                useCSS: true,
                speed: 2000,
                auto: true,
                autoHover: true,
            });
        });

        function closepopup() {
            document.getElementById('divPopupContainer').style.display = "none";
        }
    </script>
  
</body>
</html>
