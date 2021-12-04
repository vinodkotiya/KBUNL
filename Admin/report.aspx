<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="report.aspx.cs" Inherits="Admin_report" %>

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
                        <asp:HiddenField ID="hdfReportLevelId" runat="server" Value="1" />
                        <asp:HiddenField ID="hdfOtherReportId" runat="server" Value="0" />
                        <asp:HiddenField ID="hdfReportGroup" runat="server" Value="" />
                        <asp:HiddenField ID="hdf" runat="server" Value="" />
                        <asp:Panel ID="panelAddNew" runat="server">
                            <div class="panel panel-">
                                <div class="panel-headidefaultng">
                                    <h3 class="panel-title"><strong>Add/Update</strong> Report</h3>
                                </div>
                                <div class="panel-body">

                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hdfDept_Id" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfReportId" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfFile_UploadedPath" runat="server" Value="No" />
                                                <asp:HiddenField ID="hdfReportUpdateId" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfReportLevel" runat="server" Value="1" />
                                                <asp:HiddenField ID="hdfParentReportId" runat="server" Value="0" />
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">
                                                            <!-- ENTER Type -->
                                                            <div class="form-group" style="position: relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Report Type<span style="color: red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group" style="position: relative;">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:DropDownList ID="ddlReportType" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                                                                            <asp:ListItem Text="--Select Type--" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="Daily" Value="Daily"></asp:ListItem>
                                                                            <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                                                                            <asp:ListItem Text="Yearly" Value="Yearly"></asp:ListItem>
                                                                            <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -15px; padding-left: 315px; font-size: 13px; color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlReportType" InitialValue="0" runat="server" ErrorMessage="Please select report type" ForeColor="Red" ControlToValidate="ddlReportType" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <!-- ENTER Report Date -->
                                                            <div class="form-group" style="position: relative;" id="divRptDate" runat="server">
                                                                <label class="col-md-3 col-xs-12 control-label">Report Date<span style="color: red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group" style="position: relative;">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtReportDate" placeholder="Enter Date" class="form-control" runat="server" MaxLength="10"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender" runat="server" TargetControlID="txtReportDate" ValidChars="0123456789/"></asp:FilteredTextBoxExtender>
                                                                        <asp:CalendarExtender runat="server" TargetControlID="txtReportDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -15px; padding-left: 315px; font-size: 13px; color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtReportDate" runat="server" ErrorMessage="Report date cannot be blank" ForeColor="Red" ControlToValidate="txtReportDate" CssClass="validator"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ForeColor="Red" ID="regulareexp" runat="server" SetFocusOnError="true" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$" ControlToValidate="txtReportDate" CssClass="validator" ErrorMessage="Please Select Vaid Date"></asp:RegularExpressionValidator>
                                                                </div>
                                                            </div>
                                                            <!-- ENTER Report Year-->
                                                            <div class="form-group" style="position: relative;" id="divRptYear" runat="server" visible="false">
                                                                <label class="col-md-3 col-xs-12 control-label">Report Year<span style="color: red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group" style="position: relative;">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:DropDownList ID="ddlYear" class="form-control" runat="server"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -15px; padding-left: 315px; font-size: 13px; color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlYear" runat="server" ErrorMessage="Please select year" InitialValue="0" ForeColor="Red" ControlToValidate="ddlYear" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <!-- ENTER Report Year-->
                                                            <div class="form-group" style="position: relative;" id="divRptMonth" runat="server" visible="false">
                                                                <label class="col-md-3 col-xs-12 control-label">Report Month<span style="color: red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group" style="position: relative;">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:DropDownList ID="ddlmonth" class="form-control" runat="server">
                                                                            <asp:ListItem Value="0" Text="--All--"></asp:ListItem>
                                                                            <asp:ListItem Value="01" Text="JAN"></asp:ListItem>
                                                                            <asp:ListItem Value="02" Text="FEB"></asp:ListItem>
                                                                            <asp:ListItem Value="03" Text="MAR"></asp:ListItem>
                                                                            <asp:ListItem Value="04" Text="APR"></asp:ListItem>
                                                                            <asp:ListItem Value="05" Text="MAY"></asp:ListItem>
                                                                            <asp:ListItem Value="06" Text="JUN"></asp:ListItem>
                                                                            <asp:ListItem Value="07" Text="JUL"></asp:ListItem>
                                                                            <asp:ListItem Value="08" Text="AUG"></asp:ListItem>
                                                                            <asp:ListItem Value="09" Text="SEP"></asp:ListItem>
                                                                            <asp:ListItem Value="10" Text="OCT"></asp:ListItem>
                                                                            <asp:ListItem Value="11" Text="NOV"></asp:ListItem>
                                                                            <asp:ListItem Value="12" Text="DEC"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -15px; padding-left: 315px; font-size: 13px; color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlmonth" InitialValue="0" runat="server" ErrorMessage="Please select month" ForeColor="Red" ControlToValidate="ddlmonth" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <!-- ENTER Report Title-->
                                                            <div class="form-group" style="position: relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Report Title (English)<span style="color: red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group" style="position: relative;">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtTitleEnglish" placeholder="Enter Report Title" class="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -15px; padding-left: 315px; font-size: 13px; color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtTitle" runat="server" ErrorMessage="Report title cannot be blank" ForeColor="Red" ControlToValidate="txtTitleEnglish" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="position: relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Report Title (Hindi)<span style="color: red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group" style="position: relative;">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtTitleHindi" placeholder="Enter Report Title" class="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -15px; padding-left: 315px; font-size: 13px; color: red;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Report title cannot be blank" ForeColor="Red" ControlToValidate="txtTitleHindi" CssClass="validator"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="position: relative;" id="divrptGroup" runat="server" visible="false">
                                                                <label class="col-md-3 col-xs-12 control-label">Report Group</label>
                                                                <div class="col-md-6 col-xs-12" style="width: 15%;">
                                                                    <div class="input-group">
                                                                        <asp:CheckBox ID="chkReportGroup" class="form-control" Style="padding: 10px !important;" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="chkReportGroup_CheckedChanged"></asp:CheckBox>
                                                                    </div>
                                                                </div>
                                                                <label class="col-md-3 col-xs-12 control-label" style="width: 10%;">Seq No<span style="color: red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12" style="width: 15%;">
                                                                    <div class="input-group">
                                                                        <asp:TextBox ID="txtSeqNo" class="form-control" runat="server" MaxLength="3"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSeqNo" runat="server" TargetControlID="txtSeqNo" ValidChars="0123456789"></asp:FilteredTextBoxExtender>
                                                                    </div>
                                                                </div>
                                                                <div style="position: absolute; bottom: -15px; margin-left: 50%;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtSeqNo" runat="server" Display="Dynamic" ErrorMessage="Seq no. cannot be blank" ForeColor="Red" ControlToValidate="txtSeqNo"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divlnkType" runat="server" visible="false">
                                                                <label class="col-md-3 col-xs-12 control-label">Link Type<span style="color: red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group" style="width: 60%;">
                                                                        <table cellspacing="0" cellpadding="0" style="border: none; border-collapse: collapse; width: 100%; margin-top: 5px;">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:RadioButton ID="rbtnAttachment" class="form-control" runat="server" Text="Attachment" GroupName="lnk" AutoPostBack="true" OnCheckedChanged="rbtnAttachment_CheckedChanged" /></td>
                                                                                <td>
                                                                                    <asp:RadioButton ID="rbtnURL" class="form-control" runat="server" Text="URL" GroupName="lnk" AutoPostBack="true" OnCheckedChanged="rbtnURL_CheckedChanged" /></td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divURL" runat="server" visible="false">
                                                                <label class="col-md-3 col-xs-12 control-label">URL<span style="color: red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtUrl" placeholder="Enter URL(http:// or https://)" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                                                    </div>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtUrl" runat="server" Display="Dynamic" ErrorMessage="URL cannot be blank" ForeColor="Red" ControlToValidate="txtUrl"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <!-- Attach File -->
                                                            <div class="form-group" id="divAttachment" runat="server">
                                                                <label class="col-md-3 col-xs-12 control-label">Attachment<span style="color: red;">*</span></label>
                                                                <div class="col-md-4 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"></span>
                                                                        <asp:FileUpload ID="FileUploadImage" runat="server" class="btn-primary" name="filename" title="Browse file" />
                                                                    </div>
                                                                    <div><small>doc, xls, pdf, jpg, gif and png extensions supported</small></div>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorFileUploadImage" runat="server" Display="Dynamic" ErrorMessage="Plese upload file" ForeColor="Red" ControlToValidate="FileUploadImage"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <span runat="server" style="display: none; font-weight: bold;" id="span_FileStatus"></span>
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
                                    <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9)">Add New Report</asp:LinkButton>
                                    <a id="lnkbtnBack" runat="server" class="btn btn-primary pull-right" style="background-color: rgba(199, 15, 15, 0.9)">Back</a>
                                </div>
                                <div class="panel-body panel-body-table">
                                    <asp:GridView ID="grdview" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Report Type" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("ReportType") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Report Group" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("ReportGroupCheck") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Title (English/Hindi)" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdfReportIdGrd" runat="server" Value='<%#Eval("ReportId")%>' />
                                                    <asp:Label ID="lblTitleEnglish" runat="server" Text='<%#Eval("ReportTitleEnglish") %>'></asp:Label><br />
                                                    <asp:Label ID="lblTitleHindi" runat="server" Text='<%#Eval("ReportTitleHindi") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="File" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <a id="A1" runat="server" target="_blank" href='<%#Eval("AttachmentURL")%>' visible='<%#(Convert.ToBoolean(Eval("DownloadButtonVisible")))%>'>View File</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upload On" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Eval("UploadOn") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("ReportId") %>' OnClick="Update_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Report" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <a id="linkReports" runat="server" visible='<%#(Convert.ToBoolean(Eval("ReportButtonVisible")))%>' href='<%#Eval("NextLevelLink")%>' class="btn btn-danger btn-rounded bs-actionsbox"><span>Reports</span></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkActionDelete" CssClass="btn btn-danger btn-rounded bs-actionsbox" CausesValidation="false" OnClick="Delete_Click" CommandArgument='<%# Eval("ReportId") %>' runat="server">
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

