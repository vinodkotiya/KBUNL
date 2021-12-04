<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WCTopBarDept.ascx.cs" Inherits="WCCommon_WCTopBar" %>
<style type="text/css">
    .switch {position: relative;display: inline-block;width:50px;height: 19px;}
    .switch input {display:none;}
    .slider {position: absolute;cursor: pointer;top: 0;left: 0;right: 0;bottom: 0;background-color: #f0f0f0;-webkit-transition:.4s;transition:.4s;}
    .slider:before {position: absolute;content: "";height: 19px;width: 19px;left: 0px;bottom: 0px;background-color:#e9bc0c;-webkit-transition:.4s;transition:.4s;}
    input:checked + .slider {background-color: #ffffff;}
    input:focus + .slider {box-shadow: 0 0 1px #2196F3;}
    input:checked + .slider:before {-webkit-transform: translateX(31px);-ms-transform: translateX(31px);transform: translateX(31px);}
    /* Rounded sliders */
    .slider.round {border-radius: 19px;}
    .slider.round:before {border-radius: 50%;}    
</style>
<div class="topbar">
    <div class="container">
        <div class="themebox">
            <div class="fontsizes">
                <table cellpadding="0" cellspacing="0" class="tbl">
                    <tr>
                        <td><span class="fontsmaller" onclick="zoom_smaller()">A</span></td>
                        <td><span class="fontnormal" onclick="zoom_normal()">A</span></td>
                        <td><span class="fontlarger" onclick="zoom_larger()">A</span></td>
                    </tr>
                </table>             
            </div>
            <div class="themes">
                <asp:LinkButton ID="lbtn_themeblue" runat="server" CssClass="themebtn themebtn_blue" OnClick="lbtn_themeblue_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtn_themegreen" runat="server" CssClass="themebtn themebtn_green" OnClick="lbtn_themegreen_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtn_themeblack" runat="server" CssClass="themebtn themebtn_purple" OnClick="lbtn_themeblack_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtn_themeorange" runat="server" CssClass="themebtn themebtn_orange" OnClick="lbtn_themeorange_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtn_themered" runat="server" CssClass="themebtn themebtn_red" OnClick="lbtn_themered_Click"></asp:LinkButton>
            </div>
        </div>
        <ul>
            <li class="login"><a onclick="document.getElementById('id01').style.display='block'" style="cursor:pointer;">Login</a></li>
            <%--<li class="search-icon">
                <asp:TextBox ID="TextBox1" runat="server" placeholder="Search.." CssClass="searchbox"></asp:TextBox>
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="search-button"></asp:LinkButton>
            </li>--%>
            <li class="language">
                <div class="language com" style="position:relative; color:#ffffff;">
                    <div style="width:60px;float:left; padding-right:4px;">English</div>
                    <div style="width:50px; float:left;">
                        <label class="switch" style=" margin:0px;"> 
                            <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true"/> 
                            <div class="slider round"></div> 
                        </label>
                    </div>
                    <div style="width:42px; float: left; text-align: right;">Hindi</div>
                    <div style="clear:both;"></div>
                </div>
            </li>
            <li><a href="Default.aspx" class="backtohome" style="padding:3px 5px;">SRHQ Home</a></li>
        </ul>
    </div>
    <div id="id01" class="modalbox">
        <div class="modal-content">
            <div class="imgcontainer">
                <span onclick="document.getElementById('id01').style.display='none'" class="closeBox" title="Close Modal">&times;</span>
            </div>
            <asp:UpdatePanel ID="updatepanel1" runat="server">
            <ContentTemplate>
            <h3>Login Form</h3>
            <div class="feedback-form">
                <div class="inputbox">
                    <asp:TextBox ID="txtUserName" placeholder="User Name" CssClass="input-design" MaxLength="50" runat="server"></asp:TextBox>
                </div>
                <div class="inputbox">
                    <asp:TextBox ID="txtPassword" TextMode="Password" placeholder="Password" CssClass="input-design" MaxLength="50" runat="server"></asp:TextBox>
                </div>
                <div class="submit-button" style="margin-bottom: 10px;">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="button" OnClick="btnLogin_Click" />
                </div>
                <asp:LinkButton ID="lnkForgotPwd" runat="server" OnClick="lnk_forgotPwd_Click" class="forgot-link">Forgot Password?</asp:LinkButton>

                <div style="text-align: center;">
                    <asp:Label ID="lblSugMsg" runat="server" Text=""></asp:Label>
                </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="100">
                <ProgressTemplate>
                    <asp:Panel ID="Pnl1" CssClass="overlay" runat="server" meta:resourcekey="Pnl1Resource1">
                        <asp:Panel ID="Pnl2" CssClass="loader" runat="server" meta:resourcekey="Pnl2Resource1">
                            <img alt="" src="images/p1.gif" />
                        </asp:Panel>
                    </asp:Panel>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>

        <table class="colorful-stripe" cellpadding="0" cellspacing="0" border="0" style="bottom: 0px;">
            <tr>
                <td class="yellow"></td>
                <td class="green"></td>
                <td class="blue"></td>
                <td class="pink"></td>
                <td class="yellow"></td>
                <td class="green"></td>
                <td class="pink"></td>
            </tr>
        </table>
    </div>
</div>
<script type="text/javascript">
    // Get the modal
    var modalbox = document.getElementById('id01');

    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target == modalbox) {
            modalbox.style.display = "none";
        }
    }
</script>
