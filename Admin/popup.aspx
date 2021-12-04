<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="popup.aspx.cs" Inherits="Admin_popup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        .visiblelink {display: inline-block;}
        .hidelink {display: none;}
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
                                    <h3 class="panel-title"><strong>Add/Update </strong>Popup</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hdfDept_Id" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfRIDPopup" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfAttachmentPath" runat="server" Value="NA" />
                                                <asp:HiddenField ID="hdfPopupImagePath" runat="server" Value="NA" />
                                                <asp:HiddenField ID="hdfPopupVideoPath" runat="server" Value="NA" />

                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">

                                                            <!-- Popup Title -->
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Title (English)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtTitleEnglish" placeholder="Enter Title" class="form-control" runat="server" MaxLength="150"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                 <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ErrorMessage="Title cannot be blank" ForeColor="Red" ControlToValidate="txtTitleEnglish" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                             <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Title (Hindi)<span style="color:red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtTitleHindi" placeholder="Enter Title" class="form-control" runat="server" MaxLength="150"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                 <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtTitleHindi" runat="server" ErrorMessage="Title cannot be blank" ForeColor="Red" ControlToValidate="txtTitleHindi" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

                                                            <!-- Popup Type -->
                                                             <div class="form-group" style="position: relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Popup Type</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group" style="position: relative;">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:DropDownList ID="ddlPopupType" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPopupType_SelectedIndexChanged">
                                                                            <asp:ListItem Text="Image" Value="Image"></asp:ListItem>
                                                                            <asp:ListItem Text="Video" Value="Video"></asp:ListItem>
                                                                            <asp:ListItem Text="Text" Value="Text"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <!-- Popup Image -->
                                                            <div class="form-group"  style="position:relative;" id="divPopupImage" runat="server">
                                                                <label class="col-md-3 col-xs-12 control-label">Popup Image</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:FileUpload ID="fupPopupImage" runat="server" class="btn-primary" name="filename" title="Browse file" CssClass="form-control" />
                                                                    </div>
                                                                    <div><small>jpg, gif and png extensions supported</small></div>
                                                                </div>
                                                            </div>

                                                            <!-- Popup Video -->
                                                            <div class="form-group"  style="position:relative;" id="divPopupVideo" runat="server">
                                                                <label class="col-md-3 col-xs-12 control-label">Popup Video</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:FileUpload ID="fupPopupVideo" runat="server" class="btn-primary" name="filenamevideo" title="Browse file" CssClass="form-control" />
                                                                    </div>
                                                                    <div><small>mp4, mov and avi extensions supported</small></div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group" style="position: relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Document Link Type</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group" style="position: relative;">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:DropDownList ID="ddlLinkType" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLinkType_SelectedIndexChanged" >
                                                                             <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="Attachment" Value="Attachment"></asp:ListItem>
                                                                            <asp:ListItem Text="Link" Value="Link"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                  <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlLinkType" runat="server" ErrorMessage="Please Select Link Type" ForeColor="Red" ControlToValidate="ddlLinkType" CssClass="validator" InitialValue="0" Enabled="false"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

                                                            <!-- Attachment -->
                                                            <div class="form-group" id="divAttachment" runat="server" visible="false" style="position: relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Attachment (if any)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <asp:FileUpload ID="fupAttachment" runat="server" class="btn-primary" name="filename" title="Browse file" />
                                                                    </div>
                                                                </div>
                                                                 <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorfupAttachment" runat="server" ErrorMessage="Please Upload Image" ForeColor="Red" ControlToValidate="fupAttachment" CssClass="validator" Enabled="false"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

                                                            <!-- Link URL -->
                                                            <div class="form-group" id="divURL" runat="server" visible="false" style="position: relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Link URL (if any)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtLinkURL" placeholder="Enter Link URL (Optional)" class="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                 <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtLinkURL" runat="server" ErrorMessage="Please Enter Link URL" ForeColor="Red" ControlToValidate="txtLinkURL" CssClass="validator" Enabled="false"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                             <!-- Popup Content -->
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Details (English)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtDetailsEnglish" placeholder="Enter Details" TextMode="MultiLine" class="form-control" runat="server" MaxLength="150"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Details (Hindi)</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtDetailsHindi" placeholder="Enter Details" TextMode="MultiLine" class="form-control" runat="server" MaxLength="150"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Display From (Date Time)<span style="color:red;">*</span></label>
                                                                <div class="col-md-3 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtDisplayFromDate" placeholder="Display From Date" class="form-control" runat="server" MaxLength="10"></asp:TextBox>                                                                        
                                                                        <asp:DropDownList ID="ddlDisplayFromTime" runat="server">
                                                                        </asp:DropDownList>
                                                                        <asp:CalendarExtender runat="server" TargetControlID="txtDisplayFromDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                    </div>
                                                                </div>
                                                                 <div style="padding-top: .50%; margin-left: 10px;position:absolute;bottom: -15px;padding-left: 315px;font-size: 13px;color: red;">
                                                                    <asp:RequiredFieldValidator ID="RFVDisplayFrom" runat="server" ErrorMessage="Display From Date cannot be blank" ForeColor="Red" ControlToValidate="txtDisplayFromDate" CssClass="validator"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ForeColor="Red" ID="REVDisplayFrom" runat="server" SetFocusOnError="true" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$" ControlToValidate="txtDisplayFromDate" CssClass="validator" ErrorMessage="Please Enter Valid Date"></asp:RegularExpressionValidator>
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
                                    <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9)">Add New Popup</asp:LinkButton>
                                </div>
                                   <!--Error Message-->
                            <div id="alertgrid" runat="server">
                                <asp:Label ID="lblgridmsg" runat="server" Text=""></asp:Label>
                            </div>
                                <div class="panel-body panel-body-table">
                                    <asp:GridView ID="grdview" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="30" OnPageIndexChanging="grdview_PageIndexChanging">
                                        <Columns>
                                             <asp:TemplateField HeaderText="Srno" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdfRIDPopupGrd" runat="server" Value='<%#Eval("RID") %>' />
                                                    <%#Container.DataItemIndex+1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Image/Video" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div id="divimagebox" runat="server" visible='<%#Eval("PopupType").ToString()=="Image"%>'>
                                                        <img src='<%#Eval("PopupImage")%>' alt="" width="120px" />
                                                    </div>
                                                    <div id="divvideobox" runat="server" visible='<%#Eval("PopupType").ToString()=="Video"%>'>
                                                        <video width="100%" controls="">
                                                            <source src='<%#Eval("PopupVideo")%>' type="video/mp4">
                                                            Your browser does not support the video tag.
                                                        </video>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             
                                           
                                            <asp:TemplateField HeaderText="Title" HeaderStyle-Width="200px">
                                                <ItemTemplate>
                                                    <b><%#Eval("TitleEnglish") %></b><br />
                                                    <b id="titlehindi" runat="server" visible='<%#Eval("TitleHindi").ToString() !=Eval("TitleEnglish").ToString() %>'><%#Eval("TitleHindi") %></b>
                                                </ItemTemplate>
                                            </asp:TemplateField>      
                                            <asp:TemplateField HeaderText="Link Type" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                  <%#Eval("LinkType")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attachment/Link URL" HeaderStyle-Width="200px">
                                                <ItemTemplate>
                                                    <a href='<%#Eval("LinkAndAttachment")%>' target="_blank" style="color: #0067ac;"><%#Eval("LinkAndAttachment")%></a><br />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="IsActive" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" Checked='<%#Eval("IsActive") %>' CommandArgument='<%# Eval("RID") %>' AutoPostBack="true" OnCheckedChanged="cbSwitch_CheckedChanged" runat="server" />
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
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtenderDelete" runat="server" TargetControlID="lnkActionDelete" ConfirmText="Are you sure you want to delete this record?"></asp:ConfirmButtonExtender>
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

