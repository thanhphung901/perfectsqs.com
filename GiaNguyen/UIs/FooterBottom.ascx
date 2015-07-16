<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FooterBottom.ascx.cs" Inherits="perfectsqs.com.UIs.FooterBottom" %>

<div class="wrap footer bgWhite">
<div class="wmn lin-f"></div>
  <div class="container">
    <div class="row rFooter">
      <div class=" fleft col6 col-md-6 colleftfoot">
        <div class="menu-main-menu-container">
          <h2>Quick link</h2>
          <ul class="f-links">
            <asp:Repeater ID="Rpmenu" runat="server">
                <ItemTemplate>
                    <li><a href="<%#GetLink(Eval("cat_url"),Eval("cat_seo_url"),1)%>"><%#Eval("cat_name")%></a></li>
                </ItemTemplate>
            </asp:Repeater>
          </ul>
        </div>
      </div>
      <div class="f-right-sec fright col6 col-md-6 col-sm-12 col-xs-12">
        <div class="top-right">
          <div class="col6 fright">
            <h2>India Office</h2>
            <div class="clearfix"></div>
            <h3>Perfect-Squares Technologies Pvt Ltd. <br />
              1st Floor, V-4, RK Complex<br />
              KSSIDC Compound, Electronics City <br />
              Phase I, Hosur Road Bangalore-560100 <br />
              Karnataka, India </h3>
          </div>
          <div class="col6 fright">
            <h2>US Office</h2>
            <div class="clearfix"></div>
            <h3>610 S Industrial Blvd<br>
              City Euless, TX 76040 - USA <br />
              Tel. 972 850 7626 - Fax. 972 476 0855 <br />
              info@perfectsqs.com </h3>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
<div class="wrap wCrp bgWhite"><p class="copyright">© PERFECT SQUARES. All Rights Reserved.</p></div>