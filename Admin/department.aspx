<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="department.aspx.cs" Inherits="Admin_UploadType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../js/showImages.js"></script>
    <style type="text/css">
        .visiblelink {
            display: inline-block;
        }

        .hidelink {
            display: none;
        }
    </style>
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
                                    <h3 class="panel-title" id="h_Tag" runat="server"></h3>
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">

                                                <asp:HiddenField ID="hfUserID" runat="server" />
                                                <asp:HiddenField ID="hfImage_UploadedPath" runat="server" />

                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="70%">

                                                            <!-- Event Title -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Title</label>
                                                                <div class="col-md-8 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtTitle" placeholder="Enter Title" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                    <asp:RequiredFieldValidator ID="RFV_Title" Display="Dynamic" runat="server" ValidationGroup="grp" ErrorMessage="Title cannot be blank" ForeColor="Red" ControlToValidate="txtTitle" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                              
                                                            </div>

                                                            <!--  Description -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Description English</label>
                                                                <div class="col-md-8 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtDescription" placeholder="Enter Description" class="form-control" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                                    </div>
                                                                     <asp:RequiredFieldValidator ID="rfv_Des" Display="Dynamic" runat="server" ValidationGroup="grp" ErrorMessage="Description cannot be blank" ForeColor="Red" ControlToValidate="txtDescription" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

                                                            <!--  Description H-->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Description Hindi</label>
                                                                <div class="col-md-8 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtDesH" placeholder="Enter Hindi Description" class="form-control" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                                    </div>
                                                                     <asp:RequiredFieldValidator ID="rfv_DesH" Display="Dynamic" runat="server" ValidationGroup="grp" ErrorMessage="Description cannot be blank" ForeColor="Red" ControlToValidate="txtDescription" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                              <!--  Vision E-->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Vision English</label>
                                                                <div class="col-md-8 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtVisionE" placeholder="Enter English Vision" class="form-control" runat="server" TextMode="MultiLine" Rows="3" MaxLength="300"></asp:TextBox>
                                                                    </div>
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="grp" runat="server" ErrorMessage="Vision cannot be blank" ForeColor="Red" ControlToValidate="txtVisionE" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                              <!--  Vision H-->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Vision Hindi</label>
                                                                <div class="col-md-8 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtVisionH" placeholder="Enter Hindi Vision" class="form-control" runat="server" TextMode="MultiLine" Rows="3" MaxLength="500"></asp:TextBox>
                                                                    </div>
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ValidationGroup="grp" runat="server" ErrorMessage="Vision cannot be blank" ForeColor="Red" ControlToValidate="txtVisionH" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <!-- Attachment -->
                                                            <div class="form-group">
                                                                <label class="col-md-3 col-xs-12 control-label">Image</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <asp:FileUpload ID="FileUploader1" runat="server" accept="image/*" class="btn-primary" name="filename" onchange="showMyImage(this)" title="Browse file" />
                                                                    </div>
                                                                </div>
                                                            </div>


                                                        </td>
                                                        <td>
                                                            <div class="form-group">


                                                                <div class="input-group">
                                                                    <img src="" runat="server" clientidmode="Static" id="divShowImage" alt="" style="width: 100%" />
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
                                                    <asp:Button ID="btnSave" class="btn btn-primary pull-right" runat="server" Text="Submit" ValidationGroup="grp" OnClick="btnSave_Click" />
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

