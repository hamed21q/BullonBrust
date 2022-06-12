using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public class Spawner : MonoBehaviour
{
    public List<Pattern> patterns;

    private ObjectPooler pool;

    private bool isActive;
    private float lastWave;
    private float lastSpawn;
    private int currentCount;
    private Pattern pattern;

    public Spawn spawn;

    private List<PoolObject> currentObjects = new List<PoolObject>();

    private void Start()
    {
        pool = GetComponent<ObjectPooler>();
        currentObjects.Clear();
    }

    private void Update()
    {
        if (isActive && spawn.prefabs.Count > 0)
        {
            if (Time.time > lastWave + spawn.waveTime)
            {
                lastWave = Time.time;
                currentCount = 0;
                var key = spawn.patterns.Length > 0 ? spawn.randomPattern.key : "";
                pattern = spawn.patterns.Length > 0 ? patterns.Find(p => p.key == key) : patterns[0];
                if (pattern == null) pattern = patterns[0];
            }
            if (currentCount >= 0 && Time.time > lastSpawn + spawn.spawnTime && currentCount < spawn.count)
            {
                lastSpawn = Time.time;
                currentCount++;
                var point = pattern.spawnPoints.Random();
                var offset = new Vector3(Random.Range(-pattern.randomOffset, pattern.randomOffset), 0);
                var rotation = Random.Range(-pattern.randomRotation, pattern.randomOffset);
                var obj = pool.Spawn(spawn.prefabs.Random(),
                    point.position + offset,
                    Quaternion.Euler(point.eulerAngles + Vector3.forward * rotation));
                currentObjects.Add(obj);
            }
        }
    }

    public void Run()
    {
        isActive = true;
        lastWave = Time.time;
        lastSpawn = Time.time;
        currentCount = -1;
    }
    public void Stop()
    {
        isActive = false;
        Clear();
    }

    public void Clear()
    {
        foreach (var item in currentObjects)
            item.Destroy();
        currentObjects.Clear();
    }
    public int currentItemsCount()
    {
        return currentObjects.Count;
    }
}

[System.Serializable]
public class Spawn
{
    public float waveTime;
    public float spawnTime;
    public float count;
    public Probability[] patterns;
    public List<PoolObject> prefabs;

    public Probability randomPattern
    {
        get
        {
            var sum = 0f;
            foreach (var item in patterns)
                sum += item.chance;
            var random = Random.Range(0f, sum);
            sum = 0;
            foreach (var item in patterns)
            {
                sum += item.chance;
                if (sum > random)
                    return item;
            }
            return patterns[0];
        }
    }

    [System.Serializable]
    public class Probability
    {
        public string key = "";
        public float chance = 1;
    }
}

[System.Serializable]
public class Pattern
{
    public string key = "";
    public float randomRotation;
    public float randomOffset;
    public List<Transform> spawnPoints;
}