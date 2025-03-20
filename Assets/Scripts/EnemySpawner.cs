using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform spawnPositionTransform;
    [SerializeField] int spawnCount = 2;
    [SerializeField] float initialWait = 1f;
    [SerializeField] float waitForNext = 2f; 
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(initialWait);
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(enemyPrefab, spawnPositionTransform.position, Quaternion.identity);
            yield return new WaitForSeconds(waitForNext);
        }
        
    }
}
