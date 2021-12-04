<%@ Page Language="C#" AutoEventWireup="true" CodeFile="print-survey-response.aspx.cs" Inherits="Admin_print_survey_response" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>   
    <link href="../css/MainStyleSheet.css" rel="stylesheet" type="text/css">
    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("div_print").innerHTML;
            var printWindow = window.open('', '', 'height=200,width=400');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }
    </script>

    <style type="text/css">
        .table {width:100%;border-collapse: collapse;font-family: Arial;
            font-size:14px;
            text-align:center;
        }

            .table td {
                padding: 5px;
                vertical-align: top;
                text-align:left;
            }

                .table td h2 {
                    text-align: center;
                    padding-top: 0px;
                    padding-bottom: 0px !important;
                }

        .table .innercontent{font-family:Arial;font-size:14px;}
        .table .innercontent td{border:none !important;vertical-align:top;}
        .table .innercontent td span{padding:5px;display:inline-block;}
        .table .innercontent td label{padding-left:5px;display:inline;}
        .table .innercontent .childcontent td{width:10%;}
        .table .innercontent .childcontent label{padding-left:20px;display:inline;}
        .table .innercontent .childcontent span{padding-left:20px;display:block;}
        

        .lnkbtnback {
            background-color: #333333;
            color: #ffffff;
            cursor: pointer;
            padding: 3px 15px;
            margin-top: 20px;
            text-align: center;
        }

        .printdiv button {
            background-color: #333333;
            color: #ffffff;
            cursor: pointer;
            padding: 3px 15px;
        }

        .printdiv {
            padding-top: 20px;
            text-align: center;
        }

        @media print{
            .printdiv{display:none;}
        }   
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="div_print">        
            <asp:HiddenField ID="hdfDeptId" runat="server" Value="0" />
            <asp:HiddenField ID="hdfSurveyId" runat="server" Value="0" />
            <asp:HiddenField ID="hdfEmpCode" runat="server" Value="" />
            <table clllspacing="0" cellpadding="0" style="width:100%;">
                <tr>
                    <td>
                        <table clllspacing="0" cellpadding="0" class="table">
                            <tr>
                                <td style="width:120px;">
                                    <img src="../images/logo2.png" alt="Logo" />
                                </td>
                                <td style="vertical-align:middle; text-align:center;">
                                    <h4 style="font-size:18px; font-weight:bold; line-height:26px;"><asp:Label ID="lblSurveyTitle" runat="server" Text=""></asp:Label></h4>
                                </td>
                                <td style="width:120px;">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table clllspacing="0" cellpadding="0" style="width:100%;">
                            <tr>
                                <td style="text-align:center; padding:5px;"><b>Employee Code:</b>&nbsp;&nbsp;<asp:Label ID="lblEmpCode" runat="server" Text=""></asp:Label></td>
                                <td style="text-align:center; padding:5px;"><b>Employee Name:</b>&nbsp;&nbsp;<asp:Label ID="lblEmpName" runat="server" Text=""></asp:Label></td>
                                <td style="text-align:center; padding:5px;"><b>Mobile No:</b>&nbsp;&nbsp;<asp:Label ID="lblMobileNo" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>
                    </td>                    
                </tr>
            </table>
             <table>
                <tr>
                    <td colspan="3">
                        <h2>Survey Information</h2>
                        <asp:DataList ID="dtlstSurveyQuestion" runat="server" RepeatDirection="Vertical" OnItemDataBound="dtlstSurveyQuestion_ItemDataBound">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdfSurveyQuestionId" runat="server" Value='<%#Eval("SurveyQuestionId")%>' />
                                <asp:HiddenField ID="hdfSurveyQuestionOption" runat="server" Value='<%#Eval("SurveyQuestionOption")%>' />
                                <table cellpadding="0" cellspacing="0" class="surveytable">
                                    <tr>
                                        <td>
                                            <span class="srno"><%#Container.ItemIndex+1%>.</span>
                                        </td>
                                        <td>
                                            <div class="question"><%#Eval("SurveyQuestion")%></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <div class="response">
                                                <table>
                                                    <tr>
                                                        <td style="width:50px;">
                                                            Ans. 
                                                        </td>
                                                        <td>                
                                                            <asp:DataList ID="dtlstSurveyQuestionSingleChoice" CssClass="childcontent" Style="border: none;" runat="server" RepeatDirection="Horizontal">
                                                                <ItemTemplate>
                                                                    <%#Eval("OptionValue")%><asp:Label ID="lblcomma" runat="server" Visible='<%#Eval("LastRecord").ToString()!= "LastRecord"%>'>,&nbsp;</asp:Label>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                            <asp:DataList ID="dtlstSurveyQuestionMultiChoice" CssClass="childcontent" Style="border: none;" runat="server" RepeatDirection="Horizontal">
                                                                <ItemTemplate>
                                                                    <%#Eval("OptionValue")%><asp:Label ID="lblcomma" runat="server" Visible='<%#Eval("LastRecord").ToString()!= "LastRecord"%>'>,&nbsp;</asp:Label>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                            <asp:Label ID="lblSurveyResponseText" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                                
                                            </div>
                                        </td>
                                    </tr>
                                </table>                              
                            </ItemTemplate>
                        </asp:DataList></td>
                </tr>
            </table>
           
        </div>
         <div class='printdiv'>
                <input type="button" onclick="javascript:window.print()" class='button-link' value="Print" />
            </div>
    </form>
</body>
</html>
