<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Department.aspx.cs" Inherits="_Default" ClientIDMode="AutoID" %>

<%@ Register Src="~/English/WCCommon/WcHeadTags.ascx" TagPrefix="uc1" TagName="WcHeadTags" %>
<%@ Register Src="~/English/WCCommon/WCTopBar.ascx" TagPrefix="uc1" TagName="WCTopBar" %>
<%@ Register Src="~/English/WCCommon/WCFooter.ascx" TagPrefix="uc1" TagName="WCFooter" %>
<%@ Register Src="~/English/WCCommon/WCEmployeeServices.ascx" TagPrefix="uc1" TagName="WCEmpServices" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,initial-scale=1">
    <title></title>
    <!--Header - Start-->
    <uc1:WcHeadTags runat="server" ID="WcHeadTags" />
    <!--Header - End-->
    <!-- OWL Corousel - Start -->
    <link href="../js/owl_corousel/owl.carousel.css" rel="stylesheet" type="text/css" />
    <link href="../js/owl_corousel/owl.theme.css" rel="stylesheet" type="text/css" />
    <script src="../js/owl_corousel/owl.carousel.js" type="text/javascript"></script>
    <link href="../js/owl_corousel/bootstrapTheme.css" rel="stylesheet" type="text/css" />
    <!-- OWL Corousel - End -->
    
    <style>
        .hoplogin{position:fixed; top:0; left:0; width:100%; height:100%; background-color:rgba(255,255,255,0.8);}
        .hoplogin .passcodebox{margin:0 auto; width:300px; text-align:center; padding:20px; background-color:#ffffff; border:1px solid #dbdbdb; border-radius:5px; margin-top:10%;padding:20px;}
         .button{padding:5px 5px; margin-top:10px;text-transform:uppercase;font-family:Roboto;font-size:14px;color:#f0f0f0;background-color:#1565b2;border-radius:2px;border:1px solid #466522; padding:5px;}
         .hoptextbox{padding:5px 5px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptmgr" runat="server"></asp:ScriptManager>

        <div class="hoplogin" id="divPassCode" runat="server">
            <div class="passcodebox">
                <div style="padding:5px; font-weight:bold;">
                     Please Enter Passcode
                    </div>
               <div style="padding:5px;">
                     <asp:TextBox ID="txtPassCode" runat="server" placeholde="Please enter Passcode" CssClass="hoptextbox" TextMode="Password" ></asp:TextBox>
               
                </div>
                <asp:Button ID="btncontinue" runat="server" OnClick="btncontinue_Click" Text="Continue" CssClass="button"   /><br /><br />
                <asp:Label ID="lblPassCodeMsg" runat="server"></asp:Label>
            </div>
        </div>

        <div class="pagecontainer" id="divDepartmentcontent" runat="server">
            <!--MenuBar - Start-->
            <uc1:WCTopBar runat="server" ID="WCTopBar1" />
            <!--MenuBar - End-->

            <!--Content - Start-->
            <div class="content-outer">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="contentbox1" style="    width: 240px;">
                            <!-- Employee Services - Start -->
                            <uc1:WCEmpServices runat="server" ID="WCEmpServices" />
                            <!-- Employee Services - End -->
                            <div id="divGen" runat="Server" Visible="False" >
                            <div style="height: 260px;">
                                <iframe class="resp-iframe1" src="http://10.1.168.1/livedata.aspx?v=1" gesture="media"  allow="encrypted-media" scrolling="no" border="0" width="100%" height="100%" frameborder="0" align="left" style="overflow-y:auto;overflow-x:hidden;"></iframe>
                            </div>	
                            <div  class="employee-services"><a href="http://10.1.169.6/cc/" target="_blank"><span class="imgbox"><img src="../Uploads/EmployeeServiceIcon/ServiceIcon_2882021162258.png" alt=""></span><span class="title">Contracts Closing</span></a>
                                <a href="http://10.0.237.11/reports/gdams-realview.aspx" target="_blank"><span class="imgbox"><img src="../Uploads/EmployeeServiceIcon/ServiceIcon_2682021172052.png" alt=""></span><span class="title">NTPC Realtime Data Generation</span></a>
                                <a href="http://10.1.168.1/app/POReport.aspx?HOP=1" target="_blank"><span class="imgbox"><img src="../Uploads/EmployeeServiceIcon/ServiceIcon_259202110455.jpg" alt=""></span><span class="title">Deptwise Wage Payment Status</span></a>
                                <a href="http://10.1.168.1/app/POReport.aspx?HOP=1" target="_blank"><span class="imgbox"><img src="../Uploads/EmployeeServiceIcon/ServiceIcon_259202110455.jpg" alt=""></span><span class="title">MSME Payment Status</span></a>
                            </div>
                            </div>
                            </td>
                        </td>
                        <td class="contentboxinner">
                            <asp:HiddenField ID="hdfDeptId" runat="server" Value="0" />
                            <!--Inner Banner - Start -->
                            <div class="innerbannerdept"  style="border-bottom: 5px solid #2c3f13; background-image: url(../images/deptbanner.png);  border-radius: 10px; background-repeat: no-repeat;background-size: cover;">
                                <div class="bannerpatch" >
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <h4>
                                                    <asp:Label ID="lblDepartmentName" runat="server" Text=""  style="font-family: Arial Black; font-size: 30px;"></asp:Label></h4>
                                                <div class="breadcrumb">
                                                    <a href="Default.aspx" class="home">Home</a>
                                                    <a class="active">
                                                        <asp:Label ID="lblDptNameBrdCrum" runat="server" Text="" ></asp:Label></a>
                                                </div>
                                                <div class="ourteam">
                                                    <a id="linkOurTeam" runat="server" class="home" target="_blank">  <img src="../images/phone-icon-blue.png" height="16px" /> Our Team</a>
                                                </div>
                                            </td>
                                            <td align="right">
                                                <div id="divAboutDepartment" runat="server" class="aboutDepartment"></div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <!--Inner Banner - End -->

                            <!--Slider & Highlights - Start -->
                            <div class="aboutdept2container" id="divSliderHigh" runat="Server">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="adcol1">
                                            <!-- Banner Start -->
                                            <div class="banner" id="ntpcbanner" runat="server">
                                            </div>
                                            <!-- Banner End -->
                                        </td>
                                        <td class="adcol2">
                                            <div class="highlights" style="margin-left: 30px; margin-right: 0px;">
                                                <div class="hlcontent">
                                                    <h4>Updates</h4>
                                                    <asp:Literal ID="ltrHighlight" runat="server"></asp:Literal>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!--Slider & Highlights - End -->

                            <!-- Updates - Start -->
                            <asp:UpdatePanel ID="updatepanel_documents" runat="server">
                                <ContentTemplate>
                                    <div runat="server" id="divGauge" Visible="false" style="height: 170px; border: 1px solid #cbcbcb; background-color: #ffffff; margin-bottom: 5px; border-radius: 5px;  overflow: hidden; text-align: left; margin-bottom:10px;">
                                            
                                        <div>
                                            <iframe class="resp-iframe1" src="http://10.1.168.1/livedata1.aspx" gesture="media"  allow="encrypted-media" scrolling="no" border="0" width="100%" height="100%" frameborder="0" align="left" style="overflow-y:auto;overflow-x:hidden;"></iframe>
                                        </div>											
                                    </div>
                                    <asp:HiddenField ID="hfActiveTabIndex" runat="server" Value="0" />
                                    <asp:HiddenField ID="hfDocLevelCode_forParent_tmp" runat="server" />
                                    <div class="updates-container department">

                                        <div class="filter department">
                                            <asp:Repeater ID="repeater_DocumentTab" runat="server">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfDocID" runat="server" Value='<%#Eval("DocID") %>' />
                                                    <asp:HiddenField ID="hfDocLevelCode" runat="server" Value='<%#Eval("DocLevelCode") %>' />
                                                    <asp:HiddenField ID="hfDocLevelCode_forChild" runat="server" Value='<%#Eval("DocLevelCode_forChild") %>' />
                                                    <asp:LinkButton ID="linktab" runat="server" Text='<%#Eval("DocTitleEnglish") %>' CommandArgument='<%# Eval("DocID") %>' OnClick="View_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>


                                        <div class="updates-content department">

                                            <asp:Repeater ID="repeater_Document" runat="server">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfDocID" runat="server" Value='<%#Eval("DocID") %>' />
                                                    <asp:HiddenField ID="hfDocLevelCode" runat="server" Value='<%#Eval("DocLevelCode") %>' />
                                                    <asp:HiddenField ID="hfDocLevelCode_forChild" runat="server" Value='<%#Eval("DocLevelCode_forChild") %>' />
                                                    <asp:HiddenField ID="hfDocLevelCode_forParent" runat="server" Value='<%#Eval("DocLevelCode_forParent") %>' />
                                                    <asp:LinkButton ID="linkbtnFolder" runat="server" Text='<%#Eval("DocTitleEnglish") %>' CssClass='<%#Eval("Extension") %>' Visible='<%# Eval("IsDirectory").ToString() == "True" %>' OnClick="ViewChild_Click"></asp:LinkButton>
                                                    <a id="linkdocument_attachment" runat="server" href='<%#Eval("AttachmentURL_Admin")%>' visible='<%# Eval("IsAttachment").ToString() == "True" %>' target="_blank" class='<%#Eval("Extension") %>'><%#Eval("DocTitleEnglish") %></a>
                                                    <a id="linkdocument_url" runat="server" href='<%#Eval("URL")%>' visible='<%# Eval("IsURL").ToString() == "True" %>' target="_blank" class='<%#Eval("Extension") %>'><%#Eval("DocTitleEnglish") %></a>
                                                </ItemTemplate>
                                            </asp:Repeater>


                                            <div style="text-align: right;">
                                                <asp:LinkButton ID="linkBack" runat="server" OnClick="linkBack_Click" Text="Back"></asp:LinkButton>
                                            </div>

                                        </div>
                                           <div style="background-color:#1e522e;  color:#ffffff; padding:10px 20px; display:inline-block; margin-top:10px; font-size:18px; z-index:99; float:left;">
                                                         <asp:Label ID="lblHopHeading" runat="server" Text="CEO Reports"></asp:Label>
                                                 </div> <br/>
                                        <div class="updates-content department" style="margin-top: 30px;">
                                            <asp:Panel ID="pnlHopDept" runat="server">
                                                <asp:Repeater ID="repeater_HOP" runat="server">
                                                    <ItemTemplate>

                                                        <asp:HiddenField ID="hfDeptID" runat="server" Value='<%#Eval("DeptID") %>' />
                                                        <asp:LinkButton ID="linkbtnFolder" runat="server" Text='<%#Eval("Department") %>' CssClass="folder" Visible="true" OnClick="ViewDept_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlDept" runat="server">
                                                <asp:Repeater ID="repeater_DocByDept" runat="server">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfDocID" runat="server" Value='<%#Eval("DocID") %>' />

                                                        <a id="linkdocument_attachment" runat="server" href='<%#Eval("AttachmentURL_Admin")%>' visible='<%# Eval("IsAttachment").ToString() == "True" %>' target="_blank" class='<%#Eval("Extension") %>'><%#Eval("DocTitleEnglish") %></a>
                                                        <a id="linkdocument_url" runat="server" href='<%#Eval("URL")%>' visible='<%# Eval("IsURL").ToString() == "True" %>' target="_blank" class='<%#Eval("Extension") %>'><%#Eval("DocTitleEnglish") %></a>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <div style="text-align: right;">
                                                    <asp:LinkButton ID="linkBackHop" runat="server" OnClick="linkBackHop_Click" Text="Back" Visible="false"></asp:LinkButton>
                                                </div>
                                            </asp:Panel>

                                            <asp:Panel ID="pnlForAllDept" runat="server">
                                                <div>
                                                    <asp:Label ID="lblAcess" runat="server" Text=""></asp:Label>
                                                </div>
                                                <asp:Repeater ID="repeater_allDept" runat="server">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfDocID" runat="server" Value='<%#Eval("DocID") %>' />
                                                        <a id="linkdocument_attachment" runat="server" href="access-denied.aspx" visible='<%# Eval("IsAttachment").ToString() == "True" %>' class='<%#Eval("Extension") %>' title="Access is Denied"><%#Eval("DocTitleEnglish") %></a>
                                                        <a id="linkdocument_url" runat="server" href="#" visible='<%# Eval("IsURL").ToString() == "True" %>' class='<%#Eval("Extension") %>'><%#Eval("DocTitleEnglish") %></a>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                            </asp:Panel>
                                            <asp:Panel ID="pnlForAllDeptLogin" runat="server" visible="False">
                                                
                                                <asp:Repeater ID="repeater_allDeptLogin" runat="server">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfDocID" runat="server" Value='<%#Eval("DocID") %>' />
                                                            <a id="linkdocument_attachment" runat="server" href='<%#Eval("AttachmentURL_Admin")%>' visible='<%# Eval("IsAttachment").ToString() == "True" %>' target="_blank" class='<%#Eval("Extension") %>'><%#Eval("DocTitleEnglish") %></a>
                                                            <a id="linkdocument_url" runat="server" href='<%#Eval("URL")%>' visible='<%# Eval("IsURL").ToString() == "True" %>' target="_blank" class='<%#Eval("Extension") %>'><%#Eval("DocTitleEnglish") %></a>
                                                        </ItemTemplate>
                                                </asp:Repeater>

                                            </asp:Panel>
                                           
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <!-- Updates - End -->
                                <!--More Information-->
                                <div id="divMoreinformation" runat="server" class="highlights" style="background-color: #ffffff;border: 1px solid #cbcbcb;margin-top: 20px;padding: 20px;border-radius: 5px;">

                                </div>
                                 <!--Department Admin-->
                                 <div id="divDeptAdmin" runat="server" class="highlights" style="background-color: #ffffff;border: 1px solid #cbcbcb;margin-top: 20px;padding: 20px;border-radius: 5px;">

                                </div>
                                <div style="border-radius: 15px 50px; background-color: #6d9665; color: #ffffff; padding: 10px 20px; display: inline-block; margin-top: -10px; z-index: 99; position: relative;">
                                    Total Files:
                                    <asp:Label ID="lblTotalFile" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="deptvisitor" style="border-radius: 15px 50px; background-color: #6d9665; color: #ffffff; padding: 10px 20px; display: inline-block; margin-top: -10px; z-index: 99; position: relative;" id="divDeptVisitors" runat="server"></div>
                             
                        </td>
                    </tr>
                </table>
            </div>
            <!--Content - End-->

            <!--popup1-->
            <!-- ModalPopupExtender -->


            <!--Footer - Start-->
            <uc1:WCFooter runat="server" ID="WCFooter" />
            <!--Footer - End-->

            <!--Pop-Up Modal-->


            
        </div>


        <script type="text/javascript">
            $(document).ready(function () {
                show_tab1();
            });

            function hide_updates() {
                document.getElementById('linktab1').classList.remove('active');
                document.getElementById('linktab2').classList.remove('active');
                document.getElementById('linktab3').classList.remove('active');
                document.getElementById('linktab4').classList.remove('active');
                document.getElementById('linktab5').classList.remove('active');

                document.getElementById('divtab1').style.display = "none";
                document.getElementById('divtab2').style.display = "none";
                document.getElementById('divtab3').style.display = "none";
                document.getElementById('divtab4').style.display = "none";
                document.getElementById('divtab5').style.display = "none";
            }
            function show_tab1() {
                hide_updates();
                document.getElementById('linktab1').classList.add('active');
                document.getElementById('divtab1').style.display = "block";
            }
            function show_tab2() {
                hide_updates();
                document.getElementById('linktab2').classList.add('active');
                document.getElementById('divtab2').style.display = "block";
            }
            function show_tab3() {
                hide_updates();
                document.getElementById('linktab3').classList.add('active');
                document.getElementById('divtab3').style.display = "block";
            }
            function show_tab4() {
                hide_updates();
                document.getElementById('linktab4').classList.add('active');
                document.getElementById('divtab4').style.display = "block";
            }
            function show_tab5() {
                hide_updates();
                document.getElementById('linktab5').classList.add('active');
                document.getElementById('divtab5').style.display = "block";
            }

        </script>


    </form>
</body>
</html>
