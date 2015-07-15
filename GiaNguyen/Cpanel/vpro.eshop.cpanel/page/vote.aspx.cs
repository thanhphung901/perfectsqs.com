using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.Data;
using vpro.eshop.cpanel.ucControls;
using System.Reflection;

namespace vpro.eshop.cpanel.page
{
    public partial class vote : System.Web.UI.Page
    {
        #region Declare

        private int m_cat_id = 0;
        int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            m_cat_id = Utils.CIntDef(Request["vote_id"]);
            if (!IsPostBack)
            {
                LoadCategoryParent();
                Getinfo();
                ucHeader.HeaderLevel1 = "Vote";
                ucHeader.HeaderLevel1_Url = "../page/vote-list.aspx";
                ucHeader.HeaderLevel2 = "Add new/Update vote";
                ucHeader.HeaderLevel2_Url = "../page/vote.aspx";
            }
        }
        private void LoadCategoryParent()
        {
            try
            {
                var CatList = (
                                from t2 in DB.VOTEs
                                select t2
                                
                            );

                if (CatList.ToList().Count > 0)
                {
                    DataRelation relCat;
                    DataTable tbl = DataUtil.LINQToDataTable(CatList);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(tbl);

                    tbl.PrimaryKey = new DataColumn[] { tbl.Columns["VOTE_OID"] };
                    relCat = new DataRelation("Category_parent", ds.Tables[0].Columns["VOTE_OID"], ds.Tables[0].Columns["VOTE_PARENT_ID"], false);

                    ds.Relations.Add(relCat);
                    DataSet dsCat = ds.Clone();
                    DataTable CatTable = ds.Tables[0];

                    TransformTableWithSpace(ref CatTable, dsCat.Tables[0], relCat, null);

                    ddlCategory.DataSource = dsCat.Tables[0];
                    ddlCategory.DataTextField = "VOTE_QUESTION";
                    ddlCategory.DataValueField = "VOTE_OID";
                    ddlCategory.DataBind();
                    ListItem l = new ListItem("---- Root ----", "0", true);
                    l.Selected = true;
                    ddlCategory.Items.Insert(0, l);
                }
                else
                {
                    DataTable dt = new DataTable("Newtable");

                    dt.Columns.Add(new DataColumn("VOTE_OID"));
                    dt.Columns.Add(new DataColumn("VOTE_QUESTION"));

                    DataRow row = dt.NewRow();
                    row["VOTE_OID"] = 0;
                    row["VOTE_QUESTION"] = "--------Root--------";
                    dt.Rows.Add(row);

                    ddlCategory.DataTextField = "VOTE_QUESTION";
                    ddlCategory.DataValueField = "VOTE_OID";
                    ddlCategory.DataSource = dt;
                    ddlCategory.DataBind();



                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        #region Getinfo
        private void Getinfo()
        {
            try
            {
                var list = DB.VOTEs.Where(n => n.VOTE_OID == m_cat_id).ToList();
                if (list.Count > 0)
                {
                    Dractive.SelectedValue = list[0].VOTE_ACTIVE.ToString();
                    txtques.Value = list[0].VOTE_QUESTION;
                    ddlCategory.SelectedValue = list[0].VOTE_PARENT_ID.ToString();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion
        #region relation
        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        public static void TransformTableWithSpace(ref DataTable source, DataTable dest, DataRelation rel, DataRow parentRow)
        {
            if (parentRow == null)
            {
                foreach (DataRow row in source.Rows)
                {
                    if (!row.HasErrors && (Utils.CIntDef(row["VOTE_PARENT_ID"]) <= 0))
                    {
                        row["VOTE_QUESTION"] = (Utils.CIntDef(row["VOTE_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(row["VOTE_RANK"]))) + row["VOTE_QUESTION"];
                        dest.Rows.Add(row.ItemArray);
                        row.RowError = "dirty";
                        if (Utils.CStrDef(row["VOTE_QUESTION"]) != "------- Root -------" || Utils.CStrDef(row["VOTE_QUESTION"]) != "------- All -------")
                            TransformTableWithSpace(ref source, dest, rel, row);
                    }
                }
            }
            else
            {
                DataRow[] children = parentRow.GetChildRows(rel);
                if (!parentRow.HasErrors)
                {
                    parentRow["VOTE_QUESTION"] = (Utils.CIntDef(parentRow["VOTE_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(parentRow["VOTE_RANK"]))) + parentRow["VOTE_QUESTION"];
                    dest.Rows.Add(parentRow.ItemArray);
                    parentRow.RowError = "dirty";
                }
                if (children != null && children.Length > 0)
                {
                    foreach (DataRow child in children)
                    {
                        if (!child.HasErrors)
                        {
                            child["VOTE_QUESTION"] = (Utils.CIntDef(child["VOTE_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(child["VOTE_RANK"]))) + child["VOTE_QUESTION"];
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
        private void SaveInfo(string strLink = "")
        {
            try
            {
               

                int Cat_Parent_Id = Utils.CIntDef(ddlCategory.SelectedValue, 0);
                int Cat_Rank = 1;
                if (Cat_Parent_Id > 0)
                {
                    var CatParent = DB.GetTable<VOTE>().Where(c => c.VOTE_OID == Cat_Parent_Id);
                    Cat_Rank = Utils.CIntDef(CatParent.Single().VOTE_RANK) + 1;
                }

                if (m_cat_id == 0)
                {
                    //insert

                    VOTE vo_insert = new VOTE();
                    vo_insert.VOTE_RANK = Cat_Rank;
                    vo_insert.VOTE_PARENT_ID = Cat_Parent_Id;
                    vo_insert.VOTE_QUESTION = txtques.Value;
                    vo_insert.VOTE_DATE = DateTime.Now;
                    vo_insert.VOTE_ACTIVE =Utils.CIntDef(Dractive.SelectedValue);
                    vo_insert.VOTE_COUNT = 0;
                    DB.VOTEs.InsertOnSubmit(vo_insert);
                    DB.SubmitChanges();

                    var _cat = DB.GetTable<VOTE>().OrderByDescending(g => g.VOTE_OID).Take(1);

                    m_cat_id = _cat.Single().VOTE_OID;
                    strLink = "vote.aspx?vote_id=" + m_cat_id;
                    strLink = string.IsNullOrEmpty(strLink) ? "vote-list.aspx" : strLink;
                }
                else
                {
                    //update
                    var c_update = DB.GetTable<VOTE>().Where(g => g.VOTE_OID == m_cat_id);

                    if (c_update.ToList().Count > 0)
                    {
                        c_update.First().VOTE_RANK = Cat_Rank;
                        c_update.First().VOTE_PARENT_ID = Cat_Parent_Id;
                        c_update.First().VOTE_QUESTION = txtques.Value;
                        c_update.First().VOTE_DATE = DateTime.Now;
                        c_update.First().VOTE_ACTIVE = Utils.CIntDef(Dractive.SelectedValue);

                        DB.SubmitChanges();

                        strLink = string.IsNullOrEmpty(strLink) ? "vote-list.aspx" : strLink;
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

        protected void lbtSaveNew_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveInfo("vote.aspx");
        }
    }
}