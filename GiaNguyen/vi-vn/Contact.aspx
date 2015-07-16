<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true"
    CodeBehind="Contact.aspx.cs" Inherits="perfectsqs.com.vi_vn.Contact" %>
    <%@ Register Src="~/UIs/BannerDetail.ascx" TagName="BannerDetail" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="ltrFavicon" runat="server" EnableViewState="false"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc1:BannerDetail ID="BannerDetail1" runat="server" />
<div class="container">
    <div class="rowmn rMain">
      <div class="breadcrumbBox">
        <ul class="breadcrumb">
          <li><a href="/">Home </a></li>
          <li class="active">Contact Us</li>
        </ul>
      </div>
      <div class="clearfix"></div>
      <div class="box">
        <div class="heading_page">Contact Us</div>
        <div class="innerbox">
          <div class="contacBox">
            <p style="font-size: 16px;  color: #F89A19">Please complete the form below and we will contact you within 24 hours." </p>
            <div class="col6 col-sm-12 col-md-6">
              <div class="frmn">
                <div class="frm_group">
                  <div class="lbl"> Your Name: <span class="red">*</span></div>
                  <div class="input_group">
                    <input class="form-control" placeholder="Your first name" type="text" id="txtName" runat="server">
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="Please enter your name"
                        ControlToValidate="txtName" Display="None" ForeColor="Red" ValidationGroup="G40">*</asp:RequiredFieldValidator>
                  </div>
                </div>
                <div class="frm_group">
                  <div class="lbl">Phone: <span class="red"></span></div>
                  <div class="input_group">
                    <input class="form-control" type="text" id="txtPhone" runat="server">
                  </div>
                </div>
                <div class="frm_group">
                  <div class="lbl">Your Email: <span class="red">*</span></div>
                  <div class="input_group">
                    <input class="form-control" type="text" id="txtEmail" runat="server">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter your email address"
                        ControlToValidate="txtEmail" Display="None" ForeColor="Red" ValidationGroup="G40">*</asp:RequiredFieldValidator>
                  </div>
                </div>
                <div class="frm_group">
                  <div class="lbl">Subject: <span class="red">*</span></div>
                  <div class="input_group">
                    <input class="form-control" type="text" id="txtSubject" runat="server">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter your subject"
                        ControlToValidate="txtSubject" Display="None" ForeColor="Red" ValidationGroup="G40">*</asp:RequiredFieldValidator>
                  </div>
                </div>
                <div class="frm_group">
                  <div class="lbl">Your Message: <span class="red"></span></div>
                  <div class="input_group">
                    <textarea name="" id="txtMessage" class="form-control" rows="5"  style=" min-height:80px;" runat="server"></textarea>
                  </div>
                </div>
                <div class="frm_group">
                  <div class="lbl">Code: <span class="red">*</span></div>
                  <div class="input_group captcha">
                    <input type="text"  class="form-control" id="" name="" placeholder="" required>
                    <div class="clearfix"></div>
                    <img src="/vi-vn/images/captcha.jpg" /> </div>
                </div>
                <div class="frm_group">
                  <div class="lbl"> <span class="red"> </span></div>
                  <div class="input_group  ">
                    <div class="ct-submit">
                        <asp:LinkButton ID="btnSubmit" runat="server" ValidationGroup="G40" 
                            onclick="btnSubmit_Click">&nbsp;</asp:LinkButton>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="col5 col-sm-12 col-md-5 fright">  
              <div class="address">
                <p> <i class="fa fa-map-marker "></i><strong>US Office:</strong><br>
                  Perfect Squares, Inc.,<br>
                  610 S Industrial Blvd., Suite 210<br>
                  Euless, TX 76040 USA</p>
                <p> <i class="fa fa-map-marker "></i><strong>India Office:</strong><br>
                  Perfect-Squares Technologies Pvt Ltd.<br>
                  1st Floor, V-4, RK Complex, KSSIDC Compound, Electronics City<br>
                  Phase I, Hosur Road Bangalore-560100<br>
                  Karnataka, India </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
   <div class="wmn line-bt-main"></div>
   <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="G40" />
</asp:Content>
