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
    public partial class MenuTop : System.Web.UI.UserControl
    {
        #region Declare
        Propertity per = new Propertity();
        Function fun = new Function();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Load_Menu1();
            }
        }

        #region Load data

        protected void Load_Menu1()
        {
            try
            {
                Rpmenu.DataSource = per.Loadmenu(0, 10, 1);
                Rpmenu.DataBind();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }

        }
        protected IQueryable Load_Menu2(object cat_parent_id)
        {
            try
            {
                var list = per.Menu2(cat_parent_id);
                return list;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }

        }
        #endregion

        #region Function
        public string GetLink(object Cat_Url, object Cat_Seo_Url, object Cat_Type)
        {
            try
            {
                string temp = fun.Getlink_Cat(Cat_Url, Cat_Seo_Url);
                return temp;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetStyleActive(object Cat_Seo_Url, object Cat_Url)
        {
            try
            {
                string _curl = Utils.CStrDef(Request.QueryString["curl"]);
                string _seoUrl = Utils.CStrDef(Request.QueryString["purl"]);
                return per.GetStyleActive(Cat_Seo_Url, Cat_Url);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        #endregion
    }
}