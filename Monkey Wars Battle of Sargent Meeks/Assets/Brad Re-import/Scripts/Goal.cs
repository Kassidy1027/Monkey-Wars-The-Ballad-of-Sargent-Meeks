using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private bool inHeli;
    private bool crashed;
    private float rotationTime;
    private float rideTime;
    public Transform target;
    public Transform crashTarget;
    public Transform player;
    public Camera heliCam;
    public BoxCollider trigger;
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        inHeli = false;
        crashed = false;
        rotationTime = 0;
        rideTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (inHeli && transform.position.y < 40)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 10, Space.World);
        }
        if (inHeli && transform.position.y > 40 && rotationTime < 5)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * -15, Space.World);
            rotationTime += Time.deltaTime;
        }
        if (inHeli && rotationTime >= 5)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 25 * Time.deltaTime);
            rideTime += Time.deltaTime;
        }
        if (inHeli && rideTime > 5)
        {
            inHeli = false;
            crashed = true;
            Instantiate(explosionPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
        if (crashed && transform.position.y > 0)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * -50, Space.World);
            transform.position = Vector3.MoveTowards(transform.position, crashTarget.position, 25 * Time.deltaTime);
        }
        if (crashed && transform.position.y <= 0)
        {
            player.position = new Vector3(230, 0, 315);
            crashed = false;
            heliCam.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            heliCam.enabled = true;
            inHeli = true;
            trigger.enabled = false;
            player.position = new Vector3(20, 0, 20);
        }
    }
}
