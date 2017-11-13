using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Configuration;
using System.Data.SqlClient;

namespace LinkedU.autocomplete
{
    public partial class UniversityName : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["term"] != null)
            {
                List<University> universities = new List<University>();

                string connStr = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    using (SqlCommand comm = conn.CreateCommand())
                    {
                        comm.CommandText = "SELECT UNITID, INSTNM FROM universities WHERE INSTNM LIKE @name ORDER BY INSTNM";
                        comm.Parameters.AddWithValue("@name", "%" + Request.QueryString["term"] + "%");

                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                universities.Add(new University(reader.GetString(1), reader.GetInt32(0)));
                            }
                        }
                    }
                }

                Response.Write(JsonConvert.SerializeObject(universities));
            }
        }
    }


    public class University
    {
        private string _name;
        private int _id;

        public University(string name, int id)
        {
            _name = name;
            _id = id;
        }

        public string value { get { return _name; } set { _name = value; } }
        public int id { get { return _id; } set { _id = value; } }
    }
}
