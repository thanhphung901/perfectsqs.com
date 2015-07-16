<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsDetail.ascx.cs"
    Inherits="perfectsqs.com.UIs.NewsDetail" %>
<%@ Register Src="BannerDetail.ascx" TagName="BannerDetail" TagPrefix="uc1" %>
<%@ Register Src="Path.ascx" TagName="Path" TagPrefix="uc2" %>
<uc1:BannerDetail ID="BannerDetail1" runat="server" />
<div class="container">
    <div class="rowmn rMain">
        <div class="breadcrumbBox">
            <ul class="breadcrumb">
                <uc2:Path ID="Path1" runat="server" />
            </ul>
        </div>
        <div class="clearfix">
        </div>
        <div class="box">
            <div class="heading_page"><asp:Label ID="lbNewsTitle" runat="server" /></div>
            <div class="innerbox">
                <div class="list-media">
                    <div class="item-media">
                        <div class="inner-item-media">
                            <div class="content-media"><asp:Literal ID="liHtml" runat="server"></asp:Literal></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="wmn line-bt-main">
</div>
