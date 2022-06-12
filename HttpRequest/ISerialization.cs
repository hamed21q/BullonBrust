using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISerialization
{
    string contentType { get; }
    T Deserialize<T>(string text);
}
