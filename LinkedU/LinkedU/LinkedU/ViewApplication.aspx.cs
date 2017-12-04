using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkedU
{
    public partial class ViewApplication : System.Web.UI.Page
    {

        string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            if ((Session["UserID"] == null) || ((Session["AccountType"].ToString() != "University") && (Session["UserID"].ToString() != Request.QueryString["id"])))
                Response.Redirect("Default.aspx");

            if (Request.QueryString["id"] == null)
                return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand comm = conn.CreateCommand())
                {

                    comm.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                    comm.Parameters.AddWithValue("@universityUserID", Session["UserID"]);

                    comm.CommandText = "SELECT personalMessage, userID FROM applications WHERE id = @id AND universityID = (SELECT universityID FROM users WHERE userID = @universityUserID)";
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Response.Redirect("Default.aspx");
                        }
                        else
                        {
                            reader.Read();
                            PanelPersonalMessage.Controls.Add(new Label()
                            {
                                Text = reader.GetString(0)
                            });
                            comm.Parameters.AddWithValue("@userID", reader.GetInt32(1));
                        }
                    }


                    comm.CommandText = "UPDATE applications SET notification_seen = GETDATE() WHERE id = @id AND notification_seen IS NULL";
                    comm.ExecuteNonQuery();

                    GlobalNotificationControl.Refresh();
          
                    comm.CommandText = "SELECT CONCAT(firstName, ' ', lastName) as fullName FROM users WHERE userID = @userID";

                    StudentName.Text = comm.ExecuteScalar().ToString();

                    comm.CommandText = "SELECT highschool, graduationyear, gpa FROM student_profiles WHERE userID = @userID";
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            if (!reader.IsDBNull(2))
                                StudentGPA.Text = String.Format("{0:N2} GPA", Math.Round(reader.GetDouble(2), 2));

                            if (!reader.IsDBNull(0))
                                StudentHighSchool.Text = reader.GetString(0);

                            if (!reader.IsDBNull(1))
                                StudentGraduationYear.Text = String.Format("Class of {0}", reader.GetInt32(1).ToString());
                        }
                    }


                    comm.CommandText = "SELECT [file] FROM student_files " +
                        "INNER JOIN student_file_types ON student_files.file_type = student_file_types.id AND student_file_types.name = 'Profile Picture' " +
                        "WHERE userID = @userID ";

                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            byte[] bytes = (byte[])reader.GetValue(0);
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            StudentImage.ImageUrl = "data:image/png;base64," + base64String;
                        }
                    }


                    comm.CommandText = "select max(pct.test_percentile) percentile, tests.name, score.test_score from student_test_scores score " +
                        "INNER JOIN student_test_percentiles pct ON pct.test_type = score.test_type AND pct.test_score <= score.test_score " +
                        "INNER JOIN student_test_types tests ON tests.id = score.test_type " +
                        "WHERE userID = @userID group by pct.test_type, tests.name, score.test_score order by percentile DESC";

                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TableRow row = new TableRow();

                                TableCell cell1 = new TableCell()
                                {
                                    Text = reader.GetString(1)
                                };
                                TableCell cell2 = new TableCell()
                                {
                                    Text = reader.GetDouble(2).ToString()
                                };
                                TableCell cell3 = new TableCell()
                                {
                                    Text = reader.GetInt32(0).ToString()
                                };

                                row.Cells.Add(cell1);
                                row.Cells.Add(cell2);
                                row.Cells.Add(cell3);

                                TableTestScores.Rows.Add(row);
                            }
                        }
                    }

                    comm.CommandText = "select ec_name, name from student_extracurriculars " +
                        "inner join extracurricular_types ON extracurricular_types.id = student_extracurriculars.ec_type " +
                        "WHERE userID = @userID";

                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TableRow row = new TableRow();

                                TableCell cell1 = new TableCell()
                                {
                                    Text = reader.GetString(0)
                                };
                                TableCell cell2 = new TableCell()
                                {
                                    Text = reader.GetString(1)
                                };

                                row.Cells.Add(cell1);
                                row.Cells.Add(cell2);

                                TableExtraCurriculars.Rows.Add(row);
                            }
                        }
                    }

                }
            }
        }
    }
}