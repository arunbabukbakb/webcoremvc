using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace webcoremvc.Models
{
    public class DbModel
    {
        public SqlConnection conn;
        SqlDataAdapter objDa;

        private string constring;
        
        public DbModel()
        {
            constring = GetConnectionString();
            conn = new SqlConnection(constring);
            OpenConn();
        }

        public static string GetConnectionString()
        {
            return Startup.ConnectionString;
        }

        public bool OpenConn()
        {
            try
            {
                if (this.conn.State == ConnectionState.Closed)
                    conn.Open();
            }
            catch (Exception err)
            {
                return false;
            }
            return true;
        }

        public void CloseConn()
        {
            try
            {
                if (this.conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public Object ReturnScalarQuery(string query)
        {
            SqlCommand command;
            try
            {
                command = new SqlCommand(query, conn);
                command.CommandType = CommandType.Text;
                return command.ExecuteScalar();
            }
            catch (Exception err)
            {
                return null;
            }
        }


        public Object ReturnScalarCmd(SqlCommand cmd)
        {
            try
            {
                cmd.Connection = conn;
                return cmd.ExecuteScalar();
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public DataTable RetDt(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            cmd.Connection = conn;
            objDa = new SqlDataAdapter(cmd);
            objDa.Fill(dt);
            return dt;
        }

        public DataSet RetDs(SqlCommand cmd)
        {
            DataSet ds = new DataSet();
            cmd.Connection = conn;
            objDa = new SqlDataAdapter(cmd);
            objDa.Fill(ds);
            return ds;
        }
    }
}
