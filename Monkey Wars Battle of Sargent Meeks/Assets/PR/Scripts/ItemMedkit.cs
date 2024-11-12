using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMedkit : ShopItem
{
    public int amount;
    public UIHealthManager healthUI;
    public void Buy()
    {
        Health pH = Player.GetComponent<Health>();
        if (points.points >= price && pH.currentHealth < pH.maxHealth)
        {
            pH.currentHealth += amount;
            if (pH.currentHealth > pH.maxHealth)
            {
                pH.currentHealth = pH.maxHealth;
            }
            points.UpdatePoints(price * -1);
            healthUI.UpdateVals();
            UpdateStatistics();
            PriceIncrease();
        }
    }
}
