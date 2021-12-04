<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="about-us-content.aspx.cs" Inherits="Admin_about_us_content" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <style type="text/css">
        table {
            border-spacing: 5px;
            border-collapse: separate !important;
        }

        td {
            vertical-align: top;
            min-width: 120px;
            padding: 10px 10px;
        }

            td input[type=text] {
                width: 200px;
            }

            td textarea {
                width: 400px;
            }
    </style>
    <div class="admin_breadcrumb" id="div_headTitle" runat="server">
       
    </div>
    <div class="admin_rhs_content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                 <asp:HiddenField ID="hdfDept_Id" runat="server" Value="0" />
                <table cellpadding="5" cellspacing="5px;">
                    <tr>
                        <td>About Us (English)<span style="color:red;">*</span></td>
                        <td style="position:relative;" colspan="3">
                            <cc1:Editor ID="txtAboutUsE" runat="server" Height="200px" Visible="true" class="form-control"/>
                            <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -10px; padding-left:5px; font-size: 13px; color: red;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtAboutUsE" runat="server"  ErrorMessage="About us cannot be blank" ForeColor="Red" ControlToValidate="txtAboutUsE" CssClass="validator" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>About Us (Hindi)<span style="color:red;">*</span></td>
                        <td style="position:relative;" colspan="3">
                            <cc1:Editor ID="txtAboutUsH" runat="server" Height="200px" Visible="true" class="form-control" />
                             <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -10px; padding-left:5px; font-size: 13px; color: red;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtMsgHin" runat="server"  ErrorMessage="About us cannot be blank" ForeColor="Red" ControlToValidate="txtAboutUsH" CssClass="validator" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>                     
                </table>
                <h4 style="background-color:#efefef; padding:5px 10px;">How to Reach</h4>
                <table cellpadding="5" cellspacing="5">
                    <tr>
                        <td>Address (English)<span style="color:red;">*</span></td>
                        <td style="position:relative;">
                            <asp:TextBox ID="txtAddresssE" class="form-control" runat="server" TextMode="MultiLine" Width="360px"></asp:TextBox>
                            <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -10px; padding-left:5px; font-size: 13px; color: red;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtAddresssE" runat="server"  ErrorMessage="Address cannot be blank" ForeColor="Red" ControlToValidate="txtAddresssE" CssClass="validator" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                        <td>Address (Hindi)<span style="color:red;">*</span></td>
                        <td style="position:relative;">
                            <asp:TextBox ID="txtAddressH" class="form-control" runat="server" TextMode="MultiLine" Width="360px"></asp:TextBox>
                             <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -10px; padding-left:5px; font-size: 13px; color: red;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtAddressH" runat="server"  ErrorMessage="Address cannot be blank" ForeColor="Red" ControlToValidate="txtAddressH" CssClass="validator" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>By Flight (English)<span style="color:red;">*</span></td>
                        <td style="position:relative;">
                            <asp:TextBox ID="txtFliteE" class="form-control" runat="server" Width="360px"></asp:TextBox>
                            <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -10px; padding-left:5px; font-size: 13px; color: red;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtFliteE" runat="server"  ErrorMessage="Flite cannot be blank" ForeColor="Red" ControlToValidate="txtFliteE" CssClass="validator" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                        <td>By Flight (Hindi)<span style="color:red;">*</span></td>
                        <td style="position:relative;">
                            <asp:TextBox ID="txtFliteH" class="form-control" runat="server" Width="360px"></asp:TextBox>
                             <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -10px; padding-left:5px; font-size: 13px; color: red;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtFliteH" runat="server"  ErrorMessage="Flite cannot be blank" ForeColor="Red" ControlToValidate="txtFliteH" CssClass="validator" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>By Road (English)<span style="color:red;">*</span></td>
                        <td style="position:relative;">
                            <asp:TextBox ID="txtRoadE" class="form-control" runat="server" Width="360px"></asp:TextBox>
                            <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -10px; padding-left:5px; font-size: 13px; color: red;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtRoadE" runat="server"  ErrorMessage="Road cannot be blank" ForeColor="Red" ControlToValidate="txtRoadE" CssClass="validator" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                        <td>By Road (Hindi)<span style="color:red;">*</span></td>
                        <td style="position:relative;">
                            <asp:TextBox ID="txtRoadH" class="form-control" runat="server" Width="360px"></asp:TextBox>
                             <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -10px; padding-left:5px; font-size: 13px; color: red;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtRoadH" runat="server"  ErrorMessage="Road cannot be blank" ForeColor="Red" ControlToValidate="txtFliteH" CssClass="validator" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                     <tr>
                        <td>By Train (English)<span style="color:red;">*</span></td>
                        <td style="position:relative;">
                            <asp:TextBox ID="txtTrainE" class="form-control" runat="server" Width="360px"></asp:TextBox>
                            <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -10px; padding-left:5px; font-size: 13px; color: red;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtTrainE" runat="server"  ErrorMessage="Train cannot be blank" ForeColor="Red" ControlToValidate="txtTrainE" CssClass="validator" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                        <td>By Train (Hindi)<span style="color:red;">*</span></td>
                        <td style="position:relative;">
                            <asp:TextBox ID="txtTrainH" class="form-control" runat="server" Width="360px"></asp:TextBox>
                             <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -10px; padding-left:5px; font-size: 13px; color: red;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtTrainH" runat="server"  ErrorMessage="Train cannot be blank" ForeColor="Red" ControlToValidate="txtTrainH" CssClass="validator" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Project Images<span style="color:red;">*</span>
                            <br /><small>jpg, gif and png extensions supported</small>
                        </td>
                        <td style="position:relative;">
                            <asp:FileUpload ID="fupload1" runat="server" Height="24px" />
                            <span style="padding-left:5px;font-size: 13px;color:red;"> <asp:RequiredFieldValidator ID="RequiredFieldValidatorfUpload1" runat="server" ErrorMessage="Please upload photo"   ControlToValidate="fupload1" CssClass="validator" ValidationGroup="b"></asp:RequiredFieldValidator></span>
                            <asp:HiddenField ID="hdfImg1" runat="server" Value="NA" />
                           
                            <asp:FileUpload ID="fUpload2" runat="server" Height="24px" />
                            <asp:HiddenField ID="hdfImg2" runat="server" Value="NA" />
                            <span style="padding-left:5px;font-size: 13px;color:red;"> <asp:RequiredFieldValidator ID="RequiredFieldValidatorfUpload2" runat="server" ErrorMessage="Please upload photo" ControlToValidate="fUpload2" CssClass="validator" ValidationGroup="b"></asp:RequiredFieldValidator></span>

                            <asp:FileUpload ID="fupload3" runat="server" Height="24px" />
                            <asp:HiddenField ID="hdfImg3" runat="server" Value="NA" />
                            <span style="padding-left:5px;font-size: 13px;color:red;"> <asp:RequiredFieldValidator ID="RequiredFieldValidatorfupload3" runat="server" ErrorMessage="Please upload photo" ControlToValidate="fupload3" CssClass="validator" ValidationGroup="b"></asp:RequiredFieldValidator></span>
                        </td>
                       <td>Uploaded Image</td>
                        <td > <p>
                            <asp:Image ID="img1" runat="server" Style="max-width: 100px;" ImageUrl="~/Admin/images/default-photo.png" />
                            <asp:Image ID="img2" runat="server" Style="max-width: 100px;" ImageUrl="~/Admin/images/default-photo.png" />
                            <asp:Image ID="img3" runat="server" Style="max-width: 100px;" ImageUrl="~/Admin/images/default-photo.png" />
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>Map URL</td>
                        <td colspan="3"><asp:TextBox ID="txtLinkURL" class="form-control" runat="server" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3">
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" Width="87px" class="btn btn-primary"  />
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
                <asp:HiddenField ID="HDN_EventID" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSubmit" />
                <asp:PostBackTrigger ControlID="btnUpdate" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

