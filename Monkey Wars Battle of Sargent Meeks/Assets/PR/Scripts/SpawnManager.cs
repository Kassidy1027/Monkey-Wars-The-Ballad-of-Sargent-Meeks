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
    public int[] enemyCost;

    // keeps track of the round number (might get moved to another script later)
    private int roundNumber = 1;

    // keeps tracked of the number of enemies spawned and the total to spawn (prevents the round from increasing multiple times due to lack of immediate enemies)
    private int totalSpawns;
    private int amountSpawned;


    // current enemy value (combined basic equivalent) and number of enemies to spawn
    [Header("Enemy Value and Total")]
    public float enemyVal = 20f;
    public float enemiesCount = 20f;

    // how the round increase in difficulty (by percentage)
    [Header("Wave Increase")]
    public float enemyValIncrease = 1.02f;
    public float enemyIncrease = 1.01f;

    [HideInInspector]
    public GameObject[] enemies;

    // controls the UI (FOR TESTING ONLY)
    [Header("FOR TESTING, REMOVE LATER")]
    public UITextController UIT;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        // resets round number, spawns the first wave, and updates UI
        roundNumber = 1;
        time = 2f;
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

        time -= Time.deltaTime;

        // progress round if there are no enemies left
        if (enemies.Length == 0 && time <= 0)
        {

        }

        // FOR TESTING, REMOVE LATER
        // skips the round
        if (Input.GetKeyDown("b"))
        {
            if (enemies.Length >= totalSpawns) {
                foreach (GameObject i in enemies)
                {
                    Destroy(i.gameObject);
                }
                roundIncrease();

                DecideSpawns();

                time = 2f;
            }
        }
    }

    private void roundIncrease()
    {
        // increase the enemy value and cound by a percentage
        Mathf.CeilToInt(enemyVal *= enemyValIncrease);
        //Mathf.CeilToInt(enemiesCount *= enemyIncrease);

        // decreases the total value to prevent values ramping out of control
        if (roundNumber % 25 == 0)
        {
            Mathf.CeilToInt(enemyVal /= (enemyValIncrease * 1.5f));
            //Mathf.CeilToInt(enemiesCount /= (enemyIncrease * 1.5f));
        }

        DecideSpawnPoints();

        // increase round number and update text
        roundNumber++;
        UIT.UpdateRoundText(roundNumber);
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
            for (int i = 0; i < enemyCost.Length; i++)
            {
                //if (enemyCost[i] <= currVal / 2 && enCount > 0)
                if (enemyCost[i] <= currVal / 3)
                {
                    //Debug.Log("Spawning " + enemyTypes[i].name + " after " + ((i % 20) + 1) + " seconds");
                    StartCoroutine(SpawnObject(enemyTypes[i], 0.0f ) );
                    //Random.Range(0.5f, 10.0f)
                    currVal -= enemyCost[i];
                    //enCount--;
                    spawned = true;

                    enCount++;

                }
            }

            //if (!spawned && enCount > 0)
            //{
                //Debug.Log("Spawning basic after some seconds to fill cap");
                //StartCoroutine(SpawnObject(enemyTypes[enemyTypes.Length - 1], Random.Range(1f, 3f) ) );
                //currVal -= enemyCost[enemyCost.Length - 1];
                //enCount--;
            //}

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
             
        Vector3 sP = new Vector3(point.position.x + Random.Range(-4.0f, 4.0f), point.position.y, point.position.z + Random.Range(-3f, 3f));
        Instantiate(spawn, sP, Quaternion.identity);
    }

    /*
     * DECIDE SPAWNPOINTS
     */
    private void DecideSpawnPoints()
    {
        foreach (Transform t in spawnPoints)
        {
            t.gameObject.SetActive(true);
        }

        int[] points = new int[4];
        for (int i = 0; i < 4; i++)
        {
            do
            {
                points[i] = Random.Range(0, spawnPoints.Length);
            } while (points[i] == points[(i + 1) % points.Length] ||
                     points[i] == points[(i + 2) % points.Length] ||
                     points[i] == points[(i + 3) % points.Length]);
            //Debug.Log(points[i]);
        }

        foreach (int i in points)
        {
            spawnPoints[i].gameObject.SetActive(false);
        }
    }
}
