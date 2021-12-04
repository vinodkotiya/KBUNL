<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WCFooter.ascx.cs" Inherits="WCCommon_WCTopBar" %>

<div class="footer">
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
                <div class="ip">आईपी एड्रेस: <asp:label ID="lblIPAddress" runat="server" Text=""></asp:label></div>
                <div class="visitors">टोटल विजिटर:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblVisitor" runat="server" Text=""></asp:Label></div>
                
            </td>
            <td class="col4">
                <div class="feedback">
                    हमें अपनी बहुमूल्य प्रतिक्रिया दें<br />
                    <a href="feedback.aspx" class="button">प्रतिक्रिया</a>
                </div>
            </td>
        </tr>
    </table>
</div>
<!--Copyright - Start-->
<div class="copyright" id="divCopyright" runat="server">
</div>
<!--Copyright - End-->




