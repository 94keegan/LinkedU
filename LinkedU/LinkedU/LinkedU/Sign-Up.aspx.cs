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
            SqlConnection dbConnection = new SqlConnection(connectionString);

            // Check if UserID has already been taken
            try
            {
                dbConnection.Open();
                dbConnection.ChangeDatabase("kssuth1_Assign4");

                string loginInfo = "SELECT * FROM login WHERE UserName='" + txtUserName.Text + "'";
                SqlCommand login = new SqlCommand(loginInfo, dbConnection);
                SqlDataReader records = login.ExecuteReader();
                if (records.Read())
                {
                    valid = false;
                    signupError = true;
                }
                records.Close();
            }
            catch (SqlException ex)
            {
                successful = false;
                if (ex.Number == 911) // non-existent DB
                {
                    SqlCommand sqlCommand = new SqlCommand("CREATE DATABASE kssuth1_Assign4", dbConnection);
                    sqlCommand.ExecuteNonQuery();
                    dbConnection.ChangeDatabase("kssuth1_Assign4");
                }
                else
                {
                    signupError = true;
                }

            }

            // Insert user into the user and login tables
            try
            {
                if (valid)
                {
                    // Insert login values
                    string loginInsert = "INSERT INTO login VALUES('" + txtUserName.Text + "', '" + txtPassword.Text + "')";
                    SqlCommand login = new SqlCommand(loginInsert, dbConnection);
                    login.ExecuteNonQuery();

                    // Insert user values
                    string userInsert = "INSERT INTO users VALUES('" + txtUserName.Text + "', '" + txtEmail.Text + "')";
                    SqlCommand user = new SqlCommand(userInsert, dbConnection);
                    user.ExecuteNonQuery();
                }

            }
            catch (SqlException ex)
            {
                successful = false;
                if (ex.Number == 911) // non-existent DB
                {
                    SqlCommand sqlCommand = new SqlCommand("CREATE DATABASE kssuth1_Assign4", dbConnection);
                    sqlCommand.ExecuteNonQuery();
                    dbConnection.ChangeDatabase("kssuth1_Assign4");
                }
                else
                {
                    signupError = true;

                }
            }
            if (valid && successful)
            {
                signupError = false;
            }
        }
    }
}