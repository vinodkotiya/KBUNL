<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="admin-change-email.aspx.cs" Inherits="admin_ChangePass" Title="Change Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        td {
            padding: 5px;
        }
    </style>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID= "hdfUserName" runat="server" />
            <div style="padding-left: 20px; padding-right: 20px;">
                <div style="padding-top: 15px;">

                    <h3>Update Admin Email ID</h3>
                </div>
                <div style="margin-bottom: 10px; margin-top: 20px; width: 800px;">
                    <table cellpadding="5" cellspacing="5" width="100%">
                        <tr>
                            <td width="150px">
                                <asp:Label ID="Label1" runat="server" Text="Admin EmailID"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtEmailID" runat="server" MaxLength="100" Style="padding: 5px 10px; width: 400px;"></asp:TextBox></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btn_update" runat="server" Text="Update" OnClick="btn_update_Click" CssClass="button button-blue" />
                            </td>
                            <td></td>
                        </tr>
                        <tr style="margin-top: 15px;">
                            <td></td>
                            <td colspan="2" align="left" style="padding-bottom: 5px;">


                                <asp:Label ID="lblmsg" runat="server"></asp:Label>
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

