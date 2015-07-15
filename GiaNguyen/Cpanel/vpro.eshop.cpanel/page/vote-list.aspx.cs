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
using System.Reflection;
namespace vpro.eshop.cpanel.page
{
    public partial class vote_list : System.Web.UI.Page
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
                ucHeader.HeaderLevel1 = "VOTE";
                ucHeader.HeaderLevel1_Url = "../page/vote-list.aspx";
                ucHeader.HeaderLevel2 = "DS VOTE";
                ucHeader.HeaderLevel2_Url = "../page/vote-list.aspx";

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
            return "vote.aspx?vote_id=" + Utils.CStrDef(obj_id);
        }
        #region relation
        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        public static void TransformTableWithSpace(ref DataTable source, DataTable dest, DataRelation rel, DataRow parentRow)
        {
            if (parentRow == null)
            {
                foreach (DataRow row in source.Rows)
                {
                    if (!row.HasErrors && (Utils.CIntDef(row["VOTE_PARENT_ID"]) <= 0))
                    {
                        row["VOTE_QUESTION"] = (Utils.CIntDef(row["VOTE_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(row["VOTE_RANK"]))) + row["VOTE_QUESTION"];
                        dest.Rows.Add(row.ItemArray);
                        row.RowError = "dirty";
                        if (Utils.CStrDef(row["VOTE_QUESTION"]) != "------- Root -------" || Utils.CStrDef(row["VOTE_QUESTION"]) != "------- All -------")
                            TransformTableWithSpace(ref source, dest, rel, row);
                    }
                }
            }
            else
            {
                DataRow[] children = parentRow.GetChildRows(rel);
                if (!parentRow.HasErrors)
                {
                    parentRow["VOTE_QUESTION"] = (Utils.CIntDef(parentRow["VOTE_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(parentRow["VOTE_RANK"]))) + parentRow["VOTE_QUESTION"];
                    dest.Rows.Add(parentRow.ItemArray);
                    parentRow.RowError = "dirty";
                }
                if (children != null && children.Length > 0)
                {
                    foreach (DataRow child in children)
                    {
                        if (!child.HasErrors)
                        {
                            child["VOTE_QUESTION"] = (Utils.CIntDef(child["VOTE_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(child["VOTE_RANK"]))) + child["VOTE_QUESTION"];
                            dest.Rows.Add(child.ItemArray);
                            child.RowError = "dirty";
                            TransformTableWithSpace(ref source, dest, rel, child);
                        }
                    }
                }
            }
        }

        public static string Duplicate(string partToDuplicate, int howManyTimes)
        {
            string result = "";

            for (int i = 0; i < howManyTimes; i++)
                result += partToDuplicate;

            return result;
        }
        #endregion
        private void SearchResult()
        {
            try
            {
                string keyword = txtKeyword.Value;

                var AllList = (from g in DB.VOTEs
                               where ("" == keyword || (g.VOTE_QUESTION).Contains(keyword))
                               select g);

                if (AllList.ToList().Count > 0)
                {
                    DataRelation relCat;
                    Session["VoteList"] = DataUtil.LINQToDataTable(AllList);
                    //DataTable tbl = Session["VoteList"] as DataTable;
                    DataTable tbl = DataUtil.LINQToDataTable(AllList);

                    DataSet ds = new DataSet();
                    ds.Tables.Add(tbl);

                    tbl.PrimaryKey = new DataColumn[] { tbl.Columns["VOTE_OID"] };
                    relCat = new DataRelation("Category_parent", ds.Tables[0].Columns["VOTE_OID"], ds.Tables[0].Columns["VOTE_PARENT_ID"], false);

                    ds.Relations.Add(relCat);
                    DataSet dsCat = ds.Clone();
                    DataTable CatTable = ds.Tables[0];

                    TransformTableWithSpace(ref CatTable, dsCat.Tables[0], relCat, null);

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
                int CatId = Utils.CIntDef(GridItemList.DataKeys[e.Item.ItemIndex]);

                var g_delete = DB.GetTable<VOTE>().Where(g => g.VOTE_OID == CatId);

                DB.VOTEs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

              

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("vote-list.aspx");
            }
        }

        private void DeleteAllFilesInFolder(string folderpath)
        {
            foreach (var f in System.IO.Directory.GetFiles(folderpath))
                System.IO.File.Delete(f);
        }

        public string getPos(object Cat_Pos)
        {
            //return Utils.CIntDef(Cat_Pos) == 0 ? "Trên" : Utils.CIntDef(Cat_Pos) == 1 ? "Trái" : Utils.CIntDef(Cat_Pos) == 2 ? "Trên + Trái" : "Khác";
            return Utils.CIntDef(Cat_Pos) == 0 ? "Trên" : "Khác";
        }

        public string getLanguage(object Cat_Pos)
        {
            return Utils.CIntDef(Cat_Pos) == 1 ? "Viet Nam" : "All";
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
                var g_delete = DB.GetTable<VOTE>().Where(g => items.Contains(g.VOTE_OID));

                DB.VOTEs.DeleteAllOnSubmit(g_delete);
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

            DataTable dataTable = Session["VoteList"] as DataTable;
            DataView sortedView = new DataView(dataTable);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;

            GridItemList.DataSource = sortedView;
            GridItemList.DataBind();

        }

        //protected void GridItemList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        //{
        //    GridItemList.CurrentPageIndex = e.NewPageIndex;
        //    _count = (Utils.CIntDef(GridItemList.CurrentPageIndex, 0) * GridItemList.PageSize);
        //    GridItemList.DataSource = Session["VoteList"] as DataTable;
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
                e.Item.Cells[5].Attributes.Add("onClick", "return confirm('Do you want delete?');");
            }

        }

        #endregion
    }
}