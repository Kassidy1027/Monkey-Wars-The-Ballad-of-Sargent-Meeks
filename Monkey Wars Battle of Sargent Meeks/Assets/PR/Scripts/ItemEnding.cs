using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEnding : ShopItem
{
    public GameObject canvas;
    public Camera camera;
    public AudioListener audio;
    public Camera playerCam;
    public AudioListener playerAudio;
    public Canvas priceCanvas;
    public EscapeScene escaper;
    public PauseHandler pauseHandler;
    public TMPro.TMP_Text textBox;
    public GameObject deathScreen;
    public StatisticLogHandler stats;
    public void Buy()
    {
        if (points.points >= price)
        {
            Player.SetActive(false);
            canvas.SetActive(false);
            priceCanvas.enabled = false;
            camera.enabled = true;
            audio.enabled = true;
            playerCam.enabled = false;
            playerAudio.enabled = false;
            pauseHandler.canPause = false;
            deathScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            stats.WriteGameStats();
            textBox.text = "You Escaped!"; 
            points.UpdatePoints(price * -1);
            PriceIncrease();
            escaper.escaped = true;
        }
    }
}
