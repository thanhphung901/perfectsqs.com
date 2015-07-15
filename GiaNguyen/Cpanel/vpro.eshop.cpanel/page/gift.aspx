<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="gift.aspx.cs" Inherits="vpro.eshop.cpanel.page.gift" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>New - Product | Vpro.Eshop </title>
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
            <a href="gift_list.aspx">
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
           
            <%-- <tr>
                <td height="5" colspan="3" align="left">
                </td>
            </tr>--%>
            <tr>
                <td colspan="3" align="left">
                    <asp:Label CssClass="user" ID="lblError" runat="server"></asp:Label>
                </td>
            </tr>
            <tr id="trCat" runat="server">
                <th valign="top" class="left">
                    Tên quà tặng
                </th>
                <td>
                    <asp:TextBox ID="txtGiftName" runat="server"></asp:TextBox>
                </td>
            </tr>
           
            <tr>
                <th valign="top" class="left">
                    Description
                </th>
                <td>
                    <textarea id="txtGiftDesc" runat="server" style="width: 500px;" onkeyup="ParseDesc(this);" onblur="ParseDesc(this);"></textarea>
                </td>
            </tr>
           
            <tr>
                <th valign="top" class="left">
                    <span class="user">*</span>Điểm
                </th>
                <td>
                    <input type="text" name="txtGiftPoint" id="txtGiftPoint" runat="server" style="width: 500px;"
                        onkeyup="ParseText(this);" onblur="ParseText(this);" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập điểm"
                        Text="Vui lòng nhập điểm" ControlToValidate="txtGiftPoint" CssClass="errormes"></asp:RequiredFieldValidator>
                </td>
            </tr>
             
            <tr>
                <th valign="top" class="left">
                    Số lượng tồn
                </th>
                <td>
                    <input type="text" name="txtGridAmount" id="txtGridAmount" runat="server" style="width: 425px;" />
                    
                </td>
            </tr>
            
            <tr style="height: 20px;">
                <th valign="top" class="left">
                    Số lượng cho phép tối đa
                </th>
                <td height="25">
                   <input type="text" name="txtGridMaxAmount" id="txtGridMaxAmount" runat="server" style="width: 425px;" />
                    
                </td>
            </tr>
            <tr style="height: 20px;">
                <th valign="top" class="left">
                    Content chi tiết
                </th>
                <td height="25">
                    
                       <textarea id="txtGiftContent" runat="server" style="width: 500px;" onkeyup="ParseDesc(this);" onblur="ParseDesc(this);"></textarea>

                </td>
            </tr>
           
        </table>
       
        
        <div id="dvImage" style="width: 650px;" data-collapse>
            <h3 class="collapse">
                Image Infomation</h3>
            <div>
                <table width="auto" border="0">
                    <tr id="trUploadImage1" runat="server">
                        <th valign="top" class="left">
                            Hình minh họa
                        </th>
                        <td>
                            <input id="fileImage1" type="file" name="fileImage1" size="50" runat="server" style="width: 500px;">
                        </td>
                    </tr>
                    <tr id="trImage1" runat="server">
                        <th valign="top" class="left">
                            <asp:ImageButton ID="btnDelete1" runat="server" ImageUrl="../images/delete_icon.gif"
                                BorderWidth="0" Width="13px" OnClick="btnDelete1_Click" ToolTip="Do you want delete this image?">
                            </asp:ImageButton>
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
