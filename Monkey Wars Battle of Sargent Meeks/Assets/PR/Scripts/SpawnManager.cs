using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Points")]
    public Transform[] spawnPoints;
    [Header("Enemy Types")]
    public GameObject[] enemyTypes;
    public int[] enemyCost;

    private int roundNumber = 1;

    public float enemyVal = 20f;
    public float enemiesCount = 20f;

    public float enemyValIncrease = 1.02f;
    public float enemyIncrease = 1.01f;

    public GameObject[] enemies;

    public UITextController UIT;

    // Start is called before the first frame update
    void Start()
    {
        roundNumber = 1;
        DecideSpawns();
        UIT.UpdateText(Mathf.CeilToInt(enemiesCount), roundNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            roundIncrease();
            DecideSpawns();
        }

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
        Mathf.CeilToInt(enemyVal *= enemyValIncrease);
        Mathf.CeilToInt(enemiesCount *= enemyIncrease);

        if (roundNumber % 25 == 0)
        {
            Mathf.CeilToInt(enemyVal /= (enemyValIncrease * 1.5f));
        }

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

    private void SpawnObject(GameObject spawn)
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 sP = new Vector3(point.position.x + Random.Range(-3f, 3f), point.position.y, point.position.z + Random.Range(-3f, 3f));
        Instantiate(spawn, sP, Quaternion.identity);
    }
}
