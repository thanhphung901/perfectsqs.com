using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.eshop.cpanel.ucControls;
using vpro.functions;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
namespace vpro.eshop.cpanel.page
{
    public partial class phieuthu : System.Web.UI.Page
    {
        #region Declare
        private int m_ad_id = 0;
        int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            m_ad_id = Utils.CIntDef(Request.QueryString["ad_id"]);
            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "Phiếu thu";
                ucHeader.HeaderLevel1_Url = "../page/list-phieuthu.aspx";
                ucHeader.HeaderLevel2 = "Thêm mới / Cập nhật";
                ucHeader.HeaderLevel2_Url = "../page/phieuthu.aspx";

                getInfo();
            }
        }
        #region My Functions

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        private void getInfo()
        {
            try
            {


                var G_info = (from g in DB.PHIEUTHUs
                              where g.PHIEUTHU_ID == m_ad_id
                              select g
                            );

                if (G_info.ToList().Count > 0)
                {
                    Txtname.Value = G_info.ToList()[0].PHIEUTHU_NAMEHV;
                    Txtphone.Value = G_info.ToList()[0].PHIEUTHU_PHONEHV;
                    Txtnameschool.Value = G_info.ToList()[0].PHIEUTHU_NAMESCHOOL;
                    Txtnganh.Value = G_info.ToList()[0].PHIEUTHU_NGANH;
                    txtDesc.Value = G_info.ToList()[0].PHIEUTHU_REASON;
                    Txtprice.Value = G_info.ToList()[0].PHIEUTHU_PRICE.ToString();
                    Txtpricechar.Value = G_info.ToList()[0].PHIEUTHU_PRICENAME;
                    Rdactive.SelectedValue = G_info.ToList()[0].PHIEUTHU_STATUS.ToString();
                    Txtnamecreateph.Value = G_info.ToList()[0].PHIEUTHU_NAMECREATE;
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



                if (m_ad_id == 0)
                {
                    //insert
                    PHIEUTHU g_insert = new PHIEUTHU();

                    g_insert.PHIEUTHU_NAMEHV = Txtname.Value;
                    g_insert.PHIEUTHU_PHONEHV = Txtphone.Value;
                    g_insert.PHIEUTHU_NAMESCHOOL = Txtnameschool.Value;
                    g_insert.PHIEUTHU_NGANH = Txtnganh.Value;
                    g_insert.PHIEUTHU_REASON = txtDesc.Value;
                    g_insert.PHIEUTHU_PRICE = Utils.CDecDef(Txtprice.Value);
                    g_insert.PHIEUTHU_PRICENAME = Txtpricechar.Value;
                    g_insert.PHIEUTHU_STATUS = Utils.CIntDef(Rdactive.SelectedValue);
                    g_insert.PHIEUTHU_DATE = DateTime.Now;
                    g_insert.PHIEUTHU_NAMECREATE = Txtnamecreateph.Value;
                    DB.PHIEUTHUs.InsertOnSubmit(g_insert);

                    DB.SubmitChanges();

                    //get new id
                    var _new = DB.GetTable<PHIEUTHU>().OrderByDescending(g => g.PHIEUTHU_ID).Take(1);
                    m_ad_id = _new.Single().PHIEUTHU_ID;

                    strLink = string.IsNullOrEmpty(strLink) ? "list-phieuthu.aspx?m_ad_id=" + m_ad_id : strLink;
                }
                else
                {
                    //update
                    var g_update = DB.GetTable<PHIEUTHU>().Where(g => g.PHIEUTHU_ID == m_ad_id);

                    if (g_update.ToList().Count > 0)
                    {
                        g_update.First().PHIEUTHU_NAMEHV = Txtname.Value;
                        g_update.First().PHIEUTHU_PHONEHV = Txtphone.Value;
                        g_update.First().PHIEUTHU_NAMESCHOOL = Txtnameschool.Value;
                        g_update.First().PHIEUTHU_NGANH = Txtnganh.Value;
                        g_update.First().PHIEUTHU_REASON = txtDesc.Value;
                        g_update.First().PHIEUTHU_PRICE = Utils.CDecDef(Txtprice.Value);
                        g_update.First().PHIEUTHU_PRICENAME = Txtpricechar.Value;
                        g_update.First().PHIEUTHU_STATUS = Utils.CIntDef(Rdactive.SelectedValue);
                        g_update.First().PHIEUTHU_DATE = DateTime.Now;
                        g_update.First().PHIEUTHU_NAMECREATE = Txtnamecreateph.Value;
                        DB.SubmitChanges();

                        strLink = string.IsNullOrEmpty(strLink) ? "list-phieuthu.aspx" : strLink;
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

        private void Delete()
        {
            try
            {
                var list = DB.PHIEUTHUs.Where(n => n.PHIEUTHU_ID == m_ad_id);
                DB.PHIEUTHUs.DeleteAllOnSubmit(list);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion
        #region button
        protected void lbtSaveNew_Click(object sender, EventArgs e)
        {
            SaveInfo("phieuthu.aspx");
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            Delete();
            Response.Redirect("list-phieuthu.aspx");
        }

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }
        #endregion
    }
}