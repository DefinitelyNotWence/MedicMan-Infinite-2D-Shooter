using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    public GameObject[] enemyPrefabs; // This is an array which is a GameObject that I can use to store multiple items, in this case I am using it to store the enemy prefabs
    public LevelManager levelManager;

    private float spawnRangeX = 25f;
    private float spawnRangeY = 25f;
    private float startDelay = 2f;
    public float spawnInterval;
    public int maxEnemies;
    public int enemyCount = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", startDelay, spawnInterval); // Invoke repeating starts a timer which continuously runs, it will run the specified method after a set time delay
        maxEnemies = 3;
    }

    private void SpawnEnemy()
    {
        if (levelManager.level < 5)
        {
            if (enemyCount < maxEnemies)
            {
                if (levelManager.enemiesLeft > enemyCount)
                {
                    int enemyIndex = 0;
                    Vector2 spawnPos = new Vector2(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY));
                    Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation); // Instantiate spawns in the specified prefab in a specified location within the game world
                    enemyCount += 1;
                }

            }
        }
        else if (levelManager.level >= 5) // an else if statement will only run if the previous if condition is not met, if this condition is met this code will run
        {
            if (enemyCount < maxEnemies)
            {
                if (levelManager.enemiesLeft > enemyCount)
                {
                    int enemyIndex = Random.Range(0, enemyPrefabs.Length);
                    Vector2 spawnPos = new Vector2(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY));
                    Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);
                    enemyCount += 1;
                }

            }
        }
    }
}
