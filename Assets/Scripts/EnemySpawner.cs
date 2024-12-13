using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float timeToSpawn = 1.0f;
    private float spawnCounter;
    private Transform target;
    public Transform minPoint, maxPoint;
    private float despawnDistance = 10.0f;
    private List<GameObject> spawnedEnmies = new List<GameObject>();

    public int checkPerFrame;
    private int checkCount;

    public List<WaveInfo> waves;

    private int currentWave;

    private float waveCounter;
    // Start is called before the first frame update
    void Start()
    {
        spawnCounter = timeToSpawn;
        target = PlayerHealthController.instance.transform;
        despawnDistance = Vector3.Distance(transform.position, maxPoint.position) + 4f;

        currentWave = -1;
        GotoNextWave();
    }

    void Update()
    {
        SpawnEnemy();
    }

    /// <summary>
    /// 生成敌人方法
    /// </summary>
    private void SpawnEnemy()
    {
        if (PlayerHealthController.instance.gameObject.activeSelf)
        {
            if (currentWave < waves.Count)
            {
                waveCounter -= Time.deltaTime;
                if (waveCounter <= 0)
                {
                    GotoNextWave();
                }
                spawnCounter -= Time.deltaTime;
                if (spawnCounter <= 0)
                {
                    spawnCounter = waves[currentWave].timeBeteenSpawns;

                    GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);
                    spawnedEnmies.Add(newEnemy);
                }
            }

        }

        transform.position = target.position;

        int checkTarget = checkCount + checkPerFrame;

        while (checkCount < checkTarget)
        {
            if (checkCount < spawnedEnmies.Count)
            {
                if (spawnedEnmies[checkCount] != null)
                {
                    if (Vector3.Distance(transform.position, spawnedEnmies[checkCount].transform.position) > despawnDistance)
                    {
                        Destroy(spawnedEnmies[checkCount]);
                        spawnedEnmies.RemoveAt(checkCount);
                        checkTarget--;
                    }
                    else
                    {
                        checkCount++;
                    }
                }
                else
                {
                    spawnedEnmies.RemoveAt(checkCount);
                    checkTarget--;
                }
            }
            else
            {
                checkCount = 0;
                checkTarget = 0;

            }

        }
    }

    /// <summary>
    /// 生成位置
    /// </summary>
    /// <returns></returns>
    public Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;
        bool spawnVerticalEdge = Random.Range(0f, 1f) > .5f;

        if (spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(minPoint.position.y, maxPoint.position.y);

            if (Random.Range(0f, 1.0f) > .5f)
            {
                spawnPoint.x = maxPoint.position.x;
            }
            else
            {
                spawnPoint.x = minPoint.position.x;
            }
        }
        else
        {
            spawnPoint.x = Random.Range(minPoint.position.x, maxPoint.position.x);

            if (Random.Range(0f, 1.0f) > .5f)
            {
                spawnPoint.y = maxPoint.position.y;
            }
            else
            {
                spawnPoint.y = minPoint.position.y;
            }

        }
        return spawnPoint;
    }

    public void GotoNextWave()
    {
        currentWave++;

        if (currentWave >= waves.Count)
        {
            currentWave = waves.Count - 1;
        }

        waveCounter = waves[currentWave].waveLength;
        spawnCounter = waves[currentWave].timeBeteenSpawns;
    }
}

[System.Serializable]
public class WaveInfo
{
    public GameObject enemyToSpawn;
    public float waveLength = 10.0f;

    public float timeBeteenSpawns = 1.0f;
}
