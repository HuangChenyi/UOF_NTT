<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OptionFieldUC2.ascx.cs" Inherits="WKF_OptionalFields_OptionFieldUC2" %>
<%@ Reference Control="~/WKF/FormManagement/VersionFieldUserControl/VersionFieldUC.ascx" %>
<%@ Register Src="~/Common/ChoiceCenter/UC_ChoiceList.ascx" TagPrefix="uc1" TagName="UC_ChoiceList" %>


<script>

    function CheckedData(source, arguments) {



        var item = $('#<%=txtItem.ClientID%>').val();
        
        var amount = $find("<%=rnumAmount.ClientID%>").get_value();

        var data = [item,amount];
        var result = $uof.pageMethod.syncUc("CDS/WKF_Fields/OptionFieldUC2.ascx", "CheckedData", data);
    
        if (result !="") {
            arguments.IsValid = false;
            $('#<%=CustomValidator1.ClientID%>').text(result);
            return;
        }
        else {
                        arguments.IsValid = true;
            return;
        }
    

    }

</script>

<table class="PopTable" style="width:600px">
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="類別"></asp:Label>
        </td>
        <td>
            <asp:RadioButtonList ID="rbListType" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem>A</asp:ListItem>
                 <asp:ListItem>B</asp:ListItem>
                 <asp:ListItem>C</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td>

            <asp:Label ID="Label2" runat="server" Text="品項"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtItem" runat="server"></asp:TextBox>
        </td>
            </tr>
    <tr>
        <td>
             <asp:Label ID="Label3" runat="server" Text="金額"></asp:Label>
        </td>
        <td>
            <telerik:RadNumericTextBox ID="rnumAmount" runat="server" Value="0"
                MaxValue="9999"  MinValue="0"
                ></telerik:RadNumericTextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label4" runat="server" Text="負責人"></asp:Label>
        </td>
        <td>
            <uc1:uc_choicelist runat="server" id="UC_ChoiceList" />
        </td>
    </tr>
</table>

<asp:CustomValidator ID="CustomValidator1" runat="server" 
    Display="Dynamic" ClientValidationFunction="CheckedData"
    ErrorMessage="CustomValidator"></asp:CustomValidator>



<asp:Label ID="lblHasNoAuthority" runat="server" Text="無填寫權限" ForeColor="Red" Visible="False" meta:resourcekey="lblHasNoAuthorityResource1"></asp:Label>
<asp:Label ID="lblToolTipMsg" runat="server" Text="不允許修改(唯讀)" Visible="False" meta:resourcekey="lblToolTipMsgResource1"></asp:Label>
<asp:Label ID="lblModifier" runat="server" Visible="False" meta:resourcekey="lblModifierResource1"></asp:Label>
<asp:Label ID="lblMsgSigner" runat="server" Text="填寫者" Visible="False" meta:resourcekey="lblMsgSignerResource1"></asp:Label>
<asp:Label ID="lblAuthorityMsg" runat="server" Text="具填寫權限人員" Visible="False" meta:resourcekey="lblAuthorityMsgResource1"></asp:Label>