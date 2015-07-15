using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace vpro.functions
{
    public static class Utils
    {
        public static int CIntDef(object Expression, int DefaultValue=0)
        {
            try
            {
                return System.Convert.ToInt32(Expression);
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        public static string CStrGuid(object Expression, string DefaultValue="")
        {
            try
            {
                Guid g;
                g = new Guid(Expression.ToString());
                return Expression.ToString();
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        public static Guid CGuidDef(object Expression, string DefaultValue = "00000000-0000-0000-0000-000000000000")
        {
            try
            {
                Guid g;
                g = new Guid(Expression.ToString());
                return g;
            }
            catch (Exception)
            {
                Guid g;
                g = new Guid(DefaultValue);
                return g;
            }
        }

        public static long CLngDef(object Expression, int DefaultValue=0)
        {
            try
            {
                return System.Convert.ToInt32(Expression);
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        public static bool CBoolDef(object Experssion, bool DefaultValue=false)
        {
            try
            {
                return System.Convert.ToBoolean(Experssion);
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        public static decimal CDecDef(object Expression, decimal DefaultValue=0)
        {
            try
            {
                return System.Convert.ToDecimal(Expression);
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        public static double CDblDef(object Expression, double DefaultValue=0)
        {
            try
            {
                return System.Convert.ToDouble(Expression);
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        public static DateTime CDateDef(object Expression, DateTime DefaultValue)
        {
            try
            {
                return System.Convert.ToDateTime(Expression);
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        public static string CStrDef(object Expression, string DefaultValue = "")
        {
            try
            {
                return Expression.ToString().Trim();
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }
    }

    public static class Common
    {
        public static string Encrypt(string cleanString, string salt)
        {
            System.Text.Encoding encoding;
            byte[] clearBytes = null;
            byte[] hashedBytes = null;
            encoding = System.Text.Encoding.GetEncoding("unicode");
            clearBytes = encoding.GetBytes(salt.ToLower().Trim() + cleanString.Trim());
            hashedBytes = MD5hash(clearBytes);
            return BitConverter.ToString(hashedBytes);
        }

        public static string CreateSalt()
        {
            byte[] bytSalt = new byte[9];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytSalt);
            return Convert.ToBase64String(bytSalt);
        }

        public static byte[] MD5hash(byte[] data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);
            return result;
        }

        public static string getOnClickScript(string ctrlName)
        {
            string strScript = "";
            strScript = "if(event.which || event.keyCode || event.charCode){if ((event.charCode == 13) || (event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + ctrlName + "').focus();return false;}} else {return true}; ";
            return strScript;
        }

        public static string getSubmitScript(string btnName)
        {
            string strScript = "";
            strScript = "if(event.which || event.keyCode || event.charCode){if ((event.charCode == 13) || (event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnName + "').click();return false;}} else {return true}; ";
            return strScript;
        }

        public static string getPriceValue(object value)
        {
            return Utils.CDecDef(value, 0) == 0 ? "0" : string.Format("{0:#,#}", value);
        }

    }

    public static class PathFiles
    {
        public static string GetPathObject()
        {
            return System.Configuration.ConfigurationManager.AppSettings["Subweb"] + "/data";
        }

        public static string GetPathNews(int news_id)
        {
            return GetPathObject() + "/news/" + news_id.ToString() + "/";
        }

        public static string GetPathCategory(int cat_id)
        {
            return GetPathObject() + "/categories/" + cat_id.ToString() + "/";
        }

        public static string GetPathAdItems(int ad_id)
        {
            return GetPathObject() + "/aditems/" + ad_id.ToString() + "/";
        }

        public static string GetPathExt(int ad_id)
        {
            return GetPathObject() + "/ext_files/" + ad_id.ToString() + "/";
        }

        public static string GetPathFooter()
        {
            return GetPathObject() + "/footer/";
        }

        public static string GetPathContact()
        {
            return GetPathObject() + "/contact/";
        }

        public static string GetPathConfigs()
        {
            return GetPathObject() + "/configs/";
        }

        public static string GetPathBanner(int banner_id)
        {
            return GetPathObject() + "/configs/" + banner_id.ToString() + "/";
        }

        public static string GetPathOnline(int online_id)
        {
            return GetPathObject() + "/onlines/" + online_id.ToString() + "/";
        }
    }
}