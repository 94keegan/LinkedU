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
            if (!IsPostBack)
            {
                lblAlert.Attributes["class"] = "";
                // Display email to reset password if nothing in GET
                if (string.IsNullOrWhiteSpace(Request.QueryString["email"]) && string.IsNullOrWhiteSpace(Request.QueryString["genString"]))
                {
                    txtEmail.Visible = true;
                    chkPhone.Visible = true;
                    lblQuestion.Visible = false;
                    txtAnswer.Visible = false;
                    txtNewPassword.Visible = false;
                    txtNewPasswordConfirm.Visible = false;

                    // Load carriers into dropdownlist
                    SMS.TextSenderClient client = new SMS.TextSenderClient();
                    foreach (var carrier in client.getCarriers())
                    {
                        ddlCarrier.Items.Add(carrier);
                    }
                }
                else // Display question, answer, and new password box to reset password if GET contains valid email and genString
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;
                    using (SqlConnection dbConnection = new SqlConnection(connectionString))
                    {
                        dbConnection.Open();
                        try
                        {

                            // Check if gen_string/email combo is in DB
                            int resetExists = 0;
                            using (SqlCommand comm = dbConnection.CreateCommand())
                            {
                                comm.CommandText = "SELECT resetID FROM reset_password WHERE email = @email AND gen_String = @genString";
                                comm.Parameters.AddWithValue("@email", Request.QueryString["email"]);
                                comm.Parameters.AddWithValue("@genString", Request.QueryString["genString"]);
                                resetExists = comm.ExecuteScalar() == null ? 0 : int.Parse(comm.ExecuteScalar().ToString());
                            }

                            if (resetExists > 0)
                            {
                                txtEmail.Visible = false;
                                chkPhone.Visible = false;
                                lblQuestion.Visible = true;
                                txtAnswer.Visible = true;
                                txtNewPassword.Visible = true;
                                txtNewPasswordConfirm.Visible = true;

                                // Set question
                                using (SqlCommand comm = dbConnection.CreateCommand())
                                {
                                    comm.CommandText = "SELECT securityQuestion FROM users WHERE email = @email";
                                    comm.Parameters.AddWithValue("@email", Request.QueryString["email"]);
                                    SqlDataReader reader = comm.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        lblQuestion.Text = string.Concat("Question: ", reader["securityQuestion"].ToString());
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
                                comm.CommandText = "SELECT userID FROM users WHERE email = @email";
                                comm.Parameters.AddWithValue("@email", txtEmail.Text);
                                userExists = comm.ExecuteScalar() == null ? 0 : int.Parse(comm.ExecuteScalar().ToString());
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
                                if (chkPhone.Checked && txtPhone.Text.Length == 10 && !string.IsNullOrWhiteSpace(ddlCarrier.SelectedValue))
                                {
                                    SMS.TextSenderClient client = new SMS.TextSenderClient();
                                    client.sendSMS(ddlCarrier.SelectedValue, txtPhone.Text,
                                        string.Concat("This message was automatically generated via the LinkedU website.<br />",
                                        "<p>Click the link to reset your password. -> ",
                                        Request.Url.Authority, "/Reset.aspx?",
                                        "email=" + txtEmail.Text,
                                        "&genString=" + genString));
                                }
                                else
                                {
                                    lblAlert.Visible = true;
                                    lblAlert.Text = "Phone number is incorrect!";
                                    lblAlert.Attributes["class"] = "alert alert-danger";
                                    throw new Exception("Error sending SMS!");
                                }

                                // Send email
                                MailAddress messageFrom = new MailAddress("Linkedu368@gmail.com", "LinkedU");
                                MailAddress messageTo = new MailAddress(txtEmail.Text);
                                MailMessage emailMessage = new MailMessage();
                                emailMessage.To.Add(messageTo.Address);
                                emailMessage.From = messageFrom;
                                emailMessage.Subject = "LinkedU || Reset";
                                string body = string.Concat("This message was automatically generated via the LinkedU website.<br />",
                                    "<p>Click the link to reset your password. -> ",
                                    Request.Url.Authority,"/Reset.aspx?",
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
                                throw new Exception("Error sending SMS!");
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
                        if (!string.IsNullOrWhiteSpace(txtNewPassword.Text) && !string.IsNullOrWhiteSpace(txtNewPasswordConfirm.Text) && txtNewPassword.Text.Equals(txtNewPasswordConfirm.Text))
                        {
                            SqlTransaction transaction = dbConnection.BeginTransaction();
                            // Check answer
                            int answerCorrect = 0;
                            using (SqlCommand comm = dbConnection.CreateCommand())
                            {
                                comm.Transaction = transaction;
                                comm.CommandText = "SELECT userID FROM users WHERE securityAnswer = @answer";
                                comm.Parameters.AddWithValue("@answer", txtAnswer.Text);
                                answerCorrect = comm.ExecuteScalar() == null ? 0 : int.Parse(comm.ExecuteScalar().ToString());
                            }

                            if (answerCorrect > 0)
                            {
                                string userID = "";
                                string userLogin = "";
                                // Get userLogin
                                using (SqlCommand comm = dbConnection.CreateCommand())
                                {
                                    comm.Transaction = transaction;
                                    comm.CommandText = "SELECT userID FROM users WHERE email = @email";
                                    comm.Parameters.AddWithValue("@email", Request.QueryString["email"]);
                                    SqlDataReader reader = comm.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        userID = reader["userID"].ToString();
                                    }
                                    reader.Close();
                                }

                                using (SqlCommand comm = dbConnection.CreateCommand())
                                {
                                    comm.Transaction = transaction;
                                    comm.CommandText = "SELECT userLogin FROM logins WHERE userID = @userID";
                                    comm.Parameters.AddWithValue("@userID", userID);
                                    SqlDataReader reader = comm.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        userLogin = reader["userLogin"].ToString();
                                    }
                                    reader.Close();
                                }

                                // Reset password
                                string resetPasswordInsert = "UPDATE logins SET userPassword = @userPassword WHERE userLogin = @userLogin";
                                using (SqlCommand updatePassword = new SqlCommand(resetPasswordInsert, dbConnection))
                                {
                                    updatePassword.Transaction = transaction;
                                    updatePassword.Parameters.AddWithValue("@userPassword", txtNewPassword.Text);
                                    updatePassword.Parameters.AddWithValue("@userLogin", userLogin);
                                    updatePassword.ExecuteNonQuery();
                                }
                                lblAlert.Visible = true;
                                lblAlert.Text = "Password has been reset!";
                                lblAlert.Attributes["class"] = "alert alert-success";

                                // Delete reset password entry
                                string resetPasswordDelete = "DELETE FROM reset_password WHERE email = @email";
                                using (SqlCommand deleteReset = new SqlCommand(resetPasswordDelete, dbConnection))
                                {
                                    deleteReset.Transaction = transaction;
                                    deleteReset.Parameters.AddWithValue("@email", Request.QueryString["email"]);
                                    deleteReset.ExecuteNonQuery();
                                }
                                transaction.Commit();
                            }
                            else
                            {
                                lblAlert.Visible = true;
                                lblAlert.Text = "Answer is incorrect!";
                                lblAlert.Attributes["class"] = "alert alert-danger";
                            }
                        }
                        else
                        {
                            lblAlert.Visible = true;
                            lblAlert.Text = "Passwords do not match!";
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
                ddlCarrier.Visible = true;
            }
            else
            {
                txtPhone.Visible = false;
                ddlCarrier.Visible = false;
            }
        }
    }
}