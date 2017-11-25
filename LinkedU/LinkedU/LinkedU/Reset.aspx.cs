using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;

namespace LinkedU
{
    public partial class Reset : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblAlert.Attributes["class"] = "";
            // Display email to reset password if nothing in GET
            if (string.IsNullOrWhiteSpace(Request.QueryString["email"]) && string.IsNullOrWhiteSpace(Request.QueryString["genString"]))
            {
                txtEmail.Visible = true;
                lblQuestion.Visible = false;
                txtAnswer.Visible = false;
                txtNewPassword.Visible = false;
                txtNewPasswordConfirm.Visible = false;
            }
            else // Display question, answer, and new password box to reset password if GET contains valid email and genString
            {
                string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;
                using (SqlConnection dbConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlTransaction transaction = dbConnection.BeginTransaction();
                        // Check if gen_string/email combo is in DB
                        int resetExists = 0;
                        using (SqlCommand comm = dbConnection.CreateCommand())
                        {
                            comm.CommandText = "SELECT * FROM reset_password WHERE email = @email && genString = @genString";
                            comm.Parameters.AddWithValue("@email", Request.QueryString["email"]);
                            comm.Parameters.AddWithValue("@genString", Request.QueryString["genString"]);
                            resetExists = int.Parse(comm.ExecuteScalar().ToString());
                        }

                        if (resetExists > 0)
                        {
                            txtEmail.Visible = false;
                            lblQuestion.Visible = true;
                            txtAnswer.Visible = true;
                            txtNewPassword.Visible = true;
                            txtNewPasswordConfirm.Visible = true;

                            // Set question
                            using (SqlCommand comm = dbConnection.CreateCommand())
                            {
                                comm.CommandText = "SELECT question FROM logins WHERE email = @email";
                                comm.Parameters.AddWithValue("@email", Request.QueryString["email"]);
                                SqlDataReader reader = comm.ExecuteReader();
                                while (reader.Read())
                                {
                                    lblQuestion.Text = reader["question"].ToString();
                                }
                            }
                        }
                        else
                        {
                            lblAlert.Visible = true;
                            lblAlert.Text = "Error in URL!";
                            lblAlert.Attributes["class"] = "alert alert-danger";
                        }
                    }
                    catch (SqlException ex)
                    {
                        lblAlert.Visible = true;
                        lblAlert.Text = "Error Resetting Password!";
                        lblAlert.Attributes["class"] = "alert alert-danger";
                    }
                }
            }
        }


        /// <summary>
        /// If email and genString are not in GET then store values in DB and send email.
        /// If email and genString are in GET then reset password.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();
                // Create password reset in DB and send email to user
                if (string.IsNullOrWhiteSpace(Request.QueryString["email"]) && string.IsNullOrWhiteSpace(Request.QueryString["genString"]))
                {
                    try
                    {
                        SqlTransaction transaction = dbConnection.BeginTransaction();
                        try
                        {
                            // Check if email is in DB
                            int userExists = 0;
                            using (SqlCommand comm = dbConnection.CreateCommand())
                            {
                                comm.Transaction = transaction;
                                comm.CommandText = "SELECT userLogin FROM logins WHERE email = @email";
                                comm.Parameters.AddWithValue("@email", txtEmail.Text);
                                userExists = int.Parse(comm.ExecuteScalar().ToString());
                            }

                            if (userExists > 0)
                            {
                                string genString = genRandomString();
                                // Insert logins values
                                string resetPasswordInsert = "INSERT INTO reset_password (email, gen_string) VALUES (@email, @gen_string)";
                                using (SqlCommand login = new SqlCommand(resetPasswordInsert, dbConnection))
                                {
                                    login.Transaction = transaction;
                                    login.Parameters.AddWithValue("@email", txtEmail.Text);
                                    login.Parameters.AddWithValue("@gen_string", genString);
                                    login.ExecuteNonQuery();
                                }

                                // Send SMS
                                if (chkPhone.Checked)
                                {
                                    // TODO: Send SMS and check phone format
                                }
                                // Send email
                                MailAddress messageFrom = new MailAddress(txtEmail.Text);
                                MailAddress messageTo = new MailAddress("Linkedu368@gmail.com", "LinkedU");
                                MailMessage emailMessage = new MailMessage();
                                emailMessage.To.Add(messageTo.Address);
                                emailMessage.From = messageFrom;
                                emailMessage.Subject = "LinkedU || Contact Us";
                                string body = string.Concat("This message was automatically generated via the LinkedU website.<br />",
                                    "<p>Click the link to reset your password. -> ",
                                    "localhost:/LinkedU/Reset.aspx?", // TODO: Change address when uploaded to IIS
                                    "email=" + txtEmail.Text,
                                    "&genString=" + genString);
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
                            else
                            {
                                lblAlert.Visible = true;
                                lblAlert.Text = "User does not exist with this email!";
                                lblAlert.Attributes["class"] = "alert alert-danger";
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                    catch (SqlException ex)
                    {
                        lblAlert.Visible = true;
                        lblAlert.Text = "Error Resetting Password!";
                        lblAlert.Attributes["class"] = "alert alert-danger";
                    }
                }
                else
                {
                    try
                    {
                        SqlTransaction transaction = dbConnection.BeginTransaction();
                        // Check answer
                        int answerCorrect = 0;
                        using (SqlCommand comm = dbConnection.CreateCommand())
                        {
                            comm.CommandText = "SELECT userLogin FROM logins WHERE answer = @answer";
                            comm.Parameters.AddWithValue("@answer", txtAnswer.Text);
                            answerCorrect = int.Parse(comm.ExecuteScalar().ToString());
                        }

                        if (answerCorrect > 0)
                        {
                            string userLogin = "";
                            // Get userLogin
                            using (SqlCommand comm = dbConnection.CreateCommand())
                            {
                                comm.CommandText = "SELECT userLogin FROM logins WHERE email = @email";
                                comm.Parameters.AddWithValue("@email", Request.QueryString["email"]);
                                SqlDataReader reader = comm.ExecuteReader();
                                while (reader.Read())
                                {
                                    userLogin = reader["userLogin"].ToString();
                                }
                            }

                            // Reset password in DB
                            string resetPasswordInsert = "UPDATE password FROM login SET password = @password WHERE VALUES userLogin = @userLogin)";
                            using (SqlCommand updatePassword = new SqlCommand(resetPasswordInsert, dbConnection))
                            {
                                updatePassword.Transaction = transaction;
                                updatePassword.Parameters.AddWithValue("@password", txtNewPassword);
                                updatePassword.Parameters.AddWithValue("@userLogin", userLogin);
                                updatePassword.ExecuteNonQuery();
                            }
                            lblAlert.Visible = true;
                            lblAlert.Text = "Password has been reset!";
                            lblAlert.Attributes["class"] = "alert alert-success";
                        }
                        else
                        {
                            lblAlert.Visible = true;
                            lblAlert.Text = "Answer is incorrect!";
                            lblAlert.Attributes["class"] = "alert alert-danger";
                        }
                    }
                    catch (SqlException ex)
                    {
                        lblAlert.Visible = true;
                        lblAlert.Text = "Error Resetting Password!";
                        lblAlert.Attributes["class"] = "alert alert-danger";
                    }
                }
            }
        }

        /// <summary>
        /// Generate random 10 character string to identify account to reset
        /// </summary>
        /// <returns></returns>
        public String genRandomString()
        {
            string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder genString = new StringBuilder();
            Random rnd = new Random();
            while (genString.Length < 10)
            {
                int index = (int)(rnd.NextDouble() * CHARS.Length);
                genString.Append(CHARS[index]);
            }
            return genString.ToString();
        }

        /// <summary>
        /// Shows and hides the phone number textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkPhone_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPhone.Checked)
            {
                txtPhone.Visible = true;
            }
            else
            {
                txtPhone.Visible = false;
            }
        }
    }
}