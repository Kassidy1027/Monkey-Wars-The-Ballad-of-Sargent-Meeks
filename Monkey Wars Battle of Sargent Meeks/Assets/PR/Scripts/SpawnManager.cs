using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    // Spawn Point locations
    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    // Enemy prefabs and their cost (will be merged later on)
    [Header("Enemy Types")]
    public GameObject[] enemyTypes;

    // keeps track of the round number (might get moved to another script later)
    private int roundNumber = 1;

    // keeps tracked of the number of enemies spawned and the total to spawn (prevents the round from increasing multiple times due to lack of immediate enemies)
    private int totalSpawns;
    private int enemiesSpawned;

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

    // Start is called before the first frame update
    void Start()
    {
        // resets round number, spawns the first wave, and updates UI
        roundNumber = 1;
        DecideSpawnPoints();

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
    }

    private void roundIncrease()
    {
        // reset values
        enemiesSpawned = 0;

        // increase the enemy value and cound by a percentage
        enemyVal *= enemyValIncrease;

        // decreases the total value to prevent values ramping out of control
        if (roundNumber % 25 == 0)
        {
            enemyVal /= (enemyValIncrease * 1.25f);
        }

        // increase round number and update text
        roundNumber++;
        UIT.UpdateRoundText(roundNumber);
        StatisticManager.UpdateStat("Rounds", 1);

        DecideSpawnPoints();
        DecideSpawns();
    }

    private void DecideSpawns()
    {
        int currVal = Mathf.CeilToInt(enemyVal);
        int enCount = 0;
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
                    StartCoroutine(SpawnObject(enemyTypes[i], Random.Range(0.1f, 15.5f)) );
                    currVal -= cost;

                    spawned = true;

                    enCount++;                        
                }

            }
        }
        totalSpawns = enCount;
    }

    /*
     * SPAWN AN ENEMY AT A RANDOM POINT
     */
    private IEnumerator SpawnObject(GameObject spawn, float time)
    {
        yield return new WaitForSecondsRealtime(time);

        Transform point;
        do
        {
            point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        } while (!point.gameObject.activeSelf);
             
        Vector3 sP = new Vector3(point.position.x + Random.Range(-1.0f, 1.0f), point.position.y, point.position.z + Random.Range(-1.0f, 1.0f));
        Instantiate(spawn, sP, Quaternion.identity);
        enemiesSpawned++;
    }

    /*
     * DECIDE SPAWNPOINTS
     */
    private void DecideSpawnPoints()
    {
        int num = Mathf.FloorToInt(roundNumber * 1.3f);

        if (num > spawnPoints.Length)
            num = spawnPoints.Length;

        foreach (Transform t in spawnPoints)
        {
            t.gameObject.SetActive(false);
        }

        while (num > 0)
        {
            int i = Random.Range(0, spawnPoints.Length - 1);
            if (!spawnPoints[i].gameObject.activeSelf)
            {
                spawnPoints[i].gameObject.SetActive(true);
                num--;
            }        
        }
    }


    // for checking if round can end
    private bool FinishedSpawningEnemies()
    {
        return enemiesSpawned >= totalSpawns;
    }

    private bool Chance(int max)
    {
        return Random.Range(0, max) == 0;
    }

}
