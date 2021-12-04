<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Hindi_default" %>

<%@ Register Src="~/Hindi/WCCommon/WcHeadTags.ascx" TagPrefix="uc1" TagName="WcHeadTags" %>
<%@ Register Src="~/Hindi/WCCommon/WCTopBar.ascx" TagPrefix="uc1" TagName="WCTopBar" %>
<%@ Register Src="~/Hindi/WCCommon/WCFooter.ascx" TagPrefix="uc1" TagName="WCFooter" %>
<%@ Register Src="~/Hindi/WCCommon/WCEmployeeServices.ascx" TagPrefix="uc1" TagName="WCEmpServices" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                                            <h5>विभाग अपडेट</h5>
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
				                                <li><a href="#slide1"><span>हमारी दृष्टि </span> भारत के विकास को ऊर्जा प्रदान करते हुए  विश्‍व  की अग्रणी विद्युत कंपनी बनना ।</a></li>
				                                <li><a href="#slide2"><span>लक्ष्य </span> नवप्रवर्तन एवं स्फूर्ति द्वारा संचालित रहते हुए किफायती, दक्षतापूर्ण एवं पर्यावरण-हितैषी तरीके से विश्वसनीय विद्युत-ऊर्जा एवं संबद्ध सेवाएँ प्रदान करना ।</a></li>
				                                <li>
                                                    <a href="#slide3"><span>मूल मान्यताएँ </span> </a>
                                                    <p>सत्यनिष्ठा</p>
                                                    <p>ग्राहक को प्रधानता</p>
                                                    <p>संगठन पर गौरव</p>
                                                    <p>परस्पर आदर एवं विश्वास</p>
                                                    <p>नवप्रवर्तन एवं ज्ञानार्जन</p>
                                                    <p>सम्पूर्ण गुणवत्ता एवं सुरक्षा</p>
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
                                            <h5>आज का विचार</h5>
                                        </td>
                                    </tr>
                                </table>
                                <p id="divThought" runat="server" class="thought"></p>
                                <div class="hindiword">आज का हिंदी शब्द</div>
                                <div class="hwh" id="divHWH" runat="server"></div>
                                <div class="hwe" id="divHWE" runat="server"></div>

                            </div>
                            <!--Thoughts of the day & Hindi Word End-->
                        </td>
                        <td>
                            <table style="width: 100%; padding:0px;">
                                <tr>
                                    <td class="contentbox2">
                                        <!-- Banner Start -->
                                        <div class="banner" id="ntpcbanner">
                                            <ul class="slider">
                                                <% if (dtBanner != null && dtBanner.Rows.Count > 0)
                                                    {
                                                        foreach (System.Data.DataRow row in dtBanner.Rows)
                                                        {%>
                                                            <li><img src="<%=row["BannerPath"] %>" title="<%=row["BannerTextH"] %>"" /></li>
                                                        <%}
                                                    }
                                                    %>
                                            </ul>
                                        </div>
                                        <!-- Banner End -->

                                        <!-- Announcement Start -->
                                        <div class="home-announcement">
                                            <div id="divAnnouncements" runat="server"></div>
                                        </div>
                                        <!-- Announcement End -->
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
                                                </section>
                                            </div>
                                        </div>
                                        <!--HOP Message Box End-->
                                    </td>
                                </tr>
                            </table>


                            <table style="width: 100%; padding:0px;">
                                <tr>
                                    <td class="contentbox2">
                                        <div style="height: 80px; border: 1px solid #cbcbcb; background-color: #ffffff; margin-bottom: 5px; border-radius: 5px;  overflow: hidden; text-align: left; margin-bottom:10px;">
                                           <!-- <h4 style="padding:10px; font-weight:600; padding-bottom:0px;">जनरेशन डेटा </h4>-->
                                            <div>
                                                <iframe class="resp-iframe1" src="http://10.1.168.1/livedata.aspx" gesture="media"  allow="encrypted-media" scrolling="no" border="0" width="100%" height="100%" frameborder="0" align="left" style="overflow-y:auto;overflow-x:hidden;"></iframe>
                                            </div>											
                                        </div>

                                        <!-- Updates - Start -->
                                        <asp:UpdatePanel ID="updatepanel1" runat="server">
                                            <ContentTemplate>
                                                <div class="updates-container">
                                                    <table class="filter">
                                                        <tr>
                                                            <td><asp:LinkButton ID="linkInformation" runat="server" OnClick="show_information">सूचना</asp:LinkButton></td>
                                                            <td><asp:LinkButton ID="linkCirculars" runat="server" OnClick="show_circulars">परिपत्र</asp:LinkButton></td>
                                                            <td><asp:LinkButton ID="linkEvents" runat="server" OnClick="show_events">आयोजन/फोटो गलेरी</asp:LinkButton></td>
                                                            <%--<td><asp:LinkButton ID="linkNotices" runat="server" OnClick="show_notices">नोटिस</asp:LinkButton></td>
                                                            <td><asp:LinkButton ID="linkNews" runat="server" OnClick="show_news">समाचार</asp:LinkButton></td>
                                                            <td><asp:LinkButton ID="linkInformation" runat="server" OnClick="show_information">सूचना</asp:LinkButton></td>--%>                                                            
															<td><asp:LinkButton ID="linkGGMSchedule" runat="server" OnClick="show_ggmschedule">कैलेंडर </asp:LinkButton></td>
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
                                                                <h5><a href="gallery.aspx" style="border-radius: 15px 50px;padding-left: 10px;padding-right: 10px;color: #f7f7f7;font-weight: 600;background-color: #6e94b9;">फोटो गलेरी (सभी देखें)</a></h5>
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
                                        <asp:UpdatePanel ID="updatepanel2" runat="server">
                                            <ContentTemplate>
                                                <div class="birthday-container">
                                                    <table class="filter">
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="linkBirthdays" runat="server" OnClick="show_birthdays">जन्मदिन</asp:LinkButton></td>
                                                            <td>
                                                                <asp:LinkButton ID="linkSuperannuation" runat="server" OnClick="show_superannuations">सेवा-निवृत्ति</asp:LinkButton></td>
                                                            <td>
                                                                <asp:LinkButton ID="linkWelcome" runat="server" OnClick="show_welcomes" Style="border-right: 0;">स्वागत</asp:LinkButton></td>
                                                        </tr>
                                                    </table>
                                                    <div class="updates-content">
                                                        <div id="divBirthdays" runat="server">
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
                                                                <asp:LinkButton ID="linkHalloffame" runat="server" OnClick="linkHalloffame_Click">हॉल ऑफ द फेम</asp:LinkButton></td>
                                                            <td>
                                                                <asp:LinkButton ID="linkSafetyTrining" runat="server" OnClick="linkSafetyTrining_Click">सुरक्षा प्रशिक्षण</asp:LinkButton></td>
                                                            <td>
                                                                <asp:LinkButton ID="linkMedicalExam" runat="server" OnClick="linkMedicalExam_Click" Style="border-right: 0;">चिकित्सा परीक्षण</asp:LinkButton></td>
                                                        </tr>
                                                    </table>
                                                    <div class="updates-content" style="height: 250px;">
                                                        <div id="divSafetyTrining" runat="server">
                                                        </div>
                                                        <div id="divMedicalExam" runat="server">
                                                        </div>
                                                        <div id="divHalloffame" runat="server">
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <!-- Schedule and Hall of Fame - End-->
                                        <!-- Weather -- Start-->
                                        <div class="thoughtbox">
                                            
                                            <a class="weatherwidget-io" href="https://forecast7.com/en/26d2185d29/kanti/" data-label_1="काँटी" data-label_2="मौसम" data-theme="clear" >KBUNL WEATHER</a>
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
