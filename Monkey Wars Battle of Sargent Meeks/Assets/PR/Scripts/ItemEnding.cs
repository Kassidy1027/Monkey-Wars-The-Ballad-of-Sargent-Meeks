using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEnding : ShopItem
{
    public void Buy()
    {
        if (points.points >= price)
        {
            GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
            points.UpdatePoints(price * -1);
            PriceIncrease();
        }
    }
}
