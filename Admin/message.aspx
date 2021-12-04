<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="message.aspx.cs" Inherits="Admin_news_post" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .form-horizontal table { border-spacing: 5px; border-collapse: separate !important; }
        .form-horizontal td { vertical-align: top; min-width: 120px; padding: 10px 10px; }
        .form-horizontal td input[type=text] { width: 200px; }
        .form-horizontal td textarea { width: 400px; }
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
        <!-- PAGE CONTENT WRAPPER -->
        <div class="page-content-wrap">
            <div class="col-md-12 innerpage">

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="panelAddNew" runat="server">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong>Add/Update</strong> Messages</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hdfDept_Id" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfMsgID" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfImage_UploadedPath" runat="server" Value="No" />
                                                <table width="100%" cellpadding="0" cellspacing="10">
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
                                                        <td>Message (English)</td>
                                                        <td style="position: relative;">
                                                            <asp:TextBox ID="txtMsgEng" class="form-control" runat="server" Height="150" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                                        </td>
                                                        <td>Message (Hindi)</td>
                                                        <td style="position: relative;">
                                                            <asp:TextBox ID="txtMsgHin" class="form-control" runat="server" Height="150" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>                                                        
                                                        <td>Image<span style="color: red;">*</span></td>
                                                        <td style="position: relative;">
                                                            <p>
                                                                <asp:Image ID="imgProfile" runat="server" Style="max-width: 100px;" />
                                                            </p>
                                                            <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -10px; padding-left: 5px; font-size: 13px; color: red;">
                                                                <asp:RequiredFieldValidator ID="RFV_ProfilePhoto" runat="server" ErrorMessage="Please Select Profile Photo" ForeColor="Red" ControlToValidate="FileUpload1" CssClass="validator"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <asp:FileUpload ID="FileUpload1" runat="server" Height="24px" />
                                                            <asp:HiddenField ID="hdfPhoto" runat="server" Value="No" />
                                                            <p>
                                                                <small>Only Image (jpg, gif and png extensions supported) :: Size (150 x 150)</small>
                                                            </p>
                                                        </td>
                                                    </tr>
                                                    <tr>                                                        
                                                        <td>HOP Message (Attachment)</td>
                                                        <td style="position: relative;">
                                                            <asp:FileUpload ID="fileUploadMessage" runat="server" Height="24px" />
                                                            <asp:HiddenField ID="hdfuploadmessage" runat="server" Value="No" />                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>                                                        
                                                        <td>HOP Schedule (Attachment)</td>
                                                        <td style="position: relative;">
                                                            <asp:FileUpload ID="fileUploadSchedule" runat="server" Height="24px" />
                                                            <asp:HiddenField ID="hdfuploadschedule" runat="server" Value="No" />                                                            
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!--Error Message-->
                                                <div id="alert" runat="server">
                                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                                </div>
                                                <br />
                                                <div class="panel-footer">
                                                    <asp:Button ID="btnClear" CausesValidation="false" class="btn btn-default" runat="server" Text="Close Form" OnClick="btnClose_Click" />
                                                    <asp:Button ID="btnSave" class="btn btn-primary pull-right" runat="server" Text="Submit" OnClick="btnSave_Click" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSave" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>


                        </asp:Panel>
                        <asp:Panel ID="panelView" runat="server">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-right" Style="background-color: rgba(199, 15, 15, 0.9)">Add New Message</asp:LinkButton>
                                </div>
                                <div class="panel-body panel-body-table">

                                    <asp:GridView ID="gridMessages" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="MessageID" Visible="false" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMsgID" runat="server" Text='<%#Eval("MsgID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Profile Image" HeaderStyle-Width="100px">
                                                <ItemTemplate>
                                                    <img src='../<%#Eval("PhotoPath") %>' style="width: 100%; width: 100px;" alt="" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name" HeaderStyle-Width="150px">
                                                <ItemTemplate>
                                                    <%#Eval("Name") %><br />
                                                    <%#Eval("NameH") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation" HeaderStyle-Width="150px">
                                                <ItemTemplate>
                                                    <%#Eval("Designation") %><br />
                                                    <%#Eval("DesignationH") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Message">
                                                <ItemTemplate>
                                                    <%#Eval("MessageEnglish") %><br />
                                                    <%#Eval("MessageHindi") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>  
                                            <asp:TemplateField HeaderText="MessageFile" HeaderStyle-Width="100px">
                                                <ItemTemplate>
                                                    <div id="divmessagefile" runat="server" visible='<%#Eval("MessageAttachment").ToString()!="" %>' style="text-align:center">
                                                        <a href='<%#Eval("MessageAttachment_Admin") %>' target="_blank">View</a>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>  
                                            <asp:TemplateField HeaderText="ScheduleFile" HeaderStyle-Width="100px">
                                                <ItemTemplate>
                                                    <div id="divschedulefile" runat="server" visible='<%#Eval("ScheduleAttachment").ToString()!="" %>' style="text-align:center">
                                                        <a href='<%#Eval("ScheduleAttachment_Admin") %>' target="_blank">View</a>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>  
                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("MsgID") %>' OnClick="Update_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                           
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <!--Error Message-->
                            <div id="alertgrid" runat="server">
                                <asp:Label ID="lblgridmsg" runat="server" Text=""></asp:Label>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>

                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="100">
                    <ProgressTemplate>
                        <asp:Panel ID="Pnl1" CssClass="overlay" runat="server" meta:resourcekey="Pnl1Resource1">
                            <asp:Panel ID="Pnl2" CssClass="loader" runat="server" meta:resourcekey="Pnl2Resource1">
                                <img alt="" src="../images/p1.gif" />
                            </asp:Panel>
                        </asp:Panel>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
        <!-- END PAGE CONTENT WRAPPER -->
    </div>
</asp:Content>

