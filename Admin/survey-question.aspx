<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="survey-question.aspx.cs" Inherits="Admin_survey_question" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="admin_breadcrumb">
        Admin ><a href="survey.aspx">Survey ></a>Survey Question
    </div>
    <div class="admin_rhs_content">
        <!-- PAGE CONTENT WRAPPER -->
        <div class="page-content-wrap">
            <div class="col-md-12 innerpage">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                      <b> Survey Title: </b> <span id="SurveyTitle" runat="server"></span>
                        <asp:Panel ID="panelAddNew" runat="server">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong>Add/Update</strong> Survey Question : </h3>
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hdfSurveyId" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfSurveyQuestionId" runat="server" Value="0" />
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top" width="80%">
                                                            <!-- ENTER Article Title -->
                                                            <div class="form-group" style="position: relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Survey Question<span style="color: red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:TextBox ID="txtSurveyQuestion" TextMode="MultiLine" placeholder="Enter survey question" class="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -13px; font-size: small; margin-left: 330px;">
                                                                    <asp:RequiredFieldValidator ID="RFV_txtSurveyQuestion" runat="server" ErrorMessage="Survey question cannot be blank" ForeColor="Red" ControlToValidate="txtSurveyQuestion"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="position: relative;">
                                                                <label class="col-md-3 col-xs-12 control-label">Options<span style="color: red;">*</span></label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                        <asp:DropDownList ID="ddlOptions" class="form-control" runat="server">
                                                                            <asp:ListItem Text="--Select Option--" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="Single Option" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="Multiple Option" Value="2"></asp:ListItem>
                                                                            <asp:ListItem Text="Text" Value="3"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div style="padding-top: .50%; margin-left: 10px; position: absolute; bottom: -13px; font-size: small; margin-left: 330px;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlOptions" InitialValue="0" runat="server" ErrorMessage="Please select option" ForeColor="Red" ControlToValidate="ddlOptions"></asp:RequiredFieldValidator>
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
                                    </asp:UpdatePanel>
                                </div>
                            </div>


                        </asp:Panel>
                        <asp:Panel ID="panelView" runat="server">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <table width="100%">
                                        <tr>
                                            <td align="left">
                                                <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9)">Add New Survey Question</asp:LinkButton>
                                            </td>
                                            <td align="right"><a href="survey.aspx" class="btn btn-primary pull-left" style="background-color: rgba(199, 15, 15, 0.9); float: none!important">Back</a></td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="panel-body panel-body-table">
                                    <asp:GridView ID="grdview" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdview_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SurveyQuestionId" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSurveyQuestionId" runat="server" Text='<%#Eval("SurveyQuestionId")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdfSurveyId" runat="server" Value='<%#Eval("SurveyQuestionId")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Survey Question" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSurveyQuestion" runat="server" Text='<%#Eval("SurveyQuestion")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Option" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%#Eval("QuestionOption")%>
                                                    <asp:HiddenField ID="hdfQuestionOption" runat="server" Value='<%#Eval("SurveyQuestionOption")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("SurveyQuestionId") %>' OnClick="lnkEdit_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Options" HeaderStyle-Width="150px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkSurveyDetails" CssClass="btn btn-facebook btn-rounded bs-actionsbox"
                                                        CausesValidation="false" OnClick="ViewList_Click" CommandArgument='<%# Eval("SurveyQuestionId") %>' runat="server"> <span style="color:white"><span class="fa fa-link "></span>Options</span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkActionDelete" CssClass="btn btn-danger btn-rounded bs-actionsbox" CausesValidation="false" OnClick="Delete_Click" CommandArgument='<%# Eval("SurveyQuestionId") %>' runat="server">
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
                                    <div class="InnerContent" id="Div2" runat="server" style="width: 90%; margin: auto; max-height: 500px; overflow: auto; padding-bottom: 20px;">
                                        <div class=" blank20">
                                        </div>
                                        <div class="gap">
                                        </div>
                                        <div id="Div3">

                                            <div class="chart" id="chart" runat="server">
                                            </div>
                                            <div style="text-align: center;">
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

