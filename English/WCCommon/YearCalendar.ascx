<%@ Control Language="C#" AutoEventWireup="true" CodeFile="YearCalendar.ascx.cs" Inherits="WCCommon_YearCalendar" %>
<div class="calendarcontainer">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <asp:LinkButton ID="lnkbtnPrevious" runat="server" OnClick="lnkbtnPrevious_Click"><img src="../images/calendar-scroll.png" alt="" /></asp:LinkButton></td>
            <td style="padding-left: 65px;">
                <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label></td>
            <td>
                <asp:LinkButton ID="lnkbtnNext" runat="server" OnClick="lnkbtnNext_Click"><img src="../images/calendar-scroll1.png" alt="" /></asp:LinkButton></td>
        </tr>
    </table>
    <div class="yearcontainer">
        <asp:DataList ID="dtlstYear" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" OnItemDataBound="dtlstYear_ItemDataBound">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="lnkbtnCalendarYear" runat="server" Text='<%#Eval("Year")%>' OnClick="lnkbtnCalendarYear_Click"></asp:LinkButton>
                <%--<asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year")%>'></asp:Label>--%>
            </ItemTemplate>
        </asp:DataList>
    </div>
</div>
