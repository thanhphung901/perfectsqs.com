using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace vpro.eshop.cpanel
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("page/login.aspx");
            //global::System.Configuration.ConfigurationManager.ConnectionStrings["vpro_eshopConnectionString"].ConnectionString, mappingSource
        }
    }
}
