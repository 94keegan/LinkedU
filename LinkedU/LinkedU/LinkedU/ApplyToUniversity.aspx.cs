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
    public partial class ApplyToUniversity : System.Web.UI.Page
    {

        string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserID"] == null) || (Session["AccountType"].ToString() != "Student"))
                Response.Redirect("Sign-In.aspx");

            if (Request.QueryString["uid"] == null)
                return;

            if (!IsPostBack)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand comm = conn.CreateCommand())
                    {
                        comm.Parameters.AddWithValue("@userID", Session["UserID"]);
                        comm.Parameters.AddWithValue("@universityID", Request.QueryString["uid"]);

                        comm.CommandText = "SELECT personalMessage FROM applications WHERE userID = @userID AND universityID = @universityID";

                        object message = comm.ExecuteScalar();
                        if (message != null)
                        {
                            TextBoxMessage.Text = message.ToString();
                            TextBoxMessage.ReadOnly = true;
                            ButtonSubmit.Enabled = false;
                        }
                    }
                }
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();


                using (SqlCommand comm = conn.CreateCommand())
                {
                    comm.Parameters.AddWithValue("@userID", Session["UserID"]);
                    comm.Parameters.AddWithValue("@universityID", Request.QueryString["uid"]);
                    comm.Parameters.AddWithValue("@personalMessage", TextBoxMessage.Text);

                    comm.CommandText = "INSERT INTO applications (userID, universityID, personalMessage, applied) VALUES (@userID, @universityID, @personalMessage, GETDATE())";
                    comm.ExecuteNonQuery();

                    TextBoxMessage.ReadOnly = true;
                    ButtonSubmit.Enabled = false;
                }
            }
        }
    }
}