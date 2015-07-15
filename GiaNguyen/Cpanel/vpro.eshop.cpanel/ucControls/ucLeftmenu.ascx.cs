using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace vpro.eshop.cpanel.ucControls
{
    public partial class ucLeftmenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["USER_NAME"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_USER_NAME"]);

            lblUser.Text = Utils.CStrDef(Session["USER_NAME"]);
        }
    }
}