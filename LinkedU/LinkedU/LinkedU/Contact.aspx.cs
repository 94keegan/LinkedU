using System;
using System.Net;
using System.Net.Mail;
using System.Web.UI;

namespace LinkedU
{
    public partial class Contact : Page
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

        /// <summary>
        /// Generate and send email to LinkedU staff email address
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Send Email
            if (!string.IsNullOrWhiteSpace(txtName.Text) && !string.IsNullOrWhiteSpace(txtEmail.Text) && !string.IsNullOrWhiteSpace(txtMessage.Text))
            {
                try
                {
                    MailAddress messageFrom = new MailAddress(txtEmail.Text);
                    MailAddress messageTo = new MailAddress("Linkedu368@gmail.com", "LinkedU");
                    MailMessage emailMessage = new MailMessage();
                    emailMessage.To.Add(messageTo.Address);
                    emailMessage.From = messageFrom;
                    emailMessage.Subject = "LinkedU || Contact Us";
                    string body = string.Concat("This message was automatically generated via the LinkedU website.<br />",
                        "<p>Name: ", txtName.Text, "<br />",
                        "Contact Email: ", txtEmail.Text, "<br />",
                        "Message: ", txtMessage.Text);
                    emailMessage.Body = body;
                    emailMessage.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    // Credentials are necessary if the server requires the client
                    // to authenticate before it will send e-mail on the client's behalf.
                    NetworkCredential NetworkCred = new NetworkCredential("Linkedu368@gmail.com", "isuit368");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.ServicePoint.MaxIdleTime = 0;
                    smtp.ServicePoint.SetTcpKeepAlive(true, 2000, 2000);
                    emailMessage.Priority = MailPriority.High;
                    smtp.Send(emailMessage);
                    // Clean up.
                    emailMessage.Dispose();
                    lblAlert.Visible = true;
                    lblAlert.Text = "Email has been sent!";
                    lblAlert.Attributes["class"] = "alert alert-success";
                }
                catch (Exception ex)
                {
                    lblAlert.Text = "Email failed to send!";
                    lblAlert.Visible = true;
                    lblAlert.Attributes["class"] = "alert alert-danger";
                }
            }
        }
    }
}