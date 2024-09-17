using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBossDead : MonoBehaviour
{
    public Health bossHP;
    public MeshRenderer winText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHP.currentHealth <= 0)
        {
            winText.enabled = true;
        }
    }
}
