using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace LinkedU
{
    public partial class Default : System.Web.UI.Page
    {

        string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandText = "select [file], mu.UniversityID from MarkedUni mu " +
                        "inner join users u ON u.universityID = mu.UniversityID " +
                        "inner join university_files uf ON uf.userID = u.userID " +
                        "inner join university_file_types uft ON uft.id = uf.file_type AND uft.name = 'Logo'";


                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            byte[] bytes = (byte[])reader.GetValue(0);
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            ImageFeaturedUniversity.ImageUrl = "data:image/png;base64," + base64String;

                            HyperLinkFeaturedUniversity.NavigateUrl = String.Format(HyperLinkFeaturedUniversity.NavigateUrl, reader.GetInt32(1));
                        }
                    }
                }
            }
        }
    }
}