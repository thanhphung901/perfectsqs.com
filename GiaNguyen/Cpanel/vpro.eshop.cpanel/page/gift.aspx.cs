using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using vpro.functions;
using System.Data;
using vpro.eshop.cpanel.ucControls;
using System.IO;

namespace vpro.eshop.cpanel.page
{
    public partial class gift : System.Web.UI.Page
    {

        #region Declare

        private int m_gift_id = 0;
        int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_gift_id = Utils.CIntDef(Request["gift_id"]);
           
            if (m_gift_id == 0)
            {
                dvDelete.Visible = false;
                trImage1.Visible = false;
              
            }
           

            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "New - Product";
                ucHeader.HeaderLevel1_Url = "../page/news_list.aspx";
                ucHeader.HeaderLevel2 = "Add new/Update quà tặng";
                ucHeader.HeaderLevel2_Url = "../page/news.aspx";

                getInfo();
            }

        }

        #endregion

        #region Button Events

      

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
        }

        protected void btnDelete1_Click(object sender, ImageClickEventArgs e)
        {
            string strLink = "";
            try
            {
                var n_info = DB.GetTable<ESHOP_GIFT>().Where(n => n.GIFT_ID == m_gift_id);

                if (n_info.ToList().Count > 0)
                {
                    if (!string.IsNullOrEmpty(n_info.ToList()[0].GIFT_IMAGE))
                    {
                        string imagePath = Server.MapPath(PathFiles.GetPathNews(m_gift_id) + n_info.ToList()[0].GIFT_IMAGE);
                        n_info.ToList()[0].GIFT_IMAGE = "";
                        DB.SubmitChanges();

                        if (File.Exists(imagePath))
                            File.Delete(imagePath);

                        strLink = "gift.aspx?gift_id=" + m_gift_id;
                    }
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

        #region My functions

   
      

        private void getInfo()
        {
            try
            {
                  
                var G_info = (from g in DB.ESHOP_GIFTs
                              where g.GIFT_ID == m_gift_id
                              select new {
                                  g
                              }
                            );

                if (G_info.ToList().Count > 0)
                {
                   
                    txtGiftName.Text = G_info.ToList()[0].g.GIFT_NAME;
                    txtGiftDesc.Value = G_info.ToList()[0].g.GIFT_DESC;
                    txtGiftPoint.Value = Utils.CStrDef(G_info.ToList()[0].g.GIFT_POINT);
                    txtGridAmount.Value = Utils.CStrDef(G_info.ToList()[0].g.GIFT_AMOUNT);
                    txtGridMaxAmount.Value = Utils.CStrDef(G_info.ToList()[0].g.GIFT_MAX_AMOUNT);
                    txtGiftContent.Value = Utils.CStrDef(G_info.ToList()[0].g.GIFT_CONTENT);
                    //image
                    if (!string.IsNullOrEmpty(G_info.ToList()[0].g.GIFT_IMAGE))
                    {
                        trUploadImage1.Visible = false;
                        trImage1.Visible = true;
                        Image1.Src = PathFiles.GetPathNews(m_gift_id) + G_info.ToList()[0].g.GIFT_IMAGE;
                        hplImage1.NavigateUrl = PathFiles.GetPathNews(m_gift_id) + G_info.ToList()[0].g.GIFT_IMAGE;
                        hplImage1.Text = G_info.ToList()[0].g.GIFT_IMAGE; 
                    }
                    else
                    {
                        trUploadImage1.Visible = true;
                        trImage1.Visible = false;
                    }
                }
                

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void SaveInfo(string strLink = "")
        {
            try
            {
                //get image
                string News_Image1;

                if (trUploadImage1.Visible == true)
                {
                    if (fileImage1.PostedFile != null)
                    {
                        News_Image1 = Path.GetFileName(fileImage1.PostedFile.FileName);
                    }
                    else
                    {
                        News_Image1 = "";
                    }
                }
                else
                {
                    News_Image1 = hplImage1.Text;
                }

                if (m_gift_id == 0)
                {

                    //insert
                    ESHOP_GIFT gift_insert  = new ESHOP_GIFT();

                    gift_insert .GIFT_NAME =txtGiftName.Text;
                    gift_insert .GIFT_DESC = txtGiftDesc.Value;
                    gift_insert .GIFT_POINT = Utils.CIntDef(txtGiftPoint.Value);

                    gift_insert.GIFT_AMOUNT = Utils.CIntDef(txtGridAmount.Value);
                    gift_insert.GIFT_MAX_AMOUNT = Utils.CIntDef(txtGridMaxAmount.Value);
                    gift_insert.GIFT_CONTENT = Utils.CStrDef(txtGiftContent.Value);
                    
                    
                    gift_insert .GIFT_IMAGE = News_Image1;

                    
                    gift_insert .GIFT_PULISHDATE = DateTime.Now;
                    //english language

                    DB.ESHOP_GIFTs.InsertOnSubmit(gift_insert);
                    DB.SubmitChanges();

                    //update cat news
                    var _new = DB.GetTable<ESHOP_GIFT>().OrderByDescending(g => g.GIFT_ID).Take(1);

                    m_gift_id = _new.Single().GIFT_ID;
               
                    strLink = string.IsNullOrEmpty(strLink) ? "gift.aspx?gift_id=" + m_gift_id : strLink;
                }
                else
                {
                    //update
                    var c_update = DB.GetTable<ESHOP_GIFT>().Where(g => g.GIFT_ID == m_gift_id);

                    if (c_update.ToList().Count > 0)
                    {
                        c_update.ToList()[0].GIFT_NAME = txtGiftName.Text;
                        c_update.ToList()[0].GIFT_DESC = txtGiftDesc.Value;
                        c_update.ToList()[0].GIFT_POINT =Utils.CIntDef(txtGiftPoint.Value);
                        c_update.ToList()[0].GIFT_AMOUNT = Utils.CIntDef(txtGridAmount.Value);
                        c_update.ToList()[0].GIFT_MAX_AMOUNT = Utils.CIntDef(txtGridMaxAmount.Value);
                        c_update.ToList()[0].GIFT_CONTENT = Utils.CStrDef(txtGiftContent.Value);
                        c_update.ToList()[0].GIFT_PULISHDATE = DateTime.Now;
                        c_update.ToList()[0].GIFT_IMAGE = News_Image1;

                    

                        DB.SubmitChanges();

                        strLink = string.IsNullOrEmpty(strLink) ? "gift_list.aspx" : strLink;
                    }
                }

                //update images
                if (trUploadImage1.Visible)
                {
                    if (!string.IsNullOrEmpty(fileImage1.PostedFile.FileName))
                    {
                        string pathfile = Server.MapPath("../data/news/" + m_gift_id);
                        string fullpathfile = pathfile + "/" + News_Image1;

                        if (!Directory.Exists(pathfile))
                        {
                            Directory.CreateDirectory(pathfile);
                        }

                        fileImage1.PostedFile.SaveAs(fullpathfile);
                    }

                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                if (!string.IsNullOrEmpty(strLink))
                { Response.Redirect(strLink); }
            }
        }

        private void DeleteInfo()
        {
            string strLink="";
            try
            {
                var G_info = DB.GetTable<ESHOP_GIFT>().Where(g => g.GIFT_ID == m_gift_id);

                DB.ESHOP_GIFTs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                //delete folder
                string fullpath = Server.MapPath(PathFiles.GetPathNews(m_gift_id));
                if (Directory.Exists(fullpath))
                {
                    DeleteAllFilesInFolder(fullpath);
                    Directory.Delete(fullpath);
                }

                strLink = "gift_list.aspx";

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

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        public string getLink(object GroupId)
        {
            return "gift.aspx?gift_id=" + Utils.CStrDef(GroupId);
        }

     
        #endregion

        protected void lbtSaveNew_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }
    }
}