<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="department-info-update.aspx.cs" Inherits="Admin_department_info_update" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="admin_breadcrumb">
        Department > Information
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
                                    <h3 class="panel-title"><strong>Add/Update</strong> Department Information</h3>
                                </div>
                                <div class="panel-body">

                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hdfDept_Details_Id" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfDeptId" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfEmpployee_Id" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfImage_UploadedPath" runat="server" Value="No" />
                                                <asp:HiddenField ID="hdfRetrieveImageFile" runat="server" Value="No" />
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">                                                           
                                                              
                                                            <!-- ENTER Description English -->
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">About Dept. (English)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                         <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtDescriptionEnglish" runat="server"  class="form-control" TextMode="MultiLine" MaxLength="160"></asp:TextBox>
                                                                     <%--   <asp:HtmlEditorExtender ID="HtmlEditorExtender1" runat="server" TargetControlID="txtDescriptionEnglish">
                                                                        </asp:HtmlEditorExtender>--%>
                                                                    </div>
                                                                </div>
                                                               <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtDescriptionEnglish" runat="server" ErrorMessage="About Dept. cannot be blank" ForeColor="Red" ControlToValidate="txtDescriptionEnglish" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <!-- ENTER Description Hindi -->
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">About Dept. (Hindi)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                         <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtDescriptionHindi" runat="server"  class="form-control" TextMode="MultiLine" MaxLength="160"></asp:TextBox>
                                                                       <%-- <asp:HtmlEditorExtender ID="HtmlEditorExtendertxtDescriptionHindi" runat="server" TargetControlID="txtDescriptionHindi">
                                                                        </asp:HtmlEditorExtender>--%>
                                                                    </div>
                                                                </div>
                                                               <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtDescriptionHindi" runat="server" ErrorMessage="About Dept. cannot be blank" ForeColor="Red" ControlToValidate="txtDescriptionHindi" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>  
                                                            
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">More Info(In English)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>                                                                        
                                                                        <cc1:Editor ID="txtTextEnglish" runat="server" Height="200px" Visible="true" class="form-control"/>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">More Info(In Hindi)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>                                                                        
                                                                        <cc1:Editor ID="txtTextHindi" runat="server" Height="200px" Visible="true" class="form-control"/>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                             
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center" style="padding-top:20px;"> <asp:Button ID="btnSave" class="btn btn-primary pull-center" runat="server" Text="Submit" OnClick="btnSave_Click" /></td>
                                                    </tr>
                                                </table>
                                                <!--Error Message-->
                                                <div id="alert" runat="server">
                                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                                </div>
                                                <br />
                                                <div class="panel-footer" style="display:none;">
                                                    <asp:Button ID="btnClear" Visible="false" CausesValidation="false" class="btn btn-default" runat="server" Text="Close Form" OnClick="btnClose_Click" />
                                                   
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
        <!-- Inner Content End -->
    </div>
</asp:Content>

