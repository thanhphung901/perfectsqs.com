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
    public partial class units : System.Web.UI.Page
    {

        #region Declare

        private int m_unit_id = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_unit_id = Utils.CIntDef(Request["unit_id"]);

            if (m_unit_id == 0)
            {
                dvDelete.Visible = false;
            }

            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "Product - New";
                ucHeader.HeaderLevel1_Url = "../page/news_list.aspx";
                ucHeader.HeaderLevel2 = "Add new/Update tiền tệ";
                ucHeader.HeaderLevel2_Url = "../page/units.aspx";

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
            SaveInfo("units.aspx");
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
                var G_info = (from g in DB.ESHOP_UNITs
                              where g.UNIT_ID == m_unit_id
                              select g
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtName.Value = G_info.ToList()[0].UNIT_NAME;
                    txtDesc.Value = G_info.ToList()[0].UNIT_DESC;
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
                string Name = txtName.Value;
                string Desc = txtDesc.Value;

                if (m_unit_id == 0)
                {
                    //insert
                    ESHOP_UNIT g_insert = new ESHOP_UNIT();
                    g_insert.UNIT_NAME= Name;
                    g_insert.UNIT_DESC = Desc;

                    DB.ESHOP_UNITs.InsertOnSubmit(g_insert);

                    DB.SubmitChanges();

                    strLink = string.IsNullOrEmpty(strLink) ? "units_list.aspx" : strLink;
                }
                else
                {
                    //update
                    var g_update = DB.GetTable<ESHOP_UNIT>().Where(g => g.UNIT_ID == m_unit_id);

                    if (g_update.ToList().Count > 0)
                    {
                        g_update.Single().UNIT_NAME = Name;
                        g_update.Single().UNIT_DESC = Desc;

                        DB.SubmitChanges();

                        strLink = string.IsNullOrEmpty(strLink) ? "units_list.aspx" : strLink;
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
                var G_info = DB.GetTable<ESHOP_UNIT>().Where(g => g.UNIT_ID == m_unit_id);

                DB.ESHOP_UNITs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                Response.Redirect("units_list.aspx");

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        #endregion

    }
}