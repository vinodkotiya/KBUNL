<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="e-magazine.aspx.cs" Inherits="Admin_news_post" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .form-control {
            padding-top: 3px;
            padding-bottom: 3px;
        }
    </style>
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
                                    <h3 class="panel-title"><strong>Add/Update</strong> E-Magazine</h3>
                                </div>
                                <div class="panel-body">

                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hdfDept_Id" runat="server" Value="0" />
                                                <asp:HiddenField ID="hfEMagazineID" runat="server" />
                                                <asp:HiddenField ID="hdfImage_UploadedPath" runat="server" />
                                                <asp:HiddenField ID="hdfCoverImage_UploadedPath" runat="server" />
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">
                                                            <!-- ENTER Article Title -->
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Year<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>

                                                                        <asp:DropDownList ID="ddlyear" runat="server" class="form-control"  Width="200px">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -15px; padding-left: 315px; font-size: 13px; color: red;">
                                                                    <asp:RequiredFieldValidator ID="RFV_Year" runat="server" ErrorMessage="Year cannot be blank" ForeColor="Red" ControlToValidate="ddlyear" InitialValue="0" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Month<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:DropDownList ID="ddlMonth" runat="server" class="form-control" Width="200px">
                                                                            <asp:ListItem Value="0">--Select Month--</asp:ListItem>
                                                                            <asp:ListItem Value="January">January</asp:ListItem>
                                                                            <asp:ListItem Value="February">February</asp:ListItem>
                                                                            <asp:ListItem Value="March">March</asp:ListItem>
                                                                            <asp:ListItem Value="April">April</asp:ListItem>
                                                                            <asp:ListItem Value="May">May</asp:ListItem>
                                                                            <asp:ListItem Value="June">June</asp:ListItem>
                                                                            <asp:ListItem Value="July">July</asp:ListItem>
                                                                            <asp:ListItem Value="August">August</asp:ListItem>
                                                                            <asp:ListItem Value="September">September</asp:ListItem>
                                                                            <asp:ListItem Value="October">October</asp:ListItem>
                                                                            <asp:ListItem Value="November">November</asp:ListItem>
                                                                            <asp:ListItem Value="December">December</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -15px; padding-left: 315px; font-size: 13px; color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Month cannot be blank" ForeColor="Red" ControlToValidate="ddlMonth" InitialValue="0" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Cover Image (186 x 255px)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:FileUpload ID="fileUploadCover" runat="server" class="btn-primary" name="filename" title="Browse file" />
                                                                    </div>
                                                                    <div><small>jpg, gif and png extensions supported</small></div>
                                                                </div>
                                                                 <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -15px; padding-left: 315px; font-size: 13px; color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorfileUploadCover" runat="server" ErrorMessage="Please upload file" ForeColor="Red" ControlToValidate="fileUploadCover"  CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">File<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:FileUpload ID="FileUploadImage" runat="server" class="btn-primary" name="filename" title="Browse file" />
                                                                    </div>
                                                                    <div><small>doc and pdf extensions supported</small></div>
                                                                </div>
                                                                 <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -15px; padding-left: 315px; font-size: 13px; color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorFileUploadImage" runat="server" ErrorMessage="Please upload file" ForeColor="Red" ControlToValidate="FileUploadImage"  CssClass="validator"></asp:RequiredFieldValidator>
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
                                    <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9)">Add New E-Magazine</asp:LinkButton>
                                </div>
                                <div class="panel-body panel-body-table">

                                    <asp:GridView ID="gridMagazine" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" Visible="false" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbEMagazineID" runat="server" Text='<%#Eval("EMagazineID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Year" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Month" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMonth" runat="server" Text='<%#Eval("Month") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cover Image" HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <img src='<%#Eval("CoverImage") %>' style="width:200px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="File" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <a id="A1" runat="server" target="_blank" href='<%# DataBinder.Eval(Container,"DataItem.AttachmentPath") %>'>View File</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("EMagazineID") %>' OnClick="Update_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkActionDelete" CssClass="btn btn-danger btn-rounded bs-actionsbox" CausesValidation="false" OnClick="Delete_Click" CommandArgument='<%# Eval("EMagazineID") %>' runat="server">
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

