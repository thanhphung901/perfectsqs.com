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

//Create by Lucnv 25-12-2012

namespace vpro.eshop.cpanel.page
{
    public partial class order : System.Web.UI.Page
    {

        #region Declare

        private int m_order_id = 0;
        private int m_cus_id = 0;
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

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_order_id = Utils.CIntDef(Request["order_id"]);
            m_cus_id = Utils.CIntDef(Request["cus_id"]);

            if (m_order_id == 0)
            {
                dvDelete.Visible = false;
            }

            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "Đơn đặt hàng";
                ucHeader.HeaderLevel1_Url = "../page/order_list.aspx";
                ucHeader.HeaderLevel2 = "Chi tiết";
                ucHeader.HeaderLevel2_Url = "../page/order.aspx";

                getInfo();
                LoadGridItems();
            }

        }

        #endregion

        #region Button Events

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
        }

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            string strLink = "";

            try
            {
                var _items = DB.GetTable<ESHOP_ORDER>().Where(o => o.ORDER_ID == m_order_id);

                if (_items.ToList().Count > 0)
                {
                    _items.Single().ORDER_STATUS = Utils.CIntDef(ddlStatus.SelectedValue);

                    DB.SubmitChanges();

                    strLink = "order_list.aspx?cus_id=" + m_cus_id;
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

        #region My Functions

        private void getInfo()
        {
            try
            {
                var G_info = (from g in DB.ESHOP_ORDERs
                              where g.ORDER_ID == m_order_id
                              select g
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtOrderCode.Value = G_info.ToList()[0].ORDER_CODE;
                    txtOrderDate.Value = string.Format("{0:dd-MM-yyyy}", G_info.ToList()[0].ORDER_PUBLISHDATE);
                    txtOrderDesc.Value = G_info.ToList()[0].ORDER_FIELD1;
                    ddlStatus.SelectedValue = Utils.CStrDef(G_info.ToList()[0].ORDER_STATUS, "0");

                    txtName.Value = G_info.ToList()[0].ORDER_NAME;
                    txtEmail.Value = G_info.ToList()[0].ORDER_EMAIL;
                    txtAddress.Value = G_info.ToList()[0].ORDER_ADDRESS;
                    txtPhone.Value = G_info.ToList()[0].ORDER_PHONE;
                    ucDate.returnDate = Utils.CDateDef(G_info.ToList()[0].ORDER_PUBLISHDATE, DateTime.Now);

                    lblTotal.Text = "Tổng tiền : " + GetMoney(G_info.ToList()[0].ORDER_TOTAL_ALL);
                }
                else
                {
                    ucDate.returnDate = DateTime.Now;
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void DeleteInfo()
        {
            try
            {
                var G_info = DB.GetTable<ESHOP_ORDER>().Where(g => g.ORDER_ID == m_order_id);

                DB.ESHOP_ORDERs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                //Response.Redirect("order_list.aspx?cus_id=" + m_cus_id);

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void LoadGridItems()
        {
            try
            {
                var AllList = (from o in DB.ESHOP_ORDER_ITEMs
                               where o.ORDER_ID==m_order_id
                               orderby o.ESHOP_NEW.NEWS_TITLE descending
                               select new
                               {
                                   o.ITEM_ID,
                                   o.ESHOP_NEW.NEWS_TITLE,
                                   o.ITEM_QUANTITY,
                                   o.ITEM_PRICE,
                                   o.ITEM_SUBTOTAL,
                                   o.ITEM_FIELD1,
                                   o.ITEM_FIELD2
                               });


                if (AllList.ToList().Count > 0)
                    Session["OrderItems"] = DataUtil.LINQToDataTable(AllList);

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

        public string GetNewsTitle(object Title, object Color, object Size)
        {
            try
            {
                return Utils.CStrDef(Title) + "(" + Utils.CStrDef(Color) + "/" + Utils.CStrDef(Size) + ")";
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        public string GetMoney(object obj_value)
        {
            return string.Format("{0:#,#} đ", obj_value).Replace(',','.');
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

            DataTable dataTable = Session["OrderItems"] as DataTable;
            DataView sortedView = new DataView(dataTable);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            GridItemList.DataSource = sortedView;
            GridItemList.DataBind();
        }

        protected void GridItemList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            GridItemList.CurrentPageIndex = e.NewPageIndex;
            _count = (Utils.CIntDef(GridItemList.CurrentPageIndex, 0) * GridItemList.PageSize);
            GridItemList.DataSource = Session["OrderItems"] as DataTable;
            GridItemList.DataBind();
        }


        #endregion
    }
}