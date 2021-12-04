<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login2" %>
<link href="css/MainStyleSheet.css" rel="stylesheet" />
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,initial-scale=1">
    <title>Login</title>
    <!--Header - Start-->
    <%--<uc1:WcHeadTags runat="server" ID="WcHeadTags" />--%>
    <!--Header - End-->
</head>
<body>
    <form id="form1" runat="server">
        <div class="loginpagecontainer">
            <div class="loginform">
                <a href="../Default.aspx">
                    <img src="../images/ntpc-logo.png" alt="ntpc-logo" style="width: 90px;" class="logo" style="vertical-align:top;">
                    <img src="../images/kbunl.jpg" alt="kbunl-logo" style="width: 90px;"  class="logo" style="vertical-align:top;">
                </a>
                <div class="innercontent">
                    <h4>Login Form</h4>
                    <div class="forminputdiv">
                        <div class="userinput">
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="inputusername" placeholder="User Name"></asp:TextBox>

                            <asp:TextBox ID="txtPassword" runat="server" CssClass="inputusername" placeholder="Password" TextMode="Password"></asp:TextBox>
                         <!-- <h5 style="color:red">Today We have locked the login for maintenance of Intranet. Please try after 6:00 PM</h5> -->
                            <asp:Button ID="btnLogin"  runat="server" Text="Login" CssClass="button" OnClick="btnLogin_Click" />
                             <asp:Button ID="btnForgotPwdSubmit" runat="server" Text="Submit" CssClass="button" OnClick="btnForgotPwdSubmit_Click" Visible="false" />
                            <asp:LinkButton ID="lnkbtnCancel" runat="server" Text="Cancel" OnClick="lnkbtnCancel_Click" Visible="false"></asp:LinkButton>
                            <br />
                            <asp:LinkButton ID="lbkbtnForgotPassword" runat="server" Text="Forgot Password?" OnClick="lbkbtnForgotPassword_Click"></asp:LinkButton>
                            
                        </div>
                    </div>
                </div>
            </div>
            <div style="text-align: center;">
                <asp:Label ID="lblSugMsg" runat="server" Text=""></asp:Label>
                <span runat="server" id="pwdDcrpt"></span>
            </div>
        </div>
    </form>
</body>
</html>
