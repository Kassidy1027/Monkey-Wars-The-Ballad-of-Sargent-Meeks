using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemLaserGun : ShopItem
{
    public void Buy()
    {
        if (points.points >= price)
        {
            WeaponSwap ws = Player.GetComponent<WeaponSwap>();
            ws.weapon4 = true;
            points.UpdatePoints(price * -1);
            Destroy(this.gameObject);
        }
    }
}
