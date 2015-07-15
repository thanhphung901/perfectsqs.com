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
using System.Net.Mail;

namespace vpro.eshop.cpanel.page
{
    public partial class news_list : System.Web.UI.Page
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
                ucHeader.HeaderLevel1 = "News - Products";
                ucHeader.HeaderLevel1_Url = "../page/news_list.aspx";
                ucHeader.HeaderLevel2 = "List";
                ucHeader.HeaderLevel2_Url = "../page/news_list.aspx";
                Loadchuyenmuc();
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
            return "news.aspx?news_id=" + Utils.CStrDef(obj_id);
        }
        public string getLink_comment(object obj_id)
        {
            return "news_comment.aspx?news_id=" + Utils.CStrDef(obj_id);
        }
        public int Getcount_comment(object NewsID)
        {
            try
            {
                int _iNewsID = Utils.CIntDef(NewsID);
                var _vComment = DB.GetTable<ESHOP_NEWS_COMMENT>().Where(a => a.NEWS_ID == _iNewsID );
                return _vComment.ToList().Count;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return 0;
            }
        }
        public string getTypeNew(object obj_id)
        {
            return (Utils.CIntDef(obj_id) == 0) ? "News" : ((Utils.CIntDef(obj_id) == 1) ? "Products" : "Other");
        }
        public void Loadchuyenmuc()
        {
            try
            {
                var CatList = (
                                from t2 in DB.ESHOP_CATEGORies
                                where t2.CAT_RANK > 0
                                select new
                                {
                                    CAT_ID = t2.CAT_NAME == "------- Root -------" ? 0 : t2.CAT_ID,
                                    CAT_NAME = (string.IsNullOrEmpty(t2.CAT_CODE) ? t2.CAT_NAME : t2.CAT_NAME + "(" + t2.CAT_CODE + ")"),
                                    CAT_NAME_EN = (string.IsNullOrEmpty(t2.CAT_CODE_EN) ? t2.CAT_NAME_EN : t2.CAT_NAME_EN + "(" + t2.CAT_CODE_EN + ")"),
                                    CAT_PARENT_ID = t2.CAT_PARENT_ID,
                                    CAT_RANK = t2.CAT_RANK
                                }
                            );

                if (CatList.ToList().Count > 0)
                {
                    DataRelation relCat;
                    DataTable tbl = DataUtil.LINQToDataTable(CatList);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(tbl);

                    tbl.PrimaryKey = new DataColumn[] { tbl.Columns["CAT_ID"] };
                    relCat = new DataRelation("Category_parent", ds.Tables[0].Columns["CAT_ID"], ds.Tables[0].Columns["CAT_PARENT_ID"], false);

                    ds.Relations.Add(relCat);
                    DataSet dsCat = ds.Clone();
                    DataTable CatTable = ds.Tables[0];

                    DataUtil.TransformTableWithSpace(ref CatTable, dsCat.Tables[0], relCat, null);

                    ddlCategory.DataSource = dsCat.Tables[0];
                    ddlCategory.DataTextField = "CAT_NAME";
                    ddlCategory.DataValueField = "CAT_ID";
                    ddlCategory.DataBind();

                }
                ListItem l = new ListItem("------ Select Category ------", "0", true);
                l.Selected = true;
                ddlCategory.Items.Insert(0, l);

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void SearchResult()
        {
            try
            {
                string keyword =CpanelUtils.ClearUnicode(txtKeyword.Value);

                var AllList = (from g in DB.ESHOP_NEWs
                               where "" == keyword || DB.fClearUnicode(g.NEWS_TITLE).Contains(keyword) || g.NEWS_DESC.Contains(keyword)
                               orderby g.NEWS_ID descending
                               select g);


                if (AllList.ToList().Count > 0)
                    Session["NewsList"] = DataUtil.LINQToDataTable(AllList);

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

        public string GetNewsStatus(object NewsID, object NewsTitle)
        {
            try
            {
                int _iNewsID = Utils.CIntDef(NewsID);
                var _vComment = DB.GetTable<ESHOP_NEWS_COMMENT>().Where(a=>a.NEWS_ID == _iNewsID && a.COMMENT_CHECK == 0);
                if (_vComment.ToList().Count > 0)
                {
                    return Utils.CStrDef(NewsTitle) + " - <font color='#FF0000'>There is a new feedback</font>";
                }
                else
                {
                    return Utils.CStrDef(NewsTitle);
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }

        private void EventDelete(DataGridCommandEventArgs e)
        {
            try
            {
                int NewsId = Utils.CIntDef(GridItemList.DataKeys[e.Item.ItemIndex]);

                var g_delete = DB.GetTable<ESHOP_NEW>().Where(g => g.NEWS_ID == NewsId);

                DB.ESHOP_NEWs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

                //delete folder
                string fullpath = Server.MapPath(PathFiles.GetPathNews(NewsId));
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
                Response.Redirect("news_list.aspx");
            }
        }

        public string getStatus(object obj_status)
        {
            return Utils.CIntDef(obj_status) == 0 ? "Hide" : "Show";
        }

        public string getLanguage(object News_Language)
        {
            return Utils.CIntDef(News_Language) == 1 ? "Việt Nam" : "All";
        }

        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy HH:mm:ss}", News_PublishDate);
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
                        {}
                        j++;
                    }

                    i++;
                }

                //delete 
                var g_delete = DB.GetTable<ESHOP_NEW>().Where(g => items.Contains(g.NEWS_ID));

                DB.ESHOP_NEWs.DeleteAllOnSubmit(g_delete);
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

        private void DeleteAllFilesInFolder(string folderpath)
        {
            foreach (var f in System.IO.Directory.GetFiles(folderpath))
                System.IO.File.Delete(f);
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
                        strLink = "news.aspx?news_id=" + Utils.CStrDef(GridItemList.DataKeys[i]);
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

            DataTable dataTable = Session["NewsList"] as DataTable;
            DataView sortedView = new DataView(dataTable);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            GridItemList.DataSource = sortedView;
            GridItemList.DataBind();
        }

        //protected void GridItemList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        //{
        //    GridItemList.CurrentPageIndex = e.NewPageIndex;
        //    _count = (Utils.CIntDef(GridItemList.CurrentPageIndex, 0) * GridItemList.PageSize);
        //    GridItemList.DataSource = Session["NewsList"] as DataTable;
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
                e.Item.Cells[9].Attributes.Add("onClick", "return confirm('Do you want to delete?');");
            }

        }

        #endregion

        protected void GridItemList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            GridItemList.CurrentPageIndex = e.NewPageIndex;
            _count = (Utils.CIntDef(GridItemList.CurrentPageIndex, 0) * GridItemList.PageSize);
            GridItemList.DataSource = Session["NewsList"] as DataTable;
            GridItemList.DataBind();
        }

        //gui mail km
        public void SendEmailSMTP(string strSubject, string toAddress, string ccAddress, string bccAddress, string body, bool isHtml, bool isSSL)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["Email"]), Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"]));
                    mail.To.Add(toAddress);
                    if (ccAddress != "")
                    {
                        mail.CC.Add(ccAddress);
                    }
                    if (bccAddress != "")
                    {
                        mail.Bcc.Add(bccAddress);
                    }
                    mail.Subject = strSubject;

                    string str = body;
                    mail.Body = str;
                    mail.IsBodyHtml = isHtml;
                    SmtpClient client = new SmtpClient();
                    client.EnableSsl = isSSL;
                    client.Host = Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailHost"]);
                    client.Port = Utils.CIntDef(System.Configuration.ConfigurationManager.AppSettings["EmailPort"]);
                    client.Credentials = new System.Net.NetworkCredential(Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["Email"]), Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailPassword"]));

                    client.Send(mail);
                }
            }
            catch (SmtpException ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void Send_Mail_Content(string MailContent, string Email)
        {
            try
            {
                string strEmailBody = "";
                string url = string.IsNullOrEmpty(Utils.CStrDef(Session["News_url"])) ? "/tin-tuc/" + Utils.CStrDef(Session["News_seo_url"]) + ".aspx" : Utils.CStrDef(Session["News_url"]);


                strEmailBody = "<html><body>";
                strEmailBody += "Click on the links below to see details.<br />";
                strEmailBody += MailContent;
                strEmailBody += "</body></html>";

                SendEmailSMTP("Please visit the website http://algervina.com", Email, "", "", strEmailBody, true, false);

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        protected void lbtnSendMail_Click(object sender, EventArgs e)
        {
            //int i = 0;
            //int j = 0;
            //HtmlInputCheckBox check = new HtmlInputCheckBox();
            //int[] items = new int[GridItemList.Items.Count];

            //string _sMailContent = string.Empty;

            //try
            //{
            //    foreach (DataGridItem item in GridItemList.Items)
            //    {
            //        check = new HtmlInputCheckBox();
            //        check = (HtmlInputCheckBox)item.Cells[1].FindControl("chkSelect");

            //        if (check.Checked)
            //        {
            //            items[j] = Utils.CIntDef(GridItemList.DataKeys[i]);
            //            try
            //            {
            //                //Lấy nội dung mail
            //                var _v = DB.ESHOP_NEWs.Single(a => a.NEWS_ID == items[j]);
            //                if (_v != null)
            //                {
            //                    _v.NEWS_SENDDATE = DateTime.Now;
            //                    DB.SubmitChanges();

            //                    string link = _v.NEWS_TYPE == 0 ? System.Configuration.ConfigurationManager.AppSettings["URLWebsite1"] + "tin-tuc/" + _v.NEWS_SEO_URL + ".aspx" : System.Configuration.ConfigurationManager.AppSettings["URLWebsite1"] + "san-pham/" + _v.NEWS_SEO_URL + ".aspx";
            //                    _sMailContent += "<a href='" + link + "'>" + _v.NEWS_TITLE + "</a><br />";
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                clsVproErrorHandler.HandlerError(ex);
            //            }
            //            j++;
            //        }

            //        i++;
            //    }
            //    //Gửi mail
            //    var _vEmailReceive = DB.GetTable<ESHOP_MAIL_RECIVE>().Where(a => a.MAIL_ACTIVE == 1);
            //    foreach (var item in _vEmailReceive)
            //    {
            //        Send_Mail_Content(_sMailContent, item.MAIL_NAME);
            //    }
            //    Response.Write("<script LANGUAGE='JavaScript' >alert('Thông báo: Tin tức đã được gửi thành công!');document.location='" + ResolveClientUrl("/cpanel/page/news_list.aspx") + "';</script>");
            //}
            //catch (Exception ex)
            //{
            //    clsVproErrorHandler.HandlerError(ex);
            //}
            //finally
            //{
            //    items = null;
            //    SearchResult();
            //}
        }
        protected void Drchuyenmuc_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                int id = Utils.CIntDef(ddlCategory.SelectedValue);
                var list = (from a in DB.ESHOP_NEWs
                            join b in DB.ESHOP_NEWS_CATs on a.NEWS_ID equals b.NEWS_ID
                            where b.CAT_ID == id
                            select a).OrderByDescending(n => n.NEWS_PUBLISHDATE).ToList();
                GridItemList.DataSource = list;
                GridItemList.DataBind();
                GridItemList.AllowPaging = false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void Change_nguon(object sender, EventArgs e)
        {
            int N_ID = Utils.CIntDef(Ddnguon.SelectedValue, 0);
            if (N_ID == 0)
            {
                var s = DB.ESHOP_NEWs.Where(n => n.NEWS_TYPE == 0).ToList();
                if (s.Count > 0)
                    Session["ProList"] = DataUtil.LINQToDataTable(s);

                GridItemList.DataSource = s;
                if (s.ToList().Count > GridItemList.PageSize)
                    GridItemList.AllowPaging = true;
                else
                    GridItemList.AllowPaging = false;
                GridItemList.DataBind();
            }
            else
            {
                var s = DB.ESHOP_NEWs.Where(n => n.NEWS_TYPE == 1).ToList();
                if (s.Count > 0)
                    Session["ProList"] = DataUtil.LINQToDataTable(s);

                GridItemList.DataSource = s;
                if (s.ToList().Count > GridItemList.PageSize)
                    GridItemList.AllowPaging = true;
                else
                    GridItemList.AllowPaging = false;
                GridItemList.DataBind();
            }
        }
    }
}