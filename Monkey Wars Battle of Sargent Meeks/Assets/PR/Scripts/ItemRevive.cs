using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRevive : ShopItem
{
    public GameObject reviveSprite;
    public void Buy()
    {
        if (points.points >= price)
        {
            reviveSprite.SetActive(true);
            Player.GetComponent<Health>().hasRevive = true;
            points.UpdatePoints(price * -1);
            PriceIncrease();
        }
    }
}
