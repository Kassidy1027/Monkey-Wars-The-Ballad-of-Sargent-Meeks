using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ItemPistol : ShopItem
{
    public void Buy()
    {
        if (points.points >= price)
        {
            WeaponSwap ws = Player.GetComponent<WeaponSwap>();
            ws.weapon2 = true;
            points.UpdatePoints(price * -1);
            UpdateStatistics();
            Destroy(this.gameObject);
        }
    }
}
