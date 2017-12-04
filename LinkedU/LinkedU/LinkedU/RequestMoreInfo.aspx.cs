using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;

namespace LinkedU
{
    public partial class RequestMoreInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRequest_Click(object sender, EventArgs e)
        {
            String email = string.Format("{0}", Request.Form["email"]);
            String customMessage = string.Format("{0}", Request.Form["message"]);
            String name = Request.Form["name"];
            string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;
            MailMessage msg = new MailMessage();
            SmtpClient client = new System.Net.Mail.SmtpClient();
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {



            }
            try
            {
                msg.Subject = "Request for more information";
                msg.Body = name + " is interested in finding out more about you. They have requested that you send them more information about yourself. Please contact them at  " + email + " at your earliest convenience to find out more about what they have to offer. This is the additional message they provided:" + customMessage;
                msg.From = new MailAddress("linkedu368@gmail.com");
                msg.To.Add("linkedu368@gmail.com");
                msg.IsBodyHtml = true;
                client.Host = "smtp.gmail.com";
                System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential("linkedu368@gmail.com", "isuit368");
                client.Port = int.Parse("587");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicauthenticationinfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);
                lblTest.Visible = true;
                lblTest.Text = "Your request was sent! An email was sent to the university you're interested in!";
            }
            catch
            {

            }
        }
    }
}
