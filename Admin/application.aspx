<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="application.aspx.cs" Inherits="Admin_application" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                    <h3 class="panel-title"><strong>Add/Update</strong>  Application</h3>
                                </div>
                                <div class="panel-body">

                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hdfDept_Id" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfApplicationId" runat="server" Value="0" />
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">
                                                            <!-- ENTER Title -->
                                                            <div class="form-group" style="position: relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Application Name (English)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtApplicationNameE" placeholder="Enter Application Name" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -15px; padding-left: 315px; font-size: 13px; color: red;">
                                                                    <asp:RequiredFieldValidator ID="RFV_txtApplicationNameE" runat="server" Display="Dynamic" ErrorMessage="Application name cannot be blank" ForeColor="Red" ControlToValidate="txtApplicationNameE"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Application Name (Hindi)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtApplicationNameH" placeholder="Enter Application Name" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -15px; padding-left: 315px; font-size: 13px; color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtApplicationNameH" runat="server" Display="Dynamic" ErrorMessage="Application name cannot be blank" ForeColor="Red" ControlToValidate="txtApplicationNameH"></asp:RequiredFieldValidator>
                                                                </div>

                                                            </div>
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">URL<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtUrl" placeholder="Enter URL(http:// or https://)" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                 <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -15px; padding-left: 315px; font-size: 13px; color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtUrl" runat="server" Display="Dynamic" ErrorMessage="URL cannot be blank" ForeColor="Red" ControlToValidate="txtUrl"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
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
                                    <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9)">Add New Application</asp:LinkButton>
                                </div>
                                <div class="panel-body panel-body-table">

                                    <asp:GridView ID="grdApplication" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Application Name (English)" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("ApplicationNameEnglish") %>
                                                    <asp:HiddenField ID="hdfApplicationIdGrd" runat="server" Value='<%#Eval("ApplicationId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Application Name (Hindi)" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("ApplicationNameHindi") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="URL" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("ApplicationURL") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="IsActive" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" Checked='<%#Eval("IsActive") %>' CommandArgument='<%# Eval("ApplicationId") %>' AutoPostBack="true" OnCheckedChanged="cbSwitch_CheckedChanged" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("ApplicationId") %>' OnClick="Update_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkActionDelete" CssClass="btn btn-danger btn-rounded bs-actionsbox" CausesValidation="false" OnClick="Delete_Click" CommandArgument='<%# Eval("ApplicationId") %>' runat="server">
                                                                        <span>Delete</span>
                                                    </asp:LinkButton>
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtenderDelete" runat="server" TargetControlID="lnkActionDelete" ConfirmText="Are you sure you want to delete this record?"></asp:ConfirmButtonExtender>
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
        <!-- Inner Content End -->
    </div>
</asp:Content>

