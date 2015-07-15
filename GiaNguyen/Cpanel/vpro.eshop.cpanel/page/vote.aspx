<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="vote.aspx.cs" Inherits="vpro.eshop.cpanel.page.vote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>VOTE | Vpro.Eshop</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="icon_function_parent">
        <div class="icon_function_Child">
            <asp:LinkButton ID="lbtSaveNew" runat="server" OnClick="lbtSaveNew_Click">
                <img src="../Images/ICON_DELETE.png" width="30" height="30" style="border: 0px" /><div>
                    Save & Add Another</div>
            </asp:LinkButton>
        </div>
        <div class="icon_function_Child">
            <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click"><img src="../Images/ICON_SAVE.png" width="30" height="30" style="border: 0px" /><div>
                    Save</div></asp:LinkButton>
        </div>
        <div class="icon_function_Child">
            <a href="vote-list.aspx">
                <img src="../Images/ICON_RETURN.png" width="30" height="30" style="border: 0px" />
                <div>
                    Back</div>
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
                    Root
                </th>
                <td>
                    <asp:DropDownList ID="ddlCategory" runat="server" Width="500px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    <span class="user">*</span>Câu hỏi
                </th>
                <td>
                    <input type="text" name="txtques" id="txtques" runat="server" style="width: 500px;" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập câu hỏi"
                        Text="Vui lòng nhập câu hỏi" ControlToValidate="txtques" CssClass="errormes"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="height: 20px;">
                <th valign="top" class="left">
                    Trạng thái
                </th>
                <td height="25">
                    <asp:RadioButtonList ID="Dractive" runat="server" RepeatColumns="5">
                        <asp:ListItem Text="Kích hoạt" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Không kích hoạt" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
