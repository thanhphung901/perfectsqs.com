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
using System.IO;

namespace vpro.eshop.cpanel.page
{
    public partial class extensionfiles : System.Web.UI.Page
    {
        #region Declare

        private int m_ext_id = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_ext_id = Utils.CIntDef(Request["ext_id"]);

            if (m_ext_id == 0)
            {
                dvDelete.Visible = false;
                trImage1.Visible = false;
            } 

            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "Product - New";
                ucHeader.HeaderLevel1_Url = "../page/news_list.aspx";
                ucHeader.HeaderLevel2 = "Dạng mở rộng file";
                ucHeader.HeaderLevel2_Url = "../page/extensionfiles.aspx";

                getInfo();
            }

        }

        #endregion

        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }

        protected void lbtSaveNew_Click(object sender, EventArgs e)
        {
            SaveInfo("extensionfiles.aspx");
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
        }

        protected void btnDelete1_Click(object sender, ImageClickEventArgs e)
        {
            string strLink = "";
            try
            {
                var n_info = DB.GetTable<ESHOP_EXT_FILE>().Where(n => n.EXT_FILE_ID == m_ext_id);

                if (n_info.ToList().Count > 0)
                {
                    if (!string.IsNullOrEmpty(n_info.ToList()[0].EXT_FILE_IMAGE))
                    {
                        string imagePath = Server.MapPath(PathFiles.GetPathExt(m_ext_id) + n_info.ToList()[0].EXT_FILE_IMAGE);
                        n_info.ToList()[0].EXT_FILE_IMAGE = "";
                        DB.SubmitChanges();

                        if (File.Exists(imagePath))
                            File.Delete(imagePath);

                        strLink = "extensionfiles.aspx?ext_id=" + m_ext_id;
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

        #region My Functions

        private void getInfo()
        {
            try
            {
                var G_info = (from g in DB.ESHOP_EXT_FILEs
                              where g.EXT_FILE_ID == m_ext_id
                              select g
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtName.Value = G_info.ToList()[0].EXT_FILE_NAME;
                    txtDesc.Value = G_info.ToList()[0].EXT_FILE_DESC;

                    //image
                    if (!string.IsNullOrEmpty(G_info.ToList()[0].EXT_FILE_IMAGE))
                    {
                        trUploadImage1.Visible = false;
                        trImage1.Visible = true;
                        Image1.Src = PathFiles.GetPathExt(m_ext_id) + G_info.ToList()[0].EXT_FILE_IMAGE;
                        hplImage1.NavigateUrl = PathFiles.GetPathExt(m_ext_id) + G_info.ToList()[0].EXT_FILE_IMAGE;
                        hplImage1.Text = G_info.ToList()[0].EXT_FILE_IMAGE;
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
                string Name = txtName.Value;
                string Desc = txtDesc.Value;

                //get image
                string Ext_Image;

                if (trUploadImage1.Visible == true)
                {
                    if (fileImage1.PostedFile != null)
                    {
                        Ext_Image = Path.GetFileName(fileImage1.PostedFile.FileName);
                    }
                    else
                    {
                        Ext_Image = "";
                    }
                }
                else
                {
                    Ext_Image = hplImage1.Text;
                }

                if (m_ext_id == 0)
                {
                    //insert
                    ESHOP_EXT_FILE g_insert = new ESHOP_EXT_FILE();
                    g_insert.EXT_FILE_NAME = Name;
                    g_insert.EXT_FILE_DESC = Desc;
                    g_insert.EXT_FILE_IMAGE = Ext_Image;

                    DB.ESHOP_EXT_FILEs.InsertOnSubmit(g_insert);

                    DB.SubmitChanges();

                    //get new id
                    var _new = DB.GetTable<ESHOP_EXT_FILE>().OrderByDescending(g => g.EXT_FILE_ID).Take(1);

                    m_ext_id = _new.Single().EXT_FILE_ID;

                    strLink = string.IsNullOrEmpty(strLink) ? "extensionfiles_list.aspx" : strLink;
                }
                else
                {
                    //update
                    var g_update = DB.GetTable<ESHOP_EXT_FILE>().Where(g => g.EXT_FILE_ID == m_ext_id);

                    if (g_update.ToList().Count > 0)
                    {
                        g_update.Single().EXT_FILE_NAME = Name;
                        g_update.Single().EXT_FILE_DESC = Desc;
                        g_update.Single().EXT_FILE_IMAGE = Ext_Image;

                        DB.SubmitChanges();

                       
                        strLink = string.IsNullOrEmpty(strLink) ? "extensionfiles_list.aspx" : strLink;
                    }
                }

                //update images
                if (trUploadImage1.Visible)
                {
                    if (fileImage1.PostedFile != null)
                    {
                        string pathfile = Server.MapPath("../data/ext_files/" + m_ext_id);
                        string fullpathfile = pathfile + "/" + Ext_Image;

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
            string strLink = "";
            try
            {
                var G_info = DB.GetTable<ESHOP_EXT_FILE>().Where(g => g.EXT_FILE_ID == m_ext_id);

                DB.ESHOP_EXT_FILEs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();


                //delete folder
                string fullpath = Server.MapPath(PathFiles.GetPathExt(m_ext_id));
                if (Directory.Exists(fullpath))
                {
                    DeleteAllFilesInFolder(fullpath);
                    Directory.Delete(fullpath);
                }

                strLink="extensionfiles_list.aspx";

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

    }
}