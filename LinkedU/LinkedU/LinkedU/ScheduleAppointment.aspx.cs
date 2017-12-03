using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Configuration;
using System.Data.SqlClient;
namespace LinkedU
{
    public partial class ScheduleAppointment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRequest_Click(object sender, EventArgs e)
        {
            String contacttype = "";
            String time = "";
            String val = DropDownList1.SelectedValue.ToString();
            int value = Int32.Parse(val);
            String val2 = DropDownList2.SelectedValue.ToString();
            int value2 = Int32.Parse(val2);
            if (value == 1)
            {
                 contacttype = "Over the Phone";
            }
            else if ((value == 2))
            {
                 contacttype = "In person";
            }



            if (value2 == 1)
            {
                time = "8am to 9am";
            }
            else if (value2 == 2)
            {
                time = "9am to 10am";
            }
            else if (value2 == 3)
            {
                time = "10am to 11am";
            }
            else if (value2 == 4)
            {
                time = "1pm to 2pm";
            }
            else if (value2 == 5)
            {
                time = "2pm to 3pm";
            }
            else if (value2 == 6)
            {
                time = "3pm to 4pm";
            }


            String email = string.Format("{0}", Request.Form["email"]);
            String name = Request.Form["Name"];

            string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;
            MailMessage msg = new MailMessage();
            SmtpClient client = new System.Net.Mail.SmtpClient();
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            { 

            }
            try
            {
                
                msg.Subject = "Appointment Request";
                msg.Body = name + "  is interested in meeting with you! They would like to meet " + contacttype + " On  " +  Calendar1.SelectedDate.Month + "/" + Calendar1.SelectedDate.Day +   " from " + time + " . If you are available at this time, please reply all to this email. The user has been CC'd on it."; 
                msg.From = new MailAddress("linkedu368@gmail.com");
                msg.To.Add("linkedu368@gmail.com");
                msg.IsBodyHtml = true;
                msg.CC.Add(email);
                
                client.Host = "smtp.gmail.com";
                System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential("linkedu368@gmail.com", "isuit368");
                client.Port = int.Parse("587");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicauthenticationinfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);
            }
            catch (Exception ex)
            {

            }
            String email2 = string.Format("{0}", Request.Form["email"]);
         
            MailMessage msg2 = new MailMessage();
            SmtpClient client2 = new System.Net.Mail.SmtpClient();
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {

            }
            try
            {
                msg2.Subject = "Appointment Request Confirmation";
                msg2.Body = "You appointment request has been sent. The university you're interested in has been sent your meeting request. You were CC'd on that email. If the University is available to meet with you at that time, they will select reply all. Thanks!";
                msg2.From = new MailAddress("linkedu368@gmail.com");
                msg2.To.Add(email2);
                msg2.IsBodyHtml = true;
                

                client2.Host = "smtp.gmail.com";
                System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential("linkedu368@gmail.com", "isuit368");
                client2.Port = int.Parse("587");
                client2.EnableSsl = true;
                client2.UseDefaultCredentials = false;
                client2.Credentials = basicauthenticationinfo;
                client2.DeliveryMethod = SmtpDeliveryMethod.Network;
                client2.Send(msg2);
            }
            catch (Exception ex)
            {

            }
        }
    }
}