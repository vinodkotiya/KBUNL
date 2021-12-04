<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="menu-management.aspx.cs" Inherits="Admin_menu_management" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="admin_breadcrumb">
Admin >Menu
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
                                            <h3 class="panel-title"><strong>Add/Update</strong> Menu</h3>
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
                                                              <!-- ENTER Title -->
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Menu Title<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtMenuTitle" placeholder="Enter Menu Title" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ErrorMessage="Menu title cannot be blank" ForeColor="Red" ControlToValidate="txtMenuTitle" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Menu Title (Hindi)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtMenuTitleH" placeholder="Enter Menu Title (Hindi)" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Menu title Hindi cannot be blank" ForeColor="Red" ControlToValidate="txtMenuTitleH" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

                                                                 <!-- ENTER Title -->
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Link URL<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtLinkURL" placeholder="Enter Link URL" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtLinkURL" runat="server" ErrorMessage="Link URL cannot be blank" ForeColor="Red" ControlToValidate="txtLinkURL" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>  

                                                               <!-- URL Open IN Type -->
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">URL Open In<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:DropDownList ID="ddlUrlOpen" runat="server" class="form-control" >
                                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="NA" Value="NA"></asp:ListItem>
                                                                            <asp:ListItem Text="Open In Same Page" Value="Open In Same Page"></asp:ListItem>
                                                                            <asp:ListItem Text="Open In New Page" Value="Open In New Page"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlUploadType" runat="server" ErrorMessage="Please select url open in" ForeColor="Red" ControlToValidate="ddlUrlOpen" CssClass="validator" InitialValue="0"></asp:RequiredFieldValidator>
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
                                            <table width="100%">
                                                <tr>
                                                    <td width="200px">
                                                        <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" style="background-color: rgba(199, 15, 15, 0.9)">Add New Menu</asp:LinkButton>                                            
                                                    </td>
                                                    <td width="100px">Static Links</td>
                                                    <td><b>About Us: </b> about-us.aspx</td>
                                                    <td><b>Directory:</b> directory.aspx</td>
                                                    <td><b>E-Magazines:</b> e-magazines.aspx</td>                                                  
                                                </tr>
                                                <tr>
                                                    <td width="200px"></td>
                                                    <td width="100px"></td>
                                                    <td><b>Gallery: </b> gallery.aspx</td>
                                                    <td><b>OpinionPoll:</b> opinion-poll.aspx</td>
                                                    <td><b>Survey:</b> survey.aspx</td>    
                                                </tr>
                                                <tr>
                                                    <td width="200px"></td>
                                                    <td width="100px"></td>
                                                    <td><b>Important Contacts:</b> important-contacts.aspx</td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                            
                                        </div>
                                        <div class="panel-body panel-body-table">
                                        
                                            <asp:GridView ID="grdNotice" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False"  OnPageIndexChanging="grdNotice_PageIndexChanging" OnRowDataBound="grdNotice_RowDataBound">
                                                <Columns>  
                                                    <asp:TemplateField HeaderText="Menu Title" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMenuTitle" runat="server" Text='<%#Eval("MenuTitle") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Menu Title (Hindi)" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMenuTitleH" runat="server" Text='<%#Eval("MenuTitleH") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="URL Open" HeaderStyle-Width="300px" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdfMenuId" runat="server" Value='<%#Eval("RID") %>' />
                                                            <asp:Label ID="lblURLOpenIn" runat="server" Text='<%#Eval("URLOpenIn") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Sequence" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSequence" runat="server" Text='<%#Eval("Sequence") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Link URL" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLinkURL" runat="server" Text='<%#Eval("LinkURL") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Menu Options" HeaderStyle-Width="140px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkActionViewList" CssClass="btn btn-facebook btn-rounded bs-actionsbox" CausesValidation="false" OnClick="ViewList_Click" CommandArgument='<%# Eval("RID") %>' runat="server">
                                                                        <span style="color:white"><span class="fa fa-link "></span>View</span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                      
                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("RID") %>' OnClick="Update_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
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

