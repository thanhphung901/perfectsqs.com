<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuTop.ascx.cs" Inherits="perfectsqs.com.UIs.MenuTop" %>

<div class=" wrap wMainMenu">
  <div class="container ">
    <div class="rowmn">
      <div class="col12 cNavx col-xs-12 col-sm-12">
        <div class="navx"> <a href="#" id="pull">MENU <i class="fa fa-list"></i></a>
          <ul>
            <asp:Repeater ID="Rpmenu" runat="server">
                <ItemTemplate>
                    <li class='<%#GetStyleActive(Eval("cat_seo_url"),Eval("cat_url")) %>'>
                        <a href="<%#GetLink(Eval("cat_url"),Eval("cat_seo_url"),1)%>"><%#Eval("cat_name")%></a>
                        <asp:Repeater ID="Repeater1" runat="server" DataSource='<%# Load_Menu2(Eval("Cat_ID")) %>'>
                            <HeaderTemplate>
                                <ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li><a href="<%#GetLink(Eval("cat_url"),Eval("cat_seo_url"),1)%>">
                                    <%#Eval("cat_name")%>
                                    <asp:Repeater ID="Repeater1" runat="server" DataSource='<%# Load_Menu2(Eval("Cat_ID")) %>'>
                                        <HeaderTemplate>
                                            <ul>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <li><a href="<%#GetLink(Eval("cat_url"),Eval("cat_seo_url"),1)%>">
                                                <%#Eval("cat_name")%></a></li>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </ul>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </a></li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
          </ul>
        </div>
      </div>
    </div>
  </div>
</div>