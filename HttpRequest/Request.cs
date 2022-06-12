using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Request
{
    protected ISerialization serializationOption;
    public Request(ISerialization serialization)
    {
        serializationOption = serialization;
    }
}
