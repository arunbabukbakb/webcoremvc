using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace webcoremvc.Models
{
    public class SiteMenuModel
    {
        DbModel objdb=new DbModel();
        SqlCommand cmdobj;
        DataTable dt;
        DataSet ds;

        public int Flag { get; set; }
        public int MenuID { get; set; }
        public string MenuTextEn { get; set; }
        public string MenuTextAr { get; set; }
        public string Link { get; set; }
        public int ParentMenuID { get; set; }
        public int IsParent { get; set; }
        public string MenuOrder { get; set; }
        public string HelpTextEn { get; set; }
        public string HelpTextAr { get; set; }
        public List<int> RoleList { get; set; }
        public int RoleID { get; set; }
        public bool IsVisible { get; set; }
        private bool _DataNotAvailable = false;
        public bool DataNotAvailable { get; set; }
        public string MenuIcon { get; set; }
        public string HomeIcon { get; set; }

        public DataTable SelectAllMenu()
        {
            dt = new DataTable();
            try
            {
                cmdobj = new SqlCommand();
                cmdobj.CommandType = CommandType.StoredProcedure;
                cmdobj.Connection = objdb.conn;
                cmdobj.CommandText = "Usp_SelectMenu";
                cmdobj.Parameters.AddWithValue("@Flag", Flag);
                cmdobj.Parameters.AddWithValue("@MenuID", MenuID);
                cmdobj.Parameters.AddWithValue("@MenuTextEn", MenuTextEn);
                cmdobj.Parameters.AddWithValue("@RoleID", RoleID);
                dt = objdb.RetDt(cmdobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objdb.CloseConn();
            }
            return dt;
        }

        public List<sitemenulist> GetMenuList()
        {
            List<sitemenulist> listdata = new List<sitemenulist>();
            try
            {
                Flag = 7;
                RoleID = 7;
                MenuTextEn = "";
                MenuID = 0;
                dt = new DataTable();
                dt = SelectAllMenu();

                listdata = (from DataRow dr in dt.Rows
                            select new sitemenulist()
                            {
                                MenuID = Convert.ToInt32(dr["MenuID"].ToString()),
                                MenuTextEn = dr["MenuTextEn"].ToString(),
                                MenuTextAr = dr["MenuTextAr"].ToString(),
                                Link = dr["Link"].ToString(),
                                ParentMenuID = Convert.ToInt32(dr["ParentMenuID"].ToString()),
                                IsParent = Convert.ToInt32(dr["IsParent"].ToString()),
                                MenuOrder = dr["MenuOrder"].ToString(),
                                HelpTextEn = dr["HelpTextEn"].ToString(),
                                HelpTextAr = dr["HelpTextAr"].ToString(),
                                MenuIcon = dr["MenuIcon"].ToString(),
                                HomeIcon = dr["HomeIcon"].ToString()
                            }).ToList();
            }
            catch (Exception ex)
            {
                var error = ex.Message.ToString();
            }
            return listdata;
        }

    }

    public class sitemenulist
    {
        public int MenuID { get; set; }
        public string MenuTextEn { get; set; }
        public string MenuTextAr { get; set; }
        public string Link { get; set; }
        public int ParentMenuID { get; set; }
        public int IsParent { get; set; }
        public string MenuOrder { get; set; }
        public string HelpTextEn { get; set; }
        public string HelpTextAr { get; set; }
        public bool IsVisible { get; set; }
        private bool _DataNotAvailable = false;
        public bool DataNotAvailable { get; set; }
        public string MenuIcon { get; set; }
        public string HomeIcon { get; set; }
    }

    public class loadMenu
    {
        re
    }

}
