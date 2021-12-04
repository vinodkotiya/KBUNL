<%@ Page Language="C#" AutoEventWireup="true" CodeFile="directory.aspx.cs" Inherits="directory" %>

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
                                    <h4>Directory</h4>
                                    <div class="breadcrumb">
                                        <a href="Default.aspx" class="home">Home</a>
                                        <a class="active">Directory</a>                                        
                                    </div>
                                    <h5><a href="important-contacts.aspx" class="home">Important Contact Numbers</a></h5>
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
                                                        <label>Dept.</label>
                                                        <asp:DropDownList ID="ddlDepartment" AutoPostBack="true" CssClass="dropdown" runat="server" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" Width="120px"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <label>Grade</label>
                                                        <asp:DropDownList ID="ddlGrade" AutoPostBack="true" CssClass="dropdown" runat="server" OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged" Width="60px"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <label>Desig.</label>
                                                        <asp:DropDownList ID="ddlDesignation" AutoPostBack="true" CssClass="dropdown" runat="server" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged" Width="120px"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <label>EmpNo</label>
                                                        <asp:TextBox ID="txtEmpNo" CssClass="txtbox" AutoPostBack="true" runat="server" OnTextChanged="txtEmpNo_TextChanged" Width="80px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <label>Name</label>
                                                        <asp:TextBox ID="txtname" CssClass="txtbox" AutoPostBack="true" runat="server" OnTextChanged="txtname_TextChanged" Width="100px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <label>Blood Group</label>
                                                        <asp:DropDownList ID="ddlBloodGroup" AutoPostBack="true" CssClass="dropdown" runat="server" OnSelectedIndexChanged="ddlBloodGroup_SelectedIndexChanged" Width="60px"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="directorycontent" style="padding-top:30px;">
                                            <div style="text-align:right;">
                                                <asp:ImageButton ID="btnExport" runat="server" ImageUrl="../images/icons/excel.png" Width="30" ToolTip="Export to Excel" OnClick="btnExport_Click"/>
                                            </div>
                                            <asp:Repeater ID="repeaterEmployee" runat="server">
                                                <ItemTemplate>
                                                    <div class="directorybox">
                                                        <div class="imgbox">
                                                            <img src='../<%#Eval("PhotoPath")%>' width="100px" />
                                                        </div>
                                                        <br />
                                                        <table>
                                                            <tr>
                                                                <td class="name"><%#Eval("EmpName")%></td>
                                                                <td class="empno"><%#Eval("EmpCode")%></td>
                                                            </tr>
                                                        </table>
                                                        <div class="designation"><%#Eval("Designation")%> (<%#Eval("Department")%>)</div>
                                                        <div class="phone"><%#Eval("ContactNo")%></div>
                                                        <div class="email"><%#Eval("EmailID")%></div>
														<div class="quarter" id="divQtr" runat="server" visible='<%#Eval("QuarterDetails").ToString()!=""%>'><%#Eval("QuarterDetails")%></div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <br />
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
                                                            <asp:LinkButton ID="lbtnNext" runat="server" CausesValidation="false" CssClass="next" OnClick="lbtnNext_Click">></asp:LinkButton>
                                                        </li>
                                                    </li>

                                                </ul>
                                                <div class="border"></div>
                                            </div>


                                            <%--<div class="paging">
                                                <a href="#" class="active">1</a>
                                                <a href="#">2</a>
                                                <a href="#">3</a>
                                                <a href="#">4</a>
                                                <a href="#">5</a>
                                            </div>--%>
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
