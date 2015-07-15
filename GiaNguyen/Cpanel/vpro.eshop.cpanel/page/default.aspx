<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="vpro.eshop.cpanel.page._default" %>

<%@ Register Src="../ucControls/ucHeader.ascx" TagName="ucHeader" TagPrefix="uc1" %>
<%@ Register Src="../ucControls/ucFooter.ascx" TagName="ucFooter" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Cpanel Pro-v1.0</title>
    <link href="../Styles/Cpanel_Login.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/Cpanel_Site.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/UtilitiesCpanel.js" type="text/javascript"></script>
    <asp:Literal ID="ltrFavicon" runat="server" EnableViewState="false"></asp:Literal>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:ucHeader ID="ucHeader1" runat="server" />
    <div id="content_product">
        <asp:PlaceHolder ID="phdLeft" runat="server"></asp:PlaceHolder>
        <div id="contentpr_right">
        <div id="menu">
            <div id="menu_parent">
                <div id="menu_child">
                    <div id="menu_images">
                        <img src="../Images/m_folder.png" width="70" height="70" /></div>
                    <div id="menu_title">
                        <a href="../page/category_list.aspx">Categories</a></div>
                    <div id="menu_content">
                        Category is the place to declare all the menu shown on the website. Includes menu(top, left, right....)</div>
                </div>
                <div id="menu_child">
                    <div id="menu_images">
                        <img src="../Images/Admin_news.png" width="70" height="70" /></div>
                    <div id="menu_title">
                        <a href="../page/news_list.aspx?type=0">News</a></div>
                    <div id="menu_content">
                        Add/ Update all articles on the website, the news article, the article writing service.</div>
                </div>
                <div id="menu_child">
                    <div id="menu_images">
                        <img src="../Images/Admin_adv.png" width="70" height="70" /></div>
                    <div id="menu_title">
                        <a href="../page/aditem_list.aspx">Banners</a>
                    </div>
                    <div id="menu_content">
                        Upload the banner ad from the client side, the logo of the partner company.</div>
                </div>
                <div id="menu_child">
                    <div id="menu_images">
                        <img src="../Images/m_contact.png" width="70" height="70" /></div>
                    <div id="menu_title">
                        <a href="../page/online_list.aspx">Support Online</a></div>
                    <div id="menu_content">
                        Support online list</div>
                </div>
            </div>
            <div id="menu_parent">
                <div id="menu_child">
                    <div id="menu_images">
                        <img src="../Images/m_contact_info.png" width="70" height="70" /></div>
                    <div id="menu_title">
                        <a href="../page/contact_list.aspx">Contact Information</a></div>
                    <div id="menu_content">
                        View information from the customer contact sent from the website.</div>
                </div>
                <div id="menu_child" style="display:none;">
                    <div id="menu_images">
                        <img src="../Images/app_product.png" width="70" height="70" /></div>
                    <div id="menu_title">
                        <a href="../page/news_list.aspx?type=1">Products</a></div>
                    <div id="menu_content">
                        Add/ update all products on the website, add new products.</div>
                </div>
                <div id="menu_child">
                    <div id="menu_images">
                        <img src="../Images/Admin_User_Group.png" width="70" height="70" /></div>
                    <div id="menu_title">
                        <a href="../page/user_list.aspx">Manager</a></div>
                    <div id="menu_content">
                        Add / delete / edit / permissions for the administrator group and the administrator.</div>
                </div>
                <div id="menu_child">
                    <div id="menu_images">
                        <img src="../Images/Admin_General.png" width="70" height="70" /></div>
                    <div id="menu_title">
                        <a href="../page/config_meta.aspx">General Configuration </a>
                    </div>
                    <div id="menu_content">
                        Meta configuration for seo &amp; logo, configure footer</div>
                </div>
            </div>
        </div>
        </div>
    </div>
    <uc2:ucFooter ID="ucFooter1" runat="server" />
    </form>
</body>
</html>
