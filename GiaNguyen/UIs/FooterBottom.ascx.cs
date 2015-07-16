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
    public partial class FooterBottom : System.Web.UI.UserControl
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
                Rpmenu.DataSource = per.LoadmenuFooter(1, 8);
                Rpmenu.DataBind();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
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
        #endregion
    }
}