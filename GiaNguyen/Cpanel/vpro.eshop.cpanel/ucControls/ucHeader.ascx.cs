using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace vpro.eshop.cpanel.ucControls
{
    public partial class ucHeader : System.Web.UI.UserControl
    {

        #region Declare

        eshopdbDataContext DB = new eshopdbDataContext();
        int itemCount = 0;

        private static string _HeaderLevel1;
        private static string _HeaderLevel1_Url;
        private static string _HeaderLevel2;
        private static string _HeaderLevel2_Url;


        #endregion

        #region Properties

        public static string HeaderLevel1
        {
            get { return _HeaderLevel1; }
            set { _HeaderLevel1 = value; }
        }

        public static string HeaderLevel1_Url
        {
            get { return _HeaderLevel1_Url; }
            set { _HeaderLevel1_Url = value; }
        }

        public static string HeaderLevel2
        {
            get { return _HeaderLevel2; }
            set { _HeaderLevel2= value; }
        }

        public static string HeaderLevel2_Url
        {
            get { return _HeaderLevel2_Url; }
            set { _HeaderLevel2_Url = value; }
        }

        #endregion

        #region Form Event

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["USER_ID"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_USER_ID"]);
            Session["USER_UN"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_USER_UN"]);
            Session["USER_NAME"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_USER_NAME"]);
            Session["GROUP_ID"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_GROUP_ID"]);
            Session["GROUP_TYPE"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_GROUP_TYPE"]);
            
            lblUser.Text = Utils.CStrDef(Session["USER_NAME"]);
            if (Utils.CIntDef(Session["USER_ID"]) == 0)
                Response.Redirect("../page/Logout.aspx");
            string s = Request.RawUrl;
            lb1.Text = s.Contains("default") ? "" : " > ";
            if (!IsPostBack)
            {
                
                if (s.Contains("default"))
                {
                    hplHeader1.InnerText = "";
                    //  hplHeader1.HRef = HeaderLevel1_Url;
                    hplHeader2.InnerText = "";
                    // hplHeader2.HRef = HeaderLevel2_Url;
                }
                else
                {
                    lblHeader.Text = HeaderLevel1;
                    hplHeader1.InnerText = HeaderLevel1;
                    hplHeader1.HRef = HeaderLevel1_Url;
                    hplHeader2.InnerText = HeaderLevel2;
                    hplHeader2.HRef = HeaderLevel2_Url;
                }                           
            }
        }

        #endregion

        #region My Function

        public string GetHeader()
        {
            if (itemCount > 0)
            {
                return "<ul class=\"sub\">";
            }
            return "";
        }

        public string GetFooter()
        {
            if (itemCount > 0)
            {
                return "</ul>";
            }
            return "";
        }

        #endregion
    }
}