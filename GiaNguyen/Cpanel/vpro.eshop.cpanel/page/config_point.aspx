<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="config_point.aspx.cs" Inherits="vpro.eshop.cpanel.page.config_point" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Cấu hình tích lũy điểm | Vpro.Eshop</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="icon_function_parent">
<%--        <div class="icon_function_Child">
            <asp:LinkButton ID="lbtHelp" runat="server">
                <img src="../Images/ICON_Help.jpg" width="30" height="30" style="border: 0px" /><div>
                    Trợ giúp</div>
            </asp:LinkButton>
        </div>--%>
        <div class="icon_function_Child">
            <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click"><img src="../Images/ICON_SAVE.png" width="30" height="30" style="border: 0px" /><div>
                    Save</div></asp:LinkButton>
        </div>
        <div class="icon_function_Child">
            <a href="#" onclick="javascript:document.location.reload(true);">
                <img src="../Images/ICON_UPDATE.jpg" width="30" height="30" style="border: 0px" /><div>
                    Refesh</div>
            </a>
        </div>

    </div>
    <!--icon_function_parent-->
    <div id="field">
        <table width="auto" border="0">
            <tr>
                <td height="5" colspan="3" align="left">
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    <span class="user">*</span>Tỉ lệ tiền sang điểm
                </th>
                <td>
                    <input type="text" name="txtPointToMoney" id="txtPointToMoney" runat="server" 
                        style=" width:500px;" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập tỉ lệ tiền sang điểm"
                        Text="Vui lòng nhập tỉ lệ tiền sang điểm" ControlToValidate="txtPointToMoney" CssClass="errormes"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    <span class="user">*</span>Tỉ lệ điểm sang tiền
                </th>
                <td>
                    <input type="text" name="txtMoneyToPoint" id="txtMoneyToPoint" runat="server"  
                        style=" width:500px;" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Vui lòng nhập tỉ lệ tiền sang điểm"
                        Text="Vui lòng nhập tỉ lệ tiền sang điểm" ControlToValidate="txtMoneyToPoint" CssClass="errormes"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
