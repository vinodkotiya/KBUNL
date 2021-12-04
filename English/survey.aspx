<%@ Page Language="C#" AutoEventWireup="true" CodeFile="survey.aspx.cs" Inherits="English_survey" ClientIDMode="AutoID" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/English/WCCommon/WcHeadTags.ascx" TagPrefix="uc1" TagName="WcHeadTags" %>
<%@ Register Src="~/English/WCCommon/WCTopBar.ascx" TagPrefix="uc1" TagName="WCTopBar" %>
<%@ Register Src="~/English/WCCommon/WCFooter.ascx" TagPrefix="uc1" TagName="WCFooter" %>
<%@ Register Src="~/English/WCCommon/WCEmployeeServices.ascx" TagPrefix="uc1" TagName="WCEmpServices" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--Header - Start-->
    <uc1:WcHeadTags runat="server" ID="WcHeadTags" />
    <!--Header - End-->

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptmgr" runat="server"></asp:ScriptManager>
        <div class="pagecontainer">
            <!--MenuBar - Start-->
            <uc1:WCTopBar runat="server" ID="WCTopBar1" />
            <!--MenuBar - End-->

            <!--Content - Start-->
            <div class="content-outer">

                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="contentbox1">
                            <!-- Employee Services - Start -->
                            <uc1:WCEmpServices runat="server" ID="WCEmpServices" />
                            <!-- Employee Services - End -->
                        </td>
                        <td class="contentboxinner">
                            <!--Inner Banner - Start -->
                            <div class="innerbanner">
                                <img src="../images/banner-survey.jpg" alt="" />
                                <div class="bannerpatch">
                                    <h4>Survey</h4>
                                    <div class="breadcrumb">
                                        <a href="Default.aspx" class="home">Home</a>
                                        <a class="active">Survey</a>
                                    </div>
                                </div>
                            </div>
                            <!--Inner Banner - End -->
                            <!--About Department - Start -->
                            <asp:UpdatePanel ID="updatepnl" runat="server">
                                <ContentTemplate>
                                    <div class="surveycontainer">
                                        <div class="surveycontent">
                                            <asp:HiddenField ID="hdfSurveyId" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdfIPAddress" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdfDeptId" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdfEmpName" runat="server" Value="" />
                                            <asp:HiddenField ID="hdfEmpCode" runat="server" Value="" />
                                            <asp:HiddenField ID="hdfMobileNo" runat="server" Value="" />
                                            <asp:HiddenField ID="hdfPassword" runat="server" Value="" />
                                            <asp:HiddenField ID="hdfSurveyResponseId" runat="server" Value="0" />
                                            <asp:Panel ID="pnlnotavailable" runat="server" Visible="false" CssClass="notavailable">
                                                <span>
                                                    There are no surveys currently available.<br/>
                                                    Please visit later.
                                                </span>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlSurvey" runat="server" Visible="false">
                                                    <asp:DataList ID="dtlstSurvey" runat="server">
                                                        <ItemTemplate>
                                                            <div class="box survey">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:HiddenField ID="hdfDataListSurveyId" runat="server" Value='<%#Eval("SurveyId")%>' />
                                                                            <asp:HiddenField ID="hdfSurveyTitle" runat="server" Value='<%#Eval("SurveyName")%>' />
                                                                           <h5><b><%#Container.ItemIndex+1%>.</b></h5>
                                                                        </td>
                                                                        <td> <h5><%#Eval("SurveyName")%></h5></td>
                                                                        <td class='download'>
                                                                            <asp:LinkButton ID="lnkbtnSelect" runat="server" Text="Select" OnClick="lnkbtnSelect_Click"> Take The Survey</asp:LinkButton></td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlEmp" runat="server" Visible="false">
                                                
                                                <div class="empdata">
                                                    <p style="color:#466522;font-size:16px;padding:10px;">Please login with employee number and password.</p>
                                                     <div class="inputbox">
                                                        <asp:TextBox ID="txtEmpCode" placeholder="Enter user name" runat="server" Text=""></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCode" runat="server" ErrorMessage="Please enter user name" ControlToValidate="txtEmpCode" CssClass="validator" ValidationGroup="V"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="inputbox">
                                                        <asp:TextBox ID="txtPassword" runat="server" placeholder="Enter Password" TextMode="Password" Text=""></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ErrorMessage="Please enter password" ControlToValidate="txtPassword" CssClass="validator" ValidationGroup="V"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <asp:LinkButton ID="lnkbtnParticipate" runat="server" Text="Participate" OnClick="lnkbtnParticipate_Click" ValidationGroup="V"></asp:LinkButton>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlSurveyQuestion" runat="server" Visible="false">
                                               <div class="surveytitle" style="font-size:17px; line-height:24px;"><b> <asp:Label ID="lblSurveyTitle" runat="server" Text=""></asp:Label></b></div>
                                                    <asp:DataList ID="dtlstSurveyQuestion" runat="server" RepeatDirection="Vertical" CssClass="surveyquestion" OnItemDataBound="dtlstSurveyQuestion_ItemDataBound">
                                                        <ItemTemplate>
                                                            <div class="box survey">
                                                                <table>
                                                                    <tr>
                                                                        <td style="width:30px; padding-top: 2px;">
                                                                            <asp:HiddenField ID="hdfSurveyQuestionId" runat="server" Value='<%#Eval("SurveyQuestionId")%>' />
                                                                            <asp:HiddenField ID="hdfSurveyQuestionOption" runat="server" Value='<%#Eval("SurveyQuestionOption")%>' />
                                                                            <span style="font-size:16px;"><%#Container.ItemIndex+1%>.</span>
                                                                        </td>
                                                                        <td><h5><%#Eval("SurveyQuestion")%></h5></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:30px;">&nbsp;</td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rbtnlstOptions" runat="server" RepeatDirection="Horizontal" CssClass="surveyoptions"></asp:RadioButtonList>
                                                                            <asp:CheckBoxList ID="chklstOptions" runat="server" RepeatDirection="Horizontal" CssClass="surveyoptions"></asp:CheckBoxList>
                                                                            <asp:TextBox ID="txtQuestionReply" runat="server" CssClass="inputtext" TextMode="MultiLine"></asp:TextBox>                                                                            
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                <asp:LinkButton ID="lbkbtnSubmit" runat="server" Text="Submit" OnClick="lbkbtnSubmit_Click" Width="65px" style="text-align:center;margin-left:30px;" ></asp:LinkButton>
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender" runat="server" TargetControlID="lbkbtnSubmit" ConfirmText="Are you sure you want to submit survey?"></asp:ConfirmButtonExtender>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlSuccessfull" runat="server" Visible="false">
                                                <div class="success">
                                                    <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label>
                                                    <a href="Default.aspx">Home</a>
                                                </div>
                                            </asp:Panel>
                                            <div style="text-align: center; padding-top:10px;">
                                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <!--About Department - End -->
                        </td>
                    </tr>
                </table>
            </div>
            <!--Content - End-->
            <!--Footer - Start-->
            <uc1:WCFooter runat="server" ID="WCFooter" />
            <!--Footer - End-->
        </div>
    </form>
</body>
</html>
