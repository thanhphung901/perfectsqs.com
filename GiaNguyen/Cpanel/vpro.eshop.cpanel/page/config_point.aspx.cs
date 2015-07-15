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

namespace vpro.eshop.cpanel.page
{

    //Code by Tran Van Minh 27/12/2012
    public partial class config_point : System.Web.UI.Page
    {

        #region Declare

        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
    

            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "Cấu hình website";
                ucHeader.HeaderLevel1_Url = "../page/config_meta.aspx";
                ucHeader.HeaderLevel2 = "Cấu hình tích lũy điểm";
                ucHeader.HeaderLevel2_Url = "../page/config_point_list.aspx";


                getInfo();
            }

        }

        #endregion

        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }

        #endregion

        #region My Functions

        private void getInfo()
        {
            try
            {
                var G_info = (from g in DB.ESHOP_POINTs
                              select g
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtPointToMoney.Value = G_info.ToList()[0].POINT_POINT_TO_MONEY.ToString();
                    txtMoneyToPoint.Value = G_info.ToList()[0].POINT_MONEY_TO_POINT.ToString();
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
                int PointToMoney = Utils.CIntDef(txtPointToMoney.Value);
                int MoneyToPoint = Utils.CIntDef(txtMoneyToPoint.Value);

                    //update
                    var g_update = DB.GetTable<ESHOP_POINT>().ToList();

                    if (g_update.ToList().Count > 0)
                    {
                        g_update.Single().POINT_POINT_TO_MONEY = PointToMoney;
                        g_update.Single().POINT_MONEY_TO_POINT = MoneyToPoint;
                        g_update.Single().POINT_UPDATEDATE = DateTime.Now;
                        DB.SubmitChanges();

                       
                    }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                if (!string.IsNullOrEmpty(strLink))
                { Response.Redirect("~/page/config.aspx"); }
            }
        }


        #endregion

    }
}