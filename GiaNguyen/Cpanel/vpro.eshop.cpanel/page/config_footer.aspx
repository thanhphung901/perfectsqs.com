<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="config_footer.aspx.cs" Inherits="vpro.eshop.cpanel.page.config_footer"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Configuration Footer | Vpro.Eshop</title>
    <script src="../tiny_mce/tiny_mce.js" type="text/javascript"></script>
    <script type="text/javascript">
        tinyMCE.init({
            // General options
            mode: "textareas",
            theme: "advanced",
            plugins: "autolink,lists,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,wordcount,advlist,autosave,visualblocks",

            // Theme options
            theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,styleselect,formatselect,fontselect,fontsizeselect",
            theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
            theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
            theme_advanced_buttons4: "insertlayer,moveforward,movebackward,absolute,|,styleprops,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,pagebreak,restoredraft,visualblocks",
            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "bottom",
            theme_advanced_resizing: true,

            // Example content CSS (should be your site CSS)
            content_css: "css/content.css",
            content_css: "/style/CustomizeStyle.css",

            // Drop lists for link/image/media/template dialogs
            template_external_list_url: "lists/template_list.js",
            external_link_list_url: "lists/link_list.js",
            external_image_list_url: "lists/image_list.js",
            media_external_list_url: "lists/media_list.js",
            file_browser_callback: "filebrowser",

            // Style formats
            style_formats: [
            { title: 'Bold text', inline: 'b' },
            { title: 'Red text', inline: 'span', styles: { color: '#ff0000'} },
            { title: 'Red header', block: 'h1', styles: { color: '#ff0000'} },
            { title: 'Example 1', inline: 'span', classes: 'example1' },
            { title: 'Example 2', inline: 'span', classes: 'example2' },
            { title: 'Table styles' },
            { title: 'Table row 1', selector: 'tr', classes: 'tablerow1' },
            { title: 'Customize Style' },
            { title: 'title_content', inline: 'span', classes: 'title_content' },
            { title: 'mod_news', inline: 'span', classes: 'mod_news' },
            { title: 'clear', inline: 'span', classes: 'clear' },
            { title: 'divsep', inline: 'span', classes: 'divsep' },
            { title: 'search', inline: 'span', classes: 'search' },
            { title: 'inputbox', inline: 'span', classes: 'inputbox' },
            { title: 'button', inline: 'span', classes: 'button' },
            { title: 'yellow button', inline: 'span', classes: 'yellow-button' },
            { title: 'modtitle', inline: 'span', classes: 'modtitle' },
            { title: 'modcontent', inline: 'span', classes: 'modcontent' },
            { title: 'modwrap-l2', inline: 'span', classes: 'modwrap-l2' },
            { title: 'modwrap-l3', inline: 'span', classes: 'modwrap-l3' },
            { title: 'headline', inline: 'span', classes: 'headline' },
            { title: 'rounded', inline: 'span', classes: 'rounded' },
            { title: 'item', inline: 'span', classes: 'item' },
            { title: 'item-bg', inline: 'span', classes: 'item-bg' },
            { title: 'quote', inline: 'span', classes: 'quote' },
            { title: 'leadingarticles', inline: 'span', classes: 'leadingarticles' },
            { title: 'readmore', inline: 'span', classes: 'readmore' },
            { title: 'mod-menu', inline: 'span', classes: 'mod-menu' },
            { title: 'menu', inline: 'span', classes: 'menu' },
            { title: 'last', inline: 'span', classes: 'last' },
            { title: 'title', inline: 'span', classes: 'title' },
            { title: 'ff_formtitle', inline: 'span', classes: 'ff_formtitle' },
            { title: 'content_outline', inline: 'span', classes: 'content_outline' },
            { title: 'modcontent-l1', inline: 'span', classes: 'modcontent-l1' },
            { title: 'modcontent-l2', inline: 'span', classes: 'modcontent-l2' },
            { title: 'modcontent-l3', inline: 'span', classes: 'modcontent-l3' },
            { title: 'jwts_tabbernav', inline: 'span', classes: 'jwts_tabbernav' },
            { title: 'jwts_tabbernav', inline: 'span', classes: 'jwts_tabbernav' },
            { title: 'jobs-left', inline: 'span', classes: 'jobs-left' },
            { title: 'jobs-right', inline: 'span', classes: 'jobs-right' },
            { title: 'results', inline: 'span', classes: 'results' },
            { title: 'modwrap-l1_adv1', inline: 'span', classes: 'modwrap-l1_adv1' },
            { title: 'modwrap-l1_adv2', inline: 'span', classes: 'modwrap-l1_adv2' },
            { title: 'supportname', inline: 'span', classes: 'supportname' },
            { title: 'cck_field', inline: 'span', classes: 'cck_field' },
            { title: 'color-button', inline: 'span', classes: 'color-button' },
            { title: 'modlink', inline: 'span', classes: 'modlink' },
            { title: 'contentheading', inline: 'span', classes: 'contentheading' },
            { title: 'contentpagetitle', inline: 'span', classes: 'contentpagetitle' },
            { title: 'plainrows', inline: 'span', classes: 'plainrows' },
            { title: 'odd', inline: 'span', classes: 'odd' },
            { title: 'colorstripes', inline: 'span', classes: 'colorstripes' },
            { title: 'greystripes', inline: 'span', classes: 'greystripes' },
            { title: 'main', inline: 'span', classes: 'main' },
            { title: 'design1', inline: 'span', classes: 'design1' },
            { title: 'jazin-full', inline: 'span', classes: 'jazin-full' },
            { title: 'jazin-boxwrap', inline: 'span', classes: 'jazin-boxwrap' },
            { title: 'jazin-box', inline: 'span', classes: 'jazin-box' },
            { title: 'jazin-section', inline: 'span', classes: 'jazin-section' },
            { title: 'jazin-content', inline: 'span', classes: 'jazin-content' },
            { title: 'jazin-links', inline: 'span', classes: 'jazin-links' }
        ],

            // Replace values for the template plugin
            template_replace_values: {
                username: "Some User",
                staffid: "991234"
            }
        });
        function filebrowser(field_name, url, type, win) {

            fileBrowserURL = "/cpanel/FileManager/Default.aspx?sessionid=<%= Session.SessionID.ToString() %>";

            tinyMCE.activeEditor.windowManager.open({
                title: "Ajax File Manager",
                url: fileBrowserURL,
                width: 950,
                height: 650,
                inline: 0,
                maximizable: 1,
                close_previous: 0
            }, {
                window: win,
                input: field_name,
                sessionid: '<%= Session.SessionID.ToString() %>'
            }
);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="icon_function_parent">
        <%--        <div class="icon_function_Child">
            <asp:LinkButton ID="lbtHelp" runat="server">
                <img src="../Images/ICON_Help.jpg" width="30" height="30" style="border: 0px" /><div>
                    Trợ giúp</div>
            </asp:LinkButton>
        </div>--%>
        <div class="icon_function_Child">
            <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click"><img src="../Images/ICON_SAVE.png" width="30" height="30" style="border: 0px" /><div>
                    Save</div></asp:LinkButton>
        </div>
        <div class="icon_function_Child">
            <a href="config_footer.aspx">
                <img src="../Images/ICON_UPDATE.jpg" width="30" height="30" style="border: 0px" /><div>
                    Refesh</div>
            </a>
        </div>
    </div>
    <!--icon_function_parent-->
    <div id="field">
        <table width="auto" border="0">
            <tr>
                <td>
                    <textarea id="mrk" cols="20" rows="10" class="mrk" style="height: 500px;" runat="server"></textarea>
                </td>
            </tr>
        </table>
    </div>
    <div id="field" style="display:none;">
        Tiếng anh
        <table width="auto" border="0">
            <tr>
                <td>
                    <textarea id="mrk1" cols="20" rows="10" class="mrk" style="height: 500px;" runat="server"></textarea>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
