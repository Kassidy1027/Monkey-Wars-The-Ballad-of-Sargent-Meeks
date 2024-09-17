using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCamScript : MonoBehaviour
{
    public Camera deathCam;
    public MeshRenderer deathText;
    public AudioListener deathCamAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (deathCam.enabled == true)
        {
            deathText.enabled = true;
            deathCamAudio.enabled = true;
            if (Input.GetKeyDown("return"))
            {
                SceneManager.LoadScene("MilitaryBase");
            }
        }
    }
}
