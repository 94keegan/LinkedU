using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace LinkedU
{
    public partial class Sign_Up : Page
    {
        public bool valid = true;
        public bool successful = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            txtPassword.Attributes["type"] = "password";
            txtConfPassword.Attributes["type"] = "password";

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

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                // Check if passwords match
                if (!string.IsNullOrWhiteSpace(txtPassword.Text) && !string.IsNullOrWhiteSpace(txtConfPassword.Text) && txtPassword.Text.Equals(txtConfPassword.Text))
                {
                    successful = true;
                    PanelSignupError.Visible = false;
                }
                else
                {
                    successful = false;
                    PanelSignupError.Visible = true;
                    lblSignupError.Text = "Passwords do not match!";
                }

                dbConnection.Open();

                // Insert user into the user and login tables
                try
                {
                    if (valid && successful)
                    {
                        SqlTransaction transaction = dbConnection.BeginTransaction();

                        try
                        {
                            // Insert logins values
                            string loginInsert = "INSERT INTO logins (userLogin, userPassword) VALUES (@username, @password)";
                            using (SqlCommand login = new SqlCommand(loginInsert, dbConnection))
                            {
                                login.Transaction = transaction;
                                login.Parameters.AddWithValue("@username", txtUserName.Text);
                                login.Parameters.AddWithValue("@password", txtPassword.Text);
                                login.ExecuteNonQuery();
                            }

                            //retrieve auto-generated userID
                            int userid = 0;
                            string loginSelect = "SELECT userID FROM logins WHERE userLogin = @username";
                            using (SqlCommand select = new SqlCommand(loginSelect, dbConnection))
                            {
                                select.Transaction = transaction;
                                select.Parameters.AddWithValue("@username", txtUserName.Text);
                                userid = int.Parse(select.ExecuteScalar().ToString());
                            }

                            // Insert user values
                            string userInsert = "INSERT INTO users (userID, accountType, universityName, firstName, lastName, email, securityQuestion, securityAnswer) VALUES (@userID, @accountType, @universityName, @firstName, @lastName, @email, @securityQuestion, @securityAnswer)";
                            using (SqlCommand user = new SqlCommand(userInsert, dbConnection))
                            {
                                user.Transaction = transaction;
                                user.Parameters.AddWithValue("@userID", userid);
                                user.Parameters.AddWithValue("@accountType", ddlAccountType.Text);
                                user.Parameters.AddWithValue("@universityName", UniversityID.Value);
                                user.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                                user.Parameters.AddWithValue("@lastName", txtLastname.Text);
                                user.Parameters.AddWithValue("@email", txtEmail.Text);
                                user.Parameters.AddWithValue("@securityQuestion", txtQuestion.Text);
                                user.Parameters.AddWithValue("@securityAnswer", txtAnswer.Text);
                                user.ExecuteNonQuery();
                            }

                            transaction.Commit();
                        } catch (Exception ex)
                        {
                            successful = false;
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    successful = false;
                    PanelSignupError.Visible = true;
                    lblSignupError.Text= "Error Signing Up!";
                }
                if (valid && successful)
                {
                    PanelSignupError.Visible = false;
                    Response.Cookies["UserName"].Value = txtUserName.Text;
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(14);
                    Response.Redirect("Default.aspx");
                }
            }
        }

        /// <summary>
        /// Change textboxes depending on what account type is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlAccountType.Text)
            {
                case "Student":
                    txtFirstName.Visible = true;
                    txtLastname.Visible = true;
                    txtUniversityName.Visible = false;
                    txtUniversityName.Attributes["required"] = "required";
                    txtFirstName.Attributes["required"] = "";
                    txtLastname.Attributes["required"] = "";
                    break;
                case "University":
                    txtFirstName.Visible = false;
                    txtLastname.Visible = false;
                    txtUniversityName.Visible = true;
                    txtUniversityName.Attributes["required"] = "";
                    txtFirstName.Attributes["required"] = "required";
                    txtLastname.Attributes["required"] = "required";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Check if UserID has already been taken
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtUserName_OnTextChanged(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                try
                {
                    dbConnection.Open();

                    string loginInfo = "SELECT 1 FROM logins WHERE userLogin LIKE @username";
                    using (SqlCommand login = new SqlCommand(loginInfo, dbConnection))
                    {
                        login.Parameters.AddWithValue("@username", txtUserName.Text);
                        if (login.ExecuteScalar() != null)
                        {
                            valid = false;
                            PanelSignupError.Visible = true;
                            lblSignupError.Text = "User name already taken!";
                        }
                        else
                        {
                            valid = true;
                            PanelSignupError.Visible = false;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Response.Write(ex.Message);
                    successful = false;
                    PanelSignupError.Visible = true;
                    lblSignupError.Text = "Error Signing Up!";
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

            public string Value { get { return _name; } set { _name = value; } }
            public int ID { get { return _id; } set { _id = value; } }
        }
    }
}