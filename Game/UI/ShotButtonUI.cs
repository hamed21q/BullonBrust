using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotButtonUI : MonoBehaviour
{
    public TextHelper text;
    private GameManager game;
    private void Start()
    {
        game = GameManager.instance;
        game.OnBulletNumberChanges += OnChange;
        gameObject.SetActive(game.data.achivements.bullets > 0);

    }
    public void OnChange()
    {
        text.text = game.data.achivements.bullets.ToString();
        gameObject.SetActive(game.data.achivements.bullets > 0);
    }
}
