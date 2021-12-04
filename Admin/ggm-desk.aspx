<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ggm-desk.aspx.cs" Inherits="Admin_news_post" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script type="text/javascript">
         function isNumberKey(evt) {
             var charCode = (evt.which) ? evt.which : event.keyCode
             if (charCode > 31 && (charCode < 48 || charCode > 57))
                 return false;

             return true;
         }
     </script>
    <div class="admin_breadcrumb">
        Admin > GGM Desk
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
                                    <h3 class="panel-title"><strong>Add/Update</strong> Record</h3>
                                </div>
                                <div class="panel-body">

                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                               <asp:HiddenField ID="hfRID" runat="server" />
                                                <asp:HiddenField ID="hfFile_UploadedPath" ClientIDMode="Static" runat="server" />

                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">
                                                            <!-- ENTER Title -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Title</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtTitle" placeholder="Enter Title" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                    <asp:RequiredFieldValidator ID="RFV_Title" runat="server" Display="Dynamic" ErrorMessage="Title cannot be blank" ForeColor="Red" ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
                                                                </div>

                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">URL</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtUrl" placeholder="Enter URL(http:// or https://)" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                                    </div>
                                                                    <span style="font-size: 13px; padding-left: 45%;">OR</span>
                                                                </div>

                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">File</label>
                                                                <div class="col-md-4 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:FileUpload ID="File_Upload" runat="server" class="btn-primary" name="filename" title="Browse file" />
                                                                     </div>
                                                                   </div>
                                                                <span runat="server" style="display:none;font-weight:bold;" id="span_FileStatus"></span>
                                                            </div>

                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Sequence</label>
                                                                <div class="col-md-4 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                       <asp:TextBox runat="server" ID="txtSequence" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="2"></asp:TextBox>
                                                                       
                                                                     </div>
                                                                   </div>
                                                                <span runat="server" style="display:none;font-weight:bold;" id="span1"></span>
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
                                    <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9)">Add New Record</asp:LinkButton>
                                </div>
                                <div class="panel-body panel-body-table">

                                    <asp:GridView ID="gridRecord" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="gridRecord_PageIndexChanging" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SrNo" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRID" runat="server" Text='<%#Eval("SrNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Title" HeaderStyle-Width="500px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="File/Url" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <a id="A1" runat="server" target="_blank" href='<%# DataBinder.Eval(Container,"DataItem.URL") %>'>View</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Sequence" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("Sequence") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ActiveStatus" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkActive" CssClass="btn btn-facebook btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("RID") %>' OnClick="lnkActive_Click" runat="server">
                                                         <asp:Label ID="lblActive" runat="server" ForeColor="White" Text='<%#Eval("ActiveStatus")%>'></asp:Label>

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

