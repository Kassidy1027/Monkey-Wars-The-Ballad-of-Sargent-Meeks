using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMaxHealth : ShopItem
{
    public int amount;
    public UIHealthManager healthUI;
    public void Buy()
    {
        if (points.points >= price)
        {
            Health pH = Player.GetComponent<Health>();
            pH.maxHealth += amount;
            pH.currentHealth += amount;
            points.UpdatePoints(price * -1);
            healthUI.UpdateVals();
            PriceIncrease();
        }
    }
}
