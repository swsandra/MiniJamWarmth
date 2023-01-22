using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate = 1;
    [SerializeField] GameObject[] enemyPrefabs;
    [Header("Health")]
    [SerializeField] int maxHealth;
    [SerializeField] int minHealth;
    [SerializeField] bool randomizeHealth;
    [Header("Instantiation positions")]
    [SerializeField] Vector3 bottomLeft;
    [SerializeField] Vector3 topRight;
    [Header("Other")]
    [SerializeField] int initialWait;
    [SerializeField] int initialEnemies;
    int randomHealth;
    float xRandomPos;
    float yRandomPos;
    

    void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
        for (int i = 0; i < initialEnemies; i++){
            SpawnEnemy();
        }
    }

    void SpawnEnemy(){
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        float xSize = enemyPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        float ySize = enemyPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        Vector2 halfEnemySize = new Vector2(xSize/2, ySize/2);
        bool validPosition = false;
        while (!validPosition){
            validPosition = true;
            xRandomPos = Random.Range(bottomLeft.x, topRight.x);
            yRandomPos = Random.Range(bottomLeft.y, topRight.y);
            Collider2D[] cols = Physics2D.OverlapBoxAll(new Vector3(xRandomPos, yRandomPos, 0), halfEnemySize, 0f);
            for (int i=0; i < cols.Length; i++){
                if (cols[i].CompareTag("Obstacle") || cols[i].CompareTag("Enemy")){ // Check Collision
                    // Debug.Log("Not valid position");
                    validPosition = false;
                }
            }
        }
        GameObject go = Instantiate(enemyPrefab, new Vector3(xRandomPos, yRandomPos, 0), Quaternion.identity);
        if (randomizeHealth){
            randomHealth = Random.Range(minHealth, maxHealth+1);
            go.GetComponent<Enemy>().health = randomHealth;
        }
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
