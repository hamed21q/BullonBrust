using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardPanel : Panel
{
    public LeaderBoardRow rowPrefab;
    public RectTransform container;
    private GameManager game;
    public LeaderBoardList data => game.data.leaderBoardList;
    public override void Awake()
    {
        game = GameManager.instance;
    }
    private void OnEnable()
    {
        UpdataData();
        InstantiateLeaderBoard();
    }
    private void UpdataData()
    {
        GameManager.instance.UpdateLeaderBoard(RefreshList);
    }
    private void InstantiateLeaderBoard()
    {
        for (int i = 0; i < data.leaderBoardData.Length; i++)
        {
            GameObject obj = Instantiate(rowPrefab.gameObject, container);

            LeaderBoardRow row = obj.GetComponent<LeaderBoardRow>();
            row.name.text = data.leaderBoardData[i].username;
            row.score.text = data.leaderBoardData[i].score.ToString();
            row.rank.text = GenerateRank(i + 1);
        }
    }

    private string GenerateRank(int number)
    {
        switch (number)
        {
            case 1 : return "first";
            case 2 : return "second";
            case 3 : return "third";
            default : return number + "th";
        }
    }
    private void OnDisable()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
    public void RefreshList()
    {
        OnDisable();
        InstantiateLeaderBoard();
    }
}