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
using vpro.eshop.cpanel.Components;
using System.IO;

namespace vpro.eshop.cpanel.page
{
    public partial class category_list : System.Web.UI.Page
    {

        #region Declare

        int _count = 0;
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
            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "Category";
                ucHeader.HeaderLevel1_Url = "../page/category_list.aspx";
                ucHeader.HeaderLevel2 = "DS Category";
                ucHeader.HeaderLevel2_Url = "../page/category_list.aspx";

                SearchResult();

                txtKeyword.Attributes.Add("onKeyPress", Common.getSubmitScript(lbtSearch.ClientID));
            }

        }

        #endregion

        #region My Functions

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        public string getLink(object obj_id)
        {
            return "category.aspx?cat_id=" + Utils.CStrDef(obj_id);
        }

        private void SearchResult()
        {
            try
            {
                string keyword =CpanelUtils.ClearUnicode(txtKeyword.Value);

                var AllList = (from g in DB.ESHOP_CATEGORies
                               where ("" == keyword || (DB.fClearUnicode(g.CAT_NAME)).Contains(keyword)) && g.CAT_RANK > 0
                               select new { 
                                   g.CAT_ID,
                                   g.CAT_PARENT_ID,
                                   CAT_NAME = (string.IsNullOrEmpty(g.CAT_CODE) ? g.CAT_NAME : g.CAT_NAME + "(" + g.CAT_CODE + ")"),
                                   g.CAT_POSITION,
                                   g.CAT_LANGUAGE,
                                   g.CAT_ORDER,
                                   g.CAT_PERIOD_ORDER,
                                   g.CAT_RANK
                               });

                if (AllList.ToList().Count > 0)
                {
                    DataRelation relCat;
                    Session["CatList"] = DataUtil.LINQToDataTable(AllList);
                    //DataTable tbl = Session["CatList"] as DataTable;
                    DataTable tbl = DataUtil.LINQToDataTable(AllList);

                    DataSet ds = new DataSet();
                    ds.Tables.Add(tbl);

                    tbl.PrimaryKey = new DataColumn[] { tbl.Columns["CAT_ID"] };
                    relCat = new DataRelation("Category_parent", ds.Tables[0].Columns["CAT_ID"], ds.Tables[0].Columns["CAT_PARENT_ID"], false);

                    ds.Relations.Add(relCat);
                    DataSet dsCat = ds.Clone();
                    DataTable CatTable = ds.Tables[0];

                    DataUtil.TransformTableWithSpace(ref CatTable, dsCat.Tables[0], relCat, null);
                    if (IsPostBack)
                    {
                        GridItemList.DataSource = AllList;
                        GridItemList.DataBind();
                    }
                    else
                    {
                        GridItemList.DataSource = dsCat.Tables[0];
                        GridItemList.DataBind();
                    }
                }

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
                int CatId = Utils.CIntDef(GridItemList.DataKeys[e.Item.ItemIndex]);

                var g_delete = DB.GetTable<ESHOP_CATEGORy>().Where(g => g.CAT_ID == CatId);

                DB.ESHOP_CATEGORies.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

                //delete folder
                string fullpath = Server.MapPath(PathFiles.GetPathCategory(CatId));
                if (Directory.Exists(fullpath))
                {
                    DeleteAllFilesInFolder(fullpath);
                    Directory.Delete(fullpath);
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("category_list.aspx");
            }
        }

        private void DeleteAllFilesInFolder(string folderpath)
        {
            foreach (var f in System.IO.Directory.GetFiles(folderpath))
                System.IO.File.Delete(f);
        }

        public string getPos(object Cat_Pos)
        {
            string str = "";
            switch (Utils.CIntDef(Cat_Pos))
            {
                case 0:
                    str = "Menu";
                    break;
                case 1: str = "Left";
                    break;
                case 2: str = "Menu & Left";
                    break;
                case 10: str = "Other";
                    break;
            }
            return str;            
        }

        public string getLanguage(object Cat_Pos)
        {
            return Utils.CIntDef(Cat_Pos) == 1 ? "Viet Nam" : "English";
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
                var g_delete = DB.GetTable<ESHOP_CATEGORy>().Where(g => items.Contains(g.CAT_ID));

                DB.ESHOP_CATEGORies.DeleteAllOnSubmit(g_delete);
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
            try
            {
                int i = 0;
                HtmlInputText txtOrder;
                HtmlInputText txtOrderPeriod;

                foreach (DataGridItem itemgrid in GridItemList.Items)
                {
                    txtOrder = (HtmlInputText)itemgrid.Cells[5].FindControl("txtOrder");
                    txtOrderPeriod = (HtmlInputText)itemgrid.Cells[6].FindControl("txtOrderPeriod");

                    int CatId = Utils.CIntDef(GridItemList.DataKeys[i]);
                    var c_update = DB.GetTable<ESHOP_CATEGORy>().Where(g => g.CAT_ID == CatId);

                    if (c_update.ToList().Count > 0)
                    {
                        c_update.Single().CAT_ORDER = Utils.CIntDef(txtOrder.Value);
                        c_update.Single().CAT_PERIOD_ORDER = Utils.CIntDef(txtOrderPeriod.Value);

                        DB.SubmitChanges();
                    }

                    i++;
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            { SearchResult(); }
        }

        protected void lbtEdit_Click(object sender, EventArgs e)
        {
            HtmlInputCheckBox check = new HtmlInputCheckBox();
            string strLink = "";
            int i = 0;

            try
            {
                foreach (DataGridItem item in GridItemList.Items)
                {
                    check = new HtmlInputCheckBox();
                    check = (HtmlInputCheckBox)item.Cells[1].FindControl("chkSelect");

                    if (check.Checked)
                    {
                        strLink = "category.aspx?cat_id=" + Utils.CStrDef(GridItemList.DataKeys[i]);
                        break;
                    }
                    i++;
                }

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

            DataTable dataTable = Session["CatList"] as DataTable;
            DataView sortedView = new DataView(dataTable);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;

            GridItemList.DataSource = sortedView;
            GridItemList.DataBind();

        }

        //protected void GridItemList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        //{
        //    GridItemList.CurrentPageIndex = e.NewPageIndex;
        //    _count = (Utils.CIntDef(GridItemList.CurrentPageIndex, 0) * GridItemList.PageSize);
        //    GridItemList.DataSource = Session["CatList"] as DataTable;
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
                e.Item.Cells[7].Attributes.Add("onClick", "return confirm('Do you want delete this category?');");
            }

        }

        #endregion

    }
}