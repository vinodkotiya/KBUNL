<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="hindi-word-of-the-day.aspx.cs" Inherits="Admin_thought_of_the_day" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="admin_breadcrumb" id="div_headTitle" runat="server">
       
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
                                    <h3 class="panel-title"><strong>Add/Update</strong>Hindi Word</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hdfDept_Id" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfRID" runat="server" Value="0" />
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">                                                            
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Hindi Word (In English)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtHindiWordEnglish" placeholder="Enter Hindi Word (In English)" class="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px">
                                                                   <asp:RequiredFieldValidator ID="RFV1" runat="server" ErrorMessage="Hindi Word cannot be blank" ForeColor="Red" ControlToValidate="txtHindiWordEnglish"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Hindi Word (In Hindi)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtHindiWordHindi" placeholder="Enter Hindi Word (In Hindi)" class="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px">
                                                                   <asp:RequiredFieldValidator ID="RFV2" runat="server" ErrorMessage="Hindi Word cannot be blank" ForeColor="Red" ControlToValidate="txtHindiWordHindi"></asp:RequiredFieldValidator>
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
                                                    <asp:Button ID="btnSave" class="btn btn-primary pull-right" runat="server" Text="Submit"  ValidationGroup="b"  OnClick="btnSave_Click" />
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
                                    <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9)">Add New Hindi Word</asp:LinkButton>
                                </div>
                                <div class="panel-body panel-body-table">
                                    <asp:GridView ID="grdHindiWords" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False"  AllowPaging="true" PageSize="30" OnPageIndexChanging="grdThoughts_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SrNo.">
                                                <ItemTemplate>
                                                    <%#Eval("Srno")%>
                                                    <asp:HiddenField ID="hdfThoughtId" runat="server" Value='<%#Eval("RID")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hindi Word (English)">
                                                <ItemTemplate>
                                                    <%#Eval("WordOfTheDayEnglish")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hindi Word (Hindi)">
                                                <ItemTemplate>
                                                    <%#Eval("WordOfTheDayHindi")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                             
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("RID") %>' OnClick="lnkEdit_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkActionDelete" CommandArgument='<%# Eval("RID") %>' CssClass="btn btn-danger btn-rounded bs-actionsbox" CausesValidation="false" OnClick="Delete_Click"  runat="server">
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

