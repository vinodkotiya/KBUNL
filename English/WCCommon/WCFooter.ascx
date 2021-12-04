<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WCFooter.ascx.cs" Inherits="WCCommon_WCTopBar" %>

<div class="footer" style="border-radius: 10px;
    background-repeat: no-repeat;
    background-size: cover; background-image: url('https://png.pngtree.com/thumb_back/fh260/back_our/20190620/ourmid/pngtree-lynx-summer-green-banner-background-image_155644.jpg');">
    <asp:HiddenField ID="HDN_IPAddress" runat="server" />
    <table>
        <tr>
            <td class="col1">
                <img src="../images/ntpc-logo-footer.png" alt="" class="logo" />
            </td>
            <td class="col2">
                <div class="address">
                    <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                </div>
            </td>
            <td class="col3">
                <div class="ip">Login: <asp:label ID="lblIPAddress" runat="server" Text="Anonymous"></asp:label></div>
                <div class="visitors"> Total Visitors:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblVisitor" runat="server" Text=""></asp:Label></div>
            </td>
            <td class="col4">                
                <div class="feedback">
                    Give us your valuable feedback<br />
                    <a href="feedback.aspx" class="button">Feedback</a>
                </div>
            </td>
        </tr>
    </table>
</div>
<!--Copyright - Start-->
<div class="copyright" id="divCopyright" runat="server">
</div>
<!--Copyright - End-->




