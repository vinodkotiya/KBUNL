<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="notice.aspx.cs" Inherits="Admin_notice" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
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
                                    <h3 class="panel-title"><strong>Add/Update</strong> Notice</h3>
                                </div>
                                <div class="panel-body">

                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hdfDept_Id" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfNoticeId" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfImage_UploadedPath" runat="server" Value="No" />
                                                <asp:HiddenField ID="hdfUpdateImagePath" runat="server" Value="No" />
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">  
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Notice Date<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtNoticeDate" runat="server" MaxLength="10" class="form-control"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender" runat="server" ValidChars="0123456789/" TargetControlID="txtNoticeDate"></asp:FilteredTextBoxExtender>
                                                                        <asp:CalendarExtender ID="CalendarExtendertxtNoticeDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtNoticeDate" ></asp:CalendarExtender>
                                                                    </div>
                                                                </div>
                                                                 <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RFVBannertxtNoticeDate" runat="server" ErrorMessage="Notice date cannot be blank" ForeColor="Red" ControlToValidate="txtNoticeDate" CssClass="validator"></asp:RequiredFieldValidator>
                                                                     <asp:RegularExpressionValidator ForeColor="Red" ID="regulareexp" runat="server" SetFocusOnError="true" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$" ControlToValidate="txtNoticeDate" CssClass="validator" ErrorMessage="Please Select Vaid Date"></asp:RegularExpressionValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Notice (English)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtNotice" runat="server"  class="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                  <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="rfv_txtNotice" runat="server" ErrorMessage="Notice cannot be blank" ForeColor="Red" ControlToValidate="txtNotice" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                             <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Notice (Hindi)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtNoticeHindi" runat="server"  class="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                  <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtNoticeHindi" runat="server" ErrorMessage="Notice cannot be blank" ForeColor="Red" ControlToValidate="txtNoticeHindi" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group"  style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Notice File</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:FileUpload ID="fupNotice" runat="server" class="btn-primary" name="filename" title="Browse file" />
                                                                    </div>
                                                                    <div><small>doc, xls, pdf, jpg, gif and png extensions supported</small></div>
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
                                    <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9)">Add New Notice</asp:LinkButton>
                                </div>
                                <div class="panel-body panel-body-table">

                                    <asp:GridView ID="grdNotice" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Notice Date" HeaderStyle-Width="100px">
                                                <ItemTemplate>
                                                     <%#Eval("NoticeDate") %>
                                                    <asp:HiddenField ID="hdfNoticeIdGrd" runat="server" Value='<%#Eval("NoticeId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Notice (English)" HeaderStyle-Width="200px">
                                                <ItemTemplate>
                                                    <%#Eval("NoticeEnglish") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Notice (Hindi)" HeaderStyle-Width="200px">
                                                <ItemTemplate>
                                                    <%#Eval("NoticeHindi") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="File" HeaderStyle-Width="200px">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hyperlnkFile" NavigateUrl='<%#Eval("FilePath") %>' runat="server" Target="_blank" Visible='<%#(Convert.ToBoolean(Eval("FileDownloadVisible"))) %>'>View File</asp:HyperLink>
                                                    <%--<a target="_blank" href='<%#Eval("FilePath") %>'></a>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IsActive" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" Checked='<%#Eval("IsActive") %>' CommandArgument='<%# Eval("NoticeId") %>' AutoPostBack="true" OnCheckedChanged="cbSwitch_CheckedChanged" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("NoticeId") %>' OnClick="Update_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkActionDelete" CssClass="btn btn-danger btn-rounded bs-actionsbox" CausesValidation="false" OnClick="Delete_Click" CommandArgument='<%# Eval("NoticeId") %>' runat="server">
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

