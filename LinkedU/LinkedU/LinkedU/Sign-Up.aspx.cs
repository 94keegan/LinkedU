using System;
using System.Configuration;
using System.Data.SqlClient;

namespace LinkedU
{
    public partial class Sign_Up : System.Web.UI.Page
    {
        public bool valid = true;
        public bool successful = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            txtPassword.Attributes["type"] = "password";
            txtConfPassword.Attributes["type"] = "password";
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
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
                        string userInsert = "INSERT INTO users (userID, firstName, lastName, email, securityQuestion, securityAnswer) VALUES (@userID, @accountType, @universityName, @firstName, @lastName, @email, @securityQuestion, @securityAnswer)";
                        using (SqlCommand user = new SqlCommand(userInsert, dbConnection))
                        {
                            user.Parameters.AddWithValue("@userID", userid);
                            user.Parameters.AddWithValue("@accountType", ddlAccountType.Text);
                            user.Parameters.AddWithValue("@universityName", txtUniversityName.Text);
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
                    PanelSignupError.Visible = true;
                    LabelSignupError.Text= "Error Signing Up!";
                }
                if (valid && successful)
                {
                    PanelSignupError.Visible = false;
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
                            LabelSignupError.Text = "User name already taken!";
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
                    LabelSignupError.Text = "Error Signing Up!";
                }
            }
        }
    }
}