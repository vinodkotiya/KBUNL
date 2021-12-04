<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="video.aspx.cs" Inherits="Admin_news_post" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .form-control {
            padding-top: 3px;
            padding-bottom: 3px;
        }
    </style>
    <div class="admin_breadcrumb">
        Admin > Videos
    </div>
    <div class="admin_rhs_content">
        <!-- PAGE CONTENT WRAPPER -->
        <div class="page-content-wrap">
            <div class="col-md-12 innerpage">

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hfVideoID" runat="server" />
                        <asp:HiddenField ID="hfLink_UploadedPath" runat="server" />
                        <asp:HiddenField ID="hfCoverImage_UploadedPath" runat="server" />
                        <asp:Panel ID="panelAddProgress" runat="server">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong>Add/Update</strong> Progress Video</h3>
                                </div>
                                <div id="alert" runat="server">
                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="panel-body">

                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">

                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="50%">
                                                            <!-- ENTER Article Title -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Title</label>
                                                                <div class="col-md-4 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtProTitle" runat="server" CssClass="form-control"></asp:TextBox>

                                                                    </div>
                                                                    <asp:RequiredFieldValidator ID="rfvProgress" Display="Dynamic" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtProTitle" ValidationGroup="g_pro"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px">
                                                                    
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Cover Image</label>
                                                                <div class="col-md-4 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:FileUpload ID="FileUploadProgressImg" runat="server" class="btn-primary" name="filename" title="Browse file" Width="100%" />
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Video Link</label>
                                                                <div class="col-md-4 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:FileUpload ID="FileUploadProgressLink" runat="server" class="btn-primary" name="filename" title="Browse file" Width="100%" />
                                                                    </div>

                                                                </div>

                                                            </div>
                                                        </td>

                                                    </tr>

                                                </table>
                                                <!--Error Message-->

                                                <br />
                                                <div class="panel-footer">
                                                    <asp:Button ID="btnClearPro" CausesValidation="false" class="btn btn-default" runat="server" Text="Close Form" OnClick="btnClosePro_Click" />
                                                    <asp:Button ID="btnSave" class="btn btn-primary pull-right" runat="server" Text="Save" OnClick="btnProgressSave_Click" ValidationGroup="g_pro" />
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
                        <asp:Panel ID="panelAddSafety" runat="server">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong>Add/Update</strong>  Safety Video</h3>

                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="50%">
                                                            <!-- ENTER Article Title -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Title</label>
                                                                <div class="col-md-4 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtSafetyTitle" runat="server" CssClass="form-control"></asp:TextBox>

                                                                    </div>
                                                                     <asp:RequiredFieldValidator ID="RFV_safety" runat="server" Display="Dynamic" ErrorMessage="Required" ValidationGroup="g_safe" ForeColor="Red" ControlToValidate="txtSafetyTitle"></asp:RequiredFieldValidator>
                                                                </div>
                                                               
                                                            </div>

                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Cover Image</label>
                                                                <div class="col-md-4 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:FileUpload ID="FileUploadSafetyImg" runat="server" class="btn-primary" name="filename" title="Browse file" Width="100%" />
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Video Link</label>
                                                                <div class="col-md-4 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:FileUpload ID="FileUploadSafetyLink" runat="server"  class="btn-primary" name="filename" title="Browse file" Width="100%" />
                                                                    </div>

                                                                </div>

                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>

                                             
                                                <br />
                                                <div class="panel-footer">
                                                    <asp:Button ID="btnClearSafety" CausesValidation="false" class="btn btn-default" runat="server" Text="Close Form" OnClick="btnClearSafety_Click" />
                                                    <asp:Button ID="btnSaveSafety" class="btn btn-primary pull-right" runat="server" ValidationGroup="g_safe" Text="Save" OnClick="btnSaveSafety_Click" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSaveSafety" />

                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <!--Error Message-->

                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="panelView" runat="server">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <asp:LinkButton ID="lbtn_AddPro" runat="server" OnClick="lbtn_AddPro_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9)">Add Progress</asp:LinkButton>
                                    <asp:LinkButton ID="lnkAddSafety" runat="server" OnClick="lnkAddSafety_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9); margin-left: 1%;">Add Safety</asp:LinkButton>
                                </div>

                                <div class="panel-body panel-body-table">

                                    <asp:GridView ID="gridMagazine" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Title" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVideoTitle" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType" runat="server" Text='<%#Eval("VideoType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cover Image" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <img src='<%#Eval("VideoCoverImg") %>' width="60%" alt="" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Link" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <a id="A1" runat="server" target="_blank" href='<%# DataBinder.Eval(Container,"DataItem.VideoLink") %>'>View Link</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
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

