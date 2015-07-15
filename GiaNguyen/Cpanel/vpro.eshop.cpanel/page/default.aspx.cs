using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.IO;
using System.Text;

namespace vpro.eshop.cpanel.page
{
    public partial class _default : System.Web.UI.Page
    {
        eshopdbDataContext db = new eshopdbDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.CIntDef(Session["GROUP_TYPE"], 0) == 2)
            {
                Response.Redirect("news_list.aspx");
            }
            var _configs = db.GetTable<ESHOP_CONFIG>().OrderBy(c => c.CONFIG_ID).Take(1);

            if (_configs.ToList().Count > 0)
            {
                if (!string.IsNullOrEmpty(_configs.ToList()[0].CONFIG_FAVICON))
                    ltrFavicon.Text = "<link rel='shortcut icon' href='" + PathFiles.GetPathConfigs() + _configs.ToList()[0].CONFIG_FAVICON + "' />";
            }
            Session["USER_ID"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_USER_ID"]);
            Session["USER_UN"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_USER_UN"]);
            Session["USER_NAME"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_USER_NAME"]);
            Session["GROUP_ID"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_GROUP_ID"]);
            Session["GROUP_TYPE"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_GROUP_TYPE"]);

            UserControl ucLeftAdmin = Page.LoadControl("../ucControls/ucLeftmenu.ascx") as UserControl;
            UserControl ucLeftEditor = Page.LoadControl("../ucControls/ucLeftmenu_editor.ascx") as UserControl;

            if (Utils.CIntDef(Session["GROUP_TYPE"]) == 1)
                phdLeft.Controls.Add(ucLeftAdmin);
            else if (Utils.CIntDef(Session["GROUP_TYPE"]) == 2)
                phdLeft.Controls.Add(ucLeftEditor);
            else
                phdLeft.Controls.Add(ucLeftAdmin);
            //Response.Redirect("login.aspx");
            //CreateFile();
        }
        private void CreateFile()
        {
            var list = db.ESHOP_NEWs.Where(n => n.NEWS_TYPE == 1);
            foreach (var i in list)
            {
                Create_att(i.NEWS_ID);
            }
        }
        #region Attfile
        private string Chuoiheader_footer(int type)
        {

            string pathFile;
            string strHTMLContent = string.Empty;
            pathFile = type == 1 ? Server.MapPath(PathFiles.GetPathConfigs() + "/header_baogia.htm") : Server.MapPath(PathFiles.GetPathConfigs() + "/footer_baogia.htm");
            if ((File.Exists(pathFile)))
            {
                StreamReader objNewsReader;
                //objNewsReader = New StreamReader(pathFile, System.Text.Encoding.Default)
                objNewsReader = new StreamReader(pathFile);
                strHTMLContent = objNewsReader.ReadToEnd();
                objNewsReader.Close();
            }
            return strHTMLContent;
        }
        private string Gethtml(int m_news_id)
        {
            string pathFile;
            string strHTMLContent = string.Empty;
            if (m_news_id > 0)
            {

                var newsInfo = db.GetTable<ESHOP_NEW>().Where(n => n.NEWS_ID == m_news_id);

                pathFile = Server.MapPath(PathFiles.GetPathNews(m_news_id) + "/" + newsInfo.ToList()[0].NEWS_FILEHTML);

                if ((File.Exists(pathFile)))
                {
                    StreamReader objNewsReader;
                    objNewsReader = new StreamReader(pathFile);
                    strHTMLContent = objNewsReader.ReadToEnd();
                    objNewsReader.Close();
                    StringBuilder strBody = new StringBuilder();
                }
            }
            return strHTMLContent;
        }
        private string Main(int m_news_id)
        {
            string getimgpro = "";
            string namepro = "";
            string price = "";
            string desc = "";
            var newsInfo = db.GetTable<ESHOP_NEW>().Where(n => n.NEWS_ID == m_news_id).ToList();
            if (newsInfo.Count > 0)
            {
                getimgpro = "http://beta.vanmatbich.com/" + PathFiles.GetPathNews(m_news_id) + newsInfo[0].NEWS_IMAGE3;
                namepro = newsInfo[0].NEWS_TITLE;
                price = string.Format("{0:0,0 VNĐ}", newsInfo[0].NEWS_PRICE1);
                desc = newsInfo[0].NEWS_DESC;
            }
            string chuoimain = string.Empty;
            chuoimain += "<table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0 width='100%'";
            chuoimain += "style='width:100.0%;border-collapse:collapse;mso-yfti-tbllook:1184;";
            chuoimain += "mso-padding-alt:3.75pt 3.75pt 3.75pt 3.75pt'>";
            chuoimain += "<tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes'>";
            chuoimain += " <td width=170 style='width:127.5pt;padding:3.75pt 3.75pt 3.75pt 3.75pt'>";
            chuoimain += "<p class=MsoNormal align=center style='text-align:center'><span";
            chuoimain += " class=SpellE><b><span style='font-size:12.0pt'>Tên</span></b></span><b><span";
            chuoimain += " style='font-size:12.0pt'> <span class=SpellE>hàng</span></span></b><span";
            chuoimain += "  style='font-size:12.0pt'><o:p></o:p></span></p>";
            chuoimain += "</td>";
            chuoimain += "<td style='padding:3.75pt 3.75pt 3.75pt 3.75pt'>";
            chuoimain += "<p class=MsoNormal align=center style='text-align:center'><span";
            chuoimain += " class=SpellE><b><span style='font-size:12.0pt'>Thông</span></b></span><b><span";
            chuoimain += " style='font-size:12.0pt'> tin chi <span class=SpellE>tiết</span></span></b><span";
            chuoimain += " style='font-size:12.0pt'><o:p></o:p></span></p>";
            chuoimain += "  </td>";
            chuoimain += " <td width=120 style='width:1.25in;padding:3.75pt 3.75pt 3.75pt 3.75pt'>";
            chuoimain += " <p class=MsoNormal align=center style='text-align:center'><span";
            chuoimain += " class=SpellE><b><span style='font-size:12.0pt'>Đơn</span></b></span><b><span";
            chuoimain += " style='font-size:12.0pt'> <span class=SpellE>giá</span></span></b><span";
            chuoimain += "style='font-size:12.0pt'><o:p></o:p></span></p>";
            chuoimain += "</td>";
            chuoimain += " </tr>";
            chuoimain += "<tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes'>";
            chuoimain += "<td width=180 valign=top style='width:135.0pt;padding:3.75pt 3.75pt 3.75pt 3.75pt'>";
            chuoimain += " <p class=MsoNormal align=center style='text-align:center'><span";
            chuoimain += "style='font-size:10.0pt;font-family:'Times New Roman','serif';mso-fareast-font-family:";
            chuoimain += "'Times New Roman';mso-no-proof:yes'><img width=133 height=150";
            chuoimain += " id='_x0000_i1025'";
            chuoimain += " src='" + getimgpro + "'";
            chuoimain += "  ></span><br/><span";
            chuoimain += "  style='font-size:12.0pt;font-family:'Times New Roman','serif';mso-fareast-font-family:";
            chuoimain += "  'Times New Roman''>" + namepro + "</p>";
            chuoimain += "  </td>";
            chuoimain += "  <td valign=top style='padding:3.75pt 3.75pt 3.75pt 3.75pt'>";
            chuoimain += desc;
            chuoimain += " </td>";
            chuoimain += "<td width=120 valign=top style='width:1.25in;padding:3.75pt 3.75pt 3.75pt 3.75pt'>";

            chuoimain += "" + price + "";
            chuoimain += " </td>";
            chuoimain += "</tr>";
            chuoimain += "</table>";
            return chuoimain;

        }
        private void Create_att(int m_news_id)
        {
            try
            {
                StringBuilder strBody = new StringBuilder();
                string chuoi = "";
                chuoi += "<html ";
                chuoi += "xmlns:o='urn:schemas-microsoft-com:office:office' ";
                chuoi += "xmlns:w='urn:schemas-microsoft-com:office:word'";
                chuoi += "xmlns='http://www.w3.org/TR/REC-html40'>";
                chuoi += "<head><title>Time</title>";
                chuoi += "<style>";
                chuoi += "<!--";
                /* Font Definitions */
                chuoi += "@font-face";
                chuoi += "	{font-family:Wingdings;";
                chuoi += "	panose-1:5 0 0 0 0 0 0 0 0 0;";
                chuoi += "	mso-font-charset:2;";
                chuoi += "	mso-generic-font-family:auto;";
                chuoi += "	mso-font-pitch:variable;";
                chuoi += "mso-font-signature:0 268435456 0 0 -2147483648 0;}";
                chuoi += "@font-face";
                chuoi += "	{font-family:'Cambria Math';";
                chuoi += "	panose-1:2 4 5 3 5 4 6 3 2 4;";
                chuoi += "	mso-font-charset:0;";
                chuoi += "	mso-generic-font-family:roman;";
                chuoi += "	mso-font-pitch:variable;";
                chuoi += "	mso-font-signature:-536870145 1107305727 0 0 415 0;}";
                chuoi += "@font-face";
                chuoi += "	{font-family:Tahoma;";
                chuoi += "	panose-1:2 11 6 4 3 5 4 4 2 4;";
                chuoi += "	mso-font-charset:0;";
                chuoi += "	mso-generic-font-family:swiss;";
                chuoi += "	mso-font-pitch:variable;";
                chuoi += "	mso-font-signature:-520081665 -1073717157 41 0 66047 0;} /* Style Definitions */";
                chuoi += "p.MsoNormal, li.MsoNormal, div.MsoNormal";
                chuoi += "{mso-style-unhide:no;";
                chuoi += "mso-style-qformat:yes;";
                chuoi += "mso-style-parent:'';";
                chuoi += "margin:0in;";
                chuoi += "margin-bottom:.0001pt;";
                chuoi += "mso-pagination:widow-orphan;";
                chuoi += "font-size:7.5pt;";
                chuoi += "mso-bidi-font-size:8.0pt;";
                chuoi += "font-family:'Arial','sans-serif';";
                chuoi += "mso-fareast-font-family:Arial;}";
                chuoi += "p";
                chuoi += "	{mso-style-unhide:no;";
                chuoi += "mso-margin-top-alt:auto;";
                chuoi += "margin-right:0in;";
                chuoi += "mso-margin-bottom-alt:auto;";
                chuoi += "margin-left:0in;";
                chuoi += "mso-pagination:widow-orphan;";
                chuoi += "font-size:12.0pt;";
                chuoi += "font-family:'Times New Roman','serif';";
                chuoi += "mso-fareast-font-family:'Times New Roman';}";
                chuoi += "p.MsoAcetate, li.MsoAcetate, div.MsoAcetate";
                chuoi += "{mso-style-unhide:no;";
                chuoi += "mso-style-link:'Balloon Text Char';";
                chuoi += "margin:0in;";
                chuoi += "margin-bottom:.0001pt;";
                chuoi += "mso-pagination:widow-orphan;";
                chuoi += "font-size:8.0pt;";
                chuoi += "font-family:'Tahoma','sans-serif';";
                chuoi += "mso-fareast-font-family:Arial;}";
                chuoi += "p.small, li.small, div.small";
                chuoi += "{mso-style-name:small;";
                chuoi += "mso-style-unhide:no;";
                chuoi += "mso-style-parent:'';";
                chuoi += "margin:0in;";
                chuoi += "margin-bottom:.0001pt;";
                chuoi += "mso-pagination:widow-orphan;";
                chuoi += "font-size:1.0pt;";
                chuoi += "font-family:'Arial','sans-serif';";
                chuoi += "mso-fareast-font-family:Arial;}";
                chuoi += "span.BalloonTextChar";
                chuoi += "	{mso-style-name:'Balloon Text Char';";
                chuoi += "	mso-style-unhide:no;";
                chuoi += "	mso-style-locked:yes;";
                chuoi += "	mso-style-link:'Balloon Text';";
                chuoi += "	mso-ansi-font-size:8.0pt;";
                chuoi += "	mso-bidi-font-size:8.0pt;";
                chuoi += "	font-family:'Tahoma','sans-serif';";
                chuoi += "	mso-ascii-font-family:Tahoma;";
                chuoi += "	mso-fareast-font-family:Arial;";
                chuoi += "mso-hansi-font-family:Tahoma;";
                chuoi += "	mso-bidi-font-family:Tahoma;}";
                chuoi += "span.SpellE";
                chuoi += "	{mso-style-name:'';";
                chuoi += "	mso-spl-e:yes;}";
                chuoi += ".MsoChpDefault";
                chuoi += "	{mso-style-type:export-only;";
                chuoi += "mso-default-props:yes;";
                chuoi += "font-size:10.0pt;";
                chuoi += "mso-ansi-font-size:10.0pt;";
                chuoi += "mso-bidi-font-size:10.0pt;}";
                chuoi += "@page WordSection1";
                chuoi += "{size:595.3pt 841.9pt;";
                chuoi += "margin:.6in .6in .6in .9in;";
                chuoi += "mso-header-margin:.5in;";
                chuoi += "mso-footer-margin:.5in;";
                chuoi += "mso-paper-source:0;}";
                chuoi += "div.WordSection1";
                chuoi += "	{page:WordSection1;}";
                /* List Definitions */
                chuoi += " @list l0";
                chuoi += "{mso-list-id:1623265660;";
                chuoi += "mso-list-template-ids:-395659252;}";
                chuoi += "@list l0:level1";
                chuoi += "{mso-level-number-format:bullet;";
                chuoi += "mso-level-text:;";
                chuoi += "mso-level-tab-stop:.5in;";
                chuoi += "mso-level-number-position:left;";
                chuoi += "text-indent:-.25in;";
                chuoi += "so-ansi-font-size:10.0pt;";
                chuoi += "font-family:Symbol;}";
                chuoi += "ol";
                chuoi += "{margin-bottom:0in;}";
                chuoi += "ul";
                chuoi += "{margin-bottom:0in;}";
                chuoi += "-->";
                chuoi += "</style>";
                chuoi += "</head>";
                strBody.Append(chuoi);
                strBody.Append(Chuoiheader_footer(1));
                strBody.Append(Main(m_news_id));
                strBody.Append(Chuoiheader_footer(2));
                StreamWriter fsoFile;
                string pathfile = Server.MapPath("/data/news/" + m_news_id);
                if (!Directory.Exists(pathfile))
                {
                    Directory.CreateDirectory(pathfile);
                }
                string path_ms = Server.MapPath(PathFiles.GetPathNews(m_news_id) + "baogia_" + m_news_id + ".doc");
                fsoFile = File.CreateText(path_ms);
                fsoFile.Write(strBody);
                fsoFile.Close();

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}