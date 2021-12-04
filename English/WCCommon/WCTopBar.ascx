<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WCTopBar.ascx.cs" Inherits="WCCommon_WCTopBar" %>
	<!--Header - Start-->
			<div class="header">
                 <asp:HiddenField ID="hdfMedium" runat="server" Value="" />
				<table cellpadding="0" cellspacing="0">
					<tr>
						<td class="logobox">
							<a href="../Default.aspx">
								<img src="../images/ntpc-logo.png" alt="ntpc-logo" class="logo" style="vertical-align:top;"/>
								<img src="../images/kbunl.jpg" alt="kbunl-logo" class="logo" style="vertical-align:top;"/>
							</a>
							<div class="direcrorysearch">
								<asp:TextBox ID="txtDirectorySearch" runat="server" placeholder="Directory Search" ValidationGroup="search"></asp:TextBox>
								<asp:Button ID="btnDirectorySearch" runat="server" Text="Search" OnClick="btnDirectorySearch_Click" CssClass="btn" ValidationGroup="search" AllowFocus="false"/>
							</div>
							<div style="text-align:left; font-size:14px; color:#6b6b6b; padding-top:5px;">
								<span style="border-radius: 15px 50px;    padding-left: 10px;    padding-right: 10px; color: #f7f7f7;    font-weight: 600;    background-color: #1565b2">Date & Time: <asp:label ID="lblDateTime" runat="server" Text=""></asp:label></span>
							</div>
						</td>
						<td class="projectbox" id="divProjectInformation" runat="server">                           
						</td>
						<td class="linkbox">							
                            <div class="themes">      
                                <asp:LinkButton ID="lbtn_themeblue" runat="server" CssClass="themebtn themebtn_blue" OnClick="lbtn_themeblue_Click"></asp:LinkButton>                             
                                <asp:LinkButton ID="lbtn_themered" runat="server" CssClass="themebtn themebtn_red" OnClick="lbtn_themered_Click"></asp:LinkButton>
                                <asp:LinkButton ID="lbtn_themegreen" runat="server" CssClass="themebtn themebtn_green" OnClick="lbtn_themegreen_Click"></asp:LinkButton>
                                <asp:LinkButton ID="lbtn_themeorange" runat="server" CssClass="themebtn themebtn_orange" OnClick="lbtn_themeorange_Click"></asp:LinkButton>                                
                                <asp:LinkButton ID="lbtn_themepurple" runat="server" CssClass="themebtn themebtn_purple" OnClick="lbtn_themepurple_Click"></asp:LinkButton>								
                            </div>
							<div class="toplinks">								
								<a href="../login.aspx" class="login" target="_blank">Login</a>
                                हिंदी
                                <asp:LinkButton ID="lnkbtnMedium" runat="server" OnClick="lnkbtnMedium_Click" ><asp:Image ID="imgMedium" runat="server" ImageUrl="../images/toggle-english.png" /></asp:LinkButton>
                                English
							</div>
							<div class="bottomlinks">
								<a href="download.aspx">Downloads</a>
								<!--	<a href="reports.aspx">Reports</a>-->
								<a href="search.aspx">Archive</a>
							</div>
							<div style="text-align:right; font-size:14px; color:#6b6b6b; padding-top:5px;">
								<span style="border-radius: 15px 50px;    padding-left: 10px;    padding-right: 10px;color: #f7f7f7;    font-weight: 600;    background-color: #1565b2">IP Address: <asp:label ID="lblIPAddress" runat="server" Text=""></asp:label></span>
							</div>
						</td>
					</tr>
				</table>
			</div>
			<!--Header - End-->
            <!--MenuBar - Start-->
			<div class="menubar">
                <asp:Literal ID="ltrMenu" runat="server"></asp:Literal>
			</div>
			<!--MenuBar - End-->






