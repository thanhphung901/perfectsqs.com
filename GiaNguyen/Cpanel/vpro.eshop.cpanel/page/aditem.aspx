<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="aditem.aspx.cs" Inherits="vpro.eshop.cpanel.page.aditem" %>

<%@ Register Src="../Calendar/pickerAndCalendar.ascx" TagName="pickerAndCalendar"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Banner| Vpro.Eshop</title>
    <link href="../Calendar/calendarStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
				<!--
        function ToggleAll(e, action) {
            if (e.checked) {
                CheckAll();
            }
            else {
                ClearAll();
            }
        }

        function CheckAll() {
            var ml = document.forms[0];
            var len = ml.elements.length;
            for (var i = 1; i < len; i++) {
                var e = ml.elements[i];

                if (e.name.toString().indexOf("chkSelect") > 0)
                    e.checked = true;
            }
            ml.MainContent_GridItemList_toggleSelect.checked = true;
        }

        function ClearAll() {
            var ml = document.forms[0];
            var len = ml.elements.length;
            for (var i = 1; i < len; i++) {
                var e = ml.elements[i];
                if (e.name.toString().indexOf("chkSelect") > 0)
                    e.checked = false;
            }
            ml.MainContent_GridItemList_toggleSelect.checked = false;
        }

        function selectChange() {
            var radioButtons = document.getElementsByName("rblType");
            for (var x = 0; x < radioButtons.length; x++) {
                if (radioButtons[x].checked) {
                    if (radioButtons[x].value == 1)
                    { CheckAll(); }
                }
            }

        }
				    
				// -->
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="icon_function_parent">
        <%--       <div class="icon_function_Child">
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
            <asp:LinkButton ID="lbtSaveNew" runat="server" OnClick="lbtSaveNew_Click"><img src="../Images/ICON_ADD_NEW.png" width="30" height="30" style="border: 0px" /><div>
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
        <div class="icon_function_Child">
            <a href="aditem_list.aspx">
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
                <td height="18" align="left" style="border-bottom: #e3e3e3 1px  solid;" class="general"
                    colspan="2">
                    General Information
                </td>
            </tr>
            <tr>
                <td height="5" colspan="2" align="left">
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    <span class="user">*</span>Code
                </th>
                <td>
                    <input type="text" name="txtCode" id="txtCode" runat="server" style="width: 500px;" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter the code"
                        Text="Please enter the code" ControlToValidate="txtCode" CssClass="errormes"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th valign="top" class="left">
                    Description
                </th>
                <td>
                    <textarea id="txtDesc" runat="server" style="width: 500px;"></textarea>
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
                    Type
                </th>
                <td height="25">
                    <asp:RadioButtonList ID="rblBannerType" runat="server" RepeatColumns="5">
                        <asp:ListItem Selected="True" Text="Image" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Flash" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="height: 20px;">
                <th valign="top" class="left">
                    Position
                </th>
                <td height="25">
                    <asp:RadioButtonList ID="rblAdPos" runat="server" RepeatColumns="5">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="display:none;">
                <th valign="top" class="left">
                    <span class="user">*</span>Width
                </th>
                <td>
                    <input type="text" name="txtWidth" id="txtWidth" runat="server" onblur="this.value=formatNumeric(this.value);"
                        maxlength="4" style="width: 500px;" value="200" />
                </td>
            </tr>
            <tr style="display:none;">
                <th valign="top" class="left">
                    <span class="user">*</span>Height
                </th>
                <td>
                    <input type="text" name="txtHeight" id="txtHeight" runat="server" onblur="this.value=formatNumeric(this.value);"
                        maxlength="4" style="width: 500px;" value="100" />
                    <asp:RangeValidator ID="rvHeight" runat="server" ControlToValidate="txtHeight" Type="Integer"
                        ErrorMessage="Chiều cao phải lớn hơn 0" MaximumValue="1000" MinimumValue="1"
                        SetFocusOnError="True" CssClass="errormes"></asp:RangeValidator>
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
            <tr style="display:none;">
                <th valign="top" class="left">
                    <span class="user">*</span>From date
                </th>
                <td>
                    <uc1:pickerAndCalendar ID="ucFromDate" runat="server" />
                </td>
            </tr>
            <tr style="display:none;">
                <th valign="top" class="left">
                    <span class="user">*</span>To date
                </th>
                <td>
                    <uc1:pickerAndCalendar ID="ucToDate" runat="server" />
                </td>
            </tr>
            <tr style="height: 20px;display:none">
                <th valign="top" class="left">
                    Language
                </th>
                <td height="25">
                    <asp:RadioButtonList ID="rblLanguage" runat="server" RepeatColumns="5">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="height: 20px;display:none;">
                <th valign="top" class="left">
                     Click Count
                </th>
                <td height="25">
                    <asp:Label ID="lblCount" runat="server" EnableViewState="false"></asp:Label>
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
                    Select category
                </td>
            </tr>
            <tr>
                <td height="5" colspan="2" align="left">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div style="height: 500px; overflow: auto;">
                        <asp:DataGrid ID="GridItemList" CellPadding="0" runat="server" AutoGenerateColumns="False"
                            Width="100%" DataKeyField="CAT_ID" CssClass="tdGridTable" SelectedIndex="0" PagerStyle-Mode="NumericPages"
                            PagerStyle-HorizontalAlign="Right" AllowPaging="false" AllowSorting="true" GridLines="None">
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
                                <asp:TemplateColumn HeaderStyle-Width="1%" ItemStyle-Wrap="False" HeaderStyle-CssClass="tdGridHeader"
                                    ItemStyle-CssClass="tdGridRow" HeaderStyle-Wrap="False">
                                    <HeaderTemplate>
                                        <input type="checkbox" id="toggleSelect" runat="server" onclick="javascript: ToggleAll(this,0);"
                                            style="border-top-style: none; border-right-style: none; border-left-style: none;
                                            border-bottom-style: none" name="toggleSign">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <input id="chkSelect" type="checkbox" name="chkSelect" runat="server" style="border-top-style: none;
                                            border-right-style: none; border-left-style: none; border-bottom-style: none"
                                            checked='<%#CheckCat(DataBinder.Eval(Container.DataItem, "CAT_ID")) %>'>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="False" CssClass="tdGridHeader" Width="1%"></HeaderStyle>
                                    <ItemStyle Wrap="False" CssClass="tdGridRow" HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Categories" HeaderStyle-Width="97%" ItemStyle-Wrap="False"
                                    HeaderStyle-CssClass="tdGridHeader" ItemStyle-CssClass="tdGridRow" HeaderStyle-Wrap="False">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "CAT_NAME")%>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="False" CssClass="tdGridHeader" Width="97%"></HeaderStyle>
                                    <ItemStyle Wrap="False" CssClass="tdGridRow"></ItemStyle>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
