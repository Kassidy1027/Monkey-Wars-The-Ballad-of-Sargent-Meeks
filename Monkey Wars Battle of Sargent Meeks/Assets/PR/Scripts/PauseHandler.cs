using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    public bool canPause;
    public bool paused;
    public GameObject pauseMenu;

    public static PauseHandler PS;

    private WeaponSwap ws;

    private void Awake()
    {
        if (PauseHandler.PS != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            PauseHandler.PS = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        canPause = true;
        paused = false;

        ws = GameObject.FindWithTag("Player").GetComponent<WeaponSwap>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (canPause)
            {
                paused = !paused;

                PauseMenu(paused);
            }
        }
    }

    public void PauseMenu(bool set)
    {
        pauseMenu.SetActive(set);

        if (set)
        {
            ws.currentWeapon.showCrosshair = false;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            ws.currentWeapon.showCrosshair = true;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Unpause()
    {
        pauseMenu.SetActive(false);
        ws.currentWeapon.showCrosshair = true;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
