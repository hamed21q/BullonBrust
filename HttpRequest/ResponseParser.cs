using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ResponseParser
{
    public ISerialization serialization;
    public ResponseParser(ISerialization serialization)
    {
        this.serialization = serialization;
    }
    public T parse<T>(string json)
    {
        return serialization.Deserialize<T>(json);
    }
    
}
