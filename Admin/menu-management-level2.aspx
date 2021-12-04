<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="menu-management-level2.aspx.cs" Inherits="Admin_menu_management_level2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="admin_breadcrumb">
Admin ><a href="menu-management.aspx"> <asp:Label ID="lblMenuName" runat="server" Text=""></asp:Label></a>
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
                                            <h3 class="panel-title"><strong>Add/Update</strong> Menu Option</h3>
                                        </div>
                                        <div class="panel-body">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>                                                
                                            <div class="form-horizontal">
                                                 <asp:HiddenField ID="hdfMenuLevel2Id" runat="server" /> 
                                                 <asp:HiddenField ID="hdfMenuId" runat="server" />          
                                                <asp:HiddenField ID="hdfMenuName" runat="server" />                                               
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">  
                                                             <!-- select Title English -->
                                                            <div class="form-group" runat="server" id="divDptListE" visible="false" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Department(English)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:DropDownList runat="server" ID="ddlDeptE" class="form-control" Style="-webkit-appearance:menulist" OnSelectedIndexChanged="ddlDeptE_SelectedIndexChanged" AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                               <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="rfv_ddlE" InitialValue="0" runat="server" ErrorMessage="Department(English) cannot be blank" ForeColor="Red" ControlToValidate="ddlDeptE" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                              <!-- select Title Hindi -->
                                                            <div class="form-group" runat="server" id="divDptListH" visible="false" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Department(Hindi)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:DropDownList runat="server" ID="ddlDeptH" class="form-control" Style="-webkit-appearance:menulist">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server" ErrorMessage="Department(Hindi) cannot be blank" ForeColor="Red" ControlToValidate="ddlDeptH" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                              <!-- ENTER Title -->
                                                            <div class="form-group" runat="server" id="divMenuTitleE" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Menu Title(English)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtMenuTitle" placeholder="Enter Menu Title" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                               <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RFV_Title"  runat="server" ErrorMessage="Menu title(English) cannot be blank" ForeColor="Red" ControlToValidate="txtMenuTitle" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>  
                                                            <div class="form-group" runat="server" id="divMenuTitleH" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Menu Title (Hindi)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtMenuTitleH" placeholder="Enter Menu Title (Hindi)" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                 <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3"  runat="server" ErrorMessage="Menu title(Hindi) cannot be blank" ForeColor="Red" ControlToValidate="txtMenuTitleH" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

                                                                 <!-- ENTER Title -->
                                                            <div class="form-group" runat="server" id="divLinkURL" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Link URL</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtLinkURL" placeholder="Enter Link URL" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtLinkURL"  runat="server" ErrorMessage="Link URL cannot be blank" ForeColor="Red" ControlToValidate="txtLinkURL" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>  

                                                               <!-- URL Open IN Type -->
                                                            <div class="form-group" runat="server" id="divURLOpen" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">URL Open In</label>
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
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlUploadType"  runat="server" ErrorMessage="Please select url open in" ForeColor="Red" ControlToValidate="ddlUrlOpen" InitialValue="0" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>                                                      
                                                          
                                                             <div class="form-group"  style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label"> Sequence No.</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtSequenceNo" placeholder="Enter Sequence No" class="form-control" runat="server" ></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                              <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtSequenceNo"  runat="server" ErrorMessage="Sequence no cannot be blank" ForeColor="Red" ControlToValidate="txtSequenceNo" CssClass="validator"></asp:RequiredFieldValidator>
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
                                                    <td align="left"><asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" style="background-color: rgba(199, 15, 15, 0.9)">Add New Menu Option</asp:LinkButton></td>
                                                    <td align="right"><a href="menu-management.aspx" class="btn btn-primary pull-left" style="background-color: rgba(199, 15, 15, 0.9); float:none!important">Back</a></td>
                                                </tr>
                                            </table>                                           
                                            
                                        </div>
                                        <div class="panel-body panel-body-table">
                                        
                                            <asp:GridView ID="grdNotice" EmptyDataText="No menu option found" CssClass="table" runat="server" AutoGenerateColumns="False"  OnPageIndexChanging="grdNotice_PageIndexChanging">
                                                <Columns>  
                                                       <asp:TemplateField HeaderText="Menu Title" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMenuLevel2Title" runat="server" Text='<%#Eval("TitleE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Menu Title (Hindi)" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMenuTitleH" runat="server" Text='<%#Eval("TitleH") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Sequence" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSequence" runat="server" Text='<%#Eval("Sequence") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Linked URL" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLinkURL" runat="server" Text='<%#Eval("LinkURL") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SubMenu Options" HeaderStyle-Width="170px" ItemStyle-HorizontalAlign="Center" >
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkActionViewList" CssClass="btn btn-facebook btn-rounded bs-actionsbox" CausesValidation="false" OnClick="ViewList_Click" CommandArgument='<%# Eval("RID") %>' runat="server">
                                                                        <span style="color:white"><span class="fa fa-link "></span>View</span>
                                                            </asp:LinkButton>
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

