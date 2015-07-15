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
    public partial class news_news : System.Web.UI.Page
    {

        #region Declare

        private int m_news_id = 0;
        int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();
        private int m_news_type = 2;
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

            m_news_id = Utils.CIntDef(Request["news_id"]);
            hplBack.HRef = "news.aspx?news_id=" + m_news_id;
            CheckTypeNews();
            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "Product New";
                ucHeader.HeaderLevel1_Url = "../page/news_list.aspx";
                if (m_news_type == 2)
                {
                    ucHeader.HeaderLevel2 = "Chọn món ăn";
                }
                if (m_news_type == 1)
                {
                    ucHeader.HeaderLevel2 = "Chọn nguyên liệu";
                }
                ucHeader.HeaderLevel2_Url = "../page/news_news.aspx";

                LoadCat();
            }

        }

        #endregion

        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveNewsCat();
        }

        #endregion

        #region My Functions

        private void SaveNewsCat()
        {
            string strLink = "";

            try
            {
                int i = 0;
                HtmlInputCheckBox check = new HtmlInputCheckBox();

                foreach (DataGridItem item in GridItemList.Items)
                {
                    check = new HtmlInputCheckBox();
                    check = (HtmlInputCheckBox)item.Cells[1].FindControl("chkSelect");

                    if (check.Checked)
                    {
                        ESHOP_NEWS_NEW grinsert = new ESHOP_NEWS_NEW();
                        grinsert.NEWS_ID2 = Utils.CIntDef(GridItemList.DataKeys[i]);
                        grinsert.NEWS_ID1 = m_news_id;
                        DB.ESHOP_NEWS_NEWs.InsertOnSubmit(grinsert);
                    }

                    i++;
                }

                DB.SubmitChanges();
                strLink = "news.aspx?news_id=" + m_news_id;
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

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        private void LoadCat()
        {
            try
            {
                if (m_news_type == 2)
                {
                    var AllList = (from g in DB.ESHOP_NEWs
                                   where g.NEWS_TYPE == 1
                                   select new
                                   {
                                       g.NEWS_ID,
                                       g.NEWS_TITLE

                                   });

                    if (AllList.ToList().Count > 0)
                    {

                        Session["NewsNewsList"] = DataUtil.LINQToDataTable(AllList);
                        DataTable tbl = Session["NewsNewsList"] as DataTable;

                        GridItemList.Columns[2].HeaderText = "Món ăn";
                        GridItemList.DataSource = tbl;
                        GridItemList.DataBind();
                    }
                }
                if (m_news_type == 1)
                {
                    var AllList = (from g in DB.ESHOP_NEWs
                                   where g.NEWS_TYPE == 2
                                   select new
                                   {
                                       g.NEWS_ID,
                                       g.NEWS_TITLE

                                   });

                    if (AllList.ToList().Count > 0)
                    {

                        Session["NewsNewsList"] = DataUtil.LINQToDataTable(AllList);
                        DataTable tbl = Session["NewsNewsList"] as DataTable;
                        GridItemList.Columns[2].HeaderText = "Nguyên liệu";
                        GridItemList.DataSource = tbl;
                        GridItemList.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        public bool CheckCat(object newid2)
        {
            try
            {
                if (m_news_type == 2)
                {
                    int NEWS_ID1 = Utils.CIntDef(newid2);

                    var per = DB.GetTable<ESHOP_NEWS_NEW>().Where(gp => gp.NEWS_ID1 == m_news_id && gp.NEWS_ID2 == NEWS_ID1);
                    if (per.ToList().Count > 0)
                        return true;

                    return false;
                }
                if (m_news_type == 1)
                {
                    int NEWS_ID1 = Utils.CIntDef(newid2);

                    var per = DB.GetTable<ESHOP_NEWS_NEW>().Where(gp => gp.NEWS_ID1 == m_news_id && gp.NEWS_ID2 == NEWS_ID1);
                    if (per.ToList().Count > 0)
                        return true;

                    return false;
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return false;
            }

            return false;
        }
        public void CheckTypeNews()
        {
            var q_news_news = from eshop_news in DB.ESHOP_NEWs
                              where eshop_news.NEWS_ID == m_news_id
                              select new {eshop_news.NEWS_TYPE};
            if (q_news_news.Count() > 0)
            {
                m_news_type = Utils.CIntDef(q_news_news.First().NEWS_TYPE,0);
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

            DataTable dataTable = Session["NewsNewsList"] as DataTable;
            DataView sortedView = new DataView(dataTable);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            GridItemList.DataSource = sortedView;
            GridItemList.DataBind();
        }

        #endregion
        
    }
}