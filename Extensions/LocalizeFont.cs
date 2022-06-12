using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;

[RequireComponent(typeof(LocalizeStringEvent))]
public class LocalizeFont : MonoBehaviour
{
    private TextMeshProUGUI text;
    public TMP_FontAsset persianFontAsset;
    protected virtual void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        LocalizationSettings.Instance.OnSelectedLocaleChanged += ChangeHandler;
    }
    void OnDisable() => LocalizationSettings.Instance.OnSelectedLocaleChanged -= ChangeHandler;
    IEnumerator Start()
    {
        // Wait for the localization system to initialize, loading Locales, preloading etc.
        yield return LocalizationSettings.InitializationOperation;
        ChangeHandler(LocalizationSettings.SelectedLocale);
    }

    protected void ChangeHandler(Locale value)
    {
        print("hello");
        if (value.Identifier.Code.Equals("fa-IR"))
        {
            text.font = persianFontAsset;
        }   
    }
}