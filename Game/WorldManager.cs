using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public TMPro.TMP_Text title;
    public List<Spawner> spawners;
    public List<World> worlds;

    public World currentWorld => worlds[game.data.selectedWorld];

    private GameManager game;

    private void Start()
    {
        game = GameManager.instance;
    }

    public void Play()
    {
        for (int i = 0; i < spawners.Count; i++)
        {
            if (i < currentWorld.spawns.Count)
            {
                spawners[i].spawn = currentWorld.spawns[i];
                spawners[i].Run();
            }
        }
    }

    public void GameOver()
    {
        for (int i = 0; i < spawners.Count; i++)
            spawners[i].Stop();
    }

    public void SetWorld(int index)
    {
        foreach (var item in worlds)
            item.SetActive(false);
        game.data.selectedWorld = (int)Mathf.Repeat(index, worlds.Count);
        worlds[game.data.selectedWorld].SetActive(true);
        title.text = worlds[game.data.selectedWorld].title;
    }
    public void ChangeWorld(int change)
    {
        SetWorld(game.data.selectedWorld + change);
    }

    public void Clear()
    {
        foreach (var item in spawners)
            item.Clear();
    }
    public int AvailableItemsCount(int index = 0)
    {
        return spawners[index].currentItemsCount();
    }
    public WorldData[] BuildWorlds()
    {
        var result = new WorldData[worlds.Count];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = new WorldData();
        }
        return result;
    }
}

[System.Serializable]
public class WorldData
{
    public float highscore;
    public int missionIndex;
    public object missionData;
}