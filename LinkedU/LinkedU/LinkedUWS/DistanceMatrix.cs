using Newtonsoft.Json;

public class DistanceMatrix
{
    [JsonProperty(PropertyName = "destination_addresses")]
    public string[] DestinationAddress;

    [JsonProperty(PropertyName = "origin_addresses")]
    public string[] SourceAddress;

    [JsonProperty(PropertyName = "status")]
    public string Status;

    [JsonProperty(PropertyName = "rows")]
    public Origin[] Origins;
}

public class Origin
{
    [JsonProperty(PropertyName = "elements")]
    public Path[] Paths;
}

public class Path
{
    [JsonProperty(PropertyName = "distance")]
    public TextValue Distance;

    [JsonProperty(PropertyName = "duration")]
    public TextValue Duration;

    [JsonProperty(PropertyName = "status")]
    public string Status;

}

public class TextValue
{
    [JsonProperty(PropertyName = "text")]
    public string Text;

    [JsonProperty(PropertyName = "value")]
    public int Value;
}