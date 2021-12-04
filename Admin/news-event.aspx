<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="news-event.aspx.cs" Inherits="Admin_UploadType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .visiblelink {
            display: inline-block;
        }

        .hidelink {
            display: none;
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
                                    <h3 class="panel-title"><strong>Add/Update </strong>Event</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hdfDept_Id" runat="server" Value="0" />
                                                <asp:HiddenField ID="hfRID" runat="server" />
                                                <asp:HiddenField ID="hfUserID" runat="server" />
                                                <asp:HiddenField ID="hfAttachment_UploadedPath" runat="server" Value="NA" />
                                                <asp:HiddenField ID="hdfUpdateImagePath" runat="server" Value="NA" />

                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">
                                                            <!-- Type -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Type</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                       <asp:DropDownList runat="server" ID="ddlType" class="form-control" Style="-webkit-appearance:menulist">
                                                                           <%--<asp:ListItem Value="News" Text="News"></asp:ListItem>--%>
                                                                           <asp:ListItem Value="Event" Text="Event"></asp:ListItem>
																		   <%--<asp:ListItem Value="Information" Text="Information"></asp:ListItem>--%>
                                                                       </asp:DropDownList>
                                                                    </div>
                                                                </div>                                                               
                                                            </div>
                                                            <!-- Date -->
                                                            <div class="form-group"  style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Date</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtNewsOrEvent" placeholder="Date" class="form-control" runat="server"></asp:TextBox>
                                                                        <asp:CalendarExtender runat="server" TargetControlID="txtNewsOrEvent" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                    </div>
                                                                </div>
                                                                  <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatordate" runat="server" ErrorMessage="Date cannot be blank" ForeColor="Red" ControlToValidate="txtNewsOrEvent" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <!-- Title -->
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Title (English)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtTitleEnglish" placeholder="Enter Title" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ErrorMessage="Title cannot be blank" ForeColor="Red" ControlToValidate="txtTitleEnglish" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                             <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Title (Hindi)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtTitleHindi" placeholder="Enter Title" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtTitleHindi" runat="server" ErrorMessage="Title cannot be blank" ForeColor="Red" ControlToValidate="txtTitleHindi" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

                                                            <!-- Description -->
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Description (English)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtDescriptionEnglish" placeholder="Enter Description" class="form-control" runat="server" TextMode="MultiLine" Height="150" MaxLength="500"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                              <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Description (Hindi)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtDescriptionHindi" placeholder="Enter Description" class="form-control" runat="server" TextMode="MultiLine" Height="150" MaxLength="500"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>



                                                            <!-- Link -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">URL</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtLinkURL" placeholder="Enter URL(http:// or https://)" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                                    </div>
                                                                    <span style="font-size: 13px; padding-left: 45%;">OR</span>
                                                                </div>

                                                            </div>
                                                            <!-- Attachment -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">File</label>
                                                                <div class="col-md-4 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:FileUpload ID="FileUploader1" runat="server" class="btn-primary" name="filename" title="Browse file" />

                                                                    </div>
                                                                    <div><small>doc, xls, pdf, jpg, gif and png extensions supported</small></div>
                                                                </div>
                                                                <span runat="server" style="display: none; font-weight: bold;" id="span_FileStatus"></span>
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
                                    <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9)">Add New </asp:LinkButton>
                                </div>
                                <div class="panel-body panel-body-table">
                                    <asp:GridView ID="grdNewsOrEvents" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="8" OnPageIndexChanging="grdEvents_PageIndexChanging">
                                        <Columns>
                                           
                                            <asp:TemplateField HeaderText="Type / Date" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblType" runat="server" Text='<%#Eval("Type") %>' Font-Bold="true"></asp:Label> <br /><%#Eval("NewsOrEventDate") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Title" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTitleEnglish" Text='<%#Eval("TitleEnglish") %>' runat="server"></asp:Label><br />
                                                    <asp:Label ID="lblTitleHindi" Text='<%#Eval("TitleHindi") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description" HeaderStyle-Width="400px">
                                                <ItemTemplate>
                                                    <%#Eval("DescriptionEnglish") %><br />
                                                    <%#Eval("DescriptionHindi") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="File / Link" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%--<a href='<%#Eval("AttachmentURL_Admin")%>' target="_blank" style="color:#0067ac;"><%#Eval("AttachmentURL")%></a><br />--%>
                                                    <a href='<%#Eval("URL")%>' target="_blank" style="color: #cc0000;" id="lnk" runat="server" visible='<%#Eval("Text").ToString()!="NA" %>'><%#Eval("Text") %></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Created On" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("UploadedOn") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="View/Add Image" HeaderStyle-Width="100px" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkActionViewList" CssClass="btn btn-facebook btn-rounded bs-actionsbox" CausesValidation="false" OnClick="ViewList_Click" CommandArgument='<%# Eval("RID") %>' runat="server">
                                                                        <span style="color:white"><span class="fa fa-link "></span>Images (<%#Eval("TotalImg")%>)</span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Active" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEnableStatus" CssClass="btn btn-facebook btn-rounded bs-actionsbox" CausesValidation="false" CommandArgument='<%# Eval("RID") %>' OnClick="lnkAcvtiveStatus_Click" runat="server">
                                                        <asp:Label runat="server" ID="lblActiveStatus" ForeColor="White" Text='<%#Eval("ActiveStatus") %>'></asp:Label>
                                                    </asp:LinkButton>
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtenderStatus" runat="server" TargetControlID="lnkEnableStatus" ConfirmText="Are you sure you want to change status of this record?"></asp:ConfirmButtonExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("RID") %>' OnClick="Update_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkActionDelete" CssClass="btn btn-danger btn-rounded bs-actionsbox" CausesValidation="false" OnClick="Delete_Click" CommandArgument='<%# Eval("RID") %>' runat="server">
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

