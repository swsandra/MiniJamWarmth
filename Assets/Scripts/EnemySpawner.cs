using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate = 1;
    [SerializeField] GameObject[] enemyPrefab;
    [Header("Health")]
    [SerializeField] int maxHealth;
    [SerializeField] int minHealth;
    [Header("Instantiation positions")]
    [SerializeField] Vector3 bottomLeft;
    [SerializeField] Vector3 topRight;
    [Header("Other")]
    [SerializeField] int initialWait;
    [SerializeField] int initialEnemies;
    Vector3 randomPosition;
    int randomHealth;
    

    void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
        for (int i = 0; i < initialEnemies; i++){
            SpawnEnemy();
        }
    }

    void SpawnEnemy(){
        randomHealth = Random.Range(minHealth, maxHealth+1);
        // TODO: fix que no hagan spawn encima de otros
        randomPosition = new Vector3(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y), 0);
        GameObject go = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], randomPosition, Quaternion.identity);
        go.GetComponent<Enemy>().health = randomHealth;
        GameManager.instance.TotalFactories += 1;
    }

    IEnumerator SpawnEnemyCoroutine(){
        yield return new WaitForSeconds(initialWait);
        while (GameManager.instance.Time > 0){
            yield return new WaitForSeconds(1/spawnRate);
            SpawnEnemy();
        }
    }
}
