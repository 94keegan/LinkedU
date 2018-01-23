using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkedU
{
    public partial class StudentSearch : System.Web.UI.Page
    {

        string connStr = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;
        float longitude;
        float latitude;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserID"] == null) || (Session["AccountType"].ToString() != "University"))
                Response.Redirect("Default.aspx");


            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandText = "SELECT latitude, longitud FROM users " +
                        "inner join universities ON universities.UNITID = users.universityID " +
                        "WHERE userID = @userID";
                    comm.Parameters.AddWithValue("@userID", Session["UserID"]);

                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            reader.Read();
                            latitude = (float)reader.GetDouble(0);
                            longitude = (float)reader.GetDouble(1);

                        }
                    }
                }
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand comm = conn.CreateCommand())
                {

                    comm.CommandText = "select users.userID, concat(lastName, ', ', firstName) as name, round(gpa, 2), graduationyear, eccount, " +
                      "highscore.name as highesttest, highscore.percentile as highestpctl," +
                      "CASE WHEN myp.notification_seen IS NOT NULL THEN 'Viewed' WHEN myp.promoted IS NOT NULL THEN 'Sent' ELSE '' END as pstatus, myp.id " +
                      "FROM users inner join student_profiles ON users.userID = student_profiles.userID " +
                      "LEFT OUTER JOIN promotions myp ON myp.userID = users.userID AND myp.universityID = (SELECT universityID FROM users WHERE userID = @userID) " +
                      "OUTER APPLY (SELECT COUNT(*) as eccount FROM student_extracurriculars WHERE userID = users.userID) as extracurriculars " +
                      "OUTER APPLY (" +
                      "  SELECT TOP 1 max(pct.test_percentile) percentile, pct.test_type, tests.name " +
                      "  FROM student_test_scores score " +
                      "  INNER JOIN student_test_percentiles pct ON pct.test_type = score.test_type AND pct.test_score <= score.test_score " +
                      "  INNER JOIN student_test_types tests ON tests.id = score.test_type " +
                      "  WHERE userID = users.userID group by pct.test_type, tests.name order by percentile DESC " +
                      ") as highscore " +
                      "WHERE gpa >= @gpa " +
                      "AND eccount >= @eccount " +
                      "AND (@ecpref = 0 OR EXISTS (SELECT 1 FROM student_extracurriculars WHERE userID = users.userID AND ec_type = @ecpref)) " +
                      "AND ( " +
                      "    3959 * " +
                      "    acos( " +
                      "        cos( radians( @lat ) ) * " +
                      "        cos( radians( latitude ) ) * " +
                      "        cos( " +
                      "            radians( longitude ) - radians( @lng ) " +
                      "        ) + " +
                      "        sin(radians(@lat)) * " +
                      "        sin(radians(latitude)) " +
                      "    )" +
                      ") <= @dist ";

                    if (SearchMinorityStatus.SelectedValue == "Y")
                        comm.CommandText += " AND (gender != 'M' AND race != 'W') ";

                    switch (DropDownListPromotionStatus.SelectedValue)
                    {
                        case "N":
                            comm.CommandText += "AND myp.promoted IS NULL ";
                            break;
                        case "V":
                            comm.CommandText += "AND myp.notification_seen IS NOT NULL ";
                            break;
                        case "S":
                            comm.CommandText += "AND (myp.promoted IS NOT NULL AND myp.notification_seen IS NULL) ";
                            break;
                    }

                    if (SearchMinimumPercentile.Text.Length > 0)
                    {
                        comm.CommandText += " AND EXISTS (select 1 FROM student_test_scores score " +
                        "INNER JOIN student_test_percentiles pct ON pct.test_type = score.test_type AND pct.test_score <= score.test_score " +
                        "INNER JOIN student_test_types tests ON tests.id = score.test_type " +
                        "WHERE userID = users.userID and pct.test_percentile >= @pct) ";

                        comm.Parameters.Add("@pct", SqlDbType.Float).Value = float.Parse(SearchMinimumPercentile.Text);
                    }

                    comm.Parameters.AddWithValue("@userID", Session["UserID"]);
                    comm.Parameters.Add("@gpa", SqlDbType.Float).Value = SearchMinimumGPA.Text.Length > 0? float.Parse(SearchMinimumGPA.Text): 0F;
                    comm.Parameters.Add("@eccount", SqlDbType.Float).Value = SearchMinimumExtraCurriculars.Text.Length > 0 ? float.Parse(SearchMinimumExtraCurriculars.Text) : 0F;
                    comm.Parameters.Add("@ecpref", SqlDbType.Int).Value = SearchExtraCurricular.SelectedValue;
                    comm.Parameters.Add("@lat", SqlDbType.Decimal).Value = latitude;
                    comm.Parameters.Add("@lng", SqlDbType.Decimal).Value = longitude;
                    comm.Parameters.Add("@dist", SqlDbType.Int).Value = TextBoxSearchRadius.Text;

                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            string highscore = "N/A";

                            if (!reader.IsDBNull(5))
                            {
                                highscore = String.Format("{0} ({1} percentile)", reader.GetString(5), AddOrdinal(reader.GetInt32(6)));
                            }

                            TableRow row = new TableRow();

                            TableCell[] cells = new TableCell[6];

                            cells[0] = new TableCell() { Text = string.Format("<a href=\"StudentLookup.aspx?id={0}\" target=\"_blank\">{1}</a>", reader.GetInt32(0), reader.GetString(1)) };
                            cells[1] = new TableCell() { Text = reader.GetDouble(2).ToString() };
                            cells[2] = new TableCell() { Text = reader.GetInt32(3).ToString() };
                            cells[3] = new TableCell() { Text = highscore };
                            cells[4] = new TableCell() { Text = reader.GetInt32(4).ToString() };

                            if (!reader.IsDBNull(8))
                                cells[5] = new TableCell() { Text = String.Format("<a href=\"ViewPromotion.aspx?id={0}\" target=\"_blank\">{1}</a>", reader.GetInt32(8), reader.GetString(7)) };
                            else
                                cells[5] = new TableCell() { Text = "" };


                            row.Cells.AddRange(cells);

                            ResultTable.Rows.Add(row);
                        }
                    }
                }
            }

            ResultTable.Visible = true;
        }

        /// <summary>
        /// Determines the ordinal rank suffix of a number
        /// Credit to samjudson at stackoverflow
        /// https://stackoverflow.com/a/20175
        /// </summary>
        /// <param name="num">The number whose ordinal value should be determined</param>
        /// <returns></returns>
        public static string AddOrdinal(int num)
        {
            if (num <= 0) return num.ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }

        }
    }
}
