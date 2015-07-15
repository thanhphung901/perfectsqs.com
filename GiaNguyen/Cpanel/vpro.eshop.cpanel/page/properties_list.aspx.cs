using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using vpro.functions;
using System.Web.UI.HtmlControls;
using System.IO;
using vpro.eshop.cpanel.ucControls;
using vpro.eshop.cpanel.Components;

namespace vpro.eshop.cpanel.page
{
    public partial class properties_list : System.Web.UI.Page
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
                ucHeader.HeaderLevel1 = "Thuộc tính";
                ucHeader.HeaderLevel1_Url = "../page/properties_list.aspx";
                ucHeader.HeaderLevel2 = "DS Thuộc tính";
                ucHeader.HeaderLevel2_Url = "../page/properties_list.aspx";

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
            return "properties.aspx?prop_id=" + Utils.CStrDef(obj_id);
        }

        private void SearchResult()
        {
            try
            {
                string keyword = txtKeyword.Value;

                var AllList = (from g in DB.ESHOP_PROPERTies
                               where ("" == keyword || (g.PROP_NAME).Contains(keyword)) && g.PROP_RANK > 0
                               select new
                               {
                                   g.PROP_ID,
                                   g.PROP_PARENT_ID,
                                   g.PROP_NAME,
                                   g.PROP_RANK
                               });

                if (AllList.ToList().Count > 0)
                {
                    DataRelation relCat;
                    Session["PropList"] = DataUtil.LINQToDataTable(AllList);
                    //DataTable tbl = Session["CatList"] as DataTable;
                    DataTable tbl = DataUtil.LINQToDataTable(AllList);

                    DataSet ds = new DataSet();
                    ds.Tables.Add(tbl);

                    tbl.PrimaryKey = new DataColumn[] { tbl.Columns["PROP_ID"] };
                    relCat = new DataRelation("Category_parent", ds.Tables[0].Columns["PROP_ID"], ds.Tables[0].Columns["PROP_PARENT_ID"], false);

                    ds.Relations.Add(relCat);
                    DataSet dsCat = ds.Clone();
                    DataTable CatTable = ds.Tables[0];

                    CpanelUtils.TransformTableWithSpace(ref CatTable, dsCat.Tables[0], relCat, null);

                    GridItemList.DataSource = dsCat.Tables[0];
                    //if (CatTable.Rows.Count > GridItemList.PageSize)
                    //    GridItemList.AllowPaging = true;
                    //else
                    //    GridItemList.AllowPaging = false;
                    GridItemList.DataBind();
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
                int PropId = Utils.CIntDef(GridItemList.DataKeys[e.Item.ItemIndex]);

                var g_delete = DB.GetTable<ESHOP_PROPERTy>().Where(g => g.PROP_ID == PropId);

                DB.ESHOP_PROPERTies.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

                //delete folder
                string fullpath = Server.MapPath(PathFiles.GetPathCategory(PropId));
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
                Response.Redirect("properties_list.aspx");
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
                var g_delete = DB.GetTable<ESHOP_PROPERTy>().Where(g => items.Contains(Convert.ToInt32(g.PROP_ID)));

                DB.ESHOP_PROPERTies.DeleteAllOnSubmit(g_delete);
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

            DataTable dataTable = Session["PropList"] as DataTable;
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
                e.Item.Cells[4].Attributes.Add("onClick", "return confirm('Do you want delete?');");
            }

        }

        #endregion
    }
}