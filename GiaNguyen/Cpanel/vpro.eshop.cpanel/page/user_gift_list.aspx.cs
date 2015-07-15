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
    public partial class user_gift_list : System.Web.UI.Page
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
                ucHeader.HeaderLevel1 = "Quà tặng";
                ucHeader.HeaderLevel1_Url = "../page/user_gift_list.aspx";
                ucHeader.HeaderLevel2 = "Khách hàng và quà tặng ";
                ucHeader.HeaderLevel2_Url = "../page/user_gift_list.aspx";

           

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
               
                DateTime fromDate=ucFromDate.returnDate;
                DateTime toDate=new DateTime(ucToDate.returnDate.Year, ucToDate.returnDate.Month, ucToDate.returnDate.Day,23,59,59);

                var AllList = (from g in DB.ESHOP_GIFTs
                               from c in DB.ESHOP_CUSTOMERs
                               from user_gifts in DB.ESHOP_USER_GIFTs
                               where user_gifts.CUSTOMER_ID == c.CUSTOMER_ID && user_gifts.GIFT_ID == g.GIFT_ID
                               && ("" == keyword
                               || (c.CUSTOMER_FULLNAME).Contains(keyword))
                               && user_gifts.USER_GIFT_DATE <= toDate && user_gifts.USER_GIFT_DATE >= fromDate
                               orderby user_gifts.USER_GIFT_DATE descending
                               select new { 
                                   user_gifts.USER_GIFT_DATE,
                                   user_gifts.USER_GIFT_ID,
                                   user_gifts.USER_GIFT_STATUS,
                                   user_gifts.USER_GIFT_QUANTITY,
                                   c.CUSTOMER_FULLNAME
                                   
                               }).Distinct();


                if (AllList.ToList().Count > 0)
                    Session["UserGiftList"] = DataUtil.LINQToDataTable(AllList);

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
                int ObjtId = Utils.CIntDef(GridItemList.DataKeys[e.Item.ItemIndex]);

                var g_delete = DB.GetTable<ESHOP_USER_GIFT>().Where(g => g.GIFT_ID == ObjtId);

                DB.ESHOP_USER_GIFTs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("user_gift_list.aspx?");
            }
        }

        public string getPublishDate(object obj_date)
        {
            return string.Format("{0:dd-MM-yyyy}", obj_date);
        }

        public string getLink(object obj_id)
        {
            return "user_gift.aspx?user_gift_id=" + Utils.CStrDef(obj_id) + "&cus_id=" + Utils.CStrDef(Request["cus_id"]);
        }

        

        public string getStatusGift(object obj_status)
        { 
            switch (Utils.CIntDef(obj_status))
            {
                case 0:
                    return "<font color='#FF0000'>Chưa xử lý</font>";
                case 1:
                    return "<font color='#0c5cd4'>Đã giao quà</font>";
              
                default:
                    return "Chưa xử lý";
            }
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
                var g_delete = DB.GetTable<ESHOP_USER_GIFT>().Where(g => items.Contains(g.USER_GIFT_ID));

                DB.ESHOP_USER_GIFTs.DeleteAllOnSubmit(g_delete);
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

            DataTable dataTable = Session["UserGiftList"] as DataTable;
            DataView sortedView = new DataView(dataTable);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            GridItemList.DataSource = sortedView;
            GridItemList.DataBind();
        }

        protected void GridItemList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            GridItemList.CurrentPageIndex = e.NewPageIndex;
            _count = (Utils.CIntDef(GridItemList.CurrentPageIndex, 0) * GridItemList.PageSize);
            GridItemList.DataSource = Session["UserGiftList"] as DataTable;
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