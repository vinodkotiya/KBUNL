<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="announcement.aspx.cs" Inherits="Admin_UploadType" %>

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
                                    <h3 class="panel-title"><strong>Add/Update </strong>Announcement</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hdfDept_Id" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfRID" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfAttachment_UploadedPath" runat="server" Value="No" />

                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">

                                                            <!-- Event Title -->
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Announcement Text(English)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtTitleE" placeholder="Enter Announcement Text" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom:-12px; padding-left: 315px; font-size: 13px; color: red;">
                                                                    <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ErrorMessage="Announcement text cannot be blank" ForeColor="Red" ControlToValidate="txtTitleE" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                             <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Announcement Text(Hindi)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtTitleH" placeholder="Enter Announcement Text" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom:-12px; padding-left: 315px; font-size: 13px; color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Announcement text cannot be blank" ForeColor="Red" ControlToValidate="txtTitleH" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

                                                            <!-- Event Detail -->
                                                            <div class="form-group" style="display: none;">
                                                                <label class="col-md-3 col-xs-12 control-label">Description(English)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtDescriptionE" placeholder="Enter Description (Optional)" class="form-control" runat="server" TextMode="MultiLine" Height="150" ></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                             <!-- Event Detail -->
                                                            <div class="form-group" style="display: none;">
                                                                <label class="col-md-3 col-xs-12 control-label">Description(Hindi)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtDescriptionH" placeholder="Enter Description (Optional)" class="form-control" runat="server" TextMode="MultiLine" Height="150"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <!-- Attachment -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Attachment (if any)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <asp:FileUpload ID="FileUploader1" runat="server" class="btn-primary" name="filename" title="Browse file" />
                                                                    </div>
                                                                    <div><small>doc, xls, pdf, jpg, gif and png extensions supported</small></div>
                                                                </div>
                                                            </div>

                                                            <!-- Link URL -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Link URL (if any)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtLinkURL" placeholder="Enter Link URL (Optional)" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                             <!-- Valid Date -->
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Valid Date<span style="color:red;">*</span></label>
                                                                <div class="col-md-3 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtValidDate" placeholder="Valid Date" class="form-control" runat="server"></asp:TextBox>
                                                                        <asp:CalendarExtender runat="server" TargetControlID="txtValidDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                    </div>
                                                                </div>
                                                                  <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom:-12px; padding-left: 315px; font-size: 13px; color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Date cannot be blank" ForeColor="Red" ControlToValidate="txtValidDate" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label"> Sequence No.<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtSequenceNo" placeholder="Enter Sequence No" class="form-control" runat="server" ></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                 <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom:-15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtSequenceNo" runat="server" ErrorMessage="Sequence no cannot be blank" ForeColor="Red" ControlToValidate="txtSequenceNo" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

                                                            <div class="form-group" style="position: relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Mark as New<span style="color: red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group" style="position: relative;">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:DropDownList ID="ddlNew" class="form-control" runat="server">
                                                                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                                                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
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
                                    <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9)">Add New Announcement</asp:LinkButton>
                                </div>
                                <div class="panel-body panel-body-table">
                                    <asp:GridView ID="grdAnnouncements" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="8" OnPageIndexChanging="grdAnnouncements_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfgridRID" runat="server" Value='<%#Eval("RID") %>' />
                                                    <%#Eval("SrNo") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Announcement Text(English)" HeaderStyle-Width="300px">
                                                <ItemTemplate>
                                                    <%#Eval("TitleE") %><br />
                                                    <%#Eval("DescriptionE") %><br />
                                                    <a href='<%#Eval("AttachmentURL_Admin")%>' target="_blank" style="color: #0067ac;"><%#Eval("AttachmentURL")%></a><br />
                                                    <a href='<%#Eval("LinkURL")%>' target="_blank" style="color: #cc0000;"><%#Eval("LinkURL")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Announcement Text(Hindi)" HeaderStyle-Width="300px">
                                                <ItemTemplate>
                                                    <%#Eval("TitleH") %><br />
                                                    <%#Eval("DescriptionH") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valid Date" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("ValidDate") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Posted On" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("PostedOn") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Posted By" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("Department") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Seq." HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("Sequence") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="New" HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("IsNew") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("RID") %>' OnClick="Update_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
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

