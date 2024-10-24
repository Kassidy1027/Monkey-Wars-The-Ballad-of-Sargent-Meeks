using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMaxHealth : ShopItem
{
    public int amount;
    public void Buy()
    {
        Health pH = Player.GetComponent<Health>();
        pH.maxHealth += amount;
        pH.currentHealth += amount;
        PriceIncrease();
    }
}
