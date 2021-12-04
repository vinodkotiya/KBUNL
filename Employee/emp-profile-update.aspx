<%@ Page Title="" Language="C#" MasterPageFile="~/Employee/Employee.master" AutoEventWireup="true" CodeFile="emp-profile-update.aspx.cs" Inherits="Employee_emp_profile_update" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .row {
            margin-right: 0px;
            margin-left: 0px;
        }

        label {
            font-family: sourcesanspro-regular;
            color: #8c9099;
            font-size: 14px;
            font-weight: normal;
            margin-right: 10px;
        }

        .table {
            width: 100%;
        }

            .table th {
                font-size: 13px;
            }

            .table td {
                font-size: 13px;
            }

            .row .exptbllbl {padding-top:2px; font-weight:bold; color:#333333;}
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
        Employee Profile
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
                                    <h3 class="panel-title">Employee Profile Update</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-horizontal">
                                                <asp:HiddenField ID="hdfEmployeeId" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfImagePath" runat="server" Value="NA" />
                                                 <div id="alert" runat="server">
                                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                                </div>
                                                <br />

                                                <!--Employee Code and Employee Name-->
                                                <div class="row">
                                                    <div class="col-1">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label9" runat="server" Text="Emp Code"></asp:Label>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:Label ID="lblEmpCode"  runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-2">
                                                        <div class="exptbllbl" >
                                                            <asp:Label ID="Label3" runat="server" Text="Emp Name"></asp:Label>
                                                        </div>
                                                        <div class="exptbl" style="position:relative;">
                                                            <asp:Label ID="lblEmpName" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both;"></div>
                                                </div>
                                                
                                                <!--Department and Cadre-->
                                                <div class="row">
                                                    <div class="col-1">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label2" runat="server" Text="Department"></asp:Label>
                                                        </div>
                                                        <div class="exptbl">
                                                            <asp:Label ID="lblEmpDept"  runat="server"></asp:Label>
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
                                                            <asp:Label ID="Label4" runat="server" Text="Level"></asp:Label>
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
                                                            <asp:Label ID="Label11" runat="server" Text="Designation"></asp:Label>
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
                                                            <asp:Label ID="Label5" runat="server" Text="Date of Birth"></asp:Label>
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
                                                            <asp:Label ID="Label7" runat="server" Text="Date of Retiremnent"></asp:Label>
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
                                                            <asp:Label ID="Label10" runat="server" Text="Date of Joining at Project"></asp:Label>
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

                                                <!--DOP & Email-->
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
                                                    <div class="col-2">
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
                                                            <asp:Label ID="Label1" runat="server" Text="Blood Group"></asp:Label>
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

                                                <div class="row">
                                                    <div class="col-1">
                                                        <div class="exptbllbl">
                                                            <asp:Label ID="Label22" runat="server" Text="Photo"></asp:Label>
                                                        </div>
                                                        <div class="exptbl">
                                                             <asp:FileUpload ID="FileUploadImage" runat="server" class="btn-primary" name="filename" title="Browse file" Style="width: 100%;" />
                                                            
                                                            <div><small>jpg, gif and png extensions supported. Size(in pixel): 200 x 200</small></div>
                                                            <div style="text-align: right;">
                                                                <asp:Image ID="imgEmpPhoto" runat="server" ImageUrl="../images/profilepic.png" Width="100px" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-2">
                                                        

                                                    </div>
                                                    <div style="clear: both;"></div>
                                                </div>
                                                <!--Error Message-->
                                               
                                                <div class="panel-footer" style="text-align:center;">
                                                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" ValidationGroup="b" Text="Submit" OnClick="btnSave_Click" />
                                                <%--</div>--%>
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

