<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Slide.ascx.cs" Inherits="perfectsqs.com.UIs.Slide" %>
<div class="wrap bgWhite">
    <link rel="stylesheet" href="/vi-vn/Styles/nivo-slider.css" type="text/css" media="screen" />
    <script type="text/javascript" src="/vi-vn/scripts/jquery.nivo.slider.js"></script>
    <script>
        //slider nivo
        $(window).load(function () {
            $('#slider').nivoSlider();
        });
    </script>
    <div class="slider_main">
        <div class="innerSlider">
            <div class="slider-wrapper theme-default">
                <div id="slider" class="nivoSlider">
                    <asp:Repeater ID="Rpslider" runat="server">
                        <ItemTemplate>
                            <div class="itemSlide"><%# GetImageAd(Eval("AD_ITEM_ID"), Eval("AD_ITEM_FILENAME"), Eval("AD_ITEM_TARGET"), Eval("AD_ITEM_URL"))%></div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
    <div class="bottomSlide">
    </div>
</div>
