using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthManager : MonoBehaviour
{
    public Health playerHealth;
    public Slider hpBar;
    public TMP_Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        hpBar.maxValue = playerHealth.maxHealth;
        hpBar.value = playerHealth.currentHealth;
        healthText.text = "HP: " + hpBar.value.ToString();
    }

    // Called when the halth value changes
    public void UpdateVals()
    {
        hpBar.value = playerHealth.currentHealth;
        healthText.text = "HP: " + hpBar.value.ToString();
    }
}
