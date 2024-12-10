using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private class ToSpawn
    {
        public float timer;
        public GameObject enemy;

        public ToSpawn(float t, GameObject e)
        {
            timer = t;
            enemy = e;
        }
    }



    // Spawn Point locations
    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    // Enemy prefabs and their cost (will be merged later on)
    [Header("Enemy Types")]
    public GameObject[] enemyTypes;

    // keeps track of the round number (might get moved to another script later)
    private int roundNumber = 1;

    // keeps tracked of the number of enemies spawned and the total to spawn (prevents the round from increasing multiple times due to lack of immediate enemies)
    private float spawnTimer;
    private Stack<ToSpawn> spawnList = new Stack<ToSpawn>();

    // current enemy value (combined basic equivalent) and number of enemies to spawn
    [Header("Enemy Value and Total")]
    public float enemyVal = 20f;

    // how the round increase in difficulty (by percentage)
    [Header("Wave Increase")]
    public float enemyValIncrease = 1.02f;

    [HideInInspector]
    public GameObject[] enemies;

    // controls the UI (FOR TESTING ONLY)
    [Header("FOR TESTING, REMOVE LATER")]
    public UITextController UIT;
    private int spawnPointsNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        // resets round number, spawns the first wave, and updates UI
        roundNumber = 1;
        spawnTimer = 0.0f;
        spawnPointsNum = 0;
        DecideSpawnPoints(spawnPointsNum);

        DecideSpawns();

        UIT.UpdateRoundText(roundNumber);
    }

    // Update is called once per frame
    void Update()
    {
        // find all enemies in the scene
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        UIT.UpdateEnemyCount(enemies.Length);

        if (FinishedSpawningEnemies())
        {
            if (enemies.Length <= 0)
            {
                roundIncrease();
            }
        }
        else
        {
            // decrease the timer to spawn the next enemy
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0)
            {
                ToSpawn e = spawnList.Pop();
                SpawnObject(e.enemy);

                spawnTimer = e.timer;
            }
        }
    }

    private void roundIncrease()
    {
        //spawnList.Clear();

        // increase the enemy value and cound by a percentage
        enemyVal *= enemyValIncrease;

        // decreases the total value to prevent values ramping out of control
        if (roundNumber % 25 == 0)
        {
            enemyVal /= (enemyValIncrease * 1.25f);
        }
        if (roundNumber % 10 == 0) 
        {
            spawnPointsNum++;
        }


        // increase round number and update text
        roundNumber++;
        UIT.UpdateRoundText(roundNumber);
        StatisticManager.UpdateStat("Rounds", 1);


        DecideSpawnPoints(spawnPointsNum);
        DecideSpawns();
    }

    private void DecideSpawns()
    {
        int currVal = Mathf.CeilToInt(enemyVal);
        bool spawned = true;

        //while (enCount > 0)
        while(spawned)
        {
            spawned = false;
            for (int i = 0; i < enemyTypes.Length; i++)
            {
                // attempt to spawn in an enemy by checking its cost compared to the available cost this round.

                int cost = enemyTypes[i].GetComponent<EnemyBehavior>().cost;
                if (cost <= currVal / 2)
                {
                    // spawn the enemy and reduce the current cost
                    // also increase the number of enemies to track over the round
                    //Debug.Log("Spawning!!");
                    currVal -= cost;
                    spawnList.Push(new ToSpawn(Random.Range(0.1f, 5.0f), enemyTypes[i] ) );

                    spawned = true;
                       
                }

            }
        }
    }

    /*
     * SPAWN AN ENEMY AT A RANDOM POINT
     */
    private void SpawnObject(GameObject spawn)
    {
        Transform point;
        do
        {
            point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        } while (!point.gameObject.activeSelf);
             
        Vector3 sP = new Vector3(point.position.x + Random.Range(-1.0f, 1.0f), point.position.y, point.position.z + Random.Range(-1.0f, 1.0f));
        Instantiate(spawn, sP, Quaternion.identity);
    }

    /*
     * DECIDE SPAWNPOINTS
     */
    private void DecideSpawnPoints(int num)
    {
        for(int i = 0; i < num; i++)
        {
            GameObject p = spawnPoints[Random.Range(0, spawnPoints.Length)].gameObject;

            p.SetActive(true);

        }
    }


    // for checking if round can end
    private bool FinishedSpawningEnemies()
    {
        return spawnList.Count <= 0;
    }

    private bool Chance(int max)
    {
        return Random.Range(0, max) == 0;
    }

}
