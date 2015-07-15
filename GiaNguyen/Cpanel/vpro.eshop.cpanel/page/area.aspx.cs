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
    public partial class area : System.Web.UI.Page
    {
        #region Declare

        private int m_cat_id = 0;
        int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_cat_id = Utils.CIntDef(Request["area_id"]);

            if (m_cat_id == 0)
            {
                dvDelete.Visible = false;
            }

            if (!IsPostBack)
            {
                ucHeader.HeaderLevel1 = "Khu vực";
                ucHeader.HeaderLevel1_Url = "../page/area_list.aspx";
                ucHeader.HeaderLevel2 = "Add new/Update Khu vực";
                ucHeader.HeaderLevel2_Url = "../page/area.aspx";

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
            SaveInfo("area.aspx");
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
                    row["ARE_ID"] = 0;
                    row["ARE_NAME"] = "--------Root--------";
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
                              where c.PROP_ID == m_cat_id
                              select c
                            );

                if (G_info.ToList().Count > 0)
                {
                    ddlCategory.SelectedValue = Utils.CStrDef(G_info.ToList()[0].PROP_PARENT_ID);
                    txtCode.Value = G_info.ToList()[0].PROP_CODE;
                    txtName.Value = G_info.ToList()[0].PROP_NAME;
                    txtDesc.Value = G_info.ToList()[0].PROP_DESC;
                    txtOrder.Value = Utils.CStrDef(G_info.ToList()[0].PROP_PRIORITY);
                    rblActive.SelectedValue = Utils.CStrDef(G_info.ToList()[0].PROP_ACTIVE);
                    txtShippingFee.Value = Utils.CStrDef(G_info.ToList()[0].PROP_SHIPPING_FEE);
                }
                else
                {
                    LoadCategoryParent();
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
                string Area_Code = txtCode.Value;
                string Area_Name = txtName.Value;
                string Area_Desc = txtDesc.Value;


                int Area_Parent_Id = Utils.CIntDef(ddlCategory.SelectedValue, 0);
                int Area_Active = Utils.CIntDef(rblActive.SelectedValue);
                int Area_Order = Utils.CIntDef(txtOrder.Value);
                decimal Area_ShippingFee = Utils.CDecDef(txtShippingFee.Value);
                decimal Area_HomeFee = Utils.CDecDef(txtHomeFee.Value);
                string Area_Parent_Path = "0";
                int Area_Rank = 1;

                //english language
                //string Cat_Code_En = txtCodeEn.Value;
                //string Cat_Name_En = txtNameEn.Value;
                //string Cat_Desc_En = txtDescEn.Value;
                //string Cat_Seo_Url_En = txtSeoUrlEn.Value;
                //string Cat_Seo_Tittle_En = txtSeoTitleEn.Value;
                //string Cat_Seo_Keyword_En = txtSeoKeywordEn.Value;
                //string Cat_Seo_Description_En = txtSeoDescriptionEn.Value;


                if (Area_Parent_Id > 0)
                {
                    var CatParent = DB.GetTable<ESHOP_PROPERTy>().Where(c => c.PROP_ID == Area_Parent_Id);

                    Area_Parent_Path = CatParent.Single().PROP_PARENT_PATH + "," + Utils.CStrDef(Area_Parent_Id);
                    Area_Rank = Utils.CIntDef(CatParent.Single().PROP_RANK) + 1;
                }
              
                if (m_cat_id == 0)
                {
                    //insert

                    ESHOP_PROPERTy area_insert = new ESHOP_PROPERTy();

                    area_insert.PROP_CODE = Area_Code;
                    area_insert.PROP_NAME = Area_Name;
                    area_insert.PROP_DESC = Area_Desc;


                    area_insert.PROP_PARENT_ID = Area_Parent_Id;
                    area_insert.PROP_ACTIVE = Area_Active;
                    area_insert.PROP_PRIORITY = Area_Order;

                    area_insert.PROP_PARENT_PATH = Area_Parent_Path;
                    area_insert.PROP_RANK = Area_Rank;
                    area_insert.PROP_SHIPPING_FEE = Area_ShippingFee;

                    DB.ESHOP_PROPERTies.InsertOnSubmit(area_insert);
                    DB.SubmitChanges();

                    var _cat = DB.GetTable<ESHOP_PROPERTy>().OrderByDescending(g => g.PROP_ID).Take(1);

                    m_cat_id = Utils.CIntDef(_cat.Single().PROP_ID);

                    strLink = string.IsNullOrEmpty(strLink) ? "area_list.aspx?area_id=" + m_cat_id : strLink;
                }
                else
                {
                    //update
                    var c_update = DB.GetTable<ESHOP_PROPERTy>().Where(g => g.PROP_ID == m_cat_id);

                    if (c_update.ToList().Count > 0)
                    {
                        c_update.First().PROP_CODE = Area_Code;
                        c_update.First().PROP_NAME = Area_Name;
                        c_update.First().PROP_DESC = Area_Desc;


                        c_update.First().PROP_PARENT_ID = Area_Parent_Id;
                        c_update.First().PROP_ACTIVE = Area_Active;
                        c_update.First().PROP_PRIORITY = Area_Order;

                        c_update.First().PROP_PARENT_PATH = Area_Parent_Path;
                        c_update.First().PROP_RANK = Area_Rank;
                        c_update.First().PROP_SHIPPING_FEE = Area_ShippingFee;

                        DB.SubmitChanges();

                        strLink = string.IsNullOrEmpty(strLink) ? "area_list.aspx" : strLink;
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
                var G_info = DB.GetTable<ESHOP_PROPERTy>().Where(g => g.PROP_ID == m_cat_id);

                DB.ESHOP_PROPERTies.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();
                Response.Redirect("area_list.aspx");
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
            return "area.aspx?area_id=" + Utils.CStrDef(GroupId);
        }

        public static void TransformTableWithSpace(ref DataTable source, DataTable dest, DataRelation rel, DataRow parentRow)
        {
            if (parentRow == null)
            {
                foreach (DataRow row in source.Rows)
                {
                    if (!row.HasErrors && (Utils.CIntDef(row["AREA_PARENT_ID"]) <= 0))
                    {
                        row["AREA_NAME"] = (Utils.CIntDef(row["AREA_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(row["AREA_RANK"]))) + row["AREA_NAME"];
                        dest.Rows.Add(row.ItemArray);
                        row.RowError = "dirty";
                        if (Utils.CStrDef(row["AREA_NAME"]) != "------- Root -------")
                            TransformTableWithSpace(ref source, dest, rel, row);
                    }
                }
            }
            else
            {
                DataRow[] children = parentRow.GetChildRows(rel);
                if (!parentRow.HasErrors)
                {
                    parentRow["AREA_NAME"] = (Utils.CIntDef(parentRow["AREA_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(parentRow["AREA_RANK"]))) + parentRow["AREA_NAME"];
                    dest.Rows.Add(parentRow.ItemArray);
                    parentRow.RowError = "dirty";
                }
                if (children != null && children.Length > 0)
                {
                    foreach (DataRow child in children)
                    {
                        if (!child.HasErrors)
                        {
                            child["AREA_NAME"] = (Utils.CIntDef(child["AREA_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(child["AREA_RANK"]))) + child["AREA_NAME"];
                            dest.Rows.Add(child.ItemArray);
                            child.RowError = "dirty";
                            TransformTableWithSpace(ref source, dest, rel, child);
                        }
                    }
                }
            }
        }

        public static string Duplicate(string partToDuplicate, int howManyTimes)
        {
            string result = "";

            for (int i = 0; i < howManyTimes; i++)
                result += partToDuplicate;

            return result;
        }

        #endregion
    }
}