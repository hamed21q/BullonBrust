using Newtonsoft.Json;

[System.Serializable]
public partial class UserFormat
{
    public User user;
    [JsonProperty("token")]
    public string token;
}
