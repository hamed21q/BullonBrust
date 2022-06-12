using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonSerialization : ISerialization
{
    public string contentType => "aplication/json";

    public T Deserialize<T>(string text)
    {
        try
        {
            var result = JsonConvert.DeserializeObject<T>(text);
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Could not parse response {text}. {ex.Message}");
            return default;
        }
    }
}