using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using LinkedU.DistanceFinder;

namespace LinkedU
{
    public partial class UniversitySearch : System.Web.UI.Page
    {

        string connStr = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;
        float longitude;
        float latitude;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserID"] != null)
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    using (SqlCommand comm = conn.CreateCommand())
                    {
                        comm.CommandText = "SELECT latitude, longitude FROM student_profiles WHERE userID = @userID";
                        comm.Parameters.AddWithValue("@userID", Session["UserID"]);

                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader != null && reader.HasRows)
                            {
                                reader.Read();
                                latitude = (float)reader.GetDouble(0);
                                longitude = (float)reader.GetDouble(1);

                                DropDownRadiusCenter.Items[0].Enabled = true;
                                TextBoxSearchCenter.Visible = DropDownRadiusCenter.SelectedValue == "addr";

                            }
                        }
                    }
                }
            }
            /*
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandText = "SELECT DISTINCT STABBR FROM universities ORDER BY STABBR";

                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SearchState.Items.Add(new ListItem(reader.GetString(0)));
                        }
                    }
                }
            }
            */
        }

        protected void SearchValue_TextChanged(object sender, EventArgs e)
        {
            if (DropDownRadiusCenter.SelectedValue == "addr")
            {
                using (DistanceFinderSoapClient distance = new DistanceFinderSoapClient())
                {
                    try
                    {
                        Geolocation geo = distance.GetLocationFromAddress(TextBoxSearchCenter.Text);

                        latitude = geo.Latitude;
                        longitude = geo.Longitude;
                    }
                    catch
                    {

                    }
                }
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand comm = conn.CreateCommand())
                {

                    comm.CommandText = "SELECT UNITID, INSTNM, ADDR, CITY, STABBR, ZIP, " +
                      "   ( 3959 * " +
                      "    acos( " +
                      "        cos( radians( @lat ) ) * " +
                      "        cos( radians( LATITUDE ) ) * " +
                      "        cos( " +
                      "            radians( LONGITUD ) - radians( @lng ) " +
                      "        ) + " +
                      "        sin(radians(@lat)) * " +
                      "        sin(radians(LATITUDE)) " +
                      "    ) " +
                      ") as distance " +
                      "FROM universities " +
                      "WHERE (INSTNM LIKE @query " +
                      "OR WEBADDR LIKE @query " +
                      "OR ADMINURL LIKE @query " +
                      "OR IALIAS LIKE @query " +
                      "OR F1SYSNAM LIKE @query " +
                      "OR COUNTYNM LIKE @query " +
                      ") " +
                      "AND (@hloffer = -1 OR HLOFFER >= @hloffer) " +
                      "AND ( " +
                      "    3959 * " +
                      "    acos( " +
                      "        cos( radians( @lat ) ) * " +
                      "        cos( radians( LATITUDE ) ) * " +
                      "        cos( " +
                      "            radians( LONGITUD ) - radians( @lng ) " +
                      "        ) + " +
                      "        sin(radians(@lat)) * " +
                      "        sin(radians(LATITUDE)) " +
                      "    )" +
                      ") < @dist " +
                      "ORDER BY distance";

                    comm.Parameters.Add("@query", SqlDbType.VarChar).Value = "%" + SearchName.Text + "%";
                    comm.Parameters.Add("@hloffer", SqlDbType.Int).Value = SearchHighestLevel.SelectedValue;
                    comm.Parameters.Add("@lat", SqlDbType.Decimal).Value = latitude;
                    comm.Parameters.Add("@lng", SqlDbType.Decimal).Value = longitude;
                    comm.Parameters.Add("@dist", SqlDbType.Int).Value = TextBoxSearchRadius.Text;

                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TableRow row = new TableRow();

                            TableCell[] cells = new TableCell[6];

                            cells[0] = new TableCell() { Text = string.Format("<a href=\"UniversityLookup.aspx?uid={0}\" target=\"_blank\">{1}</a>", reader.GetInt32(0), reader.GetString(1)) };
                            cells[1] = new TableCell() { Text = reader.GetString(2) };
                            cells[2] = new TableCell() { Text = reader.GetString(3) };
                            cells[3] = new TableCell() { Text = reader.GetString(4) };
                            cells[4] = new TableCell() { Text = reader.GetString(5) };
                            cells[5] = new TableCell() { Text = String.Format("{0,2:n} Miles", reader.GetDouble(6)) };


                            row.Cells.AddRange(cells);

                            ResultTable.Rows.Add(row);
                        }
                    }
                }
            }
            
            ResultTable.Visible = true;
        }

        protected void DropDownRadiusCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownRadiusCenter.SelectedValue == "me")
            {
                TextBoxSearchCenter.Visible = false;
            }
            else
            {
                TextBoxSearchCenter.Visible = true;
            }
        }
    }
}