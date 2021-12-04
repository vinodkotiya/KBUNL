<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="gallery-album-photos.aspx.cs" Inherits="Admin_news_post" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../js/showImages.js"></script>
    <asp:HiddenField ID="hfAlbumID" runat="server" />
    <div class="admin_breadcrumb" runat="server" id="div_headTitle">
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
                                    <h3 class="panel-title"><strong>Add/Update</strong> options : <span id="spanAlbumTitle" runat="server"></span></h3>
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hfPhotoID" runat="server" />
                                                <asp:HiddenField ID="hfImage_UploadedPath" runat="server" />
                                                <asp:HiddenField ID="hfThumbnailImage_UploadedPath" runat="server" />
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="40%">
                                                            <!--ENTER Flie Uploader  -->
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Image 1</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <asp:FileUpload ID="FileUploadImage1" runat="server" accept="image/*" class="btn-primary" onchange="showMyImage(this)" name="filename" title="Browse file" />
                                                                  
                                                                </div>
                                                            </div>
                                                             <!--ENTER Flie Uploader  -->
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Image 2</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <asp:FileUpload ID="FileUploadImage2" runat="server" accept="image/*" class="btn-primary" onchange="showMyImage(this)" name="filename" title="Browse file" />
                                                                
                                                                </div>
                                                            </div>
                                                             <!--ENTER Flie Uploader  -->
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Image 3</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <asp:FileUpload ID="FileUploadImage3" runat="server" accept="image/*" class="btn-primary" onchange="showMyImage(this)" name="filename" title="Browse file" />
                                                                  
                                                                </div>
                                                            </div>
                                                             <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Image 4</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <asp:FileUpload ID="FileUploadImage4" runat="server" accept="image/*" class="btn-primary" onchange="showMyImage(this)" name="filename" title="Browse file" />
                                                                   
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Image 5</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <asp:FileUpload ID="FileUploadImage5" runat="server" accept="image/*" class="btn-primary" onchange="showMyImage(this)" name="filename" title="Browse file" />
                                                                    
                                                                </div>
                                                            </div>                                                            
                                                        </td>
                                                        <td  valign="top" width="40%">
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Image 6</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <asp:FileUpload ID="FileUploadImage6" runat="server" accept="image/*" class="btn-primary" onchange="showMyImage(this)" name="filename" title="Browse file" />
                                                                </div>
                                                            </div>

                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Image 7</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <asp:FileUpload ID="FileUploadImage7" runat="server" accept="image/*" class="btn-primary" onchange="showMyImage(this)" name="filename" title="Browse file" />
                                                                </div>
                                                            </div>

                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Image 8</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <asp:FileUpload ID="FileUploadImage8" runat="server" accept="image/*" class="btn-primary" onchange="showMyImage(this)" name="filename" title="Browse file" />
                                                                </div>
                                                            </div>

                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Image 9</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <asp:FileUpload ID="FileUploadImage9" runat="server" accept="image/*" class="btn-primary" onchange="showMyImage(this)" name="filename" title="Browse file" />
                                                                </div>
                                                            </div>

                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Image 10</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <asp:FileUpload ID="FileUploadImage10" runat="server" accept="image/*" class="btn-primary" onchange="showMyImage(this)" name="filename" title="Browse file" />
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <div class="form-group">
                                                                <div class="input-group">
                                                                    <img src="" runat="server" clientidmode="Static" id="divShowImage" alt="" style="width: 80%" />
                                                                </div>
                                                            </div>
                                                             <div class="form-group">
                                                                <div class="input-group">
                                                                    <img src="" runat="server" clientidmode="Static" id="divShowThumbImage" alt="" style="width: 50%" />
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>

                                                <div><small>jpg, gif and png extensions supported</small></div>
                                                <!--Error Message-->
                                                <div id="alert" runat="server" style="margin-top: 15px;">
                                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                                </div>
                                                <br />
                                                <div class="panel-footer">
                                                    <asp:Button ID="btnClear" CausesValidation="false" class="btn btn-default" runat="server" Text="Cancel" OnClick="btnClose_Click" />
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
                                <div class="panel-heading" style="display: none;">
                                    <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9)">Add New Option</asp:LinkButton>
                                </div>
                                <div class="panel-body panel-body-table">
                                    <asp:HiddenField ID="hfQuizID" runat="server" />
                                    <asp:GridView ID="gridQuizOption" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" Visible="false" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbOptionID" runat="server" Text='<%#Eval("PhotoID") %>' Visible="true"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Image" HeaderStyle-Width="200px">
                                                <ItemTemplate>
                                                    <img src='<%#Eval("PhotoPath") %>' style="width: 50%;" />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Thumb Image" HeaderStyle-Width="200px">
                                                <ItemTemplate>
                                                    <img src='<%#Eval("PhotoThumbnailPath") %>' style="width: 30%;" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkActionDelete" CssClass="btn btn-danger btn-rounded bs-actionsbox" CausesValidation="false" OnClick="Delete_Click" CommandArgument='<%# Eval("PhotoID") %>' runat="server">
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

        <style type="text/css">
            label{padding-top:0px!important;}
        </style>
    </div>
</asp:Content>

