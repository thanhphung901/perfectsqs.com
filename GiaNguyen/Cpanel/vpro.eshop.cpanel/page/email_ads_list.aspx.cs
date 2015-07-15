using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using vpro.eshop.cpanel.ucControls;
using vpro.functions;
using System.IO;

namespace vpro.eshop.cpanel.page
{
    public partial class email_ads_list : System.Web.UI.Page
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
                ucHeader.HeaderLevel1 = "Danh sách đăng ký nhận email";
                ucHeader.HeaderLevel1_Url = "../page/email_ads_list.aspx";
                ucHeader.HeaderLevel2 = "Danh sách đăng ký nhận email";
                ucHeader.HeaderLevel2_Url = "../page/email_ads_list.aspx";

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

            DataTable dataTable = Session["AdItemList"] as DataTable;
            DataView sortedView = new DataView(dataTable);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            GridItemList.DataSource = sortedView;
            GridItemList.DataBind();
        }

        protected void GridItemList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            GridItemList.CurrentPageIndex = e.NewPageIndex;
            _count = (Utils.CIntDef(GridItemList.CurrentPageIndex, 0) * GridItemList.PageSize);
            GridItemList.DataSource = Session["AdItemList"] as DataTable;
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
                e.Item.Cells[3].Attributes.Add("onClick", "return confirm('Do you want delete?');");
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
            return "email_register.aspx?ad_id=" + obj_id;
        }
        public string getImage(object obj_id, object obj_image1)
        {
            if (!string.IsNullOrEmpty(Utils.CStrDef(obj_id)) && Utils.CIntDef(obj_id) > 0)
                return "<img src='" + PathFiles.GetPathAdItems(Utils.CIntDef(obj_id)) + Utils.CStrDef(obj_image1) + "' width='60px' border='0' />";
            else
                return "";
        }
        public string getLinkImage(object obj_id, object obj_file)
        {
            if (!string.IsNullOrEmpty(Utils.CStrDef(obj_file)) && Utils.CIntDef(obj_id) > 0)
            {
                return "<a href='" + PathFiles.GetPathAdItems(Utils.CIntDef(obj_id)) + Utils.CStrDef(obj_file) + "' target='_blank'>" + Utils.CStrDef(obj_file) + "</a>";
            }

            return "";
        }
        public string getPos(object Ad_Pos)
        {
            return Utils.CIntDef(Ad_Pos) == 0 ? "Giữa" : Utils.CIntDef(Ad_Pos) == 1 ? "Trái" : Utils.CIntDef(Ad_Pos) == 3 ? "Dưới" : Utils.CIntDef(Ad_Pos) == 5 ? "Trên" : Utils.CIntDef(Ad_Pos) == 2 ? "Phải" : "Khác";
        }
        private void SearchResult()
        {
            try
            { 
                string keyword = txtKeyword.Value;

                var AllList = (from g in DB.ESHOP_POINTs
                               orderby g.POINT_ID descending
                               where ("" == keyword || (g.POINT_FIELD1).Contains(keyword))
                               select g);


                if (AllList.ToList().Count > 0)
                    Session["AdItemList"] = DataUtil.LINQToDataTable(AllList);

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
                string Banner_File = "";

                var G_info = DB.GetTable<ESHOP_POINT>().Where(g => g.POINT_ID == ad_id);

                if (G_info.ToList().Count > 0)
                    // Banner_File = Utils.CStrDef(G_info.ToList()[0].PER_ID);

                    DB.ESHOP_POINTs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                //delete file
                //if (!string.IsNullOrEmpty(Banner_File))
                //{
                //    string fullpath = Server.MapPath(PathFiles.GetPathAdItems(ad_id) + Banner_File);

                //    if (File.Exists(fullpath))
                //    {
                //        File.Delete(fullpath);
                //    }
                //}

                strLink = "email_ads_list.aspx";

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
                        try
                        {
                            //delete folder
                            string fullpath = Server.MapPath(PathFiles.GetPathNews(items[j]));
                            if (Directory.Exists(fullpath))
                            {
                                DeleteAllFilesInFolder(fullpath);
                                Directory.Delete(fullpath);
                            }
                        }
                        catch (Exception)
                        { }
                        j++;
                    }

                    i++;
                }

                //delete 
                var g_delete = DB.GetTable<ESHOP_POINT>().Where(g => items.Contains(g.POINT_ID));

                DB.ESHOP_POINTs.DeleteAllOnSubmit(g_delete);
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
                        strLink = "email_register.aspx?ad_id=" + Utils.CStrDef(GridItemList.DataKeys[i]);
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

    }
}