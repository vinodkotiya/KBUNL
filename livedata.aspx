<%@ Page Title="" Language="VB" MasterPageFile="MasterPageLiveData.master" AutoEventWireup="true"
    CodeFile="livedata.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="style1">
        <div id="sidebar-d">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Timer ID="Timer1" runat="server" Interval="10000">
                    </asp:Timer>

                    <div id="divHori" runat="Server" >
                    <table>
                        <thead>
                            <tr>
                                <th>Unit #1</th>
                                <th>Unit #2</th>
                                <th>Unit #3</th>
                                <th>Unit #4</th>
 <th>Total</th>
                                <th>Frequency</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td><asp:Label ID="Label19" runat="server"></asp:Label></td>
                                <td><asp:Label ID="Label14" runat="server"></asp:Label></td>
                                <td><asp:Label ID="Label6" runat="server"></asp:Label></td>
                                <td><asp:Label ID="Label10" runat="server"></asp:Label></td>
 <td><asp:Label ID="lbTotal" runat="server" Text="Loading.."></asp:Label></td>
                                <td><asp:Label ID="Label8" runat="server"></asp:Label></td>
                            </tr>
                        </tbody>
                    </table>
                   </div>
                   <div id="divVert" runat="Server" Visible = "False" >
                    <div class="divTable">
                        <div class="divTableHeading">
                        <div class="divTableRow">
                        <div class="divTableHead">Live Generation</div>
                        </div>
                        </div>
                        <div class="divTableBody">
                        <div class="divTableRow">
                        <div class="divTableCell">Unit #1</div>
                        <div class="divTableCell"><span id="ContentPlaceHolder1_Label19v"><asp:Label ID="Label19v" runat="server"></asp:Label></span></div>
                        </div>
                        <div class="divTableRow">
                        <div class="divTableCell">Unit #2</div>
                        <div class="divTableCell"><span id="ContentPlaceHolder1_Label14v"><asp:Label ID="Label14v" runat="server"></asp:Label></span></div>
                        </div>
                        <div class="divTableRow">
                        <div class="divTableCell">Unit #3</div>
                        <div class="divTableCell"><span id="ContentPlaceHolder1_Label6v"><asp:Label ID="Label6v" runat="server"></asp:Label></span></div>
                        </div>
                        <div class="divTableRow">
                        <div class="divTableCell">Unit #4</div>
                        <div class="divTableCell"><span id="ContentPlaceHolder1_lbTotaldv"></span><asp:Label ID="Label10v" runat="server"></asp:Label></span></div>
                        </div>
                        <div class="divTableRow" style = "font-weight: bold;">
                        <div class="divTableCell">Total</div>
                        <div class="divTableCell"><span id="ContentPlaceHolder1_lbTotalv"><asp:Label ID="lbTotalv" runat="server" Text="Loading.."></asp:Label></span></div>
                        </div>
                        <div class="divTableRow" style ="font-weight: bold;">
                        <div class="divTableCell">Frequency</div>
                        <div class="divTableCell"><span id="ContentPlaceHolder1_Label8v"><asp:Label ID="Label8v" runat="server"></asp:Label></span></div>
                        </div>
                        </div>
                        </div>
                   
                   </div>
                    <asp:HiddenField ID="HiddenField1" runat="server" Visible="False" Value="0" />
                    <asp:HiddenField ID="HiddenField2" runat="server" Visible="False" />
                    <asp:HiddenField ID="HiddenField3" runat="server" Visible="False" Value="0" />
                    <asp:HiddenField ID="HiddenField4" runat="server" Visible="False" />
                    <asp:HiddenField ID="HiddenField5" runat="server" Visible="False" Value="0" />
                    <asp:HiddenField ID="HiddenField6" runat="server" Visible="False" />
                            <asp:HiddenField ID="HiddenField7" runat="server" Visible="False" Value="0" />
                                <asp:HiddenField ID="HiddenField8" runat="server" Visible="False" />
                                <asp:Timer ID="Timer24" runat="server" Enabled="False" Interval="1000"></asp:Timer>
                                <asp:GridView ID="gvStatus" runat="server"   Visible="False" >
                                    <EmptyDataTemplate><div>No Data Available</div></EmptyDataTemplate></asp:GridView>
                </ContentTemplate>

            </asp:UpdatePanel>
          

        </div>


    </div>




</asp:Content>
