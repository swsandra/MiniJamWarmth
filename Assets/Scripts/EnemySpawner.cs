using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate = 1;
    [SerializeField] int maxHealth;
    [SerializeField] int minHealth;
    [SerializeField] GameObject[] enemyPrefab;
    [SerializeField] Vector3 bottomLeft;
    [SerializeField] Vector3 topRight;
    Vector3 randomPosition;
    

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy(){
        while (true){
            yield return new WaitForSeconds(1/spawnRate);
            int randomHealth = Random.Range(minHealth, maxHealth+1);
            // TODO: fix que no hagan spawn encima de otros
            randomPosition = new Vector3(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y), 0);
            GameObject go = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], randomPosition, Quaternion.identity);
            go.GetComponent<Enemy>().health = randomHealth;
        }
    }
}
