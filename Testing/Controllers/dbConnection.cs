using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Testing.Controllers
{
    public class dbConnection : IDisposable
    {
        string construct = ConfigurationManager.ConnectionStrings["koneksi"].ConnectionString;
        private MySqlConnection db = new MySqlConnection();
 
        public void Dispose()
        {
            db.Dispose();
        }

        public string[] listmovie()
        {
            string SqlQuery;
            string[] respon = new string[] { " "," " };
            string temp="";
            try
            {
                db = new MySqlConnection(construct);
                db.Open();

                MySqlCommand com = new MySqlCommand("", db);
                SqlQuery = "SELECT * FROM movie";
                com.CommandText = SqlQuery;
                MySqlDataAdapter mda = new MySqlDataAdapter(com);
                DataTable dt = new DataTable();
                mda.Fill(dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            temp += dt.Rows[i]["Title"] + "|" + dt.Rows[i]["Genre"] + "|" + dt.Rows[i]["Duration"].ToString() + "|" + dt.Rows[i]["Director"];
                            if (i < dt.Rows.Count - 1) temp += '\n';
                        }
                    }
                    else
                    {
                        return new string[] { "-9", "error" };
                    }
                }
                else return new string[] { "-9", "error" };
                return new string[] { "0", temp };
            }
            catch (Exception ex)
            {
                
                return new string[] { "-9", ex.Message };
            }
            finally
            {
                db.Close();
            }
        }
        
    }
}