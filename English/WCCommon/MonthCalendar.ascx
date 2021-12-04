<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MonthCalendar.ascx.cs" Inherits="WCCommon_MonthCalendar" %>
<div class="calendarcontainer">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <asp:LinkButton ID="lnkbtnPrevious" runat="server" OnClick="lnkbtnPrevious_Click"><img src="../images/calendar-scroll.png" alt="" /></asp:LinkButton></td>
            <td colspan="2">
                <asp:Label ID="lblYear" runat="server" Text=""></asp:Label></td>
            <td>
                <asp:LinkButton ID="lnkbtnNext" runat="server" OnClick="lnkbtnNext_Click"><img src="../images/calendar-scroll1.png" alt="" /></asp:LinkButton></td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="lnkbtnJan" runat="server" OnClick="lnkbtnJan_Click">Jan</asp:LinkButton>
                <asp:HiddenField ID="hdfJan" runat="server" Value="01" />
            </td>
            <td>
                <asp:LinkButton ID="lnkbtnFeb" runat="server" OnClick="lnkbtnFeb_Click">Feb</asp:LinkButton>
                <asp:HiddenField ID="hdfFeb" runat="server" Value="02" />
            </td>
            <td>
                <asp:LinkButton ID="lnkbtnMar" runat="server" OnClick="lnkbtnMar_Click">Mar</asp:LinkButton>
                <asp:HiddenField ID="hdfMar" runat="server" Value="03" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="lnkbtnApr" runat="server" OnClick="lnkbtnApr_Click">Apr</asp:LinkButton>
                <asp:HiddenField ID="hdfApr" runat="server" Value="04" />
            </td>
            <td>
                <asp:LinkButton ID="lnkbtnMay" runat="server" OnClick="lnkbtnMay_Click">May</asp:LinkButton>
                <asp:HiddenField ID="hdfMay" runat="server" Value="05" />
            </td>
            <td>
                <asp:LinkButton ID="lnkbtnJun" runat="server" OnClick="lnkbtnJun_Click">Jun</asp:LinkButton>
                <asp:HiddenField ID="hdfjun" runat="server" Value="06" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:LinkButton ID="lnkbtnJul" runat="server" OnClick="lnkbtnJul_Click">Jul</asp:LinkButton>
                <asp:HiddenField ID="hdfJul" runat="server" Value="07" />
            </td>
            <td>
                <asp:LinkButton ID="lnkbtnAug" runat="server" OnClick="lnkbtnAug_Click">Aug</asp:LinkButton>
                <asp:HiddenField ID="hdfAug" runat="server" Value="08" />
            </td>
            <td>
                <asp:LinkButton ID="lnkbtnSep" runat="server" OnClick="lnkbtnSep_Click">Sep</asp:LinkButton>
                <asp:HiddenField ID="hdfSep" runat="server" Value="09" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:LinkButton ID="lnkbtnOct" runat="server" OnClick="lnkbtnOct_Click">Oct</asp:LinkButton>
                <asp:HiddenField ID="hdfOct" runat="server" Value="10" />
            </td>
            <td>
                <asp:LinkButton ID="lnkbtnNov" runat="server" OnClick="lnkbtnNov_Click">Nov</asp:LinkButton>
                <asp:HiddenField ID="hdfNov" runat="server" Value="11" />
            </td>
            <td>
                <asp:LinkButton ID="lnkbtnDec" runat="server" OnClick="lnkbtnDec_Click">Dec</asp:LinkButton>
                <asp:HiddenField ID="hdfDec" runat="server" Value="12" />
            </td>
        </tr>
    </table>

</div>
