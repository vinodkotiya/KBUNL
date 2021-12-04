<%@ Page Title="" Language="C#" MasterPageFile="~/Employee/Employee.master" AutoEventWireup="true" CodeFile="Employee_ChangePass.aspx.cs" Inherits="Employee_Employee_ChangePass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        td {
            padding: 5px;
        }
    </style>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdfUserName" runat="server" />
            <asp:HiddenField ID="hdfEmployeeId" runat="server" Value="0" />
            <div style="padding-left: 20px; padding-right: 20px;">
                <div style="padding-top: 15px;">

                    <h3>Change Password</h3>
                </div>
                <div style="margin-bottom: 10px; margin-top: 20px; width: 400px;">

                    <table cellpadding="5" cellspacing="5" width="100%">



                        <tr>
                            <td width="150px">
                                <asp:Label ID="Label1" runat="server" Text="Current password"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtOP" runat="server" TextMode="Password" Style="padding: 5px 10px;"></asp:TextBox></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="New password"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtNP" runat="server" TextMode="Password" Style="padding: 5px 10px;"></asp:TextBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Confirm password"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtCP" runat="server" TextMode="Password" Style="padding: 5px 10px;"></asp:TextBox>
                                <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNP"
                                 ControlToValidate="txtCP" ErrorMessage="password do not match"></asp:CompareValidator>--%>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btn_submit" runat="server" Text="Change" OnClick="btn_submit_Click" CssClass="button button-blue" />
                            </td>
                            <td></td>
                        </tr>
                        <tr style="margin-top: 15px;">
                            <td colspan="3" align="center" style="padding-bottom: 5px;">
                                <div id="alert" runat="server">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>


                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="100">
        <ProgressTemplate>
            <asp:Panel ID="Pnl1" CssClass="overlay" runat="server" meta:resourcekey="Pnl1Resource1">
                <asp:Panel ID="Pnl2" CssClass="loader" runat="server" meta:resourcekey="Pnl2Resource1">
                    <img alt="" src="../images/p1.gif" width="50px" />
                </asp:Panel>
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>

