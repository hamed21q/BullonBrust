using Newtonsoft.Json;

[System.Serializable]
public partial class User
{
    [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
    public string username;
    [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
    public string email;
    [JsonProperty("score", NullValueHandling = NullValueHandling.Ignore)]
    public int score;
}
