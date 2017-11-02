using System;
using System.Configuration;
using System.Data.SqlClient;

namespace LinkedU
{
    public partial class Sign_Up : System.Web.UI.Page
    {
        private bool signupError = false;
        public bool SignupError { get { return signupError; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtPassword.Attributes["type"] = "password";
            txtConfPassword.Attributes["type"] = "password";
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            bool valid = true;
            bool successful = true;
            string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                // Check if UserID has already been taken
                try
                {
                    dbConnection.Open();

                    //Changed the query to "SELECT 1" from "SELECT *". Returns less data, and we only need to test existence
                    string loginInfo = "SELECT 1 FROM login WHERE UserName = @username";
                    using (SqlCommand login = new SqlCommand(loginInfo, dbConnection))
                    {
                        login.Parameters.AddWithValue("@username", txtUserName.Text);
                        //Changed to execute scalar. Much more efficient for getting a single value, or the existance of a row
                        if (login.ExecuteScalar() != null)
                        {
                            valid = false;
                            signupError = true;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Response.Write(ex.Message);
                    successful = false;
                    signupError = true;
                    
                }

                // Insert user into the user and login tables
                try
                {
                    if (valid && successful)
                    {
                        // Insert login values
                        string loginInsert = "INSERT INTO login VALUES(@username, @password)";
                        SqlCommand login = new SqlCommand(loginInsert, dbConnection);
                        login.Parameters.AddWithValue("@username", txtUserName.Text);
                        login.Parameters.AddWithValue("@password", txtPassword.Text);
                        login.ExecuteNonQuery();

                        // Insert user values
                        string userInsert = "INSERT INTO users VALUES(@username, @email)";
                        SqlCommand user = new SqlCommand(userInsert, dbConnection);
                        user.Parameters.AddWithValue("@username", txtUserName.Text);
                        user.Parameters.AddWithValue("@email", txtEmail.Text);
                        user.ExecuteNonQuery();
                    }

                }
                catch (SqlException ex)
                {
                    successful = false;
                    signupError = true;
                }
                if (valid && successful)
                {
                    signupError = false;
                }
            }
        }
    }
}