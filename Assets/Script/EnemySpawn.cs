using System.Collections;
using UnityEngine;
using System;
using System.Numerics;
using Random = UnityEngine.Random;
//using Quaternion = UnityEngine.Quaternion;

public class EnemySpawn : MonoBehaviour
{
    PlayerC playerC = new PlayerC();
    [SerializeField] private float spawnRate = 1f;

    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] BossPrefabs;

    [SerializeField] private Transform[] SpawnPoint;
    [SerializeField] private bool canSpawn = true;
    
    public void FixedUpdate()
    {
        

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while (canSpawn)
        {
            yield return wait;
            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];
            int randP = Random.Range(0, SpawnPoint.Length);

            int RandSpawnPoint = randP;
            Instantiate(enemyToSpawn, SpawnPoint[randP].position, transform.rotation);
        }
    }

    public void SpawnA()
    {
        StartCoroutine(Spawner());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
