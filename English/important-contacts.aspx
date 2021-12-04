<%@ Page Language="C#" AutoEventWireup="true" CodeFile="important-contacts.aspx.cs" Inherits="English_telephone_directory" %>

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
                                    <h4>Important Contacts</h4>
                                    <div class="breadcrumb">
                                        <a href="Default.aspx" class="home">Home</a>
                                        <a href="directory.aspx">Directory</a>
                                        <a class="active">Important Contacts</a>
                                    </div>
                                </div>
                            </div>
                            <!--Inner Banner - End -->

                            <!--About Department - Start -->
                            <asp:UpdatePanel ID="updatepnl" runat="server">
                                <ContentTemplate>
                                    <div class="directorycontainer">
                                        <div class="filter">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label>Category</label>
                                                        <asp:DropDownList ID="ddlGroup" CssClass="dropdown" runat="server" Width="250px" ValidationGroup="impcontacts" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <label>Location/Name</label>
                                                        <asp:TextBox ID="txtname" CssClass="txtbox" runat="server" Width="200px" ValidationGroup="impcontacts" AutoPostBack="true" OnTextChanged="txtname_TextChanged"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <%--<asp:Button ID="btnView" runat="server" Text="View" CssClass="pagebutton" OnClick="btnView_Click" ValidationGroup="impcontacts" />--%>
                                                        <%--<asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="pagebutton" OnClick="btnReset_Click" ValidationGroup="impcontacts"/>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="directorycontent impcontact" style="padding-top: 30px;">
                                            <div style="text-align: right;">
                                                <asp:ImageButton ID="btnExport" runat="server" ImageUrl="../images/icons/excel.png" Width="30" ToolTip="Export to Excel" OnClick="btnExport_Click" />
                                            </div>
                                            <table cellspacing="0" cellpadding="0">

                                                <asp:Repeater ID="repeaterEmployee" runat="server">
                                                    <ItemTemplate>
                                                        <tr id="headingrow" runat="server" visible='<%# Eval("SrNo").ToString()=="1" %>'>
                                                            <th colspan="5"><span><%# Eval("GroupNameEnglish") %></span></th>
                                                        </tr>
                                                        <tr id="headingrow2" runat="server" visible='<%# Eval("SrNo").ToString()=="1" %>'>
                                                            <th>Location/Name</th>
                                                            <th>Phone</th>
                                                            <th>Intercom</th>
                                                            <th>Email</th>
                                                            <th>Address</th>
                                                        </tr>
                                                        <tr>
                                                            <td><span><%# Eval("LocationOrNameEnglish") %></span></td>
                                                            <td><%# Eval("Phone") %></td>
                                                            <td><%# Eval("Intercome") %></td>
                                                            <td><span><%# Eval("Email") %></span></td>
                                                            <td><span><%# Eval("Address") %></span></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>

                                            <br />
                                            <div id="paging" runat="server" class="paging">
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
                                                            <asp:LinkButton ID="lbtnNext" runat="server" CausesValidation="false" CssClass="next" OnClick="lbtnNext_Click">&gt;</asp:LinkButton>
                                                        </li>
                                                    </li>
                                                </ul>
                                                <div class="border">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnExport" />
                                </Triggers>
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
