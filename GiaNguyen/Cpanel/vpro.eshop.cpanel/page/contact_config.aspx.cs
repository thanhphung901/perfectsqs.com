using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.eshop.cpanel.ucControls;
using vpro.functions;
using System.IO;

//Create by LUCNV 23-12-2012

namespace vpro.eshop.cpanel.page
{
    public partial class contact_config : System.Web.UI.Page
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
            SaveHTMLInfo3();
            SaveHTMLInfo4();
            SaveHTMLInfo5();
            SaveHTMLInfo6();
        }

        #endregion

        #region My Function

        private void CreateDirectory()
        {
            m_pathFile = PathFiles.GetPathContact();

            if (!Directory.Exists(Server.MapPath(m_pathFile)))
            {
                Directory.CreateDirectory(Server.MapPath(m_pathFile));
            }

            Session["FileManager"] = m_pathFile;
        }
        private void showFileHTML6()
        {
            string pathFile;
            string strHTMLContent;

            pathFile = Server.MapPath(PathFiles.GetPathContact() + "/map-e.htm");

            if ((File.Exists(pathFile)))
            {
                StreamReader objNewsReader;
                //objNewsReader = New StreamReader(pathFile, System.Text.Encoding.Default)
                objNewsReader = new StreamReader(pathFile);
                strHTMLContent = objNewsReader.ReadToEnd();
                objNewsReader.Close();

                Textarea4.Value = strHTMLContent;
            }
        }
        private void showFileHTML5()
        {
            string pathFile;
            string strHTMLContent;

            pathFile = Server.MapPath(PathFiles.GetPathContact() + "/map-vi.htm");

            if ((File.Exists(pathFile)))
            {
                StreamReader objNewsReader;
                //objNewsReader = New StreamReader(pathFile, System.Text.Encoding.Default)
                objNewsReader = new StreamReader(pathFile);
                strHTMLContent = objNewsReader.ReadToEnd();
                objNewsReader.Close();
                Textarea3.Value = strHTMLContent;
            }
        }
        private void showFileHTML4()
        {
            string pathFile;
            string strHTMLContent;

            pathFile = Server.MapPath(PathFiles.GetPathContact() + "/slogan-e.htm");

            if ((File.Exists(pathFile)))
            {
                StreamReader objNewsReader;
                //objNewsReader = New StreamReader(pathFile, System.Text.Encoding.Default)
                objNewsReader = new StreamReader(pathFile);
                strHTMLContent = objNewsReader.ReadToEnd();
                objNewsReader.Close();

                Textarea2.Value = strHTMLContent;
            }
        }
        private void showFileHTML3()
        {
            string pathFile;
            string strHTMLContent;

            pathFile = Server.MapPath(PathFiles.GetPathContact() + "/slogan-vi.htm");

            if ((File.Exists(pathFile)))
            {
                StreamReader objNewsReader;
                //objNewsReader = New StreamReader(pathFile, System.Text.Encoding.Default)
                objNewsReader = new StreamReader(pathFile);
                strHTMLContent = objNewsReader.ReadToEnd();
                objNewsReader.Close();

                Textarea1.Value = strHTMLContent;
            }
        }

        private void showFileHTML1()
        {
            string pathFile;
            string strHTMLContent;

            pathFile = Server.MapPath(PathFiles.GetPathContact() + "/contact-e.htm");

            if ((File.Exists(pathFile)))
            {
                StreamReader objNewsReader;
                //objNewsReader = New StreamReader(pathFile, System.Text.Encoding.Default)
                objNewsReader = new StreamReader(pathFile);
                strHTMLContent = objNewsReader.ReadToEnd();
                objNewsReader.Close();

                mrk1.Value = strHTMLContent;
            }
        }
        private void showFileHTML()
        {
            string pathFile;
            string strHTMLContent;

            pathFile = Server.MapPath(PathFiles.GetPathContact() + "/contact-vi.htm");

            if ((File.Exists(pathFile)))
            {
                StreamReader objNewsReader;
                //objNewsReader = New StreamReader(pathFile, System.Text.Encoding.Default)
                objNewsReader = new StreamReader(pathFile);
                strHTMLContent = objNewsReader.ReadToEnd();
                objNewsReader.Close();

                mrk.Value = strHTMLContent;
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

                strFileName = PathFiles.GetPathContact() + "/contact-vi.htm";
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

                strFileName = PathFiles.GetPathContact() + "/contact-e.htm";
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
        private void SaveHTMLInfo3()
        {
            try
            {
                string strHTMLFileLocation;
                string strFileName;
                string strHTMLContent;
                StreamWriter fsoFile;

                strFileName = PathFiles.GetPathContact() + "/slogan-vi.htm";
                strHTMLFileLocation = Server.MapPath(strFileName);
                strHTMLContent = Textarea1.Value;

                fsoFile = File.CreateText(strHTMLFileLocation);
                fsoFile.Write(strHTMLContent);
                fsoFile.Close();


            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void SaveHTMLInfo4()
        {
            try
            {
                string strHTMLFileLocation;
                string strFileName;
                string strHTMLContent;
                StreamWriter fsoFile;

                strFileName = PathFiles.GetPathContact() + "/slogan-e.htm";
                strHTMLFileLocation = Server.MapPath(strFileName);
                strHTMLContent = Textarea2.Value;

                fsoFile = File.CreateText(strHTMLFileLocation);
                fsoFile.Write(strHTMLContent);
                fsoFile.Close();


            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void SaveHTMLInfo5()
        {
            try
            {
                string strHTMLFileLocation;
                string strFileName;
                string strHTMLContent;
                StreamWriter fsoFile;

                strFileName = PathFiles.GetPathContact() + "/map-vi.htm";
                strHTMLFileLocation = Server.MapPath(strFileName);
                strHTMLContent = Textarea3.Value;

                fsoFile = File.CreateText(strHTMLFileLocation);
                fsoFile.Write(strHTMLContent);
                fsoFile.Close();


            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void SaveHTMLInfo6()
        {
            try
            {
                string strHTMLFileLocation;
                string strFileName;
                string strHTMLContent;
                StreamWriter fsoFile;

                strFileName = PathFiles.GetPathContact() + "/map-e.htm";
                strHTMLFileLocation = Server.MapPath(strFileName);
                strHTMLContent = Textarea4.Value;

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
                ucHeader.HeaderLevel1 = "Contact Information";
                ucHeader.HeaderLevel1_Url = "../page/contact_list.aspx";
                ucHeader.HeaderLevel2 = "Cấu hình liên hệ";
                ucHeader.HeaderLevel2_Url = "../page/contact_config.aspx";

                showFileHTML();
                showFileHTML1();
                showFileHTML3();
                showFileHTML4();
                showFileHTML5();
                showFileHTML6();
            }

        }

        #endregion

    }
}