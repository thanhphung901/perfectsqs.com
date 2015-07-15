using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using vpro.functions;
using System.Data;
using vpro.eshop.cpanel.ucControls;
using System.IO;

namespace vpro.eshop.cpanel.page
{
    public partial class news_copy : System.Web.UI.Page
    {

        #region Declare

        private int m_news_id = 0;
        //int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_news_id = Utils.CIntDef(Request["news_id"]);
            hplBack.HRef = "news.aspx?news_id=" + m_news_id;

            if (m_news_id == 0)
            {
                trImage1.Visible = false;
            }

            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "Tin tức - Sản phẩm";
                ucHeader.HeaderLevel1_Url = "../page/news_list.aspx";
                ucHeader.HeaderLevel2 = "Sao chép Tin tức - Sản phẩm";
                ucHeader.HeaderLevel2_Url = "../page/news_copy.aspx";

                getInfo();
            }

        }

        #endregion

        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            if (CheckExitsLink(txtSeoUrl.Value))
                lblError.Text = "Đã tồn tại Seo Url, vui lòng nhập Seo Url khác cho tin.";
            else
                SaveInfo();
        }

        #endregion

        #region My functions

        private void LoadCategoryParent()
        {
            try
            {
                var CatList = (
                                from t2 in DB.ESHOP_CATEGORies
                                where t2.CAT_RANK > 0
                                select new
                                {
                                    CAT_ID = t2.CAT_NAME == "------- Root -------" ? 0 : t2.CAT_ID,
                                    CAT_NAME = (string.IsNullOrEmpty(t2.CAT_CODE) ? t2.CAT_NAME : t2.CAT_NAME + "(" + t2.CAT_CODE + ")"),
                                    CAT_PARENT_ID = t2.CAT_PARENT_ID,
                                    CAT_RANK = t2.CAT_RANK
                                }
                            );

                if (CatList.ToList().Count > 0)
                {
                    DataRelation relCat;
                    DataTable tbl = DataUtil.LINQToDataTable(CatList);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(tbl);

                    tbl.PrimaryKey = new DataColumn[] { tbl.Columns["CAT_ID"] };
                    relCat = new DataRelation("Category_parent", ds.Tables[0].Columns["CAT_ID"], ds.Tables[0].Columns["CAT_PARENT_ID"], false);

                    ds.Relations.Add(relCat);
                    DataSet dsCat = ds.Clone();
                    DataTable CatTable = ds.Tables[0];

                    DataUtil.TransformTableWithSpace(ref CatTable, dsCat.Tables[0], relCat, null);

                    ddlCategory.DataSource = CatTable;
                    ddlCategory.DataTextField = "CAT_NAME";
                    ddlCategory.DataValueField = "CAT_ID";
                    ddlCategory.DataBind();
                }
                else
                {
                    DataTable dt = new DataTable("Newtable");

                    dt.Columns.Add(new DataColumn("CAT_ID"));
                    dt.Columns.Add(new DataColumn("CAT_NAME"));

                    DataRow row = dt.NewRow();
                    row["CAT_ID"] = 0;
                    row["CAT_NAME"] = "--------Root--------";
                    dt.Rows.Add(row);

                    ddlCategory.DataTextField = "CAT_NAME";
                    ddlCategory.DataValueField = "CAT_ID";
                    ddlCategory.DataSource = dt;
                    ddlCategory.DataBind();

                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void LoadUnits()
        {
            try
            {
                var units = DB.GetTable<ESHOP_UNIT>();

                ddlUnit1.DataSource = units;
                ddlUnit1.DataTextField = "UNIT_NAME";
                ddlUnit1.DataValueField = "UNIT_ID";
                ddlUnit1.DataBind();

                ddlUnit2.DataSource = units;
                ddlUnit2.DataTextField = "UNIT_NAME";
                ddlUnit2.DataValueField = "UNIT_ID";
                ddlUnit2.DataBind();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void getInfo()
        {
            try
            {
                LoadUnits();
                LoadCategoryParent();
                Components.CpanelUtils.createItemTarget(ref ddlTarget);
                Components.CpanelUtils.createItemLanguage(ref rblLanguage);

                var G_info = (from n in DB.ESHOP_NEWs
                              join c in DB.ESHOP_NEWS_CATs on n.NEWS_ID equals c.NEWS_ID
                              where n.NEWS_ID == m_news_id
                              select new
                              {
                                  n,
                                  c.CAT_ID
                              }
                            );

                if (G_info.ToList().Count > 0)
                {
                    //trCat.Visible = false;
                    ddlCategory.SelectedValue = Utils.CStrDef(G_info.ToList()[0].CAT_ID);
                    txtTitle.Value = G_info.ToList()[0].n.NEWS_TITLE;
                    txtDesc.Value = G_info.ToList()[0].n.NEWS_DESC;
                    txtUrl.Value = G_info.ToList()[0].n.NEWS_URL;
                    ddlTarget.SelectedValue = G_info.ToList()[0].n.NEWS_TARGET;

                    rblNewsType.SelectedValue = Utils.CStrDef(G_info.ToList()[0].n.NEWS_TYPE);
                    rblStatus.SelectedValue = Utils.CStrDef(G_info.ToList()[0].n.NEWS_SHOWTYPE);
                    rblNewsPeriod.SelectedValue = Utils.CStrDef(G_info.ToList()[0].n.NEWS_PERIOD);
                    rblFeefback.SelectedValue = Utils.CStrDef(G_info.ToList()[0].n.NEWS_FEEDBACKTYPE);
                    rblLanguage.SelectedValue = Utils.CStrDef(G_info.ToList()[0].n.NEWS_LANGUAGE);
                    txtOrder.Value = Utils.CStrDef(G_info.ToList()[0].n.NEWS_ORDER, "1");
                    txtOrderPeriod.Value = Utils.CStrDef(G_info.ToList()[0].n.NEWS_ORDER_PERIOD, "1");
                    lblCount.Text = string.IsNullOrEmpty(Utils.CStrDef(G_info.ToList()[0].n.NEWS_COUNT)) ? "0" : Utils.CStrDef(G_info.ToList()[0].n.NEWS_COUNT);
                    lblSendEmail.Text = G_info.ToList()[0].n.NEWS_SENDDATE == null ? "Chưa gửi" : "Gửi lần cuối vào " + string.Format("{0:dd/MM/yyyy HH:mm:ss}", G_info.ToList()[0].n.NEWS_SENDDATE);

                    //seo
                    txtSeoTitle.Value = Utils.CStrDef(G_info.ToList()[0].n.NEWS_SEO_TITLE);
                    txtSeoKeyword.Value = Utils.CStrDef(G_info.ToList()[0].n.NEWS_SEO_KEYWORD);
                    txtSeoDescription.Value = Utils.CStrDef(G_info.ToList()[0].n.NEWS_SEO_DESC);
                    txtSeoUrl.Value = Utils.CStrDef(G_info.ToList()[0].n.NEWS_SEO_URL);

                    //PRICE
                    txtPrice.Value = Utils.CStrDef(G_info.ToList()[0].n.NEWS_PRICE1, "0");
                    txtPriceSub.Value = Utils.CStrDef(G_info.ToList()[0].n.NEWS_PRICE2, "0");
                    ddlUnit1.SelectedValue = Utils.CStrDef(G_info.ToList()[0].n.UNIT_ID1);
                    ddlUnit2.SelectedValue = Utils.CStrDef(G_info.ToList()[0].n.UNIT_ID2);

                    //image
                    if (!string.IsNullOrEmpty(G_info.ToList()[0].n.NEWS_IMAGE3))
                    {
                        trImage1.Visible = true;
                        Image1.Src = PathFiles.GetPathNews(m_news_id) + G_info.ToList()[0].n.NEWS_IMAGE3;
                        hplImage1.NavigateUrl = PathFiles.GetPathNews(m_news_id) + G_info.ToList()[0].n.NEWS_IMAGE3;
                        hplImage1.Text = G_info.ToList()[0].n.NEWS_IMAGE3;
                    }
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
                if (m_news_id > 0)
                {

                    //insert
                    ESHOP_NEW news_insert = new ESHOP_NEW();

                    news_insert.NEWS_TITLE = txtTitle.Value;
                    news_insert.NEWS_DESC = txtDesc.Value;
                    news_insert.NEWS_URL = txtUrl.Value;
                    news_insert.NEWS_TARGET = ddlCategory.SelectedValue;
                    news_insert.NEWS_SEO_URL = txtSeoUrl.Value;
                    news_insert.NEWS_SEO_TITLE = txtSeoTitle.Value;
                    news_insert.NEWS_SEO_KEYWORD = txtSeoKeyword.Value;
                    news_insert.NEWS_SEO_DESC = txtSeoDescription.Value;

                    news_insert.NEWS_TYPE = Utils.CIntDef(rblNewsType.SelectedValue);
                    news_insert.NEWS_SHOWTYPE = Utils.CIntDef(rblStatus.SelectedValue);
                    news_insert.NEWS_PERIOD = Utils.CIntDef(rblNewsPeriod.SelectedValue);
                    news_insert.NEWS_SHOWINDETAIL = Utils.CIntDef(rblShowDetail.SelectedValue);
                    news_insert.NEWS_FEEDBACKTYPE = Utils.CIntDef(rblFeefback.SelectedValue);
                    news_insert.NEWS_LANGUAGE = Utils.CIntDef(rblLanguage.SelectedValue);

                    news_insert.NEWS_ORDER = Utils.CIntDef(txtOrder.Value);
                    news_insert.NEWS_ORDER_PERIOD = Utils.CIntDef(txtOrderPeriod.Value);
                    news_insert.NEWS_PRICE1 = Utils.CIntDef(txtPrice.Value);
                    news_insert.UNIT_ID1 = Utils.CIntDef(ddlUnit1.SelectedValue);
                    news_insert.NEWS_PRICE2 = Utils.CIntDef(txtPriceSub.Value);
                    news_insert.UNIT_ID2 = Utils.CIntDef(ddlUnit2.SelectedValue);

                    news_insert.USER_ID = Utils.CIntDef(Session["USER_ID"]);
                    news_insert.NEWS_PUBLISHDATE = DateTime.Now;

                    DB.ESHOP_NEWs.InsertOnSubmit(news_insert);
                    DB.SubmitChanges();

                    //update cat news
                    var _new = DB.GetTable<ESHOP_NEW>().OrderByDescending(g => g.NEWS_ID).Take(1);

                    //save catefory
                    SaveNewsCategory(_new.Single().NEWS_ID);

                    if (Utils.CIntDef(rblNewsImage.SelectedValue) == 1 || Utils.CIntDef(rblNewsAlbum.SelectedValue) == 1 || Utils.CIntDef(rblNewsContent.SelectedValue) == 1 || Utils.CIntDef(rblNewsAtt.SelectedValue) == 1)
                        copyAllFiles(_new.Single().NEWS_ID);

                    if (Utils.CIntDef(rblNewsImage.SelectedValue) == 1)
                        copyImage(_new.Single().NEWS_ID);

                    if (Utils.CIntDef(rblNewsAlbum.SelectedValue) == 1)
                        copyAlbums(_new.Single().NEWS_ID);

                    if (Utils.CIntDef(rblNewsContent.SelectedValue) == 1)
                        copyHtml(_new.Single().NEWS_ID);

                    if (Utils.CIntDef(rblNewsAtt.SelectedValue) == 1)
                        copyAtt(_new.Single().NEWS_ID);

                    strLink = string.IsNullOrEmpty(strLink) ? "news.aspx?news_id=" + _new.Single().NEWS_ID : strLink;
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

        private bool CheckExitsLink(string strLink)
        {
            try
            {
                var exits = (from c in DB.ESHOP_NEWs where c.NEWS_SEO_URL == strLink select c);

                if (exits.ToList().Count > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return false;

            }

        }

        private void SaveNewsCategory(int NewsId)
        {
            try
            {
                ESHOP_NEWS_CAT nc = new ESHOP_NEWS_CAT();
                nc.CAT_ID = Utils.CIntDef(ddlCategory.SelectedValue);
                nc.NEWS_ID = NewsId;

                DB.ESHOP_NEWS_CATs.InsertOnSubmit(nc);
                DB.SubmitChanges();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void copyAllFiles(int id)
        {
            try
            {
                string pathCopy = Server.MapPath(PathFiles.GetPathNews(id));
                string pathOld = Server.MapPath(PathFiles.GetPathNews(m_news_id));

                if (!Directory.Exists(pathCopy))
                {
                    Directory.CreateDirectory(pathCopy);
                }

                //copy all file
                {
                    string fileName = "";
                    string destFile = "";

                    string[] files = Directory.GetFiles(pathOld);
                    foreach (string s in files)
                    {
                        fileName = Path.GetFileName(s);
                        destFile = Path.Combine(pathCopy, fileName);
                        File.Copy(s, destFile);
                    }
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void copyImage(int id)
        {
            try
            {
                string pathCopy = Server.MapPath(PathFiles.GetPathNews(id));
                string pathOld = Server.MapPath(PathFiles.GetPathNews(m_news_id));

                if (!Directory.Exists(pathCopy))
                {
                    Directory.CreateDirectory(pathCopy);
                }

                var _oldNews = DB.GetTable<ESHOP_NEW>().Where(n => n.NEWS_ID == m_news_id);

                if (!string.IsNullOrEmpty(_oldNews.ToList()[0].NEWS_IMAGE3))
                {
                    ////copy file
                    //if (File.Exists(pathOld + _oldNews.ToList()[0].NEWS_IMAGE1))
                    //    File.Copy(pathOld + _oldNews.ToList()[0].NEWS_IMAGE1, pathCopy + _oldNews.ToList()[0].NEWS_IMAGE1);

                    //update database
                    var _copyNews = DB.GetTable<ESHOP_NEW>().Where(n => n.NEWS_ID == id);
                    _copyNews.Single().NEWS_IMAGE3 = _oldNews.ToList()[0].NEWS_IMAGE3;

                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void copyHtml(int id)
        {
            try
            {
                var _oldNews = DB.GetTable<ESHOP_NEW>().Where(n => n.NEWS_ID == m_news_id);

                if (!string.IsNullOrEmpty(_oldNews.ToList()[0].NEWS_FILEHTML))
                {
                    var _copyNews = DB.GetTable<ESHOP_NEW>().Where(n => n.NEWS_ID == id);
                    _copyNews.Single().NEWS_FILEHTML = _oldNews.ToList()[0].NEWS_FILEHTML;

                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void copyAlbums(int id)
        {
            try
            {
                var _oldNews = DB.GetTable<ESHOP_NEWS_IMAGE>().Where(n => n.NEWS_ID == m_news_id);

                foreach (var item in _oldNews)
                {
                    ESHOP_NEWS_IMAGE img_insert = new ESHOP_NEWS_IMAGE();

                    img_insert.NEWS_IMG_IMAGE1 = item.NEWS_IMG_IMAGE1;
                    img_insert.NEWS_IMG_IMAGE2 = item.NEWS_IMG_IMAGE2;
                    img_insert.NEWS_IMG_IMAGE3 = item.NEWS_IMG_IMAGE3;
                    img_insert.NEWS_IMG_ORDER = item.NEWS_IMG_ORDER;
                    img_insert.NEWS_IMG_DESC = item.NEWS_IMG_DESC;
                    img_insert.NEWS_IMG_SHOWTYPE = item.NEWS_IMG_SHOWTYPE;
                    img_insert.NEWS_ID = id;

                    DB.ESHOP_NEWS_IMAGEs.InsertOnSubmit(img_insert);
                }

                DB.SubmitChanges();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void copyAtt(int id)
        {
            try
            {
                var _oldNews = DB.GetTable<ESHOP_NEWS_ATT>().Where(n => n.NEWS_ID == m_news_id);

                foreach (var item in _oldNews)
                {
                    ESHOP_NEWS_ATT att_insert = new ESHOP_NEWS_ATT();

                    att_insert.NEWS_ATT_NAME = item.NEWS_ATT_NAME;
                    att_insert.EXT_ID = item.EXT_ID;
                    att_insert.NEWS_ATT_ORDER = item.NEWS_ATT_ORDER;
                    att_insert.NEWS_ATT_URL = item.NEWS_ATT_URL;
                    att_insert.NEWS_ATT_FILE = item.NEWS_ATT_FILE;
                    att_insert.NEWS_ID = id;

                    DB.ESHOP_NEWS_ATTs.InsertOnSubmit(att_insert);
                }

                DB.SubmitChanges();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        #endregion
    }
}