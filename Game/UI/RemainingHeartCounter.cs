using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainingHeartCounter : MonoBehaviour
{
    public GameObject heartPrefab;
    public Transform container;
    private List<GameObject> hearts;
    private void Start()
    {
        hearts = new List<GameObject>();
        GameManager.instance.OnLooseHeart += removeHeart;
        GameManager.instance.OnGameStarted += GenerateHeartUi;
    }

    private void GenerateHeartUi()
    {
        ClearContainer();
        hearts.Clear();
        for (int i = 0; i < GameManager.instance.maxHealth; i++)
        {
            hearts.Add(Instantiate(heartPrefab, container));
        }
    }
    private void ClearContainer()
    {
        foreach (Transform item in container)
        {
            Destroy(item.gameObject);
        }
    }
    public void removeHeart()
    {
        if (hearts[hearts.Count - 1] == null) return;
        Destroy(hearts[hearts.Count - 1]);
        hearts.RemoveAt(hearts.Count - 1);
    }
}
