<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="config_meta.aspx.cs" Inherits="vpro.eshop.cpanel.page.config_meta" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Karpach.WebControls" Namespace="Karpach.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Config Meta | Vpro.EsHOP</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<style type="text/css">
    .inputcolor input
    {
        width:40px;
        font-size: 9px;
        }

</style>
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
            <a href="config_meta.aspx">
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
                    <span class="user">*</span>Title
                </th>
                <td>
                    <input type="text" name="txtSeoTitle" id="txtSeoTitle" runat="server" style="width: 500px;" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter the Seo Title"
                        Text="Please enter the Seo Title" ControlToValidate="txtSeoTitle" CssClass="errormes"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    <span class="user">*</span>Title(English)
                </th>
                <td>
                    <input type="text" name="txtSeoTitleEn" id="txtSeoTitleEn" runat="server" style="width: 500px;" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter the Seo Title"
                        Text="Please enter the Seo Title" ControlToValidate="txtSeoTitleEn" CssClass="errormes"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    <span class="user">*</span>Meta Description
                </th>
                <td>
                    <textarea id="txtSeoDesc" runat="server" style="width: 500px;"></textarea>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter the Meta Description"
                        Text="Please enter the Meta Description" ControlToValidate="txtSeoDesc" CssClass="errormes"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    <span class="user">*</span>Meta Description(English)
                </th>
                <td>
                    <textarea id="txtSeoDescEn" runat="server" style="width: 500px;"></textarea>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter the Meta Description"
                        Text="Please enter the Meta Description" ControlToValidate="txtSeoDescEn"
                        CssClass="errormes"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    <span class="user">*</span>Meta Keyword
                </th>
                <td>
                    <textarea id="txtSeoKeyword" runat="server" style="width: 500px;"></textarea>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter the Meta Keyword"
                        Text="Please enter the Meta Keyword" ControlToValidate="txtSeoKeyword" CssClass="errormes"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    <span class="user">*</span>Meta Keyword(English)
                </th>
                <td>
                    <textarea id="txtSeoKeywordEn" runat="server" style="width: 500px;"></textarea>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please enter the Meta Keyword"
                        Text="Please enter the Meta Keyword" ControlToValidate="txtSeoKeywordEn"
                        CssClass="errormes"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="trUpload" runat="server">
                <th valign="top" class="left" nowrap>
                    Favicon
                </th>
                <td>
                    <input id="fileImage1" type="file" name="fileImage1" size="50" runat="server" style="width: 500px;">
                </td>
            </tr>
            <tr id="trFile" runat="server">
                <th valign="top" class="left">
                    <asp:ImageButton ID="btnDelete1" runat="server" ImageUrl="../images/delete_icon.gif"
                        BorderWidth="0" Width="13px" OnClick="btnDelete1_Click" ToolTip="Do you want delete this file?">
                    </asp:ImageButton>
                </th>
                <td>
                    <asp:HyperLink runat="server" ID="hplFile" Target="_blank"></asp:HyperLink><br />
                    <asp:Literal EnableViewState="false" runat="server" ID="ltrImage"></asp:Literal>
                </td>
            </tr>
            <tr id="trUploadBG" runat="server">
                <th valign="top" class="left" nowrap>
                    Background
                </th>
                <td>
                    <input id="fileImageBG" type="file" name="fileImageBG" size="50" runat="server" style="width: 500px;">
                </td>
            </tr>
            <tr id="trFileBG" runat="server">
                <th valign="top" class="left">
                    <asp:ImageButton ID="btnDeleteBG" runat="server" ImageUrl="../images/delete_icon.gif"
                        BorderWidth="0" Width="13px" ToolTip="Do you want delete this file?" OnClick="btnDeleteBG_Click">
                    </asp:ImageButton>
                </th>
                <td>
                    <asp:HyperLink runat="server" ID="hplFileBG" Target="_blank"></asp:HyperLink><br />
                    <asp:Literal EnableViewState="false" runat="server" ID="ltrImageBG"></asp:Literal>
                </td>
            </tr>
            <tr id="tr1" runat="server">
                <th valign="top" class="left" nowrap>
                    Color hover
                </th>
                <td class="inputcolor">
                    <cc1:ColorPicker ID="ColorPicker1" runat="server"/>
                </td>
            </tr>
           <%-- <tr id="tr2" runat="server">
                <th valign="top" class="left" nowrap>
                    Chọn màu hover
                </th>
                <td style="width: 50px">
                    <asp:ColorPickerExtender ID="ColorPickerExtender1" runat="server">
                    </asp:ColorPickerExtender>
                </td>
            </tr>--%>
        </table>
    </div>
</asp:Content>
