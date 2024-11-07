using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ItemBombLauncher : ShopItem
{
    public void Buy()
    {
        if (points.points >= price)
        {
            WeaponSwap ws = Player.GetComponent<WeaponSwap>();
            ws.weapon3 = true;
            points.UpdatePoints(price * -1); 
            UpdateStatistics();
            Destroy(this.gameObject);
        }
    }
}
