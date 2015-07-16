using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controller;
using GiaNguyen.Components;
using System.Web.UI.HtmlControls;
using System.IO;
using vpro.functions;

namespace perfectsqs.com.vi_vn
{
    public partial class Contact : System.Web.UI.Page
    {
        #region Declare
        Config cf = new Config();
        SendMail sm = new SendMail();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Show_File_HTML("contact-vi.htm");
            }
            var _configs = cf.Config_meta();

            if (_configs.ToList().Count > 0)
            {
                if (!string.IsNullOrEmpty(_configs.ToList()[0].CONFIG_FAVICON))
                    ltrFavicon.Text = "<link rel='shortcut icon' href='" + PathFiles.GetPathConfigs() + _configs.ToList()[0].CONFIG_FAVICON + "' />";
            }

            HtmlHead header = base.Header;
            HtmlMeta headerDes = new HtmlMeta();
            HtmlMeta headerKey = new HtmlMeta();
            headerDes.Name = "Description";
            headerKey.Name = "Keywords";

            header.Title = "Contact";
        }
        //private void Show_File_HTML(string HtmlFile)
        //{
        //    string pathFile;
        //    string strHTMLContent;
        //    pathFile = Server.MapPath("../Data/contact/" + HtmlFile);

        //    if ((File.Exists(pathFile)))
        //    {
        //        StreamReader objNewsReader;
        //        objNewsReader = new StreamReader(pathFile);
        //        strHTMLContent = objNewsReader.ReadToEnd();
        //        objNewsReader.Close();

        //        Literal1.Text = strHTMLContent;
        //    }
        //    else
        //        Literal1.Text = "";

        //}

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
            string _sEmailCC = string.Empty;
            string _sEmail = txtEmail.Value;
            string _sName = txtName.Value;
            string _phone = txtPhone.Value;
            string _content = txtMessage.Value;
            string _title = txtSubject.Value;
            cf.Insert_contact(_sName, _sEmail, _title, _phone, _content);
            //string _mailBody = string.Empty;
            //_mailBody += "<br/><br/><strong>Tên khách hàng</strong>: " + _sName;
            //_mailBody += "<br/><br/><strong>Email</strong>: " + _sEmail;
            //_mailBody += "<br/><br/><strong>Số điện thoại</strong>: " + _phone;
            //_mailBody += "<br/><br/><strong>Địa chỉ</strong>: " + _add;
            //_mailBody += "<br/><br/><strong>Nội dung</strong>: " + _content + "<br/><br/>";
            //string _sMailBody = string.Empty;
            //_sMailBody += "Cám ơn quý khách: " + _sName + " đã đặt liên hệ với chúng tôi. Đây là email được gửi từ website của " + System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"] + " <br>" + _mailBody;
            //_sEmailCC = cf.Getemail(2).Count > 0 ? cf.Getemail(2)[0].EMAIL_TO : "";
            //sm.SendEmailSMTP("Thông báo: Bạn đã liên hệ thành công", _sEmail, _sEmailCC, "", _sMailBody, true, false);
            string strScript = "<script>";
            strScript += "alert(' Thank you for your request for us!');";
            strScript += "window.location='/';";
            strScript += "</script>";
            Page.RegisterClientScriptBlock("strScript", strScript);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

    }
}