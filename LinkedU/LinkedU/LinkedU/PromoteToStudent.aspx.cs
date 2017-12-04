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

            if (Request.QueryString["id"] == null)
                Response.Redirect("Default.aspx");
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {



            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();


                using (SqlCommand comm = conn.CreateCommand())
                {
                    comm.Parameters.AddWithValue("@userID", Request.QueryString["id"]);
                    comm.Parameters.AddWithValue("@universityID", Session["UserID"]);
                    comm.Parameters.AddWithValue("@personalMessage", TextBoxMessage.Text);

                    comm.CommandText = "INSERT INTO promotions (userID, universityID, personalMessage, promoted) " +
                        "SELECT @userID, universityID, @personalMessage, GETDATE() FROM users WHERE userID = @universityID";
                    comm.ExecuteNonQuery();

                    TextBoxMessage.ReadOnly = true;
                    ButtonSubmit.Enabled = false;
                }
            }
        }
    }
}