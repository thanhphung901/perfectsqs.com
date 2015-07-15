<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="config_banner.aspx.cs" Inherits="vpro.eshop.cpanel.page.config_banner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Config Banner| Vpro.Eshop</title>
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
            <asp:LinkButton ID="lbtSaveNew" runat="server" OnClick="lbtSaveNew_Click"><img src="../Images/ICON_DELETE.png" width="30" height="30" style="border: 0px" /><div>
                    Save & Add New</div></asp:LinkButton>
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
    </div>
    <!--icon_function_parent-->
    <div id="field">
        <table width="auto" border="0">
            <tr>
                <td height="18" align="left" style="border-bottom: #e3e3e3 1px  solid;" class="general"
                    colspan="2">
                    Banner Information
                </td>
            </tr>
            <tr>
                <td height="5" colspan="2" align="left">
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    <span class="user">*</span>Description
                </th>
                <td>
                    <input type="text" name="txtTitle" id="txtTitle" runat="server" style="width: 500px;" />
                    <asp:Label ID="lblError" runat="server" CssClass="errormes" Text="Vui lòng nhập Description"
                        Visible="false"></asp:Label>
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
            <tr style="height: 20px;">
                <th valign="top" class="left">
                    Type
                </th>
                <td height="25">
                    <asp:RadioButtonList ID="rblBannerType" runat="server" RepeatColumns="5">
                        <asp:ListItem Selected="True" Text="Hình" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Flash" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
              <tr style="height: 20px;">
                <th valign="top" class="left">
                    Logo/Banner
                </th>
                <td height="25">
                    <asp:RadioButtonList ID="rblLogoBanner" runat="server" RepeatColumns="5">
                        <asp:ListItem Selected="True" Text="Logo" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Banner" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Banner phụ" Value="3"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    Order
                </th>
                <td>
                    <input type="text" name="txtOrder" id="txtOrder" runat="server" onblur="this.value=formatNumeric(this.value);"
                        maxlength="4" style="width: 500px;" value="1" />
                </td>
            </tr>
 
            <tr id="trUpload" runat="server">
                <th valign="top" class="left" nowrap>
                    Upload File
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
            <tr>
                <td style="height: 10px;" colspan="2">
                </td>
            </tr>
            <tr>
                <td height="18" align="left" style="border-bottom: #e3e3e3 1px  solid;" class="general"
                    colspan="2">
                    List Banners
                </td>
            </tr>
            <tr>
                <td height="5" colspan="2" align="left">
                </td>
            </tr>
            <tr>
                <th width="50" class="left">
                    Filter
                </th>
                <td width="300px;">
                    <input name="txtKeyword" type="text" id="txtKeyword" runat="server" style="float: left;
                        width: 300px" />
                    <div id="click" style="float: left; margin-left: 5px;">
                        <asp:LinkButton ID="lbtSearch" runat="server" OnClick="lbtSearch_Click">Search</asp:LinkButton>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height: 10px;" colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:DataGrid ID="GridItemList" CellPadding="0" runat="server" AutoGenerateColumns="False"
                        Width="100%" DataKeyField="BANNER_ID" CssClass="tdGridTable" SelectedIndex="0"
                        PagerStyle-Mode="NumericPages" PagerStyle-HorizontalAlign="Right" AllowPaging="false"
                        PageSize="15" PagerStyle-CssClass="PageClass" AllowSorting="true" OnItemCommand="GridItemList_ItemCommand"
                        OnItemDataBound="GridItemList_ItemDataBound" OnPageIndexChanged="GridItemList_PageIndexChanged"
                        OnSortCommand="GridItemList_SortCommand" GridLines="None">
                        <AlternatingItemStyle BackColor="#f3f3f3" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="#" HeaderStyle-CssClass="tdGridHeader" ItemStyle-CssClass="tdGridRow"
                                HeaderStyle-Wrap="False">
                                <HeaderStyle Width="1%"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSTT" runat="server" EnableViewState="False" Text='<%# getOrder() %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" CssClass="tdGridRow"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Description" HeaderStyle-Width="97%" ItemStyle-Wrap="False"
                                HeaderStyle-CssClass="tdGridHeader" ItemStyle-CssClass="tdGridRow" HeaderStyle-Wrap="False"
                                SortExpression="BANNER_DESC">
                                <ItemTemplate>
                                    <a href='<%# getLink(DataBinder.Eval(Container.DataItem, "BANNER_ID")) %>'>
                                        <%# DataBinder.Eval(Container.DataItem, "BANNER_DESC")%>
                                    </a>
                                </ItemTemplate>
                                <HeaderStyle Wrap="False" CssClass="tdGridHeader" Width="97%"></HeaderStyle>
                                <ItemStyle Wrap="False" CssClass="tdGridRow"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Banner File" HeaderStyle-Width="97%" ItemStyle-Wrap="False"
                                HeaderStyle-CssClass="tdGridHeader" ItemStyle-CssClass="tdGridRow" HeaderStyle-Wrap="False">
                                <ItemTemplate>
                                    <%# getLinkImage(DataBinder.Eval(Container.DataItem, "BANNER_ID"),DataBinder.Eval(Container.DataItem, "BANNER_FILE")) %>
                                </ItemTemplate>
                                <HeaderStyle Wrap="False" CssClass="tdGridHeader" Width="97%"></HeaderStyle>
                                <ItemStyle Wrap="False" CssClass="tdGridRow"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="#" HeaderStyle-Width="1%" ItemStyle-Wrap="False"
                                HeaderStyle-CssClass="tdGridHeader" ItemStyle-CssClass="tdGridRow" HeaderStyle-Wrap="False">
                                <ItemTemplate>
                                    <a href='<%# getLink(DataBinder.Eval(Container.DataItem, "BANNER_ID")) %>'>Edit
                                    </a>
                                </ItemTemplate>
                                <HeaderStyle Wrap="False" CssClass="tdGridHeader" Width="1%"></HeaderStyle>
                                <ItemStyle Wrap="False" CssClass="tdGridRow" HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Delete">
                                <HeaderStyle Wrap="False" CssClass="tdGridHeader" Width="1%"></HeaderStyle>
                                <ItemStyle Wrap="False" CssClass="tdGridRow" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="Delete">
                                <img src="../images/delete_icon.gif" title="Delete" border="0">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle Mode="NumericPages" HorizontalAlign="Right"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
