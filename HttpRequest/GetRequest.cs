using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class GetRequest : Request
{
    public GetRequest(ISerialization serialization) : base(serialization)
    {

    }
    public async Task<T> Send<T>(string url)
    {
        try
        {
            using UnityWebRequest request = UnityWebRequest.Get(url);
            request.SetRequestHeader("Content-Type", serializationOption.contentType);
            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed: {request.error}");
            }
            var result = serializationOption.Deserialize<T>(request.downloadHandler.text);
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError($"{nameof(Send)} failed: {ex.Message}");
            return default;
        }
    }
}
