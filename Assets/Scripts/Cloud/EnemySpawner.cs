using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject cloud;
    public float totalWaitTime = 4f;

    private float currWaitTime;
    private int randomSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        currWaitTime = totalWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currWaitTime <= 0)
        {
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(cloud, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            currWaitTime = totalWaitTime;
        }
        else
        {
            currWaitTime -= Time.deltaTime;
        }

    }
}
