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
    public partial class user_gift : System.Web.UI.Page
    {

        #region Declare

        private int m_user_gift_id = 0;
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
            m_user_gift_id = Utils.CIntDef(Request["user_gift_id"]);
            m_cus_id = Utils.CIntDef(Request["cus_id"]);

            if (m_user_gift_id == 0)
            {
                dvDelete.Visible = false;
            }

            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "Khách hàng và quà tặng";
                ucHeader.HeaderLevel1_Url = "../page/user_gift_list.aspx";
                ucHeader.HeaderLevel2 = "Chi tiết";
                ucHeader.HeaderLevel2_Url = "../page/user_gift.aspx";

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
                var _items = DB.GetTable<ESHOP_USER_GIFT>().Where(o => o.USER_GIFT_ID == m_user_gift_id);

                if (_items.ToList().Count > 0)
                {
                    _items.Single().USER_GIFT_STATUS = Utils.CIntDef(ddlStatus.SelectedValue);
                    _items.Single().USER_GIFT_DESC =Utils.CStrDef(txtOrderDesc.Value);
                    DB.SubmitChanges();

                    strLink = "user_gift_list.aspx?cus_id=" + m_cus_id;
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
                var G_info = (from g in DB.ESHOP_USER_GIFTs
                              join c in DB.ESHOP_CUSTOMERs on g.CUSTOMER_ID equals c.CUSTOMER_ID
                              where g.USER_GIFT_ID == m_user_gift_id
                              select new
                              {
                                  c.CUSTOMER_FULLNAME,
                                  c.CUSTOMER_EMAIL,
                                  c.CUSTOMER_ADDRESS,
                                  c.CUSTOMER_PHONE1,
                                  g.USER_GIFT_DESC,
                                  g.USER_GIFT_DATE,
                                  g.USER_GIFT_STATUS
                              }
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtName.Value = G_info.ToList()[0].CUSTOMER_FULLNAME;
                    txtEmail.Value = G_info.ToList()[0].CUSTOMER_EMAIL;
                    txtAddress.Value = G_info.ToList()[0].CUSTOMER_ADDRESS;
                    txtPhone.Value = G_info.ToList()[0].CUSTOMER_PHONE1;
                    txtOrderDesc.Value = G_info.ToList()[0].USER_GIFT_DESC;
                    ddlStatus.SelectedValue = Utils.CStrDef(G_info.ToList()[0].USER_GIFT_STATUS);
                    txtOrderDate.Value =String.Format( "{0:dd/MM/yyyy}",G_info.ToList()[0].USER_GIFT_DATE);
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
                var G_info = DB.GetTable<ESHOP_USER_GIFT>().Where(g => g.USER_GIFT_ID == m_user_gift_id);

                DB.ESHOP_USER_GIFTs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                Response.Redirect("user_gift_list.aspx?cus_id=" + m_cus_id);

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
                var AllList = (from o in DB.ESHOP_USER_GIFTs
                               from g in DB.ESHOP_GIFTs
                               where o.USER_GIFT_ID==m_user_gift_id && o.GIFT_ID==g.GIFT_ID
                               select new
                               {
                                   o.GIFT_ID,
                                   o.CUSTOMER_ID,
                                   o.USER_GIFT_DATE,
                                   o.USER_GIFT_ID,
                                   o.USER_GIFT_QUANTITY,
                                   o.USER_GIFT_STATUS,
                                   g.GIFT_NAME,
                                   g.GIFT_DESC
                                   
                               });


                if (AllList.ToList().Count > 0)
                    Session["UserGiftItems"] = DataUtil.LINQToDataTable(AllList);

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

        public string GetMoney(object obj_value)
        {
            return string.Format("{0:#,#}", obj_value).Replace(',','.');
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

            DataTable dataTable = Session["UserGiftItems"] as DataTable;
            DataView sortedView = new DataView(dataTable);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            GridItemList.DataSource = sortedView;
            GridItemList.DataBind();
        }

        protected void GridItemList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            GridItemList.CurrentPageIndex = e.NewPageIndex;
            _count = (Utils.CIntDef(GridItemList.CurrentPageIndex, 0) * GridItemList.PageSize);
            GridItemList.DataSource = Session["UserGiftItems"] as DataTable;
            GridItemList.DataBind();
        }


        #endregion
    }
}