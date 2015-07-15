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
using System.IO;
using System.Net.Mail;


namespace vpro.eshop.cpanel.page
{
    public partial class page_comment : System.Web.UI.Page
    {
        #region Declare

        int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();
        private int m_mail_id = 0;
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
            if (!IsPostBack)
            {
                m_mail_id = Utils.CIntDef(Request["MAIL_ID"]);
                ucHeader.HeaderLevel1 = "Comment";
                ucHeader.HeaderLevel1_Url = "../page/page-comment.aspx";
                ucHeader.HeaderLevel2 = "Danh sách Comment";
                ucHeader.HeaderLevel2_Url = "../page/page-comment.aspx";

                SearchResult();

                txtKeyword.Attributes.Add("onKeyPress", Common.getSubmitScript(lbtSearch.ClientID));
            }

        }

        #endregion
        private void SearchResult()
        {
            try
            {
                string keyword = txtKeyword.Value;

                var AllList = (from g in DB.ESHOP_NEWS_COMMENTs
                               where "" == keyword || g.COMMENT_NAME.Contains(keyword)
                               orderby g.COMMENT_ID descending
                               select g);


                if (AllList.ToList().Count > 0)
                    Session["NewsList"] = DataUtil.LINQToDataTable(AllList);

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
        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        private void EventDelete(DataGridCommandEventArgs e)
        {
            try
            {
                int NewsId = Utils.CIntDef(GridItemList.DataKeys[e.Item.ItemIndex]);

                var g_delete = DB.GetTable<ESHOP_NEWS_COMMENT>().Where(g => g.COMMENT_ID == NewsId);

                DB.ESHOP_NEWS_COMMENTs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("page-comment.aspx");
            }
        }
        private void DeleteAllFilesInFolder(string folderpath)
        {
            foreach (var f in System.IO.Directory.GetFiles(folderpath))
                System.IO.File.Delete(f);
        }
        protected void lbtSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }

        protected void lbtAn_Click(object sender, EventArgs e)
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
                var list = DB.ESHOP_NEWS_COMMENTs.Where(g => items.Contains(g.COMMENT_ID)).ToList();
                foreach (var i1 in list)
                {
                    i1.COMMENT_STATUS = 0;
                    DB.SubmitChanges();
                }
                SearchResult();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }

        }


        protected void Lbhien_Click(object sender, EventArgs e)
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
                var list = DB.ESHOP_NEWS_COMMENTs.Where(g => items.Contains(g.COMMENT_ID)).ToList();
                foreach (var i1 in list)
                {
                    i1.COMMENT_STATUS = 1;
                    DB.SubmitChanges();
                }
                SearchResult();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }

        }
        private void DeleteInfo()
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
        public string checkactive(object check)
        {
            int active = Utils.CIntDef(check);
            return active == 1 ? "Hiện" : "Ẩn";
        }
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

            DataTable dataTable = Session["NewsList"] as DataTable;
            DataView sortedView = new DataView(dataTable);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            GridItemList.DataSource = sortedView;
            GridItemList.DataBind();
        }

        //protected void GridItemList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        //{
        //    GridItemList.CurrentPageIndex = e.NewPageIndex;
        //    _count = (Utils.CIntDef(GridItemList.CurrentPageIndex, 0) * GridItemList.PageSize);
        //    GridItemList.DataSource = Session["NewsList"] as DataTable;
        //    GridItemList.DataBind();
        //}

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
                e.Item.Cells[6].Attributes.Add("onClick", "return confirm('Do you want delete?');");
            }

        }

        #endregion

        protected void GridItemList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            GridItemList.CurrentPageIndex = e.NewPageIndex;
            _count = (Utils.CIntDef(GridItemList.CurrentPageIndex, 0) * GridItemList.PageSize);
            GridItemList.DataSource = Session["NewsList"] as DataTable;
            GridItemList.DataBind();
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
        }
        public string getLink(object obj_id)
        {
            return "news.aspx?news_id=" + Utils.CStrDef(obj_id);
        }
        public string getname(object news_id)
        {
            int id = Utils.CIntDef(news_id);
            var list = DB.ESHOP_NEWs.Where(n => n.NEWS_ID == id).Single();
            if (list != null)
            {
                return list.NEWS_TITLE;
            }
            return "";
        }
    }
}