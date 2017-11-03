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
                    {
                        {
                            {
                        }
                        {
                            loginError = true;
                        }
                    }
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
            {
                {
                    {
                        {
                            loginError = true;
                        }
                }
                {
                    loginError = true;
                }
            }
        }
    }
}