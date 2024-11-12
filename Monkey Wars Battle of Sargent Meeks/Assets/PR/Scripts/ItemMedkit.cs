using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMedkit : ShopItem
{
    public int amount;
    public UIHealthManager healthUI;
    public void Buy()
    {
        if (points.points >= price)
        {
            Health pH = Player.GetComponent<Health>();
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
