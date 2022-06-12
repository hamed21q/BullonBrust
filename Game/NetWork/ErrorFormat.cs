using Newtonsoft.Json;

[System.Serializable]
public partial class ErrorFormat
{
    [JsonProperty("message")]
    public string message;
    public Errors errors;

}
