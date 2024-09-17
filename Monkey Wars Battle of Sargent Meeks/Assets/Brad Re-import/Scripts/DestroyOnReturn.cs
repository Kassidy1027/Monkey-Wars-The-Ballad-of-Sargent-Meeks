using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnReturn : MonoBehaviour
{
    public GameObject menuText;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            Destroy(menuText);
        }
    }
}
