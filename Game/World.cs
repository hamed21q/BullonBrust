    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public string title;
    public List<GameObject> objects;
    public List<Spawn> spawns;

    public void SetActive(bool value)
    {
        foreach (var obj in objects)
        {
            obj.SetActive(value);
        }
    }
}
