using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactTrigger : MonoBehaviour
{
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            source.Play();
        }
        
    }

}