using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkedU
{
    public partial class UniversityProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("Sign-In.aspx");

            if (Session["AccountType"].ToString() == "Student")
                Response.Redirect("StudentProfile.aspx");
        }
    }
}