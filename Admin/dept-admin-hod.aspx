<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="dept-admin-hod.aspx.cs" Inherits="Admin_pop_up_images" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="admin_breadcrumb">
        Admin > Department Admin
    </div>
    <div class="admin_rhs_content">


        <!-- PAGE CONTENT WRAPPER -->
        <div class="page-content-wrap">

            <div class="col-md-12 innerpage">

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="HDN_EID" runat="server" Value="0" />
                        <asp:Panel ID="panelAddNew" runat="server">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong>Add</strong> Department Admin </h3>
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hfRID" runat="server" />


                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">
                                                            <!--ENTER Flie Uploader  -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Employee No.<span style="color: red;">*</span></label>
                                                                <div class="col-md-3 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtEmpIdAdmin" placeholder="Emp No." class="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <asp:RequiredFieldValidator ID="rfvAdmin" Display="Dynamic" ForeColor="Red" Font-Size="13px" runat="server" ErrorMessage="Enter Emp No. For Admin" ControlToValidate="txtEmpIdAdmin" ValidationGroup="GrpAdm"></asp:RequiredFieldValidator>
                                                                </div>                                                                
                                                            </div>

                                                            <div class="form-group" style="position: relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Department<span style="color: red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group" style="position: relative;">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:DropDownList ID="ddlDepartment" class="form-control" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label"></label>
                                                                <asp:Button ID="btnSaveAdmin" class="btn btn-primary" runat="server" Text="Add" OnClick="btnSaveAdmin_Click" ValidationGroup="GrpAdm" style="margin-left:15px;"/>
                                                                <asp:Label ID="lblAdmin" runat="server" ForeColor="Red" Font-Size="13px"></asp:Label>
                                                            </div>
                                                        </td>

                                                    </tr>
                                                </table>
                                                <!--Error Message-->
                                                <div id="alert" runat="server" style="margin-top: 15px;">
                                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                                </div>

                                            </div>
                                        </ContentTemplate>

                                    </asp:UpdatePanel>
                                </div>
                            </div>


                        </asp:Panel>
                        <asp:Panel ID="panelView" runat="server">
                            <div class="panel panel-default">
                                <div class="panel-body panel-body-table">
                                    <!--Error Message-->
                                    <div id="alertgrid" runat="server">
                                        <asp:Label ID="lblgridmsg" runat="server" Text=""></asp:Label>
                                    </div>
                                        <asp:GridView ID="gridDptAdmin" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Emp-ID" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdfEmployeeIdAdmin" runat="server" Value='<%# Eval("EID") %>' />
                                                            <asp:HiddenField ID="hdfEmployeeIdAdminCode" runat="server" Value='<%# Eval("EID") %>' />
                                                        <%# Eval("EmpCode") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="140px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%# Eval("EmpName") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%# Eval("Department") %>' runat="server" ID="lblAdminDept"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Assign" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkActionDelete" CssClass="btn btn-danger btn-rounded btn-facebook" CausesValidation="false" OnClick="ViewModule_Click" CommandArgument='<%# Eval("EmpCode") %>' runat="server">
                                                                        <span style="color:white">Assign Module</span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkActionDeleteAdmin" CssClass="btn btn-danger btn-rounded bs-actionsbox" CausesValidation="false" OnClick="DeleteAdmin_Click" CommandArgument='<%# Eval("EmpCode") %>' runat="server">
                                                                        <span>Delete</span>
                                                        </asp:LinkButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtenderDelete" runat="server" TargetControlID="lnkActionDeleteAdmin" ConfirmText="Are you sure you want to delete this record?"></asp:ConfirmButtonExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Reset_PWD to 123456" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkActionResetPassword" CssClass="btn btn-info btn-rounded bs-actionsbox" CausesValidation="false" OnClick="ResetPassword_Click" CommandArgument='<%# Eval("EID") %>' runat="server" ToolTip="Reset Password"> <span>Reset</span> </asp:LinkButton>
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtenderResetPassword" runat="server" TargetControlID="lnkActionResetPassword" ConfirmText="Are you sure you want to reset password of this employee?"></asp:ConfirmButtonExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                </div>
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


