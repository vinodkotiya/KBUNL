<%@ Page Language="C#" AutoEventWireup="true" CodeFile="feedback.aspx.cs" Inherits="feedback" %>

<%@ Register Src="~/English/WCCommon/WcHeadTags.ascx" TagPrefix="uc1" TagName="WcHeadTags" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,initial-scale=1">
    <title>Feedback</title>
    <!--Header - Start-->
    <uc1:WcHeadTags runat="server" ID="WcHeadTags" />
    <!--Header - End-->
</head>
<body>
    <form id="form1" runat="server">
        <div class="suggestionpagecontainer">
            <div class="suggestionform">
                <div class="innercontent">
                    <h4>Feedback Form</h4>
                    <div class="forminputdiv">
                        <div class="userinput">
                            <div class="inputcontrols">
                                <asp:TextBox ID="txtName" runat="server" CssClass="inputusername" placeholder="Name" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ErrorMessage="Please Enter Your Name" ControlToValidate="txtName" CssClass="validator"></asp:RequiredFieldValidator>
                            </div>
                            <div class="inputcontrols">
                                <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="inputusername" placeholder="Phone No" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtPhoneNo" runat="server" ErrorMessage="Please Enter Phone No" ControlToValidate="txtPhoneNo" CssClass="validator"></asp:RequiredFieldValidator>
                            </div>
                            <div class="inputcontrols">
                                <asp:TextBox ID="txtEmpCode" runat="server" CssClass="inputusername" placeholder="Employee Code" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtEmpCode" runat="server" ErrorMessage="Please Enter Employee Code" ControlToValidate="txtEmpCode" CssClass="validator"></asp:RequiredFieldValidator>
                            </div>
                            <div class="inputcontrols" style="font-family:Roboto;">
                                <asp:TextBox ID="txtSuggestion" runat="server" CssClass="inputusername" placeholder="Suggestion/Feedback" TextMode="MultiLine" Height="80px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtSuggestion" runat="server" ErrorMessage="Please Enter Your Suggestion/Feedback" ControlToValidate="txtSuggestion" CssClass="validator"></asp:RequiredFieldValidator>
                            </div>
                            <table style="width:100%;">
                                <tr>
                                    <td><asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="button" OnClick="btnSubmit_Click" /></td>
                                     <td><a href="Default.aspx" class="btnhome">Home</a></td>
                                </tr>
                            </table>
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
