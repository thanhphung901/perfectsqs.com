using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.IO;
using vpro.eshop.cpanel.Components;
using System.Data;
using vpro.eshop.cpanel.ucControls;

namespace vpro.eshop.cpanel.page
{
    public partial class color : System.Web.UI.Page
    {

        #region Declare

        private int m_color_id = 0;
        int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_color_id = Utils.CIntDef(Request["color_id"]);

            if (m_color_id == 0)
            {
                dvDelete.Visible = false;
            }

            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "Màu sắc";
                ucHeader.HeaderLevel1_Url = "../page/color_list.aspx";
                ucHeader.HeaderLevel2 = "Add new/Update màu sắc";
                ucHeader.HeaderLevel2_Url = "../page/color.aspx";

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
            SaveInfo("color.aspx");
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
        }

        #endregion

        #region My functions

        //private void LoadCategoryParent()
        //{
        //    try
        //    {
        //        var CatList = (
        //                        from t2 in DB.ESHOP_PROPERTies
        //                        select new
        //                        {
        //                            PROP_ID = t2.PROP_NAME == "------- Root -------" ? 0 : t2.PROP_ID,
        //                            PROP_NAME = t2.PROP_NAME,
        //                            PROP_PARENT_ID = t2.PROP_PARENT_ID,
        //                            PROP_RANK = t2.PROP_RANK

        //                        }
        //                    );

        //        if (CatList.ToList().Count > 0)
        //        {
        //            DataRelation relCat;
        //            DataTable tbl = DataUtil.LINQToDataTable(CatList);
        //            DataSet ds = new DataSet();
        //            ds.Tables.Add(tbl);

        //            tbl.PrimaryKey = new DataColumn[] { tbl.Columns["PROP_ID"] };
        //            relCat = new DataRelation("Category_parent", ds.Tables[0].Columns["PROP_ID"], ds.Tables[0].Columns["PROP_PARENT_ID"], false);

        //            ds.Relations.Add(relCat);
        //            DataSet dsCat = ds.Clone();
        //            DataTable CatTable = ds.Tables[0];

        //            CpanelUtils.TransformTableWithSpace(ref CatTable, dsCat.Tables[0], relCat, null);

        //            ddlCategory.DataSource = dsCat.Tables[0];
        //            ddlCategory.DataTextField = "PROP_NAME";
        //            ddlCategory.DataValueField = "PROP_ID";
        //            ddlCategory.DataBind();

        //        }
        //        else
        //        {
        //            DataTable dt = new DataTable("Newtable");

        //            dt.Columns.Add(new DataColumn("PROP_ID"));
        //            dt.Columns.Add(new DataColumn("PROP_NAME"));

        //            DataRow row = dt.NewRow();
        //            row["PROP_ID"] = 0;
        //            row["PROP_NAME"] = "--------Root--------";
        //            dt.Rows.Add(row);

        //            ddlCategory.DataTextField = "PROP_NAME";
        //            ddlCategory.DataValueField = "PROP_ID";
        //            ddlCategory.DataSource = dt;
        //            ddlCategory.DataBind();



        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        clsVproErrorHandler.HandlerError(ex);
        //    }
        //}

        private void getInfo()
        {
            try
            {
                //LoadCategoryParent();

                var G_info = (from c in DB.ESHOP_COLORs
                              where c.COLOR_ID == m_color_id
                              select c
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtName.Value = G_info.ToList()[0].COLOR_NAME;
                    txtDesc.Value = G_info.ToList()[0].COLOR_DESC;
                    rblStatus.SelectedValue = Utils.CStrDef(G_info.ToList()[0].COLOR_ACTIVE);
                    txtOrder.Value = Utils.CStrDef(G_info.ToList()[0].COLOR_PRIORITY);
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
                string Color_Name = txtName.Value;
                string Color_Desc = txtDesc.Value;
                int Color_Active = Utils.CIntDef(rblStatus.SelectedValue);
                int Color_Priority = Utils.CIntDef(txtOrder.Value);

                if (m_color_id == 0)
                {
                    //insert

                    ESHOP_COLOR cat_insert = new ESHOP_COLOR();

                    cat_insert.COLOR_NAME = Color_Name;
                    cat_insert.COLOR_DESC = Color_Desc;
                    cat_insert.COLOR_ACTIVE = Color_Active;
                    cat_insert.COLOR_PRIORITY = Color_Priority;

                    DB.ESHOP_COLORs.InsertOnSubmit(cat_insert);
                    DB.SubmitChanges();

                    var _cat = DB.GetTable<ESHOP_COLOR>().OrderByDescending(g => g.COLOR_ID).Take(1);

                    m_color_id = _cat.Single().COLOR_ID;

                    strLink = string.IsNullOrEmpty(strLink) ? "color.aspx?color_id=" + m_color_id : strLink;
                }
                else
                {
                    //update
                    var c_update = DB.GetTable<ESHOP_COLOR>().Where(g => g.COLOR_ID == m_color_id);

                    if (c_update.ToList().Count > 0)
                    {
                        c_update.Single().COLOR_NAME = Color_Name;
                        c_update.Single().COLOR_DESC = Color_Desc;
                        c_update.Single().COLOR_PRIORITY = Color_Priority;
                        c_update.Single().COLOR_ACTIVE = Color_Active;

                        DB.SubmitChanges();

                        strLink = string.IsNullOrEmpty(strLink) ? "color_list.aspx" : strLink;
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
            try
            {
                var G_info = DB.GetTable<ESHOP_COLOR>().Where(g => g.COLOR_ID == m_color_id);

                DB.ESHOP_COLORs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                //delete folder
                string fullpath = Server.MapPath(PathFiles.GetPathCategory(m_color_id));
                if (Directory.Exists(fullpath))
                {
                    DeleteAllFilesInFolder(fullpath);
                    Directory.Delete(fullpath);
                }

                Response.Redirect("color_list.aspx");

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void DeleteAllFilesInFolder(string folderpath)
        {
            foreach (var f in System.IO.Directory.GetFiles(folderpath))
                System.IO.File.Delete(f);
        }

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        public string getLink(object GroupId)
        {
            return "color.aspx?color_id=" + Utils.CStrDef(GroupId);
        }

        #endregion
    }
}