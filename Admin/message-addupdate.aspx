<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="message-addupdate.aspx.cs" Inherits="Admin_news_post" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        table { border-spacing: 5px; border-collapse: separate !important; }

        td { vertical-align: top; min-width: 120px; padding: 10px 10px; }

        td input[type=text] { width: 200px; }

        td textarea { width: 400px; }
    </style>
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace

        specialKeys.push(9); //Tab
        specialKeys.push(46); //Delete
        specialKeys.push(36); //Home
        specialKeys.push(35); //End
        specialKeys.push(37); //Left
        specialKeys.push(39); //Right
        specialKeys.push(191);

        function IsText(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 126) || keyCode == 32 || keyCode == 9 || keyCode == 8 || keyCode == 46 || specialKeys.indexOf(keyCode) != -1);
            return ret;
        }

        function IsPhone(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || keyCode == 9 || keyCode == 8 || keyCode == 45 || specialKeys.indexOf(keyCode) != -1);
            return ret;
        }
        function IsAlphaNumeric(e) {
            var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
            var ret = ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || keyCode == 32 || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
            return ret;
        }
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            return ret;
        }
        function IsDate(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            return ret;
        }
    </script>
    <div class="admin_breadcrumb" id="div_headTitle" runat="server">
    </div>
    <div class="admin_rhs_content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdfDept_Id" runat="server" Value="0" />



                <div class="page-content-wrap">
                    <div class="col-md-12 innerpage">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title"><strong>Message of First Person</strong></h3>
                            </div>
                            <div class="panel-body">
                                <table cellpadding="5" cellspacing="5px;">
                                    <tr>
                                        <td>Name (English)<span style="color: red;">*</span></td>
                                        <td style="position: relative;">
                                            <asp:TextBox ID="txtNameEnglish" class="form-control" onkeypress="return IsText(event)" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                                            <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -10px; padding-left: 5px; font-size: 13px; color: red;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtName" runat="server" ErrorMessage="Name cannot be blank" ForeColor="Red" ControlToValidate="txtNameEnglish" CssClass="validator"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                        <td>Name (Hindi)<span style="color: red;">*</span></td>
                                        <td style="position: relative;">
                                            <asp:TextBox ID="txtNameHindi" class="form-control" onkeypress="return IsText(event)" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                                            <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -10px; padding-left: 5px; font-size: 13px; color: red;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtNameHindi" runat="server" ErrorMessage="Name cannot be blank" ForeColor="Red" ControlToValidate="txtNameHindi" CssClass="validator"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Designation (English)<span style="color: red;">*</span></td>
                                        <td style="position: relative;">
                                            <asp:TextBox ID="txtDesignationE" class="form-control" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                                            <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -10px; padding-left: 5px; font-size: 13px; color: red;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtDesignation" runat="server" ErrorMessage="Designation cannot be blank" ForeColor="Red" ControlToValidate="txtDesignationE" CssClass="validator"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                        <td>Designation (Hindi)<span style="color: red;">*</span></td>
                                        <td style="position: relative;">
                                            <asp:TextBox ID="txtDesignationH" class="form-control" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                                            <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -10px; padding-left: 5px; font-size: 13px; color: red;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtDesignationH" runat="server" ErrorMessage="Designation cannot be blank" ForeColor="Red" ControlToValidate="txtDesignationH" CssClass="validator"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Message (English)<span style="color: red;">*</span></td>
                                        <td style="position: relative;">
                                            <asp:TextBox ID="txtMsgEng" class="form-control" runat="server" Height="150" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                            <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -10px; padding-left: 5px; font-size: 13px; color: red;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtMsgEng" runat="server" ErrorMessage="Message(English) cannot be blank" ForeColor="Red" ControlToValidate="txtMsgEng" CssClass="validator"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                        <td>Message (Hindi)<span style="color: red;">*</span></td>
                                        <td style="position: relative;">
                                            <asp:TextBox ID="txtMsgHin" class="form-control" runat="server" Height="150" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                            <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -10px; padding-left: 5px; font-size: 13px; color: red;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtMsgHin" runat="server" ErrorMessage="Message(Hindi) cannot be blank" ForeColor="Red" ControlToValidate="txtMsgHin" CssClass="validator"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Image<span style="color: red;">*</span></td>
                                        <td colspan="3" style="position: relative;">
                                            <p>
                                                <asp:Image ID="imgProfile" runat="server" Style="max-width: 100px;" />
                                            </p>
                                            <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -10px; padding-left: 5px; font-size: 13px; color: red;">
                                                <asp:RequiredFieldValidator ID="RFV_ProfilePhoto" runat="server" ErrorMessage="Please Select Profile Photo" ForeColor="Red" ControlToValidate="FileUpload1" CssClass="validator"></asp:RequiredFieldValidator>
                                            </div>
                                            <asp:FileUpload ID="FileUpload1" runat="server" Height="24px" />
                                            <asp:HiddenField ID="hdfPhoto" runat="server" Value="No" />
                                            <p>
                                                <asp:Label ID="Label1" runat="server" Text="Note: Recommend Dimension (150*150)"></asp:Label>
                                            </p>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td colspan="3">
                                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" Width="87px" class="btn btn-primary" />
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="84px" OnClick="btnUpdate_Click" class="btn btn-primary" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td colspan="3">
                                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="HDN_EventID" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSubmit" />
                <asp:PostBackTrigger ControlID="btnUpdate" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

