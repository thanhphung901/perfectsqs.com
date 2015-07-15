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
using System.IO;

namespace vpro.eshop.cpanel.page
{
    public partial class list_phieuthu : System.Web.UI.Page
    {
        #region Declare

        private int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "Danh sách phiếu thu";
                ucHeader.HeaderLevel1_Url = "../page/list-phieuthu.aspx";
                ucHeader.HeaderLevel2 = "Danh sách phiếu thu";
                ucHeader.HeaderLevel2_Url = "../page/list-phieuthu.aspx";

                SearchResult();

                txtKeyword.Attributes.Add("onKeyPress", Common.getSubmitScript(lbtSearch.ClientID));
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

            DataTable dataTable = Session["listphieuthu"] as DataTable;
            DataView sortedView = new DataView(dataTable);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            GridItemList.DataSource = sortedView;
            GridItemList.DataBind();
        }

        protected void GridItemList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            GridItemList.CurrentPageIndex = e.NewPageIndex;
            _count = (Utils.CIntDef(GridItemList.CurrentPageIndex, 0) * GridItemList.PageSize);
            GridItemList.DataSource = Session["listphieuthu"] as DataTable;
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
                e.Item.Cells[11].Attributes.Add("onClick", "return confirm('Bạn có chắc chắn xóa?');");
            }

        }

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

        #region My Function

       
        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        public string getLink(object obj_id)
        {
            return "phieuthu.aspx?ad_id=" + obj_id;
        }
        public string Formatmoney(object price)
        {
            return String.Format("{0:0,0 VNĐ}", price);
        }
        public string getdate(object date)
        {
            return string.Format("{0:dd/MM/yyyy}", date);
        }

        private void SearchResult()
        {
            try
            {
                string keyword = txtKeyword.Value;

                var AllList = (from g in DB.PHIEUTHUs
                               orderby g.PHIEUTHU_DATE descending
                               where ("" == keyword || (g.PHIEUTHU_NAMEHV).Contains(keyword) || (g.PHIEUTHU_PHONEHV).Contains(keyword))
                               select g);


                if (AllList.ToList().Count > 0)
                    Session["listphieuthu"] = DataUtil.LINQToDataTable(AllList);

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
            int BannerId = Utils.CIntDef(GridItemList.DataKeys[e.Item.ItemIndex]);
            DeleteInfo(BannerId);
        }

        private void DeleteInfo(int ad_id)
        {
            string strLink = "";
            try
            {
                var G_info = DB.GetTable<PHIEUTHU>().Where(g => g.PHIEUTHU_ID == ad_id);             
                DB.PHIEUTHUs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                //delete file
               
                strLink = "list-phieuthu.aspx";

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

        private void DeleteAllFilesInFolder(string folderpath)
        {
            foreach (var f in System.IO.Directory.GetFiles(folderpath))
                System.IO.File.Delete(f);
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
                var g_delete = DB.GetTable<PHIEUTHU>().Where(g => items.Contains(g.PHIEUTHU_ID));

                DB.PHIEUTHUs.DeleteAllOnSubmit(g_delete);
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

        

        #endregion
    }
}