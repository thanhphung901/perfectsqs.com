using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace vpro.eshop.cpanel
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        eshopdbDataContext db = new eshopdbDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
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
            
            var _configs = db.GetTable<ESHOP_CONFIG>().OrderBy(c => c.CONFIG_ID).Take(1);

            if (_configs.ToList().Count > 0)
            {
                if (!string.IsNullOrEmpty(_configs.ToList()[0].CONFIG_FAVICON))
                    ltrFavicon.Text = "<link rel='shortcut icon' href='" + PathFiles.GetPathConfigs() + _configs.ToList()[0].CONFIG_FAVICON + "' />";
            }
        }
    }
}
