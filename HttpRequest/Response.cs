public class Response
{
	public string json;
	public long responseCode;
    public Response(string json, long status)
    {
		this.json = json;
		this.responseCode = status;
    }
}