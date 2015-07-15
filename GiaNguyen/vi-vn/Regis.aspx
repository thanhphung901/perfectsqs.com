<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="Regis.aspx.cs" Inherits="perfectsqs.com.vi_vn.Regis" %>
<%@ Register src="../UIs/Regis.ascx" tagname="Regis" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="ltrFavicon" runat="server" EnableViewState="false"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Regis ID="Regis1" runat="server" />
</asp:Content>
