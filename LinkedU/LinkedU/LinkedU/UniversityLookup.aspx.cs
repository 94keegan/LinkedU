using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Configuration;

namespace LinkedU
{
    public partial class UniversityLookup : System.Web.UI.Page
    {

        string connStr = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;
        Regex urltester = new Regex("^(https?://)?(?:www\\.)?[A-Za-z0-9\\-\\.]+\\.edu(/?)");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["uid"] != null)
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    using (SqlCommand comm = conn.CreateCommand())
                    {
                        comm.CommandText = "SELECT INSTNM as [Name], ADDR as [Address], CITY as [City]," +
                            "STABBR as [State], ZIP as [Zip Code], CHFNM as [Administrator], CHFTITLE as [Title], " +
                            "GENTELE as [Telephone], " +
                            "CASE " +
                            " WHEN OBEREG = 0 THEN 'US Service schools' " +
                            " WHEN OBEREG = 1 THEN 'New England' " +
                            " WHEN OBEREG = 2 THEN 'Mid East' " +
                            " WHEN OBEREG = 3 THEN 'Great Lakes' " +
                            " WHEN OBEREG = 4 THEN 'Plains' " +
                            " WHEN OBEREG = 5 THEN 'Southeast' " +
                            " WHEN OBEREG = 6 THEN 'Southwest' " +
                            " WHEN OBEREG = 7 THEN 'Rocky Mountains' " +
                            " WHEN OBEREG = 8 THEN 'Far West' " +
                            " WHEN OBEREG = 9 THEN 'Outlying areas' " +
                            "END as [Region], " +
                            "CASE " +
                            " WHEN HLOFFER = 1 THEN 'Award of less than one academic year' " +
                            " WHEN HLOFFER = 2 THEN 'At least 1, but less than 2 academic yrs' " +
                            " WHEN HLOFFER = 3 THEN 'Associate''s degree' " +
                            " WHEN HLOFFER = 4 THEN 'At least 2, but less than 4 academic yrs' " +
                            " WHEN HLOFFER = 5 THEN 'Bachelor''s degree' " +
                            " WHEN HLOFFER = 6 THEN 'Postbaccalaureate certificate' " +
                            " WHEN HLOFFER = 7 THEN 'Master''s degree' " +
                            " WHEN HLOFFER = 8 THEN 'Post-master''s certificate' " +
                            " WHEN HLOFFER = 9 THEN 'Doctor''s degree' " +
                            " WHEN HLOFFER = -3 THEN '{Not available}' " +
                            "END as [Highest Offering], " +
                            "WEBADDR as [Web Address], APPLURL [Applications] " +
                            "FROM universities WHERE UNITID = @uid";

                        comm.Parameters.Add("@uid", SqlDbType.Float).Value = Request.QueryString["uid"];

                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                Dictionary<string, string> properties = new Dictionary<string, string>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {

                                    properties.Add(reader.GetName(i), reader.GetValue(i).ToString());

                                    if (reader.GetValue(i).ToString().Trim().Length > 0)
                                    {
                                        Panel field = new Panel();
                                        Label property = new Label()
                                        {
                                            Text = reader.GetName(i),
                                            Width = 130,
                                            EnableTheming = true
                                        };
                                        property.Font.Bold = true;
                                        field.Controls.Add(property);

                                        if (urltester.IsMatch(reader.GetValue(i).ToString()))
                                        {
                                            UriBuilder builder = new UriBuilder(reader.GetValue(i).ToString());
                                            HyperLink url = new HyperLink()
                                            {
                                                Text = reader.GetValue(i).ToString(),
                                                NavigateUrl = builder.Uri.AbsoluteUri,
                                                Target = "_blank"
                                            };
                                            field.Controls.Add(url);
                                        }
                                        else
                                        {
                                            Label value = new Label()
                                            {
                                                Text = reader.GetValue(i).ToString()
                                            };
                                            field.Controls.Add(value);
                                        }


                                        UniversityInformation.Controls.Add(field);
                                    }
                                }

                                string destination = String.Format("{0},{1},{2},{3}", properties["Name"].Replace(" ", "+"), properties["Address"].Replace(" ", "+"), properties["City"].Replace(" ", "+"), properties["State"].Replace(" ", "+"));
                                string source = "";

                                if (Session["UserName"] != null)
                                {
                                    source = "1305+Welling+St,Bloomington+IL,61701";
                                }
                                
                                if (source == "")
                                {
                                    Control iframe = new LiteralControl(String.Format("<iframe width=\"100%\" height=\"300\" frameborder=\"0\" scrolling=\"no\" marginheight=\"0\" marginwidth=\"0\" src=\"https://www.google.com/maps/embed/v1/place?key={0}&q={1}\" ></iframe>", WebConfigurationManager.AppSettings.Get("GoogleMapsApiKey"), destination));
                                    UniversityMap.Controls.Add(iframe);
                                } else
                                { 
                                    Control iframe = new LiteralControl(String.Format("<iframe width=\"100%\" height=\"300\" frameborder=\"0\" scrolling=\"no\" marginheight=\"0\" marginwidth=\"0\" src=\"https://www.google.com/maps/embed/v1/directions?key={0}&origin={1}&destination={2}\" ></iframe>", WebConfigurationManager.AppSettings.Get("GoogleMapsApiKey"), source, destination));
                                    UniversityMap.Controls.Add(iframe);
                                }

                            }
                            
                        }
                    }
                }
            }
        }
    }
}