using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;

namespace LinkedUWS
{
    /// <summary>
    /// Summary description for DistanceFinder
    /// </summary>
    [WebService(Namespace = "http://iis2.it.ilstu.edu/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DistanceFinder : System.Web.Services.WebService
    {

        private string apiKey = WebConfigurationManager.AppSettings.Get("GoogleMapsApiKey");

        [WebMethod]
        public Path GetDistance(float sourceLat, float sourceLong, float destLat, float destLong)
        {
            Path path = new Path();

            try
            {
                using (WebClient wc = new WebClient())
                {

                    try
                    {

                        Uri uri = new Uri(String.Format("https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins={0},{1}&destinations={2},{3}", sourceLat, sourceLong, destLat, destLong));
                        string json = wc.DownloadString(uri);

                        if (json != null)
                        {

                            DistanceMatrix dm = JsonConvert.DeserializeObject<DistanceMatrix>(json);

                            if (dm.Status == "OK")
                            {

                                path.Distance = dm.Origins.First().Paths.First().Distance;
                                path.Duration = dm.Origins.First().Paths.First().Duration;
                                path.SourceAddress = dm.SourceAddress[0];
                                path.DestinationAddress = dm.DestinationAddress[0];

                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            } catch
            {

            }

            return path;
        }

        [WebMethod]
        public Geolocation GetLocation(string street, string city, string state, string zip)
        {
            Geolocation geo = new Geolocation();

            try
            {
                using (WebClient wc = new WebClient())
                {

                    try
                    {
                        Uri uri = new Uri(String.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0},{1},{2},{3}&key={4}", street.Replace(" ", "+"), city.Replace(" ", "+"), state, zip, apiKey));
                        string json = wc.DownloadString(uri);

                        if (json != null)
                        {

                            AddressResult address = JsonConvert.DeserializeObject<AddressResult>(json);

                            if (address.Status == "OK")
                            {
                                geo.Latitude = address.Results.First().Geometry.Location.Latitude;
                                geo.Longitude = address.Results.First().Geometry.Location.Longitude;
                                geo.FormattedAddress = address.Results.First().FormattedAddress;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch
            {

            }

            return geo;
        }

    }
}
