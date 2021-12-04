<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="employees.aspx.cs" Inherits="Admin_news_post" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .row { margin-right: 0px; margin-left: 0px; }
        label { font-family: sourcesanspro-regular; color: #8c9099; font-size: 14px; font-weight: normal; margin-right: 10px; }
        .table { width: 100%; }
        .table th { font-size: 13px; }
        .table td { font-size: 13px; }
        .row .exptbllbl, label { color: #000000; }
        .form-control { color: #000000; }
    </style>
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace

        function IsText(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 126) || keyCode == 32 || keyCode == 9 || keyCode == 8 || keyCode == 46 || specialKeys.indexOf(keyCode) != -1);
            return ret;
        }

        function IsPhone(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || keyCode == 9 || keyCode == 8 || keyCode == 45 || specialKeys.indexOf(keyCode) != -1);
            return ret;
        }
        function isAlfa(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 32 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122)) {
                return false;
            }
            return true;
        }
        function IsAlphaNumeric(e) {
            var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
            var ret = ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || keyCode == 32 || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
            return ret;
        }
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            return ret;
        }
        function IsDate(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || keyCode == 47 || keyCode == 9 || keyCode == 8 || keyCode == 46 || specialKeys.indexOf(keyCode) != -1);
            return ret;
        }

        $(function () {
            $('#txtFirstName').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey || e.altKey) {
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                        e.preventDefault();
                    }
                }
            });
        });
    </script>

    <div class="admin_breadcrumb">
        Admin > Employee
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
                                    <h3 class="panel-title"><strong>Add/Update</strong> Employee</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="HDN_EID" runat="server" />
                                                <asp:HiddenField ID="HDN_ImagePath" runat="server" />
                                                <div style="text-align: right;">
                                                    <img id="EmpPhoto" visible="false" runat="server" width="50" src="~/Uploads/EmpPhoto/Blank.png" alt="" />
                                                </div>

                                                <!--EmpCode-->
                                                <div class="row">
                                                    <div class="col-1">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label9" runat="server" Text="Emp Code"></asp:Label>
                                                            <span style="color: #ff0000">*</span>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:TextBox ID="txtEmpCode" class="form-control" MaxLength="6" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;" runat="server" autocomplete="off"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtEmpCode" SetFocusOnError="true"
                                                                runat="server" ErrorMessage="Please enter Emp code" ControlToValidate="txtEmpCode"
                                                                ValidationGroup="b" CssClass="validator"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both;"></div>
                                                </div>

                                                <!--EmpName-->
                                                <div class="row">
                                                    <div class="col-1">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label3" runat="server" Text="Emp Name"></asp:Label>
                                                            <span style="color: #ff0000">*</span>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:TextBox ID="txtEmpName" class="form-control" onkeypress="return isAlfa(event)" runat="server" MaxLength="50" autocomplete="off"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtEmpName" SetFocusOnError="true"
                                                                runat="server" ErrorMessage="Please enter Emp name" ControlToValidate="txtEmpName"
                                                                ValidationGroup="b" CssClass="validator"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-2">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label17" runat="server" Text="Emp Name (Hindi)"></asp:Label>
                                                            <span style="color: #ff0000">*</span>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:TextBox ID="txtEmpNameH" class="form-control" runat="server" MaxLength="50" autocomplete="off"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFVNameH" SetFocusOnError="true"
                                                                runat="server" ErrorMessage="Please enter Emp name in hindi" ControlToValidate="txtEmpNameH"
                                                                ValidationGroup="b" CssClass="validator"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both;"></div>
                                                </div>

                                                <!--Department and cadre-->
                                                <div class="row">
                                                    <div class="col-1">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label2" runat="server" Text="Department"></asp:Label>
                                                            <span style="color: #ff0000">*</span>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:DropDownList ID="ddlDept" class="form-control" runat="server" AppendDataBoundItems="True">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlDept" SetFocusOnError="true" InitialValue="0"
                                                                runat="server" ErrorMessage="Please select dept" ControlToValidate="ddlDept"
                                                                ValidationGroup="b" CssClass="validator"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="col-2">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="lblCadre" runat="server" Text="Cadre"></asp:Label>
                                                            <span style="color: #ff0000">*</span>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:DropDownList ID="ddlCadre" class="form-control" runat="server">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="Executive" Text="Executive"></asp:ListItem>
                                                                <asp:ListItem Value="Non Executive" Text="Non Executive"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlCadre" runat="server" ErrorMessage="Please select cadre" SetFocusOnError="true" ControlToValidate="ddlCadre" CssClass="validator" InitialValue="0" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both;"></div>
                                                </div>

                                                <!--Level and Designation-->
                                                <div class="row">
                                                    <div class="col-1">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label6" runat="server" Text="Level"></asp:Label>
                                                            <span style="color: #ff0000">*</span>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:DropDownList ID="ddlLevel" class="form-control" runat="server" AppendDataBoundItems="True">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorLevel" SetFocusOnError="true" InitialValue="0"
                                                                runat="server" ErrorMessage="Please select level" ControlToValidate="ddlLevel"
                                                                ValidationGroup="b" CssClass="validator"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-2">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label4" runat="server" Text="Designation"></asp:Label>
                                                            <span style="color: #ff0000">*</span>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:DropDownList ID="ddlDesignation" class="form-control" runat="server" AppendDataBoundItems="True">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlDesignation" SetFocusOnError="true" InitialValue="0"
                                                                runat="server" ErrorMessage="Please select designation" ControlToValidate="ddlDesignation"
                                                                ValidationGroup="b" CssClass="validator"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both;"></div>
                                                </div>

                                                <!--Seniority Order and EmailID-->
                                                <div class="row">
                                                    <div class="col-1">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label13" runat="server" Text="EmailID"></asp:Label>
                                                            <span style="color: #ff0000">*</span>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:TextBox ID="txtEmailID" class="form-control" runat="server" MaxLength="50" autocomplete="off"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtEmailID" SetFocusOnError="true"
                                                                runat="server" ErrorMessage="Please enter email" ControlToValidate="txtEmailID"
                                                                ValidationGroup="b" CssClass="validator"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatortxtEmailId" SetFocusOnError="true"
                                                                runat="server" ValidationGroup="b" ErrorMessage="Enter valid emailid"
                                                                ControlToValidate="txtEmailID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                CssClass="validator"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both;"></div>
                                                </div>

                                                <!--Mobile No & Alternate Mobile No-->
                                                <div class="row">
                                                    <div class="col-1">
                                                        <div class="exptbllbl" style="margin-top: 8px;">
                                                            <asp:Label ID="Label14" runat="server" Text="Mobile No."></asp:Label>
                                                            <span style="color: #ff0000">*</span>
                                                        </div>
                                                        <div class="exptbl" style="margin-top: 8px;">
                                                            <asp:TextBox ID="txtMobile" class="form-control" runat="server" MaxLength="15" onkeypress="return IsPhone(event)" autocomplete="off"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtMobile" SetFocusOnError="true"
                                                                runat="server" ErrorMessage="Please enter Mobile No." ControlToValidate="txtMobile"
                                                                ValidationGroup="b" CssClass="validator"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-2">
                                                        <div class="exptbllbl" style="margin-top: 8px;">
                                                            <asp:Label ID="Label21" runat="server" Text="Alternate Contact No."></asp:Label>
                                                        </div>
                                                        <div class="exptbl" style="margin-top: 8px;">
                                                            <asp:TextBox ID="txtAlternateMobile" class="form-control" runat="server" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both;"></div>
                                                </div>

                                                <!--Intercom Office & Intercom Residence-->
                                                <div class="row">
                                                    <div class="col-1">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label18" runat="server" Text="Intercom Office"></asp:Label>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:TextBox ID="txtIntercomOfc" class="form-control" runat="server" MaxLength="20" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-2">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label16" runat="server" Text="Intercom Residence"></asp:Label>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:TextBox ID="txtIntercomRes" class="form-control" runat="server" MaxLength="20" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both;"></div>
                                                </div>

                                                <!--DOB & DOR-->
                                                <div class="row">
                                                    <div class="col-1">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label1" runat="server" Text="Date of Birth"></asp:Label>
                                                            <span style="color: #ff0000">*</span>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:TextBox ID="txtDOB" class="form-control" runat="server" MaxLength="10" placeholder="Date of Birth" autocomplete="off" AutoPostBack="true" OnTextChanged="txtDOB_TextChanged"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CEDOB" runat="server" Enabled="true" Format="dd/MM/yyyy" TargetControlID="txtDOB"></asp:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RFVDOB" runat="server" ErrorMessage="Please enter date of birth" SetFocusOnError="true" ControlToValidate="txtDOB" CssClass="validator" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ForeColor="Red" ID="REVDOB" runat="server" SetFocusOnError="true" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$" ControlToValidate="txtDOB" ErrorMessage="Please enter valid date in dd/mm/yyyy format" ValidationGroup="b" CssClass="validator"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-2">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label8" runat="server" Text="Date of Retiremnent"></asp:Label>
                                                            <span style="color: #ff0000">*</span>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:TextBox ID="txtDOR" class="form-control" runat="server" MaxLength="10" placeholder="Date of Retirement" autocomplete="off"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CEDOR" runat="server" Enabled="true" Format="dd/MM/yyyy" TargetControlID="txtDOR"></asp:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RFVDOR" runat="server" ErrorMessage="Please enter date of retirement" SetFocusOnError="true" ControlToValidate="txtDOB" CssClass="validator" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ForeColor="Red" ID="REVDOR" runat="server" SetFocusOnError="true" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$" ControlToValidate="txtDOR" ErrorMessage="Please enter valid date in dd/mm/yyyy format" ValidationGroup="b" CssClass="validator"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both;"></div>
                                                </div>

                                                <!--DOJ-->
                                                <div class="row">
                                                    <div class="col-1">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label12" runat="server" Text="Date of Joining at NTPC"></asp:Label>
                                                            <span style="color: #ff0000">*</span>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:TextBox ID="txtDOJNTPC" class="form-control" runat="server" MaxLength="10" placeholder="Date of Joining at NTPC" autocomplete="off"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CEDOJNTPC" runat="server" Enabled="true" Format="dd/MM/yyyy" TargetControlID="txtDOJNTPC"></asp:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RFVDOJNTPC" runat="server" ErrorMessage="Please enter date of joining at NTPC" SetFocusOnError="true" ControlToValidate="txtDOJNTPC" CssClass="validator" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ForeColor="Red" ID="REVDOJNTPC" runat="server" SetFocusOnError="true" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$" ControlToValidate="txtDOJNTPC" ErrorMessage="Please enter valid date in dd/mm/yyyy format" ValidationGroup="b" CssClass="validator"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-2">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label23" runat="server" Text="Date of Joining at Project"></asp:Label>
                                                            <span style="color: #ff0000">*</span>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:TextBox ID="txtDOJP" class="form-control" runat="server" MaxLength="10" placeholder="Date of Joining at Project" autocomplete="off"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CEDOJP" runat="server" Enabled="true" Format="dd/MM/yyyy" TargetControlID="txtDOJP"></asp:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RFVDOJP" runat="server" ErrorMessage="Please enter date of joining at project" SetFocusOnError="true" ControlToValidate="txtDOJP" CssClass="validator" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ForeColor="Red" ID="REVDOJP" runat="server" SetFocusOnError="true" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$" ControlToValidate="txtDOJP" ErrorMessage="Please enter valid date in dd/mm/yyyy format" ValidationGroup="b" CssClass="validator"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both;"></div>
                                                </div>

                                                <!--DOP-->
                                                <div class="row">
                                                    <div class="col-1">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label24" runat="server" Text="Date of Promotion(Last)"></asp:Label>
                                                            <span style="color: #ff0000">*</span>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:TextBox ID="txtDOPLast" class="form-control" runat="server" MaxLength="10" placeholder="Last Promotion Date" autocomplete="off"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CEDOPLast" runat="server" Enabled="true" Format="dd/MM/yyyy" TargetControlID="txtDOPLast"></asp:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter last promotion date" SetFocusOnError="true" ControlToValidate="txtDOPLast" CssClass="validator" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ForeColor="Red" ID="REVDOPLast" runat="server" SetFocusOnError="true" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$" ControlToValidate="txtDOPLast" ErrorMessage="Please enter valid date in dd/mm/yyyy format" ValidationGroup="b" CssClass="validator"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both;"></div>
                                                </div>

                                                <!--Gender & Bloodgroup-->
                                                <div class="row">
                                                    <div class="col-1">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label19" runat="server" Text="Gender"></asp:Label>
                                                            <span style="color: #ff0000">*</span>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:RadioButtonList ID="rbtGender" RepeatDirection="Horizontal" runat="server">
                                                                <asp:ListItem Value="Male" Selected="True">Male</asp:ListItem>
                                                                <asp:ListItem Value="Female">Female</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorrbtGender" runat="server" ErrorMessage="Please select gender" SetFocusOnError="true" ControlToValidate="rbtGender" CssClass="validator" InitialValue="0" ValidationGroup="b"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-2">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label10" runat="server" Text="Blood Group"></asp:Label>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:DropDownList ID="ddlBloodGroup" class="form-control" runat="server" AppendDataBoundItems="True">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both;"></div>
                                                </div>

                                                <!--Quarter Type & QuarterNo-->
                                                <div class="row">
                                                    <div class="col-1">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label29" runat="server" Text="Area"></asp:Label>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:DropDownList ID="ddlArea" class="form-control" runat="server">
                                                                <asp:ListItem Value="">NA</asp:ListItem>
                                                                <asp:ListItem Value="NH-1" Text="NH-1"></asp:ListItem>
                                                                <asp:ListItem Value="NH-2" Text="NH-2"></asp:ListItem>
                                                                <asp:ListItem Value="NH-3" Text="NH-3"></asp:ListItem>
                                                                <asp:ListItem Value="NH-4" Text="NH-4"></asp:ListItem>
                                                                <asp:ListItem Value="NH-5" Text="NH-5"></asp:ListItem>
                                                                <asp:ListItem Value="NH-6" Text="NH-6"></asp:ListItem>
                                                                <asp:ListItem Value="TTS" Text="TTS"></asp:ListItem>
                                                                <asp:ListItem Value="BTH" Text="BTH"></asp:ListItem>
                                                                <asp:ListItem Value="LVA" Text="LVA"></asp:ListItem>
                                                                <asp:ListItem Value="HVA" Text="HVA"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-2">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label25" runat="server" Text="Quarter Type"></asp:Label>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:DropDownList ID="ddlQuarterType" class="form-control" runat="server">
                                                                <asp:ListItem Value="">NA</asp:ListItem>
                                                                <asp:ListItem Value="A" Text="A"></asp:ListItem>
                                                                <asp:ListItem Value="B" Text="B"></asp:ListItem>
                                                                <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                                <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                                <asp:ListItem Value="EXP I" Text="EXP I"></asp:ListItem>
                                                                <asp:ListItem Value="EXP II" Text="EXP II"></asp:ListItem>
                                                                <asp:ListItem Value="EXP III" Text="EXP III"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    
                                                    <div style="clear: both;"></div>
                                                </div>

                                                <!--Location-->
                                                <div class="row">
                                                    <div class="col-1">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label26" runat="server" Text="Quarter Number"></asp:Label>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:TextBox ID="txtQuarterNumber" runat="server" placeholder="" class="form-control" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-2">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label20" runat="server" Text="PAN No"></asp:Label>

                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:TextBox ID="txtPanNo" class="form-control" runat="server" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both;"></div>
                                                </div>

                                                <asp:UpdatePanel ID="up2" runat="server">
                                                    <ContentTemplate>
                                                        <!--Release-->
                                                        <div class="row">
                                                            <div class="col-1">
                                                                <div class="exptbllbl">
                                                                    <asp:Label ID="Label22" runat="server" Text="Released from Project"></asp:Label>
                                                                </div>
                                                                <div class="exptbl">
                                                                    <asp:DropDownList ID="ddlReleased" class="form-control" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlReleased_SelectedIndexChanged">
                                                                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-2" id="divreleaseddate" runat="server">
                                                                <div class="exptbllbl">
                                                                    <asp:Label ID="Label27" runat="server" Text="Release Date"></asp:Label>
                                                                </div>
                                                                <div class="exptbl">
                                                                    <asp:TextBox ID="txtReleaseDate" class="form-control" runat="server" MaxLength="10" placeholder="Release Date" autocomplete="off"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CEReleaseDate" runat="server" Enabled="true" Format="dd/MM/yyyy" TargetControlID="txtReleaseDate"></asp:CalendarExtender>
                                                                </div>
                                                            </div>
                                                            <div style="clear: both;"></div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-1" id="divreleasedreason" runat="server">
                                                                <div class="exptbllbl">
                                                                    <asp:Label ID="Label28" runat="server" Text="Release Reason"></asp:Label>
                                                                </div>
                                                                <div class="exptbl">
                                                                    <asp:UpdatePanel ID="up1" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlReleaseReason" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReleaseReason_SelectedIndexChanged">
                                                                                <asp:ListItem Value="">NA</asp:ListItem>
                                                                                <asp:ListItem Value="Transfer" Text="Transfer"></asp:ListItem>
                                                                                <asp:ListItem Value="Superannuation" Text="Superannuation"></asp:ListItem>
                                                                                <asp:ListItem Value="VRS" Text="VRS"></asp:ListItem>
                                                                                <asp:ListItem Value="Other Reason" Text="Other Reason"></asp:ListItem>
                                                                            </asp:DropDownList><br />
                                                                            <asp:TextBox ID="txtReleaseReason" class="form-control" runat="server" MaxLength="50" placeholder="Enter Release Remark" autocomplete="off"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                            <div class="col-2">
                                                                <div class="exptbllbl">
                                                                    <asp:Label ID="Label11" runat="server" Text="Photo"></asp:Label>
                                                                </div>
                                                                <div class="exptbl">
                                                                    <asp:FileUpload ID="FileUploadImage" runat="server" class="btn-primary" name="filename" title="Browse file" Style="width: 100%;" />
                                                                    <small>Only Image (jpg, gif and png extensions supported) :: Size (200 x 200)</small>
                                                                </div>
                                                            </div>

                                                            <div style="clear: both;"></div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="row" style="display: none;">
                                                    <div class="col-1">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label5" runat="server" Text="Is Department Admin"></asp:Label>
                                                        </div>
                                                        <div class="exptbl" style="padding-top: 10px;">
                                                            <asp:CheckBox ID="chkIsDepartmentAdmin" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-2">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label7" runat="server" Text="Is HOD"></asp:Label>

                                                        </div>
                                                        <div class="exptbl" style="padding-top: 10px;">
                                                            <asp:CheckBox ID="chkIsHOD" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div style="clear: both;"></div>
                                                </div>
                                                <!--Error Message-->
                                                <div id="alert" runat="server">
                                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                                </div>
                                                <br />
                                                <div class="panel-footer">
                                                    <asp:Button ID="btnClear" CausesValidation="false" class="btn btn-default" runat="server" Text="Close Form" OnClick="btnClose_Click" />
                                                    <asp:Button ID="btnSave" class="btn btn-primary pull-right" runat="server" ValidationGroup="b" Text="Submit" OnClick="btnSave_Click" />
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
                                    <table width="100%">
                                        <tr>
                                            <td align="left">
                                                <asp:LinkButton ID="lbtn_AddNew" runat="server" OnClick="lbtn_AddNew_Click" class="btn btn-primary pull-left" Style="background-color: rgba(199, 15, 15, 0.9)">Add New Employee</asp:LinkButton></td>
                                            <td align="right">
                                                <table>
                                                    <tr>
                                                        <td style="padding-right: 5px;">
                                                            <asp:Label ID="lblSearch_EmpID" runat="server">EmpID</asp:Label></td>
                                                        <td style="padding-right: 10px;">
                                                            <asp:TextBox ID="txtSearch_EmpID" runat="server" Width="100px"></asp:TextBox></td>
                                                        <td style="padding-right: 5px;">
                                                            <asp:Label ID="lblSearch_EmpName" runat="server">Emp Name</asp:Label></td>
                                                        <td style="padding-right: 10px;">
                                                            <asp:TextBox ID="txtSearch_EmpName" runat="server" Width="150px"></asp:TextBox></td>
                                                        <td>
                                                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" class="btn btn-primary pull-right" Style="background-color: rgba(199, 15, 15, 0.9); float: none!important;" />
                                                        </td>
                                                        <td style="padding-left: 20px;">
                                                            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="../images/icons/excel.png" Width="30" ToolTip="Export to Excel" OnClick="btnExport_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <!--Error Message-->
                                <div id="alertgrid" runat="server">
                                    <asp:Label ID="lblgridmsg" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="panel-body panel-body-table">
                                    <asp:GridView ID="gridEmployee" EmptyDataText="No Data Found" CssClass="table" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="20" OnPageIndexChanging="gridEmployee_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="EmpID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_EID" runat="server" Text='<%#Eval("EID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EmpCode" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_EmpCode" runat="server" Text='<%#Eval("EmpCode")%>'></asp:Label><br />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Image" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Image ID="Image1" Width="40px" ImageUrl='<%#Eval("PhotoPath_Admin")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name" HeaderStyle-Width="200px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_EmpName" runat="server" Text='<%#Eval("EmpName")%>'></asp:Label><br />
                                                    <%#Eval("EmpNameHindi")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department/ Designation" HeaderStyle-Width="150px" ItemStyle-Width="200">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Dept" runat="server" Text='<%#Eval("Department")%>'></asp:Label><br />
                                                    <asp:Label ID="lbl_Desig" runat="server" Text='<%#Eval("Designation")%>' Style="color: #0091da;"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Level" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_LevelID" runat="server" Text='<%#Eval("Level")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email / Mobile / Intercom" HeaderStyle-Width="200px">
                                                <ItemTemplate>
                                                    <%#Eval("EmailID")%><br />
                                                    <asp:Label ID="lbl_Mobile" runat="server" Text='<%#Eval("Mobile")%>'></asp:Label><br />
                                                    <%#Eval("IntercomOffice")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gender" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Gender" runat="server" Text='<%#Eval("Gender")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ReleaseStatus" runat="server" Text='<%#Eval("ReleaseStatus")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IsDeptAdmin" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeptAdmin" runat="server" Text='<%#Eval("IsDeptAdmin")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IsHOD" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIsHOD" runat="server" Text='<%#Eval("IsHOD")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-default btn-rounded btn-sm" CausesValidation="false" CommandArgument='<%# Eval("EID") %>' OnClick="Update_Click" runat="server"><span class="fa fa-pencil"></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkActionDelete" CssClass="btn btn-danger btn-rounded bs-actionsbox" CausesValidation="false" OnClick="Delete_Click" CommandArgument='<%# Eval("EID") %>' runat="server"> <span>Delete</span> </asp:LinkButton>
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtenderDelete" runat="server" TargetControlID="lnkActionDelete" ConfirmText="Are you sure you want to delete this record?"></asp:ConfirmButtonExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reset_PWD<br/>to 123456" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkActionResetPassword" CssClass="btn btn-info btn-rounded bs-actionsbox" CausesValidation="false" OnClick="ResetPassword_Click" CommandArgument='<%# Eval("EID") %>' runat="server" ToolTip="Reset Password"> <span>Reset</span> </asp:LinkButton>
                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtenderResetPassword" runat="server" TargetControlID="lnkActionResetPassword" ConfirmText="Are you sure you want to reset password of this employee?"></asp:ConfirmButtonExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                    </Triggers>

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

