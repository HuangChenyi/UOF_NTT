﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="WKF_FormManagement_VersionFieldUserControl_LimitUserSetUC" CodeBehind="LimitUserSetUC.ascx.cs" %>
<%@ Reference Control="~/WKF/FormManagement/VersionFieldUserControl/VersionFieldUC.ascx" %>
<%@ Register Src="../../../Common/ChoiceCenter/UC_ChoiceList.ascx" TagName="UC_ChoiceList" TagPrefix="uc2" %>
<%@ Register Src="~/Common/ChoiceCenter/UC_ChoiceListMobile.ascx" TagPrefix="uc3" TagName="UC_ChoiceListMobile" %>
<%-- 為了避免designer錯誤，所以要與Mobile元件同步 --%>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate></ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    function cvExcludeApplicant_<%=UC_ChoiceList1.ClientID%>(e, arg) {
        var choicelist = $('#<%=UC_ChoiceList1.ClientID%>' + '_hiddenJSON').val();
        var applicantGuid = $('#<%=hfApplicantGuid.ClientID%>').val();
        var realValue = $("#<%=hfRealValue.ClientID%>").val();
        var data = [choicelist, '', realValue, applicantGuid];
          var result = $uof.pageMethod.syncUc('WKF/FormManagement/VersionFieldUserControl/LimitUserSetUC.ascx', 'IsIncludeApplicant', data, function (response) { });
          if (result == "True") {
              arg.IsValid = false;
              $('#<%=hyperlinkFocus1.ClientID%>').focus();
          }
          else {
              arg.IsValid = true;
          }
    }
</script>

<table cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td>
            <table>
                 <tr>
                    <td>
                        <asp:LinkButton ID="lnk_Edit" runat="server" OnClick="lnk_Edit_Click"
                            Visible="False" CausesValidation="False" meta:resourcekey="lnk_EditResource1">[修改]
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnk_Cannel" runat="server" OnClick="lnk_Cannel_Click"
                            Visible="False" CausesValidation="False"
                            meta:resourcekey="lnk_CannelResource1">[取消]
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnk_Submit" runat="server" OnClick="lnk_Submit_Click"
                            Visible="False" CausesValidation="False"
                            meta:resourcekey="lnk_SubmitResource1">[確定]
                        </asp:LinkButton>
                        <asp:Image ID="imgAllUser" runat="server" meta:resourcekey="imgAllUserResource1" />
                    </td>
                    <td>
                        <span id="SpanValue" runat="server">
                            <asp:Label ID="lblAllUserValue" runat="server" meta:resourcekey="lblAllUserValueResource1"></asp:Label>
                        </span>
                    </td>
                </tr>
            </table>
        </td>
        <td>
            <asp:Label ID="lblMsgSigner" runat="server" Text="填寫者" meta:resourcekey="lblMsgSignerResource1" Visible="False" ></asp:Label>
            <asp:Label ID="lblMsgEditor" runat="server" Text="修改者" Visible="False" meta:resourcekey="lblMsgEditorResource1"></asp:Label>
            <asp:Label ID="lblHasNoAuthority" runat="server" Text="無填寫權限" ForeColor="Red" meta:resourcekey="lblHasNoAuthorityResource1" Visible="False"></asp:Label>
            <asp:Label ID="lblAllPeople" runat="server" Text="所有人員" meta:resourcekey="lblAllPeopleResource1" Visible="False"></asp:Label>
            <asp:Label ID="lblAllDep" runat="server" Text="所有部門" meta:resourcekey="lblAllDepResource1" Visible="False"></asp:Label>
            <asp:Label ID="lblAllFunc" runat="server" Text="所有職務" meta:resourcekey="lblAllFuncResource1" Visible="False"></asp:Label>
            <asp:Label ID="lblAllRank" runat="server" Text="所有職級" meta:resourcekey="lblAllRankResource1" Visible="False"></asp:Label>
            <asp:Label ID="lblAuthorityMsg" runat="server" Text="具填寫權限人員" meta:resourcekey="lblAuthorityMsgResource1" Visible="False"></asp:Label>
        </td>

    </tr>
    <tr>
        <td id="tdInvisible" runat="server">
            <asp:Label ID="lblInvisible" runat="server" Text="******"></asp:Label>
        </td>
        <td runat="server">
            <div style="display: none">
                <uc3:UC_ChoiceListMobile runat="server" ID="UC_ChoiceListMobile" />
            </div>
            <uc2:UC_ChoiceList ID="UC_ChoiceList1" runat="server" ShowMember="False" />
            <asp:Label ID="lblFiller" CssClass="FillerVisible" runat="server" meta:resourcekey="lblFillerResource1"></asp:Label>
            <asp:Label ID="lblModifier" runat="server" Visible="False" meta:resourcekey="lblModifierResource1"></asp:Label>
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="不允許空白" meta:resourcekey="CustomValidator1Resource1" 
                Visible="False" Display="Dynamic">
            </asp:CustomValidator>
            <asp:CustomValidator ID="cvCheckExcludeAppliant" runat="server" ErrorMessage="不允許選擇申請者" SetFocusOnError="true"
                Display="Dynamic" meta:resourcekey="cvCheckExcludeAppliantResource1">
            </asp:CustomValidator>
            <asp:HyperLink id="hyperlinkFocus1" NavigateUrl="#" runat="server"/>
        </td>
    </tr>
</table>
<span style="display: none">
    <asp:Label ID="lblRealValue" runat="server" meta:resourcekey="lblRealValueResource1"></asp:Label></span>
<asp:HiddenField ID="hfIsVisible" runat="server" />
<asp:HiddenField ID="hfApplicantGuid" runat="server" />
<asp:HiddenField ID="hfRealValue" runat="server" />
