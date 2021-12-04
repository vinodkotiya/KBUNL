<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="admin-update.aspx.cs" Inherits="Admin_UploadType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" src="../js/showImages.js"></script>
    <style type="text/css">
        .visiblelink{display:inline-block;}
        .hidelink{display:none;}
    </style>
    <div class="admin_breadcrumb">
        Admin > Updates
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
                                <h3 class="panel-title"><strong>Add/Update </strong>Updates</h3>
                            </div>
                            <div class="panel-body">                                        
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>                                                
                                <div class="form-horizontal">
                                    
                                    <asp:HiddenField ID="hfRID" runat="server" />    
                                    <asp:HiddenField ID="hfAttachment_UploadedPath" runat="server" />    
                                                                             
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td valign="top" width="70%">      
                                                
                                                <!-- Event Title -->
                                                <div class="form-group">
                                                    <label class="col-md-3 col-xs-12 control-label">Title</label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                            <asp:TextBox ID="txtTitle" placeholder="Enter Title" class="form-control" runat="server" MaxLength="150"></asp:TextBox>
                                                        </div>
                                                        <asp:RequiredFieldValidator ID="RFV_Title" Display="Dynamic" runat="server" ErrorMessage="Title cannot be blank" ForeColor="Red" ControlToValidate="txtTitle" CssClass="validator"></asp:RequiredFieldValidator>
                                                    </div>
                                                    
                                                </div> 

                                                <!-- Image Path -->
                                                <div class="form-group">
                                                    <label class="col-md-3 col-xs-12 control-label">Image</label>
                                                    <div class="col-md-6 col-xs-12">
                                                        <div class="input-group">                                                                       
                                                            <asp:FileUpload ID="FileUploader1" runat="server" accept="image/*" onchange="showMyImage(this)" class="btn-primary" name="filename"  title="Browse file" />        
                                                           
                                                        </div>
                                                    </div>                                                               
                                                </div>     

                                                <!-- Description -->
                                                <div class="form-group">
                                                    <label class="col-md-3 col-xs-12 control-label">Description</label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <div class="input-group">                                                                       
                                                            <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                            <asp:TextBox ID="txtDesc" placeholder="Enter Description" TextMode="MultiLine" Rows="5" class="form-control" runat="server"></asp:TextBox>                                                                    
                                                        </div>
                                                        <asp:RequiredFieldValidator ID="rfv_Desc" Display="Dynamic" runat="server" ErrorMessage="Description cannot be blank" ForeColor="Red" ControlToValidate="txtDesc" CssClass="validator"></asp:RequiredFieldValidator>
                                                    </div>                                                               
                                                </div>    
                                            </td>
                                            <td>
                                                <td>
                                                            <div class="form-group">
                                                                <div class="input-group">
                                                                    <img src="" runat="server" clientidmode="Static" id="divShowImage" alt="" style="width: 100%" />
                                                                </div>
                                                            </div>

                                                        </td>
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
                                <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" style="background-color: rgba(199, 15, 15, 0.9)">Add New Update</asp:LinkButton>                                            
                            </div>
                            <div class="panel-body panel-body-table">                                        
                                <asp:GridView ID="grdUpdates" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False"  AllowPaging="true" PageSize="8" OnPageIndexChanging="grdUpdates_PageIndexChanging">
                                    <Columns>  
                                        <asp:TemplateField HeaderText="Srno" HeaderStyle-Width="50px">
                                            <ItemTemplate>
                                                <%#Eval("SrNo") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Title" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                               <%#Eval("Title") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description" HeaderStyle-Width="400px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                 <%#Eval("Description") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Image" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                               
                                                <img src='../<%#Eval("ImagePath")%>' alt="" width="60%"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Created On" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Eval("CreatedOn") %>
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

