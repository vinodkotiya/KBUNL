<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="change-mail.aspx.cs" Inherits="Admin_news_post" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style>
table {
    border-spacing:5px;
    border-collapse:separate !important;
}
</style>
<script type="text/javascript">
    var specialKeys = new Array();
    specialKeys.push(8); //Backspace

    specialKeys.push(9); //Tab
    specialKeys.push(46); //Delete
    specialKeys.push(36); //Home
    specialKeys.push(35); //End
    specialKeys.push(37); //Left
    specialKeys.push(39); //Right
    specialKeys.push(191);

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
        var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
        return ret;
    }    
    </script>
 <div class="admin_breadcrumb">
        Admin > Email 
    </div>
 <div class="admin_rhs_content">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <asp:HiddenField ID="HDN_PhotoPath" runat="server" />
    <table width="40%" cellpadding="5" cellspacing="5px;">
        <tr>
            <td  width="150" style=" height:36px;">
                Email
            </td>
            <td>
                 <asp:TextBox ID="txtEmail" class="form-control"  runat="server" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td style="height: 36px">
            </td>
            <td style="height: 36px">
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Update"  Width="87px" class="btn btn-primary" />
            </td>
        </tr>
        <tr>
            <td style="height: 36px">
            </td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>

  
    <asp:HiddenField ID="HDN_EventID" runat="server" />
    

   </ContentTemplate>
   <Triggers>
              <asp:PostBackTrigger ControlID="btnSubmit" />
            
        </Triggers>
    </asp:UpdatePanel>
 </div>
</asp:Content>

