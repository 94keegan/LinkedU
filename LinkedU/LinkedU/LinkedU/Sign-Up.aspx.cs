﻿using System;
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
<<<<<<< HEAD
                dbConnection.Open();
                dbConnection.ChangeDatabase("kssuth1_Assign4");

                string loginInfo = "SELECT * FROM Users WHERE UserName='" + txtUserName.Text + "'";
                SqlCommand login = new SqlCommand(loginInfo, dbConnection);
                SqlDataReader records = login.ExecuteReader();
                if (records.Read())
=======
                // Check if UserID has already been taken
                try
>>>>>>> 871b72e0d5c4eb8ac73daed920f60a04cc37f0dd
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
<<<<<<< HEAD
                    // Insert user values
                    string userInsert = "INSERT INTO users VALUES('" + txtUserName.Text + "', '" + txtEmail.Text + "')";
                    SqlCommand user = new SqlCommand(userInsert, dbConnection);
                    user.ExecuteNonQuery();
                }
=======
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
                        string userInsert = "INSERT INTO users (userID, email) VALUES (@userID, @email)";
                        using (SqlCommand user = new SqlCommand(userInsert, dbConnection))
                        {
                            user.Parameters.AddWithValue("@userID", userid);
                            user.Parameters.AddWithValue("@email", txtEmail.Text);
                            user.ExecuteNonQuery();
                        }
                    }
>>>>>>> 871b72e0d5c4eb8ac73daed920f60a04cc37f0dd

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