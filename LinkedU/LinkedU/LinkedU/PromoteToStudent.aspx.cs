using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace LinkedU
{
    public partial class PromoteToStudent : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserID"] == null) || (Session["AccountType"].ToString() != "University"))
                Response.Redirect("Sign-In.aspx");

            if (Request.QueryString["uid"] == null)
                return;
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

                    comm.CommandText = "INSERT INTO promotions (userID, universityID, personalMessage, promoted) VALUES (@userID, @universityID, @personalMessage, GETDATE())";
                    comm.ExecuteNonQuery();

                    TextBoxMessage.ReadOnly = true;
                    ButtonSubmit.Enabled = false;
                }
            }
        }
    }
}