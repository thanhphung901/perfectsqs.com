﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="perfectsqs.com.UIs.Header" %>
<div class="wrap wHeader  ">
    <div class="container">
        <div class="rowmn rHeader">
            <div class="col2 col-xs-12 col-md-2 col-sm-12 cLogo">
                <asp:Repeater ID="Rplogo" runat="server">
                    <ItemTemplate>
                        <%# Getbanner(Eval("BANNER_TYPE"),Eval("BANNER_FIELD1"), Eval("BANNER_ID"), Eval("BANNER_FILE"))%>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class=" col7 col-md-7 col-xs-12 col-sm-12 fright rightTop">
                <div class="top-right-header">
                    <asp:Literal ID="liTitle" runat="server"></asp:Literal>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
</div>
