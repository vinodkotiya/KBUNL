<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="UploadType.aspx.cs" Inherits="Admin_UploadType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        .visiblelink{display:inline-block;}
        .hidelink{display:none;}
    </style>
     <div class="admin_breadcrumb">
        Admin >Updates
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
                                            <h3 class="panel-title"><strong>Add/Update</strong>Updates</h3>
                                        </div>
                                        <div class="panel-body">
                                        
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>                                                
                                            <div class="form-horizontal">
                                                 <asp:HiddenField ID="hdfUploadTypeId" runat="server" />    
                                                  <asp:HiddenField ID="hfImage_UploadedPath" runat="server" />                                             
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">      
                                                               <!-- Upload Type -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Updates Type</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:DropDownList ID="ddlUploadType" runat="server" class="form-control" >
                                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="News" Value="News"></asp:ListItem>
                                                                            <asp:ListItem Text="Event" Value="Event"></asp:ListItem>
                                                                            <asp:ListItem Text="Notice" Value="Notice"></asp:ListItem>
                                                                            <asp:ListItem Text="Circular" Value="Circular"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: 338px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlUploadType" runat="server" ErrorMessage="Please select updates type" ForeColor="Red" ControlToValidate="ddlUploadType" CssClass="validator" InitialValue="0"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>                                                      
                                                            <!-- ENTER Article Title -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Title</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtTitle" placeholder="Enter Title" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: 289px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ErrorMessage="Title cannot be blank" ForeColor="Red" ControlToValidate="txtTitle" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div> 
                                                             <div class="form-group" style="display:none;">
                                                                <label class="col-md-3 col-xs-12 control-label">Content</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtContent" placeholder="Enter Content" class="form-control" runat="server" TextMode="MultiLine" Height="150"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                 <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: 120px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Content cannot be blank" ForeColor="Red" ControlToValidate="txtContent" CssClass="validator"></asp:RequiredFieldValidator>--%>
                                                                </div>
                                                            </div> 
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">File</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                       
                                                                        <asp:FileUpload ID="FileUploadImage" runat="server"  class="btn-primary" name="filename"  title="Browse file" />
                                                                        
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
                                                    <asp:Button ID="btnClear" CausesValidation="false"  class="btn btn-default" runat="server" Text="Close Form" OnClick="btnClose_Click" />
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
                                            <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" style="background-color: rgba(199, 15, 15, 0.9)">Add New Updates</asp:LinkButton>                                            
                                        </div>
                                        <div class="panel-body panel-body-table">                                        
                                            <asp:GridView ID="grdNotice" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False"  AllowPaging="true" PageSize="8" OnPageIndexChanging="grdNotice_PageIndexChanging">
                                                <Columns>  
                                                       <asp:TemplateField HeaderText="Type" HeaderStyle-Width="120px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUploadType" runat="server" Text='<%#Eval("UploadType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Title">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdfgrdUploadTypeId" runat="server" Value='<%#Eval("RID") %>' />
                                                            <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Content" HeaderStyle-Width="500px" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContent" runat="server" Text='<%#Eval("UploadTypeContent") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Posted On" HeaderStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPostedOn" runat="server" Text='<%#Eval("PostedOn") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="File" HeaderStyle-Width="80px">
                                                        <ItemTemplate>                                                            
                                                            <a id="A1" runat="server" target="_blank" href='<%# DataBinder.Eval(Container,"DataItem.AttachmentURLAdmin") %>' class='<%# Eval("AttachmentVisible") %>'>View File</a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                                                     
                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="80px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("RID") %>' OnClick="Update_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="80px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkActionDelete" CssClass="btn btn-danger btn-rounded bs-actionsbox" CausesValidation="false" OnClick="Delete_Click" CommandArgument='<%# Eval("RID") %>' runat="server">
                                                                        <span>Delete</span>
                                                            </asp:LinkButton>
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

