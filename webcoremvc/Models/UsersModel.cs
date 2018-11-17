using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace webcoremvc.Models
{
    public class UsersModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public int userid { get; set; }
        public int visits { get; set; }
        public int SchoolID { get; set; }
        public int RoleID { get; set; }
        public int PollStatus { get; set; }
        public string Email { get; set; }
        public string Specialisation { get; set; }
    }

    public class UserMethods
    {
        DbModel objdb;
        SqlCommand cmd;
        DataTable dt;
        DataSet ds;

        public bool ValidateLogin(UsersModel objuser)
        {
            try
            {
                objdb = new DbModel();
                bool IsValidlogin = false;
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = objdb.conn;
                cmd.CommandTimeout = 0;
                cmd.CommandText = "Usp_ValidateLogin";
                cmd.Parameters.AddWithValue("@Username", objuser.username);
                cmd.Parameters.AddWithValue("@Password", objuser.password);
                dt = new DataTable();
                dt = objdb.RetDt(cmd);
                if (dt.Rows.Count > 0)
                {
                    IsValidlogin = true;
                    int Temp;
                    if (int.TryParse(dt.Rows[0]["UserID"].ToString(), out Temp))
                    {
                        objuser.userid = Temp;

                    }
                    if (int.TryParse(dt.Rows[0]["Visits"].ToString(), out Temp))
                    {
                        objuser.visits = Temp;

                    }
                    if (int.TryParse(dt.Rows[0]["SchoolID"].ToString(), out Temp))
                    {
                        //System.Web.HttpContext.Current.Session["SchoolID"] = Temp;
                        objuser.SchoolID = Temp;
                    }
                    if (int.TryParse(dt.Rows[0]["RoleID"].ToString(), out Temp))
                    {
                        objuser.RoleID = Temp;
                    }
                    if (int.TryParse(dt.Rows[0]["Poll"].ToString(), out Temp))
                    {
                        objuser.PollStatus = Temp;
                    }
                    else
                    {
                        objuser.PollStatus = 0;
                    }
                    //System.Web.HttpContext.Current.Session["SchoolType"] = "0";
                    objuser.Email = dt.Rows[0]["EmailAddress"].ToString();
                    objuser.Specialisation = dt.Rows[0]["Specialisation"].ToString();

                }

                return IsValidlogin;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objdb.CloseConn();
            }
        }
    }
}
