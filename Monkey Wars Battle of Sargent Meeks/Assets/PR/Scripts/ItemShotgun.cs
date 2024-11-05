using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShotgun : ShopItem
{
    public void Buy()
    {
        if (points.points >= price)
        {
            WeaponSwap ws = Player.GetComponent<WeaponSwap>();
            ws.weapon5 = true;
            points.UpdatePoints(price * -1);
            Destroy(this.gameObject);
        }
    }
}
