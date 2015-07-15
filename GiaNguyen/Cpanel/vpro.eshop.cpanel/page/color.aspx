<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="color.aspx.cs" Inherits="vpro.eshop.cpanel.page.color" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Category | Vpro.Eshop </title>
    <script src="../Jquery/jquery.min.1.7.2.js" type="text/javascript"></script>
    <script src="../Jquery/JqueryCollapse/jquery.collapse.js" type="text/javascript"></script>
    <script language="javascript">
        new jQueryCollapse($("#seo"), {
            query: 'div h2'
        });

        function ParseText(objsent) {
            ParseUrl(objsent, document.getElementById('MainContent_txtSeoUrl'));
            document.getElementById('MainContent_txtSeoTitle').value = objsent.value;
            document.getElementById('MainContent_txtSeoKeyword').value = objsent.value;
            //document.getElementById('MainContent_txtSeoDescription').value = objsent.value;
        }
        function ParseTextEn(objsent) {
            ParseUrl(objsent, document.getElementById('MainContent_txtSeoUrlEn'));
            document.getElementById('MainContent_txtSeoTitleEn').value = objsent.value;
            document.getElementById('MainContent_txtSeoKeywordEn').value = objsent.value;
            //document.getElementById('MainContent_txtSeoDescription').value = objsent.value;
        }
        function ParseDesc(objsent) {
            document.getElementById('MainContent_txtSeoDescription').value = objsent.value;
        }
        function ParseDescEn(objsent) {
            document.getElementById('MainContent_txtSeoDescriptionEn').value = objsent.value;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="icon_function_parent">
<%--        <div class="icon_function_Child">
            <asp:LinkButton ID="lbtHelp" runat="server">
                <img src="../Images/ICON_Help.jpg" width="30" height="30" style="border: 0px" /><div>
                    Trợ giúp</div>
            </asp:LinkButton>
        </div>--%>
        <div class="icon_function_Child" id="dvDelete" runat="server">
            <asp:LinkButton ID="lbtDelete" runat="server" OnClick="lbtDelete_Click" OnClientClick="return confirm('Do you want delete?');"
                CausesValidation="false">
                <img src="../Images/ICON_DELETE.png" width="30" height="30" style="border: 0px" /><div>
                    Delete</div>
            </asp:LinkButton>
        </div>
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
            <a href="#" onclick="javascript:document.location.reload(true);">
                <img src="../Images/ICON_UPDATE.jpg" width="30" height="30" style="border: 0px" /><div>
                    Refesh</div>
            </a>
        </div>
        <div class="icon_function_Child">
            <a href="color_list.aspx">
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
                <td colspan="3" align="left">
                    <asp:Label CssClass="user" ID="lblError" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    <span class="user">*</span>Tên màu sắc
                </th>
                <td>
                    <input type="text" name="txtName" id="txtName" runat="server" style="width: 500px;"
                        onkeyup="ParseText(this);" onblur="ParseText(this);" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập tên màu sắc"
                        Text="Vui lòng nhập tên màu sắc" ControlToValidate="txtName" CssClass="errormes"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    Description
                </th>
                <td>
                    <textarea id="txtDesc" runat="server" style="width: 500px;" onkeyup="ParseDesc(this);"
                        onblur="ParseDesc(this);"></textarea>
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    Order
                </th>
                <td>
                    <input type="text" name="txtOrder" id="txtOrder" runat="server" onkeyup="this.value=formatNumeric(this.value);"
                        onblur="this.value=formatNumeric(this.value);" maxlength="4" style="width: 500px;"
                        value="1" />
                </td>
            </tr>
            <tr style="height: 20px;">
                <th valign="top" class="left">
                    Activate
                </th>
                <td height="25">
                    <asp:RadioButtonList ID="rblStatus" runat="server" RepeatColumns="5">
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        <asp:ListItem Selected="True" Text="Yes" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
