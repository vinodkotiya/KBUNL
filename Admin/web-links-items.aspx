<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="web-links-items.aspx.cs" Inherits="Admin_news_post" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="admin_breadcrumb">
        Admin ><a href="intranet-category.aspx">Intranet Category</a> > Intranet Links
    </div>
 <div class="admin_rhs_content">

                        <asp:HiddenField ID="hfquiz" runat="server" />
        <!-- PAGE CONTENT WRAPPER -->
                    <div class="page-content-wrap">
                     
                        <div class="col-md-12 innerpage">
                         
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>             
                                <asp:Panel ID="panelAddNew" runat="server">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h3 class="panel-title"><strong>Add/Update</strong> options : <span id="spanQuestionTitle" runat="server"></span></h3>
                                        </div>
                                        <div class="panel-body">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>                                                
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hfOptionID" runat="server" />                                                
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">                                                           
                                                            <!-- ENTER Article Title -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Link Name</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtLinkName" placeholder="Enter Link Name" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px">
                                                                    <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ErrorMessage="Links cannot be blank" ForeColor="Red" ControlToValidate="txtLinkName"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>   
                                                          
                                                            <!--ENTER Sequence -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Web Links</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtWebLinks" placeholder="Enter Web Links" class="form-control" runat="server" MaxLength="300"></asp:TextBox>
                                                                    </div>                                                    
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px">
                                                                    <asp:RequiredFieldValidator ID="RFV_Sequence" runat="server" ErrorMessage="Web Links cannot be blank" ForeColor="Red" ControlToValidate="txtWebLinks"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!--Error Message-->
                                                <div id="alert" runat="server" style="margin-top:15px;">
                                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                                </div>
                                                <br />
                                                <div class="panel-footer">
                                                    <asp:Button ID="btnClear" CausesValidation="false"  class="btn btn-default" runat="server" Text="Cancel" OnClick="btnClose_Click" />
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
                                        <div class="panel-heading" style="display:none;">
                                            <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" style="background-color: rgba(199, 15, 15, 0.9)">Add New Option</asp:LinkButton>                                            
                                        </div>
                                        <div class="panel-body panel-body-table">   
                                        <asp:HiddenField ID="hfQuizID" runat="server" />
                                            <asp:GridView ID="gridQuizOption" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False">
                                                <Columns>     
                                                    <asp:TemplateField HeaderText="ID" Visible="false" HeaderStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbOptionID" runat="server" Text='<%#Eval("SubCategoryID") %>' Visible="true"></asp:Label>                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>      
                                                    <asp:TemplateField HeaderText="Links Name" HeaderStyle-Width="700px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOptionText" runat="server" Text='<%#Eval("SubCategoryName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>    
                                                    <asp:TemplateField HeaderText="Web Links" HeaderStyle-Width="700px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCategoryLinks" runat="server" Text='<%#Eval("SubCategoryLink") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>   
                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("SubCategoryID") %>' OnClick="Update_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                         
                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkActionDelete" CssClass="btn btn-danger btn-rounded bs-actionsbox" CausesValidation="false" OnClick="Delete_Click" CommandArgument='<%# Eval("SubCategoryID") %>' runat="server">
                                                                        <span>Delete</span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
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

