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
          <div class="itemSlide"> <a href=""> <img src="data/slide-1.jpg" title="..."/></a> </div>
          <div class="itemSlide"> <a href=""><img src="data/slide-2.jpg" title="...."/></a> </div>
          <div class="itemSlide"> <a href=""><img src="data/slide-3.jpg" title="caption"/></a> </div>
          <div class="itemSlide"> <a href=""> <img src="data/slide-4.jpg" title="..."/></a> </div>
          <div class="itemSlide"> <a href=""> <img src="data/slide-5.jpg" title="..."/></a> </div>
          <div class="itemSlide"> <a href=""> <img src="data/slide-6.jpg" title="..."/></a> </div>
        </div>
      </div>
    </div>
  </div>
  <div class="bottomSlide"></div>
</div>