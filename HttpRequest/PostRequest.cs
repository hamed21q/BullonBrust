using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class PostRequest : Request
{
	public PostRequest(ISerialization serialization) : base(serialization)
	{

	}
	public async Task<Response> Send(string url, WWWForm form)
    {
        try
        {
			using UnityWebRequest request = UnityWebRequest.Post(url, form);
			request.SetRequestHeader("Accept", serializationOption.contentType);
			var operation = request.SendWebRequest();
			while (!operation.isDone)
			{
				await Task.Yield();
			}
			return new Response(request.downloadHandler.text, request.responseCode);
		}
        catch (Exception)
        {
            throw new Exception();
        }
    }
	public async Task<Response> Send(string url, WWWForm form, string token)
	{
		try
		{
			using UnityWebRequest request = UnityWebRequest.Post(url, form);
			request.SetRequestHeader("Accept", serializationOption.contentType);
			request.SetRequestHeader("Authorization", "Bearer " + token);
			var operation = request.SendWebRequest();
			while (!operation.isDone)
			{
				await Task.Yield();
			}
			return new Response(request.downloadHandler.text, request.responseCode);
		}
		catch (Exception)
		{
			throw new Exception();
		}
	}
}
