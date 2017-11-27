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
                    using (SqlConnection dbConnection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            dbConnection.Open();

                            string UsersInfo = "SELECT userLogin, userPassword FROM logins WHERE userLogin LIKE @userLogin";
                            using (SqlCommand Users = new SqlCommand(UsersInfo, dbConnection))
                            {

                                Users.Parameters.AddWithValue("@userLogin", Request.Cookies["UserName"].Value);
                                using (SqlDataReader records = Users.ExecuteReader())
                                {
                                    if (records.Read())
                                    {
                                        do
                                        {
                                            txtUserName.Text = records["userLogin"].ToString();
                                            txtPassword.Text = records["userPassword"].ToString();
                                        } while (records.Read());
                                    }
                                }
                            }
                        }
                        catch
                        {
                            loginError = true;
                        }
                    }
                }

                //Capture the referring web page, if in the same webiste (has the same host) and store in the web form, so it's accessible after sign-in
                if ((Request.UrlReferrer != null) && (Request.UrlReferrer.Host == Request.Url.Host) && (!Request.UrlReferrer.AbsolutePath.Contains("Reset.aspx")))
                    SignInReferrer.Value = Request.UrlReferrer.AbsoluteUri;
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
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                try
                {
                    dbConnection.Open();

                    string usersInfo = "SELECT userLogin, userPassword, userID FROM logins WHERE userLogin = @userLogin AND userPassword = @userPassword";
                    using (SqlCommand users = new SqlCommand(usersInfo, dbConnection))
                    {

                        users.Parameters.AddWithValue("@userLogin", txtUserName.Text);
                        users.Parameters.AddWithValue("@userPassword", txtPassword.Text);
                        using (SqlDataReader records = users.ExecuteReader())
                        {
                            if (records.Read())
                            {
                                do
                                {
                                    if (txtUserName.Text.ToLower().Equals(records["userLogin"].ToString().ToLower()) && txtPassword.Text.ToLower().Equals(records["userPassword"].ToString().ToLower().ToString().ToLower()))
                                    {
                                        Session["UserName"] = records["userLogin"].ToString();
                                        Session["UserID"] = records["userID"].ToString();

                                        //if referred from another page in this website, return to that page
                                        if (SignInReferrer.Value != "")
                                            Response.Redirect(SignInReferrer.Value);
                                        else
                                            Response.Redirect("Default.aspx");
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
                        }
                    }
                }
                catch
                {

                    loginError = true;
                }
            }
        }
    }
}