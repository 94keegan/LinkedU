using System;
using System.Web.UI;

namespace LinkedU
{
    public partial class Logoff : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Sign-In.aspx");
        }
    }
}