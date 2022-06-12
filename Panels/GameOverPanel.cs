using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : Panel
{
    public TextHelper score;
    public TextHelper highScore;
    private GameManager game;

    public override void Start()
    {
        game = GameManager.instance;
        score.text += game.Score;
        highScore.text += game.HighScore;
    }
}
