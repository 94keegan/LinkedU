using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace LinkedU
{
    public partial class UniversitySearch : System.Web.UI.Page
    {

        string connStr = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
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
        }

        protected void SearchValue_TextChanged(object sender, EventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand comm = conn.CreateCommand())
                {

                    comm.CommandText = "SELECT UNITID, INSTNM, ADDR, CITY, STABBR, ZIP FROM universities " +
                      "WHERE (INSTNM LIKE @query " +
                      "OR WEBADDR LIKE @query " +
                      "OR ADMINURL LIKE @query " +
                      "OR IALIAS LIKE @query " +
                      "OR F1SYSNAM LIKE @query " +
                      "OR COUNTYNM LIKE @query " +
                      ") " +
                      "AND STABBR LIKE @state " +
                      "AND (@region = -1 OR OBEREG = @region) " +
                      "AND (@hloffer = -1 OR HLOFFER >= @hloffer) " +
                      "AND CITY LIKE @city " +
                      "ORDER BY INSTNM";

                    comm.Parameters.Add("@query", SqlDbType.VarChar).Value = "%" + SearchName.Text + "%";
                    comm.Parameters.Add("@city", SqlDbType.VarChar).Value = "%" + SearchCity.Text + "%";
                    comm.Parameters.Add("@state", SqlDbType.NVarChar).Value = SearchState.SelectedIndex > 0 ? SearchState.SelectedValue : "%";
                    comm.Parameters.Add("@region", SqlDbType.Int).Value = SearchRegion.SelectedValue;
                    comm.Parameters.Add("@hloffer", SqlDbType.Int).Value = SearchHighestLevel.SelectedValue;

                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TableRow row = new TableRow();

                            TableCell[] cells = new TableCell[5];

                            cells[0] = new TableCell() { Text = string.Format("<a href=\"UniversityLookup.aspx?uid={0}\" target=\"_blank\">{1}</a>", reader.GetInt32(0), reader.GetString(1)) };
                            cells[1] = new TableCell() { Text = reader.GetString(2) };
                            cells[2] = new TableCell() { Text = reader.GetString(3) };
                            cells[3] = new TableCell() { Text = reader.GetString(4) };
                            cells[4] = new TableCell() { Text = reader.GetString(5) };


                            row.Cells.AddRange(cells);

                            ResultTable.Rows.Add(row);
                        }
                    }
                }
            }
            
            ResultTable.Visible = true;
        }
    }
}