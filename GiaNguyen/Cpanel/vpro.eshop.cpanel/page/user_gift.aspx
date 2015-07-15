<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="user_gift.aspx.cs" Inherits="vpro.eshop.cpanel.page.user_gift" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Contact Information | vpro.eshop</title>
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
            <asp:LinkButton ID="lbtSave" runat="server" onclick="lbtSave_Click"><img src="../Images/ICON_SAVE.png" width="30" height="30" style="border: 0px" /><div>
                    Save</div></asp:LinkButton>
        </div>
        <div class="icon_function_Child" id="dvDelete" runat="server">
            <asp:LinkButton ID="lbtDelete" runat="server" OnClick="lbtDelete_Click" OnClientClick="return confirm('Do you want delete?');"
                CausesValidation="false">
                <img src="../Images/ICON_DELETE.png" width="30" height="30" style="border: 0px" /><div>
                    Delete</div>
            </asp:LinkButton>
        </div>
        <div class="icon_function_Child">
            <a href="#" onclick="javascript:document.location.reload(true);">
                <img src="../Images/ICON_UPDATE.jpg" width="30" height="30" style="border: 0px" /><div>
                    Refesh</div>
            </a>
        </div>
        <div class="icon_function_Child">
            <a href="user_gift_list.aspx">
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
                <td height="18" align="left" style="border-bottom: #e3e3e3 1px  solid;" class="general"
                    colspan="2">
                    Thông tin nhận quà
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    Ngày đổi quà
                </th>
                <td>
                    <input type="text" name="txtOrderDate" id="txtOrderDate" runat="server" style="width: 500px;"
                        readonly="readonly" />
                </td>
            </tr>
            
            <tr>
                <th valign="top" class="left">
                    Trạng thái
                </th>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="500px">
                        <asp:ListItem Value="0" Text="Chưa xử lý"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Đã giao quà"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    Ghi chú
                </th>
                <td>
                    <textarea id="txtOrderDesc" runat="server" style="width: 500px;" ></textarea>
                </td>
            </tr>
            <tr>
                <td height="5" colspan="3" align="left">
                </td>
            </tr>
            <tr>
                <td height="18" align="left" style="border-bottom: #e3e3e3 1px  solid;" class="general"
                    colspan="2">
                    Thông tin người mua hàng
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    Tên
                </th>
                <td>
                    <input type="text" name="txtName" id="txtName" runat="server" style="width: 500px;"
                        readonly="readonly" />
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    Email
                </th>
                <td>
                    <input type="text" name="txtEmail" id="txtEmail" runat="server" style="width: 500px;"
                        readonly="readonly" />
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    Địa chỉ
                </th>
                <td>
                    <input type="text" name="txtAddress" id="txtAddress" runat="server" style="width: 500px;"
                        readonly="readonly" />
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    Điện thoại
                </th>
                <td>
                    <input type="text" name="txtPhone" id="txtPhone" runat="server" style="width: 500px;"
                        readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td height="5" colspan="2" align="left">
                </td>
            </tr>
           
            <tr>
                <td height="18" align="left" style="border-bottom: #e3e3e3 1px  solid;" class="general"
                    colspan="2">
                    Danh sách mặt hàng
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <asp:DataGrid ID="GridItemList" CellPadding="0" runat="server" AutoGenerateColumns="False"
                        Width="100%" DataKeyField="USER_GIFT_ID" CssClass="tdGridTable" SelectedIndex="0"
                        PagerStyle-Mode="NumericPages" PagerStyle-HorizontalAlign="Right" AllowPaging="false"
                        PageSize="20" PagerStyle-CssClass="PageClass" AllowSorting="true" OnPageIndexChanged="GridItemList_PageIndexChanged"
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
                            <asp:TemplateColumn HeaderText="Quà tặng" HeaderStyle-Width="25%" ItemStyle-Wrap="False"
                                HeaderStyle-CssClass="tdGridHeader" ItemStyle-CssClass="tdGridRow" HeaderStyle-Wrap="False"
                                SortExpression="GIFT_NAME">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "GIFT_NAME")%>
                                </ItemTemplate>
                                <HeaderStyle Wrap="False" CssClass="tdGridHeader" Width="25%"></HeaderStyle>
                                <ItemStyle Wrap="False" CssClass="tdGridRow"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Description" HeaderStyle-Width="25%" ItemStyle-Wrap="False"
                                HeaderStyle-CssClass="tdGridHeader" ItemStyle-CssClass="tdGridRow" HeaderStyle-Wrap="False"
                                SortExpression="GIFT_DESC">
                                <ItemTemplate>
                                    <%# GetMoney(DataBinder.Eval(Container.DataItem, "GIFT_DESC"))%>
                                </ItemTemplate>
                                <HeaderStyle Wrap="False" CssClass="tdGridHeader" Width="25%"></HeaderStyle>
                                <ItemStyle Wrap="False" CssClass="tdGridRow" HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Số lượng" HeaderStyle-Width="24%" ItemStyle-Wrap="False"
                                HeaderStyle-CssClass="tdGridHeader" ItemStyle-CssClass="tdGridRow" HeaderStyle-Wrap="False"
                                SortExpression="USER_GIFT_QUANTITY">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "USER_GIFT_QUANTITY")%>
                                </ItemTemplate>
                                <HeaderStyle Wrap="False" CssClass="tdGridHeader" Width="24%" HorizontalAlign="Left">
                                </HeaderStyle>
                                <ItemStyle Wrap="False" CssClass="tdGridRow" HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateColumn>
                           
                        </Columns>
                        <PagerStyle Mode="NumericPages" HorizontalAlign="Right"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <th valign="top" class="left" style=" text-align: right;" colspan="2">
                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                </th>
                
            </tr>
            <tr>
                <td height="15" colspan="2" align="left">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
