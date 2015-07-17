using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controller;
using vpro.functions;

namespace perfectsqs.com.UIs
{
    public partial class Intro : System.Web.UI.UserControl
    {
        News_details ndetail = new News_details();
        Product_Details pro_detail = new Product_Details();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { Show_File_HTML(); }
        }

        private void Show_File_HTML()
        {
            try
            {
                string _sNews_Seo_Url = "";
                string _sCat_Seo_Url = "introduce";
                if (!string.IsNullOrEmpty(_sCat_Seo_Url))
                {
                    _sNews_Seo_Url = ndetail.Get_News_seo_url(_sCat_Seo_Url);
                }
                liHtml.Text = ndetail.Showfilehtm(_sCat_Seo_Url, _sNews_Seo_Url);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
    }
}