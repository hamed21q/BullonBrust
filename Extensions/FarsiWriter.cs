using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(TextHelper))]
public class FarsiWriter : MonoBehaviour
{
    public bool convertOnAwake;
    private TextHelper text;

    private void Awake()
    {
        text = GetComponent<TextHelper>();
        /*
        text.AddListener((_text) =>
             text.SetTextWithoutCallback(Convert(_text)));
        if (convertOnAwake)
            text.SetTextWithoutCallback(Convert(text.text));*/
    }

    public static string Convert(string text)
    {
        var strArray = (text ?? "").Split('\n');
        var convertedStrArray = new List<string>();
        for (int j = 0; j < strArray.Length; j++)
        {
            var strInner = strArray[j].Split(' ');
            var convertedStr = "";
            for (var m = 0; m < strInner.Length; m++)
            {
                var str = strInner[m];
                var atleastOne = false;
                var convertedWord = "";
                for (int i = 0; i < str.Length; i++)
                {
                    var beforeChasban = false;
                    var afterChasban = false;
                    var currentCharPos = reqularAlphabet.ToList().IndexOf(str[i].ToString());
                    if (currentCharPos >= 0)
                    {
                        if (i > 0)
                        {
                            if (makesBeforeChasban.Contains(str[i - 1].ToString()))
                            {
                                atleastOne = true;
                                beforeChasban = true;
                            }
                            else
                            {
                                beforeChasban = false;
                            }
                        }
                        else
                        {
                            beforeChasban = false;
                        }
                        if (i < str.Length - 1)
                        {
                            if (makesAfterChasban.Contains(str[i + 1].ToString()))
                            {
                                atleastOne = true;
                                afterChasban = true;
                            }
                            else
                            {
                                afterChasban = false;
                            }
                        }
                        else
                        {
                            afterChasban = false;
                        }

                        if (!beforeChasban && !afterChasban)
                        {
                            convertedWord = arabicAlphabet[currentCharPos][0] + convertedWord;
                        }
                        if (beforeChasban && !afterChasban)
                        {
                            convertedWord = arabicAlphabet[currentCharPos][2] + convertedWord;
                        }
                        if (!beforeChasban && afterChasban)
                        {
                            convertedWord = arabicAlphabet[currentCharPos][1] + convertedWord;
                        }
                        if (beforeChasban && afterChasban)
                        {
                            convertedWord = arabicAlphabet[currentCharPos][3] + convertedWord;
                        }
                    }
                    else
                    {
                        if (atleastOne)
                        {
                            convertedWord = str[i] + convertedWord;
                        }
                        else
                        {
                            convertedWord += str[i];
                        }
                    }
                }
                convertedStr = convertedWord + " " + convertedStr;
            }

            convertedStrArray.Add(convertedStr);
        }
        var finalValue = convertedStrArray.Aggregate((i, j) => i + "\n" + j);
        var numberWord = "";
        for (var i = 0; i < finalValue.Length; i++)
        {
            if (numbers.ToList().IndexOf(finalValue[i].ToString()) >= 0)
            {
                numberWord += finalValue[i];
            }
            else
            {
                if (numberWord != "")
                {
                    var tempWord = numberWord.Reverse().ToString();
                    //console.log(finalValue.substring(0, i - tempWord.length));
                    finalValue = finalValue.Replace(numberWord, tempWord);
                    numberWord = "";
                }
            }
        }
        return finalValue.Trim();
    }

    public static string[] makesBeforeChasban = { "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??" };
    public static string[] makesAfterChasban = { "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??" };
    public static string[] reqularAlphabet = { "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "??", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", };
    public static string[] numbers = { "??", "??", "??", "??", "??", "??", "??", "??", "??", "??" };
    public static string[][] arabicAlphabet = {
        new string[] { "??", "??", "???", "???" },
        new string[] { "??", "??", "???", "???" },
        new string[] { "???", "???", "???", "???" },
        new string[] { "???", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "??", "???", "???" },
        new string[] { "??", "??", "???", "???" },
        new string[] { "??", "??", "???", "???" },
        new string[] { "??", "??", "???", "???" },
        new string[] { "??", "??", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "??? ", "???", "???" },
        new string[] { "??", "??? ", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "??", "???", "???" },
        new string[] { "???", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "??", "???", "???", "???" },
        new string[] { "???", "???", "???", "???" },
        new string[] { "???", "???", "???", "???" },
        new string[] { "??", "??", "??", "??" },
        new string[] { "??", "??", "??", "??" },
        new string[] { "??", "??", "??", "??" },
        new string[] { "??", "??", "??", "??" },
        new string[] { "??", "??", "??", "??" },
        new string[] { "??", "??", "??", "??" },
        new string[] { "??", "??", "??", "??" },
        new string[] { "??", "??", "??", "??" },
        new string[] { "??", "??", "??", "??" },
        new string[] { "??", "??", "??", "??" },
    };
}
