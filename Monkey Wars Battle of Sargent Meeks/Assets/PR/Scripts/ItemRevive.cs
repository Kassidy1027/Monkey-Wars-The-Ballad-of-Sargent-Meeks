using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRevive : ShopItem
{
    public void Buy()
    {
        Player.GetComponent<Health>().hasRevive = true;
        PriceIncrease();
    }
}
