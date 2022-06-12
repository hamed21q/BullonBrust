using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleLanguage : MonoBehaviour
{
    public LanguageManager lm;
    private Toggle toggle;
    private void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(ChangeLanguage);
    }
    public void ChangeLanguage(bool value)
    {
        lm.ChangeLanguage(value);
    }
}
