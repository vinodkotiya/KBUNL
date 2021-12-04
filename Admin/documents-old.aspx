<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="documents-old.aspx.cs" Inherits="Admin_report" %>
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
                                    <h3 class="panel-title"><strong>Add/Update</strong> Report</h3>
                                </div>
                                <div class="panel-body">

                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hfDeptID" runat="server" />
                                                <asp:HiddenField ID="hfUserID" runat="server" />
                                                <asp:HiddenField ID="hdfParentReportId" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfReportId" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfReportUpdateId" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfReportLevel" runat="server" Value="1" />
                                                <asp:HiddenField ID="hdfReportTitle" runat="server" Value="" />
                                                <asp:HiddenField ID="hdfFile_UploadedPath" ClientIDMode="Static" runat="server" Value="NA"/>
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">
                                                            <!-- ENTER Title -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Report Title(English)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12" >
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtTitleEnglish" placeholder="Enter Title" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                    <asp:RequiredFieldValidator ID="RFV_Title" runat="server" Display="Dynamic" ErrorMessage="Title cannot be blank" ForeColor="Red" ControlToValidate="txtTitleEnglish"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Report Title(Hindi)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12" >
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtTitleHindi" placeholder="Enter Title" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtTitleHindi" runat="server" Display="Dynamic" ErrorMessage="Title cannot be blank" ForeColor="Red" ControlToValidate="txtTitleHindi"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                              <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Report Group</label>
                                                                <div class="col-md-6 col-xs-12" style="width:15%;">
                                                                    <div class="input-group">
                                                                        <asp:CheckBox ID="chkReportGroup" class="form-control"  style="padding:10px !important;" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="chkReportGroup_CheckedChanged"></asp:CheckBox>
                                                                    </div>
                                                                </div>
                                                                  <label class="col-md-3 col-xs-12 control-label" style="width:10%;">Seq No<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12" style="width:15%;">
                                                                    <div class="input-group">
                                                                        <asp:TextBox ID="txtSeqNo" class="form-control" runat="server" MaxLength="3"></asp:TextBox>
                                                                       <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSeqNo" runat="server" TargetControlID="txtSeqNo" ValidChars="0123456789"></asp:FilteredTextBoxExtender>
                                                                    </div>
                                                                </div>
                                                                  <div style="position:absolute;bottom:-15px;margin-left:50%;">
                                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtSeqNo" runat="server" Display="Dynamic" ErrorMessage="Seq no. cannot be blank" ForeColor="Red" ControlToValidate="txtSeqNo"></asp:RequiredFieldValidator>
                                                                  </div>
                                                            </div>
                                                             <div class="form-group" id="divlnkType" runat="server" visible="false">
                                                                <label class="col-md-3 col-xs-12 control-label">Link Type<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12" >
                                                                    <div class="input-group" style="width:60%;">
                                                                        <table cellspacing="0" cellpadding="0" style="border:none;border-collapse:collapse;width:100%;margin-top:5px;">
                                                                            <tr>
                                                                                <td><asp:RadioButton ID="rbtnAttachment" class="form-control" runat="server" Text="Attachment" GroupName="lnk" AutoPostBack="true" OnCheckedChanged="rbtnAttachment_CheckedChanged" /></td>
                                                                                <td><asp:RadioButton ID="rbtnURL"  class="form-control" runat="server" Text="URL"  GroupName="lnk" AutoPostBack="true" OnCheckedChanged="rbtnURL_CheckedChanged"  /></td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divURL" runat="server" visible="false">
                                                                <label class="col-md-3 col-xs-12 control-label">URL<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtUrl" placeholder="Enter URL(http:// or https://)" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                                    </div>
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtUrl" runat="server" Display="Dynamic" ErrorMessage="URL cannot be blank" ForeColor="Red" ControlToValidate="txtUrl"></asp:RequiredFieldValidator>
                                                                </div>
                                                               
                                                            </div>
                                                            <div class="form-group" id="divAttachment" runat="server" visible="false">
                                                                <label class="col-md-3 col-xs-12 control-label">Attachment<span style="color:red;">*</span></label>
                                                                <div class="col-md-4 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:FileUpload ID="File_Upload" runat="server" class="btn-primary" name="filename" title="Browse file" />
                                                                     </div>
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidatorFile_Upload" runat="server" Display="Dynamic" ErrorMessage="Plese upload file" ForeColor="Red" ControlToValidate="File_Upload"></asp:RequiredFieldValidator>
                                                                   </div>
                                                                <span runat="server" style="display:none;font-weight:bold;" id="span_FileStatus"></span>
                                                               
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
                                    <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9)">Add New Report</asp:LinkButton>
                                    <a id="lnkbtnBack" runat="server" class="btn btn-primary pull-right" style="background-color: rgba(199, 15, 15, 0.9)">Back</a>
                                </div>
                                <div class="panel-body panel-body-table">
                                    <asp:GridView ID="grdview" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                             <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                   <%#Container.DataItemIndex+1%>
                                                </ItemTemplate>
                                                 </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Title" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdfReportIdGrd" runat="server" Value='<%#Eval("ReportId") %>' />
                                                    <asp:HiddenField ID="hdfParentReportIdGrd" runat="server" Value='<%#Eval("ParentReportId") %>' />
                                                    <asp:HiddenField ID="hdfReportLevelGrd" runat="server" Value='<%#Eval("ReportLevel") %>' />
                                                    <asp:HiddenField ID="hdfReportTitleGrd" runat="server" Value='<%#Eval("ReportTitleEnglish") %>' />
                                                    <%#Eval("ReportTitleEnglish")%><br />
                                                    <%#Eval("ReportTitleHindi") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Group" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("ReportGroup")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Type" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("LinkType")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="File/Url" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hyprLnkAttachment" runat="server" Text="View File/URL" Visible='<%#(Convert.ToBoolean(Eval("DownloadButtonVisible")))%>' NavigateUrl='<%#Eval("AttachmentAndURL")%>' Target="_blank"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Seq No" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("SeqNo")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("ReportId") %>' OnClick="Update_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Report" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <a id="linkReports" runat="server" Visible='<%#(Convert.ToBoolean(Eval("ReportButtonVisible")))%>' href='<%#Eval("NextLevelLink")%>' class="btn btn-danger btn-rounded bs-actionsbox"><span>Reports</span></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkActionDelete" CssClass="btn btn-danger btn-rounded bs-actionsbox" CausesValidation="false" OnClick="Delete_Click" CommandArgument='<%# Eval("ReportId") %>' runat="server">
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

