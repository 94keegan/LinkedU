using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkedU
{
    public partial class Logoff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
<<<<<<< HEAD
            Session["UserName"] = null;
            Response.Redirect("Sign-Up.aspx");
=======
            Session.Clear();

            Response.Redirect("Sign-In.aspx");
>>>>>>> dd80ba928738314105fd8e21c52f3d1a07b72159
        }
    }
}