<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="empanelled-hospitals.aspx.cs" Inherits="Admin_news_post" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="admin_breadcrumb">
        Admin > Empanelled Hospitals
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
                                            <h3 class="panel-title"><strong>Add/Update</strong> Empanelled Hospital</h3>
                                        </div>
                                        <div class="panel-body">
                                        
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>                                                
                                            <div class="form-horizontal">
                                                 <asp:HiddenField ID="hfRID" runat="server" />    
                                                  <asp:HiddenField ID="hfImage_UploadedPath" runat="server" />                                             
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">                                                           
                                                            <!-- ENTER Article Title -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Hospital Name</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtHospitalName" placeholder="Enter Hospital Name" class="form-control" runat="server" MaxLength="300"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px">
                                                                    <asp:RequiredFieldValidator ID="RFV_HospitalName" runat="server" ErrorMessage="Hospital Name cannot be blank" ForeColor="Red" ControlToValidate="txtHospitalName"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div> 
                                                            
                                                            <!-- ENTER Location -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Location</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtLocation" placeholder="Enter Location" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px">
                                                                    <asp:RequiredFieldValidator ID="RFV_Location" runat="server" ErrorMessage="Location cannot be blank" ForeColor="Red" ControlToValidate="txtLocation"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div> 

                                                            <!-- ENTER Address -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Address</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtAddress" placeholder="Enter Address" class="form-control" runat="server" MaxLength="500"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px">
                                                                    <asp:RequiredFieldValidator ID="RFV_Address" runat="server" ErrorMessage="Address cannot be blank" ForeColor="Red" ControlToValidate="txtAddress"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div> 

                                                            <!-- ENTER Contact No -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Contact No.</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtContactNo" placeholder="Enter Contact No." class="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px">
                                                                    <asp:RequiredFieldValidator ID="RFV_ContactNo" runat="server" ErrorMessage="Contact No cannot be blank" ForeColor="Red" ControlToValidate="txtContactNo"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div> 

                                                            <!-- ENTER Email ID -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Email ID</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtEmailID" placeholder="Enter Email ID" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px">
                                                                    <asp:RequiredFieldValidator ID="RFV_EmailID" runat="server" ErrorMessage="Email ID cannot be blank" ForeColor="Red" ControlToValidate="txtEmailID"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div> 

                                                            <!-- ENTER Website -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Website</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtWebsite" placeholder="Enter Website" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px">
                                                                    <asp:RequiredFieldValidator ID="RFV_txtWebsite" runat="server" ErrorMessage="Website cannot be blank" ForeColor="Red" ControlToValidate="txtWebsite"></asp:RequiredFieldValidator>
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
                                            <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" style="background-color: rgba(199, 15, 15, 0.9)">Add New</asp:LinkButton>                                            
                                        </div>
                                        <div class="panel-body panel-body-table">
                                        
                                            <asp:GridView ID="gridRecords" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False">
                                                <Columns>    
                                                    <asp:TemplateField HeaderText="Hospital Name" HeaderStyle-Width="300px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbRID"  runat="server" Text='<%#Eval("RID") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblHospital" runat="server" Text='<%#Eval("HospitalName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Location" HeaderStyle-Width="120px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLocation" runat="server" Text='<%#Eval("Location") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Address" HeaderStyle-Width="300px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ContactNo" HeaderStyle-Width="120px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContactNo" runat="server" Text='<%#Eval("ContactNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EmailID" HeaderStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmailID" runat="server" Text='<%#Eval("EmailID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Website" HeaderStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWebsite" runat="server" Text='<%#Eval("Website") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                         
                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("RID") %>' OnClick="Update_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                   
                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px">
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

