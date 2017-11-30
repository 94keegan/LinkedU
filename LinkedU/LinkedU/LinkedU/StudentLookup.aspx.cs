using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkedU
{
    public partial class StudentLookup : System.Web.UI.Page
    {

        string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            //if ((Session["UserID"] == null) || (Session["AccountType"].ToString() != "University"))
            //    Response.Redirect("Default.aspx");

            if (Request.QueryString["id"] == null)
                return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandText = "SELECT CONCAT(firstName, ' ', lastName) as fullName FROM users WHERE userID = @userID";
                    comm.Parameters.AddWithValue("@userID", Request.QueryString["id"]);

                    StudentName.Text = comm.ExecuteScalar().ToString();


                    comm.CommandText = "SELECT [file] FROM student_files INNER JOIN student_file_types ON student_files.file_type = student_file_types.id AND student_file_types.name = 'Profile Picture' WHERE userID = @userID ";

                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            byte[] bytes = (byte[])reader.GetValue(0);
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            StudentImage.ImageUrl = "data:image/png;base64," + base64String;
                        }
                    }

                    
                }
            }
        }
    }
}