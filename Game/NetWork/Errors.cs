using Newtonsoft.Json;

[System.Serializable]
public partial class Errors
{
    [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
    public string[] Username { get; set; }

    [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
    public string[] Email { get; set; }

    [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
    public string[] Password { get; set; }
}
