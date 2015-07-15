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

namespace vpro.eshop.cpanel.page
{
    public partial class websitelink : System.Web.UI.Page
    {
        #region Declare

        private int m_website_id = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_website_id = Utils.CIntDef(Request["website_id"]);

            if (m_website_id == 0)
            {
                dvDelete.Visible = false;
            }

            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "Website Link";
                ucHeader.HeaderLevel1_Url = "../page/websitelink_list.aspx";
                ucHeader.HeaderLevel2 = "Add new/Update";
                ucHeader.HeaderLevel2_Url = "../page/websitelink.aspx";

                getInfo();
            }

        }

        #endregion

        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }

        protected void lbtSaveNew_Click(object sender, EventArgs e)
        {
            SaveInfo("websitelink.aspx");
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
        }

        #endregion

        #region My Functions

        private void getInfo()
        {
            try
            {

                Components.CpanelUtils.createItemLanguage(ref rblLanguage);
                Components.CpanelUtils.createItemTarget(ref ddlTarget);

                var G_info = (from g in DB.ESHOP_WEBLINKs
                              where g.WEBSITE_LINKS_ID == m_website_id
                              select g
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtName.Value = G_info.ToList()[0].WEBSITE_LINKS_NAME;
                    txtUrl.Value = G_info.ToList()[0].WEBSITE_LINKS_URL;
                    ddlTarget.SelectedValue = G_info.ToList()[0].WEBSITE_LINKS_TARGET;
                    txtOrder.Value = Utils.CStrDef(G_info.ToList()[0].WEBSITE_LINKS_ORDER);
                    rblLanguage.SelectedValue = Utils.CStrDef(G_info.ToList()[0].WEBSITE_LINKS_LANGUAGE);
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
                if (m_website_id == 0)
                {
                    //insert
                    ESHOP_WEBLINK g_insert = new ESHOP_WEBLINK();
                    g_insert.WEBSITE_LINKS_NAME = txtName.Value;
                    g_insert.WEBSITE_LINKS_URL = txtUrl.Value;
                    g_insert.WEBSITE_LINKS_TARGET = ddlTarget.SelectedValue;
                    g_insert.WEBSITE_LINKS_ORDER = Utils.CIntDef(txtOrder.Value);
                    g_insert.WEBSITE_LINKS_LANGUAGE = Utils.CIntDef(rblLanguage.SelectedValue);

                    DB.ESHOP_WEBLINKs.InsertOnSubmit(g_insert);

                    DB.SubmitChanges();

                    strLink = string.IsNullOrEmpty(strLink) ? "websitelink_list.aspx" : strLink;
                }
                else
                {
                    //update
                    var g_update = DB.GetTable<ESHOP_WEBLINK>().Where(g => g.WEBSITE_LINKS_ID == m_website_id);

                    if (g_update.ToList().Count > 0)
                    {
                        g_update.Single().WEBSITE_LINKS_NAME = txtName.Value;
                        g_update.Single().WEBSITE_LINKS_URL = txtUrl.Value;
                        g_update.Single().WEBSITE_LINKS_TARGET = ddlTarget.SelectedValue;
                        g_update.Single().WEBSITE_LINKS_ORDER = Utils.CIntDef(txtOrder.Value);
                        g_update.Single().WEBSITE_LINKS_LANGUAGE = Utils.CIntDef(rblLanguage.SelectedValue);

                        DB.SubmitChanges();

                        strLink = string.IsNullOrEmpty(strLink) ? "websitelink_list.aspx" : strLink;
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

        private void DeleteInfo()
        {
            try
            {
                var G_info = DB.GetTable<ESHOP_WEBLINK>().Where(g => g.WEBSITE_LINKS_ID == m_website_id);

                DB.ESHOP_WEBLINKs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                Response.Redirect("websitelink_list.aspx");

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        #endregion
    }
}