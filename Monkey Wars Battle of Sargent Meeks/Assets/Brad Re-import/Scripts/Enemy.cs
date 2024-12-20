using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Boolean playerInRange;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.transform;
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 3 * Time.deltaTime);
            var lookDir = target.position - transform.position;
            lookDir.y = 0;
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }
}
