using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputField : MonoBehaviour
{
    public new string name;
    public TextHelper error;
    [HideInInspector] public InputHelper input;
    private void Start()
    {
        input = GetComponent<InputHelper>();
    }

}
