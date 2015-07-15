using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

using System.Data;
using vpro.eshop.cpanel.ucControls;
using System.Web.UI.HtmlControls;

//Create by Lucnv 23-12-2012

namespace vpro.eshop.cpanel.page
{
    public partial class config_email : System.Web.UI.Page
    {

        #region Declare

        private int m_email_id = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_email_id = Utils.CIntDef(Request["email_id"]);

            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "Cấu hình website";
                ucHeader.HeaderLevel1_Url = "../page/config_meta.aspx";
                ucHeader.HeaderLevel2 = "Cấu hình Email";
                ucHeader.HeaderLevel2_Url = "../page/config_email.aspx";

                getInfo();
            }

        }

        #endregion

        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }

        #endregion

        #region My Functions

        private void getInfo()
        {
            try
            {
                var G_info = (from g in DB.ESHOP_EMAILs
                              where g.EMAIL_ID == m_email_id
                              select g
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtSTT.Value = G_info.ToList()[0].EMAIL_STT.ToString();
                    txtDesc.Value = G_info.ToList()[0].EMAIL_DESC;
                    txtTo.Value = G_info.ToList()[0].EMAIL_TO;
                    txtCc.Value = G_info.ToList()[0].EMAIL_CC;
                    txtBcc.Value = G_info.ToList()[0].EMAIL_BCC;
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void SaveInfo(string strLink = "")
        {
            try
            {

                if (m_email_id > 0)
                {
                    //update
                    var g_update = DB.GetTable<ESHOP_EMAIL>().Where(g => g.EMAIL_ID == m_email_id);

                    if (g_update.ToList().Count > 0)
                    {
                        g_update.Single().EMAIL_TO = txtTo.Value;
                        g_update.Single().EMAIL_CC = txtCc.Value;
                        g_update.Single().EMAIL_BCC = txtBcc.Value;

                        DB.SubmitChanges();

                        strLink = string.IsNullOrEmpty(strLink) ? "config_email_list.aspx" : strLink;
                    }
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                if (!string.IsNullOrEmpty(strLink))
                { Response.Redirect(strLink); }
            }
        }

        #endregion

    }
}