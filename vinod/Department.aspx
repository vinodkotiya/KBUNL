<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Department.aspx.cs" Inherits="_Default" ClientIDMode="AutoID" %>

<%@ Register Src="~/English/WCCommon/WcHeadTags.ascx" TagPrefix="uc1" TagName="WcHeadTags" %>
<%@ Register Src="~/English/WCCommon/WCTopBar.ascx" TagPrefix="uc1" TagName="WCTopBar" %>
<%@ Register Src="~/English/WCCommon/WCFooter.ascx" TagPrefix="uc1" TagName="WCFooter" %>
<%@ Register Src="~/English/WCCommon/WCEmployeeServices.ascx" TagPrefix="uc1" TagName="WCEmpServices" %>
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
                                    <!--Inner Banner - Start -->
                                    <div class="innerbannerdept">
                                        <div class="bannerpatch">                                            
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <h4><asp:label id="lblDepartmentName" runat="server" Text=""></asp:label></h4>
                                                        <div class="breadcrumb">
                                                            <a href="Default.aspx" class="home">Home</a>
                                                            <a class="active"><asp:label id="lblDptNameBrdCrum" runat="server" Text=""></asp:label></a>
                                                        </div>
                                                         <div class="ourteam">
                                                            <a id="linkOurTeam" runat="server" class="home" target="_blank">Our Team</a>
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
                                    <div class="aboutdept2container">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="adcol1">
                                                    <!-- Banner Start -->
							                        <div class="banner" id="ntpcbanner" runat="server">								                        
							                        </div>
							                        <!-- Banner End -->
                                                </td>
                                                <td class="adcol2">
                                                    <div class="highlights" style="margin-left:30px; margin-right:0px;">                                                        
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
                                            <asp:HiddenField ID="hfActiveTabIndex" runat="server" Value="0"/>
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
                                                            <asp:LinkButton ID="linkbtnFolder" runat="server" Text='<%#Eval("DocTitleEnglish") %>' CssClass='<%#Eval("Extension") %>' visible='<%# Eval("IsDirectory").ToString() == "True" %>' OnClick="ViewChild_Click"></asp:LinkButton>
                                                            <a id="linkdocument_attachment" runat="server" href='<%#Eval("AttachmentURL_Admin")%>' visible='<%# Eval("IsAttachment").ToString() == "True" %>' target="_blank" class='<%#Eval("Extension") %>'><%#Eval("DocTitleEnglish") %></a>
                                                            <a id="linkdocument_url" runat="server" href='<%#Eval("URL")%>' visible='<%# Eval("IsURL").ToString() == "True" %>' target="_blank" class='<%#Eval("Extension") %>'><%#Eval("DocTitleEnglish") %></a>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <div style="text-align:right;">
                                                        <asp:LinkButton ID="linkBack" runat="server" OnClick="linkBack_Click" Text="Back"></asp:LinkButton>
                                                    </div>
								                </div>

                                                <div class="deptvisitor" style="background-color:#1565b2; color:#ffffff; padding:10px 20px; display:inline-block; margin-top:-10px; z-index:99; position:relative;" id="divDeptVisitors" runat="server"></div>
							                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
							        <!-- Updates - End -->

                                    <!--More Information-->
                                    <div id="divMoreinformation" runat="server" class="highlights" style="background-color: #ffffff;border: 1px solid #cbcbcb;margin-top: 20px;padding: 20px;border-radius: 5px;">

                                    </div>

                                </td>
                            </tr>
                        </table>
            </div>
            <!--Content - End-->

            <!--Footer - Start-->
            <uc1:WCFooter runat="server" ID="WCFooter" />
            <!--Footer - End-->
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
