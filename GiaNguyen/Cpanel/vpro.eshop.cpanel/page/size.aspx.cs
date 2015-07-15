using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using vpro.eshop.cpanel.ucControls;
using System.IO;

namespace vpro.eshop.cpanel.page
{
    public partial class size : System.Web.UI.Page
    {
        #region Declare

        private int m_size_id = 0;
        int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_size_id = Utils.CIntDef(Request["size_id"]);

            if (m_size_id == 0)
            {
                dvDelete.Visible = false;
            }

            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "Kích cỡ";
                ucHeader.HeaderLevel1_Url = "../page/size_list.aspx";
                ucHeader.HeaderLevel2 = "Add new/Update kích cỡ";
                ucHeader.HeaderLevel2_Url = "../page/size.aspx";

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
            SaveInfo("size.aspx");
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

                var G_info = (from c in DB.ESHOP_SIZEs
                              where c.SIZE_ID == m_size_id
                              select c
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtName.Value = G_info.ToList()[0].SIZE_NAME;
                    txtDesc.Value = G_info.ToList()[0].SIZE_DESC;
                    rblStatus.SelectedValue = Utils.CStrDef(G_info.ToList()[0].SIZE_ACTIVE);
                    txtOrder.Value = Utils.CStrDef(G_info.ToList()[0].SIZE_PRIORITY);
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

                if (m_size_id == 0)
                {
                    //insert

                    ESHOP_SIZE cat_insert = new ESHOP_SIZE();

                    cat_insert.SIZE_NAME = Color_Name;
                    cat_insert.SIZE_DESC = Color_Desc;
                    cat_insert.SIZE_ACTIVE = Color_Active;
                    cat_insert.SIZE_PRIORITY = Color_Priority;

                    DB.ESHOP_SIZEs.InsertOnSubmit(cat_insert);
                    DB.SubmitChanges();

                    var _cat = DB.GetTable<ESHOP_SIZE>().OrderByDescending(g => g.SIZE_ID).Take(1);

                    m_size_id = _cat.Single().SIZE_ID;

                    strLink = string.IsNullOrEmpty(strLink) ? "size.aspx?size_id=" + m_size_id : strLink;
                }
                else
                {
                    //update
                    var c_update = DB.GetTable<ESHOP_SIZE>().Where(g => g.SIZE_ID == m_size_id);

                    if (c_update.ToList().Count > 0)
                    {
                        c_update.Single().SIZE_NAME = Color_Name;
                        c_update.Single().SIZE_DESC = Color_Desc;
                        c_update.Single().SIZE_PRIORITY = Color_Priority;
                        c_update.Single().SIZE_ACTIVE = Color_Active;

                        DB.SubmitChanges();

                        strLink = string.IsNullOrEmpty(strLink) ? "size_list.aspx" : strLink;
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
                var G_info = DB.GetTable<ESHOP_SIZE>().Where(g => g.SIZE_ID == m_size_id);

                DB.ESHOP_SIZEs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                //delete folder
                string fullpath = Server.MapPath(PathFiles.GetPathCategory(m_size_id));
                if (Directory.Exists(fullpath))
                {
                    DeleteAllFilesInFolder(fullpath);
                    Directory.Delete(fullpath);
                }

                Response.Redirect("size_list.aspx");

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
            return "size.aspx?size_id=" + Utils.CStrDef(GroupId);
        }

        #endregion
    }
}