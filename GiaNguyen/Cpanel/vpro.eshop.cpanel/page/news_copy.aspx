<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="news_copy.aspx.cs" Inherits="vpro.eshop.cpanel.page.news_copy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Sao chép tin - Product | Vpro.Eshop </title>
    <script src="../Jquery/jquery.min.1.7.2.js" type="text/javascript"></script>
    <script src="../Jquery/JqueryCollapse/jquery.collapse.js" type="text/javascript"></script>
    <script language="javascript">
        new jQueryCollapse($("#seo"), {
            query: 'div h2'
        });

        new jQueryCollapse($("#dvPrice"), {
            query: 'div h2'
        });

        function ParseText(objsent) {
            ParseUrl(objsent, document.getElementById('MainContent_txtSeoUrl'));
            document.getElementById('MainContent_txtSeoTitle').value = objsent.value;
            document.getElementById('MainContent_txtSeoKeyword').value = objsent.value;
        }
        function ParseDesc(objsent) {
            document.getElementById('MainContent_txtSeoDescription').value = objsent.value;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="icon_function_parent">
        <div class="icon_function_Child">
            <asp:LinkButton ID="lbtHelp" runat="server">
                <img src="../Images/ICON_Help.jpg" width="30" height="30" style="border: 0px" /><div>
                    Trợ giúp</div>
            </asp:LinkButton>
        </div>
        <div class="icon_function_Child">
            <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click"><img src="../Images/ICON_SAVE.png" width="30" height="30" style="border: 0px" /><div>
                    Sao chép</div></asp:LinkButton>
        </div>
        <div class="icon_function_Child">
            <a href="#" onclick="javascript:document.location.reload(true);">
                <img src="../Images/ICON_UPDATE.jpg" width="30" height="30" style="border: 0px" /><div>
                    Refesh</div>
            </a>
        </div>
        <div class="icon_function_Child">
             <a href="#" id="hplBack" runat="server">
                <img src="../Images/ICON_RETURN.png" width="30" height="30" style="border: 0px" />
                <div>
                    Back</div>
            </a>
        </div>
    </div>
    <!--icon_function_parent-->
    <div id="field">
        <table width="auto" border="0">
        <tr id="trNewsFunction" runat="server">
                <td colspan="3" align="left">
                    <div id="icon_function_news">
                        <div class="icon_function_items">
                            <a href="#" id="hplCatNews" runat="server">
                                <img src="../Images/Button_Admin/ChonChuyenMuc.png" width="15" height="15" style="border: 0px" />
                                Categories </a>
                        </div>
                        <%--                        <div class="icon_function_items">
                            <a href="#" id="hplCatProducts" runat="server">
                                <img src="../Images/ICON_UPDATE.jpg" width="15" height="15" style="border: 0px" />
                                <asp:Label ID="lbCatNews" runat="server" Text="Chọn món ăn "></asp:Label></a>
                        </div>--%>
                        <div class="icon_function_items">
                            <a href="#" id="hplEditorHTMl" runat="server">
                                <img src="../Images/Button_Admin/c_html.png" width="15" height="15" style="border: 0px" />
                                HTML Compose </a>
                        </div>
                        <div class="icon_function_items">
                            <a href="#" id="hplNewsAtt" runat="server">
                                <img src="../Images/Button_Admin/Filedinhkem.png" width="15" height="15" style="border: 0px" />
                                Attach File </a>
                        </div>
                        <div class="icon_function_items">
                            <a href="#" id="hplAlbum" runat="server">
                                <img src="../Images/Button_Admin/Album_Hinh.png" width="15" height="15" style="border: 0px" />
                                Album </a>
                        </div>
                        <div class="icon_function_items">
                            <a href="#" id="hplComment" runat="server">
                                <img src="../Images/Button_Admin/ThongTinPhanHoi.png" width="15" height="15" style="border: 0px" />
                                Feedback</a>
                        </div>
                        <div class="icon_function_items">
                            <a href="#" id="bplNewsCopy" runat="server">
                                <img src="../Images/Button_Admin/c_copy.png" width="15" height="15" style="border: 0px" />
                                Copy</a>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td height="5" colspan="3" align="left">
                </td>
            </tr>
            <tr>
                <td height="18" align="left" style="border-bottom: #e3e3e3 1px  solid;" class="general"
                    colspan="2">
                    Tùy chọn sao chép
                </td>
            </tr>
            <tr>
                <td height="5" colspan="3" align="left">
                </td>
            </tr>
            <tr style="height: 20px;">
                <th valign="top" class="left">
                    Sao chép hình minh họa
                </th>
                <td height="25">
                    <asp:RadioButtonList ID="rblNewsImage" runat="server" RepeatColumns="5">
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        <asp:ListItem Selected="True" Text="Yes" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="height: 20px;">
                <th valign="top" class="left">
                    Sao chép album hình
                </th>
                <td height="25">
                    <asp:RadioButtonList ID="rblNewsAlbum" runat="server" RepeatColumns="5">
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        <asp:ListItem Selected="True" Text="Yes" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="height: 20px;">
                <th valign="top" class="left">
                    Sao chép Content
                </th>
                <td height="25">
                    <asp:RadioButtonList ID="rblNewsContent" runat="server" RepeatColumns="5">
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        <asp:ListItem Selected="True" Text="Yes" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="height: 20px;">
                <th valign="top" class="left">
                    Sao chép file đính kèm
                </th>
                <td height="25">
                    <asp:RadioButtonList ID="rblNewsAtt" runat="server" RepeatColumns="5">
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        <asp:ListItem Selected="True" Text="Yes" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td height="18" align="left" style="border-bottom: #e3e3e3 1px  solid;" class="general"
                    colspan="2">
                    Thông tin chung
                </td>
            </tr>
            <tr>
                <td height="5" colspan="3" align="left">
                </td>
            </tr>
            <tr>
                <td colspan="3" align="left">
                    <asp:Label CssClass="user" ID="lblError" runat="server"></asp:Label>
                </td>
            </tr>
            <tr id="trCat" runat="server">
                <th valign="top" class="left">
                    Category
                </th>
                <td>
                    <asp:DropDownList ID="ddlCategory" runat="server" Width="500px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    <span class="user">*</span>Title
                </th>
                <td>
                    <input type="text" name="txtTitle" id="txtTitle" runat="server" style="width: 500px;"
                        onkeyup="ParseText(this);" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập tên Title"
                        Text="Vui lòng nhập Title" ControlToValidate="txtTitle" CssClass="errormes"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    Description
                </th>
                <td>
                    <textarea id="txtDesc" runat="server" style="width: 500px;" onkeyup="ParseDesc(this);"></textarea>
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    Link
                </th>
                <td>
                    <input type="text" name="txtUrl" id="txtUrl" runat="server" style="width: 425px;" />
                    <span>
                        <asp:DropDownList ID="ddlTarget" runat="server">
                        </asp:DropDownList>
                    </span>
                </td>
            </tr>
            <tr style="height: 20px;">
                <th valign="top" class="left">
                    Loại thông tin
                </th>
                <td height="25">
                    <asp:Literal ID="liNameTitle" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr style="height: 20px;">
                <th valign="top" class="left">
                    Cho phép phản hồi
                </th>
                <td height="25">
                    <asp:RadioButtonList ID="rblFeefback" runat="server" RepeatColumns="5">
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Yes" Selected="True" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="height: 20px;">
                <th valign="top" class="left">
                    Language
                </th>
                <td height="25">
                    <asp:RadioButtonList ID="rblLanguage" runat="server" RepeatColumns="5">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    Lượt truy cập
                </th>
                <td>
                    <asp:Label ID="lblCount" runat="server" EnableViewState="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    Gửi email
                </th>
                <td>
                    <asp:Label ID="lblSendEmail" runat="server" EnableViewState="false"></asp:Label>
                </td>
            </tr>
        </table>
        <div id="dvOption" style="width: 650px;" data-collapse>
            <h3 class="collapse">
                Thông tin Activate</h3>
            <div>
                <table width="auto" border="0">
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
                    <tr style="height: 20px;">
                        <th valign="top" class="left">
                            Outside of homepage
                        </th>
                        <td height="25">
                            <asp:RadioButtonList ID="rblNewsPeriod" runat="server" RepeatColumns="5">
                                <asp:ListItem Selected="True" Text="No" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr style="height: 20px;">
                        <th valign="top" class="left">
                            Activate trong chi tiết
                        </th>
                        <td height="25">
                            <asp:RadioButtonList ID="rblShowDetail" runat="server" RepeatColumns="5">
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                <asp:ListItem Selected="True" Text="Yes" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <th valign="top" class="left">
                            Order
                        </th>
                        <td>
                            <input type="text" name="txtOrder" id="txtOrder" runat="server" onkeyup="this.value=formatNumeric(this.value);" onblur="this.value=formatNumeric(this.value);"
                                maxlength="4" style="width: 500px;" value="1" />
                        </td>
                    </tr>
                    <tr>
                        <th valign="top" class="left">
                            Order trang chủ
                        </th>
                        <td>
                            <input type="text" name="txtOrderPeriod" id="txtOrderPeriod" runat="server" onkeyup="this.value=formatNumeric(this.value);" onblur="this.value=formatNumeric(this.value);"
                                maxlength="4" style="width: 500px;" value="1" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="dvPrice" style="width: 650px;" data-collapse>
            <h3 class="collapse open">
                Thông tin giá cả</h3>
            <div>
                <table width="auto" border="0">
                    <tr>
                        <th valign="top" class="left">
                            Giá bán
                        </th>
                        <td>
                            <input type="text" name="txtPrice" id="txtPrice" runat="server" onblur="this.value=formatNumeric(this.value);"
                                maxlength="20" style="width: 425px;" value="0" />
                            <span>
                                <asp:DropDownList ID="ddlUnit1" runat="server">
                                </asp:DropDownList>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <th valign="top" class="left">
                            Giá khuyến mãi
                        </th>
                        <td>
                            <input type="text" name="txtPriceSub" id="txtPriceSub" runat="server" onblur="this.value=formatNumeric(this.value);"
                                maxlength="20" style="width: 425px;" value="0" />
                            <span>
                                <asp:DropDownList ID="ddlUnit2" runat="server">
                                </asp:DropDownList>
                            </span>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="seo" style="width: 650px;" data-collapse>
            <h3 class="collapse">
                SEO Parameters</h3>
            <div>
                <table width="auto" border="0">
                    <tr>
                        <th valign="top" class="left">
                            <span class="user">*</span>SEO Title
                        </th>
                        <td>
                            <input type="text" name="txtSeoTitle" id="txtSeoTitle" runat="server" style="width: 500px;" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Vui lòng nhập tên nhóm"
                                Text="Please enter the Seo Title" ControlToValidate="txtSeoTitle" CssClass="errormes"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <th valign="top" class="left">
                            <span class="user">*</span>SEO URL
                        </th>
                        <td>
                            <input type="text" name="txtSeoUrl" id="txtSeoUrl" runat="server" style="width: 500px;" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Vui lòng nhập tên nhóm"
                                Text="Please enter the Seo Title" ControlToValidate="txtSeoUrl" CssClass="errormes"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <th valign="top" class="left">
                            SEO Keyword
                        </th>
                        <td>
                            <textarea id="txtSeoKeyword" runat="server" style="width: 500px;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <th valign="top" class="left">
                            SEO Description
                        </th>
                        <td>
                            <textarea id="txtSeoDescription" runat="server" style="width: 500px;"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="dvImage" style="width: 650px;" data-collapse>
            <h3 class="collapse">
                Image Infomation</h3>
            <div>
                <table width="auto" border="0">
                    <tr id="trImage1" runat="server">
                        <th valign="top" class="left">
                            Hình minh họa
                        </th>
                        <td>
                            <asp:HyperLink runat="server" ID="hplImage1" Target="_blank"></asp:HyperLink><br />
                            <img id="Image1" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
