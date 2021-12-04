<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="opinion-poll.aspx.cs" Inherits="Admin_news_post" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="admin_breadcrumb">
        Admin > Opinion Poll
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
                                            <h3 class="panel-title"><strong>Add/Update</strong> Poll</h3>
                                        </div>
                                        <div class="panel-body">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>                                                
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="HDN_PollID" runat="server" />                                                
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">                                                           
                                                            <!-- ENTER Article Title -->
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Poll Title</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtTitle" placeholder="Enter Title" class="form-control" runat="server" MaxLength="200" ></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 320px;position:absolute;bottom:-15px;font-size:12px;">
                                                                    <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ErrorMessage="Poll Title cannot be blank" ForeColor="Red" ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Begin Date</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtBeginDate" placeholder="Enter Begin Date" class="form-control" runat="server" MaxLength="10"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalenderExtender1" runat="server" Enabled="true" Format="dd/MM/yyyy" TargetControlID="txtBeginDate"></asp:CalendarExtender>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtBeginDate" runat="server" ValidChars="0123456789/" TargetControlID="txtBeginDate"></asp:FilteredTextBoxExtender>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 200px;position:absolute;bottom:-15px;font-size:12px;">
                                                                    <asp:RegularExpressionValidator ForeColor="Red" ID="regulareexp" runat="server" SetFocusOnError="true" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$" ControlToValidate="txtBeginDate" CssClass="validator" ErrorMessage="Please Select Vaid Date"></asp:RegularExpressionValidator>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Begin Date cannot be blank" ForeColor="Red" ControlToValidate="txtBeginDate"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div> 
                                                            <div class="form-group" style="position:relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">End Date</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtEndDate" placeholder="Enter End Date" class="form-control" runat="server" MaxLength="10"></asp:TextBox>
                                                                         <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" Format="dd/MM/yyyy" TargetControlID="txtEndDate"></asp:CalendarExtender>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtEndDate" runat="server" ValidChars="0123456789/" TargetControlID="txtEndDate"></asp:FilteredTextBoxExtender>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 200px;position:absolute;bottom:-15px;font-size:12px;">
                                                                    <asp:RegularExpressionValidator ForeColor="Red" ID="RegularExpressionValidator1" runat="server" SetFocusOnError="true" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$" ControlToValidate="txtEndDate" CssClass="validator" ErrorMessage="Please Select Vaid Date"></asp:RegularExpressionValidator>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="End Date cannot be blank" ForeColor="Red" ControlToValidate="txtEndDate"></asp:RequiredFieldValidator>
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
                                            <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" style="background-color: rgba(199, 15, 15, 0.9)">Add New Poll</asp:LinkButton>                                            
                                        </div>
                                        <div class="panel-body panel-body-table">   
                                            <asp:GridView ID="grdOpinionMaster" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdOpinionMaster_RowDataBound">
                                                <Columns>
                                                   <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                           <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Blog ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_POPID" runat="server" Text='<%#Eval("PollID")%>'></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Title">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Title" runat="server" Text='<%#Eval("PollTitle")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Begin Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Begindate" runat="server" Text='<%#Eval("BeginDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Last Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_LastDate" runat="server" Text='<%#Eval("LastDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  
                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("PollID") %>' OnClick="lnkEdit_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Add/View Option" HeaderStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkActionViewList" CssClass="btn btn-facebook btn-rounded bs-actionsbox"
                                                                CausesValidation="false" OnClick="ViewList_Click" CommandArgument='<%# Eval("PollID") %>' runat="server"> <span style="color:white"><span class="fa fa-link "></span>Option</span></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="View Voting" HeaderStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkViewVote" OnClick="lnkViewVote_Click"  CommandArgument='<%# Eval("PollID") %>' runat="server">View Vote</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkActionDelete" CssClass="btn btn-danger btn-rounded bs-actionsbox" CausesValidation="false" OnClick="Delete_Click" CommandArgument='<%# Eval("PollID") %>' runat="server">
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
                                 <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowPopUpChart"
                            PopupControlID="PanelShowPopUpChart" BackgroundCssClass="modalBackground" CancelControlID="btnClose">
                        </asp:ModalPopupExtender>
                             <asp:Button ID="btnShowPopUpChart" runat="server" Style="display: none;" />
                              <asp:Button ID="btnCloseChart" runat="server" Style="display: none;" />
                        <asp:Panel ID="PanelShowPopUpChart" runat="server" class="modalpanel_AddOption">
                            <div id="div1" runat="server" class="moduleheader">
                                <div class="staff">
                                </div>
                                <div style="background-color: #ffffff; width: 100%">
                                     <div class="blockheader">Employee Voting</div>
                                    <div class="InnerContent" id="Div2" runat="server" style="width: 90%; margin: auto;
                                        max-height: 500px; overflow: auto; padding-bottom:20px;">
                                        <div class=" blank20">
                                        </div>
                                        <div class="gap">
                                        </div>
                                        <div id="Div3">
                                             <div class="chart" id="chart" runat="server">
                                            </div>
                                            <div style=" text-align:center;">
                                                <asp:Button ID="btnClose" runat="server" Text="Close" />
                                            </div>
                                        </div>
                                    </div>
                                
                                </div>
                                
                                <div style="clear: both;">
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

