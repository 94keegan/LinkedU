using Newtonsoft.Json;

public class AddressResult
{
    [JsonProperty(PropertyName = "results")]
    public Result[] Results;

    [JsonProperty(PropertyName = "status")]
    public string Status;
}

public class Result
{
    [JsonProperty(PropertyName = "address_components")]
    public AddressComponent[] AddressComponents;

    [JsonProperty(PropertyName = "formatted_address")]
    public string FormattedAddress;

    [JsonProperty(PropertyName = "geometry")]
    public Geometry Geometry;

    [JsonProperty(PropertyName = "partial_match")]
    public bool PartialMatch;

    [JsonProperty(PropertyName = "place_id")]
    public string PlaceID;

    [JsonProperty(PropertyName = "types")]
    public string[] Types;
}

public class AddressComponent
{
    [JsonProperty(PropertyName = "long_name")]
    public string LongName;

    [JsonProperty(PropertyName = "short_name")]
    public string ShortName;

    [JsonProperty(PropertyName = "types")]
    public string[] Types;
}

public class Geometry
{
    [JsonProperty(PropertyName = "bounds")]
    public Bounds Bounds;

    [JsonProperty(PropertyName = "location")]
    public Coordinates Location;

    [JsonProperty(PropertyName = "location_type")]
    public string LocationType;

    [JsonProperty(PropertyName = "viewport")]
    public Bounds ViewPort;
}

public class Bounds
{
    [JsonProperty(PropertyName = "northeast")]
    public Coordinates NorthEast;

    [JsonProperty(PropertyName = "southwest")]
    public Coordinates SouthWest;
}

public class Coordinates
{
    [JsonProperty(PropertyName = "lat")]
    public float Latitude;

    [JsonProperty(PropertyName = "lng")]
    public float Longitude;
}