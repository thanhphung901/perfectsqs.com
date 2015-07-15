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
    public partial class customer : System.Web.UI.Page
    {

        #region Declare

        private int m_customer_id = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_customer_id = Utils.CIntDef(Request["customer_id"]);

            if (m_customer_id == 0)
            {
                dvDelete.Visible = false;
            }

            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "Khách hàng";
                ucHeader.HeaderLevel1_Url = "../page/customer_list.aspx";
                ucHeader.HeaderLevel2 = "Thông tin khách hàng";
                ucHeader.HeaderLevel2_Url = "../page/customer_list.aspx";
                getInfo();
            }

        }

        #endregion

        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            //if (CheckExits(txtCustomerUN.Value))
            //    lblError.Text = "Đã tồn tại Tên Logon, vui lòng nhập Tên Logon khác.";
            //else
                SaveInfo();
        }

        protected void lbtSaveNew_Click(object sender, EventArgs e)
        {
            //if (CheckExits(txtCustomerUN.Value))
            //    lblError.Text = "Đã tồn tại Tên Logon, vui lòng nhập Tên Logon khác.";
            //else
                SaveInfo("customer.aspx");
        }

        private bool CheckExits(string strUN)
        {
            try
            {
                if (m_customer_id == 0)
                {
                    var exits = (from c in DB.ESHOP_CUSTOMERs where c.CUSTOMER_UN == strUN select c);

                    if (exits.ToList().Count > 0)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {  clsVproErrorHandler.HandlerError(ex);
                return false;
              
            }
        }
        

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
        }

        #endregion

        #region My Functions
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy}", News_PublishDate);
        }
        private void getInfo()
        {
            try
            {
                var G_info = (from g in DB.ESHOP_CUSTOMERs
                              where g.CUSTOMER_ID == m_customer_id 
                              select g
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtCustomerName.Value =Utils.CStrDef(G_info.ToList()[0].CUSTOMER_FULLNAME);
                    txtCustomerAddress.Value = Utils.CStrDef(G_info.ToList()[0].CUSTOMER_ADDRESS);
                    txtCustomerPhone1.Value = Utils.CStrDef(G_info.ToList()[0].CUSTOMER_PHONE1);
                    txtCustomerEmail.Value = Utils.CStrDef(G_info.ToList()[0].CUSTOMER_EMAIL);
                    rblsex.SelectedValue = Utils.CStrDef(G_info.ToList()[0].CUSTOMER_FIELD3);
                    txtngaysinh.Value = getDate(G_info.ToList()[0].CUSTOMER_UPDATE);
                    if (G_info.ToList()[0].CUSTOMER_ADDRESS != null)
                    {
                        string s = G_info.ToList()[0].CUSTOMER_FIELD1;
                        var city = DB.ESHOP_PROPERTies.Where(n => n.PROP_ID.ToString() == s).ToList();
                        if (city.Count > 0)
                        {
                            Txtcity.Value = city[0].PROP_NAME;
                        }
                    }
                    if (G_info.ToList()[0].CUSTOMER_FIELD1 != null)
                    {
                        string s = G_info.ToList()[0].CUSTOMER_FIELD2;
                        var city = DB.ESHOP_PROPERTies.Where(n => n.PROP_ID.ToString() == s).ToList();
                        if (city.Count > 0)
                        {
                            Txtquan.Value = city[0].PROP_NAME;
                        }
                    }
                    //txtCustomerUN.Value = Utils.CStrDef(G_info.ToList()[0].USER_UN);
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

                var g_update = DB.GetTable<ESHOP_USER>().Where(g => g.USER_ID == m_customer_id);

                if (g_update.ToList().Count > 0)
                {

                    g_update.Single().USER_NAME = txtCustomerName.Value.ToString();
                    g_update.Single().USER_PHONE = txtCustomerPhone1.Value;
                    g_update.Single().USER_EMAIL = txtCustomerEmail.Value;
                    //g_update.Single().USER_FIELD1 = Txtsex.Value;
                    //g_update.Single().USER_UN = txtCustomerUN.Value;
                    g_update.Single().USER_DATE = DateTime.Now;
                    DB.SubmitChanges();

                    strLink = string.IsNullOrEmpty(strLink) ? "customer_list.aspx" : strLink;
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
            try
            {
                var G_info = DB.GetTable<ESHOP_CUSTOMER>().Where(g => g.CUSTOMER_ID == m_customer_id);

                DB.ESHOP_CUSTOMERs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                Response.Redirect("customer_list.aspx");

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        #endregion

    }
}