<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="employee-service.aspx.cs" Inherits="Admin_employee_service" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
           <div class="admin_breadcrumb">
        Admin > Employee Services
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
                                    <h3 class="panel-title"><strong>Add/Update</strong> Employee Service</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hdfEmployeeService_Id" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfImage_UploadedPath" runat="server" Value="No" />
                                                <asp:HiddenField ID="hdfUpdateImagePath" runat="server" Value="No" />
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">  
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Service Title(English)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtServiceTitleEnglish" runat="server" MaxLength="500" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                 <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RFVBannertxtServiceTitleEnglish" runat="server" ErrorMessage="Service title cannot be blank" ForeColor="Red" ControlToValidate="txtServiceTitleEnglish" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Service Title(Hindi)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtServiceTitleHindi" runat="server"  class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                  <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="rfv_txtServiceTitleHindi" runat="server" ErrorMessage="Service title cannot be blank" ForeColor="Red" ControlToValidate="txtServiceTitleHindi" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Sequence No<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtSeqNo" runat="server"  class="form-control"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSeqNo" runat="server" ValidChars="0123456789" TargetControlID="txtSeqNo"></asp:FilteredTextBoxExtender>
                                                                    </div>
                                                                </div>
                                                                  <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Service title cannot be blank" ForeColor="Red" ControlToValidate="txtServiceTitleHindi" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                             <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Service Link</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtServiceLink" runat="server"  class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                 <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatortxtServiceLink" runat="server" ErrorMessage="Service link cannot be blank" ForeColor="Red" ControlToValidate="txtServiceLink" CssClass="validator"></asp:RequiredFieldValidator>--%>
                                                                </div>
                                                            </div>
                                                            <div class="form-group"  style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Icon<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:FileUpload ID="fupIcon" runat="server" class="btn-primary" name="filename" title="Browse file" CssClass="form-control" />
                                                                    </div>
                                                                    <div><small>Only Image (jpg, gif and png extensions supported)</small></div>
                                                                </div>
                                                                 <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorfupIcon" runat="server" ErrorMessage="Please upload service icon" ForeColor="Red" ControlToValidate="fupIcon" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                             <!-- URL Open IN Type -->
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">URL Open In</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:DropDownList ID="ddlUrlOpen" runat="server" class="form-control" >
                                                                           <asp:ListItem Text="Open In New Page" Value="Open In New Page"></asp:ListItem>
                                                                            <asp:ListItem Text="Open In Same Page" Value="Open In Same Page"></asp:ListItem>
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
                                    <table width="100%">
                                        <tr>
                                            <td width="200px">
                                                <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9)">Add New Employee Service</asp:LinkButton>
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

                                    <asp:GridView ID="grdService" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Service Title" HeaderStyle-Width="200px">
                                                <ItemTemplate>
                                                     <%#Eval("ServiceTitleEnglish") %><br />
                                                     <%#Eval("ServiceTitleHindi") %>
                                                    <asp:HiddenField ID="hdfServiceIdGrd" runat="server" Value='<%#Eval("EmployeeServiceId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="Seq No" HeaderStyle-Width="70px">
                                                <ItemTemplate>
                                                    <%#Eval("SeqNo") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Service Link" HeaderStyle-Width="200px">
                                                <ItemTemplate>
                                                    <%#Eval("ServiceLink") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Icon" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgServiceIcon" runat="server" Height="50px" Width="50px" ImageUrl='<%#Eval("Icon") %>' Visible='<%#(Convert.ToBoolean(Eval("LinkVisible"))) %>' style="background-color:#1565b2; border-radius:50%;" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Open In" HeaderStyle-Width="120px">
                                                <ItemTemplate>
                                                    <%#Eval("URLOpenIn") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IsActive" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" Checked='<%#Eval("IsActive") %>' CommandArgument='<%# Eval("EmployeeServiceId") %>' AutoPostBack="true" OnCheckedChanged="cbSwitch_CheckedChanged" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("EmployeeServiceId") %>' OnClick="Update_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkActionDelete" CssClass="btn btn-danger btn-rounded bs-actionsbox" CausesValidation="false" OnClick="Delete_Click" CommandArgument='<%# Eval("EmployeeServiceId") %>' runat="server">
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

