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

    public static string[] makesBeforeChasban = { "ب", "پ", "ت", "ث", "ج", "چ", "ح", "خ", "س", "ش", "ص", "ض", "ط", "ظ", "ع", "غ", "ف", "ق", "ک", "ك", "گ", "ل", "م", "ن", "ه", "ی", "ي", "ئ" };
    public static string[] makesAfterChasban = { "ا", "آ", "أ", "إ", "ب", "پ", "ت", "ث", "ج", "چ", "ح", "خ", "د", "ذ", "ر", "ز", "ژ", "س", "ش", "ص", "ض", "ط", "ظ", "ع", "غ", "ف", "ق", "ک", "ك", "گ", "ل", "م", "ن", "و", "ه", "ی", "ي", "ؤ", "ئ" };
    public static string[] reqularAlphabet = { "ا", "آ", "أ", "إ", "ب", "پ", "ت", "ث", "ج", "چ", "ح", "خ", "د", "ذ", "ر", "ز", "ژ", "س", "ش", "ص", "ض", "ط", "ظ", "ع", "غ", "ف", "ق", "ک", "ك", "گ", "ل", "م", "ن", "و", "ؤ", "ه", "ی", "ي", "ئ", "ء", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", };
    public static string[] numbers = { "١", "٢", "٣", "٤", "٥", "٦", "٧", "٨", "٩", "٠" };
    public static string[][] arabicAlphabet = {
        new string[] { "ا", "ا", "ﺎ", "ﺎ" },
        new string[] { "آ", "آ", "ﺂ", "ﺂ" },
        new string[] { "ﺃ", "ﺃ", "ﺄ", "ﺄ" },
        new string[] { "ﺇ", "ﺃ", "ﺈ", "ﺈ" },
        new string[] { "ب", "ﺑ", "ﺐ", "ﺒ" },
        new string[] { "پ", "ﭘ", "ﭗ", "ﭙ" },
        new string[] { "ت", "ﺗ", "ﺖ", "ﺘ" },
        new string[] { "ث", "ﺛ", "ﺚ", "ﺜ" },
        new string[] { "ج", "ﺟ", "ﺞ", "ﺠ" },
        new string[] { "چ", "ﭼ", "ﭻ", "ﭽ" },
        new string[] { "ح", "ﺣ", "ﺢ", "ﺤ" },
        new string[] { "خ", "ﺧ", "ﺦ", "ﺨ" },
        new string[] { "د", "د", "ﺪ", "ﺪ" },
        new string[] { "ذ", "ذ", "ﺬ", "ﺬ" },
        new string[] { "ر", "ر", "ﺮ", "ﺮ" },
        new string[] { "ز", "ز", "ﺰ", "ﺰ" },
        new string[] { "ژ", "ژ", "ﮋ", "ﮋ" },
        new string[] { "س", "ﺳ", "ﺲ", "ﺴ" },
        new string[] { "ش", "ﺷ", "ﺶ", "ﺸ" },
        new string[] { "ص", "ﺻ", "ﺺ", "ﺼ" },
        new string[] { "ض", "ﺿ", "ﺾ", "ﻀ" },
        new string[] { "ط", "ﻃ", "ﻂ", "ﻄ" },
        new string[] { "ظ", "ﻇ", "ﻆ", "ﻈ" },
        new string[] { "ع", "ﻋ", "ﻊ", "ﻌ" },
        new string[] { "غ", "ﻏ", "ﻎ", "ﻐ" },
        new string[] { "ف", "ﻓ", "ﻒ", "ﻔ" },
        new string[] { "ق", "ﻗ", "ﻖ", "ﻘ" },
        new string[] { "ک", "ﻛ ", "ﻚ", "ﻜ" },
        new string[] { "ک", "ﻛ ", "ﻚ", "ﻜ" },
        new string[] { "گ", "ﮔ", "ﮓ", "ﮕ" },
        new string[] { "ل", "ﻟ", "ﻞ", "ﻠ" },
        new string[] { "م", "ﻣ", "ﻢ", "ﻤ" },
        new string[] { "ن", "ﻧ", "ﻦ", "ﻨ" },
        new string[] { "و", "و", "ﻮ", "ﻮ" },
        new string[] { "ﺅ", "ﺅ", "ﺆ", "ﺆ" },
        new string[] { "ه", "ﻫ", "ﻪ", "ﻬ" },
        new string[] { "ی", "ﯾ", "ﯽ", "ﯿ" },
        new string[] { "ی", "ﯾ", "ﯽ", "ﯿ" },
        new string[] { "ﺉ", "ﺋ", "ﺊ", "ﺌ" },
        new string[] { "ﺀ", "ﺀ", "ﺀ", "ﺀ" },
        new string[] { "١", "١", "١", "١" },
        new string[] { "٢", "٢", "٢", "٢" },
        new string[] { "٣", "٣", "٣", "٣" },
        new string[] { "٤", "٤", "٤", "٤" },
        new string[] { "٥", "٥", "٥", "٥" },
        new string[] { "٦", "٦", "٦", "٦" },
        new string[] { "٧", "٧", "٧", "٧" },
        new string[] { "٨", "٨", "٨", "٨" },
        new string[] { "٩", "٩", "٩", "٩" },
        new string[] { "٠", "٠", "٠", "٠" },
    };
}
