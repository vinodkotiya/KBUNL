<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="form-module.aspx.cs" Inherits="Admin_news_post" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .fixelement {
            position: fixed;
            background-color: #17a388;
            color: #ffffff;
            left: 43%;
            top: 0;
            padding: 5px;
            text-align: center;
            width: 600px;
            border-bottom-right-radius: 10px;
            border-bottom-left-radius: 10px;
            margin-left: -150px;
            z-index: 99999;
        }

            .fixelement.info {
                background-color: #17a388;
            }

            .fixelement.error {
                background-color: #ff0000;
            }
    </style>
    <script type="text/javascript">
        function Hidedivmsg() {
            var seconds = 8;
            setTimeout(function () {
                document.getElementById("<%=divmsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
    <div class="admin_breadcrumb" id="id_adminbreadcrumb" runat="server">
    </div>

    <div class="panel panel-default">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <script type="text/javascript" language="javascript">
                    Sys.Application.add_load(Hidedivmsg);
                </script>
                <div class="fixelement" id="divmsg" runat="server" visible="false">
                    <asp:LinkButton ID="lnkbtnMsg" runat="server" CssClass="closebtn" OnClick="lnkbtnMsg_Click">
                  <img src="images/cancel-circle .png" alt="" style="float:right;" /></asp:LinkButton>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="panel-body panel-body-table">
                    <asp:HiddenField ID="hdfEmpCode" runat="server" />
                    <asp:HiddenField ID="hdfEmId" runat="server" Value="0" />

                    <asp:GridView ID="grdModule" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <span><%#Container.DataItemIndex+1 %></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Module" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <span><%#Eval("ModuleName") %></span>
                                    <asp:HiddenField Value='<%#Eval("ModuleId") %>' runat="server" ID="hdfModuleIdGrd" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ModuleGroup" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <span><%#Eval("GroupName") %></span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Assign" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkModule" Checked='<%#Convert.ToBoolean(Eval("ModulePermission"))%>' OnCheckedChanged="chkModule_CheckedChanged" AutoPostBack="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>



                </div>
                <asp:Button ID="btnUpdate" runat="server" Visible="false" CssClass="btn btn-info pull-right" OnClick="btnUpdate_Click" Text="Assign" />
            </ContentTemplate>

        </asp:UpdatePanel>
    </div>


</asp:Content>

