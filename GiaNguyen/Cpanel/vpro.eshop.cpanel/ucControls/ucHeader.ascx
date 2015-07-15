<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucHeader.ascx.cs" Inherits="vpro.eshop.cpanel.ucControls.ucHeader" %>
<div id="outer">
    <div id="logo">
    <a href="../page/default.aspx">
        <img src="../Images/logo_t.png" alt="" style="border: none;width:250px"/></a>
    </div>
    <!-- InstanceBeginEditable name="Title" -->
    <div id="menu_main" style="display:none;">
        <div id="menu_header">
            <asp:Label ID="lblHeader" runat="server"></asp:Label></div>
        <div id="menu_text">
            <a href="" id="hplHeader1" runat="server">
                </a><asp:Label ID="lb1" runat="server" Text=""></asp:Label><a href="" id="hplHeader2" runat="server">
            </a>
        </div>
        <!--menu_text-->
    </div>
    <!-- InstanceEndEditable -->
    <!--menu_main-->
    <div id="admin" style="display:none;">
        <a href="default.aspx">Trang chủ </a>| <span class="webcome">Chào mừng</span> <a href="../page/user_changepass.aspx"
                class="user">
                <asp:Label runat="server" ID="lblUser" EnableViewState="false"></asp:Label></a>
        <a href="../page/user_changepass.aspx">[ Thay đổi mật khẩu ]</a> <a href="logout.aspx">
            [ Thoát ]</a>
    </div>
    <!--admin-->
    <div id="admin" style="margin-top: 20px; font-size: 13px; font-weight: bold;">
       
    </div>
    <!--admin-->
</div>
