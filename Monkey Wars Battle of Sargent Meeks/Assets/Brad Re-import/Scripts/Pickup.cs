using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public WeaponSwap weaponswap;
    public MeshRenderer weaponModel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            weaponModel.enabled = false;
            if (tag == "Pistol")
            {
                weaponswap.weapon2 = true;
            }
            if (tag == "Cluster")
            {
                weaponswap.weapon3 = true;
            }
            if (tag == "Railgun")
            {
                weaponswap.weapon4 = true;
            }
        }
    }
}
