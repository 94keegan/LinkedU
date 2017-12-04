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
            Refresh();
        }

        public void Refresh()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;

            if (Session["UserID"] == null)
            {
                GlobalNotifications.Visible = false;
                return;
            }

            GlobalNotificationItems.Controls.Clear();

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
                                string icon = "unchecked";
                                if (reader.IsDBNull(1))
                                    newNotifications++;
                                else
                                    icon = "check";

                                HtmlGenericControl li = new HtmlGenericControl("li");
                                li.InnerHtml = String.Format("<a href=\"View{2}.aspx?id={0}\"><span class=\"glyphicon glyphicon-{3}\"></span> {2} received from {1}</a>", reader.GetInt32(0), reader.GetString(2), interest, icon);
                                GlobalNotificationItems.Controls.Add(li);
                                totalNotifications++;

                            }

                            if (newNotifications > 0)
                            {
                                GlobalNotifications.Attributes["class"] = "dropdown active";
                                GlobalNotificationCount.Text = newNotifications.ToString();
                            }
                            else if (totalNotifications == 0)
                            {
                                GlobalNotifications.Attributes["class"] = "disabled";
                                GlobalNotificationCount.Text = "";
                            }
                            else
                            {
                                GlobalNotifications.Attributes["class"] = "dropdown";
                                GlobalNotificationCount.Text = "";
                            }
                        }
                    }
                }
            }
        }
    }
}