using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace perfectsqs.com.vi_vn
{
    public partial class Regis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHead header = base.Header;
            header.Title = "Đăng ký giao diện";
        }
    }
}