using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using vpro.eshop.cpanel.ucControls;
using vpro.functions;
using System.IO;

namespace vpro.eshop.cpanel.page
{
    public partial class config_footer : System.Web.UI.Page
    {
        #region Declare

        string m_pathFile = "";
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveHTMLInfo();
            SaveHTMLInfo1();
        }

        #endregion

        #region My Function

        private void CreateDirectory()
        {
            m_pathFile = PathFiles.GetPathFooter();

            if (!Directory.Exists(Server.MapPath(m_pathFile)))
            {
                Directory.CreateDirectory(Server.MapPath(m_pathFile));
            }

            Session["FileManager"] = m_pathFile;
        }

        private void showFileHTML()
        {
            try
            {
                string pathFile;
                string strHTMLContent;
                pathFile = Server.MapPath(PathFiles.GetPathFooter() + "/footer-vi.htm");
                if ((File.Exists(pathFile)))
                {
                    StreamReader objNewsReader;
                    //objNewsReader = New StreamReader(pathFile, System.Text.Encoding.Default)
                    objNewsReader = new StreamReader(pathFile);
                    strHTMLContent = objNewsReader.ReadToEnd();
                    objNewsReader.Close();

                    mrk.Value = strHTMLContent;
                    //mrkEn.Value = strHTMLContentEn;
                }
            }
            catch (Exception ex) {
                clsVproErrorHandler.HandlerError(ex);

            }
        }
        private void showFileHTML1()
        {
            try
            {
                string pathFile;
                string strHTMLContent;
                pathFile = Server.MapPath(PathFiles.GetPathFooter() + "/footer-e.htm");
                if ((File.Exists(pathFile)))
                {
                    StreamReader objNewsReader;
                    //objNewsReader = New StreamReader(pathFile, System.Text.Encoding.Default)
                    objNewsReader = new StreamReader(pathFile);
                    strHTMLContent = objNewsReader.ReadToEnd();
                    objNewsReader.Close();

                    mrk1.Value = strHTMLContent;
                    //mrkEn.Value = strHTMLContentEn;
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);

            }
        }
        private void SaveHTMLInfo()
        {
            try
            {
                string strHTMLFileLocation;
                string strFileName;
                string strHTMLContent;
                StreamWriter fsoFile;

                strFileName = PathFiles.GetPathFooter() + "/footer-vi.htm";
                strHTMLFileLocation = Server.MapPath(strFileName);
                strHTMLContent = mrk.Value;


                fsoFile = File.CreateText(strHTMLFileLocation);
                fsoFile.Write(strHTMLContent);
                fsoFile.Close();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void SaveHTMLInfo1()
        {
            try
            {
                string strHTMLFileLocation;
                string strFileName;
                string strHTMLContent;
                StreamWriter fsoFile;

                strFileName = PathFiles.GetPathFooter() + "/footer-e.htm";
                strHTMLFileLocation = Server.MapPath(strFileName);
                strHTMLContent = mrk1.Value;


                fsoFile = File.CreateText(strHTMLFileLocation);
                fsoFile.Write(strHTMLContent);
                fsoFile.Close();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {

            CreateDirectory();

            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "Cấu hình website";
                ucHeader.HeaderLevel1_Url = "../page/config_meta.aspx";
                ucHeader.HeaderLevel2 = "Cấu hình Footer";
                ucHeader.HeaderLevel2_Url = "../page/config_footer.aspx";

                showFileHTML();
                showFileHTML1();
            }

        }

        #endregion

    }
}