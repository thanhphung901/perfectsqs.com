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

//Create by lucnv 25-12-2012

namespace vpro.eshop.cpanel.page
{
    public partial class order_list : System.Web.UI.Page
    {

        #region Declare

        int _count = 0;
        int m_cus_id = 0;
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
            m_cus_id = Utils.CIntDef(Request["cus_id"]);

            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "Đơn đặt hàng";
                ucHeader.HeaderLevel1_Url = "../page/order_list.aspx";
                ucHeader.HeaderLevel2 = "Danh sách đơn hàng";
                ucHeader.HeaderLevel2_Url = "../page/order_list.aspx";

                LoadCategoryParent();

                ucFromDate.returnDate = DateTime.Now.Add(new TimeSpan(-30, 0, 0, 0));
                ucToDate.returnDate = DateTime.Now;

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

        private void SearchResult()
        {
            try
            {
                string keyword = txtKeyword.Value;
                //int Cat_Id=Utils.CIntDef(ddlCategory.SelectedValue);
                DateTime fromDate=ucFromDate.returnDate;
                DateTime toDate=new DateTime(ucToDate.returnDate.Year, ucToDate.returnDate.Month, ucToDate.returnDate.Day,23,59,59);

                int _status =Utils.CIntDef(ddlStatus.SelectedValue);
                if (_status != 99)
                {
                    var AllList = (from o in DB.ESHOP_ORDERs
                                   join o_i in DB.ESHOP_ORDER_ITEMs on o.ORDER_ID equals o_i.ORDER_ID
                                   where ("" == keyword || (o.ORDER_CODE).Contains(keyword) || (o.ORDER_FIELD1).Contains(keyword))
                                   && o.ORDER_PUBLISHDATE <= toDate && o.ORDER_PUBLISHDATE >= fromDate
                                   && o.ORDER_STATUS == _status
                                   orderby o.ORDER_PUBLISHDATE descending
                                   select o).Distinct();

                    if (AllList.ToList().Count > 0)
                        Session["OrderList"] = DataUtil.LINQToDataTable(AllList);

                    GridItemList.DataSource = AllList;
                    if (AllList.ToList().Count > GridItemList.PageSize)
                        GridItemList.AllowPaging = true;
                    else
                        GridItemList.AllowPaging = false;
                    GridItemList.DataBind();
                }
                else
                {
                    var AllList = (from o in DB.ESHOP_ORDERs
                                   join o_i in DB.ESHOP_ORDER_ITEMs on o.ORDER_ID equals o_i.ORDER_ID
                                   where ("" == keyword || (o.ORDER_CODE).Contains(keyword) || (o.ORDER_FIELD1).Contains(keyword))
                                   && o.ORDER_PUBLISHDATE <= toDate && o.ORDER_PUBLISHDATE >= fromDate
                                   orderby o.ORDER_PUBLISHDATE descending
                                   select o).Distinct();

                    if (AllList.ToList().Count > 0)
                        Session["OrderList"] = DataUtil.LINQToDataTable(AllList);

                    GridItemList.DataSource = AllList;
                    if (AllList.ToList().Count > GridItemList.PageSize)
                        GridItemList.AllowPaging = true;
                    else
                        GridItemList.AllowPaging = false;
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
                int ObjtId = Utils.CIntDef(GridItemList.DataKeys[e.Item.ItemIndex]);

                var g_delete = DB.GetTable<ESHOP_ORDER>().Where(g => g.ORDER_ID == ObjtId);

                DB.ESHOP_ORDERs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("order_list.aspx");
            }
        }

        public string getPublishDate(object obj_date)
        {
            return string.Format("{0:dd-MM-yyyy}", obj_date);
        }

        public string getLink(object obj_id)
        {
            return "order.aspx?order_id=" + Utils.CStrDef(obj_id);
        }

        private void LoadCategoryParent()
        {
            //try
            //{
            //    var CatList = (
            //                    from t2 in DB.ESHOP_CATEGORies
            //                    select new
            //                    {
            //                        CAT_ID = t2.CAT_NAME == "------- Root -------" ? 0 : t2.CAT_ID,
            //                        CAT_NAME = (string.IsNullOrEmpty(t2.CAT_CODE) ? t2.CAT_NAME : t2.CAT_NAME + "(" + t2.CAT_CODE + ")"),
            //                        CAT_PARENT_ID = t2.CAT_PARENT_ID,
            //                        CAT_RANK = t2.CAT_RANK
            //                    }
            //                );

            //    if (CatList.ToList().Count > 0)
            //    {
            //        DataRelation relCat;
            //        DataTable tbl = DataUtil.LINQToDataTable(CatList);
            //        DataSet ds = new DataSet();
            //        ds.Tables.Add(tbl);

            //        tbl.PrimaryKey = new DataColumn[] { tbl.Columns["CAT_ID"] };
            //        relCat = new DataRelation("Category_parent", ds.Tables[0].Columns["CAT_ID"], ds.Tables[0].Columns["CAT_PARENT_ID"], false);

            //        ds.Relations.Add(relCat);
            //        DataSet dsCat = ds.Clone();
            //        DataTable CatTable = ds.Tables[0];

            //        DataUtil.TransformTableWithSpace(ref CatTable, dsCat.Tables[0], relCat, null);

            //        ddlCategory.DataSource = dsCat.Tables[0];
            //        ddlCategory.DataTextField = "CAT_NAME";
            //        ddlCategory.DataValueField = "CAT_ID";
            //        ddlCategory.DataBind();
            //    }
            //    else
            //    {
            //        DataTable dt = new DataTable("Newtable");

            //        dt.Columns.Add(new DataColumn("CAT_ID"));
            //        dt.Columns.Add(new DataColumn("CAT_NAME"));

            //        DataRow row = dt.NewRow();
            //        row["CAT_ID"] = 0;
            //        row["CAT_NAME"] = "--------All--------";
            //        dt.Rows.Add(row);

            //        ddlCategory.DataTextField = "CAT_NAME";
            //        ddlCategory.DataValueField = "CAT_ID";
            //        ddlCategory.DataSource = dt;
            //        ddlCategory.DataBind();

            //    }

            //}
            //catch (Exception ex)
            //{
            //    clsVproErrorHandler.HandlerError(ex);
            //}
        }

        public string getOrderStatus(object obj_status)
        { 
            switch (Utils.CIntDef(obj_status))
            {
                case 0:
                    return "<font color='#FF0000'>Chưa xử lý</font>";
                case 1:
                    return "<font color='#0c5cd4'>Liên hệ khách hàng</font>";
                case 2:
                    return "<font color='#0c5cd4'>Giao hàng</font>";
                case 3:
                    return "<font color='#529214'>Thành công</font>";
                case 4:
                    return "<font color='#c4670c'>Thất bại</font>";
                default:
                    return "Chưa xử lý";
            }
        }

        public string getOrderPayment(object obj_payment)
        {
            switch (Utils.CIntDef(obj_payment))
            {
                case 1:
                    return "<font color='#0c5cd4'>Thu tiền tại cửa hàng</font>";
                case 2:
                    return "<font color='#529214'>Thu tiền tại nhà</font>";
                default:
                    return "Khác";
            }
        }

        public string GetMoney(object obj_value)
        {
            return string.Format("{0:#,#} đ", obj_value).Replace(',', '.');
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
                var g_delete = DB.GetTable<ESHOP_ORDER>().Where(g => items.Contains(g.ORDER_ID));

                DB.ESHOP_ORDERs.DeleteAllOnSubmit(g_delete);
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

            DataTable dataTable = Session["OrderList"] as DataTable;
            DataView sortedView = new DataView(dataTable);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            GridItemList.DataSource = sortedView;
            GridItemList.DataBind();
        }

        protected void GridItemList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            GridItemList.CurrentPageIndex = e.NewPageIndex;
            _count = (Utils.CIntDef(GridItemList.CurrentPageIndex, 0) * GridItemList.PageSize);
            GridItemList.DataSource = Session["OrderList"] as DataTable;
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
                e.Item.Cells[6].Attributes.Add("onClick", "return confirm('Do you want delete?');");
            }

        }

        #endregion

    }
}