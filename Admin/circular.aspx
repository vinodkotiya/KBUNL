<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="circular.aspx.cs" Inherits="Admin_UploadType" %>

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
                                    <h3 class="panel-title"><strong>Add/Update </strong>Latest News</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hdfDept_Id" runat="server" Value="0" />
                                                <asp:HiddenField ID="hfRID" runat="server" />
                                                <asp:HiddenField ID="hfAttachment1_UploadedPath" runat="server" />
                                                <asp:HiddenField ID="hfAttachment2_UploadedPath" runat="server" />
                                                <asp:HiddenField ID="hfAttachment3_UploadedPath" runat="server" />
                                                <asp:HiddenField ID="hfAttachment4_UploadedPath" runat="server" />
                                                <asp:HiddenField ID="hfAttachment5_UploadedPath" runat="server" />

                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">
                                                            <div class="form-group" style="position: relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Posted By<span style="color: red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group" style="position: relative;">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:DropDownList ID="ddlDepartment" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group" style="position:relative;" id="divOtherSource" runat="server">
                                                                <label class="col-md-3 col-xs-12 control-label">Source (if others)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtOthers" placeholder="Enter name of Other Source" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                 <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RFVOthers" runat="server" ErrorMessage="Source cannot be blank" ForeColor="Red" ControlToValidate="txtOthers" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

                                                            <!-- Type -->
                                                            <div class="form-group" style="position: relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Type <span style="color: red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group" style="position: relative;">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:DropDownList ID="ddlType" class="form-control" runat="server">
                                                                            <asp:ListItem Text="Circular" Value="Circular"></asp:ListItem>
                                                                            <asp:ListItem Text="Information" Value="Information"></asp:ListItem>
                                                                            <asp:ListItem Text="Notice" Value="Notice"></asp:ListItem>
                                                                            <asp:ListItem Text="News" Value="News"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <!-- Title -->
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Title (English)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtTitleEnglish" placeholder="Enter Title" class="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                 <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ErrorMessage="Title cannot be blank" ForeColor="Red" ControlToValidate="txtTitleEnglish" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                             <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Title (Hindi)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtTitleHindi" placeholder="Enter Title" class="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                 <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtTitleHindi" runat="server" ErrorMessage="Title cannot be blank" ForeColor="Red" ControlToValidate="txtTitleHindi" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

                                                            <div class="form-group" style="position: relative; display:none;">
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

                                                            <!-- Attachment -->
                                                            <div id="divAttachment" runat="server">  
                                                                <div class="form-group">
                                                                    <label class="col-md-3 col-xs-12 control-label">Attachment(1) Title</label>
                                                                    <div class="col-md-6 col-xs-12">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="txtAttachmentTitle1" placeholder="Enter Title of Attachment1" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-md-3 col-xs-12 control-label">Attachment(1)</label>
                                                                    <div class="col-md-6 col-xs-12">
                                                                        <div class="input-group">
                                                                            <asp:FileUpload ID="FileUploadAttachment1" runat="server" class="btn-primary" name="filename" title="Browse file" CssClass="form-control" />
                                                                        </div>
                                                                        <div><small>doc, xls, pdf, ppt, mp4, avi, mov, jpg, gif and png extensions supported</small></div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-md-3 col-xs-12 control-label">Attachment(2) Title</label>
                                                                    <div class="col-md-6 col-xs-12">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="txtAttachmentTitle2" placeholder="Enter Title of Attachment2" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-md-3 col-xs-12 control-label">Attachment(2)</label>
                                                                    <div class="col-md-6 col-xs-12">
                                                                        <div class="input-group">
                                                                            <asp:FileUpload ID="FileUploadAttachment2" runat="server" class="btn-primary" name="filename" title="Browse file" CssClass="form-control" />
                                                                        </div>
                                                                        <div><small>doc, xls, pdf, ppt, mp4, avi, mov, jpg, gif and png extensions supported</small></div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-md-3 col-xs-12 control-label">Attachment(3) Title</label>
                                                                    <div class="col-md-6 col-xs-12">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="txtAttachmentTitle3" placeholder="Enter Title of Attachment3" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-md-3 col-xs-12 control-label">Attachment(3)</label>
                                                                    <div class="col-md-6 col-xs-12">
                                                                        <div class="input-group">
                                                                            <asp:FileUpload ID="FileUploadAttachment3" runat="server" class="btn-primary" name="filename" title="Browse file" CssClass="form-control" />
                                                                        </div>
                                                                        <div><small>doc, xls, pdf, ppt, mp4, avi, mov, jpg, gif and png extensions supported</small></div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-md-3 col-xs-12 control-label">Attachment(4) Title</label>
                                                                    <div class="col-md-6 col-xs-12">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="txtAttachmentTitle4" placeholder="Enter Title of Attachment4" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-md-3 col-xs-12 control-label">Attachment(4)</label>
                                                                    <div class="col-md-6 col-xs-12">
                                                                        <div class="input-group">
                                                                            <asp:FileUpload ID="FileUploadAttachment4" runat="server" class="btn-primary" name="filename" title="Browse file" CssClass="form-control" />
                                                                        </div>
                                                                        <div><small>doc, xls, pdf, ppt, mp4, avi, mov, jpg, gif and png extensions supported</small></div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-md-3 col-xs-12 control-label">Attachment(5) Title</label>
                                                                    <div class="col-md-6 col-xs-12">
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="txtAttachmentTitle5" placeholder="Enter Title of Attachment1" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-md-3 col-xs-12 control-label">Attachment(5)</label>
                                                                    <div class="col-md-6 col-xs-12">
                                                                        <div class="input-group">
                                                                            <asp:FileUpload ID="FileUploadAttachment5" runat="server" class="btn-primary" name="filename" title="Browse file" CssClass="form-control" />
                                                                        </div>
                                                                        <div><small>doc, xls, pdf, ppt, mp4, avi, mov, jpg, gif and png extensions supported</small></div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <!-- Link URL -->
                                                            <div class="form-group" id="divURL" runat="server">
                                                                <label class="col-md-3 col-xs-12 control-label">Link URL (if any)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtLinkURL" placeholder="Enter Link URL (Optional)" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">display Upto<span style="color:red;">*</span></label>
                                                                <div class="col-md-3 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtValidDate" placeholder="Valid Date" class="form-control" runat="server" MaxLength="10"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderDate" runat="server" ValidChars="0123456789/" TargetControlID="txtValidDate"></asp:FilteredTextBoxExtender>
                                                                        <asp:CalendarExtender runat="server" TargetControlID="txtValidDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                    </div>
                                                                </div>
                                                                 <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Display upto cannot be blank" ForeColor="Red" ControlToValidate="txtValidDate" CssClass="validator"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ForeColor="Red" ID="regulareexp" runat="server" SetFocusOnError="true" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$" ControlToValidate="txtValidDate" CssClass="validator" ErrorMessage="Please Select Vaid Date"></asp:RegularExpressionValidator>
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
                                    <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9)">Add New</asp:LinkButton>
                                </div>
                                <div class="panel-body panel-body-table">
                                    <asp:GridView ID="grdCirculars" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="30" OnPageIndexChanging="grdCirculars_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Srno" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfgridRID" runat="server" Value='<%#Eval("RID") %>' />
                                                    <%#Eval("SrNo") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <%#Eval("Type") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Title" HeaderStyle-Width="200px">
                                                <ItemTemplate>
                                                    <b><%#Eval("TitleEnglish") %></b><br />
                                                    <b id="titlehindi" runat="server" visible='<%#Eval("TitleHindi").ToString() !=Eval("TitleEnglish").ToString() %>'><%#Eval("TitleHindi") %></b>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                            
                                            <asp:TemplateField HeaderText="Attachment/Link URL" HeaderStyle-Width="200px">
                                                <ItemTemplate>
                                                    <a href='<%#"../"+Eval("AttachmentURL1")%>' target="_blank" style="color: #0067ac;"><%#Eval("AttachmentTitle1")%></a> 
                                                    <a href='<%#"../"+Eval("AttachmentURL2")%>' target="_blank" style="color: #0067ac;"><%#Eval("AttachmentTitle2")%></a> 
                                                    <a href='<%#"../"+Eval("AttachmentURL3")%>' target="_blank" style="color: #0067ac;"><%#Eval("AttachmentTitle3")%></a> 
                                                    <a href='<%#"../"+Eval("AttachmentURL4")%>' target="_blank" style="color: #0067ac;"><%#Eval("AttachmentTitle4")%></a> 
                                                    <a href='<%#"../"+Eval("AttachmentURL5")%>' target="_blank" style="color: #0067ac;"><%#Eval("AttachmentTitle5")%></a> 
                                                    <a href='<%#Eval("LinkURL")%>' target="_blank" style="color:#cc0000;"><%#Eval("LinkURL")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Display Upto" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("ValidDate") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Posted On" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("PostedOn") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Posted By" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("Department") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="New" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("IsNew") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("RID") %>' OnClick="Update_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
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

