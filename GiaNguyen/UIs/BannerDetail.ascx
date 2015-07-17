<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerDetail.ascx.cs" Inherits="perfectsqs.com.UIs.BannerDetail" %>

<div class="banner-cate">
    <asp:Repeater ID="Rpslider" runat="server">
        <ItemTemplate>
            <%# GetImageAd(Eval("AD_ITEM_ID"), Eval("AD_ITEM_FILENAME"), Eval("AD_ITEM_TARGET"), Eval("AD_ITEM_URL"))%>
        </ItemTemplate>
    </asp:Repeater>
</div>