<%@ Page Language="C#" AutoEventWireup="true" CodeFile="directory-newjoining.aspx.cs" Inherits="directory" %>

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
    <style type="text/css">
        label { font-size: 12px !important; font-weight: 600; }
        .gridview{font-size:12px; border:1px solid #dbdbdb; border-collapse:collapse;}
        .gridview th{font-size:12px; border:1px solid #dbdbdb; border-collapse:collapse;}
        .gridview td{font-size:12px; border:1px solid #dbdbdb; border-collapse:collapse;}

        .gridviewpaging table{width:auto;}
        .gridviewpaging td{border:none; }
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
                            <asp:HiddenField ID="hdfDeptId" runat="server" Value="2" />
                            <!--Inner Banner - Start -->
                            <div class="innerbanner">
                                <img src="../images/banner-directory.jpg" alt="" />
                                <div class="bannerpatch">
                                    <h4>प्रोजेक्ट के नए सदस्य</h4>
                                    <div class="breadcrumb">
                                        <a href="Default.aspx" class="home">होम</a>
                                        <a class="active">प्रोजेक्ट के नए सदस्य</a>
                                    </div>
                                    <h5><a href="important-contacts.aspx" class="home">महत्वपूर्ण संपर्क</a></h5>
                                </div>
                            </div>
                            <!--Inner Banner - End -->

                            <!--About Department - Start -->
                            <asp:UpdatePanel ID="updatepnl" runat="server">
                                <ContentTemplate>
                                    <div class="directorycontent" style="margin-top: 30px; background-color:#ffffff;">
                                        <asp:HiddenField ID="hfsearch" runat="server" />
                                        <table>
                                            <tr>
                                                <td style="border:none;">
                                                    <div class="direcrorysearch">
								                        <asp:TextBox ID="txtDirectorySearch" runat="server" placeholder="सर्च" AutoPostBack="true" OnTextChanged="btnDirectorySearch_Click"></asp:TextBox>
								                        <asp:Button ID="btnDirectorySearch" runat="server" Text="सर्च करें" OnClick="btnDirectorySearch_Click" CssClass="btn" />
							                        </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:GridView ID="gridDirectory" EmptyDataText="No Record Found" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="50" CssClass="gridview" PagerStyle-CssClass="gridviewpaging" OnPageIndexChanging="gridDirectory_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Photo" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <img src='../<%#Eval("PhotoPath") %>' alt="" width="100" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EmpNo">
                                                    <ItemTemplate>
                                                        <%#Eval("EmpCode") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <%#Eval("EmpNameHindi") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department">
                                                    <ItemTemplate>
                                                        <%#Eval("DepartmentH") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <%#Eval("DesignationH") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Level">
                                                    <ItemTemplate>
                                                        <%#Eval("Level") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email">
                                                    <ItemTemplate>
                                                        <%#Eval("EmailID") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile<br/>Alt.Mob">
                                                    <ItemTemplate>
                                                        <%#Eval("Mobile") %><br />
                                                        <%#Eval("AlternateMobileNumber") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Intercom">
                                                    <ItemTemplate>
                                                        <%#Eval("IntercomOffice") %><br />
                                                        <%#Eval("IntercomResidence") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DOJ">
                                                    <ItemTemplate>
                                                        <%#Eval("DOJ_atProject") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
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
