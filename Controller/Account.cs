using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Web;
using System.Web.UI;
using vpro.functions;

namespace Controller
{
    public class Account
    {
        #region Decclare
        dbVuonRauVietDataContext db = new dbVuonRauVietDataContext();
        #endregion
        public  bool Login(string Email, string MatKhau)
        {
            var dangnhap = from a in db.ESHOP_CUSTOMERs
                           where a.CUSTOMER_EMAIL == Email && a.CUSTOMER_PW == MatKhau
                           select a;
            if (dangnhap.ToList().Count > 0)
            {
                Load_All_Cuss(Email);
                return true;
            }
            else
                return false;
        }
        private void Load_All_Cuss(string email)
        {
            try
            {
                var _cus = db.GetTable<ESHOP_CUSTOMER>().Where(a => a.CUSTOMER_EMAIL == email);
                if (_cus.ToList().Count > 0)
                {
                    HttpContext.Current.Session["User_Name"] = _cus.ToList()[0].CUSTOMER_FULLNAME;
                    HttpContext.Current.Session["User_ID"] = _cus.ToList()[0].CUSTOMER_ID;
                    HttpContext.Current.Session["User_Phone"] = _cus.ToList()[0].CUSTOMER_PHONE1;
                    HttpContext.Current.Session["User_Email"] = _cus.ToList()[0].CUSTOMER_EMAIL;
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
    }
}
