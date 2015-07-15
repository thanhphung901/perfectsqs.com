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

namespace vpro.eshop.cpanel.page
{
    public partial class online_list : System.Web.UI.Page
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
                ucHeader.HeaderLevel1 = "Support online";
                ucHeader.HeaderLevel1_Url = "../page/online_list.aspx";
                ucHeader.HeaderLevel2 = "Add new/Update";
                ucHeader.HeaderLevel2_Url = "../page/online.aspx";

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
            return "online.aspx?online_id=" + Utils.CStrDef(obj_id);
        }

        public string getType(object obj_type)
        {
            string str="";
            switch (Utils.CIntDef(obj_type))
            {
                case 0:
                    str = "Yahoo";
                    break;
                case 1:
                    str = "Skype";
                    break;
                case 2:
                    str = "Hotline";
                    break;
                case 3:
                    str = "Facebook";
                    break;
                case 4:
                    str = "Google+";
                    break;
                case 5:
                    str = "Twitter";
                    break;
                case 6:
                    str = "Youtube";
                    break;
            }
            return str;
        }

        private void SearchResult()
        {
            try
            {
                string keyword = txtKeyword.Value;

                var AllList = (from g in DB.ESHOP_ONLINEs
                               where "" == keyword || (g.ONLINE_NICKNAME).Contains(keyword) || (g.ONLINE_DESC).Contains(keyword)
                               select g);


                if (AllList.ToList().Count > 0)
                    Session["OnlineList"] = DataUtil.LINQToDataTable(AllList);

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
                int UnitId = Utils.CIntDef(GridItemList.DataKeys[e.Item.ItemIndex]);

                var g_delete = DB.GetTable<ESHOP_ONLINE>().Where(g => g.ONLINE_ID == UnitId);

                DB.ESHOP_ONLINEs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

                //delete folder
                string fullpath = Server.MapPath(PathFiles.GetPathOnline(UnitId));
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
                Response.Redirect("online_list.aspx");
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
                var g_delete = DB.GetTable<ESHOP_ONLINE>().Where(g => items.Contains(g.ONLINE_ID));

                DB.ESHOP_ONLINEs.DeleteAllOnSubmit(g_delete);
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
                        strLink = "online.aspx?online_id=" + Utils.CStrDef(GridItemList.DataKeys[i]);
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

            DataTable dataTable = Session["OnlineList"] as DataTable;
            DataView sortedView = new DataView(dataTable);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            GridItemList.DataSource = sortedView;
            GridItemList.DataBind();
        }

        protected void GridItemList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            GridItemList.CurrentPageIndex = e.NewPageIndex;
            _count = (Utils.CIntDef(GridItemList.CurrentPageIndex, 0) * GridItemList.PageSize);
            GridItemList.DataSource = Session["OnlineList"] as DataTable;
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
                e.Item.Cells[5].Attributes.Add("onClick", "return confirm('Do you want delete?');");
            }

        }

        #endregion

    }
}