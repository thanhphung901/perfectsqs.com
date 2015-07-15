using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;


namespace vpro.eshop.cpanel.ucControls
{
    public partial class ucNewsInfo : System.Web.UI.UserControl
    {

        #region Declare

        private int m_news_id = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            m_news_id = Utils.CIntDef(Request["news_id"]);
            if (!IsPostBack)
                LoadInfo();
        }

        private void LoadInfo()
        {
            try
            {
                var News = DB.GetTable<ESHOP_NEW>().Where(n=>n.NEWS_ID==m_news_id);

                if (News.ToList().Count > 0)
                {
                    lblname.Text = News.ToList()[0].NEWS_TITLE;
                    lblPublishDate.Text = string.Format("{0:dd-MM-yyyy}", News.ToList()[0].NEWS_PUBLISHDATE);
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
    }
}