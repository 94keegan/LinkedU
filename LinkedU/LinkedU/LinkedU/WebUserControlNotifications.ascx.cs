using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LinkedU
{
    public partial class WebUserControlNotifications : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;

            if (Session["UserID"] == null)
            {
                GlobalNotifications.Visible = false;
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand comm = conn.CreateCommand())
                {
                    comm.Parameters.AddWithValue("@userID", Session["UserID"]);
                    string interest = "";

                    if (Session["AccountType"].ToString() == "Student")
                    {
                        interest = "Promotion";
                        comm.CommandText = "SELECT TOP 10 a.id, a.notification_seen, INSTNM FROM promotions a " +
                            "inner join universities promoter ON promoter.UNITID = a.universityID " +
                            "inner join users ON users.userID = a.userID " +
                            "where users.userID = @userID";
                    }
                    else if (Session["AccountType"].ToString() == "University")
                    {
                        interest = "Application";
                        comm.CommandText = "SELECT TOP 10 a.id, a.notification_seen, CONCAT(applicant.firstName, ' ', applicant.lastName) FROM applications a " +
                            "inner join users ON users.universityID = a.universityID " +
                            "inner join users applicant ON applicant.userID = a.userID " +
                            "where users.userID = @userID";
                    }
                    else
                    {
                        comm.CommandText = "";
                    }

                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            int newNotifications = 0;
                            int totalNotifications = 0;

                            
                            while (reader.Read())
                            {
                                HtmlGenericControl li = new HtmlGenericControl("li");
                                li.InnerHtml = String.Format("<a href=\"View{2}.asmx?id={0}\">{2} received from {1}</a>", reader.GetInt32(0), reader.GetString(2), interest);
                                GlobalNotificationItems.Controls.Add(li);
                                totalNotifications++;

                                if (reader.IsDBNull(1))
                                    newNotifications++;
                            }

                            if (newNotifications > 0)
                            {
                                GlobalNotifications.Attributes["class"] += " active";
                                GlobalNotificationCount.Text = newNotifications.ToString();
                            }
                            else
                            {
                                if (totalNotifications == 0)
                                    GlobalNotifications.Attributes["class"] = "disabled";
                            }
                        }
                    }
                }
            }
        }
    }
}