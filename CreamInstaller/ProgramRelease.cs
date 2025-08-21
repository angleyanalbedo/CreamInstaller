using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CreamInstaller;

public class ProgramRelease
{
    private Asset asset;

    private string[] changes;

    private Version version;

    [JsonProperty("tag_name", NullValueHandling = NullValueHandling.Ignore)]
    public string TagName { get; set; }

    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    [JsonProperty("draft", NullValueHandling = NullValueHandling.Ignore)]
    public bool Draft { get; set; }

    [JsonProperty("prerelease", NullValueHandling = NullValueHandling.Ignore)]
    public bool Prerelease { get; set; }

    [JsonProperty("assets", NullValueHandling = NullValueHandling.Ignore)]
    public List<Asset> Assets { get; } = new();

    [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
    public string Body { get; set; }

    public Asset Asset => asset ??= Assets.FirstOrDefault(a => a.Name == Program.RepositoryPackage || a.Name == Program.RepositoryExecutable);

    public Version Version => version ??= new Version(TagName?.Substring(1) ?? "0.0.0");

    public string[] Changes => changes ??= (Body ?? "").Replace("- ", "").Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
}

public class Asset
{
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
    public int Size { get; set; }

    [JsonProperty("browser_download_url", NullValueHandling = NullValueHandling.Ignore)]
    public string BrowserDownloadUrl { get; set; }
}