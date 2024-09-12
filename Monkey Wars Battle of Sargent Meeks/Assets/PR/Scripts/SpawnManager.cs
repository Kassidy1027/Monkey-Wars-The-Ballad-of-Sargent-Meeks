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

    // Start is called before the first frame update
    void Start()
    {
        // resets round number, spawns the first wave, and updates UI
        roundNumber = 1;
        DecideSpawns();
        UIT.UpdateText(Mathf.CeilToInt(enemiesCount), roundNumber);
    }

    // Update is called once per frame
    void Update()
    {
        // find all enemies in the scene
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // progress round if there are no enemies left
        if (enemies.Length == 0)
        {
            roundIncrease();
            DecideSpawns();
        }

        // FOR TESTING, REMOVE LATER
        // skips the round
        if (Input.GetKeyDown("b"))
        {
            foreach (GameObject i in enemies)
            {
                Destroy(i.gameObject);
            }
        }
    }

    private void roundIncrease()
    {
        // increase the enemy value and cound by a percentage
        Mathf.CeilToInt(enemyVal *= enemyValIncrease);
        Mathf.CeilToInt(enemiesCount *= enemyIncrease);

        // decreases the total value to prevent values ramping out of control
        if (roundNumber % 25 == 0)
        {
            Mathf.CeilToInt(enemyVal /= (enemyValIncrease * 1.5f));
        }

        // increase round number and update text
        roundNumber++;
        UIT.UpdateText(Mathf.CeilToInt(enemiesCount), roundNumber);
    }

    private void DecideSpawns()
    {
        int currVal = Mathf.CeilToInt(enemyVal);
        int enCount = Mathf.CeilToInt(enemiesCount);
        bool spawned = false;

        while (enCount > 0)
        {
            spawned = false;
            for (int i = 0; i < enemyCost.Length; i++)
            {
                if (enemyCost[i] <= currVal / 2 && enCount > 0)
                {
                    SpawnObject(enemyTypes[i]);
                    currVal -= enemyCost[i];
                    enCount--;
                    spawned = true;
                }
            }

            if (!spawned && enCount > 0)
            {
                SpawnObject(enemyTypes[enemyTypes.Length - 1]);
                currVal -= enemyCost[enemyCost.Length - 1];
                enCount--;
            }

        }

    }

    /*
     * SPAWN AN ENEMY AT A RANDOM POINT
     */
    private void SpawnObject(GameObject spawn)
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 sP = new Vector3(point.position.x + Random.Range(-3f, 3f), point.position.y, point.position.z + Random.Range(-3f, 3f));
        Instantiate(spawn, sP, Quaternion.identity);
    }
}
