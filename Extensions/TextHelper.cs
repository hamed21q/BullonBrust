using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextHelper : MonoBehaviour
{
    private TMP_Text _tmpText;
    public TMP_Text tmpText => _tmpText ?? Load();

    public string text { get => tmpText.text; set => tmpText.text = value; }

    public TMP_Text Load()
    {
        _tmpText = GetComponent<TMP_Text>();
        return _tmpText;
    }
}
