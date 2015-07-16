using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using Controller;

namespace perfectsqs.com.UIs
{
    public partial class FooterTop : System.Web.UI.UserControl
    {
        #region Declare
        Config cf = new Config();
        Function fun = new Function();
        Home index = new Home();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Loadlist_news();
        }

        public void Loadlist_news()
        {
            var list = index.Loadindex(0, 1, 3);
            int count = 0;
            string str = "";
            count = list.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    string img = GetImageT(list[i].NEWS_ID, list[i].NEWS_IMAGE3);
                    string link = GetLink(list[i].NEWS_URL, list[i].NEWS_SEO_URL, list[i].CAT_SEO_URL);
                    str += String.Format(@"<div class='itemCate col4 col-md-4 col-sm-12'>
                        <div class='category-inner-box'>
                            <h1>{0}</h1>{1}<p>{2}</p>
                            <a class='more-btn' href='{3}'></a><span class='check'></span></div></div>"
                        , list[i].NEWS_TITLE, img, list[i].NEWS_DESC, link);
                }
            }
            lblLoadNews.Text = str;
        }
        public string GetLink(object News_Url, object News_Seo_Url, object cat_seo)
        {
            try
            {
                return fun.Getlink_News(News_Url, News_Seo_Url, cat_seo);
            }
            catch (Exception ex)
            {
                vpro.functions.clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public string GetImageT(object News_Id, object News_Image1)
        {
            try
            {
                return fun.GetImageT_News_Hasclass(News_Id, News_Image1, "");
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
    }
}