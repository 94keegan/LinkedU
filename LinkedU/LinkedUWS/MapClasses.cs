using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkedUWS
{


    public class Path
    {
        private TextValue _distance;
        private TextValue _duration;
        private string _source;
        private string _destination;

        public Path ()
        {

        }

        public TextValue Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }
        public TextValue Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public string SourceAddress
        {
            get { return _source; }
            set { _source = value; }
        }

        public string DestinationAddress
        {
            get { return _destination; }
            set { _destination = value; }
        }
    }

    public class Geolocation
    {
        private float _latitude;
        private float _longitude;
        private string _formattedaddress;

        public float Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
        public float Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        public string FormattedAddress
        {
            get { return _formattedaddress; }
            set { _formattedaddress = value; }
        }
    }
}