using System.Text.Json;
using AWS.Models;

const string url = "https://ip-ranges.amazonaws.com/ip-ranges.json";
const string downloadedFileName = "downloaded_raw.json";
const string ipsFileName = "aws_ip_ranges.json";

using (var client = new HttpClient())
{
    await using var s = await client.GetStreamAsync(url);
    await using var fs = new FileStream(downloadedFileName, FileMode.Create);
    await s.CopyToAsync(fs);
}

var jsonContent = File.ReadAllText(downloadedFileName);
var res = JsonSerializer.Deserialize<IpRanges>(jsonContent);

var it = 0;
var antiDuplicateList = new List<string>();
if (res?.Prefixes != null)
{
    await using var writer = new StreamWriter(ipsFileName);
    foreach (var prefix in res.Prefixes)
    {
        if (!antiDuplicateList.Contains(prefix.IpPrefix))
        {
            writer.WriteLine(prefix.IpPrefix);
            antiDuplicateList.Add(prefix.IpPrefix);
            it++;
        }
    }
}
else
{
    Console.WriteLine("Failed to deserialize prefixes");
}

Console.WriteLine($"Work done, parsed and converted {it} ip ranges to {ipsFileName} file");