<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="vpro.eshop.cpanel.page.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login | Vpro.Eshop</title>
    <link href="../Styles/Cpanel_Login.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function setfocus() {
            var txtUN = document.getElementById("txtUN");
            txtUN.focus();
        }

    </script>
    <asp:Literal ID="ltrFavicon" runat="server" EnableViewState="false"></asp:Literal>
</head>
<body>
    <form id="form1" runat="server">
    <div id="outer">
        <div id="logo">
            <%--<img src="../Images/logo_t.png" alt="" style="border: none;width:250px"/>--%></div>
        <div id="content">
            <div id="login">
                <img src="../Images/Login_header.png" width="330" height="5" />
                <div id="title">
                    Login</div>
                <div id="information">
                    <table width="332" border="0" cellpadding="3" style="padding-top: 15px;">

                        <tr>
                            <td width="100" align="right" valign="middle">
                                Username
                            </td>
                            <td width="5">
                                &nbsp;
                            </td>
                            <td width="195" align="left">
                                <label id="textfield">
                                    <input name="txtUN" type="text" class="text" id="txtUN" runat="server" />
                                    <script language="javascript">
                                        setfocus();
                                    </script>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="middle">
                                Password
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="left">
                                <label>
                                    <input name="txtPW" type="password" class="text" id="txtPW" runat="server" />
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="middle">
                                <%--<label>
                                    <input type="checkbox" name="checkbox" id="checkbox" />
                                </label>
                                Ghi nhớ--%>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="left">
                                <div id="click" class="click" style="margin-top: 10px;">
                                    <asp:LinkButton ID="lbtLogin" runat="server" OnClick="lbtLogin_Click">Login</asp:LinkButton></div>
                            </td>
                        </tr>
                        
                        <%--<tr>
                            <td align="right" valign="middle">
                                <img src="../Images/MuiTen.jpg" width="9" height="7" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="left">
                                <div class="fogetpass">
                                    <a href="#" class="a">Bạn quên mật khẩu ? </a>
                                </div>
                            </td>
                        </tr>--%>
                        <tr>
                            <td colspan="3" style="text-align: center;">
                                <asp:Label ID="lblError" runat="server" EnableViewState="False" CssClass="user" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                         <%--<tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>--%>
                    </table>
                </div>
            </div>
        </div>
        <div id="footer">
            Copyright © 2015 by nidushealth.com. All rights reserved</div>
    </div>
    </form>
</body>
</html>
