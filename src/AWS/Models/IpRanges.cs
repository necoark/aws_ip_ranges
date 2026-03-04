using System.Text.Json.Serialization;

namespace AWS.Models;

public class IpRanges
{
    [JsonPropertyName("syncToken")]
    public string SyncToken { get; set; }

    [JsonPropertyName("createDate")]
    public string CreateDate { get; set; }

    [JsonPropertyName("prefixes")]
    public List<PrefixDesc> Prefixes { get; set; }
}

public class PrefixDesc
{
    [JsonPropertyName("ip_prefix")]
    public string IpPrefix { get; set; }

    [JsonPropertyName("region")]
    public string Region { get; set; }

    [JsonPropertyName("service")]
    public string Service { get; set; }

    [JsonPropertyName("network_border_group")]
    public string NetworkBorderGroup { get; set; }
}