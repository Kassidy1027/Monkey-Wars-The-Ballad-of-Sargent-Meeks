using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDamageUp : ShopItem
{
    public void Buy()
    {
        if (points.points >= price)
        {
            WeaponSwap playerWeapon = Player.GetComponent<WeaponSwap>();
            playerWeapon.currentWeapon.power *= 2;
            points.UpdatePoints(price * -1);
            PriceIncrease();
        }
    }
}
