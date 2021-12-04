<%@ Page Language="C#" AutoEventWireup="true" CodeFile="feedback.aspx.cs" Inherits="feedback" %>
<%@ Register Src="~/Hindi/WCCommon/WcHeadTags.ascx" TagPrefix="uc1" TagName="WcHeadTags" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,initial-scale=1">
    <title>फीडबैक</title>
    <!--Header - Start-->
    <uc1:WcHeadTags runat="server" ID="WcHeadTags" />
    <!--Header - End-->
</head>
<body>
    <form id="form1" runat="server">
        <div class="suggestionpagecontainer">
            <div class="suggestionform">
                <div class="innercontent">
                    <h4>फीडबैक फार्म</h4>
                    <div class="forminputdiv">
                        <div class="userinput">
                            <div class="inputcontrols">
                                <asp:TextBox ID="txtName" runat="server" CssClass="inputusername" placeholder="नाम" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ErrorMessage="कृपया नाम दर्ज करें" ControlToValidate="txtName" CssClass="validator"></asp:RequiredFieldValidator>
                            </div>
                            <div class="inputcontrols">
                                <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="inputusername" placeholder="फोन नंबर" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtPhoneNo" runat="server" ErrorMessage="कृपया फोन नंबर दर्ज करें" ControlToValidate="txtPhoneNo" CssClass="validator"></asp:RequiredFieldValidator>
                            </div>
                            <div class="inputcontrols">
                                <asp:TextBox ID="txtEmpCode" runat="server" CssClass="inputusername" placeholder="कर्मचारी कोड" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtEmpCode" runat="server" ErrorMessage="कृपया कर्मचारी कोड दर्ज करें" ControlToValidate="txtEmpCode" CssClass="validator"></asp:RequiredFieldValidator>
                            </div>
                            <div class="inputcontrols">
                                <asp:TextBox ID="txtSuggestion" runat="server" CssClass="inputusername" placeholder="सुझाव" TextMode="MultiLine" Height="80px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtSuggestion" runat="server" ErrorMessage="कृपया अपना सुझाव दर्ज करें" ControlToValidate="txtSuggestion" CssClass="validator"></asp:RequiredFieldValidator>
                            </div>
                            <table style="width:100%;">
                                <tr>
                                    <td><asp:Button ID="btnSubmit" runat="server" Text="सबमिट" CssClass="button" OnClick="btnSubmit_Click" /></td>
                                    <td><a href="Default.aspx" class="btnhome">होम</a></td>
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
