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
                dbConnection.Open();
                dbConnection.ChangeDatabase("kssuth1_Assign4");

                // Check if UserID has already been taken
                try
                {
                    dbConnection.Open();

                    //Changed the query to "SELECT 1" from "SELECT *". Returns less data, and we only need to test existence
                    string loginInfo = "SELECT 1 FROM logins WHERE userLogin LIKE @username";
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
                        // Insert logins values
                        string loginInsert = "INSERT INTO logins (userLogin, userPassword) VALUES (@username, @password)";
                        using (SqlCommand login = new SqlCommand(loginInsert, dbConnection))
                        {
                            login.Parameters.AddWithValue("@username", txtUserName.Text);
                            login.Parameters.AddWithValue("@password", txtPassword.Text);
                            login.ExecuteNonQuery();
                        }

                        //retrieve auto-generated userID
                        int userid = 0;
                        string loginSelect = "SELECT userID FROM logins WHERE userLogin = @username";
                        using (SqlCommand select = new SqlCommand(loginSelect, dbConnection))
                        {
                            select.Parameters.AddWithValue("@username", txtUserName.Text);
                            userid = int.Parse(select.ExecuteScalar().ToString());
                        }

                        // Insert user values
                        string userInsert = "INSERT INTO users (userID, firstName, lastName, email, securityQuestion, securityAnswer) VALUES (@userID, @firstName, @lastName, @email, @securityQuestion, @securityAnswer)";
                        using (SqlCommand user = new SqlCommand(userInsert, dbConnection))
                        {
                            user.Parameters.AddWithValue("@userID", userid);
                            user.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                            user.Parameters.AddWithValue("@lastName", txtLastname.Text);
                            user.Parameters.AddWithValue("@email", txtEmail.Text);
                            user.Parameters.AddWithValue("@securityQuestion", txtQuestion.Text);
                            user.Parameters.AddWithValue("@securityAnswer", txtAnswer.Text);
                            user.ExecuteNonQuery();
                        }
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