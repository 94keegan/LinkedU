using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace LinkedU
{
    public partial class Sign_In : Page
    {
        private bool loginError = false;
        public bool LoginError { get { return loginError; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtPassword.Attributes["type"] = "password";
            if (!Page.IsPostBack)
            {
                if (Request.Cookies["UserName"] != null)
                {
                    chkRememberMe.Checked = true;
                    string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;
                    SqlConnection dbConnection = new SqlConnection(connectionString);
                    try
                    {
                        dbConnection.Open();

                        string UsersInfo = "SELECT * FROM Users WHERE UserName='" + Request.Cookies["UserName"].Value + "'";
                        SqlCommand Users = new SqlCommand(UsersInfo, dbConnection);
                        SqlDataReader records = Users.ExecuteReader();
                        if (records.Read())
                        {
                            do
                            {
                                txtUserName.Text = records["UserName"].ToString();
                                txtPassword.Text = records["Password"].ToString();
                            } while (records.Read());
                        }
                        records.Close();
                    }
                    catch (SqlException ex)
                    {
                           loginError = true;
                    }
                    dbConnection.Close();
                }
            }
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            if (chkRememberMe.Checked)
            {
                if (!String.IsNullOrWhiteSpace(txtUserName.Text))
                {
                    Response.Cookies["UserName"].Value = txtUserName.Text;
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(14);
                }
            }
            else
            {
                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
            }

            string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;
            SqlConnection dbConnection = new SqlConnection(connectionString);

            try
            {
                dbConnection.Open();

                string usersInfo = "SELECT * FROM Users WHERE UserName='" + txtUserName.Text + "' AND Password='" + txtPassword.Text + "'";
                SqlCommand users = new SqlCommand(usersInfo, dbConnection);
                SqlDataReader records = users.ExecuteReader();
                if (records.Read())
                {
                    do
                    {
                        if (txtUserName.Text.ToLower().Equals(records["UserName"].ToString().ToLower()) && txtPassword.Text.ToLower().Equals(records["Password"].ToString().ToLower().ToString().ToLower()))
                        {
                            Session["UserName"] = records["UserName"].ToString();
                            Response.Redirect("LoginHome.aspx");
                        }
                        else
                        {
                            loginError = true;
                        }
                    } while (records.Read());
                }
                else
                {
                    loginError = true;
                }
                records.Close();
            }
            catch (SqlException ex)
            {

                    loginError = true;
            }
            dbConnection.Close();
        }
    }
}