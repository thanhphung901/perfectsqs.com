using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using vpro.functions;
using System.Data;
using vpro.eshop.cpanel.ucControls;
using System.IO;
using vpro.eshop.cpanel.Components;

namespace vpro.eshop.cpanel.page
{
    public partial class properties : System.Web.UI.Page
    {

        #region Declare

        private int m_prop_id = 0;
        int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_prop_id = Utils.CIntDef(Request["prop_id"]);

            if (m_prop_id == 0)
            {
                dvDelete.Visible = false;
            }

            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "Thuộc tính";
                ucHeader.HeaderLevel1_Url = "../page/properties_list.aspx";
                ucHeader.HeaderLevel2 = "Add new/Update thuộc tính";
                ucHeader.HeaderLevel2_Url = "../page/properties.aspx";

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
            SaveInfo("properties.aspx");
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
        }

        #endregion

        #region My functions

        private void LoadCategoryParent()
        {
            try
            {
                var CatList = (
                                from t2 in DB.ESHOP_PROPERTies
                                select new
                                {
                                    PROP_ID = t2.PROP_NAME == "------- Root -------" ? 0 : t2.PROP_ID,
                                    PROP_NAME = t2.PROP_NAME,
                                    PROP_PARENT_ID = t2.PROP_PARENT_ID,
                                    PROP_RANK = t2.PROP_RANK

                                }
                            );

                if (CatList.ToList().Count > 0)
                {
                    DataRelation relCat;
                    DataTable tbl = DataUtil.LINQToDataTable(CatList);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(tbl);

                    tbl.PrimaryKey = new DataColumn[] { tbl.Columns["PROP_ID"] };
                    relCat = new DataRelation("Category_parent", ds.Tables[0].Columns["PROP_ID"], ds.Tables[0].Columns["PROP_PARENT_ID"], false);

                    ds.Relations.Add(relCat);
                    DataSet dsCat = ds.Clone();
                    DataTable CatTable = ds.Tables[0];

                    CpanelUtils.TransformTableWithSpace(ref CatTable, dsCat.Tables[0], relCat, null);

                    ddlCategory.DataSource = dsCat.Tables[0];
                    ddlCategory.DataTextField = "PROP_NAME";
                    ddlCategory.DataValueField = "PROP_ID";
                    ddlCategory.DataBind();

                }
                else
                {
                    DataTable dt = new DataTable("Newtable");

                    dt.Columns.Add(new DataColumn("PROP_ID"));
                    dt.Columns.Add(new DataColumn("PROP_NAME"));

                    DataRow row = dt.NewRow();
                    row["PROP_ID"] = 0;
                    row["PROP_NAME"] = "--------Root--------";
                    dt.Rows.Add(row);

                    ddlCategory.DataTextField = "PROP_NAME";
                    ddlCategory.DataValueField = "PROP_ID";
                    ddlCategory.DataSource = dt;
                    ddlCategory.DataBind();



                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void getInfo()
        {
            try
            {
                LoadCategoryParent();
                
                var G_info = (from c in DB.ESHOP_PROPERTies
                              where c.PROP_ID == m_prop_id
                              select c
                            );

                if (G_info.ToList().Count > 0)
                {
                    ddlCategory.SelectedValue = Utils.CStrDef(G_info.ToList()[0].PROP_PARENT_ID);
                    txtName.Value = G_info.ToList()[0].PROP_NAME;
                    txtDesc.Value = G_info.ToList()[0].PROP_DESC;
                    rblStatus.SelectedValue = Utils.CStrDef(G_info.ToList()[0].PROP_ACTIVE);
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
                string Prop_Name = txtName.Value;
                string Prop_Desc = txtDesc.Value;

                int Prop_Parent_Id = Utils.CIntDef(ddlCategory.SelectedValue, 0);
                int Prop_Status = Utils.CIntDef(rblStatus.SelectedValue);
                int Prop_Rank = 1;

                if (Prop_Parent_Id > 0)
                {
                    var CatParent = DB.GetTable<ESHOP_PROPERTy>().Where(c => c.PROP_ID == Prop_Parent_Id);

                    Prop_Rank = Utils.CIntDef(CatParent.Single().PROP_RANK) + 1;
                }

                if (m_prop_id == 0)
                {
                    //insert

                    ESHOP_PROPERTy cat_insert = new ESHOP_PROPERTy();

                    cat_insert.PROP_NAME = Prop_Name;
                    cat_insert.PROP_DESC = Prop_Desc;

                    cat_insert.PROP_PARENT_ID = Prop_Parent_Id;
                    cat_insert.PROP_ACTIVE = Prop_Status;
                    cat_insert.PROP_RANK = Prop_Rank;

                    DB.ESHOP_PROPERTies.InsertOnSubmit(cat_insert);
                    DB.SubmitChanges();

                    var _cat = DB.GetTable<ESHOP_CATEGORy>().OrderByDescending(g => g.CAT_ID).Take(1);

                    m_prop_id = _cat.Single().CAT_ID;

                    strLink = string.IsNullOrEmpty(strLink) ? "properties_list.aspx" : strLink;
                }
                else
                {
                    //update
                    var c_update = DB.GetTable<ESHOP_CATEGORy>().Where(g => g.CAT_ID == m_prop_id);

                    if (c_update.ToList().Count > 0)
                    {
                        c_update.Single().CAT_NAME = Prop_Name;
                        c_update.Single().CAT_DESC = Prop_Desc;

                        c_update.Single().CAT_PARENT_ID = Prop_Parent_Id;
                        c_update.Single().CAT_STATUS = Prop_Status;

                        c_update.Single().CAT_RANK = Prop_Rank;

                        DB.SubmitChanges();

                        strLink = string.IsNullOrEmpty(strLink) ? "properties_list.aspx" : strLink;
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
                var G_info = DB.GetTable<ESHOP_PROPERTy>().Where(g => g.PROP_ID == m_prop_id);

                DB.ESHOP_PROPERTies.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                //delete folder
                string fullpath = Server.MapPath(PathFiles.GetPathCategory(m_prop_id));
                if (Directory.Exists(fullpath))
                {
                    DeleteAllFilesInFolder(fullpath);
                    Directory.Delete(fullpath);
                }

                Response.Redirect("properties_list.aspx");

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
            return "properties.aspx?prop_id=" + Utils.CStrDef(GroupId);
        }

        #endregion
    }
}