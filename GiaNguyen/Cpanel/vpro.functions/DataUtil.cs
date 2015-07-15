using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace vpro.functions
{
    public static class DataUtil
    {
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
                    if (!row.HasErrors && (Utils.CIntDef( row["CAT_PARENT_ID"]) <= 0))
                    {
                        row["CAT_NAME"] = (Utils.CIntDef(row["CAT_RANK"]) <= 1 ? "" : Duplicate("------",Utils.CIntDef( row["CAT_RANK"] ))) + row["CAT_NAME"];
                        dest.Rows.Add(row.ItemArray);
                        row.RowError = "dirty";
                        if (Utils.CStrDef(row["CAT_NAME"]) != "------- Root -------" || Utils.CStrDef(row["CAT_NAME"]) != "------- All -------")
                            TransformTableWithSpace( ref source,dest, rel, row);
                    }
                }
            }
            else
            {
                DataRow[] children = parentRow.GetChildRows(rel);
                if (!parentRow.HasErrors)
                {
                    parentRow["CAT_NAME"] =( Utils.CIntDef(parentRow["CAT_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(parentRow["CAT_RANK"]))) + parentRow["CAT_NAME"];
                    dest.Rows.Add(parentRow.ItemArray);
                    parentRow.RowError = "dirty";
                }
                if (children != null && children.Length > 0)
                {
                    foreach (DataRow child in children)
                    {
                        if (!child.HasErrors)
                        {
                            child["CAT_NAME"] =( Utils.CIntDef(child["CAT_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(child["CAT_RANK"]))) + child["CAT_NAME"];
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
            string result="";

            for (int i =0; i < howManyTimes; i++)
                result +=partToDuplicate;

            return result;
        }

    }
}
