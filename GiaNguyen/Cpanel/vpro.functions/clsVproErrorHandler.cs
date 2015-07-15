using System;
using System.IO;
using System.Web;

namespace vpro.functions
{
    public class clsVproErrorHandler
    {
        public static void LogError(string Filename, Exception E)
        {
            string lastError = "";
            const String format = "dd/MM/yyyy hh:mm:ss";
            try
            {
                if (File.Exists(Filename))
                {
                    StreamReader f = File.OpenText(Filename);
                    lastError = f.ReadToEnd();
                    f.Close();
                }
                using (StreamWriter LogF = File.CreateText(Filename))
                {
                    LogF.WriteLine("[Date {0}]", DateTime.Now.ToString(format));
                    LogF.WriteLine(E.ToString());
                    LogF.WriteLine("-----------------------------------------------");
                    if ((lastError.CompareTo("")) != 0)
                    {
                        LogF.Write(lastError);
                    }
                    LogF.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void HandlerError(Exception E)
        {
            string strFileName = "";
            strFileName = HttpContext.Current.Request.PhysicalApplicationPath + System.Configuration.ConfigurationManager.AppSettings["LogFiles"];
            LogError(strFileName, E);
        }
    }
}