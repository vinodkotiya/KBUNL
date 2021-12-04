<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="opinion-poll-option.aspx.cs" Inherits="Admin_news_post" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="admin_breadcrumb">
        Admin > <a href="opinion-poll.aspx">Opinion Poll</a> > Opinion Poll Option
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
                                            <h3 class="panel-title"><strong>Add/Update</strong> Option : <span id="spanQuestionTitle" runat="server"></span></h3>
                                        </div>
                                        <div class="panel-body">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>                                                
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hdfPollOptionId" runat="server" Value="0" />  
                                                <asp:HiddenField ID="hdfPollId" runat="server" Value="0" />
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">                                                           
                                                            <!-- ENTER Article Title -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Option Title</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtLinkName" placeholder="Enter Option Title" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px">
                                                                    <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ErrorMessage="Title cannot be blank" ForeColor="Red" ControlToValidate="txtLinkName"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>  
                                                            <!--ENTER Sequence -->
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!--Error Message-->
                                                <div id="alert" runat="server" style="margin-top:15px;">
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
                                        <div class="panel-heading" style="display:none;">
                                            <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" style="background-color: rgba(199, 15, 15, 0.9)">Add New Option</asp:LinkButton>                                            
                                        </div>
                                        <div class="panel-body panel-body-table">   
                                        <asp:HiddenField ID="hfQuizID" runat="server" />
                                            <asp:GridView ID="gridPollOption" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False">
                                                <Columns>   
                                                     <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                           <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID" Visible="false" HeaderStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbOptionID" runat="server" Text='<%#Eval("OptionID") %>' Visible="true"></asp:Label>                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>      
                                                    <asp:TemplateField HeaderText="Option Title" HeaderStyle-Width="700px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOptionText" runat="server" Text='<%#Eval("OptionText") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>    
                                                  
                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("OptionID") %>' OnClick="Update_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                         
                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkActionDelete" CssClass="btn btn-danger btn-rounded bs-actionsbox" CausesValidation="false" OnClick="Delete_Click" CommandArgument='<%# Eval("OptionID") %>' runat="server">
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

