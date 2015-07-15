using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.Linq.SqlClient;
using vpro.functions;
using System.Text.RegularExpressions;

namespace Controller
{
    public class Search_result
    {
        #region Decclare
        dbVuonRauVietDataContext db = new dbVuonRauVietDataContext();
        #endregion
        public List<Pro_details_entity> Load_search_result(string _txt,int type)
        {
            List<Pro_details_entity> l = new List<Pro_details_entity>();
            var list = (from c in db.ESHOP_NEWS_CATs
                          join a in db.ESHOP_NEWs on c.NEWS_ID equals a.NEWS_ID
                          join b in db.ESHOP_CATEGORies on c.CAT_ID equals b.CAT_ID
                          where (SqlMethods.Like(a.NEWS_KEYWORD_ASCII, ClearUnicode(_txt)) || "" == _txt || "%%" == _txt)
                          && a.NEWS_TYPE == type
                          select new { a.NEWS_ID, a.NEWS_TITLE, a.NEWS_IMAGE3,a.NEWS_PRICE1, a.NEWS_DESC, a.NEWS_SEO_URL, a.NEWS_URL, a.NEWS_ORDER, a.NEWS_ORDER_PERIOD, a.NEWS_PUBLISHDATE, b.CAT_SEO_URL }).Distinct().OrderByDescending(n => n.NEWS_ID).OrderByDescending(n => n.NEWS_ORDER);
            foreach (var i in list)
            {
                Pro_details_entity pro = new Pro_details_entity();
                pro.NEWS_ID = i.NEWS_ID;
                pro.NEWS_TITLE = i.NEWS_TITLE;
                pro.NEWS_IMAGE3 = i.NEWS_IMAGE3;
                pro.NEWS_DESC = i.NEWS_DESC;
                pro.NEWS_SEO_URL = i.NEWS_SEO_URL;
                pro.NEWS_URL = i.NEWS_URL;
                pro.NEWS_ORDER = Utils.CIntDef(i.NEWS_ORDER);
                pro.NEWS_ORDER_PERIOD = Utils.CIntDef(i.NEWS_ORDER_PERIOD);
                pro.NEWS_PRICE1 = Utils.CDecDef(i.NEWS_PRICE1);
                pro.NEWS_PUBLISHDATE = Utils.CDateDef(i.NEWS_PUBLISHDATE, DateTime.Now);
                pro.CAT_SEO_URL = i.CAT_SEO_URL;
                l.Add(pro);
            }
            return l;

        }
        private  string ClearUnicode(string SourceString)
        {

            SourceString = Regex.Replace(SourceString, "[ÂĂÀÁẠẢÃÂẦẤẬẨẪẰẮẶẲẴàáạảãâầấậẩẫăằắặẳẵ]", "a");
            SourceString = Regex.Replace(SourceString, "[ÈÉẸẺẼÊỀẾỆỂỄèéẹẻẽêềếệểễ]", "e");
            SourceString = Regex.Replace(SourceString, "[IÌÍỈĨỊìíịỉĩ]", "i");
            SourceString = Regex.Replace(SourceString, "[ÒÓỌỎÕÔỒỐỔỖỘƠỜỚỞỠỢòóọỏõôồốộổỗơờớợởỡ]", "o");
            SourceString = Regex.Replace(SourceString, "[ÙÚỦŨỤƯỪỨỬỮỰùúụủũưừứựửữ]", "u");
            SourceString = Regex.Replace(SourceString, "[ỲÝỶỸỴỳýỵỷỹ]", "y");
            SourceString = Regex.Replace(SourceString, "[đĐ]", "d");

            return SourceString;
        }
    }
}
