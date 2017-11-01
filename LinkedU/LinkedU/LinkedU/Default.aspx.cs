using System;
using System.Web.UI;

namespace LinkedU
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Cookies["UserName"] == null)
                {
                    Response.Redirect("Sign-In.aspx");
                }
            }
        }
    }
}