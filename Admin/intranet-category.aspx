<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="intranet-category.aspx.cs" Inherits="Admin_news_post" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="admin_breadcrumb">
        Admin > Intranet Links
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
                                            <h3 class="panel-title"><strong>Add/Update</strong> Intranet Category</h3>
                                        </div>
                                        <div class="panel-body">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>                                                
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hfQuestionID" runat="server" />                                                
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">                                                           
                                                            <!-- ENTER Article Title -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Category</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtCategory" placeholder="Enter Title" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px">
                                                                    <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ErrorMessage="Title cannot be blank" ForeColor="Red" ControlToValidate="txtCategory"></asp:RequiredFieldValidator>
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
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    
                                       
                                </asp:Panel>
                                <asp:Panel ID="panelView" runat="server">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" style="background-color: rgba(199, 15, 15, 0.9)">Add New Intranet Category</asp:LinkButton>                                            
                                        </div>
                                        <div class="panel-body panel-body-table">   
                                            <asp:GridView ID="gridQuestionBank" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False">
                                                <Columns>     
                                                    <asp:TemplateField HeaderText="ID" Visible="false" HeaderStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbQuizID"  runat="server" Text='<%#Eval("CategoryID") %>'></asp:Label>                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>      
                                                    <asp:TemplateField HeaderText="Title" HeaderStyle-Width="500px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuizTitle" runat="server" Text='<%#Eval("CategoryName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                     
                                                    <asp:TemplateField HeaderText="View/Add Links" HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkActionViewList" CssClass="btn btn-facebook btn-rounded bs-actionsbox" CausesValidation="false" OnClick="ViewList_Click" CommandArgument='<%# Eval("CategoryID") %>' runat="server">
                                                                        <span style="color:white"><span class="fa fa-link "></span>Links</span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("CategoryID") %>' OnClick="Update_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkActionDelete" CssClass="btn btn-danger btn-rounded bs-actionsbox" CausesValidation="false" OnClick="Delete_Click" CommandArgument='<%# Eval("CategoryID") %>' runat="server">
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

