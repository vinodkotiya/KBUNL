<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="documents.aspx.cs" Inherits="Admin_report" %>
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
                        <%--Level: <asp:Label ID="lblDocLevel" runat="server"></asp:Label>, Parent RecordID: <asp:Label ID="lblParentRecordID" runat="server"></asp:Label>, DocLevelCode: <asp:Label ID="lblDocLevelcode" runat="server"></asp:Label>--%>
                        <!--HiddenFields-->
                        <asp:HiddenField ID="hfDeptID" runat="server" Value="0" />
                        <asp:HiddenField ID="hfDocID" runat="server" Value="0" />
                        <asp:HiddenField ID="hfDocID_forEdit" runat="server" Value="0" />
                        <asp:HiddenField ID="hfDocLevelCode" runat="server" Value="0" />
                        <asp:HiddenField ID="hfParentDocRID" runat="server" Value="0" />
                        <asp:HiddenField ID="hfDocLevel" runat="server" Value="0" />
                        <asp:HiddenField ID="hfDocUploadPath" runat="server" Value="" />
                        <asp:HiddenField ID="hfDocURL" runat="server" Value="" />

                        <!--Add New Tab-->
                        <asp:Panel ID="panelAddNew_DocumentTab" runat="server">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong>Add/Update</strong> Document Tab</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel_DocumentTab" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">
                                                            <!-- ENTER Title -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Document Tab(English)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12" >
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtDocumentTabEnglish" placeholder="Enter name of Document Tab" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                    </div>
                                                                    <asp:RequiredFieldValidator ID="RFV_DocTabEng" runat="server" Display="Dynamic" ErrorMessage="Document Tab name cannot be blank" ForeColor="Red" ControlToValidate="txtDocumentTabEnglish"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Document Tab(Hindi)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12" >
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtDocumentTabHindi" placeholder="Enter name of Document Tab" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                    </div>
                                                                    <asp:RequiredFieldValidator ID="RFV_DocTabHin" runat="server" Display="Dynamic" ErrorMessage="Document Tab name cannot be blank" ForeColor="Red" ControlToValidate="txtDocumentTabHindi"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Sequence<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtDocumentTabSeqNo" class="form-control" runat="server" MaxLength="3"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSeqNo" runat="server" TargetControlID="txtDocumentTabSeqNo" ValidChars="0123456789"></asp:FilteredTextBoxExtender>
                                                                    </div>
                                                                </div>
                                                                <div style="position:absolute;bottom:-15px;margin-left:50%;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtSeqNo" runat="server" Display="Dynamic" ErrorMessage="Seq no. cannot be blank" ForeColor="Red" ControlToValidate="txtDocumentTabSeqNo"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>                                                             
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!--Error Message-->
                                                <div id="alert_DocumentTab" runat="server">
                                                    <asp:Label ID="lblMsg_DocumentTab" runat="server" Text=""></asp:Label>
                                                </div>
                                                <br />
                                                <div class="panel-footer">
                                                    <asp:Button ID="btnClose_DocumentTab" CausesValidation="false" class="btn btn-default" runat="server" Text="Close Form" OnClick="btnClose_DocumentTab_Click" />
                                                    <asp:Button ID="btnSave_DocumentTab" class="btn btn-primary pull-right" runat="server" Text="Submit" OnClick="btnSave_DocumentTab_Click" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </asp:Panel>
                        
                        <!--Add New Folder-->
                        <asp:Panel ID="panelAddNew_Folder" runat="server">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong>Add/Update</strong> Folder</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">
                                                            <!-- ENTER Title -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Folder Name(English)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12" >
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtFolderNameEng" placeholder="Enter folder name" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                    </div>
                                                                    <asp:RequiredFieldValidator ID="RFVFolderNameEng" runat="server" Display="Dynamic" ErrorMessage="Folder name cannot be blank" ForeColor="Red" ControlToValidate="txtFolderNameEng"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Folder Name(Hindi)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12" >
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtFolderNameHin" placeholder="Enter folder name" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                    </div>
                                                                    <asp:RequiredFieldValidator ID="RFVFolderNameHin" runat="server" Display="Dynamic" ErrorMessage="Folder name cannot be blank" ForeColor="Red" ControlToValidate="txtFolderNameHin"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Sequence<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtFolderSequence" class="form-control" runat="server" MaxLength="3"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FTB_FolderSequence" runat="server" TargetControlID="txtFolderSequence" ValidChars="0123456789"></asp:FilteredTextBoxExtender>
                                                                    </div>
                                                                </div>
                                                                <div style="position:absolute;bottom:-15px;margin-left:50%;">
                                                                    <asp:RequiredFieldValidator ID="RFV_FolderSequence" runat="server" Display="Dynamic" ErrorMessage="Sequence cannot be blank" ForeColor="Red" ControlToValidate="txtFolderSequence"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>                                                             
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!--Error Message-->
                                                <div id="alert_Folder" runat="server">
                                                    <asp:Label ID="lblMsg_Folder" runat="server" Text=""></asp:Label>
                                                </div>
                                                <br />
                                                <div class="panel-footer">
                                                    <asp:Button ID="btnClose_Folder" CausesValidation="false" class="btn btn-default" runat="server" Text="Close Form" OnClick="btnClose_Folder_Click" />
                                                    <asp:Button ID="btnSave_Folder" class="btn btn-primary pull-right" runat="server" Text="Submit" OnClick="btnSave_Folder_Click" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </asp:Panel>
                        
                        <!--Add New Document/Report/Links-->
                        <asp:Panel ID="panelAddNew_Document" runat="server">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong>Add/Update</strong> Document / Document Link</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <div class="form-group" style="position: relative;">
                                                    <label class="col-md-3 col-xs-12 control-label">Document Title (English)<span style="color: red;">*</span></label>
                                                    <div class="col-md-6 col-xs-12">
                                                        <div class="input-group" style="position: relative;">
                                                            <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                            <asp:TextBox ID="txtDocumentTitleEnglish" placeholder="Enter Document Title" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -15px; padding-left: 315px; font-size: 13px; color: red;">
                                                        <asp:RequiredFieldValidator ID="RFV_DocumentTitleEnglish" runat="server" ErrorMessage="Document title cannot be blank" ForeColor="Red" ControlToValidate="txtDocumentTitleEnglish" CssClass="validator"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group" style="position: relative;">
                                                    <label class="col-md-3 col-xs-12 control-label">Document Title (Hindi)<span style="color: red;">*</span></label>
                                                    <div class="col-md-6 col-xs-12">
                                                        <div class="input-group" style="position: relative;">
                                                            <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                            <asp:TextBox ID="txtDocumentTitleHindi" placeholder="Enter Document Title" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -15px; padding-left: 315px; font-size: 13px; color: red;">
                                                        <asp:RequiredFieldValidator ID="RFV_DocumentTitleHindi" runat="server" ErrorMessage="Document title cannot be blank" ForeColor="Red" ControlToValidate="txtDocumentTitleHindi" CssClass="validator"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group" style="position:relative;">
                                                    <label class="col-md-3 col-xs-12 control-label">Sequence<span style="color:red;">*</span></label>
                                                    <div class="col-md-6 col-xs-12">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                            <asp:TextBox ID="txtDocumentSequence" class="form-control" runat="server" MaxLength="3"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="FTB_DocumentSequence" runat="server" TargetControlID="txtDocumentSequence" ValidChars="0123456789"></asp:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                    <div style="position:absolute;bottom:-15px;margin-left:50%;">
                                                        <asp:RequiredFieldValidator ID="RFV_DocumentSequence" runat="server" Display="Dynamic" ErrorMessage="Sequence" ForeColor="Red" ControlToValidate="txtDocumentSequence"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>       
                                                <div class="form-group" style="position: relative;">
                                                    <label class="col-md-3 col-xs-12 control-label">Document Link Type<span style="color: red;">*</span></label>
                                                    <div class="col-md-6 col-xs-12">
                                                        <div class="input-group" style="position: relative;">
                                                            <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                            <asp:DropDownList ID="ddlLinkType" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLinkType_SelectedIndexChanged" >
                                                                <asp:ListItem Text="Attachment" Value="Attachment"></asp:ListItem>
                                                                <asp:ListItem Text="Link" Value="Link"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" id="divURL" runat="server" visible="false" >
                                                    <label class="col-md-3 col-xs-12 control-label">Link URL<span style="color:red;">*</span></label>
                                                    <div class="col-md-6 col-xs-12">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                            <asp:TextBox ID="txtUrl" placeholder="Enter URL(http:// or https://)" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- Attach File -->
                                                <div class="form-group" id="divAttachment" runat="server" >
                                                    <label class="col-md-3 col-xs-12 control-label">Attachment<span style="color:red;">*</span></label>
                                                    <div class="col-md-4 col-xs-12">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"></span>
                                                            <asp:FileUpload ID="FileUploader" runat="server" class="btn-primary" name="filename" title="Browse file" />
                                                        </div>                                                        
                                                    </div>
                                                </div>

                                                <!--Error Message-->
                                                <div id="alert_Document" runat="server">
                                                    <asp:Label ID="lblMsg_Document" runat="server" Text=""></asp:Label>
                                                </div>
                                                <br />
                                                <div class="panel-footer">
                                                    <asp:Button ID="btnClose_Document" CausesValidation="false" class="btn btn-default" runat="server" Text="Close Form" OnClick="btnClose_Document_Click" />
                                                    <asp:Button ID="btnSave_Document" class="btn btn-primary pull-right" runat="server" Text="Submit" OnClick="btnSave_Document_Click" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSave_Document" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </asp:Panel>
                        
                        <asp:Panel ID="panelView" runat="server">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <asp:LinkButton ID="lbtn_AddNew_DocumentTab" runat="server" OnClick="lbtn_AddNew_DocumentTab_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9); margin-right:10px;">Add New Document Tab</asp:LinkButton>
                                    <asp:LinkButton ID="lbtn_AddNew_Folder" runat="server" OnClick="lbtn_AddNew_Folder_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9); margin-right:10px;">Add New Folder</asp:LinkButton>
                                    <asp:LinkButton ID="lbtn_AddNew_Document" runat="server" OnClick="lbtn_AddNew_Document_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9); margin-right:10px;">Add New Document</asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtnBack" runat="server" OnClick="lnkbtnBack_Click" class="btn btn-primary pull-right" Style="background-color: rgba(199, 15, 15, 0.9); margin-right:10px;">Back</asp:LinkButton>
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
                                                    <asp:HiddenField ID="hfGrid_DocID" runat="server" Value='<%#Eval("DocID") %>' />
                                                    <asp:HiddenField ID="hfGrid_DeptID" runat="server" Value='<%#Eval("DeptID") %>' />
                                                    <asp:HiddenField ID="hfGrid_ParentDocRID" runat="server" Value='<%#Eval("ParentDocRID") %>' />
                                                    <asp:HiddenField ID="hfGrid_DocLevel" runat="server" Value='<%#Eval("DocLevel") %>' />
                                                    <asp:HiddenField ID="hfGrid_DocLevelCode" runat="server" Value='<%#Eval("DocLevelCode") %>' />
                                                    <%#Eval("DocTitleEnglish")%><br />
                                                    <%#Eval("DocTitleHindi") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Doc Type" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("DocumentType")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Link Type" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("LinkType")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="File/Url" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <a ID="FileLink" runat="server" href='<%#Eval("AttachmentURL_Admin")%>' visible='<%# Eval("IsAttachment").ToString() == "True" %>' target="_blank">View</a>
                                                    <a ID="FileURL" runat="server" href='<%#Eval("URL")%>' visible='<%# Eval("IsURL").ToString() == "True" %>' target="_blank">View</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Sequence" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("Sequence")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("DocID") %>' OnClick="Update_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Folder/Files" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkView" CssClass="btn btn-facebook btn-rounded bs-actionsbox" CausesValidation="false" CommandArgument='<%# Eval("DocID") %>' OnClick="View_Click" runat="server" visible='<%# Eval("IsDirectory").ToString() == "True" %>'><span style="color:white"><span class="fa fa-link "></span>View</span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkActionDelete" CssClass="btn btn-danger btn-rounded bs-actionsbox" CausesValidation="false" OnClick="Delete_Click" CommandArgument='<%# Eval("DocID") %>' runat="server">
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

