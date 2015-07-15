using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

using System.Data;
using System.Web.UI.HtmlControls;
using vpro.eshop.cpanel.ucControls;

//Create by lucnv 21-12-2012

namespace vpro.eshop.cpanel.page
{
    public partial class news_comment : System.Web.UI.Page
    {

        #region Declare

        int _count = 0;
        int m_news_id = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region properties

        public SortDirection sortProperty
        {
            get
            {
                if (ViewState["SortingState"] == null)
                {
                    ViewState["SortingState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["SortingState"];
            }
            set
            {
                ViewState["SortingState"] = value;
            }
        }

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            m_news_id= Utils.CIntDef(Request["news_id"]);

            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "News - Products";
                ucHeader.HeaderLevel1_Url = "../page/news_list.aspx";
                ucHeader.HeaderLevel2 = "Feedback";
                ucHeader.HeaderLevel2_Url = "../page/news_comment.aspx?news_id=" + m_news_id;

                hplBack.HRef = "../page/news.aspx?news_id=" + m_news_id;

                SearchResult();

                txtKeyword.Attributes.Add("onKeyPress", Common.getSubmitScript(lbtSearch.ClientID));
            }
            hplCatNews.HRef = "news_category.aspx?news_id=" + m_news_id;
            hplEditorHTMl.HRef = "news_editor.aspx?news_id=" + m_news_id;
            hplNewsAtt.HRef = "news_attachment.aspx?news_id=" + m_news_id;
            hplAlbum.HRef = "news_images.aspx?news_id=" + m_news_id;
            bplNewsCopy.HRef = "news_copy.aspx?news_id=" + m_news_id;
            hplComment.HRef = "news_comment.aspx?news_id=" + m_news_id;
            //hplCatProducts.HRef = "news_news.aspx?news_id=" + m_news_id;


        }

        #endregion

        #region My Functions

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        private void SearchResult()
        {
            try
            {
                string keyword = txtKeyword.Value;

                var AllList = (from g in DB.ESHOP_NEWS_COMMENTs
                               where ("" == keyword || (g.COMMENT_NAME).Contains(keyword) || (g.COMMENT_EMAIL).Contains(keyword) || (g.COMMENT_CONTENT).Contains(keyword))
                               && g.NEWS_ID==m_news_id
                               orderby g.COMMENT_PUBLISHDATE descending
                               select g);

                var _vNewsComment = DB.GetTable<ESHOP_NEWS_COMMENT>().Where(a => a.NEWS_ID == m_news_id);
                foreach (var item in _vNewsComment)
                {
                    item.COMMENT_CHECK = 1;
                    DB.SubmitChanges();
                }

                if (AllList.ToList().Count > 0)
                    Session["CommentList"] = DataUtil.LINQToDataTable(AllList);

                GridItemList.DataSource = AllList;
                if (AllList.ToList().Count > GridItemList.PageSize)
                    GridItemList.AllowPaging = true;
                else
                    GridItemList.AllowPaging = false;
                GridItemList.DataBind();


            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void EventDelete(DataGridCommandEventArgs e)
        {
            try
            {
                int UnitId = Utils.CIntDef(GridItemList.DataKeys[e.Item.ItemIndex]);

                var g_delete = DB.GetTable<ESHOP_NEWS_COMMENT>().Where(g => g.COMMENT_ID == UnitId);

                DB.ESHOP_NEWS_COMMENTs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("news_comment.aspx?news_id=" + m_news_id);
            }
        }

        public bool GetCheck(object Obj_Status)
        {
            return Utils.CIntDef(Obj_Status)==1 ? true : false;
        }

        #endregion

        #region Button Envents

        protected void lbtSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            int i = 0;
            int j = 0;
            HtmlInputCheckBox check = new HtmlInputCheckBox();
            int[] items = new int[GridItemList.Items.Count];

            try
            {
                foreach (DataGridItem item in GridItemList.Items)
                {
                    check = new HtmlInputCheckBox();
                    check = (HtmlInputCheckBox)item.Cells[1].FindControl("chkSelect");

                    if (check.Checked)
                    {
                        items[j] = Utils.CIntDef(GridItemList.DataKeys[i]);
                        j++;
                    }

                    i++;
                }

                //delete 
                var g_delete = DB.GetTable<ESHOP_NEWS_COMMENT>().Where(g => items.Contains(g.COMMENT_ID));

                DB.ESHOP_NEWS_COMMENTs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                items = null;
                SearchResult();
            }

        }

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            HtmlInputCheckBox check = new HtmlInputCheckBox();
            string strLink = "";
            int i = 0;
            int j = 0;
            int k = 0;

            int[] items_check = new int[GridItemList.Items.Count];
            int[] items_uncheck = new int[GridItemList.Items.Count];

            try
            {
                foreach (DataGridItem item in GridItemList.Items)
                {
                    check = new HtmlInputCheckBox();
                    check = (HtmlInputCheckBox)item.Cells[1].FindControl("chkDisplay");

                    if (check.Checked)
                    {
                        items_check[j] = Utils.CIntDef(GridItemList.DataKeys[i]);
                        j++;
                    }
                    else
                    {
                        items_uncheck[k] = Utils.CIntDef(GridItemList.DataKeys[i]);
                        k++;
                    }
                    i++;
                }

                //update check 
                var update_check = DB.GetTable<ESHOP_NEWS_COMMENT>().Where(g => items_check.Contains(g.COMMENT_ID));
                if (update_check.ToList().Count>0)
                    update_check.ToList().ForEach(em => em.COMMENT_STATUS = 1);

                //update uncheck 
                var update_uncheck = DB.GetTable<ESHOP_NEWS_COMMENT>().Where(g => items_uncheck.Contains(g.COMMENT_ID));
                if (update_uncheck.ToList().Count > 0)
                    update_uncheck.ToList().ForEach(em => em.COMMENT_STATUS = 0);

                DB.SubmitChanges();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                if (!string.IsNullOrEmpty(strLink))
                    Response.Redirect(strLink);
            }

        }

        #endregion

        #region Grid Events

        protected void GridItemList_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (sortProperty == SortDirection.Ascending)
            {
                sortProperty = SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                sortProperty = SortDirection.Ascending;
                sortingDirection = "Asc";
            }

            DataTable dataTable = Session["CommentList"] as DataTable;
            DataView sortedView = new DataView(dataTable);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            GridItemList.DataSource = sortedView;
            GridItemList.DataBind();
        }

        protected void GridItemList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            GridItemList.CurrentPageIndex = e.NewPageIndex;
            _count = (Utils.CIntDef(GridItemList.CurrentPageIndex, 0) * GridItemList.PageSize);
            GridItemList.DataSource = Session["CommentList"] as DataTable;
            GridItemList.DataBind();
        }

        protected void GridItemList_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (((LinkButton)e.CommandSource).CommandName == "Delete")
            {
                EventDelete(e);
            }
        }

        protected void GridItemList_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((((e.Item.ItemType == ListItemType.Item) | (e.Item.ItemType == ListItemType.AlternatingItem)) | (e.Item.ItemType == ListItemType.SelectedItem)))
            {
                e.Item.Cells[6].Attributes.Add("onClick", "return confirm('Do you want to delete?');");
            }

        }

        #endregion

    }
}