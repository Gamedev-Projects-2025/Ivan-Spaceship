using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    //creating a class for enemies
    [System.Serializable]
    public class Enemy
    {
        public int score; // the score each kill nets
        [SerializeField] public Mover prefabToSpawn; // mover to control the speed and direction
        [SerializeField] public Vector3 velocityOfSpawnedObject; // enemy speed
    }

    public List<Enemy> enemies;

    [Tooltip("Minimum time between consecutive spawns, in seconds")][SerializeField] float minTimeBetweenSpawns = 0.2f;
    [Tooltip("Maximum time between consecutive spawns, in seconds")][SerializeField] float maxTimeBetweenSpawns = 1.0f;
    [Tooltip("Maximum distance in X between spawner and spawned objects, in meters")][SerializeField] float maxXDistance = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnRoutine();
    }

    async void SpawnRoutine()
    {
        while (true)
        {
            float timeBetweenSpawnsInSeconds = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
            await Awaitable.WaitForSecondsAsync(timeBetweenSpawnsInSeconds);       // co-routines
            if (!this) break;

            //generating a random number for the enemy to spawn
            int randomNum = Random.Range(0,enemies.Count);

            Vector3 positionOfSpawnedObject = new Vector3(
            transform.position.x + Random.Range(-maxXDistance, +maxXDistance),
            transform.position.y,
            transform.position.z);

            //choosing a random enemy to spawn
            GameObject newObject = Instantiate(enemies[randomNum].prefabToSpawn.gameObject, positionOfSpawnedObject, Quaternion.identity);
            newObject.GetComponent<Mover>().SetVelocity(enemies[randomNum].velocityOfSpawnedObject);

            EnemyScore enemyScore = newObject.GetComponent<EnemyScore>();
            if (enemyScore != null)
            {
                enemyScore.scoreValue = enemies[randomNum].score;
            }
        }
    }
}
