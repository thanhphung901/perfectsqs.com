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

namespace vpro.eshop.cpanel.page
{
    public partial class booking_list : System.Web.UI.Page
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
                ucHeader.HeaderLevel1 = "Khách hàng";
                ucHeader.HeaderLevel1_Url = "../page/booking_list.aspx";
                ucHeader.HeaderLevel2 = "Danh sách đặt vé";
                ucHeader.HeaderLevel2_Url = "../page/booking_list.aspx";

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

        //public string getLink(object obj_id)
        //{
        //    return "booking.aspx?customer_id=" + Utils.CStrDef(obj_id);
        //}
        public string getsex(object sexid)
        {
            int id = Utils.CIntDef(sexid);
            switch (id)
            {
                case 0: return "Nam";
                case 1: return "Nữ";
                default: return "Khác";
            }
        }
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy}", News_PublishDate);
        }
        private void SearchResult()
        {
            try
            {
                string keyword = txtKeyword.Value;

                var AllList = (from g in DB.ESHOP_CONTACT_BOOKINGs
                               where ("" == keyword || (g.CONT_FULLNAME.ToString()).Contains(keyword))
                               select g);


                if (AllList.ToList().Count > 0)
                    Session["CustomerList"] = DataUtil.LINQToDataTable(AllList);

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
                int CustomerId = Utils.CIntDef(GridItemList.DataKeys[e.Item.ItemIndex]);

                var g_delete = DB.GetTable<ESHOP_CONTACT_BOOKING>().Where(g => g.CONT_ID == CustomerId);

                DB.ESHOP_CONTACT_BOOKINGs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("booking_list.aspx");
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
                var g_delete = DB.GetTable<ESHOP_USER>().Where(g => items.Contains(g.USER_ID));

                DB.ESHOP_USERs.DeleteAllOnSubmit(g_delete);
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
                        strLink = "booking.aspx?customer_id=" + Utils.CStrDef(GridItemList.DataKeys[i]);
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

            DataTable dataTable = Session["CustomerList"] as DataTable;
            DataView sortedView = new DataView(dataTable);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            GridItemList.DataSource = sortedView;
            GridItemList.DataBind();
        }

        protected void GridItemList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            GridItemList.CurrentPageIndex = e.NewPageIndex;
            _count = (Utils.CIntDef(GridItemList.CurrentPageIndex, 0) * GridItemList.PageSize);
            GridItemList.DataSource = Session["CustomerList"] as DataTable;
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
                e.Item.Cells[16].Attributes.Add("onClick", "return confirm('Do you want delete?');");
            }

        }

        #endregion
    }
}