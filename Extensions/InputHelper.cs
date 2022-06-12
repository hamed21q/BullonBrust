using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class InputHelper : MonoBehaviour
{
    private TMP_InputField _tmpInputField;
    public TMP_InputField tmpInputField => _tmpInputField ?? Load();

    public string text { get => tmpInputField.text; set => tmpInputField.text = value; }

    public TMP_InputField Load()
    {
        _tmpInputField = GetComponent<TMP_InputField>();
        return _tmpInputField;
    }
}
